using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.ProductionAmplatsBonus
{
    public partial class ucSafetyNew : BaseUserControl
    {
        public ucSafetyNew()
        {
            InitializeComponent();
        }

        BindingSource bs = new BindingSource();
        BindingSource bs1 = new BindingSource();

        public void LoadOrgGrid()
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = "SELECT distinct(Orgunit) unit FROM [dbo].[tbl_Import_BCS_Personnel]  .0order by Orgunit ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            DataTable dtOrg = _dbMan.ResultsDataTable;

            bs.DataSource = dtOrg;

            OrgGrid.DataSource = bs;

            OrgGrid.Columns[0].Width = 140;

            OrgGrid.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void frmSafetyNew_Load(object sender, EventArgs e)
        {
            ProdMonthTxt.Text = Convert.ToString(SysSettings.ProdMonth);
            Procedures procs = new Procedures();
            procs.ProdMonthVis(Convert.ToInt32(ProdMonthTxt.Text));
            ProdMonth1Txt.Text = Procedures.Prod2;
            ProdMonth1Txt.TextAlign = HorizontalAlignment.Center;


            LoadOrgGrid();

            ShowBtn_Click(null, null);
        }

        private void ShowBtn_Click(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = "Select OrgUnit, RI, LTI, Fatal from tbl_BCS_SafetyCapture " +
                                  "Where ProdMonth = '" + ProdMonthTxt.Value + "' order by orgunit ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            DataTable dt = _dbMan.ResultsDataTable;

            bs1.DataSource = dt;

            grid.DataSource = bs1;

            grid.Columns[0].Width = 100;
            grid.Columns[1].Width = 60;
            grid.Columns[2].Width = 60;
            grid.Columns[3].Width = 60;
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
           
        }

        private void ProdMonthTxt_Click(object sender, EventArgs e)
        {
            Procedures procs = new Procedures();
            procs.ProdMonthCalc(Convert.ToInt32(ProdMonthTxt.Text));
            ProdMonthTxt.Text = Procedures.Prod.ToString();
            procs.ProdMonthVis(Convert.ToInt32(ProdMonthTxt.Text));
            ProdMonth1Txt.Text = Procedures.Prod2;
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            bs.Filter = "[Unit] <> 'bob'";

            if (txtFilter.Text == "")
                bs.Filter = bs.Filter;// + string.Format("and [Equipment Number] LIKE '{0}%'", '%');
            else
                bs.Filter = bs.Filter + string.Format(" and [Unit] LIKE '{0}%'", txtFilter.Text);
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = "Delete tbl_BCS_SafetyCapture Where ProdMonth = '" + ProdMonthTxt.Value + "' and OrgUnit = '" + OrgGrid.CurrentRow.Cells[0].Value.ToString() + "' " +
                                  "Insert into tbl_BCS_SafetyCapture values ( '" + OrgGrid.CurrentRow.Cells[0].Value.ToString() + "', '" + ProdMonthTxt.Value + "', " +
                                  " '" + txtRI.Text + "', '" + txtLti.Text + "', '" + txtFatal.Text + "' ) ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            frmMessage MsgFrm = new frmMessage();
            MsgFrm.Text = "Saved";
            MsgFrm.Text = "Saved Successfully";
            MsgFrm.Show();

            ShowBtn_Click(null, null);
        }
    }
}
