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
using Mineware.Systems.Global;
using Mineware.Systems.ProductionAmplatsGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.ProductionAmplatsBonus
{
    public partial class ucAuditReports : BaseUserControl
    {
        public ucAuditReports()
        {
            InitializeComponent();
            FormRibbonPages.Add(rpReports);
            FormActiveRibbonPage = rpReports;
            FormMainRibbonPage = rpReports;
            RibbonControl = rcReports;
        }

        Report theReport = new Report();
        Procedures proc = new Procedures();
        DataTable dtUsers;

        private void LoadUsers()
        {
            

            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            if (lblReportType.Text == "Gangs Removed") //do gangs queries - column naming different than factor tables
            {
                _dbMan1.SqlStatement = " select a.SystemUser UserID,b.Name from \r\n " +
                                       " tbl_BCS_GangsAudit_Removed a, dbo.BMCS_Users b \r\n " +
                                       " where a.prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n " +
                                       " and a.SystemUser = b.UserID  \r\n " +
                                       " group by a.SystemUser, b.Name \r\n " +
                                       " order by a.SystemUser ";
            }
            else // do factors queries
            {

                _dbMan1.SqlStatement = " select  a.SystemUser UserID,b.Name  from \r\n ";
                if (lblReportType.Text == "Engineering Production")
                {
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " BMCS_Audit_General_Factors a, dbo.BMCS_Users b \r\n ";
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " where a.prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";
                }
                if (lblReportType.Text == "Engineering Workshops")
                {
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " BMCS_Audit_Workshop_Factors a, dbo.BMCS_Users b \r\n ";
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " where a.prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";
                }
                if (lblReportType.Text == "TSD Vent")
                {
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " BMCS_Audit_TSDVent_Factors a, dbo.BMCS_Users b \r\n ";
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " where a.prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";
                }
                if (lblReportType.Text == "Engineering Shafts")
                {
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " BMCS_Audit_Shaft_Factors a, dbo.BMCS_Users b \r\n ";
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " where a.prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";
                }
                if (lblReportType.Text == "Production")
                {
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " BMCS_Audit_SystemSettings a, dbo.BMCS_Users b \r\n ";
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " where a.prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";
                }
                if (lblReportType.Text == "Plant")
                {
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " BMCS_Audit_Eng_PlantParameters a, dbo.BMCS_Users b \r\n ";
                    _dbMan1.SqlStatement = _dbMan1.SqlStatement + " where a.yearmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n ";
                }


                _dbMan1.SqlStatement = _dbMan1.SqlStatement + " and a.UserID = b.UserID  \r\n " +
                " group by a.UserID, b.Name \r\n " +
                " order by a.UserID ";
            }

            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();

            if (_dbMan1.ResultsDataTable.Rows.Count < 1)
            {
                MessageBox.Show("There are no records saved for report : " + lblReportType.Text + "  For month : " + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)));
                return;
            }

            dtUsers = _dbMan1.ResultsDataTable;

            LookUpEditUsers.DataSource = dtUsers;
            LookUpEditUsers.ValueMember = "UserID";
            LookUpEditUsers.DisplayMember = "Name";


            //Set visibility
            if ((lblReportType.Text == "Engineering Production") || (lblReportType.Text == "Plant"))
                editUsers.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            else
                editUsers.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

        }

        private void frmAuditReports_Load(object sender, EventArgs e)
        {
            editProdmonth.EditValue = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsDate(ProductionAmplatsGlobalTSysSettings._currentProductionMonth.ToString());

            LoadUsers();

            if (lblReportType.Text == "Gangs Removed")
            {
                editProdmonth.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
             

                lblDate.Visible = true;
                theDate.Visible = true;

               
            }
            else
            {
                
            }
        }

        private void Close1Btn_Click(object sender, EventArgs e)
        {
            
        }

        private void showBtn_Click(object sender, EventArgs e)
        {
            

        }

      

        private void ProdMonth1Txt_TextChanged(object sender, EventArgs e)
        {
            //LoadUsers();
        }

        private void btnShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //First check if there are any records
            if (dtUsers.Rows.Count < 1)
            {
                MessageBox.Show("There are no records saved for report : " + lblReportType.Text + "  For month : " + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)));
                return;
            }

            //
            //Load Engineering Production
            //
            if (lblReportType.Text == "Engineering Production")
            {
                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = " select a.*, b.Name from BMCS_Audit_General_Factors a, dbo.BMCS_Users b \r\n " +
                                         " where a.prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and a.UserID = '" + editUsers.EditValue + "' \r\n " +
                                         " and a.UserID = b.UserID \r\n " +
                                         " order by a.userid, a.calendardate ";

                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ResultsTableName = "EngProd";
                _dbMan1.ExecuteInstruction();

                DataTable dt1 = _dbMan1.ResultsDataTable;

                DataSet ds1 = new DataSet();
                ds1.Tables.Add(dt1);

                theReport.RegisterData(ds1);

                theReport.Load("ProdEngAudit.frx");

            }

            //
            //Load Engineering Workshops
            //
            if (lblReportType.Text == "Engineering Workshops")
            {
                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = " select a.*, b.Name from BMCS_Audit_Workshop_Factors a, dbo.BMCS_Users b \r\n " +
                                         " where a.prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  \r\n " +
                                         " and a.UserID = b.UserID \r\n " +
                                         " order by a.userid, a.calendardate ";

                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ResultsTableName = "EngWS";
                _dbMan1.ExecuteInstruction();

                DataTable dt1 = _dbMan1.ResultsDataTable;

                DataSet ds1 = new DataSet();
                ds1.Tables.Add(dt1);

                theReport.RegisterData(ds1);

                theReport.Load("EngWorkshopAudit.frx");

            }

            //
            //Load Engineering TSD Ventilation
            //
            if (lblReportType.Text == "TSD Vent")
            {
                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = " select a.*, b.Name from BMCS_Audit_TSDVent_Factors a, dbo.BMCS_Users b \r\n " +
                                         " where a.prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  \r\n " +
                                         " and a.UserID = b.UserID \r\n " +
                                         " order by a.userid, a.calendardate ";

                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ResultsTableName = "TSD";
                _dbMan1.ExecuteInstruction();

                DataTable dt1 = _dbMan1.ResultsDataTable;

                DataSet ds1 = new DataSet();
                ds1.Tables.Add(dt1);

                theReport.RegisterData(ds1);

                theReport.Load("EngTSDAudit.frx");

            }

            //
            //Load Engineering Shafts
            //
            if (lblReportType.Text == "Engineering Shafts")
            {
                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = " select a.*, b.Name from BMCS_Audit_Shaft_Factors a, dbo.BMCS_Users b \r\n " +
                                         " where a.prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  \r\n " +
                                         " and a.UserID = b.UserID \r\n " +
                                         " order by a.userid, a.calendardate ";

                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ResultsTableName = "Shafts";
                _dbMan1.ExecuteInstruction();

                DataTable dt1 = _dbMan1.ResultsDataTable;

                DataSet ds1 = new DataSet();
                ds1.Tables.Add(dt1);

                theReport.RegisterData(ds1);

                theReport.Load("EngShaftsAudit.frx");

            }

            //
            //Load Production
            //
            if (lblReportType.Text == "Production")
            {
                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = " select a.*, b.Name from BMCS_Audit_SystemSettings a, dbo.BMCS_Users b \r\n " +
                                         " where a.prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  \r\n " +
                                         " and a.UserID = b.UserID \r\n " +
                                         " order by a.userid, a.timestamp ";

                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ResultsTableName = "Prod";
                _dbMan1.ExecuteInstruction();

                DataTable dt1 = _dbMan1.ResultsDataTable;

                DataSet ds1 = new DataSet();
                ds1.Tables.Add(dt1);

                theReport.RegisterData(ds1);

                theReport.Load("ProdAudit.frx");

            }


            //
            //Load Plant Audit
            //
            if (lblReportType.Text == "Plant")
            {
                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = " select * from BMCS_Audit_Eng_PlantParameters " +
                                       " where yearmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and UserID = '" + editUsers.EditValue + "' ";
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ResultsTableName = "PlantAudit";
                _dbMan1.ExecuteInstruction();

                DataTable dt1 = _dbMan1.ResultsDataTable;

                DataSet ds1 = new DataSet();
                ds1.Tables.Add(dt1);

                theReport.RegisterData(ds1);

                theReport.Load("PlantAudit.frx");

            }



            //
            //Load Gangs Removed
            //
            if (lblReportType.Text == "Gangs Removed")
            {
                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = " select a.*, '2013/11/25' SelectedDate, b.Name from tbl_BCS_GangsAudit_Removed a, BMCS_Users b \r\n " +
                                       " where a.TimeStamp >= '2013/11/25'  \r\n " +
                                       " and a.TimeStamp <= '2013/11/26'  \r\n " +
                                       " and a.SystemUser = b.UserID \r\n " +
                                       " order by a.IndustryNumber, a.TimeStamp ";

                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ResultsTableName = "GangsRem";
                _dbMan1.ExecuteInstruction();

                DataTable dt1 = _dbMan1.ResultsDataTable;

                DataSet ds1 = new DataSet();
                ds1.Tables.Add(dt1);

                theReport.RegisterData(ds1);

                theReport.Load("UserRemovedAudit.frx");

            }


            //theReport.Design();

            pcReport.Clear();
            theReport.Prepare();
            theReport.Preview = pcReport;
            theReport.ShowPrepared();
        }

        private void editProdmonth_EditValueChanged(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OnCloseTabRequest(new CloseTabArg(tabCaption));
        }
    }
}
