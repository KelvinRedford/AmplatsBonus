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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using System.Globalization;
using System.IO;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using DevExpress.Utils.Drawing;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.ProductionAmplatsBonus
{
    public partial class ucTrammingCapture : BaseUserControl
    {
        public ucTrammingCapture()
        {
            InitializeComponent();
            FormRibbonPages.Add(rpMain);
            FormActiveRibbonPage = rpMain;
            FormMainRibbonPage = rpMain;
            RibbonControl = rcMain;
        }

        Procedures procs = new Procedures();

        private DateTime startDate;
        private DateTime stopDate;
        
        //PDF Checker
        private string directory;
        private string pdfName;
        private string fileName;

        private string teamgroup;
        private string shift;
        private string orgold;
        private string level;

        private void checkDirectory()
        {
            directory = @"C:\Users\" + Environment.UserName + @"\Desktop\BCS Data Extraction\Shift Captures";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private void checkPdf()
        {
            pdfName = directory + @"\Tramming " + DateTime.Now.ToString("MMM-dd-yyyy HH-mm-ss") + fileName + ".pdf";
            if (!File.Exists(pdfName))
            {
                FileStream fs = new FileStream(pdfName, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                fs.Close();
                
            }
        }

        public static Form IsBookingFormAlreadyOpen(Type FormType)
        {
            foreach (Form OpenForm in Application.OpenForms)
            {
                if (OpenForm.GetType() == FormType)
                    return OpenForm;
            }

            return null;
        }

        void UpdateTrammingOtherInsertUpdate_3Month()
        {
            // Update value
            MWDataManager.clsDataAccess TramUpdate = new MWDataManager.clsDataAccess();
            TramUpdate.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            TramUpdate.SqlStatement = "SELECT '' AS A";//Not Used :)
            UseWaitCursor = true;
            foreach (DataGridViewRow row in dgvTrammingOther.Rows)
            {
                if (dgvTrammingOther.Rows[row.Index].Cells["Value"].FormattedValue.ToString() == "N ")
                {
                    // Row Updated To New Value (Update the current row value)
                    TramUpdate.SqlStatement = TramUpdate.SqlStatement + " INSERT INTO [Mineware].[dbo].[tbl_BCS_Tramming_Gang_3Month]  " +
                        "VALUES " +
                        "( " +
                        "'0' " +
                        ",'" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editHoistMonth.EditValue)) + "' " +
                        ",'" + dgvTrammingOther.Rows[row.Index].Cells["Day"].FormattedValue.ToString() + "' " +
                        ",'" + dgvTrammingOther.Rows[row.Index].Cells["IndustryNumber"].FormattedValue.ToString() + "' " +
                        ",'" + dgvTrammingOther.Rows[row.Index].Cells["Designation"].FormattedValue.ToString() + "' " +
                        ",'" + dgvTrammingOther.Rows[row.Index].Cells["OrgOldB"].FormattedValue.ToString() + "' " +
                        ",'" + dgvTrammingOther.Rows[row.Index].Cells["Orgunit"].FormattedValue.ToString() + "' " +
                        ",'" + dgvTrammingOther.Rows[row.Index].Cells["Value"].FormattedValue.ToString() + "' " +
                        ",'" + dgvTrammingOther.Rows[row.Index].Cells["TeamGroupB"].FormattedValue.ToString() + "' " +
                        ",'" + dgvTrammingOther.Rows[row.Index].Cells["Team"].FormattedValue.ToString() + "' " +
                        ",'0' " +
                        ",'0' " +
                        ",'31' " +
                        ",'" + dgvTrammingOther.Rows[row.Index].Cells["Orgunit"].FormattedValue.ToString().Substring(0, 4) + "' " +
                        ",'Y' " +
                        ",'N' " +
                        ",'" + dgvTrammingOther.Rows[row.Index].Cells["LevelB"].FormattedValue.ToString() + "' " +
                        ",'" + Environment.UserName + "' " +
                        ",'" + DateTime.Now + "' " +
                        ",'2' " +
                        ",'" + dgvTrammingOther.Rows[row.Index].Cells["ShiftB"].FormattedValue.ToString() + "' " +
                         ")  DELETE FROM [Mineware].[dbo].[tbl_BCS_Tramming_Gang] WHERE IndustryNumber = '" + dgvTrammingOther.Rows[row.Index].Cells["IndustryNumber"].FormattedValue.ToString() + "' AND WorkingOrgUnit = '" + dgvTrammingOther.Rows[row.Index].Cells["Orgunit"].FormattedValue.ToString() + "' AND YearMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editHoistMonth.EditValue)) + "' AND Date = '1900-01-01'  AND Team = '" + dgvTrammingOther.Rows[row.Index].Cells["Team"].FormattedValue.ToString() + "' AND Designation = '" + dgvTrammingOther.Rows[row.Index].Cells["Designation"].FormattedValue.ToString() + "' ";
                }
            }
            TramUpdate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            TramUpdate.queryReturnType = MWDataManager.ReturnType.longNumber;
            TramUpdate.ExecuteInstruction();
            UseWaitCursor = false;
        }

        void UpdateTrammingOtherInsertUpdate()
        {
            // Update value
            MWDataManager.clsDataAccess TramUpdate = new MWDataManager.clsDataAccess();
            TramUpdate.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            TramUpdate.SqlStatement = "SELECT '' AS A";//Not Used :)
            UseWaitCursor = true;
            foreach (DataGridViewRow row in dgvTrammingOther.Rows)
            {
                if (dgvTrammingOther.Rows[row.Index].Cells["Value"].FormattedValue.ToString() == "N ")
                {
                    // Row Updated To New Value (Update the current row value)
                    TramUpdate.SqlStatement = TramUpdate.SqlStatement + " INSERT INTO [Mineware].[dbo].[tbl_BCS_Tramming_Gang]  " +
                        "VALUES " +
                        "( " +
                        "'0' " +
                        ",'" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editHoistMonth.EditValue)) + "' " +
                        ",'" + dgvTrammingOther.Rows[row.Index].Cells["Day"].FormattedValue.ToString() + "' " +
                        ",'" + dgvTrammingOther.Rows[row.Index].Cells["IndustryNumber"].FormattedValue.ToString() + "' " +
                        ",'" + dgvTrammingOther.Rows[row.Index].Cells["Designation"].FormattedValue.ToString() + "' " +
                        ",'" + dgvTrammingOther.Rows[row.Index].Cells["OrgOldB"].FormattedValue.ToString() + "' " +
                        ",'" + dgvTrammingOther.Rows[row.Index].Cells["Orgunit"].FormattedValue.ToString() + "' " +
                        ",'" + dgvTrammingOther.Rows[row.Index].Cells["Value"].FormattedValue.ToString() + "' " +
                        ",'" + dgvTrammingOther.Rows[row.Index].Cells["TeamGroupB"].FormattedValue.ToString() + "' " +
                        ",'" + dgvTrammingOther.Rows[row.Index].Cells["Team"].FormattedValue.ToString() + "' " +
                        ",'0' " +
                        ",'0' " +
                        ",'31' " +
                        ",'" + dgvTrammingOther.Rows[row.Index].Cells["Orgunit"].FormattedValue.ToString().Substring(0, 4) + "' " +
                        ",'Y' " +
                        ",'N' " +
                        ",'" + dgvTrammingOther.Rows[row.Index].Cells["LevelB"].FormattedValue.ToString() + "' " +
                        ",'" + Environment.UserName + "' " +
                        ",'" + DateTime.Now + "' " +
                        ",'2' " +
                        ",'" + dgvTrammingOther.Rows[row.Index].Cells["ShiftB"].FormattedValue.ToString() + "' " +
                         ")  DELETE FROM [Mineware].[dbo].[tbl_BCS_Tramming_Gang] WHERE IndustryNumber = '" + dgvTrammingOther.Rows[row.Index].Cells["IndustryNumber"].FormattedValue.ToString() + "' AND WorkingOrgUnit = '" + dgvTrammingOther.Rows[row.Index].Cells["Orgunit"].FormattedValue.ToString() + "' AND YearMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editHoistMonth.EditValue)) + "' AND Date = '1900-01-01'  AND Team = '" + dgvTrammingOther.Rows[row.Index].Cells["Team"].FormattedValue.ToString() + "' AND Designation = '" + dgvTrammingOther.Rows[row.Index].Cells["Designation"].FormattedValue.ToString() + "' ";
                }
            }
            TramUpdate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            TramUpdate.queryReturnType = MWDataManager.ReturnType.longNumber;
            TramUpdate.ExecuteInstruction();
            UseWaitCursor = false;
        }

        void UpdateTrammingOtherDelete_3Month()
        {
            //Delete value
            MWDataManager.clsDataAccess TramUpdate = new MWDataManager.clsDataAccess();
            TramUpdate.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            TramUpdate.SqlStatement = "SELECT '' AS A";//Not Used :)
            UseWaitCursor = true;
            foreach (DataGridViewRow row in dgvTrammingOther.Rows)
            {
                if (dgvTrammingOther.Rows[row.Index].Cells["Value"].FormattedValue.ToString() == "Delete")
                {
                    // Row Deleted if value is set to empty
                    TramUpdate.SqlStatement = TramUpdate.SqlStatement + " DELETE FROM [Mineware].[dbo].[tbl_BCS_Tramming_Gang_3Month] " +
                     " WHERE IndustryNumber = '" + dgvTrammingOther.Rows[row.Index].Cells["IndustryNumber"].FormattedValue.ToString() + "' AND WorkingOrgUnit = '" + dgvTrammingOther.Rows[row.Index].Cells["Orgunit"].FormattedValue.ToString() + "' AND YearMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editHoistMonth.EditValue)) + "' AND Date = '" + dgvTrammingOther.Rows[row.Index].Cells["Day"].FormattedValue.ToString() + "' AND Team = '" + dgvTrammingOther.Rows[row.Index].Cells["Team"].FormattedValue.ToString() + "' AND Designation = '" + dgvTrammingOther.Rows[row.Index].Cells["Designation"].FormattedValue.ToString() + "' ";
                }
            }
            TramUpdate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            TramUpdate.queryReturnType = MWDataManager.ReturnType.longNumber;
            TramUpdate.ExecuteInstruction();
            UseWaitCursor = false;
        }

        void UpdateTrammingOtherDelete()
        {
            //Delete value
            MWDataManager.clsDataAccess TramUpdate = new MWDataManager.clsDataAccess();
            TramUpdate.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            TramUpdate.SqlStatement = "SELECT '' AS A";//Not Used :)
            UseWaitCursor = true;
            foreach (DataGridViewRow row in dgvTrammingOther.Rows)
            {
                if (dgvTrammingOther.Rows[row.Index].Cells["Value"].FormattedValue.ToString() == "Delete")
                {
                    // Row Deleted if value is set to empty
                    TramUpdate.SqlStatement = TramUpdate.SqlStatement + " DELETE FROM [Mineware].[dbo].[tbl_BCS_Tramming_Gang] " +
                     " WHERE IndustryNumber = '" + dgvTrammingOther.Rows[row.Index].Cells["IndustryNumber"].FormattedValue.ToString() + "' AND WorkingOrgUnit = '" + dgvTrammingOther.Rows[row.Index].Cells["Orgunit"].FormattedValue.ToString() + "' AND YearMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editHoistMonth.EditValue)) + "' AND Date = '" + dgvTrammingOther.Rows[row.Index].Cells["Day"].FormattedValue.ToString() + "' AND Team = '" + dgvTrammingOther.Rows[row.Index].Cells["Team"].FormattedValue.ToString() + "' AND Designation = '" + dgvTrammingOther.Rows[row.Index].Cells["Designation"].FormattedValue.ToString() + "' ";
                }
            }
            TramUpdate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            TramUpdate.queryReturnType = MWDataManager.ReturnType.longNumber;
            TramUpdate.ExecuteInstruction();
            UseWaitCursor = false;
        }

        void UpdateTrammingAtoGInsertUpdate_3Month()
        {
            // Update value
            MWDataManager.clsDataAccess TramUpdate = new MWDataManager.clsDataAccess();
            TramUpdate.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            TramUpdate.SqlStatement = "SELECT '' AS A";//Not Used :)
            UseWaitCursor = true;
            foreach (DataGridViewRow row in dgvAToG.Rows)
            {
                if (dgvAToG.Rows[row.Index].Cells["Value2"].FormattedValue.ToString() != "Delete")
                {
                    TramUpdate.SqlStatement = TramUpdate.SqlStatement + " INSERT INTO [Mineware].[dbo].[tbl_BCS_Tramming_Gang_3Month]  " +
                        "VALUES " +
                        "( " +
                        "'0' " +
                        ",'" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editHoistMonth.EditValue)) + "' " +
                        ",'" + dgvAToG.Rows[row.Index].Cells["Day2"].FormattedValue.ToString() + "' " +
                        ",'" + dgvAToG.Rows[row.Index].Cells["IndustryNumber2"].FormattedValue.ToString() + "' " +
                        ",'" + dgvAToG.Rows[row.Index].Cells["Designation2"].FormattedValue.ToString() + "' " +
                        ",'" + dgvAToG.Rows[row.Index].Cells["OrgunitOldA"].FormattedValue.ToString() + "' " +
                        ",'" + dgvAToG.Rows[row.Index].Cells["Orgunit2"].FormattedValue.ToString() + "' " +
                        ",'N' " +
                        ",'" + dgvAToG.Rows[row.Index].Cells["TeamGroupA"].FormattedValue.ToString() + "' " +
                        ",'" + dgvAToG.Rows[row.Index].Cells["Team2"].FormattedValue.ToString() + "' " +
                        ",'" + dgvAToG.Rows[row.Index].Cells["Value2"].FormattedValue.ToString() + "' " +
                        ",'0' " +
                        ",'31' " +
                        ",'" + dgvAToG.Rows[row.Index].Cells["Orgunit2"].FormattedValue.ToString().Substring(0, 4) + "' " +
                        ",'Y' " +
                        ",'N' " +
                        ",'" + dgvAToG.Rows[row.Index].Cells["LevelA"].FormattedValue.ToString() + "' " +
                        ",'" + Environment.UserName + "' " +
                        ",'" + DateTime.Now + "' " +
                        ",'2' " +
                        ",'" + dgvAToG.Rows[row.Index].Cells["ShiftA"].FormattedValue.ToString() + "' " +
                         ")  DELETE FROM [Mineware].[dbo].[tbl_BCS_Tramming_Gang_3Month] WHERE IndustryNumber = '" + dgvAToG.Rows[row.Index].Cells["IndustryNumber2"].FormattedValue.ToString() + "' AND WorkingOrgUnit = '" + dgvAToG.Rows[row.Index].Cells["Orgunit2"].FormattedValue.ToString() + "' AND YearMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editHoistMonth.EditValue)) + "' AND Date = '1900-01-01'  AND Team = '" + dgvAToG.Rows[row.Index].Cells["Team2"].FormattedValue.ToString() + "' AND Designation = '" + dgvAToG.Rows[row.Index].Cells["Designation2"].FormattedValue.ToString() + "' ";

                }
            }
            TramUpdate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            TramUpdate.queryReturnType = MWDataManager.ReturnType.longNumber;
            TramUpdate.ExecuteInstruction();
            UseWaitCursor = false;
        }

        void UpdateTrammingAtoGInsertUpdate()
        {
            // Update value
            MWDataManager.clsDataAccess TramUpdate = new MWDataManager.clsDataAccess();
            TramUpdate.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            TramUpdate.SqlStatement = "SELECT '' AS A";//Not Used :)
            UseWaitCursor = true;
            foreach (DataGridViewRow row in dgvAToG.Rows)
            {
                if (dgvAToG.Rows[row.Index].Cells["Value2"].FormattedValue.ToString() != "Delete")
                {

                    TramUpdate.SqlStatement = TramUpdate.SqlStatement + " INSERT INTO [Mineware].[dbo].[tbl_BCS_Tramming_Gang]  " +
                        "VALUES " +
                        "( " +
                        "'0' " +
                        ",'" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editHoistMonth.EditValue)) + "' " +
                        ",'" + dgvAToG.Rows[row.Index].Cells["Day2"].FormattedValue.ToString() + "' " +
                        ",'" + dgvAToG.Rows[row.Index].Cells["IndustryNumber2"].FormattedValue.ToString() + "' " +
                        ",'" + dgvAToG.Rows[row.Index].Cells["Designation2"].FormattedValue.ToString() + "' " +
                       ",'" + dgvAToG.Rows[row.Index].Cells["OrgunitOldA"].FormattedValue.ToString() + "' " +
                        ",'" + dgvAToG.Rows[row.Index].Cells["Orgunit2"].FormattedValue.ToString() + "' " +
                        ",'N' " +
                        ",'" + dgvAToG.Rows[row.Index].Cells["TeamGroupA"].FormattedValue.ToString() + "' " +
                        ",'" + dgvAToG.Rows[row.Index].Cells["Team2"].FormattedValue.ToString() + "' " +
                        ",'" + dgvAToG.Rows[row.Index].Cells["Value2"].FormattedValue.ToString() + "' " +
                        ",'0' " +
                        ",'31' " +
                        ",'" + dgvAToG.Rows[row.Index].Cells["Orgunit2"].FormattedValue.ToString().Substring(0,4) + "' " +
                        ",'Y' " +
                        ",'N' " +
                        ",'" + dgvAToG.Rows[row.Index].Cells["LevelA"].FormattedValue.ToString() + "' " +
                        ",'" + Environment.UserName + "' " +
                        ",'" + DateTime.Now + "' " +
                        ",'2' " +
                        ",'" + dgvAToG.Rows[row.Index].Cells["ShiftA"].FormattedValue.ToString() + "' " +
                        ")  DELETE FROM [Mineware].[dbo].[tbl_BCS_Tramming_Gang] WHERE IndustryNumber = '" + dgvAToG.Rows[row.Index].Cells["IndustryNumber2"].FormattedValue.ToString() + "' AND WorkingOrgUnit = '" + dgvAToG.Rows[row.Index].Cells["Orgunit2"].FormattedValue.ToString() + "' AND YearMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editHoistMonth.EditValue)) + "' AND Date = '1900-01-01'  AND Team = '" + dgvAToG.Rows[row.Index].Cells["Team2"].FormattedValue.ToString() + "' AND Designation = '" + dgvAToG.Rows[row.Index].Cells["Designation2"].FormattedValue.ToString() + "' ";

                    //old
                    // Row Updated To New Value (Update the current row value)
                    //TramUpdate.SqlStatement = TramUpdate.SqlStatement + " IF EXISTS (SELECT * FROM [Mineware].[dbo].[tbl_BCS_Tramming_Gang] WHERE IndustryNumber = '" + dgvAToG.Rows[row.Index].Cells["IndustryNumber2"].FormattedValue.ToString() + "' AND WorkingOrgUnit = '" + dgvAToG.Rows[row.Index].Cells["Orgunit2"].FormattedValue.ToString() + "' AND Date  = '" + dgvAToG.Rows[row.Index].Cells["Day2"].FormattedValue.ToString() + "' AND YearMonth = '" + MillMonth.Value + "' AND Team = '" + dgvAToG.Rows[row.Index].Cells["Team2"].FormattedValue.ToString() + "' AND Designation = '" + dgvAToG.Rows[row.Index].Cells["Designation2"].FormattedValue.ToString() + "') " +
                    //" BEGIN " +
                    //" UPDATE [Mineware].[dbo].[tbl_BCS_Tramming_Gang] " +
                    //" SET Hoppers = '" + dgvAToG.Rows[row.Index].Cells["Value2"].FormattedValue.ToString() + "' " +
                    //" WHERE IndustryNumber = '" + dgvAToG.Rows[row.Index].Cells["IndustryNumber2"].FormattedValue.ToString() + "' AND WorkingOrgUnit = '" + dgvAToG.Rows[row.Index].Cells["Orgunit2"].FormattedValue.ToString() + "' AND Date  = '" + dgvAToG.Rows[row.Index].Cells["Day2"].FormattedValue.ToString() + "' AND Team = '" + dgvAToG.Rows[row.Index].Cells["Team2"].FormattedValue.ToString() + "' AND Designation = '" + dgvAToG.Rows[row.Index].Cells["Designation2"].FormattedValue.ToString() + "' " +
                    //" END " +
                    //" ELSE " +
                    //" BEGIN " +
                    //" INSERT INTO [Mineware].[dbo].[tbl_BCS_Tramming_Gang] (ID, YearMonth, IndustryNumber, Designation, OrgUnit, WorkingOrgunit, Attendance, TeamGroup " +
                    //"    , Team, Hoppers, Shift, TotalShift, Section, Added, Removed, Level, SystemUser, TimeStamp, ExceptionID, TypeShift) " +
                    //"    SELECT TOP (1)ID, YearMonth, IndustryNumber, Designation, OrgUnit, WorkingOrgunit, Attendance, TeamGroup " +
                    //"    , Team, Hoppers, Shift, TotalShift, Section, Added, Removed, Level, SystemUser, TimeStamp, ExceptionID, TypeShift FROM [Mineware].[dbo].[tbl_BCS_Tramming_Gang] " +
                    //"    WHERE IndustryNumber = '" + dgvAToG.Rows[row.Index].Cells["IndustryNumber2"].FormattedValue.ToString() + "' AND WorkingOrgUnit = '" + dgvAToG.Rows[row.Index].Cells["Orgunit2"].FormattedValue.ToString() + "' AND YearMonth = '" + MillMonth.Value + "'  AND Team = '" + dgvAToG.Rows[row.Index].Cells["Team2"].FormattedValue.ToString() + "' AND Designation = '" + dgvAToG.Rows[row.Index].Cells["Designation2"].FormattedValue.ToString() + "' " +
                    //"    UPDATE [Mineware].[dbo].[tbl_BCS_Tramming_Gang] " +
                    //"    SET Hoppers = '" + dgvAToG.Rows[row.Index].Cells["Value2"].FormattedValue.ToString() + "', Date = '" + dgvAToG.Rows[row.Index].Cells["Day2"].FormattedValue.ToString() + "' " +
                    //"    WHERE IndustryNumber = '" + dgvAToG.Rows[row.Index].Cells["IndustryNumber2"].FormattedValue.ToString() + "' AND WorkingOrgUnit = '" + dgvAToG.Rows[row.Index].Cells["Orgunit2"].FormattedValue.ToString() + "' AND YearMonth = '" + MillMonth.Value + "' AND Team = '" + dgvAToG.Rows[row.Index].Cells["Team2"].FormattedValue.ToString() + "' AND Designation = '" + dgvAToG.Rows[row.Index].Cells["Designation2"].FormattedValue.ToString() + "' AND Date IS NULL " +
                    //"  DELETE FROM [Mineware].[dbo].[tbl_BCS_Tramming_Gang] WHERE IndustryNumber = '" + dgvAToG.Rows[row.Index].Cells["IndustryNumber2"].FormattedValue.ToString() + "' AND WorkingOrgUnit = '" + dgvAToG.Rows[row.Index].Cells["Orgunit2"].FormattedValue.ToString() + "' AND YearMonth = '" + MillMonth.Value + "' AND Date = '1900-01-01'  AND Team = '" + dgvAToG.Rows[row.Index].Cells["Team2"].FormattedValue.ToString() + "' AND Designation = '" + dgvAToG.Rows[row.Index].Cells["Designation2"].FormattedValue.ToString() + "'" +
                    //" END";
                }
            }
            TramUpdate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            TramUpdate.queryReturnType = MWDataManager.ReturnType.longNumber;
            TramUpdate.ExecuteInstruction();
            UseWaitCursor = false;
        }

        void UpdateTrammingAtoGDelete_3Month()
        {
            //Delete value
            MWDataManager.clsDataAccess TramUpdate = new MWDataManager.clsDataAccess();
            TramUpdate.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            TramUpdate.SqlStatement = "SELECT '' AS A";//Not Used :)
            UseWaitCursor = true;
            foreach (DataGridViewRow row in dgvAToG.Rows)
            {
                if (dgvAToG.Rows[row.Index].Cells["Value2"].FormattedValue.ToString() == "Delete")
                {
                    // Row Deleted if value is set to empty
                    TramUpdate.SqlStatement = TramUpdate.SqlStatement + " DELETE FROM [Mineware].[dbo].[tbl_BCS_Tramming_Gang_3Month] " +
                     " WHERE IndustryNumber = '" + dgvAToG.Rows[row.Index].Cells["IndustryNumber2"].FormattedValue.ToString() + "' AND WorkingOrgUnit = '" + dgvAToG.Rows[row.Index].Cells["Orgunit2"].FormattedValue.ToString() + "' AND YearMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editHoistMonth.EditValue)) + "' AND Date = '" + dgvAToG.Rows[row.Index].Cells["Day2"].FormattedValue.ToString() + "' AND Team = '" + dgvAToG.Rows[row.Index].Cells["Team2"].FormattedValue.ToString() + "' AND Designation = '" + dgvAToG.Rows[row.Index].Cells["Designation2"].FormattedValue.ToString() + "' ";
                }
            }
            TramUpdate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            TramUpdate.queryReturnType = MWDataManager.ReturnType.longNumber;
            TramUpdate.ExecuteInstruction();
            UseWaitCursor = false;
        }

        void UpdateTrammingAtoGDelete()
        {
            //Delete value
            MWDataManager.clsDataAccess TramUpdate = new MWDataManager.clsDataAccess();
            TramUpdate.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            TramUpdate.SqlStatement = "SELECT '' AS A";//Not Used :)
            UseWaitCursor = true;
            foreach (DataGridViewRow row in dgvAToG.Rows)
            {
                if (dgvAToG.Rows[row.Index].Cells["Value2"].FormattedValue.ToString() == "Delete")
                {
                    // Row Deleted if value is set to empty
                    TramUpdate.SqlStatement = TramUpdate.SqlStatement + " DELETE FROM [Mineware].[dbo].[tbl_BCS_Tramming_Gang] " +
                     " WHERE IndustryNumber = '" + dgvAToG.Rows[row.Index].Cells["IndustryNumber2"].FormattedValue.ToString() + "' AND WorkingOrgUnit = '" + dgvAToG.Rows[row.Index].Cells["Orgunit2"].FormattedValue.ToString() + "' AND YearMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editHoistMonth.EditValue)) + "' AND Date = '" + dgvAToG.Rows[row.Index].Cells["Day2"].FormattedValue.ToString() + "' AND Team = '" + dgvAToG.Rows[row.Index].Cells["Team2"].FormattedValue.ToString() + "' AND Designation = '" + dgvAToG.Rows[row.Index].Cells["Designation2"].FormattedValue.ToString() + "' ";
                }
            }
            TramUpdate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            TramUpdate.queryReturnType = MWDataManager.ReturnType.longNumber;
            TramUpdate.ExecuteInstruction();
            UseWaitCursor = false;
        }

        void LoadDate()
        {
            MWDataManager.clsDataAccess _dbMan1New = new MWDataManager.clsDataAccess();
            _dbMan1New.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            _dbMan1New.SqlStatement = "declare @pm varchar(10) \r\n" +
                                   "set @pm = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editHoistMonth.EditValue)) + "' \r\n" +

                                   "select Startdate, (startdate + 32)-day((startdate + 32)) bb from ( \r\n" +
                                   "select convert(datetime,(substring(@pm,1,4)+ '-'+substring(@pm,5,2) +'-01')) Startdate) a \r\n";
            _dbMan1New.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1New.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1New.ExecuteInstruction();

            DataTable Neil = _dbMan1New.ResultsDataTable;
            StartDate.Value = Convert.ToDateTime(Neil.Rows[0]["Startdate"].ToString());
            EndDate.Value = Convert.ToDateTime(Neil.Rows[0]["bb"].ToString());

            startDate = Convert.ToDateTime(Neil.Rows[0]["Startdate"].ToString());
            stopDate = Convert.ToDateTime(Neil.Rows[0]["bb"].ToString());
        }

        void LoadTrammingAtoG()
        {
            this.Cursor = Cursors.WaitCursor;
            this.UseWaitCursor = true;
            gvAToG.Bands[0].Caption = "Teams A / G Shift Captures For: " + Orgunitlbl.Text;
            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            _dbMan1.SqlStatement = "  Select a.*,  \r\n" +
                    " case when   b.IndustryNumber is null then 'No' else 'Yes' end as Saved from ( \r\n" +

                    "  select * from tbl_BCS_Tramming_Gang  \r\n" +
                    "where workingorgunit = '" + Orgunitlbl.Text + "'  \r\n" +
                    "and date = (select max(date) from tbl_BCS_Tramming_Gang   \r\n" +
                    "where workingorgunit = '" + Orgunitlbl.Text + "')  \r\n" +
                    "  ) a  \r\n" +
                    "left outer join   \r\n" +
                    "(  \r\n" +
                    "Select * from [dbo].[tbl_BCS_Tramming_Gang_3Month] \r\n" +
                    "where workingorgunit = '" + Orgunitlbl.Text + "'  \r\n" +
                    " and date = (select max(date) from [tbl_BCS_Tramming_Gang_3Month]   \r\n" +
                    "where workingorgunit = '" + Orgunitlbl.Text + "') ) b   \r\n" +
                    " on a.ID = b.ID and a.IndustryNumber = b.IndustryNumber  \r\n" +
                    " order by a.team ";





            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();

            //21-07-2018. Script updated, grid updated to have Day1 - Day31 Visible, ClockColumns added visible - false
            //MessageBox.Show(String.Format("{0:yyyy-MM-dd}",DateTxt.Value));
            MWDataManager.clsDataAccess _dbMan12 = new MWDataManager.clsDataAccess();
            _dbMan12.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            _dbMan12.SqlStatement = " declare @start datetime \r\n" +
                                    " declare @end datetime \r\n" +
                                    " set @start = '" + String.Format("{0:yyyy-MM-dd}", StartDate.Value) + "' \r\n" +
                                    " set @end = '" + String.Format("{0:yyyy-MM-dd}", EndDate.Value) + "' \r\n" +
" SELECT * from  \r\n" +
" ( \r\n" +
   " SELECT IndustryNumber, designation, team, teamgroup, typeshift, level, orgunit \r\n" +
       " ,max(Day1) Day1 \r\n" +
       " ,max(Day2) Day2 \r\n" +
       " ,max(Day3) Day3 \r\n" +
       " ,max(Day4) Day4 \r\n" +
       " ,max(day5) Day5 \r\n" +
       " ,max(day6) Day6 \r\n" +
       " ,max(day7) Day7 \r\n" +
       " ,max(day8) Day8 \r\n" +
       " ,max(day9) Day9 \r\n" +
       " ,max(day10) Day10 \r\n" +
       " ,max(day11) Day11 \r\n" +
       " ,max(day12) Day12 \r\n" +
       " ,max(day13) Day13 \r\n" +
       " ,max(day14) Day14 \r\n" +
       " ,max(day15) Day15 \r\n" +
       " ,max(day16) Day16 \r\n" +
       " ,max(day17) Day17 \r\n" +
       " ,max(day18) Day18 \r\n" +
       " ,max(day19) Day19 \r\n" +
       " ,max(day20) Day20 \r\n" +
       " ,max(day21) Day21 \r\n" +
       " ,max(day22) Day22 \r\n" +
       " ,max(day23) Day23 \r\n" +
       " ,max(day24) Day24 \r\n" +
       " ,max(day25) Day25 \r\n" +
       " ,max(day26) Day26 \r\n" +
       " ,max(day27) Day27 \r\n" +
       " ,max(day28) Day28 \r\n" +
       " ,max(day29) Day29 \r\n" +
       " ,max(day30) Day30 \r\n" +
       " ,max(day31) Day31 \r\n" +
   " FROM ( \r\n" +
       " SELECT IndustryNumber, designation, team, teamgroup, typeshift, level, orgunit, \r\n" +
       " CASE WHEN @start = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day1, \r\n" +
       " CASE WHEN @start+1 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+1 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day2, \r\n" +
       " CASE WHEN @start+2 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+2 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day3, \r\n" +
       " CASE WHEN @start+3 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+3 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day4, \r\n" +
       " CASE WHEN @start+4 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+4 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day5, \r\n" +
       " CASE WHEN @start+5 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+5 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day6, \r\n" +
       " CASE WHEN @start+6 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+6 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day7, \r\n" +
       " CASE WHEN @start+7 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+7 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day8, \r\n" +
       " CASE WHEN @start+8 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+8 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance) \r\n" +
       " else '' end as Day9,   \r\n" +
       " CASE WHEN @start+9 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+9 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)  \r\n" +
       " else '' end as Day10,  \r\n" +
       " CASE WHEN @start+10 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+10 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day11, \r\n" +
       " CASE WHEN @start+11 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       "  WHEN @start+11 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)  \r\n" +
       " else '' end as Day12, \r\n" +
       " CASE WHEN @start+12 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+12 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day13, \r\n" +
       " CASE WHEN @start+13 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+13 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day14, \r\n" +
       " CASE WHEN @start+14 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+14 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day15, \r\n" +
       " CASE WHEN @start+15 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+15 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day16, \r\n" +
       " CASE WHEN @start+16 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+16 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day17, \r\n" +
       " CASE WHEN @start+17 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+17 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day18, \r\n" +
       " CASE WHEN @start+18 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+18 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day19, \r\n" +
       " CASE WHEN @start+19 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+19 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)  \r\n" +
       " else '' end as Day20, \r\n" +
       " CASE WHEN @start+20 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+20 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day21, \r\n" +
       " CASE WHEN @start+21 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+21 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day22, \r\n" +
       " CASE WHEN @start+22 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+22 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day23, \r\n" +
       " CASE WHEN @start+23 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+23 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day24, \r\n" +
       " CASE WHEN @start+24 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+24 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance) \r\n" +
       " else '' end as Day25,   \r\n" +
       " CASE WHEN @start+25 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+25 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)  \r\n" +
       " else '' end as Day26,  \r\n" +
       " CASE WHEN @start+26 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+26 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)  \r\n" +
       " else '' end as Day27, \r\n" +
       " CASE WHEN @start+27 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+27 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day28, \r\n" +
       " CASE WHEN @start+28 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+28 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)  \r\n" +
       " else '' end as Day29, \r\n" +
       " CASE WHEN @start+29 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+29 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day30, \r\n" +
       " CASE WHEN @start+30 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+30 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day31 \r\n" +
       " FROM [Mineware].[dbo].tbl_BCS_Tramming_Gang   \r\n" +
       " WHERE Team in ('A','B','C','D','E','F','G') and workingorgunit = '" + Orgunitlbl.Text + "'  and yearmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editHoistMonth.EditValue)) + "'   \r\n" +
   " ) a GROUP BY  IndustryNumber, designation, team, teamgroup, typeshift, level, orgunit  \r\n" +
" ) a \r\n" +
       " LEFT OUTER JOIN \r\n" +
       " ( \r\n" +
       "	SELECT nnn \r\n" +
           " ,max(Day1Clock) Day1Clock \r\n" +
           " ,max(Day2Clock) Day2Clock \r\n" +
           " ,max(Day3Clock) Day3Clock \r\n" +
           " ,max(Day4Clock) Day4Clock \r\n" +
           " ,max(Day5Clock) Day5Clock \r\n" +
           " ,max(Day6Clock) Day6Clock \r\n" +
           " ,max(Day7Clock) Day7Clock \r\n" +
           " ,max(Day8Clock) Day8Clock \r\n" +
           " ,max(Day9Clock) Day9Clock \r\n" +
           " ,max(Day10Clock) Day10Clock \r\n" +
           " ,max(Day11Clock) Day11Clock \r\n" +
           " ,max(Day12Clock) Day12Clock \r\n" +
           " ,max(Day13Clock) Day13Clock \r\n" +
           " ,max(Day14Clock) Day14Clock \r\n" +
           " ,max(Day15Clock) Day15Clock \r\n" +
           " ,max(Day16Clock) Day16Clock \r\n" +
           " ,max(Day17Clock) Day17Clock \r\n" +
           " ,max(Day18Clock) Day18Clock \r\n" +
           " ,max(Day19Clock) Day19Clock \r\n" +
           " ,max(Day20Clock) Day20Clock \r\n" +
           " ,max(Day21Clock) Day21Clock \r\n" +
           " ,max(Day22Clock) Day22Clock \r\n" +
           " ,max(Day23Clock) Day23Clock \r\n" +
           " ,max(Day24Clock) Day24Clock \r\n" +
           " ,max(Day25Clock) Day25Clock \r\n" +
           " ,max(Day26Clock) Day26Clock \r\n" +
           " ,max(Day27Clock) Day27Clock \r\n" +
           " ,max(Day28Clock) Day28Clock \r\n" +
           " ,max(Day29Clock) Day29Clock \r\n" +
           " ,max(Day30Clock) Day30Clock \r\n" +
           " ,max(Day31Clock) Day31Clock \r\n" +


           " FROM  \r\n" +
           " ( \r\n" +
           "	SELECT IndustryNumber nnn, \r\n" +
           " CASE WHEN TheDate = @start THEN expectedatwork+LeaveFlag END AS Day1Clock, \r\n" +
           " CASE WHEN TheDate = @start+1 THEN expectedatwork+LeaveFlag END AS Day2Clock, \r\n" +
           " CASE WHEN TheDate = @start+2 THEN expectedatwork+LeaveFlag END AS Day3Clock, \r\n" +
           " CASE WHEN TheDate = @start+3 THEN expectedatwork+LeaveFlag END AS Day4Clock, \r\n" +
           " CASE WHEN TheDate = @start+4 THEN expectedatwork+LeaveFlag END AS Day5Clock, \r\n" +
           " CASE WHEN TheDate = @start+5 THEN expectedatwork+LeaveFlag END AS Day6Clock, \r\n" +
           " CASE WHEN TheDate = @start+6 THEN expectedatwork+LeaveFlag END AS Day7Clock, \r\n" +
           " CASE WHEN TheDate = @start+7 THEN expectedatwork+LeaveFlag END AS Day8Clock, \r\n" +
           " CASE WHEN TheDate = @start+8 THEN expectedatwork+LeaveFlag END AS Day9Clock, \r\n" +
           " CASE WHEN TheDate = @start+9 THEN expectedatwork+LeaveFlag END AS Day10Clock, \r\n" +
           " CASE WHEN TheDate = @start+10 THEN expectedatwork+LeaveFlag END AS Day11Clock, \r\n" +
           " CASE WHEN TheDate = @start+11 THEN expectedatwork+LeaveFlag END AS Day12Clock, \r\n" +
           " CASE WHEN TheDate = @start+12 THEN expectedatwork+LeaveFlag END AS Day13Clock, \r\n" +
           " CASE WHEN TheDate = @start+13 THEN expectedatwork+LeaveFlag END AS Day14Clock, \r\n" +
           " CASE WHEN TheDate = @start+14 THEN expectedatwork+LeaveFlag END AS Day15Clock, \r\n" +
           " CASE WHEN TheDate = @start+15 THEN expectedatwork+LeaveFlag END AS Day16Clock, \r\n" +
           " CASE WHEN TheDate = @start+16 THEN expectedatwork+LeaveFlag END AS Day17Clock, \r\n" +
           " CASE WHEN TheDate = @start+17 THEN expectedatwork+LeaveFlag END AS Day18Clock, \r\n" +
           " CASE WHEN TheDate = @start+18 THEN expectedatwork+LeaveFlag END AS Day19Clock, \r\n" +
           " CASE WHEN TheDate = @start+19 THEN expectedatwork+LeaveFlag END AS Day20Clock, \r\n" +
           " CASE WHEN TheDate = @start+20 THEN expectedatwork+LeaveFlag END AS Day21Clock, \r\n" +
           " CASE WHEN TheDate = @start+21 THEN expectedatwork+LeaveFlag END AS Day22Clock, \r\n" +
           " CASE WHEN TheDate = @start+22 THEN expectedatwork+LeaveFlag END AS Day23Clock, \r\n" +
           " CASE WHEN TheDate = @start+23 THEN expectedatwork+LeaveFlag END AS Day24Clock, \r\n" +
           " CASE WHEN TheDate = @start+24 THEN expectedatwork+LeaveFlag END AS Day25Clock, \r\n" +
           " CASE WHEN TheDate = @start+25 THEN expectedatwork+LeaveFlag END AS Day26Clock, \r\n" +
           " CASE WHEN TheDate = @start+26 THEN expectedatwork+LeaveFlag END AS Day27Clock, \r\n" +
           " CASE WHEN TheDate = @start+27 THEN expectedatwork+LeaveFlag END AS Day28Clock, \r\n" +
           " CASE WHEN TheDate = @start+28 THEN expectedatwork+LeaveFlag END AS Day29Clock, \r\n" +
           " CASE WHEN TheDate = @start+29 THEN expectedatwork+LeaveFlag END AS Day30Clock, \r\n" +
           " CASE WHEN TheDate = @start+30 THEN expectedatwork+LeaveFlag END AS Day31Clock \r\n" +

           " FROM [Mineware].[dbo].[tbl_Import_BMCS_Clocking_Total] \r\n" +
           "	WHERE [TheDate] >= @start and [TheDate] <= @start +31 \r\n" +
           " ) a GROUP BY nnn  \r\n" +
       " ) b ON a.IndustryNumber = b.nnn ORDER BY IndustryNumber ASC";

            _dbMan12.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan12.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan12.ExecuteInstruction();


            DataTable dt = _dbMan12.ResultsDataTable;


            DataSet ds = new DataSet();

            ds.Tables.Add(dt);

            gcAToG.DataSource = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                Tram1.FieldName = "IndustryNumber";
                Tram2.FieldName = "designation";
                Tram5.FieldName = "team";
                Tram10.FieldName = "Day1";
                Tram11.FieldName = "Day2";
                Tram12.FieldName = "Day3";
                Tram13.FieldName = "Day4";
                Tram14.FieldName = "Day5";
                Tram15.FieldName = "Day6";
                Tram16.FieldName = "Day7";
                Tram17.FieldName = "Day8";
                Tram18.FieldName = "Day9";
                Tram19.FieldName = "Day10";
                Tram20.FieldName = "Day11";
                Tram21.FieldName = "Day12";
                Tram22.FieldName = "Day13";
                Tram23.FieldName = "Day14";
                Tram24.FieldName = "Day15";
                Tram25.FieldName = "Day16";
                Tram26.FieldName = "Day17";
                Tram27.FieldName = "Day18";
                Tram28.FieldName = "Day19";
                Tram29.FieldName = "Day20";
                Tram30.FieldName = "Day21";
                Tram31.FieldName = "Day22";
                Tram32.FieldName = "Day23";
                Tram33.FieldName = "Day24";
                Tram34.FieldName = "Day25";
                Tram35.FieldName = "Day26";
                Tram36.FieldName = "Day27";
                Tram37.FieldName = "Day28";
                Tram38.FieldName = "Day29";
                Tram39.FieldName = "Day30";
                Tram40.FieldName = "Day31";
                Tram41.FieldName = "Day1Clock";
                Tram42.FieldName = "Day2Clock";
                Tram43.FieldName = "Day3Clock";
                Tram44.FieldName = "Day4Clock";
                Tram45.FieldName = "Day5Clock";
                Tram46.FieldName = "Day6Clock";
                Tram47.FieldName = "Day7Clock";
                Tram48.FieldName = "Day8Clock";
                Tram49.FieldName = "Day9Clock";
                Tram50.FieldName = "Day10Clock";
                Tram51.FieldName = "Day11Clock";
                Tram52.FieldName = "Day12Clock";
                Tram53.FieldName = "Day13Clock";
                Tram54.FieldName = "Day14Clock";
                Tram55.FieldName = "Day15Clock";
                Tram56.FieldName = "Day16Clock";
                Tram57.FieldName = "Day17Clock";
                Tram58.FieldName = "Day18Clock";
                Tram59.FieldName = "Day19Clock";
                Tram60.FieldName = "Day20Clock";
                Tram61.FieldName = "Day21Clock";
                Tram62.FieldName = "Day22Clock";
                Tram63.FieldName = "Day23Clock";
                Tram64.FieldName = "Day24Clock";
                Tram65.FieldName = "Day25Clock";
                Tram66.FieldName = "Day26Clock";
                Tram67.FieldName = "Day27Clock";
                Tram68.FieldName = "Day28Clock";
                Tram69.FieldName = "Day29Clock";
                Tram70.FieldName = "Day30Clock";
                Tram71.FieldName = "Day31Clock";

                gvAToG.Columns[9].Visible = true;
                gvAToG.Columns[10].Visible = true;
                gvAToG.Columns[11].Visible = true;
                gvAToG.Columns[12].Visible = true;
                gvAToG.Columns[13].Visible = true;
                gvAToG.Columns[14].Visible = true;
                gvAToG.Columns[15].Visible = true;
                gvAToG.Columns[16].Visible = true;
                gvAToG.Columns[17].Visible = true;
                gvAToG.Columns[18].Visible = true;
                gvAToG.Columns[19].Visible = true;
                gvAToG.Columns[20].Visible = true;
                gvAToG.Columns[21].Visible = true;
                gvAToG.Columns[22].Visible = true;
                gvAToG.Columns[23].Visible = true;
                gvAToG.Columns[24].Visible = true;
                gvAToG.Columns[25].Visible = true;
                gvAToG.Columns[26].Visible = true;
                gvAToG.Columns[27].Visible = true;
                gvAToG.Columns[28].Visible = true;
                gvAToG.Columns[29].Visible = true;
                gvAToG.Columns[30].Visible = true;
                gvAToG.Columns[31].Visible = true;
                gvAToG.Columns[32].Visible = true;
                gvAToG.Columns[33].Visible = true;
                gvAToG.Columns[34].Visible = true;
                gvAToG.Columns[35].Visible = true;
                gvAToG.Columns[36].Visible = true;
                gvAToG.Columns[37].Visible = true;
                gvAToG.Columns[38].Visible = true;
                gvAToG.Columns[39].Visible = true;

            }

            gvAToG.Columns[9].Caption = startDate.ToString("dd  MMM   ddd");
            gvAToG.Columns[10].Caption = startDate.AddDays(1).ToString("dd  MMM   ddd");
            gvAToG.Columns[11].Caption = startDate.AddDays(2).ToString("dd  MMM   ddd");
            gvAToG.Columns[12].Caption = startDate.AddDays(3).ToString("dd  MMM   ddd");
            gvAToG.Columns[13].Caption = startDate.AddDays(4).ToString("dd  MMM   ddd");
            gvAToG.Columns[14].Caption = startDate.AddDays(5).ToString("dd  MMM   ddd");
            gvAToG.Columns[15].Caption = startDate.AddDays(6).ToString("dd  MMM   ddd");
            gvAToG.Columns[16].Caption = startDate.AddDays(7).ToString("dd  MMM   ddd");
            gvAToG.Columns[17].Caption = startDate.AddDays(8).ToString("dd  MMM   ddd");
            gvAToG.Columns[18].Caption = startDate.AddDays(9).ToString("dd  MMM   ddd");
            gvAToG.Columns[19].Caption = startDate.AddDays(10).ToString("dd  MMM   ddd");
            gvAToG.Columns[20].Caption = startDate.AddDays(11).ToString("dd  MMM   ddd");
            gvAToG.Columns[21].Caption = startDate.AddDays(12).ToString("dd  MMM   ddd");
            gvAToG.Columns[22].Caption = startDate.AddDays(13).ToString("dd  MMM   ddd");
            gvAToG.Columns[23].Caption = startDate.AddDays(14).ToString("dd  MMM   ddd");
            gvAToG.Columns[24].Caption = startDate.AddDays(15).ToString("dd  MMM   ddd");
            gvAToG.Columns[25].Caption = startDate.AddDays(16).ToString("dd  MMM   ddd");
            gvAToG.Columns[26].Caption = startDate.AddDays(17).ToString("dd  MMM   ddd");
            gvAToG.Columns[27].Caption = startDate.AddDays(18).ToString("dd  MMM   ddd");
            gvAToG.Columns[28].Caption = startDate.AddDays(19).ToString("dd  MMM   ddd");
            gvAToG.Columns[29].Caption = startDate.AddDays(20).ToString("dd  MMM   ddd");
            gvAToG.Columns[30].Caption = startDate.AddDays(21).ToString("dd  MMM   ddd");
            gvAToG.Columns[31].Caption = startDate.AddDays(22).ToString("dd  MMM   ddd");
            gvAToG.Columns[32].Caption = startDate.AddDays(23).ToString("dd  MMM   ddd");
            gvAToG.Columns[33].Caption = startDate.AddDays(24).ToString("dd  MMM   ddd");
            gvAToG.Columns[34].Caption = startDate.AddDays(25).ToString("dd  MMM   ddd");
            gvAToG.Columns[35].Caption = startDate.AddDays(26).ToString("dd  MMM   ddd");
            gvAToG.Columns[36].Caption = startDate.AddDays(27).ToString("dd  MMM   ddd");
            gvAToG.Columns[37].Caption = startDate.AddDays(28).ToString("dd  MMM   ddd");
            gvAToG.Columns[38].Caption = startDate.AddDays(29).ToString("dd  MMM   ddd");
            gvAToG.Columns[39].Caption = startDate.AddDays(30).ToString("dd  MMM   ddd");

            if (_dbMan12.ResultsDataTable.Rows.Count != 0)
            {
                for (int i = 0; i < gvAToG.Columns.Count; i++)
                {
                    var result = String.Format("{0:dd  MMM   ddd}", EndDate.Value);
                    if (gvAToG.Columns[i].Caption == result)
                    {
                        var removestr = gvAToG.Columns[i].FieldName.ToString().Substring(3, 2);
                        int remove = Convert.ToInt32(removestr) + 1;
                        for (int re = remove; re < 32; re++)
                        {
                            gvAToG.Columns["Day" + re].Visible = false;

                        }
                    }
                }
            }
            this.Cursor = Cursors.Default;
            this.UseWaitCursor = false;
            return;
        }

        void LoadTrammingOther()
        {
            this.Cursor = Cursors.WaitCursor;
            this.UseWaitCursor = true;
            gvOther.Bands[0].Caption = "Teams Not A / G Shift Captures For: " + Orgunitlbl.Text;
            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            _dbMan1.SqlStatement = "  Select a.*,  \r\n" +
                    " case when   b.IndustryNumber is null then 'No' else 'Yes' end as Saved from ( \r\n" +

                    "  select * from tbl_BCS_Tramming_Gang  \r\n" +
                    "where workingorgunit = '" + Orgunitlbl.Text + "'  \r\n" +
                    "and date = (select max(date) from tbl_BCS_Tramming_Gang   \r\n" +
                    "where workingorgunit = '" + Orgunitlbl.Text + "')  \r\n" +
                    "  ) a  \r\n" +
                    "left outer join   \r\n" +
                    "(  \r\n" +
                    "Select * from [dbo].[tbl_BCS_Tramming_Gang_3Month] \r\n" +
                    "where workingorgunit = '" + Orgunitlbl.Text + "'  \r\n" +
                    " and date = (select max(date) from [tbl_BCS_Tramming_Gang_3Month]   \r\n" +
                    "where workingorgunit = '" + Orgunitlbl.Text + "') ) b   \r\n" +
                    " on a.ID = b.ID and a.IndustryNumber = b.IndustryNumber  \r\n" +
                    " order by a.team ";





            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();

            //21-07-2018. Script updated, grid updated to have Day1 - Day31 Visible, ClockColumns added visible - false
            //MessageBox.Show(String.Format("{0:yyyy-MM-dd}",DateTxt.Value));
            MWDataManager.clsDataAccess _dbMan12 = new MWDataManager.clsDataAccess();
            _dbMan12.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            _dbMan12.SqlStatement = " declare @start datetime \r\n" +
                                    " declare @end datetime \r\n" +
                                    " set @start = '" + String.Format("{0:yyyy-MM-dd}", StartDate.Value) + "' \r\n" +
                                    " set @end = '" + String.Format("{0:yyyy-MM-dd}", EndDate.Value) + "' \r\n" +
" SELECT * from  \r\n" +
" ( \r\n" +
   " SELECT IndustryNumber, designation, team, teamgroup, typeshift, level, orgunit \r\n" +
       " ,max(Day1) Day1 \r\n" +
       " ,max(Day2) Day2 \r\n" +
       " ,max(Day3) Day3 \r\n" +
       " ,max(Day4) Day4 \r\n" +
       " ,max(day5) Day5 \r\n" +
       " ,max(day6) Day6 \r\n" +
       " ,max(day7) Day7 \r\n" +
       " ,max(day8) Day8 \r\n" +
       " ,max(day9) Day9 \r\n" +
       " ,max(day10) Day10 \r\n" +
       " ,max(day11) Day11 \r\n" +
       " ,max(day12) Day12 \r\n" +
       " ,max(day13) Day13 \r\n" +
       " ,max(day14) Day14 \r\n" +
       " ,max(day15) Day15 \r\n" +
       " ,max(day16) Day16 \r\n" +
       " ,max(day17) Day17 \r\n" +
       " ,max(day18) Day18 \r\n" +
       " ,max(day19) Day19 \r\n" +
       " ,max(day20) Day20 \r\n" +
       " ,max(day21) Day21 \r\n" +
       " ,max(day22) Day22 \r\n" +
       " ,max(day23) Day23 \r\n" +
       " ,max(day24) Day24 \r\n" +
       " ,max(day25) Day25 \r\n" +
       " ,max(day26) Day26 \r\n" +
       " ,max(day27) Day27 \r\n" +
       " ,max(day28) Day28 \r\n" +
       " ,max(day29) Day29 \r\n" +
       " ,max(day30) Day30 \r\n" +
       " ,max(day31) Day31 \r\n" +
   " FROM ( \r\n" +
       " SELECT IndustryNumber, designation, team, teamgroup, typeshift, level, orgunit, \r\n" +
       " CASE WHEN @start = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day1, \r\n" +
       " CASE WHEN @start+1 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+1 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day2, \r\n" +
       " CASE WHEN @start+2 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+2 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day3, \r\n" +
       " CASE WHEN @start+3 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+3 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day4, \r\n" +
       " CASE WHEN @start+4 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+4 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day5, \r\n" +
       " CASE WHEN @start+5 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+5 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day6, \r\n" +
       " CASE WHEN @start+6 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+6 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day7, \r\n" +
       " CASE WHEN @start+7 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+7 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day8, \r\n" +
       " CASE WHEN @start+8 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+8 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance) \r\n" +
       " else '' end as Day9,   \r\n" +
       " CASE WHEN @start+9 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+9 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)  \r\n" +
       " else '' end as Day10,  \r\n" +
       " CASE WHEN @start+10 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+10 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day11, \r\n" +
       " CASE WHEN @start+11 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       "  WHEN @start+11 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)  \r\n" +
       " else '' end as Day12, \r\n" +
       " CASE WHEN @start+12 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+12 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day13, \r\n" +
       " CASE WHEN @start+13 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+13 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day14, \r\n" +
       " CASE WHEN @start+14 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+14 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day15, \r\n" +
       " CASE WHEN @start+15 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+15 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day16, \r\n" +
       " CASE WHEN @start+16 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+16 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day17, \r\n" +
       " CASE WHEN @start+17 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+17 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day18, \r\n" +
       " CASE WHEN @start+18 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+18 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day19, \r\n" +
       " CASE WHEN @start+19 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+19 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)  \r\n" +
       " else '' end as Day20, \r\n" +
       " CASE WHEN @start+20 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+20 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day21, \r\n" +
       " CASE WHEN @start+21 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+21 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day22, \r\n" +
       " CASE WHEN @start+22 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+22 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day23, \r\n" +
       " CASE WHEN @start+23 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+23 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day24, \r\n" +
       " CASE WHEN @start+24 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+24 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance) \r\n" +
       " else '' end as Day25,   \r\n" +
       " CASE WHEN @start+25 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+25 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)  \r\n" +
       " else '' end as Day26,  \r\n" +
       " CASE WHEN @start+26 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+26 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)  \r\n" +
       " else '' end as Day27, \r\n" +
       " CASE WHEN @start+27 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+27 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day28, \r\n" +
       " CASE WHEN @start+28 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+28 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)  \r\n" +
       " else '' end as Day29, \r\n" +
       " CASE WHEN @start+29 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+29 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day30, \r\n" +
       " CASE WHEN @start+30 = date and team in ('A','B','C', 'D', 'E') then convert(varchar(10),Hoppers)  \r\n" +
       " WHEN @start+30 = date and team not in ('A','B','C', 'D', 'E') then convert(varchar(10),Attendance)   \r\n" +
       " else '' end as Day31 \r\n" +
       " FROM [Mineware].[dbo].tbl_BCS_Tramming_Gang   \r\n" +
       " WHERE Team not in ('A','B','C','D','E','F','G') and workingorgunit = '" + Orgunitlbl.Text + "'  and yearmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editHoistMonth.EditValue)) + "'   \r\n" +
   " ) a GROUP BY  IndustryNumber, designation, team, teamgroup, typeshift, level, orgunit \r\n" +
" ) a \r\n" +
       " LEFT OUTER JOIN \r\n" +
       " ( \r\n" +
       "	SELECT nnn \r\n" +
           " ,max(Day1Clock) Day1Clock \r\n" +
           " ,max(Day2Clock) Day2Clock \r\n" +
           " ,max(Day3Clock) Day3Clock \r\n" +
           " ,max(Day4Clock) Day4Clock \r\n" +
           " ,max(Day5Clock) Day5Clock \r\n" +
           " ,max(Day6Clock) Day6Clock \r\n" +
           " ,max(Day7Clock) Day7Clock \r\n" +
           " ,max(Day8Clock) Day8Clock \r\n" +
           " ,max(Day9Clock) Day9Clock \r\n" +
           " ,max(Day10Clock) Day10Clock \r\n" +
           " ,max(Day11Clock) Day11Clock \r\n" +
           " ,max(Day12Clock) Day12Clock \r\n" +
           " ,max(Day13Clock) Day13Clock \r\n" +
           " ,max(Day14Clock) Day14Clock \r\n" +
           " ,max(Day15Clock) Day15Clock \r\n" +
           " ,max(Day16Clock) Day16Clock \r\n" +
           " ,max(Day17Clock) Day17Clock \r\n" +
           " ,max(Day18Clock) Day18Clock \r\n" +
           " ,max(Day19Clock) Day19Clock \r\n" +
           " ,max(Day20Clock) Day20Clock \r\n" +
           " ,max(Day21Clock) Day21Clock \r\n" +
           " ,max(Day22Clock) Day22Clock \r\n" +
           " ,max(Day23Clock) Day23Clock \r\n" +
           " ,max(Day24Clock) Day24Clock \r\n" +
           " ,max(Day25Clock) Day25Clock \r\n" +
           " ,max(Day26Clock) Day26Clock \r\n" +
           " ,max(Day27Clock) Day27Clock \r\n" +
           " ,max(Day28Clock) Day28Clock \r\n" +
           " ,max(Day29Clock) Day29Clock \r\n" +
           " ,max(Day30Clock) Day30Clock \r\n" +
           " ,max(Day31Clock) Day31Clock \r\n" +
           " FROM  \r\n" +
           " ( \r\n" +
           "	SELECT IndustryNumber nnn, \r\n" +
           " CASE WHEN TheDate = @start THEN expectedatwork+LeaveFlag END AS Day1Clock, \r\n" +
           " CASE WHEN TheDate = @start+1 THEN expectedatwork+LeaveFlag END AS Day2Clock, \r\n" +
           " CASE WHEN TheDate = @start+2 THEN expectedatwork+LeaveFlag END AS Day3Clock, \r\n" +
           " CASE WHEN TheDate = @start+3 THEN expectedatwork+LeaveFlag END AS Day4Clock, \r\n" +
           " CASE WHEN TheDate = @start+4 THEN expectedatwork+LeaveFlag END AS Day5Clock, \r\n" +
           " CASE WHEN TheDate = @start+5 THEN expectedatwork+LeaveFlag END AS Day6Clock, \r\n" +
           " CASE WHEN TheDate = @start+6 THEN expectedatwork+LeaveFlag END AS Day7Clock, \r\n" +
           " CASE WHEN TheDate = @start+7 THEN expectedatwork+LeaveFlag END AS Day8Clock, \r\n" +
           " CASE WHEN TheDate = @start+8 THEN expectedatwork+LeaveFlag END AS Day9Clock, \r\n" +
           " CASE WHEN TheDate = @start+9 THEN expectedatwork+LeaveFlag END AS Day10Clock, \r\n" +
           " CASE WHEN TheDate = @start+10 THEN expectedatwork+LeaveFlag END AS Day11Clock, \r\n" +
           " CASE WHEN TheDate = @start+11 THEN expectedatwork+LeaveFlag END AS Day12Clock, \r\n" +
           " CASE WHEN TheDate = @start+12 THEN expectedatwork+LeaveFlag END AS Day13Clock, \r\n" +
           " CASE WHEN TheDate = @start+13 THEN expectedatwork+LeaveFlag END AS Day14Clock, \r\n" +
           " CASE WHEN TheDate = @start+14 THEN expectedatwork+LeaveFlag END AS Day15Clock, \r\n" +
           " CASE WHEN TheDate = @start+15 THEN expectedatwork+LeaveFlag END AS Day16Clock, \r\n" +
           " CASE WHEN TheDate = @start+16 THEN expectedatwork+LeaveFlag END AS Day17Clock, \r\n" +
           " CASE WHEN TheDate = @start+17 THEN expectedatwork+LeaveFlag END AS Day18Clock, \r\n" +
           " CASE WHEN TheDate = @start+18 THEN expectedatwork+LeaveFlag END AS Day19Clock, \r\n" +
           " CASE WHEN TheDate = @start+19 THEN expectedatwork+LeaveFlag END AS Day20Clock, \r\n" +
           " CASE WHEN TheDate = @start+20 THEN expectedatwork+LeaveFlag END AS Day21Clock, \r\n" +
           " CASE WHEN TheDate = @start+21 THEN expectedatwork+LeaveFlag END AS Day22Clock, \r\n" +
           " CASE WHEN TheDate = @start+22 THEN expectedatwork+LeaveFlag END AS Day23Clock, \r\n" +
           " CASE WHEN TheDate = @start+23 THEN expectedatwork+LeaveFlag END AS Day24Clock, \r\n" +
           " CASE WHEN TheDate = @start+24 THEN expectedatwork+LeaveFlag END AS Day25Clock, \r\n" +
           " CASE WHEN TheDate = @start+25 THEN expectedatwork+LeaveFlag END AS Day26Clock, \r\n" +
           " CASE WHEN TheDate = @start+26 THEN expectedatwork+LeaveFlag END AS Day27Clock, \r\n" +
           " CASE WHEN TheDate = @start+27 THEN expectedatwork+LeaveFlag END AS Day28Clock, \r\n" +
           " CASE WHEN TheDate = @start+28 THEN expectedatwork+LeaveFlag END AS Day29Clock, \r\n" +
           " CASE WHEN TheDate = @start+29 THEN expectedatwork+LeaveFlag END AS Day30Clock, \r\n" +
           " CASE WHEN TheDate = @start+30 THEN expectedatwork+LeaveFlag END AS Day31Clock \r\n" +
           " FROM [Mineware].[dbo].[tbl_Import_BMCS_Clocking_Total] \r\n" +
           "	WHERE [TheDate] >= @start and [TheDate] <= @start +31 \r\n" +
           " ) a GROUP BY nnn \r\n" +
       " ) b ON a.IndustryNumber = b.nnn ORDER BY IndustryNumber ASC";

            _dbMan12.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan12.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan12.ExecuteInstruction();


            DataTable dt = _dbMan12.ResultsDataTable;


            DataSet ds = new DataSet();

            ds.Tables.Add(dt);

            gcOther.DataSource = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                bandedGridColumn1.FieldName = "IndustryNumber";
                bandedGridColumn2.FieldName = "designation";
                bandedGridColumn5.FieldName = "team";
                bandedGridColumn6.FieldName = "Day1";
                bandedGridColumn7.FieldName = "Day2";
                bandedGridColumn8.FieldName = "Day3";
                bandedGridColumn9.FieldName = "Day4";
                bandedGridColumn10.FieldName = "Day5";
                bandedGridColumn11.FieldName = "Day6";
                bandedGridColumn12.FieldName = "Day7";
                bandedGridColumn13.FieldName = "Day8";
                bandedGridColumn14.FieldName = "Day9";
                bandedGridColumn15.FieldName = "Day10";
                bandedGridColumn16.FieldName = "Day11";
                bandedGridColumn17.FieldName = "Day12";
                bandedGridColumn18.FieldName = "Day13";
                bandedGridColumn19.FieldName = "Day14";
                bandedGridColumn20.FieldName = "Day15";
                bandedGridColumn21.FieldName = "Day16";
                bandedGridColumn22.FieldName = "Day17";
                bandedGridColumn23.FieldName = "Day18";
                bandedGridColumn24.FieldName = "Day19";
                bandedGridColumn25.FieldName = "Day20";
                bandedGridColumn26.FieldName = "Day21";
                bandedGridColumn27.FieldName = "Day22";
                bandedGridColumn28.FieldName = "Day23";
                bandedGridColumn29.FieldName = "Day24";
                bandedGridColumn30.FieldName = "Day25";
                bandedGridColumn31.FieldName = "Day26";
                bandedGridColumn32.FieldName = "Day27";
                bandedGridColumn33.FieldName = "Day28";
                bandedGridColumn34.FieldName = "Day29";
                bandedGridColumn35.FieldName = "Day30";
                bandedGridColumn36.FieldName = "Day31";
                bandedGridColumn41.FieldName = "Day1Clock";
                bandedGridColumn42.FieldName = "Day2Clock";
                bandedGridColumn43.FieldName = "Day3Clock";
                bandedGridColumn44.FieldName = "Day4Clock";
                bandedGridColumn45.FieldName = "Day5Clock";
                bandedGridColumn46.FieldName = "Day6Clock";
                bandedGridColumn47.FieldName = "Day7Clock";
                bandedGridColumn48.FieldName = "Day8Clock";
                bandedGridColumn49.FieldName = "Day9Clock";
                bandedGridColumn50.FieldName = "Day10Clock";
                bandedGridColumn51.FieldName = "Day11Clock";
                bandedGridColumn52.FieldName = "Day12Clock";
                bandedGridColumn53.FieldName = "Day13Clock";
                bandedGridColumn54.FieldName = "Day14Clock";
                bandedGridColumn55.FieldName = "Day15Clock";
                bandedGridColumn56.FieldName = "Day16Clock";
                bandedGridColumn57.FieldName = "Day17Clock";
                bandedGridColumn58.FieldName = "Day18Clock";
                bandedGridColumn59.FieldName = "Day19Clock";
                bandedGridColumn60.FieldName = "Day20Clock";
                bandedGridColumn61.FieldName = "Day21Clock";
                bandedGridColumn62.FieldName = "Day22Clock";
                bandedGridColumn63.FieldName = "Day23Clock";
                bandedGridColumn64.FieldName = "Day24Clock";
                bandedGridColumn65.FieldName = "Day25Clock";
                bandedGridColumn66.FieldName = "Day26Clock";
                bandedGridColumn67.FieldName = "Day27Clock";
                bandedGridColumn68.FieldName = "Day28Clock";
                bandedGridColumn69.FieldName = "Day29Clock";
                bandedGridColumn70.FieldName = "Day30Clock";
                bandedGridColumn71.FieldName = "Day31Clock";

                gvOther.Columns[3].Visible = true;
                gvOther.Columns[4].Visible = true;
                gvOther.Columns[5].Visible = true;
                gvOther.Columns[6].Visible = true;
                gvOther.Columns[7].Visible = true;
                gvOther.Columns[8].Visible = true;
                gvOther.Columns[9].Visible = true;
                gvOther.Columns[10].Visible = true;
                gvOther.Columns[11].Visible = true;
                gvOther.Columns[12].Visible = true;
                gvOther.Columns[13].Visible = true;
                gvOther.Columns[14].Visible = true;
                gvOther.Columns[15].Visible = true;
                gvOther.Columns[16].Visible = true;
                gvOther.Columns[17].Visible = true;
                gvOther.Columns[18].Visible = true;
                gvOther.Columns[19].Visible = true;
                gvOther.Columns[20].Visible = true;
                gvOther.Columns[21].Visible = true;
                gvOther.Columns[22].Visible = true;
                gvOther.Columns[23].Visible = true;
                gvOther.Columns[24].Visible = true;
                gvOther.Columns[25].Visible = true;
                gvOther.Columns[26].Visible = true;
                gvOther.Columns[27].Visible = true;
                gvOther.Columns[28].Visible = true;
                gvOther.Columns[29].Visible = true;
                gvOther.Columns[30].Visible = true;
                gvOther.Columns[31].Visible = true;
                gvOther.Columns[32].Visible = true;
                gvOther.Columns[33].Visible = true;

            }

            gvOther.Columns[3].Caption = startDate.ToString("dd  MMM   ddd");
            gvOther.Columns[4].Caption = startDate.AddDays(1).ToString("dd  MMM   ddd");
            gvOther.Columns[5].Caption = startDate.AddDays(2).ToString("dd  MMM   ddd");
            gvOther.Columns[6].Caption = startDate.AddDays(3).ToString("dd  MMM   ddd");
            gvOther.Columns[7].Caption = startDate.AddDays(4).ToString("dd  MMM   ddd");
            gvOther.Columns[8].Caption = startDate.AddDays(5).ToString("dd  MMM   ddd");
            gvOther.Columns[9].Caption = startDate.AddDays(6).ToString("dd  MMM   ddd");
            gvOther.Columns[10].Caption = startDate.AddDays(7).ToString("dd  MMM   ddd");
            gvOther.Columns[11].Caption = startDate.AddDays(8).ToString("dd  MMM   ddd");
            gvOther.Columns[12].Caption = startDate.AddDays(9).ToString("dd  MMM   ddd");
            gvOther.Columns[13].Caption = startDate.AddDays(10).ToString("dd  MMM   ddd");
            gvOther.Columns[14].Caption = startDate.AddDays(11).ToString("dd  MMM   ddd");
            gvOther.Columns[15].Caption = startDate.AddDays(12).ToString("dd  MMM   ddd");
            gvOther.Columns[16].Caption = startDate.AddDays(13).ToString("dd  MMM   ddd");
            gvOther.Columns[17].Caption = startDate.AddDays(14).ToString("dd  MMM   ddd");
            gvOther.Columns[18].Caption = startDate.AddDays(15).ToString("dd  MMM   ddd");
            gvOther.Columns[19].Caption = startDate.AddDays(16).ToString("dd  MMM   ddd");
            gvOther.Columns[20].Caption = startDate.AddDays(17).ToString("dd  MMM   ddd");
            gvOther.Columns[21].Caption = startDate.AddDays(18).ToString("dd  MMM   ddd");
            gvOther.Columns[22].Caption = startDate.AddDays(19).ToString("dd  MMM   ddd");
            gvOther.Columns[23].Caption = startDate.AddDays(20).ToString("dd  MMM   ddd");
            gvOther.Columns[24].Caption = startDate.AddDays(21).ToString("dd  MMM   ddd");
            gvOther.Columns[25].Caption = startDate.AddDays(22).ToString("dd  MMM   ddd");
            gvOther.Columns[26].Caption = startDate.AddDays(23).ToString("dd  MMM   ddd");
            gvOther.Columns[27].Caption = startDate.AddDays(24).ToString("dd  MMM   ddd");
            gvOther.Columns[28].Caption = startDate.AddDays(25).ToString("dd  MMM   ddd");
            gvOther.Columns[29].Caption = startDate.AddDays(26).ToString("dd  MMM   ddd");
            gvOther.Columns[30].Caption = startDate.AddDays(27).ToString("dd  MMM   ddd");
            gvOther.Columns[31].Caption = startDate.AddDays(28).ToString("dd  MMM   ddd");
            gvOther.Columns[32].Caption = startDate.AddDays(29).ToString("dd  MMM   ddd");
            gvOther.Columns[33].Caption = startDate.AddDays(30).ToString("dd  MMM   ddd");


            if (_dbMan12.ResultsDataTable.Rows.Count != 0)
            {
                for (int i = 0; i < gvOther.Columns.Count; i++)
                {
                    var result = String.Format("{0:dd  MMM   ddd}", EndDate.Value);
                    if (gvOther.Columns[i].Caption == result)
                    {
                        var removestr = gvOther.Columns[i].FieldName.ToString().Substring(3, 2);
                        int remove = Convert.ToInt32(removestr) + 1;
                        for (int re = remove; re < 32; re++)
                        {
                            gvOther.Columns["Day" + re].Visible = false;

                        }
                    }
                }
            }
            this.Cursor = Cursors.Default;
            this.UseWaitCursor = false;
            return;
        }

        void LoadDataTram()
        {


            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan1.SqlStatement = " select Level, orgunit from tbl_BCS_Tramming_Levels where YearMonth >= year(getdate()-90)*100+month(getdate()-90) group by  Level, orgunit order by convert(decimal(18,0),substring(level,6,2)), orgunit   ";
            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();

            DataTable dt3 = _dbMan1.ResultsDataTable;

            foreach (DataRow r in dt3.Rows)
            {
                LevelCombo.Items.Add(r["Level"]);
            }

            LVLTreeView.Nodes.Clear();


            string Lvl = "";
            string Org = "";
            string Ring1 = "";
            string Hole = "";

            for (int i = 0; i < dt3.Rows.Count; i++)
            {
                if (Lvl != dt3.Rows[i]["Level"].ToString())
                {
                    TreeNode node = new TreeNode(dt3.Rows[i]["Level"].ToString());
                    node.NodeFont = new Font("Microsoft Sans Serif", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                    //node.ForeColor = Color.DimGray;
                    Org = "";
                    for (int child = 0; child < dt3.Rows.Count; child++)
                    {
                        if (dt3.Rows[child]["Level"].ToString() == dt3.Rows[i]["Level"].ToString())
                        {
                            if (Org != dt3.Rows[child]["orgunit"].ToString())
                            {

                                Org = dt3.Rows[child]["orgunit"].ToString();

                                TreeNode childNode = new TreeNode(Org);
                                childNode.NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular, GraphicsUnit.Pixel);
                                childNode.ForeColor = Color.DimGray;

                                node.Nodes.Add(childNode);
                            }
                            Org = dt3.Rows[child]["orgunit"].ToString();

                        }
                    }

                    LVLTreeView.Nodes.Add(node);
                }

                Lvl = dt3.Rows[i]["Level"].ToString();



            }



        }

        private void bandedGridView6_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView View = sender as GridView;

            //Day 1
            var wd1 = "";
            var wd1clock = "";
            //Day 2
            var wd2 = "";
            var wd2clock = "";
            //Day3
            var wd3 = "";
            var wd3clock = "";
            //Day4
            var wd4 = "";
            var wd4clock = "";
            //Day5
            var wd5 = "";
            var wd5clock = "";
            //Day6
            var wd6 = "";
            var wd6clock = "";
            //Day7
            var wd7 = "";
            var wd7clock = "";
            //Day8
            var wd8 = "";
            var wd8clock = "";
            //Day9
            var wd9 = "";
            var wd9clock = "";
            //Day10
            var wd10 = "";
            var wd10clock = "";
            //Day11
            var wd11 = "";
            var wd11clock = "";
            //Day12
            var wd12 = "";
            var wd12clock = "";
            //Day13
            var wd13 = "";
            var wd13clock = "";
            //Day14
            var wd14 = "";
            var wd14clock = "";
            //Day15
            var wd15 = "";
            var wd15clock = "";
            //Day16
            var wd16 = "";
            var wd16clock = "";
            //Day17
            var wd17 = "";
            var wd17clock = "";
            //Day18
            var wd18 = "";
            var wd18clock = "";
            //Day19
            var wd19 = "";
            var wd19clock = "";
            //Day20
            var wd20 = "";
            var wd20clock = "";
            //Day21
            var wd21 = "";
            var wd21clock = "";
            //Day22
            var wd22 = "";
            var wd22clock = "";
            //Day23
            var wd23 = "";
            var wd23clock = "";
            //Day24
            var wd24 = "";
            var wd24clock = "";
            //Day25
            var wd25 = "";
            var wd25clock = "";
            //Day26
            var wd26 = "";
            var wd26clock = "";
            //Day27
            var wd27 = "";
            var wd27clock = "";
            //Day28
            var wd28 = "";
            var wd28clock = "";
            //Day29
            var wd29 = "";
            var wd29clock = "";
            //Day30
            var wd30 = "";
            var wd30clock = "";
            //Day31
            var wd31 = "";
            var wd31clock = "";

            //incase null Add blanks
            //Day1
            wd1 = View.GetRowCellValue(e.RowHandle, "Day1Clock").ToString() + "                                             ";

            wd1clock = View.GetRowCellValue(e.RowHandle, "Day1Clock").ToString() + "                                        ";
            //Day2
            wd2 = View.GetRowCellValue(e.RowHandle, "Day2Clock").ToString() + "                                             ";

            wd2clock = View.GetRowCellValue(e.RowHandle, "Day2Clock").ToString() + "                                        ";
            //Day3
            wd3 = View.GetRowCellValue(e.RowHandle, "Day3Clock").ToString() + "                                             ";

            wd3clock = View.GetRowCellValue(e.RowHandle, "Day3Clock").ToString() + "                                        ";
            //Day4
            wd4 = View.GetRowCellValue(e.RowHandle, "Day4Clock").ToString() + "                                             ";

            wd4clock = View.GetRowCellValue(e.RowHandle, "Day4Clock").ToString() + "                                        ";
            //Day5
            wd5 = View.GetRowCellValue(e.RowHandle, "Day5Clock").ToString() + "                                             ";

            wd5clock = View.GetRowCellValue(e.RowHandle, "Day5Clock").ToString() + "                                        ";
            //Day6
            wd6 = View.GetRowCellValue(e.RowHandle, "Day6Clock").ToString() + "                                             ";

            wd6clock = View.GetRowCellValue(e.RowHandle, "Day6Clock").ToString() + "                                        ";
            //Day7
            wd7 = View.GetRowCellValue(e.RowHandle, "Day7Clock").ToString() + "                                             ";

            wd7clock = View.GetRowCellValue(e.RowHandle, "Day7Clock").ToString() + "                                        ";
            //Day8
            wd8 = View.GetRowCellValue(e.RowHandle, "Day8Clock").ToString() + "                                             ";

            wd8clock = View.GetRowCellValue(e.RowHandle, "Day8Clock").ToString() + "                                        ";

            //Day9
            wd9 = View.GetRowCellValue(e.RowHandle, "Day9Clock").ToString() + "                                             ";

            wd9clock = View.GetRowCellValue(e.RowHandle, "Day9Clock").ToString() + "                                        ";
            //Day10
            wd10 = View.GetRowCellValue(e.RowHandle, "Day10Clock").ToString() + "                                             ";

            wd10clock = View.GetRowCellValue(e.RowHandle, "Day10Clock").ToString() + "                                        ";
            //Day11
            wd11 = View.GetRowCellValue(e.RowHandle, "Day11Clock").ToString() + "                                             ";

            wd11clock = View.GetRowCellValue(e.RowHandle, "Day11Clock").ToString() + "                                        ";
            //Day12
            wd12 = View.GetRowCellValue(e.RowHandle, "Day12Clock").ToString() + "                                             ";

            wd12clock = View.GetRowCellValue(e.RowHandle, "Day12Clock").ToString() + "                                        ";
            //Day13
            wd13 = View.GetRowCellValue(e.RowHandle, "Day13Clock").ToString() + "                                             ";

            wd13clock = View.GetRowCellValue(e.RowHandle, "Day13Clock").ToString() + "                                        ";
            //Day14
            wd14 = View.GetRowCellValue(e.RowHandle, "Day14Clock").ToString() + "                                             ";
            wd14 = wd14.Substring(0, 1);

            wd14clock = View.GetRowCellValue(e.RowHandle, "Day14Clock").ToString() + "                                        ";
            //Day15
            wd15 = View.GetRowCellValue(e.RowHandle, "Day15Clock").ToString() + "                                             ";

            wd15clock = View.GetRowCellValue(e.RowHandle, "Day15Clock").ToString() + "                                        ";
            //Day16
            wd16 = View.GetRowCellValue(e.RowHandle, "Day16Clock").ToString() + "                                             ";

            wd16clock = View.GetRowCellValue(e.RowHandle, "Day16Clock").ToString() + "                                        ";
            //Day17
            wd17 = View.GetRowCellValue(e.RowHandle, "Day17Clock").ToString() + "                                             ";

            wd17clock = View.GetRowCellValue(e.RowHandle, "Day17Clock").ToString() + "                                        ";
            //Day18
            wd18 = View.GetRowCellValue(e.RowHandle, "Day18Clock").ToString() + "                                             ";

            wd18clock = View.GetRowCellValue(e.RowHandle, "Day18Clock").ToString() + "                                        ";
            //Day19
            wd19 = View.GetRowCellValue(e.RowHandle, "Day19Clock").ToString() + "                                             ";

            wd19clock = View.GetRowCellValue(e.RowHandle, "Day19Clock").ToString() + "                                        ";
            //Day20
            wd20 = View.GetRowCellValue(e.RowHandle, "Day20Clock").ToString() + "                                             ";

            wd20clock = View.GetRowCellValue(e.RowHandle, "Day20Clock").ToString() + "                                        ";
            //Day21
            wd21 = View.GetRowCellValue(e.RowHandle, "Day21Clock").ToString() + "                                             ";

            wd21clock = View.GetRowCellValue(e.RowHandle, "Day21Clock").ToString() + "                                        ";
            //Day22
            wd22 = View.GetRowCellValue(e.RowHandle, "Day22Clock").ToString() + "                                             ";

            wd22clock = View.GetRowCellValue(e.RowHandle, "Day22Clock").ToString() + "                                        ";
            //Day23
            wd23 = View.GetRowCellValue(e.RowHandle, "Day23Clock").ToString() + "                                             ";

            wd23clock = View.GetRowCellValue(e.RowHandle, "Day23Clock").ToString() + "                                        ";
            //Day24
            wd24 = View.GetRowCellValue(e.RowHandle, "Day24Clock").ToString() + "                                             ";

            wd24clock = View.GetRowCellValue(e.RowHandle, "Day24Clock").ToString() + "                                        ";
            //Day25
            wd25 = View.GetRowCellValue(e.RowHandle, "Day25Clock").ToString() + "                                             ";

            wd25clock = View.GetRowCellValue(e.RowHandle, "Day25Clock").ToString() + "                                        ";
            //Day26
            wd26 = View.GetRowCellValue(e.RowHandle, "Day26Clock").ToString() + "                                             ";

            wd26clock = View.GetRowCellValue(e.RowHandle, "Day26Clock").ToString() + "                                        ";
            //Day27
            wd27 = View.GetRowCellValue(e.RowHandle, "Day27Clock").ToString() + "                                             ";

            wd27clock = View.GetRowCellValue(e.RowHandle, "Day27Clock").ToString() + "                                        ";
            //Day28
            wd28 = View.GetRowCellValue(e.RowHandle, "Day28Clock").ToString() + "                                             ";

            wd28clock = View.GetRowCellValue(e.RowHandle, "Day28Clock").ToString() + "                                        ";
            //Day29
            wd29 = View.GetRowCellValue(e.RowHandle, "Day29Clock").ToString() + "                                             ";

            wd29clock = View.GetRowCellValue(e.RowHandle, "Day29Clock").ToString() + "                                        ";
            //Day30
            wd30 = View.GetRowCellValue(e.RowHandle, "Day30Clock").ToString() + "                                             ";

            wd30clock = View.GetRowCellValue(e.RowHandle, "Day30Clock").ToString() + "                                        ";
            //Day31
            wd31 = View.GetRowCellValue(e.RowHandle, "Day31Clock").ToString() + "                                             ";

            wd31clock = View.GetRowCellValue(e.RowHandle, "Day31Clock").ToString() + "                                        ";

            if (e.Column.AbsoluteIndex == 3)
            {
                if (wd1clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd1clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd1clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);

                }
            }

            if (e.Column.AbsoluteIndex == 4)
            {
                if (wd2clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd2clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd2clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 5)
            {
                if (wd3clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd3clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd3clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 6)
            {
                if (wd4clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd4clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd4clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 7)
            {
                if (wd5clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd5clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd5clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 8)
            {
                if (wd6clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd6clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd1clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 9)
            {
                if (wd7clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd7clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd7clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 10)
            {
                if (wd8clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd8clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd8clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 11)
            {
                if (wd9clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd9clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd9clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 12)
            {
                if (wd10clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd10clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd10clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 13)
            {
                if (wd11clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd11clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd11clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 14)
            {
                if (wd12clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd12clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd12clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 15)
            {
                if (wd13clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd13clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd13clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 16)
            {
                if (wd14clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd14clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd14clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 17)
            {
                if (wd15clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd15clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd15clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 18)
            {
                if (wd16clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd16clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd16clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 19)
            {
                if (wd17clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd17clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd17clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 20)
            {
                if (wd18clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd18clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd18clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 21)
            {
                if (wd19clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd19clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd19clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 22)
            {
                if (wd20clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd20clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd20clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 23)
            {
                if (wd21clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd21clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd21clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 24)
            {
                if (wd22clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd22clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd22clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 25)
            {
                if (wd23clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd23clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd23clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 26)
            {
                if (wd24clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd24clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd24clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 27)
            {
                if (wd25clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd25clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd25clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 28)
            {
                if (wd26clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd26clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd26clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 29)
            {
                if (wd27clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd27clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd27clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 30)
            {
                if (wd28clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd28clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd28clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 31)
            {
                if (wd29clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd29clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd29clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 32)
            {
                if (wd30clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd30clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd30clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 33)
            {
                if (wd31clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd31clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd31clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            //Day1
            //if (e.Column.AbsoluteIndex == 3)
            //{
            //    e.Appearance.BackColor = Color.White;
            //    if (wd1 == "N ")
            //    {
            //        e.Appearance.BackColor = Color.Gainsboro;
            //        e.DisplayText = wd1clock;

            //    }
            //    else
            //    {
            //        if (wd1clock.Trim() != "N ")
            //        {
            //            e.DisplayText = wd1clock;
            //            e.Appearance.ForeColor = Color.Gainsboro;
            //        }
            //    }
            //}
            ////Day2
            //if (e.Column.AbsoluteIndex == 4)
            //{
            //    e.Appearance.BackColor = Color.White;
            //    if (wd2 == "N ")
            //    {
            //        e.Appearance.BackColor = Color.Gainsboro;
            //        e.DisplayText = wd2clock;

            //    }
            //    else
            //    {
            //        if (wd2clock.Trim() != "N ")
            //        {
            //            e.DisplayText = wd2clock;
            //            e.Appearance.ForeColor = Color.Gainsboro;
            //        }
            //    }
            //}
            ////Day3
            //if (e.Column.AbsoluteIndex == 5) { e.Appearance.BackColor = Color.White; if (wd3 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd3clock; } else { if (wd3clock.Trim() != "N ") { e.DisplayText = wd3clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day4
            //if (e.Column.AbsoluteIndex == 6) { e.Appearance.BackColor = Color.White; if (wd4 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd4clock; } else { if (wd4clock.Trim() != "N ") { e.DisplayText = wd4clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day5
            //if (e.Column.AbsoluteIndex == 7) { e.Appearance.BackColor = Color.White; if (wd5 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd5clock; } else { if (wd5clock.Trim() != "N ") { e.DisplayText = wd5clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day6
            //if (e.Column.AbsoluteIndex == 8) { e.Appearance.BackColor = Color.White; if (wd6 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd6clock; } else { if (wd6clock.Trim() != "N ") { e.DisplayText = wd6clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day7
            //if (e.Column.AbsoluteIndex == 9) { e.Appearance.BackColor = Color.White; if (wd7 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd7clock; } else { if (wd7clock.Trim() != "N ") { e.DisplayText = wd7clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day8
            //if (e.Column.AbsoluteIndex == 10) { e.Appearance.BackColor = Color.White; if (wd8 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd8clock; } else { if (wd8clock.Trim() != "N ") { e.DisplayText = wd8clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day9
            //if (e.Column.AbsoluteIndex == 11) { e.Appearance.BackColor = Color.White; if (wd9 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd9clock; } else { if (wd9clock.Trim() != "N ") { e.DisplayText = wd9clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day10
            //if (e.Column.AbsoluteIndex == 12) { e.Appearance.BackColor = Color.White; if (wd10 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd10clock; } else { if (wd10clock.Trim() != "N ") { e.DisplayText = wd10clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day11
            //if (e.Column.AbsoluteIndex == 13) { e.Appearance.BackColor = Color.White; if (wd11 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd11clock; } else { if (wd11clock.Trim() != "N ") { e.DisplayText = wd11clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day12
            //if (e.Column.AbsoluteIndex == 14) { e.Appearance.BackColor = Color.White; if (wd12 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd12clock; } else { if (wd12clock.Trim() != "N ") { e.DisplayText = wd12clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day13
            //if (e.Column.AbsoluteIndex == 15) { e.Appearance.BackColor = Color.White; if (wd13 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd13clock; } else { if (wd13clock.Trim() != "N ") { e.DisplayText = wd13clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day14
            //if (e.Column.AbsoluteIndex == 16) { e.Appearance.BackColor = Color.White; if (wd14 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd14clock; } else { if (wd14clock.Trim() != "N ") { e.DisplayText = wd14clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day15
            //if (e.Column.AbsoluteIndex == 17) { e.Appearance.BackColor = Color.White; if (wd15 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd15clock; } else { if (wd15clock.Trim() != "N ") { e.DisplayText = wd15clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day16
            //if (e.Column.AbsoluteIndex == 18) { e.Appearance.BackColor = Color.White; if (wd16 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd16clock; } else { if (wd16clock.Trim() != "N ") { e.DisplayText = wd16clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day17
            //if (e.Column.AbsoluteIndex == 19) { e.Appearance.BackColor = Color.White; if (wd17 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd17clock; } else { if (wd17clock.Trim() != "N ") { e.DisplayText = wd17clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day18
            //if (e.Column.AbsoluteIndex == 20) { e.Appearance.BackColor = Color.White; if (wd18 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd18clock; } else { if (wd18clock.Trim() != "N ") { e.DisplayText = wd18clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day19
            //if (e.Column.AbsoluteIndex == 21) { e.Appearance.BackColor = Color.White; if (wd19 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd19clock; } else { if (wd19clock.Trim() != "N ") { e.DisplayText = wd19clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day20
            //if (e.Column.AbsoluteIndex == 22) { e.Appearance.BackColor = Color.White; if (wd20 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd20clock; } else { if (wd20clock.Trim() != "N ") { e.DisplayText = wd20clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day21
            //if (e.Column.AbsoluteIndex == 23) { e.Appearance.BackColor = Color.White; if (wd21 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd21clock; } else { if (wd21clock.Trim() != "N ") { e.DisplayText = wd21clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day22
            //if (e.Column.AbsoluteIndex == 24) { e.Appearance.BackColor = Color.White; if (wd22 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd22clock; } else { if (wd22clock.Trim() != "N ") { e.DisplayText = wd22clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day23
            //if (e.Column.AbsoluteIndex == 25) { e.Appearance.BackColor = Color.White; if (wd23 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd23clock; } else { if (wd23clock.Trim() != "N ") { e.DisplayText = wd23clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day24
            //if (e.Column.AbsoluteIndex == 26) { e.Appearance.BackColor = Color.White; if (wd24 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd24clock; } else { if (wd24clock.Trim() != "N ") { e.DisplayText = wd24clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day25
            //if (e.Column.AbsoluteIndex == 27) { e.Appearance.BackColor = Color.White; if (wd25 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd25clock; } else { if (wd25clock.Trim() != "N ") { e.DisplayText = wd25clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day26
            //if (e.Column.AbsoluteIndex == 28) { e.Appearance.BackColor = Color.White; if (wd26 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd26clock; } else { if (wd26clock.Trim() != "N ") { e.DisplayText = wd26clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day27
            //if (e.Column.AbsoluteIndex == 29) { e.Appearance.BackColor = Color.White; if (wd27 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd27clock; } else { if (wd27clock.Trim() != "N ") { e.DisplayText = wd27clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day28
            //if (e.Column.AbsoluteIndex == 30) { e.Appearance.BackColor = Color.White; if (wd28 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd28clock; } else { if (wd28clock.Trim() != "N ") { e.DisplayText = wd28clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day29
            //if (e.Column.AbsoluteIndex == 31) { e.Appearance.BackColor = Color.White; if (wd29 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd29clock; } else { if (wd29clock.Trim() != "N ") { e.DisplayText = wd29clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day30
            //if (e.Column.AbsoluteIndex == 32) { e.Appearance.BackColor = Color.White; if (wd30 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd30clock; } else { if (wd30clock.Trim() != "N ") { e.DisplayText = wd30clock; e.Appearance.ForeColor = Color.Gainsboro; } } }
            ////Day31
            //if (e.Column.AbsoluteIndex == 33) { e.Appearance.BackColor = Color.White; if (wd31 == "N ") { e.Appearance.BackColor = Color.Gainsboro; e.DisplayText = wd31clock; } else { if (wd31clock.Trim() != "N ") { e.DisplayText = wd31clock; e.Appearance.ForeColor = Color.Gainsboro; } } }

            //if (gvProdMineOther.GetFocusedValue().ToString() == "S ")
            //{
            //    MessageBox.Show("Test");
            //}
            //Day 1
            var D1 = "";
            var DD1 = "";
            //Day 2
            var D2 = "";
            var DD2 = "";
            //Day 3
            var D3 = "";
            var DD3 = "";
            //Day 4
            var D4 = "";
            var DD4 = "";
            //Day 5
            var D5 = "";
            var DD5 = "";
            //Day 6
            var D6 = "";
            var DD6 = "";
            //Day 7
            var D7 = "";
            var DD7 = "";
            //Day 8
            var D8 = "";
            var DD8 = "";
            //Day 9 
            var D9 = "";
            var DD9 = "";
            //Day 10 
            var D10 = "";
            var DD10 = "";
            //Day 11 
            var D11 = "";
            var DD11 = "";
            //Day 12
            var D12 = "";
            var DD12 = "";
            //Day 16
            var D13 = "";
            var DD13 = "";
            //Day 14
            var D14 = "";
            var DD14 = "";
            //Day 15
            var D15 = "";
            var DD15 = "";
            //Day 16
            var D16 = "";
            var DD16 = "";
            //Day 17
            var D17 = "";
            var DD17 = "";
            //Day 18
            var D18 = "";
            var DD18 = "";
            //Day 19
            var D19 = "";
            var DD19 = "";
            //Day 20
            var D20 = "";
            var DD20 = "";
            //Day 21
            var D21 = "";
            var DD21 = "";
            //Day 22
            var D22 = "";
            var DD22 = "";
            //Day 23
            var D23 = "";
            var DD23 = "";
            //Day 24
            var D24 = "";
            var DD24 = "";
            //Day 25
            var D25 = "";
            var DD25 = "";
            //Day 26
            var D26 = "";
            var DD26 = "";
            //Day 27
            var D27 = "";
            var DD27 = "";
            //Day 28
            var D28 = "";
            var DD28 = "";
            //Day 29
            var D29 = "";
            var DD29 = "";
            //Day 30
            var D30 = "";
            var DD30 = "";
            //Day 31
            var D31 = "";
            var DD31 = "";

            //Day 1

            D1 = View.GetRowCellValue(e.RowHandle, "Day1").ToString() + "                                             ";
            D1 = D1.Substring(0, 1);

            DD1 = View.GetRowCellValue(e.RowHandle, "Day1Clock").ToString() + "                                             ";
            DD1 = DD1.Substring(0, 2);

            //Day 2

            D2 = View.GetRowCellValue(e.RowHandle, "Day2").ToString() + "                                             ";
            D2 = D2.Substring(0, 1);

            DD2 = View.GetRowCellValue(e.RowHandle, "Day2Clock").ToString() + "                                             ";
            DD2 = DD2.Substring(0, 2);

            //Day 3

            D3 = View.GetRowCellValue(e.RowHandle, "Day3").ToString() + "                                             ";
            D3 = D3.Substring(0, 1);

            DD3 = View.GetRowCellValue(e.RowHandle, "Day3Clock").ToString() + "                                             ";
            DD3 = DD3.Substring(0, 2);

            //Day 4

            D4 = View.GetRowCellValue(e.RowHandle, "Day4").ToString() + "                                             ";
            D4 = D4.Substring(0, 1);

            DD4 = View.GetRowCellValue(e.RowHandle, "Day4Clock").ToString() + "                                             ";
            DD4 = DD4.Substring(0, 2);

            //Day 5

            D5 = View.GetRowCellValue(e.RowHandle, "Day5").ToString() + "                                             ";
            D5 = D5.Substring(0, 1);

            DD5 = View.GetRowCellValue(e.RowHandle, "Day5Clock").ToString() + "                                             ";
            DD5 = DD5.Substring(0, 2);

            //Day 6

            D6 = View.GetRowCellValue(e.RowHandle, "Day6").ToString() + "                                             ";
            D6 = D6.Substring(0, 1);

            DD6 = View.GetRowCellValue(e.RowHandle, "Day6Clock").ToString() + "                                             ";
            DD6 = DD6.Substring(0, 2);

            //Day 7

            D7 = View.GetRowCellValue(e.RowHandle, "Day7").ToString() + "                                             ";
            D7 = D7.Substring(0, 1);

            DD7 = View.GetRowCellValue(e.RowHandle, "Day7Clock").ToString() + "                                             ";
            DD7 = DD7.Substring(0, 2);

            //Day 8

            D8 = View.GetRowCellValue(e.RowHandle, "Day8").ToString() + "                                             ";
            D8 = D8.Substring(0, 1);

            DD8 = View.GetRowCellValue(e.RowHandle, "Day8Clock").ToString() + "                                             ";
            DD8 = DD8.Substring(0, 2);

            //Day 9

            D9 = View.GetRowCellValue(e.RowHandle, "Day9").ToString() + "                                             ";
            D9 = D9.Substring(0, 1);

            DD9 = View.GetRowCellValue(e.RowHandle, "Day9Clock").ToString() + "                                             ";
            DD9 = DD9.Substring(0, 2);

            //Day 10

            D10 = View.GetRowCellValue(e.RowHandle, "Day10").ToString() + "                                             ";
            D10 = D10.Substring(0, 1);

            DD10 = View.GetRowCellValue(e.RowHandle, "Day10Clock").ToString() + "                                             ";
            DD10 = DD10.Substring(0, 2);

            //Day 11

            D11 = View.GetRowCellValue(e.RowHandle, "Day11").ToString() + "                                             ";
            D11 = D11.Substring(0, 1);

            DD11 = View.GetRowCellValue(e.RowHandle, "Day11Clock").ToString() + "                                             ";
            DD11 = DD11.Substring(0, 2);

            //Day12

            D12 = View.GetRowCellValue(e.RowHandle, "Day12").ToString() + "                                ";
            D12 = D12.Substring(0, 1);

            DD12 = View.GetRowCellValue(e.RowHandle, "Day12Clock").ToString() + "                                             ";
            DD12 = DD12.Substring(0, 2);

            //Day13

            D13 = View.GetRowCellValue(e.RowHandle, "Day13").ToString() + "                                ";
            D13 = D13.Substring(0, 1);

            DD13 = View.GetRowCellValue(e.RowHandle, "Day13Clock").ToString() + "                                             ";
            DD13 = DD13.Substring(0, 2);

            //Day14

            D14 = View.GetRowCellValue(e.RowHandle, "Day14").ToString() + "                                ";
            D14 = D14.Substring(0, 1);

            DD14 = View.GetRowCellValue(e.RowHandle, "Day14Clock").ToString() + "                                             ";
            DD14 = DD14.Substring(0, 2);

            //Day15

            D15 = View.GetRowCellValue(e.RowHandle, "Day15").ToString() + "                                ";
            D15 = D15.Substring(0, 1);

            DD15 = View.GetRowCellValue(e.RowHandle, "Day15Clock").ToString() + "                                             ";
            DD15 = DD15.Substring(0, 2);

            //Day16

            D16 = View.GetRowCellValue(e.RowHandle, "Day16").ToString() + "                                ";
            D16 = D16.Substring(0, 1);

            DD16 = View.GetRowCellValue(e.RowHandle, "Day16Clock").ToString() + "                                             ";
            DD16 = DD16.Substring(0, 2);

            //Day17

            D17 = View.GetRowCellValue(e.RowHandle, "Day17").ToString() + "                                ";
            D17 = D17.Substring(0, 3);

            DD17 = View.GetRowCellValue(e.RowHandle, "Day17Clock").ToString() + "                                             ";
            DD17 = DD17.Substring(0, 2);

            //Day18

            D18 = View.GetRowCellValue(e.RowHandle, "Day18").ToString() + "                                ";
            D18 = D18.Substring(0, 1);

            DD18 = View.GetRowCellValue(e.RowHandle, "Day18Clock").ToString() + "                                             ";
            DD18 = DD18.Substring(0, 2);

            //Day19

            D19 = View.GetRowCellValue(e.RowHandle, "Day19").ToString() + "                                ";
            D19 = D19.Substring(0, 1);

            DD19 = View.GetRowCellValue(e.RowHandle, "Day19Clock").ToString() + "                                             ";
            DD19 = DD19.Substring(0, 2);

            //Day20

            D20 = View.GetRowCellValue(e.RowHandle, "Day20").ToString() + "                                ";
            D20 = D20.Substring(0, 1);

            DD20 = View.GetRowCellValue(e.RowHandle, "Day20Clock").ToString() + "                                             ";
            DD20 = DD20.Substring(0, 2);

            //Day21

            D21 = View.GetRowCellValue(e.RowHandle, "Day21").ToString() + "                                ";
            D21 = D21.Substring(0, 1);

            DD21 = View.GetRowCellValue(e.RowHandle, "Day21Clock").ToString() + "                                             ";
            DD21 = DD21.Substring(0, 2);

            //Day22

            D22 = View.GetRowCellValue(e.RowHandle, "Day22").ToString() + "                                ";
            D22 = D22.Substring(0, 1);

            DD22 = View.GetRowCellValue(e.RowHandle, "Day22Clock").ToString() + "                                             ";
            DD22 = DD22.Substring(0, 2);

            //Day23

            D23 = View.GetRowCellValue(e.RowHandle, "Day23").ToString() + "                                ";
            D23 = D23.Substring(0, 1);

            DD23 = View.GetRowCellValue(e.RowHandle, "Day23Clock").ToString() + "                                             ";
            DD23 = DD23.Substring(0, 2);

            //Day24

            D24 = View.GetRowCellValue(e.RowHandle, "Day24").ToString() + "                                ";
            D24 = D24.Substring(0, 1);

            DD24 = View.GetRowCellValue(e.RowHandle, "Day24Clock").ToString() + "                                             ";
            DD24 = DD24.Substring(0, 2);

            //Day25

            D25 = View.GetRowCellValue(e.RowHandle, "Day25").ToString() + "                                ";
            D25 = D25.Substring(0, 1);

            DD25 = View.GetRowCellValue(e.RowHandle, "Day25Clock").ToString() + "                                             ";
            DD25 = DD25.Substring(0, 2);

            //Day26

            D26 = View.GetRowCellValue(e.RowHandle, "Day26").ToString() + "                                ";
            D26 = D26.Substring(0, 1);

            DD26 = View.GetRowCellValue(e.RowHandle, "Day26Clock").ToString() + "                                             ";
            DD26 = DD26.Substring(0, 2);

            //Day27

            D27 = View.GetRowCellValue(e.RowHandle, "Day27").ToString() + "                                ";
            D27 = D27.Substring(0, 1);

            DD27 = View.GetRowCellValue(e.RowHandle, "Day27Clock").ToString() + "                                             ";
            DD27 = DD27.Substring(0, 2);

            //Day28

            D28 = View.GetRowCellValue(e.RowHandle, "Day28").ToString() + "                                ";
            D28 = D28.Substring(0, 1);

            DD28 = View.GetRowCellValue(e.RowHandle, "Day28Clock").ToString() + "                                             ";
            DD28 = DD28.Substring(0, 2);

            //Day29

            D29 = View.GetRowCellValue(e.RowHandle, "Day29").ToString() + "                                ";
            D29 = D29.Substring(0, 1);

            DD29 = View.GetRowCellValue(e.RowHandle, "Day29Clock").ToString() + "                                             ";
            DD29 = DD29.Substring(0, 2);

            //Day30

            D30 = View.GetRowCellValue(e.RowHandle, "Day30").ToString() + "                                ";
            D30 = D30.Substring(0, 1);

            DD30 = View.GetRowCellValue(e.RowHandle, "Day30Clock").ToString() + "                                             ";
            DD30 = DD30.Substring(0, 2);

            //Day31

            D31 = View.GetRowCellValue(e.RowHandle, "Day31").ToString() + "                                ";
            D31 = D31.Substring(0, 1);

            DD31 = View.GetRowCellValue(e.RowHandle, "Day31Clock").ToString() + "                                             ";
            DD31 = DD31.Substring(0, 2);

            ////Day 1 

            //if (e.Column.AbsoluteIndex == 3)
            //{
            //    if (D1 == "S" || DD1 == "YS")
            //    {
            //        e.Appearance.BackColor = Color.MistyRose;
            //        //  e.DisplayText = wd1clock;

            //    }

            //}

            ////Day 2


            //if (e.Column.AbsoluteIndex == 4)
            //{
            //    if (D2 == "S" || DD2 == "YS")
            //    {
            //        e.Appearance.BackColor = Color.MistyRose;

            //    }

            //}

            ////Day 3


            //if (e.Column.AbsoluteIndex == 5)
            //{
            //    if (D3 == "S" || DD3 == "YS")
            //    {
            //        e.Appearance.BackColor = Color.MistyRose;

            //    }

            //}

            ////Day 4


            //if (e.Column.AbsoluteIndex == 6)
            //{
            //    if (D4 == "S" || DD4 == "YS")
            //    {
            //        e.Appearance.BackColor = Color.MistyRose;

            //    }

            //}

            ////Day 5


            //if (e.Column.AbsoluteIndex == 7)
            //{
            //    if (D5 == "S" || DD5 == "YS")
            //    {
            //        e.Appearance.BackColor = Color.MistyRose;

            //    }

            //}

            ////Day 6


            //if (e.Column.AbsoluteIndex == 8)
            //{
            //    if (D6 == "S" || DD6 == "YS")
            //    {
            //        e.Appearance.BackColor = Color.MistyRose;

            //    }

            //}

            ////Day 7


            //if (e.Column.AbsoluteIndex == 9)
            //{
            //    if (D7 == "S" || DD7 == "YS")
            //    {
            //        e.Appearance.BackColor = Color.MistyRose;

            //    }

            //}

            ////Day 8


            //if (e.Column.AbsoluteIndex == 10)
            //{
            //    if (D8 == "S" || DD8 == "YS")
            //    {
            //        e.Appearance.BackColor = Color.MistyRose;

            //    }

            //}

            ////Day 9


            //if (e.Column.AbsoluteIndex == 11) { if (D9 == "S" || DD9 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day10
            //if (e.Column.AbsoluteIndex == 12) { if (D10 == "S" || DD10 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day11
            //if (e.Column.AbsoluteIndex == 13) { if (D11 == "S" || DD11 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day12
            //if (e.Column.AbsoluteIndex == 14) { if (D12 == "S" || DD12 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day13
            //if (e.Column.AbsoluteIndex == 15) { if (D13 == "S" || DD13 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day14
            //if (e.Column.AbsoluteIndex == 16) { if (D14 == "S" || DD14 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day15
            //if (e.Column.AbsoluteIndex == 17) { if (D15 == "S" || DD15 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day16
            //if (e.Column.AbsoluteIndex == 18) { if (D16 == "S" || DD16 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day17
            //if (e.Column.AbsoluteIndex == 19) { if (D17 == "S" || DD17 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day18
            //if (e.Column.AbsoluteIndex == 20) { if (D18 == "S" || DD18 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day19
            //if (e.Column.AbsoluteIndex == 21) { if (D19 == "S" || DD19 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day20
            //if (e.Column.AbsoluteIndex == 22) { if (D20 == "S" || DD20 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day21
            //if (e.Column.AbsoluteIndex == 23) { if (D21 == "S" || DD21 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day22
            //if (e.Column.AbsoluteIndex == 24) { if (D22 == "S" || DD22 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day23
            //if (e.Column.AbsoluteIndex == 25) { if (D23 == "S" || DD23 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day24
            //if (e.Column.AbsoluteIndex == 26) { if (D24 == "S" || DD24 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day25
            //if (e.Column.AbsoluteIndex == 27) { if (D25 == "S" || DD25 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day26
            //if (e.Column.AbsoluteIndex == 28) { if (D26 == "S" || DD26 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day27
            //if (e.Column.AbsoluteIndex == 29) { if (D27 == "S" || DD27 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day28
            //if (e.Column.AbsoluteIndex == 30) { if (D28 == "S" || DD28 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day29
            //if (e.Column.AbsoluteIndex == 31) { if (D29 == "S" || DD29 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day30
            //if (e.Column.AbsoluteIndex == 32) { if (D30 == "S" || DD30 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day31
            //if (e.Column.AbsoluteIndex == 33) { if (D31 == "S" || DD31 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            //Day 1

            if (e.Column.AbsoluteIndex == 3)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day1").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 2

            if (e.Column.AbsoluteIndex == 4)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day2").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 3

            if (e.Column.AbsoluteIndex == 5)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day3").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 4

            if (e.Column.AbsoluteIndex == 6)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day4").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 5

            if (e.Column.AbsoluteIndex == 7)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day5").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 6

            if (e.Column.AbsoluteIndex == 8)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day6").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 7

            if (e.Column.AbsoluteIndex == 9)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day7").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 8

            if (e.Column.AbsoluteIndex == 10)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day8").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 9

            if (e.Column.AbsoluteIndex == 11)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day9").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 10

            if (e.Column.AbsoluteIndex == 12)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day10").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 11

            if (e.Column.AbsoluteIndex == 13)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day11").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 12

            if (e.Column.AbsoluteIndex == 14)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day12").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 13

            if (e.Column.AbsoluteIndex == 15)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day13").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 14

            if (e.Column.AbsoluteIndex == 16)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day14").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 15

            if (e.Column.AbsoluteIndex == 17)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day15").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 16

            if (e.Column.AbsoluteIndex == 18)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day16").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 17

            if (e.Column.AbsoluteIndex == 19)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day17").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 18

            if (e.Column.AbsoluteIndex == 20)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day18").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 19

            if (e.Column.AbsoluteIndex == 21)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day19").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 20

            if (e.Column.AbsoluteIndex == 22)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day20").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 21

            if (e.Column.AbsoluteIndex == 23)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day21").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 22

            if (e.Column.AbsoluteIndex == 24)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day22").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 23

            if (e.Column.AbsoluteIndex == 25)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day23").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 24

            if (e.Column.AbsoluteIndex == 26)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day24").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 25

            if (e.Column.AbsoluteIndex == 27)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day25").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 26

            if (e.Column.AbsoluteIndex == 28)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day26").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 27

            if (e.Column.AbsoluteIndex == 29)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day27").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 28

            if (e.Column.AbsoluteIndex == 30)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day28").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 29

            if (e.Column.AbsoluteIndex == 31)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day29").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 30

            if (e.Column.AbsoluteIndex == 32)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day30").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 31

            if (e.Column.AbsoluteIndex == 33)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day31").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }
        }

        private void bandedGridView6_DoubleClick(object sender, EventArgs e)
        {
            return;

            //simpleButton2_Click(null, null);

            frmProp RepFrm = (frmProp)IsBookingFormAlreadyOpen(typeof(frmProp));
            if (RepFrm == null)
            {
                RepFrm = new frmProp();
                RepFrm.Text = "Edit Gang Member";
                RepFrm.IDLbl.Text = "1";
                RepFrm.MonthLbl.Text = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editHoistMonth.EditValue)).ToString();
                RepFrm.OrgUnitLbl.Text = Orgunitlbl.Text;
                RepFrm.lblHoppersTramEdit.Text = gvAToG.GetRowCellValue(gvAToG.FocusedRowHandle, gvAToG.Columns["Hoppers"]).ToString();
                RepFrm.lblTeamGroupTramEdit.Text = gvAToG.GetRowCellValue(gvAToG.FocusedRowHandle, gvAToG.Columns["TeamGroup"]).ToString();
                RepFrm.lblTeamTramEdit.Text = gvAToG.GetRowCellValue(gvAToG.FocusedRowHandle, gvAToG.Columns["Team"]).ToString();
                string ss = Orgunitlbl.Text;
                RepFrm.LvlLbl.Text = "zzz";

                // string test = bandedGridView6.GetRowCellValue(bandedGridView6.FocusedRowHandle, bandedGridView6.Columns["Attendance"]).ToString();

                if (gvAToG.GetRowCellValue(gvAToG.FocusedRowHandle, gvAToG.Columns["Attendance"]).ToString() == "N ")
                {
                    RepFrm.AttRG.SelectedIndex = 1;
                }

                if (gvAToG.GetRowCellValue(gvAToG.FocusedRowHandle, gvAToG.Columns["Attendance"]).ToString() == "Y ")
                {
                    RepFrm.AttRG.SelectedIndex = 0;
                }




                if (Orgunitlbl.Text != "")
                {
                    RepFrm.LvlLbl.Text = ss.Substring(5, 2);

                    string lvl = "";

                    if (ss.Substring(5, 1) == "1" && ss.Substring(5, 2) != "10" && ss.Substring(5, 2) != "12" && ss.Substring(5, 2) != "13" && ss.Substring(5, 2) != "14" && ss.Substring(5, 2) != "15" && ss.Substring(5, 2) != "16" && ss.Substring(5, 2) != "17" && ss.Substring(5, 2) != "18" && ss.Substring(5, 2) != "19")
                        lvl = "3";

                    if (ss.Substring(5, 2) == "01")
                        lvl = "3";


                    if (ss.Substring(5, 1) == "2")
                        lvl = "2";
                    if (ss.Substring(5, 2) == "02")
                        lvl = "2";

                    if (ss.Substring(5, 1) == "3")
                        lvl = "3";
                    if (ss.Substring(5, 2) == "03")
                        lvl = "3";

                    if (ss.Substring(5, 1) == "4")
                        lvl = "4";
                    if (ss.Substring(5, 2) == "04")
                        lvl = "4";

                    if (ss.Substring(5, 1) == "5")
                        lvl = "5";
                    if (ss.Substring(5, 2) == "05")
                        lvl = "5";

                    if (ss.Substring(5, 1) == "6")
                        lvl = "6";
                    if (ss.Substring(5, 2) == "06")
                        lvl = "6";

                    if (ss.Substring(5, 1) == "7")
                        lvl = "7";
                    if (ss.Substring(5, 2) == "07")
                        lvl = "7";

                    if (ss.Substring(5, 1) == "8")
                        lvl = "8";
                    if (ss.Substring(5, 2) == "08")
                        lvl = "8";

                    if (ss.Substring(5, 1) == "9")
                        lvl = "9";
                    if (ss.Substring(5, 2) == "09")
                        lvl = "9";


                    if (ss.Substring(5, 2) == "10")
                        lvl = "10";
                    if (ss.Substring(5, 2) == "11")
                        lvl = "11";
                    if (ss.Substring(5, 2) == "12")
                        lvl = "12";
                    if (ss.Substring(5, 2) == "13")
                        lvl = "13";
                    if (ss.Substring(5, 2) == "14")
                        lvl = "14";
                    if (ss.Substring(5, 2) == "15")
                        lvl = "15";
                    if (ss.Substring(5, 2) == "16")
                        lvl = "16";
                    if (ss.Substring(5, 2) == "17")
                        lvl = "17";
                    if (ss.Substring(5, 2) == "18")
                        lvl = "18";
                    if (ss.Substring(5, 2) == "19")
                        lvl = "19";


                    RepFrm.LvlLbl.Text = lvl;





                }
                RepFrm.DateLbl.Text = String.Format("{0:yyyy-MM-dd}", DateTxt.Value);


                if (DS.Checked == true)
                {
                    Shift = "D";

                }

                if (AS.Checked == true)
                {
                    Shift = "A";

                }

                if (NS.Checked == true)
                {
                    Shift = "N";

                }
                RepFrm.ShiftLbl.Text = Shift;
                RepFrm.IndNoLbl.Text = IndNoLbl.Text;
                RepFrm.IndNumTxt.Text = IndNoLbl.Text;
                //RepFrm.IndNoLbl

                RepFrm.Show();
            }
            else
            {
                RepFrm.WindowState = FormWindowState.Maximized;
                RepFrm.Select();
            }
        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            frmAddTramCrewcs ABSfrm = new frmAddTramCrewcs();
            ABSfrm._connection = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            ABSfrm.ShowDialog();

            LoadDataTram();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            LoadDate();
            LoadTrammingAtoG();
            LoadTrammingOther();

        }

        private void LVLTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (LVLTreeView.SelectedNode.Parent == null)
            {
                try
                {
                    // Orgunitlbl.Text = LVLTreeView.SelectedNode.Text.ToString();

                }
                catch { }
            }

            if (LVLTreeView.SelectedNode != null)
            {
                if (LVLTreeView.SelectedNode.Level != 0)
                {
                    Orgunitlbl.Text = LVLTreeView.SelectedNode.Text.ToString();
                    Orglbl.Text = LVLTreeView.SelectedNode.Text.ToString();
                    Orglbl2.Text = LVLTreeView.SelectedNode.Text.ToString();

                    if (dgvTrammingOther.Rows[0].Cells["Orgunit"].FormattedValue.ToString() != "Fill")
                    {
                        var message = "You have made changes to the current Org without saving, do you want to save updates made?";
                        var caption = "Unsaved Changes";
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        MessageBoxIcon icon = MessageBoxIcon.Information;
                        DialogResult result;
                        result = MessageBox.Show(message, caption, buttons, icon);

                        if (result == DialogResult.Yes)
                        {
                            btnUpdate2_ItemClick(null, null);
                        }
                        else if (result == DialogResult.No)
                        {
                            dgvTrammingOther.Rows.Clear();
                            dgvTrammingOther.Rows.Add("Fill");
                        }
                    }

                    if (dgvAToG.Rows[0].Cells["Orgunit2"].FormattedValue.ToString() != "Fill")
                    {
                        var message = "You have made changes to the current Org without saving, do you want to save updates made?";
                        var caption = "Unsaved Changes";
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        MessageBoxIcon icon = MessageBoxIcon.Information;
                        DialogResult result;
                        result = MessageBox.Show(message, caption, buttons, icon);

                        if (result == DialogResult.Yes)
                        {
                            Updatebtn_ItemClick(null, null);
                        }
                        else if (result == DialogResult.No)
                        {
                            dgvAToG.Rows.Clear();
                            dgvAToG.Rows.Add("Fill");
                        }
                    }
                }

            }
        }

        string Shift = "";
        private void DS_CheckedChanged(object sender, EventArgs e)
        {
            if (DS.Checked == true)
            {
                Shift = "D";
                AS.Checked = false;
                NS.Checked = false;
            }

            if (AS.Checked == true)
            {
                Shift = "A";
                DS.Checked = false;
                NS.Checked = false;
            }

            if (NS.Checked == true)
            {
                Shift = "N";
                DS.Checked = false;
                AS.Checked = false;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //if (Orgunitlbl.Text == "Orgunitlbl")
            //{
            //    MessageBox.Show("Please Select a Level");
            //    return;
            //}


            //frmProp RepFrm = (frmProp)IsBookingFormAlreadyOpen(typeof(frmProp));
            //if (RepFrm == null)
            //{
            //    RepFrm = new frmProp();
            //    RepFrm.Text = "Add Gang Member";
            //    RepFrm.IDLbl.Text = "1";
            //    RepFrm.MonthLbl.Text = MillMonth.Value.ToString();
            //    RepFrm.OrgUnitLbl.Text = Orgunitlbl.Text;
            //    string ss = Orgunitlbl.Text;
            //    RepFrm.LvlLbl.Text = "zzz";
            //    if (Orgunitlbl.Text != "")
            //    {
            //        RepFrm.LvlLbl.Text = ss.Substring(5, 2);

            //        string lvl = "";

            //        if (ss.Substring(5, 1) == "1" && ss.Substring(5, 2) != "10" && ss.Substring(5, 2) != "12" && ss.Substring(5, 2) != "13" && ss.Substring(5, 2) != "14" && ss.Substring(5, 2) != "15" && ss.Substring(5, 2) != "16" && ss.Substring(5, 2) != "17" && ss.Substring(5, 2) != "18" && ss.Substring(5, 2) != "19")
            //            lvl = "3";

            //        if (ss.Substring(5, 2) == "01")
            //            lvl = "3";

            //        if (ss.Substring(5, 1) == "2")
            //            lvl = "2";
            //        if (ss.Substring(5, 2) == "02")
            //            lvl = "2";

            //        if (ss.Substring(5, 1) == "3")
            //            lvl = "3";
            //        if (ss.Substring(5, 2) == "03")
            //            lvl = "3";

            //        if (ss.Substring(5, 1) == "4")
            //            lvl = "4";
            //        if (ss.Substring(5, 2) == "04")
            //            lvl = "4";

            //        if (ss.Substring(5, 1) == "5")
            //            lvl = "5";
            //        if (ss.Substring(5, 2) == "05")
            //            lvl = "5";

            //        if (ss.Substring(5, 1) == "6")
            //            lvl = "6";
            //        if (ss.Substring(5, 2) == "06")
            //            lvl = "6";

            //        if (ss.Substring(5, 1) == "7")
            //            lvl = "7";
            //        if (ss.Substring(5, 2) == "07")
            //            lvl = "7";

            //        if (ss.Substring(5, 1) == "8")
            //            lvl = "8";
            //        if (ss.Substring(5, 2) == "08")
            //            lvl = "8";

            //        if (ss.Substring(5, 1) == "9")
            //            lvl = "9";
            //        if (ss.Substring(5, 2) == "09")
            //            lvl = "9";

            //        if (ss.Substring(5, 2) == "10")
            //            lvl = "10";
            //        if (ss.Substring(5, 2) == "11")
            //            lvl = "11";
            //        if (ss.Substring(5, 2) == "12")
            //            lvl = "12";
            //        if (ss.Substring(5, 2) == "13")
            //            lvl = "13";
            //        if (ss.Substring(5, 2) == "14")
            //            lvl = "14";
            //        if (ss.Substring(5, 2) == "15")
            //            lvl = "15";
            //        if (ss.Substring(5, 2) == "16")
            //            lvl = "16";
            //        if (ss.Substring(5, 2) == "17")
            //            lvl = "17";
            //        if (ss.Substring(5, 2) == "18")
            //            lvl = "18";
            //        if (ss.Substring(5, 2) == "19")
            //            lvl = "19";


            //        RepFrm.LvlLbl.Text = lvl;

            if (Orgunitlbl.Text == "Orgunitlbl")
            {
                MessageBox.Show("Please Select a Level");
                return;
            }

            frmTrammingProp RepFrm = (frmTrammingProp)IsBookingFormAlreadyOpen(typeof(frmTrammingProp));
            if (RepFrm == null)
            {
                RepFrm = new frmTrammingProp();
                RepFrm._connection = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                RepFrm.tbMillMonth.Text = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editHoistMonth.EditValue)).ToString();
                RepFrm.tbOrg.Text = Orgunitlbl.Text;
                RepFrm.tbLevel.Text = lblLevelFill.Text;
                RepFrm.Show();
            }

            //    RepFrm.DateLbl.Text = String.Format("{0:yyyy-MM-dd}", DateTxt.Value);
            //    if (DS.Checked == true)
            //    {
            //        Shift = "D";
            //    }
            //    if (AS.Checked == true)
            //    {
            //        Shift = "A";
            //    }
            //    if (NS.Checked == true)
            //    {
            //        Shift = "N";
            //    }
            //    RepFrm.ShiftLbl.Text = Shift;
            //    RepFrm.IndNoLbl.Text = IndNoLbl.Text;
            //    RepFrm.IndNumTxt.Text = IndNoLbl.Text;
            //    //RepFrm.IndNoLbl;
            //    RepFrm.Show();
            //}
            //else
            //{
            //    RepFrm.WindowState = FormWindowState.Maximized;
            //    RepFrm.Select();
            //}
        }

        private void bandedGridView6_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
            Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
            Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            string Section = "";
            string ss = "";
            string lvl = "";

            Section = Orgunitlbl.Text.Substring(0, 4);
            ss = Orgunitlbl.Text;

            if (Orgunitlbl.Text != "")
            {
                //RepFrm.LvlLbl.Text = ss.Substring(5, 2);



                if (ss.Substring(5, 1) == "1" && ss.Substring(5, 2) != "10" && ss.Substring(5, 2) != "12" && ss.Substring(5, 2) != "13" && ss.Substring(5, 2) != "14" && ss.Substring(5, 2) != "15" && ss.Substring(5, 2) != "16" && ss.Substring(5, 2) != "17" && ss.Substring(5, 2) != "18" && ss.Substring(5, 2) != "19")
                    lvl = "3";

                if (ss.Substring(5, 2) == "01")
                    lvl = "3";


                if (ss.Substring(5, 1) == "2")
                    lvl = "2";
                if (ss.Substring(5, 2) == "02")
                    lvl = "2";

                if (ss.Substring(5, 1) == "3")
                    lvl = "3";
                if (ss.Substring(5, 2) == "03")
                    lvl = "3";

                if (ss.Substring(5, 1) == "4")
                    lvl = "4";
                if (ss.Substring(5, 2) == "04")
                    lvl = "4";

                if (ss.Substring(5, 1) == "5")
                    lvl = "5";
                if (ss.Substring(5, 2) == "05")
                    lvl = "5";

                if (ss.Substring(5, 1) == "6")
                    lvl = "6";
                if (ss.Substring(5, 2) == "06")
                    lvl = "6";

                if (ss.Substring(5, 1) == "7")
                    lvl = "7";
                if (ss.Substring(5, 2) == "07")
                    lvl = "7";

                if (ss.Substring(5, 1) == "8")
                    lvl = "8";
                if (ss.Substring(5, 2) == "08")
                    lvl = "8";

                if (ss.Substring(5, 1) == "9")
                    lvl = "9";
                if (ss.Substring(5, 2) == "09")
                    lvl = "9";

                if (ss.Substring(5, 2) == "10")
                    lvl = "10";
                if (ss.Substring(5, 2) == "11")
                    lvl = "11";
                if (ss.Substring(5, 2) == "12")
                    lvl = "12";
                if (ss.Substring(5, 2) == "13")
                    lvl = "13";
                if (ss.Substring(5, 2) == "14")
                    lvl = "14";
                if (ss.Substring(5, 2) == "15")
                    lvl = "15";
                if (ss.Substring(5, 2) == "16")
                    lvl = "16";
                if (ss.Substring(5, 2) == "17")
                    lvl = "17";
                if (ss.Substring(5, 2) == "18")
                    lvl = "18";
                if (ss.Substring(5, 2) == "19")
                    lvl = "19";


                //RepFrm.LvlLbl.Text = lvl;





            }

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " delete from tbl_BCS_Tramming_Gang_3Month  \r\n" +
                                  " where  ID = '1' and YearMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editHoistMonth.EditValue)).ToString() + "' \r\n" +
                                  " and [Date] = '" + String.Format("{0:yyyy-MM-dd}", DateTxt.Value) + "' and IndustryNumber = '" + IndNoLbl.Text + "'  \r\n" +
                                  " and OrgUnit = '" + Orgunitlbl.Text + "' and WorkingOrgUnit = '" + Orgunitlbl.Text + "'  \r\n" +
                                  " and Section = '" + Section + "' and [Level] = '" + lvl + "' ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            // _dbMan.ExecuteInstruction();
        }

        private void MillMonth_Click(object sender, EventArgs e)
        {
            
            LoadDate();
            LoadDataTram();
            LoadTrammingAtoG();
            LoadTrammingOther();

        }

        private void AS_CheckedChanged(object sender, EventArgs e)
        {
            if (DS.Checked == true)
            {
                Shift = "D";
                AS.Checked = false;
                NS.Checked = false;
            }

            if (AS.Checked == true)
            {
                Shift = "A";
                DS.Checked = false;
                NS.Checked = false;
            }

            if (NS.Checked == true)
            {
                Shift = "N";
                DS.Checked = false;
                AS.Checked = false;
            }
        }

        private void NS_CheckedChanged(object sender, EventArgs e)
        {
            if (DS.Checked == true)
            {
                Shift = "D";
                AS.Checked = false;
                NS.Checked = false;
            }

            if (AS.Checked == true)
            {
                Shift = "A";
                DS.Checked = false;
                NS.Checked = false;
            }

            if (NS.Checked == true)
            {
                Shift = "N";
                DS.Checked = false;
                AS.Checked = false;
            }
        }

        private void Orgunitlbl_TextChanged(object sender, EventArgs e)
        {
            simpleButton3_Click(null, null);
        }

        private void OrgUnitCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Level = LevelCombo.SelectedItem.ToString();
            string Orgunit = OrgUnitCombo.SelectedItem.ToString();

            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan1.SqlStatement = " select section from tbl_BCS_Tramming_Levels where level = '" + Level + "' and YearMonth = '201602' and shift = '" + Shift + "' and OrgUnit = '" + Orgunit + "'  order by Section   ";
            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();

            DataTable dt3 = _dbMan1.ResultsDataTable;

            lblLevelFill.Text = dt3.Rows[0]["Section"].ToString();
        }

        private void LevelCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DS.Checked == true)
            {
                Shift = "D";
                AS.Checked = false;
                NS.Checked = false;
            }

            if (AS.Checked == true)
            {
                Shift = "A";
                DS.Checked = false;
                NS.Checked = false;
            }

            if (NS.Checked == true)
            {
                Shift = "N";
                DS.Checked = false;
                AS.Checked = false;
            }

            string Level = LevelCombo.SelectedItem.ToString();

            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan1.SqlStatement = " select OrgUnit from tbl_BCS_Tramming_Levels where level = '" + Level + "' and YearMonth = '201602' and shift = '" + Shift + "' group by OrgUnit order by OrgUnit   ";
            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();

            DataTable dt3 = _dbMan1.ResultsDataTable;

            foreach (DataRow r in dt3.Rows)
            {
                OrgUnitCombo.Items.Add(r["OrgUnit"]);
            }


        }

        private void ucTrammingCapture_Load(object sender, EventArgs e)
        {

          

            editHoistMonth.EditValue = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsDate(ProductionAmplatsGlobal.ProductionAmplatsGlobalTSysSettings._currentProductionMonth.ToString());

            LoadDataTram();
            dgvTrammingOther.Rows.Add("Fill");
            dgvAToG.Rows.Add("Fill");
        }

        private void bandedGridView1_RowClick(object sender, RowClickEventArgs e)
        {

        }

        private void bandedGridView1_DoubleClick(object sender, EventArgs e)
        {
            return;

            //simpleButton2_Click(null, null);

            frmProp RepFrm = (frmProp)IsBookingFormAlreadyOpen(typeof(frmProp));
            if (RepFrm == null)
            {
                RepFrm = new frmProp();
                RepFrm.Text = "Edit Gang Member";
                RepFrm.IDLbl.Text = "1";
                RepFrm.MonthLbl.Text = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editHoistMonth.EditValue)).ToString();
                RepFrm.OrgUnitLbl.Text = Orgunitlbl.Text;
                RepFrm.lblHoppersTramEdit.Text = gvOther.GetRowCellValue(gvOther.FocusedRowHandle, gvOther.Columns["Hoppers"]).ToString();
                RepFrm.lblTeamGroupTramEdit.Text = gvOther.GetRowCellValue(gvOther.FocusedRowHandle, gvOther.Columns["TeamGroup"]).ToString();
                RepFrm.lblTeamTramEdit.Text = gvOther.GetRowCellValue(gvOther.FocusedRowHandle, gvOther.Columns["Team"]).ToString();
                string ss = Orgunitlbl.Text;
                RepFrm.LvlLbl.Text = "zzz";

                // string test = bandedGridView6.GetRowCellValue(bandedGridView6.FocusedRowHandle, bandedGridView6.Columns["Attendance"]).ToString();

                if (gvOther.GetRowCellValue(gvOther.FocusedRowHandle, gvOther.Columns["Attendance"]).ToString() == "N ")
                {
                    RepFrm.AttRG.SelectedIndex = 1;
                }

                if (gvOther.GetRowCellValue(gvOther.FocusedRowHandle, gvOther.Columns["Attendance"]).ToString() == "Y ")
                {
                    RepFrm.AttRG.SelectedIndex = 0;
                }




                if (Orgunitlbl.Text != "")
                {
                    RepFrm.LvlLbl.Text = ss.Substring(5, 2);

                    string lvl = "";

                    if (ss.Substring(5, 1) == "1" && ss.Substring(5, 2) != "10" && ss.Substring(5, 2) != "12" && ss.Substring(5, 2) != "13" && ss.Substring(5, 2) != "14" && ss.Substring(5, 2) != "15" && ss.Substring(5, 2) != "16" && ss.Substring(5, 2) != "17" && ss.Substring(5, 2) != "18" && ss.Substring(5, 2) != "19")
                        lvl = "3";

                    if (ss.Substring(5, 2) == "01")
                        lvl = "3";


                    if (ss.Substring(5, 1) == "2")
                        lvl = "2";
                    if (ss.Substring(5, 2) == "02")
                        lvl = "2";

                    if (ss.Substring(5, 1) == "3")
                        lvl = "3";
                    if (ss.Substring(5, 2) == "03")
                        lvl = "3";

                    if (ss.Substring(5, 1) == "4")
                        lvl = "4";
                    if (ss.Substring(5, 2) == "04")
                        lvl = "4";

                    if (ss.Substring(5, 1) == "5")
                        lvl = "5";
                    if (ss.Substring(5, 2) == "05")
                        lvl = "5";

                    if (ss.Substring(5, 1) == "6")
                        lvl = "6";
                    if (ss.Substring(5, 2) == "06")
                        lvl = "6";

                    if (ss.Substring(5, 1) == "7")
                        lvl = "7";
                    if (ss.Substring(5, 2) == "07")
                        lvl = "7";

                    if (ss.Substring(5, 1) == "8")
                        lvl = "8";
                    if (ss.Substring(5, 2) == "08")
                        lvl = "8";

                    if (ss.Substring(5, 1) == "9")
                        lvl = "9";
                    if (ss.Substring(5, 2) == "09")
                        lvl = "9";


                    if (ss.Substring(5, 2) == "10")
                        lvl = "10";
                    if (ss.Substring(5, 2) == "11")
                        lvl = "11";
                    if (ss.Substring(5, 2) == "12")
                        lvl = "12";
                    if (ss.Substring(5, 2) == "13")
                        lvl = "13";
                    if (ss.Substring(5, 2) == "14")
                        lvl = "14";
                    if (ss.Substring(5, 2) == "15")
                        lvl = "15";
                    if (ss.Substring(5, 2) == "16")
                        lvl = "16";
                    if (ss.Substring(5, 2) == "17")
                        lvl = "17";
                    if (ss.Substring(5, 2) == "18")
                        lvl = "18";
                    if (ss.Substring(5, 2) == "19")
                        lvl = "19";


                    RepFrm.LvlLbl.Text = lvl;





                }
                RepFrm.DateLbl.Text = String.Format("{0:yyyy-MM-dd}", DateTxt.Value);


                if (DS.Checked == true)
                {
                    Shift = "D";

                }

                if (AS.Checked == true)
                {
                    Shift = "A";

                }

                if (NS.Checked == true)
                {
                    Shift = "N";

                }
                RepFrm.ShiftLbl.Text = Shift;
                RepFrm.IndNoLbl.Text = IndNoLbl.Text;
                RepFrm.IndNumTxt.Text = IndNoLbl.Text;
                //RepFrm.IndNoLbl

                RepFrm.Show();
            }
            else
            {
                RepFrm.WindowState = FormWindowState.Maximized;
                RepFrm.Select();
            }
        }

        private void bandedGridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {


            IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
            Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();



        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void textBox1_Click(object sender, EventArgs e)
        {


        }

        public void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {


        }

        public object lb_Item = null;

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //try
            //{
            //    lb_Item = null;

            //    if (lbDays2.Items.Count == 0)
            //    {
            //        return;
            //    }
            //    int index = lbDays2.IndexFromPoint(e.X, e.Y);
            //    string s = lbDays2.Items[index].ToString();
            //    DragDropEffects dde1 = DoDragDrop(s, DragDropEffects.All);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            //if (lb_Item != null)
            //{
            //    lbDays2.Items.Add(lb_Item);
            //    lb_Item = null;
            //}
        }

        private void listBox1_MouseLeave(object sender, EventArgs e)
        {
            ListBox lb = sender as ListBox;
        }

        private void listBox1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void gridControl1_DragDrop(object sender, DragEventArgs e)
        {
            //try
            //{
            //    Point p = this.gcOther.PointToClient(new Point(e.X, e.Y));
            //    int row = gvOther.CalcHitInfo(p.X, p.Y).RowHandle;
            //    if (row > -1)
            //    {
            //        if (gvOther.CalcHitInfo(p.X, p.Y).Column.AbsoluteIndex < 3)
            //        {
            //            return;
            //        }
            //        else if (gvOther.CalcHitInfo(p.X, p.Y).Column.FieldName != null && gvOther.CalcHitInfo(p.X, p.Y).Column.FieldName != "team")
            //        {
            //            this.gvOther.SetRowCellValue(row, gvOther.CalcHitInfo(p.X, p.Y).Column.FieldName, lbDays2.Text.Substring(0, 1));
            //            lblSetValFill2.Text = lbDays2.Text;

            //            var org = Orgunitlbl.Text;
            //            var industry = IndNoLbl.Text;
            //            var day = lblDayFill2.Text;
            //            var val = lblSetValFill2.Text.Substring(0, 1);
                        
            //            dgvTrammingOther.Rows.Add(org, industry, day, val);
            //        }
                    
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void gridControl1_DragOver(object sender, DragEventArgs e)
        {
            //e.Effect = DragDropEffects.All;
        }

        private void lblValue_Click(object sender, EventArgs e)
        {

        }

        private void lblHintDrag_Click(object sender, EventArgs e)
        {

        }

        public object lb_Item2 = null;

        private void listBox2_MouseDown(object sender, MouseEventArgs e)
        {
            //try
            //{
            //    lb_Item2 = null;

            //    if (lbDays.Items.Count == 0)
            //    {
            //        return;
            //    }
            //    int index = lbDays.IndexFromPoint(e.X, e.Y);
            //    string s = lbDays.Items[index].ToString();
            //    DragDropEffects dde2 = DoDragDrop(s, DragDropEffects.All);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void listBox2_MouseLeave(object sender, EventArgs e)
        {
            ListBox lb = sender as ListBox;
        }

        private void listBox2_DragEnter(object sender, DragEventArgs e)
        {
            //if (lb_Item2 != null)
            //{
            //    lbDays.Items.Add(lb_Item2);
            //    lb_Item2 = null;
            //}
        }

        private void listBox2_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void gridControl4_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void gridControl4_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ValueType)))
            {
                e.Effect = DragDropEffects.None;
            }
            else
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void gridControl4_DragDrop(object sender, DragEventArgs e)
        {
            //try
            //{
            //    Point p = this.gcAToG.PointToClient(new Point(e.X, e.Y));
            //    int row = gvAToG.CalcHitInfo(p.X, p.Y).RowHandle;
            //    if (row > -1)
            //    {
            //        if (gvAToG.CalcHitInfo(p.X, p.Y).Column.AbsoluteIndex < 3)
            //        {
            //            return;
            //        }
            //        else if (gvAToG.CalcHitInfo(p.X, p.Y).Column.FieldName != null && gvAToG.CalcHitInfo(p.X, p.Y).Column.FieldName != "team")
            //        {

            //            {
            //            this.gvAToG.SetRowCellValue(row, gvAToG.CalcHitInfo(p.X, p.Y).Column.FieldName, lbDays.Text.Substring(0,2));
            //            lblSelValFill.Text = lbDays.Text;

            //            var org = Orgunitlbl.Text;
            //            var industry = IndNoLbl.Text;
            //            var day = lblDayfill.Text;
            //            var value = lblSelValFill.Text.Substring(0, 2);
            //            dgvAToG.Rows.Add(org, industry, day, value);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //}

        }
        private string blankval;

        private void gvAToG_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Value.ToString() == "")
            {
                blankval = "blank";
            }
        }



        private void gvAToG_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            
            try
            {

                if (dgvAToG.Rows[0].Cells["Orgunit2"].FormattedValue.ToString() == "Fill")
                {
                    dgvAToG.Rows.RemoveAt(0);
                }

                //if (gvAToG.GetFocusedValue().ToString() == "")
                //{

                //}
                //else
                //{
                //    int result = Convert.ToInt16(gvAToG.GetFocusedValue());
                //}

                if (e.Column.FieldName == "Day1")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value);
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();

                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day1"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var team = Teamlbl.Text;
                    var des = Desiglbl.Text;
                    
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day2")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(1.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString();
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day2"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }

                if (e.Column.FieldName == "Day3")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(2.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString();
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day3"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day4")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(3.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day4"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day5")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(4.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day5"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day6")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(5.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day6"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day7")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(6.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString(); 
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day7"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day8")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(7.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString(); 
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day8"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day9")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(8.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString(); 
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day9"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day10")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(9.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day10"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day11")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(10.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString(); 
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day11"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day12")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(11.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString(); 
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day12"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day13")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(12.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day13"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day14")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(13.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString(); 
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day14"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day15")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(14.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString(); 
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day15"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day16")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(15.00));
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString(); 
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day16"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day17")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(16.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString(); 
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day17"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day18")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(17.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day18"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day19")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(18.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString();
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day19"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day20")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(19.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day20"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day21")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(20.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString();
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day21"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day22")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(21.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString(); 
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day22"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day23")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(22.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString();
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day23"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day24")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(23.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString(); 
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day24"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day25")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(24.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day25"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day26")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(25.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString(); 
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day26"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day27")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(26.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString(); 
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day27"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day28")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(27.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString(); 
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day28"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day29")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(28.00));
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString(); 
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day29"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day30")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(29.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString();
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day30"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                }
                if (e.Column.FieldName == "Day31")
                {
                    lblDayfill.Text = "";
                    lblDayfill.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(30.00));
                    IndNoLbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[0]).ToString();
                    Desiglbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[1]).ToString();
                    teamgroup = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[3]).ToString();
                    level = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[8]).ToString();
                    orgold = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[2]).ToString(); 
                    shift = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[6]).ToString();
                    Teamlbl.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    lblTeamAG.Text = gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns[4]).ToString();
                    if (gvAToG.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day31"]).ToString() == "")
                    {
                        lblSelValFill.Text = "Delete";
                    }
                    else
                    {
                        lblSelValFill.Text = gvAToG.GetFocusedValue().ToString();
                    }
                    var team = Teamlbl.Text;
                    var org = Orgunitlbl.Text;
                    var industry = IndNoLbl.Text;
                    var day = lblDayfill.Text;
                    var value = lblSelValFill.Text;
                    var des = Desiglbl.Text;
                    dgvAToG.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);

                }

            }
            catch (FormatException ex)
            {
                MessageBox.Show("Not allowed");
                gvAToG.SetFocusedValue("");
                dgvAToG.Rows.Add("Fill");
            }

        }


        private void gvOther_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {

                if (dgvTrammingOther.Rows[0].Cells["Orgunit"].FormattedValue.ToString() == "Fill")
                {
                    dgvTrammingOther.Rows.RemoveAt(0);
                }


                if (gvOther.FocusedValue.ToString() == "N " || gvOther.FocusedValue.ToString() == "")
                {
                    if (e.Column.FieldName == "Day1"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value);
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day1"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var team = Teamlbl.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }

                    if (e.Column.FieldName == "Day2"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(1.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day2"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day3"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(2.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day3"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day4"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(3.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day4"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day5"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(4.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day5"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day6"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(5.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day6"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day7"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(6.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day7"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day8"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(7.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day8"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day9"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(8.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day9"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day10"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(9.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day10"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day11"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(10.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day11"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day12"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(11.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day12"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day13"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(12.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day13"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day14"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(13.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day14"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day15"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(14.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day15"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day16"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(15.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day16"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day17"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(16.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day17"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day18"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(17.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day18"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day19"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(18.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day19"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day20"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(19.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day20"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day21"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(20.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day21"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day22"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(21.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day22"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day23"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(22.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day23"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day24"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(23.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day24"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day25"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(24.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day25"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day26"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(25.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day26"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day27"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(26.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day27"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day28"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(27.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day28"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day29"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(28.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day29"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day30"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(29.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day30"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }


                    if (e.Column.FieldName == "Day31"  )
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(30.00));
                        teamgroup = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["teamgroup"]).ToString();
                        shift = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["typeshift"]).ToString();
                        orgold = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["orgunit"]).ToString();
                        level = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns["level"]).ToString();
                        IndNoLbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[0]).ToString();
                        Desiglbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[1]).ToString();
                        Teamlbl.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        lblTeamOther.Text = gvOther.GetRowCellValue(e.RowHandle, gvOther.Columns[2]).ToString();
                        var team = Teamlbl.Text;
                        if (gvOther.GetRowCellValue(e.RowHandle, gvAToG.Columns["Day31"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var des = Desiglbl.Text;
                        dgvTrammingOther.Rows.Add(org, industry, day, value, team, des, teamgroup, shift, level, orgold);
                    }

                }
                else
                {
                    MessageBox.Show("Not Allowed, please correct characters('N ') and or clear the cell.", "Error");
                    gvOther.SetFocusedValue("");
                }
                


            

            }
            catch (NullReferenceException ex)
            {
            }
        }

        private void StartDate_ValueChanged(object sender, EventArgs e)
        {
            LoadTrammingAtoG();
            LoadTrammingOther();
        }

        private void EndDate_ValueChanged(object sender, EventArgs e)
        {
            LoadTrammingAtoG();
            LoadTrammingOther();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {

        }

        private void gcAToG_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void gcAToG_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void gcAToG_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void LVLTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            if (Orgunitlbl.Text == "Orgunitlbl")
            {
                MessageBox.Show("Please Select a Level");
                return;
            }

            frmTrammingProp RepFrm = (frmTrammingProp)IsBookingFormAlreadyOpen(typeof(frmTrammingProp));
            if (RepFrm == null)
            {
                RepFrm = new frmTrammingProp();
                RepFrm._connection = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                RepFrm.tbMillMonth.Text = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editHoistMonth.EditValue)).ToString();
                RepFrm.tbOrg.Text = Orgunitlbl.Text;
                RepFrm.Show();
            }
        }

        private void LVLTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        private void MillMonth_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnAddGangMember_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (tcTeams.SelectedTab == tbTeamsAG)
            {
                if (Orgunitlbl.Text == "Orgunitlbl")
                {
                    MessageBox.Show("Please Select a Level");
                    return;
                }

                frmTrammingProp RepFrm = (frmTrammingProp)IsBookingFormAlreadyOpen(typeof(frmTrammingProp));
                if (RepFrm == null)
                {
                    RepFrm = new frmTrammingProp();
                    RepFrm._connection = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    RepFrm.tbMillMonth.Text = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editHoistMonth.EditValue)).ToString();
                    RepFrm.tbOrg.Text = Orgunitlbl.Text;
                    RepFrm.tbLevel.Text = lblLevelFill.Text;
                    RepFrm.ShowDialog();
                    RepFrm.StartPosition = FormStartPosition.CenterScreen;
                    RepFrm.FormClosed += new FormClosedEventHandler(RepFrm_FormClosed);
                    LoadTrammingAtoG();
                    LoadTrammingOther();
                }
            }
            else
            {
                if (Orgunitlbl.Text == "Orgunitlbl")
                {
                    MessageBox.Show("Please Select a Level");
                    return;
                }

                frmTrammingProp RepFrm = (frmTrammingProp)IsBookingFormAlreadyOpen(typeof(frmTrammingProp));
                if (RepFrm == null)
                {
                    RepFrm = new frmTrammingProp();
                    RepFrm._connection = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    RepFrm.tbMillMonth.Text = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editHoistMonth.EditValue)).ToString();
                    RepFrm.tbOrg.Text = Orgunitlbl.Text;
                    RepFrm.ShowDialog();
                    RepFrm.StartPosition = FormStartPosition.CenterScreen;
                    RepFrm.FormClosed += new FormClosedEventHandler(RepFrm_FormClosed);
                    LoadTrammingAtoG();
                    LoadTrammingOther();
                }
            }
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string Section = "";
            string ss = "";
            string lvl = "";

            Section = Orgunitlbl.Text.Substring(0, 4);
            ss = Orgunitlbl.Text;

            if (Orgunitlbl.Text != "")
            {
                //RepFrm.LvlLbl.Text = ss.Substring(5, 2);



                if (ss.Substring(5, 1) == "1" && ss.Substring(5, 2) != "10" && ss.Substring(5, 2) != "12" && ss.Substring(5, 2) != "13" && ss.Substring(5, 2) != "14" && ss.Substring(5, 2) != "15" && ss.Substring(5, 2) != "16" && ss.Substring(5, 2) != "17" && ss.Substring(5, 2) != "18" && ss.Substring(5, 2) != "19")
                    lvl = "3";

                if (ss.Substring(5, 2) == "01")
                    lvl = "3";


                if (ss.Substring(5, 1) == "2")
                    lvl = "2";
                if (ss.Substring(5, 2) == "02")
                    lvl = "2";

                if (ss.Substring(5, 1) == "3")
                    lvl = "3";
                if (ss.Substring(5, 2) == "03")
                    lvl = "3";

                if (ss.Substring(5, 1) == "4")
                    lvl = "4";
                if (ss.Substring(5, 2) == "04")
                    lvl = "4";

                if (ss.Substring(5, 1) == "5")
                    lvl = "5";
                if (ss.Substring(5, 2) == "05")
                    lvl = "5";

                if (ss.Substring(5, 1) == "6")
                    lvl = "6";
                if (ss.Substring(5, 2) == "06")
                    lvl = "6";

                if (ss.Substring(5, 1) == "7")
                    lvl = "7";
                if (ss.Substring(5, 2) == "07")
                    lvl = "7";

                if (ss.Substring(5, 1) == "8")
                    lvl = "8";
                if (ss.Substring(5, 2) == "08")
                    lvl = "8";

                if (ss.Substring(5, 1) == "9")
                    lvl = "9";
                if (ss.Substring(5, 2) == "09")
                    lvl = "9";

                if (ss.Substring(5, 2) == "10")
                    lvl = "10";
                if (ss.Substring(5, 2) == "11")
                    lvl = "11";
                if (ss.Substring(5, 2) == "12")
                    lvl = "12";
                if (ss.Substring(5, 2) == "13")
                    lvl = "13";
                if (ss.Substring(5, 2) == "14")
                    lvl = "14";
                if (ss.Substring(5, 2) == "15")
                    lvl = "15";
                if (ss.Substring(5, 2) == "16")
                    lvl = "16";
                if (ss.Substring(5, 2) == "17")
                    lvl = "17";
                if (ss.Substring(5, 2) == "18")
                    lvl = "18";
                if (ss.Substring(5, 2) == "19")
                    lvl = "19";


                //RepFrm.LvlLbl.Text = lvl;
            }

        }

        private void Updatebtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (tcTeams.SelectedTab == tbTeamsAG)
            {
                this.Cursor = Cursors.WaitCursor;
                this.UseWaitCursor = true;
                if (dgvAToG.Rows[0].Cells[0].Value.ToString() != "Fill")
                {

                    UpdateTrammingAtoGDelete();
                    UpdateTrammingAtoGDelete_3Month();

                    UpdateTrammingAtoGInsertUpdate();
                    UpdateTrammingAtoGInsertUpdate_3Month();



                    dgvAToG.Rows.Clear();
                    dgvAToG.Rows.Add("Fill");
                    LoadTrammingAtoG();
                    alertControl1.Buttons.GetButtonByHint("Open").Visible = false;
                    alertControl1.Show(frmMain.ActiveForm, "Notification", "Update was successful");
                }
                this.Cursor = Cursors.Default;
                this.UseWaitCursor = false;
            }
            else
            {
                if (dgvTrammingOther.Rows[0].Cells[0].Value.ToString() != "Fill")
                {
                    UpdateTrammingOtherDelete();
                    UpdateTrammingOtherDelete_3Month();
                    UpdateTrammingOtherInsertUpdate();
                    UpdateTrammingOtherInsertUpdate_3Month();

                    dgvTrammingOther.Rows.Clear();
                    dgvTrammingOther.Rows.Add("Fill");
                    LoadTrammingOther();
                    alertControl1.Buttons.GetButtonByHint("Open").Visible = false;
                    alertControl1.Show(frmMain.ActiveForm, "Notification", "Update was successful");
                }
            }
        }

        private void Teamlbl_Click(object sender, EventArgs e)
        {

        }

        public static Form frmTrammingProp;

        private void btnAddGangMember2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            

        }

        void RepFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadTrammingAtoG();
            LoadTrammingOther();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string Section = "";
            string ss = "";
            string lvl = "";

            Section = Orgunitlbl.Text.Substring(0, 4);
            ss = Orgunitlbl.Text;

            if (Orgunitlbl.Text != "")
            {
                //RepFrm.LvlLbl.Text = ss.Substring(5, 2);



                if (ss.Substring(5, 1) == "1" && ss.Substring(5, 2) != "10" && ss.Substring(5, 2) != "12" && ss.Substring(5, 2) != "13" && ss.Substring(5, 2) != "14" && ss.Substring(5, 2) != "15" && ss.Substring(5, 2) != "16" && ss.Substring(5, 2) != "17" && ss.Substring(5, 2) != "18" && ss.Substring(5, 2) != "19")
                    lvl = "3";

                if (ss.Substring(5, 2) == "01")
                    lvl = "3";


                if (ss.Substring(5, 1) == "2")
                    lvl = "2";
                if (ss.Substring(5, 2) == "02")
                    lvl = "2";

                if (ss.Substring(5, 1) == "3")
                    lvl = "3";
                if (ss.Substring(5, 2) == "03")
                    lvl = "3";

                if (ss.Substring(5, 1) == "4")
                    lvl = "4";
                if (ss.Substring(5, 2) == "04")
                    lvl = "4";

                if (ss.Substring(5, 1) == "5")
                    lvl = "5";
                if (ss.Substring(5, 2) == "05")
                    lvl = "5";

                if (ss.Substring(5, 1) == "6")
                    lvl = "6";
                if (ss.Substring(5, 2) == "06")
                    lvl = "6";

                if (ss.Substring(5, 1) == "7")
                    lvl = "7";
                if (ss.Substring(5, 2) == "07")
                    lvl = "7";

                if (ss.Substring(5, 1) == "8")
                    lvl = "8";
                if (ss.Substring(5, 2) == "08")
                    lvl = "8";

                if (ss.Substring(5, 1) == "9")
                    lvl = "9";
                if (ss.Substring(5, 2) == "09")
                    lvl = "9";

                if (ss.Substring(5, 2) == "10")
                    lvl = "10";
                if (ss.Substring(5, 2) == "11")
                    lvl = "11";
                if (ss.Substring(5, 2) == "12")
                    lvl = "12";
                if (ss.Substring(5, 2) == "13")
                    lvl = "13";
                if (ss.Substring(5, 2) == "14")
                    lvl = "14";
                if (ss.Substring(5, 2) == "15")
                    lvl = "15";
                if (ss.Substring(5, 2) == "16")
                    lvl = "16";
                if (ss.Substring(5, 2) == "17")
                    lvl = "17";
                if (ss.Substring(5, 2) == "18")
                    lvl = "18";
                if (ss.Substring(5, 2) == "19")
                    lvl = "19";
            }
        }

        private void btnUpdate2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            

        }

        private void gcAToG_MouseHover(object sender, EventArgs e)
        {

        }

        private void gcOther_DoubleClick(object sender, EventArgs e)
        {

        }

        private void gvOther_Click(object sender, EventArgs e)
        {

        }

        private void gvAToG_Click(object sender, EventArgs e)
        {

        }

        private void gvOther_DoubleClick(object sender, EventArgs e)
        {
            var val = gvOther.GetFocusedValue();
            MessageBox.Show(val.ToString());
            gvOther.SetFocusedValue("test");
        }

        private void gcOther_DoubleClick_1(object sender, EventArgs e)
        {
            if (Convert.ToString(gvOther.FocusedValue) == "")
            {
                gvOther.SetFocusedValue("N ");
            }
            else if (Convert.ToString(gvOther.FocusedValue) == "N ")
            {
                gvOther.SetFocusedValue("");
            }
        }

        private void gcAToG_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void gcAToG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                
            }
        }

        private void dgvAToG_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gvAToG_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void gcAToG_EditorKeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void gvAToG_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            
        }

        private void gvAToG_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            if (blankval != "blank")
            {
                try
                {
                    Convert.ToInt32(e.Value);
                }
                catch (Exception ex)
                {
                    e.Valid = false;
                }
            }
         blankval = "not blank";
        }

        private void btnPrintAsPdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (tcTeams.SelectedTab == tbTeamsAG)
            {
                this.Cursor = Cursors.WaitCursor;
                this.UseWaitCursor = true;
                checkDirectory();
                fileName = "TeamsAToGShifts";
                checkPdf();



                CompositeLink comp = new CompositeLink(new PrintingSystem());
                PrintableComponentLink link1 = new PrintableComponentLink();
                PrintableComponentLink link2 = new PrintableComponentLink();
                link1.Component = gcOther;
                link1.CreateReportHeaderArea += new CreateAreaEventHandler(link1_CreateReportHeaderArea);
                link2.Component = gcAToG;
                link2.CreateReportHeaderArea += new CreateAreaEventHandler(link2_CreateReportHeaderArea);
                comp.Links.Add(link2);
                comp.Links.Add(link1);
                comp.Landscape = true;
                comp.PaperKind = System.Drawing.Printing.PaperKind.A3;
                comp.ExportToPdf(pdfName);
                this.Cursor = Cursors.Default;
                this.UseWaitCursor = false;
                alertControl1.Buttons.GetButtonByHint("Open").Visible = true;
                alertControl1.Show(frmMain.ActiveForm, "Print Completed", "Click on folder to open");
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
                this.UseWaitCursor = true;
                checkDirectory();
                fileName = "TeamsNotAToGShifts";
                checkPdf();


                CompositeLink comp = new CompositeLink(new PrintingSystem());
                PrintableComponentLink link1 = new PrintableComponentLink();
                PrintableComponentLink link2 = new PrintableComponentLink();
                link1.Component = gcOther;
                link1.CreateReportHeaderArea += new CreateAreaEventHandler(link1_CreateReportHeaderArea);
                link2.Component = gcAToG;
                link2.CreateReportHeaderArea += new CreateAreaEventHandler(link2_CreateReportHeaderArea);
                comp.Links.Add(link2);
                comp.Links.Add(link1);
                comp.Landscape = true;
                comp.PaperKind = System.Drawing.Printing.PaperKind.A3;
                comp.ExportToPdf(pdfName);
                this.Cursor = Cursors.Default;
                this.UseWaitCursor = false;
                alertControl1.Buttons.GetButtonByHint("Open").Visible = true;
                alertControl1.Show(frmMain.ActiveForm, "Print Completed", "Click on folder to open");
            }
            
        }

        private void btnPrintAsPdf2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            


            

        }

        void link2_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
        {
            e.Graph.DrawImage(peLogo.Image, new Rectangle(1095, 0, 280, 80), DevExpress.XtraPrinting.BorderSide.None, Color.White);
            e.Graph.Font = new Font("Arial", 10);
            e.Graph.DrawString("Prod Month: " + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editHoistMonth.EditValue)).ToString(), Color.Black, new Rectangle(0, 50, 280, 80), DevExpress.XtraPrinting.BorderSide.None);
            e.Graph.Font = new Font("Arial", 24);
            e.Graph.DrawString("Bonus Control - Shift Captures   (Production and Tramming)", Color.Black, new Rectangle(450, 0, 500, 80), DevExpress.XtraPrinting.BorderSide.None);
            e.Graph.BorderWidth = 0;
            e.Graph.DrawLine(new Point(30, 80), new Point(1350, 80), Color.Orange, 2);
        }

        void link1_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
        {
            
            //adds space
            e.Graph.BorderWidth = 0;
            e.Graph.DrawLine(new Point(30, 80), new Point(1350, 80), Color.White, 5);
            
        }

        private void alertControl1_ButtonClick(object sender, AlertButtonClickEventArgs e)
        {
            if (e.Info.Caption == "Print Completed" && e.ButtonName == "btnLoad")
            {
                if (File.Exists(pdfName))
                {
                    Process.Start(pdfName);
                }
                else
                {

                }
            }
        }

        private void gvOther_BeforePrintRow(object sender, DevExpress.XtraGrid.Views.Printing.CancelPrintRowEventArgs e)
        {
            //GridView view = (GridView)sender;
            //using (GridViewPrintAppearances pa = new GridViewPrintAppearances(view))
            //{
            //    pa.Combine(pa, view.BaseInfo.GetDefaultPrintAppearance());
            //    Color color = pa.HeaderPanel.BackColor;
            //    pa.Row.BackColor = Color.Red;
            //}
           

        }



        private void gvOther_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;

            ////if (e.Column.AbsoluteIndex == 3)
            ////{
            ////    if (wd1clock.Substring(0, 1) == "S")
            ////    {
            ////        e.Graphics.DrawString(wd1clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
            ////    }
            ////    else
            ////    {
            ////        e.Graphics.DrawString(wd1clock, this.Font, Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);

            ////    }
            ////}

            if (e.Column.AbsoluteIndex == 3)
            {
                //e.Appearance.DrawString(new GraphicsCache(g), "Test", new Rectangle(155,155,10,10), Brushes.Black, StringFormat.GenericDefault);
            }

            ////if (e.Column.AbsoluteIndex == 3)
            ////{
                
            ////}


        }

        private void gcOther_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gvAToG_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView View = sender as GridView;

            //Day 1
            var wd1 = "";
            var wd1clock = "";
            //Day 2
            var wd2 = "";
            var wd2clock = "";
            //Day3
            var wd3 = "";
            var wd3clock = "";
            //Day4
            var wd4 = "";
            var wd4clock = "";
            //Day5
            var wd5 = "";
            var wd5clock = "";
            //Day6
            var wd6 = "";
            var wd6clock = "";
            //Day7
            var wd7 = "";
            var wd7clock = "";
            //Day8
            var wd8 = "";
            var wd8clock = "";
            //Day9
            var wd9 = "";
            var wd9clock = "";
            //Day10
            var wd10 = "";
            var wd10clock = "";
            //Day11
            var wd11 = "";
            var wd11clock = "";
            //Day12
            var wd12 = "";
            var wd12clock = "";
            //Day13
            var wd13 = "";
            var wd13clock = "";
            //Day14
            var wd14 = "";
            var wd14clock = "";
            //Day15
            var wd15 = "";
            var wd15clock = "";
            //Day16
            var wd16 = "";
            var wd16clock = "";
            //Day17
            var wd17 = "";
            var wd17clock = "";
            //Day18
            var wd18 = "";
            var wd18clock = "";
            //Day19
            var wd19 = "";
            var wd19clock = "";
            //Day20
            var wd20 = "";
            var wd20clock = "";
            //Day21
            var wd21 = "";
            var wd21clock = "";
            //Day22
            var wd22 = "";
            var wd22clock = "";
            //Day23
            var wd23 = "";
            var wd23clock = "";
            //Day24
            var wd24 = "";
            var wd24clock = "";
            //Day25
            var wd25 = "";
            var wd25clock = "";
            //Day26
            var wd26 = "";
            var wd26clock = "";
            //Day27
            var wd27 = "";
            var wd27clock = "";
            //Day28
            var wd28 = "";
            var wd28clock = "";
            //Day29
            var wd29 = "";
            var wd29clock = "";
            //Day30
            var wd30 = "";
            var wd30clock = "";
            //Day31
            var wd31 = "";
            var wd31clock = "";

            //incase null Add blanks
            //Day1
            wd1 = View.GetRowCellValue(e.RowHandle, "Day1Clock").ToString() + "                                             ";

            wd1clock = View.GetRowCellValue(e.RowHandle, "Day1Clock").ToString() + "                                        ";
            //Day2
            wd2 = View.GetRowCellValue(e.RowHandle, "Day2Clock").ToString() + "                                             ";

            wd2clock = View.GetRowCellValue(e.RowHandle, "Day2Clock").ToString() + "                                        ";
            //Day3
            wd3 = View.GetRowCellValue(e.RowHandle, "Day3Clock").ToString() + "                                             ";

            wd3clock = View.GetRowCellValue(e.RowHandle, "Day3Clock").ToString() + "                                        ";
            //Day4
            wd4 = View.GetRowCellValue(e.RowHandle, "Day4Clock").ToString() + "                                             ";

            wd4clock = View.GetRowCellValue(e.RowHandle, "Day4Clock").ToString() + "                                        ";
            //Day5
            wd5 = View.GetRowCellValue(e.RowHandle, "Day5Clock").ToString() + "                                             ";

            wd5clock = View.GetRowCellValue(e.RowHandle, "Day5Clock").ToString() + "                                        ";
            //Day6
            wd6 = View.GetRowCellValue(e.RowHandle, "Day6Clock").ToString() + "                                             ";

            wd6clock = View.GetRowCellValue(e.RowHandle, "Day6Clock").ToString() + "                                        ";
            //Day7
            wd7 = View.GetRowCellValue(e.RowHandle, "Day7Clock").ToString() + "                                             ";

            wd7clock = View.GetRowCellValue(e.RowHandle, "Day7Clock").ToString() + "                                        ";
            //Day8
            wd8 = View.GetRowCellValue(e.RowHandle, "Day8Clock").ToString() + "                                             ";

            wd8clock = View.GetRowCellValue(e.RowHandle, "Day8Clock").ToString() + "                                        ";

            //Day9
            wd9 = View.GetRowCellValue(e.RowHandle, "Day9Clock").ToString() + "                                             ";

            wd9clock = View.GetRowCellValue(e.RowHandle, "Day9Clock").ToString() + "                                        ";
            //Day10
            wd10 = View.GetRowCellValue(e.RowHandle, "Day10Clock").ToString() + "                                             ";

            wd10clock = View.GetRowCellValue(e.RowHandle, "Day10Clock").ToString() + "                                        ";
            //Day11
            wd11 = View.GetRowCellValue(e.RowHandle, "Day11Clock").ToString() + "                                             ";

            wd11clock = View.GetRowCellValue(e.RowHandle, "Day11Clock").ToString() + "                                        ";
            //Day12
            wd12 = View.GetRowCellValue(e.RowHandle, "Day12Clock").ToString() + "                                             ";

            wd12clock = View.GetRowCellValue(e.RowHandle, "Day12Clock").ToString() + "                                        ";
            //Day13
            wd13 = View.GetRowCellValue(e.RowHandle, "Day13Clock").ToString() + "                                             ";

            wd13clock = View.GetRowCellValue(e.RowHandle, "Day13Clock").ToString() + "                                        ";
            //Day14
            wd14 = View.GetRowCellValue(e.RowHandle, "Day14Clock").ToString() + "                                             ";
            wd14 = wd14.Substring(0, 1);

            wd14clock = View.GetRowCellValue(e.RowHandle, "Day14Clock").ToString() + "                                        ";
            //Day15
            wd15 = View.GetRowCellValue(e.RowHandle, "Day15Clock").ToString() + "                                             ";

            wd15clock = View.GetRowCellValue(e.RowHandle, "Day15Clock").ToString() + "                                        ";
            //Day16
            wd16 = View.GetRowCellValue(e.RowHandle, "Day16Clock").ToString() + "                                             ";

            wd16clock = View.GetRowCellValue(e.RowHandle, "Day16Clock").ToString() + "                                        ";
            //Day17
            wd17 = View.GetRowCellValue(e.RowHandle, "Day17Clock").ToString() + "                                             ";

            wd17clock = View.GetRowCellValue(e.RowHandle, "Day17Clock").ToString() + "                                        ";
            //Day18
            wd18 = View.GetRowCellValue(e.RowHandle, "Day18Clock").ToString() + "                                             ";

            wd18clock = View.GetRowCellValue(e.RowHandle, "Day18Clock").ToString() + "                                        ";
            //Day19
            wd19 = View.GetRowCellValue(e.RowHandle, "Day19Clock").ToString() + "                                             ";

            wd19clock = View.GetRowCellValue(e.RowHandle, "Day19Clock").ToString() + "                                        ";
            //Day20
            wd20 = View.GetRowCellValue(e.RowHandle, "Day20Clock").ToString() + "                                             ";

            wd20clock = View.GetRowCellValue(e.RowHandle, "Day20Clock").ToString() + "                                        ";
            //Day21
            wd21 = View.GetRowCellValue(e.RowHandle, "Day21Clock").ToString() + "                                             ";

            wd21clock = View.GetRowCellValue(e.RowHandle, "Day21Clock").ToString() + "                                        ";
            //Day22
            wd22 = View.GetRowCellValue(e.RowHandle, "Day22Clock").ToString() + "                                             ";

            wd22clock = View.GetRowCellValue(e.RowHandle, "Day22Clock").ToString() + "                                        ";
            //Day23
            wd23 = View.GetRowCellValue(e.RowHandle, "Day23Clock").ToString() + "                                             ";

            wd23clock = View.GetRowCellValue(e.RowHandle, "Day23Clock").ToString() + "                                        ";
            //Day24
            wd24 = View.GetRowCellValue(e.RowHandle, "Day24Clock").ToString() + "                                             ";

            wd24clock = View.GetRowCellValue(e.RowHandle, "Day24Clock").ToString() + "                                        ";
            //Day25
            wd25 = View.GetRowCellValue(e.RowHandle, "Day25Clock").ToString() + "                                             ";

            wd25clock = View.GetRowCellValue(e.RowHandle, "Day25Clock").ToString() + "                                        ";
            //Day26
            wd26 = View.GetRowCellValue(e.RowHandle, "Day26Clock").ToString() + "                                             ";

            wd26clock = View.GetRowCellValue(e.RowHandle, "Day26Clock").ToString() + "                                        ";
            //Day27
            wd27 = View.GetRowCellValue(e.RowHandle, "Day27Clock").ToString() + "                                             ";

            wd27clock = View.GetRowCellValue(e.RowHandle, "Day27Clock").ToString() + "                                        ";
            //Day28
            wd28 = View.GetRowCellValue(e.RowHandle, "Day28Clock").ToString() + "                                             ";

            wd28clock = View.GetRowCellValue(e.RowHandle, "Day28Clock").ToString() + "                                        ";
            //Day29
            wd29 = View.GetRowCellValue(e.RowHandle, "Day29Clock").ToString() + "                                             ";

            wd29clock = View.GetRowCellValue(e.RowHandle, "Day29Clock").ToString() + "                                        ";
            //Day30
            wd30 = View.GetRowCellValue(e.RowHandle, "Day30Clock").ToString() + "                                             ";

            wd30clock = View.GetRowCellValue(e.RowHandle, "Day30Clock").ToString() + "                                        ";
            //Day31
            wd31 = View.GetRowCellValue(e.RowHandle, "Day31Clock").ToString() + "                                             ";

            wd31clock = View.GetRowCellValue(e.RowHandle, "Day31Clock").ToString() + "                                        ";

            if (e.Column.AbsoluteIndex == 9)
            {
                if (wd1clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd1clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd1clock, this.Font, Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);

                }
            }

            if (e.Column.AbsoluteIndex == 10)
            {
                if (wd2clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd2clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd2clock, this.Font, Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 11)
            {
                if (wd3clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd3clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd3clock, this.Font, Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 12)
            {
                if (wd4clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd4clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd4clock, this.Font, Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 13)
            {
                if (wd5clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd5clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd5clock, this.Font, Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 14)
            {
                if (wd6clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd6clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd1clock, this.Font, Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 15)
            {
                if (wd7clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd7clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd7clock, this.Font, Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 16)
            {
                if (wd8clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd8clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd8clock, this.Font, Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 17)
            {
                if (wd9clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd9clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd9clock, this.Font, Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 18)
            {
                if (wd10clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd10clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd10clock, this.Font, Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 19)
            {
                if (wd11clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd11clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd11clock, this.Font, Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 20)
            {
                if (wd12clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd12clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd12clock, this.Font, Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 21)
            {
                if (wd13clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd13clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd13clock, this.Font, Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 22)
            {
                if (wd14clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd14clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd14clock, this.Font, Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 23)
            {
                if (wd15clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd15clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd15clock, this.Font, Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 24)
            {
                if (wd16clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd16clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd16clock, this.Font, Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 25)
            {
                if (wd17clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd17clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd17clock, this.Font, Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 26)
            {
                if (wd18clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd18clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd18clock, this.Font, Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 27)
            {
                if (wd19clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd19clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd19clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 28)
            {
                if (wd20clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd20clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd20clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 29)
            {
                if (wd21clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd21clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd21clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 30)
            {
                if (wd22clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd22clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd22clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 31)
            {
                if (wd23clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd23clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd23clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 32)
            {
                if (wd24clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd24clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd24clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 33)
            {
                if (wd25clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd25clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd25clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 34)
            {
                if (wd26clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd26clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd26clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 35)
            {
                if (wd27clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd27clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd27clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 36)
            {
                if (wd28clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd28clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd28clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 37)
            {
                if (wd29clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd29clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd29clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 38)
            {
                if (wd30clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd30clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd30clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 39)
            {
                if (wd31clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd31clock, this.Font, Brushes.Red, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd31clock, this.Font, Brushes.LightSlateGray, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            //Day1
            //if (e.Column.AbsoluteIndex == 3)
            //{
            //    e.Appearance.BackColor = Color.White;
            //    if (wd1 == "N ")
            //    {
            //        e.Appearance.BackColor = Color.LightSlateGray;
            //        e.DisplayText = wd1clock;

            //    }
            //    else
            //    {
            //        if (wd1clock.Trim() != "N ")
            //        {
            //            e.DisplayText = wd1clock;
            //            e.Appearance.ForeColor = Color.LightSlateGray;
            //        }
            //    }
            //}
            ////Day2
            //if (e.Column.AbsoluteIndex == 4)
            //{
            //    e.Appearance.BackColor = Color.White;
            //    if (wd2 == "N ")
            //    {
            //        e.Appearance.BackColor = Color.LightSlateGray;
            //        e.DisplayText = wd2clock;

            //    }
            //    else
            //    {
            //        if (wd2clock.Trim() != "N ")
            //        {
            //            e.DisplayText = wd2clock;
            //            e.Appearance.ForeColor = Color.LightSlateGray;
            //        }
            //    }
            //}
            ////Day3
            //if (e.Column.AbsoluteIndex == 5) { e.Appearance.BackColor = Color.White; if (wd3 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd3clock; } else { if (wd3clock.Trim() != "N ") { e.DisplayText = wd3clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day4
            //if (e.Column.AbsoluteIndex == 6) { e.Appearance.BackColor = Color.White; if (wd4 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd4clock; } else { if (wd4clock.Trim() != "N ") { e.DisplayText = wd4clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day5
            //if (e.Column.AbsoluteIndex == 7) { e.Appearance.BackColor = Color.White; if (wd5 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd5clock; } else { if (wd5clock.Trim() != "N ") { e.DisplayText = wd5clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day6
            //if (e.Column.AbsoluteIndex == 8) { e.Appearance.BackColor = Color.White; if (wd6 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd6clock; } else { if (wd6clock.Trim() != "N ") { e.DisplayText = wd6clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day7
            //if (e.Column.AbsoluteIndex == 9) { e.Appearance.BackColor = Color.White; if (wd7 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd7clock; } else { if (wd7clock.Trim() != "N ") { e.DisplayText = wd7clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day8
            //if (e.Column.AbsoluteIndex == 10) { e.Appearance.BackColor = Color.White; if (wd8 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd8clock; } else { if (wd8clock.Trim() != "N ") { e.DisplayText = wd8clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day9
            //if (e.Column.AbsoluteIndex == 11) { e.Appearance.BackColor = Color.White; if (wd9 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd9clock; } else { if (wd9clock.Trim() != "N ") { e.DisplayText = wd9clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day10
            //if (e.Column.AbsoluteIndex == 12) { e.Appearance.BackColor = Color.White; if (wd10 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd10clock; } else { if (wd10clock.Trim() != "N ") { e.DisplayText = wd10clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day11
            //if (e.Column.AbsoluteIndex == 13) { e.Appearance.BackColor = Color.White; if (wd11 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd11clock; } else { if (wd11clock.Trim() != "N ") { e.DisplayText = wd11clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day12
            //if (e.Column.AbsoluteIndex == 14) { e.Appearance.BackColor = Color.White; if (wd12 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd12clock; } else { if (wd12clock.Trim() != "N ") { e.DisplayText = wd12clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day13
            //if (e.Column.AbsoluteIndex == 15) { e.Appearance.BackColor = Color.White; if (wd13 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd13clock; } else { if (wd13clock.Trim() != "N ") { e.DisplayText = wd13clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day14
            //if (e.Column.AbsoluteIndex == 16) { e.Appearance.BackColor = Color.White; if (wd14 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd14clock; } else { if (wd14clock.Trim() != "N ") { e.DisplayText = wd14clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day15
            //if (e.Column.AbsoluteIndex == 17) { e.Appearance.BackColor = Color.White; if (wd15 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd15clock; } else { if (wd15clock.Trim() != "N ") { e.DisplayText = wd15clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day16
            //if (e.Column.AbsoluteIndex == 18) { e.Appearance.BackColor = Color.White; if (wd16 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd16clock; } else { if (wd16clock.Trim() != "N ") { e.DisplayText = wd16clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day17
            //if (e.Column.AbsoluteIndex == 19) { e.Appearance.BackColor = Color.White; if (wd17 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd17clock; } else { if (wd17clock.Trim() != "N ") { e.DisplayText = wd17clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day18
            //if (e.Column.AbsoluteIndex == 20) { e.Appearance.BackColor = Color.White; if (wd18 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd18clock; } else { if (wd18clock.Trim() != "N ") { e.DisplayText = wd18clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day19
            //if (e.Column.AbsoluteIndex == 21) { e.Appearance.BackColor = Color.White; if (wd19 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd19clock; } else { if (wd19clock.Trim() != "N ") { e.DisplayText = wd19clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day20
            //if (e.Column.AbsoluteIndex == 22) { e.Appearance.BackColor = Color.White; if (wd20 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd20clock; } else { if (wd20clock.Trim() != "N ") { e.DisplayText = wd20clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day21
            //if (e.Column.AbsoluteIndex == 23) { e.Appearance.BackColor = Color.White; if (wd21 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd21clock; } else { if (wd21clock.Trim() != "N ") { e.DisplayText = wd21clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day22
            //if (e.Column.AbsoluteIndex == 24) { e.Appearance.BackColor = Color.White; if (wd22 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd22clock; } else { if (wd22clock.Trim() != "N ") { e.DisplayText = wd22clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day23
            //if (e.Column.AbsoluteIndex == 25) { e.Appearance.BackColor = Color.White; if (wd23 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd23clock; } else { if (wd23clock.Trim() != "N ") { e.DisplayText = wd23clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day24
            //if (e.Column.AbsoluteIndex == 26) { e.Appearance.BackColor = Color.White; if (wd24 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd24clock; } else { if (wd24clock.Trim() != "N ") { e.DisplayText = wd24clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day25
            //if (e.Column.AbsoluteIndex == 27) { e.Appearance.BackColor = Color.White; if (wd25 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd25clock; } else { if (wd25clock.Trim() != "N ") { e.DisplayText = wd25clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day26
            //if (e.Column.AbsoluteIndex == 28) { e.Appearance.BackColor = Color.White; if (wd26 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd26clock; } else { if (wd26clock.Trim() != "N ") { e.DisplayText = wd26clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day27
            //if (e.Column.AbsoluteIndex == 29) { e.Appearance.BackColor = Color.White; if (wd27 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd27clock; } else { if (wd27clock.Trim() != "N ") { e.DisplayText = wd27clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day28
            //if (e.Column.AbsoluteIndex == 30) { e.Appearance.BackColor = Color.White; if (wd28 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd28clock; } else { if (wd28clock.Trim() != "N ") { e.DisplayText = wd28clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day29
            //if (e.Column.AbsoluteIndex == 31) { e.Appearance.BackColor = Color.White; if (wd29 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd29clock; } else { if (wd29clock.Trim() != "N ") { e.DisplayText = wd29clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day30
            //if (e.Column.AbsoluteIndex == 32) { e.Appearance.BackColor = Color.White; if (wd30 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd30clock; } else { if (wd30clock.Trim() != "N ") { e.DisplayText = wd30clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }
            ////Day31
            //if (e.Column.AbsoluteIndex == 33) { e.Appearance.BackColor = Color.White; if (wd31 == "N ") { e.Appearance.BackColor = Color.LightSlateGray; e.DisplayText = wd31clock; } else { if (wd31clock.Trim() != "N ") { e.DisplayText = wd31clock; e.Appearance.ForeColor = Color.LightSlateGray; } } }

            //if (gvProdMineOther.GetFocusedValue().ToString() == "S ")
            //{
            //    MessageBox.Show("Test");
            //}
            //Day 1
            var D1 = "";
            var DD1 = "";
            //Day 2
            var D2 = "";
            var DD2 = "";
            //Day 3
            var D3 = "";
            var DD3 = "";
            //Day 4
            var D4 = "";
            var DD4 = "";
            //Day 5
            var D5 = "";
            var DD5 = "";
            //Day 6
            var D6 = "";
            var DD6 = "";
            //Day 7
            var D7 = "";
            var DD7 = "";
            //Day 8
            var D8 = "";
            var DD8 = "";
            //Day 9 
            var D9 = "";
            var DD9 = "";
            //Day 10 
            var D10 = "";
            var DD10 = "";
            //Day 11 
            var D11 = "";
            var DD11 = "";
            //Day 12
            var D12 = "";
            var DD12 = "";
            //Day 16
            var D13 = "";
            var DD13 = "";
            //Day 14
            var D14 = "";
            var DD14 = "";
            //Day 15
            var D15 = "";
            var DD15 = "";
            //Day 16
            var D16 = "";
            var DD16 = "";
            //Day 17
            var D17 = "";
            var DD17 = "";
            //Day 18
            var D18 = "";
            var DD18 = "";
            //Day 19
            var D19 = "";
            var DD19 = "";
            //Day 20
            var D20 = "";
            var DD20 = "";
            //Day 21
            var D21 = "";
            var DD21 = "";
            //Day 22
            var D22 = "";
            var DD22 = "";
            //Day 23
            var D23 = "";
            var DD23 = "";
            //Day 24
            var D24 = "";
            var DD24 = "";
            //Day 25
            var D25 = "";
            var DD25 = "";
            //Day 26
            var D26 = "";
            var DD26 = "";
            //Day 27
            var D27 = "";
            var DD27 = "";
            //Day 28
            var D28 = "";
            var DD28 = "";
            //Day 29
            var D29 = "";
            var DD29 = "";
            //Day 30
            var D30 = "";
            var DD30 = "";
            //Day 31
            var D31 = "";
            var DD31 = "";

            //Day 1

            D1 = View.GetRowCellValue(e.RowHandle, "Day1").ToString() + "                                             ";
            D1 = D1.Substring(0, 1);

            DD1 = View.GetRowCellValue(e.RowHandle, "Day1Clock").ToString() + "                                             ";
            DD1 = DD1.Substring(0, 2);

            //Day 2

            D2 = View.GetRowCellValue(e.RowHandle, "Day2").ToString() + "                                             ";
            D2 = D2.Substring(0, 1);

            DD2 = View.GetRowCellValue(e.RowHandle, "Day2Clock").ToString() + "                                             ";
            DD2 = DD2.Substring(0, 2);

            //Day 3

            D3 = View.GetRowCellValue(e.RowHandle, "Day3").ToString() + "                                             ";
            D3 = D3.Substring(0, 1);

            DD3 = View.GetRowCellValue(e.RowHandle, "Day3Clock").ToString() + "                                             ";
            DD3 = DD3.Substring(0, 2);

            //Day 4

            D4 = View.GetRowCellValue(e.RowHandle, "Day4").ToString() + "                                             ";
            D4 = D4.Substring(0, 1);

            DD4 = View.GetRowCellValue(e.RowHandle, "Day4Clock").ToString() + "                                             ";
            DD4 = DD4.Substring(0, 2);

            //Day 5

            D5 = View.GetRowCellValue(e.RowHandle, "Day5").ToString() + "                                             ";
            D5 = D5.Substring(0, 1);

            DD5 = View.GetRowCellValue(e.RowHandle, "Day5Clock").ToString() + "                                             ";
            DD5 = DD5.Substring(0, 2);

            //Day 6

            D6 = View.GetRowCellValue(e.RowHandle, "Day6").ToString() + "                                             ";
            D6 = D6.Substring(0, 1);

            DD6 = View.GetRowCellValue(e.RowHandle, "Day6Clock").ToString() + "                                             ";
            DD6 = DD6.Substring(0, 2);

            //Day 7

            D7 = View.GetRowCellValue(e.RowHandle, "Day7").ToString() + "                                             ";
            D7 = D7.Substring(0, 1);

            DD7 = View.GetRowCellValue(e.RowHandle, "Day7Clock").ToString() + "                                             ";
            DD7 = DD7.Substring(0, 2);

            //Day 8

            D8 = View.GetRowCellValue(e.RowHandle, "Day8").ToString() + "                                             ";
            D8 = D8.Substring(0, 1);

            DD8 = View.GetRowCellValue(e.RowHandle, "Day8Clock").ToString() + "                                             ";
            DD8 = DD8.Substring(0, 2);

            //Day 9

            D9 = View.GetRowCellValue(e.RowHandle, "Day9").ToString() + "                                             ";
            D9 = D9.Substring(0, 1);

            DD9 = View.GetRowCellValue(e.RowHandle, "Day9Clock").ToString() + "                                             ";
            DD9 = DD9.Substring(0, 2);

            //Day 10

            D10 = View.GetRowCellValue(e.RowHandle, "Day10").ToString() + "                                             ";
            D10 = D10.Substring(0, 1);

            DD10 = View.GetRowCellValue(e.RowHandle, "Day10Clock").ToString() + "                                             ";
            DD10 = DD10.Substring(0, 2);

            //Day 11

            D11 = View.GetRowCellValue(e.RowHandle, "Day11").ToString() + "                                             ";
            D11 = D11.Substring(0, 1);

            DD11 = View.GetRowCellValue(e.RowHandle, "Day11Clock").ToString() + "                                             ";
            DD11 = DD11.Substring(0, 2);

            //Day12

            D12 = View.GetRowCellValue(e.RowHandle, "Day12").ToString() + "                                ";
            D12 = D12.Substring(0, 1);

            DD12 = View.GetRowCellValue(e.RowHandle, "Day12Clock").ToString() + "                                             ";
            DD12 = DD12.Substring(0, 2);

            //Day13

            D13 = View.GetRowCellValue(e.RowHandle, "Day13").ToString() + "                                ";
            D13 = D13.Substring(0, 1);

            DD13 = View.GetRowCellValue(e.RowHandle, "Day13Clock").ToString() + "                                             ";
            DD13 = DD13.Substring(0, 2);

            //Day14

            D14 = View.GetRowCellValue(e.RowHandle, "Day14").ToString() + "                                ";
            D14 = D14.Substring(0, 1);

            DD14 = View.GetRowCellValue(e.RowHandle, "Day14Clock").ToString() + "                                             ";
            DD14 = DD14.Substring(0, 2);

            //Day15

            D15 = View.GetRowCellValue(e.RowHandle, "Day15").ToString() + "                                ";
            D15 = D15.Substring(0, 1);

            DD15 = View.GetRowCellValue(e.RowHandle, "Day15Clock").ToString() + "                                             ";
            DD15 = DD15.Substring(0, 2);

            //Day16

            D16 = View.GetRowCellValue(e.RowHandle, "Day16").ToString() + "                                ";
            D16 = D16.Substring(0, 1);

            DD16 = View.GetRowCellValue(e.RowHandle, "Day16Clock").ToString() + "                                             ";
            DD16 = DD16.Substring(0, 2);

            //Day17

            D17 = View.GetRowCellValue(e.RowHandle, "Day17").ToString() + "                                ";
            D17 = D17.Substring(0, 3);

            DD17 = View.GetRowCellValue(e.RowHandle, "Day17Clock").ToString() + "                                             ";
            DD17 = DD17.Substring(0, 2);

            //Day18

            D18 = View.GetRowCellValue(e.RowHandle, "Day18").ToString() + "                                ";
            D18 = D18.Substring(0, 1);

            DD18 = View.GetRowCellValue(e.RowHandle, "Day18Clock").ToString() + "                                             ";
            DD18 = DD18.Substring(0, 2);

            //Day19

            D19 = View.GetRowCellValue(e.RowHandle, "Day19").ToString() + "                                ";
            D19 = D19.Substring(0, 1);

            DD19 = View.GetRowCellValue(e.RowHandle, "Day19Clock").ToString() + "                                             ";
            DD19 = DD19.Substring(0, 2);

            //Day20

            D20 = View.GetRowCellValue(e.RowHandle, "Day20").ToString() + "                                ";
            D20 = D20.Substring(0, 1);

            DD20 = View.GetRowCellValue(e.RowHandle, "Day20Clock").ToString() + "                                             ";
            DD20 = DD20.Substring(0, 2);

            //Day21

            D21 = View.GetRowCellValue(e.RowHandle, "Day21").ToString() + "                                ";
            D21 = D21.Substring(0, 1);

            DD21 = View.GetRowCellValue(e.RowHandle, "Day21Clock").ToString() + "                                             ";
            DD21 = DD21.Substring(0, 2);

            //Day22

            D22 = View.GetRowCellValue(e.RowHandle, "Day22").ToString() + "                                ";
            D22 = D22.Substring(0, 1);

            DD22 = View.GetRowCellValue(e.RowHandle, "Day22Clock").ToString() + "                                             ";
            DD22 = DD22.Substring(0, 2);

            //Day23

            D23 = View.GetRowCellValue(e.RowHandle, "Day23").ToString() + "                                ";
            D23 = D23.Substring(0, 1);

            DD23 = View.GetRowCellValue(e.RowHandle, "Day23Clock").ToString() + "                                             ";
            DD23 = DD23.Substring(0, 2);

            //Day24

            D24 = View.GetRowCellValue(e.RowHandle, "Day24").ToString() + "                                ";
            D24 = D24.Substring(0, 1);

            DD24 = View.GetRowCellValue(e.RowHandle, "Day24Clock").ToString() + "                                             ";
            DD24 = DD24.Substring(0, 2);

            //Day25

            D25 = View.GetRowCellValue(e.RowHandle, "Day25").ToString() + "                                ";
            D25 = D25.Substring(0, 1);

            DD25 = View.GetRowCellValue(e.RowHandle, "Day25Clock").ToString() + "                                             ";
            DD25 = DD25.Substring(0, 2);

            //Day26

            D26 = View.GetRowCellValue(e.RowHandle, "Day26").ToString() + "                                ";
            D26 = D26.Substring(0, 1);

            DD26 = View.GetRowCellValue(e.RowHandle, "Day26Clock").ToString() + "                                             ";
            DD26 = DD26.Substring(0, 2);

            //Day27

            D27 = View.GetRowCellValue(e.RowHandle, "Day27").ToString() + "                                ";
            D27 = D27.Substring(0, 1);

            DD27 = View.GetRowCellValue(e.RowHandle, "Day27Clock").ToString() + "                                             ";
            DD27 = DD27.Substring(0, 2);

            //Day28

            D28 = View.GetRowCellValue(e.RowHandle, "Day28").ToString() + "                                ";
            D28 = D28.Substring(0, 1);

            DD28 = View.GetRowCellValue(e.RowHandle, "Day28Clock").ToString() + "                                             ";
            DD28 = DD28.Substring(0, 2);

            //Day29

            D29 = View.GetRowCellValue(e.RowHandle, "Day29").ToString() + "                                ";
            D29 = D29.Substring(0, 1);

            DD29 = View.GetRowCellValue(e.RowHandle, "Day29Clock").ToString() + "                                             ";
            DD29 = DD29.Substring(0, 2);

            //Day30

            D30 = View.GetRowCellValue(e.RowHandle, "Day30").ToString() + "                                ";
            D30 = D30.Substring(0, 1);

            DD30 = View.GetRowCellValue(e.RowHandle, "Day30Clock").ToString() + "                                             ";
            DD30 = DD30.Substring(0, 2);

            //Day31

            D31 = View.GetRowCellValue(e.RowHandle, "Day31").ToString() + "                                ";
            D31 = D31.Substring(0, 1);

            DD31 = View.GetRowCellValue(e.RowHandle, "Day31Clock").ToString() + "                                             ";
            DD31 = DD31.Substring(0, 2);

            ////Day 1 

            //if (e.Column.AbsoluteIndex == 3)
            //{
            //    if (D1 == "S" || DD1 == "YS")
            //    {
            //        e.Appearance.BackColor = Color.MistyRose;
            //        //  e.DisplayText = wd1clock;

            //    }

            //}

            ////Day 2


            //if (e.Column.AbsoluteIndex == 4)
            //{
            //    if (D2 == "S" || DD2 == "YS")
            //    {
            //        e.Appearance.BackColor = Color.MistyRose;

            //    }

            //}

            ////Day 3


            //if (e.Column.AbsoluteIndex == 5)
            //{
            //    if (D3 == "S" || DD3 == "YS")
            //    {
            //        e.Appearance.BackColor = Color.MistyRose;

            //    }

            //}

            ////Day 4


            //if (e.Column.AbsoluteIndex == 6)
            //{
            //    if (D4 == "S" || DD4 == "YS")
            //    {
            //        e.Appearance.BackColor = Color.MistyRose;

            //    }

            //}

            ////Day 5


            //if (e.Column.AbsoluteIndex == 7)
            //{
            //    if (D5 == "S" || DD5 == "YS")
            //    {
            //        e.Appearance.BackColor = Color.MistyRose;

            //    }

            //}

            ////Day 6


            //if (e.Column.AbsoluteIndex == 8)
            //{
            //    if (D6 == "S" || DD6 == "YS")
            //    {
            //        e.Appearance.BackColor = Color.MistyRose;

            //    }

            //}

            ////Day 7


            //if (e.Column.AbsoluteIndex == 9)
            //{
            //    if (D7 == "S" || DD7 == "YS")
            //    {
            //        e.Appearance.BackColor = Color.MistyRose;

            //    }

            //}

            ////Day 8


            //if (e.Column.AbsoluteIndex == 10)
            //{
            //    if (D8 == "S" || DD8 == "YS")
            //    {
            //        e.Appearance.BackColor = Color.MistyRose;

            //    }

            //}

            ////Day 9


            //if (e.Column.AbsoluteIndex == 11) { if (D9 == "S" || DD9 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day10
            //if (e.Column.AbsoluteIndex == 12) { if (D10 == "S" || DD10 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day11
            //if (e.Column.AbsoluteIndex == 13) { if (D11 == "S" || DD11 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day12
            //if (e.Column.AbsoluteIndex == 14) { if (D12 == "S" || DD12 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day13
            //if (e.Column.AbsoluteIndex == 15) { if (D13 == "S" || DD13 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day14
            //if (e.Column.AbsoluteIndex == 16) { if (D14 == "S" || DD14 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day15
            //if (e.Column.AbsoluteIndex == 17) { if (D15 == "S" || DD15 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day16
            //if (e.Column.AbsoluteIndex == 18) { if (D16 == "S" || DD16 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day17
            //if (e.Column.AbsoluteIndex == 19) { if (D17 == "S" || DD17 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day18
            //if (e.Column.AbsoluteIndex == 20) { if (D18 == "S" || DD18 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day19
            //if (e.Column.AbsoluteIndex == 21) { if (D19 == "S" || DD19 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day20
            //if (e.Column.AbsoluteIndex == 22) { if (D20 == "S" || DD20 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day21
            //if (e.Column.AbsoluteIndex == 23) { if (D21 == "S" || DD21 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day22
            //if (e.Column.AbsoluteIndex == 24) { if (D22 == "S" || DD22 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day23
            //if (e.Column.AbsoluteIndex == 25) { if (D23 == "S" || DD23 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day24
            //if (e.Column.AbsoluteIndex == 26) { if (D24 == "S" || DD24 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day25
            //if (e.Column.AbsoluteIndex == 27) { if (D25 == "S" || DD25 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day26
            //if (e.Column.AbsoluteIndex == 28) { if (D26 == "S" || DD26 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day27
            //if (e.Column.AbsoluteIndex == 29) { if (D27 == "S" || DD27 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day28
            //if (e.Column.AbsoluteIndex == 30) { if (D28 == "S" || DD28 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day29
            //if (e.Column.AbsoluteIndex == 31) { if (D29 == "S" || DD29 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day30
            //if (e.Column.AbsoluteIndex == 32) { if (D30 == "S" || DD30 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            ////Day31
            //if (e.Column.AbsoluteIndex == 33) { if (D31 == "S" || DD31 == "YS") { e.Appearance.BackColor = Color.MistyRose; } }

            //Day 1

            if (e.Column.AbsoluteIndex == 9)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day1").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 2

            if (e.Column.AbsoluteIndex == 10)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day2").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 3

            if (e.Column.AbsoluteIndex == 11)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day3").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 4

            if (e.Column.AbsoluteIndex == 12)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day4").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 5

            if (e.Column.AbsoluteIndex == 13)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day5").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 6

            if (e.Column.AbsoluteIndex == 14)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day6").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 7

            if (e.Column.AbsoluteIndex == 15)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day7").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 8

            if (e.Column.AbsoluteIndex == 16)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day8").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 9

            if (e.Column.AbsoluteIndex == 17)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day9").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 10

            if (e.Column.AbsoluteIndex == 18)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day10").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 11

            if (e.Column.AbsoluteIndex == 19)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day11").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 12

            if (e.Column.AbsoluteIndex == 20)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day12").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 13

            if (e.Column.AbsoluteIndex == 21)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day13").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 14

            if (e.Column.AbsoluteIndex == 22)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day14").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 15

            if (e.Column.AbsoluteIndex == 23)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day15").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 16

            if (e.Column.AbsoluteIndex == 24)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day16").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 17

            if (e.Column.AbsoluteIndex == 25)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day17").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 18

            if (e.Column.AbsoluteIndex == 26)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day18").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 19

            if (e.Column.AbsoluteIndex == 27)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day19").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 20

            if (e.Column.AbsoluteIndex == 28)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day20").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 21

            if (e.Column.AbsoluteIndex == 29)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day21").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 22

            if (e.Column.AbsoluteIndex == 30)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day22").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 23

            if (e.Column.AbsoluteIndex == 31)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day23").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 24

            if (e.Column.AbsoluteIndex == 32)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day24").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 25

            if (e.Column.AbsoluteIndex == 33)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day25").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 26

            if (e.Column.AbsoluteIndex == 34)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day26").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 27

            if (e.Column.AbsoluteIndex == 35)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day27").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 28

            if (e.Column.AbsoluteIndex == 36)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day28").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 29

            if (e.Column.AbsoluteIndex == 37)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day29").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 30

            if (e.Column.AbsoluteIndex == 38)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day30").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }

            //Day 31

            if (e.Column.AbsoluteIndex == 39)
            {
                if (View.GetRowCellValue(e.RowHandle, "Day31").ToString() == "S ")
                {
                    e.Appearance.ForeColor = Color.MistyRose;
                }
            }
        }

        private void tcTeams_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcTeams.SelectedTab == tbTeamsAG)
            {
                lblTeams.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                lblOther.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else
            {
                lblTeams.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                lblOther.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OnCloseTabRequest(new CloseTabArg(tabCaption));
        }

        private void editHoistMonth_EditValueChanged(object sender, EventArgs e)
        {
            LoadDate();
            LoadDataTram();
            LoadTrammingAtoG();
            LoadTrammingOther();
        }
    }
}
