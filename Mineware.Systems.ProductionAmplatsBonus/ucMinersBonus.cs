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
    public partial class ucMinersBonus : BaseUserControl
    {
       // DataSet ReportMinerBonus = new DataSet();
        Report report = new Report();
        private string _reportFolder = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\Reports\";
        public ucMinersBonus()
        {
            InitializeComponent();
            FormRibbonPages.Add(rpReports);
            FormActiveRibbonPage = rpReports;
            FormMainRibbonPage = rpReports;
            RibbonControl = rcReports;
        }

        private void frmMinersBonus_Load(object sender, EventArgs e)
        {
            editProdmonth.EditValue = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsDate(ProductionAmplatsGlobalTSysSettings._currentProductionMonth.ToString());
        }

      

        private void ProdMonth1Txt_TextChanged(object sender, EventArgs e)
        {
            LoadListBoxStoping();
        }

        public void LoadListBoxStoping()
        {

            if (editActivity.EditValue.ToString() == "0")
            {
                //
                //  Load Incomplete List Box
                //
                lbIncomplete.Items.Clear();

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " select distinct(sec) sec from (select SUBSTRING(orgunit,1,6) sec from tbl_BCS_StopingRepNew b \r\n " +
                                      " where SUBSTRING(orgunit,1,6) not in (select Gang from Mineware.dbo.tbl_BCS_New_Status where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and Activity = 'SM') \r\n " +
                                      " and SUBSTRING(orgunit,1,6) not in (select distinct(orgunit) orgunit from dbo.tbl_BCS_ARMS_Interface_TransferNew where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and ActivityCode <> 1 and Type = '08' )  \r\n " +
                                      " and b.prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "')a order by sec \r\n " +
                                      " \r\n " +
                                      " ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Stoping";
                _dbMan.ExecuteInstruction();

                DataTable dt = _dbMan.ResultsDataTable;

                lbIncomplete.Items.Add("");
                foreach (DataRow dr in dt.Rows)
                {
                    lbIncomplete.Items.Add(dr["sec"].ToString());
                }

                //
                //Load Printed Listbox
                //
                lbTransfer.Items.Clear();

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = " select Gang from Mineware.dbo.tbl_BCS_New_Status where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  \r\n " +
                                       " and Activity = 'SM' and gang not in ( \r\n " +
                                       " select distinct(orgunit) orgunit from dbo.tbl_BCS_ARMS_Interface_TransferNew  \r\n " +
                                       " where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and ActivityCode <> 1 and Type = '08') \r\n " +
                                       " ";
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ExecuteInstruction();

                DataTable dt1 = _dbMan1.ResultsDataTable;

                lbTransfer.Items.Add("");
                foreach (DataRow dr1 in dt1.Rows)
                {
                    lbTransfer.Items.Add(dr1["gang"].ToString());
                }

                //
                //Load Transferred Listbox
                //
                lbPrinted.Items.Clear();

                MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
                _dbMan2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan2.SqlStatement = " select distinct(orgunit) orgunit from dbo.tbl_BCS_ARMS_Interface_TransferNew  \r\n " +
                                       " where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and ActivityCode <> 1 and Type = '08' \r\n " +
                                       " \r\n " +
                                       " \r\n " +
                                       " ";
                _dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan2.ExecuteInstruction();

                DataTable dt2 = _dbMan2.ResultsDataTable;

                lbPrinted.Items.Add("");
                foreach (DataRow dr2 in dt2.Rows)
                {
                    lbPrinted.Items.Add(dr2["orgunit"].ToString());
                }


            }


            if (editActivity.EditValue.ToString() == "1")
            {
                //
                //  Load Incomplete List Box
                //
                lbIncomplete.Items.Clear();

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " select distinct(sec) sec from (select SUBSTRING(orgunit,1,6) sec from [tbl_BCS_DevRepNew] b \r\n " +
                                      " where SUBSTRING(orgunit,1,6) not in (select Gang from Mineware.dbo.tbl_BCS_New_Status where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and Activity = 'DM') \r\n " +
                                      " and SUBSTRING(orgunit,1,6) not in (select distinct(orgunit) orgunit from dbo.tbl_BCS_ARMS_Interface_TransferNew where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and ActivityCode = 1 and Type = '08' )  \r\n " +
                                      " and b.prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "')a order by sec \r\n " +
                                      " \r\n " +
                                      " ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Stoping";
                _dbMan.ExecuteInstruction();

                DataTable dt = _dbMan.ResultsDataTable;

                lbIncomplete.Items.Add("");
                foreach (DataRow dr in dt.Rows)
                {
                    lbIncomplete.Items.Add(dr["sec"].ToString());
                }

                //
                //Load Printed Listbox
                //
                lbTransfer.Items.Clear();

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = " select Gang from Mineware.dbo.tbl_BCS_New_Status where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  \r\n " +
                                       " and Activity = 'DM' and gang not in ( \r\n " +
                                       " select distinct(orgunit) orgunit from dbo.tbl_BCS_ARMS_Interface_TransferNew  \r\n " +
                                       " where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and ActivityCode = 1 and Type = '08') \r\n " +
                                       " ";
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ExecuteInstruction();

                DataTable dt1 = _dbMan1.ResultsDataTable;

                lbTransfer.Items.Add("");
                foreach (DataRow dr1 in dt1.Rows)
                {
                    lbTransfer.Items.Add(dr1["gang"].ToString());
                }

                //
                //Load Transferred Listbox
                //
                lbPrinted.Items.Clear();

                MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
                _dbMan2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan2.SqlStatement = " select distinct(orgunit) orgunit from dbo.tbl_BCS_ARMS_Interface_TransferNew  \r\n " +
                                       " where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and ActivityCode = 1 and Type = '08' \r\n " +
                                       " \r\n " +
                                       " \r\n " +
                                       " ";
                _dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan2.ExecuteInstruction();

                DataTable dt2 = _dbMan2.ResultsDataTable;

                lbPrinted.Items.Add("");
                foreach (DataRow dr2 in dt2.Rows)
                {
                    lbPrinted.Items.Add(dr2["orgunit"].ToString());
                }


            }

        }

        private void showBtn_Click(object sender, EventArgs e)
        {
            

        }

        private void lbIncomplete_Click(object sender, EventArgs e)
        {
            if (lbTransfer.Items.Count > 0)
            {
                lbTransfer.SetSelected(0, false);
            }
            if (lbPrinted.Items.Count > 0)
            {
                lbPrinted.SetSelected(0, false);
            }
            Minerlbl.Text = lbIncomplete.SelectedItem.ToString();
            
        }

        private void lbPrinted_Click(object sender, EventArgs e)
        {
            if (lbTransfer.Items.Count > 0)
            {
                lbTransfer.SetSelected(0, false);
            }
            if (lbIncomplete.Items.Count > 0)
            {
                lbIncomplete.SetSelected(0, false);
            }
            Minerlbl.Text = lbPrinted.SelectedItem.ToString();
        }

        private void lbTransfer_Click(object sender, EventArgs e)
        {
            if (lbIncomplete.Items.Count > 0)
            {
                lbIncomplete.SetSelected(0, false);
            }
            if (lbPrinted.Items.Count > 0)
            {
                lbIncomplete.SetSelected(0, false);
            }
            Minerlbl.Text = lbTransfer.SelectedItem.ToString();
        }

        private void Close1Btn_Click(object sender, EventArgs e)
        {
            
        }

        private void rdbDev_CheckedChanged(object sender, EventArgs e)
        {
            LoadListBoxStoping();
        }

        private void rdbStoping_CheckedChanged(object sender, EventArgs e)
        {
            LoadListBoxStoping();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {

        }
       
        
        
        DialogResult result;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
           
        
        }

        private void btnShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            string sec = Minerlbl.Text + "%";

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);


            if (editActivity.EditValue.ToString() == "0")
            {
                _dbMan.SqlStatement = " select '1' id, orgunit, workplace, averageemployees, production, wastesqm ,productionsw, reefid, ds_lti dslti, ns_lti nslti from ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " tbl_BCS_StopingRepNew a, mineware.dbo.tbl_bcs_Workplace w  \r\n ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " where a.workplace = w.description and gangtype = 'S' \r\n ";
            }
            if (editActivity.EditValue.ToString() == "1")
            {
                _dbMan.SqlStatement = " select '1' id, orgunit, workplace, averageemployees, production, 0 wastesqm , 0 productionsw, reefid, ds_lti dslti, ns_lti nslti from ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " tbl_BCS_DevRepNew a, mineware.dbo.tbl_bcs_Workplace w where a.workplace = w.description  \r\n ";

            }
            _dbMan.SqlStatement = _dbMan.SqlStatement + " and prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";
            _dbMan.SqlStatement = _dbMan.SqlStatement + " and AverageEmployees is not null \r\n ";
            _dbMan.SqlStatement = _dbMan.SqlStatement + " and OrgUnit like '" + sec + "' \r\n ";
            _dbMan.SqlStatement = _dbMan.SqlStatement + " ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ResultsTableName = "Stoping";
            _dbMan.ExecuteInstruction();

            DataTable dt = _dbMan.ResultsDataTable;

            dataGridView1.Rows.Clear();
            //DataGrid.RowCount = 0;
            dataGridView1.ColumnCount = 50;
            dataGridView1.RowCount = 200;

            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 5;

            dataGridView1.Columns[1].HeaderText = "orgunit";
            dataGridView1.Columns[1].Width = 120;

            dataGridView1.Columns[2].HeaderText = "workplace";
            dataGridView1.Columns[2].Width = 50;


            dataGridView1.Columns[3].HeaderText = "AverageEmployees";
            dataGridView1.Columns[3].Width = 50;


            dataGridView1.Columns[4].HeaderText = "Production";
            dataGridView1.Columns[4].Width = 50;

            dataGridView1.Columns[5].HeaderText = "Waste Sqm";
            dataGridView1.Columns[5].Width = 50;


            dataGridView1.Columns[6].HeaderText = "Production SW";
            dataGridView1.Columns[6].Width = 50;


            dataGridView1.Columns[7].HeaderText = "Basic";
            dataGridView1.Columns[7].Width = 50;

            dataGridView1.Columns[8].HeaderText = "SW";
            dataGridView1.Columns[8].Width = 50;


            DataGrid.Rows.Clear();
            //DataGrid.RowCount = 0;
            DataGrid.ColumnCount = 50;
            DataGrid.RowCount = 200;


            DataGrid.Columns[0].HeaderText = "ID";
            DataGrid.Columns[0].Width = 50;

            DataGrid.Columns[1].HeaderText = "time";
            DataGrid.Columns[1].Width = 50;

            DataGrid.Columns[2].HeaderText = "prodmonth";
            DataGrid.Columns[2].Width = 50;

            DataGrid.Columns[3].HeaderText = "act";
            DataGrid.Columns[3].Width = 50;

            DataGrid.Columns[4].HeaderText = "ind";
            DataGrid.Columns[4].Width = 50;

            DataGrid.Columns[5].HeaderText = "initials";
            DataGrid.Columns[5].Width = 50;

            DataGrid.Columns[6].HeaderText = "surname";
            DataGrid.Columns[6].Width = 50;

            DataGrid.Columns[7].HeaderText = "cat";
            DataGrid.Columns[7].Width = 50;

            DataGrid.Columns[8].HeaderText = "org";
            DataGrid.Columns[8].Width = 50;

            DataGrid.Columns[9].HeaderText = "shift";
            DataGrid.Columns[9].Width = 50;

            DataGrid.Columns[10].HeaderText = "workplace";
            DataGrid.Columns[10].Width = 50;

            DataGrid.Columns[11].HeaderText = "pas";
            DataGrid.Columns[11].Width = 50;

            DataGrid.Columns[12].HeaderText = "gross";
            DataGrid.Columns[12].Width = 50;

            DataGrid.Columns[13].HeaderText = "shifts";
            DataGrid.Columns[13].Width = 50;

            DataGrid.Columns[14].HeaderText = "awops";
            DataGrid.Columns[14].Width = 50;

            DataGrid.Columns[15].HeaderText = "element";
            DataGrid.Columns[15].Width = 50;


            DataGrid.Columns[16].HeaderText = "Non PU";
            DataGrid.Columns[16].Width = 50;

            DataGrid.Columns[17].HeaderText = "RI";
            DataGrid.Columns[17].Width = 50;

            DataGrid.Columns[18].HeaderText = "LTI";
            DataGrid.Columns[18].Width = 50;

            DataGrid.Columns[19].HeaderText = "PhyCond";
            DataGrid.Columns[19].Width = 50;

            DataGrid.Columns[20].HeaderText = "NetAmount";
            DataGrid.Columns[20].Width = 50;

            DataGrid.Columns[21].HeaderText = "Type";
            DataGrid.Columns[21].Width = 50;



            DataGrid.Columns[22].HeaderText = "Trans";
            DataGrid.Columns[22].Width = 50;

            DataGrid.Columns[23].HeaderText = "TransTime";
            DataGrid.Columns[23].Width = 50;

            DataGrid.Columns[24].HeaderText = "hole";
            DataGrid.Columns[24].Width = 50;

            DataGrid.Columns[25].HeaderText = "finpay";
            DataGrid.Columns[25].Width = 50;







            MWDataManager.clsDataAccess _dbManData = new MWDataManager.clsDataAccess();
            _dbManData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManData.SqlStatement = "Select  '17' Ordera,'1' id, '' orgunit, '' workplace,  '' AvgEmp, '' Prod, '' WasteSQM, '' ProdSW , '' Basic, '' SW, '' tt, '' dslti, '' nslti ";
            _dbManData.SqlStatement = _dbManData.SqlStatement + " union   ";
            _dbManData.SqlStatement = _dbManData.SqlStatement + "Select '18' Ordera, '1' id, '' orgunit, '' workplace,  '' AvgEmp, '' Prod, '' WasteSQM, '' ProdSW , '' Basic, '' SW, '' tt, '' dslti, '' nslti ";
            _dbManData.SqlStatement = _dbManData.SqlStatement + " union   ";
            _dbManData.SqlStatement = _dbManData.SqlStatement + "Select  '19' Ordera,'1' id, '' orgunit, '' workplace,  '' AvgEmp, '' Prod, '' WasteSQM, '' ProdSW , '' Basic, '' SW , '' tt, '' dslti, '' nslti";
            _dbManData.SqlStatement = _dbManData.SqlStatement + " union   ";
            _dbManData.SqlStatement = _dbManData.SqlStatement + "Select  '99' Ordera,'1' id, '' orgunit, '' workplace,  '' AvgEmp, '' Prod, '' WasteSQM, '' ProdSW , '' Basic, '' SW, '' tt, '' dslti, '' nslti ";

            int row = 0;

            decimal dslti = 0;
            decimal nslti = 0;

            decimal dsltipen = 0;
            decimal nsltipen = 0;

            decimal totalpay = 0;

            foreach (DataRow r in dt.Rows)
            {


                //int NewRow = dataGridView1.Rows.Add();
                dataGridView1.Rows[row].Cells[0].Value = r["id"].ToString();
                dataGridView1.Rows[row].Cells[1].Value = r["orgunit"].ToString();
                dataGridView1.Rows[row].Cells[2].Value = r["workplace"].ToString();
                dataGridView1.Rows[row].Cells[3].Value = r["averageemployees"].ToString();
                dataGridView1.Rows[row].Cells[4].Value = r["production"].ToString();
                dataGridView1.Rows[row].Cells[5].Value = r["wastesqm"].ToString();
                dataGridView1.Rows[row].Cells[6].Value = r["productionsw"].ToString();

                if (Convert.ToDecimal(r["dslti"].ToString()) > 0)
                    dslti = dslti + Convert.ToDecimal(r["dslti"].ToString());

                if (Convert.ToDecimal(r["nslti"].ToString()) > 0)
                    nslti = nslti + Convert.ToDecimal(r["nslti"].ToString());


                MWDataManager.clsDataAccess _dbManStartRand = new MWDataManager.clsDataAccess();
                _dbManStartRand.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                if (editActivity.EditValue.ToString() == "0")
                {


                    _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " ";


                    if (_dbMan.ResultsDataTable.Rows[row]["reefid"].ToString() == "2")
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " select case when '" + _dbMan.ResultsDataTable.Rows[row]["production"].ToString() + "' = 0 then 0 else basic end as basic  from (select isnull(MAX(BIPStopingAmount),0) basic from tbl_BCS_BIPStoping \r\n ";
                    else
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " select case when '" + _dbMan.ResultsDataTable.Rows[row]["production"].ToString() + "' = 0 then 0 else basic end as basic  from (select isnull(MAX(BIPStopingAmount),0) basic from tbl_BCS_BIPStopingMer \r\n ";
                    _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";
                    _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " and BIPStopingAvgEmp = '" + _dbMan.ResultsDataTable.Rows[row]["averageemployees"].ToString() + "' and BIPStopingSQM <= '" + _dbMan.ResultsDataTable.Rows[row]["production"].ToString() + "' \r\n ";
                    _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " )a ";
                    _dbManStartRand.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManStartRand.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManStartRand.ResultsTableName = "new";
                    _dbManStartRand.ExecuteInstruction();


                }


                string DevType = "";

                if (editActivity.EditValue.ToString() == "1")
                {
                    if (r["orgunit"].ToString().Substring(6, 1) == "F")
                    {
                        DevType = "FWD";
                    }

                    if (r["orgunit"].ToString().Substring(6, 1) == "R")
                    {
                        DevType = "RSE";
                    }

                    if (r["orgunit"].ToString().Substring(6, 1) == "B")
                    {
                        DevType = "BH";
                    }
                }


                string WaterEndApplied1 = "";

                MWDataManager.clsDataAccess _dbManWaterA1 = new MWDataManager.clsDataAccess();
                _dbManWaterA1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManWaterA1.SqlStatement = _dbManWaterA1.SqlStatement + " select * from dbo.tbl_BCS_OrgunitWaterEnd \r\n ";
                _dbManWaterA1.SqlStatement = _dbManWaterA1.SqlStatement + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";
                _dbManWaterA1.SqlStatement = _dbManWaterA1.SqlStatement + " and orgunit = '" + r["orgunit"].ToString() + "' \r\n ";

                _dbManWaterA1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManWaterA1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManWaterA1.ExecuteInstruction();



                if (_dbManWaterA1.ResultsDataTable.Rows.Count > 0)
                {
                    WaterEndApplied1 = "Y";
                }
                else
                {
                    WaterEndApplied1 = "N";
                }

                MWDataManager.clsDataAccess _dbManWaterEnd1 = new MWDataManager.clsDataAccess();
                _dbManWaterEnd1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManWaterEnd1.SqlStatement = _dbManWaterEnd1.SqlStatement + "select * from dbo.tbl_BCS_OrgunitFactor \r\n ";
                _dbManWaterEnd1.SqlStatement = _dbManWaterEnd1.SqlStatement + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";
                _dbManWaterEnd1.SqlStatement = _dbManWaterEnd1.SqlStatement + " and orgunit = '" + _dbMan.ResultsDataTable.Rows[row]["orgunit"].ToString() + "' \r\n ";

                _dbManWaterEnd1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManWaterEnd1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManWaterEnd1.ExecuteInstruction();

                Decimal WaterEndFactor1 = 1;
                Decimal WaterEndMeters1 = 1;

                foreach (DataRow dr in _dbManWaterEnd1.ResultsDataTable.Rows)
                {
                    WaterEndFactor1 = Convert.ToDecimal(dr["Factor"].ToString());
                    WaterEndMeters1 = Convert.ToDecimal(dr["Metres"].ToString());
                }



                if (editActivity.EditValue.ToString() == "1")
                {
                    if (DevType == "RSE")
                    {
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " Select isnull(max(BIPDevRaisesAmount),0) basic \r\n  ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " from tbl_BCS_BIPDevRaises \r\n  ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " where  BIPDevRaisesSQM <= '" + Convert.ToDecimal(_dbMan.ResultsDataTable.Rows[row]["production"].ToString()) + "' \r\n  ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " and  BIPDevRaisesAvgEmp = '" + _dbMan.ResultsDataTable.Rows[row]["averageemployees"].ToString() + "' \r\n ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " and ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";

                    }

                    if (DevType == "FWD")
                    {
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " Select isnull(max(BIPDevLateralAmount),0) basic \r\n  ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " from tbl_BCS_BIPDevLateral \r\n  ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " where  BIPDevLateralSQM <= '" + Convert.ToDecimal(_dbMan.ResultsDataTable.Rows[row]["production"].ToString()) + "' \r\n  ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " and  BIPDevLateralAvgEmp = '" + _dbMan.ResultsDataTable.Rows[row]["averageemployees"].ToString() + "' \r\n ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " and ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";


                    }

                    if (DevType == "BH")
                    {
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " Select isnull(max(BIPDevBHAmount),0) basic \r\n  ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " from tbl_BCS_BIPDevBoxHole \r\n  ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " where  BIPDevBHSQM <= '" + Convert.ToDecimal(_dbMan.ResultsDataTable.Rows[row]["production"].ToString()) + "' \r\n  ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " and  BIPDevBHAvgEmp = '" + _dbMan.ResultsDataTable.Rows[row]["averageemployees"].ToString() + "' \r\n ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " and ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";


                    }


                    if (WaterEndApplied1 == "Y")
                    {
                        _dbManStartRand.SqlStatement = "";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " Select isnull(max(BIPDevLateralAmount),0) basic \r\n  ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " from [tbl_BCS_BIPDevLateralWaterEnds] \r\n  ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " where  BIPDevLateralSQM <= '" + Convert.ToDecimal(_dbMan.ResultsDataTable.Rows[row]["production"].ToString()) + "' \r\n  ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " and  BIPDevLateralAvgEmp = '" + _dbMan.ResultsDataTable.Rows[row]["averageemployees"].ToString() + "' \r\n ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " and ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";

                    }


                    _dbManStartRand.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManStartRand.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManStartRand.ResultsTableName = "new";
                    _dbManStartRand.ExecuteInstruction();

                }

                DataTable dt1 = _dbManStartRand.ResultsDataTable;

                if (dt1.Rows.Count > 0)
                {

                    dataGridView1.Rows[row].Cells[7].Value = _dbManStartRand.ResultsDataTable.Rows[0]["basic"].ToString();
                }
                else
                {
                    dataGridView1.Rows[row].Cells[7].Value = 0;
                }


                dataGridView1.Rows[row].Cells[7].Value = Convert.ToDecimal(dataGridView1.Rows[row].Cells[7].Value) * WaterEndFactor1;



                MWDataManager.clsDataAccess _dbManIncSweeps1 = new MWDataManager.clsDataAccess();
                _dbManIncSweeps1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManIncSweeps1.SqlStatement = _dbManIncSweeps1.SqlStatement + " select case when " + _dbMan.ResultsDataTable.Rows[row]["productionsw"].ToString() + " = 0 then 0 else BIPStopingAmount end as BIPStopingAmount from ( Select isnull(max(BIPStopingAmount),0) BIPStopingAmount \r\n  ";
                if (_dbMan.ResultsDataTable.Rows[row]["reefid"].ToString() != "2")
                    _dbManIncSweeps1.SqlStatement = _dbManIncSweeps1.SqlStatement + " from mineware.dbo.tbl_BCS_BIPStopingswmer \r\n  ";
                else
                    _dbManIncSweeps1.SqlStatement = _dbManIncSweeps1.SqlStatement + " from dbo.tbl_BCS_BIPStopingsw \r\n  ";
                _dbManIncSweeps1.SqlStatement = _dbManIncSweeps1.SqlStatement + " where BIPStopingSQM <= " + _dbMan.ResultsDataTable.Rows[row]["productionsw"].ToString() + " \r\n  ";
                _dbManIncSweeps1.SqlStatement = _dbManIncSweeps1.SqlStatement + " and BIPStopingAvgEmp = " + _dbMan.ResultsDataTable.Rows[row]["averageemployees"].ToString() + " \r\n ";
                _dbManIncSweeps1.SqlStatement = _dbManIncSweeps1.SqlStatement + " and ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  \r\n ";

                if (editActivity.EditValue.ToString() == "1")
                    _dbManIncSweeps1.SqlStatement = _dbManIncSweeps1.SqlStatement + " and ProdMonth = '" + "221603" + "'  \r\n ";

                _dbManIncSweeps1.SqlStatement = _dbManIncSweeps1.SqlStatement + " )a   ";



                _dbManIncSweeps1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManIncSweeps1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManIncSweeps1.ResultsTableName = "Sweeps";
                _dbManIncSweeps1.ExecuteInstruction();

                DataTable dt2 = _dbManIncSweeps1.ResultsDataTable;

                if (dt2.Rows.Count > 0)
                {

                    dataGridView1.Rows[row].Cells[8].Value = _dbManIncSweeps1.ResultsDataTable.Rows[0]["BIPStopingAmount"].ToString();
                }
                else
                {
                    dataGridView1.Rows[row].Cells[8].Value = 0;
                }


                dataGridView1.Rows[row].Cells[8].Value = Convert.ToDecimal(dataGridView1.Rows[row].Cells[8].Value) * WaterEndFactor1;


                //////Select Data



                _dbManData.SqlStatement = _dbManData.SqlStatement + " union select '" + row + "' rowa, '" + dataGridView1.Rows[row].Cells[0].Value + "' id, \r\n  ";
                _dbManData.SqlStatement = _dbManData.SqlStatement + " '" + dataGridView1.Rows[row].Cells[1].Value + "' orgunit, \r\n  ";
                _dbManData.SqlStatement = _dbManData.SqlStatement + " '" + dataGridView1.Rows[row].Cells[2].Value + "' workplace, \r\n  ";
                _dbManData.SqlStatement = _dbManData.SqlStatement + " '" + Math.Round(Convert.ToDecimal(dataGridView1.Rows[row].Cells[3].Value), 0) + "' AvgEmp ,\r\n ";
                _dbManData.SqlStatement = _dbManData.SqlStatement + " '" + Math.Round(Convert.ToDecimal(dataGridView1.Rows[row].Cells[4].Value), 0) + "' Prod, \r\n ";
                _dbManData.SqlStatement = _dbManData.SqlStatement + " '" + Math.Round(Convert.ToDecimal(dataGridView1.Rows[row].Cells[5].Value), 0) + "' WasteSQM ,\r\n ";
                _dbManData.SqlStatement = _dbManData.SqlStatement + " '" + Math.Round(Convert.ToDecimal(dataGridView1.Rows[row].Cells[6].Value), 0) + "' ProdSW, \r\n ";
                _dbManData.SqlStatement = _dbManData.SqlStatement + " '" + Math.Round(Convert.ToDecimal(dataGridView1.Rows[row].Cells[7].Value), 0) + "' Basic, \r\n ";
                _dbManData.SqlStatement = _dbManData.SqlStatement + " '" + Math.Round(Convert.ToDecimal(dataGridView1.Rows[row].Cells[8].Value), 0) + "' SW, \r\n ";
                _dbManData.SqlStatement = _dbManData.SqlStatement + " '" + Math.Round(Convert.ToDecimal(dataGridView1.Rows[row].Cells[8].Value) + Convert.ToDecimal(dataGridView1.Rows[row].Cells[7].Value), 0) + "' tt, '" + r["dslti"].ToString() + "' dslti,  '" + r["nslti"].ToString() + "' nslti \r\n ";


                totalpay = totalpay + Math.Round(Convert.ToDecimal(dataGridView1.Rows[row].Cells[8].Value) + Convert.ToDecimal(dataGridView1.Rows[row].Cells[7].Value), 0);

                row = row + 1;
            }





            _dbManData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManData.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManData.ResultsTableName = "GridData";
            _dbManData.ExecuteInstruction();

            dsltipen = Convert.ToDecimal(1.2);

            if (dslti > 0)
                dsltipen = Convert.ToDecimal(0.5);


            nsltipen = Convert.ToDecimal(1.2);

            if (nslti > 0)
                nsltipen = Convert.ToDecimal(0.5);


            DataTable dt3 = _dbManData.ResultsDataTable;

            DataSet ReportMinerBonus = new DataSet();


            MWDataManager.clsDataAccess _dbManData2 = new MWDataManager.clsDataAccess();
            _dbManData2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            if (editActivity.EditValue.ToString() == "0")
                _dbManData2.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_StopingMiner]  '" + sec + "', '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + dsltipen + "', '" + nsltipen + "' ";
            else
                _dbManData2.SqlStatement = " exec  mineware.[dbo].[sp_BMCS_DevMiner]  '" + sec + "', '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + dsltipen + "', '" + nsltipen + "' ";
            _dbManData2.SqlStatement = _dbManData2.SqlStatement + "    ";
            _dbManData2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManData2.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManData2.ResultsTableName = "Data2";
            _dbManData2.ExecuteInstruction();


            DataTable Emp = _dbManData2.ResultsDataTable;
            row = 0;

            string act1 = "";

            if (editActivity.EditValue.ToString() == "0")
                act1 = "0";
            if (editActivity.EditValue.ToString() == "1")
                act1 = "1";

            foreach (DataRow r in Emp.Rows)
            {

                if (r["industrynumber"].ToString() != "")
                {

                    DataGrid.Rows[row].Cells[0].Value = "1";
                    DataGrid.Rows[row].Cells[1].Value = "";
                    DataGrid.Rows[row].Cells[2].Value = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString();
                    DataGrid.Rows[row].Cells[3].Value = act1;
                    DataGrid.Rows[row].Cells[4].Value = r["industrynumber"].ToString();
                    DataGrid.Rows[row].Cells[5].Value = r["initials"].ToString();
                    DataGrid.Rows[row].Cells[6].Value = r["surname"].ToString();
                    DataGrid.Rows[row].Cells[7].Value = r["category"].ToString();

                    DataGrid.Rows[row].Cells[8].Value = Minerlbl.Text;


                    DataGrid.Rows[row].Cells[9].Value = "D";

                    if (r["lbl"].ToString() == "Night Shift Cleaner")
                        DataGrid.Rows[row].Cells[9].Value = "N";

                    DataGrid.Rows[row].Cells[10].Value = "";
                    DataGrid.Rows[row].Cells[11].Value = "";

                    if (act1 == "0")
                    {
                        if (r["lbl"].ToString() == "Night Shift Cleaner")
                        {
                            DataGrid.Rows[row].Cells[12].Value = (totalpay / Convert.ToDecimal(r["totshifts"].ToString())) * Convert.ToDecimal(r["ss"].ToString()) * Convert.ToDecimal(2) * Convert.ToDecimal(r["lti"].ToString());
                            DataGrid.Rows[row].Cells[15].Value = "Night Shift Cleaning Bonus";
                        }
                        else
                        {


                            DataGrid.Rows[row].Cells[12].Value = (totalpay / Convert.ToDecimal(r["totshifts"].ToString())) * Convert.ToDecimal(r["ss"].ToString()) * Convert.ToDecimal(5) * Convert.ToDecimal(r["lti"].ToString());
                            DataGrid.Rows[row].Cells[15].Value = "Stoping Contract Bonus";
                        }

                        DataGrid.Rows[row].Cells[18].Value = dslti;
                    }


                    if (act1 == "1")
                    {
                        if (r["lbl"].ToString() == "Night Shift Cleaner")
                        {
                            DataGrid.Rows[row].Cells[12].Value = (totalpay / (Convert.ToDecimal(r["totshifts"].ToString()) + Convert.ToDecimal(0.00000001))) * Convert.ToDecimal(r["ss"].ToString()) * Convert.ToDecimal(3) * Convert.ToDecimal(r["lti"].ToString());
                            DataGrid.Rows[row].Cells[15].Value = "Night Shift Cleaning Bonus";
                        }
                        else
                        {
                            DataGrid.Rows[row].Cells[12].Value = (totalpay / (Convert.ToDecimal(r["totshifts"].ToString()) + Convert.ToDecimal(0.00000001))) * Convert.ToDecimal(r["ss"].ToString()) * Convert.ToDecimal(5) * Convert.ToDecimal(r["lti"].ToString());

                            DataGrid.Rows[row].Cells[15].Value = "Development Contract Bonus";
                        }

                        DataGrid.Rows[row].Cells[18].Value = nslti;
                    }

                    DataGrid.Rows[row].Cells[13].Value = Convert.ToDecimal(r["ss"].ToString());

                    DataGrid.Rows[row].Cells[14].Value = Convert.ToDecimal(r["awpno"].ToString());

                    DataGrid.Rows[row].Cells[16].Value = 0;

                    DataGrid.Rows[row].Cells[17].Value = 0;
                    DataGrid.Rows[row].Cells[19].Value = 0;


                    DataGrid.Rows[row].Cells[20].Value = Convert.ToDecimal(DataGrid.Rows[row].Cells[12].Value) - (Convert.ToDecimal(DataGrid.Rows[row].Cells[12].Value) * Convert.ToDecimal(r["total"].ToString()) / 100);

                    DataGrid.Rows[row].Cells[12].Value = Math.Round(Convert.ToDecimal(DataGrid.Rows[row].Cells[12].Value), 2);
                    DataGrid.Rows[row].Cells[20].Value = Math.Round(Convert.ToDecimal(DataGrid.Rows[row].Cells[20].Value), 2);

                    DataGrid.Rows[row].Cells[21].Value = "08";

                    DataGrid.Rows[row].Cells[22].Value = "";

                    DataGrid.Rows[row].Cells[23].Value = "";

                    DataGrid.Rows[row].Cells[24].Value = "0";
                    DataGrid.Rows[row].Cells[25].Value = "0";








                    row = row + 1;
                }

            }

            MWDataManager.clsDataAccess _dbManDataSB1 = new MWDataManager.clsDataAccess();
            _dbManDataSB1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  declare @Prodmonth varchar(10)  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  declare @Org varchar(10)  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  set @Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  set @Org = '" + Minerlbl.Text + "'  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  declare @Start datetime  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  set @Start = (select min(calendardate) from mineware.dbo.tbl_BCS_Planning where substring(orgunitds,0,6) =  substring(@Org,0,6) and Prodmonth = @Prodmonth and activity = 0)  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  declare @End datetime  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  set @End = (select max(calendardate) from mineware.dbo.tbl_BCS_Planning where substring(orgunitds,0,6) =  substring(@Org,0,6) and Prodmonth = @Prodmonth and activity = 0)  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  select a.*, name, Shift, c.atwork, isnull(ttawop,0) Awop, isnull(SAWOP,0) Sick,  cond from (select distinct(industrynumber) ind from mineware.[dbo].[tbl_BCS_Gangs_3Month]  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  where orgunit =  substring(@Org,1,5) and prodmonth = @Prodmonth) a  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  left outer join   ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  (select * from tbl_BCS_Condition  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  where orgunit =  @Org and  prodmonth = @Prodmonth) b on a.ind = b.industrynumber  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  left outer join  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  (select industrynumber, count(industrynumber) atwork, shift from  mineware.[dbo].[tbl_BCS_Gangs_3Month]  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  where orgunit =  substring(@Org,1,5) and prodmonth = @Prodmonth  and codes in ('N','ON', 'LOaa', 'DR', 'NA','HA1','PW','TE', 'TI1')  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  group by industrynumber, shift) c on a.ind = c.industrynumber  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  left outer join  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  (select a.* from (select industrynumber ind1, count(industrynumber) ttawop from Mineware.dbo.tbl_Import_BMCS_Clocking_Total  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  where thedate >= @Start  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  and thedate <= @End and leaveflag in ('QA','A', 'AW') group by industrynumber ) a) awtot   ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  on a.ind = awtot.ind1  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  left outer join   ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  (select a.*, b.factor SAWOP from (select industrynumber ind1, count(industrynumber) ssawop from Mineware.dbo.tbl_Import_BMCS_Clocking_Total  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  where thedate >= @Start  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  and thedate <= @End and leaveflag in ('S') group by industrynumber ) a   ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  left outer join  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  (select * from mineware.[dbo].[tbl_BCS_AbsenteeismFactors] where prodmonth = @Prodmonth) b on a.ssawop = b.shiftno) awtot1   ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  on a.ind = awtot1.ind1  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  left outer join  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  (select industrynumber, max(initials+'.'+Surname) name, max(initials) initials, max(Surname) Surname  from Mineware.dbo.tbl_Import_BCS_Personnel_Latest group by industrynumber) z  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  on a.ind = z.industrynumber  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  union select 'z991', '', '', '', 0, 0, 0  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  union select 'z992', '', '', '', 0, 0, 0  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  union select 'z993', '', '', '', 0, 0, 0  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  union select 'z994', '', '', '', 0, 0, 0  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  union select 'z995', '', '', '', 0, 0, 0  ";
            _dbManDataSB1.SqlStatement = _dbManDataSB1.SqlStatement + "  order by ind  ";

            _dbManDataSB1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManDataSB1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManDataSB1.ResultsTableName = "SB";
            _dbManDataSB1.ExecuteInstruction();

            ///Prodmonth & Orgunit
            ///
            MWDataManager.clsDataAccess _dbManDataSB2 = new MWDataManager.clsDataAccess();
            _dbManDataSB2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            _dbManDataSB2.SqlStatement = _dbManDataSB2.SqlStatement + "  Select  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + Minerlbl.Text + "' ";


            _dbManDataSB2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManDataSB2.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManDataSB2.ResultsTableName = "Header";
            _dbManDataSB2.ExecuteInstruction();

            ReportMinerBonus.Tables.Add(_dbMan.ResultsDataTable);
            ReportMinerBonus.Tables.Add(_dbManData.ResultsDataTable);
            ReportMinerBonus.Tables.Add(_dbManData2.ResultsDataTable);
            ReportMinerBonus.Tables.Add(_dbManDataSB1.ResultsDataTable);
            ReportMinerBonus.Tables.Add(_dbManDataSB2.ResultsDataTable);



            report.RegisterData(ReportMinerBonus);
            if (editActivity.EditValue.ToString() == "0")
                report.Load(_reportFolder+ "MinerBonusStope.frx");
            else
                report.Load(_reportFolder+ "MinerBonusDev.frx");

            //put back in design
            //  report.Design();

            pcReport.Clear();
            report.Prepare();
            report.Preview = pcReport;
            report.ShowPrepared();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void btnTransfer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            result = MessageBox.Show("Are you sure you want to transfer the Bonus Details to the ARMS Interface?", "Transfer Record", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {

                if ("" == "")
                {

                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " select * from dbo.tbl_BCS_ARMS_Interface_TransferNew  \r\n  ";
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " Where OrgUnit = '" + Minerlbl.Text + "'  \r\n ";
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
                    _dbMandelete.SqlStatement = _dbMandelete.SqlStatement + " Delete dbo.tbl_BCS_ARMS_Interface_TransferNew ";
                    _dbMandelete.SqlStatement = _dbMandelete.SqlStatement + " Where OrgUnit = '" + Minerlbl.Text + "'  ";
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
                                _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " Insert into dbo.tbl_BCS_ARMS_Interface_TransferNew  values (  \r\n ";
                                _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " '" + DataGrid.Rows[row].Cells[0].Value.ToString() + "', getdate(), '" + DataGrid.Rows[row].Cells[2].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[3].Value.ToString() + "', ";
                                _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " '" + DataGrid.Rows[row].Cells[4].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[5].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[6].Value.ToString() + "', ";

                                _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " '" + DataGrid.Rows[row].Cells[7].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[8].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[9].Value.ToString() + "', ";
                                _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " '" + DataGrid.Rows[row].Cells[10].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[11].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[12].Value.ToString() + "', ";
                                _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " '" + DataGrid.Rows[row].Cells[13].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[14].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[15].Value.ToString() + "', ";
                                _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " '" + DataGrid.Rows[row].Cells[16].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[17].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[18].Value.ToString() + "', ";
                                _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " '" + DataGrid.Rows[row].Cells[19].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[20].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[21].Value.ToString() + "', ";
                                _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " '" + DataGrid.Rows[row].Cells[22].Value.ToString() + "', null, '" + DataGrid.Rows[row].Cells[24].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[25].Value.ToString() + "'";

                                _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " )  \r\n ";


                            }
                        }

                    }

                    _dbManInsert.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManInsert.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManInsert.ResultsTableName = "Transfer";
                    _dbManInsert.ExecuteInstruction();


                    MessageBox.Show("Org Unit Transfered", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadListBoxStoping();



                }
            }
        }

        private void editActivity_EditValueChanged(object sender, EventArgs e)
        {
            LoadListBoxStoping();
        }

        private void editProdmonth_EditValueChanged(object sender, EventArgs e)
        {
            LoadListBoxStoping();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OnCloseTabRequest(new CloseTabArg(tabCaption));
        }

        private void accordionControlElement1_Click(object sender, EventArgs e)
        {

        }
    }
}
