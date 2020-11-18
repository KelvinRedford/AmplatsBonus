﻿ using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using FastReport;
using DevExpress.XtraEditors;
using Mineware.Systems.GlobalConnect;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionAmplatsGlobal;

namespace Mineware.Systems.ProductionAmplatsBonus
{
    public partial class ucSBBonus : BaseUserControl
    {
        public ucSBBonus()
        {
            InitializeComponent();
            FormRibbonPages.Add(rpReports);
            FormActiveRibbonPage = rpReports;
            FormMainRibbonPage = rpReports;
            RibbonControl = rcReports;
        }

        Report report = new Report();
        private string _reportFolder = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\Reports\";

        private void frmSBBonus_Load(object sender, EventArgs e)
        {
            if (this.Text == "Stoping Shift Boss Bonus")
            {
                rdbDev.Checked = false;
                rdbStoping.Checked = true;
            }
            else
            {
                rdbStoping.Checked = false;
                rdbDev.Checked = true;
            }

            editProdmonth.EditValue = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsDate(ProductionAmplatsGlobalTSysSettings._currentProductionMonth.ToString());
        }

     
        private void Close1Btn_Click(object sender, EventArgs e)
        {
            //Close();
        }

        private void ProdMonth1Txt_TextChanged(object sender, EventArgs e)
        {
            //
            // Load the SB list boxes
            //

            if (this.Text == "Stoping Shift Boss Bonus")
            {

                //Incomplete
                lbIncomplete.Items.Clear();

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " select distinct(sec) sec from (select substring(orgunit,1,5) sec from tbl_BCS_StopingRepNew b \r\n " +
                                      " where  \r\n " +
                                      " substring(orgunit,1,5) not in (select gang from tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activity = 'SM') \r\n " +
                                      " and substring(orgunit,1,5) not in (select distinct(orgunit) orgunit from mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0' and type = '08') \r\n " +
                                      " and b.prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') a order by sec ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Incomplete";
                _dbMan.ExecuteInstruction();

                DataTable dt = _dbMan.ResultsDataTable;

                foreach (DataRow dr in dt.Rows)
                {
                    lbIncomplete.Items.Add(dr["sec"].ToString());
                }


                //Printed
                lbPrinted.Items.Clear();

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = " select gang sec from tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activity = 'SM'  \r\n " +
                                      " and gang not in (select distinct(orgunit) orgunit from mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0'  and type = '08') \r\n " +
                                      " and gang not like '______R%' and gang not like '_______Z' \r\n " +
                                      " order by gang ";
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ResultsTableName = "Printed";
                _dbMan1.ExecuteInstruction();

                DataTable dt1 = _dbMan1.ResultsDataTable;

                foreach (DataRow dr1 in dt1.Rows)
                {
                    lbPrinted.Items.Add(dr1["sec"].ToString());
                }


                //Transferred
                lbTransfer.Items.Clear();

                MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
                _dbMan2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan2.SqlStatement = " select distinct(orgunit) orgunit from mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where substring(orgunit+'    ',6,1) = '' and prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0' \r\n " +
                                       " and orgunit not like '______R%' and orgunit not like '_______Z' \r\n " +
                                       " and shift = 'D' and activitycode = 0 and type = '08' group by orgunit order by orgunit ";
                _dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan2.ResultsTableName = "Incomplete";
                _dbMan2.ExecuteInstruction();

                DataTable dt2 = _dbMan2.ResultsDataTable;

                foreach (DataRow dr2 in dt2.Rows)
                {
                    lbTransfer.Items.Add(dr2["orgunit"].ToString());
                }
            }
            else /////////////////////////////////////////Development///////////////////////////////////////
            {
                //Incomplete
                lbIncomplete.Items.Clear();

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " select distinct(sec) sec from (select substring(orgunit,1,5) sec from tbl_BCS_DevRepNew b \r\n " +
                                      " where  \r\n " +
                                      " substring(orgunit,1,5) not in (select gang from tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activity = 'SM') \r\n " +
                                      " and substring(orgunit,1,5) not in (select distinct(orgunit) orgunit from mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '1' and type = '08') \r\n " +
                                      " and b.prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') a order by sec ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Incomplete";
                _dbMan.ExecuteInstruction();

                DataTable dt = _dbMan.ResultsDataTable;

                foreach (DataRow dr in dt.Rows)
                {
                    lbIncomplete.Items.Add(dr["sec"].ToString());
                }


                //Printed
                lbPrinted.Items.Clear();

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = " select gang sec from tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activity = 'DM'  \r\n " +
                                      " and gang not in (select distinct(orgunit) orgunit from mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '1'  and type = '08') \r\n " +
                                      " and gang not like '______R%' and gang not like '_______Z' \r\n " +
                                      " order by gang ";
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ResultsTableName = "Printed";
                _dbMan1.ExecuteInstruction();

                DataTable dt1 = _dbMan1.ResultsDataTable;

                foreach (DataRow dr1 in dt1.Rows)
                {
                    lbPrinted.Items.Add(dr1["sec"].ToString());
                }


                //Transferred
                lbTransfer.Items.Clear();

                MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
                _dbMan2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan2.SqlStatement = " select distinct(substring(orgunit,1,5)) orgunit from mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where  substring(orgunit+'    ',6,1) = '' and  prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '1' \r\n " +
                                       " and orgunit not like '______R%' and orgunit not like '_______Z' \r\n " +
                                       " and shift = 'D' and activitycode = 1 and type = '08' group by orgunit order by orgunit ";
                _dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan2.ResultsTableName = "Incomplete";
                _dbMan2.ExecuteInstruction();

                DataTable dt2 = _dbMan2.ResultsDataTable;

                foreach (DataRow dr2 in dt2.Rows)
                {
                    lbTransfer.Items.Add(dr2["orgunit"].ToString());
                }
            }

        }

        private void lbTransfer_Click(object sender, EventArgs e)
        {
            //clear other list boxes and set lblOrgUnit
            //////////if (lbIncomplete.Items.Count > 0)
            //////////{
            //////////    lbIncomplete.SetSelected(lbIncomplete.SelectedIndex, false);
            //////////}
            //////////if (lbPrinted.Items.Count > 0)
            //////////{
            //////////    lbPrinted.SetSelected(0, false);
            //////////}

            if (lbIncomplete.Items.Count > 0)
            {
                lbIncomplete.SetSelected(0, false);
            }
            if (lbPrinted.Items.Count > 0)
            {
                lbPrinted.SetSelected(0, false);
            }

            if (lbTransfer.SelectedIndex > -1)
              lblOrgunit.Text = lbTransfer.SelectedItem.ToString();
        }

        private void lbIncomplete_Click(object sender, EventArgs e)
        {
            //clear other list boxes and set lblOrgUnit
            if (lbPrinted.Items.Count > 0)
            {
                lbPrinted.SetSelected(0, false);
            }
            if (lbTransfer.Items.Count > 0)
            {
                lbTransfer.SetSelected(0, false);
            }
            lblOrgunit.Text = lbIncomplete.SelectedItem.ToString();
        }

        private void lbPrinted_Click(object sender, EventArgs e)
        {
            //clear other list boxes and set lblOrgUnit 
            if (lbIncomplete.Items.Count > 0)
            {
                lbIncomplete.SetSelected(0, false);
            }
            if (lbTransfer.Items.Count > 0)
            {
                lbTransfer.SetSelected(0, false);
            }
            lblOrgunit.Text = lbPrinted.SelectedItem.ToString();
        }

          decimal Call = 0;
          decimal Mined = 0;
          decimal PercRate = 0;

          decimal LateralCall = 0;
          decimal LateralMined = 0;
          decimal LateralPercRate = 0;

          decimal RaiseCall = 0;
          decimal RaiseMined = 0;
          decimal RaisePercRate = 0;

          decimal NoDecimalPercRate = 0;
          decimal LateralNoDecimalPercRate = 0;
          decimal RaiseNoDecimalPercRate = 0;
          decimal BaseRate = 0;
          decimal SafetyPayment = 0;
        decimal SafetyAchieved = 0;

        decimal EngPayment = 0;
        decimal EngAchieved = 0;

        decimal TonsPayment = 0;
        decimal TonsAchieved = 0;
        
        decimal SweepsPayment = 0;
        decimal SweepsAchieved = 0;

        decimal LateralPayment = 0;
        decimal LateralAchieved = 0;

        decimal RaisePayment = 0;
        decimal RaiseAchieved = 0;


        double FiftyPercRule = 0;

        string SafeFacused = "";
        string EngFacused = "";
        string TonsFacused = "";
        string SweepsFacused = "";

        string LateralFacused = "";
        string RaiseFacused = "";


        decimal LatFactor1 = 0;
        decimal LatFactor2 = 0;
        decimal LatFactor3 = 0;

        decimal LatFactor4 = 0;
        decimal RaiseFactor4 = 0;



        decimal Factor1 = 0;
        decimal Factor2 = 0;
        decimal Factor3 = 0;
        decimal Factor4 = 0;



        private void showBtn_Click(object sender, EventArgs e)
        {



        }
        DialogResult result;

         string Act = "0";

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void ProdMonth1Txt_TextAlignChanged(object sender, EventArgs e)
        {

        }

        private void pcReport_Click(object sender, EventArgs e)
        {
            
        }

        private void pcReport_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void ActivityGroup_Enter(object sender, EventArgs e)
        {
           
        }

        private void rdbStoping_Click(object sender, EventArgs e)
        {
            
        }

        private void rdbDev_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbDev.Checked == true)
            {
                this.Text = "Development Shift Boss Bonus";
                ProdMonth1Txt_TextChanged(null, null);
            }
        }

        private void rdbStoping_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbStoping.Checked == true)
            {
                this.Text = "Stoping Shift Boss Bonus";
                ProdMonth1Txt_TextChanged(null, null);
            }
        }

        private void editActivity_EditValueChanged(object sender, EventArgs e)
        {
            if(editActivity.EditValue.ToString() == "0")
            {
                rdbStoping.Checked = true;
                rdbDev.Checked = false;
            }
            else
            {
                rdbStoping.Checked = false;
                rdbDev.Checked = true;
            }
        }

        private void btnShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            Factor1 = 0;
            Factor2 = 0;
            Factor3 = 0;
            Factor4 = 0;

            LatFactor1 = 0;
            LatFactor2 = 0;
            LatFactor3 = 0;
            LatFactor4 = 0;



            if (lblOrgunit.Text == "LblOrgUnit")
            {
                MessageBox.Show("Please Select a Shift Boss Section");
                return;
            }

            string SB = lblOrgunit.Text.Substring(0, 5) + "%";//lbTransfer.SelectedItem.ToString().Substring(0,5) + "%";
            string SB1 = lblOrgunit.Text.Substring(0, 5);//lbTransfer.SelectedItem.ToString().Substring(0, 5);

            string Month1 = "";
            string Month2 = "";

            MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
            _dbMan2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            if (this.Text == "Stoping Shift Boss Bonus")
            {
                _dbMan2.SqlStatement = " select * from [mineware].[dbo].[tbl_BCS_SBBonusCriteria]  " +
                                       "where SBSection like '" + SB + "' and prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";
            }
            else
            {
                _dbMan2.SqlStatement = " select prodmonth, sb sbsection, safety, rock, tonshoisted tons, " +
                                        "planlateral lateralcall, measlateral lateral, measlateral lateralmined, planraises raiseCall, measraises raises, measraises raisemined, plantotal, meastotal " +
                                        "from [mineware].[dbo].[tbl_BCS_SBoss_DevResults] where sb like '" + SB + "' and prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";

                //_dbMan2.SqlStatement = " select * from BMCS_SBBonusCriteriaDev  " +
                //                       "where SBSection like '" + SB + "' and prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";
            }
            _dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan2.ResultsTableName = "ShiftBoss";
            _dbMan2.ExecuteInstruction();



            if (_dbMan2.ResultsDataTable.Rows.Count > 0)
            {
                if (this.Text == "Stoping Shift Boss Bonus")
                {
                    SBlbl.Text = SB;//_dbMan2.ResultsDataTable.Rows[0]["SBSection"].ToString();
                    Call = Convert.ToDecimal(_dbMan2.ResultsDataTable.Rows[0]["Call"].ToString());
                    Mined = Convert.ToDecimal(_dbMan2.ResultsDataTable.Rows[0]["Mined"].ToString());
                }
                else
                {
                    SBlbl.Text = SB;//_dbMan2.ResultsDataTable.Rows[0]["SBSection"].ToString();
                                    //Call = Convert.ToDecimal(_dbMan2.ResultsDataTable.Rows[0]["Call"].ToString());
                                    //Mined = Convert.ToDecimal(_dbMan2.ResultsDataTable.Rows[0]["Mined"].ToString());

                    LateralCall = Convert.ToDecimal(_dbMan2.ResultsDataTable.Rows[0]["LateralCall"].ToString());
                    LateralMined = Convert.ToDecimal(_dbMan2.ResultsDataTable.Rows[0]["LateralMined"].ToString());

                    RaiseCall = Convert.ToDecimal(_dbMan2.ResultsDataTable.Rows[0]["RaiseCall"].ToString());
                    RaiseMined = Convert.ToDecimal(_dbMan2.ResultsDataTable.Rows[0]["RaiseMined"].ToString());

                    Call = Convert.ToDecimal(_dbMan2.ResultsDataTable.Rows[0]["plantotal"].ToString());
                    Mined = Convert.ToDecimal(_dbMan2.ResultsDataTable.Rows[0]["meastotal"].ToString());
                }

            }

            if (Call > 0)
            {
                if (this.Text == "Stoping Shift Boss Bonus")
                {

                    MWDataManager.clsDataAccess _dbManLimit = new MWDataManager.clsDataAccess();
                    _dbManLimit.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManLimit.SqlStatement = " select * from tbl_BCS_SBBonusFactor where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";
                    _dbManLimit.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManLimit.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManLimit.ResultsTableName = "Dates";
                    _dbManLimit.ExecuteInstruction();

                    PercRate = Math.Round(Convert.ToDecimal((Mined / Call) * 100), 3);
                    //if (PercRate > 120)
                    //{
                    //    PercRate = Convert.ToDecimal(_dbManLimit.ResultsDataTable.Rows[0]["CallLimit"].ToString());
                    //}
                    NoDecimalPercRate = (Mined / Call);
                }
                else
                {

                    MWDataManager.clsDataAccess _dbManLimit = new MWDataManager.clsDataAccess();
                    _dbManLimit.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManLimit.SqlStatement = " select * from tbl_BCS_SBBonusFactor where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";
                    _dbManLimit.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManLimit.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManLimit.ResultsTableName = "Dates";
                    _dbManLimit.ExecuteInstruction();

                    PercRate = Math.Round(Convert.ToDecimal((Mined / Call) * 100), 3);
                    //if (PercRate > 110)
                    //{
                    //    PercRate = Convert.ToDecimal(_dbManLimit.ResultsDataTable.Rows[0]["DevCallLimit"].ToString());
                    //}

                    // PercRate = Math.Round(Convert.ToDecimal((Mined / Call) * 100), 3);
                    if (RaiseCall + LateralCall > 0)
                        NoDecimalPercRate = ((RaiseMined + LateralMined) / (RaiseCall + LateralCall)); //Mined / Call;

                    //LateralPercRate = Math.Round(Convert.ToDecimal((LateralMined / LateralCall) * 100), 1);
                    if (LateralCall > 0)
                    {


                        LateralPercRate = Math.Round(Convert.ToDecimal((100 / LateralCall) * LateralMined), 3);
                        //if (LateralPercRate > 110)
                        //{
                        //    LateralPercRate = Convert.ToDecimal(_dbManLimit.ResultsDataTable.Rows[0]["DevCallLimit"].ToString());
                        //}
                        LateralNoDecimalPercRate = (LateralMined / LateralCall);
                    }
                    else
                    {
                        LateralPercRate = 0;
                        LateralNoDecimalPercRate = 0;
                    }

                    //RaisePercRate = Math.Round(Convert.ToDecimal((RaiseMined / RaiseCall) * 100), 1);
                    if (RaiseCall > 0)
                    {
                        RaisePercRate = Math.Round(Convert.ToDecimal((100 / RaiseCall) * RaiseMined), 3);
                        //if (RaisePercRate > 110)
                        //{
                        //    RaisePercRate = Convert.ToDecimal(_dbManLimit.ResultsDataTable.Rows[0]["DevCallLimit"].ToString());
                        //}
                        RaiseNoDecimalPercRate = (RaiseMined / RaiseCall);
                    }
                    else
                    {
                        RaisePercRate = 0;
                        RaiseNoDecimalPercRate = 0;
                    }
                }
            }
            else
            {
                if (this.Text == "Stoping Shift Boss Bonus")
                {

                    MessageBox.Show("No Call was planned for this OrgUnit");
                    return;
                }
            }

            Procedures procs = new Procedures();

            Month1 = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue));
            Month2 = (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + 1).ToString();
            procs.ProdMonthVis(Convert.ToInt32(Month2));
            Month2 = Procedures.Prod2;


            /////////////////////Get Dates////////////////////////////////////

            MWDataManager.clsDataAccess _dbManDates = new MWDataManager.clsDataAccess();
            _dbManDates.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManDates.SqlStatement = " select * from mineware.dbo.tbl_BCS_SECCAL " +
                                       " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' " +
                                       " and Sectionid like '" + SB + "' ";
            _dbManDates.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManDates.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManDates.ResultsTableName = "Dates";
            _dbManDates.ExecuteInstruction();

            DataTable date = _dbManDates.ResultsDataTable;

            foreach (DataRow dr in date.Rows)
            {
                BeginDate.Value = Convert.ToDateTime(dr["BeginDate"].ToString());
                EndDate.Value = Convert.ToDateTime(dr["EndDate"].ToString());
            }
            //////////////////////////////////////////////////////////////////



            OrgUnitGrid.Visible = true;
            OrgUnitGrid.Rows.Clear();
            OrgUnitGrid.RowCount = 5;
            OrgUnitGrid.ColumnCount = 10;


            OrgUnitGrid.Columns[0].HeaderText = "Org1";
            OrgUnitGrid.Columns[1].HeaderText = "Org2";
            OrgUnitGrid.Columns[2].HeaderText = "Org3";
            OrgUnitGrid.Columns[3].HeaderText = "Org4";
            OrgUnitGrid.Columns[4].HeaderText = "Org5";
            OrgUnitGrid.Columns[5].HeaderText = "Org6";
            OrgUnitGrid.Columns[6].HeaderText = "Org7";
            OrgUnitGrid.Columns[7].HeaderText = "Org8";
            OrgUnitGrid.Columns[8].HeaderText = "Org9";
            OrgUnitGrid.Columns[9].HeaderText = "Org10";

            OrgUnitGrid.Columns[0].Width = 70;
            OrgUnitGrid.Columns[1].Width = 70;
            OrgUnitGrid.Columns[2].Width = 70;
            OrgUnitGrid.Columns[3].Width = 70;
            OrgUnitGrid.Columns[4].Width = 70;
            OrgUnitGrid.Columns[5].Width = 70;
            OrgUnitGrid.Columns[6].Width = 70;
            OrgUnitGrid.Columns[7].Width = 70;
            OrgUnitGrid.Columns[8].Width = 70;
            OrgUnitGrid.Columns[9].Width = 70;

            for (int i = 0; i <= OrgUnitGrid.Rows.Count - 1; i++)
            {
                OrgUnitGrid.Rows[i].Cells[0].Value = "";
                OrgUnitGrid.Rows[i].Cells[1].Value = "";
                OrgUnitGrid.Rows[i].Cells[2].Value = "";
                OrgUnitGrid.Rows[i].Cells[3].Value = "";
                OrgUnitGrid.Rows[i].Cells[4].Value = "";
                OrgUnitGrid.Rows[i].Cells[5].Value = "";
                OrgUnitGrid.Rows[i].Cells[6].Value = "";
                OrgUnitGrid.Rows[i].Cells[7].Value = "";
                OrgUnitGrid.Rows[i].Cells[8].Value = "";
                OrgUnitGrid.Rows[i].Cells[9].Value = "";
            }


            MWDataManager.clsDataAccess _dbManOrg = new MWDataManager.clsDataAccess();
            _dbManOrg.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            if (this.Text == "Stoping Shift Boss Bonus")
            {
                _dbManOrg.SqlStatement = " select * from tbl_BCS_StopingRepNew sr, mineware.dbo.tbl_bcs_Workplace w " +
                                       " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' " +
                                       " and orgunit like '" + SB + "' and sr.Panel = w.workplaceid ";
            }
            else
            {
                _dbManOrg.SqlStatement = " select * from tbl_BCS_DevRepNew  " +
                                       " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' " +
                                       " and orgunit like '" + SB + "' ";
            }

            _dbManOrg.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManOrg.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManOrg.ResultsTableName = "OrgUnit";
            _dbManOrg.ExecuteInstruction();

            DataTable dt = _dbManOrg.ResultsDataTable;

            int col = 0;

            BaseRate = 0;
            int possibleShifts = 0;
            int DSLTI = 0;
            int NSLTI = 0;

            int Production = 0;
            int z = 0;

            decimal NewBaseRate = 0;


            foreach (DataRow dr in dt.Rows)
            {
                OrgUnitGrid.Rows[0].Cells[col].Value = dr["OrgUnit"].ToString().Substring(4, 4);

                if (this.Text == "Stoping Shift Boss Bonus")
                {
                    if (Convert.ToDouble(dr["WasteSQM"].ToString()) > 0)
                    {
                        OrgUnitGrid.Rows[1].Cells[col].Value = Convert.ToInt16(dr["Production"].ToString()) - Convert.ToInt16(dr["WasteSqm"].ToString());

                        // OrgUnitGrid.Rows[2].Cells[col].Value = Convert.ToInt16(dr["Production"].ToString()) - Convert.ToInt16(dr["WasteSqm"].ToString());

                        Production = Convert.ToInt32(dr["Production"]) - Convert.ToInt32(dr["WasteSQM"]); // Convert.ToInt32(dr["ProductionSW"]) + Convert.ToInt32(OrgUnitGrid.Rows[1].Cells[col].Value);
                        MWDataManager.clsDataAccess _dbManWate = new MWDataManager.clsDataAccess();
                        _dbManWate.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                        if (dr["Reefid"].ToString() == "1")
                        {

                            _dbManWate.SqlStatement = " select max(bipstopingamount) bipstopingamount from dbo.tbl_BCS_BIPStopingMer  ";
                            _dbManWate.SqlStatement = _dbManWate.SqlStatement + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'   ";
                            _dbManWate.SqlStatement = _dbManWate.SqlStatement + " and bipstopingavgemp = '" + dr["AverageEmployees"] + "'  and bipstopingsqm <= '" + Production + "'  ";

                        }
                        else
                        {
                            _dbManWate.SqlStatement = " select max(bipstopingamount) bipstopingamount from dbo.tbl_BCS_BIPStoping  ";
                            _dbManWate.SqlStatement = _dbManWate.SqlStatement + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'   ";
                            _dbManWate.SqlStatement = _dbManWate.SqlStatement + " and bipstopingavgemp = '" + dr["AverageEmployees"].ToString() + "'  and bipstopingsqm <= '" + Production + "'  ";
                        }

                        _dbManWate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbManWate.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dbManWate.ResultsTableName = "Waste";
                        _dbManWate.ExecuteInstruction();

                        if (_dbManWate.ResultsDataTable.Rows[0][0] != DBNull.Value)
                        {
                            if (Production > 0)
                                z = Convert.ToInt32(_dbManWate.ResultsDataTable.Rows[0][0].ToString());
                        }
                        else
                        {
                            z = 0;
                        }


                        MWDataManager.clsDataAccess _dbManSweeps = new MWDataManager.clsDataAccess();
                        _dbManSweeps.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                        if (dr["Reefid"].ToString() == "1")
                        {
                            //MWDataManager.clsDataAccess _dbManSweeps = new MWDataManager.clsDataAccess();
                            //_dbManSweeps.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _dbManSweeps.SqlStatement = " Select max(BIPStopingAmount) BIPStopingAmount ";
                            _dbManSweeps.SqlStatement = _dbManSweeps.SqlStatement + " from mineware.dbo.tbl_BCS_BIPStopingswmer  ";
                            _dbManSweeps.SqlStatement = _dbManSweeps.SqlStatement + " where BIPStopingSQM <= '" + (Convert.ToInt32(dr["ProductionSW"].ToString())) + "'  ";
                            _dbManSweeps.SqlStatement = _dbManSweeps.SqlStatement + " and BIPStopingAvgEmp = '" + dr["AverageEmployees"].ToString() + "' ";
                            _dbManSweeps.SqlStatement = _dbManSweeps.SqlStatement + " and ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  ";
                            //_dbManSweeps.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                            //_dbManSweeps.queryReturnType = MWDataManager.ReturnType.DataTable;
                            //_dbManSweeps.ResultsTableName = "Dates";
                            //_dbManSweeps.ExecuteInstruction();
                        }
                        else
                        {
                            //    MWDataManager.clsDataAccess _dbManSweeps = new MWDataManager.clsDataAccess();
                            //    _dbManSweeps.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                            _dbManSweeps.SqlStatement = " Select max(BIPStopingAmount) BIPStopingAmount ";
                            _dbManSweeps.SqlStatement = _dbManSweeps.SqlStatement + " from tbl_BCS_BIPStopingsw  ";
                            _dbManSweeps.SqlStatement = _dbManSweeps.SqlStatement + " where BIPStopingSQM <= '" + (Convert.ToInt32(dr["ProductionSW"].ToString())) + "'  ";
                            _dbManSweeps.SqlStatement = _dbManSweeps.SqlStatement + " and BIPStopingAvgEmp = '" + dr["AverageEmployees"].ToString() + "' ";
                            _dbManSweeps.SqlStatement = _dbManSweeps.SqlStatement + " and ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  ";
                            //_dbManSweeps.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                            //_dbManSweeps.queryReturnType = MWDataManager.ReturnType.DataTable;
                            //_dbManSweeps.ResultsTableName = "Dates";
                            //_dbManSweeps.ExecuteInstruction();


                        }

                        _dbManSweeps.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbManSweeps.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dbManSweeps.ResultsTableName = "Dates";
                        _dbManSweeps.ExecuteInstruction();

                        if (_dbManSweeps.ResultsDataTable.Rows.Count > 0)
                        {

                            //Production = Production + Convert.ToInt32(_dbManSweeps.ResultsDataTable.Rows[0][0].ToString());
                            if (_dbManSweeps.ResultsDataTable.Rows[0][0] != DBNull.Value)
                            {
                                if (dr["Reefid"].ToString() == "1")
                                {
                                    if (Convert.ToInt32(dr["ProductionSW"].ToString()) > 18)
                                        z = z + Convert.ToInt32(_dbManSweeps.ResultsDataTable.Rows[0][0].ToString());
                                }
                                else
                                {
                                    if (Convert.ToInt32(dr["ProductionSW"].ToString()) > 20)
                                        z = z + Convert.ToInt32(_dbManSweeps.ResultsDataTable.Rows[0][0].ToString());

                                }
                            }
                        }

                        OrgUnitGrid.Rows[2].Cells[col].Value = z; //_dbManWate.ResultsDataTable.Rows[0][0].ToString();

                        BaseRate = BaseRate + z;


                    }
                    else
                    {
                        OrgUnitGrid.Rows[1].Cells[col].Value = Convert.ToInt16(dr["Production"].ToString());
                        OrgUnitGrid.Rows[2].Cells[col].Value = dr["BIP"].ToString();

                        BaseRate = BaseRate + Convert.ToDecimal(dr["BIP"].ToString());
                    }
                }
                else
                {
                    OrgUnitGrid.Rows[1].Cells[col].Value = Convert.ToDecimal(dr["Production"].ToString());
                    OrgUnitGrid.Rows[2].Cells[col].Value = dr["BIP"].ToString();
                    BaseRate = BaseRate + Convert.ToDecimal(dr["BIP"].ToString());
                }


                possibleShifts = Convert.ToInt16(dr["PossibleShifts"].ToString());

                if (Convert.ToInt16(dr["DS_LTI"].ToString()) > 0)
                {

                    DSLTI = DSLTI + Convert.ToInt16(dr["DS_LTI"].ToString());
                }

                if (Convert.ToInt16(dr["NS_LTI"].ToString()) > 0)
                {
                    NSLTI = NSLTI + Convert.ToInt16(dr["NS_LTI"].ToString());
                }

                col = col + 1;
            }


            //Get Z-Crew LTI's
            MWDataManager.clsDataAccess _dbManZLTI = new MWDataManager.clsDataAccess();
            _dbManZLTI.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManZLTI.SqlStatement = " Select * from tbl_BCS_ZRepNew \r\n " +
                                      " Where GangType = 'Z' \r\n " +
                                      " and ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n " +
                                      " and AverageEmployees is not null \r\n " +
                                      " and OrgUnit like '" + SB + "' \r\n " +
                                       " ";
            _dbManZLTI.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManZLTI.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManZLTI.ResultsTableName = "Z_LTI";
            _dbManZLTI.ExecuteInstruction();

            DataTable dtZLTI = _dbManZLTI.ResultsDataTable;

            foreach (DataRow dr in dtZLTI.Rows)
            {
                DSLTI = DSLTI + Convert.ToInt16(dr["DS_LTI"].ToString());
                NSLTI = NSLTI + Convert.ToInt16(dr["NS_LTI"].ToString());
            }


            decimal AdjBaseRate = 0;

            MWDataManager.clsDataAccess _dbManRate = new MWDataManager.clsDataAccess();
            _dbManRate.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            _dbManRate.SqlStatement = " select * from tbl_BCS_SBBonusFactor where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";


            _dbManRate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManRate.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManRate.ResultsTableName = "OrgUnit";
            _dbManRate.ExecuteInstruction();

            if (_dbManRate.ResultsDataTable.Rows.Count < 1)
            {
                MessageBox.Show("Please save factors in system settings", "No Factors saved", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            decimal CallLimit = 0;


            //MessageBox.Show(AdjBaseRate.ToString());

            decimal DayShiftFacor = 0;
            decimal NightShiftFacor = 0;
            decimal AwopRate1 = 0;
            decimal AwopRate2 = 0;
            decimal AwopRate3 = 0;

            //decimal Factor4 = 0;

            if (this.Text == "Stoping Shift Boss Bonus")
            {


                CallLimit = Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["CallLimit"].ToString()) / 100;

                if (NoDecimalPercRate < CallLimit)
                {

                    //AdjBaseRate = Math.Round(BaseRate * NoDecimalPercRate, 2);
                    AdjBaseRate = BaseRate * NoDecimalPercRate;
                }
                else
                {
                    //AdjBaseRate = Math.Round(BaseRate * CallLimit, 2);
                    if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) >= 2020000009)
                        AdjBaseRate = BaseRate * NoDecimalPercRate;
                    else
                        AdjBaseRate = BaseRate * CallLimit;
                }

                SafetyAchieved = Convert.ToDecimal(_dbMan2.ResultsDataTable.Rows[0]["Safety"].ToString());
                EngAchieved = Convert.ToDecimal(_dbMan2.ResultsDataTable.Rows[0]["Rock"].ToString());
                TonsAchieved = Convert.ToDecimal(_dbMan2.ResultsDataTable.Rows[0]["Tons"].ToString());
                SweepsAchieved = Convert.ToDecimal(_dbMan2.ResultsDataTable.Rows[0]["Sweeps"].ToString());

                DayShiftFacor = Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["DSFactor"].ToString());
                NightShiftFacor = Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["NSFactor"].ToString());



                if (SafetyAchieved < 75)
                {
                    SafetyPayment = Convert.ToDecimal(AdjBaseRate * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["SafetyFactor1"].ToString()));
                    SafeFacused = _dbManRate.ResultsDataTable.Rows[0]["SafetyFactor1"].ToString();
                }
                else if (SafetyAchieved >= 75 && SafetyAchieved <= Convert.ToDecimal(84.99))
                {
                    SafetyPayment = Convert.ToDecimal(AdjBaseRate * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["SafetyFactor2"].ToString()));
                    SafeFacused = _dbManRate.ResultsDataTable.Rows[0]["SafetyFactor2"].ToString();
                }
                else if (SafetyAchieved >= 85 && SafetyAchieved <= Convert.ToDecimal(89.99))
                {
                    SafetyPayment = Convert.ToDecimal(AdjBaseRate * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["SafetyFactor3"].ToString()));
                    SafeFacused = _dbManRate.ResultsDataTable.Rows[0]["SafetyFactor3"].ToString();
                }
                else if (SafetyAchieved >= 90)
                {
                    SafetyPayment = Convert.ToDecimal(AdjBaseRate * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["SafetyFactor4"].ToString()));
                    SafeFacused = _dbManRate.ResultsDataTable.Rows[0]["SafetyFactor4"].ToString();
                }

                //////////////////////Eng////////////////////////////////////////////

                if (EngAchieved < 80)
                {
                    EngPayment = Convert.ToDecimal(SafetyPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["RockFactor1"].ToString()));
                    EngFacused = _dbManRate.ResultsDataTable.Rows[0]["RockFactor1"].ToString();
                }
                else if (EngAchieved >= 80 && EngAchieved <= Convert.ToDecimal(89.9))
                {
                    EngPayment = Convert.ToDecimal(SafetyPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["RockFactor2"].ToString()));
                    EngFacused = _dbManRate.ResultsDataTable.Rows[0]["RockFactor2"].ToString();
                }
                else if (EngAchieved >= 90 && EngAchieved <= Convert.ToDecimal(94.9))
                {
                    EngPayment = Convert.ToDecimal(SafetyPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["RockFactor3"].ToString()));
                    EngFacused = _dbManRate.ResultsDataTable.Rows[0]["RockFactor3"].ToString();
                }
                else if (EngAchieved >= 95)
                {
                    EngPayment = Convert.ToDecimal(SafetyPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["RockFactor4"].ToString()));
                    EngFacused = _dbManRate.ResultsDataTable.Rows[0]["RockFactor4"].ToString();
                }

                ////////////////////////////////////////////////////////////////////////

                /////////////////////////////Tons//////////////////////
                if (TonsAchieved < 80)
                {
                    TonsPayment = Convert.ToDecimal(EngPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["TonsFactor1"].ToString()));
                    TonsFacused = _dbManRate.ResultsDataTable.Rows[0]["TonsFactor1"].ToString();


                    Factor1 = Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["TonsFactor1"].ToString());

                    if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) >= 202009)
                    {

                        if (PercRate > Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["TonsFactor1"].ToString()))
                            Factor1 = TonsAchieved / 100;
                        else
                            Factor1 = Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["TonsFactor1"].ToString());

                        TonsPayment = Convert.ToDecimal(EngPayment * Factor1);
                        TonsFacused = PercRate.ToString();

                    }
                }
                else if (TonsAchieved >= 80 && TonsAchieved <= Convert.ToDecimal(89.9))
                {
                    TonsPayment = Convert.ToDecimal(EngPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["TonsFactor2"].ToString()));
                    TonsFacused = _dbManRate.ResultsDataTable.Rows[0]["TonsFactor2"].ToString();

                    Factor2 = Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["TonsFactor2"].ToString());

                    if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) >= 202009)
                    {

                        if (PercRate > Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["TonsFactor2"].ToString()))
                            Factor2 = TonsAchieved / 100;
                        else
                            Factor2 = Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["TonsFactor2"].ToString());

                        TonsPayment = Convert.ToDecimal(EngPayment * Factor2);
                        TonsFacused = PercRate.ToString();

                    }
                }
                else if (TonsAchieved >= 90 && TonsAchieved < Convert.ToDecimal(99))
                {
                    TonsPayment = Convert.ToDecimal(EngPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["TonsFactor3"].ToString()));
                    TonsFacused = _dbManRate.ResultsDataTable.Rows[0]["TonsFactor3"].ToString();

                    Factor3 = Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["TonsFactor3"].ToString());

                    if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) >= 202009)
                    {

                        if (PercRate > Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["TonsFactor3"].ToString()))
                            Factor3 = TonsAchieved / 100;
                        else
                            Factor3 = Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["TonsFactor3"].ToString());

                        TonsPayment = Convert.ToDecimal(EngPayment * Factor3);
                        TonsFacused = PercRate.ToString();

                    }
                }
                else if (TonsAchieved >= 99)
                {
                    TonsPayment = Convert.ToDecimal(EngPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["TonsFactor4"].ToString()));
                    TonsFacused = _dbManRate.ResultsDataTable.Rows[0]["TonsFactor4"].ToString();

                    Factor4 = Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["TonsFactor4"].ToString());

                    if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) >= 202009)
                    {

                        if (PercRate > Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["TonsFactor4"].ToString()))
                            Factor4 = TonsAchieved / 100;
                        else
                            Factor4 = Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["TonsFactor4"].ToString());

                        TonsPayment = Convert.ToDecimal(EngPayment * Factor4);
                        TonsFacused = PercRate.ToString();

                    }

                    // TonsPayment = Convert.ToDecimal(EngPayment * Convert.ToDecimal(PercRate/100));
                    //  TonsFacused = PercRate.ToString();

                    //  if (PercRate > Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["TonsFactor4"].ToString()))
                    //      Factor4 = PercRate / 100;
                    //  else
                    //     Factor4 = Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["TonsFactor4"].ToString());

                    // TonsPayment = Convert.ToDecimal(EngPayment * Factor4);
                    // TonsFacused = PercRate.ToString();

                }
                ///////////////////////////////////////////////////////

                //////////////////////Sweeps////////////////////////////////////////
                if (SweepsAchieved < 60)
                {
                    SweepsPayment = Convert.ToDecimal(TonsPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["SweepsFactor1"].ToString()));
                    SweepsFacused = _dbManRate.ResultsDataTable.Rows[0]["SweepsFactor1"].ToString();
                }
                else if (SweepsAchieved >= 60 && SweepsAchieved <= Convert.ToDecimal(79.99))
                {
                    SweepsPayment = Convert.ToDecimal(TonsPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["SweepsFactor2"].ToString()));
                    SweepsFacused = _dbManRate.ResultsDataTable.Rows[0]["SweepsFactor2"].ToString();
                }
                else if (SweepsAchieved >= 80 && SweepsAchieved <= Convert.ToDecimal(89.99))
                {
                    SweepsPayment = Convert.ToDecimal(TonsPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["SweepsFactor3"].ToString()));
                    SweepsFacused = _dbManRate.ResultsDataTable.Rows[0]["SweepsFactor3"].ToString();
                }
                else if (SweepsAchieved >= 90)
                {
                    SweepsPayment = Convert.ToDecimal(TonsPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["SweepsFactor4"].ToString()));
                    SweepsFacused = _dbManRate.ResultsDataTable.Rows[0]["SweepsFactor4"].ToString();
                }
                ///////////////////////////////////////////////////////////////////
            }
            else
            {

                CallLimit = Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["DevCallLimit"].ToString()) / 100;

                if (NoDecimalPercRate < CallLimit)
                {

                    //AdjBaseRate = Math.Round(BaseRate * NoDecimalPercRate, 2);
                    AdjBaseRate = BaseRate * NoDecimalPercRate;
                }
                else
                {
                    if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) >= 20200009)
                        AdjBaseRate = BaseRate * NoDecimalPercRate;
                    else
                        AdjBaseRate = BaseRate * CallLimit;
                }

                SafetyAchieved = Convert.ToDecimal(_dbMan2.ResultsDataTable.Rows[0]["Safety"].ToString());
                EngAchieved = Convert.ToDecimal(_dbMan2.ResultsDataTable.Rows[0]["Rock"].ToString());
                TonsAchieved = Convert.ToDecimal(_dbMan2.ResultsDataTable.Rows[0]["Tons"].ToString());
                LateralAchieved = Convert.ToDecimal(_dbMan2.ResultsDataTable.Rows[0]["Lateral"].ToString());
                RaiseAchieved = Convert.ToDecimal(_dbMan2.ResultsDataTable.Rows[0]["Raises"].ToString());

                DayShiftFacor = Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["DevDSFactor"].ToString());
                NightShiftFacor = Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["DevNSFactor"].ToString());



                if (SafetyAchieved < 75)
                {
                    SafetyPayment = Convert.ToDecimal(AdjBaseRate * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["DevSafetyFactor1"].ToString()));
                    SafeFacused = _dbManRate.ResultsDataTable.Rows[0]["DevSafetyFactor1"].ToString();
                }
                else if (SafetyAchieved >= 75 && SafetyAchieved <= Convert.ToDecimal(84.99))
                {
                    SafetyPayment = Convert.ToDecimal(AdjBaseRate * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["DevSafetyFactor2"].ToString()));
                    SafeFacused = _dbManRate.ResultsDataTable.Rows[0]["DevSafetyFactor2"].ToString();
                }
                else if (SafetyAchieved >= 85 && SafetyAchieved <= Convert.ToDecimal(89.99))
                {
                    SafetyPayment = Convert.ToDecimal(AdjBaseRate * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["DevSafetyFactor3"].ToString()));
                    SafeFacused = _dbManRate.ResultsDataTable.Rows[0]["DevSafetyFactor3"].ToString();
                }
                else if (SafetyAchieved >= 90)
                {
                    SafetyPayment = Convert.ToDecimal(AdjBaseRate * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["DevSafetyFactor4"].ToString()));
                    SafeFacused = _dbManRate.ResultsDataTable.Rows[0]["DevSafetyFactor4"].ToString();
                }

                //////////////////////Eng////////////////////////////////////////////

                if (EngAchieved < 80)
                {
                    EngPayment = Convert.ToDecimal(SafetyPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["DevRockFactor1"].ToString()));
                    EngFacused = _dbManRate.ResultsDataTable.Rows[0]["DevRockFactor1"].ToString();
                }
                else if (EngAchieved >= 80 && EngAchieved <= Convert.ToDecimal(89.9))
                {
                    EngPayment = Convert.ToDecimal(SafetyPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["DevRockFactor2"].ToString()));
                    EngFacused = _dbManRate.ResultsDataTable.Rows[0]["DevRockFactor2"].ToString();
                }
                else if (EngAchieved >= 90 && EngAchieved <= Convert.ToDecimal(94.9))
                {
                    EngPayment = Convert.ToDecimal(SafetyPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["DevRockFactor3"].ToString()));
                    EngFacused = _dbManRate.ResultsDataTable.Rows[0]["DevRockFactor3"].ToString();
                }
                else if (EngAchieved >= 95)
                {
                    EngPayment = Convert.ToDecimal(SafetyPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["DevRockFactor4"].ToString()));
                    EngFacused = _dbManRate.ResultsDataTable.Rows[0]["DevRockFactor4"].ToString();
                }

                ////////////////////////////////////////////////////////////////////////

                /////////////////////////////Tons//////////////////////
                if (TonsAchieved < 80)
                {
                    TonsPayment = Convert.ToDecimal(EngPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["DevTonsFactor1"].ToString()));
                    TonsFacused = _dbManRate.ResultsDataTable.Rows[0]["DevTonsFactor1"].ToString();
                }
                else if (TonsAchieved >= 80 && TonsAchieved <= Convert.ToDecimal(89.9))
                {
                    TonsPayment = Convert.ToDecimal(EngPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["DevTonsFactor2"].ToString()));
                    TonsFacused = _dbManRate.ResultsDataTable.Rows[0]["DevTonsFactor2"].ToString();
                }
                else if (TonsAchieved >= 90 && TonsAchieved < Convert.ToDecimal(99))
                {
                    TonsPayment = Convert.ToDecimal(EngPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["DevTonsFactor3"].ToString()));
                    TonsFacused = _dbManRate.ResultsDataTable.Rows[0]["DevTonsFactor3"].ToString();
                }
                else if (TonsAchieved >= 99)
                {
                    TonsPayment = Convert.ToDecimal(EngPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["DevTonsFactor4"].ToString()));
                    TonsFacused = _dbManRate.ResultsDataTable.Rows[0]["DevTonsFactor4"].ToString();

                    Factor4 = Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["DevTonsFactor4"].ToString());


                }
                ///////////////////////////////////////////////////////

                //////////////////////Lateral////////////////////////////////////////




                if (LateralCall > 0)
                {

                    if ((LateralAchieved / LateralCall) * 100 < 80)
                    {
                        LateralPayment = Convert.ToDecimal(TonsPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["LateralFactor1"].ToString()));
                        LateralFacused = _dbManRate.ResultsDataTable.Rows[0]["LateralFactor1"].ToString();


                        if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) >= 202009)
                        {

                            if (LateralPercRate > Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["LateralFactor1"].ToString()))
                                LatFactor1 = LateralPercRate / 100;
                            else
                                LatFactor1 = Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["LateralFactor1"].ToString());

                            LateralPayment = Convert.ToDecimal(TonsPayment * LatFactor2);
                            LateralFacused = LatFactor1.ToString();

                        }
                    }
                    else if ((LateralAchieved / LateralCall) * 100 >= 80 && (LateralAchieved / LateralCall) * 100 <= Convert.ToDecimal(89.99))
                    {
                        LateralPayment = Convert.ToDecimal(TonsPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["LateralFactor2"].ToString()));
                        LateralFacused = _dbManRate.ResultsDataTable.Rows[0]["LateralFactor2"].ToString();


                        if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) >= 202009)
                        {

                            if (LateralPercRate > Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["LateralFactor2"].ToString()))
                                LatFactor2 = LateralPercRate / 100;
                            else
                                LatFactor2 = Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["LateralFactor2"].ToString());

                            LateralPayment = Convert.ToDecimal(TonsPayment * LatFactor2);
                            LateralFacused = LatFactor2.ToString();

                        }
                    }
                    else if ((LateralAchieved / LateralCall) * 100 >= 90 && (LateralAchieved / LateralCall) * 100 <= Convert.ToDecimal(99.99))
                    {
                        LateralPayment = Convert.ToDecimal(TonsPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["LateralFactor3"].ToString()));
                        LateralFacused = _dbManRate.ResultsDataTable.Rows[0]["LateralFactor3"].ToString();

                        if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) >= 202009)
                        {

                            if (LateralPercRate > Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["LateralFactor3"].ToString()))
                                LatFactor3 = LateralPercRate / 100;
                            else
                                LatFactor3 = Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["LateralFactor3"].ToString());

                            LateralPayment = Convert.ToDecimal(TonsPayment * LatFactor3);
                            LateralFacused = LatFactor3.ToString();

                        }
                    }
                    else if ((LateralAchieved / LateralCall) * 100 >= 100)
                    {
                        LateralPayment = Convert.ToDecimal(TonsPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["LateralFactor4"].ToString()));
                        LateralFacused = _dbManRate.ResultsDataTable.Rows[0]["LateralFactor4"].ToString();

                        LatFactor4 = Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["LateralFactor4"].ToString());

                        if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) >= 202009)
                        {

                            if (LateralPercRate > Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["LateralFactor4"].ToString()))
                                LatFactor4 = LateralPercRate / 100;
                            else
                                LatFactor4 = Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["LateralFactor4"].ToString());

                            LateralPayment = Convert.ToDecimal(TonsPayment * LatFactor4);
                            LateralFacused = LatFactor4.ToString();

                        }
                    }
                }
                else
                {
                    if (RaiseCall > 0)
                    {
                        LateralPayment = Convert.ToDecimal(TonsPayment * 1);
                        LateralFacused = "1";
                    }
                    else
                    {
                        LateralPayment = Convert.ToDecimal(TonsPayment * 0);
                        LateralFacused = "0";
                    }
                }
                ///////////////////////////////////////////////////////////////////

                //////////////////////Lateral////////////////////////////////////////
                if (RaiseCall > 0)
                {

                    if ((RaiseAchieved / RaiseCall) * 100 < 80)
                    {
                        RaisePayment = Convert.ToDecimal(LateralPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["RaiseFactor1"].ToString()));
                        RaiseFacused = _dbManRate.ResultsDataTable.Rows[0]["RaiseFactor1"].ToString();
                    }
                    else if ((RaiseAchieved / RaiseCall) * 100 >= 80 && (RaiseAchieved / RaiseCall) * 100 <= Convert.ToDecimal(89.99))
                    {
                        RaisePayment = Convert.ToDecimal(LateralPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["RaiseFactor2"].ToString()));
                        RaiseFacused = _dbManRate.ResultsDataTable.Rows[0]["RaiseFactor2"].ToString();
                    }
                    else if ((RaiseAchieved / RaiseCall) * 100 >= 90 && (RaiseAchieved / RaiseCall) * 100 <= Convert.ToDecimal(99.99))
                    {
                        RaisePayment = Convert.ToDecimal(LateralPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["RaiseFactor3"].ToString()));
                        RaiseFacused = _dbManRate.ResultsDataTable.Rows[0]["RaiseFactor3"].ToString();
                    }
                    else if ((RaiseAchieved / RaiseCall) * 100 >= 100)
                    {
                        RaisePayment = Convert.ToDecimal(LateralPayment * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["RaiseFactor4"].ToString()));
                        RaiseFacused = _dbManRate.ResultsDataTable.Rows[0]["RaiseFactor4"].ToString();


                        RaiseFactor4 = Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["RaiseFactor4"].ToString());

                        //  if (ProdMonthTxt.Value >= 202008)
                        //  {

                        //       if (RaisePercRate > Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["LateralFactor4"].ToString()))
                        //          RaiseFactor4 = RaisePercRate / 100;
                        //      else
                        //        RaiseFactor4 = Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["LateralFactor4"].ToString());
                        //
                        //  LateralPayment = Convert.ToDecimal(LateralPayment * RaiseFactor4);
                        //  LateralFacused = RaiseFactor4.ToString();

                        //  }
                    }
                }
                else
                {
                    if (LateralCall > 0)
                    {

                        RaisePayment = Convert.ToDecimal(LateralPayment * 1);
                        RaiseFacused = "1";
                    }
                    else
                    {
                        RaisePayment = Convert.ToDecimal(LateralPayment * 0);
                        RaiseFacused = "0";
                    }
                }
                ///////////////////////////////////////////////////////////////////
            }


            decimal SafetyVar = 0;
            decimal EngVar = 0;
            decimal TonsVar = 0;
            decimal SweepsVar = 0;
            decimal LateralVar = 0;
            decimal RaiseVar = 0;


            if (this.Text == "Stoping Shift Boss Bonus")
            {

                SafetyVar = Convert.ToDecimal(SafetyPayment - AdjBaseRate);
                EngVar = Convert.ToDecimal(EngPayment - SafetyPayment);
                TonsVar = Convert.ToDecimal(TonsPayment - EngPayment);
                SweepsVar = Convert.ToDecimal(SweepsPayment - TonsPayment);
            }
            else
            {
                SafetyVar = Convert.ToDecimal(SafetyPayment - AdjBaseRate);
                EngVar = Convert.ToDecimal(EngPayment - SafetyPayment);
                TonsVar = Convert.ToDecimal(TonsPayment - EngPayment);
                LateralVar = Convert.ToDecimal(LateralPayment - TonsPayment);
                RaiseVar = Convert.ToDecimal(RaisePayment - LateralPayment);
            }

            if (this.Text == "Stoping Shift Boss Bonus")
            {
                // MessageBox.Show(BaseRate.ToString());
                FiftyPercRule = Math.Round(Convert.ToDouble(BaseRate / 2), 2);

                if (SweepsPayment < (BaseRate / 2))
                {
                    FiftyPercRule = Math.Round(Convert.ToDouble(BaseRate) / 2, 2);
                }
                else
                {
                    FiftyPercRule = Convert.ToDouble(SweepsPayment);
                }
            }
            else
            {
                FiftyPercRule = Math.Round(Convert.ToDouble(BaseRate / 2), 2);

                if (RaisePayment < (BaseRate / 2))
                {
                    FiftyPercRule = Math.Round(Convert.ToDouble(BaseRate) / 2, 2);
                }
                else
                {
                    FiftyPercRule = Convert.ToDouble(RaisePayment);
                }
            }

            /////////////////////Get Industry No////////////////////////////////////

            MWDataManager.clsDataAccess _dbManIndNo = new MWDataManager.clsDataAccess();
            _dbManIndNo.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManIndNo.SqlStatement = " select distinct(IndustryNumber) IndNumber from tbl_BCS_Gangs where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  and orgunit like '" + SB1 + "' ";
            _dbManIndNo.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManIndNo.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManIndNo.ResultsTableName = "IndNo";
            _dbManIndNo.ExecuteInstruction();

            DataTable IndNo = _dbManIndNo.ResultsDataTable;

            string ind = "";

            foreach (DataRow dr in IndNo.Rows)
            {
                ind = ind + @"'" + dr["IndNumber"].ToString() + @"',";



            }
            ind = ind + @"''";
            //////////////////////////////////////////////////////////////////

            // MessageBox.Show(ind);
            // return;

            ///////////////////////////////////Get Ppl//////////////////////////

            MWDataManager.clsDataAccess _dbManPpl = new MWDataManager.clsDataAccess();
            _dbManPpl.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " select *, isnull(abzz,0) Ab, isnull(S2,0) S1 from (select * from (select\r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " IndustryNumber, Shift, SUM(Work) Work, sum(Ab) Ab1, sum(S1a) S1a, isnull(MyName, 'Not in System. NA')  MyName  \r\n";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " from (\r\n";

            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + "  select IndustryNumber, Date, Shift, Codes,\r\n";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " case when Workaaaaaa is null then 0 else Workaaaaaa end as work,\r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + "  case when Ab1 is null then 0 else Ab1 end as Ab, isnull(S1,0) S1a , \r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " case when IndNo is null then IndustryNumber else IndNo end as IndNo, \r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + "  case when TheDate is null then Date else TheDate end as TheDate,\r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " [Leave Flag] [Leave Flag], IndNo1, MyName\r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + "  from (  \r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " select IndustryNumber, Date, Shift,case when codes in('N','NA','TE','PR','NW','FB') then 1 else 0 end as Workaaaaaa,Codes from tbl_BCS_Gangs\r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + "  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + "  and orgunit like '" + SB1 + "' ) bcs\r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " left outer join \r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + "  ( \r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " select [IndustryNumber] IndNo, TheDate, [LeaveFlag] [Leave Flag],\r\n  ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " case when [LeaveFlag] in('N','NA','TE','PR','NW','FB') then 1 else 0 end as Work1,\r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " case when [LeaveFlag] in('A','AW') then 1 else 0 end as Ab1, \r\n ";

            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " case when [LeaveFlag] in('S','SU') then 1 else 0 end as S1\r\n ";

            //_dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " (select count(TheDate) S1 from dbo.tbl_Import_BMCS_Clocking_Total \r\n ";
            //_dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " from dbo.tbl_Import_BMCS_Clocking_Total\r\n ";
            //_dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " where TheDate >= '" + String.Format("{0:yyyy-MM-dd}", BeginDate.Value) + "'\r\n ";
            //_dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " and TheDate <= '" + String.Format("{0:yyyy-MM-dd}", EndDate.Value) + "'\r\n ";
            //_dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " and [IndustryNumber] in (" + ind + ") and [expectedatwork] = 'Y'\r\n ";

            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " from dbo.tbl_Import_BMCS_Clocking_Total\r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " where TheDate >= '" + String.Format("{0:yyyy-MM-dd}", BeginDate.Value) + "'\r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " and TheDate <= '" + String.Format("{0:yyyy-MM-dd}", EndDate.Value) + "'\r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " and [IndustryNumber] in (" + ind + ") and [expectedatwork] = 'Y'\r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " )sym on bcs.IndustryNumber = sym.IndNo and bcs.Date = sym.TheDate\r\n ";

            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " left outer join \r\n  ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " (  select [Recource Referance] IndNo1, Surname+'.'+Initials MyName from ( \r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " select * from [dbo].[tbl_bcs_ResourceInformation]\r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " where [Recource Referance] in (" + ind + ")\r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " and [End Date] >= '" + String.Format("{0:yyyy-MM-dd}", EndDate.Value) + "' ) a\r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " left outer join\r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " (select * from [dbo].[tbl_bcs_HRA_Personal])b on a.[Resource Tag] = b.[Resource Tag] ) sym2\r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " on bcs.IndustryNumber = sym2.IndNo1\r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " ) Main\r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + "   group by IndustryNumber, Shift, MyName\r\n ";


            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " )Main2 \r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " left outer join \r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + "  ( \r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " select IndNo, SUM(Ab1) Abzz, SUM(S2) S2 from ( select [IndustryNumber] IndNo, \r\n  ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " case when [LeaveFlag] in('N','NA','TE','NW','FB') then 1 else 0 end as Work1,\r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " case when [LeaveFlag] in('A','AW') then 1 else 0 end as Ab1  ,case when [LeaveFlag] in('S','SU') then 1 else 0 end as S2 \r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " from dbo.tbl_Import_BMCS_Clocking_Total\r\n ";
            if ((Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) == 201409) && ((SB1.Substring(0, 4) == "0114") || (SB1.Substring(0, 4) == "0121") || (SB1.Substring(0, 4) == "0122") || (SB1.Substring(0, 4) == "0113")))
                _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " where TheDate >= '2014-09-04'\r\n ";
            else
                _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " where TheDate >= '" + String.Format("{0:yyyy-MM-dd}", BeginDate.Value) + "'\r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " and TheDate <= '" + String.Format("{0:yyyy-MM-dd}", EndDate.Value) + "'\r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " and [IndustryNumber] in (" + ind + ")  )a group by IndNo)AWOP on Main2.IndustryNumber = awop.IndNo \r\n ";


            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + "  ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " ) a left outer join \r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + "  (select * from mineware.[dbo].[tbl_BCS_AbsenteeismFactors] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and shiftno <> 0) zz on a.s2 = zz.shiftno \r\n ";
            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " order by Shift, IndustryNumber, Work\r\n\r\n ";
            _dbManPpl.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManPpl.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManPpl.ResultsTableName = "Ppl";
            _dbManPpl.ExecuteInstruction();

            DataTable dtPpl = _dbManPpl.ResultsDataTable;

            //////////////////////////////////////////////////////////////////////


            ////////////////////////////////////Do Day Shift and Night Shift////////////////////////////////////////////////
            DSGrid.Visible = true;
            DSGrid.Rows.Clear();
            DSGrid.RowCount = 1000;
            DSGrid.ColumnCount = 20;


            DSGrid.Columns[0].HeaderText = "Emp";
            DSGrid.Columns[1].HeaderText = "Ind";
            DSGrid.Columns[2].HeaderText = "Factor";
            DSGrid.Columns[3].HeaderText = "Posible Shifts";
            DSGrid.Columns[4].HeaderText = "Shifts Worked";
            DSGrid.Columns[5].HeaderText = "Pro-Rata";
            DSGrid.Columns[6].HeaderText = "Awop";
            DSGrid.Columns[7].HeaderText = "Rand";
            DSGrid.Columns[8].HeaderText = "Pro-Rate Awop";
            DSGrid.Columns[9].HeaderText = "LTI";
            DSGrid.Columns[10].HeaderText = "Rand";
            DSGrid.Columns[11].HeaderText = "Pro-Rate Pay";

            DSGrid.Columns[12].HeaderText = "SickDays";
            DSGrid.Columns[13].HeaderText = "AWOPDays";
            DSGrid.Columns[14].HeaderText = "SickPec";
            DSGrid.Columns[15].HeaderText = "AwopPerc";
            DSGrid.Columns[16].HeaderText = "TotPerc";
            DSGrid.Columns[17].HeaderText = "Pot";

            DSGrid.Columns[18].HeaderText = "FinPay";




            DSGrid.Columns[0].Width = 70;
            DSGrid.Columns[1].Width = 70;
            DSGrid.Columns[2].Width = 70;
            DSGrid.Columns[3].Width = 70;
            DSGrid.Columns[4].Width = 70;
            DSGrid.Columns[5].Width = 70;
            DSGrid.Columns[6].Width = 70;
            DSGrid.Columns[7].Width = 70;
            DSGrid.Columns[8].Width = 70;
            DSGrid.Columns[9].Width = 70;
            DSGrid.Columns[10].Width = 70;
            DSGrid.Columns[11].Width = 70;


            for (int i = 0; i <= DSGrid.Rows.Count - 1; i++)
            {
                DSGrid.Rows[i].Cells[0].Value = "";
                DSGrid.Rows[i].Cells[1].Value = "";
                DSGrid.Rows[i].Cells[2].Value = "";
                DSGrid.Rows[i].Cells[3].Value = "";
                DSGrid.Rows[i].Cells[4].Value = "";
                DSGrid.Rows[i].Cells[5].Value = "";
                DSGrid.Rows[i].Cells[6].Value = "";
                DSGrid.Rows[i].Cells[7].Value = "";
                DSGrid.Rows[i].Cells[8].Value = "";
                DSGrid.Rows[i].Cells[9].Value = "";
                DSGrid.Rows[i].Cells[10].Value = "";
                DSGrid.Rows[i].Cells[11].Value = "";
            }



            ////////////////////////Night Shift////////////////////////////////////////////////
            NSGrid.Visible = true;
            NSGrid.Rows.Clear();
            NSGrid.RowCount = 1000;
            NSGrid.ColumnCount = 20;


            NSGrid.Columns[0].HeaderText = "Emp";
            NSGrid.Columns[1].HeaderText = "Ind";
            NSGrid.Columns[2].HeaderText = "Factor";
            NSGrid.Columns[3].HeaderText = "Posible Shifts";
            NSGrid.Columns[4].HeaderText = "Shifts Worked";
            NSGrid.Columns[5].HeaderText = "Pro-Rata";
            NSGrid.Columns[6].HeaderText = "Awop";
            NSGrid.Columns[7].HeaderText = "Rand";
            NSGrid.Columns[8].HeaderText = "Pro-Rate Awop";
            NSGrid.Columns[9].HeaderText = "LTI";
            NSGrid.Columns[10].HeaderText = "Rand";
            NSGrid.Columns[11].HeaderText = "Pro-Rate Pay";

            NSGrid.Columns[12].HeaderText = "SickDays";
            NSGrid.Columns[13].HeaderText = "AWOPDays";
            NSGrid.Columns[14].HeaderText = "SickPec";
            NSGrid.Columns[15].HeaderText = "AwopPerc";
            NSGrid.Columns[16].HeaderText = "TotPerc";
            NSGrid.Columns[17].HeaderText = "Pot";

            NSGrid.Columns[18].HeaderText = "FinPay";




            NSGrid.Columns[0].Width = 70;
            NSGrid.Columns[1].Width = 70;
            NSGrid.Columns[2].Width = 70;
            NSGrid.Columns[3].Width = 70;
            NSGrid.Columns[4].Width = 70;
            NSGrid.Columns[5].Width = 70;
            NSGrid.Columns[6].Width = 70;
            NSGrid.Columns[7].Width = 70;
            NSGrid.Columns[8].Width = 70;
            NSGrid.Columns[9].Width = 70;
            NSGrid.Columns[10].Width = 70;
            NSGrid.Columns[11].Width = 70;


            for (int i = 0; i <= NSGrid.Rows.Count - 1; i++)
            {
                NSGrid.Rows[i].Cells[0].Value = "";
                NSGrid.Rows[i].Cells[1].Value = "";
                NSGrid.Rows[i].Cells[2].Value = "";
                NSGrid.Rows[i].Cells[3].Value = "";
                NSGrid.Rows[i].Cells[4].Value = "";
                NSGrid.Rows[i].Cells[5].Value = "";
                NSGrid.Rows[i].Cells[6].Value = "";
                NSGrid.Rows[i].Cells[7].Value = "";
                NSGrid.Rows[i].Cells[8].Value = "";
                NSGrid.Rows[i].Cells[9].Value = "";
                NSGrid.Rows[i].Cells[10].Value = "";
                NSGrid.Rows[i].Cells[11].Value = "";
            }
            //////////////////////////////////////////////////////////////////////////////////

            int DSrow = 0;
            int NSrow = 0;
            double ColTwo = 0;
            double ColFive = 0;
            double ColEight = 0;
            double AwopFactor = 0;

            foreach (DataRow ds in dtPpl.Rows)
            {
                if (ds["Shift"].ToString() == "D")
                {
                    DSGrid.Rows[DSrow].Cells[0].Value = ds["Myname"].ToString();
                    DSGrid.Rows[DSrow].Cells[1].Value = ds["IndustryNumber"].ToString();
                    DSGrid.Rows[DSrow].Cells[2].Value = Convert.ToString(Math.Round(FiftyPercRule, 2) * Convert.ToDouble(DayShiftFacor));
                    ColTwo = FiftyPercRule * Convert.ToDouble(DayShiftFacor);
                    DSGrid.Rows[DSrow].Cells[3].Value = possibleShifts;
                    DSGrid.Rows[DSrow].Cells[4].Value = ds["Work"].ToString();

                    if (Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[3].Value) > 0)
                    {
                        DSGrid.Rows[DSrow].Cells[5].Value = Math.Round(Convert.ToDecimal((Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[2].Value) + Convert.ToDecimal(0.001)) / Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[3].Value) * Convert.ToDecimal(ds["Work"].ToString())), 2);
                        ColFive = Convert.ToDouble((Convert.ToDouble(DSGrid.Rows[DSrow].Cells[2].Value)) / Convert.ToDouble(DSGrid.Rows[DSrow].Cells[3].Value) * Convert.ToDouble(ds["Work"].ToString()));
                    }
                    DSGrid.Rows[DSrow].Cells[6].Value = ds["Ab"].ToString();

                    if (Convert.ToInt16(ds["Ab"].ToString()) == 0)
                    {
                        DSGrid.Rows[DSrow].Cells[8].Value = Convert.ToString(Math.Round(Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[5].Value) * 1, 2));
                        ColEight = ColFive * 1;
                        AwopFactor = 1;
                    }

                    if (Convert.ToInt16(ds["Ab"].ToString()) == 1)
                    {

                        DSGrid.Rows[DSrow].Cells[8].Value = Convert.ToString(Math.Round(Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[5].Value) * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["OneAwop"].ToString()), 2));
                        ColEight = ColFive;// * Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["OneAwop"].ToString());
                        AwopFactor = Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["OneAwop"].ToString());
                    }

                    if (Convert.ToInt16(ds["Ab"].ToString()) == 2)
                    {

                        DSGrid.Rows[DSrow].Cells[8].Value = Convert.ToString(Math.Round(Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[5].Value) * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["TwoAwop"].ToString()), 2));
                        ColEight = ColFive;// *Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["TwoAwop"].ToString());
                        AwopFactor = Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["TwoAwop"].ToString());
                    }

                    if (Convert.ToInt16(ds["Ab"].ToString()) >= 3)
                    {

                        DSGrid.Rows[DSrow].Cells[8].Value = Convert.ToString(Math.Round(Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[5].Value) * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["ThreeAwop"].ToString()), 2));
                        ColEight = ColFive;// *Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["ThreeAwop"].ToString());
                        AwopFactor = Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["ThreeAwop"].ToString());
                    }

                    //DSGrid.Rows[DSrow].Cells[12].Value = Convert.ToDecimal(ds["s1"].ToString());
                    //DSGrid.Rows[DSrow].Cells[13].Value = DSGrid.Rows[DSrow].Cells[5].Value;
                    //if (Convert.ToDecimal(ds["s1"].ToString()) > 0)
                    //  DSGrid.Rows[DSrow].Cells[14].Value = Convert.ToDecimal(100)- Convert.ToDecimal(ds["Factor"].ToString());
                    //else
                    //   DSGrid.Rows[DSrow].Cells[14].Value = 0;

                    //DSGrid.Rows[DSrow].Cells[15].Value = Convert.ToDecimal(100) - (Convert.ToDecimal(AwopFactor) * Convert.ToDecimal(100));

                    //DSGrid.Rows[DSrow].Cells[16].Value = Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[11].Value) *( Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[15].Value)/100);


                    //DSGrid.Rows[DSrow].Cells[17].Value = Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[12].Value) + Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[12].Value);


                    DSGrid.Rows[DSrow].Cells[7].Value = Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[8].Value) - Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[5].Value);

                    DSGrid.Rows[DSrow].Cells[9].Value = DSLTI;

                    if (DSLTI == 0)
                    {
                        //Convert.ToDecimal(Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[2].Value) / Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[3].Value) * Convert.ToDecimal(ds["Work"].ToString()))
                        //DSGrid.Rows[DSrow].Cells[11].Value = Math.Round(Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[8].Value) * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["ZeroLti"].ToString()),2);

                        //DSGrid.Rows[DSrow].Cells[11].Value = Math.Round((Convert.ToDecimal(Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[2].Value) / Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[3].Value) * Convert.ToDecimal(ds["Work"].ToString()))) * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["ZeroLti"].ToString()), 2);
                        //DSGrid.Rows[DSrow].Cells[10].Value = Math.Round((Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[8].Value) * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["ZeroLti"].ToString())) - Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[8].Value), 2); 

                        //DSGrid.Rows[DSrow].Cells[11].Value = Math.Round((Convert.ToDouble(ColTwo / Convert.ToDouble(DSGrid.Rows[DSrow].Cells[3].Value) * Convert.ToDouble(ds["Work"].ToString()))) * Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["ZeroLti"].ToString()), 2);
                        DSGrid.Rows[DSrow].Cells[11].Value = Math.Round((Convert.ToDouble(ColTwo / Convert.ToDouble(DSGrid.Rows[DSrow].Cells[3].Value) * Convert.ToDouble(ds["Work"].ToString()))) * Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["ZeroLti"].ToString()), 2);
                        DSGrid.Rows[DSrow].Cells[10].Value = Math.Round(ColEight * Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["ZeroLti"].ToString()) - ColEight, 2);
                    }

                    if (DSLTI >= 1)
                    {
                        //DSGrid.Rows[DSrow].Cells[11].Value = Math.Round(Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[8].Value) * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["OneLti"].ToString()), 2);

                        //DSGrid.Rows[DSrow].Cells[11].Value = Math.Round((Convert.ToDecimal(Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[2].Value) / Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[3].Value) * Convert.ToDecimal(ds["Work"].ToString()))) * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["OneLti"].ToString()), 2);
                        //DSGrid.Rows[DSrow].Cells[10].Value = Math.Round((Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[8].Value) * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["OneLti"].ToString())) - Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[8].Value), 2); 

                        //DSGrid.Rows[DSrow].Cells[11].Value = Math.Round((Convert.ToDouble(ColTwo / Convert.ToDouble(DSGrid.Rows[DSrow].Cells[3].Value) * Convert.ToDouble(ds["Work"].ToString()))) * Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["OneLti"].ToString()), 2);
                        DSGrid.Rows[DSrow].Cells[11].Value = Math.Round((Convert.ToDouble(ColTwo / Convert.ToDouble(DSGrid.Rows[DSrow].Cells[3].Value) * Convert.ToDouble(ds["Work"].ToString()))) * Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["OneLti"].ToString()), 2);
                        DSGrid.Rows[DSrow].Cells[10].Value = Math.Round(ColEight * Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["OneLti"].ToString()) - ColEight, 2);
                    }

                    //DSGrid.Rows[DSrow].Cells[10].Value = Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[11].Value) - Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[8].Value); 
                    //DSGrid.Rows[DSrow].Cells[10].Value = Math.Round((Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[8].Value) * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["ZeroLti"].ToString())) - Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[8].Value),2); 


                    DSGrid.Rows[DSrow].Cells[12].Value = Convert.ToDecimal(ds["s1"].ToString());
                    DSGrid.Rows[DSrow].Cells[13].Value = DSGrid.Rows[DSrow].Cells[6].Value;
                    if (Convert.ToDecimal(ds["s1"].ToString()) > 0)
                        DSGrid.Rows[DSrow].Cells[14].Value = Convert.ToDecimal(100) - Convert.ToDecimal(ds["Factor"].ToString());
                    else
                        DSGrid.Rows[DSrow].Cells[14].Value = 0;

                    DSGrid.Rows[DSrow].Cells[15].Value = Convert.ToDecimal(100) - (Convert.ToDecimal(AwopFactor) * Convert.ToDecimal(100));



                    DSGrid.Rows[DSrow].Cells[16].Value = Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[14].Value) + Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[15].Value);


                    DSGrid.Rows[DSrow].Cells[17].Value = Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[11].Value) * (Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[16].Value) / 100);




                    DSGrid.Rows[DSrow].Cells[18].Value = Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[11].Value) - Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[17].Value);
                    DSGrid.Rows[DSrow].Cells[11].Value = DSGrid.Rows[DSrow].Cells[18].Value;

                    DSGrid.Rows[DSrow].Cells[10].Value = Math.Round(Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[10].Value), 2);
                    DSGrid.Rows[DSrow].Cells[11].Value = Math.Round(Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[11].Value), 2);
                    DSGrid.Rows[DSrow].Cells[17].Value = Math.Round(Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[17].Value), 2);
                    DSGrid.Rows[DSrow].Cells[18].Value = Math.Round(Convert.ToDecimal(DSGrid.Rows[DSrow].Cells[18].Value), 2);



                    DSrow = DSrow + 1;



                }
                else
                {
                    NSGrid.Rows[NSrow].Cells[0].Value = ds["Myname"].ToString();
                    NSGrid.Rows[NSrow].Cells[1].Value = ds["IndustryNumber"].ToString();
                    NSGrid.Rows[NSrow].Cells[2].Value = Math.Round(Math.Round(FiftyPercRule, 2) * Convert.ToDouble(NightShiftFacor) + 0.001, 2);
                    ColTwo = Math.Round(FiftyPercRule, 2) * Convert.ToDouble(NightShiftFacor);
                    ColTwo = FiftyPercRule * Convert.ToDouble(NightShiftFacor);
                    NSGrid.Rows[NSrow].Cells[3].Value = possibleShifts;
                    NSGrid.Rows[NSrow].Cells[4].Value = ds["Work"].ToString();

                    if (Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[3].Value) > 0)
                    {
                        //NSGrid.Rows[NSrow].Cells[5].Value = Math.Round(Convert.ToDecimal((Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[2].Value.ToString()) + Convert.ToDecimal(0.001)) / Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[3].Value.ToString()) * Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[4].Value.ToString())), 2);
                        NSGrid.Rows[NSrow].Cells[5].Value = Math.Round(Convert.ToDecimal(ColTwo + Convert.ToDouble(0.001)) / Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[3].Value.ToString()) * Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[4].Value.ToString()), 2);
                        ColFive = Convert.ToDouble(ColTwo) / Convert.ToDouble(NSGrid.Rows[NSrow].Cells[3].Value.ToString()) * Convert.ToDouble(NSGrid.Rows[NSrow].Cells[4].Value.ToString());
                    }

                    NSGrid.Rows[NSrow].Cells[6].Value = ds["Ab"].ToString();

                    if (Convert.ToInt16(ds["Ab"].ToString()) == 0)
                    {

                        NSGrid.Rows[NSrow].Cells[8].Value = Convert.ToString(Math.Round(Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[5].Value) * 1, 2));
                        //NSGrid.Rows[NSrow].Cells[8].Value = Convert.ToString(Math.Round(ColFive * 1, 2));
                        ColEight = ColFive * 1;
                        AwopFactor = 1;

                    }

                    if (Convert.ToInt16(ds["Ab"].ToString()) == 1)
                    {

                        //NSGrid.Rows[NSrow].Cells[8].Value = Convert.ToString(Math.Round(Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[5].Value) * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["OneAwop"].ToString()), 2));
                        NSGrid.Rows[NSrow].Cells[8].Value = Convert.ToString(Math.Round(ColFive * Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["OneAwop"].ToString()), 2));
                        ColEight = ColFive;// *Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["OneAwop"].ToString());
                        AwopFactor = Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["OneAwop"].ToString());
                    }

                    if (Convert.ToInt16(ds["Ab"].ToString()) == 2)
                    {

                        //NSGrid.Rows[NSrow].Cells[8].Value = Convert.ToString(Math.Round(Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[5].Value) * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["TwoAwop"].ToString()), 2));
                        NSGrid.Rows[NSrow].Cells[8].Value = Convert.ToString(Math.Round(ColFive * Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["TwoAwop"].ToString()), 2));
                        ColEight = ColFive;// *Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["TwoAwop"].ToString());
                        AwopFactor = Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["TwoAwop"].ToString());
                    }

                    if (Convert.ToInt16(ds["Ab"].ToString()) >= 3)
                    {

                        //NSGrid.Rows[NSrow].Cells[8].Value = Convert.ToString(Math.Round(Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[5].Value) * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["ThreeAwop"].ToString()), 2));
                        NSGrid.Rows[NSrow].Cells[8].Value = Convert.ToString(Math.Round(ColFive * Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["ThreeAwop"].ToString()), 2));
                        ColEight = ColFive;// *Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["ThreeAwop"].ToString());
                        AwopFactor = Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["ThreeAwop"].ToString());
                    }

                    NSGrid.Rows[NSrow].Cells[7].Value = Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[8].Value) - Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[5].Value);

                    NSGrid.Rows[NSrow].Cells[8].Value = NSGrid.Rows[NSrow].Cells[5].Value;

                    NSGrid.Rows[NSrow].Cells[9].Value = NSLTI;

                    if (NSLTI == 0)
                    {
                        //NSGrid.Rows[NSrow].Cells[11].Value = Math.Round(Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[8].Value) * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["ZeroLti"].ToString()), 2);

                        //NSGrid.Rows[NSrow].Cells[11].Value = Math.Round((Convert.ToDecimal(Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[2].Value) / Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[3].Value) * Convert.ToDecimal(ds["Work"].ToString()))) * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["ZeroLti"].ToString()), 2);
                        //NSGrid.Rows[NSrow].Cells[10].Value = Math.Round((Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[8].Value) * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["ZeroLti"].ToString())) - Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[8].Value), 2); 

                        //NSGrid.Rows[NSrow].Cells[11].Value = Math.Round(ColEight * Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["ZeroLti"].ToString()), 2);
                        NSGrid.Rows[NSrow].Cells[11].Value = Math.Round(ColEight * Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["ZeroLti"].ToString()), 2);
                        NSGrid.Rows[NSrow].Cells[10].Value = Math.Round(ColEight * Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["ZeroLti"].ToString()) - ColEight, 2);
                    }

                    if (NSLTI >= 1)
                    {
                        //NSGrid.Rows[NSrow].Cells[11].Value = Math.Round((Convert.ToDecimal(Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[2].Value) / Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[3].Value) * Convert.ToDecimal(ds["Work"].ToString()))) * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["OneLti"].ToString()), 2);

                        //NSGrid.Rows[NSrow].Cells[11].Value = Math.Round((Convert.ToDecimal(Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[2].Value) / Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[3].Value) * Convert.ToDecimal(ds["Work"].ToString()))) * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["OneLti"].ToString()), 2);
                        //NSGrid.Rows[NSrow].Cells[10].Value = Math.Round((Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[8].Value) * Convert.ToDecimal(_dbManRate.ResultsDataTable.Rows[0]["OneLti"].ToString())) - Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[8].Value), 2); 

                        //NSGrid.Rows[NSrow].Cells[11].Value = Math.Round(ColEight * Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["OneLti"].ToString()), 2);
                        NSGrid.Rows[NSrow].Cells[11].Value = Math.Round(ColEight * Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["OneLti"].ToString()), 2);
                        NSGrid.Rows[NSrow].Cells[10].Value = Math.Round(ColEight * Convert.ToDouble(_dbManRate.ResultsDataTable.Rows[0]["OneLti"].ToString()) - ColEight, 2);
                    }


                    //NSGrid.Rows[NSrow].Cells[10].Value = Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[11].Value) - Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[8].Value); 

                    NSGrid.Rows[NSrow].Cells[8].Value = NSGrid.Rows[NSrow].Cells[5].Value;


                    NSGrid.Rows[NSrow].Cells[12].Value = Convert.ToDecimal(ds["s1"].ToString());
                    NSGrid.Rows[NSrow].Cells[13].Value = NSGrid.Rows[NSrow].Cells[6].Value;
                    if (Convert.ToDecimal(ds["s1"].ToString()) > 0)
                        NSGrid.Rows[NSrow].Cells[14].Value = Convert.ToDecimal(100) - Convert.ToDecimal(ds["Factor"].ToString());
                    else
                        NSGrid.Rows[NSrow].Cells[14].Value = 0;

                    NSGrid.Rows[NSrow].Cells[15].Value = Convert.ToDecimal(100) - (Convert.ToDecimal(AwopFactor) * Convert.ToDecimal(100));



                    NSGrid.Rows[NSrow].Cells[16].Value = Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[14].Value) + Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[15].Value);


                    NSGrid.Rows[NSrow].Cells[17].Value = Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[11].Value) * (Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[16].Value) / 100);




                    NSGrid.Rows[NSrow].Cells[18].Value = Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[11].Value) - Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[17].Value);

                    NSGrid.Rows[NSrow].Cells[11].Value = NSGrid.Rows[NSrow].Cells[18].Value;

                    NSGrid.Rows[NSrow].Cells[10].Value = Convert.ToString(Math.Round(Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[10].Value), 2));
                    NSGrid.Rows[NSrow].Cells[11].Value = Math.Round(Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[11].Value), 2).ToString();
                    NSGrid.Rows[NSrow].Cells[17].Value = Math.Round(Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[17].Value), 2).ToString();
                    NSGrid.Rows[NSrow].Cells[18].Value = Math.Round(Convert.ToDecimal(NSGrid.Rows[NSrow].Cells[18].Value), 2).ToString();




                    //NSGrid.Rows[NSrow].Cells[11].Value = NSGrid.Rows[NSrow].Cells[18].Value;

                    NSrow = NSrow + 1;
                }
            }

            DSGrid.RowCount = DSrow;
            NSGrid.RowCount = NSrow;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////



            MWDataManager.clsDataAccess _dbManReportOrg = new MWDataManager.clsDataAccess();
            _dbManReportOrg.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManReportOrg.SqlStatement = " select '" + Month1 + "' Month1,'" + Month2 + "' Month2,'" + SB.Substring(0, 5) + "' SB,'" + OrgUnitGrid.Rows[0].Cells[0].Value.ToString() + "' Org1, '" + OrgUnitGrid.Rows[0].Cells[1].Value.ToString() + "'  Org2, '" + OrgUnitGrid.Rows[0].Cells[2].Value.ToString() + "'  Org3, '" + OrgUnitGrid.Rows[0].Cells[3].Value.ToString() + "'  Org4, '" + OrgUnitGrid.Rows[0].Cells[4].Value.ToString() + "'  Org5, '" + OrgUnitGrid.Rows[0].Cells[5].Value.ToString() + "'  Org6, '" + OrgUnitGrid.Rows[0].Cells[6].Value.ToString() + "'  Org7, '" + OrgUnitGrid.Rows[0].Cells[7].Value.ToString() + "'  Org8, '" + OrgUnitGrid.Rows[0].Cells[8].Value.ToString() + "'  Org9, '" + OrgUnitGrid.Rows[0].Cells[9].Value.ToString() + "'  Org10 ";
            _dbManReportOrg.SqlStatement = _dbManReportOrg.SqlStatement + "  ,'" + OrgUnitGrid.Rows[1].Cells[0].Value.ToString() + "' Meter1, '" + OrgUnitGrid.Rows[1].Cells[1].Value.ToString() + "'  Meter2, '" + OrgUnitGrid.Rows[1].Cells[2].Value.ToString() + "'  Meter3, '" + OrgUnitGrid.Rows[1].Cells[3].Value.ToString() + "'  Meter4, '" + OrgUnitGrid.Rows[1].Cells[4].Value.ToString() + "'  Meter5, '" + OrgUnitGrid.Rows[1].Cells[5].Value.ToString() + "'  Meter6, '" + OrgUnitGrid.Rows[1].Cells[6].Value.ToString() + "'  Meter7, '" + OrgUnitGrid.Rows[1].Cells[7].Value.ToString() + "'  Meter8, '" + OrgUnitGrid.Rows[1].Cells[8].Value.ToString() + "'  Meter9, '" + OrgUnitGrid.Rows[1].Cells[9].Value.ToString() + "'  Meter10 ";
            _dbManReportOrg.SqlStatement = _dbManReportOrg.SqlStatement + "  ,'" + OrgUnitGrid.Rows[2].Cells[0].Value.ToString() + "' Pay1, '" + OrgUnitGrid.Rows[2].Cells[1].Value.ToString() + "'  Pay2, '" + OrgUnitGrid.Rows[2].Cells[2].Value.ToString() + "'  Pay3, '" + OrgUnitGrid.Rows[2].Cells[3].Value.ToString() + "'  Pay4, '" + OrgUnitGrid.Rows[2].Cells[4].Value.ToString() + "'  Pay5, '" + OrgUnitGrid.Rows[2].Cells[5].Value.ToString() + "'  Pay6, '" + OrgUnitGrid.Rows[2].Cells[6].Value.ToString() + "'  Pay7, '" + OrgUnitGrid.Rows[2].Cells[7].Value.ToString() + "'  Pay8, '" + OrgUnitGrid.Rows[2].Cells[8].Value.ToString() + "'  Pay9, '" + OrgUnitGrid.Rows[2].Cells[9].Value.ToString() + "'  Pay10 ";
            _dbManReportOrg.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManReportOrg.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManReportOrg.ResultsTableName = "OrgUnit";
            _dbManReportOrg.ExecuteInstruction();

            DataSet dsABS1 = new DataSet();
            dsABS1.Tables.Add(_dbManReportOrg.ResultsDataTable);

            report.RegisterData(dsABS1);

            MWDataManager.clsDataAccess _dbManReportPay = new MWDataManager.clsDataAccess();
            _dbManReportPay.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            if (this.Text == "Stoping Shift Boss Bonus")
            {



                _dbManReportPay.SqlStatement = " select '" + BaseRate + "' BaseRate,'" + Math.Round(AdjBaseRate, 2) + "' AdjBaseRate,'" + Mined + "' Mined,'" + Call + "' Call,'" + PercRate + "' PerRate,'" + Math.Round(SafetyPayment, 2) + "' SafetyPay,'" + Math.Round(EngPayment, 2) + "' EngPay, '" + Math.Round(TonsPayment, 2) + "' TonsPay,'" + Math.Round(SweepsPayment, 2) + "' SweepPay, ";
                _dbManReportPay.SqlStatement = _dbManReportPay.SqlStatement + "  '" + SafetyAchieved + "' SafetyAchieved,'" + EngAchieved + "' EngAchieved, '" + TonsAchieved + "' TonsAchieved, '" + SweepsAchieved + "' SweepsAchieved, ";
                _dbManReportPay.SqlStatement = _dbManReportPay.SqlStatement + " '" + _dbManRate.ResultsDataTable.Rows[0]["SafetyFactor1"].ToString() + "'SafetyFactor1,'" + _dbManRate.ResultsDataTable.Rows[0]["SafetyFactor2"].ToString() + "'SafetyFactor2,'" + _dbManRate.ResultsDataTable.Rows[0]["SafetyFactor3"].ToString() + "'SafetyFactor3,'" + _dbManRate.ResultsDataTable.Rows[0]["SafetyFactor4"].ToString() + "'SafetyFactor4, ";
                _dbManReportPay.SqlStatement = _dbManReportPay.SqlStatement + " '" + _dbManRate.ResultsDataTable.Rows[0]["RockFactor1"].ToString() + "'RockFactor1,'" + _dbManRate.ResultsDataTable.Rows[0]["RockFactor2"].ToString() + "'RockFactor2,'" + _dbManRate.ResultsDataTable.Rows[0]["RockFactor3"].ToString() + "'RockFactor3,'" + _dbManRate.ResultsDataTable.Rows[0]["RockFactor4"].ToString() + "'RockFactor4, ";
                _dbManReportPay.SqlStatement = _dbManReportPay.SqlStatement + " '" + Factor1 + "'TonsFactor1,'" + Factor2 + "'TonsFactor2,'" + Factor3 + "'TonsFactor3,'" + Factor4 + "'TonsFactor4, ";

                _dbManReportPay.SqlStatement = _dbManReportPay.SqlStatement + " '" + _dbManRate.ResultsDataTable.Rows[0]["SweepsFactor1"].ToString() + "'SweepsFactor1,'" + _dbManRate.ResultsDataTable.Rows[0]["SweepsFactor2"].ToString() + "'SweepsFactor2,'" + _dbManRate.ResultsDataTable.Rows[0]["SweepsFactor3"].ToString() + "'SweepsFactor3,'" + _dbManRate.ResultsDataTable.Rows[0]["SweepsFactor4"].ToString() + "'SweepsFactor4, ";
                _dbManReportPay.SqlStatement = _dbManReportPay.SqlStatement + " '" + Math.Round(SafetyVar, 2) + "' SafetyVar, '" + Math.Round(EngVar, 2) + "' EngVar, '" + Math.Round(TonsVar, 2) + "'TonsVar, '" + Math.Round(SweepsVar, 2) + "' SweepsVar,'" + Math.Round(FiftyPercRule, 2) + "' FiftyPercRule, '" + SafeFacused + "' SafeFacused,'" + EngFacused + "' EngFacused, '" + TonsFacused + "' TonsFacused, '" + SweepsFacused + "' SweepsFacused, ";
                _dbManReportPay.SqlStatement = _dbManReportPay.SqlStatement + " '" + _dbManRate.ResultsDataTable.Rows[0]["LateralFactor1"].ToString() + "'LateralFactor1,'" + _dbManRate.ResultsDataTable.Rows[0]["LateralFactor2"].ToString() + "'LateralFactor2,'" + _dbManRate.ResultsDataTable.Rows[0]["LateralFactor3"].ToString() + "'LateralFactor3,'" + _dbManRate.ResultsDataTable.Rows[0]["LateralFactor4"].ToString() + "'LateralFactor4,'" + Math.Round(LateralPayment, 2) + "'LateralPay,'" + LateralAchieved + "' LateralAchieved, ";
                _dbManReportPay.SqlStatement = _dbManReportPay.SqlStatement + " '" + _dbManRate.ResultsDataTable.Rows[0]["RaiseFactor1"].ToString() + "'RaiseFactor1,'" + _dbManRate.ResultsDataTable.Rows[0]["RaiseFactor2"].ToString() + "'RaiseFactor2,'" + _dbManRate.ResultsDataTable.Rows[0]["RaiseFactor3"].ToString() + "'RaiseFactor3,'" + _dbManRate.ResultsDataTable.Rows[0]["RaiseFactor4"].ToString() + "'RaiseFactor4,'" + Math.Round(RaisePayment, 2) + "'RaisePay,'" + RaiseAchieved + "'RaiseAchieved,'" + LateralMined + "' LateralMined,'" + LateralCall + "' LateralCall,'" + LateralPercRate + "'  LateralPerRate,'" + RaiseMined + "' RaiseMined,'" + RaiseCall + "' RaiseCall,'" + RaisePercRate + "' RaisePerRate, '" + Math.Round(LateralVar, 2) + "' LateralVar,'" + Math.Round(RaiseVar, 2) + "' RaiseVar,'" + LateralFacused + "' LateralFacused, '" + RaiseFacused + "' RaiseFacused, '" + Math.Round((CallLimit * 100), 0) + "' MyCallLimit  ";

            }
            else
            {

                _dbManReportPay.SqlStatement = " select '" + BaseRate + "' BaseRate,'" + Math.Round(AdjBaseRate, 2) + "' AdjBaseRate,'" + Mined + "' Mined,'" + Call + "' Call,'" + PercRate + "' PerRate,'" + Math.Round(SafetyPayment, 2) + "' SafetyPay,'" + Math.Round(EngPayment, 2) + "' EngPay, '" + Math.Round(TonsPayment, 2) + "' TonsPay,'" + Math.Round(SweepsPayment, 2) + "' SweepPay, ";
                _dbManReportPay.SqlStatement = _dbManReportPay.SqlStatement + "  '" + SafetyAchieved + "' SafetyAchieved,'" + EngAchieved + "' EngAchieved, '" + TonsAchieved + "' TonsAchieved, '" + SweepsAchieved + "' SweepsAchieved, ";
                _dbManReportPay.SqlStatement = _dbManReportPay.SqlStatement + " '" + _dbManRate.ResultsDataTable.Rows[0]["DevSafetyFactor1"].ToString() + "'SafetyFactor1,'" + _dbManRate.ResultsDataTable.Rows[0]["DevSafetyFactor2"].ToString() + "'SafetyFactor2,'" + _dbManRate.ResultsDataTable.Rows[0]["DevSafetyFactor3"].ToString() + "'SafetyFactor3,'" + _dbManRate.ResultsDataTable.Rows[0]["DevSafetyFactor4"].ToString() + "'SafetyFactor4, ";
                _dbManReportPay.SqlStatement = _dbManReportPay.SqlStatement + " '" + _dbManRate.ResultsDataTable.Rows[0]["DevRockFactor1"].ToString() + "'RockFactor1,'" + _dbManRate.ResultsDataTable.Rows[0]["DevRockFactor2"].ToString() + "'RockFactor2,'" + _dbManRate.ResultsDataTable.Rows[0]["DevRockFactor3"].ToString() + "'RockFactor3,'" + _dbManRate.ResultsDataTable.Rows[0]["DevRockFactor4"].ToString() + "'RockFactor4, ";
                _dbManReportPay.SqlStatement = _dbManReportPay.SqlStatement + " '" + _dbManRate.ResultsDataTable.Rows[0]["DevTonsFactor1"].ToString() + "'TonsFactor1,'" + _dbManRate.ResultsDataTable.Rows[0]["DevTonsFactor2"].ToString() + "'TonsFactor2,'" + _dbManRate.ResultsDataTable.Rows[0]["DevTonsFactor3"].ToString() + "'TonsFactor3,'" + Factor4 + "'TonsFactor4, ";
                _dbManReportPay.SqlStatement = _dbManReportPay.SqlStatement + " '" + _dbManRate.ResultsDataTable.Rows[0]["SweepsFactor1"].ToString() + "'SweepsFactor1,'" + _dbManRate.ResultsDataTable.Rows[0]["SweepsFactor2"].ToString() + "'SweepsFactor2,'" + _dbManRate.ResultsDataTable.Rows[0]["SweepsFactor3"].ToString() + "'SweepsFactor3,'" + _dbManRate.ResultsDataTable.Rows[0]["SweepsFactor4"].ToString() + "'SweepsFactor4, ";
                _dbManReportPay.SqlStatement = _dbManReportPay.SqlStatement + " '" + Math.Round(SafetyVar, 2) + "' SafetyVar, '" + Math.Round(EngVar, 2) + "' EngVar, '" + Math.Round(TonsVar, 2) + "'TonsVar, '" + Math.Round(SweepsVar, 2) + "' SweepsVar,'" + Math.Round(FiftyPercRule, 2) + "' FiftyPercRule, '" + SafeFacused + "' SafeFacused,'" + EngFacused + "' EngFacused, '" + TonsFacused + "' TonsFacused, '" + SweepsFacused + "' SweepsFacused, ";
                _dbManReportPay.SqlStatement = _dbManReportPay.SqlStatement + " '" + LatFactor1 + "'LateralFactor1,'" + LatFactor2 + "'LateralFactor2,'" + LatFactor3 + "'LateralFactor3,'" + LatFactor4 + "'LateralFactor4,'" + Math.Round(LateralPayment, 2) + "'LateralPay,'" + LateralAchieved + "' LateralAchieved, ";
                _dbManReportPay.SqlStatement = _dbManReportPay.SqlStatement + " '" + _dbManRate.ResultsDataTable.Rows[0]["RaiseFactor1"].ToString() + "'RaiseFactor1,'" + _dbManRate.ResultsDataTable.Rows[0]["RaiseFactor2"].ToString() + "'RaiseFactor2,'" + _dbManRate.ResultsDataTable.Rows[0]["RaiseFactor3"].ToString() + "'RaiseFactor3,'" + RaiseFactor4 + "'RaiseFactor4,'" + Math.Round(RaisePayment, 2) + "'RaisePay,'" + RaiseAchieved + "'RaiseAchieved,'" + LateralMined + "' LateralMined,'" + LateralCall + "' LateralCall,'" + LateralPercRate + "'  LateralPerRate,'" + RaiseMined + "' RaiseMined,'" + RaiseCall + "' RaiseCall,'" + RaisePercRate + "' RaisePerRate, '" + Math.Round(LateralVar, 2) + "' LateralVar,'" + Math.Round(RaiseVar, 2) + "' RaiseVar,'" + LateralFacused + "' LateralFacused, '" + RaiseFacused + "' RaiseFacused, '" + Math.Round((CallLimit * 100), 0) + "' MyCallLimit  ";

            }

            _dbManReportPay.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManReportPay.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManReportPay.ResultsTableName = "Payment";
            _dbManReportPay.ExecuteInstruction();

            DataSet dsABS2 = new DataSet();
            dsABS2.Tables.Add(_dbManReportPay.ResultsDataTable);

            report.RegisterData(dsABS2);

            //return;
            ////////////////////////////////////////////Shift Bosses D/S and N/S//////////////////////////
            MWDataManager.clsDataAccess _dbManShiftBossesDS = new MWDataManager.clsDataAccess();
            _dbManShiftBossesDS.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManShiftBossesDS.SqlStatement = "  ";

            decimal MyDS1 = 0;




            for (int i = 0; i <= DSGrid.Rows.Count - 1; i++)
            {
                if (i != DSGrid.Rows.Count - 1)
                {
                    MyDS1 = Convert.ToDecimal(DSGrid.Rows[i].Cells[2].Value.ToString());
                    MyDS1 = Math.Round(MyDS1, 2);
                    _dbManShiftBossesDS.SqlStatement = _dbManShiftBossesDS.SqlStatement + " select '" + DayShiftFacor + "' DSFactor, '" + DSGrid.Rows[i].Cells[0].Value.ToString() + "' Emp,'" + DSGrid.Rows[i].Cells[1].Value.ToString() + "' IndNum,'" + MyDS1 + "' Factor,'" + DSGrid.Rows[i].Cells[3].Value.ToString() + "' PosShifts,'" + DSGrid.Rows[i].Cells[4].Value.ToString() + "' WorkShifts, ";
                    _dbManShiftBossesDS.SqlStatement = _dbManShiftBossesDS.SqlStatement + "'" + DSGrid.Rows[i].Cells[5].Value.ToString() + "' ProRata1, '" + DSGrid.Rows[i].Cells[6].Value.ToString() + "' Awop,'" + DSGrid.Rows[i].Cells[7].Value.ToString() + "' AwopRand,'" + DSGrid.Rows[i].Cells[8].Value.ToString() + "' ProRataAwop,'" + DSGrid.Rows[i].Cells[9].Value.ToString() + "' LTI, '" + DSGrid.Rows[i].Cells[10].Value.ToString() + "' LTIRand, '" + DSGrid.Rows[i].Cells[11].Value.ToString() + "' LTIProRata, ";
                    _dbManShiftBossesDS.SqlStatement = _dbManShiftBossesDS.SqlStatement + "'" + DSGrid.Rows[i].Cells[12].Value.ToString() + "' Sickdays, '" + DSGrid.Rows[i].Cells[13].Value.ToString() + "' Awopdays,'" + DSGrid.Rows[i].Cells[14].Value.ToString() + "' sickperc,'" + DSGrid.Rows[i].Cells[15].Value.ToString() + "' awopperd,'" + DSGrid.Rows[i].Cells[16].Value.ToString() + "' totperc, '" + DSGrid.Rows[i].Cells[17].Value.ToString() + "' pot, '" + DSGrid.Rows[i].Cells[18].Value.ToString() + "' finpay  ";


                    _dbManShiftBossesDS.SqlStatement = _dbManShiftBossesDS.SqlStatement + " union ";
                }
                if (i == DSGrid.Rows.Count - 1)
                {
                    MyDS1 = Convert.ToDecimal(DSGrid.Rows[i].Cells[2].Value.ToString());
                    MyDS1 = Math.Round(MyDS1, 2);
                    _dbManShiftBossesDS.SqlStatement = _dbManShiftBossesDS.SqlStatement + " select '" + DayShiftFacor + "' DSFactor, '" + DSGrid.Rows[i].Cells[0].Value.ToString() + "' Emp,'" + DSGrid.Rows[i].Cells[1].Value.ToString() + "' IndNum,'" + MyDS1 + "' Factor,'" + DSGrid.Rows[i].Cells[3].Value.ToString() + "' PosShifts,'" + DSGrid.Rows[i].Cells[4].Value.ToString() + "' WorkShifts, ";
                    _dbManShiftBossesDS.SqlStatement = _dbManShiftBossesDS.SqlStatement + "'" + DSGrid.Rows[i].Cells[5].Value.ToString() + "' ProRata1, '" + DSGrid.Rows[i].Cells[6].Value.ToString() + "' Awop,'" + DSGrid.Rows[i].Cells[7].Value.ToString() + "' AwopRand,'" + DSGrid.Rows[i].Cells[8].Value.ToString() + "' ProRataAwop,'" + DSGrid.Rows[i].Cells[9].Value.ToString() + "' LTI, '" + DSGrid.Rows[i].Cells[10].Value.ToString() + "' LTIRand, '" + DSGrid.Rows[i].Cells[11].Value.ToString() + "' LTIProRata, ";
                    _dbManShiftBossesDS.SqlStatement = _dbManShiftBossesDS.SqlStatement + "'" + DSGrid.Rows[i].Cells[12].Value.ToString() + "' Sickdays, '" + DSGrid.Rows[i].Cells[13].Value.ToString() + "' Awopdays,'" + DSGrid.Rows[i].Cells[14].Value.ToString() + "' sickperc,'" + DSGrid.Rows[i].Cells[15].Value.ToString() + "' awopperd,'" + DSGrid.Rows[i].Cells[16].Value.ToString() + "' totperc, '" + DSGrid.Rows[i].Cells[17].Value.ToString() + "' pot, '" + DSGrid.Rows[i].Cells[18].Value.ToString() + "' finpay  ";
                    _dbManShiftBossesDS.SqlStatement = _dbManShiftBossesDS.SqlStatement + "  ";
                }
            }



            _dbManShiftBossesDS.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManShiftBossesDS.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManShiftBossesDS.ResultsTableName = "DS";
            _dbManShiftBossesDS.ExecuteInstruction();

            DataSet dsDS = new DataSet();
            dsDS.Tables.Add(_dbManShiftBossesDS.ResultsDataTable);

            report.RegisterData(dsDS);


            ////////////////////////////////////
            MWDataManager.clsDataAccess _dbManShiftBossesNS = new MWDataManager.clsDataAccess();
            _dbManShiftBossesNS.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManShiftBossesNS.SqlStatement = "  ";

            decimal MyNS1 = 0;
            for (int i = 0; i <= NSGrid.Rows.Count - 1; i++)
            {
                if (i != NSGrid.Rows.Count - 1)
                {
                    MyNS1 = Convert.ToDecimal(NSGrid.Rows[i].Cells[2].Value.ToString());
                    MyNS1 = Math.Round(MyNS1, 2);
                    _dbManShiftBossesNS.SqlStatement = _dbManShiftBossesNS.SqlStatement + " select '" + NightShiftFacor + "' DSFactor, '" + NSGrid.Rows[i].Cells[0].Value.ToString() + "' Emp,'" + NSGrid.Rows[i].Cells[1].Value.ToString() + "' IndNum,'" + MyNS1 + "' Factor,'" + NSGrid.Rows[i].Cells[3].Value.ToString() + "' PosShifts,'" + NSGrid.Rows[i].Cells[4].Value.ToString() + "' WorkShifts, ";
                    _dbManShiftBossesNS.SqlStatement = _dbManShiftBossesNS.SqlStatement + "'" + NSGrid.Rows[i].Cells[5].Value.ToString() + "' ProRata1, '" + NSGrid.Rows[i].Cells[6].Value.ToString() + "' Awop,'" + NSGrid.Rows[i].Cells[7].Value.ToString() + "' AwopRand,'" + NSGrid.Rows[i].Cells[8].Value.ToString() + "' ProRataAwop,'" + NSGrid.Rows[i].Cells[9].Value.ToString() + "' LTI, '" + NSGrid.Rows[i].Cells[10].Value.ToString() + "' LTIRand, '" + NSGrid.Rows[i].Cells[11].Value.ToString() + "' LTIProRata, ";
                    _dbManShiftBossesNS.SqlStatement = _dbManShiftBossesNS.SqlStatement + "'" + NSGrid.Rows[i].Cells[12].Value.ToString() + "' Sickdays, '" + NSGrid.Rows[i].Cells[13].Value.ToString() + "' Awopdays,'" + NSGrid.Rows[i].Cells[14].Value.ToString() + "' sickperc,'" + NSGrid.Rows[i].Cells[15].Value.ToString() + "' awopperd,'" + NSGrid.Rows[i].Cells[16].Value.ToString() + "' totperc, '" + NSGrid.Rows[i].Cells[17].Value.ToString() + "' pot, '" + NSGrid.Rows[i].Cells[18].Value.ToString() + "' finpay ";
                    _dbManShiftBossesNS.SqlStatement = _dbManShiftBossesNS.SqlStatement + " union ";
                }
                if (i == NSGrid.Rows.Count - 1)
                {
                    MyNS1 = Convert.ToDecimal(NSGrid.Rows[i].Cells[2].Value.ToString());
                    MyNS1 = Math.Round(MyNS1, 2);
                    _dbManShiftBossesNS.SqlStatement = _dbManShiftBossesNS.SqlStatement + " select '" + NightShiftFacor + "' DSFactor, '" + NSGrid.Rows[i].Cells[0].Value.ToString() + "' Emp,'" + NSGrid.Rows[i].Cells[1].Value.ToString() + "' IndNum,'" + MyNS1 + "' Factor,'" + NSGrid.Rows[i].Cells[3].Value.ToString() + "' PosShifts,'" + NSGrid.Rows[i].Cells[4].Value.ToString() + "' WorkShifts, ";
                    _dbManShiftBossesNS.SqlStatement = _dbManShiftBossesNS.SqlStatement + "'" + NSGrid.Rows[i].Cells[5].Value.ToString() + "' ProRata1, '" + NSGrid.Rows[i].Cells[6].Value.ToString() + "' Awop,'" + NSGrid.Rows[i].Cells[7].Value.ToString() + "' AwopRand,'" + NSGrid.Rows[i].Cells[8].Value.ToString() + "' ProRataAwop,'" + NSGrid.Rows[i].Cells[9].Value.ToString() + "' LTI, '" + NSGrid.Rows[i].Cells[10].Value.ToString() + "' LTIRand, '" + NSGrid.Rows[i].Cells[11].Value.ToString() + "' LTIProRata, ";
                    _dbManShiftBossesNS.SqlStatement = _dbManShiftBossesNS.SqlStatement + "'" + NSGrid.Rows[i].Cells[12].Value.ToString() + "' Sickdays, '" + NSGrid.Rows[i].Cells[13].Value.ToString() + "' Awopdays,'" + NSGrid.Rows[i].Cells[14].Value.ToString() + "' sickperc,'" + NSGrid.Rows[i].Cells[15].Value.ToString() + "' awopperd,'" + NSGrid.Rows[i].Cells[16].Value.ToString() + "' totperc, '" + NSGrid.Rows[i].Cells[17].Value.ToString() + "' pot, '" + NSGrid.Rows[i].Cells[18].Value.ToString() + "' finpay  ";
                    _dbManShiftBossesNS.SqlStatement = _dbManShiftBossesNS.SqlStatement + "  ";
                }
            }
            _dbManShiftBossesNS.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManShiftBossesNS.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManShiftBossesNS.ResultsTableName = "NS";
            _dbManShiftBossesNS.ExecuteInstruction();

            DataSet dsNS = new DataSet();
            dsNS.Tables.Add(_dbManShiftBossesNS.ResultsDataTable);

            report.RegisterData(dsNS);

            /////////////////////////////////////////////////////////////////////////////////////////////

            if (this.Text == "Stoping Shift Boss Bonus")
            {

                report.Load(_reportFolder + "SBBonus.frx");
            }
            else
            {
                report.Load(_reportFolder + "DevSBBonus.frx");
            }


            //   report.Design();

            pcReport.Clear();
            report.Prepare();
            report.Preview = pcReport;
            report.ShowPrepared();

        }

        private void btnTransfer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //return;

            result = MessageBox.Show("Are you sure you want to transfer the Bonus Details to the ARMS Interface?", "Transfer Record", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
                _dbMan2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan2.SqlStatement = "select * from mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew   " +
                                       " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunit = '" + lblOrgunit.Text + "' and Transferred = 'Y'   ";
                _dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan2.ResultsTableName = "ShiftBoss";
                _dbMan2.ExecuteInstruction();

                if ((_dbMan2.ResultsDataTable.Rows.Count > 0) && (clsUserInfo.UserID != "MINEWARE"))
                {
                    MessageBox.Show("OrgUnit has already been Transfered", "Transfer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    MWDataManager.clsDataAccess _dbMandelete = new MWDataManager.clsDataAccess();
                    _dbMandelete.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMandelete.SqlStatement = "delete  from mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew   " +
                                           " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunit like '" + lblOrgunit.Text + "' and Type = '08'   ";
                    _dbMandelete.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMandelete.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMandelete.ResultsTableName = "ShiftBoss";
                    _dbMandelete.ExecuteInstruction();

                    MWDataManager.clsDataAccess _dbManInsert = new MWDataManager.clsDataAccess();
                    _dbManInsert.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    // _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " Insert into tbl_BCS_ARMS_Interface_TransferNew values(";


                    if (this.Text == "Stoping Shift Boss Bonus")
                    {
                        Act = "0";
                    }
                    else
                    {
                        Act = "1";
                    }

                    for (int i = 0; i <= DSGrid.Rows.Count - 1; i++)
                    {
                        int index = DSGrid.Rows[i].Cells[0].Value.ToString().IndexOf(".");
                        string emp = DSGrid.Rows[i].Cells[0].Value.ToString() + "    ";



                        emp = emp.Substring(index + 1, 5);

                        _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + "Insert into mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew values( '1', Getdate(),'" + Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) + "', '" + Act + "', '" + DSGrid.Rows[i].Cells[1].Value.ToString() + "', '" + emp.Trim() + "' ";
                        _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + ", '" + DSGrid.Rows[i].Cells[0].Value.ToString().Substring(0, index) + "', 'DayShift ShiftBoss ', '" + lblOrgunit.Text + "','D', '','','" + DSGrid.Rows[i].Cells[2].Value.ToString() + "' ";
                        _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + ",'" + DSGrid.Rows[i].Cells[4].Value.ToString() + "','" + DSGrid.Rows[i].Cells[6].Value.ToString() + "', 'DayShift ShiftBoss Bonus','0','0','" + DSGrid.Rows[i].Cells[9].Value.ToString() + "', ";
                        _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " '0', '" + DSGrid.Rows[i].Cells[8].Value.ToString() + "', '08', 'Y', GetDate(),'0.00', '" + DSGrid.Rows[i].Cells[11].Value.ToString() + "')";
                    }

                    for (int i = 0; i <= NSGrid.Rows.Count - 1; i++)
                    {
                        int index = NSGrid.Rows[i].Cells[0].Value.ToString().IndexOf(".");
                        string emp = NSGrid.Rows[i].Cells[0].Value.ToString() + "    ";

                        emp = emp.Substring(index + 1, 5);

                        _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " Insert into mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew values('1', Getdate(),'" + Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) + "', '" + Act + "', '" + NSGrid.Rows[i].Cells[1].Value.ToString() + "', '" + emp.Trim() + "' ";
                        _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + ", '" + NSGrid.Rows[i].Cells[0].Value.ToString().Substring(0, index) + "', 'NightShift ShiftBoss ', '" + lblOrgunit.Text + "','N', '','','" + NSGrid.Rows[i].Cells[2].Value.ToString() + "' ";
                        _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + ",'" + NSGrid.Rows[i].Cells[4].Value.ToString() + "','" + NSGrid.Rows[i].Cells[6].Value.ToString() + "', 'NightShift ShiftBoss Bonus','0','0','" + NSGrid.Rows[i].Cells[9].Value.ToString() + "', ";
                        _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " '0', '" + NSGrid.Rows[i].Cells[8].Value.ToString() + "', '08', 'Y', GetDate(),'0.00', '" + NSGrid.Rows[i].Cells[11].Value.ToString() + "')";
                    }

                    _dbManInsert.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManInsert.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManInsert.ResultsTableName = "ShiftBoss";
                    _dbManInsert.ExecuteInstruction();



                }


                ///////////////////////////BMCS_ShiftBoss_Transfer Table////////////////////////////////////////////////////
                decimal DSTotal = 0;
                decimal NSTotal = 0;

                for (int i = 0; i <= DSGrid.Rows.Count - 1; i++)
                {
                    DSTotal = DSTotal + Convert.ToDecimal(DSGrid.Rows[i].Cells[11].Value);
                }
                for (int i = 0; i <= NSGrid.Rows.Count - 1; i++)
                {
                    NSTotal = NSTotal + Convert.ToDecimal(NSGrid.Rows[i].Cells[11].Value);
                }

                decimal TotalPayment = Math.Round(DSTotal, 2) + Math.Round(NSTotal, 2);
                decimal RandPerSqm = Math.Round(TotalPayment / (Mined + Convert.ToDecimal(0.0001)), 2);


                //MWDataManager.clsDataAccess _dbManTransDel = new MWDataManager.clsDataAccess();
                //_dbManTransDel.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                //_dbManTransDel.SqlStatement = " delete from BMCS_ShiftBoss_Transfer where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and Sectionid = '" + lblOrgunit.Text + "' ";

                //_dbManTransDel.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                //_dbManTransDel.queryReturnType = MWDataManager.ReturnType.DataTable;
                //_dbManTransDel.ResultsTableName = "ShiftBoss";
                //_dbManTransDel.ExecuteInstruction();

                //MWDataManager.clsDataAccess _dbManTrans = new MWDataManager.clsDataAccess();
                //_dbManTrans.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                //_dbManTrans.SqlStatement = "insert into BMCS_ShiftBoss_Transfer values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "','" + lblOrgunit.Text + "', '" + Act + "','" + Math.Round(NoDecimalPercRate, 2) + "','" + Mined + "', '" + Call + "',";
                //_dbManTrans.SqlStatement = _dbManTrans.SqlStatement + " '"+SafetyAchieved+"','"+EngAchieved+"','"+TonsAchieved+"','"+SweepsAchieved+"','"+BaseRate+"','"+FiftyPercRule+"', '"+DSTotal+"', '"+NSTotal+"' ";
                //_dbManTrans.SqlStatement = _dbManTrans.SqlStatement + " ,'"+TotalPayment+"', '"+RandPerSqm+"') ";
                //_dbManTrans.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                //_dbManTrans.queryReturnType = MWDataManager.ReturnType.DataTable;
                //_dbManTrans.ResultsTableName = "ShiftBoss";
                //_dbManTrans.ExecuteInstruction();

                ////////////////////////////////////////////////////////////////////////////////////////////////////////

                ProdMonth1Txt_TextChanged(null, null);

                MessageBox.Show("Bonus Details was successfully transferred to the ARMS Interface", "Transferred", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void editProdmonth_EditValueChanged(object sender, EventArgs e)
        {
            ProdMonth1Txt_TextChanged(null, null);
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OnCloseTabRequest(new CloseTabArg(tabCaption));
        }
    }
}
