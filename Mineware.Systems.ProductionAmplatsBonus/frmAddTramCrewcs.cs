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
using DevExpress.XtraEditors;
using Mineware.Systems.ProductionAmplatsGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.ProductionAmplatsBonus
{
    public partial class frmAddTramCrewcs : XtraForm
    {
        public string _connection;
        public frmAddTramCrewcs()
        {
            InitializeComponent();
        }

         public void LoadLvls()
        {

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = _connection;
                _dbMan.SqlStatement = " ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " Select * from (SELECT convert(decimal(18,0),substring(level,6,6)) order1,* ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " FROM [Mineware].[dbo].[tbl_BCS_Tramming_Levels] ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " where yearmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') a  order by order1, orgunit ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ExecuteInstruction();

                DataTable dtMain = _dbMan.ResultsDataTable;

                DataSet ds = new DataSet();

                ds.Tables.Add(dtMain);

                BookGrid.DataSource = ds.Tables[0];                

                colLevel.FieldName = "Level";
                ColOrg.FieldName = "OrgUnit";
                ColShift.FieldName = "Shift";
                ColMO.FieldName = "Section";


         }

       

        private void frmAddTramCrewcs_Load(object sender, EventArgs e)
        {
            editProdmonth.EditValue = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsDate(ProductionAmplatsGlobal.ProductionAmplatsGlobalTSysSettings._currentProductionMonth.ToString()); 

            //LoadLvls();

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = _connection;
            _dbMan.SqlStatement = " ";
            _dbMan.SqlStatement = _dbMan.SqlStatement + "select sectionid, name from mineware.dbo.tbl_BCS_SECTION where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";
            _dbMan.SqlStatement = _dbMan.SqlStatement + "and hierarchicalid = 4 order by sectionid ";

            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            DataTable dtMain = _dbMan.ResultsDataTable;
            LookUpEditSection.DataSource = dtMain;
            LookUpEditSection.ValueMember = "sectionid";
            LookUpEditSection.DisplayMember = "name";



            MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
            _dbMan2.ConnectionString = _connection;
            _dbMan2.SqlStatement = " ";
            _dbMan2.SqlStatement = _dbMan2.SqlStatement + "select distinct(level) aa, order1 from (SELECT convert(decimal(18,0),substring(level,6,6)) order1,* ";
            _dbMan2.SqlStatement = _dbMan2.SqlStatement + "FROM [Mineware].[dbo].[tbl_BCS_Tramming_Levels] ";
            _dbMan2.SqlStatement = _dbMan2.SqlStatement + ") a  order by order1 ";
            _dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan2.ExecuteInstruction();

            DataTable dtMain1 = _dbMan2.ResultsDataTable;
            LookUpEditLevel.DataSource = dtMain1;
            LookUpEditLevel.ValueMember = "aa";
            LookUpEditLevel.DisplayMember = "aa";

            editLevel.EditValue = 0;

            editShift.EditValue = 0;

        }

        private void ProdMonthTxt_Click(object sender, EventArgs e)
        {
            
        }

        private void LvlCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        DialogResult result;

        

        private void btnAddOrgunit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // check if exist

            result = MessageBox.Show("Are you sure you want to transfer the Bonus Details to the ARMS Interface?", "Transfer Record", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = _connection;
                _dbMan.SqlStatement = " ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "exec mineware.dbo.BMCS_Insert_Boxholes '" + editProdmonth.EditValue + "' ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + ",'" + editLevel.EditValue + "' , '" + lblNewCrew.Text + "', '" + editShift.EditValue + "' ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + ",'" + editSections.EditValue + "'  ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ExecuteInstruction();

                MessageBox.Show("Bonus Details was successfully transferred", "Transferred", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLvls();

            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void lblNewCrew_Click(object sender, EventArgs e)
        {

        }

        private void editProdmonth_EditValueChanged(object sender, EventArgs e)
        {


            LoadLvls();



            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = _connection;
            _dbMan.SqlStatement = " ";
            _dbMan.SqlStatement = _dbMan.SqlStatement + "select sectionid, name from mineware.dbo.tbl_BCS_SECTION where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";
            _dbMan.SqlStatement = _dbMan.SqlStatement + "and hierarchicalid = 4 order by sectionid ";

            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            DataTable dtMain = _dbMan.ResultsDataTable;
            LookUpEditSection.DataSource = dtMain;
            LookUpEditSection.ValueMember = "sectionid";
            LookUpEditSection.DisplayMember = "name";

            if (_dbMan.ResultsDataTable.Rows.Count > 0)
            {

                editSections.EditValue = _dbMan.ResultsDataTable.Rows[0][0].ToString();
            }
        }

        private void editLevel_EditValueChanged(object sender, EventArgs e)
        {
            if (editLevel.EditValue.ToString() != "")
            {
                if (editSections.EditValue.ToString() != "")
                {
                    string lvl = editLevel.EditValue.ToString();
                    lvl = lvl + "              ";
                    lvl = lvl.Substring(5, 4);
                    lvl = "0" + lvl.Trim();
                    lvl = lvl.Substring(lvl.Length - 2, 2);

                    string shift = "C";

                    if (editShift.EditValue.ToString() == "D")
                        shift = "A";
                    if (editShift.EditValue.ToString() == "A")
                        shift = "B";


                    lblNewCrew.Text = editSections.EditValue.ToString() + "T" + lvl + shift + AddTxt.Text;
                }
            }
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}
