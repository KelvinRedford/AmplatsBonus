using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using FastReport;
using System.Net;
using FastReport.Utils;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;

namespace Mineware.Systems.ProductionAmplatsBonus
{
    public partial class frmProductionProp : XtraForm
    {
        public string _connection;
        public frmProductionProp()
        {
            InitializeComponent();
        }

        public void loadData()
        {
            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = _connection;
            _dbMan1.SqlStatement = "SELECT DISTINCT IndustryNumber + ' - ' + Surname + '.' + Initials AS EmployeeDetail FROM [Mineware].[dbo].[tbl_Import_BCS_Personnel_Latest]";
            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();

            DataTable dtEmp = _dbMan1.ResultsDataTable;
            luEmpDetail.Properties.DataSource = dtEmp;



        }

        private void insertValue_3Month()
        {
            MWDataManager.clsDataAccess _dbMan4 = new MWDataManager.clsDataAccess();
            _dbMan4.ConnectionString = _connection;
            _dbMan4.SqlStatement = " INSERT INTO [Mineware].[dbo].[tbl_BCS_Gangs_3Month] VALUES('1','" + tbMillMonth.Text + "','1','','" + tbOrg.Text + "','','','','" + luShift.EditValue.ToString() + "','','','','" + luCategory.Text.ToString() + "','" + luEmpDetail.EditValue.ToString().Substring(0, 8) + "','','','','','" + Environment.UserName + "','" + DateTime.Today + "','0','0','0','0','','')";
            _dbMan4.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan4.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan4.ExecuteInstruction();
        }

        public void insertValue()
        {
            MWDataManager.clsDataAccess _dbMan4 = new MWDataManager.clsDataAccess();
            _dbMan4.ConnectionString = _connection;
            _dbMan4.SqlStatement = " INSERT INTO [Mineware].[dbo].[tbl_BCS_Gangs] VALUES('1','" + tbMillMonth.Text + "','1','','" + tbOrg.Text + "','','','','" + luShift.EditValue.ToString() + "','','','','" + luCategory.Text.ToString() +"','" + luEmpDetail.EditValue.ToString().Substring(0, 8) + "','','','','','" + Environment.UserName + "','" + DateTime.Today + "','0','0','0','0','','')";
            _dbMan4.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan4.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan4.ExecuteInstruction();
        }

        public void lbfill()
        {
            MWDataManager.clsDataAccess _dbMan4 = new MWDataManager.clsDataAccess();
            _dbMan4.ConnectionString = _connection;
            _dbMan4.SqlStatement = "SELECT DISTINCT IndustryNumber + '-' + Category + '-' + Shift AS IndustryNumber FROM [Mineware].[dbo].[tbl_BCS_Gangs] WHERE OrgUnit = '" + tbOrg.Text + "' AND ProdMonth = '" + tbMillMonth.Text + "'";
            _dbMan4.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan4.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan4.ExecuteInstruction();

            DataTable dtcrews = _dbMan4.ResultsDataTable;

            lbGangs.DisplayMember = "IndustryNumber";
            lbGangs.ValueMember = "IndustryNumber";
            lbGangs.DataSource = dtcrews;
        }

        private void delGang_3Month()
        {
            MWDataManager.clsDataAccess _dbMan4 = new MWDataManager.clsDataAccess();
            _dbMan4.ConnectionString = _connection;
            _dbMan4.SqlStatement = "DELETE FROM [Mineware].[dbo].[tbl_BCS_Gangs_3Month] WHERE OrgUnit = '" + tbOrg.Text + "' AND ProdMonth = '" + tbMillMonth.Text + "' AND IndustryNumber = '" + ind + "' AND Category = '" + des + "' AND Shift = '" + shift + "'";
            _dbMan4.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan4.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan4.ExecuteInstruction();
        }

        public void delGang()
        {
            MWDataManager.clsDataAccess _dbMan4 = new MWDataManager.clsDataAccess();
            _dbMan4.ConnectionString = _connection;
            _dbMan4.SqlStatement = "DELETE FROM [Mineware].[dbo].[tbl_BCS_Gangs] WHERE OrgUnit = '" + tbOrg.Text + "' AND ProdMonth = '" + tbMillMonth.Text + "' AND IndustryNumber = '" + ind + "' AND Category = '" + des + "' AND Shift = '" + shift + "'";
            _dbMan4.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan4.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan4.ExecuteInstruction();

            alertControl1.Show(null, "Information", val + " Deleted Successfully");
        }

        private void luTeamGroup_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void frmProductionProp_Load(object sender, EventArgs e)
        {
            this.Icon = frmMain.ActiveForm.Icon;
            loadData();
            lbfill();

            luShift.Properties.DataSource = "";
            DataTable dt0 = new DataTable();
            dt0.Columns.Add("Shift");
            dt0.Rows.Add("D");
            dt0.Rows.Add("N");
            luShift.Properties.DataSource = dt0;

            if (tbOrg.Text.Length == 4)
            {
                //tbDesignation.Text = "Mine Overseer";
                DataTable dt = new DataTable();
                dt.Columns.Add("Designation");
                dt.Rows.Add("Mine Overseer");
                luCategory.Properties.DataSource = dt;
                luCategory.ItemIndex = 0;

            }
             if (tbOrg.Text.Length == 5)
            {
                luCategory.Properties.DataSource = "";
               // tbDesignation.Text = "ShiftBoss";
                DataTable dt = new DataTable();
                dt.Columns.Add("Designation");
                dt.Rows.Add("Shift Boss");
                luCategory.Properties.DataSource = dt;
                luCategory.ItemIndex = 0;
            }
             if (tbOrg.Text.Length >= 6)
            {
                luCategory.Properties.DataSource = "";
                // tbDesignation.Text = "ShiftBoss";
                DataTable dt = new DataTable();
                dt.Columns.Add("Designation");
                dt.Rows.Add("Stoper");
                dt.Rows.Add("Developer");
                dt.Rows.Add("Night Shift Cleaner");
                dt.Rows.Add("Special Team Leader Stoping");
                dt.Rows.Add("Stope Machine Operator");
                dt.Rows.Add("Stope Machine Assistant");
                dt.Rows.Add("Cleaning Specialist-Stope");
                dt.Rows.Add("Other");
                dt.Rows.Add("Special Team Leader Development");
                dt.Rows.Add("Development Machine Operator");
                dt.Rows.Add("Development Machine Assistant");
                dt.Rows.Add("Development");
                dt.Rows.Add("Loader Driver");
                luCategory.Properties.DataSource = dt;
                luCategory.ItemIndex = 0;
            }
            //finish


        }

        private void sbtnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                insertValue();
                insertValue_3Month();
                lbfill();

                luEmpDetail.SelectedText = "";
                //luDesignation.SelectedText = "";
                luCategory.SelectedText = "";
                luShift.SelectedText = "";
                alertControl1.Show(null, "Information", "Record Added Successfully");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        public string val = "";
        private string ind;
        private string des;
        private string shift;

        private void lbGangs_DoubleClick(object sender, EventArgs e)
        {
            val = lbGangs.SelectedValue.ToString();

            ind = val.Split('-')[0];
            des = val.Split('-')[1];
            shift = val.Split('-')[2];

            var message = "Are you sure you want to delete?";
            var caption = "Delete Notification";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Information;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons, icon);

            if (result == DialogResult.Yes)
            {
                delGang();
                delGang_3Month();
                lbfill();
            }
            else if (result == DialogResult.No)
            {

            }
        }

        private void luEmpDetail_EditValueChanged(object sender, EventArgs e)
        {
            //MWDataManager.clsDataAccess _dbMan3 = new MWDataManager.clsDataAccess();
            //_dbMan3.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //_dbMan3.SqlStatement = "SELECT DISTINCT Designation FROM [NorthamPas].[dbo].[Employee_All_New] WHERE EmployeeNo = '" + luEmpDetail.Text.Substring(0, 8) + "'";
            //_dbMan3.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //_dbMan3.queryReturnType = MWDataManager.ReturnType.DataTable;
            //_dbMan3.ExecuteInstruction();

            //DataTable dtDesignation = _dbMan3.ResultsDataTable;
            //luCategory.Properties.DataSource = dtDesignation;
            //if (dtDesignation.Rows.Count == 0)
            //{ }
            //else
            //{
            //    tbDesignation.Text = dtDesignation.Rows[0][0].ToString();
            //    var result = dtDesignation;
            //    luCategory.ItemIndex = 0;
            //}
        }

        private void luDesignation_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void tbDesignation_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
