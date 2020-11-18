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
using System.IO;
using DevExpress.XtraEditors;

namespace Mineware.Systems.ProductionAmplatsBonus
{
    public partial class frmCat2Capt : XtraForm
    {
        public string _connection;
        public frmCat2Capt()
        {
            InitializeComponent();
        }

        private void frmCat2Capt_Load(object sender, EventArgs e)
        {
            date1.Value = DateTime.Now;

            ProdMonthTxt.Text = Convert.ToString(SysSettings.ProdMonth);
                                    
            LoadMO();
            MOCombo.SelectedIndex = 0;

            LoadOrg();

            LoadGrid();
            
        }

        void GetMonth()
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = _connection;
            _dbMan.SqlStatement = "select max(p.prodmonth) pm from mineware.dbo.tbl_BCS_Planning p, mineware.dbo.tbl_BCS_Sections_Complete sc \r\n " +
                                  "where p.CalendarDate = '" + String.Format("{0:yyyy-MM-dd}", date1.Value) + "' \r\n " +
                                  "and p.Prodmonth = sc.Prodmonth \r\n "+
                                  "and p.SectionID = sc.SECTIONID \r\n " +
                                  "and sc.SECTIONID_2 = '"+MOCombo.Text+"' ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            if (_dbMan.ResultsDataTable.Rows.Count > 0)
            {
                ProdMonthTxt.Text = _dbMan.ResultsDataTable.Rows[0]["pm"].ToString();
            }

        }

        void LoadMO()
        {
            MOCombo.Items.Clear();

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = _connection;
            _dbMan.SqlStatement = "select distinct(sc.SECTIONID_2) MOSection from mineware.dbo.tbl_BCS_Planning p, mineware.dbo.tbl_BCS_Sections_Complete sc \r\n " +
                                  "where p.Prodmonth = '"+ProdMonthTxt.Text+"' \r\n "+
                                  "and p.Prodmonth = sc.Prodmonth \r\n " +
                                  "and p.SectionID = sc.SECTIONID";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            DataTable dt = _dbMan.ResultsDataTable;

            foreach (DataRow dr in dt.Rows)
            {
                MOCombo.Items.Add(dr["MOSection"].ToString());
            }

        }

        void LoadOrg()
        {
            OrgCombo.Items.Clear();

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = _connection;
            _dbMan.SqlStatement = "Select distinct Substring(OrgUnit,1,8) as OrgUnit from BMCS_AllOrgUnits \r\n "+
                                  "Where len(OrgUnit) > 7 and OrgUnit like '"+MOCombo.Text+"%'";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            DataTable dt = _dbMan.ResultsDataTable;

            foreach (DataRow dr in dt.Rows)
            {
                OrgCombo.Items.Add(dr["OrgUnit"].ToString());
            }
        }

        void LoadActivity()        
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = _connection;
            _dbMan.SqlStatement = "select CONVERT(int,Activity ) Activity from mineware.dbo.tbl_BCS_Planmonth \r\n " +
                                  "where Prodmonth = '"+ProdMonthTxt.Text+"' \r\n "+
                                  "and SUBSTRING(Sectionid,1,4) = '"+OrgCombo.Text.Substring(0,4)+"' \r\n " +
                                  "group by Activity";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            if (_dbMan.ResultsDataTable.Rows.Count > 0)
            {
                if (_dbMan.ResultsDataTable.Rows[0][0].ToString() == "1")
                {
                    txtActivity.Text = "Development";
                    lblActCode.Text = "1";
                }
                else
                {
                    txtActivity.Text = "Stoping";
                    lblActCode.Text = "0";
                }
            }

        }

        void LoadShift()
        {
            txtShift.Text = "Day";

            if (OrgCombo.Text.Length > 7)
            {
                if (OrgCombo.Text.Substring(6, 1) == "M")
                    txtShift.Text = "Day";
                if (OrgCombo.Text.Substring(6, 1) == "U")
                    txtShift.Text = "Day";

                if (OrgCombo.Text.Substring(6, 1) == "P")
                    txtShift.Text = "Night";
                if (OrgCombo.Text.Substring(6, 1) == "N")
                    txtShift.Text = "Night";
                if (OrgCombo.Text.Substring(6, 1) == "T")
                    txtShift.Text = "Night";
                if (OrgCombo.Text.Substring(6, 1) == "G")
                    txtShift.Text = "Night";
            }

        }

        void LoadGrid()
        {
            
            int q = 0;

            if (OrgCombo.Text.Length > 7)
            {
                //get correct drop down items
                #region

                if (((OrgCombo.Text.Substring(6, 1) == "Q") || (OrgCombo.Text.Substring(6, 1) == "D")) && (OrgCombo.Text.Substring(4, 1) == "A"))
                    q = 1;
                if (((OrgCombo.Text.Substring(7, 1) == "Q") || (OrgCombo.Text.Substring(7, 1) == "D")) && (OrgCombo.Text.Substring(4, 1) == "T"))
                    q = 1;

                if ((OrgCombo.Text.Substring(7, 1) == "A") && (OrgCombo.Text.Substring(4, 1) == "T"))
                    q = 1;
                if ((OrgCombo.Text.Substring(7, 1) == "B") && (OrgCombo.Text.Substring(4, 1) == "T"))
                    q = 1;
                if ((OrgCombo.Text.Substring(7, 1) == "F") && (OrgCombo.Text.Substring(4, 1) == "T"))
                    q = 1;
                if ((OrgCombo.Text.Substring(7, 1) == "G") && (OrgCombo.Text.Substring(4, 1) == "T"))
                    q = 1;
                if ((OrgCombo.Text.Substring(7, 1) == "E") && (OrgCombo.Text.Substring(4, 1) == "T"))
                    q = 1;
                if ((OrgCombo.Text.Substring(7, 1) == "M") && (OrgCombo.Text.Substring(4, 1) == "T"))
                    q = 1;
                if ((OrgCombo.Text.Substring(7, 1) == "C") && (OrgCombo.Text.Substring(4, 1) == "T"))
                    q = 1;

                if (OrgCombo.Text.Substring(4, 2) == "T6")
                    q = 1;
                if (OrgCombo.Text.Substring(4, 2) == "T7")
                    q = 1;

                if ((OrgCombo.Text.Substring(4, 2) == "T3") && (txtActivity.Text == "Development"))
                    q = 2;


                //get info for drop downs in query
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = _connection;

                if ((txtActivity.Text == "Stoping") && (q == 0))
                {
                    
                    _dbMan.SqlStatement = "select 1 num, 'Special Team Leader Stoping' designation \r\n " +
                                          "union \r\n " +
                                          "select 2 num, 'Stope Machine Operator' designation  \r\n " +
                                          "union \r\n " +
                                          "select 3 num, 'Stope Machine Asst' designation  \r\n " +
                                          "union  \r\n " +
                                          "select 4 num, 'Asst Construction Gr 1 - Stoping' designation  \r\n " +
                                          "union  \r\n " +
                                          "select 5 num, 'Cleaning Specialist-Stope' designation  \r\n " +
                                          "union \r\n " +
                                          "select 6 num, 'Stoping' designation  \r\n " +
                                          "order by num";
                }
                else if (q == 1)
                {
                    _dbMan.SqlStatement = "select 1 num, 'Team Leader' designation \r\n " +
                                          "union \r\n " +
                                          "select 2 num, 'Other' designation";
                }
                else if (q == 2)
                {
                    _dbMan.SqlStatement = "select 1 num, 'Team Leader' designation  \r\n " +
                                          "union  \r\n " +
                                          "select 2 num, 'Loco Drivers' designation  \r\n " +
                                          "union  \r\n " +
                                          "select 3 num, 'Loader Driver' designation  \r\n " +
                                          "union  \r\n " +
                                          "select 4 num, 'Other' designation";
                }
                else
                {
                    _dbMan.SqlStatement = "select 1 num, 'Special Team Leader Development' designation  \r\n " +
                                          "union \r\n " +
                                          "select 2 num, 'Development Machine Operator' designation  \r\n " +
                                          "union \r\n " +
                                          "select 3 num, 'Development Machine Asst' designation \r\n " +
                                          "union \r\n " +
                                          "select 4 num, 'Loader Driver' designation \r\n " +
                                          "union  \r\n " +
                                          "select 5 num, 'Development' designation \r\n " +
                                          "union  \r\n " +
                                          "select 6 num, 'Asst Gr 1 Construction - Development' designation \r\n " +
                                          "union \r\n " +
                                          "select 7 num, 'Loco Driver Dies' designation  \r\n " +
                                          "order by num";
                }
                
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ExecuteInstruction();

                DataTable dt = _dbMan.ResultsDataTable;

                grid.RowCount = 17;


                DataGridViewColumn newColSub = new DataGridViewColumn();
                DataGridViewComboBoxCell cellSub = new DataGridViewComboBoxCell();
                newColSub.CellTemplate = cellSub;

                foreach (DataRow dr in dt.Rows)
                {
                    cellSub.Items.Add(dr["designation"].ToString());
                }

                newColSub.HeaderText = "Category";
                newColSub.Name = "Asset";
                newColSub.Visible = true;
                newColSub.Width = 210;
                cellSub.Style.BackColor = Color.White;
                cellSub.FlatStyle = FlatStyle.Flat ;
                grid.Columns.Add(newColSub);

                grid.ColumnCount = 3;
                grid.Columns[0].Visible = false;

                grid.Columns[2].HeaderText = "Industry Number";
                grid.Columns[2].Width = 135;

                DataGridViewColumn newColSub1 = new DataGridViewColumn();
                DataGridViewComboBoxCell cellSub1 = new DataGridViewComboBoxCell();
                newColSub1.CellTemplate = cellSub1;

                cellSub1.Items.Add(" ");
                cellSub1.Items.Add("N");
                cellSub1.Items.Add("S");
                cellSub1.Items.Add("DR");
                cellSub1.Items.Add("A");
                cellSub1.Items.Add("AC");
                cellSub1.Items.Add("L");
                cellSub1.Items.Add("LU");
                cellSub1.Items.Add("NS");

                newColSub1.HeaderText = "Shift Code";
                newColSub1.Name = "Asset";
                newColSub1.Visible = true;
                newColSub1.Width = 80;
                cellSub1.Style.BackColor = Color.White;
                cellSub1.FlatStyle = FlatStyle.Flat;
                grid.Columns.Add(newColSub1);

                #endregion                
            }

        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OrgCombo_TextChanged(object sender, EventArgs e)
        {
            if (OrgCombo.Text.Length > 3)
            {
                LoadActivity();
                LoadShift();
                LoadGrid();
            }
            
        }

        private void date1_CloseUp(object sender, EventArgs e)
        {
            GetMonth();
            LoadMO();
            LoadOrg();
        }

        private void MOCombo_TextChanged(object sender, EventArgs e)
        {
            LoadOrg();
        }

        private void LogonBtn_Click(object sender, EventArgs e)
        {

        }

        private void Close1Btn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
