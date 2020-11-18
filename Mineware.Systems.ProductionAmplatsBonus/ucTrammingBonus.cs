using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using FastReport;
using System.Text.RegularExpressions;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;
using Mineware.Systems.ProductionAmplatsGlobal;

namespace Mineware.Systems.ProductionAmplatsBonus
{
    public partial class ucTrammingBonus : BaseUserControl
    {
        Report report = new Report();
        private string _reportFolder = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\Reports\";
        public ucTrammingBonus()
        {
            InitializeComponent();
            FormRibbonPages.Add(rpReports);
            FormActiveRibbonPage = rpReports;
            FormMainRibbonPage = rpReports;
            RibbonControl = rcReports;
        }


        string Shift = "";

        public void LoadListBoxes()
        {



            if (rdbDS.Checked == true)
            {
                Shift = "D";

                lbIncomplete.Items.Clear();

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = "  select distinct(workingorgunit) WO from tbl_BCS_Tramming_Gang_3Month \r\n " +
                                      " where YearMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  \r\n " +
                                      " and TypeShift = '" + Shift + "' and workingorgunit not in (select distinct(orgunit) o from mineware.dbo.[tbl_BCS_ARMS_Interface_Transfer_TramNewCalcAccuTrackNewCalc] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ) \r\n " +
                                      " order by workingorgunit \r\n " +
                                      "  \r\n " +
                                      " ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Stoping";
                _dbMan.ExecuteInstruction();

                DataTable dt = _dbMan.ResultsDataTable;

                lbIncomplete.Items.Add("");
                foreach (DataRow dr in dt.Rows)
                {
                    lbIncomplete.Items.Add(dr["WO"].ToString());
                }


                lbTransfer.Items.Clear();

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = "  select distinct(orgunit) WO from mineware.dbo.[tbl_BCS_ARMS_Interface_Transfer_TramNewCalcAccuTrackNewCalc] \r\n " +
                                      " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  \r\n " +
                                      " and Shift = '" + Shift + "'  \r\n " +
                                      " order by orgunit \r\n " +
                                      "  \r\n " +
                                      " ";
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ResultsTableName = "Stoping";
                _dbMan1.ExecuteInstruction();

                DataTable dt1 = _dbMan1.ResultsDataTable;

                lbTransfer.Items.Add("");
                foreach (DataRow dr in dt1.Rows)
                {
                    lbTransfer.Items.Add(dr["WO"].ToString());
                }
            }

            if (rdbAS.Checked == true)
            {
                Shift = "A";

                lbIncomplete.Items.Clear();

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = "  select distinct(workingorgunit) WO from tbl_BCS_Tramming_Gang_3Month \r\n " +
                                      " where YearMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  \r\n " +
                                      " and TypeShift = '" + Shift + "' and workingorgunit not in (select distinct(orgunit) o from mineware.dbo.[tbl_BCS_ARMS_Interface_Transfer_TramNewCalcAccuTrackNewCalc] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "')  \r\n " +
                                      " order by workingorgunit \r\n " +
                                      "  \r\n " +
                                      " ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Stoping";
                _dbMan.ExecuteInstruction();

                DataTable dt = _dbMan.ResultsDataTable;

                lbIncomplete.Items.Add("");
                foreach (DataRow dr in dt.Rows)
                {
                    lbIncomplete.Items.Add(dr["WO"].ToString());
                }

                lbTransfer.Items.Clear();

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = "  select distinct(orgunit) WO from mineware.dbo.[tbl_BCS_ARMS_Interface_Transfer_TramNewCalcAccuTrackNewCalc] \r\n " +
                                      " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  \r\n " +
                                      " and Shift = '" + Shift + "'  \r\n " +
                                      " order by orgunit \r\n " +
                                      "  \r\n " +
                                      " ";
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ResultsTableName = "Stoping";
                _dbMan1.ExecuteInstruction();

                DataTable dt1 = _dbMan1.ResultsDataTable;

                lbTransfer.Items.Add("");
                foreach (DataRow dr in dt1.Rows)
                {
                    lbTransfer.Items.Add(dr["WO"].ToString());
                }
            }

            if (rdbNS.Checked == true)
            {
                Shift = "N";

                lbIncomplete.Items.Clear();

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = "  select distinct(workingorgunit) WO from tbl_BCS_Tramming_Gang_3Month \r\n " +
                                      " where YearMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  \r\n " +
                                      " and TypeShift = '" + Shift + "' and workingorgunit not in (select distinct(orgunit) o from mineware.dbo.[tbl_BCS_ARMS_Interface_Transfer_TramNewCalcAccuTrackNewCalc] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "')  \r\n " +
                                      " order by workingorgunit \r\n " +
                                      "  \r\n " +
                                      " ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Stoping";
                _dbMan.ExecuteInstruction();

                DataTable dt = _dbMan.ResultsDataTable;

                lbIncomplete.Items.Add("");
                foreach (DataRow dr in dt.Rows)
                {
                    lbIncomplete.Items.Add(dr["WO"].ToString());
                }

                lbTransfer.Items.Clear();

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = "  select distinct(orgunit) WO from mineware.dbo.[tbl_BCS_ARMS_Interface_Transfer_TramNewCalcAccuTrackNewCalc] \r\n " +
                                      " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  \r\n " +
                                      " and Shift = '" + Shift + "'  \r\n " +
                                      " order by orgunit \r\n " +
                                      "  \r\n " +
                                      " ";
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ResultsTableName = "Stoping";
                _dbMan1.ExecuteInstruction();

                DataTable dt1 = _dbMan1.ResultsDataTable;

                lbTransfer.Items.Add("");
                foreach (DataRow dr in dt1.Rows)
                {
                    lbTransfer.Items.Add(dr["WO"].ToString());
                }
            }
        }
        string Mindate = "";
        string Maxdate = "";

        private void showBtn_Click(object sender, EventArgs e)
        {
             
            
        }

        private void lbIncomplete_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblOrgunit.Text = lbIncomplete.SelectedItem.ToString();
        }

        private void lbPrinted_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblOrgunit.Text = lbPrinted.SelectedItem.ToString();
        }

        private void lbTransfer_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblOrgunit.Text = lbTransfer.SelectedItem.ToString();
        }

        private void frmTrammingBonus_Load(object sender, EventArgs e)
        {
            editProdmonth.EditValue = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsDate(ProductionAmplatsGlobalTSysSettings._currentProductionMonth.ToString());
        }

     
        private void ProdMonth1Txt_TextChanged(object sender, EventArgs e)
        {
            


        }

        private void Close1Btn_Click(object sender, EventArgs e)
        {
           
        }

        private void rdbAS_Click(object sender, EventArgs e)
        {
            LoadListBoxes();
        }

        private void rdbNS_Click(object sender, EventArgs e)
        {
            LoadListBoxes();
        }

        private void rdbDS_Click(object sender, EventArgs e)
        {
            LoadListBoxes();
        }

        private void DataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            


        }

        private void btnShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataSet ReportTramBonus = new DataSet();


            string lvl = "";

            if (lblOrgunit.Text.Substring(5, 1) == "1" && lblOrgunit.Text.Substring(5, 2) != "10" && lblOrgunit.Text.Substring(5, 2) != "12" && lblOrgunit.Text.Substring(5, 2) != "13" && lblOrgunit.Text.Substring(5, 2) != "14" && lblOrgunit.Text.Substring(5, 2) != "15" && lblOrgunit.Text.Substring(5, 2) != "16" && lblOrgunit.Text.Substring(5, 2) != "17" && lblOrgunit.Text.Substring(5, 2) != "18" && lblOrgunit.Text.Substring(5, 2) != "19")
                lvl = "3";

            if (lblOrgunit.Text.Substring(5, 2) == "01")
                lvl = "3";

            if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) >= 201804)
            {
                if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) <= 201805)
                {
                    if (lblOrgunit.Text.Substring(5, 1) == "2")
                        lvl = "3";
                    if (lblOrgunit.Text.Substring(5, 2) == "02")
                        lvl = "3";
                }
                else
                {

                    if (lblOrgunit.Text.Substring(5, 1) == "2")
                        lvl = "2";
                    if (lblOrgunit.Text.Substring(5, 2) == "02")
                        lvl = "2";

                }
            }
            else
            {
                if (lblOrgunit.Text.Substring(5, 1) == "2")
                    lvl = "2";
                if (lblOrgunit.Text.Substring(5, 2) == "02")
                    lvl = "2";

            }

            if (lblOrgunit.Text.Substring(5, 1) == "3")
                lvl = "3";
            if (lblOrgunit.Text.Substring(5, 2) == "03")
                lvl = "3";

            if (lblOrgunit.Text.Substring(5, 1) == "4")
                lvl = "4";
            if (lblOrgunit.Text.Substring(5, 2) == "04")
                lvl = "4";

            if (lblOrgunit.Text.Substring(5, 1) == "5")
                lvl = "5";
            if (lblOrgunit.Text.Substring(5, 2) == "05")
                lvl = "5";

            if (lblOrgunit.Text.Substring(5, 1) == "6")
                lvl = "6";
            if (lblOrgunit.Text.Substring(5, 2) == "06")
                lvl = "6";

            if (lblOrgunit.Text.Substring(5, 1) == "7")
                lvl = "7";
            if (lblOrgunit.Text.Substring(5, 2) == "07")
                lvl = "7";

            if (lblOrgunit.Text.Substring(5, 1) == "8")
                lvl = "8";
            if (lblOrgunit.Text.Substring(5, 2) == "08")
                lvl = "8";

            if (lblOrgunit.Text.Substring(5, 1) == "9")
                lvl = "9";
            if (lblOrgunit.Text.Substring(5, 2) == "09")
                lvl = "9";


            if (lblOrgunit.Text.Substring(5, 2) == "10")
                lvl = "10";
            if (lblOrgunit.Text.Substring(5, 2) == "11")
                lvl = "11";
            if (lblOrgunit.Text.Substring(5, 2) == "12")
                lvl = "12";
            if (lblOrgunit.Text.Substring(5, 2) == "13")
                lvl = "13";
            if (lblOrgunit.Text.Substring(5, 2) == "14")
                lvl = "14";
            if (lblOrgunit.Text.Substring(5, 2) == "15")
                lvl = "15";
            if (lblOrgunit.Text.Substring(5, 2) == "16")
                lvl = "16";
            if (lblOrgunit.Text.Substring(5, 2) == "17")
                lvl = "17";
            if (lblOrgunit.Text.Substring(5, 2) == "18")
                lvl = "18";
            if (lblOrgunit.Text.Substring(5, 2) == "19")
                lvl = "19";

            //return;





            //if (lblOrgunit.Text.Length == 7)
            // lvl =lblOrgunit.Text.Substring(5, 1);
            //else
            //  lvl = lblOrgunit.Text.Substring(5, 2);


            //if (lblOrgunit.Text == "0123T9C2")
            //    lvl = "9";

            //if (lblOrgunit.Text == "0125T8C1")
            //    lvl = "8";


            //if (lblOrgunit.Text == "0742T4C1")
            //    lvl = "4";


            //if (lblOrgunit.Text == "0123T9A1")
            //    lvl = "9";

            //if (lblOrgunit.Text == "0743T1C1")
            //    lvl = "1";


            //if (lblOrgunit.Text == "0730T3A1")
            //    lvl = "3";

            //if (lblOrgunit.Text == "0730T3B1")
            //    lvl = "3";

            //if (lblOrgunit.Text == "0730T3C1")
            //    lvl = "3";

            //if (lblOrgunit.Text == "0730T4A1")
            //    lvl = "4";


            // satety

            decimal safety = Convert.ToDecimal(1.2);



            MWDataManager.clsDataAccess _dbManSafety2 = new MWDataManager.clsDataAccess();
            _dbManSafety2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManSafety2.SqlStatement = "select isnull(sum(tt),0) tt from (select ri+lti+fatal tt from tbl_BCS_SafetyCapture " +
                                        "where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunit = '" + lblOrgunit.Text + "') a";
            _dbManSafety2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManSafety2.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManSafety2.ResultsTableName = "Safety";
            _dbManSafety2.ExecuteInstruction();

            if (_dbManSafety2.ResultsDataTable.Rows[0]["tt"].ToString() != "0")
                safety = Convert.ToDecimal(0.5);


            // do grid
            DataGrid.Rows.Clear();
            //DataGrid.RowCount = 0;
            DataGrid.ColumnCount = 50;
            DataGrid.RowCount = 200;

            DataGrid.Columns[0].HeaderText = "Prodmonth";
            DataGrid.Columns[0].Width = 50;

            DataGrid.Columns[1].HeaderText = "Org";
            DataGrid.Columns[1].Width = 120;

            DataGrid.Columns[2].HeaderText = "Lvl";
            DataGrid.Columns[2].Width = 50;

            DataGrid.Columns[3].HeaderText = "Shift";
            DataGrid.Columns[3].Width = 50;

            DataGrid.Columns[4].HeaderText = "Cat";
            DataGrid.Columns[4].Width = 80;

            DataGrid.Columns[5].HeaderText = "Desig";
            DataGrid.Columns[5].Width = 80;

            DataGrid.Columns[6].HeaderText = "Ind";
            DataGrid.Columns[6].Width = 80;

            DataGrid.Columns[7].HeaderText = "hoppers";
            DataGrid.Columns[7].Width = 80;

            DataGrid.Columns[8].HeaderText = "Lti";
            DataGrid.Columns[8].Width = 80;

            DataGrid.Columns[9].HeaderText = "Bonus";
            DataGrid.Columns[9].Width = 80;

            DataGrid.Columns[10].HeaderText = "Type";
            DataGrid.Columns[10].Width = 80;



            DataGrid.Columns[11].HeaderText = "Blanc";
            DataGrid.Columns[11].Width = 80;

            DataGrid.Columns[12].HeaderText = "Add N";
            DataGrid.Columns[12].Width = 80;

            DataGrid.Columns[13].HeaderText = "Date";
            DataGrid.Columns[13].Width = 80;

            DataGrid.Columns[14].HeaderText = "Blanc";
            DataGrid.Columns[14].Width = 80;

            DataGrid.Columns[15].HeaderText = "Status";
            DataGrid.Columns[15].Width = 80;




            MWDataManager.clsDataAccess _dbManDataGenData = new MWDataManager.clsDataAccess();
            _dbManDataGenData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            // get pot
            if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) < 201604)
            {

                //MWDataManager.clsDataAccess _dbManDataGenData = new MWDataManager.clsDataAccess();
                //_dbManDataGenData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManDataGenData.SqlStatement = "declare @lvl int   \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "declare @prodmonth varchar(10)  \r\n ";

                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "declare @Htons1 int \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "declare @M2 int \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "declare @M3 int \r\n ";

                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "set @lvl = '" + lvl + "'  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "set @prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";


                // get reef tons hoisted
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "set @Htons1 = (select sum(milltons)+sum(stackertons) a from mineware.dbo.[vw_bcs_survey]mill where prodmonth = @prodmonth) \r\n ";

                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "set @Htons1 = @Htons1 - (select sum(stackertons) b from mineware.dbo.[vw_bcs_survey]mill where  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "prodmonth = (select max(prodmonth) from mineware.dbo.tbl_BCS_Planmonth where prodmonth < @prodmonth)) \r\n ";

                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "set @Htons1 = @Htons1 + (select sum(ptons)-sum(ttons) a from mineware.dbo.[vw_bcs_survey]_mud where prodmonth = @prodmonth) \r\n ";


                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "select *, convert(decimal(18,0),lvltons)/(convert(decimal(18,9),tottons)+0.00000001) *(hoisttons*rate) * (per2/100) LocoPot \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + " ,convert(decimal(18,0),lvltons)/(convert(decimal(18,9),tottons)+0.00000001) *(hoisttons*rate) * (per1/100) tlPot \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + " ,convert(decimal(18,0),lvltons)/(convert(decimal(18,9),tottons)+0.00000001) *(hoisttons*rate) * (per3/100) LoaderPot \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + " ,0 TransTLPot \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + " ,convert(decimal(18,0),lvltons)/(convert(decimal(18,9),tottons)+0.00000001) *(hoisttons*rate) * (per4/100) TransTLPotOther \r\n ";

                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + " ,convert(decimal(18,0),lvltons)/(convert(decimal(18,9),tottons)+0.00000001) *(hoisttons*rate) * (per5/100) ldPotOther \r\n ";

                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + " ,convert(decimal(18,0),lvltons)/(convert(decimal(18,9),tottons)+0.00000001) *(hoisttons*rate) * (per6/100) finPotOther \r\n ";





                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + " from ( \r\n ";

                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "select * from (select isnull(sum(lvltons),0) lvltons,  isnull(sum(facetons+vamptons+devtons+osstons),0) tottons,  isnull(@Htons1,0) hoisttons from ( \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "select lvl, Sum(facetons) facetons, Sum(vampTons) vampTons, Sum(devtons) devtons, \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "Sum(osstons) osstons , sum(lvltons) lvltons, sum(facetons+vamptons+devtons+osstons) tottons \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + " from (  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "select case when LevelNumber IS not null then LevelNumber else lvl1 end as Lvl,  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "*, case when LevelNumber = @lvl then facetons+vamptons+devtons+osstons else 0 end as lvltons from  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "(  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "select pm.oreflowid, o.levelnumber lvl1,case when pm.activity = 0 then squaremetrestotal*stopewidth*convertedcubics/100 else 0  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "end as facetons,  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "socketwidth vampTons, case when pm.activity = 1 and w.reefwaste = 'R' then  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "((metrestotal * measwidth *measheight) + (reefcubics+wastecubics))*convertedcubics else 0 end as devtons,  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "case when pm.activity = 0  then  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "( (reefcubics+wastecubics))*convertedcubics else 0 end as osstons  from mineware.dbo.tbl_BCS_Planmonth pm,  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "mineware.dbo.[vw_bcs_survey] ss, mineware.dbo.tbl_bcs_Workplace w, mineware.dbo.oreflowentities o  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "where pm.workplaceid = w.workplaceid and w.oreflowid = o.oreflowid and pm.workplaceid = ss.workplaceid and pm.sectionid = ss.sectionid \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "and pm.activity = ss.activitycode and pm.Prodmonth = ss.PRODMONTH and pm.Prodmonth = @prodmonth)a \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "left outer join  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "(  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "select o.OreFlowID OreFlowID1, o1.LevelNumber from mineware.dbo.oreflowentities o, mineware.dbo.oreflowentities o1  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "where  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "o.ParentOreFlowID = o1.OreFlowID and  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "o.OreFlowCode = 'BH') b on a.oreflowid = b.oreflowid1) a group by lvl ) a) a, \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "(select top(1) rate from tbl_bcs_hoist where prodmonth <= @prodmonth \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "order by prodmonth desc) b , \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "(select top(1) * from tbl_bcs_trammingper where prodmonth <= @prodmonth \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "order by prodmonth desc) c) a \r\n ";
                //_dbManDataGenData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                //_dbManDataGenData.queryReturnType = MWDataManager.ReturnType.DataTable;
                //_dbManDataGenData.ResultsTableName = "Data";
                //_dbManDataGenData.ExecuteInstruction();
            }
            else
            {

                //MWDataManager.clsDataAccess _dbManDataGenData = new MWDataManager.clsDataAccess();
                //_dbManDataGenData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManDataGenData.SqlStatement = "declare @pm varchar(10) set @pm = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'   \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "declare @lvl varchar(10) set @lvl = '" + lvl + "'  \r\n ";



                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "declare @reef varchar(10)  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "set @reef = ( select   \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "case when @lvl = '1' then 'Both'   \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "when @lvl = '2' then 'Both'  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "when mer = 'Y' and ug2 = '' then 'Mer'  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "when ug2 = 'Y' and mer = '' then 'Ug2'  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "when ug2 = 'Y' and mer = 'Y' then 'Both'  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "end as reef  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + " from mineware.[dbo].[tbl_Survey_Bonus_MOReefTypeLink] where prodmonth = @pm  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "and mo = '" + lblOrgunit.Text.Substring(0, 4) + "')  \r\n ";


                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "select *  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + ", pot * (per2/100) LocoPot   \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "  , pot * (per1/100) tlPot   \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "  , pot * (per3/100) LoaderPot   \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "  ,0 TransTLPot   \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "  , pot * (per4/100) TransTLPotOther  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "  , pot * (per5/100) ldPotOther   \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "  , pot * (per6/100) finPotOther   \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + " from (  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "select tottons lvltons, @reef reef,   \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "case when @reef = 'Mer' then merpot   \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + " when @reef = 'Ug2' then ug2pot  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + " when @reef = 'Both' then totpot  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "end as pot ,  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "a.* from mineware.[dbo].[tbl_Survey_Bonus_trammingFiguresNew] a  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "left outer join \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "mineware.[dbo].[tbl_Survey_Bonus_TrammingFiguresNewTotal] b on a.prodmonth = b.prodmonth and a.Level = b.Level \r\n ";


                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + " where a.prodmonth = @pm  \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "and a.level = @lvl) a,  \r\n ";


                //_dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "(select top(1) * from bmcs_trammingper where prodmonth <= @pm   \r\n ";

                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "(select top(1) * from mineware.[dbo].tbl_bcs_trammingpernew where prodmonth <= @pm   \r\n ";
                _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + " order by prodmonth desc) b  \r\n ";

                //_dbManDataGenData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                //_dbManDataGenData.queryReturnType = MWDataManager.ReturnType.DataTable;
                //_dbManDataGenData.ResultsTableName = "Data";
                //_dbManDataGenData.ExecuteInstruction();

            }

            _dbManDataGenData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManDataGenData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManDataGenData.ResultsTableName = "Data";
            _dbManDataGenData.ExecuteInstruction();


            string reef = "Mer";
            if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) >= 201604)
                reef = _dbManDataGenData.ResultsDataTable.Rows[0]["Reef"].ToString();

            LocoDriverPotLbl.Text = _dbManDataGenData.ResultsDataTable.Rows[0]["LocoPot"].ToString();
            TLPotLbl.Text = _dbManDataGenData.ResultsDataTable.Rows[0]["tlPot"].ToString();
            LoaderDriverLbl.Text = _dbManDataGenData.ResultsDataTable.Rows[0]["LoaderPot"].ToString();
            TransTlLbl.Text = (Convert.ToDecimal(_dbManDataGenData.ResultsDataTable.Rows[0]["TransTLPot"].ToString()) + Convert.ToDecimal(_dbManDataGenData.ResultsDataTable.Rows[0]["TransTLPotOther"].ToString())).ToString();

            LDLbl.Text = _dbManDataGenData.ResultsDataTable.Rows[0]["ldPotOther"].ToString();

            GenLbl.Text = _dbManDataGenData.ResultsDataTable.Rows[0]["finPotOther"].ToString();

            // get tot hoppers

            string ll = "Level" + lvl.ToString();
            MWDataManager.clsDataAccess _dbManTot = new MWDataManager.clsDataAccess();
            _dbManTot.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            if (ll == "Level3")
            {
                if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) >= 201804 && ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) < 201806)
                    _dbManTot.SqlStatement = " select isnull(sum(hoppers),0) TotHoppers from Mineware.dbo.tbl_BCS_Tramming_Gang where YearMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and level in ('Level1', 'Level2', 'Level3') ";
                else
                    _dbManTot.SqlStatement = " select isnull(sum(hoppers),0) TotHoppers from Mineware.dbo.tbl_BCS_Tramming_Gang where YearMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and level in ('Level1', 'Level3') ";

            }
            else
                _dbManTot.SqlStatement = " select isnull(sum(hoppers),0) TotHoppers from Mineware.dbo.tbl_BCS_Tramming_Gang where YearMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and level = '" + ll + "' ";
            //substring(workingOrgUnit,6,2) = '" + lblOrgunit.Text.Substring(5,2) + "' ";
            if (reef == "Mer")
                _dbManTot.SqlStatement = _dbManTot.SqlStatement + " and  substring(workingorgunit,1,4) in (select mo from mineware.[dbo].[tbl_Survey_Bonus_MOReefTypeLink] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and mer = 'Y') ";
            if (reef == "Ug2")
                _dbManTot.SqlStatement = _dbManTot.SqlStatement + " and  substring(workingorgunit,1,4) in (select mo from mineware.[dbo].[tbl_Survey_Bonus_MOReefTypeLink] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and Ug2 = 'Y') ";

            _dbManTot.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManTot.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManTot.ResultsTableName = "Dates";
            _dbManTot.ExecuteInstruction();

            TotHoppLbl.Text = _dbManTot.ResultsDataTable.Rows[0]["TotHoppers"].ToString();


            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = "  Select min(calendardate) mindate, max(calendardate) maxdate, '" + lblOrgunit.Text + "' bbbb,  '" + safety + "' ss, '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' pm \r\n " +
                                  "  from mineware.dbo.tbl_bcs_CALENDARMILL a, mineware.dbo.tbl_Code_Calendar_Type b  \r\n " +
                                  "  where a.millmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and a.oreflowid = 'MC1'\r\n " +
                                  " and b.calendardate >= a.startdate \r\n " +
                                  " and b.calendardate <= a.enddate \r\n " +
                                  " and b.workingday = 'Y'";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ResultsTableName = "Dates";
            _dbMan.ExecuteInstruction();

            ReportTramBonus.Tables.Add(_dbMan.ResultsDataTable);

            Mindate = _dbMan.ResultsDataTable.Rows[0]["mindate"].ToString();
            Maxdate = _dbMan.ResultsDataTable.Rows[0]["maxdate"].ToString();


            MWDataManager.clsDataAccess _dbManPotInfo = new MWDataManager.clsDataAccess();
            _dbManPotInfo.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) != "1")
                _dbManPotInfo.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_Tramming_ShiftsNew]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LocoDriverPotLbl.Text + "',  '" + TotHoppLbl.Text + "',  '" + safety + "' \r\n ";
            else
                _dbManPotInfo.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_Tramming_Shifts]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LocoDriverPotLbl.Text + "',  '" + TotHoppLbl.Text + "',  '" + safety + "' \r\n ";
            //"    \r\n " +
            //"  \r\n " +
            //"  \r\n " +
            //"  \r\n " +
            //" ";
            _dbManPotInfo.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManPotInfo.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManPotInfo.ResultsTableName = "a";
            _dbManPotInfo.ExecuteInstruction();

            decimal potshifts = Convert.ToDecimal(_dbManPotInfo.ResultsDataTable.Rows[0]["potshifts"].ToString());
            decimal pot = Convert.ToDecimal(_dbManPotInfo.ResultsDataTable.Rows[0]["pot"].ToString());


            MWDataManager.clsDataAccess _dbManPotInfo1 = new MWDataManager.clsDataAccess();
            _dbManPotInfo1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //if (ProdMonthTxt.Text != "1")

            if (ll == "Level3")
            {
                if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) >= 201804 && ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) < 201806)
                {

                    if (reef == "Both")
                        _dbManPotInfo1.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTL_ShiftsNew3lvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TLPotLbl.Text + "',  '" + lvl + "',  '" + safety + "' \r\n ";

                    if (reef == "Mer")
                        _dbManPotInfo1.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTL_ShiftsNewMer3lvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TLPotLbl.Text + "',  '" + lvl + "',  '" + safety + "' \r\n ";

                    if (reef == "Ug2")
                        _dbManPotInfo1.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTL_ShiftsNewUg23lvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TLPotLbl.Text + "',  '" + lvl + "',  '" + safety + "' \r\n ";

                }
                else
                {

                    if (reef == "Both")
                        _dbManPotInfo1.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTL_ShiftsNew3lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TLPotLbl.Text + "',  '" + lvl + "',  '" + safety + "' \r\n ";

                    if (reef == "Mer")
                        _dbManPotInfo1.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTL_ShiftsNewMer3lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TLPotLbl.Text + "',  '" + lvl + "',  '" + safety + "' \r\n ";

                    if (reef == "Ug2")
                        _dbManPotInfo1.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTL_ShiftsNewUg23lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TLPotLbl.Text + "',  '" + lvl + "',  '" + safety + "' \r\n ";
                }
            }
            else
            {
                if (reef == "Both")
                    _dbManPotInfo1.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTL_ShiftsNew]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TLPotLbl.Text + "',  '" + lvl + "',  '" + safety + "' \r\n ";

                if (reef == "Mer")
                    _dbManPotInfo1.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTL_ShiftsNewMer]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TLPotLbl.Text + "',  '" + lvl + "',  '" + safety + "' \r\n ";

                if (reef == "Ug2")
                    _dbManPotInfo1.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTL_ShiftsNewUg2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TLPotLbl.Text + "',  '" + lvl + "',  '" + safety + "' \r\n ";

            }


            _dbManPotInfo1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManPotInfo1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManPotInfo1.ResultsTableName = "a";
            _dbManPotInfo1.ExecuteInstruction();


            if (_dbManPotInfo1.ResultsDataTable.Rows[0]["potshifts"] != DBNull.Value)
                potshifts = potshifts + Convert.ToDecimal(_dbManPotInfo1.ResultsDataTable.Rows[0]["potshifts"].ToString());

            if (_dbManPotInfo1.ResultsDataTable.Rows[0]["pot"] != DBNull.Value)
                pot = pot + Convert.ToDecimal(_dbManPotInfo1.ResultsDataTable.Rows[0]["pot"].ToString());


            MWDataManager.clsDataAccess _dbManPotInfo2 = new MWDataManager.clsDataAccess();
            _dbManPotInfo2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            // if (ProdMonthTxt.Text != "1")


            if (ll == "Level3")
            {
                if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) >= 201804 && ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) < 201806)
                {
                    if (reef == "Both")
                        _dbManPotInfo2.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingLoaderDriverNew_ShiftsNew3LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LoaderDriverLbl.Text + "',  '" + lvl + "',  '" + safety + "'  \r\n ";

                    if (reef == "Mer")
                        _dbManPotInfo2.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingLoaderDriverNew_ShiftsNewMer3LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LoaderDriverLbl.Text + "',  '" + lvl + "',  '" + safety + "'  \r\n ";

                    if (reef == "Ug2")
                        _dbManPotInfo2.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingLoaderDriverNew_ShiftsNewUg23LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LoaderDriverLbl.Text + "',  '" + lvl + "',  '" + safety + "'  \r\n ";
                }
                else
                {
                    if (reef == "Both")
                        _dbManPotInfo2.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingLoaderDriverNew_ShiftsNew3Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LoaderDriverLbl.Text + "',  '" + lvl + "',  '" + safety + "'  \r\n ";

                    if (reef == "Mer")
                        _dbManPotInfo2.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingLoaderDriverNew_ShiftsNewMer3Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LoaderDriverLbl.Text + "',  '" + lvl + "',  '" + safety + "'  \r\n ";

                    if (reef == "Ug2")
                        _dbManPotInfo2.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingLoaderDriverNew_ShiftsNewUg23Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LoaderDriverLbl.Text + "',  '" + lvl + "',  '" + safety + "'  \r\n ";
                }
            }
            else
            {
                if (reef == "Both")
                    _dbManPotInfo2.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingLoaderDriverNew_ShiftsNew]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LoaderDriverLbl.Text + "',  '" + lvl + "',  '" + safety + "'  \r\n ";

                if (reef == "Mer")
                    _dbManPotInfo2.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingLoaderDriverNew_ShiftsNewMer]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LoaderDriverLbl.Text + "',  '" + lvl + "',  '" + safety + "'  \r\n ";

                if (reef == "Ug2")
                    _dbManPotInfo2.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingLoaderDriverNew_ShiftsNewUg2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LoaderDriverLbl.Text + "',  '" + lvl + "',  '" + safety + "'  \r\n ";

            }




            // else
            //     _dbManPotInfo2.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingLoaderDriverNew_Shifts]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LoaderDriverLbl.Text + "',  '" + lvl + "',  '" + safety + "'  \r\n ";

            //"    \r\n " +
            //                 "  \r\n " +
            //                 "  \r\n " +
            //                 "  \r\n " +
            //" ";
            _dbManPotInfo2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManPotInfo2.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManPotInfo2.ResultsTableName = "a";
            _dbManPotInfo2.ExecuteInstruction();

            potshifts = potshifts + Convert.ToDecimal(_dbManPotInfo2.ResultsDataTable.Rows[0]["potshifts"].ToString());
            if (_dbManPotInfo2.ResultsDataTable.Rows[0]["pot"] != DBNull.Value)
                pot = pot + Convert.ToDecimal(_dbManPotInfo2.ResultsDataTable.Rows[0]["pot"].ToString());


            MWDataManager.clsDataAccess _dbManPotInfo3 = new MWDataManager.clsDataAccess();
            _dbManPotInfo3.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            if (ll == "Level3")
            {
                if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) >= 201804 && ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) < 201806)
                {
                    if (reef == "Both")
                        _dbManPotInfo3.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportTLNew_Shifts3LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TransTlLbl.Text + "',  '" + lvl + "',  '" + safety + "'   \r\n ";

                    if (reef == "Mer")
                        _dbManPotInfo3.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportTLNew_ShiftsMer3LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TransTlLbl.Text + "',  '" + lvl + "',  '" + safety + "'   \r\n ";

                    if (reef == "Ug2")
                        _dbManPotInfo3.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportTLNew_ShiftsUg23LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TransTlLbl.Text + "',  '" + lvl + "',  '" + safety + "'   \r\n ";
                }
                else
                {

                    if (reef == "Both")
                        _dbManPotInfo3.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportTLNew_Shifts3Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TransTlLbl.Text + "',  '" + lvl + "',  '" + safety + "'   \r\n ";

                    if (reef == "Mer")
                        _dbManPotInfo3.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportTLNew_ShiftsMer3Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TransTlLbl.Text + "',  '" + lvl + "',  '" + safety + "'   \r\n ";

                    if (reef == "Ug2")
                        _dbManPotInfo3.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportTLNew_ShiftsUg23Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TransTlLbl.Text + "',  '" + lvl + "',  '" + safety + "'   \r\n ";
                }
            }
            else
            {
                if (reef == "Both")
                    _dbManPotInfo3.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportTLNew_Shifts]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TransTlLbl.Text + "',  '" + lvl + "',  '" + safety + "'   \r\n ";

                if (reef == "Mer")
                    _dbManPotInfo3.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportTLNew_ShiftsMer]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TransTlLbl.Text + "',  '" + lvl + "',  '" + safety + "'   \r\n ";

                if (reef == "Ug2")
                    _dbManPotInfo3.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportTLNew_ShiftsUg2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TransTlLbl.Text + "',  '" + lvl + "',  '" + safety + "'   \r\n ";


            }
            _dbManPotInfo3.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManPotInfo3.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManPotInfo3.ResultsTableName = "a";
            _dbManPotInfo3.ExecuteInstruction();

            potshifts = potshifts + Convert.ToDecimal(_dbManPotInfo3.ResultsDataTable.Rows[0]["potshifts"].ToString());
            if (_dbManPotInfo3.ResultsDataTable.Rows[0]["pot"] != DBNull.Value)
                pot = pot + Convert.ToDecimal(_dbManPotInfo3.ResultsDataTable.Rows[0]["pot"].ToString());



            MWDataManager.clsDataAccess _dbManPotInfo4 = new MWDataManager.clsDataAccess();
            _dbManPotInfo4.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);


            if (ll == "Level3")
            {
                if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) >= 201804 && ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) < 201806)
                {
                    if (reef == "Both")
                        _dbManPotInfo4.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportGenNew_Shifts3LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + GenLbl.Text + "',  '" + lvl + "',  '" + safety + "'  \r\n ";
                    if (reef == "Mer")
                        _dbManPotInfo4.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportGenNew_ShiftsMer3LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + GenLbl.Text + "',  '" + lvl + "',  '" + safety + "'  \r\n ";
                    if (reef == "Ug2")
                        _dbManPotInfo4.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportGenNew_ShiftsUg23LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + GenLbl.Text + "',  '" + lvl + "',  '" + safety + "'  \r\n ";
                }
                else
                {

                    if (reef == "Both")
                        _dbManPotInfo4.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportGenNew_Shifts3Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + GenLbl.Text + "',  '" + lvl + "',  '" + safety + "'  \r\n ";
                    if (reef == "Mer")
                        _dbManPotInfo4.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportGenNew_ShiftsMer3Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + GenLbl.Text + "',  '" + lvl + "',  '" + safety + "'  \r\n ";
                    if (reef == "Ug2")
                        _dbManPotInfo4.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportGenNew_ShiftsUg23Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + GenLbl.Text + "',  '" + lvl + "',  '" + safety + "'  \r\n ";
                }
            }
            else
            {
                if (reef == "Both")
                    _dbManPotInfo4.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportGenNew_Shifts]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + GenLbl.Text + "',  '" + lvl + "',  '" + safety + "'  \r\n ";
                if (reef == "Mer")
                    _dbManPotInfo4.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportGenNew_ShiftsMer]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + GenLbl.Text + "',  '" + lvl + "',  '" + safety + "'  \r\n ";
                if (reef == "Ug2")
                    _dbManPotInfo4.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportGenNew_ShiftsUg2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + GenLbl.Text + "',  '" + lvl + "',  '" + safety + "'  \r\n ";

            }
            _dbManPotInfo4.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManPotInfo4.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManPotInfo4.ResultsTableName = "a";
            _dbManPotInfo4.ExecuteInstruction();

            potshifts = potshifts + Convert.ToDecimal(_dbManPotInfo4.ResultsDataTable.Rows[0]["potshifts"].ToString());
            if (_dbManPotInfo4.ResultsDataTable.Rows[0]["pot"] != DBNull.Value)
                pot = pot + Convert.ToDecimal(_dbManPotInfo4.ResultsDataTable.Rows[0]["pot"].ToString());

            MWDataManager.clsDataAccess _dbManPotInfo5 = new MWDataManager.clsDataAccess();
            _dbManPotInfo5.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            if (ll == "Level3")
            {
                if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) >= 201804 && ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) < 201806)
                {
                    if (reef == "Both")
                        _dbManPotInfo5.SqlStatement = "  exec  mineware.[dbo].[sp_BMCS_TrammingTransportLDNew_Shifts3LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LDLbl.Text + "',  '" + lvl + "' ,  '" + safety + "'  \r\n ";

                    if (reef == "Mer")
                        _dbManPotInfo5.SqlStatement = "  exec  mineware.[dbo].[sp_BMCS_TrammingTransportLDNew_ShiftsMer3LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LDLbl.Text + "',  '" + lvl + "' ,  '" + safety + "'  \r\n ";


                    if (reef == "Ug2")
                        _dbManPotInfo5.SqlStatement = "  exec  mineware.[dbo].[sp_BMCS_TrammingTransportLDNew_ShiftsUg23LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LDLbl.Text + "',  '" + lvl + "' ,  '" + safety + "'  \r\n ";
                }
                else
                {

                    if (reef == "Both")
                        _dbManPotInfo5.SqlStatement = "  exec  mineware.[dbo].[sp_BMCS_TrammingTransportLDNew_Shifts3Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LDLbl.Text + "',  '" + lvl + "' ,  '" + safety + "'  \r\n ";

                    if (reef == "Mer")
                        _dbManPotInfo5.SqlStatement = "  exec  mineware.[dbo].[sp_BMCS_TrammingTransportLDNew_ShiftsMer3Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LDLbl.Text + "',  '" + lvl + "' ,  '" + safety + "'  \r\n ";


                    if (reef == "Ug2")
                        _dbManPotInfo5.SqlStatement = "  exec  mineware.[dbo].[sp_BMCS_TrammingTransportLDNew_ShiftsUg23Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LDLbl.Text + "',  '" + lvl + "' ,  '" + safety + "'  \r\n ";
                }
            }
            else
            {
                if (reef == "Both")
                    _dbManPotInfo5.SqlStatement = "  exec  mineware.[dbo].[sp_BMCS_TrammingTransportLDNew_Shifts]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LDLbl.Text + "',  '" + lvl + "' ,  '" + safety + "'  \r\n ";

                if (reef == "Mer")
                    _dbManPotInfo5.SqlStatement = "  exec  mineware.[dbo].[sp_BMCS_TrammingTransportLDNew_ShiftsMer]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LDLbl.Text + "',  '" + lvl + "' ,  '" + safety + "'  \r\n ";


                if (reef == "Ug2")
                    _dbManPotInfo5.SqlStatement = "  exec  mineware.[dbo].[sp_BMCS_TrammingTransportLDNew_ShiftsUg2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LDLbl.Text + "',  '" + lvl + "' ,  '" + safety + "'  \r\n ";
            }

            _dbManPotInfo5.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManPotInfo5.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManPotInfo5.ResultsTableName = "a";
            _dbManPotInfo5.ExecuteInstruction();

            potshifts = potshifts + Convert.ToDecimal(_dbManPotInfo5.ResultsDataTable.Rows[0]["potshifts"].ToString());
            if (_dbManPotInfo5.ResultsDataTable.Rows[0]["pot"] != DBNull.Value)
                pot = pot + Convert.ToDecimal(_dbManPotInfo5.ResultsDataTable.Rows[0]["pot"].ToString());


            decimal potpershift = 0;
            if (potshifts > 0)
                potpershift = pot / potshifts;

            if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) == "201608")
            {
                potpershift = 0;
                pot = 0;
            }



            MWDataManager.clsDataAccess _dbManzz = new MWDataManager.clsDataAccess();
            _dbManzz.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManzz.SqlStatement = "  Select '" + pot + "' zz,  '" + potshifts + "' xxxx ";

            _dbManzz.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManzz.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManzz.ResultsTableName = "Other";
            _dbManzz.ExecuteInstruction();

            ReportTramBonus.Tables.Add(_dbManzz.ResultsDataTable);



            MWDataManager.clsDataAccess _dbManData = new MWDataManager.clsDataAccess();
            _dbManData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManData.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_Tramming]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LocoDriverPotLbl.Text + "',  '" + TotHoppLbl.Text + "' ,  '" + potpershift + "',  '" + safety + "' \r\n " +
                                  "    \r\n " +
                                  "  \r\n " +
                                  "  \r\n " +
                                  "  \r\n " +
                                  " ";
            _dbManData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManData.ResultsTableName = "Data";
            _dbManData.ExecuteInstruction();

            ReportTramBonus.Tables.Add(_dbManData.ResultsDataTable);

            int row = 0;
            DataTable dtWP = _dbManData.ResultsDataTable;
            // load grid
            foreach (DataRow dr in dtWP.Rows)
            {
                if (dr["industrynumber"].ToString() != "")
                {
                    DataGrid.Rows[row].Cells[0].Value = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue));
                    DataGrid.Rows[row].Cells[1].Value = lblOrgunit.Text;
                    DataGrid.Rows[row].Cells[2].Value = lvl;
                    if (rdbDS.Checked == true)
                        DataGrid.Rows[row].Cells[3].Value = "D";
                    if (rdbAS.Checked == true)
                        DataGrid.Rows[row].Cells[3].Value = "A";
                    if (rdbNS.Checked == true)
                        DataGrid.Rows[row].Cells[3].Value = "N";

                    DataGrid.Rows[row].Cells[4].Value = dr["aa"].ToString();

                    DataGrid.Rows[row].Cells[5].Value = dr["Designation"].ToString();
                    DataGrid.Rows[row].Cells[6].Value = dr["industrynumber"].ToString();
                    DataGrid.Rows[row].Cells[7].Value = dr["tothoppers"].ToString();
                    DataGrid.Rows[row].Cells[8].Value = "0";
                    DataGrid.Rows[row].Cells[9].Value = dr["finpay"].ToString();
                    DataGrid.Rows[row].Cells[9].Value = Math.Round(Convert.ToDecimal(DataGrid.Rows[row].Cells[9].Value), 9);
                    DataGrid.Rows[row].Cells[10].Value = "Mining Tramming Bonus";
                    DataGrid.Rows[row].Cells[11].Value = "";
                    DataGrid.Rows[row].Cells[12].Value = "N";



                    DataGrid.Rows[row].Cells[13].Value = "";
                    row = row + 1;
                }

            }


            MWDataManager.clsDataAccess _dbManDataTL = new MWDataManager.clsDataAccess();
            _dbManDataTL.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            if (ll == "Level3")
            {
                if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) >= 201804 && ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) < 201806)
                {
                    if (reef == "Both")
                        _dbManDataTL.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTL3LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TLPotLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";

                    if (reef == "Mer")
                        _dbManDataTL.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTLMer3LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TLPotLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";


                    if (reef == "Ug2")
                        _dbManDataTL.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTLUg23LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TLPotLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";
                }
                else
                {
                    if (reef == "Both")
                        _dbManDataTL.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTL3Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TLPotLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";

                    if (reef == "Mer")
                        _dbManDataTL.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTLMer3Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TLPotLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";


                    if (reef == "Ug2")
                        _dbManDataTL.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTLUg23Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TLPotLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";
                }

            }
            else
            {
                if (reef == "Both")
                    _dbManDataTL.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTL]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TLPotLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";

                if (reef == "Mer")
                    _dbManDataTL.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTLMer]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TLPotLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";


                if (reef == "Ug2")
                    _dbManDataTL.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTLUg2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TLPotLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";

            }


            _dbManDataTL.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManDataTL.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManDataTL.ResultsTableName = "DataTL";
            _dbManDataTL.ExecuteInstruction();

            ReportTramBonus.Tables.Add(_dbManDataTL.ResultsDataTable);

            DataTable dtWP1 = _dbManDataTL.ResultsDataTable;
            // load grid
            foreach (DataRow dr in dtWP1.Rows)
            {
                if (dr["indno"].ToString() != "")
                {
                    DataGrid.Rows[row].Cells[0].Value = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue));
                    DataGrid.Rows[row].Cells[1].Value = lblOrgunit.Text;
                    DataGrid.Rows[row].Cells[2].Value = lvl;
                    if (rdbDS.Checked == true)
                        DataGrid.Rows[row].Cells[3].Value = "D";
                    if (rdbAS.Checked == true)
                        DataGrid.Rows[row].Cells[3].Value = "A";
                    if (rdbNS.Checked == true)
                        DataGrid.Rows[row].Cells[3].Value = "N";

                    DataGrid.Rows[row].Cells[4].Value = "Tram Team Leader";

                    DataGrid.Rows[row].Cells[5].Value = dr["Designation"].ToString();
                    DataGrid.Rows[row].Cells[6].Value = dr["indno"].ToString();
                    DataGrid.Rows[row].Cells[7].Value = dr["tothoppers"].ToString();
                    DataGrid.Rows[row].Cells[8].Value = "0";
                    DataGrid.Rows[row].Cells[9].Value = dr["finpay"].ToString();
                    DataGrid.Rows[row].Cells[9].Value = Math.Round(Convert.ToDecimal(DataGrid.Rows[row].Cells[9].Value), 9);
                    DataGrid.Rows[row].Cells[10].Value = "Mining Tramming Bonus";
                    DataGrid.Rows[row].Cells[11].Value = "";
                    DataGrid.Rows[row].Cells[12].Value = "N";



                    DataGrid.Rows[row].Cells[13].Value = "";
                    row = row + 1;
                }

            }


            MWDataManager.clsDataAccess _dbManDataLD = new MWDataManager.clsDataAccess();
            _dbManDataLD.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);


            if (ll == "Level3")
            {
                if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) >= 201804 && ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) < 201806)
                {
                    if (reef == "Both")
                        _dbManDataLD.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingLoaderDriverNew3LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LoaderDriverLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";

                    if (reef == "Mer")
                        _dbManDataLD.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingLoaderDriverNewMer3LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LoaderDriverLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";

                    if (reef == "Ug2")
                        _dbManDataLD.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingLoaderDriverNewUg23LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LoaderDriverLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";
                }
                else
                {

                    if (reef == "Both")
                        _dbManDataLD.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingLoaderDriverNew3Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LoaderDriverLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";

                    if (reef == "Mer")
                        _dbManDataLD.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingLoaderDriverNewMer3Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LoaderDriverLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";

                    if (reef == "Ug2")
                        _dbManDataLD.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingLoaderDriverNewUg23Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LoaderDriverLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";
                }

            }
            else
            {
                if (reef == "Both")
                    _dbManDataLD.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingLoaderDriverNew]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LoaderDriverLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";

                if (reef == "Mer")
                    _dbManDataLD.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingLoaderDriverNewMer]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LoaderDriverLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";

                if (reef == "Ug2")
                    _dbManDataLD.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingLoaderDriverNewUg2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LoaderDriverLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";

            }


            _dbManDataLD.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManDataLD.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManDataLD.ResultsTableName = "DataLD";
            _dbManDataLD.ExecuteInstruction();

            ReportTramBonus.Tables.Add(_dbManDataLD.ResultsDataTable);

            DataTable dtWP2 = _dbManDataLD.ResultsDataTable;
            // load grid
            foreach (DataRow dr in dtWP2.Rows)
            {
                if (dr["indno"].ToString() != "")
                {
                    DataGrid.Rows[row].Cells[0].Value = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue));
                    DataGrid.Rows[row].Cells[1].Value = lblOrgunit.Text;
                    DataGrid.Rows[row].Cells[2].Value = lvl;
                    if (rdbDS.Checked == true)
                        DataGrid.Rows[row].Cells[3].Value = "D";
                    if (rdbAS.Checked == true)
                        DataGrid.Rows[row].Cells[3].Value = "A";
                    if (rdbNS.Checked == true)
                        DataGrid.Rows[row].Cells[3].Value = "N";

                    DataGrid.Rows[row].Cells[4].Value = "Tram Loco Driver";

                    DataGrid.Rows[row].Cells[5].Value = dr["Designation"].ToString();
                    DataGrid.Rows[row].Cells[6].Value = dr["indno"].ToString();
                    DataGrid.Rows[row].Cells[7].Value = dr["tothoppers"].ToString();
                    DataGrid.Rows[row].Cells[8].Value = "0";
                    DataGrid.Rows[row].Cells[9].Value = dr["finpay"].ToString();
                    //DataGrid.Rows[row].Cells[9].Value = Math.Round(Convert.ToDecimal(DataGrid.Rows[row].Cells[9].Value), 9);
                    DataGrid.Rows[row].Cells[10].Value = "Mining Tramming Bonus";
                    DataGrid.Rows[row].Cells[11].Value = "";
                    DataGrid.Rows[row].Cells[12].Value = "N";



                    DataGrid.Rows[row].Cells[13].Value = "";
                    row = row + 1;
                }

            }

            MWDataManager.clsDataAccess _dbManDataTranspTL = new MWDataManager.clsDataAccess();
            _dbManDataTranspTL.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            if (ll == "Level3")
            {
                if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) >= 201804 && ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) < 201806)
                {
                    if (reef == "Both")
                        _dbManDataTranspTL.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportTLNew3LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TransTlLbl.Text + "',  '" + potpershift + "'  ,  '" + lvl + "',  '" + safety + "'  \r\n ";

                    if (reef == "Mer")
                        _dbManDataTranspTL.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportTLNewMer3LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TransTlLbl.Text + "',  '" + potpershift + "'  ,  '" + lvl + "',  '" + safety + "'  \r\n ";
                    if (reef == "Ug2")
                        _dbManDataTranspTL.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportTLNewUg23LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TransTlLbl.Text + "',  '" + potpershift + "'  ,  '" + lvl + "',  '" + safety + "'  \r\n ";

                }
                else
                {
                    if (reef == "Both")
                        _dbManDataTranspTL.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportTLNew3Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TransTlLbl.Text + "',  '" + potpershift + "'  ,  '" + lvl + "',  '" + safety + "'  \r\n ";

                    if (reef == "Mer")
                        _dbManDataTranspTL.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportTLNewMer3Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TransTlLbl.Text + "',  '" + potpershift + "'  ,  '" + lvl + "',  '" + safety + "'  \r\n ";
                    if (reef == "Ug2")
                        _dbManDataTranspTL.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportTLNewUg23Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TransTlLbl.Text + "',  '" + potpershift + "'  ,  '" + lvl + "',  '" + safety + "'  \r\n ";
                }
            }
            else
            {
                if (reef == "Both")
                    _dbManDataTranspTL.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportTLNew]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TransTlLbl.Text + "',  '" + potpershift + "'  ,  '" + lvl + "',  '" + safety + "'  \r\n ";

                if (reef == "Mer")
                    _dbManDataTranspTL.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportTLNewMer]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TransTlLbl.Text + "',  '" + potpershift + "'  ,  '" + lvl + "',  '" + safety + "'  \r\n ";
                if (reef == "Ug2")
                    _dbManDataTranspTL.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportTLNewUg2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + TransTlLbl.Text + "',  '" + potpershift + "'  ,  '" + lvl + "',  '" + safety + "'  \r\n ";

            }
            _dbManDataTranspTL.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManDataTranspTL.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManDataTranspTL.ResultsTableName = "DataTransportTL";
            _dbManDataTranspTL.ExecuteInstruction();

            ReportTramBonus.Tables.Add(_dbManDataTranspTL.ResultsDataTable);

            DataTable dtWP3 = _dbManDataTranspTL.ResultsDataTable;
            // load grid
            foreach (DataRow dr in dtWP3.Rows)
            {
                if (dr["indno"].ToString() != "")
                {
                    DataGrid.Rows[row].Cells[0].Value = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue));
                    DataGrid.Rows[row].Cells[1].Value = lblOrgunit.Text;
                    DataGrid.Rows[row].Cells[2].Value = lvl;
                    if (rdbDS.Checked == true)
                        DataGrid.Rows[row].Cells[3].Value = "D";
                    if (rdbAS.Checked == true)
                        DataGrid.Rows[row].Cells[3].Value = "A";
                    if (rdbNS.Checked == true)
                        DataGrid.Rows[row].Cells[3].Value = "N";

                    DataGrid.Rows[row].Cells[4].Value = "Transport Team Leader";

                    DataGrid.Rows[row].Cells[5].Value = dr["Designation"].ToString();
                    DataGrid.Rows[row].Cells[6].Value = dr["indno"].ToString();
                    DataGrid.Rows[row].Cells[7].Value = dr["tothoppers"].ToString();
                    DataGrid.Rows[row].Cells[8].Value = "0";
                    DataGrid.Rows[row].Cells[9].Value = dr["finpay"].ToString();
                    //DataGrid.Rows[row].Cells[9].Value = Math.Round(Convert.ToDecimal(DataGrid.Rows[row].Cells[9].Value), 9);
                    DataGrid.Rows[row].Cells[10].Value = "Mining Tramming Bonus";
                    DataGrid.Rows[row].Cells[11].Value = "";
                    DataGrid.Rows[row].Cells[12].Value = "N";



                    DataGrid.Rows[row].Cells[13].Value = "";
                    row = row + 1;
                }

            }

            MWDataManager.clsDataAccess _dbManDataTranspGen = new MWDataManager.clsDataAccess();
            _dbManDataTranspGen.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);


            if (ll == "Level3")
            {
                if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) >= 201804 && ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) < 201806)
                {
                    if (reef == "Both")
                        _dbManDataTranspGen.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportGenNew3LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + GenLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";
                    if (reef == "Mer")
                        _dbManDataTranspGen.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportGenNewMer3LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + GenLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";

                    if (reef == "Ug2")
                        _dbManDataTranspGen.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportGenNewUg23LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + GenLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";
                }
                else
                {

                    if (reef == "Both")
                        _dbManDataTranspGen.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportGenNew3Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + GenLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";
                    if (reef == "Mer")
                        _dbManDataTranspGen.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportGenNewMer3Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + GenLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";

                    if (reef == "Ug2")
                        _dbManDataTranspGen.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportGenNewUg23Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + GenLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";
                }
            }
            else
            {
                if (reef == "Both")
                    _dbManDataTranspGen.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportGenNew]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + GenLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";
                if (reef == "Mer")
                    _dbManDataTranspGen.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportGenNewMer]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + GenLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";

                if (reef == "Ug2")
                    _dbManDataTranspGen.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportGenNewUg2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + GenLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'  \r\n ";

            }
            _dbManDataTranspGen.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManDataTranspGen.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManDataTranspGen.ResultsTableName = "DataTransportGen";
            _dbManDataTranspGen.ExecuteInstruction();

            ReportTramBonus.Tables.Add(_dbManDataTranspGen.ResultsDataTable);

            DataTable dtWP4 = _dbManDataTranspGen.ResultsDataTable;
            // load grid
            foreach (DataRow dr in dtWP4.Rows)
            {
                if (dr["indno"].ToString() != "")
                {
                    DataGrid.Rows[row].Cells[0].Value = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue));
                    DataGrid.Rows[row].Cells[1].Value = lblOrgunit.Text;
                    DataGrid.Rows[row].Cells[2].Value = lvl;
                    if (rdbDS.Checked == true)
                        DataGrid.Rows[row].Cells[3].Value = "D";
                    if (rdbAS.Checked == true)
                        DataGrid.Rows[row].Cells[3].Value = "A";
                    if (rdbNS.Checked == true)
                        DataGrid.Rows[row].Cells[3].Value = "N";

                    DataGrid.Rows[row].Cells[4].Value = "Transport General";

                    DataGrid.Rows[row].Cells[5].Value = dr["Designation"].ToString();
                    DataGrid.Rows[row].Cells[6].Value = dr["indno"].ToString();
                    DataGrid.Rows[row].Cells[7].Value = dr["tothoppers"].ToString();
                    DataGrid.Rows[row].Cells[8].Value = "0";
                    DataGrid.Rows[row].Cells[9].Value = dr["finpay"].ToString();
                    // DataGrid.Rows[row].Cells[9].Value = Math.Round(Convert.ToDecimal(DataGrid.Rows[row].Cells[9].Value), 9);
                    DataGrid.Rows[row].Cells[10].Value = "Mining Tramming Bonus";
                    DataGrid.Rows[row].Cells[11].Value = "";
                    DataGrid.Rows[row].Cells[12].Value = "N";



                    DataGrid.Rows[row].Cells[13].Value = "";
                    row = row + 1;
                }

            }



            MWDataManager.clsDataAccess _dbManDataTranspLD = new MWDataManager.clsDataAccess();
            _dbManDataTranspLD.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            if (ll == "Level3")
            {
                if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) >= 201804 && ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsInt(Convert.ToDateTime(editProdmonth.EditValue)) < 201806)
                {
                    if (reef == "Both")
                        _dbManDataTranspLD.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportLDNew3LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LDLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'   \r\n ";

                    if (reef == "Mer")
                        _dbManDataTranspLD.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportLDNewMer3LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LDLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'   \r\n ";

                    if (reef == "Ug2")
                        _dbManDataTranspLD.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportLDNewUg23LvlInc2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LDLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'   \r\n ";
                }
                else
                {
                    if (reef == "Both")
                        _dbManDataTranspLD.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportLDNew3Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LDLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'   \r\n ";

                    if (reef == "Mer")
                        _dbManDataTranspLD.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportLDNewMer3Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LDLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'   \r\n ";

                    if (reef == "Ug2")
                        _dbManDataTranspLD.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportLDNewUg23Lvl]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LDLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'   \r\n ";
                }
            }
            else
            {
                if (reef == "Both")
                    _dbManDataTranspLD.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportLDNew]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LDLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'   \r\n ";

                if (reef == "Mer")
                    _dbManDataTranspLD.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportLDNewMer]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LDLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'   \r\n ";

                if (reef == "Ug2")
                    _dbManDataTranspLD.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_TrammingTransportLDNewUg2]  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Mindate + "', '" + Maxdate + "', '" + lblOrgunit.Text + "',  '" + LDLbl.Text + "',  '" + potpershift + "',  '" + lvl + "',  '" + safety + "'   \r\n ";

            }
            _dbManDataTranspLD.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManDataTranspLD.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManDataTranspLD.ResultsTableName = "DataTransportLD";
            _dbManDataTranspLD.ExecuteInstruction();

            ReportTramBonus.Tables.Add(_dbManDataTranspLD.ResultsDataTable);

            DataTable dtWP6 = _dbManDataTranspLD.ResultsDataTable;
            // load grid
            foreach (DataRow dr in dtWP6.Rows)
            {
                if (dr["indno"].ToString() != "")
                {
                    DataGrid.Rows[row].Cells[0].Value = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue));
                    DataGrid.Rows[row].Cells[1].Value = lblOrgunit.Text;
                    DataGrid.Rows[row].Cells[2].Value = lvl;
                    if (rdbDS.Checked == true)
                        DataGrid.Rows[row].Cells[3].Value = "D";
                    if (rdbAS.Checked == true)
                        DataGrid.Rows[row].Cells[3].Value = "A";
                    if (rdbNS.Checked == true)
                        DataGrid.Rows[row].Cells[3].Value = "N";

                    DataGrid.Rows[row].Cells[4].Value = "Transport Loader Driver";

                    DataGrid.Rows[row].Cells[5].Value = dr["Designation"].ToString();
                    DataGrid.Rows[row].Cells[6].Value = dr["indno"].ToString();
                    DataGrid.Rows[row].Cells[7].Value = dr["tothoppers"].ToString();
                    DataGrid.Rows[row].Cells[8].Value = "0";
                    DataGrid.Rows[row].Cells[9].Value = dr["finpay"].ToString();

                    //DataGrid.Rows[row].Cells[9].Value = Math.Round(Convert.ToDecimal(DataGrid.Rows[row].Cells[9].Value), 9);
                    DataGrid.Rows[row].Cells[10].Value = "Mining Tramming Bonus";
                    DataGrid.Rows[row].Cells[11].Value = "";
                    DataGrid.Rows[row].Cells[12].Value = "N";



                    DataGrid.Rows[row].Cells[13].Value = "";
                    row = row + 1;
                }

            }


            report.RegisterData(ReportTramBonus);

            report.Load(_reportFolder +"TramCalc.frx");

            // report.Design();

            pcReport.Clear();
            report.Prepare();
            report.Preview = pcReport;
            report.ShowPrepared();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MWDataManager.clsDataAccess _dbManInsert = new MWDataManager.clsDataAccess();
            _dbManInsert.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManInsert.SqlStatement = " delete from mineware.[dbo].[tbl_BCS_Tramming_Gang] where timestamp > getdate()-1 ";
            _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " insert into mineware.[dbo].[tbl_BCS_Tramming_Gang] ";
            _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " select * from mineware.[dbo].[tbl_BCS_Tramming_Gang_3Month] where timestamp > getdate()-1 ";
            _dbManInsert.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManInsert.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManInsert.ResultsTableName = "Transfer1";
            _dbManInsert.ExecuteInstruction();

            MessageBox.Show("Data Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnTransfer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = _dbMan.SqlStatement + " select * from mineware.dbo.[tbl_BCS_ARMS_Interface_Transfer_TramNewCalcAccuTrackNewCalc]  \r\n  ";
            _dbMan.SqlStatement = _dbMan.SqlStatement + " Where OrgUnit = '" + lblOrgunit.Text + "'  \r\n ";
            _dbMan.SqlStatement = _dbMan.SqlStatement + " and ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  \r\n ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ResultsTableName = "Stoping";
            _dbMan.ExecuteInstruction();

            if (_dbMan.ResultsDataTable.Rows.Count > 0)
            {
                MessageBox.Show("This Orgunit has already been transfered", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (clsUserInfo.UserName != "Mineware Consulting")
                    return;
            }


            MWDataManager.clsDataAccess _dbMandelete = new MWDataManager.clsDataAccess();
            _dbMandelete.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMandelete.SqlStatement = _dbMandelete.SqlStatement + " Delete mineware.dbo.[tbl_BCS_ARMS_Interface_Transfer_TramNewCalcAccuTrackNewCalc] ";
            _dbMandelete.SqlStatement = _dbMandelete.SqlStatement + " Where OrgUnit = '" + lblOrgunit.Text + "' ";
            _dbMandelete.SqlStatement = _dbMandelete.SqlStatement + " and ProdMonth =  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  \r\n ";

            _dbMandelete.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMandelete.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMandelete.ResultsTableName = "Delete";
            _dbMandelete.ExecuteInstruction();

            MWDataManager.clsDataAccess _dbManInsert = new MWDataManager.clsDataAccess();
            _dbManInsert.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            for (int row = 0; row <= DataGrid.Rows.Count - 1; row++)
            {
                if (DataGrid.Rows[row].Cells[0].Value != null)
                {
                    if (DataGrid.Rows[row].Cells[0].Value.ToString() != "")
                    {
                        _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " Insert into mineware.dbo.[tbl_BCS_ARMS_Interface_Transfer_TramNewCalcAccuTrackNewCalc]  values (  \r\n ";
                        _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " '" + DataGrid.Rows[row].Cells[0].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[1].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[2].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[3].Value.ToString() + "', ";
                        _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " '" + DataGrid.Rows[row].Cells[4].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[5].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[6].Value.ToString() + "', ";

                        _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " '" + DataGrid.Rows[row].Cells[7].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[8].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[9].Value.ToString() + "', ";
                        _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " '" + DataGrid.Rows[row].Cells[10].Value.ToString() + "',null, 'N', getdate(), ";
                        _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " null, 'UnProcessed' ";

                        _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " )  \r\n ";


                    }
                }

            }

            _dbManInsert.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManInsert.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManInsert.ResultsTableName = "Transfer";
            _dbManInsert.ExecuteInstruction();

            MessageBox.Show("Org Unit Transfered", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadListBoxes();
        }

        private void editActivity_EditValueChanged(object sender, EventArgs e)
        {
            if(editActivity.EditValue.ToString() == "DS")
            {
                rdbDS.Checked = true;
                rdbAS.Checked = false;
                rdbNS.Checked = false;
            }
            if (editActivity.EditValue.ToString() == "AS")
            {
                rdbDS.Checked = false;
                rdbAS.Checked = true;
                rdbNS.Checked = false;
            }
            if (editActivity.EditValue.ToString() == "NS")
            {
                rdbDS.Checked = false;
                rdbAS.Checked = false;
                rdbNS.Checked = true;
            }
        }

        private void editProdmonth_EditValueChanged(object sender, EventArgs e)
        {
            LoadListBoxes();


            MWDataManager.clsDataAccess _dbManDataGenData = new MWDataManager.clsDataAccess();
            _dbManDataGenData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManDataGenData.SqlStatement = "declare @lvl int   \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "declare @prodmonth varchar(10)  \r\n ";

            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "declare @Htons1 int \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "declare @M2 int \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "declare @M3 int \r\n ";

            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "set @lvl = 11 \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "set @prodmonth = '201602' \r\n ";


            // get reef tons hoisted
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "set @Htons1 = (select sum(milltons)+sum(stackertons) a from mineware.dbo.tbl_BCS_SURVEYMILL where prodmonth = @prodmonth) \r\n ";

            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "set @Htons1 = @Htons1 - (select sum(stackertons) b from mineware.dbo.tbl_BCS_SURVEYMILL where  \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "prodmonth = (select max(prodmonth) from mineware.dbo.tbl_BCS_Planmonth where prodmonth < @prodmonth)) \r\n ";

            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "set @Htons1 = @Htons1 + (select sum(ptons)-sum(ttons) a from mineware.dbo.tbl_bcs_survey_mud where prodmonth = @prodmonth) \r\n ";


            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "select *, convert(decimal(18,0),lvltons)/(convert(decimal(18,9),tottons)+0.00000001) *(hoisttons*rate) * (per2/100) LocoPot \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + " from ( \r\n ";

            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "select * from (select isnull(sum(lvltons),0) lvltons,  isnull(sum(facetons+vamptons+devtons+osstons),0) tottons,  isnull(@Htons1,0) hoisttons from ( \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "select lvl, Sum(facetons) facetons, Sum(vampTons) vampTons, Sum(devtons) devtons, \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "Sum(osstons) osstons , sum(lvltons) lvltons, sum(facetons+vamptons+devtons+osstons) tottons \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + " from (  \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "select case when LevelNumber IS not null then LevelNumber else lvl1 end as Lvl,  \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "*, case when LevelNumber = @lvl then facetons+vamptons+devtons+osstons else 0 end as lvltons from  \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "(  \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "select pm.oreflowid, o.levelnumber lvl1,case when pm.activity = 0 then squaremetrestotal*stopewidth*convertedcubics/100 else 0  \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "end as facetons,  \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "socketwidth vampTons, case when pm.activity = 1 and w.reefwaste = 'R' then  \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "((metrestotal * measwidth *measheight) + (reefcubics+wastecubics))*convertedcubics else 0 end as devtons,  \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "case when pm.activity = 0  then  \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "( (reefcubics+wastecubics))*convertedcubics else 0 end as osstons  from mineware.dbo.tbl_BCS_Planmonth pm,  \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "mineware.dbo.[vw_bcs_survey] ss, mineware.dbo.tbl_bcs_Workplace w, mineware.dbo.tbl_bcs_oreflowentities o  \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "where pm.workplaceid = w.workplaceid and w.oreflowid = o.oreflowid and pm.workplaceid = ss.workplaceid and pm.sectionid = ss.sectionid \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "and pm.activity = ss.activitycode and pm.Prodmonth = ss.PRODMONTH and pm.Prodmonth = @prodmonth)a \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "left outer join  \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "(  \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "select o.OreFlowID OreFlowID1, o1.LevelNumber from mineware.dbo.tbl_bcs_oreflowentities o, mineware.dbo.tbl_bcs_oreflowentities o1  \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "where  \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "o.ParentOreFlowID = o1.OreFlowID and  \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "o.OreFlowCode = 'BH') b on a.oreflowid = b.oreflowid1) a group by lvl ) a) a, \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "(select top(1) rate from tbl_bcs_hoist where prodmonth < @prodmonth \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "order by prodmonth desc) b , \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "(select top(1) * from tbl_bcs_trammingper where prodmonth < @prodmonth \r\n ";
            _dbManDataGenData.SqlStatement = _dbManDataGenData.SqlStatement + "order by prodmonth desc) c) a \r\n ";
            _dbManDataGenData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManDataGenData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManDataGenData.ResultsTableName = "Data";
            _dbManDataGenData.ExecuteInstruction();

            if (_dbManDataGenData.ResultsDataTable.Rows.Count > 0)
            {
                LocoDriverPotLbl.Text = _dbManDataGenData.ResultsDataTable.Rows[0]["LocoPot"].ToString();
            }
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OnCloseTabRequest(new CloseTabArg(tabCaption));
        }
    }
}
