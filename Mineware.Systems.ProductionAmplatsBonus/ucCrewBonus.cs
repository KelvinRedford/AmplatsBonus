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
    public partial class ucCrewBonus : BaseUserControl
    {

        DataSet ReportCrewBonus = new DataSet();
        private string _reportFolder = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\Reports\";
        public ucCrewBonus()
        {
            InitializeComponent();
            FormRibbonPages.Add(rpReports);
            FormActiveRibbonPage = rpReports;
            FormMainRibbonPage = rpReports;
            RibbonControl = rcReports;
        }

        private void Close1Btn_Click(object sender, EventArgs e)
        {
            
        }

        string UseTable = "";

        //double ZeroLTI2 = 0;
        //double OneLTI2 = 0;
        //double TwoLTI2 = 0;
        //double ThreeLTI2 = 0;


        int DSLTI = 0;
        int NSLTI = 0;

        Decimal DSLTIFACT = 0;
        Decimal NSLTIFACT = 0;


        Decimal StopingBIP = 0;

        


        Report report = new Report();

        private void frmStpCrewBonus_Load(object sender, EventArgs e)
        {
            editProdmonth.EditValue = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsDate(ProductionAmplatsGlobalTSysSettings._currentProductionMonth.ToString());

        }

        

        public void LoadListBoxes()
        {
            //
            // Do 'Stoping'
            //
            #region
            if (rdbStoping.Checked == true)
            {
                //
                //  Load Incomplete List Box
                //
                lbIncomplete.Items.Clear();

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = "select distinct(orgunit) wp from mineware.dbo.[vw_bcs_survey] \r\n " +
                                      "where activitycode = 0 and prodmonth = '"+ ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n "+
                                      "and orgunit not in (select gang from Mineware.dbo.tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activity = 'S') \r\n " +
                                      "and orgunit not in (select distinct(orgunit) orgunit from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0') \r\n " +
                                      "and orgunit not like '" + "______R" + "%' and orgunit not like '____T%' and SUBSTRING([orgunit], 7,1) <> 'Q' and SUBSTRING([orgunit], 7,1) <> 'D' \r\n " +
                                      "order by orgunit ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Stoping";
                _dbMan.ExecuteInstruction();

                DataTable dt = _dbMan.ResultsDataTable;

                lbIncomplete.Items.Add("");
                foreach (DataRow dr in dt.Rows)
                {
                    lbIncomplete.Items.Add(dr["wp"].ToString());
                }

                //
                //Load Printed Listbox
                //
                lbTransfer.Items.Clear();

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = "select gang from Mineware.dbo.tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activity = 'S' \r\n " +
                                       "and gang not in (select distinct(orgunit) orgunit from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew  \r\n "+
                                       "where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0') \r\n " +
                                       "and gang not like '" + "______R" + "%' and gang not like '" + "_______Z" + "' and gang not like '____T%' and SUBSTRING([gang], 7,1) <> 'Q' and SUBSTRING([gang], 7,1) <> 'D' \r\n " +
                                       "order by gang ";
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
                _dbMan2.SqlStatement = "select orgunit, max(transferred) trans from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew \r\n "+
                                       "where prodmonth = '"+ ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0' \r\n "+
                                       "and orgunit not like '" + "______R" + "%' and orgunit not like '" + "_______Z" + "' and orgunit not like '____T%' and SUBSTRING([OrgUnit], 7,1) <> 'Q' and SUBSTRING([OrgUnit], 7,1) <> 'D' \r\n " +
                                       "and shift = 'D' and activitycode = 0 and type = '28'  \r\n " +
                                       "group by orgunit order by orgunit ";
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
            #endregion

            //
            // Do 'R Crews'
            //
            #region


            #endregion

            //
            // Do 'Z-Crew'
            //
            #region
            if (rdbZCrew.Checked == true)
            {
                //
                //  Load Incomplete List Box
                //
                lbIncomplete.Items.Clear();

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                 _dbMan.SqlStatement = _dbMan.SqlStatement + "  select * from (  ";



                 if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) < 201807)
                 {
                     _dbMan.SqlStatement = _dbMan.SqlStatement + " select * from (select case when LEN(OrgunitDS) > 8 then (substring(orgunitds,1,8) + 'Z' ) else (substring(orgunitds,1,7) + 'Z' ) end as aa from BMCS_ZGangAverageNew ";
                     _dbMan.SqlStatement = _dbMan.SqlStatement + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";
                     _dbMan.SqlStatement = _dbMan.SqlStatement + " ) a ";
                 }
                 else
                 {
                     _dbMan.SqlStatement = _dbMan.SqlStatement + " select * from (select [ZGangID] aa from mineware.dbo.[tbl.BCS_ZGangsLink] ) a ";
                 }                 
                     
                     
                     
                     
                _dbMan.SqlStatement = _dbMan.SqlStatement + " where aa not in (select gang from tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activity = 'S') ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " and aa not in (select distinct(orgunit) orgunit from tbl_BCS_ARMS_Interface_TransferNew where ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0'))qwerty  group by aa order by aa ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "ZCrew";
                _dbMan.ExecuteInstruction();


               
                DataTable dt = _dbMan.ResultsDataTable;

                lbIncomplete.Items.Add("");
                foreach (DataRow dr in dt.Rows)
                {
                    lbIncomplete.Items.Add(dr["aa"].ToString());
                }

                //
                //Load Printed Listbox
                //
                lbTransfer.Items.Clear();

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " select gang from Mineware.dbo.tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  and activity = 'S' ";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and (gang like '" + "_______Z%" + "' or gang like '" + "________Z%" + "' or gang like '" + "________W%" + "' or gang like '" + "_______W%" + "') and gang not in (select distinct(orgunit) orgunit from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0') ";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " order by gang ";
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
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " select orgunit, max(transferred) trans from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0' ";
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and (orgunit like '" + "_______Z%" + "' or orgunit like '" + "________Z%" + "'or orgunit like '" + "________W%" + "' or orgunit like '" + "_______W%" + "') and shift = 'D' and activitycode = '0' group by orgunit order by orgunit ";
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
            #endregion

            //
            // Do 'Construction-Crew'
            //
            #region
            if (rdbConstCrew.Checked == true)
            {
                //
                //  Load Incomplete List Box
                //
                lbIncomplete.Items.Clear();

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                if (UseTable == "Old")
                {

                    _dbMan.SqlStatement = _dbMan.SqlStatement + " select distinct(OrgUnit) OrgUnit from tbl_BCS_Gangs ";
                }
                else
                {
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " select distinct(OrgUnit) OrgUnit from tbl_BCS_Gangs_3Month ";
                }
                _dbMan.SqlStatement = _dbMan.SqlStatement + " where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " and OrgUnit in ( ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " select distinct([OrgUnit]) OrgUnit from dbo.tbl_Import_BMCS_Clocking_Total ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " where SUBSTRING([OrgUnit], 7,1) = 'Q' ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " and SUBSTRING([OrgUnit],1,4) in ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " (select distinct(SUBSTRING(SectionID,1,4)) MO from mineware.dbo.tbl_BCS_Planning ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and Activity <> 1) ";
                //_dbMan.SqlStatement = _dbMan.SqlStatement + " and orgunit not in (select Gang from tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ) ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " ) and orgunit not in (select Gang from tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' )  order by OrgUnit ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "ZCrew";
                _dbMan.ExecuteInstruction();

                DataTable dt = _dbMan.ResultsDataTable;

                lbIncomplete.Items.Add("");
                foreach (DataRow dr in dt.Rows)
                {
                    lbIncomplete.Items.Add(dr["OrgUnit"].ToString());
                }

                //
                //Load Printed Listbox
                //
                lbTransfer.Items.Clear();

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " select gang from Mineware.dbo.tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activity = 'S' ";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and gang not in (select distinct(orgunit) orgunit from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0') ";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and gang not like '______R%' and gang not like '_______Z' ";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and gang like '%Q%' ";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " order by gang ";
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
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " select orgunit, max(transferred) trans from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0'  ";
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and orgunit not like '______R%' and orgunit not like '_______Z'  ";
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and shift ='D' and activitycode = 0 and type = '28' and orgunit like '%Q%' group by orgunit order by orgunit  ";
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
            #endregion

            //
            // Do 'XC-Crew'
            //
            #region
            if (rdbXCCrew.Checked == true)
            {
                //
                //  Load Incomplete List Box
                //
                lbIncomplete.Items.Clear();

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                if (UseTable == "Old")
                {
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " select distinct(OrgUnit) OrgUnit from tbl_BCS_Gangs ";
                }
                else
                {
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " select distinct(OrgUnit) OrgUnit from tbl_BCS_Gangs_3Month ";
                }
                _dbMan.SqlStatement = _dbMan.SqlStatement + " where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " and OrgUnit in ( ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " select distinct([OrgUnit]) OrgUnit from dbo.tbl_Import_BMCS_Clocking_Total ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " where (SUBSTRING([OrgUnit], 7,1) = 'D' or  SUBSTRING([OrgUnit], 5,1) = 'X') ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " and SUBSTRING([OrgUnit],1,4) in ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " (select distinct(SUBSTRING(SectionID,1,4)) MO from mineware.dbo.tbl_BCS_Planning";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and Activity <> 1) ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " and orgunit not in (select Gang from tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ) ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " ) and SUBSTRING(OrgUnit,5,1) <> 'T' order by OrgUnit ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "ZCrew";
                _dbMan.ExecuteInstruction();

                DataTable dt = _dbMan.ResultsDataTable;

                lbIncomplete.Items.Add("");
                foreach (DataRow dr in dt.Rows)
                {
                    lbIncomplete.Items.Add(dr["OrgUnit"].ToString());
                }

                //
                //Load Printed Listbox
                //
                lbTransfer.Items.Clear();

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " select gang from Mineware.dbo.tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activity = 'S' ";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and gang not in (select distinct(orgunit) orgunit from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0') ";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and gang not like '______R%' and gang not like '_______Z' ";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and (SUBSTRING([gang], 7,1) = 'D' or  SUBSTRING([gang], 5,1) = 'X') ";
                
                //_dbMan1.SqlStatement = _dbMan1.SqlStatement + " and gang like '______D'+'%' ";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " order by gang ";
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
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " select orgunit, max(transferred) trans from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0' ";
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and orgunit not like '______R%' and orgunit not like '_______Z' ";
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and shift = 'D' and activitycode = 0 and type = '28' ";
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and (SUBSTRING([orgunit], 7,1) = 'D' or  SUBSTRING([orgunit], 5,1) = 'X') group by orgunit order by orgunit";
               // and orgunit like '______D'+'%' and orgunit not like '____T'+'%' group by orgunit order by orgunit ";
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
            #endregion


            //
            // Do 'Development'
            //
            #region
            if (rdbDev.Checked == true)
            {
                //
                //  Load Incomplete List Box
                //
                lbIncomplete.Items.Clear();

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = _dbMan.SqlStatement + " select distinct(orgunit) wp from mineware.dbo.[vw_bcs_survey] ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " where activitycode = 1 and prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " and orgunit not in (select gang from Mineware.dbo.tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activity = 'D') ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " and orgunit not in (select distinct(orgunit) orgunit from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '1') ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " order by orgunit ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Dev";
                _dbMan.ExecuteInstruction();

                DataTable dt = _dbMan.ResultsDataTable;

                lbIncomplete.Items.Add("");
                foreach (DataRow dr in dt.Rows)
                {
                    lbIncomplete.Items.Add(dr["wp"].ToString());
                }

                //
                //Load Printed Listbox
                //
                lbTransfer.Items.Clear();

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " select gang from Mineware.dbo.tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activity = 'D' ";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and gang not in (select distinct(orgunit) orgunit from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '1') ";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and SUBSTRING(Gang, 5,1) <> 'T' ";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " order by gang ";
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
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " select orgunit, max(transferred) trans from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  ";
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and shift = 'D' and activitycode = 1 and type = '28' and orgunit in (select distinct(orgunit) wp from mineware.dbo.[vw_bcs_survey] ";
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " where activitycode = 1 and prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') group by orgunit order by orgunit ";
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
            #endregion

            //
            // Do 'Dev Rail Crew'
            //
            #region
            if (rdbDevRailCrew.Checked == true)
            {
                //
                //  Load Incomplete List Box
                //
                lbIncomplete.Items.Clear();

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " select distinct([OrgUnit]) OrgUnit from dbo.tbl_Import_BMCS_Clocking_Total  ";
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " where ( (SUBSTRING([OrgUnit], 8,1) = 'A' and SUBSTRING([OrgUnit], 5,1) in ( 'T', 'X')) or ( SUBSTRING([OrgUnit], 7,2) in ('T5'))  or ( SUBSTRING([OrgUnit], 7,2) in ('T7'))   or ( SUBSTRING([OrgUnit], 7,2) in ('T4') )  or ( SUBSTRING([OrgUnit], 7,2) in ('T6') )   and SUBSTRING([OrgUnit], 0,5) not in ( '0743T') ";
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " and SUBSTRING([OrgUnit],1,4) in ";
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " (select distinct(SUBSTRING(SectionID,1,4)) MO from mineware.dbo.tbl_BCS_Planning ";
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and Activity = 1)  ";
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " and orgunit not in (select Gang from tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' )) ";
                   
                    _dbMan.SqlStatement = _dbMan.SqlStatement + "  union select distinct([OrgUnit]) OrgUnit from dbo.tbl_Import_BMCS_Clocking_Total  ";
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " where SUBSTRING([OrgUnit], 6,1) in ( 'T', 'X') and SUBSTRING([OrgUnit],1,4) in  ";
                    _dbMan.SqlStatement = _dbMan.SqlStatement + "(select distinct(SUBSTRING(SectionID,1,4)) MO from mineware.dbo.tbl_BCS_Planning ";
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and Activity = 1) ";
                
                
                        _dbMan.SqlStatement = _dbMan.SqlStatement + " order by [OrgUnit] ";
               

                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;


                _dbMan.ResultsTableName = "Dev";
                _dbMan.ExecuteInstruction();

                DataTable dt = _dbMan.ResultsDataTable;

                lbIncomplete.Items.Add("");
                foreach (DataRow dr in dt.Rows)
                {
                    lbIncomplete.Items.Add(dr["OrgUnit"].ToString());
                }

                //
                //Load Printed Listbox
                //
                lbTransfer.Items.Clear();

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                //if (Convert.ToDecimal(ProdMonthTxt.Text) > 201603)
                //{
                //    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " select gang from Mineware.dbo.tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activity = 'D' ";
                //    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and gang not in (select distinct(orgunit) orgunit from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0') ";
                //    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and SUBSTRING(Gang, 5,1) = 'X' ";
                //    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " order by gang ";
                //}
                //else
                //{
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " select gang from Mineware.dbo.tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activity = 'D' ";
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and gang not in (select distinct(orgunit) orgunit from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0') ";
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and ( (SUBSTRING(gang, 8,1) = 'A' and SUBSTRING(gang, 5,1) in ( 'T', 'X'))  or ( SUBSTRING(gang, 7,2) in ('T7'))  or ( SUBSTRING(gang, 7,2) in ('T5'))  or ( SUBSTRING([gang], 7,2) in ('T6') )  )  ";

                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " union select gang from Mineware.dbo.tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activity = 'D' ";
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and gang not in (select distinct(orgunit) orgunit from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0') ";
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and SUBSTRING(gang, 6,1) in ( 'T', 'X')  ";
                   
                
                
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " order by gang ";
                //}

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
                //if (Convert.ToDecimal(ProdMonthTxt.Text) > 201603)
                //{
                //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " select orgunit, max(transferred) trans from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0'  ";
                //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and SUBSTRING(orgunit, 5,1) = 'X'   ";
                //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and shift = 'D' and activitycode = 0 and type = '28' ";
                //    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " group by orgunit order by orgunit ";
                //}
                //else
                //{
                    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " select orgunit, max(transferred) trans from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0'  ";
                    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and ( (SUBSTRING(orgunit, 8,1) = 'A' and SUBSTRING(orgunit, 5,1) in ( 'T', 'X')) or ( SUBSTRING(orgunit, 7,2) in ('T5'))  or ( SUBSTRING([OrgUnit], 7,2) in ('T7'))  or ( SUBSTRING([OrgUnit], 7,2) in ('T6') )   )   ";
                    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and shift = 'D' and activitycode = 0 and type = '28' ";
                    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " group by orgunit ";

                    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " union select orgunit, max(transferred) trans from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0'  ";
                    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and SUBSTRING(orgunit, 6,1) in ( 'T', 'X')   ";
                    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and shift = 'D' and activitycode = 0 and type = '28' ";
                    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " group by orgunit ";
                
                
                
                
                    _dbMan2.SqlStatement = _dbMan2.SqlStatement + " order by orgunit ";

                //}
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
            #endregion



            //
            // Do 'Dev Transport Crew'
            //
            #region
            if (rdbDevTransCrew.Checked == true)
            {
                //
                //  Load Incomplete List Box
                //
                lbIncomplete.Items.Clear();

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                //if (Convert.ToDecimal(ProdMonthTxt.Text) > 201603)
                //{
                //    _dbMan.SqlStatement = _dbMan.SqlStatement + " select distinct([OrgUnit]) OrgUnit from dbo.tbl_Import_BMCS_Clocking_Total  ";
                //    _dbMan.SqlStatement = _dbMan.SqlStatement + " where SUBSTRING([OrgUnit], 5,1) = 'X' ";
                //    _dbMan.SqlStatement = _dbMan.SqlStatement + " and SUBSTRING([OrgUnit],1,4) in  ";
                //    _dbMan.SqlStatement = _dbMan.SqlStatement + " (select distinct(SUBSTRING(SectionID,1,4)) MO from mineware.dbo.tbl_BCS_Planning ";
                //    _dbMan.SqlStatement = _dbMan.SqlStatement + "  where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and Activity = 1) ";
                //    _dbMan.SqlStatement = _dbMan.SqlStatement + " and orgunit not in (select Gang from tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ) ";
                //    _dbMan.SqlStatement = _dbMan.SqlStatement + " order by [OrgUnit] ";
                //}
                //else
                //{
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " select distinct([OrgUnit]) OrgUnit from dbo.tbl_Import_BMCS_Clocking_Total  ";
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " where ((SUBSTRING([OrgUnit], 8,1) = 'B' and SUBSTRING([OrgUnit], 5,1) in ( 'T', 'X') ) or ( SUBSTRING(orgunit, 6,2) in ('T4')) or ( SUBSTRING(orgunit, 6,2) in ('T5')) )  ";
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " and SUBSTRING([OrgUnit],1,4) in  ";
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " (select distinct(SUBSTRING(SectionID,1,4)) MO from mineware.dbo.tbl_BCS_Planning ";
                    _dbMan.SqlStatement = _dbMan.SqlStatement + "  where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and Activity = 1) ";
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " and orgunit not in (select Gang from tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ) ";


                    _dbMan.SqlStatement = _dbMan.SqlStatement + " union select distinct([OrgUnit]) OrgUnit from dbo.tbl_Import_BMCS_Clocking_Total  ";
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " where SUBSTRING([OrgUnit], 5,3) in ( 'D1T') ";
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " and SUBSTRING([OrgUnit],1,4) in  ";
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " (select distinct(SUBSTRING(SectionID,1,4)) MO from mineware.dbo.tbl_BCS_Planning ";
                    _dbMan.SqlStatement = _dbMan.SqlStatement + "  where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and Activity = 1) ";
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " and orgunit not in (select Gang from tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ) ";
                    
                
                
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " order by [OrgUnit] ";

                //}
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Dev";
                _dbMan.ExecuteInstruction();

                DataTable dt = _dbMan.ResultsDataTable;

                lbIncomplete.Items.Add("");
                foreach (DataRow dr in dt.Rows)
                {
                    lbIncomplete.Items.Add(dr["OrgUnit"].ToString());
                }

                //
                //Load Printed Listbox
                //
                lbTransfer.Items.Clear();

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                //if (Convert.ToDecimal(ProdMonthTxt.Text) > 201603)
                //{
                //    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " select gang from Mineware.dbo.tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activity = 'D' ";
                //    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and gang not in (select distinct(orgunit) orgunit from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0') ";
                //    _dbMan1.SqlStatement = _dbMan1.SqlStatement + "  and SUBSTRING(Gang, 5,1) = 'X' ";
                //    _dbMan1.SqlStatement = _dbMan1.SqlStatement + "  order by gang ";
                //}
                //else
                //{
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " select gang from Mineware.dbo.tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activity = 'D' ";
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and gang not in (select distinct(orgunit) orgunit from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0') ";
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and ((SUBSTRING([gang], 8,1) = 'B' and SUBSTRING([gang], 5,1) in ( 'T', 'X') ) or ( SUBSTRING(gang, 6,2) in ('T4')) or ( SUBSTRING(gang, 6,2) in ('T5')) ) ";

                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " union select gang from Mineware.dbo.tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activity = 'D' ";
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and gang not in (select distinct(orgunit) orgunit from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0') ";
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and  SUBSTRING([gang], 5,3) in ( 'D1T')  ";
                    
                
                
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + "  order by gang ";
                //}
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
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " select orgunit, max(transferred) trans from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0'  ";
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + "  and ((SUBSTRING([orgunit], 8,1) = 'B' and SUBSTRING([orgunit], 5,1) in ( 'T', 'X') ) or ( SUBSTRING(orgunit, 6,2) in ('T4')) or ( SUBSTRING(orgunit, 6,2) in ('T5')) ) ";
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and shift = 'D' and activitycode = 0 and type = '28' ";
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " group by orgunit ";

                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " union select orgunit, max(transferred) trans from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0'  ";
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + "  and SUBSTRING([orgunit], 5,3) in ( 'D1T')  ";
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and shift = 'D' and activitycode = 0 and type = '28' ";
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " group by orgunit ";
                
               _dbMan2.SqlStatement = _dbMan2.SqlStatement + " order by orgunit ";
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
            #endregion

            //
            // Do 'Dev Cleaning Crew'
            //
            #region
            if (rdbDevCleanCrew.Checked == true)
            {
                //
                //  Load Incomplete List Box
                //
                lbIncomplete.Items.Clear();

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = _dbMan.SqlStatement + " select distinct([OrgUnit]) OrgUnit from dbo.tbl_Import_BMCS_Clocking_Total ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " where SUBSTRING([OrgUnit], 5,2) in ('T3','X3') and [orgunit] in (select distinct(orgunit) orgunit from tbl_BCS_Gangs where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " and SUBSTRING([OrgUnit],1,4) in ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " (select distinct(SUBSTRING(SectionID,1,4)) MO from mineware.dbo.tbl_BCS_Planning ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and Activity = 1)  ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " and orgunit not in (select Gang from tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ) ";

                _dbMan.SqlStatement = _dbMan.SqlStatement + "union ";
                
                _dbMan.SqlStatement = _dbMan.SqlStatement + " select distinct([OrgUnit]) OrgUnit from dbo.tbl_Import_BMCS_Clocking_Total ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " where SUBSTRING([OrgUnit], 5,4) like ('X__C') and [orgunit] in (select distinct(orgunit) orgunit from tbl_BCS_Gangs where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " and SUBSTRING([OrgUnit],1,4) in ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " (select distinct(SUBSTRING(SectionID,1,4)) MO from mineware.dbo.tbl_BCS_Planning ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and Activity = 1)  ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " and orgunit not in (select Gang from tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ) ";

                _dbMan.SqlStatement = _dbMan.SqlStatement + "union ";

                _dbMan.SqlStatement = _dbMan.SqlStatement + " select distinct([OrgUnit]) OrgUnit from dbo.tbl_Import_BMCS_Clocking_Total ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " where SUBSTRING([OrgUnit], 6,2) like ('T7') and [orgunit] in (select distinct(orgunit) orgunit from tbl_BCS_Gangs where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " and SUBSTRING([OrgUnit],1,4) in ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " (select distinct(SUBSTRING(SectionID,1,4)) MO from mineware.dbo.tbl_BCS_Planning ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and Activity = 1)  ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " and orgunit not in (select Gang from tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ) ";
           


                
                _dbMan.SqlStatement = _dbMan.SqlStatement + " order by [OrgUnit] ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Dev";
                _dbMan.ExecuteInstruction();

                DataTable dt = _dbMan.ResultsDataTable;

                lbIncomplete.Items.Add("");
                foreach (DataRow dr in dt.Rows)
                {
                    lbIncomplete.Items.Add(dr["OrgUnit"].ToString());
                }

                //
                //Load Printed Listbox
                //
                lbTransfer.Items.Clear();

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " select gang from Mineware.dbo.tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activity = 'D' ";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and gang not in (select distinct(orgunit) orgunit from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0') ";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and SUBSTRING(gang, 5,2) in ('T3','X3') ";

                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " union select gang from Mineware.dbo.tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activity = 'D' ";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and gang not in (select distinct(orgunit) orgunit from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0') ";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and SUBSTRING([gang], 5,4) like ('X__C') ";


                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " order by gang  ";
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
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " select orgunit, max(transferred) trans from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0' ";
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and SUBSTRING([OrgUnit], 5,2) in ('T3','X3')  ";
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and shift = 'D' and activitycode = 0 and type = '28' ";
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " group by orgunit ";


                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " union select orgunit, max(transferred) trans from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0' ";
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and SUBSTRING([OrgUnit], 5,4) like ('X__C')   ";
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and shift = 'D' and activitycode = 0 and type = '28' ";
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " group by orgunit ";
                
                 _dbMan2.SqlStatement = _dbMan2.SqlStatement + "order by orgunit ";
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
            #endregion


            //
            // Do 'Dev Construction Crew'
            //
            #region
            if (rdbDevConCrew.Checked == true)
            {
                //
                //  Load Incomplete List Box
                //
                lbIncomplete.Items.Clear();

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = _dbMan.SqlStatement + " select distinct([OrgUnit]) OrgUnit from dbo.tbl_Import_BMCS_Clocking_Total ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " where  (SUBSTRING([OrgUnit], 5,2) in ( 'T6', 'X6')  or SUBSTRING([OrgUnit], 6,2) in ( 'T6') or SUBSTRING([OrgUnit], 7,2) in ( 'T6')) ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " and SUBSTRING([OrgUnit],1,4) in ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " (select distinct(SUBSTRING(SectionID,1,4)) MO from mineware.dbo.tbl_BCS_Planning ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + "  where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and Activity = 1) ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " and orgunit not in (select Gang from tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ) ";
                _dbMan.SqlStatement = _dbMan.SqlStatement + " order by [OrgUnit] ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Dev";
                _dbMan.ExecuteInstruction();

                DataTable dt = _dbMan.ResultsDataTable;

                lbIncomplete.Items.Add("");
                foreach (DataRow dr in dt.Rows)
                {
                    lbIncomplete.Items.Add(dr["OrgUnit"].ToString());
                }

                //
                //Load Printed Listbox
                //
                lbTransfer.Items.Clear();

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " select gang from Mineware.dbo.tbl_BCS_New_Status where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activity = 'D' ";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and gang not in (select distinct(orgunit) orgunit from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0') ";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and (SUBSTRING([gang], 5,2) in ( 'T6', 'X6')  or SUBSTRING([gang], 6,2) in ( 'T6')   or SUBSTRING([gang], 7,2) in ( 'T6')) ";
                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " order by gang ";
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
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " select orgunit, max(transferred) trans from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and activitycode = '0'  ";
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and (SUBSTRING([OrgUnit], 5,2) in ( 'T6', 'X6')  or SUBSTRING([OrgUnit], 6,2) in ( 'T6') or SUBSTRING([OrgUnit], 7,2) in ( 'T6'))  ";
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " and shift = 'D' and activitycode = 0 and type = '28' ";
                _dbMan2.SqlStatement = _dbMan2.SqlStatement + " group by orgunit order by orgunit ";
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
            #endregion
        }

     
         string ShiftBoss = "";

         int TotalDSShifts = 0;
         int TotalNSShifts = 0;
         int TotalShift = 0;
        
         int TotalDSAwops = 0;
         int TotalNSAwops = 0;
         int TotalAwops = 0;

         decimal WaterEndFactor = 0;
         decimal WaterEndMeters = 0;

         double HoleBonus = 0;

         string Workplaceid = "";
         string description = "";

        int BonusShifts = 0;
        Decimal RDOBonusShifts = 0;
        Decimal RDOPosShifts = 0;
        int SqmPerMan = 0;
        Decimal MPerShift = 0;                
        int PossShifts = 0;

        int AvgEmp = 0;
         Decimal AvgRDOs = 0;

        int wastesqmexp = 0; 
        int totsqmexp = 0;
        int totswpexp = 0;

        decimal totmexp = 0; 
 
        Decimal bip = 0;
        Decimal TotalPayment = 0;


        decimal wwendfact = 0;

        decimal wwendfact1 = 0; 



        private void showBtn_Click(object sender, EventArgs e)
        {
           
            
        }

        private void lbTransfer_SelectedIndexChanged(object sender, EventArgs e)
        {
           
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

            DsLbl.Text = lbPrinted.SelectedItem.ToString();



            if (rdbStoping.Checked == true)
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " select * from mineware.dbo.[vw_bcs_survey] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and  activitycode = 0 and orgunit = '" + DsLbl.Text + "' ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Stoping";
                _dbMan.ExecuteInstruction();

                if (_dbMan.ResultsDataTable.Rows.Count > 0)
                {

                    Sectionlbl.Text = _dbMan.ResultsDataTable.Rows[0]["Sectionid"].ToString();
                }
                else
                {
                    MessageBox.Show("Orgunit not found in survey database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (DsLbl.Text.Substring(6, 1) == "M")
                {
                    NSLbl.Text = DsLbl.Text.Substring(0, 6) + "N" + DsLbl.Text.Substring(7, 1);
                }

                if (DsLbl.Text.Substring(6, 1) == "U")
                {
                    NSLbl.Text = DsLbl.Text.Substring(0, 6) + "P" + DsLbl.Text.Substring(7, 1);
                }

                if (DsLbl.Text.Length == 9)
                {
                    if (DsLbl.Text.Substring(7, 1) == "M")
                    {
                        NSLbl.Text = DsLbl.Text.Substring(0, 7) + "N" + DsLbl.Text.Substring(8, 1);
                    }

                    if (DsLbl.Text.Substring(7, 1) == "U")
                    {
                        NSLbl.Text = DsLbl.Text.Substring(0, 7) + "P" + DsLbl.Text.Substring(8, 1);
                    }
                }
            }

            if (rdbRCrew.Checked == true)
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " select * from mineware.dbo.[vw_bcs_survey] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and  activitycode = 0 and orgunit = '" + DsLbl.Text + "' ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Stoping";
                _dbMan.ExecuteInstruction();

                Sectionlbl.Text = _dbMan.ResultsDataTable.Rows[0]["Sectionid"].ToString();


            }

            if (rdbZCrew.Checked == true)
            {
                Sectionlbl.Text = lblOrgunit.Text;

                if (DsLbl.Text.Substring(6, 1) == "M")
                {
                    NSLbl.Text = DsLbl.Text.Substring(0, 6) + "N" + DsLbl.Text.Substring(7, 1);
                }

                if (DsLbl.Text.Substring(6, 1) == "U")
                {
                    NSLbl.Text = DsLbl.Text.Substring(0, 6) + "P" + DsLbl.Text.Substring(7, 1);
                }

                if (DsLbl.Text.Length == 9)
                {
                    if (DsLbl.Text.Substring(7, 1) == "M")
                    {
                        NSLbl.Text = DsLbl.Text.Substring(0, 7) + "N" + DsLbl.Text.Substring(8, 1);
                    }

                    if (DsLbl.Text.Substring(7, 1) == "U")
                    {
                        NSLbl.Text = DsLbl.Text.Substring(0, 7) + "P" + DsLbl.Text.Substring(8, 1);
                    }
                }
            }

            if (rdbConstCrew.Checked == true)
            {
                Sectionlbl.Text = DsLbl.Text.Substring(0, 5);
                NSLbl.Text = "NSLabel";
            }

            if (rdbXCCrew.Checked == true)
            {
                Sectionlbl.Text = DsLbl.Text.Substring(0, 4);
                NSLbl.Text = "NSLabel";
            }

            if (rdbDev.Checked == true)
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " select * from mineware.dbo.[vw_bcs_survey] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and  activitycode = 1 and orgunit = '" + DsLbl.Text + "' ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Stoping";
                _dbMan.ExecuteInstruction();

                if (_dbMan.ResultsDataTable.Rows.Count > 0)
                {

                    Sectionlbl.Text = _dbMan.ResultsDataTable.Rows[0]["Sectionid"].ToString();
                }
                else
                {
                    MessageBox.Show("Orgunit not found in survey database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (DsLbl.Text.Substring(6, 1) == "R")
                {
                    NSLbl.Text = DsLbl.Text.Substring(0, 6) + "T" + DsLbl.Text.Substring(7, 1);
                }

                if (DsLbl.Text.Substring(6, 1) == "F")
                {
                    NSLbl.Text = DsLbl.Text.Substring(0, 6) + "G" + DsLbl.Text.Substring(7, 1);
                }

                if (DsLbl.Text == "0113A1RJ1")
                {
                    NSLbl.Text = "0113A1TJ1";// DsLbl.Text.Substring(0, 6) + "G" + DsLbl.Text.Substring(7, 1);
                }

                if (DsLbl.Text == "0743E1RB1")
                {
                    NSLbl.Text = "0743E1TB1";// DsLbl.Text.Substring(0, 6) + "G" + DsLbl.Text.Substring(7, 1);
                }
            }

            if (rdbDevRailCrew.Checked == true)
            {
                Sectionlbl.Text = DsLbl.Text.Substring(0, 4);
                NSLbl.Text = "NSLabel";
            }

            if (rdbDevTransCrew.Checked == true)
            {
                Sectionlbl.Text = DsLbl.Text.Substring(0, 4);
                NSLbl.Text = "NSLabel";
            }

            if (rdbDevCleanCrew.Checked == true)
            {
                Sectionlbl.Text = DsLbl.Text.Substring(0, 4);
                NSLbl.Text = "NSLabel";
            }

            if (rdbDevConCrew.Checked == true)
            {
                Sectionlbl.Text = DsLbl.Text.Substring(0, 5);
                NSLbl.Text = "NSLabel";
            }

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
            lblOrgunit.Text = lbIncomplete.SelectedItem.ToString();
            DsLbl.Text = lbIncomplete.SelectedItem.ToString();

            

            if (rdbStoping.Checked == true)
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " select * from mineware.dbo.[vw_bcs_survey] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and  activitycode = 0 and orgunit = '" + DsLbl.Text + "' ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Stoping";
                _dbMan.ExecuteInstruction();

                if (_dbMan.ResultsDataTable.Rows.Count > 0)
                {

                    Sectionlbl.Text = _dbMan.ResultsDataTable.Rows[0]["Sectionid"].ToString();
                }
                else
                {
                    MessageBox.Show("Orgunit not found in survey database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (DsLbl.Text.Substring(6, 1) == "M")
                {
                    NSLbl.Text = DsLbl.Text.Substring(0, 6) + "N" + DsLbl.Text.Substring(7, 1);
                }

                if (DsLbl.Text.Substring(6, 1) == "U")
                {
                    NSLbl.Text = DsLbl.Text.Substring(0, 6) + "P" + DsLbl.Text.Substring(7, 1);
                }

                if (DsLbl.Text.Length == 9)
                {
                    if (DsLbl.Text.Substring(7, 1) == "M")
                    {
                        NSLbl.Text = DsLbl.Text.Substring(0, 7) + "N" + DsLbl.Text.Substring(8, 1);
                    }

                    if (DsLbl.Text.Substring(7, 1) == "U")
                    {
                        NSLbl.Text = DsLbl.Text.Substring(0, 7) + "P" + DsLbl.Text.Substring(8, 1);
                    }
                }
            }

            if (rdbRCrew.Checked == true)
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " select * from mineware.dbo.[vw_bcs_survey] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and  activitycode = 0 and orgunit = '" + DsLbl.Text + "' ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Stoping";
                _dbMan.ExecuteInstruction();

                Sectionlbl.Text = _dbMan.ResultsDataTable.Rows[0]["Sectionid"].ToString();

              
            }

            if (rdbZCrew.Checked == true)
            {
                Sectionlbl.Text = lblOrgunit.Text;

                if (DsLbl.Text.Substring(6, 1) == "M")
                {
                    NSLbl.Text = DsLbl.Text.Substring(0, 6) + "N" + DsLbl.Text.Substring(7, 1);
                }

                if (DsLbl.Text.Substring(6, 1) == "U")
                {
                    NSLbl.Text = DsLbl.Text.Substring(0, 6) + "P" + DsLbl.Text.Substring(7, 1);
                }

                if (DsLbl.Text.Length == 9)
                {
                    if (DsLbl.Text.Substring(7, 1) == "M")
                    {
                        NSLbl.Text = DsLbl.Text.Substring(0, 7) + "N" + DsLbl.Text.Substring(8, 1);
                    }

                    if (DsLbl.Text.Substring(7, 1) == "U")
                    {
                        NSLbl.Text = DsLbl.Text.Substring(0, 7) + "P" + DsLbl.Text.Substring(8, 1);
                    }
                }
            }

            if (rdbConstCrew.Checked == true)
            {
                Sectionlbl.Text = DsLbl.Text.Substring(0, 5);
                NSLbl.Text = "NSLabel";
            }

            if (rdbXCCrew.Checked == true)
            {
                Sectionlbl.Text = DsLbl.Text.Substring(0, 4);
                NSLbl.Text = "NSLabel";
            }


            if (rdbDev.Checked == true)
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " select * from mineware.dbo.[vw_bcs_survey] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and  activitycode = 1 and orgunit = '" + DsLbl.Text + "' ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Stoping";
                _dbMan.ExecuteInstruction();

                if (_dbMan.ResultsDataTable.Rows.Count > 0)
                {

                    Sectionlbl.Text = _dbMan.ResultsDataTable.Rows[0]["Sectionid"].ToString();
                }
                else
                {
                    MessageBox.Show("Orgunit not found in survey database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (DsLbl.Text.Substring(6, 1) == "R")
                {
                    NSLbl.Text = DsLbl.Text.Substring(0, 6) + "T" + ((DsLbl.Text + "           ").ToString().Substring(7, 2));
                }

                if (DsLbl.Text.Substring(6, 1) == "F")
                {
                    NSLbl.Text = DsLbl.Text.Substring(0, 6) + "G" + ((DsLbl.Text + "           ").ToString().Substring(7, 2));
                }

                if (DsLbl.Text == "0113A1RJ1")
                {
                    NSLbl.Text = "0113A1TJ1";// DsLbl.Text.Substring(0, 6) + "G" + DsLbl.Text.Substring(7, 1);
                }

                if (DsLbl.Text == "0743E1RB1")
                {
                    NSLbl.Text = "0743E1TB1";// DsLbl.Text.Substring(0, 6) + "G" + DsLbl.Text.Substring(7, 1);
                }
            }

            if (rdbDevRailCrew.Checked == true)
            {
                Sectionlbl.Text = DsLbl.Text.Substring(0, 4);
                NSLbl.Text = "NSLabel";
            }

            if (rdbDevTransCrew.Checked == true)
            {
                Sectionlbl.Text = DsLbl.Text.Substring(0, 4);
                NSLbl.Text = "NSLabel";
            }

            if (rdbDevCleanCrew.Checked == true)
            {
                Sectionlbl.Text = DsLbl.Text.Substring(0, 4);
                NSLbl.Text = "NSLabel";
            }

            if (rdbDevConCrew.Checked == true)
            {
                Sectionlbl.Text = DsLbl.Text.Substring(0, 5);
                NSLbl.Text = "NSLabel";
            }

        }

        private void lbTransfer_Click(object sender, EventArgs e)
        {
            //clear other list boxes and set lblOrgUnit
            if (lbIncomplete.Items.Count > 0)
            {
                lbIncomplete.SetSelected(0, false);
            }
            if (lbPrinted.Items.Count > 0)
            {
                lbPrinted.SetSelected(0, false);
            }
            lblOrgunit.Text = lbTransfer.SelectedItem.ToString();


            DsLbl.Text = lbTransfer.SelectedItem.ToString();

            AdjLbl.Text = "N";

            MWDataManager.clsDataAccess _dbManAdj = new MWDataManager.clsDataAccess();
            _dbManAdj.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManAdj.SqlStatement = _dbManAdj.SqlStatement + " select case when TodayDate > MeasDate then 'Y' else 'N' end as LabelCaption from (  ";
            _dbManAdj.SqlStatement = _dbManAdj.SqlStatement + " select MAX(CalendarDate)+7 MeasDate, DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))  TodayDate from mineware.dbo.tbl_BCS_Planning   ";
            _dbManAdj.SqlStatement = _dbManAdj.SqlStatement + " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  ";
            _dbManAdj.SqlStatement = _dbManAdj.SqlStatement + " and SUBSTRING(SectionID,1,4) = '" + DsLbl.Text.Substring(0, 4) + "' )a  ";
            
            _dbManAdj.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManAdj.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManAdj.ResultsTableName = "Stoping";
            _dbManAdj.ExecuteInstruction();

            DataTable dt = _dbManAdj.ResultsDataTable;

            foreach (DataRow dr in dt.Rows)
            {
                AdjLbl.Text = dr["LabelCaption"].ToString();
            }


            if (rdbStoping.Checked == true)
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " select * from mineware.dbo.[vw_bcs_survey] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and  activitycode = 0 and orgunit = '" + DsLbl.Text + "' ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Stoping";
                _dbMan.ExecuteInstruction();

                if (_dbMan.ResultsDataTable.Rows.Count > 0)
                {

                    Sectionlbl.Text = _dbMan.ResultsDataTable.Rows[0]["Sectionid"].ToString();
                }
                else
                {
                    MessageBox.Show("No Planning found","No Planning",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }

                if (DsLbl.Text.Substring(6, 1) == "M")
                {
                    NSLbl.Text = DsLbl.Text.Substring(0, 6) + "N" + DsLbl.Text.Substring(7, 1);
                }

                if (DsLbl.Text.Substring(6, 1) == "U")
                {
                    NSLbl.Text = DsLbl.Text.Substring(0, 6) + "P" + DsLbl.Text.Substring(7, 1);
                }

                if (DsLbl.Text.Length == 9)
                {
                    if (DsLbl.Text.Substring(7, 1) == "M")
                    {
                        NSLbl.Text = DsLbl.Text.Substring(0, 7) + "N" + DsLbl.Text.Substring(8, 1);
                    }

                    if (DsLbl.Text.Substring(7, 1) == "U")
                    {
                        NSLbl.Text = DsLbl.Text.Substring(0, 7) + "P" + DsLbl.Text.Substring(8, 1);
                    }
                }
            }

            if (rdbRCrew.Checked == true)
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " select * from mineware.dbo.[vw_bcs_survey] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and  activitycode = 0 and orgunit = '" + DsLbl.Text + "' ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Stoping";
                _dbMan.ExecuteInstruction();

                 if (_dbMan.ResultsDataTable.Rows.Count > 0)
                {

                    Sectionlbl.Text = _dbMan.ResultsDataTable.Rows[0]["Sectionid"].ToString();
                }
                else
                {
                    MessageBox.Show("No Planning found", "No Planning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


            }

            //if (rdbZCrew.Checked == true)
            //{
            //    Sectionlbl.Text = lblOrgunit.Text;

            //    if (DsLbl.Text.Substring(6, 1) == "M")
            //    {
            //        NSLbl.Text = DsLbl.Text.Substring(0, 6) + "N" + DsLbl.Text.Substring(7, 1);
            //    }

            //    if (DsLbl.Text.Substring(6, 1) == "U")
            //    {
            //        NSLbl.Text = DsLbl.Text.Substring(0, 6) + "P" + DsLbl.Text.Substring(7, 1);
            //    }
            //}

            //if (rdbConstCrew.Checked == true)
            //{
            //    Sectionlbl.Text = DsLbl.Text.Substring(1, 4);
            //    NSLbl.Text = "NSLabel";
            //}

            //if (rdbXCCrew.Checked == true)
            //{
            //    Sectionlbl.Text = DsLbl.Text.Substring(1, 4);
            //    NSLbl.Text = "NSLabel";
            //}



            if (rdbZCrew.Checked == true)
            {
                Sectionlbl.Text = lblOrgunit.Text;

                if (DsLbl.Text.Substring(6, 1) == "M")
                {
                    NSLbl.Text = DsLbl.Text.Substring(0, 6) + "N" + DsLbl.Text.Substring(7, 1);
                }

                if (DsLbl.Text.Substring(6, 1) == "U")
                {
                    NSLbl.Text = DsLbl.Text.Substring(0, 6) + "P" + DsLbl.Text.Substring(7, 1);
                }

                if (DsLbl.Text.Length == 9)
                {
                    if (DsLbl.Text.Substring(7, 1) == "M")
                    {
                        NSLbl.Text = DsLbl.Text.Substring(0, 7) + "N" + DsLbl.Text.Substring(8, 1);
                    }

                    if (DsLbl.Text.Substring(7, 1) == "U")
                    {
                        NSLbl.Text = DsLbl.Text.Substring(0, 7) + "P" + DsLbl.Text.Substring(8, 1);
                    }
                }
            }

            if (rdbConstCrew.Checked == true)
            {
                Sectionlbl.Text = DsLbl.Text.Substring(0, 5);
                NSLbl.Text = "NSLabel";
            }

            if (rdbXCCrew.Checked == true)
            {
                Sectionlbl.Text = DsLbl.Text.Substring(0, 4);
                NSLbl.Text = "NSLabel";
            }

            if (rdbDev.Checked == true)
            {
                NSLbl.Text = "NSLabel";

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " select * from mineware.dbo.[vw_bcs_survey] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and  activitycode = 1 and orgunit = '" + DsLbl.Text + "' ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Stoping";
                _dbMan.ExecuteInstruction();

                if (_dbMan.ResultsDataTable.Rows.Count > 0)
                {

                    Sectionlbl.Text = _dbMan.ResultsDataTable.Rows[0]["Sectionid"].ToString();
                }
                else
                {
                    MessageBox.Show("Orgunit not found in survey database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (DsLbl.Text.Substring(6, 1) == "R")
                {
                    if (DsLbl.Text.Length > 8)
                    {
                        NSLbl.Text = DsLbl.Text.Substring(0, 6) + "T" + DsLbl.Text.Substring(7, 2);
                    }
                    else
                    {
                        NSLbl.Text = DsLbl.Text.Substring(0, 6) + "T" + DsLbl.Text.Substring(7, 1);
                    }
                }

                if (DsLbl.Text.Substring(6, 1) == "F")
                {
                    NSLbl.Text = DsLbl.Text.Substring(0, 6) + "G" + DsLbl.Text.Substring(7, 1);
                }

                if (DsLbl.Text == "0113A1RJ1")
                {
                    NSLbl.Text = "0113A1TJ1";// DsLbl.Text.Substring(0, 6) + "G" + DsLbl.Text.Substring(7, 1);
                }

                if (DsLbl.Text == "0743E1RB1")
                {
                    NSLbl.Text = "0743E1TB1";// DsLbl.Text.Substring(0, 6) + "G" + DsLbl.Text.Substring(7, 1);
                }
            }

            if (rdbDevRailCrew.Checked == true)
            {
                Sectionlbl.Text = DsLbl.Text.Substring(0, 4);
                NSLbl.Text = "NSLabel";
            }

            if (rdbDevTransCrew.Checked == true)
            {
                Sectionlbl.Text = DsLbl.Text.Substring(0, 4);
                NSLbl.Text = "NSLabel";
            }

            if (rdbDevCleanCrew.Checked == true)
            {
                Sectionlbl.Text = DsLbl.Text.Substring(0, 4);
                NSLbl.Text = "NSLabel";
            }

            if (rdbDevConCrew.Checked == true)
            {
                Sectionlbl.Text = DsLbl.Text.Substring(0, 5);
                NSLbl.Text = "NSLabel";
            }

        }

        private void Close1Btn_Click_1(object sender, EventArgs e)
        {
            
        }

        private void rdbZCrew_CheckedChanged(object sender, EventArgs e)
        {
            LoadListBoxes();
        }

        private void rdbConstCrew_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbConstCrew.Checked == true)
            {
                LoadListBoxes();
            }
        }

        private void rdbXCCrew_CheckedChanged(object sender, EventArgs e)
        {
            LoadListBoxes();
        }

        private void rdbDev_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbDev.Checked == true)
            {
                LoadListBoxes();
            }
        }

        private void rdbDevRailCrew_Click(object sender, EventArgs e)
        {
            if (rdbDevRailCrew.Checked == true)
            {
                LoadListBoxes();
            }
        }

      

        private void rdbDevTransCrew_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbDevTransCrew.Checked == true)
            {
                LoadListBoxes();
            }

        }

        private void rdbDevCleanCrew_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbDevCleanCrew.Checked == true)
            {
                LoadListBoxes();
            }
        }

        private void redDevConCrew_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbDevConCrew.Checked == true)
            {
                LoadListBoxes();
            }
        }


        DialogResult result;

        int RI = 0;
        int LTI = 0;
        int Fatal = 0;

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void rdbStoping_CheckedChanged(object sender, EventArgs e)
        {
            LoadListBoxes();
        }

        private void lbIncomplete_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void editActivity_EditValueChanged(object sender, EventArgs e)
        {
            if (editActivity.EditValue.ToString() == "0")
            {
                rdbStoping.Checked = true;
                rdbRCrew.Checked = false;
                rdbZCrew.Checked = false;
                rdbConstCrew.Checked = false;
                rdbXCCrew.Checked = false;
                rdbDev.Checked = false;
                rdbDevRailCrew.Checked = false;
                rdbDevTransCrew.Checked = false;
                rdbDevCleanCrew.Checked = false;
                rdbDevConCrew.Checked = false;
            }
            if (editActivity.EditValue.ToString() == "1")
            {
                rdbStoping.Checked = false;
                rdbRCrew.Checked = true;
                rdbZCrew.Checked = false;
                rdbConstCrew.Checked = false;
                rdbXCCrew.Checked = false;
                rdbDev.Checked = false;
                rdbDevRailCrew.Checked = false;
                rdbDevTransCrew.Checked = false;
                rdbDevCleanCrew.Checked = false;
                rdbDevConCrew.Checked = false;
            }
            if (editActivity.EditValue.ToString() == "2")
            {
                rdbStoping.Checked = false;
                rdbRCrew.Checked = false;
                rdbZCrew.Checked = true;
                rdbConstCrew.Checked = false;
                rdbXCCrew.Checked = false;
                rdbDev.Checked = false;
                rdbDevRailCrew.Checked = false;
                rdbDevTransCrew.Checked = false;
                rdbDevCleanCrew.Checked = false;
                rdbDevConCrew.Checked = false;
            }
            if (editActivity.EditValue.ToString() == "3")
            {
                rdbStoping.Checked = false;
                rdbRCrew.Checked = false;
                rdbZCrew.Checked = false;
                rdbConstCrew.Checked = true;
                rdbXCCrew.Checked = false;
                rdbDev.Checked = false;
                rdbDevRailCrew.Checked = false;
                rdbDevTransCrew.Checked = false;
                rdbDevCleanCrew.Checked = false;
                rdbDevConCrew.Checked = false;
            }
            if (editActivity.EditValue.ToString() == "4")
            {
                rdbStoping.Checked = false;
                rdbRCrew.Checked = false;
                rdbZCrew.Checked = false;
                rdbConstCrew.Checked = false;
                rdbXCCrew.Checked = true;
                rdbDev.Checked = false;
                rdbDevRailCrew.Checked = false;
                rdbDevTransCrew.Checked = false;
                rdbDevCleanCrew.Checked = false;
                rdbDevConCrew.Checked = false;
            }
            if (editActivity.EditValue.ToString() == "5")
            {
                rdbStoping.Checked = false;
                rdbRCrew.Checked = false;
                rdbZCrew.Checked = false;
                rdbConstCrew.Checked = false;
                rdbXCCrew.Checked = false;
                rdbDev.Checked = true;
                rdbDevRailCrew.Checked = false;
                rdbDevTransCrew.Checked = false;
                rdbDevCleanCrew.Checked = false;
                rdbDevConCrew.Checked = false;
            }
            if (editActivity.EditValue.ToString() == "6")
            {
                rdbStoping.Checked = false;
                rdbRCrew.Checked = false;
                rdbZCrew.Checked = false;
                rdbConstCrew.Checked = false;
                rdbXCCrew.Checked = false;
                rdbDev.Checked = false;
                rdbDevRailCrew.Checked = true;
                rdbDevTransCrew.Checked = false;
                rdbDevCleanCrew.Checked = false;
                rdbDevConCrew.Checked = false;
            }
            if (editActivity.EditValue.ToString() == "7")
            {
                rdbStoping.Checked = false;
                rdbRCrew.Checked = false;
                rdbZCrew.Checked = false;
                rdbConstCrew.Checked = false;
                rdbXCCrew.Checked = false;
                rdbDev.Checked = false;
                rdbDevRailCrew.Checked = false;
                rdbDevTransCrew.Checked = true;
                rdbDevCleanCrew.Checked = false;
                rdbDevConCrew.Checked = false;
            }
            if (editActivity.EditValue.ToString() == "8")
            {
                rdbStoping.Checked = false;
                rdbRCrew.Checked = false;
                rdbZCrew.Checked = false;
                rdbConstCrew.Checked = false;
                rdbXCCrew.Checked = false;
                rdbDev.Checked = false;
                rdbDevRailCrew.Checked = false;
                rdbDevTransCrew.Checked = false;
                rdbDevCleanCrew.Checked = true;
                rdbDevConCrew.Checked = false;
            }
            if (editActivity.EditValue.ToString() == "9")
            {
                rdbStoping.Checked = false;
                rdbRCrew.Checked = false;
                rdbZCrew.Checked = false;
                rdbConstCrew.Checked = false;
                rdbXCCrew.Checked = false;
                rdbDev.Checked = false;
                rdbDevRailCrew.Checked = false;
                rdbDevTransCrew.Checked = false;
                rdbDevCleanCrew.Checked = false;
                rdbDevConCrew.Checked = true;
            }
        }

        private void btnShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int row = 0;





            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();

            string DevType = "";

            if ("" == "")
            {

                if (rdbDev.Checked == true)
                {
                    if (DsLbl.Text.Substring(6, 1) == "F")
                    {
                        DevType = "FWD";
                    }

                    if (DsLbl.Text.Substring(6, 1) == "R")
                    {
                        DevType = "RSE";
                    }

                    if (DsLbl.Text.Substring(6, 1) == "B")
                    {
                        DevType = "BH";

                        NSLbl.Text = "";
                    }
                }


                // do grid
                DataGrid.Rows.Clear();
                //DataGrid.RowCount = 0;
                DataGrid.ColumnCount = 50;
                DataGrid.RowCount = 200;

                DataGrid.Columns[0].HeaderText = "ID";
                DataGrid.Columns[0].Width = 5;

                DataGrid.Columns[1].HeaderText = "TimeStamp";
                DataGrid.Columns[1].Width = 120;

                DataGrid.Columns[2].HeaderText = "Prodmonth";
                DataGrid.Columns[2].Width = 50;

                DataGrid.Columns[3].HeaderText = "Activity";
                DataGrid.Columns[3].Width = 10;

                DataGrid.Columns[4].HeaderText = "IndustryNumber";
                DataGrid.Columns[4].Width = 80;

                DataGrid.Columns[5].HeaderText = "Init";
                DataGrid.Columns[5].Width = 80;

                DataGrid.Columns[6].HeaderText = "Surname";
                DataGrid.Columns[6].Width = 80;

                DataGrid.Columns[7].HeaderText = "Category";
                DataGrid.Columns[7].Width = 80;

                DataGrid.Columns[8].HeaderText = "Org Unit";
                DataGrid.Columns[8].Width = 80;

                DataGrid.Columns[9].HeaderText = "Shift";
                DataGrid.Columns[9].Width = 80;

                DataGrid.Columns[10].HeaderText = "Workplace";
                DataGrid.Columns[10].Width = 80;

                DataGrid.Columns[11].HeaderText = "Pas No";
                DataGrid.Columns[11].Width = 80;

                DataGrid.Columns[12].HeaderText = "Gross Amount";
                DataGrid.Columns[12].Width = 80;

                DataGrid.Columns[13].HeaderText = "Working Shifts";
                DataGrid.Columns[13].Width = 80;

                DataGrid.Columns[14].HeaderText = "AWOPS";
                DataGrid.Columns[14].Width = 80;

                DataGrid.Columns[15].HeaderText = "Element";
                DataGrid.Columns[15].Width = 80;

                DataGrid.Columns[16].HeaderText = "Non PU LTI";
                DataGrid.Columns[16].Width = 80;

                DataGrid.Columns[17].HeaderText = "RI";
                DataGrid.Columns[17].Width = 80;

                DataGrid.Columns[18].HeaderText = "LTI";
                DataGrid.Columns[18].Width = 80;

                DataGrid.Columns[19].HeaderText = "Phys Cond";
                DataGrid.Columns[19].Width = 80;

                DataGrid.Columns[20].HeaderText = "Nett Amount";
                DataGrid.Columns[20].Width = 80;

                DataGrid.Columns[21].HeaderText = "Type";
                DataGrid.Columns[21].Width = 80;

                DataGrid.Columns[22].HeaderText = "Tranfered";
                DataGrid.Columns[22].Width = 80;

                DataGrid.Columns[23].HeaderText = "Tranfered Time Stamp";
                DataGrid.Columns[23].Width = 80;

                DataGrid.Columns[24].HeaderText = "Holes";
                DataGrid.Columns[24].Width = 80;

                DataGrid.Columns[25].HeaderText = "Fin Day";
                DataGrid.Columns[25].Width = 80;

                DataGrid.Columns[26].HeaderText = "Tot Shift";
                DataGrid.Columns[26].Width = 80;

                DataGrid.Columns[27].HeaderText = "Sick";
                DataGrid.Columns[27].Width = 80;

                DataGrid.Columns[28].HeaderText = "Mach Opp";
                DataGrid.Columns[28].Width = 80;

                DataGrid.Columns[29].HeaderText = "Mach Inc";
                DataGrid.Columns[29].Width = 80;

                DataGrid.Columns[30].HeaderText = "None Mach less safety";
                DataGrid.Columns[30].Width = 80;

                DataGrid.Columns[31].HeaderText = "AWOPPEN";
                DataGrid.Columns[31].Width = 80;

                DataGrid.Columns[32].HeaderText = "Sick Pen";
                DataGrid.Columns[32].Width = 80;


                DataGrid.Columns[33].HeaderText = "Tot Dec";
                DataGrid.Columns[33].Width = 80;

                DataGrid.Columns[34].HeaderText = "Order";
                DataGrid.Columns[34].Width = 80;

                DataGrid.Columns[35].HeaderText = "Pot";
                DataGrid.Columns[35].Width = 80;

                DataGrid.Columns[36].HeaderText = "Extra Payment";
                DataGrid.Columns[36].Width = 80;

                if (NSLbl.Text == "")
                    NSLbl.Text = "NA";


                //MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                if (rdbStoping.Checked == true)
                {
                    if (checkBox1.EditValue.ToString() == "Y")
                    {
                        _dbMan1.SqlStatement = " " +
                                              " exec mineware.dbo.sp_BMCS_GetClockingsProd_NewStopingOrig '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + DsLbl.Text + "', '" + NSLbl.Text + "'" +
                                              "  ";
                    }
                    else
                    {
                        _dbMan1.SqlStatement = " " +
                                               " exec mineware.dbo.sp_BMCS_GetClockingsProd_NewStoping '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + DsLbl.Text + "', '" + NSLbl.Text + "'" +
                                               "  ";
                    }
                }

                if (rdbRCrew.Checked == true)
                {
                    if (checkBox1.EditValue.ToString() == "Y")
                    {
                        _dbMan1.SqlStatement = " " +
                                               " exec mineware.dbo.sp_BMCS_GetClockingsProd_NewStopingOrig '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + DsLbl.Text + "', '" + NSLbl.Text + "'" +
                                               "  ";
                    }
                    else
                    {
                        _dbMan1.SqlStatement = " " +
                                               " exec mineware.dbo.sp_BMCS_GetClockingsProd_NewStoping '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + DsLbl.Text + "', '" + NSLbl.Text + "'" +
                                               "  ";
                    }
                }


                if (rdbZCrew.Checked == true)
                {
                    if (checkBox1.EditValue.ToString() == "Y")
                    {
                        _dbMan1.SqlStatement = " " +
                                               " exec mineware.dbo.sp_BMCS_GetClockingsProd_NewStopingOrig '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + DsLbl.Text + "', '" + NSLbl.Text + "'" +
                                               "  ";
                    }
                    else
                    {

                        string newns = DsLbl.Text.Substring(0, 6);

                        string newnsaa = DsLbl.Text.Substring(6, 1);

                        if (DsLbl.Text.Substring(6, 1) == "U")
                            newns = newns + "P";
                        else
                            newns = newns + "N";

                        newns = newns + (DsLbl.Text + "                         ").Substring(7, 20);
                        newns = newns.Trim();

                        NSLbl.Text = newns;

                        _dbMan1.SqlStatement = " " +
                                               " exec mineware.dbo.sp_BMCS_GetClockingsProd_NewStoping '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + DsLbl.Text + "', '" + newns + "'" +
                                               "  ";
                    }
                }

                if (rdbConstCrew.Checked == true)
                {
                    if (checkBox1.EditValue.ToString() == "Y")
                    {
                        _dbMan1.SqlStatement = " " +
                                               " exec mineware.dbo.sp_BMCS_GetClockingsProd_NewStopingConStructionOrig '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + DsLbl.Text + "', '" + NSLbl.Text + "'" +
                                               "  ";
                    }
                    else
                    {
                        _dbMan1.SqlStatement = " " +
                                                                       " exec mineware.dbo.sp_BMCS_GetClockingsProd_NewStopingConStruction '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + DsLbl.Text + "', '" + NSLbl.Text + "'" +
                                                                       "  ";

                    }
                }

                if (rdbXCCrew.Checked == true)
                {
                    if (checkBox1.EditValue.ToString() == "Y")
                    {
                        _dbMan1.SqlStatement = " " +
                                               " exec mineware.dbo.sp_BMCS_GetClockingsProd_NewOrig '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + DsLbl.Text + "', '" + NSLbl.Text + "'" +
                                               "  ";
                    }
                    else
                    {
                        _dbMan1.SqlStatement = " " +
                                                                      // " exec mineware.dbo.sp_BMCS_GetClockingsProd_New '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + DsLbl.Text + "', '" + NSLbl.Text + "'" +
                                                                      " exec mineware.dbo.sp_BMCS_GetClockingsProd_NewXCMaint '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + DsLbl.Text + "', '" + NSLbl.Text + "' ";

                    }
                }

                if (rdbDev.Checked == true)
                {
                    if (checkBox1.EditValue.ToString() == "Y")
                    {
                        _dbMan1.SqlStatement = " " +
                                               " exec mineware.dbo.sp_BMCS_GetClockingsProd_NewOrig '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + DsLbl.Text + "', '" + NSLbl.Text + "'" +
                                               " ";
                    }
                    else
                    {
                        _dbMan1.SqlStatement = " " +
                                              " exec mineware.dbo.sp_BMCS_GetClockingsProd_New '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + DsLbl.Text + "', '" + NSLbl.Text + "'" +
                                              "  ";

                    }
                }

                if (rdbDevRailCrew.Checked == true)
                {
                    if (checkBox1.EditValue.ToString() == "Y")
                    {
                        _dbMan1.SqlStatement = " " +
                                               " exec mineware.dbo.[sp_BMCS_GetClockingsProd_NewDevOtherOrig] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + DsLbl.Text + "', '" + NSLbl.Text + "'" +
                                               "  ";
                    }
                    else
                    {
                        _dbMan1.SqlStatement = " " +
                                               " exec mineware.dbo.[sp_BMCS_GetClockingsProd_NewDevOther] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + DsLbl.Text + "', '" + NSLbl.Text + "'" +
                                               "  ";

                    }
                }

                if (rdbDevTransCrew.Checked == true)
                {
                    if (checkBox1.EditValue.ToString() == "Y")
                    {
                        _dbMan1.SqlStatement = " " +
                                               " exec mineware.dbo.[sp_BMCS_GetClockingsProd_NewDevOtherOrig] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + DsLbl.Text + "', '" + NSLbl.Text + "'" +
                                               "  ";
                    }
                    else
                    {
                        _dbMan1.SqlStatement = " " +
                                               " exec mineware.dbo.[sp_BMCS_GetClockingsProd_NewDevOther] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + DsLbl.Text + "', '" + NSLbl.Text + "'" +
                                               "  ";
                    }
                }

                if (rdbDevCleanCrew.Checked == true)
                {
                    if (checkBox1.EditValue.ToString() == "Y")
                    {
                        _dbMan1.SqlStatement = " " +
                                               " exec mineware.dbo.[sp_BMCS_GetClockingsProd_NewDevOtherOrig] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + DsLbl.Text + "', '" + NSLbl.Text + "'" +
                                               "  ";
                    }
                    else
                    {
                        _dbMan1.SqlStatement = " " +
                                               " exec mineware.dbo.[sp_BMCS_GetClockingsProd_NewDevOther] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + DsLbl.Text + "', '" + NSLbl.Text + "'" +
                                               "  ";
                    }
                    //_dbMan1.SqlStatement = " " +
                    //                       " exec mineware.dbo.[sp_BMCS_GetClockingsProd_NewDevOther] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + DsLbl.Text + "', '" + NSLbl.Text + "'" +
                    //                       "  ";
                }

                if (rdbDevConCrew.Checked == true)
                {
                    if (checkBox1.EditValue.ToString() == "Y")
                    {
                        _dbMan1.SqlStatement = " " +
                                               " exec mineware.dbo.[sp_BMCS_GetClockingsProd_NewDevOtherOrig] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + DsLbl.Text + "', '" + NSLbl.Text + "'" +
                                               "  ";
                    }
                    else
                    {
                        _dbMan1.SqlStatement = " " +
                                               " exec mineware.dbo.[sp_BMCS_GetClockingsProd_NewDevOther] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + DsLbl.Text + "', '" + NSLbl.Text + "'" +
                                               "  ";
                    }

                    //_dbMan1.SqlStatement = " " +
                    //                       " exec mineware.dbo.[sp_BMCS_GetClockingsProd_NewDevOther] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + DsLbl.Text + "', '" + NSLbl.Text + "'" +
                    //                       "  ";
                }



                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ExecuteInstruction();

                DataTable dtCrewBonus = _dbMan1.ResultsDataTable;

                MWDataManager.clsDataAccess _dbManHead = new MWDataManager.clsDataAccess();
                _dbManHead.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManHead.SqlStatement = " " +
                                       " select '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "','" + DsLbl.Text + "' DSCrew, '" + NSLbl.Text + "' NSCrew" +
                                       "  ";

                _dbManHead.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManHead.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManHead.ExecuteInstruction();

                DataTable dthead = _dbManHead.ResultsDataTable;

                //////////Load Factors

                MWDataManager.clsDataAccess _dbManFactor = new MWDataManager.clsDataAccess();
                _dbManFactor.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManFactor.SqlStatement = " " +
                                       " select '" + MerBonusLbl.Text + "' MerBonus,'" + TeamLeaderBonusLbl.Text + "' TeamLeaderBonus, '" + DrillOpBonusLbl.Text + "' DrillOpBonus, '" + AbscentBonusLbl.Text + "' AbscentBonus" +
                                       "  ";

                _dbManFactor.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManFactor.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManFactor.ResultsTableName = "Factors";
                _dbManFactor.ExecuteInstruction();

                DataTable dtFactors = _dbManFactor.ResultsDataTable;



                MWDataManager.clsDataAccess _dbManWP1 = new MWDataManager.clsDataAccess();
                _dbManWP1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "  \r\n ";

                if (rdbStoping.Checked == true)
                {

                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " select * from (select * from (select workplaceid, description, sum(squaremetrestotal) squaremetrestotal, sum(facelength) fl, sum(wastesqm) wastesqm, \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "sum(sweeps) sweeps, Max(reef) reef, sum(metrestotal) metrestotal, sum(reefcubics+wastecubics) cubics, max(endtypeid) endtypeid, '1' order1 from \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "(select case when goldgrade < 0.01 then squaremetrestotal else 0 end as wastesqm, \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "s.workplaceid, s.facelength, w.description,  squaremetrestotal, sweeps, metrestotal, endtypeid,reefcubics,wastecubics, \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " case when w.reefid <> 2 then 'Mer' else 'Ug2' end as reef from mineware.dbo.[vw_bcs_survey] s, mineware.dbo.tbl_bcs_Workplace w  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "and orgunit = '" + DsLbl.Text + "' \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "and s.workplaceid = w.workplaceid) a \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " group by workplaceid, description  )a \r\n ";

                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " union \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " select null,null,null,null,null,null,null,null,null,null, '2' \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " union \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select null,null,null,null,null,null,null,null,null,null, '3' \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " union \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select null,null,null,null,null,null,null,null,null,null, '4' \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " union \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select null,null,null,null,null,null,null,null,null,null, '5')a, \r\n ";

                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "(select  sum(squaremetrestotal) TotSqm, sum(facelength) TotFl, sum(wastesqm) Totwastesqm,  \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "sum(sweeps) Totsweeps, sum(metrestotal) Totmetrestotal, sum(reefcubics+wastecubics) Totcubics from  \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "(select case when goldgrade < 0.01 then squaremetrestotal else 0 end as wastesqm, \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " s.workplaceid, s.facelength, w.description,  squaremetrestotal, sweeps, metrestotal, endtypeid,reefcubics,wastecubics,  \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " case when w.reefid <> 2 then 'Mer' else 'Ug2' end as reef from mineware.dbo.[vw_bcs_survey] s, mineware.dbo.tbl_bcs_Workplace w  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "and orgunit = '" + DsLbl.Text + "' \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and s.workplaceid = w.workplaceid)a)b \r\n ";

                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "order by order1 \r\n ";
                }

                if (rdbDev.Checked == true)
                {

                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " select * from (select * from (select workplaceid, description, convert(decimal(18,1),sum(squaremetrestotal)) squaremetrestotal, sum(facelength) fl, sum(wastesqm) wastesqm, \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "sum(sweeps) sweeps, Max(reef) reef, sum(metrestotal) metrestotal, sum(reefcubics+wastecubics) cubics, max(endtypeid) endtypeid, '1' order1 from \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "(select case when goldgrade < 0.01 then squaremetrestotal else 0 end as wastesqm, \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "s.workplaceid, s.facelength, w.description, metrestotal+ ((reefcubics+wastecubics)/10) squaremetrestotal, sweeps, metrestotal, endtypeid,reefcubics,wastecubics, \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " case when w.reefid <> 2 then 'Mer' else 'Ug2' end as reef from mineware.dbo.[vw_bcs_survey] s, mineware.dbo.tbl_bcs_Workplace w  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "and orgunit = '" + DsLbl.Text + "' \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "and s.workplaceid = w.workplaceid) a \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " group by workplaceid, description  )a \r\n ";

                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " union \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " select null,null,null,null,null,null,null,null,null,null, '2' \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " union \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select null,null,null,null,null,null,null,null,null,null, '3' \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " union \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select null,null,null,null,null,null,null,null,null,null, '4' \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " union \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select null,null,null,null,null,null,null,null,null,null, '5')a, \r\n ";

                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "(select   convert(decimal(18,1),sum(squaremetrestotal)) TotSqm, sum(facelength) TotFl, sum(wastesqm) Totwastesqm,  \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "sum(sweeps) Totsweeps, sum(metrestotal) Totmetrestotal, sum(reefcubics+wastecubics) Totcubics from  \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "(select case when goldgrade < 0.01 then squaremetrestotal else 0 end as wastesqm, \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " s.workplaceid, s.facelength, w.description,  metrestotal+ ((reefcubics+wastecubics)/10) squaremetrestotal, sweeps, metrestotal, endtypeid,reefcubics,wastecubics,  \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " case when w.reefid <> 2 then 'Mer' else 'Ug2' end as reef from mineware.dbo.[vw_bcs_survey] s, mineware.dbo.tbl_bcs_Workplace w  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "and orgunit = '" + DsLbl.Text + "' \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and s.workplaceid = w.workplaceid)a)b \r\n ";

                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "order by order1 \r\n ";
                }

                if (rdbZCrew.Checked == true)
                {
                    string org1 = DsLbl.Text;
                    org1 = (org1 + "     ").Substring(0, 6);
                    org1 = org1 + "%";

                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select top(6) * from (select *, '' workplaceid, orgunitds description, 'Mer' reef, 0 totsweeps, bip squaremetrestotal, 0 wastesqm, 0 sweeps, 0 Totwastesqm, 1 rowa from bmcs_zgangaveragenew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";


                    if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) < 201807)
                    {

                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "and orgunitds like '" + org1 + "' ";
                    }
                    else
                    {
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "and orgunitds in ( select GangID from [NorthamPas].dbo.[tbl.BCS_ZGangsLink] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and [ZGangID] = '" + DsLbl.Text + "') ";
                    }
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " select '', '', '' ,'','', '', '', '' ,'' ,'', '', '' ,'' ,'', 2 rowa ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " select '', '', '' ,'','', '', '', '' ,'' ,'', '', '' ,'' ,'', 3 rowa ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " select '', '', '' ,'','', '', '', '' ,'' ,'', '', '' ,'' ,'', 4 rowa ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " select '', '', '' ,'','', '', '', '' ,'' ,'', '', '' ,'' ,'', 5 rowa ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " select '', '', '' ,'','', '', '', '' ,'' ,'', '', '' ,'' ,'', 6 rowa ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " select '', '', '' ,'','', '', '', '' ,'' ,'', '', '' ,'' ,'', 7 rowa ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " select '', '', '' ,'','', '', '', '' ,'' ,'', '', '' ,'' ,'', 8 rowa ";

                    if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) < 201807)
                    {
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + ") a, (select convert(decimal(18,0),avg(bip)) TotSqm from  bmcs_zgangaveragenew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunitds like '" + org1 + "') b order by  rowa, description ";
                    }
                    else
                    {
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + ") a, (select convert(decimal(18,0),avg(bip)) TotSqm from  bmcs_zgangaveragenew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunitds  in ( select GangID from [NorthamPas].dbo.[tbl.BCS_ZGangsLink] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and [ZGangID] = '" + DsLbl.Text + "')) b order by  rowa, description ";

                    }


                }

                if (rdbConstCrew.Checked == true)
                {
                    string org1 = DsLbl.Text;
                    org1 = (org1 + "          ").Substring(0, 4) + (org1 + "         ").Substring(7, 1);
                    org1 = org1 + "%";

                    // in here


                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select top(6) * from (select *, '' workplaceid, orgunitds description, 'Mer' reef, 0 totsweeps, bip squaremetrestotal, 0 wastesqm, 0 sweeps, 0 Totwastesqm, 1 rowa from bmcs_zgangaveragenew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";
                    // _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "and orgunitds like '" + org1 + "' ";

                    if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) < 201810)
                    {

                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "and orgunitds like '" + org1 + "' ";
                    }
                    else
                    {
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "and orgunitds in ( select GangID from [NorthamPas].dbo.[tbl.BCS_ZGangsLink] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and [ZGangID] = '" + DsLbl.Text + "') ";
                    }



                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " select '', '', '' ,'','', '', '', '' ,'' ,'', '', '' ,'' ,'', 2 rowa ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " select '', '', '' ,'','', '', '', '' ,'' ,'', '', '' ,'' ,'', 3 rowa ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " select '', '', '' ,'','', '', '', '' ,'' ,'', '', '' ,'' ,'', 4 rowa ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " select '', '', '' ,'','', '', '', '' ,'' ,'', '', '' ,'' ,'', 5 rowa ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " select '', '', '' ,'','', '', '', '' ,'' ,'', '', '' ,'' ,'', 6 rowa ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " select '', '', '' ,'','', '', '', '' ,'' ,'', '', '' ,'' ,'', 7 rowa ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " select '', '', '' ,'','', '', '', '' ,'' ,'', '', '' ,'' ,'', 8 rowa ";


                    if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) < 201810)
                    {
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + ") a, (select convert(decimal(18,2),avg(bip)) TotSqm from  bmcs_zgangaveragenew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunitds like '" + org1 + "') b order by  rowa, description ";
                    }
                    else
                    {
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + ") a, (select convert(decimal(18,2),avg(bip)) TotSqm from  bmcs_zgangaveragenew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunitds  in ( select GangID from [NorthamPas].dbo.[tbl.BCS_ZGangsLink] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and [ZGangID] = '" + DsLbl.Text + "')) b order by  rowa, description ";

                    }


                    //_dbManWP1.SqlStatement = _dbManWP1.SqlStatement + ") a, (select convert(decimal(18,2),avg(bip)) TotSqm from  bmcs_zgangaveragenew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunitds like '" + org1 + "') b order by  rowa, description ";

                }


                if (rdbXCCrew.Checked == true)
                {
                    string org1 = DsLbl.Text;
                    org1 = (org1 + "          ").Substring(0, 4) + (org1 + "         ").Substring(7, 1);
                    org1 = org1 + "%";

                    string org2 = DsLbl.Text;
                    org2 = (org2 + "          ").Substring(0, 4) + (org2 + "         ").Substring(8, 1);
                    org2 = org2 + "%";

                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select top(6) * from (select *, '' workplaceid, orgunitds description, 'Mer' reef, 0 totsweeps, bip squaremetrestotal, 0 wastesqm, 0 sweeps, 0 Totwastesqm, 1 rowa from [Mineware].[dbo].bmcs_zgangaveragenew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";
                    //_dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "and orgunitds like '" + org1 + "' ";

                    if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) < 201808)
                    {

                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "and orgunitds like '" + org1 + "' ";
                    }
                    else
                    {
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "and orgunitds in ( select GangID from [NorthamPas].dbo.[tbl.BCS_ZGangsLink] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and [ZGangID] = '" + DsLbl.Text + "') ";
                    }

                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " select '', '', '' ,'','', '', '', '' ,'' ,'', '', '' ,'' ,'', 2 rowa ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " select '', '', '' ,'','', '', '', '' ,'' ,'', '', '' ,'' ,'', 3 rowa ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " select '', '', '' ,'','', '', '', '' ,'' ,'', '', '' ,'' ,'', 4 rowa ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " select '', '', '' ,'','', '', '', '' ,'' ,'', '', '' ,'' ,'', 5 rowa ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " select '', '', '' ,'','', '', '', '' ,'' ,'', '', '' ,'' ,'', 6 rowa ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " select '', '', '' ,'','', '', '', '' ,'' ,'', '', '' ,'' ,'', 7 rowa ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " select '', '', '' ,'','', '', '', '' ,'' ,'', '', '' ,'' ,'', 8 rowa ";


                    // _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + ") a, (select convert(decimal(18,0),avg(bip)) TotSqm from  [Mineware].[dbo].bmcs_zgangaveragenew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunitds like '" + org1 + "') b order by  rowa, description  ";

                    if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) < 201808)
                    {
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + ") a, (select convert(decimal(18,0),avg(bip)) TotSqm from  bmcs_zgangaveragenew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunitds like '" + org1 + "') b order by  rowa, description ";
                    }
                    else
                    {
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + ") a, (select convert(decimal(18,0),avg(bip)) TotSqm from  bmcs_zgangaveragenew where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunitds  in ( select GangID from [NorthamPas].dbo.[tbl.BCS_ZGangsLink] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and [ZGangID] = '" + DsLbl.Text + "')) b order by  rowa, description ";

                    }

                }

                if (rdbDevRailCrew.Checked == true)
                {
                    if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) >= 201903)
                    {

                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select top(6) *,  isnull(convert(decimal(18,1),TotSqm1/(num+0.00001)),0) TotSqm, case when description <> '' then TotSqm1 else '' end as squaremetrestotal, case when description <> '' then num else '' end as wastesqm, 0 sweeps, num Totwastesqm from (select * from (select description , '' workplaceid, 0 Totwastesqm1, 'MER' Reef, 0 totsweeps, '1' rowa From ( \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "SELECT substring('" + DsLbl.Text + "',1,4) description  \r\n ";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "  FROM [Mineware].[dbo].[tbl_BCS_DevRepNew] \r\n ";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  and orgunit in  ( select GangID from [NorthamPas].dbo.[tbl.BCS_ZGangsLink] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and [ZGangID] = '" + DsLbl.Text + "') ";




                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "  and workplace in (select description from mineware.dbo.tbl_bcs_Workplace where endtypeid in ('1','2','8','9') \r\n ";

                        if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) == "201908" && DsLbl.Text == "0744E1T507")
                            _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "  or description = '07 11 M RSE  ' \r\n ";

                        if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) == "201908" && DsLbl.Text == "0744EX7")
                            _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "  or description = '07 11 M RSE  ' \r\n ";


                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + ")) a \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "group by description) a \r\n";

                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '2'   \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '3'   \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '4'   \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '5'   \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '6'  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '7'  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '8'  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '9'  ) a, \r\n";


                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "(select sum(bip) TotSqm1, count(workplace) num  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "FROM [Mineware].[dbo].[tbl_BCS_DevRepNew] \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'   and orgunit in  ( select GangID from [NorthamPas].dbo.[tbl.BCS_ZGangsLink] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and [ZGangID] = '" + DsLbl.Text + "') ";

                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and workplace in (select description from mineware.dbo.tbl_bcs_Workplace where endtypeid in ('1','2','8','9') ";
                        if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) == "201908" && DsLbl.Text == "0744E1T507")
                            _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "  or description = '07 11 M RSE  ' \r\n ";

                        if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) == "201908" && DsLbl.Text == "0744EX7")
                            _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "  or description = '07 11 M RSE  ' \r\n ";



                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "  ) ) b \r\n";

                    }
                    else
                    {
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select top(6) *,  isnull(convert(decimal(18,1),TotSqm1/(num+0.00001)),0) TotSqm, case when description <> '' then TotSqm1 else '' end as squaremetrestotal, case when description <> '' then num else '' end as wastesqm, 0 sweeps, num Totwastesqm from (select * from (select description , '' workplaceid, 0 Totwastesqm1, 'MER' Reef, 0 totsweeps, '1' rowa From ( \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "SELECT substring('" + DsLbl.Text + "',1,4) description  \r\n ";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "  FROM [Mineware].[dbo].[tbl_BCS_DevRepNew] \r\n ";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunit like  substring('" + DsLbl.Text + "',1,4)+'%' \r\n ";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and convert(varchar(10),prodmonth)+'" + DsLbl.Text + "'+workplace <> '2018120113D1T514A14 FWD  W'  \r\n ";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and (substring(workplace,1,2) = substring('" + DsLbl.Text + "',6,2) or  substring(workplace,1,2) = substring('" + DsLbl.Text + "',9,2)) \r\n ";
                        if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) == "201902" && DsLbl.Text == "0113D1T514")
                        {
                            _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "  and workplace in (select description from mineware.dbo.tbl_bcs_Workplace where endtypeid in ('1','2')) \r\n ";
                        }
                        else
                        {
                            if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) == "201902" && (DsLbl.Text == "0113D1T414A" || DsLbl.Text == "0113D1T514A"))
                                _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "  and workplace in (select description from mineware.dbo.tbl_bcs_Workplace where endtypeid in ('8')) \r\n ";
                            else
                                _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "  and workplace in (select description from mineware.dbo.tbl_bcs_Workplace where endtypeid in ('1','2','8')) \r\n ";

                        }


                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + ") a \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "group by description) a \r\n";

                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '2'   \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '3'   \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '4'   \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '5'   \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '6'  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '7'  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '8'  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '9'  ) a, \r\n";


                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "(select sum(bip) TotSqm1, count(workplace) num  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "FROM [Mineware].[dbo].[tbl_BCS_DevRepNew] \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunit like  substring('" + DsLbl.Text + "',1,4)+'%' \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and convert(varchar(10),prodmonth)+'" + DsLbl.Text + "'+workplace <> '2018120113D1T514A14 FWD  W'  \r\n ";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "and (substring(workplace,1,2) = substring('" + DsLbl.Text + "',6,2) or  substring(workplace,1,2) = substring('" + DsLbl.Text + "',9,2))  \r\n";
                        if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) == "201902" && DsLbl.Text == "0113D1T514")
                        {
                            _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and workplace in (select description from mineware.dbo.tbl_bcs_Workplace where endtypeid in ('1','2')) ) b \r\n";
                        }
                        else
                        {
                            if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) == "201902" && (DsLbl.Text == "0113D1T414A" || DsLbl.Text == "0113D1T514A"))
                                _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and workplace in (select description from mineware.dbo.tbl_bcs_Workplace where endtypeid in ('8')) ) b \r\n";
                            else
                                _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and workplace in (select description from mineware.dbo.tbl_bcs_Workplace where endtypeid in ('1','2','8')) ) b \r\n";
                        }

                    }



                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "order by rowa \r\n";

                }


                if (rdbDevTransCrew.Checked == true)
                {
                    if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) >= 201903)
                    {

                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select top(6) *, convert(decimal(18,2),isnull(TotSqm1,0)/(num+0.00001)) TotSqm, case when description <> '' then TotSqm1 else '' end as squaremetrestotal, case when description <> '' then num else '' end as wastesqm, 0 sweeps, num Totwastesqm from (select * from (select description , '' workplaceid, 0 Totwastesqm1, 'MER' Reef, 0 totsweeps, '1' rowa From ( \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "SELECT substring('" + DsLbl.Text + "',1,4) description  \r\n ";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "  FROM [Mineware].[dbo].[tbl_BCS_DevRepNew] \r\n ";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'   and orgunit in  ( select GangID from [NorthamPas].dbo.[tbl.BCS_ZGangsLink] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and [ZGangID] = '" + DsLbl.Text + "') ";
                        // and orgunit like  substring('" + DsLbl.Text + "',1,4)+'%' \r\n ";
                        //   if (ProdMonthTxt.Text == "201812" && DsLbl.Text == "0113BT514A")
                        //   _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and orgunit = '0113D2FB' \r\n ";
                        //   else
                        //        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and (substring(workplace,1,2) = substring('" + DsLbl.Text + "',6,2) or  substring(workplace,1,2) = substring('" + DsLbl.Text + "',8,2)) \r\n ";

                        //_dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and (substring(workplace,1,2) = substring('" + DsLbl.Text + "',6,2) or  substring(workplace,1,2) = substring('" + DsLbl.Text + "',8,2)) \r\n ";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "  and workplace in (select description from mineware.dbo.tbl_bcs_Workplace where endtypeid in ('1','2','9') \r\n ";


                        if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) == "201908" && DsLbl.Text == "0744DT407")
                            _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "  or description = '07 11 M RSE  ' \r\n ";


                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " ) ) a \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "group by description) a \r\n";

                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '2'   \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '3'   \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '4'   \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '5'   \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '6'  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '7'  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '8'  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '9'  ) a, \r\n";


                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "(select sum(bip) TotSqm1, count(workplace) num  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "FROM [Mineware].[dbo].[tbl_BCS_DevRepNew] \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'   and orgunit in  ( select GangID from [NorthamPas].dbo.[tbl.BCS_ZGangsLink] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and [ZGangID] = '" + DsLbl.Text + "') ";

                        //and orgunit like  substring('" + DsLbl.Text + "',1,4)+'%' \r\n";

                        // if (ProdMonthTxt.Text == "201812" && DsLbl.Text == "0113BT514A")
                        //      _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and orgunit = '0113D2FB' \r\n ";
                        //   else
                        //       _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and (substring(workplace,1,2) = substring('" + DsLbl.Text + "',6,2) or  substring(workplace,1,2) = substring('" + DsLbl.Text + "',8,2)) \r\n ";



                        //_dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "and (substring(workplace,1,2) = substring('" + DsLbl.Text + "',6,2) or  substring(workplace,1,2) = substring('" + DsLbl.Text + "',8,2)) \r\n";


                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and workplace in (select description from mineware.dbo.tbl_bcs_Workplace where endtypeid in ('1','2','9')  \r\n ";
                        if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) == "201908" && DsLbl.Text == "0744DT407")
                            _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "  or description = '07 11 M RSE  ' \r\n ";


                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + ") ) b \r\n";
                    }
                    else
                    {

                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select top(6) *, convert(decimal(18,2),isnull(TotSqm1,0)/(num+0.00001)) TotSqm, case when description <> '' then TotSqm1 else '' end as squaremetrestotal, case when description <> '' then num else '' end as wastesqm, 0 sweeps, num Totwastesqm from (select * from (select description , '' workplaceid, 0 Totwastesqm1, 'MER' Reef, 0 totsweeps, '1' rowa From ( \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "SELECT substring('" + DsLbl.Text + "',1,4) description  \r\n ";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "  FROM [Mineware].[dbo].[tbl_BCS_DevRepNew] \r\n ";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunit like  substring('" + DsLbl.Text + "',1,4)+'%' \r\n ";
                        if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) == "201812" && DsLbl.Text == "0113BT514A")
                            _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and orgunit = '0113D2FB' \r\n ";
                        else
                            _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and (substring(workplace,1,2) = substring('" + DsLbl.Text + "',6,2) or  substring(workplace,1,2) = substring('" + DsLbl.Text + "',8,2)) \r\n ";

                        //_dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and (substring(workplace,1,2) = substring('" + DsLbl.Text + "',6,2) or  substring(workplace,1,2) = substring('" + DsLbl.Text + "',8,2)) \r\n ";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "  and workplace in (select description from mineware.dbo.tbl_bcs_Workplace where endtypeid in ('1','2')) \r\n ";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + ") a \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "group by description) a \r\n";

                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '2'   \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '3'   \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '4'   \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '5'   \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '6'  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '7'  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '8'  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '9'  ) a, \r\n";


                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "(select sum(bip) TotSqm1, count(workplace) num  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "FROM [Mineware].[dbo].[tbl_BCS_DevRepNew] \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunit like  substring('" + DsLbl.Text + "',1,4)+'%' \r\n";

                        if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) == "201812" && DsLbl.Text == "0113BT514A")
                            _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and orgunit = '0113D2FB' \r\n ";
                        else
                            _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and (substring(workplace,1,2) = substring('" + DsLbl.Text + "',6,2) or  substring(workplace,1,2) = substring('" + DsLbl.Text + "',8,2)) \r\n ";



                        //_dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "and (substring(workplace,1,2) = substring('" + DsLbl.Text + "',6,2) or  substring(workplace,1,2) = substring('" + DsLbl.Text + "',8,2)) \r\n";


                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and workplace in (select description from mineware.dbo.tbl_bcs_Workplace where endtypeid in ('1','2')) ) b \r\n";

                    }

                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "order by rowa \r\n";

                }

                if (rdbDevCleanCrew.Checked == true)
                {


                    if (DsLbl.Text.Substring(4, 1) == "X")
                    {
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select top(6) *, isnull(convert(decimal(18,2),TotSqm1/(num+0.0000001)),0) TotSqm, case when description <> '' then TotSqm1 else '' end as squaremetrestotal, case when description <> '' then num else '' end as wastesqm, 0 sweeps, num Totwastesqm from (select * from (select description , '' workplaceid, 0 Totwastesqm1, 'MER' Reef, 0 totsweeps, '1' rowa From ( \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "SELECT substring('" + DsLbl.Text + "',1,4) description  \r\n ";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "  FROM [Mineware].[dbo].[tbl_BCS_DevRepNew] \r\n ";

                        if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) < 201805)
                            _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunit like  substring('" + DsLbl.Text + "',1,4)+'%'  and substring(workplace,1,2) = substring('" + DsLbl.Text + "',6,2) \r\n ";
                        else
                            _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and (orgunit like  substring('" + DsLbl.Text + "',1,4)+substring('" + DsLbl.Text + "',7,1)+'%' or  substring(workplace,1,2) = substring('" + DsLbl.Text + "',8,2)) \r\n ";


                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and substring(orgunit,7,1) in ('R','B') \r\n ";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and workplace in (select description from mineware.dbo.tbl_bcs_Workplace where endtypeid in ( '3','4','5','11','12','9')) \r\n ";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + ") a \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "group by description) a \r\n";

                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '2'   \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '3'   \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '4'   \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '5'   \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '6'  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '7'  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '8'  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '9'  ) a, \r\n";


                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "(select sum(bip) TotSqm1, count(workplace) num  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "FROM [Mineware].[dbo].[tbl_BCS_DevRepNew] \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunit like  substring('" + DsLbl.Text + "',1,4)+'%'  and substring(workplace,1,2) = substring('" + DsLbl.Text + "',6,2) \r\n ";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and substring(orgunit,7,1) in ('R','B') \r\n ";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and workplace in (select description from mineware.dbo.tbl_bcs_Workplace where endtypeid in ( '3','4','5','11','12','9')) \r\n ";

                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " ) b \r\n";

                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "order by rowa \r\n";
                    }
                    else
                    {
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select top(6) *, isnull(convert(decimal(18,2),TotSqm1/(num+0.0000001)),0) TotSqm, case when description <> '' then TotSqm1 else '' end as squaremetrestotal, case when description <> '' then num else '' end as wastesqm, 0 sweeps, num Totwastesqm from (select * from (select description , '' workplaceid, 0 Totwastesqm1, 'MER' Reef, 0 totsweeps, '1' rowa From ( \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "SELECT substring('" + DsLbl.Text + "',1,4) description  \r\n ";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "  FROM [Mineware].[dbo].[tbl_BCS_DevRepNew] \r\n ";
                        // _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunit like  substring('" + DsLbl.Text + "',1,4)+substring('" + DsLbl.Text + "',7,1)+'%' \r\n ";

                        if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) < 201805)
                            _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunit like  substring('" + DsLbl.Text + "',1,4)+'%'  and substring(workplace,1,2) = substring('" + DsLbl.Text + "',6,2) \r\n ";
                        else
                        {
                            if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) < 202009)
                                _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and (orgunit like  substring('0744AT703',1,4)+substring('0744AT703',7,1)+'%' or  substring(workplace,1,2) = substring('0744AT703',8,2)) \r\n ";
                            else
                                _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " where  prodmonth ='" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunit in (select GangID from [NorthamPas].dbo.[tbl.BCS_ZGangsLink] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and [ZGangID] = '" + DsLbl.Text + "') ";

                        }


                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and substring(orgunit,7,1) in ('R','B') \r\n ";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and workplace in (select description from mineware.dbo.tbl_bcs_Workplace where endtypeid in ( '3','4','5','11','12','9')) \r\n ";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + ") a \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "group by description) a \r\n";

                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '2'   \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '3'   \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '4'   \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '5'   \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '6'  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '7'  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '8'  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '9'  ) a, \r\n";


                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "(select sum(bip) TotSqm1, count(workplace) num  \r\n";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "FROM [Mineware].[dbo].[tbl_BCS_DevRepNew] \r\n";
                        // _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunit like  substring('" + DsLbl.Text + "',1,4)+substring('" + DsLbl.Text + "',7,1)+'%' \r\n ";

                        if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) < 201805)
                            _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunit like  substring('" + DsLbl.Text + "',1,4)+substring('" + DsLbl.Text + "',7,1)+'%' \r\n ";
                        else
                        {
                            if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) < 202009)
                                _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  and orgunit like  substring('" + DsLbl.Text + "',1,5)+'%'  \r\n ";
                            else
                                _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " where  prodmonth ='" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunit in (select GangID from [NorthamPas].dbo.[tbl.BCS_ZGangsLink] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and [ZGangID] = '" + DsLbl.Text + "') ";
                        }
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and substring(orgunit,7,1) in ('R','B') \r\n ";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and workplace in (select description from mineware.dbo.tbl_bcs_Workplace where endtypeid in ( '3','4','5','11','12','9')) \r\n ";

                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " ) b \r\n";

                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "order by rowa \r\n";

                    }

                }


                if (rdbDevConCrew.Checked == true)
                {
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select top(6) *, convert(decimal(18,0),TotSqm1/(num+0.00001)) TotSqm, case when description <> '' then bip else '' end as squaremetrestotal, case when description <> '' then '' else '' end as wastesqm, 0 sweeps, 0 Totwastesqm from (select * from (select description, sum(bip) bip , '' workplaceid, 0 Totwastesqm1, 'MER' Reef, 0 totsweeps, '1' rowa From ( \r\n";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "SELECT orgunit description, bip  \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "  FROM [Mineware].[dbo].[tbl_BCS_DevRepNew] \r\n ";
                    // _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunit like  substring('" + DsLbl.Text + "',1,4)+substring('" + DsLbl.Text + "',7,1)+'%' \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";
                    if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) >= 201905)
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "and orgunit in  ( select GangID from [NorthamPas].dbo.[tbl.BCS_ZGangsLink] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and [ZGangID] = '" + DsLbl.Text + "') ";
                    else
                    {
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "and  (orgunit like (substring('" + DsLbl.Text + "',1,4)+substring('" + DsLbl.Text + "',7,1)+'%' ) or  orgunit like ";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "(select case when substring('" + DsLbl.Text + "',5,1) = 'A' then substring('" + DsLbl.Text + "',1,4)+'A%' when substring('" + DsLbl.Text + "',5,1) = 'B' then substring('" + DsLbl.Text + "',1,4)+'B%' when substring('" + DsLbl.Text + "',5,1) = 'D' then substring('" + DsLbl.Text + "',1,4)+'D%' else substring('" + DsLbl.Text + "',1,4)+'C%' end as aa)      ) ";
                    }
                    //_dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and substring(orgunit,7,1) in ('R','B') \r\n ";
                    //_dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and workplace in (select description from mineware.dbo.tbl_bcs_Workplace where endtypeid in ( '3','4','5','11','12')) \r\n ";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + ") a \r\n";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "group by description) a \r\n";

                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '', '2'   \r\n";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '', '3'   \r\n";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '', '4'   \r\n";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '', '5'   \r\n";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '', '6'  \r\n";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '', '7'  \r\n";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '', '8'  \r\n";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "union  \r\n";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "select '', '', '', '', '', '', '9'  ) a, \r\n";


                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "(select sum(bip) TotSqm1, count(workplace) num  \r\n";
                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "FROM [Mineware].[dbo].[tbl_BCS_DevRepNew] \r\n";
                    // _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and orgunit like  substring('" + DsLbl.Text + "',1,4)+substring('" + DsLbl.Text + "',7,1)+'%' \r\n ";


                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";

                    if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) >= 201905)
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "and orgunit in  ( select GangID from [NorthamPas].dbo.[tbl.BCS_ZGangsLink] where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and [ZGangID] = '" + DsLbl.Text + "') ";
                    else
                    {
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "and  (orgunit like (substring('" + DsLbl.Text + "',1,4)+substring('" + DsLbl.Text + "',7,1)+'%' ) or  orgunit like ";
                        _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "(select case when substring('" + DsLbl.Text + "',5,1) = 'A' then substring('" + DsLbl.Text + "',1,4)+'A%' when substring('" + DsLbl.Text + "',5,1) = 'B' then substring('" + DsLbl.Text + "',1,4)+'B%' when substring('" + DsLbl.Text + "',5,1) = 'D' then substring('" + DsLbl.Text + "',1,4)+'D%' else substring('" + DsLbl.Text + "',1,4)+'C%' end as aa)      ) ";
                    }


                    //  and (orgunit like (substring('" + DsLbl.Text + "',1,4)+substring('" + DsLbl.Text + "',7,1)+'%' ) or  orgunit like ";
                    //  _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "(select case when substring('" + DsLbl.Text + "',5,1) = 'A' then substring('" + DsLbl.Text + "',1,4)+'A%' when substring('" + DsLbl.Text + "',5,1) = 'B' then substring('" + DsLbl.Text + "',1,4)+'B%'   when substring('" + DsLbl.Text + "',5,1) = 'D' then substring('" + DsLbl.Text + "',1,4)+'D%' else substring('" + DsLbl.Text + "',1,4)+'C%' end as aa)      ) ";
                    //_dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and substring(orgunit,7,1) in ('R','B') \r\n ";
                    //_dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " and workplace in (select description from mineware.dbo.tbl_bcs_Workplace where endtypeid in ( '3','4','5','11','12')) \r\n ";

                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + " ) b \r\n";

                    _dbManWP1.SqlStatement = _dbManWP1.SqlStatement + "order by rowa \r\n";

                }

                _dbManWP1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManWP1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManWP1.ExecuteInstruction();


                if (_dbMan1.ResultsDataTable.Rows[0]["PossShift"] == DBNull.Value)
                {
                    MessageBox.Show("Zero people in orgunit", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;

                }



                DataTable dtWP = _dbManWP1.ResultsDataTable;

                BonusShifts = 0;
                RDOBonusShifts = 0;
                RDOPosShifts = Convert.ToDecimal(_dbMan1.ResultsDataTable.Rows[0]["PossShift"].ToString());
                SqmPerMan = 0;
                MPerShift = 0;

                PossShifts = Convert.ToInt32(_dbMan1.ResultsDataTable.Rows[0]["PossShift"].ToString());
                row = 0;

                foreach (DataRow dr in dtCrewBonus.Rows)
                {

                    if (dr["industrynumber"].ToString() != "")
                    {
                        //Get RDO
                        if (dr["SubGroup"].ToString() == "Machine Operator" && dr["BonusShifts"].ToString() != null)
                        {
                            RDOBonusShifts = RDOBonusShifts + Convert.ToInt32(dr["BonusShifts"].ToString());
                        }
                        DataGrid.Rows[row].Cells[0].Value = "1";
                        DataGrid.Rows[row].Cells[1].Value = String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
                        DataGrid.Rows[row].Cells[2].Value = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue));
                        DataGrid.Rows[row].Cells[3].Value = "0";

                        if (rdbDev.Checked == true)
                            DataGrid.Rows[row].Cells[3].Value = "1";

                        if (rdbDevRailCrew.Checked == true)
                            DataGrid.Rows[row].Cells[3].Value = "1";
                        if (rdbDevTransCrew.Checked == true)
                            DataGrid.Rows[row].Cells[3].Value = "1";
                        if (rdbDevCleanCrew.Checked == true)
                            DataGrid.Rows[row].Cells[3].Value = "1";
                        if (rdbConstCrew.Checked == true)
                            DataGrid.Rows[row].Cells[3].Value = "1";


                        DataGrid.Rows[row].Cells[4].Value = dr["industrynumber"].ToString();
                        DataGrid.Rows[row].Cells[5].Value = dr["initials"].ToString();
                        DataGrid.Rows[row].Cells[6].Value = dr["surname"].ToString();
                        DataGrid.Rows[row].Cells[7].Value = dr["SubGroup"].ToString();
                        DataGrid.Rows[row].Cells[8].Value = dr["orgunit"].ToString();
                        DataGrid.Rows[row].Cells[9].Value = (dr["shift"].ToString()).Substring(0, 1);
                        DataGrid.Rows[row].Cells[10].Value = _dbManWP1.ResultsDataTable.Rows[0]["description"].ToString();
                        DataGrid.Rows[row].Cells[11].Value = _dbManWP1.ResultsDataTable.Rows[0]["workplaceid"].ToString();
                        // DataGrid.Rows[row].Cells[12].Value = dr["GrossAmount"].ToString();
                        DataGrid.Rows[row].Cells[13].Value = dr["BonusShifts"].ToString();
                        DataGrid.Rows[row].Cells[14].Value = dr["ttAWOP"].ToString();
                        DataGrid.Rows[row].Cells[15].Value = "Production Unit Stoping Bonus";

                        if (rdbDev.Checked == true)
                            DataGrid.Rows[row].Cells[15].Value = "Production Unit Development Bonus";

                        if (rdbDevRailCrew.Checked == true)
                            DataGrid.Rows[row].Cells[15].Value = "Production Unit Development Bonus";
                        if (rdbDevTransCrew.Checked == true)
                            DataGrid.Rows[row].Cells[15].Value = "Production Unit Development Bonus";
                        if (rdbDevCleanCrew.Checked == true)
                            DataGrid.Rows[row].Cells[15].Value = "Production Unit Development Bonus";
                        if (rdbConstCrew.Checked == true)
                            DataGrid.Rows[row].Cells[15].Value = "Production Unit Development Bonus";

                        if (dr["SubGroup"].ToString() == "Team Leader")
                        {
                            DataGrid.Rows[row].Cells[15].Value = "Special Team Leader Stoping Bonus";
                            if (rdbDev.Checked == true)
                                DataGrid.Rows[row].Cells[15].Value = "Special Team Leader Development Bonus";
                            if (rdbDevRailCrew.Checked == true)
                                DataGrid.Rows[row].Cells[15].Value = "Special Team Leader Development Bonus";
                            if (rdbDevTransCrew.Checked == true)
                                DataGrid.Rows[row].Cells[15].Value = "Special Team Leader Development Bonus";
                            if (rdbDevCleanCrew.Checked == true)
                                DataGrid.Rows[row].Cells[15].Value = "Special Team Leader Development Bonus";
                            if (rdbConstCrew.Checked == true)
                                DataGrid.Rows[row].Cells[15].Value = "Special Team Leader Development Bonus";

                        }
                        if (dr["SubGroup"].ToString() == "Machine Operator")
                        {
                            DataGrid.Rows[row].Cells[15].Value = "Machine Operator Stoping Bonus";
                            if (rdbDev.Checked == true)
                                DataGrid.Rows[row].Cells[15].Value = "Machine Operator Development Bonus";
                        }

                        BonusShifts = BonusShifts + Convert.ToInt32(dr["BonusShifts"].ToString());

                        DataGrid.Rows[row].Cells[21].Value = "28";
                        DataGrid.Rows[row].Cells[22].Value = "";
                        DataGrid.Rows[row].Cells[23].Value = "";

                        DataGrid.Rows[row].Cells[26].Value = dr["PossShift"].ToString();
                        DataGrid.Rows[row].Cells[27].Value = dr["sickdays"].ToString();
                        DataGrid.Rows[row].Cells[34].Value = dr["flagorder"].ToString();

                        DataGrid.Rows[row].Cells[31].Value = Convert.ToDecimal(dr["ttawop"].ToString()) * Convert.ToDecimal(dr["absentfactor"].ToString());

                        if (Convert.ToDecimal(dr["ttawop"].ToString()) * Convert.ToDecimal(dr["absentfactor"].ToString()) > 90)
                            DataGrid.Rows[row].Cells[31].Value = 100;

                        DataGrid.Rows[row].Cells[32].Value = (Convert.ToDecimal(1) - Convert.ToDecimal(dr["sawop"].ToString())) * 100;

                        DataGrid.Rows[row].Cells[33].Value = Convert.ToDecimal(DataGrid.Rows[row].Cells[31].Value) + Convert.ToDecimal(DataGrid.Rows[row].Cells[32].Value);

                        if (Convert.ToDecimal(DataGrid.Rows[row].Cells[33].Value) >= Convert.ToDecimal(100))
                            DataGrid.Rows[row].Cells[33].Value = 100;


                        DataGrid.Rows[row].Cells[49].Value = dr["designation"].ToString();

                        row = row + 1;
                    }

                }


                AvgEmp = Convert.ToInt32(Math.Round(Convert.ToDecimal(BonusShifts) / Convert.ToDecimal(PossShifts), 0));
                AvgRDOs = RDOBonusShifts / RDOPosShifts;
                if (rdbStoping.Checked == true)
                    if (AvgEmp > 0)
                        SqmPerMan = Convert.ToInt32(_dbManWP1.ResultsDataTable.Rows[0]["TotSqm"].ToString()) / AvgEmp;
                    else
                        SqmPerMan = 0;


                if (rdbZCrew.Checked == true)
                    SqmPerMan = 0;
                if (rdbStoping.Checked == true)
                {
                    totsqmexp = Convert.ToInt32(_dbManWP1.ResultsDataTable.Rows[0]["TotSqm"].ToString());
                    wastesqmexp = Convert.ToInt32(_dbManWP1.ResultsDataTable.Rows[0]["Totwastesqm"].ToString());
                    totswpexp = Convert.ToInt32(_dbManWP1.ResultsDataTable.Rows[0]["totsweeps"].ToString());
                }

                if (rdbDev.Checked == true)
                {
                    totmexp = Convert.ToDecimal(_dbManWP1.ResultsDataTable.Rows[0]["TotSqm"].ToString());
                }

                decimal HolesPerM = 7;
                int RatesPerHole = 1;

                if (rdbDev.Checked == true)
                {
                    if (DevType == "FWD")
                    {
                        RatesPerHole = 2;
                        HolesPerM = Convert.ToDecimal(34.7);
                    }

                    if (DevType == "RSE")
                    {
                        RatesPerHole = 2;
                        HolesPerM = Convert.ToDecimal(17.6);
                    }


                    if (DevType == "BH")
                    {
                        RatesPerHole = 3;
                        HolesPerM = Convert.ToDecimal(24);
                    }


                    if (DevType == "FWD")
                    {
                        MPerShift = Convert.ToDecimal(1.7);
                    }
                    else if (DevType == "RSE")
                    {
                        MPerShift = Convert.ToDecimal(1.5);
                    }
                    else if (DevType == "BH")
                    {
                        MPerShift = Convert.ToDecimal(0.8);
                    }

                }
                else
                {

                    if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) < 201707)
                    {
                        if (_dbManWP1.ResultsDataTable.Rows[0]["reef"].ToString() == "Mer")
                            MPerShift = Convert.ToDecimal("14.2");
                        else
                            MPerShift = Convert.ToDecimal("15.4");
                    }

                    if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) >= 201707)
                    {
                        if (_dbManWP1.ResultsDataTable.Rows[0]["reef"].ToString() == "Mer")
                            MPerShift = Convert.ToDecimal("13.3");
                        else
                            MPerShift = Convert.ToDecimal("14.5");
                    }

                    if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) >= 201711)
                    {
                        if (_dbManWP1.ResultsDataTable.Rows[0]["reef"].ToString() == "Mer")
                            MPerShift = Convert.ToDecimal("14.2");
                        else
                            MPerShift = Convert.ToDecimal("15.4");
                    }


                    if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) >= 201907)
                    {
                        if (_dbManWP1.ResultsDataTable.Rows[0]["reef"].ToString() == "Mer")
                            MPerShift = Convert.ToDecimal("13.42");
                        else
                            MPerShift = Convert.ToDecimal("15.51");
                    }


                    if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) >= 202007)
                    {
                        if (_dbManWP1.ResultsDataTable.Rows[0]["reef"].ToString() == "Mer")
                            MPerShift = Convert.ToDecimal("13.42");
                        else
                            MPerShift = Convert.ToDecimal("15.95");
                    }




                }

                ReportCrewBonus = new DataSet();
                ReportCrewBonus.Tables.Add(_dbMan1.ResultsDataTable);
                ReportCrewBonus.Tables.Add(_dbManWP1.ResultsDataTable);
                ReportCrewBonus.Tables.Add(_dbManHead.ResultsDataTable);
                ReportCrewBonus.Tables.Add(_dbManFactor.ResultsDataTable);

                MWDataManager.clsDataAccess _dbManStartRand = new MWDataManager.clsDataAccess();
                _dbManStartRand.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " ";

                if (rdbStoping.Checked == true)
                {
                    if (_dbManWP1.ResultsDataTable.Rows[0]["reef"].ToString() != "Mer")
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " select case when '" + _dbManWP1.ResultsDataTable.Rows[0]["TotSqm"].ToString() + "' = 0 then 0 else StartRand end as StartRand  from (select isnull(MAX(BIPStopingAmount),0) StartRand from tbl_BCS_BIPStoping \r\n ";
                    else
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + "  select case when '" + _dbManWP1.ResultsDataTable.Rows[0]["TotSqm"].ToString() + "' = 0 then 0 else StartRand end as StartRand  from (select isnull(MAX(BIPStopingAmount),0) StartRand from tbl_BCS_BIPStopingMer \r\n ";
                    _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";
                    _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " and BIPStopingAvgEmp = '" + AvgEmp + "' and BIPStopingSQM <= '" + _dbManWP1.ResultsDataTable.Rows[0]["TotSqm"].ToString() + "' \r\n ";

                }


                if (rdbZCrew.Checked == true)
                {
                    _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " select '" + _dbManWP1.ResultsDataTable.Rows[0]["TotSqm"].ToString() + "' StartRand  \r\n ";
                }



                if (rdbConstCrew.Checked == true)
                {
                    _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " select '" + Convert.ToDecimal(_dbManWP1.ResultsDataTable.Rows[0]["TotSqm"].ToString()) * Convert.ToDecimal(0.8) + "' StartRand  \r\n ";
                }

                if (rdbXCCrew.Checked == true)
                {
                    _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " select '" + Convert.ToDecimal(_dbManWP1.ResultsDataTable.Rows[0]["TotSqm"].ToString()) * Convert.ToDecimal(0.8) + "' StartRand  \r\n ";
                }


                string WaterEndApplied1 = "";

                MWDataManager.clsDataAccess _dbManWaterA1 = new MWDataManager.clsDataAccess();
                _dbManWaterA1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManWaterA1.SqlStatement = _dbManWaterA1.SqlStatement + " select * from dbo.tbl_BCS_OrgunitWaterEnd \r\n ";
                _dbManWaterA1.SqlStatement = _dbManWaterA1.SqlStatement + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";
                _dbManWaterA1.SqlStatement = _dbManWaterA1.SqlStatement + " and orgunit = '" + DsLbl.Text + "' \r\n ";

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




                if (rdbDev.Checked == true)
                {

                    if (DevType == "RSE")
                    {
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " Select isnull(max(BIPDevRaisesAmount),0) StartRand \r\n  ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " from tbl_BCS_BIPDevRaises \r\n  ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " where  BIPDevRaisesSQM <= '" + Convert.ToDecimal(_dbManWP1.ResultsDataTable.Rows[0]["TotSqm"].ToString()) + "' \r\n  ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " and  BIPDevRaisesAvgEmp = '" + AvgEmp + "' \r\n ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " and ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";

                    }

                    if (DevType == "FWD")
                    {
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " Select isnull(max(BIPDevLateralAmount),0) StartRand \r\n  ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " from tbl_BCS_BIPDevLateral \r\n  ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " where  BIPDevLateralSQM <= '" + Convert.ToDecimal(_dbManWP1.ResultsDataTable.Rows[0]["TotSqm"].ToString()) + "' \r\n  ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " and  BIPDevLateralAvgEmp = '" + AvgEmp + "' \r\n ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " and ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";


                    }

                    if (DevType == "BH")
                    {
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " Select isnull(max(BIPDevBHAmount),0) StartRand \r\n  ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " from tbl_BCS_BIPDevBoxHole \r\n  ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " where  BIPDevBHSQM <= '" + Convert.ToDecimal(_dbManWP1.ResultsDataTable.Rows[0]["TotSqm"].ToString()) + "' \r\n  ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " and  BIPDevBHAvgEmp = '" + AvgEmp + "' \r\n ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " and ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";


                    }

                    if (WaterEndApplied1 == "Y")
                    {
                        _dbManStartRand.SqlStatement = "";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " Select isnull(max(BIPDevLateralAmount),0) StartRand \r\n  ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " from [tbl_BCS_BIPDevLateralWaterEnds] \r\n  ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " where  BIPDevLateralSQM <= '" + Convert.ToDecimal(_dbManWP1.ResultsDataTable.Rows[0]["TotSqm"].ToString()) + "' \r\n  ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " and  BIPDevLateralAvgEmp = '" + AvgEmp + "' \r\n ";
                        _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " and ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";

                    }


                }


                if (rdbDevRailCrew.Checked == true)
                {
                    decimal amount1 = Convert.ToDecimal(_dbManWP1.ResultsDataTable.Rows[0]["TotSqm"].ToString()) * Convert.ToDecimal(0.8);
                    _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " Select '" + Math.Round(amount1, 1) + "' StartRand \r\n  ";
                }

                if (rdbDevTransCrew.Checked == true)
                {

                    decimal amount1 = 0;

                    if (_dbManWP1.ResultsDataTable.Rows[0]["TotSqm"] != DBNull.Value)
                    {
                        if (Convert.ToInt32(ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue))) >= 201803)
                            amount1 = Convert.ToDecimal(_dbManWP1.ResultsDataTable.Rows[0]["TotSqm"].ToString()) * Convert.ToDecimal(1.0);
                        else
                            amount1 = Convert.ToDecimal(_dbManWP1.ResultsDataTable.Rows[0]["TotSqm"].ToString()) * Convert.ToDecimal(0.8);


                    }

                    _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " Select '" + Math.Round(amount1, 1) + "' StartRand \r\n  ";
                }


                if (rdbDevCleanCrew.Checked == true)
                {
                    decimal amount1 = 0;
                    if (_dbManWP1.ResultsDataTable.Rows.Count > 0)
                        amount1 = Convert.ToDecimal(_dbManWP1.ResultsDataTable.Rows[0]["TotSqm"].ToString()) * Convert.ToDecimal(0.8);
                    _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " Select '" + Math.Round(amount1, 1) + "' StartRand \r\n  ";
                }

                if (rdbDevConCrew.Checked == true)
                {
                    string test = _dbManWP1.ResultsDataTable.Rows[0]["TotSqm"].ToString();


                    decimal amount1 = Convert.ToDecimal(_dbManWP1.ResultsDataTable.Rows[0]["TotSqm"].ToString()) * Convert.ToDecimal(0.8);
                    _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " Select '" + Math.Round(amount1, 1) + "' StartRand \r\n  ";
                }

                if (rdbStoping.Checked == true)
                    _dbManStartRand.SqlStatement = _dbManStartRand.SqlStatement + " ) a \r\n ";

                _dbManStartRand.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManStartRand.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManStartRand.ResultsTableName = "StartRand";
                _dbManStartRand.ExecuteInstruction();

                DataTable dtStartRand = _dbManStartRand.ResultsDataTable;



                MWDataManager.clsDataAccess _dbManIncSweeps1 = new MWDataManager.clsDataAccess();
                _dbManIncSweeps1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManIncSweeps1.SqlStatement = _dbManIncSweeps1.SqlStatement + " select case when " + _dbManWP1.ResultsDataTable.Rows[0]["totsweeps"].ToString() + " = 0 then 0 else BIPStopingAmount end as BIPStopingAmount  from (Select isnull(max(BIPStopingAmount),0) BIPStopingAmount \r\n  ";
                if (_dbManWP1.ResultsDataTable.Rows[0]["reef"].ToString() == "Mer")
                    _dbManIncSweeps1.SqlStatement = _dbManIncSweeps1.SqlStatement + " from mineware.dbo.tbl_BCS_BIPStopingswmer \r\n  ";
                else
                    _dbManIncSweeps1.SqlStatement = _dbManIncSweeps1.SqlStatement + " from dbo.BMCS_BIPStopingsw \r\n  ";
                _dbManIncSweeps1.SqlStatement = _dbManIncSweeps1.SqlStatement + " where BIPStopingSQM <= " + _dbManWP1.ResultsDataTable.Rows[0]["totsweeps"].ToString() + " \r\n  ";
                _dbManIncSweeps1.SqlStatement = _dbManIncSweeps1.SqlStatement + " and BIPStopingAvgEmp = " + AvgEmp + " \r\n ";
                if (rdbZCrew.Checked == true)
                    _dbManIncSweeps1.SqlStatement = _dbManIncSweeps1.SqlStatement + " and ProdMonth = '209901'  \r\n ";

                if (rdbDev.Checked == true)
                    _dbManIncSweeps1.SqlStatement = _dbManIncSweeps1.SqlStatement + " and ProdMonth = '209901'  \r\n ";

                if (rdbDevCleanCrew.Checked == true)
                    _dbManIncSweeps1.SqlStatement = _dbManIncSweeps1.SqlStatement + " and ProdMonth = '209901'  \r\n ";

                if (rdbDevConCrew.Checked == true)
                    _dbManIncSweeps1.SqlStatement = _dbManIncSweeps1.SqlStatement + " and ProdMonth = '209901'  \r\n ";

                if (rdbDevRailCrew.Checked == true)
                    _dbManIncSweeps1.SqlStatement = _dbManIncSweeps1.SqlStatement + " and ProdMonth = '209901'  \r\n ";

                if (rdbDevTransCrew.Checked == true)
                    _dbManIncSweeps1.SqlStatement = _dbManIncSweeps1.SqlStatement + " and ProdMonth = '209901'  \r\n ";

                _dbManIncSweeps1.SqlStatement = _dbManIncSweeps1.SqlStatement + " and ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "')a  \r\n ";
                _dbManIncSweeps1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManIncSweeps1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManIncSweeps1.ResultsTableName = "Sweeps";
                _dbManIncSweeps1.ExecuteInstruction();

                ReportCrewBonus.Tables.Add(_dbManIncSweeps1.ResultsDataTable);


                decimal TotSqm = 0;
                if (_dbManWP1.ResultsDataTable.Rows[0]["TotSqm"] != DBNull.Value)
                    TotSqm = Convert.ToDecimal(_dbManWP1.ResultsDataTable.Rows[0]["TotSqm"].ToString());

                if (rdbStoping.Checked == true)
                    TotSqm = Math.Round(TotSqm, 0);

                if (rdbZCrew.Checked == true)
                    TotSqm = Math.Round(TotSqm, 2);

                if (rdbConstCrew.Checked == true)
                    TotSqm = Math.Round((TotSqm / AvgEmp * Convert.ToDecimal(5)) * Convert.ToDecimal(0.8), 2);



                Decimal TotA = (((TotSqm * HolesPerM) * RatesPerHole) / (RDOBonusShifts + Convert.ToDecimal(0.000001)));

                //MWDataManager.clsDataAccess _dbManData = new MWDataManager.clsDataAccess();
                //_dbManData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                //_dbManData.SqlStatement = _dbManData.SqlStatement + " select  '" + PossShifts + "' PossShifts, '" + AvgEmp + "' AvgEmp, \r\n ";
                //_dbManData.SqlStatement = _dbManData.SqlStatement + "  '" + SqmPerMan + "' SqmPerMan, '" + _dbManIncSweeps1.ResultsDataTable.Rows[0]["BIPStopingAmount"].ToString() + "' BIPStopingAmount, \r\n ";
                //_dbManData.SqlStatement = _dbManData.SqlStatement + " '" + HolesPerM + "' HolesPerM, '" + RatesPerHole + "' RatesPerHole, '" + MPerShift + "' MPerShift, " + AvgRDOs + " AvgRDOs, '" + _dbManStartRand.ResultsDataTable.Rows[0]["StartRand"].ToString() + "' StartRand, \r\n ";
                //_dbManData.SqlStatement = _dbManData.SqlStatement + " " + TotA + " TotA \r\n ";
                //_dbManData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                //_dbManData.queryReturnType = MWDataManager.ReturnType.DataTable;
                //_dbManData.ResultsTableName = "OtherData";
                //_dbManData.ExecuteInstruction();

                //ReportCrewBonus.Tables.Add(_dbManData.ResultsDataTable);

                //DSLti = 0;
                //NSLti = 0;
                //RDOLTI = 100;

                /////////////////RDO Safety Factor

                //////////////////////////////////// Safety Deductions/////////////////////////////

                MWDataManager.clsDataAccess _dbManSafety2 = new MWDataManager.clsDataAccess();
                _dbManSafety2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManSafety2.SqlStatement = " select case when b.tt = 1 then a.Cat_2_8_1st_LTI when b.tt = 2 then a.Cat_2_8_2nd_LTI when b.tt >= 3 then a.Cat_2_8_3rd_LTI else 1.2 end as SafetyFactor  \r\n  " +
                                          ", b.* from (  \r\n" +
                                          " select * from tbl_BCS_Stoping_LTI where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') a, " +

                                          "(Select " +
                                          " isnull(sum(ri)+sum(lti)+sum(fatal),0) tt, " +
                                          " case when sum(ri) > 0 then SUM(ri) else 0 end as ri, " +
                                          "  case when  sum(lti) > 0 then sum(lti) else 0 end as lti, " +
                                          "   case when sum(fatal) > 0 then sum(fatal) else 0 end as fatal,1 shift from tbl_BCS_SafetyCapture  " +
                                          "   where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  " +
                                          "   and orgunit in ('" + DsLbl.Text + "')  " +
                                          "   union  " +
                                          " Select  " +
                                          " isnull(sum(ri)+sum(lti)+sum(fatal),0) tt, " +
                                          "  case when sum(ri) > 0 then SUM(ri) else 0 end as ri,  " +
                                          " case when  sum(lti) > 0 then sum(lti) else 0 end as lti,  " +
                                          "  case when sum(fatal) > 0 then sum(fatal) else 0 end as fatal,2 shift from tbl_BCS_SafetyCapture  " +
                                          "  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'   " +
                                          "  and orgunit in  ('" + NSLbl.Text + "')) b " +
                                          "  order by shift ";
                _dbManSafety2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManSafety2.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManSafety2.ResultsTableName = "Safety";
                _dbManSafety2.ExecuteInstruction();


                DSLTI = Convert.ToInt32(_dbManSafety2.ResultsDataTable.Rows[0]["tt"].ToString());
                NSLTI = Convert.ToInt32(_dbManSafety2.ResultsDataTable.Rows[1]["tt"].ToString());

                DSLTIFACT = Convert.ToDecimal(_dbManSafety2.ResultsDataTable.Rows[0]["SafetyFactor"].ToString());
                NSLTIFACT = Convert.ToDecimal(_dbManSafety2.ResultsDataTable.Rows[1]["SafetyFactor"].ToString());


                MWDataManager.clsDataAccess _dbManWaterEnd1 = new MWDataManager.clsDataAccess();
                _dbManWaterEnd1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManWaterEnd1.SqlStatement = _dbManWaterEnd1.SqlStatement + "select * from dbo.tbl_BCS_OrgunitFactor\r\n ";
                _dbManWaterEnd1.SqlStatement = _dbManWaterEnd1.SqlStatement + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";
                _dbManWaterEnd1.SqlStatement = _dbManWaterEnd1.SqlStatement + " and orgunit = '" + DsLbl.Text + "' \r\n ";

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

                wwendfact = WaterEndFactor1;

                //string WaterEndApplied1 = "";

                //MWDataManager.clsDataAccess _dbManWaterA1 = new MWDataManager.clsDataAccess();
                //_dbManWaterA1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                //_dbManWaterA1.SqlStatement = _dbManWaterA1.SqlStatement + " select * from dbo.tbl_BCS_OrgunitWaterEnd \r\n ";
                //_dbManWaterA1.SqlStatement = _dbManWaterA1.SqlStatement + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";
                //_dbManWaterA1.SqlStatement = _dbManWaterA1.SqlStatement + " and orgunit = '" + DsLbl.Text + "' \r\n ";

                //_dbManWaterA1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                //_dbManWaterA1.queryReturnType = MWDataManager.ReturnType.DataTable;
                //_dbManWaterA1.ExecuteInstruction();

                //if (_dbManWaterA1.ResultsDataTable.Rows.Count > 0)
                //{
                //    WaterEndApplied1 = "Y";
                //}
                //else
                //{
                //    WaterEndApplied1 = "N";
                //}

                Decimal Inc = 0;

                if (_dbManIncSweeps1.ResultsDataTable.Rows.Count > 0)
                    Inc = Convert.ToDecimal(_dbManIncSweeps1.ResultsDataTable.Rows[0]["BIPStopingAmount"].ToString());
                if (_dbManStartRand.ResultsDataTable.Rows.Count > 0)
                    Inc = Inc + Convert.ToDecimal(_dbManStartRand.ResultsDataTable.Rows[0]["StartRand"].ToString());

                //if (rdbStoping.Checked == true)
                //   if  ( TotSqm == 0)
                //       Inc = Convert.ToDecimal(_dbManIncSweeps1.ResultsDataTable.Rows[0]["BIPStopingAmount"].ToString()); 


                Inc = Inc * WaterEndFactor1;

                //if (rdbConstCrew.Checked == true)
                //     Inc = TotSqm;


                StopingBIP = Inc;

                Decimal progpot = 0;
                Decimal nonepenaltyshifts = 0;

                Decimal nonepenaltyshiftsDS = 0;
                Decimal nonepenaltyshiftsNS = 0;


                Decimal progpotds = 0;
                Decimal progpotns = 0;


                // do grid
                for (row = 0; row <= DataGrid.RowCount - 1; row++)
                {
                    if (DataGrid.Rows[row].Cells[0].Value != null)
                    {
                        DataGrid.Rows[row].Cells[12].Value = Inc;


                        // addlti
                        DataGrid.Rows[row].Cells[16].Value = 0;
                        DataGrid.Rows[row].Cells[17].Value = 0;
                        DataGrid.Rows[row].Cells[18].Value = DSLTI + NSLTI;
                        DataGrid.Rows[row].Cells[19].Value = 0;

                        if (DataGrid.Rows[row].Cells[3].Value.ToString() == "0")
                        {
                            DataGrid.Rows[row].Cells[12].Value = Inc * Convert.ToDecimal(1.24);

                        }

                        if (DataGrid.Rows[row].Cells[3].Value.ToString() == "1")
                        {
                            DataGrid.Rows[row].Cells[12].Value = Inc * Convert.ToDecimal(1.24);

                        }

                        if (rdbConstCrew.Checked == true)
                        {
                            DataGrid.Rows[row].Cells[12].Value = Inc * Convert.ToDecimal(1.24);
                        }

                        if (DataGrid.Rows[row].Cells[7].Value.ToString() == "Team Leader")
                        {
                            DataGrid.Rows[row].Cells[12].Value = Inc * Convert.ToDecimal(TeamLeaderBonusLbl.Text);

                        }

                        if (DataGrid.Rows[row].Cells[7].Value.ToString() == "Machine Operator")
                        {
                            DataGrid.Rows[row].Cells[12].Value = 0;
                        }

                        if (DataGrid.Rows[row].Cells[49].Value.ToString() == "Winch Driver")
                        {
                            DataGrid.Rows[row].Cells[12].Value = Inc * Convert.ToDecimal(1.6);
                        }


                        if (DataGrid.Rows[row].Cells[49].Value.ToString() == "Jetting Gun Operator")
                        {
                            DataGrid.Rows[row].Cells[12].Value = Inc * Convert.ToDecimal(1.6);
                        }


                        if (DataGrid.Rows[row].Cells[49].Value.ToString() == "Loader Driver")
                        {
                            DataGrid.Rows[row].Cells[12].Value = Inc * Convert.ToDecimal(2);
                        }

                        DataGrid.Rows[row].Cells[12].Value = Convert.ToDecimal(DataGrid.Rows[row].Cells[12].Value.ToString()) / Convert.ToDecimal(DataGrid.Rows[row].Cells[26].Value) * Convert.ToDecimal(DataGrid.Rows[row].Cells[13].Value);


                        if (DataGrid.Rows[row].Cells[9].Value.ToString() == "D")
                            DataGrid.Rows[row].Cells[30].Value = Convert.ToDecimal(DataGrid.Rows[row].Cells[12].Value) * DSLTIFACT;
                        else
                            DataGrid.Rows[row].Cells[30].Value = Convert.ToDecimal(DataGrid.Rows[row].Cells[12].Value) * NSLTIFACT;

                        if (rdbZCrew.Checked == true)
                            DataGrid.Rows[row].Cells[30].Value = Convert.ToDecimal(DataGrid.Rows[row].Cells[30].Value);

                        if (rdbConstCrew.Checked == true)
                            DataGrid.Rows[row].Cells[30].Value = Convert.ToDecimal(DataGrid.Rows[row].Cells[30].Value);

                        if (rdbDevRailCrew.Checked == true)
                            DataGrid.Rows[row].Cells[30].Value = Convert.ToDecimal(DataGrid.Rows[row].Cells[30].Value);

                        if (rdbDevTransCrew.Checked == true)
                            DataGrid.Rows[row].Cells[30].Value = Convert.ToDecimal(DataGrid.Rows[row].Cells[30].Value);

                        if (rdbDevCleanCrew.Checked == true)
                            DataGrid.Rows[row].Cells[30].Value = Convert.ToDecimal(DataGrid.Rows[row].Cells[30].Value);

                        if (rdbDevConCrew.Checked == true)
                            DataGrid.Rows[row].Cells[30].Value = Convert.ToDecimal(DataGrid.Rows[row].Cells[30].Value);


                        //rdo
                        if (_dbManWP1.ResultsDataTable.Rows[0]["reef"].ToString() == "Mer")
                            DataGrid.Rows[row].Cells[28].Value = (Convert.ToDecimal(_dbManWP1.ResultsDataTable.Rows[0]["TotSqm"].ToString()) * Convert.ToDecimal(HolesPerM) * Convert.ToDecimal(RatesPerHole)) / ((Convert.ToDecimal(Math.Round(RDOBonusShifts, 3)) + Convert.ToDecimal(0.0000001))) * (Convert.ToDecimal(DataGrid.Rows[row].Cells[13].Value)) * DSLTIFACT;
                        else
                            DataGrid.Rows[row].Cells[28].Value = (Convert.ToDecimal(_dbManWP1.ResultsDataTable.Rows[0]["TotSqm"].ToString()) * Convert.ToDecimal(HolesPerM) * Convert.ToDecimal(RatesPerHole)) / ((Convert.ToDecimal(Math.Round(RDOBonusShifts, 3)) + Convert.ToDecimal(0.0000001))) * (Convert.ToDecimal(DataGrid.Rows[row].Cells[13].Value)) * DSLTIFACT;

                        if (rdbStoping.Checked == true)
                        {
                            if (_dbManWP1.ResultsDataTable.Rows[0]["reef"].ToString() == "Mer")
                                DataGrid.Rows[row].Cells[28].Value = (((Convert.ToDecimal(_dbManWP1.ResultsDataTable.Rows[0]["TotSqm"].ToString()) * Convert.ToDecimal(HolesPerM) * Convert.ToDecimal(RatesPerHole)) / ((Convert.ToDecimal(Math.Round(RDOBonusShifts, 3))) + Convert.ToDecimal(0.0000001)))) * (Convert.ToDecimal(DataGrid.Rows[row].Cells[13].Value)) * DSLTIFACT;
                            else
                                DataGrid.Rows[row].Cells[28].Value = (Convert.ToDecimal(_dbManWP1.ResultsDataTable.Rows[0]["TotSqm"].ToString()) * Convert.ToDecimal(HolesPerM) * Convert.ToDecimal(RatesPerHole)) / ((Convert.ToDecimal(Math.Round(RDOBonusShifts, 3)) + Convert.ToDecimal(0.0000001))) * (Convert.ToDecimal(DataGrid.Rows[row].Cells[13].Value)) * DSLTIFACT;

                        }


                        DataGrid.Rows[row].Cells[29].Value = Convert.ToDecimal(Inc) * Convert.ToDecimal(2) * DSLTIFACT * (Convert.ToDecimal(DataGrid.Rows[row].Cells[13].Value) / Convert.ToDecimal(DataGrid.Rows[row].Cells[26].Value));

                        if (DataGrid.Rows[row].Cells[7].Value.ToString() != "Machine Operator")
                        {
                            DataGrid.Rows[row].Cells[24].Value = 0;
                            DataGrid.Rows[row].Cells[25].Value = 0;
                            DataGrid.Rows[row].Cells[28].Value = 0;
                            DataGrid.Rows[row].Cells[29].Value = 0;
                        }
                        else
                        {
                            decimal a = Convert.ToDecimal(_dbManWP1.ResultsDataTable.Rows[0]["TotSqm"].ToString());
                            decimal b = (Convert.ToDecimal(DataGrid.Rows[row].Cells[26].Value) * MPerShift);
                            if (a >= b)
                            {
                            }
                            else
                            {
                                if (WaterEndApplied1 != "Y")
                                    DataGrid.Rows[row].Cells[29].Value = 0;
                            }

                            DataGrid.Rows[row].Cells[24].Value = Convert.ToDecimal(DataGrid.Rows[row].Cells[28].Value) / (DSLTIFACT + Convert.ToDecimal(0.0000000011));
                            DataGrid.Rows[row].Cells[25].Value = Convert.ToDecimal(DataGrid.Rows[row].Cells[29].Value) / (DSLTIFACT + Convert.ToDecimal(0.0000000011));
                            DataGrid.Rows[row].Cells[24].Value = Math.Round(Convert.ToDecimal(DataGrid.Rows[row].Cells[24].Value), 2);
                            DataGrid.Rows[row].Cells[25].Value = Math.Round(Convert.ToDecimal(DataGrid.Rows[row].Cells[25].Value), 2);

                        }



                        // do round 
                        DataGrid.Rows[row].Cells[12].Value = Math.Round(Convert.ToDecimal(DataGrid.Rows[row].Cells[12].Value), 2);
                        DataGrid.Rows[row].Cells[28].Value = Math.Round(Convert.ToDecimal(DataGrid.Rows[row].Cells[28].Value), 2);
                        DataGrid.Rows[row].Cells[29].Value = Math.Round(Convert.ToDecimal(DataGrid.Rows[row].Cells[29].Value), 2);
                        DataGrid.Rows[row].Cells[30].Value = Math.Round(Convert.ToDecimal(DataGrid.Rows[row].Cells[30].Value), 2);


                        if (DataGrid.Rows[row].Cells[9].Value.ToString() == "D")
                            DataGrid.Rows[row].Cells[40].Value = (Convert.ToDecimal(DataGrid.Rows[row].Cells[12].Value) + Convert.ToDecimal(DataGrid.Rows[row].Cells[28].Value) + Convert.ToDecimal(DataGrid.Rows[row].Cells[29].Value)) * DSLTIFACT;
                        else
                            DataGrid.Rows[row].Cells[40].Value = (Convert.ToDecimal(DataGrid.Rows[row].Cells[12].Value) + Convert.ToDecimal(DataGrid.Rows[row].Cells[28].Value) + Convert.ToDecimal(DataGrid.Rows[row].Cells[29].Value)) * NSLTIFACT;


                        //pot
                        DataGrid.Rows[row].Cells[35].Value = (Convert.ToDecimal(DataGrid.Rows[row].Cells[28].Value) + Convert.ToDecimal(DataGrid.Rows[row].Cells[29].Value) + Convert.ToDecimal(DataGrid.Rows[row].Cells[30].Value)) * (Convert.ToDecimal(DataGrid.Rows[row].Cells[33].Value) / 100);
                        DataGrid.Rows[row].Cells[35].Value = Math.Round(Convert.ToDecimal(DataGrid.Rows[row].Cells[35].Value), 2);

                        DataGrid.Rows[row].Cells[44].Value = DataGrid.Rows[row].Cells[35].Value;


                        progpot = progpot + Convert.ToDecimal(DataGrid.Rows[row].Cells[35].Value);
                        if (DataGrid.Rows[row].Cells[9].Value.ToString() == "D")
                            progpotds = progpotds + Convert.ToDecimal(DataGrid.Rows[row].Cells[35].Value);
                        else
                            progpotns = progpotns + Convert.ToDecimal(DataGrid.Rows[row].Cells[35].Value);


                        //Bonus Shifts without penalty
                        if (Convert.ToDecimal(DataGrid.Rows[row].Cells[27].Value) + Convert.ToDecimal(DataGrid.Rows[row].Cells[14].Value) == 0)
                            nonepenaltyshifts = nonepenaltyshifts + Convert.ToDecimal(DataGrid.Rows[row].Cells[13].Value);

                        if (DataGrid.Rows[row].Cells[9].Value.ToString() == "D")
                        {
                            if (Convert.ToDecimal(DataGrid.Rows[row].Cells[27].Value) + Convert.ToDecimal(DataGrid.Rows[row].Cells[14].Value) == 0)
                                if (Convert.ToDecimal(DataGrid.Rows[row].Cells[12].Value.ToString()) + Convert.ToDecimal(DataGrid.Rows[row].Cells[24].Value.ToString()) > 0)
                                    nonepenaltyshiftsDS = nonepenaltyshiftsDS + Convert.ToDecimal(DataGrid.Rows[row].Cells[13].Value);

                        }
                        else
                        {
                            if (Convert.ToDecimal(DataGrid.Rows[row].Cells[27].Value) + Convert.ToDecimal(DataGrid.Rows[row].Cells[14].Value) == 0)
                                nonepenaltyshiftsNS = nonepenaltyshiftsNS + Convert.ToDecimal(DataGrid.Rows[row].Cells[13].Value);

                        }
                    }
                }

                MWDataManager.clsDataAccess _dbManPage2Data = new MWDataManager.clsDataAccess();
                _dbManPage2Data.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);


                _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " select '' shift , '' Category,  '' IndustryNumber,  '' Init, '' Surname, '' WorkingShifts, ''AWOPS, '' Sick, '' TotShifts, '' Prorate, '' rdohole, ''rdoinc \r\n ";
                _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " , '0' ordera, '' TotProrata, '' LessDed ";
                _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " , '' awop, '' sick, '' TotDed, '' Pot, '' FinalPayment, '' Potfim, '' potadd, '' Designation ";

                decimal pot = 0;

                decimal pot1 = 0;
                decimal pot2 = 0;

                decimal lessdec = 0;

                TotalPayment = 0;

                if (nonepenaltyshifts < PossShifts)
                    nonepenaltyshifts = PossShifts;

                if (nonepenaltyshiftsDS < PossShifts)
                    nonepenaltyshiftsDS = PossShifts;

                if (nonepenaltyshiftsNS < PossShifts)
                    nonepenaltyshiftsNS = PossShifts;

                if (ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) == "201608")
                {
                    progpot = 0;
                    progpotds = 0;
                    progpotns = 0;

                }

                for (row = 0; row <= DataGrid.RowCount - 1; row++)
                {
                    if (DataGrid.Rows[row].Cells[0].Value != null)
                    {
                        if (Convert.ToDecimal(DataGrid.Rows[row].Cells[35].Value) == Convert.ToDecimal(0))
                        {
                            DataGrid.Rows[row].Cells[36].Value = 0;

                            if (Convert.ToDecimal(DataGrid.Rows[row].Cells[12].Value.ToString()) + Convert.ToDecimal(DataGrid.Rows[row].Cells[24].Value.ToString()) > 0)
                                DataGrid.Rows[row].Cells[36].Value = progpot / (nonepenaltyshifts + Convert.ToDecimal(0.0000000001)) * Convert.ToDecimal(DataGrid.Rows[row].Cells[13].Value);
                            if (DataGrid.Rows[row].Cells[9].Value.ToString() == "D")
                            {
                                if (Convert.ToDecimal(DataGrid.Rows[row].Cells[12].Value.ToString()) + Convert.ToDecimal(DataGrid.Rows[row].Cells[24].Value.ToString()) > 0)
                                    DataGrid.Rows[row].Cells[36].Value = progpotds / (nonepenaltyshiftsDS + Convert.ToDecimal(0.0000000001)) * Convert.ToDecimal(DataGrid.Rows[row].Cells[13].Value);
                            }
                            else
                                DataGrid.Rows[row].Cells[36].Value = Convert.ToDecimal(progpotns) / (Convert.ToDecimal(nonepenaltyshiftsNS) + Convert.ToDecimal(0.0000000001)) * Convert.ToDecimal(DataGrid.Rows[row].Cells[13].Value);
                        }
                        else
                            DataGrid.Rows[row].Cells[36].Value = 0;

                        DataGrid.Rows[row].Cells[45].Value = DataGrid.Rows[row].Cells[36].Value;

                        DataGrid.Rows[row].Cells[36].Value = Math.Round(Convert.ToDecimal(DataGrid.Rows[row].Cells[36].Value), 2);


                        //finally payment
                        DataGrid.Rows[row].Cells[20].Value = (Convert.ToDecimal(DataGrid.Rows[row].Cells[28].Value) + Convert.ToDecimal(DataGrid.Rows[row].Cells[29].Value) + Convert.ToDecimal(DataGrid.Rows[row].Cells[30].Value)) - Convert.ToDecimal(DataGrid.Rows[row].Cells[35].Value) + Convert.ToDecimal(DataGrid.Rows[row].Cells[36].Value);

                        // do rdo
                        if (DataGrid.Rows[row].Cells[7].Value.ToString() == "Machine Operator")
                        {

                            DataGrid.Rows[row].Cells[47].Value = Convert.ToDecimal(DataGrid.Rows[row].Cells[20].Value);
                            DataGrid.Rows[row].Cells[48].Value = Convert.ToDecimal(DataGrid.Rows[row].Cells[28].Value) + Convert.ToDecimal(DataGrid.Rows[row].Cells[29].Value);

                            DataGrid.Rows[row].Cells[24].Value = 0;

                            if (Convert.ToDecimal(DataGrid.Rows[row].Cells[48].Value) > 0)
                            {
                                DataGrid.Rows[row].Cells[24].Value = (Convert.ToDecimal(DataGrid.Rows[row].Cells[28].Value) * (Convert.ToDecimal(DataGrid.Rows[row].Cells[47].Value) / Convert.ToDecimal(DataGrid.Rows[row].Cells[48].Value)));

                            }


                            DataGrid.Rows[row].Cells[25].Value = 0;

                            if (Convert.ToDecimal(DataGrid.Rows[row].Cells[48].Value) > 0)
                            {
                                DataGrid.Rows[row].Cells[25].Value = (Convert.ToDecimal(DataGrid.Rows[row].Cells[29].Value) * (Convert.ToDecimal(DataGrid.Rows[row].Cells[47].Value) / Convert.ToDecimal(DataGrid.Rows[row].Cells[48].Value)));

                            }

                            DataGrid.Rows[row].Cells[24].Value = Math.Round(Convert.ToDecimal(DataGrid.Rows[row].Cells[24].Value), 2);
                            DataGrid.Rows[row].Cells[25].Value = Math.Round(Convert.ToDecimal(DataGrid.Rows[row].Cells[25].Value), 2);


                            // DataGrid.Rows[row].Cells[47].Value = (Convert.ToDecimal(DataGrid.Rows[row].Cells[28].Value));
                            //  DataGrid.Rows[row].Cells[48].Value = (Convert.ToDecimal(DataGrid.Rows[row].Cells[29].Value));

                            //   if (Convert.ToDecimal(DataGrid.Rows[row].Cells[28].Value) + Convert.ToDecimal(DataGrid.Rows[row].Cells[29].Value) > 0)
                            //   {
                            //      DataGrid.Rows[row].Cells[42].Value = Convert.ToDecimal(DataGrid.Rows[row].Cells[30].Value);

                            //  }
                        }

                        pot = (Convert.ToDecimal(DataGrid.Rows[row].Cells[28].Value) + Convert.ToDecimal(DataGrid.Rows[row].Cells[29].Value) + Convert.ToDecimal(DataGrid.Rows[row].Cells[30].Value));


                        pot1 = -Convert.ToDecimal(DataGrid.Rows[row].Cells[35].Value);
                        pot2 = Convert.ToDecimal(DataGrid.Rows[row].Cells[36].Value);

                        TotalPayment = TotalPayment + Convert.ToDecimal(DataGrid.Rows[row].Cells[20].Value);

                        _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " union select '" + DataGrid.Rows[row].Cells[9].Value + "' shift , '" + DataGrid.Rows[row].Cells[7].Value + "' Category, ";
                        _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " '" + DataGrid.Rows[row].Cells[4].Value + "' IndustryNumber,  '" + DataGrid.Rows[row].Cells[5].Value + "' Init, '" + DataGrid.Rows[row].Cells[6].Value + "' Surname, ";
                        _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " '" + DataGrid.Rows[row].Cells[13].Value + "' WorkingShifts, '" + DataGrid.Rows[row].Cells[14].Value + "' AWOPS, '" + DataGrid.Rows[row].Cells[27].Value + "' Sick \r\n ";
                        _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " ,'" + DataGrid.Rows[row].Cells[26].Value + "' TotShifts,'" + DataGrid.Rows[row].Cells[12].Value + "' Prorate,'" + DataGrid.Rows[row].Cells[28].Value + "' rdohole,'" + DataGrid.Rows[row].Cells[29].Value + "' rdoinc \r\n ";


                        _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " , '" + DataGrid.Rows[row].Cells[34].Value + "' ordera, '" + DataGrid.Rows[row].Cells[12].Value + DataGrid.Rows[row].Cells[28].Value + DataGrid.Rows[row].Cells[29].Value + "' TotProrata, '" + pot + "'  LessDed  \r\n ";
                        _dbManPage2Data.SqlStatement = _dbManPage2Data.SqlStatement + " , '" + DataGrid.Rows[row].Cells[31].Value + "' awop, '" + DataGrid.Rows[row].Cells[32].Value + "' sick, '" + DataGrid.Rows[row].Cells[33].Value + "' TotDed, '" + DataGrid.Rows[row].Cells[14].Value + "' Pot, '" + DataGrid.Rows[row].Cells[20].Value + "' FinalPayment, '" + pot1 + "' PotFinal, '" + pot2 + "' Potadd,  '" + DataGrid.Rows[row].Cells[49].Value + "' desig  \r\n ";


                    }
                }
                _dbManPage2Data.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManPage2Data.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManPage2Data.ResultsTableName = "Page2Data";
                _dbManPage2Data.ExecuteInstruction();

                ReportCrewBonus.Tables.Add(_dbManPage2Data.ResultsDataTable);
                bip = 0;
                if (_dbManStartRand.ResultsDataTable.Rows.Count > 0)
                    bip = Convert.ToDecimal(_dbManIncSweeps1.ResultsDataTable.Rows[0]["BIPStopingAmount"].ToString()) + Convert.ToDecimal(_dbManStartRand.ResultsDataTable.Rows[0]["StartRand"].ToString());


                decimal ssss = 0;

                if (_dbManStartRand.ResultsDataTable.Rows.Count > 0)
                    ssss = Convert.ToDecimal(_dbManStartRand.ResultsDataTable.Rows[0]["StartRand"].ToString());

                MWDataManager.clsDataAccess _dbManData = new MWDataManager.clsDataAccess();
                _dbManData.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManData.SqlStatement = _dbManData.SqlStatement + " select  '" + PossShifts + "' PossShifts, '" + AvgEmp + "' AvgEmp, \r\n ";
                _dbManData.SqlStatement = _dbManData.SqlStatement + "  '" + SqmPerMan + "' SqmPerMan, '" + _dbManIncSweeps1.ResultsDataTable.Rows[0]["BIPStopingAmount"].ToString() + "' BIPStopingAmount, \r\n ";
                _dbManData.SqlStatement = _dbManData.SqlStatement + " '" + HolesPerM + "' HolesPerM, '" + RatesPerHole + "' RatesPerHole, '" + MPerShift + "' MPerShift, " + Math.Round(AvgRDOs, 4) + " AvgRDOs, '" + ssss * WaterEndFactor1 + "' StartRand, \r\n ";
                _dbManData.SqlStatement = _dbManData.SqlStatement + " " + TotA + " TotA, '" + progpot + "' totpot, '" + nonepenaltyshifts + "' nonepenaltyshifts, '" + progpot / (nonepenaltyshifts + Convert.ToDecimal(0.0000000001)) + "' potpershift,   " + DSLTI + " dslti,  " + DSLTIFACT + " dsltifact,   " + NSLTI + " nslti,  " + NSLTIFACT + " nsltifact, " + WaterEndFactor1 + " WaterEndFactor1, " + WaterEndMeters1 + " WaterEndMeters1, '" + WaterEndApplied1 + "' WaterEndApplied1 \r\n ";
                _dbManData.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManData.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManData.ResultsTableName = "OtherData";
                _dbManData.ExecuteInstruction();

                ReportCrewBonus.Tables.Add(_dbManData.ResultsDataTable);

            }

            report.RegisterData(ReportCrewBonus);

            report.Load(_reportFolder +"CewBonusNew.frx");

            //report.Design();

            pcReport.Clear();
            report.Prepare();
            report.Preview = pcReport;
            report.ShowPrepared();

            


           
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OnCloseTabRequest(new CloseTabArg(tabCaption));
        }

        private void btnTransfer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (rdbXCCrew.Checked == true)
            {
                MessageBox.Show("This Orgunit is unable to transfer", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }



            result = MessageBox.Show("Are you sure you want to transfer the Bonus Details to the ARMS Interface?", "Transfer Record", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {

                if ("" == "")
                {

                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " select * from Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew  \r\n  ";
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " Where OrgUnit = '" + DsLbl.Text + "'  \r\n ";
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
                    _dbMandelete.SqlStatement = _dbMandelete.SqlStatement + " Delete Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew ";
                    _dbMandelete.SqlStatement = _dbMandelete.SqlStatement + " Where OrgUnit = '" + DsLbl.Text + "' ";
                    _dbMandelete.SqlStatement = _dbMandelete.SqlStatement + " and ProdMonth =  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  \r\n ";


                    _dbMandelete.SqlStatement = _dbMandelete.SqlStatement + " Delete Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew ";
                    _dbMandelete.SqlStatement = _dbMandelete.SqlStatement + " Where OrgUnit = '" + NSLbl.Text + "' ";
                    _dbMandelete.SqlStatement = _dbMandelete.SqlStatement + " and ProdMonth =  '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";

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
                                _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " Insert into Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew  values (  \r\n ";
                                _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " '" + DataGrid.Rows[row].Cells[0].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[1].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[2].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[3].Value.ToString() + "', ";
                                _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " '" + DataGrid.Rows[row].Cells[4].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[5].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[6].Value.ToString() + "', ";

                                _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " '" + DataGrid.Rows[row].Cells[7].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[8].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[9].Value.ToString() + "', ";
                                _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " '" + DataGrid.Rows[row].Cells[10].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[11].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[12].Value.ToString() + "', ";
                                _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " '" + DataGrid.Rows[row].Cells[13].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[14].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[15].Value.ToString() + "', ";
                                _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " '" + DataGrid.Rows[row].Cells[16].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[17].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[18].Value.ToString() + "', ";
                                _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " '" + DataGrid.Rows[row].Cells[19].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[20].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[21].Value.ToString() + "', ";
                                _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " '', null, '" + DataGrid.Rows[row].Cells[24].Value.ToString() + "', '" + DataGrid.Rows[row].Cells[25].Value.ToString() + "'";

                                _dbManInsert.SqlStatement = _dbManInsert.SqlStatement + " )  \r\n ";


                            }
                        }

                    }

                    _dbManInsert.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManInsert.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManInsert.ResultsTableName = "Transfer";
                    _dbManInsert.ExecuteInstruction();


                    // do zcrews
                    if (rdbStoping.Checked == true)
                    {
                        MWDataManager.clsDataAccess _dbManInsertZ = new MWDataManager.clsDataAccess();
                        _dbManInsertZ.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _dbManInsertZ.SqlStatement = _dbManInsertZ.SqlStatement + " delete from  Mineware.dbo.[BMCS_ZGangAverageNew]  where prodmonth = '" + DataGrid.Rows[0].Cells[2].Value.ToString() + "'   and  orgunitds = '" + DataGrid.Rows[0].Cells[8].Value.ToString() + "'  \r\n ";
                        _dbManInsertZ.SqlStatement = _dbManInsertZ.SqlStatement + " insert into  Mineware.dbo.[BMCS_ZGangAverageNew] values ('" + DataGrid.Rows[0].Cells[2].Value.ToString() + "', '" + DataGrid.Rows[0].Cells[8].Value.ToString() + "', '" + DataGrid.Rows[0].Cells[10].Value.ToString() + "', '" + StopingBIP + "', '" + DataGrid.Rows[0].Cells[26].Value.ToString() + "', '1') \r\n ";
                        _dbManInsertZ.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbManInsertZ.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dbManInsertZ.ResultsTableName = "Transfer1";
                        _dbManInsertZ.ExecuteInstruction();

                        decimal ltitot = DSLTI + NSLTI;
                        decimal ltifacttot = DSLTIFACT + NSLTIFACT;

                        _dbManInsertZ.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _dbManInsertZ.SqlStatement = " ";
                        _dbManInsertZ.SqlStatement = _dbManInsertZ.SqlStatement + " delete from Mineware.dbo.[BMCS_StopingRepNew] where prodmonth = '" + DataGrid.Rows[0].Cells[2].Value.ToString() + "' and orgunit = '" + DataGrid.Rows[0].Cells[8].Value.ToString() + "'  \r\n ";
                        _dbManInsertZ.SqlStatement = _dbManInsertZ.SqlStatement + " INSERT INTO Mineware.dbo.[BMCS_StopingRepNew] VALUES (1, '" + DataGrid.Rows[0].Cells[2].Value.ToString() + "', '" + DataGrid.Rows[0].Cells[8].Value.ToString() + "',  \r\n ";
                        _dbManInsertZ.SqlStatement = _dbManInsertZ.SqlStatement + " '" + DataGrid.Rows[0].Cells[10].Value.ToString() + "','" + DataGrid.Rows[0].Cells[11].Value.ToString() + "',   \r\n ";
                        _dbManInsertZ.SqlStatement = _dbManInsertZ.SqlStatement + " '" + AvgEmp + "', '" + wastesqmexp + "', '" + totsqmexp + "', '" + bip + "',  \r\n ";
                        _dbManInsertZ.SqlStatement = _dbManInsertZ.SqlStatement + " '" + ltitot + "','" + ltifacttot + "', '" + Fatal + "',  \r\n ";
                        _dbManInsertZ.SqlStatement = _dbManInsertZ.SqlStatement + "  '" + TotalPayment + "', 'S', 'D', '" + PossShifts + "', '0', '" + totswpexp + "',  \r\n ";
                        _dbManInsertZ.SqlStatement = _dbManInsertZ.SqlStatement + "  '" + RI + "', '" + DSLTI + "','" + NSLTI + "')  \r\n ";
                        _dbManInsertZ.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbManInsertZ.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dbManInsertZ.ResultsTableName = "Transfer2";
                        _dbManInsertZ.ExecuteInstruction();




                    }

                    if (rdbDev.Checked == true)
                    {
                        decimal ltitot = DSLTI + NSLTI;
                        decimal ltifacttot = DSLTIFACT + NSLTIFACT;

                        MWDataManager.clsDataAccess _dbManInsertZ = new MWDataManager.clsDataAccess();
                        _dbManInsertZ.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                        _dbManInsertZ.SqlStatement = " ";

                        _dbManInsertZ.SqlStatement = _dbManInsertZ.SqlStatement + " delete from Mineware.dbo.[tbl_BCS_DevRepNew] where prodmonth = '" + DataGrid.Rows[0].Cells[2].Value.ToString() + "' and orgunit = '" + DataGrid.Rows[0].Cells[8].Value.ToString() + "'  \r\n ";
                        _dbManInsertZ.SqlStatement = _dbManInsertZ.SqlStatement + " INSERT INTO Mineware.dbo.[tbl_BCS_DevRepNew] VALUES ( '" + DataGrid.Rows[0].Cells[2].Value.ToString() + "', '" + DataGrid.Rows[0].Cells[8].Value.ToString() + "',  \r\n ";
                        _dbManInsertZ.SqlStatement = _dbManInsertZ.SqlStatement + " '" + DataGrid.Rows[0].Cells[10].Value.ToString() + "',  '" + totmexp + "' ,  \r\n ";
                        _dbManInsertZ.SqlStatement = _dbManInsertZ.SqlStatement + " '" + AvgEmp + "',  '" + bip * wwendfact + "',  \r\n ";
                        _dbManInsertZ.SqlStatement = _dbManInsertZ.SqlStatement + " '" + ltitot + "','" + ltifacttot + "', '" + Fatal + "',  \r\n ";
                        _dbManInsertZ.SqlStatement = _dbManInsertZ.SqlStatement + "  '" + TotalPayment + "', 'D', 'D', '" + PossShifts + "', '0', 0,  \r\n ";
                        _dbManInsertZ.SqlStatement = _dbManInsertZ.SqlStatement + "   '" + DSLTI + "','" + NSLTI + "')  \r\n ";
                        _dbManInsertZ.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                        _dbManInsertZ.queryReturnType = MWDataManager.ReturnType.DataTable;
                        _dbManInsertZ.ResultsTableName = "Transfer2";
                        _dbManInsertZ.ExecuteInstruction();



                    }

                }


                MessageBox.Show("Org Unit Transfered", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListBoxes();
            }


            
        }

        private void editProdmonth_EditValueChanged(object sender, EventArgs e)
        {
            LoadListBoxes();


            MWDataManager.clsDataAccess _dbManSystemSettings = new MWDataManager.clsDataAccess();
            _dbManSystemSettings.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManSystemSettings.SqlStatement = " select * from tbl_BCS_SystemSettings where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";
            _dbManSystemSettings.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManSystemSettings.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManSystemSettings.ResultsTableName = "Stoping";
            _dbManSystemSettings.ExecuteInstruction();

            if (_dbManSystemSettings.ResultsDataTable.Rows.Count < 1)
            {
                return;
            }

            MerBonusLbl.Text = _dbManSystemSettings.ResultsDataTable.Rows[0]["merenskybonusincreasepercent"].ToString();
            TeamLeaderBonusLbl.Text = _dbManSystemSettings.ResultsDataTable.Rows[0]["specialteamleaderproratastoping"].ToString();
            DrillOpBonusLbl.Text = _dbManSystemSettings.ResultsDataTable.Rows[0]["machinedrilloperatorproratastoping"].ToString();
            AbscentBonusLbl.Text = _dbManSystemSettings.ResultsDataTable.Rows[0]["absentee"].ToString();


            
        }
    }
}
