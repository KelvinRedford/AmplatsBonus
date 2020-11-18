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
using System.IO;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraPrinting;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.ProductionAmplatsBonus
{
    public partial class ucProductionMinersCapture : BaseUserControl
    {
        public ucProductionMinersCapture()
        {
            InitializeComponent();
            FormRibbonPages.Add(rpMain);
            FormActiveRibbonPage = rpMain;
            FormMainRibbonPage = rpMain;
            RibbonControl = rcMain;
        }

        Procedures procs = new Procedures();

        public DateTime startDate;

        private bool raninsert = false;
        private bool randelete = false;

        //PDF Checker
        private string directory;
        private string pdfName;
        private string fileName;

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
            pdfName = directory + @"\Production " + DateTime.Now.ToString("MMM-dd-yyyy HH-mm-ss") + fileName + ".pdf";
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
            TramUpdate.SqlStatement = "DELETE FROM [Mineware].[dbo].[tbl_BCS_Gangs_3Month] " +
                                          " WHERE OrgUnit = '" + dgvProdMine.Rows[0].Cells["Orgunit"].FormattedValue.ToString() + "' " +
                                          "  AND ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  " +
				                          "  AND Date = '1900-01-01 00:00:00.000'";
            UseWaitCursor = true;
            foreach (DataGridViewRow row in dgvProdMine.Rows)
            {
                if (dgvProdMine.Rows[row.Index].Cells["Value"].FormattedValue.ToString() == "N ")
                {
                    // Row Updated To New Value (Update the current row value)
                    TramUpdate.SqlStatement = TramUpdate.SqlStatement + "INSERT INTO [Mineware].[dbo].[tbl_BCS_Gangs_3Month] " +
                        " VALUES( '1'" + //Gang ID
                   " ,'" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' " + //prodmonth
                   " ,'0' " + // activitycode
                   " ,'" + dgvProdMine.Rows[row.Index].Cells["Day"].FormattedValue.ToString() + "' " + //date
                   " ,'" + dgvProdMine.Rows[row.Index].Cells["Orgunit"].FormattedValue.ToString() + "' " + //orgunit
                   " ,'0' " + // pasnumber
                   " ,'' " + //workplace
                   " ,'' " + //panel
                   " ,'" + dgvProdMine.Rows[row.Index].Cells["Shift2"].FormattedValue.ToString() + "' " + //shift
                   " ,'' " + //firstshift
                   " ,'1' " + //numbershift
                   " ,'1' " +//totalshift
                   " ,'" + dgvProdMine.Rows[row.Index].Cells["Designation"].FormattedValue.ToString() + "' " + //category
                   " ,'" + dgvProdMine.Rows[row.Index].Cells["IndustryNumber"].FormattedValue.ToString() + "' " + //industrynumber
                   " ,'" + dgvProdMine.Rows[row.Index].Cells["Value"].FormattedValue.ToString() + "' " + //codes
                   " ,'N' " + //transfer
                   " ,'0' " + //transferedorgunit
                   " ,'N' " + // highlight
                   " ,'" + Environment.UserName + "' " + // systemuser
                   " ,'" + DateTime.Now + "' " + // timestamp
                   " ,'0' " + // exceptionid
                   " ,'0' " + // gangtype
                   " ,'0' " + // mineemployee
                   " ,'0' " + // section
                   " ,'' " + // updated
                   " ,'')"; // updateddate
                }
            }
            raninsert = true;
            TramUpdate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            TramUpdate.queryReturnType = MWDataManager.ReturnType.longNumber;
            TramUpdate.ExecuteInstruction();
            UseWaitCursor = false;
        }

        void UpdateTrammingOtherInsertUpdate()
        {            // Update value
            MWDataManager.clsDataAccess TramUpdate = new MWDataManager.clsDataAccess();
            TramUpdate.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            TramUpdate.SqlStatement = "DELETE FROM [Mineware].[dbo].[tbl_BCS_Gangs] " +
                                          " WHERE OrgUnit = '" + dgvProdMine.Rows[0].Cells["Orgunit"].FormattedValue.ToString() + "' " +
                                          "  AND ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  " +
				                          "  AND Date = '1900-01-01 00:00:00.000'";
            UseWaitCursor = true;
           foreach (DataGridViewRow row in dgvProdMine.Rows)
            {
                if (dgvProdMine.Rows[row.Index].Cells["Value"].FormattedValue.ToString() == "N ")
                {
                    // Row Updated To New Value (Update the current row value)
                    TramUpdate.SqlStatement = TramUpdate.SqlStatement + "INSERT INTO [Mineware].[dbo].[tbl_BCS_Gangs] " +
                        " VALUES('1' " + //Gang ID
                   " ,'" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' " + //prodmonth
                   " ,'0' " + // activitycode
                   " ,'" + dgvProdMine.Rows[row.Index].Cells["Day"].FormattedValue.ToString() + "' " + //date
                   " ,'" + dgvProdMine.Rows[row.Index].Cells["Orgunit"].FormattedValue.ToString() + "' " + //orgunit
                   " ,'0' " + // pasnumber
                   " ,'' " + //workplace
                   " ,'' " + //panel
                   " ,'" + dgvProdMine.Rows[row.Index].Cells["Shift2"].FormattedValue.ToString() + "' " + //shift
                   " ,'' " + //firstshift
                   " ,'1' " + //numbershift
                   " ,'1' " +//totalshift
                   " ,'" + dgvProdMine.Rows[row.Index].Cells["Designation"].FormattedValue.ToString() + "' " + //category
                   " ,'" + dgvProdMine.Rows[row.Index].Cells["IndustryNumber"].FormattedValue.ToString() + "' " + //industrynumber
                   " ,'" + dgvProdMine.Rows[row.Index].Cells["Value"].FormattedValue.ToString() + "' " + //codes
                   " ,'N' " + //transfer
                   " ,'0' " + //transferedorgunit
                   " ,'N' " + // highlight
                   " ,'" + Environment.UserName + "' " + // systemuser
                   " ,'" + DateTime.Now + "' " + // timestamp
                   " ,'0' " + // exceptionid
                   " ,'0' " + // gangtype
                   " ,'0' " + // mineemployee
                   " ,'0' " + // section
                   " ,'' " + // updated
                   " ,'')"; // updateddate

                }
            }
            raninsert = true;
            TramUpdate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            TramUpdate.queryReturnType = MWDataManager.ReturnType.longNumber;
            TramUpdate.ExecuteInstruction();
            UseWaitCursor = false;
        }


        //void UpdateTrammingOtherInsertUpdate()
        //{
        //    // Update value
        //    MWDataManager.clsDataAccess TramUpdate = new MWDataManager.clsDataAccess();
        //    TramUpdate.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
        //    TramUpdate.SqlStatement = "SELECT '' AS A";//Not Used :)
        //    UseWaitCursor = true;
        //    foreach (DataGridViewRow row in dgvProdMine.Rows)
        //    {
        //        if (dgvProdMine.Rows[row.Index].Cells["Value"].FormattedValue.ToString() == "N ")
        //        {
        //            // Row Updated To New Value (Update the current row value)
        //            TramUpdate.SqlStatement = TramUpdate.SqlStatement + " IF EXISTS (SELECT * FROM [NorthamPas].[dbo].[tbl_BCS_Gangs] WHERE IndustryNumber = '" + dgvProdMine.Rows[row.Index].Cells["IndustryNumber"].FormattedValue.ToString() + "' AND OrgUnit = '" + dgvProdMine.Rows[row.Index].Cells["Orgunit"].FormattedValue.ToString() + "' AND ProdMonth = '" + MillMonth.Value + "' AND Date ='1900-01-01 00:00:00.000' AND Shift = '" + dgvProdMine.Rows[row.Index].Cells["Shift2"].FormattedValue.ToString() + "' AND Category = '" + dgvProdMine.Rows[row.Index].Cells["Designation"].FormattedValue.ToString() + "') " +
        //             "BEGIN " +
        //             "UPDATE [NorthamPas].[dbo].[tbl_BCS_Gangs] " +
        //             "SET Codes = '" + dgvProdMine.Rows[row.Index].Cells["Value"].FormattedValue.ToString() + "', Date = '" + dgvProdMine.Rows[row.Index].Cells["Day"].FormattedValue.ToString() + "' " +
        //             "WHERE IndustryNumber = '" + dgvProdMine.Rows[row.Index].Cells["IndustryNumber"].FormattedValue.ToString() + "' AND OrgUnit = '" + dgvProdMine.Rows[row.Index].Cells["Orgunit"].FormattedValue.ToString() + "' AND Date = '1900-01-01 00:00:00.000' AND Shift = '" + dgvProdMine.Rows[row.Index].Cells["Shift2"].FormattedValue.ToString() + "' AND Category = '" + dgvProdMine.Rows[row.Index].Cells["Designation"].FormattedValue.ToString() + "' " +
        //             "END " +
        //             "ELSE " +
        //             "BEGIN " +
        //             "INSERT INTO [NorthamPas].[dbo].[tbl_BCS_Gangs] (GangID, ProdMonth, ActivityCode, OrgUnit, PasNumber, Workplace, Panel " +
        //              "  , Shift, FirstShift, NumberShift, TotalShift, Category, IndustryNumber, Transfer, TransferOrgUnit, Highlight, SystemUser, TimeStamp,ExceptionID, GangType,MineEmployee,Section,Updated,UpdatedDate) " +
        //              "  SELECT TOP (1) GangID, ProdMonth, ActivityCode, OrgUnit, PasNumber, Workplace, Panel " +
        //              "  , Shift, FirstShift, NumberShift, TotalShift, Category, IndustryNumber, Transfer, TransferOrgUnit, Highlight, SystemUser, TimeStamp,ExceptionID, GangType,MineEmployee,Section,Updated,UpdatedDate " +
        //              "  FROM [NorthamPas].[dbo].[tbl_BCS_Gangs] " +
        //              "  WHERE IndustryNumber = '" + dgvProdMine.Rows[row.Index].Cells["IndustryNumber"].FormattedValue.ToString() + "' AND OrgUnit = '" + dgvProdMine.Rows[row.Index].Cells["Orgunit"].FormattedValue.ToString() + "' AND ProdMonth = '" + MillMonth.Value + "' AND Shift = '" + dgvProdMine.Rows[row.Index].Cells["Shift2"].FormattedValue.ToString() + "' AND Category = '" + dgvProdMine.Rows[row.Index].Cells["Designation"].FormattedValue.ToString() + "' " +
        //              "  UPDATE [NorthamPas].[dbo].[tbl_BCS_Gangs] " +
        //              "  SET Codes = '" + dgvProdMine.Rows[row.Index].Cells["Value"].FormattedValue.ToString() + "', Date = '" + dgvProdMine.Rows[row.Index].Cells["Day"].FormattedValue.ToString() + "' " +
        //              "  WHERE IndustryNumber = '" + dgvProdMine.Rows[row.Index].Cells["IndustryNumber"].FormattedValue.ToString() + "' AND OrgUnit = '" + dgvProdMine.Rows[row.Index].Cells["Orgunit"].FormattedValue.ToString() + "' AND ProdMonth = '" + MillMonth.Value + "' AND Date IS NULL AND Shift = '" + dgvProdMine.Rows[row.Index].Cells["Shift2"].FormattedValue.ToString() + "' AND Category = '" + dgvProdMine.Rows[row.Index].Cells["Designation"].FormattedValue.ToString() + "' " +
        //              "  DELETE FROM [NorthamPas].[dbo].[tbl_BCS_Gangs] WHERE IndustryNumber = '" + dgvProdMine.Rows[row.Index].Cells["IndustryNumber"].FormattedValue.ToString() + "' AND OrgUnit = '" + dgvProdMine.Rows[row.Index].Cells["Orgunit"].FormattedValue.ToString() + "' AND ProdMonth = '" + MillMonth.Value + "' AND Date = '1900-01-01 00:00:00.000' " +
        //              " END";
        //        }
        //    }
        //    raninsert = true;
        //    TramUpdate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
        //    TramUpdate.queryReturnType = MWDataManager.ReturnType.longNumber;
        //    TramUpdate.ExecuteInstruction();
        //    UseWaitCursor = false;
        //}

        void LoadNextProdMonth()
        {
            MWDataManager.clsDataAccess _dbMan1New = new MWDataManager.clsDataAccess();
            _dbMan1New.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan1New.SqlStatement = "IF NOT EXISTS  \r\n" +
                " (SELECT * FROM [NorthamPas].[dbo].[tbl_BCS_GangsToBeDeleted] \r\n" +
                " WHERE ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') \r\n" +
                " BEGIN \r\n" +
                " INSERT INTO [NorthamPas].[dbo].[tbl_BCS_GangsToBeDeleted] (GangID ,ActivityCode,Date, OrgUnit, PasNumber, Workplace, Panel, Shift, \r\n" +
                " FirstShift,NumberShift, TotalShift, Category, IndustryNumber, Codes \r\n" +
			    " ,Transfer, TransferOrgUnit, Highlight, SystemUser, ExceptionID, GangType, MineEmployee, Section, Updated, UpdatedDate) \r\n" +
                " SELECT GangID ,ActivityCode,Date, OrgUnit, PasNumber, Workplace, Panel, Shift, FirstShift,NumberShift, TotalShift, Category, IndustryNumber, Codes \r\n" +
			    " ,Transfer, TransferOrgUnit, Highlight, SystemUser, ExceptionID, GangType, MineEmployee, Section, Updated, UpdatedDate \r\n" +
	            " FROM [NorthamPas].[dbo].[tbl_BCS_GangsToBeDeleted] WHERE date >= (SELECT MAX(Date) - 7 FROM [NorthamPas].[dbo].[tbl_BCS_GangsToBeDeleted]) \r\n" +
	            " UPDATE [NorthamPas].[dbo].[tbl_BCS_GangsToBeDeleted] \r\n" +
                " SET ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', TimeStamp = '" + DateTime.Today + "' \r\n" +
	            " WHERE ProdMonth IS NULL \r\n" +
	            " END ";
            _dbMan1New.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1New.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1New.ExecuteInstruction();
        }

        void UpdateTrammingOtherDelete_3Month()
        {
            //Delete value
            MWDataManager.clsDataAccess TramUpdate = new MWDataManager.clsDataAccess();
            TramUpdate.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            TramUpdate.SqlStatement = "SELECT '' AS A";//Not Used :)
            UseWaitCursor = true;
            foreach (DataGridViewRow row in dgvProdMine.Rows)
            {
                if (dgvProdMine.Rows[row.Index].Cells["Value"].FormattedValue.ToString() == "Delete")
                {
                    // Row Deleted if value is set to empty
                    TramUpdate.SqlStatement = TramUpdate.SqlStatement + " DELETE FROM [Mineware].[dbo].[tbl_BCS_Gangs_3Month] " +
                     " WHERE IndustryNumber = '" + dgvProdMine.Rows[row.Index].Cells["IndustryNumber"].FormattedValue.ToString() + "' AND OrgUnit = '" + dgvProdMine.Rows[row.Index].Cells["Orgunit"].FormattedValue.ToString() + "' AND ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' AND Date = '" + dgvProdMine.Rows[row.Index].Cells["Day"].FormattedValue.ToString() + "' AND Shift = '" + dgvProdMine.Rows[row.Index].Cells["Shift2"].FormattedValue.ToString() + "' AND Category = '" + dgvProdMine.Rows[row.Index].Cells["Designation"].FormattedValue.ToString() + "'";
                }
            }
            randelete = true;
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
            foreach (DataGridViewRow row in dgvProdMine.Rows)
            {
                if (dgvProdMine.Rows[row.Index].Cells["Value"].FormattedValue.ToString() == "Delete")
                {
                    // Row Deleted if value is set to empty
                    TramUpdate.SqlStatement = TramUpdate.SqlStatement + " DELETE FROM [Mineware].[dbo].[tbl_BCS_Gangs] " +
                     " WHERE IndustryNumber = '" + dgvProdMine.Rows[row.Index].Cells["IndustryNumber"].FormattedValue.ToString() + "' AND OrgUnit = '" + dgvProdMine.Rows[row.Index].Cells["Orgunit"].FormattedValue.ToString() + "' AND ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' AND Date = '" + dgvProdMine.Rows[row.Index].Cells["Day"].FormattedValue.ToString() + "' AND Shift = '" + dgvProdMine.Rows[row.Index].Cells["Shift2"].FormattedValue.ToString() + "' AND Category = '" + dgvProdMine.Rows[row.Index].Cells["Designation"].FormattedValue.ToString() + "'";
                }
            }
            randelete = true;
            TramUpdate.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            TramUpdate.queryReturnType = MWDataManager.ReturnType.longNumber;
            TramUpdate.ExecuteInstruction();
            UseWaitCursor = false;
        }

        void LoadStartAndEnd()
        {
            MWDataManager.clsDataAccess _dbMan1New = new MWDataManager.clsDataAccess();
            _dbMan1New.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            //_dbMan1New.SqlStatement = "declare @pm varchar(10) \r\n" +
            //                       "set @pm = '" + MillMonth.Value + "' \r\n" +

            //                       "select Startdate, (startdate + 32)-day((startdate + 32)) bb from ( \r\n" +
            //                       "select convert(datetime,(substring(@pm,1,4)+ '-'+substring(@pm,5,2) +'-01')) Startdate) a \r\n";
            _dbMan1New.SqlStatement = "SELECT TOP(1) BeginDate, EndDate FROM [mineware].[dbo].[tbl_BCS_SECCAL] WHERE Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' AND Sectionid LIKE '" + Orgunitlbl.Text.Substring(0, 4) + "%'";
            _dbMan1New.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1New.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1New.ExecuteInstruction();
            DataTable Neil = _dbMan1New.ResultsDataTable;
            if (Neil.Rows.Count != 0)
            {
                StartDate.Value = Convert.ToDateTime(Neil.Rows[0]["BeginDate"].ToString());
                EndDate.Value = Convert.ToDateTime(Neil.Rows[0]["EndDate"].ToString());
                startDate = Convert.ToDateTime(Neil.Rows[0]["BeginDate"].ToString());

            }


        }

        void LoadTrammingOther()
        {
            this.Cursor = Cursors.WaitCursor;
            this.UseWaitCursor = true;
            gvProdMineOther.Bands[0].Caption = "Production Shift Captures For: " + Orglbl.Text;
            LoadStartAndEnd();
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

            _dbMan12.SqlStatement = "  declare @start datetime \r\n" +
 "declare @end datetime  \r\n" +
 "set @start = '"+String.Format("{0:yyyy-MM-dd}", StartDate.Value)+"'  \r\n" +
 "set @end = '" + String.Format("{0:yyyy-MM-dd}", EndDate.Value) + "'  \r\n" +
 "SELECT * from   \r\n" +
 "(  \r\n" +
 "SELECT IndustryNumber \r\n" +
 ",max(Designation) Designation \r\n" +
 ",max(Shift) Shift \r\n" +
 ",max(Day1) Day1  \r\n" +
 ",max(Day2) Day2  \r\n" +
 ",max(Day3) Day3  \r\n" +
 ",max(Day4) Day4  \r\n" +
 ",max(day5) Day5  \r\n" +
 ",max(day6) Day6  \r\n" +
 ",max(day7) Day7  \r\n" +
 ",max(day8) Day8  \r\n" +
 ",max(day9) Day9  \r\n" +
 ",max(day10) Day10  \r\n" +
 ",max(day11) Day11  \r\n" +
 ",max(day12) Day12  \r\n" +
 ",max(day13) Day13  \r\n" +
 ",max(day14) Day14  \r\n" +
 ",max(day15) Day15  \r\n" +
 ",max(day16) Day16  \r\n" +
 ",max(day17) Day17  \r\n" +
 ",max(day18) Day18  \r\n" +
 ",max(day19) Day19  \r\n" +
 ",max(day20) Day20  \r\n" +
 ",max(day21) Day21  \r\n" +
 ",max(day22) Day22  \r\n" +
 ",max(day23) Day23  \r\n" +
 ",max(day24) Day24  \r\n" +
 ",max(day25) Day25  \r\n" +
 ",max(day26) Day26  \r\n" +
 ",max(day27) Day27  \r\n" +
 ",max(day28) Day28  \r\n" +
 ",max(day29) Day29  \r\n" +
 ",max(day30) Day30  \r\n" +
 ",max(day31) Day31  \r\n" +
 ",max(day32) Day32  \r\n" +
 ",max(day33) Day33  \r\n" +
 ",max(day34) Day34  \r\n" +
 ",max(day35) Day35  \r\n" +
 ",max(day36) Day36  \r\n" +
 ",max(day37) Day37  \r\n" +
 ",max(day38) Day38  \r\n" +
 ",max(day39) Day39  \r\n" +
 ",max(day40) Day40  \r\n" +
 ",max(day41) Day41  \r\n" +
 ",max(day42) Day42  \r\n" +
 ",max(day43) Day43  \r\n" +
 ",max(day44) Day44  \r\n" +
 ",max(day45) Day45  \r\n" +
 "FROM (  \r\n" +
 "SELECT IndustryNumber, Category AS [Designation], Shift, \r\n" +
 "CASE  WHEN @start = date  then convert(varchar(10),Codes)    \r\n" +
 "else '' end as Day1,  \r\n" +
 "CASE WHEN @start+1 = date then convert(varchar(10),Codes)    \r\n" +
 "else '' end as Day2,  \r\n" +
 "CASE WHEN @start+2 = date then convert(varchar(10),Codes)    \r\n" +
 "else '' end as Day3,  \r\n" +
 "CASE WHEN @start+3 = date then convert(varchar(10),Codes)   \r\n" +
 "else '' end as Day4,  \r\n" +
 "CASE WHEN @start+4 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day5,  \r\n" +
 "CASE WHEN @start+5 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day6,  \r\n" +
 "CASE WHEN @start+6 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day7,  \r\n" +
 "CASE WHEN @start+7 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day8,  \r\n" +
 "CASE WHEN @start+8 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day9,    \r\n" +
 "CASE WHEN @start+9 = date then convert(varchar(10),Codes)   \r\n" +
 "else '' end as Day10,   \r\n" +
 "CASE WHEN @start+10 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day11,  \r\n" +
 "CASE WHEN @start+11 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day12,  \r\n" +
 "CASE WHEN @start+12 = date then convert(varchar(10),Codes)   \r\n" +
 "else '' end as Day13,  \r\n" +
 "CASE WHEN @start+13 = date then convert(varchar(10),Codes)   \r\n" +
 "else '' end as Day14,  \r\n" +
 "CASE WHEN @start+14 = date then convert(varchar(10),Codes)   \r\n" +
 "else '' end as Day15,  \r\n" +
 "CASE WHEN @start+15 = date then convert(varchar(10),Codes) \r\n" +
 "else '' end as Day16,  \r\n" +
 "CASE WHEN @start+16 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day17,  \r\n" +
 "CASE WHEN @start+17 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day18,  \r\n" +
 "CASE WHEN @start+18 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day19,  \r\n" +
 "CASE WHEN @start+19 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day20,  \r\n" +
 "CASE WHEN @start+20 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day21,  \r\n" +
 "CASE WHEN @start+21 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day22,  \r\n" +
 "CASE WHEN @start+22 = date then convert(varchar(10),Codes)   \r\n" +
 "else '' end as Day23,  \r\n" +
 "CASE WHEN @start+23 = date then convert(varchar(10),Codes)   \r\n" +
 "else '' end as Day24,  \r\n" +
 "CASE WHEN @start+24 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day25,    \r\n" +
 "CASE WHEN @start+25 = date then convert(varchar(10),Codes)   \r\n" +
 "else '' end as Day26,   \r\n" +
 "CASE WHEN @start+26 = date then convert(varchar(10),Codes)   \r\n" +
 "else '' end as Day27,  \r\n" +
 "CASE WHEN @start+27 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day28,  \r\n" +
 "CASE WHEN @start+28 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day29,  \r\n" +
 "CASE WHEN @start+29 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day30,  \r\n" +
 "CASE WHEN @start+30 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day31,  \r\n" +
  "CASE WHEN @start+31 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day32,  \r\n" +
  "CASE WHEN @start+32 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day33,  \r\n" +
   "CASE WHEN @start+33 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day34,  \r\n" +
   "CASE WHEN @start+34 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day35,  \r\n" +
   "CASE WHEN @start+35 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day36,  \r\n" +
   "CASE WHEN @start+36 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day37,  \r\n" +
   "CASE WHEN @start+37 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day38,  \r\n" +
   "CASE WHEN @start+38 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day39,  \r\n" +
   "CASE WHEN @start+39 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day40,  \r\n" +
   "CASE WHEN @start+40 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day41,  \r\n" +
   "CASE WHEN @start+41 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day42,  \r\n" +
   "CASE WHEN @start+42 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day43,  \r\n" +
   "CASE WHEN @start+43 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day44,  \r\n" +
   "CASE WHEN @start+44 = date then convert(varchar(10),Codes)  \r\n" +
 "else '' end as Day45  \r\n" +

 "FROM [Mineware].[dbo].[tbl_BCS_Gangs]    \r\n" +
 "WHERE  orgunit = '" + Orgunitlbl.Text + "'  and ProdMonth = '"+ ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'    \r\n" +
 ") a GROUP BY  IndustryNumber, Shift, designation \r\n" +
 ") a  \r\n" +
 "LEFT OUTER JOIN  \r\n" +
 "(  \r\n" +
"	SELECT nnn  \r\n" +
" ,max(Day1Clock) Day1Clock  \r\n" +
" ,max(Day2Clock) Day2Clock  \r\n" +
" ,max(Day3Clock) Day3Clock  \r\n" +
" ,max(Day4Clock) Day4Clock  \r\n" +
" ,max(Day5Clock) Day5Clock  \r\n" +
" ,max(Day6Clock) Day6Clock  \r\n" +
" ,max(Day7Clock) Day7Clock  \r\n" +
" ,max(Day8Clock) Day8Clock  \r\n" +
" ,max(Day9Clock) Day9Clock  \r\n" +
" ,max(Day10Clock) Day10Clock  \r\n" +
" ,max(Day11Clock) Day11Clock  \r\n" +
" ,max(Day12Clock) Day12Clock  \r\n" +
" ,max(Day13Clock) Day13Clock  \r\n" +
" ,max(Day14Clock) Day14Clock  \r\n" +
" ,max(Day15Clock) Day15Clock  \r\n" +
" ,max(Day16Clock) Day16Clock  \r\n" +
" ,max(Day17Clock) Day17Clock  \r\n" +
" ,max(Day18Clock) Day18Clock  \r\n" +
" ,max(Day19Clock) Day19Clock  \r\n" +
" ,max(Day20Clock) Day20Clock  \r\n" +
" ,max(Day21Clock) Day21Clock  \r\n" +
" ,max(Day22Clock) Day22Clock  \r\n" +
" ,max(Day23Clock) Day23Clock  \r\n" +
" ,max(Day24Clock) Day24Clock  \r\n" +
" ,max(Day25Clock) Day25Clock  \r\n" +
" ,max(Day26Clock) Day26Clock  \r\n" +
" ,max(Day27Clock) Day27Clock  \r\n" +
 ",max(Day28Clock) Day28Clock  \r\n" +
 ",max(Day29Clock) Day29Clock  \r\n" +
 ",max(Day30Clock) Day30Clock  \r\n" +
 ",max(Day31Clock) Day31Clock  \r\n" +
",max(Day32Clock) Day32Clock  \r\n" +
",max(Day33Clock) Day33Clock  \r\n" +
",max(Day34Clock) Day34Clock  \r\n" +
",max(Day35Clock) Day35Clock  \r\n" +
",max(Day36Clock) Day36Clock  \r\n" +
",max(Day37Clock) Day37Clock  \r\n" +
",max(Day38Clock) Day38Clock  \r\n" +
",max(Day39Clock) Day39Clock  \r\n" +
",max(Day40Clock) Day40Clock  \r\n" +
",max(Day41Clock) Day41Clock  \r\n" +
",max(Day42Clock) Day42Clock  \r\n" +
",max(Day43Clock) Day43Clock  \r\n" +
",max(Day44Clock) Day44Clock  \r\n" +
",max(Day45Clock) Day45Clock  \r\n" +
 "FROM   \r\n" +
 "(  \r\n" +
"	SELECT IndustryNumber nnn,  \r\n" +
" CASE WHEN TheDate = @start THEN expectedatwork+LeaveFlag END AS Day1Clock,  \r\n" +
" CASE WHEN TheDate = @start+1 THEN expectedatwork+LeaveFlag END AS Day2Clock,  \r\n" +
" CASE WHEN TheDate = @start+2 THEN expectedatwork+LeaveFlag END AS Day3Clock,  \r\n" +
 "CASE WHEN TheDate = @start+3 THEN expectedatwork+LeaveFlag END AS Day4Clock,  \r\n" +
 "CASE WHEN TheDate = @start+4 THEN expectedatwork+LeaveFlag END AS Day5Clock,  \r\n" +
 "CASE WHEN TheDate = @start+5 THEN expectedatwork+LeaveFlag END AS Day6Clock,  \r\n" +
 "CASE WHEN TheDate = @start+6 THEN expectedatwork+LeaveFlag END AS Day7Clock,  \r\n" +
 "CASE WHEN TheDate = @start+7 THEN expectedatwork+LeaveFlag END AS Day8Clock,  \r\n" +
 "CASE WHEN TheDate = @start+8 THEN expectedatwork+LeaveFlag END AS Day9Clock,  \r\n" +
 "CASE WHEN TheDate = @start+9 THEN expectedatwork+LeaveFlag END AS Day10Clock,  \r\n" +
 "CASE WHEN TheDate = @start+10 THEN expectedatwork+LeaveFlag END AS Day11Clock,  \r\n" +
 "CASE WHEN TheDate = @start+11 THEN expectedatwork+LeaveFlag END AS Day12Clock,  \r\n" +
 "CASE WHEN TheDate = @start+12 THEN expectedatwork+LeaveFlag END AS Day13Clock,  \r\n" +
 "CASE WHEN TheDate = @start+13 THEN expectedatwork+LeaveFlag END AS Day14Clock,  \r\n" +
 "CASE WHEN TheDate = @start+14 THEN expectedatwork+LeaveFlag END AS Day15Clock,  \r\n" +
 "CASE WHEN TheDate = @start+15 THEN expectedatwork+LeaveFlag END AS Day16Clock,  \r\n" +
 "CASE WHEN TheDate = @start+16 THEN expectedatwork+LeaveFlag END AS Day17Clock,  \r\n" +
 "CASE WHEN TheDate = @start+17 THEN expectedatwork+LeaveFlag END AS Day18Clock,  \r\n" +
 "CASE WHEN TheDate = @start+18 THEN expectedatwork+LeaveFlag END AS Day19Clock,  \r\n" +
 "CASE WHEN TheDate = @start+19 THEN expectedatwork+LeaveFlag END AS Day20Clock,  \r\n" +
 "CASE WHEN TheDate = @start+20 THEN expectedatwork+LeaveFlag END AS Day21Clock,  \r\n" +
 "CASE WHEN TheDate = @start+21 THEN expectedatwork+LeaveFlag END AS Day22Clock,  \r\n" +
 "CASE WHEN TheDate = @start+22 THEN expectedatwork+LeaveFlag END AS Day23Clock,  \r\n" +
 "CASE WHEN TheDate = @start+23 THEN expectedatwork+LeaveFlag END AS Day24Clock,  \r\n" +
 "CASE WHEN TheDate = @start+24 THEN expectedatwork+LeaveFlag END AS Day25Clock,  \r\n" +
 "CASE WHEN TheDate = @start+25 THEN expectedatwork+LeaveFlag END AS Day26Clock,  \r\n" +
 "CASE WHEN TheDate = @start+26 THEN expectedatwork+LeaveFlag END AS Day27Clock,  \r\n" +
 "CASE WHEN TheDate = @start+27 THEN expectedatwork+LeaveFlag END AS Day28Clock,  \r\n" +
 "CASE WHEN TheDate = @start+28 THEN expectedatwork+LeaveFlag END AS Day29Clock,  \r\n" +
 "CASE WHEN TheDate = @start+29 THEN expectedatwork+LeaveFlag END AS Day30Clock,  \r\n" +
 "CASE WHEN TheDate = @start+30 THEN expectedatwork+LeaveFlag END AS Day31Clock,  \r\n" +
"CASE WHEN TheDate = @start+31 THEN expectedatwork+LeaveFlag END AS Day32Clock,  \r\n" +
"CASE WHEN TheDate = @start+32 THEN expectedatwork+LeaveFlag END AS Day33Clock,  \r\n" +
"CASE WHEN TheDate = @start+33 THEN expectedatwork+LeaveFlag END AS Day34Clock,  \r\n" +
"CASE WHEN TheDate = @start+34 THEN expectedatwork+LeaveFlag END AS Day35Clock,  \r\n" +
"CASE WHEN TheDate = @start+35 THEN expectedatwork+LeaveFlag END AS Day36Clock,  \r\n" +
"CASE WHEN TheDate = @start+36 THEN expectedatwork+LeaveFlag END AS Day37Clock,  \r\n" +
"CASE WHEN TheDate = @start+37 THEN expectedatwork+LeaveFlag END AS Day38Clock,  \r\n" +
"CASE WHEN TheDate = @start+38 THEN expectedatwork+LeaveFlag END AS Day39Clock,  \r\n" +
"CASE WHEN TheDate = @start+39 THEN expectedatwork+LeaveFlag END AS Day40Clock,  \r\n" +
"CASE WHEN TheDate = @start+40 THEN expectedatwork+LeaveFlag END AS Day41Clock,  \r\n" +
"CASE WHEN TheDate = @start+41 THEN expectedatwork+LeaveFlag END AS Day42Clock,  \r\n" +
"CASE WHEN TheDate = @start+42 THEN expectedatwork+LeaveFlag END AS Day43Clock,  \r\n" +
"CASE WHEN TheDate = @start+43 THEN expectedatwork+LeaveFlag END AS Day44Clock,  \r\n" +
"CASE WHEN TheDate = @start+44 THEN expectedatwork+LeaveFlag END AS Day45Clock  \r\n" +

 "FROM [Mineware].[dbo].[tbl_Import_BMCS_Clocking_Total]  \r\n" +
"	WHERE [TheDate] >= @start and [TheDate] <= @start +31  \r\n" +
" ) a GROUP BY nnn  \r\n" +
" ) b ON a.IndustryNumber = b.nnn ORDER BY IndustryNumber DESC";

            _dbMan12.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan12.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan12.ExecuteInstruction();


            DataTable dt = _dbMan12.ResultsDataTable;


            DataSet ds = new DataSet();

            ds.Tables.Add(dt);

            gcProdMineOther.DataSource = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                bandedGridColumn1.FieldName = "IndustryNumber";
                bandedGridColumn2.FieldName = "Designation";
                bandedGridColumn5.FieldName = "Shift";
                bandedGridColumn9.FieldName = "Saved";
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
                bandedGridColumn3.FieldName = "Day32";
                bandedGridColumn4.FieldName = "Day33";
                bandedGridColumn37.FieldName = "Day34";
                bandedGridColumn38.FieldName = "Day35";
                bandedGridColumn39.FieldName = "Day36";
                bandedGridColumn40.FieldName = "Day37";
                bandedGridColumn72.FieldName = "Day38";
                bandedGridColumn73.FieldName = "Day39";
                bandedGridColumn74.FieldName = "Day40";
                bandedGridColumn75.FieldName = "Day41";
                bandedGridColumn76.FieldName = "Day42";
                bandedGridColumn77.FieldName = "Day43";
                bandedGridColumn78.FieldName = "Day44";
                bandedGridColumn79.FieldName = "Day45";

                bandedGridColumn80.FieldName = "Day32Clock";
                bandedGridColumn81.FieldName = "Day33Clock";
                bandedGridColumn82.FieldName = "Day34Clock";
                bandedGridColumn83.FieldName = "Day35Clock";
                bandedGridColumn84.FieldName = "Day36Clock";
                bandedGridColumn85.FieldName = "Day37Clock";
                bandedGridColumn86.FieldName = "Day38Clock";
                bandedGridColumn87.FieldName = "Day39Clock";
                bandedGridColumn88.FieldName = "Day40Clock";
                bandedGridColumn89.FieldName = "Day41Clock";
                bandedGridColumn90.FieldName = "Day42Clock";
                bandedGridColumn91.FieldName = "Day43Clock";
                bandedGridColumn92.FieldName = "Day44Clock";
                bandedGridColumn93.FieldName = "Day45Clock";

                gvProdMineOther.Columns[3].Visible = true;
                gvProdMineOther.Columns[4].Visible = true;
                gvProdMineOther.Columns[5].Visible = true;
                gvProdMineOther.Columns[6].Visible = true;
                gvProdMineOther.Columns[7].Visible = true;
                gvProdMineOther.Columns[8].Visible = true;
                gvProdMineOther.Columns[9].Visible = true;
                gvProdMineOther.Columns[10].Visible = true;
                gvProdMineOther.Columns[11].Visible = true;
                gvProdMineOther.Columns[12].Visible = true;
                gvProdMineOther.Columns[13].Visible = true;
                gvProdMineOther.Columns[14].Visible = true;
                gvProdMineOther.Columns[15].Visible = true;
                gvProdMineOther.Columns[16].Visible = true;
                gvProdMineOther.Columns[17].Visible = true;
                gvProdMineOther.Columns[18].Visible = true;
                gvProdMineOther.Columns[19].Visible = true;
                gvProdMineOther.Columns[20].Visible = true;
                gvProdMineOther.Columns[21].Visible = true;
                gvProdMineOther.Columns[22].Visible = true;
                gvProdMineOther.Columns[23].Visible = true;
                gvProdMineOther.Columns[24].Visible = true;
                gvProdMineOther.Columns[25].Visible = true;
                gvProdMineOther.Columns[26].Visible = true;
                gvProdMineOther.Columns[27].Visible = true;
                gvProdMineOther.Columns[28].Visible = true;
                gvProdMineOther.Columns[29].Visible = true;
                gvProdMineOther.Columns[30].Visible = true;
                gvProdMineOther.Columns[31].Visible = true;
                gvProdMineOther.Columns[32].Visible = true;
                gvProdMineOther.Columns[33].Visible = true;
                gvProdMineOther.Columns[65].Visible = true;
                gvProdMineOther.Columns[66].Visible = true;
                gvProdMineOther.Columns[67].Visible = true;
                gvProdMineOther.Columns[68].Visible = true;
                gvProdMineOther.Columns[69].Visible = true;
                gvProdMineOther.Columns[70].Visible = true;
                gvProdMineOther.Columns[71].Visible = true;
                gvProdMineOther.Columns[72].Visible = true;
                gvProdMineOther.Columns[73].Visible = true;
                gvProdMineOther.Columns[74].Visible = true;
                gvProdMineOther.Columns[75].Visible = true;
                gvProdMineOther.Columns[76].Visible = true;
                gvProdMineOther.Columns[77].Visible = true;
                gvProdMineOther.Columns[78].Visible = true;
            }

            gvProdMineOther.Columns[3].Caption = startDate.ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[4].Caption = startDate.AddDays(1).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[5].Caption = startDate.AddDays(2).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[6].Caption = startDate.AddDays(3).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[7].Caption = startDate.AddDays(4).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[8].Caption = startDate.AddDays(5).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[9].Caption = startDate.AddDays(6).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[10].Caption = startDate.AddDays(7).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[11].Caption = startDate.AddDays(8).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[12].Caption = startDate.AddDays(9).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[13].Caption = startDate.AddDays(10).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[14].Caption = startDate.AddDays(11).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[15].Caption = startDate.AddDays(12).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[16].Caption = startDate.AddDays(13).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[17].Caption = startDate.AddDays(14).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[18].Caption = startDate.AddDays(15).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[19].Caption = startDate.AddDays(16).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[20].Caption = startDate.AddDays(17).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[21].Caption = startDate.AddDays(18).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[22].Caption = startDate.AddDays(19).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[23].Caption = startDate.AddDays(20).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[24].Caption = startDate.AddDays(21).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[25].Caption = startDate.AddDays(22).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[26].Caption = startDate.AddDays(23).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[27].Caption = startDate.AddDays(24).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[28].Caption = startDate.AddDays(25).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[29].Caption = startDate.AddDays(26).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[30].Caption = startDate.AddDays(27).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[31].Caption = startDate.AddDays(28).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[32].Caption = startDate.AddDays(29).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[33].Caption = startDate.AddDays(30).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[65].Caption = startDate.AddDays(31).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[66].Caption = startDate.AddDays(32).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[67].Caption = startDate.AddDays(33).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[68].Caption = startDate.AddDays(34).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[69].Caption = startDate.AddDays(35).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[70].Caption = startDate.AddDays(36).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[71].Caption = startDate.AddDays(37).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[72].Caption = startDate.AddDays(38).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[73].Caption = startDate.AddDays(39).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[74].Caption = startDate.AddDays(40).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[75].Caption = startDate.AddDays(41).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[76].Caption = startDate.AddDays(42).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[77].Caption = startDate.AddDays(43).ToString("dd  MMM   ddd");
            gvProdMineOther.Columns[78].Caption = startDate.AddDays(44).ToString("dd  MMM   ddd");

            if (_dbMan12.ResultsDataTable.Rows.Count != 0)
            {
                for (int i = 0; i < gvProdMineOther.Columns.Count; i++)
                {
                    var result = String.Format("{0:dd  MMM   ddd}", EndDate.Value);
                    if (gvProdMineOther.Columns[i].Caption == result)
                    {
                        var removestr = gvProdMineOther.Columns[i].FieldName.ToString().Substring(3, 2);
                        int remove = Convert.ToInt32(removestr) + 1;
                        for (int re = remove; re < 46; re++)
                        {
                            gvProdMineOther.Columns["Day" + re].Visible = false;

                        }
                    }
                }
            }
            //for (int i = 0; i < gvProdMineOther.Columns.Count; i++)
            //{
            //    if (gvProdMineOther.Columns[i].Caption == String.Format("{0:dd  MMM  ddd}", EndDate.Value))
            //    {
            //        int index = gvProdMineOther.Columns[i].SortIndex;
            //            gvProdMineOther.Columns[index].Visible = false;
            //    }
            //}
            this.Cursor = Cursors.Default;
            this.UseWaitCursor = false;
            return;
        }

        void LoadDataTram()
        {

            //ONLY MO
            //MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            //_dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            //_dbMan1.SqlStatement = "SELECT DISTINCT(substring(sectionid+'           ',1,4)) AS MO FROM mineware.dbo.tbl_BCS_Planning where calendardate > getdate()-90 and len(sectionid) > 4";
            //_dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //_dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            //_dbMan1.ExecuteInstruction();


            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan1.SqlStatement = " DECLARE @pm varchar(10) SET @pm = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' DECLARE @pm1 varchar(10) \r\n " +
                                    " SET @pm1 = (SELECT max(prodmonth) pm FROM  dbo.tbl_BCS_Gangs WHERE prodmonth < @pm )  \r\n " +
                                    "  SELECT Distinct substring(crew+'       ',1,4) mo FROM(SELECT DISTINCT  orgunit crew \r\n " +
                                    "  FROM dbo.tbl_BCS_Gangs WHERE prodmonth in (@pm, @pm1) ) a WHERE len(crew) > 4  ORDER BY mo ";
            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();

            DataTable dtMO = _dbMan1.ResultsDataTable;

            //ONLY ShiftBoss
            MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
            _dbMan2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
           // _dbMan2.SqlStatement = "select substring(crew+'       ',1,4) mo, crew from( select distinct (orgunit) crew from dbo.import_BMCS_Clocking_3month where thedate > getdate() -30 )a where len(crew) > 4 order by crew, len (crew)";
            _dbMan2.SqlStatement = " DECLARE @pm varchar(10) " +
                    "SET @pm = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' " +
                    "DECLARE @pm1 varchar(10) " +
                    "SET @pm1 =  " +
                    "( " +
                    "SELECT max(prodmonth) pm  " +
                    "FROM  dbo.tbl_BCS_Gangs " +
                    "WHERE prodmonth < @pm " +
                    ") " +
                    "SELECT substring(crew+'       ',1,4) mo, crew " +
                    "FROM " +
                    "( " +
                    "SELECT DISTINCT  orgunit crew " +
                    "FROM dbo.tbl_BCS_Gangs " +
                    "WHERE prodmonth in( @pm, @pm1) " +
                    ") a " +
                    "WHERE len(crew) > 4  " +
                    "ORDER BY crew, len (crew) ";
            _dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan2.ExecuteInstruction();

            DataTable dtSB = _dbMan2.ResultsDataTable;

            LVLTreeView.Nodes.Clear();

            for (int i = 0; i < (dtMO.Rows.Count); i++)
            {
                var result = dtMO.Rows[i]["mo"].ToString();
                TreeNode MO = new TreeNode(result);
                MO.NodeFont = new Font("Microsoft Sans Serif", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                
                for (int iorg = 0; iorg < (dtSB.Rows.Count); iorg++)
                    {
                        if (result == dtSB.Rows[iorg]["mo"].ToString())
                        {
                            var resultsOrg = dtSB.Rows[iorg]["crew"].ToString();
                        TreeNode Org = new TreeNode(resultsOrg);
                        Org.NodeFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular, GraphicsUnit.Pixel);
                        Org.ForeColor = Color.DimGray;

                         MO.Nodes.Add(Org);
                        }
                    }
                LVLTreeView.Nodes.Add(MO);
            }

        }

        private void ucProductionMinersCapture_Load(object sender, EventArgs e)
        {


            editProdmonth.EditValue = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsDate(ProductionAmplatsGlobal.ProductionAmplatsGlobalTSysSettings._currentProductionMonth.ToString());

            LoadDataTram();
            
            dgvProdMine.Rows.Add("Fill");
            
        }

        private void MillMonth_Click(object sender, EventArgs e)
        {
            
            LoadDataTram();
            LoadTrammingOther();

            if (gvProdMineOther.RowCount == 0 && Orgunitlbl.Text == "Orgunitlbl")
            {
               // MessageBox.Show("Test");
            }
        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {

            frmAddTramCrewcs ABSfrm = new frmAddTramCrewcs();
            ABSfrm._connection = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            ABSfrm.ShowDialog();

            LoadDataTram();

        }

        private void TramLoadBtn_Click(object sender, EventArgs e)
        {

            LoadTrammingOther();

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

            LoadTrammingOther();


        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LVLTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
             if (LVLTreeView.SelectedNode.Parent == null)
            {

            } 

            if (LVLTreeView.SelectedNode != null)
            {
                if (dgvProdMine.Rows[0].Cells["Orgunit"].FormattedValue.ToString() != "Fill")
                {
                    var message = "You have made changes to the current Org without saving, do you want to save updates made?";
                    var caption = "Unsaved Changes";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    MessageBoxIcon icon = MessageBoxIcon.Information;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons, icon);

                    if (result == DialogResult.Yes)
                    {
                        Updatebtn_ItemClick(null,null);
                    }
                    else if (result == DialogResult.No)
                    {
                        dgvProdMine.Rows.Clear();
                        dgvProdMine.Rows.Add("Fill");
                    }
                }

                if (LVLTreeView.SelectedNode.Level != 0)
                {
                    Orgunitlbl.Text = LVLTreeView.SelectedNode.Text.ToString();
                    LoadTrammingOther();
                    
                    Orglbl.Text = LVLTreeView.SelectedNode.Text.ToString();
                }
                if (LVLTreeView.SelectedNode.Level == 0)
                {
                    Orglbl.Text = LVLTreeView.SelectedNode.Text.ToString();
                  
                    Orgunitlbl.Text = LVLTreeView.SelectedNode.Text.ToString();
                }
                LoadTrammingOther();
            
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

        public object lb_Item2 = null;
        private int i2;

        private void lbDays_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void lbDays_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void lbDays_MouseLeave(object sender, EventArgs e)
        {
         ListBox lb = sender as ListBox;
        }

        private void lbDays_DragOver(object sender, DragEventArgs e)
        {
         e.Effect = DragDropEffects.All;
        }

        private void gcProdMine_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void gcProdMine_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void gvProdMine_DoubleClick(object sender, EventArgs e)
        {
        
        
        }

        public object lb_Item = null;

        private void lbDays2_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void lbDays2_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void lbDays2_MouseLeave(object sender, EventArgs e)
        {
            ListBox lb = sender as ListBox;
        }

        private void lbDays2_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void gcProdMineOther_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void gcProdMineOther_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void gvProdMineOther_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView View = sender as GridView;
            
            if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Shift"]).ToString() == "D")
            {
                //gvProdMineOther.SetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Shift"], "DayShift");
                if (e.Column.AbsoluteIndex == 2)
                {
                    e.DisplayText = "DayShift";
                }
            }
            else if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Shift"]).ToString() == "N")
            {
                //gvProdMineOther.SetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Shift"], "NightShift");

                if (e.Column.AbsoluteIndex == 2)
                {
                    e.DisplayText = "NightShift";
                }
            }

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
            //Day32
            var wd32 = "";
            var wd32clock = "";
            //Day33
            var wd33 = "";
            var wd33clock = "";
            //Day34
            var wd34 = "";
            var wd34clock = "";
            //Day35
            var wd35 = "";
            var wd35clock = "";
            //Day36
            var wd36 = "";
            var wd36clock = "";
            //Day37
            var wd37 = "";
            var wd37clock = "";
            //Day38
            var wd38 = "";
            var wd38clock = "";
            //Day39
            var wd39 = "";
            var wd39clock = "";
            //Day40
            var wd40 = "";
            var wd40clock = "";
            //Day41
            var wd41 = "";
            var wd41clock = "";
            //Day42
            var wd42 = "";
            var wd42clock = "";
            //Day43
            var wd43 = "";
            var wd43clock = "";
            //Day44
            var wd44 = "";
            var wd44clock = "";
            //Day45
            var wd45 = "";
            var wd45clock = "";


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
            wd6 = wd6.Substring(0, 1);

            wd6clock = View.GetRowCellValue(e.RowHandle, "Day6Clock").ToString() + "                                        ";
            wd6clock = wd6clock.Substring(1, 3);
            //Day7
            wd7 = View.GetRowCellValue(e.RowHandle, "Day7Clock").ToString() + "                                             ";

            wd7clock = View.GetRowCellValue(e.RowHandle, "Day7Clock").ToString() + "                                        ";
            //Day8
            wd8 = View.GetRowCellValue(e.RowHandle, "Day8Clock").ToString() + "                                             ";

            wd8clock = View.GetRowCellValue(e.RowHandle, "Day8Clock").ToString() + "                                        ";

            //Day9
            wd9 = View.GetRowCellValue(e.RowHandle, "Day9Clock").ToString() + "                                             ";
            wd9 = wd9.Substring(0, 1);

            wd9clock = View.GetRowCellValue(e.RowHandle, "Day9Clock").ToString() + "                                        ";
            wd9clock = wd9clock.Substring(1, 3);
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
            wd21clock = wd21clock.Substring(1, 3);
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

            //Day32
            wd32 = View.GetRowCellValue(e.RowHandle, "Day32Clock").ToString() + "                                             ";

            wd32clock = View.GetRowCellValue(e.RowHandle, "Day32Clock").ToString() + "                                        ";

            //Day33
            wd33 = View.GetRowCellValue(e.RowHandle, "Day33Clock").ToString() + "                                             ";

            wd33clock = View.GetRowCellValue(e.RowHandle, "Day33Clock").ToString() + "                                        ";

            //Day34
            wd34 = View.GetRowCellValue(e.RowHandle, "Day34Clock").ToString() + "                                             ";

            wd34clock = View.GetRowCellValue(e.RowHandle, "Day34Clock").ToString() + "                                        ";

            //Day35
            wd35 = View.GetRowCellValue(e.RowHandle, "Day35Clock").ToString() + "                                             ";

            wd35clock = View.GetRowCellValue(e.RowHandle, "Day35Clock").ToString() + "                                        ";

            //Day36
            wd36 = View.GetRowCellValue(e.RowHandle, "Day36Clock").ToString() + "                                             ";

            wd36clock = View.GetRowCellValue(e.RowHandle, "Day36Clock").ToString() + "                                        ";

            //Day37
            wd37 = View.GetRowCellValue(e.RowHandle, "Day37Clock").ToString() + "                                             ";

            wd37clock = View.GetRowCellValue(e.RowHandle, "Day37Clock").ToString() + "                                        ";

            //Day38
            wd38 = View.GetRowCellValue(e.RowHandle, "Day38Clock").ToString() + "                                             ";

            wd38clock = View.GetRowCellValue(e.RowHandle, "Day38Clock").ToString() + "                                        ";

            //Day39
            wd39 = View.GetRowCellValue(e.RowHandle, "Day39Clock").ToString() + "                                             ";

            wd39clock = View.GetRowCellValue(e.RowHandle, "Day39Clock").ToString() + "                                        ";

            //Day40
            wd40 = View.GetRowCellValue(e.RowHandle, "Day40Clock").ToString() + "                                             ";

            wd40clock = View.GetRowCellValue(e.RowHandle, "Day40Clock").ToString() + "                                        ";

            //Day41
            wd41 = View.GetRowCellValue(e.RowHandle, "Day41Clock").ToString() + "                                             ";

            wd41clock = View.GetRowCellValue(e.RowHandle, "Day41Clock").ToString() + "                                        ";

            //Day42
            wd42 = View.GetRowCellValue(e.RowHandle, "Day42Clock").ToString() + "                                             ";

            wd42clock = View.GetRowCellValue(e.RowHandle, "Day42Clock").ToString() + "                                        ";

            //Day43
            wd31 = View.GetRowCellValue(e.RowHandle, "Day43Clock").ToString() + "                                             ";

            wd31clock = View.GetRowCellValue(e.RowHandle, "Day43Clock").ToString() + "                                        ";

            //Day44
            wd44 = View.GetRowCellValue(e.RowHandle, "Day44Clock").ToString() + "                                             ";

            wd44clock = View.GetRowCellValue(e.RowHandle, "Day44Clock").ToString() + "                                        ";

            //Day45
            wd45 = View.GetRowCellValue(e.RowHandle, "Day45Clock").ToString() + "                                             ";

            wd45clock = View.GetRowCellValue(e.RowHandle, "Day45Clock").ToString() + "                                        ";


            if (e.Column.AbsoluteIndex == 3)
            {
                if (wd1clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd1clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd1clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 4)
            {
                if (wd2clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd2clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd2clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 5)
            {
                if (wd3clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd3clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd3clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 6)
            {
                if (wd4clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd4clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd4clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 7)
            {
                if (wd5clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd5clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd5clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 8)
            {
                if (wd6clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd6clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd1clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 9)
            {
                if (wd7clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd7clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd7clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 10)
            {
                if (wd8clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd8clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd8clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 11)
            {
                if (wd9clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd9clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd9clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 12)
            {
                if (wd10clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd10clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd10clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 13)
            {
                if (wd11clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd11clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd11clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 14)
            {
                if (wd12clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd12clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd12clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 15)
            {
                if (wd13clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd13clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd13clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 16)
            {
                if (wd14clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd14clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd14clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 17)
            {
                if (wd15clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd15clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd15clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 18)
            {
                if (wd16clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd16clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd16clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 19)
            {
                if (wd17clock.Substring(0,1) == "S")
                {
                    e.Graphics.DrawString(wd17clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd17clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 20)
            {
                if (wd18clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd18clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd18clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 21)
            {
                if (wd19clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd19clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd19clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 22)
            {
                if (wd20clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd20clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd20clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 23)
            {
                if (wd21clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd21clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd21clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 24)
            {
                if (wd22clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd22clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd22clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 25)
            {
                if (wd23clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd23clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd23clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 26)
            {
                if (wd24clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd24clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd24clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 27)
            {
                if (wd25clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd25clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd25clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 28)
            {
                if (wd26clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd26clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd26clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 29)
            {
                if (wd27clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd27clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd27clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 30)
            {
                if (wd28clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd28clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd28clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 31)
            {
                if (wd29clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd29clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd29clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 32)
            {
                if (wd30clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd30clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd30clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 33)
            {
                if (wd31clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd31clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd31clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 34)
            {
                if (wd32clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd32clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd32clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 35)
            {
                if (wd33clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd33clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd33clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 36)
            {
                if (wd34clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd34clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd34clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 37)
            {
                if (wd35clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd35clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd35clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 38)
            {
                if (wd36clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd36clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd36clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 39)
            {
                if (wd37clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd37clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd37clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 40)
            {
                if (wd38clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd38clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd38clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 41)
            {
                if (wd39clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd39clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd39clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 42)
            {
                if (wd36clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd40clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd40clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 43)
            {
                if (wd41clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd41clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd41clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }


            if (e.Column.AbsoluteIndex == 44)
            {
                if (wd42clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd42clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd42clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 45)
            {
                if (wd43clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd43clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd43clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 46)
            {
                if (wd44clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd44clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd44clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }

            if (e.Column.AbsoluteIndex == 47)
            {
                if (wd45clock.Substring(0, 1) == "S")
                {
                    e.Graphics.DrawString(wd45clock, new Font("Tahoma", 7), Brushes.MistyRose, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
                else
                {
                    e.Graphics.DrawString(wd45clock, new Font("Tahoma", 7), Brushes.Gainsboro, e.Bounds.Left + 18, e.Bounds.Top + 2, StringFormat.GenericDefault);
                }
            }












            //Day1
            //if (e.Column.AbsoluteIndex == 3)
            //{

            //    e.Appearance.BackColor = Color.White;
            //    if (wd1 == "N")
            //    {
            //        e.Appearance.BackColor = Color.Gainsboro;
            //        e.DisplayText = wd1clock.Trim();

            //    }
            //    else
            //    {
            //        if (wd1clock.Trim() != "N")
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
            //DD19 = DD19.Substring(0, 2);

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

        private void gvProdMineOther_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {



            try
            {

                if (dgvProdMine.Rows[0].Cells["Orgunit"].FormattedValue.ToString() == "Fill")
                {
                    dgvProdMine.Rows.RemoveAt(0);
                }
            
                if (gvProdMineOther.FocusedValue.ToString() == "N " || gvProdMineOther.FocusedValue.ToString() == "")
                {
                    if (e.Column.FieldName == "Day1")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value);
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day1"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }

                    if (e.Column.FieldName == "Day2")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(1.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day2"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                        
                    }


                    if (e.Column.FieldName == "Day3")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(2.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day3"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day4")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(3.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day4"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day5")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(4.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day5"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day6")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(5.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day6"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day7")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(6.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day7"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day8")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(7.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day8"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day9")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(8.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day9"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day10")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(9.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day10"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day11")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(10.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day11"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day12")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(11.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day12"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day13")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(12.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day13"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day14")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(13.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day14"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day15")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(14.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day15"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day16")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(15.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day16"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day17")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(16.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day17"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day18")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(17.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day18"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day19")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(18.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day19"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day20")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(19.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day20"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day21")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(20.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day21"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day22")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(21.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day22"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day23")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(22.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day23"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day24")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(23.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day24"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day25")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(24.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day25"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day26")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(25.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day26"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day27")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(26.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day27"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day28")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(27.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day28"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day29")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(28.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day29"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day30")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(29.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day30"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day31")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(30.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day31"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }

                    if (e.Column.FieldName == "Day32")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(31.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day32"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }

                    if (e.Column.FieldName == "Day33")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(32.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day33"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }

                    if (e.Column.FieldName == "Day34")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(33.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day34"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }

                    if (e.Column.FieldName == "Day35")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(34.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day35"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }

                    if (e.Column.FieldName == "Day36")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(35.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day36"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }

                    if (e.Column.FieldName == "Day37")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(36.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day37"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                    if (e.Column.FieldName == "Day38")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(37.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day38"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }

                    if (e.Column.FieldName == "Day39")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(38.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day39"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }

                    if (e.Column.FieldName == "Day40")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(39.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day40"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }

                    if (e.Column.FieldName == "Day41")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(40.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day41"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }

                    if (e.Column.FieldName == "Day42")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(41.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day42"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }

                    if (e.Column.FieldName == "Day43")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(42.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day43"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }

                    if (e.Column.FieldName == "Day44")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(43.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day44"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }

                    if (e.Column.FieldName == "Day45")
                    {
                        lblDayFill2.Text = "";
                        lblDayFill2.Text = String.Format("{0:yyyy-MM-dd}", StartDate.Value.AddDays(44.00));
                        IndNoLbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[0]).ToString();
                        Desiglbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[1]).ToString();
                        Teamlbl.Text = gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns[2]).ToString();
                        if (gvProdMineOther.GetRowCellValue(e.RowHandle, gvProdMineOther.Columns["Day45"]).ToString() == "")
                        {
                            lblSetValFill2.Text = "Delete";
                        }
                        else
                        {
                            lblSetValFill2.Text = gvProdMineOther.GetFocusedValue().ToString().ToUpper();
                        }
                        var org = Orgunitlbl.Text;
                        var industry = IndNoLbl.Text;
                        var day = lblDayFill2.Text;
                        var value = lblSetValFill2.Text;
                        var designation = Desiglbl.Text;
                        var shift = Teamlbl.Text;
                        dgvProdMine.Rows.Add(org, industry, day, value, shift, designation);
                    }


                }
                else
                {
                    MessageBox.Show("Not Allowed, please correct characters('N ') and or clear the cell.", "Error");
                    gvProdMineOther.SetFocusedValue("");
                }
            }
            catch (NullReferenceException ex)
            {
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {

        }

        private void Orgunitlbl_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void gcProdMineOther_DoubleClick(object sender, EventArgs e)
        {


        }

        private void Updatebtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.UseWaitCursor = true;
                
                if (dgvProdMine.Rows[0].Cells[0].Value.ToString() != "Fill")
                {
                    foreach (DataGridViewRow row in dgvProdMine.Rows)
                    {
                        if (dgvProdMine.Rows[row.Index].Cells["Value"].Value.ToString() == "Delete")
                        {
                            if (randelete == false)
                            {
                                UpdateTrammingOtherDelete();
                                UpdateTrammingOtherDelete_3Month();

                            }
                        }
                        else
                        {
                            if (raninsert == false)
                            {
                                UpdateTrammingOtherInsertUpdate();
                                UpdateTrammingOtherInsertUpdate_3Month();
                            }
                        }
                    }

                    randelete = false;
                    raninsert = false;
                    dgvProdMine.Rows.Clear();
                    dgvProdMine.Rows.Add("Fill");
                    LoadTrammingOther();
                    acAlert.Buttons.GetButtonByHint("Open").Visible = false;
                    acAlert.Show(frmMain.ActiveForm, "Notification", "Update was successful");
                }
                this.Cursor = Cursors.Default;
                this.UseWaitCursor = false;

        }

        private void gcProdMineOther_Click(object sender, EventArgs e)
        {
            //if (gvProdMineOther.PaintAppearance.FocusedCell.BackColor == Color.White)
            //{
            //    MessageBox.Show("Test");
            //}
        }

        private void gvProdMineOther_DoubleClick(object sender, EventArgs e)
        {



            //var value = gvProdMineOther.GetRowCellValue(0, "Day10Clock").ToString();
            //var val2 = gvProdMineOther.GetRowCellValue(1, "Day9Clock").ToString();

            //if (value == "YLO")
            //{
            //    MessageBox.Show("Not Allowed");
            //}
            //else
            //{
            
                if (Convert.ToString(gvProdMineOther.FocusedValue) == "")
                {
                    gvProdMineOther.SetFocusedValue("N ");
                    
                }
                else if (Convert.ToString(gvProdMineOther.FocusedValue) == "N ")
                {
                    gvProdMineOther.SetFocusedValue("");
                }
            //}
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (Orgunitlbl.Text == "Orgunitlbl")
            {
                MessageBox.Show("Please Select a Level");
                return;
            }

            frmProductionProp RepFrm = (frmProductionProp)IsBookingFormAlreadyOpen(typeof(frmProductionProp));
            if (RepFrm == null)
            {
                RepFrm = new frmProductionProp();
                RepFrm._connection = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                RepFrm.tbMillMonth.Text = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString();
                RepFrm.tbOrg.Text = Orgunitlbl.Text;
                RepFrm.ShowDialog();
                RepFrm.StartPosition = FormStartPosition.CenterScreen;
                RepFrm.FormClosed += new FormClosedEventHandler(RepFrm_FormClosed);
                LoadTrammingOther();
            }
        }
        void RepFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadTrammingOther();
        }

        private void gvProdMineOther_Click(object sender, EventArgs e)
        {

        }

        private void gvProdMineOther_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            
        }

        private void gvProdMineOther_RowStyle(object sender, RowStyleEventArgs e)
        {
            
        }

        private void StartDate_ValueChanged(object sender, EventArgs e)
        {
            LoadTrammingOther();
        }

        private void EndDate_ValueChanged(object sender, EventArgs e)
        {
            LoadTrammingOther();
        }

        private void btnPrintAsPdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.UseWaitCursor = true;
            checkDirectory();
            fileName = "TeamsProdcutionShifts";
            checkPdf();

            //DevExpress.XtraPrinting.PrintingSystem printSystem = new DevExpress.XtraPrinting.PrintingSystem();
            //DevExpress.XtraPrinting.PrintableComponentLink printLink = new DevExpress.XtraPrinting.PrintableComponentLink();
            //DevExpress.XtraPrinting.PdfExportOptions options = new DevExpress.XtraPrinting.PdfExportOptions();
            
            //printLink.Component = gcProdMineOther;
            //printLink.CreateDocument(printSystem);
            //printSystem.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A3;
            //printSystem.ShowPrintStatusDialog = true;
            //printSystem.PageSettings.Landscape = true;
            //printLink.Landscape = true;
            //printSystem.Watermark.Image = peLogo.Image;
            //printSystem.Watermark.ImageAlign = ContentAlignment.TopCenter;
            //printSystem.ExportToPdf(pdfName, options);
            //printSystem.ShowPrintStatusDialog = true;
            //acAlert.Buttons.GetButtonByHint("Open").Visible = true;
            //acAlert.Show(frmMain.ActiveForm, "Print Completed", "Click on folder to open");

            CompositeLink comp = new CompositeLink(new PrintingSystem());
            PrintableComponentLink link1 = new PrintableComponentLink();
            PrintableComponentLink link2 = new PrintableComponentLink();
            link1.Component = gcProdMineOther;
            link1.CreateReportHeaderArea += new CreateAreaEventHandler(link1_CreateReportHeaderArea);
            comp.Links.Add(link2);
            comp.Links.Add(link1);
            comp.Landscape = true;
            comp.PaperKind = System.Drawing.Printing.PaperKind.A3;
            comp.ExportToPdf(pdfName);
            this.Cursor = Cursors.Default;
            this.UseWaitCursor = false;
            acAlert.Buttons.GetButtonByHint("Open").Visible = true;
            acAlert.Show(frmMain.ActiveForm, "Print Completed", "Click on folder to open");
        }

        void link1_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
        {
            e.Graph.DrawImage(peLogo.Image, new Rectangle(1095, 0, 280, 80), DevExpress.XtraPrinting.BorderSide.None, Color.White);
            e.Graph.Font = new Font("Arial", 10);
            e.Graph.DrawString("Prod Month: " + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString(), Color.Black, new Rectangle(0, 50, 280, 80), DevExpress.XtraPrinting.BorderSide.None);
            e.Graph.Font = new Font("Arial", 24);
            e.Graph.DrawString("Bonus Control - Shift Captures   (Production and Tramming)", Color.Black, new Rectangle(450, 0, 500, 80), DevExpress.XtraPrinting.BorderSide.None);
            e.Graph.BorderWidth = 0;
            e.Graph.DrawLine(new Point(30, 80), new Point(1350, 80), Color.Orange, 2);
 


        }

        private void acAlert_ButtonClick(object sender, DevExpress.XtraBars.Alerter.AlertButtonClickEventArgs e)
        {
            if (e.Info.Caption == "Print Completed" && e.ButtonName == "btnOpen")
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

        private void gvProdMineOther_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            
        }

        private void editProdmonth_EditValueChanged(object sender, EventArgs e)
        {
            LoadDataTram();
            LoadTrammingOther();
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OnCloseTabRequest(new CloseTabArg(tabCaption));
        }
    }
}
