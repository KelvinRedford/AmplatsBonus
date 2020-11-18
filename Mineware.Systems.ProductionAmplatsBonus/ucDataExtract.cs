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
using System.Net.Mail;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using System.Globalization;
using System.IO;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraPrinting;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;
using Mineware.Systems.ProductionAmplatsGlobal;

namespace Mineware.Systems.ProductionAmplatsBonus
{
    public partial class ucDataExtract : BaseUserControl
    {

        Procedures procs = new Procedures();

        //DataTables
        private DataTable dtProd;
        private DataTable dtTramming;
        private DataTable dtSB;

        //SendToExcel
        private string excelName;
        private string directoryName;
        private string fileName;

        //PrintAsPdf
        private string pdfName;

        public ucDataExtract()
        {
            InitializeComponent();
            FormRibbonPages.Add(rpMain);
            FormActiveRibbonPage = rpMain;
            FormMainRibbonPage = rpMain;
            RibbonControl = rcMain;
        }
        
        void getProdMonth()
        {
            //Gets and Sets Production month listbox
            tbProdMonth.EditValue = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsDate(ProductionAmplatsGlobal.ProductionAmplatsGlobalTSysSettings._currentProductionMonth.ToString());
        }

        //Fill Grids
        private void fillProdGrid()
        {
            gvProd.Columns.Clear();
            gcProd.DataSource = dtProd;
            gvProd.OptionsBehavior.Editable = false;
            gvProd.BestFitColumns();
        }

        private void fillTrammingGrid()
        {
            gvTramming.Columns.Clear();
            gcTramming.DataSource = dtTramming;
            gvTramming.OptionsBehavior.Editable = false;
            gvTramming.BestFitColumns();
        }

        private void fillSBGrid()
        {
            gvSB.Columns.Clear();
            gcSB.DataSource = dtSB;
            gvSB.OptionsBehavior.Editable = false;
            gvSB.BestFitColumns();
        }

        //Load Data
        private void loadProdData()
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " SELECT * FROM Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew WHERE ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(tbProdMonth.EditValue)) + "' ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();
            dtProd = _dbMan.ResultsDataTable;
            fillProdGrid();
        }

        private void loadTrammingData()
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " SELECT * FROM Mineware.dbo.tbl_BCS_ARMS_Interface_Transfer_TramNewCalcAccuTrackNewCalc " +
            " WHERE ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(tbProdMonth.EditValue)) + "'";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();
            dtTramming = _dbMan.ResultsDataTable;
            fillTrammingGrid();
        }

        private void loadSBData()
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = "SELECT * FROM Mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew " +
                " WHERE ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(tbProdMonth.EditValue)) + "'";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();
            dtSB = _dbMan.ResultsDataTable;
            fillSBGrid();
        }

        //Directory Checker
        private void checkDirectory()
        {
            directoryName = @"C:\Users\" + Environment.UserName + @"\Desktop\BCS Data Extraction\DataExtraction";
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            
        }

        //Excel Checker
        private void checkExcel()
        {
            excelName = directoryName + @"\Extraction " + DateTime.Now.ToString("MMM-dd-yyyy HH-mm-ss") + fileName + ".Xlsx";
            if (!File.Exists(excelName))
            {
                FileStream fs = new FileStream(excelName, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                fs.Close();
            }
        }

        //Pdf Checker
        private void checkPdf()
        {
            pdfName = directoryName + @"\Extraction " + DateTime.Now.ToString("MMM-dd-yyyy HH-mm-ss") + fileName + ".pdf";
            if (!File.Exists(pdfName))
            {
                FileStream fs = new FileStream(pdfName, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                fs.Close();
            }
        }

        //LOAD EVENT
        private void ucUpdateName_Load(object sender, EventArgs e)
        {
            //Load Prod Month

            getProdMonth();

            //Load Prod Data

            loadProdData();

            //Load Tramming Data

            loadTrammingData();

            //Load SB Data

            loadSBData();
        }

        private void beProd_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void MillMonth_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.UseWaitCursor = true;
            //Gets Prod Month From Procedure
            Procedures procs = new Procedures();
            procs.ProdMonthCalc(Convert.ToInt32(MillMonth.Text));
            MillMonth.Text = Procedures.Prod.ToString();
            procs.ProdMonthVis(Convert.ToInt32(MillMonth.Text));
            MillMonth1.Text = Procedures.Prod2;

            //Load Data on ProdMonth change
            loadProdData();
            loadTrammingData();
            loadSBData();
            this.Cursor = Cursors.Default;
            this.UseWaitCursor = false;
        }

        private void btnSendToExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            checkDirectory();
            if (tcMain.SelectedTab.Name == "tpProd")
            {
            fileName = "-Production";
            checkExcel();

                XlsxExportOptions options = new XlsxExportOptions();
                options.SheetName = "Production";
            gvProd.ExportToXlsx(excelName, options);
            }

            if (tcMain.SelectedTab.Name == "tpTramming")
            {
            fileName = "-Tramming";
            checkExcel();

                                XlsxExportOptions options = new XlsxExportOptions();
                options.SheetName = "Production";
                gvTramming.ExportToXlsx(excelName, options);
            }

            if (tcMain.SelectedTab.Name == "tpSB")
            {
            fileName = "-ShiftBoss";
            checkExcel();

                                XlsxExportOptions options = new XlsxExportOptions();
                options.SheetName = "Production";
                gvSB.ExportToXlsx(excelName, options);
            }
            acControl.Show(frmMain.ActiveForm, "Export Complete", "Click on folder to open");
        }

        private void btnPrintAsPdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.UseWaitCursor = true;
            //ProdPrint
            if (tcMain.SelectedTab.Name == "tpProd")
            {
            checkDirectory();
            fileName = "-Production";
            checkPdf();

                CompositeLink comp = new CompositeLink(new PrintingSystem());
                PrintableComponentLink link1 = new PrintableComponentLink();
                PrintableComponentLink link2 = new PrintableComponentLink();
                link1.Component = gcProd;
                link1.CreateReportHeaderArea += new CreateAreaEventHandler(link1_CreateReportHeaderArea);
                comp.Links.Add(link2);
                comp.Links.Add(link1);
                comp.Landscape = true;
                comp.PaperKind = System.Drawing.Printing.PaperKind.A2;
                comp.ExportToPdf(pdfName);

            }

            if (tcMain.SelectedTab.Name == "tpTramming")
            {
            //TrammingPrint
            checkDirectory();
            fileName = "-Tramming";
            checkPdf();

                CompositeLink comp = new CompositeLink(new PrintingSystem());
                PrintableComponentLink link1 = new PrintableComponentLink();
                PrintableComponentLink link2 = new PrintableComponentLink();
                link1.Component = gcTramming;
                link1.CreateReportHeaderArea += new CreateAreaEventHandler(link1_CreateReportHeaderArea);
                comp.Links.Add(link2);
                comp.Links.Add(link1);
                comp.Landscape = true;
                comp.PaperKind = System.Drawing.Printing.PaperKind.A2;
                comp.ExportToPdf(pdfName);
            }

            if (tcMain.SelectedTab.Name == "tpSB")
            {
            //SBPrint
            checkDirectory();
            fileName = "-ShiftBoss";
            checkPdf();

                CompositeLink comp = new CompositeLink(new PrintingSystem());
                PrintableComponentLink link1 = new PrintableComponentLink();
                PrintableComponentLink link2 = new PrintableComponentLink();
                link1.Component = gcSB;
                link1.CreateReportHeaderArea += new CreateAreaEventHandler(link1_CreateReportHeaderArea);
                comp.Links.Add(link2);
                comp.Links.Add(link1);
                comp.Landscape = true;
                comp.PaperKind = System.Drawing.Printing.PaperKind.A2;
                comp.ExportToPdf(pdfName);
            }
            this.Cursor = Cursors.Default;
            this.UseWaitCursor = false;
            acControl.Show(frmMain.ActiveForm, "Print Completed", "Click on folder to open");
            //RemovedCode2

        }

        void link1_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
        {

            e.Graph.DrawImage(peLogo.Image, new Rectangle(1770, 0, 280, 80), DevExpress.XtraPrinting.BorderSide.None, Color.White);
            e.Graph.Font = new Font("Arial", 10);
            e.Graph.DrawString("Prod Month: " + MillMonth1.Text, Color.Black, new Rectangle(0, 50, 280, 80), DevExpress.XtraPrinting.BorderSide.None);
            e.Graph.Font = new Font("Arial", 24);
            e.Graph.DrawString("Bonus Control - Data Extraction Report", Color.Black, new Rectangle(800, 0, 700, 80), DevExpress.XtraPrinting.BorderSide.None);
            e.Graph.BorderWidth = 0;
            e.Graph.DrawLine(new Point(30, 80), new Point(2020, 80), Color.Orange, 2);
        }

        private void acControl_ButtonClick(object sender, DevExpress.XtraBars.Alerter.AlertButtonClickEventArgs e)
        {
            Process.Start(directoryName);
        }

        private void rcMain_Click(object sender, EventArgs e)
        {

        }

        private void tbProdMonth_EditValueChanged(object sender, EventArgs e)
        {
            loadProdData();
            loadTrammingData();
            loadSBData();
            this.Cursor = Cursors.Default;
            this.UseWaitCursor = false;
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OnCloseTabRequest(new CloseTabArg(tabCaption));
        }
    }
}
