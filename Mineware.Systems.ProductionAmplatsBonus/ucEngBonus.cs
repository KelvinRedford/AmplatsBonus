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
//using PAS.ReportSettings;
using System.Net;
using FastReport.Utils;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;
using Mineware.Systems.ProductionAmplatsGlobal;

namespace Mineware.Systems.ProductionAmplatsBonus
{
    public partial class ucEngBonus : BaseUserControl
    {
        Report theReport = new Report();
        private string _reportFolder = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\Reports\";

        public ucEngBonus()
        {
            InitializeComponent();
            FormRibbonPages.Add(rpReports);
            FormActiveRibbonPage = rpReports;
            FormMainRibbonPage = rpReports;
            RibbonControl = rcReports;
        }

        DataTable Neil = new DataTable();
        DataSet ReportDatasetEng = new DataSet();

        DataTable Neil1 = new DataTable();
        DataTable Neil2 = new DataTable();
        DataSet ReportDatasetEng1 = new DataSet();

        DataSet ReportEngPage2 = new DataSet();

        decimal tonsplan = 0;
        decimal tonsact = 0;
        decimal AchPrec = 0;

        decimal incentive = 0;

        decimal Dept = 0;


        void LoadGrid()
        {



            if (ProdRB.Checked == true)
            {
                MWDataManager.clsDataAccess _dbManSetup = new MWDataManager.clsDataAccess();
                _dbManSetup.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);



                string UseSmooth = "N";

                if ((Showlabel.Text + "           ").Substring(0, 6) == "02110V" && Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) > 201702)
                    UseSmooth = "Y";

                if (Showlabel.Text == "V Gang" && Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) > 201702)
                    UseSmooth = "Y";

                //if ((Showlabel.Text + "           ").Substring(0, 6) == "020011V" && Convert.ToInt32(ProdMonthTxt.Text) >= 201705)
                //    UseSmooth = "Y";
                //if ((Showlabel.Text + "           ").Substring(0, 6) == "020011V1" && Convert.ToInt32(ProdMonthTxt.Text) >= 201705)
                //    UseSmooth = "Y";
                //if ((Showlabel.Text + "           ").Substring(0, 6) == "020011V2A" && Convert.ToInt32(ProdMonthTxt.Text) >= 201705)
                //    UseSmooth = "Y";
                //if ((Showlabel.Text + "           ").Substring(0, 6) == "020011V2B" && Convert.ToInt32(ProdMonthTxt.Text) >= 201705)
                //    UseSmooth = "Y";
                //if ((Showlabel.Text + "           ").Substring(0, 6) == "020011V2C" && Convert.ToInt32(ProdMonthTxt.Text) >= 201705)
                //    UseSmooth = "Y";
                //if ((Showlabel.Text + "           ").Substring(0, 6) == "020011V2D" && Convert.ToInt32(ProdMonthTxt.Text) >= 201705)
                //    UseSmooth = "Y";
                //if ((Showlabel.Text + "           ").Substring(0, 6) == "020011V3A" && Convert.ToInt32(ProdMonthTxt.Text) >= 201705)
                //    UseSmooth = "Y";
                //if ((Showlabel.Text + "           ").Substring(0, 6) == "020011V3B" && Convert.ToInt32(ProdMonthTxt.Text) >= 201705)
                //    UseSmooth = "Y";



               // if ((Showlabel.Text + "           ").Substring(0, 6) == "02110V" && Convert.ToInt32(ProdMonthTxt.Text) > 201702)
                if (UseSmooth == "Y")
                {

                    _dbManSetup.SqlStatement = " select aper/100 aa, convert(decimal(18,0),ccc) TonsCall, convert(decimal(18,0),aaa) tonsact, cata, ProdDept, WShop, TSD, Shaft, BMR, smelter, conc, ChiefTech from  " +
  
                                            "( "+

                                            "select  "+
                                            " (tonsact+0.001)/(tonscall+0.001) aa, "+ 
                                            " * from mineware.[dbo].[tbl_BCS_Eng_FactorNew1]  "+
                                            " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' " +
 
 
                                            " ) a  "+
                                            " ,  "+
                                            " (select top(1) cat cata from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveCat]  " +
                                            " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and   " +
                                            "  perc < ( "+
  


                                            " SELECT isnull(sum([Call]),0)/ isnull(sum([Act]),0.01)*100  "+

                                             " FROM [mineware].[dbo].[tbl_BCS_Eng_Production] " +
                                             " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' " +
                                            "  and lvlnum in ('13','14','15','16','17') " +

  
  
                                             " ) order by perc desc) b  "+
  
                                             " ,  (SELECT isnull(avg([Call]),0) ccc, isnull(avg([Act]),0.01) aaa, isnull(avg([Call]),0)/ isnull(avg([Act]),0.01)*100 aper "+

                                            "  FROM [mineware].[dbo].[tbl_BCS_Eng_Production] "+
                                            "  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' " +
                                            "  and lvlnum in ('13','14','15','16','17')) c ";
                    
                    
                    
                    
                    
                    
                    
                    
                    //select aa, TonsCall, tonsact, cata, ProdDept, WShop, TSD, Shaft, BMR, smelter, conc, ChiefTech from (select \r\n" +
                    //                        " (tonsact+0.001)/(tonscall+0.001) aa, \r\n" +
                    //                        " * from mineware.[dbo].[tbl_BCS_Eng_FactorNew] \r\n" +
                    //                        " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') a \r\n" +
                    //                        " , \r\n" +
                    //                        " (select top(1) cat cata from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveCat] \r\n" +
                    //                         " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and  \r\n" +
                    //                        "  perc < (select  \r\n" +
                    //                        " (tonsact+0.001)/(tonscall+0.001)*100 aa from mineware.[dbo].[tbl_BCS_Eng_FactorNew] \r\n" +
                    //                        "  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') order by perc desc) b \r\n";
               
                }
                else
                {

               
                _dbManSetup.SqlStatement = "select aa, TonsCall, tonsact, cata, ProdDept, WShop, TSD, Shaft, BMR, smelter, conc, ChiefTech from (select \r\n" +
                                            " (tonsact+0.001)/(tonscall+0.001) aa, \r\n" +
                                            " * from mineware.[dbo].[tbl_BCS_Eng_FactorNew1] \r\n" +
                                            " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') a \r\n" +
                                            " , \r\n" +
                                            " (select top(1) cat cata from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveCat] \r\n" +
                                             " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and  \r\n" +
                                            "  perc < (select  \r\n" +
                                            " (tonsact+0.001)/(tonscall+0.001)*100 aa from mineware.[dbo].[tbl_BCS_Eng_FactorNew1] \r\n" +
                                            "  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') order by perc desc) b \r\n";
                //_dbManSetup.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                //_dbManSetup.queryReturnType = MWDataManager.ReturnType.DataTable;
                //_dbManSetup.ResultsTableName = "DefData";
                //_dbManSetup.ExecuteInstruction();

                }
                _dbManSetup.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManSetup.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManSetup.ResultsTableName = "DefData";
                _dbManSetup.ExecuteInstruction();

                Neil1 = _dbManSetup.ResultsDataTable;

                if (Neil1.Rows.Count < 1)
                {
                    MessageBox.Show("Report Cant Be Loaded", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                tonsplan = Convert.ToDecimal(Neil1.Rows[0]["TonsCall"].ToString());
                tonsact = Convert.ToDecimal(Neil1.Rows[0]["TonsAct"].ToString());
                AchPrec = Math.Round(Convert.ToDecimal(Neil1.Rows[0]["aa"].ToString()) * 100, 2);
                string aa = Neil1.Rows[0]["cata"].ToString();

            }


            if (ShaftRB.Checked == true)
            {

                string UseSmooth = "N";

                if ((Showlabel.Text + "           ").Substring(0, 7) == "020011V" && Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) >= 201705)
                    UseSmooth = "Y";



                if (((Showlabel.Text + "           ").Substring(0, 6) == "020011") || (Showlabel.Text == "02001") || ((Showlabel.Text + "           ").Substring(0, 5) == "02011") || (Showlabel.Text == "02121") || (Showlabel.Text == "0200141") || (Showlabel.Text == "020014"))
                {
                    MWDataManager.clsDataAccess _dbManSetup = new MWDataManager.clsDataAccess();
                    _dbManSetup.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                    if (UseSmooth == "N")
                    {
                        //MWDataManager.clsDataAccess _dbManSetup = new MWDataManager.clsDataAccess();
                        //_dbManSetup.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);



                        _dbManSetup.SqlStatement = "select aa, s1TonsCall TonsCall, s1tonsact tonsact, cata, ProdDept, WShop, TSD, Shaft, BMR, smelter, conc, ChiefTech from (select \r\n";

                        if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) == "201705")
                           _dbManSetup.SqlStatement = _dbManSetup.SqlStatement + " (S1tonsact+10+0.001)/(S1tonscall+0.001) aa, \r\n";
                        else
                            _dbManSetup.SqlStatement = _dbManSetup.SqlStatement + " (S1tonsact+0.001)/(S1tonscall+0.001) aa, \r\n";
                        _dbManSetup.SqlStatement = _dbManSetup.SqlStatement + " * from mineware.[dbo].[tbl_BCS_Eng_FactorNew1] \r\n";
                        _dbManSetup.SqlStatement = _dbManSetup.SqlStatement + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') a \r\n";
                        _dbManSetup.SqlStatement = _dbManSetup.SqlStatement + " , \r\n";
                        _dbManSetup.SqlStatement = _dbManSetup.SqlStatement + " (select top(1) cat cata from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveCat] \r\n";
                        _dbManSetup.SqlStatement = _dbManSetup.SqlStatement + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and  \r\n";
                        _dbManSetup.SqlStatement = _dbManSetup.SqlStatement + "  perc <= (select  \r\n";
                        if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) == "201705")
                        _dbManSetup.SqlStatement = _dbManSetup.SqlStatement + " (s1tonsact+10+0.001)/(s1tonscall+0.001)*100 aa from mineware.[dbo].[tbl_BCS_Eng_FactorNew1] \r\n";
                        else
                            _dbManSetup.SqlStatement = _dbManSetup.SqlStatement + " (s1tonsact+0.001)/(s1tonscall+0.001)*100 aa from mineware.[dbo].[tbl_BCS_Eng_FactorNew1] \r\n";
                       _dbManSetup.SqlStatement = _dbManSetup.SqlStatement + "  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') order by perc desc) b \r\n";
                        //_dbManSetup.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        //_dbManSetup.queryReturnType = MWDataManager.ReturnType.DataTable;
                        //_dbManSetup.ResultsTableName = "DefData";
                        //_dbManSetup.ExecuteInstruction();
                    }
                    else
                    {




                        //MWDataManager.clsDataAccess _dbManSetup = new MWDataManager.clsDataAccess();
                        //_dbManSetup.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _dbManSetup.SqlStatement = " select aper aa, convert(decimal(18,0),ccc) TonsCall, convert(decimal(18,0),aaa) tonsact, cata, ProdDept, WShop, TSD, Shaft, BMR, smelter, conc, ChiefTech from  " +

                                                "( " +

                                                "select  " +
                                                " (tonsact+0.001)/(tonscall+0.001) aa, " +
                                                " * from mineware.[dbo].[tbl_BCS_Eng_FactorNew1]  " +
                                                " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' " +


                                                " ) a  " +
                                                " ,  " +
                                                " (select top(1) cat cata from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveCat]  " +
                                                " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and   " +
                                                "  perc < ( " +



                                                " SELECT isnull(sum([Call]),0)/ isnull(sum([Act]),0.01)*100  " +

                                                 " FROM [mineware].[dbo].[tbl_BCS_Eng_Production] " +
                                                 " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' " +
                                                "  and lvlnum in ('13','14','15','16','17') " +



                                                 " ) order by perc desc) b  " +
                                                    
                                                 " ,  (SELECT isnull(avg([Call]),0) ccc, isnull(avg([Act]),0.01) aaa, isnull(avg([Call]),0)/ isnull(avg([Act]),0.01)*100 aper " +

                                                "  FROM [mineware].[dbo].[tbl_BCS_Eng_Production] " +
                                                "  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' " +
                                                "  and lvlnum in ('13','14','15','16','17')) c ";
                        //_dbManSetup.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        //_dbManSetup.queryReturnType = MWDataManager.ReturnType.DataTable;
                        //_dbManSetup.ResultsTableName = "DefData";
                        //_dbManSetup.ExecuteInstruction();
                    }


                    _dbManSetup.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManSetup.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManSetup.ResultsTableName = "DefData";
                    _dbManSetup.ExecuteInstruction();



                    Neil1 = _dbManSetup.ResultsDataTable;


                    tonsplan = Convert.ToDecimal(Neil1.Rows[0]["TonsCall"].ToString());
                    tonsact = Convert.ToDecimal(Neil1.Rows[0]["TonsAct"].ToString());
                    AchPrec = Math.Round(Convert.ToDecimal(Neil1.Rows[0]["aa"].ToString()) * 100, 2);
                   

                    string aa = Neil1.Rows[0]["cata"].ToString();


                    


                }
                else
                {
                    if (((Showlabel.Text + "           ").Substring(0, 6) == "020012") || (Showlabel.Text == "02002") || ((Showlabel.Text + "           ").Substring(0, 5) == "02020"))
                    {
                        MWDataManager.clsDataAccess _dbManSetup = new MWDataManager.clsDataAccess();
                        _dbManSetup.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _dbManSetup.SqlStatement = "select aa, s2TonsCall TonsCall, s2tonsact tonsact, cata, ProdDept, WShop, TSD, Shaft, BMR, smelter, conc, ChiefTech from (select \r\n" +
                                                    " (S2tonsact+0.001)/(S2tonscall+0.001) aa, \r\n" +
                                                    " * from mineware.[dbo].[tbl_BCS_Eng_FactorNew1] \r\n" +
                                                    " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') a \r\n" +
                                                    " , \r\n" +
                                                    " (select top(1) cat cata from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveCat] \r\n" +
                                                     " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and  \r\n" +
                                                    "  perc < (select  \r\n" +
                                                    " (s2tonsact+0.001)/(s2tonscall+0.001)*100 aa from mineware.[dbo].[tbl_BCS_Eng_FactorNew1] \r\n" +
                                                    "  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') order by perc desc) b \r\n";
                        _dbManSetup.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbManSetup.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dbManSetup.ResultsTableName = "DefData";
                        _dbManSetup.ExecuteInstruction();

                        Neil1 = _dbManSetup.ResultsDataTable;


                        tonsplan = Convert.ToDecimal(Neil1.Rows[0]["TonsCall"].ToString());
                        tonsact = Convert.ToDecimal(Neil1.Rows[0]["TonsAct"].ToString());
                        AchPrec = Math.Round(Convert.ToDecimal(Neil1.Rows[0]["aa"].ToString()) * 100, 2);
                        string aa = Neil1.Rows[0]["cata"].ToString();
                    }
                    else
                    {

                        MWDataManager.clsDataAccess _dbManSetup = new MWDataManager.clsDataAccess();
                        _dbManSetup.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _dbManSetup.SqlStatement = "select aa, convert(decimal(18,0),(s1TonsCall + s2TonsCall)/2) TonsCall, convert(decimal(18,0),(s1tonsact + s2tonsact)/2) tonsact, cata, ProdDept, WShop, TSD, Shaft, BMR, smelter, conc, ChiefTech from (select \r\n" +
                                                    " (s1tonsact + s2tonsact)/((s1TonsCall + s2TonsCall)+0.001) aa, \r\n" +
                                                    " * from mineware.[dbo].[tbl_BCS_Eng_FactorNew1] \r\n" +
                                                    " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') a \r\n" +
                                                    " , \r\n" +
                                                    " (select top(1) cat cata from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveCat] \r\n" +
                                                     " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and  \r\n" +
                                                    "  perc < (select  \r\n" +
                                                    " (s1tonsact + s2tonsact)/((s1TonsCall + s2TonsCall)+0.001)*100 aa from mineware.[dbo].[tbl_BCS_Eng_FactorNew1] \r\n" +
                                                    "  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') order by perc desc) b \r\n";
                        _dbManSetup.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbManSetup.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dbManSetup.ResultsTableName = "DefData";
                        _dbManSetup.ExecuteInstruction();

                        Neil1 = _dbManSetup.ResultsDataTable;


                        tonsplan = Convert.ToDecimal(Neil1.Rows[0]["TonsCall"].ToString());
                        tonsact = Convert.ToDecimal(Neil1.Rows[0]["TonsAct"].ToString());
                        AchPrec = Math.Round(Convert.ToDecimal(Neil1.Rows[0]["aa"].ToString()) * 100, 2);
                        string aa = Neil1.Rows[0]["cata"].ToString();

                    }
                }



                }


                if (PlantRB.Checked == true)
                {
                    if (BMRRB.Checked == true)
                    {
                        MWDataManager.clsDataAccess _dbManSetup = new MWDataManager.clsDataAccess();
                        _dbManSetup.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _dbManSetup.SqlStatement = "select aa, BMRCall TonsCall, BMRAct tonsact, cata, ProdDept, WShop, TSD, Shaft, BMR, smelter, conc, ChiefTech from (select \r\n" +
                                                    " (BMRAct+0.001)/(BMRCall+0.001) aa, \r\n" +
                                                    " * from mineware.[dbo].[tbl_BCS_Eng_FactorNew1] \r\n" +
                                                    " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') a \r\n" +
                                                    " , \r\n" +
                                                    " (select top(1) cat cata from mineware.[dbo].tbl_BCS_Eng_SurfaceIncentiveCatPlant \r\n" +
                                                     " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and  \r\n" +
                                                    "  perc < (select  \r\n" +
                                                    " (BMRAct+0.001)/(BMRCall+0.001)*100 aa from mineware.[dbo].[tbl_BCS_Eng_FactorNew1] \r\n" +
                                                    "  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') order by perc desc) b \r\n";
                        //_dbManSetup.SqlStatement = "select aa, smeltCall TonsCall, smeltAct tonsact, cata, ProdDept, WShop, TSD, Shaft, BMR, smelter, conc, ChiefTech from (select \r\n" +
                        //                            " smeltAct/(smeltCall+0.001) aa, \r\n" +
                        //                            " * from mineware.[dbo].[tbl_BCS_Eng_FactorNew] \r\n" +
                        //                            " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') a \r\n" +
                        //                            " , \r\n" +
                        //                            " (select top(1) cat cata from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveCatPlant] \r\n" +
                        //                             " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and  \r\n" +
                        //                            "  perc < (select  \r\n" +
                        //                            " smeltAct/(smeltCall+0.001)*100 aa from mineware.[dbo].[tbl_BCS_Eng_FactorNew] \r\n" +
                        //                            "  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') order by perc desc) b \r\n";

                        //_dbManSetup.SqlStatement = "select aa, smeltAct TonsCall, smeltAct tonsact, cata, ProdDept, WShop, TSD, Shaft, BMR, smelter, conc, ChiefTech from (select \r\n" +
                        //                            " (smeltAct+0.001)/(smelttonscall+0.001) aa, \r\n" +
                        //                            " * from mineware.[dbo].[tbl_BCS_Eng_FactorNew1] \r\n" +
                        //                            " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') a \r\n" +
                        //                            " , \r\n" +
                        //                            " (select top(1) cat cata from mineware.[dbo].tbl_BCS_Eng_SurfaceIncentiveCatPlant \r\n" +
                        //                             " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and  \r\n" +
                        //                            "  perc < (select  \r\n" +
                        //                            " (smeltAct+0.001)/(smelttonscall+0.001)*100 aa from mineware.[dbo].[tbl_BCS_Eng_FactorNew1] \r\n" +
                        //                            "  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') order by perc desc) b \r\n";
                        _dbManSetup.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbManSetup.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dbManSetup.ResultsTableName = "DefData";
                        _dbManSetup.ExecuteInstruction();

                        Neil1 = _dbManSetup.ResultsDataTable;


                        tonsplan = Convert.ToDecimal(Neil1.Rows[0]["TonsCall"].ToString());
                        tonsact = Convert.ToDecimal(Neil1.Rows[0]["TonsAct"].ToString());
                        AchPrec = Math.Round(Convert.ToDecimal(Neil1.Rows[0]["aa"].ToString()) * 100, 2);
                        string aa = Neil1.Rows[0]["cata"].ToString();
                    }

                    if (SmelterRB.Checked == true)
                    {
                        MWDataManager.clsDataAccess _dbManSetup = new MWDataManager.clsDataAccess();
                        _dbManSetup.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        //_dbManSetup.SqlStatement = "select aa, smeltCall TonsCall, smeltAct tonsact, cata, ProdDept, WShop, TSD, Shaft, BMR, smelter, conc, ChiefTech from (select \r\n" +
                        //                            " smeltAct/(smeltCall+0.001) aa, \r\n" +
                        //                            " * from mineware.[dbo].[tbl_BCS_Eng_FactorNew] \r\n" +
                        //                            " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') a \r\n" +
                        //                            " , \r\n" +
                        //                            " (select top(1) cat cata from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveCatPlant] \r\n" +
                        //                             " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and  \r\n" +
                        //                            "  perc < (select  \r\n" +
                        //                            " smeltAct/(smeltCall+0.001)*100 aa from mineware.[dbo].[tbl_BCS_Eng_FactorNew] \r\n" +
                        //                            "  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') order by perc desc) b \r\n";
                        _dbManSetup.SqlStatement = "select aa, smeltCall TonsCall, smeltAct tonsact, cata, ProdDept, WShop, TSD, Shaft, BMR, smelter, conc, ChiefTech from (select \r\n" +
                                                    " (smelttonsAct+0.001)/(smelttonscall+0.001) aa, \r\n" +
                                                    " * from mineware.[dbo].[tbl_BCS_Eng_FactorNew1] \r\n" +
                                                    " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') a \r\n" +
                                                    " , \r\n" +
                                                    " (select top(1) cat cata from mineware.[dbo].tbl_BCS_Eng_SurfaceIncentiveCatPlant \r\n" +
                                                     " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and  \r\n" +
                                                    "  perc < (select  \r\n" +
                                                    " (smelttonsAct+0.001)/(smelttonscall+0.001)*100 aa from mineware.[dbo].[tbl_BCS_Eng_FactorNew1] \r\n" +
                                                    "  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') order by perc desc) b \r\n";

                        _dbManSetup.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbManSetup.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dbManSetup.ResultsTableName = "DefData";
                        _dbManSetup.ExecuteInstruction();

                        Neil1 = _dbManSetup.ResultsDataTable;


                        tonsplan = Convert.ToDecimal(Neil1.Rows[0]["TonsCall"].ToString());
                        tonsact = Convert.ToDecimal(Neil1.Rows[0]["TonsAct"].ToString());
                        AchPrec = Math.Round(Convert.ToDecimal(Neil1.Rows[0]["aa"].ToString()) * 100, 2);
                        string aa = Neil1.Rows[0]["cata"].ToString();
                    }


                    if (ConcRB.Checked == true)
                    {
                        MWDataManager.clsDataAccess _dbManSetup = new MWDataManager.clsDataAccess();
                        _dbManSetup.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _dbManSetup.SqlStatement = "select aa, conctonsCall TonsCall, conctonsAct tonsact, cata, ProdDept, WShop, TSD, Shaft, BMR, smelter, conc, ChiefTech from (select \r\n" +
                                                    " (conctonsAct+0.001)/(conctonsCall+0.001) aa, \r\n" +
                                                    " * from mineware.[dbo].[tbl_BCS_Eng_FactorNew1] \r\n" +
                                                    " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') a \r\n" +
                                                    " , \r\n" +
                                                    " (select top(1) cat cata from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveCatPlant] \r\n" +
                                                     " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and  \r\n" +
                                                    "  perc < (select  \r\n" +
                                                    " (conctonsAct+0.001)/(conctonsCall+0.001)*100 aa from mineware.[dbo].[tbl_BCS_Eng_FactorNew1] \r\n" +
                                                    "  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') order by perc desc) b \r\n";
                        _dbManSetup.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbManSetup.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dbManSetup.ResultsTableName = "DefData";
                        _dbManSetup.ExecuteInstruction();

                        Neil1 = _dbManSetup.ResultsDataTable;


                        tonsplan = Convert.ToDecimal(Neil1.Rows[0]["TonsCall"].ToString());
                        tonsact = Convert.ToDecimal(Neil1.Rows[0]["TonsAct"].ToString());
                        AchPrec = Math.Round(Convert.ToDecimal(Neil1.Rows[0]["aa"].ToString()) * 100, 2);
                        string aa = Neil1.Rows[0]["cata"].ToString();
                    }

                }



            if (ProdRB.Checked == true)
                if (radioButton4.Checked == true)
                    Dept = Math.Round(Convert.ToDecimal(Neil1.Rows[0]["ProdDept"].ToString()), 2);

            if (ProdRB.Checked == true)
                if (radioButton3.Checked == true)
                    Dept = Math.Round(Convert.ToDecimal(Neil1.Rows[0]["WShop"].ToString()) , 2);

            if (ProdRB.Checked == true)
                if (radioButton2.Checked == true)
                    Dept = Math.Round(Convert.ToDecimal(Neil1.Rows[0]["TSD"].ToString()) , 2);

            if (ShaftRB.Checked == true)                
                    Dept = Math.Round(Convert.ToDecimal(Neil1.Rows[0]["Shaft"].ToString()) , 2);

            if (PlantRB.Checked == true)
                if (BMRRB.Checked == true)
                    Dept = Math.Round(Convert.ToDecimal(Neil1.Rows[0]["BMR"].ToString()) , 2);

            if (PlantRB.Checked == true)
                if (SmelterRB.Checked == true)
                    Dept = Math.Round(Convert.ToDecimal(Neil1.Rows[0]["smelter"].ToString()) , 2);

            if (PlantRB.Checked == true)
                if (ConcRB.Checked == true)
                    Dept = Math.Round(Convert.ToDecimal(Neil1.Rows[0]["conc"].ToString()) , 2);

            if (PlantRB.Checked == true)
                if (ChiefTechRB.Checked == true)
                    Dept = Math.Round(Convert.ToDecimal(Neil1.Rows[0]["ChiefTech"].ToString()) , 2);




             MWDataManager.clsDataAccess _dbManSetup2 = new MWDataManager.clsDataAccess();
            _dbManSetup2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManSetup2.SqlStatement = " select 0 aa";

            if (ProdRB.Checked == true)
            {
                

                if (Neil1.Rows[0]["cata"].ToString() == "1")
                    _dbManSetup2.SqlStatement = "select top(1)  cat1 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc  ";
                if (Neil1.Rows[0]["cata"].ToString() == "2")
                    _dbManSetup2.SqlStatement = "select top(1)  cat2 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                if (Neil1.Rows[0]["cata"].ToString() == "3")
                    _dbManSetup2.SqlStatement = "select top(1)  cat3 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                if (Neil1.Rows[0]["cata"].ToString() == "4")
                    _dbManSetup2.SqlStatement = "select top(1)  cat4 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                if (Neil1.Rows[0]["cata"].ToString() == "5")
                    _dbManSetup2.SqlStatement = "select top(1)  cat5 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                if (Neil1.Rows[0]["cata"].ToString() == "6")
                    _dbManSetup2.SqlStatement = "select top(1)  cat6 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                if (Neil1.Rows[0]["cata"].ToString() == "7")
                    _dbManSetup2.SqlStatement = "select top(1)  cat7 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                if (Neil1.Rows[0]["cata"].ToString() == "8")
                    _dbManSetup2.SqlStatement = "select top(1)  cat8 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                if (Neil1.Rows[0]["cata"].ToString() == "9")
                    _dbManSetup2.SqlStatement = "select top(1)  cat9 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                if (Neil1.Rows[0]["cata"].ToString() == "10")
                    _dbManSetup2.SqlStatement = "select top(1)  cat10 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                if (Neil1.Rows[0]["cata"].ToString() == "11")
                    _dbManSetup2.SqlStatement = "select top(1)  cat11 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                if (Neil1.Rows[0]["cata"].ToString() == "12")
                    _dbManSetup2.SqlStatement = "select top(1)  cat12 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                if (Neil1.Rows[0]["cata"].ToString() == "13")
                    _dbManSetup2.SqlStatement = "select top(1)  cat13 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                if (Neil1.Rows[0]["cata"].ToString() == "14")
                    _dbManSetup2.SqlStatement = "select top(1)  cat14 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                if (Neil1.Rows[0]["cata"].ToString() == "15")
                    _dbManSetup2.SqlStatement = "select top(1)  cat15 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                if (Neil1.Rows[0]["cata"].ToString() == "16")
                    _dbManSetup2.SqlStatement = "select top(1)  cat16 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                if (Neil1.Rows[0]["cata"].ToString() == "17")
                    _dbManSetup2.SqlStatement = "select top(1)  cat17 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                if (Neil1.Rows[0]["cata"].ToString() == "18")
                    _dbManSetup2.SqlStatement = "select top(1)  cat18 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                if (Neil1.Rows[0]["cata"].ToString() == "19")
                    _dbManSetup2.SqlStatement = "select top(1)  cat19 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                if (Neil1.Rows[0]["cata"].ToString() == "20")
                    _dbManSetup2.SqlStatement = "select top(1)  cat20 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";

                if (radioButton3.Checked == true)
                    if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) == "201606")
                        _dbManSetup2.SqlStatement = "select 157 aa ";

            }

            if (ShaftRB.Checked == true)
            {
                if (((Showlabel.Text + "           ").Substring(0, 6) == "020011") || (Showlabel.Text == "02001") || ((Showlabel.Text + "           ").Substring(0, 5) == "02011") || (Showlabel.Text == "02121") || (Showlabel.Text == "0200141") || (Showlabel.Text == "020014"))
                {

                    string UseSmooth = "N";

                    if ((Showlabel.Text + "           ").Substring(0, 7) == "020011V" && Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) >= 201705)
                        UseSmooth = "Y";
                    
                    
                    if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) == "201606")
                        _dbManSetup2.SqlStatement = "select 54 aa ";


                    if (UseSmooth == "Y")
                    {
                        if (Neil1.Rows[0]["cata"].ToString() == "1")
                            _dbManSetup2.SqlStatement = "select top(1)  cat1 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "2")
                            _dbManSetup2.SqlStatement = "select top(1)  cat2 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "3")
                            _dbManSetup2.SqlStatement = "select top(1)  cat3 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "4")
                            _dbManSetup2.SqlStatement = "select top(1)  cat4 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "5")
                            _dbManSetup2.SqlStatement = "select top(1)  cat5 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "6")
                            _dbManSetup2.SqlStatement = "select top(1)  cat6 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "7")
                            _dbManSetup2.SqlStatement = "select top(1)  cat7 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "8")
                            _dbManSetup2.SqlStatement = "select top(1)  cat8 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "9")
                            _dbManSetup2.SqlStatement = "select top(1)  cat9 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "10")
                            _dbManSetup2.SqlStatement = "select top(1)  cat10 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "11")
                            _dbManSetup2.SqlStatement = "select top(1)  cat11 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "12")
                            _dbManSetup2.SqlStatement = "select top(1)  cat12 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "13")
                            _dbManSetup2.SqlStatement = "select top(1)  cat13 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "14")
                            _dbManSetup2.SqlStatement = "select top(1)  cat14 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "15")
                            _dbManSetup2.SqlStatement = "select top(1)  cat15 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "16")
                            _dbManSetup2.SqlStatement = "select top(1)  cat16 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "17")
                            _dbManSetup2.SqlStatement = "select top(1)  cat17 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "18")
                            _dbManSetup2.SqlStatement = "select top(1)  cat18 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "19")
                            _dbManSetup2.SqlStatement = "select top(1)  cat19 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "20")
                            _dbManSetup2.SqlStatement = "select top(1)  cat20 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons <= '" + tonsact + "'  order by tons desc   ";


                    }
                    else
                    {


                        if (Neil1.Rows[0]["cata"].ToString() == "1")
                            _dbManSetup2.SqlStatement = "select top(1)  cat1 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and oneshaft <= '" + tonsact + "'  order by oneshaft desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "2")
                            _dbManSetup2.SqlStatement = "select top(1)  cat2 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and oneshaft <= '" + tonsact + "'   order by oneshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "3")
                            _dbManSetup2.SqlStatement = "select top(1)  cat3 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and oneshaft <= '" + tonsact + "'   order by oneshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "4")
                            _dbManSetup2.SqlStatement = "select top(1)  cat4 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and oneshaft <= '" + tonsact + "'   order by oneshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "5")
                            _dbManSetup2.SqlStatement = "select top(1)  cat5 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and oneshaft <= '" + tonsact + "'   order by oneshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "6")
                            _dbManSetup2.SqlStatement = "select top(1)  cat6 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and oneshaft <= '" + tonsact + "'   order by oneshaft desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "7")
                            _dbManSetup2.SqlStatement = "select top(1)  cat7 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and oneshaft <= '" + tonsact + "'   order by oneshaft desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "8")
                            _dbManSetup2.SqlStatement = "select top(1)  cat8 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and oneshaft <= '" + tonsact + "'   order by oneshaft desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "9")
                            _dbManSetup2.SqlStatement = "select top(1)  cat9 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and oneshaft <= '" + tonsact + "'  order by oneshaft desc    ";
                        if (Neil1.Rows[0]["cata"].ToString() == "10")
                            _dbManSetup2.SqlStatement = "select top(1)  cat10 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and oneshaft <= '" + tonsact + "'  order by oneshaft desc    ";
                        if (Neil1.Rows[0]["cata"].ToString() == "11")
                            _dbManSetup2.SqlStatement = "select top(1)  cat11 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and oneshaft <= '" + tonsact + "'  order by oneshaft desc    ";
                        if (Neil1.Rows[0]["cata"].ToString() == "12")
                            _dbManSetup2.SqlStatement = "select top(1)  cat12 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and oneshaft <= '" + tonsact + "'  order by oneshaft desc    ";
                        if (Neil1.Rows[0]["cata"].ToString() == "13")
                            _dbManSetup2.SqlStatement = "select top(1)  cat13 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and oneshaft <= '" + tonsact + "'  order by oneshaft desc    ";
                        if (Neil1.Rows[0]["cata"].ToString() == "14")
                            _dbManSetup2.SqlStatement = "select top(1)  cat14 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and oneshaft <= '" + tonsact + "'  order by oneshaft desc    ";
                        if (Neil1.Rows[0]["cata"].ToString() == "15")
                            _dbManSetup2.SqlStatement = "select top(1)  cat15 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and oneshaft <= '" + tonsact + "'  order by oneshaft desc    ";
                        if (Neil1.Rows[0]["cata"].ToString() == "16")
                            _dbManSetup2.SqlStatement = "select top(1)  cat16 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and oneshaft <= '" + tonsact + "'  order by oneshaft desc    ";
                        if (Neil1.Rows[0]["cata"].ToString() == "17")
                            _dbManSetup2.SqlStatement = "select top(1)  cat17 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and oneshaft <= '" + tonsact + "'  order by oneshaft desc    ";
                        if (Neil1.Rows[0]["cata"].ToString() == "18")
                            _dbManSetup2.SqlStatement = "select top(1)  cat18 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and oneshaft <= '" + tonsact + "'  order by oneshaft desc    ";
                        if (Neil1.Rows[0]["cata"].ToString() == "19")
                            _dbManSetup2.SqlStatement = "select top(1)  cat19 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and oneshaft <= '" + tonsact + "'  order by oneshaft desc    ";
                        if (Neil1.Rows[0]["cata"].ToString() == "20")
                            _dbManSetup2.SqlStatement = "select top(1)  cat20 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and oneshaft >= '" + tonsact + "'  order by oneshaft desc    ";
                    }
                }
                else
                {
                    if (((Showlabel.Text + "           ").Substring(0, 6) == "020012") || (Showlabel.Text == "02002") || ((Showlabel.Text + "           ").Substring(0, 5) == "02020"))
                    {
                        

                        if (Neil1.Rows[0]["cata"].ToString() == "1")
                            _dbManSetup2.SqlStatement = "select top(1)  cat1 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and twoshaft <= '" + tonsact + "'  order by twoshaft desc    ";
                        if (Neil1.Rows[0]["cata"].ToString() == "2")
                            _dbManSetup2.SqlStatement = "select top(1)  cat2 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and twoshaft <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "3")
                            _dbManSetup2.SqlStatement = "select top(1)  cat3 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and twoshaft <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "4")
                            _dbManSetup2.SqlStatement = "select top(1)  cat4 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and twoshaft <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "5")
                            _dbManSetup2.SqlStatement = "select top(1)  cat5 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and twoshaft <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "6")
                            _dbManSetup2.SqlStatement = "select top(1)  cat6 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and twoshaft <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "7")
                            _dbManSetup2.SqlStatement = "select top(1)  cat7 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and twoshaft <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "8")
                            _dbManSetup2.SqlStatement = "select top(1)  cat8 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and twoshaft <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "9")
                            _dbManSetup2.SqlStatement = "select top(1)  cat9 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and twoshaft <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "10")
                            _dbManSetup2.SqlStatement = "select top(1)  cat10 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and twoshaft <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "11")
                            _dbManSetup2.SqlStatement = "select top(1)  cat11 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and twoshaft <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "12")
                            _dbManSetup2.SqlStatement = "select top(1)  cat12 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and twoshaft <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "13")
                            _dbManSetup2.SqlStatement = "select top(1)  cat13 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and twoshaft <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "14")
                            _dbManSetup2.SqlStatement = "select top(1)  cat14 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and twoshaft <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "15")
                            _dbManSetup2.SqlStatement = "select top(1)  cat15 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and twoshaft <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "16")
                            _dbManSetup2.SqlStatement = "select top(1)  cat16 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and twoshaft <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "17")
                            _dbManSetup2.SqlStatement = "select top(1)  cat17 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and twoshaft <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "18")
                            _dbManSetup2.SqlStatement = "select top(1)  cat18 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and twoshaft <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "19")
                            _dbManSetup2.SqlStatement = "select top(1)  cat19 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and twoshaft <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "20")
                            _dbManSetup2.SqlStatement = "select top(1)  cat20 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and twoshaft <= '" + tonsact + "'  order by twoshaft desc     ";

                        if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) == "201606")
                            _dbManSetup2.SqlStatement = "select 345 aa ";
                    }
                    else
                    {
                        if (Neil1.Rows[0]["cata"].ToString() == "1")
                            _dbManSetup2.SqlStatement = "select top(1)  cat1 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and (oneshaft+twoshaft)/2 <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "2")
                            _dbManSetup2.SqlStatement = "select top(1)  cat2 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and (oneshaft+twoshaft)/2 <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "3")
                            _dbManSetup2.SqlStatement = "select top(1)  cat3 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and (oneshaft+twoshaft)/2 <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "4")
                            _dbManSetup2.SqlStatement = "select top(1)  cat4 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and (oneshaft+twoshaft)/2 <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "5")
                            _dbManSetup2.SqlStatement = "select top(1)  cat5 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and (oneshaft+twoshaft)/2 <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "6")
                            _dbManSetup2.SqlStatement = "select top(1)  cat6 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and (oneshaft+twoshaft)/2 <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "7")
                            _dbManSetup2.SqlStatement = "select top(1)  cat7 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and (oneshaft+twoshaft)/2 <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "8")
                            _dbManSetup2.SqlStatement = "select top(1)  cat8 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and (oneshaft+twoshaft)/2 <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "9")
                            _dbManSetup2.SqlStatement = "select top(1)  cat9 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and (oneshaft+twoshaft)/2 <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "10")
                            _dbManSetup2.SqlStatement = "select top(1)  cat10 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and (oneshaft+twoshaft)/2 <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "11")
                            _dbManSetup2.SqlStatement = "select top(1)  cat11 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and (oneshaft+twoshaft)/2 <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "12")
                            _dbManSetup2.SqlStatement = "select top(1)  cat12 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and (oneshaft+twoshaft)/2 <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "13")
                            _dbManSetup2.SqlStatement = "select top(1)  cat13 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and (oneshaft+twoshaft)/2 <= '" + tonsact + "'  order by twoshaft desc     ";
                        if (Neil1.Rows[0]["cata"].ToString() == "14")
                            _dbManSetup2.SqlStatement = "select top(1)  cat14 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and (oneshaft+twoshaft)/2 <= '" + tonsact + "'  order by twoshaft desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "15")
                            _dbManSetup2.SqlStatement = "select top(1)  cat15 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and (oneshaft+twoshaft)/2 <= '" + tonsact + "'  order by twoshaft desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "16")
                            _dbManSetup2.SqlStatement = "select top(1)  cat16 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and (oneshaft+twoshaft)/2 <= '" + tonsact + "'  order by twoshaft desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "17")
                            _dbManSetup2.SqlStatement = "select top(1)  cat17 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and (oneshaft+twoshaft)/2 <= '" + tonsact + "'  order by twoshaft desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "18")
                            _dbManSetup2.SqlStatement = "select top(1)  cat18 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and (oneshaft+twoshaft)/2 <= '" + tonsact + "'  order by twoshaft desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "19")
                            _dbManSetup2.SqlStatement = "select top(1)  cat19 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and (oneshaft+twoshaft)/2 <= '" + tonsact + "'  order by twoshaft desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "20")
                            _dbManSetup2.SqlStatement = "select top(1)  cat20 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetail] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and (oneshaft+twoshaft)/2 <= '" + tonsact + "'   order by twoshaft desc  ";

                    }


                }


            }
                if (PlantRB.Checked == true)
                {
                    if (BMRRB.Checked == true)
                    {
                        if (Neil1.Rows[0]["cata"].ToString() == "1")
                            _dbManSetup2.SqlStatement = "select top(1)  cat1 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and bmrkg <= '" + tonsact + "'  order by bmrkg desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "2")
                            _dbManSetup2.SqlStatement = "select top(1)  cat2 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and bmrkg <= '" + tonsact + "'  order by bmrkg desc    ";
                        if (Neil1.Rows[0]["cata"].ToString() == "3")
                            _dbManSetup2.SqlStatement = "select top(1)  cat3 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and bmrkg <= '" + tonsact + "'  order by bmrkg desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "4")
                            _dbManSetup2.SqlStatement = "select top(1)  cat4 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and bmrkg <= '" + tonsact + "'  order by bmrkg desc    ";
                        if (Neil1.Rows[0]["cata"].ToString() == "5")
                            _dbManSetup2.SqlStatement = "select top(1)  cat5 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and bmrkg <= '" + tonsact + "'  order by bmrkg desc    ";
                        if (Neil1.Rows[0]["cata"].ToString() == "6")
                            _dbManSetup2.SqlStatement = "select top(1)  cat6 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and bmrkg <= '" + tonsact + "'  order by bmrkg desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "7")
                            _dbManSetup2.SqlStatement = "select top(1)  cat7 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and bmrkg <= '" + tonsact + "'  order by bmrkg desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "8")
                            _dbManSetup2.SqlStatement = "select top(1)  cat8 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and bmrkg <= '" + tonsact + "'  order by bmrkg desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "9")
                            _dbManSetup2.SqlStatement = "select top(1)  cat9 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and bmrkg <= '" + tonsact + "'  order by bmrkg desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "10")
                            _dbManSetup2.SqlStatement = "select top(1)  cat10 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and bmrkg <= '" + tonsact + "'  order by bmrkg desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "11")
                            _dbManSetup2.SqlStatement = "select top(1)  cat11 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and bmrkg <= '" + tonsact + "'  order by bmrkg desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "12")
                            _dbManSetup2.SqlStatement = "select top(1)  cat12 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and bmrkg <= '" + tonsact + "'  order by bmrkg desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "13")
                            _dbManSetup2.SqlStatement = "select top(1)  cat13 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and bmrkg <= '" + tonsact + "'  order by bmrkg desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "14")
                            _dbManSetup2.SqlStatement = "select top(1)  cat14 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and bmrkg <= '" + tonsact + "'  order by bmrkg desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "15")
                            _dbManSetup2.SqlStatement = "select top(1)  cat15 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and bmrkg <= '" + tonsact + "'  order by bmrkg desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "16")
                            _dbManSetup2.SqlStatement = "select top(1)  cat16 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and bmrkg <= '" + tonsact + "'  order by bmrkg desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "17")
                            _dbManSetup2.SqlStatement = "select top(1)  cat17 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and bmrkg <= '" + tonsact + "'  order by bmrkg desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "18")
                            _dbManSetup2.SqlStatement = "select top(1)  cat18 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and bmrkg <= '" + tonsact + "'  order by bmrkg desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "19")
                            _dbManSetup2.SqlStatement = "select top(1)  cat19 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and bmrkg <= '" + tonsact + "'  order by bmrkg desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "20")
                            _dbManSetup2.SqlStatement = "select top(1)  cat20 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and bmrkg <= '" + tonsact + "'  order by bmrkg desc   ";


                    }


                    if (SmelterRB.Checked == true)
                    {
                        if (Neil1.Rows[0]["cata"].ToString() == "1")
                            _dbManSetup2.SqlStatement = "select top(1)  cat1 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc   ";
                        if (Neil1.Rows[0]["cata"].ToString() == "2")
                            _dbManSetup2.SqlStatement = "select top(1)  cat2 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "3")
                            _dbManSetup2.SqlStatement = "select top(1)  cat3 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'   order by smelter desc ";
                        if (Neil1.Rows[0]["cata"].ToString() == "4")
                            _dbManSetup2.SqlStatement = "select top(1)  cat4 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "5")
                            _dbManSetup2.SqlStatement = "select top(1)  cat5 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "6")
                            _dbManSetup2.SqlStatement = "select top(1)  cat6 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "7")
                            _dbManSetup2.SqlStatement = "select top(1)  cat7 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "8")
                            _dbManSetup2.SqlStatement = "select top(1)  cat8 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "9")
                            _dbManSetup2.SqlStatement = "select top(1)  cat9 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "10")
                            _dbManSetup2.SqlStatement = "select top(1)  cat10 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "11")
                            _dbManSetup2.SqlStatement = "select top(1)  cat11 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "12")
                            _dbManSetup2.SqlStatement = "select top(1)  cat12 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "13")
                            _dbManSetup2.SqlStatement = "select top(1)  cat13 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "14")
                            _dbManSetup2.SqlStatement = "select top(1)  cat14 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "15")
                            _dbManSetup2.SqlStatement = "select top(1)  cat15 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "16")
                            _dbManSetup2.SqlStatement = "select top(1)  cat16 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "17")
                            _dbManSetup2.SqlStatement = "select top(1)  cat17 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "18")
                            _dbManSetup2.SqlStatement = "select top(1)  cat18 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "19")
                            _dbManSetup2.SqlStatement = "select top(1)  cat19 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "20")
                            _dbManSetup2.SqlStatement = "select top(1)  cat20 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";


                    }


                    if (SmelterRB.Checked == true)
                    {
                        if (Neil1.Rows[0]["cata"].ToString() == "1")
                            _dbManSetup2.SqlStatement = "select top(1)  cat1 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "2")
                            _dbManSetup2.SqlStatement = "select top(1)  cat2 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "3")
                            _dbManSetup2.SqlStatement = "select top(1)  cat3 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "4")
                            _dbManSetup2.SqlStatement = "select top(1)  cat4 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "5")
                            _dbManSetup2.SqlStatement = "select top(1)  cat5 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'   order by smelter desc ";
                        if (Neil1.Rows[0]["cata"].ToString() == "6")
                            _dbManSetup2.SqlStatement = "select top(1)  cat6 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'   order by smelter desc ";
                        if (Neil1.Rows[0]["cata"].ToString() == "7")
                            _dbManSetup2.SqlStatement = "select top(1)  cat7 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'   order by smelter desc ";
                        if (Neil1.Rows[0]["cata"].ToString() == "8")
                            _dbManSetup2.SqlStatement = "select top(1)  cat8 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'   order by smelter desc ";
                        if (Neil1.Rows[0]["cata"].ToString() == "9")
                            _dbManSetup2.SqlStatement = "select top(1)  cat9 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'   order by smelter desc ";
                        if (Neil1.Rows[0]["cata"].ToString() == "10")
                            _dbManSetup2.SqlStatement = "select top(1)  cat10 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "11")
                            _dbManSetup2.SqlStatement = "select top(1)  cat11 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "12")
                            _dbManSetup2.SqlStatement = "select top(1)  cat12 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "13")
                            _dbManSetup2.SqlStatement = "select top(1)  cat13 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "14")
                            _dbManSetup2.SqlStatement = "select top(1)  cat14 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "15")
                            _dbManSetup2.SqlStatement = "select top(1)  cat15 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "16")
                            _dbManSetup2.SqlStatement = "select top(1)  cat16 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "17")
                            _dbManSetup2.SqlStatement = "select top(1)  cat17 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "18")
                            _dbManSetup2.SqlStatement = "select top(1)  cat18 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "19")
                            _dbManSetup2.SqlStatement = "select top(1)  cat19 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "20")
                            _dbManSetup2.SqlStatement = "select top(1)  cat20 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and smelter <= '" + tonsact + "'  order by smelter desc  ";


                    }


                    if (ConcRB.Checked == true)
                    {
                        if (Neil1.Rows[0]["cata"].ToString() == "1")
                            _dbManSetup2.SqlStatement = "select top(1)  cat1 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and conctons <= '" + tonsact + "'  order by conctons desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "2")
                            _dbManSetup2.SqlStatement = "select top(1)  cat2 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and conctons <= '" + tonsact + "'  order by conctons desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "3")
                            _dbManSetup2.SqlStatement = "select top(1)  cat3 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and conctons <= '" + tonsact + "'  order by conctons desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "4")
                            _dbManSetup2.SqlStatement = "select top(1)  cat4 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and conctons <= '" + tonsact + "'  order by conctons desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "5")
                            _dbManSetup2.SqlStatement = "select top(1)  cat5 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and conctons <= '" + tonsact + "'  order by conctons desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "6")
                            _dbManSetup2.SqlStatement = "select top(1)  cat6 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and conctons <= '" + tonsact + "'  order by conctons desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "7")
                            _dbManSetup2.SqlStatement = "select top(1)  cat7 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and conctons <= '" + tonsact + "'  order by conctons desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "8")
                            _dbManSetup2.SqlStatement = "select top(1)  cat8 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and conctons <= '" + tonsact + "'  order by conctons desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "9")
                            _dbManSetup2.SqlStatement = "select top(1)  cat9 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and conctons <= '" + tonsact + "'  order by conctons desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "10")
                            _dbManSetup2.SqlStatement = "select top(1)  cat10 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and conctons <= '" + tonsact + "'  order by conctons desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "11")
                            _dbManSetup2.SqlStatement = "select top(1)  cat11 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and conctons <= '" + tonsact + "'  order by conctons desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "12")
                            _dbManSetup2.SqlStatement = "select top(1)  cat12 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and conctons <= '" + tonsact + "'  order by conctons desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "13")
                            _dbManSetup2.SqlStatement = "select top(1)  cat13 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and conctons <= '" + tonsact + "'  order by conctons desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "14")
                            _dbManSetup2.SqlStatement = "select top(1)  cat14 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and conctons <= '" + tonsact + "'  order by conctons desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "15")
                            _dbManSetup2.SqlStatement = "select top(1)  cat15 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and conctons <= '" + tonsact + "'  order by conctons desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "16")
                            _dbManSetup2.SqlStatement = "select top(1)  cat16 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and conctons <= '" + tonsact + "'  order by conctons desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "17")
                            _dbManSetup2.SqlStatement = "select top(1)  cat17 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and conctons <= '" + tonsact + "'  order by conctons desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "18")
                            _dbManSetup2.SqlStatement = "select top(1)  cat18 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and conctons <= '" + tonsact + "'  order by conctons desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "19")
                            _dbManSetup2.SqlStatement = "select top(1)  cat19 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and conctons <= '" + tonsact + "'  order by conctons desc  ";
                        if (Neil1.Rows[0]["cata"].ToString() == "20")
                            _dbManSetup2.SqlStatement = "select top(1)  cat20 aa from mineware.[dbo].[tbl_BCS_Eng_SurfaceIncentiveDetailPlant] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and conctons <= '" + tonsact + "'  order by conctons desc  ";


                    }
                }


            
                
                _dbManSetup2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManSetup2.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManSetup2.ResultsTableName = "DefData1";
            _dbManSetup2.ExecuteInstruction();

            Neil2 = _dbManSetup2.ResultsDataTable;

            incentive = 0;
            if (_dbManSetup2.ResultsDataTable.Rows.Count > 0 )
            incentive = Math.Round(Convert.ToDecimal(Neil2.Rows[0]["aa"].ToString()), 0);


            //ReportDatasetEng1 = new DataSet();
            //ReportDatasetEng1.Tables.Add(_dbManSetup.ResultsDataTable);



            ResetGrid();
            try
            {
                EngBonusPCGrid.Rows.Clear();
               // EngBonusPCGrid.Visible = false;

                EngBonusPCGrid.ColumnCount = 90;

                EngBonusPCGrid.Columns[0].HeaderText = "Industry Number";
                EngBonusPCGrid.Columns[0].Width = 80;

                EngBonusPCGrid.Columns[1].HeaderText = "Name";
                EngBonusPCGrid.Columns[1].Width = 160;

                EngBonusPCGrid.Columns[2].HeaderText = "Org Unit";
                EngBonusPCGrid.Columns[2].Width = 80;

                EngBonusPCGrid.Columns[3].HeaderText = "Day 1";
                EngBonusPCGrid.Columns[3].Width = 50;

                EngBonusPCGrid.Columns[4].HeaderText = "Day 2";
                EngBonusPCGrid.Columns[4].Width = 50;

                EngBonusPCGrid.Columns[5].HeaderText = "Day 3";
                EngBonusPCGrid.Columns[5].Width = 50;

                EngBonusPCGrid.Columns[6].HeaderText = "Day 4";
                EngBonusPCGrid.Columns[6].Width = 50;

                EngBonusPCGrid.Columns[7].HeaderText = "Day 5";
                EngBonusPCGrid.Columns[7].Width = 50;

                EngBonusPCGrid.Columns[8].HeaderText = "Day 6";
                EngBonusPCGrid.Columns[8].Width = 50;

                EngBonusPCGrid.Columns[9].HeaderText = "Day 7";
                EngBonusPCGrid.Columns[9].Width = 50;

                EngBonusPCGrid.Columns[10].HeaderText = "Day 8";
                EngBonusPCGrid.Columns[10].Width = 50;

                EngBonusPCGrid.Columns[11].HeaderText = "Day 9";
                EngBonusPCGrid.Columns[11].Width = 50;

                EngBonusPCGrid.Columns[12].HeaderText = "Day 10";
                EngBonusPCGrid.Columns[12].Width = 50;

                EngBonusPCGrid.Columns[13].HeaderText = "Day 11";
                EngBonusPCGrid.Columns[13].Width = 50;

                EngBonusPCGrid.Columns[14].HeaderText = "Day 12";
                EngBonusPCGrid.Columns[14].Width = 50;

                EngBonusPCGrid.Columns[15].HeaderText = "Day 13";
                EngBonusPCGrid.Columns[15].Width = 50;

                EngBonusPCGrid.Columns[16].HeaderText = "Day 14";
                EngBonusPCGrid.Columns[16].Width = 50;

                EngBonusPCGrid.Columns[17].HeaderText = "Day 15";
                EngBonusPCGrid.Columns[17].Width = 50;

                EngBonusPCGrid.Columns[18].HeaderText = "Day 16";
                EngBonusPCGrid.Columns[18].Width = 50;

                EngBonusPCGrid.Columns[19].HeaderText = "Day 17";
                EngBonusPCGrid.Columns[19].Width = 50;

                EngBonusPCGrid.Columns[20].HeaderText = "Day 18";
                EngBonusPCGrid.Columns[20].Width = 50;

                EngBonusPCGrid.Columns[21].HeaderText = "Day 19";
                EngBonusPCGrid.Columns[21].Width = 50;

                EngBonusPCGrid.Columns[22].HeaderText = "Day 20";
                EngBonusPCGrid.Columns[22].Width = 50;

                EngBonusPCGrid.Columns[23].HeaderText = "Day 21";
                EngBonusPCGrid.Columns[23].Width = 50;

                EngBonusPCGrid.Columns[24].HeaderText = "Day 22";
                EngBonusPCGrid.Columns[24].Width = 50;

                EngBonusPCGrid.Columns[25].HeaderText = "Day 23";
                EngBonusPCGrid.Columns[25].Width = 50;

                EngBonusPCGrid.Columns[26].HeaderText = "Day 24";
                EngBonusPCGrid.Columns[26].Width = 50;

                EngBonusPCGrid.Columns[27].HeaderText = "Day 25";
                EngBonusPCGrid.Columns[27].Width = 50;

                EngBonusPCGrid.Columns[28].HeaderText = "Day 26";
                EngBonusPCGrid.Columns[28].Width = 50;

                EngBonusPCGrid.Columns[29].HeaderText = "Day 27";
                EngBonusPCGrid.Columns[29].Width = 50;

                EngBonusPCGrid.Columns[30].HeaderText = "Day 28";
                EngBonusPCGrid.Columns[30].Width = 50;

                EngBonusPCGrid.Columns[31].HeaderText = "Day 29";
                EngBonusPCGrid.Columns[31].Width = 50;

                EngBonusPCGrid.Columns[32].HeaderText = "Day 30";
                EngBonusPCGrid.Columns[32].Width = 50;

                EngBonusPCGrid.Columns[33].HeaderText = "Day 31";
                EngBonusPCGrid.Columns[33].Width = 50;

                EngBonusPCGrid.Columns[34].HeaderText = "Day 32";
                EngBonusPCGrid.Columns[34].Width = 50;

                EngBonusPCGrid.Columns[35].HeaderText = "Day 33";
                EngBonusPCGrid.Columns[35].Width = 50;

                EngBonusPCGrid.Columns[36].HeaderText = "Day 34";
                EngBonusPCGrid.Columns[36].Width = 50;

                EngBonusPCGrid.Columns[37].HeaderText = "Day 35";
                EngBonusPCGrid.Columns[37].Width = 50;

                EngBonusPCGrid.Columns[38].HeaderText = "Day 36";
                EngBonusPCGrid.Columns[38].Width = 50;

                EngBonusPCGrid.Columns[39].HeaderText = "Day 37";
                EngBonusPCGrid.Columns[39].Width = 50;

                EngBonusPCGrid.Columns[40].HeaderText = "Day 38";
                EngBonusPCGrid.Columns[40].Width = 50;

                EngBonusPCGrid.Columns[41].HeaderText = "Day 39";
                EngBonusPCGrid.Columns[41].Width = 50;

                EngBonusPCGrid.Columns[42].HeaderText = "Day 40";
                EngBonusPCGrid.Columns[42].Width = 50;

                EngBonusPCGrid.Columns[43].HeaderText = "Poss. Shift";
                EngBonusPCGrid.Columns[43].Width = 50;

                EngBonusPCGrid.Columns[44].HeaderText = "Inc Pay";
                EngBonusPCGrid.Columns[44].Width = 50;


                EngBonusPCGrid.Columns[60].HeaderText = "Inc Pay";
                EngBonusPCGrid.Columns[60].Width = 50;

                EngBonusPCGrid.Columns[61].HeaderText = "Fact";
                EngBonusPCGrid.Columns[61].Width = 50;

                EngBonusPCGrid.Columns[62].HeaderText = "LTI Fact";
                EngBonusPCGrid.Columns[62].Width = 50;

                EngBonusPCGrid.Columns[63].HeaderText = "Basic";
                EngBonusPCGrid.Columns[63].Width = 50;

                EngBonusPCGrid.Columns[64].HeaderText = "Bonus Shift";
                EngBonusPCGrid.Columns[64].Width = 50;

                EngBonusPCGrid.Columns[65].HeaderText = "Prorata";
                EngBonusPCGrid.Columns[65].Width = 50;


                EngBonusPCGrid.Columns[66].HeaderText = "AWOP";
                EngBonusPCGrid.Columns[66].Width = 50;

                EngBonusPCGrid.Columns[67].HeaderText = "Sick";
                EngBonusPCGrid.Columns[67].Width = 50;

                EngBonusPCGrid.Columns[68].HeaderText = "AWOPPen";
                EngBonusPCGrid.Columns[68].Width = 50;

                EngBonusPCGrid.Columns[69].HeaderText = "Sick Pen";
                EngBonusPCGrid.Columns[69].Width = 50;

                EngBonusPCGrid.Columns[70].HeaderText = "Tot Pen";
                EngBonusPCGrid.Columns[70].Width = 50;

                EngBonusPCGrid.Columns[71].HeaderText = "Pot";
                EngBonusPCGrid.Columns[71].Width = 50;

                EngBonusPCGrid.Columns[75].HeaderText = "Fin Pay";
                EngBonusPCGrid.Columns[75].Width = 50;

                string sec = "";
                sec = Showlabel.Text + "%";


                if (ProdRB.Checked == true)
                {
                    if (Showlabel.Text.Length == 4)
                        sec = Showlabel.Text ;

                    if (Showlabel.Text.Length == 3)
                        sec = Showlabel.Text;

                    if (Showlabel.Text == "02101")
                        sec = Showlabel.Text;
                }

                if (ShaftRB.Checked == true)
                {
                    if (Showlabel.Text.Length == 4)
                        sec = Showlabel.Text;

                    if (Showlabel.Text.Length == 5)
                        sec = Showlabel.Text;

                    if (Showlabel.Text.Length == 3)
                        sec = Showlabel.Text;

                }


                if (PlantRB.Checked == true)
                {
                    if (Showlabel.Text.Length == 4)
                        sec = Showlabel.Text;

                    if (Showlabel.Text.Length == 3)
                        sec = Showlabel.Text;
                   
                }

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                if (ProdRB.Checked == true)
                {

                    //MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                    _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan1.SqlStatement = " " ;

                    if (radioButton4.Checked == true)
                    {
                        if (Showlabel.Text == "V Gang" )
                            _dbMan1.SqlStatement = _dbMan1.SqlStatement + " exec mineware.[dbo].[sp_BMCS_GetClockings_EngProdNewForUseVGang] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + sec + "', '" + tonsplan + "', '" + tonsact + "', '" + AchPrec + "', '" + incentive + "', '" + Dept + "' ";
                        else
                         _dbMan1.SqlStatement = _dbMan1.SqlStatement + " exec mineware.[dbo].[sp_BMCS_GetClockings_EngProdNewForUse] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + sec + "', '" + tonsplan + "', '" + tonsact + "', '" + AchPrec + "', '" + incentive + "', '" + Dept + "' ";
                    }
                    else
                    {
                        if (NewradioBtn.Checked == false)
                        {
                            _dbMan1.SqlStatement = _dbMan1.SqlStatement + " exec mineware.[dbo].[sp_BMCS_GetClockings] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + sec + "', '" + tonsplan + "', '" + tonsact + "', '" + AchPrec + "', '" + incentive + "', '" + Dept + "' ";
                        }
                        else
                        {
                            if (radioButton2.Checked == true)
                            {
                                string ss = (sec + "            ").Substring(0, 4);
                                if ((sec + "            ").Substring(0, 4) == "036A" || (sec == "0392F%") || (sec == "0482"))
                                {

                                    MWDataManager.clsDataAccess _dbManData = new MWDataManager.clsDataAccess();
                                    _dbManData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                                    _dbManData.SqlStatement = _dbManData.SqlStatement + " select *, convert(decimal(18,2),SqmAct/(SqmCall+0.001) * 100) perc from BMCS_TSDVent_factors where prodmonth =  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'   ";

                                    _dbManData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                                    _dbManData.queryReturnType = MWDataManager.ReturnType.DataTable;
                                    _dbManData.ResultsTableName = "OtherData";
                                    _dbManData.ExecuteInstruction();

                                    if ((sec == "0482"))
                                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " exec mineware.[dbo].[sp_BMCS_GetClockings_EngProdTSDBackFillNoEx] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + sec + "', '" + _dbManData.ResultsDataTable.Rows[0]["SqmCall"].ToString() + "', '" + _dbManData.ResultsDataTable.Rows[0]["SqmAct"].ToString() + "', '" + _dbManData.ResultsDataTable.Rows[0]["perc"].ToString() + "', '" + incentive + "', '" + Dept + "' ";
                                    else
                                        _dbMan1.SqlStatement = _dbMan1.SqlStatement + " exec mineware.[dbo].[sp_BMCS_GetClockings_EngProdTSDBackFill] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + sec + "', '" + _dbManData.ResultsDataTable.Rows[0]["SqmCall"].ToString() + "', '" + _dbManData.ResultsDataTable.Rows[0]["SqmAct"].ToString() + "', '" + _dbManData.ResultsDataTable.Rows[0]["perc"].ToString() + "', '" + incentive + "', '" + Dept + "' ";

                                }
                                else
                                {

                                    if (((sec + "            ").Substring(0, 5) == "039G1") || (sec == "0392D%"))
                                    {
                                        MWDataManager.clsDataAccess _dbManData = new MWDataManager.clsDataAccess();
                                        _dbManData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                                        _dbManData.SqlStatement = _dbManData.SqlStatement + " select *, convert(decimal(18,2),MetresAct/(MetresCall+0.001) * 100) perc from BMCS_TSDVent_factors where prodmonth =  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'   ";

                                        _dbManData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                                        _dbManData.queryReturnType = MWDataManager.ReturnType.DataTable;
                                        _dbManData.ResultsTableName = "OtherData";
                                        _dbManData.ExecuteInstruction();

                                        if ((sec == "0392D%"))
                                        _dbMan1.SqlStatement = _dbMan1.SqlStatement + " exec mineware.[dbo].[sp_BMCS_GetClockings_EngProdTSDNewTechNoEx] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + sec + "', '" + _dbManData.ResultsDataTable.Rows[0]["MetresCall"].ToString() + "', '" + _dbManData.ResultsDataTable.Rows[0]["MetresAct"].ToString() + "', '" + _dbManData.ResultsDataTable.Rows[0]["perc"].ToString() + "', '" + incentive + "', '" + Dept + "' ";
                                        else
                                            _dbMan1.SqlStatement = _dbMan1.SqlStatement + " exec mineware.[dbo].[sp_BMCS_GetClockings_EngProdTSDNewTech] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + sec + "', '" + _dbManData.ResultsDataTable.Rows[0]["MetresCall"].ToString() + "', '" + _dbManData.ResultsDataTable.Rows[0]["MetresAct"].ToString() + "', '" + _dbManData.ResultsDataTable.Rows[0]["perc"].ToString() + "', '" + incentive + "', '" + Dept + "' ";

                                    }
                                    else
                                    {
                                        if ((sec == "0392A%") || (sec == "0392B%") || (sec == "0392C%"))
                                            _dbMan1.SqlStatement = _dbMan1.SqlStatement + " exec mineware.[dbo].[sp_BMCS_GetClockings_EngProdTSDOther1] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + sec + "', '" + tonsplan + "', '" + tonsact + "', '" + AchPrec + "', '" + incentive + "', '" + Dept + "' ";
                                        else
                                        {
                                            if ((sec == "0392D") || (sec == "0482"))
                                                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " exec mineware.[dbo].[sp_BMCS_GetClockings_EngProdTSDOtherNoEx] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + sec + "', '" + tonsplan + "', '" + tonsact + "', '" + AchPrec + "', '" + incentive + "', '" + Dept + "' ";
                                            else
                                                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " exec mineware.[dbo].[sp_BMCS_GetClockings_EngProdTSDOther] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + sec + "', '" + tonsplan + "', '" + tonsact + "', '" + AchPrec + "', '" + incentive + "', '" + Dept + "' ";

                                        }
                                    }

                                }
                            }
                            else
                            {
                                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " exec mineware.[dbo].[sp_BMCS_GetClockings_New] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + sec + "', '" + tonsplan + "', '" + tonsact + "', '" + AchPrec + "', '" + incentive + "', '" + Dept + "' ";

                            }
                        }
                    }
                    
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + "  ";

                    _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan1.ExecuteInstruction();

                    //NewradioBtn.Checked = false;
                }


                if (ShaftRB.Checked == true)
                {
                    _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    if ((Showlabel.Text + "           ").Substring(0, 7) == "020011V" && Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) >= 201705)
                    {
                        string newinc = "0";
                        MWDataManager.clsDataAccess _dbMan1zzz = new MWDataManager.clsDataAccess();
                        _dbMan1zzz.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _dbMan1zzz.SqlStatement = " select avg(payment) payment from mineware.[dbo].[tbl_BCS_Eng_Production] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and lvlnum in ('13','14','15','16','17')  ";
                        _dbMan1zzz.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbMan1zzz.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dbMan1zzz.ExecuteInstruction();



                        //_dbManData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        //_dbManData.queryReturnType = MWDataManager.ReturnType.DataTable;
                        //_dbManData.ResultsTableName = "OtherData";
                        //_dbManData.ExecuteInstruction();


                        if (_dbMan1zzz.ResultsDataTable.Rows.Count > 0)
                        newinc = _dbMan1zzz.ResultsDataTable.Rows[0]["payment"].ToString();

                        _dbMan1.SqlStatement = " " +
                                           " exec mineware.[dbo].[sp_BMCS_GetClockingsShaft] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + sec + "', '" + tonsplan + "', '" + tonsact + "', '" + AchPrec + "', '" + newinc + "', '" + Dept + "' " +
                                           "  ";



                    }
                    else
                    {

                        _dbMan1.SqlStatement = " " +
                                           " exec mineware.[dbo].[sp_BMCS_GetClockingsShaft] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + sec + "', '" + tonsplan + "', '" + tonsact + "', '" + AchPrec + "', '" + incentive + "', '" + Dept + "' " +
                                           "  ";

                    }

                    //MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                    //_dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    //_dbMan1.SqlStatement = " " +
                    //                       " exec mineware.[dbo].[sp_BMCS_GetClockingsShaft] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + sec + "', '" + tonsplan + "', '" + tonsact + "', '" + AchPrec + "', '" + incentive + "', '" + Dept + "' " +
                    //                       "  ";

                    _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan1.ExecuteInstruction();
                }


                if (PlantRB.Checked == true)
                {

                    //MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                    _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan1.SqlStatement = " ";
                        if (NewradioBtn.Checked == false)
                            _dbMan1.SqlStatement = _dbMan1.SqlStatement + " exec mineware.[dbo].[sp_BMCS_GetClockingsPlant] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + sec + "', '" + tonsplan + "', '" + tonsact + "', '" + AchPrec + "', '" + incentive + "', '" + Dept + "' " ;
                    else
                        {
                            if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) >= 201706)
                              _dbMan1.SqlStatement = _dbMan1.SqlStatement + " exec mineware.[dbo].[sp_BMCS_GetClockingsPlantNew] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + sec + "', '" + tonsplan + "', '" + tonsact + "', '" + AchPrec + "', '" + incentive + "', '" + Dept + "' " ;
                            else
                                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " exec mineware.[dbo].[sp_BMCS_GetClockingsPlantNewNW] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + sec + "', '" + tonsplan + "', '" + tonsact + "', '" + AchPrec + "', '" + incentive + "', '" + Dept + "' ";


                        }

                        _dbMan1.SqlStatement = _dbMan1.SqlStatement + "  ";

                    _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan1.ExecuteInstruction();
                }
              

           


                Neil = _dbMan1.ResultsDataTable;

                ReportDatasetEng = new DataSet();
                ReportDatasetEng.Tables.Add(_dbMan1.ResultsDataTable);


                foreach (DataRow r in Neil.Rows)
                {
                    int NewRow = EngBonusPCGrid.Rows.Add();
                    EngBonusPCGrid.Rows[NewRow].Cells[0].Value = r["industrynumber"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[1].Value = r["name"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[2].Value = r["orgunit"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[3].Value = r["Day1"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[4].Value = r["Day2"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[5].Value = r["Day3"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[6].Value = r["Day4"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[7].Value = r["Day5"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[8].Value = r["Day6"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[9].Value = r["Day7"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[10].Value = r["Day8"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[11].Value = r["Day9"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[12].Value = r["Day10"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[13].Value = r["Day11"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[14].Value = r["Day12"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[15].Value = r["Day13"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[16].Value = r["Day14"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[17].Value = r["Day15"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[18].Value = r["Day16"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[19].Value = r["Day17"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[20].Value = r["Day18"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[21].Value = r["Day19"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[22].Value = r["Day20"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[23].Value = r["Day21"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[24].Value = r["Day22"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[25].Value = r["Day23"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[26].Value = r["Day24"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[27].Value = r["Day25"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[28].Value = r["Day26"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[29].Value = r["Day27"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[30].Value = r["Day28"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[31].Value = r["Day29"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[32].Value = r["Day30"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[33].Value = r["Day31"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[34].Value = r["Day32"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[35].Value = r["Day33"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[36].Value = r["Day34"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[37].Value = r["Day35"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[38].Value = r["Day36"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[39].Value = r["Day37"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[40].Value = r["Day38"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[41].Value = r["Day39"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[42].Value = r["Day40"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[43].Value = r["PossShift"].ToString();

                    EngBonusPCGrid.Rows[NewRow].Cells[44].Value = r["IncPay"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[45].Value = r["factor1"].ToString();


                    EngBonusPCGrid.Rows[NewRow].Cells[45].Value = Convert.ToDecimal(r["factor1"].ToString()) * Convert.ToDecimal(r["IncPay"].ToString());

                    EngBonusPCGrid.Rows[NewRow].Cells[46].Value = (1-Convert.ToDecimal(r["dep"].ToString())) * Convert.ToDecimal(r["IncPay"].ToString());

                    decimal aa = (Convert.ToDecimal(r["factor1"].ToString()) * Convert.ToDecimal(r["IncPay"].ToString())) + ((1 - Convert.ToDecimal(r["dep"].ToString())) * Convert.ToDecimal(r["IncPay"].ToString()));

                    EngBonusPCGrid.Rows[NewRow].Cells[47].Value = (Convert.ToDecimal(r["factor1"].ToString()) * Convert.ToDecimal(r["IncPay"].ToString())) + ((1 - Convert.ToDecimal(r["dep"].ToString())) * Convert.ToDecimal(r["IncPay"].ToString()));
                    EngBonusPCGrid.Rows[NewRow].Cells[48].Value = r["bonusshifts"].ToString();
                    aa = aa/(Convert.ToDecimal(r["PossShift"].ToString())+Convert.ToDecimal(0.000001))*Convert.ToDecimal(r["bonusshifts"].ToString());
                    EngBonusPCGrid.Rows[NewRow].Cells[49].Value = aa;
                    aa = aa * Convert.ToDecimal(r["awww"].ToString());

                    EngBonusPCGrid.Rows[NewRow].Cells[50].Value = r["awops"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[51].Value = aa;

                    aa = aa * Convert.ToDecimal(r["ltifact"].ToString());
                    EngBonusPCGrid.Rows[NewRow].Cells[52].Value = r["ltifact"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[53].Value = aa;


                    EngBonusPCGrid.Rows[NewRow].Cells[54].Value = r["designation"].ToString();


                    EngBonusPCGrid.Rows[NewRow].Cells[60].Value = r["IncPay"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[61].Value = r["factor1"].ToString();
                    // safety fact
                    EngBonusPCGrid.Rows[NewRow].Cells[62].Value = r["ltifact"].ToString();

                    // basic
                    EngBonusPCGrid.Rows[NewRow].Cells[63].Value = (Convert.ToDecimal(EngBonusPCGrid.Rows[NewRow].Cells[47].Value) * Convert.ToDecimal(r["ltifact"].ToString())).ToString();

                    // bonus
                    EngBonusPCGrid.Rows[NewRow].Cells[64].Value = r["bonusshifts"].ToString();

                    // prorata
                    EngBonusPCGrid.Rows[NewRow].Cells[65].Value = Math.Round((Convert.ToDecimal(EngBonusPCGrid.Rows[NewRow].Cells[49].Value) * Convert.ToDecimal(r["ltifact"].ToString())),2);

                    // awops
                    EngBonusPCGrid.Rows[NewRow].Cells[66].Value = r["ttawop"].ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[67].Value = r["sickdays"].ToString();

                    //penalty AwOP, Sick, Tot
                    EngBonusPCGrid.Rows[NewRow].Cells[68].Value = ((Convert.ToDecimal(1)-Convert.ToDecimal(r["awww"].ToString()))*100).ToString();
                    EngBonusPCGrid.Rows[NewRow].Cells[69].Value = ((Convert.ToDecimal(1) - Convert.ToDecimal(r["SAWOP"].ToString()))*100).ToString();

                    EngBonusPCGrid.Rows[NewRow].Cells[70].Value = (Convert.ToDecimal(EngBonusPCGrid.Rows[NewRow].Cells[68].Value) + Convert.ToDecimal(EngBonusPCGrid.Rows[NewRow].Cells[69].Value)).ToString();

                    if (Convert.ToDecimal(EngBonusPCGrid.Rows[NewRow].Cells[70].Value) > Convert.ToDecimal(98))
                        EngBonusPCGrid.Rows[NewRow].Cells[70].Value = 100;
                    //Pen Pot
                    EngBonusPCGrid.Rows[NewRow].Cells[71].Value = "0.00";

                   // if (r["factor1"].ToString() == "0.91")
                    EngBonusPCGrid.Rows[NewRow].Cells[71].Value = Math.Round(Convert.ToDecimal(EngBonusPCGrid.Rows[NewRow].Cells[65].Value) * Convert.ToDecimal(EngBonusPCGrid.Rows[NewRow].Cells[70].Value) / 100, 2);


                    // final payment

                    EngBonusPCGrid.Rows[NewRow].Cells[75].Value = Math.Round(Convert.ToDecimal(EngBonusPCGrid.Rows[NewRow].Cells[65].Value) - Convert.ToDecimal(EngBonusPCGrid.Rows[NewRow].Cells[71].Value),2);



                    EngBonusPCGrid.Rows[NewRow].Cells[76].Value = "0";
                    if (r["finpay"].ToString() != "") 
                      EngBonusPCGrid.Rows[NewRow].Cells[76].Value = r["finpay"].ToString();
                
                }
                EngBonusPCGrid.Visible = true;
                EngBonusPCGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan;
                //GridSelectLabel.Text = dtgrid.Rows[0].Cells[0].Value.ToString();
                EngBonusPCGrid.ReadOnly = true;
                //Delet_Col_3 = dtgrid.Rows[0].Cells[2].Value.ToString();

            }
            catch { }
        }

        void LoadReport()
        {

            if (Neil.Rows.Count < 1)
            {
                MessageBox.Show("Report Cant Be Loaded", "Missing Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // set up page2
            MWDataManager.clsDataAccess _dbManPage2Data = new MWDataManager.clsDataAccess();
            _dbManPage2Data.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " select '' Cat, '' Designation,'' IndNo,'' Name , '' PossShifts , '' IncPay, '' Fact, '' LTIFact, '' Basic, '' BonusShift, ''  Prorata,  \r\n ";
            _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " '' AWOP, '' Sick, '' AWOPPen, '' SickPen, '' TotPen , '' Pot, '' FinPay ";
            _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + "  ";

            for (int row = 0; row <= EngBonusPCGrid.RowCount - 1; row++)
            {
                if (EngBonusPCGrid.Rows[row].Cells[0].Value != null)
                {
                    _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " union select '" + EngBonusPCGrid.Rows[row].Cells[54].Value + "' cat ,   \r\n ";
                    _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " '" + EngBonusPCGrid.Rows[row].Cells[54].Value + "' Designation,  \r\n ";
                    _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " '" + EngBonusPCGrid.Rows[row].Cells[0].Value + "' IndNo,  \r\n ";
                    _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " '" + EngBonusPCGrid.Rows[row].Cells[1].Value + "' Name,  \r\n ";
                    _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " '" + EngBonusPCGrid.Rows[row].Cells[43].Value + "' PossShifts,  \r\n ";
                    _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " '" + EngBonusPCGrid.Rows[row].Cells[60].Value + "' IncPay,  \r\n ";
                    _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " '" + EngBonusPCGrid.Rows[row].Cells[61].Value + "' Fact, \r\n ";
                    _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " '" + EngBonusPCGrid.Rows[row].Cells[62].Value + "' LTIFact, \r\n ";
                    _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " '" + EngBonusPCGrid.Rows[row].Cells[63].Value + "' Basic, \r\n ";
                    _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " '" + EngBonusPCGrid.Rows[row].Cells[64].Value + "' BonusShift, \r\n ";
                    _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " '" + EngBonusPCGrid.Rows[row].Cells[65].Value + "' Prorata, \r\n ";
                    _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " '" + EngBonusPCGrid.Rows[row].Cells[66].Value + "' AWOP, \r\n ";
                    _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " '" + EngBonusPCGrid.Rows[row].Cells[67].Value + "' Sick, \r\n ";
                    _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " '" + Math.Round(Convert.ToDecimal(EngBonusPCGrid.Rows[row].Cells[68].Value),0) + "' AWOPPen, \r\n ";
                    _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " '" + Math.Round(Convert.ToDecimal(EngBonusPCGrid.Rows[row].Cells[69].Value),0) + "' SickPen, \r\n ";
                    _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " '" + Math.Round(Convert.ToDecimal(EngBonusPCGrid.Rows[row].Cells[70].Value),0) + "' TotPen, \r\n ";
                    _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " '" + Math.Round(Convert.ToDecimal(EngBonusPCGrid.Rows[row].Cells[71].Value),2) + "' Pot, \r\n ";
                    _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " '" + Math.Round(Convert.ToDecimal(EngBonusPCGrid.Rows[row].Cells[75].Value), 2) + "' FinPay ";

                }




            }

            _dbManPage2Data.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManPage2Data.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManPage2Data.ResultsTableName = "Page2Data";
            _dbManPage2Data.ExecuteInstruction();

            //theReport.Tables.Add(_dbManPage2Data.ResultsDataTable); 


            ReportDatasetEng.Tables.Add(_dbManPage2Data.ResultsDataTable);


            
            theReport.RegisterData(ReportDatasetEng);

            if (NewradioBtn.Checked == false)
                theReport.Load(_reportFolder+ "EngBonus.frx");
            else
                theReport.Load(_reportFolder + "EngBonusSick.frx");

          //theReport.Design();

            EngBonusPC.Clear();
            theReport.Prepare();
            theReport.Preview = EngBonusPC;
            theReport.ShowPrepared();
        }

        decimal act = 0;
        string rate = "0";
        decimal perc = 0;
        decimal Call = 0;
       

        string lvl = "";

        string Shaft = "";

        




        void ProductionBonus()
        {
            MWDataManager.clsDataAccess _dbManPpl = new MWDataManager.clsDataAccess();
            _dbManPpl.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            _dbManPpl.SqlStatement = _dbManPpl.SqlStatement + " select * from tbl_bcs_general_factors  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";
            _dbManPpl.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManPpl.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManPpl.ResultsTableName = "Table1";
            _dbManPpl.ExecuteInstruction();


            // insert into table

            //get perc and tons one lvl

            perc =  Math.Round(  Convert.ToDecimal(  _dbManPpl.ResultsDataTable.Rows[0]["oneact"].ToString())/Convert.ToDecimal( _dbManPpl.ResultsDataTable.Rows[0]["onecall"].ToString())*100,2);
            act = Convert.ToDecimal( _dbManPpl.ResultsDataTable.Rows[0]["oneact"].ToString());
            Call = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["onecall"].ToString());

            MWDataManager.clsDataAccess _dbManInsert = new MWDataManager.clsDataAccess();
            _dbManInsert.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " delete from  mineware.[dbo].[tbl_BCS_Eng_Production] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";

            getrate();
            _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " insert into mineware.[dbo].[tbl_BCS_Eng_Production] values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', 'A', '1', '" + Call + "', '" + act + "', '" + perc + "' , '" + rate + "') ";
            //two
            perc =  Math.Round(Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["twoact"].ToString()) / Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["twocall"].ToString())*100,2);
            act = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["twoact"].ToString());
            Call = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["twocall"].ToString());
            getrate();
            _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " insert into mineware.[dbo].[tbl_BCS_Eng_Production] values('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', 'B', '2', '" + Call + "', '" + act + "', '" + perc + "', '" + rate + "') ";

            //three
            perc = Math.Round(Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["threeact"].ToString()) / Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["threecall"].ToString()) * 100,2);
            act = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["threeact"].ToString());
            Call = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["threecall"].ToString());
            getrate();
            _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " insert into mineware.[dbo].[tbl_BCS_Eng_Production] values('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', 'C', '3', '" + Call + "', '" + act + "', '" + perc + "', '" + rate + "') ";

            //four
            perc = Math.Round(Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["fouract"].ToString()) / Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["fourcall"].ToString()) * 100,2);
            act = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["fouract"].ToString());
            Call = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["fourcall"].ToString());
            getrate();
            _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " insert into mineware.[dbo].[tbl_BCS_Eng_Production] values('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', 'D', '4', '" + Call + "', '" + act + "', '" + perc + "', '" + rate + "') ";

            //five
            perc = Math.Round(Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["fiveact"].ToString()) / Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["fivecall"].ToString()) * 100,2);
            act = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["fiveact"].ToString());
            Call = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["fivecall"].ToString());
            getrate();
            _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " insert into mineware.[dbo].[tbl_BCS_Eng_Production] values('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', 'E', '5', '" + Call + "', '" + act + "', '" + perc + "', '" + rate + "') ";

            //six
            perc = Math.Round(Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["sixact"].ToString()) / Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["sixcall"].ToString()) * 100,2);
            act = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["sixact"].ToString());
            Call = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["sixcall"].ToString());
            getrate();
            _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " insert into mineware.[dbo].[tbl_BCS_Eng_Production] values('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', 'F', '6', '" + Call + "', '" + act + "', '" + perc + "', '" + rate + "') ";

            //seven
            perc = Math.Round(Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["sevenact"].ToString()) / Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["sevencall"].ToString()) * 100,2);
            act = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["sevenact"].ToString());
            Call = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["sevencall"].ToString());
            getrate();
            _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " insert into mineware.[dbo].[tbl_BCS_Eng_Production] values('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', 'G', '7', '" + Call + "', '" + act + "', '" + perc + "', '" + rate + "') ";

            //eight
            perc = Math.Round(Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["eightact"].ToString()) / Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["eightcall"].ToString()) * 100,2);
            act = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["eightact"].ToString());
            Call = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["eightcall"].ToString());
            getrate();
            _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " insert into mineware.[dbo].[tbl_BCS_Eng_Production] values('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', 'H', '8', '" + Call + "', '" + act + "', '" + perc + "', '" + rate + "') ";

            //nine
            perc = Math.Round(Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["nineact"].ToString()) / Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["ninecall"].ToString()) * 100,2);
            act = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["nineact"].ToString());
            Call = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["ninecall"].ToString());
            getrate();
            _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " insert into mineware.[dbo].[tbl_BCS_Eng_Production] values('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', 'I', '9', '" + Call + "', '" + act + "', '" + perc + "', '" + rate + "') ";

            //ten
            perc = Math.Round(Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["tenact"].ToString()) / Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["tencall"].ToString()) * 100,2);
            act = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["tenact"].ToString());
            Call = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["tencall"].ToString());
            getrate();
            _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " insert into mineware.[dbo].[tbl_BCS_Eng_Production] values('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', 'J', '10', '" + Call + "', '" + act + "', '" + perc + "', '" + rate + "') ";

            //eleven
            perc = Math.Round(Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["elevenact"].ToString()) / Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["elevencall"].ToString()) * 100,2);
            act = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["elevenact"].ToString());
            Call = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["elevencall"].ToString());
            getrate();
            _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " insert into mineware.[dbo].[tbl_BCS_Eng_Production] values('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', 'K', '11', '" + Call + "', '" + act + "', '" + perc + "', '" + rate + "') ";

            //Twelve
            perc = Math.Round(Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["Twelveact"].ToString()) / Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["Twelvecall"].ToString()) * 100,2);
            act = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["Twelveact"].ToString());
            Call = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["Twelvecall"].ToString());
            getrate();
            _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " insert into mineware.[dbo].[tbl_BCS_Eng_Production] values('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', 'L', '12', '" + Call + "', '" + act + "', '" + perc + "', '" + rate + "') ";

            //thirteen
            perc = Math.Round(Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["thirteenact"].ToString()) / Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["thirteencall"].ToString()) * 100,2);
            act = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["thirteenact"].ToString());
            Call = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["thirteencall"].ToString());
            getrate();
            _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " insert into mineware.[dbo].[tbl_BCS_Eng_Production] values('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', 'M', '13', '" + Call + "', '" + act + "', '" + perc + "', '" + rate + "') ";

            //fourteen
            perc = Math.Round(Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["fourteenact"].ToString()) / Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["fourteencall"].ToString()) * 100,2);
            act = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["fourteenact"].ToString());
            Call = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["fourteencall"].ToString());
            getrate();
            _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " insert into mineware.[dbo].[tbl_BCS_Eng_Production] values('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "','N', '14', '" + Call + "', '" + act + "', '" + perc + "', '" + rate + "') ";

            //fifteen
            perc = Math.Round(Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["fifteenact"].ToString()) / Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["fifteencall"].ToString()) * 100,2);
            act = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["fifteenact"].ToString());
            Call = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["fifteencall"].ToString());
            getrate();
            _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " insert into mineware.[dbo].[tbl_BCS_Eng_Production] values('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', 'O', '15', '" + Call + "', '" + act + "', '" + perc + "', '" + rate + "') ";

            //sixteen
            if (Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["sixteencall"].ToString()) > 0)
            {
                perc = Math.Round(Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["sixteenact"].ToString()) / Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["sixteencall"].ToString()) * 100, 2);
                act = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["sixteenact"].ToString());
                Call = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["sixteencall"].ToString());
            }
            else
            {
                perc = Math.Round((Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["sixteenact"].ToString()) / (Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["sixteenact"].ToString())) * 100) * 2, 2);
                act = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["sixteenact"].ToString());
                Call = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["sixteencall"].ToString());
            }
            getrate();
            _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " insert into mineware.[dbo].[tbl_BCS_Eng_Production] values('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', 'P', '16', '" + Call + "', '" + act + "', '" + perc + "', '" + rate + "') ";

            //seventeen
            perc = Math.Round(Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["seventeenact"].ToString()) / (Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["seventeencall"].ToString())+Convert.ToDecimal(0.000001)) * 100,2);
            act = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["seventeenact"].ToString());
            Call = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["seventeencall"].ToString());
            getrate();
            _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " insert into mineware.[dbo].[tbl_BCS_Eng_Production] values('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', 'Q', '17', '" + Call + "', '" + act + "', '" + perc + "', '" + rate + "') ";

            //eightteen
            perc = Math.Round(Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["eighteenact"].ToString()) / (Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["eighteencall"].ToString())+Convert.ToDecimal(0.000001)) * 100,2);
            act = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["eighteenact"].ToString());
            Call = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["eighteencall"].ToString());
            getrate();
            _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " insert into mineware.[dbo].[tbl_BCS_Eng_Production] values('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', 'R', '18', '" + Call + "', '" + act + "', '" + perc + "', '" + rate + "') ";

            //nineteen
            perc = Math.Round(Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["nineteenact"].ToString()) / (Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["nineteencall"].ToString())+Convert.ToDecimal(0.000001)) * 100,2);
            act = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["nineteenact"].ToString());
            Call = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["nineteencall"].ToString());
            getrate();
            _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " insert into mineware.[dbo].[tbl_BCS_Eng_Production] values('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', 'S', '19', '" + Call + "', '" + act + "', '" + perc + "', '" + rate + "') ";

            //twenty
            perc = Math.Round(Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["twentyact"].ToString()) / (Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["twentycall"].ToString())+Convert.ToDecimal(0.000001)) * 100,2);
            act = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["twentyact"].ToString());
            Call = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["twentycall"].ToString());
            getrate();
            _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " insert into mineware.[dbo].[tbl_BCS_Eng_Production] values('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', 'T', '20', '" + Call + "', '" + act + "', '" + perc + "', '" + rate + "') ";


            //Oneshaft
            Shaft = "1";
            perc = Math.Round(Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["OneShaftact"].ToString()) / (Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["OneShatfcall"].ToString())+Convert.ToDecimal(0.000001)) * 100,2);
            act = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["OneShaftact"].ToString());
            Call = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["OneShatfcall"].ToString());
            getrateShaft();
            _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " insert into mineware.[dbo].[tbl_BCS_Eng_Production] values('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', 'X1', '', '" + Call + "', '" + act + "', '" + perc + "', '" + rate + "') ";

           //Twoshaft
            Shaft = "2";
            perc = Math.Round(Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["TwoShaftact"].ToString()) / (Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["TwoShaftcall"].ToString()) + Convert.ToDecimal(0.000001)) * 100, 2);
            act = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["TwoShaftact"].ToString());
            Call = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["TwoShaftcall"].ToString());
            getrateShaft();
            _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " insert into mineware.[dbo].[tbl_BCS_Eng_Production] values('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', 'X2', '', '" + Call + "', '" + act + "', '" + perc + "', '" + rate + "') ";



            _dbManInsert.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManInsert.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManInsert.ResultsTableName = "Table1";
            _dbManInsert.ExecuteInstruction();


            
            
            //MWDataManager.clsDataAccess _dbManShifts = new MWDataManager.clsDataAccess();
            //_dbManShifts.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //_dbManShifts.SqlStatement = _dbManShifts.SqlStatement + "select * from BMCS_eng_level  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and biptons < '" + act + "' ";
            //_dbManShifts.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //_dbManShifts.queryReturnType = MWDataManager.ReturnType.DataTable;
            //_dbManShifts.ResultsTableName = "Table1";
            //_dbManShifts.ExecuteInstruction();


            //string rate = "0";


            // if (perc < 50) 
            //     rate =  _dbManShifts.ResultsDataTable.Rows[0]["BipPer1"].ToString();
            // if (perc >= 50 && perc < 55)  
            //     rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer2"].ToString();
            // if (perc >= 55 && perc < 60) 
            //     rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer3"].ToString();
            // if (perc >= 60 && perc < 65) 
            //     rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer4"].ToString();
            // if (perc >= 65 && perc < 70)  
            //     rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer5"].ToString();
            // if (perc >= 70 && perc < 75)  
            //     rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer6"].ToString();
            // if (perc >= 75 && perc < 80)  
            //     rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer7"].ToString();
            // if (perc >= 80 && perc < 85)  
            //     rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer8"].ToString();
            // if (perc >= 85 && perc < 90)  
            //     rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer9"].ToString();
            // if (perc >= 90 && perc < 95)  
            //     rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer10"].ToString();
            // if (perc >= 95 && perc < 100)  
            //     rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer11"].ToString();
            // if (perc >= 100 && perc < 105)  
            //     rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer12"].ToString();
            // if (perc >= 105 && perc < 110) 
            //     rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer13"].ToString();
            // if (perc >= 110 && perc < 115)  
            //     rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer14"].ToString();
            // if (perc >= 115 && perc < 120)  
            //     rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer15"].ToString();
            // if (perc >= 120 && perc < 125)  
            //     rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer16"].ToString();
            // if (perc >= 125 && perc < 200)
            //     rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer17"].ToString();


            // perc = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["twocall"].ToString()) / Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["twoact"].ToString());

            // act = Convert.ToDecimal(_dbManPpl.ResultsDataTable.Rows[0]["twoact"].ToString());




            // //MWDataManager.clsDataAccess _dbManShifts = new MWDataManager.clsDataAccess();
            // _dbManShifts.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            // _dbManShifts.SqlStatement = _dbManShifts.SqlStatement + "select * from BMCS_eng_level  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and biptons < '" + act + "' ";
            // _dbManShifts.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            // _dbManShifts.queryReturnType = MWDataManager.ReturnType.DataTable;
            // _dbManShifts.ResultsTableName = "Table1";
            // _dbManShifts.ExecuteInstruction();


            // string rate2 = "0";


            // if (perc < 50)
            //     rate2 = _dbManShifts.ResultsDataTable.Rows[0]["BipPer1"].ToString();
            // if (perc >= 50 && perc < 55)
            //     rate2 = _dbManShifts.ResultsDataTable.Rows[0]["BipPer2"].ToString();
            // if (perc >= 55 && perc < 60)
            //     rate2 = _dbManShifts.ResultsDataTable.Rows[0]["BipPer3"].ToString();
            // if (perc >= 60 && perc < 65)
            //     rate2 = _dbManShifts.ResultsDataTable.Rows[0]["BipPer4"].ToString();
            // if (perc >= 65 && perc < 70)
            //     rate2 = _dbManShifts.ResultsDataTable.Rows[0]["BipPer5"].ToString();
            // if (perc >= 70 && perc < 75)
            //     rate2 = _dbManShifts.ResultsDataTable.Rows[0]["BipPer6"].ToString();
            // if (perc >= 75 && perc < 80)
            //     rate2 = _dbManShifts.ResultsDataTable.Rows[0]["BipPer7"].ToString();
            // if (perc >= 80 && perc < 85)
            //     rate2 = _dbManShifts.ResultsDataTable.Rows[0]["BipPer8"].ToString();
            // if (perc >= 85 && perc < 90)
            //     rate2 = _dbManShifts.ResultsDataTable.Rows[0]["BipPer9"].ToString();
            // if (perc >= 90 && perc < 95)
            //     rate2 = _dbManShifts.ResultsDataTable.Rows[0]["BipPer10"].ToString();
            // if (perc >= 95 && perc < 100)
            //     rate2 = _dbManShifts.ResultsDataTable.Rows[0]["BipPer11"].ToString();
            // if (perc >= 100 && perc < 105)
            //     rate2 = _dbManShifts.ResultsDataTable.Rows[0]["BipPer12"].ToString();
            // if (perc >= 105 && perc < 110)
            //     rate2 = _dbManShifts.ResultsDataTable.Rows[0]["BipPer13"].ToString();
            // if (perc >= 110 && perc < 115)
            //     rate2 = _dbManShifts.ResultsDataTable.Rows[0]["BipPer14"].ToString();
            // if (perc >= 115 && perc < 120)
            //     rate2 = _dbManShifts.ResultsDataTable.Rows[0]["BipPer15"].ToString();
            // if (perc >= 120 && perc < 125)
            //     rate2 = _dbManShifts.ResultsDataTable.Rows[0]["BipPer16"].ToString();
            // if (perc >= 125 && perc < 200)
            //     rate2 = _dbManShifts.ResultsDataTable.Rows[0]["BipPer17"].ToString();



            // MWDataManager.clsDataAccess _dbManInsert = new MWDataManager.clsDataAccess();
            // _dbManInsert.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            // _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " delete from  mineware.[dbo].BCS_Eng_Production where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";
            // _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " insert into mineware.[dbo].BCS_Eng_Production values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', 1, '" + rate + "') ";
            // _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " insert into mineware.[dbo].BCS_Eng_Production values('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', 2, '" + rate2 + "') ";
            // _dbManInsert.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            // _dbManInsert.queryReturnType = MWDataManager.ReturnType.DataTable;
            // _dbManInsert.ResultsTableName = "Table1";
            // _dbManInsert.ExecuteInstruction();



        }

        void getrateShaft()
        {
            //MWDataManager.clsDataAccess _dbManShifts = new MWDataManager.clsDataAccess();
            //_dbManShifts.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //_dbManShifts.SqlStatement = _dbManShifts.SqlStatement + "select * from ( select * from bmcs_eng_shaft  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and biptons < '" + act + "' ";
            //_dbManShifts.SqlStatement = _dbManShifts.SqlStatement + " union select * from bmcs_eng_shaft  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and biptons = ";
            //_dbManShifts.SqlStatement = _dbManShifts.SqlStatement + " (select min(biptons) from bmcs_eng_shaft  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' )) a order by biptons desc ";
            //_dbManShifts.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //_dbManShifts.queryReturnType = MWDataManager.ReturnType.DataTable;
            //_dbManShifts.ResultsTableName = "Table1";
            //_dbManShifts.ExecuteInstruction();


            //if (perc < 50)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer1"].ToString();
            //if (perc >= 50 && perc < 55)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer2"].ToString();
            //if (perc >= 55 && perc < 60)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer3"].ToString();
            //if (perc >= 60 && perc < 65)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer4"].ToString();
            //if (perc >= 65 && perc < 70)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer5"].ToString();
            //if (perc >= 70 && perc < 75)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer6"].ToString();
            //if (perc >= 75 && perc < 80)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer7"].ToString();
            //if (perc >= 80 && perc < 85)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer8"].ToString();
            //if (perc >= 85 && perc < 90)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer9"].ToString();
            //if (perc >= 90 && perc < 95)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer10"].ToString();
            //if (perc >= 95 && perc < 100)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer11"].ToString();
            //if (perc >= 100 && perc < 105)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer12"].ToString();
            //if (perc >= 105 && perc < 110)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer13"].ToString();
            //if (perc >= 110 && perc < 115)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer14"].ToString();
            //if (perc >= 115 && perc < 120)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer15"].ToString();
            //if (perc >= 120 && perc < 125)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer16"].ToString();
            //if (perc >= 125 && perc < 200)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer17"].ToString();

            MWDataManager.clsDataAccess _dbManShifts = new MWDataManager.clsDataAccess();
            _dbManShifts.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            if (Shaft == "1")
            {

                //MWDataManager.clsDataAccess _dbManShifts = new MWDataManager.clsDataAccess();
                //_dbManShifts.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManShifts.SqlStatement = _dbManShifts.SqlStatement + "select * from ( select * from mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveDetail  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and oneshaft < '" + act + "' ";
                _dbManShifts.SqlStatement = _dbManShifts.SqlStatement + " union select * from mineware.dbo.[tbl_BCS_Eng_SurfaceIncentiveDetail]  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and oneshaft = ";
                _dbManShifts.SqlStatement = _dbManShifts.SqlStatement + " (select min(oneshaft) from mineware.dbo.[tbl_BCS_Eng_SurfaceIncentiveDetail]  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' )) a order by oneshaft desc ";
                _dbManShifts.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManShifts.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManShifts.ResultsTableName = "Table1";
                _dbManShifts.ExecuteInstruction();

            }
            else
            {

                //MWDataManager.clsDataAccess _dbManShifts = new MWDataManager.clsDataAccess();
                //_dbManShifts.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManShifts.SqlStatement = _dbManShifts.SqlStatement + "select * from ( select * from mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveDetail  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and twoshaft < '" + act + "' ";
                _dbManShifts.SqlStatement = _dbManShifts.SqlStatement + " union select * from mineware.dbo.[tbl_BCS_Eng_SurfaceIncentiveDetail]  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and twoshaft = ";
                _dbManShifts.SqlStatement = _dbManShifts.SqlStatement + " (select min(twoshaft) from mineware.dbo.[tbl_BCS_Eng_SurfaceIncentiveDetail]  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' )) a order by twoshaft desc ";
                _dbManShifts.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManShifts.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManShifts.ResultsTableName = "Table1";
                _dbManShifts.ExecuteInstruction();

            }



            //rate = _dbManShifts.ResultsDataTable.Rows[0]["cat24"].ToString();
            //if (perc < 126)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat23"].ToString();
            //if (perc < 124)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat22"].ToString();
            //if (perc < 122)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat21"].ToString();
            
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat20"].ToString();
            if (perc < 124)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat19"].ToString();
            if (perc < 122)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat18"].ToString();
            if (perc < 120)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat17"].ToString();
            if (perc < 118)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat16"].ToString();
            if (perc < 116)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat15"].ToString();
            if (perc < 114)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat14"].ToString();
            if (perc < 112)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat13"].ToString();
            if (perc < 110)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat12"].ToString();
            if (perc < 108)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat11"].ToString();
            if (perc < 106)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat10"].ToString();
            if (perc < 104)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat9"].ToString();
            if (perc < 102)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat8"].ToString();
            if (perc < 100)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat7"].ToString();
            if (perc < 98)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat6"].ToString();
            if (perc < 96)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat5"].ToString();
            if (perc < 94)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat4"].ToString();
            if (perc < 92)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat3"].ToString();
            if (perc < 90)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat2"].ToString();
            if (perc < 88)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat1"].ToString();
            if (perc < 86)
                rate = "0";

        }

        void getrate()
        {
            //MWDataManager.clsDataAccess _dbManShifts = new MWDataManager.clsDataAccess();
            //_dbManShifts.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //_dbManShifts.SqlStatement = _dbManShifts.SqlStatement + "select * from ( select * from BMCS_eng_level  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and biptons < '" + act + "' ";
            //_dbManShifts.SqlStatement = _dbManShifts.SqlStatement + " union select * from BMCS_eng_level  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and biptons = ";
            //_dbManShifts.SqlStatement = _dbManShifts.SqlStatement + " (select min(biptons) from BMCS_eng_level  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' )) a order by biptons desc ";
            //_dbManShifts.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //_dbManShifts.queryReturnType = MWDataManager.ReturnType.DataTable;
            //_dbManShifts.ResultsTableName = "Table1";
            //_dbManShifts.ExecuteInstruction();


            MWDataManager.clsDataAccess _dbManShifts = new MWDataManager.clsDataAccess();
            _dbManShifts.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManShifts.SqlStatement = _dbManShifts.SqlStatement + "select * from ( select * from mineware.dbo.[tbl_BCS_Eng_UgProdEng]  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons < '" + act + "' ";
            _dbManShifts.SqlStatement = _dbManShifts.SqlStatement + " union select * from mineware.dbo.[tbl_BCS_Eng_UgProdEng]  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and tons = ";
            _dbManShifts.SqlStatement = _dbManShifts.SqlStatement + " (select min(tons) from mineware.dbo.[tbl_BCS_Eng_UgProdEng]  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' )) a order by tons desc ";
            _dbManShifts.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManShifts.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManShifts.ResultsTableName = "Table1";
            _dbManShifts.ExecuteInstruction();


           
            rate = _dbManShifts.ResultsDataTable.Rows[0]["cat24"].ToString();
            if (perc < 126)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat23"].ToString();
            if (perc < 124)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat22"].ToString();
            if (perc < 122)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat21"].ToString();
            if (perc < 120)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat20"].ToString();
            if (perc < 118)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat19"].ToString();
            if (perc < 116)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat18"].ToString();
            if (perc < 114)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat17"].ToString();
            if (perc < 112)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat16"].ToString();
            if (perc < 110)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat15"].ToString();
            if (perc < 108)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat14"].ToString();
            if (perc < 106)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat13"].ToString();
            if (perc < 104)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat12"].ToString();
            if (perc < 102)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat11"].ToString();
            if (perc < 100)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat10"].ToString();
            if (perc < 98)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat9"].ToString();
            if (perc < 96)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat8"].ToString();
            if (perc < 94)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat7"].ToString();
            if (perc < 92)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat6"].ToString();
            if (perc < 90)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat5"].ToString();
            if (perc < 88)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat4"].ToString();
            if (perc < 86)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat3"].ToString();
            if (perc < 80)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat2"].ToString();
            if (perc < 75)
                rate = _dbManShifts.ResultsDataTable.Rows[0]["cat1"].ToString();


            //if (perc >= 70 && perc < 75)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat2"].ToString();
            //if (perc >= 75 && perc < 80)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat3"].ToString();
            //if (perc >= 80 && perc < 86)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat4"].ToString();
            //if (perc >= 86 && perc < 88)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat5"].ToString();
            //if (perc >= 88 && perc < 90)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat6"].ToString();
            //if (perc >= 90 && perc < 92)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat7"].ToString();
            //if (perc >= 92 && perc < 94)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat8"].ToString();
            //if (perc >= 94 && perc < 96)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat9"].ToString();
            //if (perc >= 96 && perc < 98)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat10"].ToString();
            //if (perc >= 98 && perc < 100)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat11"].ToString();
            //if (perc >= 100 && perc < 102)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat12"].ToString();
            //if (perc >= 102 && perc < 104)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat13"].ToString();
            //if (perc >= 104 && perc < 106)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat14"].ToString();
            //if (perc >= 106 && perc < 108)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat15"].ToString();
            //if (perc >= 108 && perc < 110)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat16"].ToString();
            //if (perc >= 110 && perc < 112)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat17"].ToString();
            //if (perc >= 112 && perc < 114)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat18"].ToString();
            //if (perc >= 114 && perc < 116)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat19"].ToString();
            //if (perc >= 116 && perc < 118)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat20"].ToString();
            //if (perc >= 118 && perc < 120)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat21"].ToString();
            //if (perc >= 120 && perc < 122)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat22"].ToString();
            //if (perc >= 122 && perc < 124)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat23"].ToString();
            //if (perc >= 126 && perc < 126)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["cat24"].ToString();



            //if (perc < 50)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer1"].ToString();
            //if (perc >= 50 && perc < 55)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer2"].ToString();
            //if (perc >= 55 && perc < 60)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer3"].ToString();
            //if (perc >= 60 && perc < 65)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer4"].ToString();
            //if (perc >= 65 && perc < 70)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer5"].ToString();
            //if (perc >= 70 && perc < 75)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer6"].ToString();
            //if (perc >= 75 && perc < 80)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer7"].ToString();
            //if (perc >= 80 && perc < 85)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer8"].ToString();
            //if (perc >= 85 && perc < 90)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer9"].ToString();
            //if (perc >= 90 && perc < 95)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer10"].ToString();
            //if (perc >= 95 && perc < 100)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer11"].ToString();
            //if (perc >= 100 && perc < 105)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer12"].ToString();
            //if (perc >= 105 && perc < 110)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer13"].ToString();
            //if (perc >= 110 && perc < 115)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer14"].ToString();
            //if (perc >= 115 && perc < 120)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer15"].ToString();
            //if (perc >= 120 && perc < 125)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer16"].ToString();
            //if (perc >= 125 && perc < 200)
            //    rate = _dbManShifts.ResultsDataTable.Rows[0]["BipPer17"].ToString();

        }

        private void showBtn_Click(object sender, EventArgs e)
        {
            

            
        }

        void ResetGrid()
        {
            EngBonusPCGrid.ColumnCount = 60;

            EngBonusPCGrid.Columns[0].Visible = true;
            EngBonusPCGrid.Columns[1].Visible = true;
            EngBonusPCGrid.Columns[2].Visible = true;
            EngBonusPCGrid.Columns[3].Visible = true;
            EngBonusPCGrid.Columns[4].Visible = true;
            EngBonusPCGrid.Columns[5].Visible = true;
            EngBonusPCGrid.Columns[6].Visible = true;
            EngBonusPCGrid.Columns[7].Visible = true;
            EngBonusPCGrid.Columns[8].Visible = true;
            EngBonusPCGrid.Columns[9].Visible = true;

            EngBonusPCGrid.Columns[10].Visible = true;
            EngBonusPCGrid.Columns[11].Visible = true;
            EngBonusPCGrid.Columns[12].Visible = true;
            EngBonusPCGrid.Columns[13].Visible = true;
            EngBonusPCGrid.Columns[14].Visible = true;
            EngBonusPCGrid.Columns[15].Visible = true;
            EngBonusPCGrid.Columns[16].Visible = true;
            EngBonusPCGrid.Columns[17].Visible = true;
            EngBonusPCGrid.Columns[18].Visible = true;
            EngBonusPCGrid.Columns[19].Visible = true;

            EngBonusPCGrid.Columns[20].Visible = true;
            EngBonusPCGrid.Columns[21].Visible = true;
            EngBonusPCGrid.Columns[22].Visible = true;
            EngBonusPCGrid.Columns[23].Visible = true;
            EngBonusPCGrid.Columns[24].Visible = true;
            EngBonusPCGrid.Columns[25].Visible = true;
            EngBonusPCGrid.Columns[26].Visible = true;
            EngBonusPCGrid.Columns[27].Visible = true;
            EngBonusPCGrid.Columns[28].Visible = true;
            EngBonusPCGrid.Columns[29].Visible = true;

            EngBonusPCGrid.Columns[30].Visible = true;
            EngBonusPCGrid.Columns[31].Visible = true;
            EngBonusPCGrid.Columns[32].Visible = true;
            EngBonusPCGrid.Columns[33].Visible = true;
            EngBonusPCGrid.Columns[34].Visible = true;
            EngBonusPCGrid.Columns[35].Visible = true;
            EngBonusPCGrid.Columns[36].Visible = true;
            EngBonusPCGrid.Columns[37].Visible = true;
            EngBonusPCGrid.Columns[38].Visible = true;
            EngBonusPCGrid.Columns[39].Visible = true;

            EngBonusPCGrid.Columns[40].Visible = true;
            


        }

        private void Close1Btn_Click(object sender, EventArgs e)
        {
           
        }

        private void FrmEngBonus_Load(object sender, EventArgs e)
        {
            editProdmonth.EditValue = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsDate(ProductionAmplatsGlobalTSysSettings._currentProductionMonth.ToString());


        }

       

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "select *, case when tt is not null then 'T' when pp is not null then 'P' else 'N' end as aa  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "from (select distinct(substring(orgunit,1,6)) oo from tbl_bcs_General_Orgunits  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') a  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "left outer join  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "(select distinct(gang) pp from [mineware].dbo.[tbl_BCS_New_Status] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and status = 'P') b  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "on a.oo = b.pp  \r\n";


                _dbMan.SqlStatement = _dbMan.SqlStatement + "left outer join  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "(select distinct(orgunit) tt from [mineware].[dbo].[tbl_BCS_ARMS_Interface_Transfer_EngNew]  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') c  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "on a.oo = c.tt  order by oo\r\n";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ExecuteInstruction();

                lbIncomplete.Items.Clear();
                lbPrinted.Items.Clear();
                lbTransfer.Items.Clear();
                Showlabel.Text = "Nothing";

                DataTable dt = _dbMan.ResultsDataTable;

                foreach (DataRow r in dt.Rows)
                {
                    if (r["aa"].ToString() == "N")
                    {
                        lbIncomplete.Items.Add(r["oo"].ToString());
                    }

                    if (r["aa"].ToString() == "P")
                    {
                        lbPrinted.Items.Add(r["oo"].ToString());
                    }

                    if (r["aa"].ToString() == "T")
                    {
                        lbTransfer.Items.Add(r["oo"].ToString());
                    }
                }
            }


        }

        private void ProdRB_CheckedChanged(object sender, EventArgs e)
        {
            if (ProdRB.Checked == true)
            {
                groupBox1.Visible = true;
            }
            else
            {
                groupBox1.Visible = false;
            }
        }

        private void PlantRB_CheckedChanged(object sender, EventArgs e)
        {
            if (PlantRB.Checked == true)
            {
                groupBox2.Visible = true;
            }
            else
            {
                groupBox2.Visible = false;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "select *, case when tt is not null then 'T' when pp is not null then 'P' else 'N' end as aa  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "from (select distinct(substring(orgunit,1,7)) oo from bmcs_Workshop_Orgunits  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') a  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "left outer join  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "(select distinct(gang) pp from [mineware].dbo.[tbl_BCS_New_Status] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and status = 'P') b  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "on a.oo = b.pp  \r\n";


                _dbMan.SqlStatement = _dbMan.SqlStatement + "left outer join  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "(select distinct(orgunit) tt from [mineware].[dbo].[tbl_BCS_ARMS_Interface_Transfer_EngNew]  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') c  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "on a.oo = c.tt    order by oo\r\n";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ExecuteInstruction();

                lbIncomplete.Items.Clear();
                lbPrinted.Items.Clear();
                lbTransfer.Items.Clear();
                Showlabel.Text = "Nothing";

                DataTable dt = _dbMan.ResultsDataTable;

                foreach (DataRow r in dt.Rows)
                {
                    if (r["aa"].ToString() == "N")
                    {
                        lbIncomplete.Items.Add(r["oo"].ToString());
                    }

                    if (r["aa"].ToString() == "P")
                    {
                        lbPrinted.Items.Add(r["oo"].ToString());
                    }

                    if (r["aa"].ToString() == "T")
                    {
                        lbTransfer.Items.Add(r["oo"].ToString());
                    }
                }
            }
        }

        private void ShaftRB_CheckedChanged(object sender, EventArgs e)
        {
            if (ShaftRB.Checked == true)
            {
                 MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "select *, case when tt is not null then 'T' when pp is not null then 'P' else 'N' end as aa  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "from (select distinct(substring(orgunit,1,5)) oo from bmcs_Shaft_Orgunits  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' union select distinct(substring(orgunit,1,6)) oo from bmcs_Shaft_Orgunits where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' union select distinct(substring(orgunit,1,7)) oo from bmcs_Shaft_Orgunits where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') a  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "left outer join  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "(select distinct(gang) pp from [mineware].dbo.[tbl_BCS_New_Status] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and status = 'P') b  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "on a.oo = b.pp  \r\n";


                _dbMan.SqlStatement = _dbMan.SqlStatement + "left outer join  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "(select distinct(orgunit) tt from [mineware].[dbo].[tbl_BCS_ARMS_Interface_Transfer_EngNew]  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') c  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "on a.oo = c.tt    order by oo\r\n";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ExecuteInstruction();

                lbIncomplete.Items.Clear();
                lbPrinted.Items.Clear();
                lbTransfer.Items.Clear();
                Showlabel.Text = "Nothing";

                DataTable dt = _dbMan.ResultsDataTable;

                foreach (DataRow r in dt.Rows)
                {
                    if (r["aa"].ToString() == "N")
                    {
                        lbIncomplete.Items.Add(r["oo"].ToString());
                    }

                    if (r["aa"].ToString() == "P")
                    {
                        lbPrinted.Items.Add(r["oo"].ToString());
                    }

                    if (r["aa"].ToString() == "T")
                    {
                        lbTransfer.Items.Add(r["oo"].ToString());
                    }
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "select *, case when tt is not null then 'T' when pp is not null then 'P' else 'N' end as aa  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "from (select distinct(substring(orgunit,1,5)) oo from bmcs_TSDVENT_Orgunits  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') a  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "left outer join  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "(select distinct(gang) pp from [mineware].dbo.[tbl_BCS_New_Status] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and status = 'P') b  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "on a.oo = b.pp  \r\n";


                _dbMan.SqlStatement = _dbMan.SqlStatement + "left outer join  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "(select distinct(orgunit) tt from [mineware].[dbo].[tbl_BCS_ARMS_Interface_Transfer_EngNew]  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') c  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "on a.oo = c.tt    order by oo\r\n";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ExecuteInstruction();

                lbIncomplete.Items.Clear();
                lbPrinted.Items.Clear();
                lbTransfer.Items.Clear();
                Showlabel.Text = "Nothing";

                DataTable dt = _dbMan.ResultsDataTable;

                foreach (DataRow r in dt.Rows)
                {
                    if (r["aa"].ToString() == "N")
                    {
                        lbIncomplete.Items.Add(r["oo"].ToString());
                    }

                    if (r["aa"].ToString() == "P")
                    {
                        lbPrinted.Items.Add(r["oo"].ToString());
                    }

                    if (r["aa"].ToString() == "T")
                    {
                        lbTransfer.Items.Add(r["oo"].ToString());
                    }
                }
            }
        }

        private void ProdMonth1Txt_TextChanged(object sender, EventArgs e)
        {
            lbIncomplete.Items.Clear();
            lbPrinted.Items.Clear();
            lbTransfer.Items.Clear();
            Showlabel.Text = "Nothing";

            NewradioBtn.Checked = false;

            if (Convert.ToDecimal(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) > 201602)
                NewradioBtn.Checked = true;
        }

        private void BMRRB_CheckedChanged(object sender, EventArgs e)
        {
            if (BMRRB.Checked == true)
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "select *, case when tt is not null then 'T' when pp is not null then 'P' else 'N' end as aa  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "from (select distinct(orgunit) oo from bmcs_Eng_Orgunits  where calendartype = 'Plant Accounting' and orgunittype = 'BMR' \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "and yearmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') a  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "left outer join  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "(select distinct(gang) pp from [mineware].dbo.[tbl_BCS_New_Status] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and status = 'P') b  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "on a.oo = b.pp  \r\n";


                _dbMan.SqlStatement = _dbMan.SqlStatement + "left outer join  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "(select distinct(orgunit) tt from [mineware].[dbo].[tbl_BCS_ARMS_Interface_Transfer_EngNew]  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') c  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "on a.oo = c.tt    order by oo\r\n";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ExecuteInstruction();

                lbIncomplete.Items.Clear();
                lbPrinted.Items.Clear();
                lbTransfer.Items.Clear();
                Showlabel.Text = "Nothing";

                DataTable dt = _dbMan.ResultsDataTable;

                foreach (DataRow r in dt.Rows)
                {
                    if (r["aa"].ToString() == "N")
                    {
                        lbIncomplete.Items.Add(r["oo"].ToString());
                    }

                    if (r["aa"].ToString() == "P")
                    {
                        lbPrinted.Items.Add(r["oo"].ToString());
                    }

                    if (r["aa"].ToString() == "T")
                    {
                        lbTransfer.Items.Add(r["oo"].ToString());
                    }
                }
            }
        }

        private void SmelterRB_CheckedChanged(object sender, EventArgs e)
        {
            if (SmelterRB.Checked == true)
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "select *, case when tt is not null then 'T' when pp is not null then 'P' else 'N' end as aa  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "from (select distinct(orgunit) oo from bmcs_Eng_Orgunits  where calendartype = 'Plant Accounting' and orgunittype = 'Smelter' \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "and yearmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') a  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "left outer join  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "(select distinct(gang) pp from [mineware].dbo.[tbl_BCS_New_Status] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and status = 'P') b  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "on a.oo = b.pp  \r\n";


                _dbMan.SqlStatement = _dbMan.SqlStatement + "left outer join  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "(select distinct(orgunit) tt from [mineware].[dbo].[tbl_BCS_ARMS_Interface_Transfer_EngNew]  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') c  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "on a.oo = c.tt    order by oo\r\n";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ExecuteInstruction();

                lbIncomplete.Items.Clear();
                lbPrinted.Items.Clear();
                lbTransfer.Items.Clear();
                Showlabel.Text = "Nothing";

                DataTable dt = _dbMan.ResultsDataTable;

                foreach (DataRow r in dt.Rows)
                {
                    if (r["aa"].ToString() == "N")
                    {
                        lbIncomplete.Items.Add(r["oo"].ToString());
                    }

                    if (r["aa"].ToString() == "P")
                    {
                        lbPrinted.Items.Add(r["oo"].ToString());
                    }

                    if (r["aa"].ToString() == "T")
                    {
                        lbTransfer.Items.Add(r["oo"].ToString());
                    }
                }
            }
        }

        private void ConcRB_CheckedChanged(object sender, EventArgs e)
        {
            if (ConcRB.Checked == true)
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "select *, case when tt is not null then 'T' when pp is not null then 'P' else 'N' end as aa  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "from (select distinct(orgunit) oo from bmcs_Eng_Orgunits  where calendartype = 'Plant Accounting' and orgunittype = 'Concentrator' \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "and yearmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') a  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "left outer join  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "(select distinct(gang) pp from [mineware].dbo.[tbl_BCS_New_Status] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and status = 'P') b  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "on a.oo = b.pp  \r\n";


                _dbMan.SqlStatement = _dbMan.SqlStatement + "left outer join  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "(select distinct(orgunit) tt from [mineware].[dbo].[tbl_BCS_ARMS_Interface_Transfer_EngNew]  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') c  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "on a.oo = c.tt    order by oo\r\n";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ExecuteInstruction();

                lbIncomplete.Items.Clear();
                lbPrinted.Items.Clear();
                lbTransfer.Items.Clear();
                Showlabel.Text = "Nothing";

                DataTable dt = _dbMan.ResultsDataTable;

                foreach (DataRow r in dt.Rows)
                {
                    if (r["aa"].ToString() == "N")
                    {
                        lbIncomplete.Items.Add(r["oo"].ToString());
                    }

                    if (r["aa"].ToString() == "P")
                    {
                        lbPrinted.Items.Add(r["oo"].ToString());
                    }

                    if (r["aa"].ToString() == "T")
                    {
                        lbTransfer.Items.Add(r["oo"].ToString());
                    }
                }
            }
        }

        private void ChiefTechRB_CheckedChanged(object sender, EventArgs e)
        {
            if (ChiefTechRB.Checked == true)
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "select *, case when tt is not null then 'T' when pp is not null then 'P' else 'N' end as aa  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "from (select distinct(orgunit) oo from bmcs_Eng_Orgunits  where calendartype = 'Plant Accounting' and orgunittype = 'Chief Technician' \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "and yearmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') a  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "left outer join  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "(select distinct(gang) pp from [mineware].dbo.[tbl_BCS_New_Status] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and status = 'P') b  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "on a.oo = b.pp  \r\n";


                _dbMan.SqlStatement = _dbMan.SqlStatement + "left outer join  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "(select distinct(orgunit) tt from [mineware].[dbo].[tbl_BCS_ARMS_Interface_Transfer_EngNew]  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') c  \r\n";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "on a.oo = c.tt    order by oo\r\n";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ExecuteInstruction();

                lbIncomplete.Items.Clear();
                lbPrinted.Items.Clear();
                lbTransfer.Items.Clear();
                Showlabel.Text = "Nothing";

                DataTable dt = _dbMan.ResultsDataTable;

                foreach (DataRow r in dt.Rows)
                {
                    if (r["aa"].ToString() == "N")
                    {
                        lbIncomplete.Items.Add(r["oo"].ToString());
                    }

                    if (r["aa"].ToString() == "P")
                    {
                        lbPrinted.Items.Add(r["oo"].ToString());
                    }

                    if (r["aa"].ToString() == "T")
                    {
                        lbTransfer.Items.Add(r["oo"].ToString());
                    }
                }
            }
        }

        private void lbIncomplete_Click(object sender, EventArgs e)
        {
            Showlabel.Text = lbIncomplete.SelectedItem.ToString();
            lbPrinted.SelectedIndex = -1;
            lbTransfer.SelectedIndex = -1;

        }

        private void lbPrinted_Click(object sender, EventArgs e)
        {
            Showlabel.Text = lbPrinted.SelectedItem.ToString();
            lbIncomplete.SelectedIndex = -1;
            lbTransfer.SelectedIndex = -1;
        }

        private void lbTransfer_Click(object sender, EventArgs e)
        {
            Showlabel.Text = lbTransfer.SelectedItem.ToString();
            lbIncomplete.SelectedIndex = -1;
            lbPrinted.SelectedIndex = -1;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
           
            
        }

        private void editActivity_EditValueChanged(object sender, EventArgs e)
        {
            if (editActivity.EditValue.ToString() == "0")
            {
                ProdRB.Checked = true;
                ShaftRB.Checked = false;
                PlantRB.Checked = false;
            }
            if (editActivity.EditValue.ToString() == "1")
            {
                ProdRB.Checked = false;
                ShaftRB.Checked = true;
                PlantRB.Checked = false;
            }
            if (editActivity.EditValue.ToString() == "2")
            {
                ProdRB.Checked = false;
                ShaftRB.Checked = false;
                PlantRB.Checked = true;
            }
        }

        private void editProduction_EditValueChanged(object sender, EventArgs e)
        {
            if (editProduction.EditValue.ToString() == "0")
            {
                radioButton4.Checked = true;
                radioButton3.Checked = false;
                radioButton2.Checked = false;
            }
            if (editProduction.EditValue.ToString() == "1")
            {
                radioButton4.Checked = false;
                radioButton3.Checked = true;
                radioButton2.Checked = false;
            }
            if (editProduction.EditValue.ToString() == "2")
            {
                radioButton4.Checked = false;
                radioButton3.Checked = false;
                radioButton2.Checked = true;
            }
        }

        private void btnShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ProdRB.Checked == true)
            {
                if (radioButton4.Checked == true)
                {
                    ProductionBonus();
                }
            }

            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan1.SqlStatement = " delete from [mineware].dbo.tbl_BCS_New_Status where gang = '" + Showlabel.Text + "' and prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' insert into [mineware].dbo.tbl_BCS_New_Status Values( '" + Showlabel.Text + "', 'E', '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + 'P' + "')\r\n";
            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();


            LoadGrid();
            LoadReport();
        }

        private void btnTransfer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            //check if trans
            MWDataManager.clsDataAccess _dbManc = new MWDataManager.clsDataAccess();
            _dbManc.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManc.SqlStatement = " select * from [mineware].[dbo].[tbl_BCS_ARMS_Interface_Transfer_EngNew] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunit = '" + Showlabel.Text + "' ";
            _dbManc.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManc.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManc.ExecuteInstruction();

            DataTable nn = _dbManc.ResultsDataTable;

            if (nn.Rows.Count > 0)
            {
                // MessageBox.Show("Data already Transfered", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //return;
            }


            MWDataManager.clsDataAccess _dbManNS = new MWDataManager.clsDataAccess();
            _dbManNS.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManNS.SqlStatement = "  ";

            _dbManNS.SqlStatement = _dbManNS.SqlStatement + " ";

            _dbManNS.SqlStatement = _dbManNS.SqlStatement + " delete from [mineware].[dbo].[tbl_BCS_ARMS_Interface_Transfer_EngNew]  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and [OrgUnit] = '" + Showlabel.Text + "'  \r\n";

            for (int k = 0; k <= EngBonusPCGrid.RowCount - 2; k++)
            {
                //_dbManNS.SqlStatement = _dbManNS.SqlStatement + " delete from [mineware].[dbo].[tbl_BMCS_ARMS_Interface_Transfer_Eng]  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and [OrgUnit] = '" + Showlabel.Text + "'  \r\n";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " insert into [mineware].[dbo].[tbl_BCS_ARMS_Interface_Transfer_EngNew] ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " values( getdate(), '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', 'E','" + EngBonusPCGrid.Rows[k].Cells[0].Value.ToString() + "', \r\n";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + "  '" + EngBonusPCGrid.Rows[k].Cells[1].Value.ToString() + "' ,'" + EngBonusPCGrid.Rows[k].Cells[54].Value.ToString() + "' ,    \r\n ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + "  '" + Showlabel.Text + "' ,'" + EngBonusPCGrid.Rows[k].Cells[48].Value.ToString() + "' ,    \r\n ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + "  '" + EngBonusPCGrid.Rows[k].Cells[50].Value.ToString() + "' ,'" + EngBonusPCGrid.Rows[k].Cells[52].Value.ToString() + "' ,    \r\n ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + "  'Y',null ,'" + EngBonusPCGrid.Rows[k].Cells[76].Value.ToString() + "' )    \r\n ";

            }
            _dbManNS.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManNS.queryReturnType = MWDataManager.ReturnType.longNumber;
            _dbManNS.ExecuteInstruction();





            MessageBox.Show("Data Transfered succesfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            radioButton4_CheckedChanged(null, null);
            radioButton3_CheckedChanged(null, null);
            radioButton2_CheckedChanged(null, null);
            ShaftRB_CheckedChanged(null, null);
            BMRRB_CheckedChanged(null, null);
            SmelterRB_CheckedChanged(null, null);
            ConcRB_CheckedChanged(null, null);
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OnCloseTabRequest(new CloseTabArg(tabCaption));
        }

        private void editProdmonth_EditValueChanged(object sender, EventArgs e)
        {
            lbIncomplete.Items.Clear();
            lbPrinted.Items.Clear();
            lbTransfer.Items.Clear();
            Showlabel.Text = "Nothing";

            NewradioBtn.Checked = false;

            if (Convert.ToDecimal(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) > 201602)
                NewradioBtn.Checked = true;

            radioButton4.Checked = false;
            radioButton4.Checked = true;
            
        }
    }
}
