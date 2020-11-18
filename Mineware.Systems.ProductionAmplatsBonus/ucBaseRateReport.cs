﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastReport;
using System.Configuration;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionAmplatsGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.ProductionAmplatsBonus
{
    public partial class ucBaseRateReport : BaseUserControl
    {
        public ucBaseRateReport()
        {
            InitializeComponent();
        }

        Report theReport = new Report();

                //Call
        double OneTonsCall = 0, TwoTonsCall = 0, ThreeTonsCall = 0, FourTonsCall = 0, FiveTonsCall = 0,
                SixTonsCall = 0, SevenTonsCall = 0, EightTonsCall = 0, NineTonsCall = 0, TenTonsCall = 0, ElevenTonsCall = 0,
                TwelveTonsCall = 0, ThirteenTonsCall = 0, FourteenTonsCall = 0, FifteenTonsCall = 0, SixteenTonsCall = 0,

                //Actual
                OneTonsAct = 0, TwoTonsAct = 0, ThreeTonsAct = 0, FourTonsAct = 0, FiveTonsAct = 0,
                SixTonsAct = 0, SevenTonsAct = 0, EightTonsAct = 0, NineTonsAct = 0, TenTonsAct = 0, ElevenTonsAct = 0,
                TwelveTonsAct = 0, ThirteenTonsAct = 0, FourteenTonsAct = 0, FifteenTonsAct = 0, SixteenTonsAct = 0,

                //Percentage
                OneTonsPerc = 0, TwoTonsPerc = 0, ThreeTonsPerc = 0, FourTonsPerc = 0, FiveTonsPerc = 0,
                SixTonsPerc = 0, SevenTonsPerc = 0, EightTonsPerc = 0, NineTonsPerc = 0, TenTonsPerc = 0, ElevenTonsPerc = 0,
                TwelveTonsPerc = 0, ThirteenTonsPerc = 0, FourteenTonsPerc = 0, FifteenTonsPerc = 0, SixteenTonsPerc = 0,

                //Base
                OneBase = 0, TwoBase = 0, ThreeBase = 0, FourBase = 0, FiveBase = 0,
                SixBase = 0, SevenBase = 0, EightBase = 0, NineBase = 0, TenBase = 0, ElevenBase = 0,
                TwelveBase = 0, ThirteenBase = 0, FourteenBase = 0, FifteenBase = 0, SixteenBase = 0,

                //Minimun Tons
            //Actual
                OneTonsAct1 = 0, TwoTonsAct1 = 0, ThreeTonsAct1 = 0, FourTonsAct1 = 0, FiveTonsAct1 = 0,
                SixTonsAct1 = 0, SevenTonsAct1 = 0, EightTonsAct1 = 0, NineTonsAct1 = 0, TenTonsAct1 = 0, ElevenTonsAct1 = 0,
                TwelveTonsAct1 = 0, ThirteenTonsAct1 = 0, FourteenTonsAct1 = 0, FifteenTonsAct1 = 0, SixteenTonsAct1 = 0;

        private void btnShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.Text == "Base Rate Calculation Report")
            {


                //First set min tons Variable
                double MinTons = 0.0;

                MWDataManager.clsDataAccess _dbManMin = new MWDataManager.clsDataAccess();
                _dbManMin.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManMin.SqlStatement = " select min(BIPTons) BIPTons from BMCS_Eng_Level " +
                                         " Where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";
                _dbManMin.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManMin.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManMin.ExecuteInstruction();

                DataTable dtMin = _dbManMin.ResultsDataTable;

                foreach (DataRow drMin in dtMin.Rows)
                {
                    //Set the Min tons to use in the query
                    MinTons = Convert.ToDouble(drMin["BIPTons"].ToString());
                }


                //Now set factors
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " select * from BMCS_General_Factors " +
                                      " Where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "GeneralFactors";
                _dbMan.ExecuteInstruction();

                DataTable dt = _dbMan.ResultsDataTable;

                foreach (DataRow dr in dt.Rows)
                {
                    OneTonsCall = Convert.ToDouble(dr["OneCall"].ToString());
                    TwoTonsCall = Convert.ToDouble(dr["TwoCall"].ToString());
                    ThreeTonsCall = Convert.ToDouble(dr["ThreeCall"].ToString());
                    FourTonsCall = Convert.ToDouble(dr["FourCall"].ToString());
                    FiveTonsCall = Convert.ToDouble(dr["FiveCall"].ToString());
                    SixTonsCall = Convert.ToDouble(dr["SixCall"].ToString());
                    SevenTonsCall = Convert.ToDouble(dr["SevenCall"].ToString());
                    EightTonsCall = Convert.ToDouble(dr["EightCall"].ToString());
                    NineTonsCall = Convert.ToDouble(dr["NineCall"].ToString());
                    TenTonsCall = Convert.ToDouble(dr["TenCall"].ToString());
                    ElevenTonsCall = Convert.ToDouble(dr["ElevenCall"].ToString());
                    TwelveTonsCall = Convert.ToDouble(dr["TwelveCall"].ToString());
                    ThirteenTonsCall = Convert.ToDouble(dr["ThirteenCall"].ToString());
                    FourteenTonsCall = Convert.ToDouble(dr["FourteenCall"].ToString());
                    FifteenTonsCall = Convert.ToDouble(dr["FifteenCall"].ToString());
                    SixteenTonsCall = Convert.ToDouble(dr["SixteenCall"].ToString());

                    OneTonsAct = Convert.ToDouble(dr["OneAct"].ToString());
                    TwoTonsAct = Convert.ToDouble(dr["TwoAct"].ToString());
                    ThreeTonsAct = Convert.ToDouble(dr["ThreeAct"].ToString());
                    FourTonsAct = Convert.ToDouble(dr["FourAct"].ToString());
                    FiveTonsAct = Convert.ToDouble(dr["FiveAct"].ToString());
                    SixTonsAct = Convert.ToDouble(dr["SixAct"].ToString());
                    SevenTonsAct = Convert.ToDouble(dr["SevenAct"].ToString());
                    EightTonsAct = Convert.ToDouble(dr["EightAct"].ToString());
                    NineTonsAct = Convert.ToDouble(dr["NineAct"].ToString());
                    TenTonsAct = Convert.ToDouble(dr["TenAct"].ToString());
                    ElevenTonsAct = Convert.ToDouble(dr["ElevenAct"].ToString());
                    TwelveTonsAct = Convert.ToDouble(dr["TwelveAct"].ToString());
                    ThirteenTonsAct = Convert.ToDouble(dr["ThirteenAct"].ToString());
                    FourteenTonsAct = Convert.ToDouble(dr["FourteenAct"].ToString());
                    FifteenTonsAct = Convert.ToDouble(dr["FifteenAct"].ToString());
                    SixteenTonsAct = Convert.ToDouble(dr["SixteenAct"].ToString());

                    OneTonsPerc = Math.Round((100 / OneTonsCall) * OneTonsAct, 2);
                    //OneTonsPerc = (OneTonsAct / OneTonsCall) * 100;
                    TwoTonsPerc = Math.Round((100 / TwoTonsCall) * TwoTonsAct, 2);
                    ThreeTonsPerc = Math.Round((100 / ThreeTonsCall) * ThreeTonsAct, 2);
                    FourTonsPerc = Math.Round((100 / FourTonsCall) * FourTonsAct, 2);
                    FiveTonsPerc = Math.Round((100 / FiveTonsCall) * FiveTonsAct, 2);
                    SixTonsPerc = Math.Round((100 / SixTonsCall) * SixTonsAct, 2);
                    SevenTonsPerc = Math.Round((100 / SevenTonsCall) * SevenTonsAct, 2);
                    EightTonsPerc = Math.Round((100 / EightTonsCall) * EightTonsAct, 2);
                    NineTonsPerc = Math.Round((100 / NineTonsCall) * NineTonsAct, 2);
                    TenTonsPerc = Math.Round((100 / TenTonsCall) * TenTonsAct, 2);
                    ElevenTonsPerc = Math.Round((100 / ElevenTonsCall) * ElevenTonsAct, 2);
                    TwelveTonsPerc = Math.Round((100 / TwelveTonsCall) * TwelveTonsAct, 2);
                    ThirteenTonsPerc = Math.Round((100 / ThirteenTonsCall) * ThirteenTonsAct, 2);
                    FourteenTonsPerc = Math.Round((100 / FourteenTonsCall) * FourteenTonsAct, 2);
                    FifteenTonsPerc = Math.Round((100 / FifteenTonsCall) * FifteenTonsAct, 2);
                    SixteenTonsPerc = Math.Round((100 / SixteenTonsCall) * SixteenTonsAct, 2);

                }

                //Set the TonsAct1 - check if < minimum then set to minimum else use the selected tons actual
                if (OneTonsAct < MinTons)
                    OneTonsAct1 = MinTons;
                else
                    OneTonsAct1 = OneTonsAct;

                if (TwoTonsAct < MinTons)
                    TwoTonsAct1 = MinTons;
                else
                    TwoTonsAct1 = TwoTonsAct;

                if (ThreeTonsAct < MinTons)
                    ThreeTonsAct1 = MinTons;
                else
                    ThreeTonsAct1 = ThreeTonsAct;

                if (FourTonsAct < MinTons)
                    FourTonsAct1 = MinTons;
                else
                    FourTonsAct1 = FourTonsAct;

                if (FiveTonsAct < MinTons)
                    FiveTonsAct1 = MinTons;
                else
                    FiveTonsAct1 = FiveTonsAct;

                if (SixTonsAct < MinTons)
                    SixTonsAct1 = MinTons;
                else
                    SixTonsAct1 = SixTonsAct;

                if (SevenTonsAct < MinTons)
                    SevenTonsAct1 = MinTons;
                else
                    SevenTonsAct1 = SevenTonsAct;

                if (EightTonsAct < MinTons)
                    EightTonsAct1 = MinTons;
                else
                    EightTonsAct1 = EightTonsAct;

                if (NineTonsAct < MinTons)
                    NineTonsAct1 = MinTons;
                else
                    NineTonsAct1 = NineTonsAct;

                if (TenTonsAct < MinTons)
                    TenTonsAct1 = MinTons;
                else
                    TenTonsAct1 = TenTonsAct;

                if (ElevenTonsAct < MinTons)
                    ElevenTonsAct1 = MinTons;
                else
                    ElevenTonsAct1 = ElevenTonsAct;

                if (TwelveTonsAct < MinTons)
                    TwelveTonsAct1 = MinTons;
                else
                    TwelveTonsAct1 = TwelveTonsAct;

                if (ThirteenTonsAct < MinTons)
                    ThirteenTonsAct1 = MinTons;
                else
                    ThirteenTonsAct1 = ThirteenTonsAct;

                if (FourteenTonsAct < MinTons)
                    FourteenTonsAct1 = MinTons;
                else
                    FourteenTonsAct1 = FourteenTonsAct;

                if (FifteenTonsAct < MinTons)
                    FifteenTonsAct1 = MinTons;
                else
                    FifteenTonsAct1 = FifteenTonsAct;

                if (SixteenTonsAct < MinTons)
                    SixteenTonsAct1 = MinTons;
                else
                    SixteenTonsAct1 = SixteenTonsAct;


                //One
                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = " select \r\n " +
                                       " case when " + OneTonsPerc + " < 50.0 then BIPPer1 \r\n " +
                                       " when " + OneTonsPerc + " >= 50.0 and " + OneTonsPerc + " < 55.0 then BIPPer2 \r\n " +
                                       " when " + OneTonsPerc + " >= 55.0 and " + OneTonsPerc + " < 60.0 then BIPPer3 \r\n " +
                                       " when " + OneTonsPerc + " >= 60.0 and " + OneTonsPerc + " < 65.0 then BIPPer4 \r\n " +
                                       " when " + OneTonsPerc + " >= 65.0 and " + OneTonsPerc + " < 70.0 then BIPPer5 \r\n " +
                                       " when " + OneTonsPerc + " >= 70.0 and " + OneTonsPerc + " < 75.0 then BIPPer6 \r\n " +
                                       " when " + OneTonsPerc + " >= 75.0 and " + OneTonsPerc + " < 80.0 then BIPPer7 \r\n " +
                                       " when " + OneTonsPerc + " >= 80.0 and " + OneTonsPerc + " < 85.0 then BIPPer8 \r\n " +
                                       " when " + OneTonsPerc + " >= 85.0 and " + OneTonsPerc + " < 90.0 then BIPPer9 \r\n " +
                                       " when " + OneTonsPerc + " >= 90.0 and " + OneTonsPerc + " < 95.0 then BIPPer10 \r\n " +
                                       " when " + OneTonsPerc + " >= 95.0 and " + OneTonsPerc + " < 100.0 then BIPPer11 \r\n " +
                                       " when " + OneTonsPerc + " >= 100.0 and " + OneTonsPerc + " < 105.0 then BIPPer12 \r\n " +
                                       " when " + OneTonsPerc + " >= 105.0 and " + OneTonsPerc + " < 110.0 then BIPPer13 \r\n " +
                                       " when " + OneTonsPerc + " >= 110.0 and " + OneTonsPerc + " < 115.0 then BIPPer14 \r\n " +
                                       " when " + OneTonsPerc + " >= 115.0 and " + OneTonsPerc + " < 120.0 then BIPPer15 \r\n " +
                                       " when " + OneTonsPerc + " >= 120.0 and " + OneTonsPerc + " < 125.0 then BIPPer16 \r\n " +
                                       " when " + OneTonsPerc + " >= 125.0 and " + OneTonsPerc + " < 200.0 then BIPPer17 \r\n " +
                                       " else 0 end as MyBase  \r\n " +
                                       " from BMCS_Eng_Level \r\n " +
                                       " where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n " +
                                       " and BIPTons = ( \r\n " +
                                       " Select top 1(BIPTons) from BMCS_Eng_Level \r\n " +
                                       " Where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and BIPTons <= '" + OneTonsAct1 + "'  \r\n " +
                                       " order by BIPTons Desc) ";
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ResultsTableName = "OneLevel";
                _dbMan1.ExecuteInstruction();

                DataTable dt1 = _dbMan1.ResultsDataTable;

                foreach (DataRow dr1 in dt1.Rows)
                {
                    OneBase = Convert.ToDouble(dr1["MyBase"].ToString());
                }


                //Two Level
                MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
                _dbMan2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan2.SqlStatement = " select \r\n " +
                                       " case when " + TwoTonsPerc + " < 50.0 then BIPPer1 \r\n " +
                                       " when " + TwoTonsPerc + " >= 50.0 and " + TwoTonsPerc + " < 55.0 then BIPPer2 \r\n " +
                                       " when " + TwoTonsPerc + " >= 55.0 and " + TwoTonsPerc + " < 60.0 then BIPPer3 \r\n " +
                                       " when " + TwoTonsPerc + " >= 60.0 and " + TwoTonsPerc + " < 65.0 then BIPPer4 \r\n " +
                                       " when " + TwoTonsPerc + " >= 65.0 and " + TwoTonsPerc + " < 70.0 then BIPPer5 \r\n " +
                                       " when " + TwoTonsPerc + " >= 70.0 and " + TwoTonsPerc + " < 75.0 then BIPPer6 \r\n " +
                                       " when " + TwoTonsPerc + " >= 75.0 and " + TwoTonsPerc + " < 80.0 then BIPPer7 \r\n " +
                                       " when " + TwoTonsPerc + " >= 80.0 and " + TwoTonsPerc + " < 85.0 then BIPPer8 \r\n " +
                                       " when " + TwoTonsPerc + " >= 85.0 and " + TwoTonsPerc + " < 90.0 then BIPPer9 \r\n " +
                                       " when " + TwoTonsPerc + " >= 90.0 and " + TwoTonsPerc + " < 95.0 then BIPPer10 \r\n " +
                                       " when " + TwoTonsPerc + " >= 95.0 and " + TwoTonsPerc + " < 100.0 then BIPPer11 \r\n " +
                                       " when " + TwoTonsPerc + " >= 100.0 and " + TwoTonsPerc + " < 105.0 then BIPPer12 \r\n " +
                                       " when " + TwoTonsPerc + " >= 105.0 and " + TwoTonsPerc + " < 110.0 then BIPPer13 \r\n " +
                                       " when " + TwoTonsPerc + " >= 110.0 and " + TwoTonsPerc + " < 115.0 then BIPPer14 \r\n " +
                                       " when " + TwoTonsPerc + " >= 115.0 and " + TwoTonsPerc + " < 120.0 then BIPPer15 \r\n " +
                                       " when " + TwoTonsPerc + " >= 120.0 and " + TwoTonsPerc + " < 125.0 then BIPPer16 \r\n " +
                                       " when " + TwoTonsPerc + " >= 125.0 and " + TwoTonsPerc + " < 200.0 then BIPPer17 \r\n " +
                                       " else 0 end as MyBase  \r\n " +
                                       " from BMCS_Eng_Level \r\n " +
                                       " where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n " +
                                       " and BIPTons = ( \r\n " +
                                       " Select top 1(BIPTons) from BMCS_Eng_Level \r\n " +
                                       " Where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and BIPTons <= '" + TwoTonsAct1 + "'  \r\n " +
                                       " order by BIPTons Desc) ";
                _dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan2.ResultsTableName = "TwoLevel";
                _dbMan2.ExecuteInstruction();

                DataTable dt2 = _dbMan2.ResultsDataTable;

                foreach (DataRow dr2 in dt2.Rows)
                {
                    TwoBase = Convert.ToDouble(dr2["MyBase"].ToString());
                }


                //Three Level
                MWDataManager.clsDataAccess _dbMan3 = new MWDataManager.clsDataAccess();
                _dbMan3.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan3.SqlStatement = " select \r\n " +
                                       " case when " + ThreeTonsPerc + " < 50.0 then BIPPer1 \r\n " +
                                       " when " + ThreeTonsPerc + " >= 50.0 and " + ThreeTonsPerc + " < 55.0 then BIPPer2 \r\n " +
                                       " when " + ThreeTonsPerc + " >= 55.0 and " + ThreeTonsPerc + " < 60.0 then BIPPer3 \r\n " +
                                       " when " + ThreeTonsPerc + " >= 60.0 and " + ThreeTonsPerc + " < 65.0 then BIPPer4 \r\n " +
                                       " when " + ThreeTonsPerc + " >= 65.0 and " + ThreeTonsPerc + " < 70.0 then BIPPer5 \r\n " +
                                       " when " + ThreeTonsPerc + " >= 70.0 and " + ThreeTonsPerc + " < 75.0 then BIPPer6 \r\n " +
                                       " when " + ThreeTonsPerc + " >= 75.0 and " + ThreeTonsPerc + " < 80.0 then BIPPer7 \r\n " +
                                       " when " + ThreeTonsPerc + " >= 80.0 and " + ThreeTonsPerc + " < 85.0 then BIPPer8 \r\n " +
                                       " when " + ThreeTonsPerc + " >= 85.0 and " + ThreeTonsPerc + " < 90.0 then BIPPer9 \r\n " +
                                       " when " + ThreeTonsPerc + " >= 90.0 and " + ThreeTonsPerc + " < 95.0 then BIPPer10 \r\n " +
                                       " when " + ThreeTonsPerc + " >= 95.0 and " + ThreeTonsPerc + " < 100.0 then BIPPer11 \r\n " +
                                       " when " + ThreeTonsPerc + " >= 100.0 and " + ThreeTonsPerc + " < 105.0 then BIPPer12 \r\n " +
                                       " when " + ThreeTonsPerc + " >= 105.0 and " + ThreeTonsPerc + " < 110.0 then BIPPer13 \r\n " +
                                       " when " + ThreeTonsPerc + " >= 110.0 and " + ThreeTonsPerc + " < 115.0 then BIPPer14 \r\n " +
                                       " when " + ThreeTonsPerc + " >= 115.0 and " + ThreeTonsPerc + " < 120.0 then BIPPer15 \r\n " +
                                       " when " + ThreeTonsPerc + " >= 120.0 and " + ThreeTonsPerc + " < 125.0 then BIPPer16 \r\n " +
                                       " when " + ThreeTonsPerc + " >= 125.0 and " + ThreeTonsPerc + " < 200.0 then BIPPer17 \r\n " +
                                       " else 0 end as MyBase  \r\n " +
                                       " from BMCS_Eng_Level \r\n " +
                                       " where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n " +
                                       " and BIPTons = ( \r\n " +
                                       " Select top 1(BIPTons) from BMCS_Eng_Level \r\n " +
                                       " Where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and BIPTons <= '" + ThreeTonsAct1 + "'  \r\n " +
                                       " order by BIPTons Desc) ";
                _dbMan3.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan3.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan3.ResultsTableName = "TwoLevel";
                _dbMan3.ExecuteInstruction();

                DataTable dt3 = _dbMan3.ResultsDataTable;

                foreach (DataRow dr3 in dt3.Rows)
                {
                    ThreeBase = Convert.ToDouble(dr3["MyBase"].ToString());
                }


                //Four Level
                MWDataManager.clsDataAccess _dbMan4 = new MWDataManager.clsDataAccess();
                _dbMan4.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan4.SqlStatement = " select \r\n " +
                                       " case when " + FourTonsPerc + " < 50.0 then BIPPer1 \r\n " +
                                       " when " + FourTonsPerc + " >= 50.0 and " + FourTonsPerc + " < 55.0 then BIPPer2 \r\n " +
                                       " when " + FourTonsPerc + " >= 55.0 and " + FourTonsPerc + " < 60.0 then BIPPer3 \r\n " +
                                       " when " + FourTonsPerc + " >= 60.0 and " + FourTonsPerc + " < 65.0 then BIPPer4 \r\n " +
                                       " when " + FourTonsPerc + " >= 65.0 and " + FourTonsPerc + " < 70.0 then BIPPer5 \r\n " +
                                       " when " + FourTonsPerc + " >= 70.0 and " + FourTonsPerc + " < 75.0 then BIPPer6 \r\n " +
                                       " when " + FourTonsPerc + " >= 75.0 and " + FourTonsPerc + " < 80.0 then BIPPer7 \r\n " +
                                       " when " + FourTonsPerc + " >= 80.0 and " + FourTonsPerc + " < 85.0 then BIPPer8 \r\n " +
                                       " when " + FourTonsPerc + " >= 85.0 and " + FourTonsPerc + " < 90.0 then BIPPer9 \r\n " +
                                       " when " + FourTonsPerc + " >= 90.0 and " + FourTonsPerc + " < 95.0 then BIPPer10 \r\n " +
                                       " when " + FourTonsPerc + " >= 95.0 and " + FourTonsPerc + " < 100.0 then BIPPer11 \r\n " +
                                       " when " + FourTonsPerc + " >= 100.0 and " + FourTonsPerc + " < 105.0 then BIPPer12 \r\n " +
                                       " when " + FourTonsPerc + " >= 105.0 and " + FourTonsPerc + " < 110.0 then BIPPer13 \r\n " +
                                       " when " + FourTonsPerc + " >= 110.0 and " + FourTonsPerc + " < 115.0 then BIPPer14 \r\n " +
                                       " when " + FourTonsPerc + " >= 115.0 and " + FourTonsPerc + " < 120.0 then BIPPer15 \r\n " +
                                       " when " + FourTonsPerc + " >= 120.0 and " + FourTonsPerc + " < 125.0 then BIPPer16 \r\n " +
                                       " when " + FourTonsPerc + " >= 125.0 and " + FourTonsPerc + " < 200.0 then BIPPer17 \r\n " +
                                       " else 0 end as MyBase  \r\n " +
                                       " from BMCS_Eng_Level \r\n " +
                                       " where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n " +
                                       " and BIPTons = ( \r\n " +
                                       " Select top 1(BIPTons) from BMCS_Eng_Level \r\n " +
                                       " Where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and BIPTons <= '" + FourTonsAct1 + "'  \r\n " +
                                       " order by BIPTons Desc) ";
                _dbMan4.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan4.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan4.ResultsTableName = "TwoLevel";
                _dbMan4.ExecuteInstruction();

                DataTable dt4 = _dbMan4.ResultsDataTable;

                foreach (DataRow dr4 in dt4.Rows)
                {
                    FourBase = Convert.ToDouble(dr4["MyBase"].ToString());
                }

                //Five Level
                MWDataManager.clsDataAccess _dbMan5 = new MWDataManager.clsDataAccess();
                _dbMan5.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan5.SqlStatement = " select \r\n " +
                                       " case when " + FiveTonsPerc + " < 50.0 then BIPPer1 \r\n " +
                                       " when " + FiveTonsPerc + " >= 50.0 and " + FiveTonsPerc + " < 55.0 then BIPPer2 \r\n " +
                                       " when " + FiveTonsPerc + " >= 55.0 and " + FiveTonsPerc + " < 60.0 then BIPPer3 \r\n " +
                                       " when " + FiveTonsPerc + " >= 60.0 and " + FiveTonsPerc + " < 65.0 then BIPPer4 \r\n " +
                                       " when " + FiveTonsPerc + " >= 65.0 and " + FiveTonsPerc + " < 70.0 then BIPPer5 \r\n " +
                                       " when " + FiveTonsPerc + " >= 70.0 and " + FiveTonsPerc + " < 75.0 then BIPPer6 \r\n " +
                                       " when " + FiveTonsPerc + " >= 75.0 and " + FiveTonsPerc + " < 80.0 then BIPPer7 \r\n " +
                                       " when " + FiveTonsPerc + " >= 80.0 and " + FiveTonsPerc + " < 85.0 then BIPPer8 \r\n " +
                                       " when " + FiveTonsPerc + " >= 85.0 and " + FiveTonsPerc + " < 90.0 then BIPPer9 \r\n " +
                                       " when " + FiveTonsPerc + " >= 90.0 and " + FiveTonsPerc + " < 95.0 then BIPPer10 \r\n " +
                                       " when " + FiveTonsPerc + " >= 95.0 and " + FiveTonsPerc + " < 100.0 then BIPPer11 \r\n " +
                                       " when " + FiveTonsPerc + " >= 100.0 and " + FiveTonsPerc + " < 105.0 then BIPPer12 \r\n " +
                                       " when " + FiveTonsPerc + " >= 105.0 and " + FiveTonsPerc + " < 110.0 then BIPPer13 \r\n " +
                                       " when " + FiveTonsPerc + " >= 110.0 and " + FiveTonsPerc + " < 115.0 then BIPPer14 \r\n " +
                                       " when " + FiveTonsPerc + " >= 115.0 and " + FiveTonsPerc + " < 120.0 then BIPPer15 \r\n " +
                                       " when " + FiveTonsPerc + " >= 120.0 and " + FiveTonsPerc + " < 125.0 then BIPPer16 \r\n " +
                                       " when " + FiveTonsPerc + " >= 125.0 and " + FiveTonsPerc + " < 200.0 then BIPPer17 \r\n " +
                                       " else 0 end as MyBase  \r\n " +
                                       " from BMCS_Eng_Level \r\n " +
                                       " where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n " +
                                       " and BIPTons = ( \r\n " +
                                       " Select top 1(BIPTons) from BMCS_Eng_Level \r\n " +
                                       " Where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and BIPTons <= '" + FiveTonsAct1 + "'  \r\n " +
                                       " order by BIPTons Desc) ";
                _dbMan5.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan5.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan5.ResultsTableName = "FiveLevel";
                _dbMan5.ExecuteInstruction();

                DataTable dt5 = _dbMan5.ResultsDataTable;

                foreach (DataRow dr5 in dt5.Rows)
                {
                    FiveBase = Convert.ToDouble(dr5["MyBase"].ToString());
                }

                //Six Level
                MWDataManager.clsDataAccess _dbMan6 = new MWDataManager.clsDataAccess();
                _dbMan6.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan6.SqlStatement = " select \r\n " +
                                       " case when " + SixTonsPerc + " < 50.0 then BIPPer1 \r\n " +
                                       " when " + SixTonsPerc + " >= 50.0 and " + SixTonsPerc + " < 55.0 then BIPPer2 \r\n " +
                                       " when " + SixTonsPerc + " >= 55.0 and " + SixTonsPerc + " < 60.0 then BIPPer3 \r\n " +
                                       " when " + SixTonsPerc + " >= 60.0 and " + SixTonsPerc + " < 65.0 then BIPPer4 \r\n " +
                                       " when " + SixTonsPerc + " >= 65.0 and " + SixTonsPerc + " < 70.0 then BIPPer5 \r\n " +
                                       " when " + SixTonsPerc + " >= 70.0 and " + SixTonsPerc + " < 75.0 then BIPPer6 \r\n " +
                                       " when " + SixTonsPerc + " >= 75.0 and " + SixTonsPerc + " < 80.0 then BIPPer7 \r\n " +
                                       " when " + SixTonsPerc + " >= 80.0 and " + SixTonsPerc + " < 85.0 then BIPPer8 \r\n " +
                                       " when " + SixTonsPerc + " >= 85.0 and " + SixTonsPerc + " < 90.0 then BIPPer9 \r\n " +
                                       " when " + SixTonsPerc + " >= 90.0 and " + SixTonsPerc + " < 95.0 then BIPPer10 \r\n " +
                                       " when " + SixTonsPerc + " >= 95.0 and " + SixTonsPerc + " < 100.0 then BIPPer11 \r\n " +
                                       " when " + SixTonsPerc + " >= 100.0 and " + SixTonsPerc + " < 105.0 then BIPPer12 \r\n " +
                                       " when " + SixTonsPerc + " >= 105.0 and " + SixTonsPerc + " < 110.0 then BIPPer13 \r\n " +
                                       " when " + SixTonsPerc + " >= 110.0 and " + SixTonsPerc + " < 115.0 then BIPPer14 \r\n " +
                                       " when " + SixTonsPerc + " >= 115.0 and " + SixTonsPerc + " < 120.0 then BIPPer15 \r\n " +
                                       " when " + SixTonsPerc + " >= 120.0 and " + SixTonsPerc + " < 125.0 then BIPPer16 \r\n " +
                                       " when " + SixTonsPerc + " >= 125.0 and " + SixTonsPerc + " < 200.0 then BIPPer17 \r\n " +
                                       " else 0 end as MyBase  \r\n " +
                                       " from BMCS_Eng_Level \r\n " +
                                       " where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n " +
                                       " and BIPTons = ( \r\n " +
                                       " Select top 1(BIPTons) from BMCS_Eng_Level \r\n " +
                                       " Where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and BIPTons <= '" + SixTonsAct1 + "'  \r\n " +
                                       " order by BIPTons Desc) ";
                _dbMan6.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan6.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan6.ResultsTableName = "SixLevel";
                _dbMan6.ExecuteInstruction();

                DataTable dt6 = _dbMan6.ResultsDataTable;

                foreach (DataRow dr6 in dt6.Rows)
                {
                    SixBase = Convert.ToDouble(dr6["MyBase"].ToString());
                }

                //Seven Level
                MWDataManager.clsDataAccess _dbMan7 = new MWDataManager.clsDataAccess();
                _dbMan7.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan7.SqlStatement = " select \r\n " +
                                       " case when " + SevenTonsPerc + " < 50.0 then BIPPer1 \r\n " +
                                       " when " + SevenTonsPerc + " >= 50.0 and " + SevenTonsPerc + " < 55.0 then BIPPer2 \r\n " +
                                       " when " + SevenTonsPerc + " >= 55.0 and " + SevenTonsPerc + " < 60.0 then BIPPer3 \r\n " +
                                       " when " + SevenTonsPerc + " >= 60.0 and " + SevenTonsPerc + " < 65.0 then BIPPer4 \r\n " +
                                       " when " + SevenTonsPerc + " >= 65.0 and " + SevenTonsPerc + " < 70.0 then BIPPer5 \r\n " +
                                       " when " + SevenTonsPerc + " >= 70.0 and " + SevenTonsPerc + " < 75.0 then BIPPer6 \r\n " +
                                       " when " + SevenTonsPerc + " >= 75.0 and " + SevenTonsPerc + " < 80.0 then BIPPer7 \r\n " +
                                       " when " + SevenTonsPerc + " >= 80.0 and " + SevenTonsPerc + " < 85.0 then BIPPer8 \r\n " +
                                       " when " + SevenTonsPerc + " >= 85.0 and " + SevenTonsPerc + " < 90.0 then BIPPer9 \r\n " +
                                       " when " + SevenTonsPerc + " >= 90.0 and " + SevenTonsPerc + " < 95.0 then BIPPer10 \r\n " +
                                       " when " + SevenTonsPerc + " >= 95.0 and " + SevenTonsPerc + " < 100.0 then BIPPer11 \r\n " +
                                       " when " + SevenTonsPerc + " >= 100.0 and " + SevenTonsPerc + " < 105.0 then BIPPer12 \r\n " +
                                       " when " + SevenTonsPerc + " >= 105.0 and " + SevenTonsPerc + " < 110.0 then BIPPer13 \r\n " +
                                       " when " + SevenTonsPerc + " >= 110.0 and " + SevenTonsPerc + " < 115.0 then BIPPer14 \r\n " +
                                       " when " + SevenTonsPerc + " >= 115.0 and " + SevenTonsPerc + " < 120.0 then BIPPer15 \r\n " +
                                       " when " + SevenTonsPerc + " >= 120.0 and " + SevenTonsPerc + " < 125.0 then BIPPer16 \r\n " +
                                       " when " + SevenTonsPerc + " >= 125.0 and " + SevenTonsPerc + " < 200.0 then BIPPer17 \r\n " +
                                       " else 0 end as MyBase  \r\n " +
                                       " from BMCS_Eng_Level \r\n " +
                                       " where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n " +
                                       " and BIPTons = ( \r\n " +
                                       " Select top 1(BIPTons) from BMCS_Eng_Level \r\n " +
                                       " Where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and BIPTons <= '" + SevenTonsAct1 + "'  \r\n " +
                                       " order by BIPTons Desc) ";
                _dbMan7.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan7.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan7.ResultsTableName = "SevenLevel";
                _dbMan7.ExecuteInstruction();

                DataTable dt7 = _dbMan7.ResultsDataTable;

                foreach (DataRow dr7 in dt7.Rows)
                {
                    SevenBase = Convert.ToDouble(dr7["MyBase"].ToString());
                }

                //Eight Level
                MWDataManager.clsDataAccess _dbMan8 = new MWDataManager.clsDataAccess();
                _dbMan8.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan8.SqlStatement = " select \r\n " +
                                       " case when " + EightTonsPerc + " < 50.0 then BIPPer1 \r\n " +
                                       " when " + EightTonsPerc + " >= 50.0 and " + EightTonsPerc + " < 55.0 then BIPPer2 \r\n " +
                                       " when " + EightTonsPerc + " >= 55.0 and " + EightTonsPerc + " < 60.0 then BIPPer3 \r\n " +
                                       " when " + EightTonsPerc + " >= 60.0 and " + EightTonsPerc + " < 65.0 then BIPPer4 \r\n " +
                                       " when " + EightTonsPerc + " >= 65.0 and " + EightTonsPerc + " < 70.0 then BIPPer5 \r\n " +
                                       " when " + EightTonsPerc + " >= 70.0 and " + EightTonsPerc + " < 75.0 then BIPPer6 \r\n " +
                                       " when " + EightTonsPerc + " >= 75.0 and " + EightTonsPerc + " < 80.0 then BIPPer7 \r\n " +
                                       " when " + EightTonsPerc + " >= 80.0 and " + EightTonsPerc + " < 85.0 then BIPPer8 \r\n " +
                                       " when " + EightTonsPerc + " >= 85.0 and " + EightTonsPerc + " < 90.0 then BIPPer9 \r\n " +
                                       " when " + EightTonsPerc + " >= 90.0 and " + EightTonsPerc + " < 95.0 then BIPPer10 \r\n " +
                                       " when " + EightTonsPerc + " >= 95.0 and " + EightTonsPerc + " < 100.0 then BIPPer11 \r\n " +
                                       " when " + EightTonsPerc + " >= 100.0 and " + EightTonsPerc + " < 105.0 then BIPPer12 \r\n " +
                                       " when " + EightTonsPerc + " >= 105.0 and " + EightTonsPerc + " < 110.0 then BIPPer13 \r\n " +
                                       " when " + EightTonsPerc + " >= 110.0 and " + EightTonsPerc + " < 115.0 then BIPPer14 \r\n " +
                                       " when " + EightTonsPerc + " >= 115.0 and " + EightTonsPerc + " < 120.0 then BIPPer15 \r\n " +
                                       " when " + EightTonsPerc + " >= 120.0 and " + EightTonsPerc + " < 125.0 then BIPPer16 \r\n " +
                                       " when " + EightTonsPerc + " >= 125.0 and " + EightTonsPerc + " < 200.0 then BIPPer17 \r\n " +
                                       " else 0 end as MyBase  \r\n " +
                                       " from BMCS_Eng_Level \r\n " +
                                       " where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n " +
                                       " and BIPTons = ( \r\n " +
                                       " Select top 1(BIPTons) from BMCS_Eng_Level \r\n " +
                                       " Where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and BIPTons <= '" + EightTonsAct1 + "'  \r\n " +
                                       " order by BIPTons Desc) ";
                _dbMan8.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan8.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan8.ResultsTableName = "EightLevel";
                _dbMan8.ExecuteInstruction();

                DataTable dt8 = _dbMan8.ResultsDataTable;

                foreach (DataRow dr8 in dt8.Rows)
                {
                    EightBase = Convert.ToDouble(dr8["MyBase"].ToString());
                }

                //Nine Level
                MWDataManager.clsDataAccess _dbMan9 = new MWDataManager.clsDataAccess();
                _dbMan9.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan9.SqlStatement = " select \r\n " +
                                       " case when " + NineTonsPerc + " < 50.0 then BIPPer1 \r\n " +
                                       " when " + NineTonsPerc + " >= 50.0 and " + NineTonsPerc + " < 55.0 then BIPPer2 \r\n " +
                                       " when " + NineTonsPerc + " >= 55.0 and " + NineTonsPerc + " < 60.0 then BIPPer3 \r\n " +
                                       " when " + NineTonsPerc + " >= 60.0 and " + NineTonsPerc + " < 65.0 then BIPPer4 \r\n " +
                                       " when " + NineTonsPerc + " >= 65.0 and " + NineTonsPerc + " < 70.0 then BIPPer5 \r\n " +
                                       " when " + NineTonsPerc + " >= 70.0 and " + NineTonsPerc + " < 75.0 then BIPPer6 \r\n " +
                                       " when " + NineTonsPerc + " >= 75.0 and " + NineTonsPerc + " < 80.0 then BIPPer7 \r\n " +
                                       " when " + NineTonsPerc + " >= 80.0 and " + NineTonsPerc + " < 85.0 then BIPPer8 \r\n " +
                                       " when " + NineTonsPerc + " >= 85.0 and " + NineTonsPerc + " < 90.0 then BIPPer9 \r\n " +
                                       " when " + NineTonsPerc + " >= 90.0 and " + NineTonsPerc + " < 95.0 then BIPPer10 \r\n " +
                                       " when " + NineTonsPerc + " >= 95.0 and " + NineTonsPerc + " < 100.0 then BIPPer11 \r\n " +
                                       " when " + NineTonsPerc + " >= 100.0 and " + NineTonsPerc + " < 105.0 then BIPPer12 \r\n " +
                                       " when " + NineTonsPerc + " >= 105.0 and " + NineTonsPerc + " < 110.0 then BIPPer13 \r\n " +
                                       " when " + NineTonsPerc + " >= 110.0 and " + NineTonsPerc + " < 115.0 then BIPPer14 \r\n " +
                                       " when " + NineTonsPerc + " >= 115.0 and " + NineTonsPerc + " < 120.0 then BIPPer15 \r\n " +
                                       " when " + NineTonsPerc + " >= 120.0 and " + NineTonsPerc + " < 125.0 then BIPPer16 \r\n " +
                                       " when " + NineTonsPerc + " >= 125.0 and " + NineTonsPerc + " < 200.0 then BIPPer17 \r\n " +
                                       " else 0 end as MyBase  \r\n " +
                                       " from BMCS_Eng_Level \r\n " +
                                       " where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n " +
                                       " and BIPTons = ( \r\n " +
                                       " Select top 1(BIPTons) from BMCS_Eng_Level \r\n " +
                                       " Where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and BIPTons <= '" + NineTonsAct1 + "'  \r\n " +
                                       " order by BIPTons Desc) ";
                _dbMan9.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan9.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan9.ResultsTableName = "NineLevel";
                _dbMan9.ExecuteInstruction();

                DataTable dt9 = _dbMan9.ResultsDataTable;

                foreach (DataRow dr9 in dt9.Rows)
                {
                    NineBase = Convert.ToDouble(dr9["MyBase"].ToString());
                }

                //Ten Level
                MWDataManager.clsDataAccess _dbMan10 = new MWDataManager.clsDataAccess();
                _dbMan10.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan10.SqlStatement = " select \r\n " +
                                       " case when " + TenTonsPerc + " < 50.0 then BIPPer1 \r\n " +
                                       " when " + TenTonsPerc + " >= 50.0 and " + TenTonsPerc + " < 55.0 then BIPPer2 \r\n " +
                                       " when " + TenTonsPerc + " >= 55.0 and " + TenTonsPerc + " < 60.0 then BIPPer3 \r\n " +
                                       " when " + TenTonsPerc + " >= 60.0 and " + TenTonsPerc + " < 65.0 then BIPPer4 \r\n " +
                                       " when " + TenTonsPerc + " >= 65.0 and " + TenTonsPerc + " < 70.0 then BIPPer5 \r\n " +
                                       " when " + TenTonsPerc + " >= 70.0 and " + TenTonsPerc + " < 75.0 then BIPPer6 \r\n " +
                                       " when " + TenTonsPerc + " >= 75.0 and " + TenTonsPerc + " < 80.0 then BIPPer7 \r\n " +
                                       " when " + TenTonsPerc + " >= 80.0 and " + TenTonsPerc + " < 85.0 then BIPPer8 \r\n " +
                                       " when " + TenTonsPerc + " >= 85.0 and " + TenTonsPerc + " < 90.0 then BIPPer9 \r\n " +
                                       " when " + TenTonsPerc + " >= 90.0 and " + TenTonsPerc + " < 95.0 then BIPPer10 \r\n " +
                                       " when " + TenTonsPerc + " >= 95.0 and " + TenTonsPerc + " < 100.0 then BIPPer11 \r\n " +
                                       " when " + TenTonsPerc + " >= 100.0 and " + TenTonsPerc + " < 105.0 then BIPPer12 \r\n " +
                                       " when " + TenTonsPerc + " >= 105.0 and " + TenTonsPerc + " < 110.0 then BIPPer13 \r\n " +
                                       " when " + TenTonsPerc + " >= 110.0 and " + TenTonsPerc + " < 115.0 then BIPPer14 \r\n " +
                                       " when " + TenTonsPerc + " >= 115.0 and " + TenTonsPerc + " < 120.0 then BIPPer15 \r\n " +
                                       " when " + TenTonsPerc + " >= 120.0 and " + TenTonsPerc + " < 125.0 then BIPPer16 \r\n " +
                                       " when " + TenTonsPerc + " >= 125.0 and " + TenTonsPerc + " < 200.0 then BIPPer17 \r\n " +
                                       " else 0 end as MyBase  \r\n " +
                                       " from BMCS_Eng_Level \r\n " +
                                       " where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n " +
                                       " and BIPTons = ( \r\n " +
                                       " Select top 1(BIPTons) from BMCS_Eng_Level \r\n " +
                                       " Where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and BIPTons <= '" + TenTonsAct1 + "'  \r\n " +
                                       " order by BIPTons Desc) ";
                _dbMan10.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan10.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan10.ResultsTableName = "TenLevel";
                _dbMan10.ExecuteInstruction();

                DataTable dt10 = _dbMan10.ResultsDataTable;

                foreach (DataRow dr10 in dt10.Rows)
                {
                    TenBase = Convert.ToDouble(dr10["MyBase"].ToString());
                }

                //Eleven Level
                MWDataManager.clsDataAccess _dbMan11 = new MWDataManager.clsDataAccess();
                _dbMan11.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan11.SqlStatement = " select \r\n " +
                                       " case when " + ElevenTonsPerc + " < 50.0 then BIPPer1 \r\n " +
                                       " when " + ElevenTonsPerc + " >= 50.0 and " + ElevenTonsPerc + " < 55.0 then BIPPer2 \r\n " +
                                       " when " + ElevenTonsPerc + " >= 55.0 and " + ElevenTonsPerc + " < 60.0 then BIPPer3 \r\n " +
                                       " when " + ElevenTonsPerc + " >= 60.0 and " + ElevenTonsPerc + " < 65.0 then BIPPer4 \r\n " +
                                       " when " + ElevenTonsPerc + " >= 65.0 and " + ElevenTonsPerc + " < 70.0 then BIPPer5 \r\n " +
                                       " when " + ElevenTonsPerc + " >= 70.0 and " + ElevenTonsPerc + " < 75.0 then BIPPer6 \r\n " +
                                       " when " + ElevenTonsPerc + " >= 75.0 and " + ElevenTonsPerc + " < 80.0 then BIPPer7 \r\n " +
                                       " when " + ElevenTonsPerc + " >= 80.0 and " + ElevenTonsPerc + " < 85.0 then BIPPer8 \r\n " +
                                       " when " + ElevenTonsPerc + " >= 85.0 and " + ElevenTonsPerc + " < 90.0 then BIPPer9 \r\n " +
                                       " when " + ElevenTonsPerc + " >= 90.0 and " + ElevenTonsPerc + " < 95.0 then BIPPer10 \r\n " +
                                       " when " + ElevenTonsPerc + " >= 95.0 and " + ElevenTonsPerc + " < 100.0 then BIPPer11 \r\n " +
                                       " when " + ElevenTonsPerc + " >= 100.0 and " + ElevenTonsPerc + " < 105.0 then BIPPer12 \r\n " +
                                       " when " + ElevenTonsPerc + " >= 105.0 and " + ElevenTonsPerc + " < 110.0 then BIPPer13 \r\n " +
                                       " when " + ElevenTonsPerc + " >= 110.0 and " + ElevenTonsPerc + " < 115.0 then BIPPer14 \r\n " +
                                       " when " + ElevenTonsPerc + " >= 115.0 and " + ElevenTonsPerc + " < 120.0 then BIPPer15 \r\n " +
                                       " when " + ElevenTonsPerc + " >= 120.0 and " + ElevenTonsPerc + " < 125.0 then BIPPer16 \r\n " +
                                       " when " + ElevenTonsPerc + " >= 125.0 and " + ElevenTonsPerc + " < 200.0 then BIPPer17 \r\n " +
                                       " else 0 end as MyBase  \r\n " +
                                       " from BMCS_Eng_Level \r\n " +
                                       " where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n " +
                                       " and BIPTons = ( \r\n " +
                                       " Select top 1(BIPTons) from BMCS_Eng_Level \r\n " +
                                       " Where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and BIPTons <= '" + ElevenTonsAct1 + "'  \r\n " +
                                       " order by BIPTons Desc) ";
                _dbMan11.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan11.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan11.ResultsTableName = "ElevenLevel";
                _dbMan11.ExecuteInstruction();

                DataTable dt11 = _dbMan11.ResultsDataTable;

                foreach (DataRow dr11 in dt11.Rows)
                {
                    ElevenBase = Convert.ToDouble(dr11["MyBase"].ToString());
                }

                //Twelve Level
                MWDataManager.clsDataAccess _dbMan12 = new MWDataManager.clsDataAccess();
                _dbMan12.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan12.SqlStatement = " select \r\n " +
                                       " case when " + TwelveTonsPerc + " < 50.0 then BIPPer1 \r\n " +
                                       " when " + TwelveTonsPerc + " >= 50.0 and " + TwelveTonsPerc + " < 55.0 then BIPPer2 \r\n " +
                                       " when " + TwelveTonsPerc + " >= 55.0 and " + TwelveTonsPerc + " < 60.0 then BIPPer3 \r\n " +
                                       " when " + TwelveTonsPerc + " >= 60.0 and " + TwelveTonsPerc + " < 65.0 then BIPPer4 \r\n " +
                                       " when " + TwelveTonsPerc + " >= 65.0 and " + TwelveTonsPerc + " < 70.0 then BIPPer5 \r\n " +
                                       " when " + TwelveTonsPerc + " >= 70.0 and " + TwelveTonsPerc + " < 75.0 then BIPPer6 \r\n " +
                                       " when " + TwelveTonsPerc + " >= 75.0 and " + TwelveTonsPerc + " < 80.0 then BIPPer7 \r\n " +
                                       " when " + TwelveTonsPerc + " >= 80.0 and " + TwelveTonsPerc + " < 85.0 then BIPPer8 \r\n " +
                                       " when " + TwelveTonsPerc + " >= 85.0 and " + TwelveTonsPerc + " < 90.0 then BIPPer9 \r\n " +
                                       " when " + TwelveTonsPerc + " >= 90.0 and " + TwelveTonsPerc + " < 95.0 then BIPPer10 \r\n " +
                                       " when " + TwelveTonsPerc + " >= 95.0 and " + TwelveTonsPerc + " < 100.0 then BIPPer11 \r\n " +
                                       " when " + TwelveTonsPerc + " >= 100.0 and " + TwelveTonsPerc + " < 105.0 then BIPPer12 \r\n " +
                                       " when " + TwelveTonsPerc + " >= 105.0 and " + TwelveTonsPerc + " < 110.0 then BIPPer13 \r\n " +
                                       " when " + TwelveTonsPerc + " >= 110.0 and " + TwelveTonsPerc + " < 115.0 then BIPPer14 \r\n " +
                                       " when " + TwelveTonsPerc + " >= 115.0 and " + TwelveTonsPerc + " < 120.0 then BIPPer15 \r\n " +
                                       " when " + TwelveTonsPerc + " >= 120.0 and " + TwelveTonsPerc + " < 125.0 then BIPPer16 \r\n " +
                                       " when " + TwelveTonsPerc + " >= 125.0 and " + TwelveTonsPerc + " < 200.0 then BIPPer17 \r\n " +
                                       " else 0 end as MyBase  \r\n " +
                                       " from BMCS_Eng_Level \r\n " +
                                       " where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n " +
                                       " and BIPTons = ( \r\n " +
                                       " Select top 1(BIPTons) from BMCS_Eng_Level \r\n " +
                                       " Where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and BIPTons <= '" + TwelveTonsAct1 + "'  \r\n " +
                                       " order by BIPTons Desc) ";
                _dbMan12.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan12.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan12.ResultsTableName = "TwelveLevel";
                _dbMan12.ExecuteInstruction();

                DataTable dt12 = _dbMan12.ResultsDataTable;

                foreach (DataRow dr12 in dt12.Rows)
                {
                    TwelveBase = Convert.ToDouble(dr12["MyBase"].ToString());
                }

                //Thirteen Level
                MWDataManager.clsDataAccess _dbMan13 = new MWDataManager.clsDataAccess();
                _dbMan13.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan13.SqlStatement = " select \r\n " +
                                       " case when " + ThirteenTonsPerc + " < 50.0 then BIPPer1 \r\n " +
                                       " when " + ThirteenTonsPerc + " >= 50.0 and " + ThirteenTonsPerc + " < 55.0 then BIPPer2 \r\n " +
                                       " when " + ThirteenTonsPerc + " >= 55.0 and " + ThirteenTonsPerc + " < 60.0 then BIPPer3 \r\n " +
                                       " when " + ThirteenTonsPerc + " >= 60.0 and " + ThirteenTonsPerc + " < 65.0 then BIPPer4 \r\n " +
                                       " when " + ThirteenTonsPerc + " >= 65.0 and " + ThirteenTonsPerc + " < 70.0 then BIPPer5 \r\n " +
                                       " when " + ThirteenTonsPerc + " >= 70.0 and " + ThirteenTonsPerc + " < 75.0 then BIPPer6 \r\n " +
                                       " when " + ThirteenTonsPerc + " >= 75.0 and " + ThirteenTonsPerc + " < 80.0 then BIPPer7 \r\n " +
                                       " when " + ThirteenTonsPerc + " >= 80.0 and " + ThirteenTonsPerc + " < 85.0 then BIPPer8 \r\n " +
                                       " when " + ThirteenTonsPerc + " >= 85.0 and " + ThirteenTonsPerc + " < 90.0 then BIPPer9 \r\n " +
                                       " when " + ThirteenTonsPerc + " >= 90.0 and " + ThirteenTonsPerc + " < 95.0 then BIPPer10 \r\n " +
                                       " when " + ThirteenTonsPerc + " >= 95.0 and " + ThirteenTonsPerc + " < 100.0 then BIPPer11 \r\n " +
                                       " when " + ThirteenTonsPerc + " >= 100.0 and " + ThirteenTonsPerc + " < 105.0 then BIPPer12 \r\n " +
                                       " when " + ThirteenTonsPerc + " >= 105.0 and " + ThirteenTonsPerc + " < 110.0 then BIPPer13 \r\n " +
                                       " when " + ThirteenTonsPerc + " >= 110.0 and " + ThirteenTonsPerc + " < 115.0 then BIPPer14 \r\n " +
                                       " when " + ThirteenTonsPerc + " >= 115.0 and " + ThirteenTonsPerc + " < 120.0 then BIPPer15 \r\n " +
                                       " when " + ThirteenTonsPerc + " >= 120.0 and " + ThirteenTonsPerc + " < 125.0 then BIPPer16 \r\n " +
                                       " when " + ThirteenTonsPerc + " >= 125.0 and " + ThirteenTonsPerc + " < 200.0 then BIPPer17 \r\n " +
                                       " else 0 end as MyBase  \r\n " +
                                       " from BMCS_Eng_Level \r\n " +
                                       " where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n " +
                                       " and BIPTons = ( \r\n " +
                                       " Select top 1(BIPTons) from BMCS_Eng_Level \r\n " +
                                       " Where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and BIPTons <= '" + ThirteenTonsAct1 + "'  \r\n " +
                                       " order by BIPTons Desc) ";
                _dbMan13.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan13.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan13.ResultsTableName = "ThirteenLevel";
                _dbMan13.ExecuteInstruction();

                DataTable dt13 = _dbMan13.ResultsDataTable;

                foreach (DataRow dr13 in dt13.Rows)
                {
                    ThirteenBase = Convert.ToDouble(dr13["MyBase"].ToString());
                }

                //Fourteen Level
                MWDataManager.clsDataAccess _dbMan14 = new MWDataManager.clsDataAccess();
                _dbMan14.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan14.SqlStatement = " select \r\n " +
                                       " case when " + FourteenTonsPerc + " < 50.0 then BIPPer1 \r\n " +
                                       " when " + FourteenTonsPerc + " >= 50.0 and " + FourteenTonsPerc + " < 55.0 then BIPPer2 \r\n " +
                                       " when " + FourteenTonsPerc + " >= 55.0 and " + FourteenTonsPerc + " < 60.0 then BIPPer3 \r\n " +
                                       " when " + FourteenTonsPerc + " >= 60.0 and " + FourteenTonsPerc + " < 65.0 then BIPPer4 \r\n " +
                                       " when " + FourteenTonsPerc + " >= 65.0 and " + FourteenTonsPerc + " < 70.0 then BIPPer5 \r\n " +
                                       " when " + FourteenTonsPerc + " >= 70.0 and " + FourteenTonsPerc + " < 75.0 then BIPPer6 \r\n " +
                                       " when " + FourteenTonsPerc + " >= 75.0 and " + FourteenTonsPerc + " < 80.0 then BIPPer7 \r\n " +
                                       " when " + FourteenTonsPerc + " >= 80.0 and " + FourteenTonsPerc + " < 85.0 then BIPPer8 \r\n " +
                                       " when " + FourteenTonsPerc + " >= 85.0 and " + FourteenTonsPerc + " < 90.0 then BIPPer9 \r\n " +
                                       " when " + FourteenTonsPerc + " >= 90.0 and " + FourteenTonsPerc + " < 95.0 then BIPPer10 \r\n " +
                                       " when " + FourteenTonsPerc + " >= 95.0 and " + FourteenTonsPerc + " < 100.0 then BIPPer11 \r\n " +
                                       " when " + FourteenTonsPerc + " >= 100.0 and " + FourteenTonsPerc + " < 105.0 then BIPPer12 \r\n " +
                                       " when " + FourteenTonsPerc + " >= 105.0 and " + FourteenTonsPerc + " < 110.0 then BIPPer13 \r\n " +
                                       " when " + FourteenTonsPerc + " >= 110.0 and " + FourteenTonsPerc + " < 115.0 then BIPPer14 \r\n " +
                                       " when " + FourteenTonsPerc + " >= 115.0 and " + FourteenTonsPerc + " < 120.0 then BIPPer15 \r\n " +
                                       " when " + FourteenTonsPerc + " >= 120.0 and " + FourteenTonsPerc + " < 125.0 then BIPPer16 \r\n " +
                                       " when " + FourteenTonsPerc + " >= 125.0 and " + FourteenTonsPerc + " < 200.0 then BIPPer17 \r\n " +
                                       " else 0 end as MyBase  \r\n " +
                                       " from BMCS_Eng_Level \r\n " +
                                       " where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n " +
                                       " and BIPTons = ( \r\n " +
                                       " Select top 1(BIPTons) from BMCS_Eng_Level \r\n " +
                                       " Where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and BIPTons <= '" + FourteenTonsAct1 + "'  \r\n " +
                                       " order by BIPTons Desc) ";
                _dbMan14.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan14.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan14.ResultsTableName = "FourteenLevel";
                _dbMan14.ExecuteInstruction();

                DataTable dt14 = _dbMan14.ResultsDataTable;

                foreach (DataRow dr14 in dt14.Rows)
                {
                    FourteenBase = Convert.ToDouble(dr14["MyBase"].ToString());
                }

                //Fifteen Level
                MWDataManager.clsDataAccess _dbMan15 = new MWDataManager.clsDataAccess();
                _dbMan15.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan15.SqlStatement = " select \r\n " +
                                       " case when " + FifteenTonsPerc + " < 50.0 then BIPPer1 \r\n " +
                                       " when " + FifteenTonsPerc + " >= 50.0 and " + FifteenTonsPerc + " < 55.0 then BIPPer2 \r\n " +
                                       " when " + FifteenTonsPerc + " >= 55.0 and " + FifteenTonsPerc + " < 60.0 then BIPPer3 \r\n " +
                                       " when " + FifteenTonsPerc + " >= 60.0 and " + FifteenTonsPerc + " < 65.0 then BIPPer4 \r\n " +
                                       " when " + FifteenTonsPerc + " >= 65.0 and " + FifteenTonsPerc + " < 70.0 then BIPPer5 \r\n " +
                                       " when " + FifteenTonsPerc + " >= 70.0 and " + FifteenTonsPerc + " < 75.0 then BIPPer6 \r\n " +
                                       " when " + FifteenTonsPerc + " >= 75.0 and " + FifteenTonsPerc + " < 80.0 then BIPPer7 \r\n " +
                                       " when " + FifteenTonsPerc + " >= 80.0 and " + FifteenTonsPerc + " < 85.0 then BIPPer8 \r\n " +
                                       " when " + FifteenTonsPerc + " >= 85.0 and " + FifteenTonsPerc + " < 90.0 then BIPPer9 \r\n " +
                                       " when " + FifteenTonsPerc + " >= 90.0 and " + FifteenTonsPerc + " < 95.0 then BIPPer10 \r\n " +
                                       " when " + FifteenTonsPerc + " >= 95.0 and " + FifteenTonsPerc + " < 100.0 then BIPPer11 \r\n " +
                                       " when " + FifteenTonsPerc + " >= 100.0 and " + FifteenTonsPerc + " < 105.0 then BIPPer12 \r\n " +
                                       " when " + FifteenTonsPerc + " >= 105.0 and " + FifteenTonsPerc + " < 110.0 then BIPPer13 \r\n " +
                                       " when " + FifteenTonsPerc + " >= 110.0 and " + FifteenTonsPerc + " < 115.0 then BIPPer14 \r\n " +
                                       " when " + FifteenTonsPerc + " >= 115.0 and " + FifteenTonsPerc + " < 120.0 then BIPPer15 \r\n " +
                                       " when " + FifteenTonsPerc + " >= 120.0 and " + FifteenTonsPerc + " < 125.0 then BIPPer16 \r\n " +
                                       " when " + FifteenTonsPerc + " >= 125.0 and " + FifteenTonsPerc + " < 200.0 then BIPPer17 \r\n " +
                                       " else 0 end as MyBase  \r\n " +
                                       " from BMCS_Eng_Level \r\n " +
                                       " where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n " +
                                       " and BIPTons = ( \r\n " +
                                       " Select top 1(BIPTons) from BMCS_Eng_Level \r\n " +
                                       " Where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and BIPTons <= '" + FifteenTonsAct1 + "'  \r\n " +
                                       " order by BIPTons Desc) ";
                _dbMan15.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan15.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan15.ResultsTableName = "FifteenLevel";
                _dbMan15.ExecuteInstruction();

                DataTable dt15 = _dbMan15.ResultsDataTable;

                foreach (DataRow dr15 in dt15.Rows)
                {
                    FifteenBase = Convert.ToDouble(dr15["MyBase"].ToString());
                }

                //Sixteen Level
                MWDataManager.clsDataAccess _dbMan16 = new MWDataManager.clsDataAccess();
                _dbMan16.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan16.SqlStatement = " select \r\n " +
                                       " case when " + SixteenTonsPerc + " < 50.0 then BIPPer1 \r\n " +
                                       " when " + SixteenTonsPerc + " >= 50.0 and " + SixteenTonsPerc + " < 55.0 then BIPPer2 \r\n " +
                                       " when " + SixteenTonsPerc + " >= 55.0 and " + SixteenTonsPerc + " < 60.0 then BIPPer3 \r\n " +
                                       " when " + SixteenTonsPerc + " >= 60.0 and " + SixteenTonsPerc + " < 65.0 then BIPPer4 \r\n " +
                                       " when " + SixteenTonsPerc + " >= 65.0 and " + SixteenTonsPerc + " < 70.0 then BIPPer5 \r\n " +
                                       " when " + SixteenTonsPerc + " >= 70.0 and " + SixteenTonsPerc + " < 75.0 then BIPPer6 \r\n " +
                                       " when " + SixteenTonsPerc + " >= 75.0 and " + SixteenTonsPerc + " < 80.0 then BIPPer7 \r\n " +
                                       " when " + SixteenTonsPerc + " >= 80.0 and " + SixteenTonsPerc + " < 85.0 then BIPPer8 \r\n " +
                                       " when " + SixteenTonsPerc + " >= 85.0 and " + SixteenTonsPerc + " < 90.0 then BIPPer9 \r\n " +
                                       " when " + SixteenTonsPerc + " >= 90.0 and " + SixteenTonsPerc + " < 95.0 then BIPPer10 \r\n " +
                                       " when " + SixteenTonsPerc + " >= 95.0 and " + SixteenTonsPerc + " < 100.0 then BIPPer11 \r\n " +
                                       " when " + SixteenTonsPerc + " >= 100.0 and " + SixteenTonsPerc + " < 105.0 then BIPPer12 \r\n " +
                                       " when " + SixteenTonsPerc + " >= 105.0 and " + SixteenTonsPerc + " < 110.0 then BIPPer13 \r\n " +
                                       " when " + SixteenTonsPerc + " >= 110.0 and " + SixteenTonsPerc + " < 115.0 then BIPPer14 \r\n " +
                                       " when " + SixteenTonsPerc + " >= 115.0 and " + SixteenTonsPerc + " < 120.0 then BIPPer15 \r\n " +
                                       " when " + SixteenTonsPerc + " >= 120.0 and " + SixteenTonsPerc + " < 125.0 then BIPPer16 \r\n " +
                                       " when " + SixteenTonsPerc + " >= 125.0 and " + SixteenTonsPerc + " < 200.0 then BIPPer17 \r\n " +
                                       " else 0 end as MyBase  \r\n " +
                                       " from BMCS_Eng_Level \r\n " +
                                       " where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n " +
                                       " and BIPTons = ( \r\n " +
                                       " Select top 1(BIPTons) from BMCS_Eng_Level \r\n " +
                                       " Where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and BIPTons <= '" + SixteenTonsAct1 + "'  \r\n " +
                                       " order by BIPTons Desc) ";
                _dbMan16.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan16.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan16.ResultsTableName = "SixteenLevel";
                _dbMan16.ExecuteInstruction();

                DataTable dt16 = _dbMan16.ResultsDataTable;

                foreach (DataRow dr16 in dt16.Rows)
                {
                    SixteenBase = Convert.ToDouble(dr16["MyBase"].ToString());
                }




                //Final
                MWDataManager.clsDataAccess _dbManFinal = new MWDataManager.clsDataAccess();
                _dbManFinal.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManFinal.SqlStatement = " select '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' Prodmonth, " +
                                           " '" + OneTonsCall + "' OneCall, '" + OneTonsAct + "' OneAct, '" + OneTonsPerc + "' OnePerc, '" + OneBase + "' OneBase, " +
                                           " '" + TwoTonsCall + "' TwoCall, '" + TwoTonsAct + "' TwoAct, '" + TwoTonsPerc + "' TwoPerc, '" + TwoBase + "' TwoBase, " +
                                           " '" + ThreeTonsCall + "' ThreeCall, '" + ThreeTonsAct + "' ThreeAct, '" + ThreeTonsPerc + "' ThreePerc, '" + ThreeBase + "' ThreeBase, " +
                                           " '" + FourTonsCall + "' FourCall, '" + FourTonsAct + "' FourAct, '" + FourTonsPerc + "' FourPerc, '" + FourBase + "' FourBase, " +
                                           " '" + FiveTonsCall + "' FiveCall, '" + FiveTonsAct + "' FiveAct, '" + FiveTonsPerc + "' FivePerc, '" + FiveBase + "' FiveBase, " +
                                           " '" + SixTonsCall + "' SixCall, '" + SixTonsAct + "' SixAct, '" + SixTonsPerc + "' SixPerc, '" + SixBase + "' SixBase, " +
                                           " '" + SevenTonsCall + "' SevenCall, '" + SevenTonsAct + "' SevenAct, '" + SevenTonsPerc + "' SevenPerc, '" + SevenBase + "' SevenBase, " +
                                           " '" + EightTonsCall + "' EightCall, '" + EightTonsAct + "' EightAct, '" + EightTonsPerc + "' EightPerc, '" + EightBase + "' EightBase, " +
                                           " '" + NineTonsCall + "' NineCall, '" + NineTonsAct + "' NineAct, '" + NineTonsPerc + "' NinePerc, '" + NineBase + "' NineBase, " +
                                           " '" + TenTonsCall + "' TenCall, '" + TenTonsAct + "' TenAct, '" + TenTonsPerc + "' TenPerc, '" + TenBase + "' TenBase, " +
                                           " '" + ElevenTonsCall + "' ElevenCall, '" + ElevenTonsAct + "' ElevenAct, '" + ElevenTonsPerc + "' ElevenPerc, '" + ElevenBase + "' ElevenBase, " +
                                           " '" + TwelveTonsCall + "' TwelveCall, '" + TwelveTonsAct + "' TwelveAct, '" + TwelveTonsPerc + "' TwelvePerc, '" + TwelveBase + "' TwelveBase, " +
                                           " '" + ThirteenTonsCall + "' ThirteenCall, '" + ThirteenTonsAct + "' ThirteenAct, '" + ThirteenTonsPerc + "' ThirteenPerc, '" + ThirteenBase + "' ThirteenBase, " +
                                           " '" + FourteenTonsCall + "' FourteenCall, '" + FourteenTonsAct + "' FourteenAct, '" + FourteenTonsPerc + "' FourteenPerc, '" + FourteenBase + "' FourteenBase, " +
                                           " '" + FifteenTonsCall + "' FifteenCall, '" + FifteenTonsAct + "' FifteenAct, '" + FifteenTonsPerc + "' FifteenPerc, '" + FifteenBase + "' FifteenBase, " +
                                           " '" + SixteenTonsCall + "' SixteenCall, '" + SixteenTonsAct + "' SixteenAct, '" + SixteenTonsPerc + "' SixteenPerc, '" + SixteenBase + "' SixteenBase ";
                _dbManFinal.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManFinal.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManFinal.ResultsTableName = "Final";
                _dbManFinal.ExecuteInstruction();

                DataTable dtFinal = _dbManFinal.ResultsDataTable;

                DataSet dsFinal = new DataSet();
                dsFinal.Tables.Add(dtFinal);
                theReport.RegisterData(dsFinal);

                theReport.Load("BaseRateCalc.frx");

                //theReport.Design();


            }
            else if (this.Text == "Engineering Artisan's per level")
            {
                MWDataManager.clsDataAccess _dbManFinal = new MWDataManager.clsDataAccess();
                _dbManFinal.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManFinal.SqlStatement = "select *, case when Artisan = 'Boilermaker' and aaa >= 60 then 1  \r\n " +
                                           "when Artisan <> 'Boilermaker' and aaa >= 30 then 1  \r\n " +
                                           "else 0 end as Highlight, \r\n " +
                                           "case when Artisan = 'Boilermaker' and aaa >= 60 then '75 %'  \r\n " +
                                           "when Artisan <> 'Boilermaker' and aaa >= 30 then '75 %'  \r\n " +
                                           "else '100 %' end as Percentage, case when Artisan = 'Boilermaker' then '60' else '30' end as Threshold \r\n " +
                                           "from ( \r\n " +
                                           "select '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ProdMonth, category Artisan, MyLevel, COUNT(LeaveFlag) aaa from ( \r\n " +
                                           "	select * from (  \r\n " +
                                           "		select SUBSTRING(Orgunit,7,1) MyLevel,  \r\n " +
                                           "		case when Designation like '%U/G Fitter%' then 'Fitter'  \r\n " +
                                           "		when Designation like '%U/G Electrician%' then 'Electrician'  \r\n " +
                                           "		when Designation like '%U/G Plater%' then 'Boilermaker'  \r\n " +
                                           "		else '' end as category  \r\n " +
                                           "		,* from tbl_Import_BMCS_Clocking_Total  \r\n " +
                                           "		where TheDate in (  \r\n " +
                                           "							select A.CalendarDate from BMCS_Caltype A \r\n " +
                                           "							left outer join BMCS_CalShifts B \r\n " +
                                           "							on A.CalendarTypeID = B.CalendarTypeID \r\n " +
                                           "							where Yearmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n " +
                                           "							and A.WorkingDay = 'Y' \r\n " +
                                           "							and A.CalendarTypeID = 'Production' \r\n " +
                                           "							and A.CalendarDate >= B.BeginDate \r\n " +
                                           "							and A.CalendarDate <= B.EndDate \r\n " +
                                           "						 ) \r\n " +
                                           "		and Orgunit like '02%'  \r\n " +
                                           "		and ExpectedAtWork = 'Y'  \r\n " +
                                           "		and LeaveFlag in ('N','NA')  \r\n " +
                                           "	)b  \r\n " +
                                           "	where category <> ''  \r\n " +
                                           "	and SUBSTRING(Orgunit,7,1) in ('A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P') \r\n " +
                                           ")a  \r\n " +
                                           "group by category, MyLevel )a \r\n " +
                                           "order by Artisan,MyLevel  ";
                _dbManFinal.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManFinal.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManFinal.ResultsTableName = "Final";
                _dbManFinal.ExecuteInstruction();

                DataTable dtFinal = _dbManFinal.ResultsDataTable;

                DataSet dsFinal = new DataSet();
                dsFinal.Tables.Add(dtFinal);
                theReport.RegisterData(dsFinal);

                theReport.Load("ArtisanPerLevel.frx");

                //theReport.Design();

                pcReport.Clear();
                theReport.Prepare();
                theReport.Preview = pcReport;
                theReport.ShowPrepared();
            }
        }

        private void frmBaseRateReport_Load(object sender, EventArgs e)
        {
            editProdmonth.EditValue = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsDate(ProductionAmplatsGlobalTSysSettings._currentProductionMonth.ToString());
        }

       

    }
}