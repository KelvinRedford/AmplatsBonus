using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionAmplatsGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.ProductionAmplatsBonus
{
    public partial class ucEngineeringParameters : BaseUserControl
    {
        public ucEngineeringParameters()
        {
            InitializeComponent();
            FormRibbonPages.Add(rpSafetyCapture);
            FormActiveRibbonPage = rpSafetyCapture;
            FormMainRibbonPage = rpSafetyCapture;
            RibbonControl = rcSafetyCapture;
        }

        private void navBarItem9_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            panel13.Visible = true;
            BSPnl.Visible = false;
            //BSPnl.Dock = DockStyle.Fill;
            PeramPnl.Visible = true;
            PeramPnl.Dock = DockStyle.Fill;

            ///New

            //LoadDailyColumns();
            // FillData();

            txtYear.Value = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
            txtMonth.Value = Convert.ToInt32(DateTime.Now.ToString("MM"));

            txtYear2.Value = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
            txtMonth2.Value = Convert.ToInt32(DateTime.Now.ToString("MM"));

            //CalLbl.Text = gvCalendarType.Rows[0].Cells[0].Value.ToString();

            // LoadTheCalendar();
            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan1.SqlStatement = " select * from mineware.dbo.[tbl_BCS_DesignationFact] ";

            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();


            DataTable dt = _dbMan1.ResultsDataTable;


            DataSet ds1 = new DataSet();

            if (ds1.Tables.Count > 0)
                ds1.Tables.Clear();

            ds1.Tables.Add(dt);

            OccGrd.Visible = true;

            OccGrd.DataSource = ds1.Tables[0];

            OccCol.FieldName = "Occupation";
            FactCol.FieldName = "Factor";
            FactShaftCol.FieldName = "FactorShaft";
            FactPlantCol.FieldName = "FactorPlant";



            LoadEngFactors();
            ESFA();
            ESFB();
        }

        private void ESFA()
        {
            int DaysInMonth = (System.DateTime.DaysInMonth(Convert.ToInt32(txtYear.Value), Convert.ToInt32(txtMonth.Value)));
            int month = Convert.ToDateTime(editProdmonth.EditValue).Month;    
            int year = Convert.ToDateTime(editProdmonth.EditValue).Year;

            MWDataManager.clsDataAccess _dbMan1ESfa = new MWDataManager.clsDataAccess();
            _dbMan1ESfa.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan1ESfa.SqlStatement = " Select 'ESF A' calendartypeid,calendardate,'Y' WORKINGDAY   from (  \r\n" +
                                   " select distinct(calendardate) from [mineware].[dbo].tbl_bcs_caltype  \r\n" +
                                   " where year(calendardate) = '" + year + "' and month(calendardate) = '" + month + "' and CALENDARDATE < GETDATE() -1 \r\n" +
                                  "  and calendardate not in (SELECT CALENDARDATE  \r\n" +

                                  "   FROM [Mineware].[dbo].[tbl_bcs_caltype]  \r\n" +
                                  "  where calendarcode = 'ESF A' and  year(calendardate) = '" + year + "' and month(calendardate) = '" + month + "' and CALENDARDATE < GETDATE() -1 )  \r\n" +
                                  "  ) a order by calendardate";

            _dbMan1ESfa.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1ESfa.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1ESfa.ExecuteInstruction();

            DataTable dt = _dbMan1ESfa.ResultsDataTable;

            if (dt.Rows.Count > 0)
            {
                MWDataManager.clsDataAccess _dbMan1ESfaInsert = new MWDataManager.clsDataAccess();
                _dbMan1ESfaInsert.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                _dbMan1ESfaInsert.SqlStatement = " select 'A' \r\n";
                foreach (DataRow r in dt.Rows)
                {
                    _dbMan1ESfaInsert.SqlStatement = _dbMan1ESfaInsert.SqlStatement + " insert into [Mineware].[dbo].[tbl_bcs_caltype] (calendarcode,calendardate,WORKINGDAY) \r\n" +
                                            "values('" + r["calendartypeid"].ToString() + "','" + r["calendardate"].ToString() + "' ,'Y')    ";
                }

                _dbMan1ESfaInsert.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1ESfaInsert.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1ESfaInsert.ExecuteInstruction();

            }

            //int daycount = Convert.ToInt32(dt.Rows[0][0]);

            //if (DaysInMonth != daycount)
            //{
            //    MessageBox.Show("Calendar Wrong");
            //}

        }

        private void ESFB()
        {
            int DaysInMonth = (System.DateTime.DaysInMonth(Convert.ToInt32(txtYear.Value), Convert.ToInt32(txtMonth.Value)));
            int month = Convert.ToDateTime(editProdmonth.EditValue).Month;
            int year = Convert.ToDateTime(editProdmonth.EditValue).Year;

            MWDataManager.clsDataAccess _dbMan1ESfb = new MWDataManager.clsDataAccess();
            _dbMan1ESfb.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            _dbMan1ESfb.SqlStatement = " Select 'ESF B' calendartypeid,calendardate,'Y' WORKINGDAY   from (  \r\n" +

                                   " select distinct(calendardate) from [mineware].[dbo].tbl_bcs_caltype  \r\n" +
                                   " where year(calendardate) = '" + year + "' and month(calendardate) = '" + month + "' and CALENDARDATE < GETDATE() -1 \r\n" +
                                  "  and calendardate not in (SELECT CALENDARDATE  \r\n" +

                                  "   FROM [Mineware].[dbo].[tbl_bcs_caltype]  \r\n" +
                                  "  where calendarcode = 'ESF B' and  year(calendardate) = '" + year + "' and month(calendardate) = '" + month + "' and CALENDARDATE < GETDATE() -1 )  \r\n" +
                                  "  ) a order by calendardate";

            _dbMan1ESfb.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1ESfb.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1ESfb.ExecuteInstruction();


            DataTable dt = _dbMan1ESfb.ResultsDataTable;

            if (dt.Rows.Count > 0)
            {
                MWDataManager.clsDataAccess _dbMan1ESfbInsert = new MWDataManager.clsDataAccess();
                _dbMan1ESfbInsert.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                _dbMan1ESfbInsert.SqlStatement = " select 'A' \r\n";
                foreach (DataRow r in dt.Rows)
                {
                    _dbMan1ESfbInsert.SqlStatement = _dbMan1ESfbInsert.SqlStatement + " insert into [Mineware].[dbo].[tbl_bcs_caltype] (calendarcode,calendardate,WORKINGDAY) \r\n" +
                                            "values('" + r["calendartypeid"].ToString() + "','" + r["calendardate"].ToString() + "' ,'Y')    ";
                }

                _dbMan1ESfbInsert.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1ESfbInsert.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1ESfbInsert.ExecuteInstruction();

            }
        }

        void LoadEngFactors()
        {







            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            _dbMan.SqlStatement = " select * from mineware.dbo.tbl_BCS_Eng_FactorNew1  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'";

            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            TonsHoistCallTxt.Text = "0";
            TonsHoistActTxt.Text = "0";
            S1CallTxt.Text = "0";
            S1ActTxt.Text = "0";
            S2CallTxt.Text = "0";
            S2ActTxt.Text = "0";
            ConcCallTxt.Text = "0";
            ConcActTxt.Text = "0";
            BMRCallTxt.Text = "0";
            BMRActTxt.Text = "0";
            SmeltCallTxt.Text = "0";
            SmeltactTxt.Text = "0";
            SmeltTonsCallTxt.Text = "0";
            SmeltTonsactTxt.Text = "0";


            EngProdTxt.Text = "1.00";
            WShopTxt2.Text = "1.00";
            ShaftTxt.Text = "1.00";
            BMRTxt4.Text = "1.00";
            SmelterTxt5.Text = "1.00";
            ConcTxt.Text = "1.00";
            ChiefTechTxt.Text = "1.00";
            TSDTxt.Text = "1.00";

            DataTable dt = _dbMan.ResultsDataTable;

            if (dt.Rows.Count > 0)
            {
                TonsHoistCallTxt.Text = dt.Rows[0]["TonsCall"].ToString();
                TonsHoistActTxt.Text = dt.Rows[0]["TonsAct"].ToString();



                S1CallTxt.Text = dt.Rows[0]["S1TonsCall"].ToString();
                S1ActTxt.Text = dt.Rows[0]["S1TonsAct"].ToString();

                S2CallTxt.Text = dt.Rows[0]["S2TonsCall"].ToString();
                S2ActTxt.Text = dt.Rows[0]["S2TonsAct"].ToString();

                ConcCallTxt.Text = dt.Rows[0]["concTonsCall"].ToString();
                ConcActTxt.Text = dt.Rows[0]["concTonsAct"].ToString();

                BMRCallTxt.Text = dt.Rows[0]["BMRCall"].ToString();
                BMRActTxt.Text = dt.Rows[0]["BMRAct"].ToString();

                SmeltCallTxt.Text = dt.Rows[0]["SmeltCall"].ToString();
                SmeltactTxt.Text = dt.Rows[0]["smeltAct"].ToString();

                SmeltTonsCallTxt.Text = dt.Rows[0]["SmeltTonsCall"].ToString();
                SmeltTonsactTxt.Text = dt.Rows[0]["SmeltTonsAct"].ToString();



                EngProdTxt.Text = dt.Rows[0]["ProdDept"].ToString();
                WShopTxt2.Text = dt.Rows[0]["Wshop"].ToString();
                ShaftTxt.Text = dt.Rows[0]["Shaft"].ToString();
                BMRTxt4.Text = dt.Rows[0]["BMR"].ToString();
                SmelterTxt5.Text = dt.Rows[0]["Smelter"].ToString();
                ConcTxt.Text = dt.Rows[0]["Conc"].ToString();
                ChiefTechTxt.Text = dt.Rows[0]["ChiefTech"].ToString();
                TSDTxt.Text = dt.Rows[0]["TSD"].ToString();
                NoAwopsTxt.Text = dt.Rows[0]["NoAwop"].ToString();
                OneAwopsTxt.Text = dt.Rows[0]["Awop1Shift"].ToString();
                TwoAwopsTxt.Text = dt.Rows[0]["Awop2Shift"].ToString();
                ThreeAwopsTxt.Text = dt.Rows[0]["Awop3Shift"].ToString();
            }


        }

        private void ucEngineeringParameters_Load(object sender, EventArgs e)
        {
            editProdmonth.EditValue = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsDate(ProductionAmplatsGlobalTSysSettings._currentProductionMonth.ToString());
        }

        private void SaveEngBtn_Click(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " delete from [mineware].dbo.tbl_BCS_Eng_FactorNew1  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'";


            _dbMan.SqlStatement = _dbMan.SqlStatement + " delete from [Mineware].[dbo].tbl_BCS_CalShifts where yearmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and calendarcode = 'Eng Total'";

            _dbMan.SqlStatement = _dbMan.SqlStatement + " insert into [Mineware].[dbo].tbl_BCS_CalShifts values ('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' , 'Eng Total', ";
            _dbMan.SqlStatement = _dbMan.SqlStatement + " '" + String.Format("{0:yyyy-MM-dd}", FromDate.Value) + "', '" + String.Format("{0:yyyy-MM-dd}", ToDate.Value) + "', ";
            _dbMan.SqlStatement = _dbMan.SqlStatement + " '" + ESFATxt.Text + "', '" + ESFBTxt.Text + "') ";


            _dbMan.SqlStatement = _dbMan.SqlStatement + " delete from [Mineware].[dbo].tbl_BCS_CalShifts where yearmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and calendarcode = 'Eng Plant Total'";

            _dbMan.SqlStatement = _dbMan.SqlStatement + " insert into [Mineware].[dbo].tbl_BCS_CalShifts values ('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' , 'Eng Plant Total', ";
            _dbMan.SqlStatement = _dbMan.SqlStatement + " '" + String.Format("{0:yyyy-MM-dd}", FromDate.Value) + "', '" + String.Format("{0:yyyy-MM-dd}", ToDate.Value) + "', ";
            _dbMan.SqlStatement = _dbMan.SqlStatement + " '" + ESFPlantATxt.Text + "', '" + ESFPlantBTxt.Text + "') ";


            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();


            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan1.SqlStatement = " insert into [mineware].dbo.tbl_BCS_Eng_FactorNew1 Values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + TonsHoistCallTxt.Text.ToString() + "', '" + TonsHoistActTxt.Text.ToString() + "', \r\n" +
                                   " '" + S1CallTxt.Text + "', '" + S1ActTxt.Text + "', '" + S2CallTxt.Text + "', '" + S2ActTxt.Text + "', \r\n" +
                                    " '" + ConcCallTxt.Text + "', '" + ConcActTxt.Text + "', '" + BMRCallTxt.Text + "', '" + BMRActTxt.Text + "', \r\n" +
                                    " '" + SmeltCallTxt.Text + "', '" + SmeltactTxt.Text + "', \r\n" +

                                   " '" + EngProdTxt.Text.ToString() + "', '" + WShopTxt2.Text.ToString() + "', '" + ShaftTxt.Text.ToString() + "', '" + BMRTxt4.Text.ToString() + "', \r\n" +
                                   " '" + SmelterTxt5.Text.ToString() + "', '" + ConcTxt.Text.ToString() + "', '" + ChiefTechTxt.Text.ToString() + "', '" + TSDTxt.Text.ToString() + "', \r\n" +
                                   " '" + NoAwopsTxt.Text.ToString() + "', '" + OneAwopsTxt.Text.ToString() + "', '" + TwoAwopsTxt.Text.ToString() + "', '" + ThreeAwopsTxt.Text.ToString() + "' ,\r\n" +
                                   " '" + SmeltTonsCallTxt.Text + "','" + SmeltTonsactTxt.Text + "') ";
            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();


            MWDataManager.clsDataAccess _dbManNS = new MWDataManager.clsDataAccess();
            _dbManNS.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManNS.SqlStatement = _dbManNS.SqlStatement + "  ";

            _dbManNS.SqlStatement = _dbManNS.SqlStatement + " ";

            for (int k = 0; k <= bandedGridView2.RowCount - 1; k++)
            {
                if (bandedGridView2.GetRowCellValue(k, bandedGridView2.Columns[0]) != null)
                {
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " ";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " update [mineware].dbo.tbl_BMCS_DesignationFact set factor = '" + bandedGridView2.GetRowCellValue(k, bandedGridView2.Columns[1]) + "', factorShaft  = '" + bandedGridView2.GetRowCellValue(k, bandedGridView2.Columns[2]) + "',   factorplant  = '" + bandedGridView2.GetRowCellValue(k, bandedGridView2.Columns[3]) + "' \r\n";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " where occupation = '" + bandedGridView2.GetRowCellValue(k, bandedGridView2.Columns[0]) + "' \r\n ";
                }
            }
            _dbManNS.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManNS.queryReturnType = MWDataManager.ReturnType.longNumber;
            _dbManNS.ExecuteInstruction();


            MWDataManager.clsDataAccess _dbManRel = new MWDataManager.clsDataAccess();
            _dbManRel.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManRel.SqlStatement = " exec [mineware].dbo.[sp_BMCS_GetRelieving] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' , '" + String.Format("{0:yyyy-MM-dd}", FromDate.Value) + "', '" + String.Format("{0:yyyy-MM-dd}", ToDate.Value) + "'";

            _dbManRel.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManRel.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManRel.ExecuteInstruction();


            MessageBox.Show("Factors were successfully saved", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadEngFactors();
        }

        private void navBarItem10_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

            BSPnl.Visible = true;
            BSPnl.Dock = DockStyle.Fill;
            panel13.Visible = true;
            PeramPnl.Visible = false;
            //panel13.Dock = DockStyle.Fill;

            LoadBaseGrid();
        }

        void LoadBaseGrid()
        {
            ////Load Headers Grid/////

            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan1.SqlStatement = " select * from mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  order by cat ";
            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();

            DataTable dt = _dbMan1.ResultsDataTable;

            if (dt.Rows.Count > 0)
            {

                Cat1.Text = dt.Rows[0]["Perc"].ToString();
                Cat2.Text = dt.Rows[1]["Perc"].ToString();
                Cat3.Text = dt.Rows[2]["Perc"].ToString();
                Cat4.Text = dt.Rows[3]["Perc"].ToString();
                Cat5.Text = dt.Rows[4]["Perc"].ToString();
                Cat6.Text = dt.Rows[5]["Perc"].ToString();
                Cat7.Text = dt.Rows[6]["Perc"].ToString();
                Cat8.Text = dt.Rows[7]["Perc"].ToString();
                Cat9.Text = dt.Rows[8]["Perc"].ToString();
                Cat10.Text = dt.Rows[9]["Perc"].ToString();
                Cat11.Text = dt.Rows[10]["Perc"].ToString();
                Cat12.Text = dt.Rows[11]["Perc"].ToString();
                Cat13.Text = dt.Rows[12]["Perc"].ToString();
                Cat14.Text = dt.Rows[13]["Perc"].ToString();
                Cat15.Text = dt.Rows[14]["Perc"].ToString();
                Cat16.Text = dt.Rows[15]["Perc"].ToString();
                Cat17.Text = dt.Rows[16]["Perc"].ToString();
                Cat18.Text = dt.Rows[17]["Perc"].ToString();
                Cat19.Text = dt.Rows[18]["Perc"].ToString();
                Cat20.Text = dt.Rows[19]["Perc"].ToString();

                Head1.Caption = Cat1.Text + "%";
                Head2.Caption = Cat2.Text + "%";
                Head3.Caption = Cat3.Text + "%";
                Head4.Caption = Cat4.Text + "%";
                Head5.Caption = Cat5.Text + "%";
                Head6.Caption = Cat6.Text + "%";
                Head7.Caption = Cat7.Text + "%";
                Head8.Caption = Cat8.Text + "%";
                Head9.Caption = Cat9.Text + "%";
                Head10.Caption = Cat10.Text + "%";
                Head11.Caption = Cat11.Text + "%";
                Head12.Caption = Cat12.Text + "%";
                Head13.Caption = Cat13.Text + "%";
                Head14.Caption = Cat14.Text + "%";
                Head15.Caption = Cat15.Text + "%";
                Head16.Caption = Cat16.Text + "%";
                Head17.Caption = Cat17.Text + "%";
                Head18.Caption = Cat18.Text + "%";
                Head19.Caption = Cat19.Text + "%";
                Head20.Caption = Cat20.Text + "%";
            }


            /////Load Data/////

            MWDataManager.clsDataAccess _dbMan3Mnth = new MWDataManager.clsDataAccess();
            _dbMan3Mnth.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan3Mnth.SqlStatement = " select * from mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveDetail where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' order by  Tons ";

            _dbMan3Mnth.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan3Mnth.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan3Mnth.ExecuteInstruction();

            DataTable dt1 = _dbMan3Mnth.ResultsDataTable;

            DataSet ds = new DataSet();

            if (ds.Tables.Count > 0)
                ds.Tables.Clear();

            ds.Tables.Add(dt1);

            EngGrid.Visible = true;

            EngGrid.DataSource = ds.Tables[0];


            if (dt1.Rows.Count > 0)
            {

                bandedGridTons.FieldName = "Tons";
                Head1.FieldName = "Cat1";
                Head2.FieldName = "Cat2";
                Head3.FieldName = "Cat3";
                Head4.FieldName = "Cat4";
                Head5.FieldName = "Cat5";
                Head6.FieldName = "Cat6";
                Head7.FieldName = "Cat7";
                Head8.FieldName = "Cat8";
                Head9.FieldName = "Cat9";
                Head10.FieldName = "Cat10";
                Head11.FieldName = "Cat11";
                Head12.FieldName = "Cat12";
                Head13.FieldName = "Cat13";
                Head14.FieldName = "Cat14";
                Head15.FieldName = "Cat15";
                Head16.FieldName = "Cat16";
                Head17.FieldName = "Cat17";
                Head18.FieldName = "Cat18";
                Head19.FieldName = "Cat19";
                Head20.FieldName = "Cat20";

                Col1Shaft.FieldName = "OneShaft";
                Col2Shaft.FieldName = "TwoShaft";
                ColCon.FieldName = "ConcTons";
                ColSmelt.FieldName = "Smelter";
                ColBMR.FieldName = "BMRKG";

            }
        }

        private void AddRowEngBtn_Click(object sender, EventArgs e)
        {
            SaveHeaders();
            SaveData();

            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan1.SqlStatement = " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveDetail Values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', 0, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null) \r\n" +
                                   "  \r\n" +
                                   "  \r\n" +
                                   "  \r\n" +
                                   "  \r\n" +
                                   "  \r\n" +
                                   "  \r\n";

            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();

            LoadBaseGrid();
        }

        private void EngTransferBtn_Click(object sender, EventArgs e)
        {

            SaveHeaders();
            SaveData();
        }


        void SaveData()
        {
            MWDataManager.clsDataAccess _dbManNS = new MWDataManager.clsDataAccess();
            _dbManNS.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManNS.SqlStatement = _dbManNS.SqlStatement + "  ";

            _dbManNS.SqlStatement = _dbManNS.SqlStatement + " delete from mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveDetail where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";
            for (int k = 0; k <= bandedGridView1.RowCount - 1; k++)
            {
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " delete from mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveDetail where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons = '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[0]) + "' ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveDetail Values(  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[0]) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[21]) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[22]) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[23]) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[25]) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[24]) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[1]) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[2]) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[3]) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[4]) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[5]) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[6]) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[7]) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[8]) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[9]) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[10]) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[11]) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[12]) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[13]) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[14]) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[15]) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[16]) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[17]) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[18]) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[19]) + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[20]) + "')  ";



            }
            _dbManNS.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManNS.queryReturnType = MWDataManager.ReturnType.longNumber;
            _dbManNS.ExecuteInstruction();

            MessageBox.Show("Factors were successfully saved", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadBaseGrid();
        }


        void SaveHeaders()
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " delete from mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";

            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = "";
            _dbMan1.SqlStatement = " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + CatLbl1.Text.ToString() + "', '" + Cat1.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + CatLbl2.Text.ToString() + "', '" + Cat2.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + CatLbl3.Text.ToString() + "', '" + Cat3.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + CatLbl4.Text.ToString() + "', '" + Cat4.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + CatLbl5.Text.ToString() + "', '" + Cat5.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + CatLbl6.Text.ToString() + "', '" + Cat6.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + CatLbl7.Text.ToString() + "', '" + Cat7.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + CatLbl8.Text.ToString() + "', '" + Cat8.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + CatLbl9.Text.ToString() + "', '" + Cat9.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + CatLbl10.Text.ToString() + "', '" + Cat10.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + CatLbl11.Text.ToString() + "', '" + Cat11.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + CatLbl12.Text.ToString() + "', '" + Cat12.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + CatLbl13.Text.ToString() + "', '" + Cat13.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + CatLbl14.Text.ToString() + "', '" + Cat14.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + CatLbl15.Text.ToString() + "', '" + Cat15.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + CatLbl16.Text.ToString() + "', '" + Cat16.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + CatLbl17.Text.ToString() + "', '" + Cat17.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + CatLbl18.Text.ToString() + "', '" + Cat18.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + CatLbl19.Text.ToString() + "', '" + Cat19.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + CatLbl20.Text.ToString() + "', '" + Cat20.Text.ToString() + "' ) ";
            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();


        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OnCloseTabRequest(new CloseTabArg(tabCaption));
        }

        private void editProdmonth_EditValueChanged(object sender, EventArgs e)
        {
            panel13.Visible = true;
            BSPnl.Visible = false;
            //BSPnl.Dock = DockStyle.Fill;
            PeramPnl.Visible = true;
            PeramPnl.Dock = DockStyle.Fill;

            ///New

            //LoadDailyColumns();
            // FillData();

            txtYear.Value = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
            txtMonth.Value = Convert.ToInt32(DateTime.Now.ToString("MM"));

            txtYear2.Value = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
            txtMonth2.Value = Convert.ToInt32(DateTime.Now.ToString("MM"));

            //CalLbl.Text = gvCalendarType.Rows[0].Cells[0].Value.ToString();

            // LoadTheCalendar();
            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan1.SqlStatement = " select * from mineware.dbo.[tbl_BCS_DesignationFact] ";

            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();


            DataTable dt = _dbMan1.ResultsDataTable;


            DataSet ds1 = new DataSet();

            if (ds1.Tables.Count > 0)
                ds1.Tables.Clear();

            ds1.Tables.Add(dt);

            OccGrd.Visible = true;

            OccGrd.DataSource = ds1.Tables[0];

            OccCol.FieldName = "Occupation";
            FactCol.FieldName = "Factor";
            FactShaftCol.FieldName = "FactorShaft";
            FactPlantCol.FieldName = "FactorPlant";



            LoadEngFactors();
            ESFA();
            ESFB();
        }
    }
}
