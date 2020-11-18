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

namespace Mineware.Systems.ProductionAmplatsBonus
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        Procedures procs = new Procedures();
        
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void HidePnls()
        {
            pnlTrammingCapture.Visible = false;
            pnlTrammingCapture.Dock = DockStyle.None;
            pnlProdCapture.Visible = false;
            pnlProdCapture.Dock = DockStyle.None;
            pnlTestGangMapping.Visible = false;
            pnlTestGangMapping.Dock = DockStyle.None;
            pnlCalculations.Visible = false;
            PnlMingParam.Visible = false;
            panel13.Visible = false;
            EngPnl.Visible = false;
            UsersPnl.Visible = false;
            GenPnl.Visible = false;
            AbsentPnl.Visible = false;
            BasicIncTablePnl.Visible = false;
            TrammingPnl.Visible = false;
            SysAdminPnl.Visible = false;
            pnlDataExtraction.Visible = false;
        }

        public void Enabled()
        {

            navBarItem3.Enabled = false;
            navBarItem13.Enabled = false;
            navBarItem1.Enabled = false;
            navBarItem2.Enabled = false;
            navBarItem14.Enabled = false;
            navBarItem7.Enabled = false;
            SBBonusItem.Enabled = false;
            navBarItem12.Enabled = false;
            navBarItem18.Enabled = false;
            navBarItem25.Enabled = false;
            navBarItem23.Visible = true;
            navBarItem4.Enabled = false;
            navBarItem23.Enabled = false;
            btnGangMapping.Enabled = false;
            btnDataExtraction.Enabled = false;

        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            navBarItem13.Enabled = false;
            WelcomeLbl.Text = "Welcome " + clsUserInfo.UserName + " to the Bonus Control System";

            Enabled();

            ///Load Users Rights///
            ///

            //btnGangMapping.Visible = false;
            if (clsUserInfo.UserName == "Mineware Consulting")
            {
                simpleButton3.Visible = true;
                navBarItem23.Visible = true;
                btnGangMapping.Visible = true;
                navBarItem13.Enabled = true;

            }
 


            MWDataManager.clsDataAccess _dbManUsers = new MWDataManager.clsDataAccess();
            _dbManUsers.ConnectionString = "";
            _dbManUsers.SqlStatement = " select * from mineware.dbo.tbl_BCS_users where username = '" + clsUserInfo.UserID + "' ";
            _dbManUsers.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManUsers.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManUsers.ExecuteInstruction();

            DataTable dtEdit = _dbManUsers.ResultsDataTable;

            if (dtEdit.Rows.Count > 0)
            {

                if (clsUserInfo.UserID == "MINEWARE")
                {
                    if (dtEdit.Rows[0]["safety"].ToString() == "Y")
                {
                    navBarItem3.Enabled = true;
                }

                if (dtEdit.Rows[0]["shiftCapt"].ToString() == "Y")
                {
                    navBarItem13.Enabled = false;
                }

                if (dtEdit.Rows[0]["MonthParamEng"].ToString() == "Y")
                {
                    navBarItem1.Enabled = true;
                }

                if (dtEdit.Rows[0]["Mining"].ToString() == "Y")
                {
                    navBarItem2.Enabled = true;
                }

                if (dtEdit.Rows[0]["General"].ToString() == "Y")
                {
                    navBarItem14.Enabled = true;
                }

                if (dtEdit.Rows[0]["MiningCrewBonus"].ToString() == "Y")
                {
                    navBarItem7.Enabled = true;
                }

                if (dtEdit.Rows[0]["SBBonusCalc"].ToString() == "Y")
                {
                    SBBonusItem.Enabled = true;
                }

                if (dtEdit.Rows[0]["BonusEng"].ToString() == "Y")
                {
                    navBarItem12.Enabled = true;
                }

                if (dtEdit.Rows[0]["Users"].ToString() == "Y")
                {
                    navBarItem4.Enabled = true;
                    btnGangMapping.Enabled = true;
                    btnDataExtraction.Enabled = true;
                    navBarItem23.Enabled = true;
                    navBarItem23.Visible = true;
                }

                if (dtEdit.Rows[0]["TrammingCapt"].ToString() == "Y")
                {
                    navBarItem18.Enabled = true;
                }

                if (dtEdit.Rows[0]["ProductionCapt"].ToString() == "Y")
                {
                    navBarItem25.Enabled = true;
                }

                }

                if (clsUserInfo.UserID != "MINEWARE")
                {
                   if (dtEdit.Rows[0]["safety"].ToString() == "Y")
                {
                    navBarItem3.Enabled = true;
                }

                if (dtEdit.Rows[0]["shiftCapt"].ToString() == "Y")
                {
                    navBarItem13.Enabled = false;
                }

                if (dtEdit.Rows[0]["MonthParamEng"].ToString() == "Y")
                {
                    navBarItem1.Enabled = true;
                }

                if (dtEdit.Rows[0]["Mining"].ToString() == "Y")
                {
                    navBarItem2.Enabled = true;
                }

                if (dtEdit.Rows[0]["General"].ToString() == "Y")
                {
                    navBarItem14.Enabled = true;
                }

                if (dtEdit.Rows[0]["MiningCrewBonus"].ToString() == "Y")
                {
                    navBarItem7.Enabled = true;
                }

                if (dtEdit.Rows[0]["SBBonusCalc"].ToString() == "Y")
                {
                    SBBonusItem.Enabled = true;
                }

                if (dtEdit.Rows[0]["BonusEng"].ToString() == "Y")
                {
                    navBarItem12.Enabled = true;
                }

                if (dtEdit.Rows[0]["Users"].ToString() == "Y")
                {
                    navBarItem4.Enabled = true;
                    btnGangMapping.Enabled = true;
                    btnDataExtraction.Enabled = true;
                    navBarItem23.Enabled = true;
                    navBarItem23.Visible = true;

                }

                if (dtEdit.Rows[0]["TrammingCapt"].ToString() == "Y")
                {
                    navBarItem18.Enabled = true;
                }

                if (dtEdit.Rows[0]["ProductionCapt"].ToString() == "Y")
                {
                    navBarItem25.Enabled = true;
                }
                }


            }



            //////////////////////////////////////


            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = "";
            _dbMan.SqlStatement = " select currentproductionmonth pm from mineware.dbo.tbl_sysset ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            SysSettings.ProdMonth = Convert.ToInt32(_dbMan.ResultsDataTable.Rows[0][0].ToString());
           
            
            ProdMonthTxt.Text = Convert.ToString(SysSettings.ProdMonth);           
            procs.ProdMonthVis(Convert.ToInt32(ProdMonthTxt.Text));
            ProdMonth1Txt.Text = Procedures.Prod2;
            ProdMonth1Txt.TextAlign = HorizontalAlignment.Center;


            MonthTxt.Text = Convert.ToString(SysSettings.ProdMonth);            
            procs.ProdMonthVis(Convert.ToInt32(MonthTxt.Text));
            Month1Txt.Text = Procedures.Prod2;
            Month1Txt.TextAlign = HorizontalAlignment.Center;


            PMnthTxt.Text = Convert.ToString(SysSettings.ProdMonth);
            procs.ProdMonthVis(Convert.ToInt32(ProdMonthTxt.Text));
            PMnthTxt1.Text = Procedures.Prod2;
            PMnthTxt1.TextAlign = HorizontalAlignment.Center;

            MillMonth.Text = Convert.ToString(SysSettings.ProdMonth);
            procs.ProdMonthVis(Convert.ToInt32(MillMonth.Text));
            MillMonth1.Text = Procedures.Prod2;
            MillMonth1.TextAlign = HorizontalAlignment.Center;


            MillMontha.Text = Convert.ToString(SysSettings.ProdMonth);
            procs.ProdMonthVis(Convert.ToInt32(MillMontha.Text));
            MillMonth1a.Text = Procedures.Prod2;
            MillMonth1a.TextAlign = HorizontalAlignment.Center;

            //LoadDailyColumns();
            //LoadTheCalendar();

          
          //  btnBcs_Click(null,null);
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

        public static Form IsReportFormAlreadyOpen(Type FormType)
        {
            foreach (Form OpenForm in Application.OpenForms)
            {
                if (OpenForm.GetType() == FormType)
                    return OpenForm;
            }

            return null;
        }

        private void btnStpCrewBonus_Click(object sender, EventArgs e)
        {
            //frmStpCrewBonus RepFrm = (frmStpCrewBonus)IsBookingFormAlreadyOpen(typeof(frmStpCrewBonus));
            //if (RepFrm == null)
            //{
            //    RepFrm = new frmStpCrewBonus();
            //    RepFrm.Text = "Stope Crew Bonus";
            //    RepFrm.Show();
            //}
            //else
            //{
            //    RepFrm.WindowState = FormWindowState.Maximized;
            //    RepFrm.Select();
            //}
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnSysAdmin_Click(object sender, EventArgs e)
        {
            //frmSysAdmin RepFrm = (frmSysAdmin)IsBookingFormAlreadyOpen(typeof(frmSysAdmin));
            //if (RepFrm == null)
            //{
            //    RepFrm = new frmSysAdmin();
            //    RepFrm.Text = "System Admin";
            //    RepFrm.Show();
            //}
            //else
            //{
            //    RepFrm.WindowState = FormWindowState.Maximized;
            //    RepFrm.Select();
            //}
        }

        private void btnSafety_Click(object sender, EventArgs e)
        {
            //ucSafetyNew RepFrm = (ucSafetyNew)IsBookingFormAlreadyOpen(typeof(ucSafetyNew));
            //if (RepFrm == null)
            //{
            //    RepFrm = new ucSafetyNew();
            //    RepFrm.Text = "Safety";
            //    RepFrm.Show();
            //}
            //else
            //{
            //    RepFrm.WindowState = FormWindowState.Maximized;
            //    RepFrm.Select();
            //}
        }

        private void btnCat2Capt_Click(object sender, EventArgs e)
        {
            frmCat2Capt RepFrm = (frmCat2Capt)IsBookingFormAlreadyOpen(typeof(frmCat2Capt));
            if (RepFrm == null)
            {
                RepFrm = new frmCat2Capt();
                RepFrm.Text = "Cat 2 to 8 Capture";
                RepFrm.Show();
            }
            else
            {
                RepFrm.WindowState = FormWindowState.Maximized;
                RepFrm.Select();
            }
        }

        private void ProdBtn_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bmp = Properties.Resources.calculator_icon;
            bmp.MakeTransparent(Color.White);
            int x = 4;// (SafetyBtn.Width - bmp.Width) / 8;
            int y = (btnBcs.Height - bmp.Height) / 2;
            e.Graphics.DrawImage(bmp, x, y);
        }

        private void btnDevCrewBonus_Click(object sender, EventArgs e)
        {

        }

        private void btnBcs_Click(object sender, EventArgs e)
        {            
            pnlCalculations.Dock = DockStyle.Fill;
            pnlCalculations.Visible = true;

            pnlShiftCapture.Visible = false;
            pnlReports.Visible = false;

            btnBcs.BackColor = Color.YellowGreen;
            btnSysset.BackColor = button5.BackColor;
            btnReports.BackColor = button5.BackColor;
        }

        private void btnSysset_Click(object sender, EventArgs e)
        {
            pnlShiftCapture.Dock = DockStyle.Fill;
            pnlShiftCapture.Visible = true;

            pnlCalculations.Visible = false;
            pnlReports.Visible = false;

            btnSysset.BackColor = Color.YellowGreen;
            btnBcs.BackColor = button5.BackColor;
            btnReports.BackColor = button5.BackColor;
        }

        private void btnStpSBBonus_Click(object sender, EventArgs e)
        {
            //frmSBBonus SBForm = (frmSBBonus)IsBookingFormAlreadyOpen(typeof(frmSBBonus));
            //if (SBForm == null)
            //{
            //    SBForm = new frmSBBonus();
            //    SBForm.Text = "Stoping Shift Boss Bonus";
            //    SBForm.Show();
            //}
            //else
            //{
            //    SBForm.WindowState = FormWindowState.Maximized;
            //    SBForm.Select();
            //}
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            string MyPath = SysSettings.CurDir + @"\BonusSystem\BonusSystem.exe";
            Process.Start(MyPath);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string MyPath = SysSettings.CurDir + @"\SystemSettings\BCS_System_Settings.exe";
            Process.Start(MyPath);
        }

        private void btnSBShifts_Click(object sender, EventArgs e)
        {
            //ucSBShifts RepFrm = (ucSBShifts)IsBookingFormAlreadyOpen(typeof(ucSBShifts));
            //if (RepFrm == null)
            //{
            //    RepFrm = new ucSBShifts();
            //    RepFrm.Text = "Shift Boss Shift Sheets";
            //    RepFrm.Show();
            //}
            //else
            //{
            //    RepFrm.WindowState = FormWindowState.Maximized;
            //    RepFrm.Select();
            //}
        }

        private void btnSBCrew_Click(object sender, EventArgs e)
        {
            //ucSBShifts RepFrm = (ucSBShifts)IsBookingFormAlreadyOpen(typeof(ucSBShifts));
            //if (RepFrm == null)
            //{
            //    RepFrm = new ucSBShifts();
            //    RepFrm.Text = "Shift Boss Crew Achievements";
            //    RepFrm.Show();
            //}
            //else
            //{
            //    RepFrm.WindowState = FormWindowState.Maximized;
            //    RepFrm.Select();
            //}
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //frmSBBonus SBForm = (frmSBBonus)IsBookingFormAlreadyOpen(typeof(frmSBBonus));
            //if (SBForm == null)
            //{
            //    SBForm = new frmSBBonus();
            //    SBForm.Text = "Development Shift Boss Bonus";
            //    SBForm.Show();
            //}
            //else
            //{
            //    SBForm.WindowState = FormWindowState.Maximized;
            //    SBForm.Select();
            //}
        }

        private void button18_Click(object sender, EventArgs e)
        {

        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            pnlReports.Dock = DockStyle.Fill;
            pnlReports.Visible = true;

            pnlCalculations.Visible = false;
            pnlShiftCapture.Visible = false;

            btnReports.BackColor = Color.YellowGreen;
            btnBcs.BackColor = button5.BackColor;
            btnSysset.BackColor = button5.BackColor;
        }

        private void btnReports_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bmp = Properties.Resources.Report;
            bmp.MakeTransparent(Color.White);
            int x = 4;// (SafetyBtn.Width - bmp.Width) / 8;
            int y = (btnBcs.Height - bmp.Height) / 2;
            e.Graphics.DrawImage(bmp, x, y);
        }

        private void btnSysset_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bmp = Properties.Resources.People_32;
            bmp.MakeTransparent(Color.White);
            int x = 4;// (SafetyBtn.Width - bmp.Width) / 8;
            int y = (btnBcs.Height - bmp.Height) / 2;
            e.Graphics.DrawImage(bmp, x, y);
        }

        private void btnSbMonthly_Click(object sender, EventArgs e)
        {
            //ucSBShifts RepFrm = (ucSBShifts)IsBookingFormAlreadyOpen(typeof(ucSBShifts));
            //if (RepFrm == null)
            //{
            //    RepFrm = new ucSBShifts();
            //    RepFrm.Text = "Shift Boss Incentive Summary";
            //    RepFrm.Show();
            //}
            //else
            //{
            //    RepFrm.WindowState = FormWindowState.Maximized;
            //    RepFrm.Select();
            //}
        }

        private void btnSbYearly_Click(object sender, EventArgs e)
        {

            //ucSBShifts RepFrm = (ucSBShifts)IsBookingFormAlreadyOpen(typeof(ucSBShifts));
            //if (RepFrm == null)
            //{
            //    RepFrm = new ucSBShifts();
            //    RepFrm.Text = "Yearly Summary";
            //    RepFrm.Show();
            //}
            //else
            //{
            //    RepFrm.WindowState = FormWindowState.Maximized;
            //    RepFrm.Select();
            //}

        }

        private void btnBaseRateReport_Click(object sender, EventArgs e)
        {
            //ucBaseRateReport RepFrm = (ucBaseRateReport)IsBookingFormAlreadyOpen(typeof(ucBaseRateReport));
            //if (RepFrm == null)
            //{
            //    RepFrm = new ucBaseRateReport();
            //    RepFrm.Text = "Base Rate Calculation Report";
            //    RepFrm.Show();
            //}
            //else
            //{
            //    RepFrm.WindowState = FormWindowState.Maximized;
            //    RepFrm.Select();
            //}
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button25_Click(object sender, EventArgs e)
        {
            //ucAuditReports RepFrm = (ucAuditReports)IsBookingFormAlreadyOpen(typeof(ucAuditReports));
            //if (RepFrm == null)
            //{
            //    RepFrm = new ucAuditReports();
            //    RepFrm.lblReportType.Text = "Engineering Production";
            //    RepFrm.Show();
            //}
            //else
            //{
            //    RepFrm.WindowState = FormWindowState.Maximized;
            //    RepFrm.Select();
            //}
        }

        private void button24_Click(object sender, EventArgs e)
        {
            //ucAuditReports RepFrm = (ucAuditReports)IsBookingFormAlreadyOpen(typeof(ucAuditReports));
            //if (RepFrm == null)
            //{
            //    RepFrm = new ucAuditReports();
            //    RepFrm.lblReportType.Text = "Engineering Workshops";
            //    RepFrm.Show();
            //}
            //else
            //{
            //    RepFrm.WindowState = FormWindowState.Maximized;
            //    RepFrm.Select();
            //}
        }

        private void button22_Click(object sender, EventArgs e)
        {
            //ucAuditReports RepFrm = (ucAuditReports)IsBookingFormAlreadyOpen(typeof(ucAuditReports));
            //if (RepFrm == null)
            //{
            //    RepFrm = new ucAuditReports();
            //    RepFrm.lblReportType.Text = "TSD Vent";
            //    RepFrm.Show();
            //}
            //else
            //{
            //    RepFrm.WindowState = FormWindowState.Maximized;
            //    RepFrm.Select();
            //}
        }

        private void button23_Click(object sender, EventArgs e)
        {
            //ucAuditReports RepFrm = (ucAuditReports)IsBookingFormAlreadyOpen(typeof(ucAuditReports));
            //if (RepFrm == null)
            //{
            //    RepFrm = new ucAuditReports();
            //    RepFrm.lblReportType.Text = "Engineering Shafts";
            //    RepFrm.Show();
            //}
            //else
            //{
            //    RepFrm.WindowState = FormWindowState.Maximized;
            //    RepFrm.Select();
            //}
        }

        private void button21_Click(object sender, EventArgs e)
        {
            //ucAuditReports RepFrm = (ucAuditReports)IsBookingFormAlreadyOpen(typeof(ucAuditReports));
            //if (RepFrm == null)
            //{
            //    RepFrm = new ucAuditReports();
            //    RepFrm.lblReportType.Text = "Production";
            //    RepFrm.Show();
            //}
            //else
            //{
            //    RepFrm.WindowState = FormWindowState.Maximized;
            //    RepFrm.Select();
            //}
        }

        private void button19_Click(object sender, EventArgs e)
        {
            //ucAuditReports RepFrm = (ucAuditReports)IsBookingFormAlreadyOpen(typeof(ucAuditReports));
            //if (RepFrm == null)
            //{
            //    RepFrm = new ucAuditReports();
            //    RepFrm.lblReportType.Text = "Plant";
            //    RepFrm.Show();
            //}
            //else
            //{
            //    RepFrm.WindowState = FormWindowState.Maximized;
            //    RepFrm.Select();
            //}
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //ucMonthlySummariesRep RepFrm = (ucMonthlySummariesRep)IsReportFormAlreadyOpen(typeof(ucMonthlySummariesRep));
            //if (RepFrm == null)
            //{
            //    RepFrm = new ucMonthlySummariesRep();
            //    RepFrm.Text = "Monthly Summary";
            //    RepFrm.Show();
            //}
            //else
            //{
            //    RepFrm.WindowState = FormWindowState.Maximized;
            //    RepFrm.Select();
            //}

        }

        private void button15_Click(object sender, EventArgs e)
        {
            //ucMonthlySummariesRep RepFrm = (ucMonthlySummariesRep)IsReportFormAlreadyOpen(typeof(ucMonthlySummariesRep));
            //if (RepFrm == null)
            //{
            //    RepFrm = new ucMonthlySummariesRep();
            //    RepFrm.Text = "Monthly Detail Summary";
            //    RepFrm.Show();
            //}
            //else
            //{
            //    RepFrm.WindowState = FormWindowState.Maximized;
            //    RepFrm.Select();
            //}
        }

        private void btnArtisanReport_Click(object sender, EventArgs e)
        {
            //ucBaseRateReport RepFrm = (ucBaseRateReport)IsBookingFormAlreadyOpen(typeof(ucBaseRateReport));
            //if (RepFrm == null)
            //{
            //    RepFrm = new ucBaseRateReport();
            //    RepFrm.Text = "Engineering Artisan's per level";
            //    RepFrm.Show();
            //}
            //else
            //{
            //    RepFrm.WindowState = FormWindowState.Maximized;
            //    RepFrm.Select();
            //}
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //ucReports RepFrm = (ucReports)IsBookingFormAlreadyOpen(typeof(ucReports));
            //if (RepFrm == null)
            //{
            //    RepFrm = new ucReports();
            //    RepFrm.Text = "Production Unit Results";
            //    RepFrm.CurReport = "Prod Unit Results";
            //    RepFrm.Show();
            //}
            //else
            //{
            //    RepFrm.WindowState = FormWindowState.Maximized;
            //    RepFrm.Select();
            //}
        }

        private void button17_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            
            HidePnls();
            pnlDataExtraction.Visible = false;
            pnlTrammingCapture.Visible = false;
            pnlProdCapture.Visible = false;
            pnlTestGangMapping.Visible = false;
            PnlMingParam.Visible = true;
            PnlMingParam.Dock = DockStyle.Fill;
        }

        private void SelectGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectGroup.SelectedIndex == 0)
                LoadMiningResultsStope();
            else
                LoadMiningResultsDev();
        }

        private void navBarControl2_Click(object sender, EventArgs e)
        {

        }

        void LoadMiningResultsStope()
        {
            gridControl6.Visible = false;


            MWDataManager.clsDataAccess _dbMan3Mnth = new MWDataManager.clsDataAccess();
            _dbMan3Mnth.ConnectionString = "";
            _dbMan3Mnth.SqlStatement = " exec northampas.[dbo].[sp_BMCS_GetStopingInfo] '" + ProdMonthTxt.Text + "'";

            _dbMan3Mnth.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan3Mnth.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan3Mnth.ExecuteInstruction();

            DataTable dt1 = _dbMan3Mnth.ResultsDataTable;

            DataSet ds = new DataSet();

            if (ds.Tables.Count > 0)
                ds.Tables.Clear();

            ds.Tables.Add(dt1);

            gridControl5.Visible = true;

            gridControl5.DataSource = ds.Tables[0];

            StpSec.FieldName = "sb";
            StpCall.FieldName = "call";
            StpAch.FieldName = "mined";
            StpSafety.FieldName = "safety";
            StpRock.FieldName = "rock";
            StpTons.FieldName = "tons";
            StpSwps.FieldName = "sweeps";
            StpSwpsBonus.FieldName = "savedSweeps";



        }

        void LoadMiningResultsDev()
        {
            gridControl5.Visible = false;
            MWDataManager.clsDataAccess _dbMan3Mnth = new MWDataManager.clsDataAccess();
            _dbMan3Mnth.ConnectionString = "";
            _dbMan3Mnth.SqlStatement = " select *, cast(round(MeasOther/PlanOther1*100,0) as int) OtherPerc  from ( select " +

                                        " case when PlanOther = 0 then null else PlanOther end as PlanOther1 , \r\n" +
                                         " *, cast(round(MeasLateral/PlanLateral1*100,0) as int) LateralPerc,cast(round( MeasRaises/PlanRaises1*100,0) as int) RaisesPerc, cast(round(MeasTotal/PlanTotal1*100,0) as int) TotalPerc  from (  select   \r\n" +
                                         " case when PlanLateral = 0 then null else PlanLateral end as PlanLateral1, \r\n" +
                                         " case when PlanRaises = 0 then null else PlanRaises end as PlanRaises1, \r\n" +
                                         " case when PlanTotal = 0 then null else PlanTotal end as PlanTotal1, \r\n" +
                                         " *, sb + ':' +sbname sb1, MeasTotal - MeasLateral - MeasRaises MeasOther,  \r\n" +
                                         " PlanTotal - PlanLateral - PlanRaises PlanOther \r\n" +
                                         " from  mineware.dbo.vw_SBoss_DevResults ) a) b  \r\n" +
                                         " where prodmonth = '" + ProdMonthTxt.Text + "'";

            _dbMan3Mnth.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan3Mnth.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan3Mnth.ExecuteInstruction();

            DataTable dt1 = _dbMan3Mnth.ResultsDataTable;

            DataSet ds = new DataSet();

            if (ds.Tables.Count > 0)
                ds.Tables.Clear();

            ds.Tables.Add(dt1);

            gridControl6.Visible = true;

            gridControl6.DataSource = ds.Tables[0];

            DevSec.FieldName = "sb";
            DevName.FieldName = "sbname";
            DevSI.FieldName = "safety";
            DevLatCall.FieldName = "PlanLateral";
            DevLatAch.FieldName = "MeasLateral";
            DevLatPer.FieldName = "LateralPerc";
            DevRaiseCall.FieldName = "PlanRaises";
            DevRaiseAch.FieldName = "MeasRaises";
            DevRaisePer.FieldName = "RaisesPerc";
            DevOtherCall.FieldName = "PlanOther";
            DevOtherAch.FieldName = "MeasOther";
            DevOtherPer.FieldName = "OtherPerc";
            DevTotalCall.FieldName = "PlanTotal";
            DevTotalAch.FieldName = "MeasTotal";
            DevTotalPer.FieldName = "TotalPerc";
            DevRE.FieldName = "Rock";
            DevReefTonsHoist.FieldName = "TonsHoisted";

        }
        

        private void navBarItem5_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {            
            pnlSBResults.Visible = true;
            pnlSBResults.Dock = DockStyle.Fill;
            SelectGroup.SelectedIndex = 1;

            MiningFactorsPnl.Visible = false;
            BasicIncTablePnl.Visible = false;
            TramPnl.Visible = false;







        }

        private void ProdMonthTxt_Click(object sender, EventArgs e)
        {
            Procedures procs = new Procedures();
            procs.ProdMonthCalc(Convert.ToInt32(ProdMonthTxt.Text));
            ProdMonthTxt.Text = Procedures.Prod.ToString();
            procs.ProdMonthVis(Convert.ToInt32(ProdMonthTxt.Text));
            ProdMonth1Txt.Text = Procedures.Prod2;

            LoadMiningFact();
        }

        private void WelcomeLbl_Click(object sender, EventArgs e)
        {

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
            _dbMan1.ConnectionString = "";
            _dbMan1.SqlStatement = " select * from mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat where prodmonth = '" + MonthTxt.Text + "'  order by cat ";
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
            _dbMan3Mnth.ConnectionString = "";
            _dbMan3Mnth.SqlStatement = " select * from mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveDetail where prodmonth = '" + MonthTxt.Text + "' order by  Tons ";

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

        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            pnlTestGangMapping.Visible = false;
            PnlMingParam.Visible = false;

            pnlTrammingCapture.Visible = false;
            pnlProdCapture.Visible = false;
            pnlDataExtraction.Visible = false;
            TrammingPnl.Visible = false;
            UsersPnl.Visible = false;
            EngPnl.Visible = true;
            EngPnl.Dock = DockStyle.Fill;
            PeramPnl.Visible = false;


        }

     

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            SaveHeaders();
            SaveData();


        }

        void SaveData()
        {
            MWDataManager.clsDataAccess _dbManNS = new MWDataManager.clsDataAccess();
            _dbManNS.ConnectionString = "";
            _dbManNS.SqlStatement = _dbManNS.SqlStatement + "  ";

            _dbManNS.SqlStatement = _dbManNS.SqlStatement + " delete from mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveDetail where prodmonth = '" + MonthTxt.Text + "' ";
            for (int k = 0; k <= bandedGridView1.RowCount - 1; k++)
            {
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " delete from mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveDetail where prodmonth = '" + MonthTxt.Text + "' and tons = '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[0]) + "' ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveDetail Values(  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + MonthTxt.Text + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView1.GetRowCellValue(k,bandedGridView1.Columns[0]) + "',  ";
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
            _dbMan.ConnectionString = "";
            _dbMan.SqlStatement = " delete from mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat where prodmonth = '" + MonthTxt.Text.ToString() + "' ";

            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = "";
            _dbMan1.SqlStatement = " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + MonthTxt.Text.ToString() + "', '" + CatLbl1.Text.ToString() + "', '" + Cat1.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + MonthTxt.Text.ToString() + "', '" + CatLbl2.Text.ToString() + "', '" + Cat2.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + MonthTxt.Text.ToString() + "', '" + CatLbl3.Text.ToString() + "', '" + Cat3.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + MonthTxt.Text.ToString() + "', '" + CatLbl4.Text.ToString() + "', '" + Cat4.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + MonthTxt.Text.ToString() + "', '" + CatLbl5.Text.ToString() + "', '" + Cat5.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + MonthTxt.Text.ToString() + "', '" + CatLbl6.Text.ToString() + "', '" + Cat6.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + MonthTxt.Text.ToString() + "', '" + CatLbl7.Text.ToString() + "', '" + Cat7.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + MonthTxt.Text.ToString() + "', '" + CatLbl8.Text.ToString() + "', '" + Cat8.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + MonthTxt.Text.ToString() + "', '" + CatLbl9.Text.ToString() + "', '" + Cat9.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + MonthTxt.Text.ToString() + "', '" + CatLbl10.Text.ToString() + "', '" + Cat10.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + MonthTxt.Text.ToString() + "', '" + CatLbl11.Text.ToString() + "', '" + Cat11.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + MonthTxt.Text.ToString() + "', '" + CatLbl12.Text.ToString() + "', '" + Cat12.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + MonthTxt.Text.ToString() + "', '" + CatLbl13.Text.ToString() + "', '" + Cat13.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + MonthTxt.Text.ToString() + "', '" + CatLbl14.Text.ToString() + "', '" + Cat14.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + MonthTxt.Text.ToString() + "', '" + CatLbl15.Text.ToString() + "', '" + Cat15.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + MonthTxt.Text.ToString() + "', '" + CatLbl16.Text.ToString() + "', '" + Cat16.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + MonthTxt.Text.ToString() + "', '" + CatLbl17.Text.ToString() + "', '" + Cat17.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + MonthTxt.Text.ToString() + "', '" + CatLbl18.Text.ToString() + "', '" + Cat18.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + MonthTxt.Text.ToString() + "', '" + CatLbl19.Text.ToString() + "', '" + Cat19.Text.ToString() + "' ) \r\n" +
                                   " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveCat Values( '" + MonthTxt.Text.ToString() + "', '" + CatLbl20.Text.ToString() + "', '" + Cat20.Text.ToString() + "' ) ";
            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();

           
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
            _dbMan1.ConnectionString = "";
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

            MWDataManager.clsDataAccess _dbMan1ESfa = new MWDataManager.clsDataAccess();
            _dbMan1ESfa.ConnectionString = "";

            _dbMan1ESfa.SqlStatement = " Select 'ESF A' calendartypeid,calendardate,'Y' WORKINGDAY   from (  \r\n" +

                                   " select distinct(calendardate) from [northampas].[dbo].caltype  \r\n" +
                                   " where year(calendardate) = '" + txtYear.Value + "' and month(calendardate) = '" + txtMonth.Value + "' and CALENDARDATE < GETDATE() -1 \r\n" +
                                  "  and calendardate not in (SELECT CALENDARDATE  \r\n" +

                                  "   FROM [Mineware].[dbo].[BMCS_CALTYPE]  \r\n" +
                                  "  where calendartypeid = 'ESF A' and  year(calendardate) = '" + txtYear.Value + "' and month(calendardate) = '" + txtMonth.Value + "' and CALENDARDATE < GETDATE() -1 )  \r\n" +
                                  "  ) a order by calendardate";

            _dbMan1ESfa.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1ESfa.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1ESfa.ExecuteInstruction();


            DataTable dt = _dbMan1ESfa.ResultsDataTable;

            if (dt.Rows.Count > 0)
            {
                MWDataManager.clsDataAccess _dbMan1ESfaInsert = new MWDataManager.clsDataAccess();
                _dbMan1ESfaInsert.ConnectionString = "";

                _dbMan1ESfaInsert.SqlStatement = " select 'A' \r\n";
                foreach (DataRow r in dt.Rows)
                {
                    _dbMan1ESfaInsert.SqlStatement = _dbMan1ESfaInsert.SqlStatement + " insert into [Mineware].[dbo].[BMCS_CALTYPE] (calendartypeid,calendardate,WORKINGDAY) \r\n" +
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

            MWDataManager.clsDataAccess _dbMan1ESfb = new MWDataManager.clsDataAccess();
            _dbMan1ESfb.ConnectionString = "";

            _dbMan1ESfb.SqlStatement = " Select 'ESF B' calendartypeid,calendardate,'Y' WORKINGDAY   from (  \r\n" +

                                   " select distinct(calendardate) from [northampas].[dbo].caltype  \r\n" +
                                   " where year(calendardate) = '" + txtYear.Value + "' and month(calendardate) = '" + txtMonth.Value + "' and CALENDARDATE < GETDATE() -1 \r\n" +
                                  "  and calendardate not in (SELECT CALENDARDATE  \r\n" +

                                  "   FROM [Mineware].[dbo].[BMCS_CALTYPE]  \r\n" +
                                  "  where calendartypeid = 'ESF B' and  year(calendardate) = '" + txtYear.Value + "' and month(calendardate) = '" + txtMonth.Value + "' and CALENDARDATE < GETDATE() -1 )  \r\n" +
                                  "  ) a order by calendardate";

            _dbMan1ESfb.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1ESfb.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1ESfb.ExecuteInstruction();


            DataTable dt = _dbMan1ESfb.ResultsDataTable;

            if (dt.Rows.Count > 0)
            {
                MWDataManager.clsDataAccess _dbMan1ESfbInsert = new MWDataManager.clsDataAccess();
                _dbMan1ESfbInsert.ConnectionString = "";

                _dbMan1ESfbInsert.SqlStatement = " select 'A' \r\n";
                foreach (DataRow r in dt.Rows)
                {
                    _dbMan1ESfbInsert.SqlStatement = _dbMan1ESfbInsert.SqlStatement + " insert into [Mineware].[dbo].[BMCS_CALTYPE] (calendartypeid,calendardate,WORKINGDAY) \r\n" +
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
            _dbMan.ConnectionString = "";
            
            _dbMan.SqlStatement = " select * from mineware.dbo.tbl_BCS_Eng_FactorNew1  where prodmonth = '" + MonthTxt.Text.ToString() + "'";

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

        private void LoadTheCalendar()
        {

            lblSelectedDate.Text = Convert.ToDateTime(txtYear.Value + "/" + txtMonth.Value + "/01").ToString("MMMM");
            lblSelectedDate.Text = lblSelectedDate.Text + " " + Convert.ToDateTime(txtYear.Value + "/" + txtMonth.Value + "/01").ToString("yyyy") ;

            DateTime StartDate = new DateTime(Convert.ToInt32(txtYear.Value), Convert.ToInt32(txtMonth.Value), 1);
            int DaysInMonth = (System.DateTime.DaysInMonth(Convert.ToInt32(txtYear.Value), Convert.ToInt32(txtMonth.Value)));
            int DayOfWeek = Convert.ToInt32(StartDate.DayOfWeek);
            int TheDay = 1;

            gvDays.RowCount = 0;

            gvDays.ColumnCount  = 7;

            for (int MyRows = 0; MyRows < 1; MyRows++)
            {
                int NewRow = gvDays.Rows.Add();
                gvDays.Rows[NewRow].Height = 20;
                for (int Days = DayOfWeek; Days < 7; Days++)
                {
                    gvDays.CurrentCell = gvDays[Days, 0];
                    gvDays.CurrentCell.Value = TheDay++;
                    

                }
            }

            ////// Now load the rest of the Lines for the Calendar 
            for (int MyRows = 0; MyRows < 5; MyRows++)
            {
                int NewRow = gvDays.Rows.Add();
                gvDays.Rows[NewRow].Height = 20;
                for (int Days = 0; Days < 7; Days++)
                {
                    gvDays.CurrentCell = gvDays[Days, NewRow];
                    if (TheDay <= DaysInMonth)
                    {
                        gvDays.CurrentCell.Value = TheDay++;
                    }
                    else
                    {
                        gvDays.CurrentCell.Value = "";
                        gvDays.CurrentCell.Style.BackColor = Color.LightGray;
                    }
                }
            }

           //////2//////

            lblSelectedDate2.Text = Convert.ToDateTime(txtYear2.Value + "/" + txtMonth2.Value + "/01").ToString("MMMM");
            lblSelectedDate2.Text = lblSelectedDate2.Text + " " + Convert.ToDateTime(txtYear2.Value + "/" + txtMonth2.Value + "/01").ToString("yyyy");

            DateTime StartDate2 = new DateTime(Convert.ToInt32(txtYear2.Value), Convert.ToInt32(txtMonth2.Value), 1);
            int DaysInMonth2 = (System.DateTime.DaysInMonth(Convert.ToInt32(txtYear2.Value), Convert.ToInt32(txtMonth2.Value)));
            int DayOfWeek2 = Convert.ToInt32(StartDate2.DayOfWeek);
            int TheDay2 = 1;

            gvDays2.RowCount = 0;
            gvDays2.ColumnCount = 7;

            for (int MyRows2 = 0; MyRows2 < 1; MyRows2++)
            {
                int NewRow2 = gvDays2.Rows.Add();
                gvDays2.Rows[NewRow2].Height = 20;
                for (int Days2 = DayOfWeek2; Days2 < 7; Days2++)
                {
                    gvDays2.CurrentCell = gvDays2[Days2, 0];
                    gvDays2.CurrentCell.Value = TheDay2++;


                }
            }

            ////// Now load the rest of the Lines for the Calendar 
            for (int MyRows2 = 0; MyRows2 < 5; MyRows2++)
            {
                int NewRow2 = gvDays2.Rows.Add();
                gvDays2.Rows[NewRow2].Height = 20;
                for (int Days2 = 0; Days2 < 7; Days2++)
                {
                    gvDays2.CurrentCell = gvDays2[Days2, NewRow2];
                    if (TheDay2 <= DaysInMonth2)
                    {
                        gvDays2.CurrentCell.Value = TheDay2++;
                    }
                    else
                    {
                        gvDays2.CurrentCell.Value = "";
                        gvDays2.CurrentCell.Style.BackColor = Color.LightGray;
                    }
                }
            }


            // get esf a

  //          SELECT *
  //FROM [Mineware].[dbo].[BMCS_CALTYPE]
  //where calendartypeid = 'ESF A' and calendardate >= '2015-11-01'

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = "";
            _dbMan.SqlStatement = _dbMan.SqlStatement = "select DAY(CalendarDate) dd, * from [Mineware].[dbo].[BMCS_CALTYPE] " +
                                    "where calendartypeid =  'ESF A' " +
                                    "and MONTH(calendardate) = ' " + txtMonth.Value.ToString() + "' " +
                                    "and YEAR(calendardate) = '" + txtYear.Value.ToString() + "' and workingday = 'N' order by CalendarDate";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            DataTable dt = _dbMan.ResultsDataTable;

            foreach (DataRow r in dt.Rows)
            {
               
                for (int x = 0; x < 7; x++)
                {
                   for (int Line = 0; Line < gvDays.RowCount; Line++) 
                   {
                    if (gvDays.Rows[Line].Cells[x].Value != null)
                    {
                        if (r["dd"].ToString() == gvDays.Rows[Line].Cells[x].Value.ToString())
                        {
                            gvDays[x, Line].Style.BackColor = Color.Red;
                            
                        }

                        if (r["dd"].ToString() == "")
                        {
                            gvDays[x, Line].Style.BackColor = Color.Gray;

                        }
                       
                    }
                   }
                  
                }

            }



            MWDataManager.clsDataAccess _dbManB = new MWDataManager.clsDataAccess();
            _dbManB.ConnectionString = "";
            _dbManB.SqlStatement = _dbMan.SqlStatement = "select DAY(CalendarDate) dd, * from [Mineware].[dbo].[BMCS_CALTYPE] " +
                                    "where calendartypeid =  'ESF B' " +
                                    "and MONTH(calendardate) = ' " + txtMonth.Value.ToString() + "' " +
                                    "and YEAR(calendardate) = '" + txtYear.Value.ToString() + "' and workingday = 'N' order by CalendarDate";
            _dbManB.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManB.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManB.ExecuteInstruction();

            DataTable dtB = _dbManB.ResultsDataTable;

            foreach (DataRow r in dtB.Rows)
            {

                for (int x = 0; x < 7; x++)
                {
                    for (int Line = 0; Line < gvDays.RowCount; Line++)
                    {
                        if (gvDays2.Rows[Line].Cells[x].Value != null)
                        {
                            if (r["dd"].ToString() == gvDays2.Rows[Line].Cells[x].Value.ToString())
                            {
                                gvDays2[x, Line].Style.BackColor = Color.Red;

                            }

                            if (r["dd"].ToString() == "")
                            {
                                gvDays2[x, Line].Style.BackColor = Color.Gray;

                            }

                        }
                    }

                }

            }

        }

        private void LoadDailyColumns()
        {
            gvDays.ColumnCount = 0;
            

            DataGridViewColumn newColSun = new DataGridViewColumn();
            DataGridViewCell cellSun = new DataGridViewTextBoxCell();
            newColSun.CellTemplate = cellSun;
            newColSun.HeaderText = "Sun";
            newColSun.Name = "Sun";
            newColSun.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            newColSun.Visible = true;
            newColSun.Width = 35;
            newColSun.Frozen = true;
            newColSun.ReadOnly = true;
            newColSun.ReadOnly = true;
            newColSun.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gvDays.Columns.Add(newColSun);

            DataGridViewColumn newColMon = new DataGridViewColumn();
            DataGridViewCell cellMon = new DataGridViewTextBoxCell();
            newColMon.CellTemplate = cellMon;
            newColMon.HeaderText = "Mon";
            newColMon.Name = "Mon";
            newColMon.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            newColMon.Visible = true;
            newColMon.Width = 35;
            newColMon.Frozen = true;
            newColMon.ReadOnly = true;
            newColMon.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gvDays.Columns.Add(newColMon);

            DataGridViewColumn newColTue = new DataGridViewColumn();
            DataGridViewCell cellTue = new DataGridViewTextBoxCell();
            newColTue.CellTemplate = cellTue;
            newColTue.HeaderText = "Tue";
            newColTue.Name = "Tue";
            newColTue.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            newColTue.Visible = true;
            newColTue.Width = 35;
            newColTue.Frozen = true;
            newColTue.ReadOnly = true;
            newColTue.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            gvDays.Columns.Add(newColTue);



            DataGridViewColumn newColWed = new DataGridViewColumn();
            DataGridViewCell cellWed = new DataGridViewTextBoxCell();
            newColWed.CellTemplate = cellWed;
            newColWed.HeaderText = "Wed";
            newColWed.Name = "Wed";
            newColWed.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            newColWed.Visible = true;
            newColWed.Width = 35;
            newColWed.Frozen = true;
            newColWed.ReadOnly = true;
            newColWed.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



            gvDays.Columns.Add(newColWed);

            DataGridViewColumn newColThu = new DataGridViewColumn();
            DataGridViewCell cellThu = new DataGridViewTextBoxCell();
            newColThu.CellTemplate = cellThu;
            newColThu.HeaderText = "Thu";
            newColThu.Name = "Thu";
            newColThu.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            newColThu.Visible = true;
            newColThu.Width = 35;
            newColThu.Frozen = true;
            newColThu.ReadOnly = true;
            newColThu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gvDays.Columns.Add(newColThu);

            DataGridViewColumn newColFri = new DataGridViewColumn();
            DataGridViewCell cellFri = new DataGridViewTextBoxCell();
            newColFri.CellTemplate = cellFri;
            newColFri.HeaderText = "Fri";
            newColFri.Name = "Fri";
            newColFri.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            newColFri.Visible = true;
            newColFri.Width = 35;
            newColFri.Frozen = true;
            newColFri.ReadOnly = true;
            newColFri.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gvDays.Columns.Add(newColFri);

            DataGridViewColumn newColSat = new DataGridViewColumn();
            DataGridViewCell cellSat = new DataGridViewTextBoxCell();
            newColSat.CellTemplate = cellSat;
            newColSat.HeaderText = "Sat";
            newColSat.Name = "Sat";
            newColSat.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            newColSat.Visible = true;
            newColSat.Width = 35;
            //newColSat.Row. = 25;
            newColSat.Frozen = true;
            newColSat.ReadOnly = true;
            newColSat.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gvDays.Columns.Add(newColSat);

            /////2////

            gvDays2.ColumnCount = 0;

            DataGridViewColumn newColSun2 = new DataGridViewColumn();
            DataGridViewCell cellSun2 = new DataGridViewTextBoxCell();
            newColSun2.CellTemplate = cellSun2;
            newColSun2.HeaderText = "Sun";
            newColSun2.Name = "Sun";
            newColSun2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            newColSun2.Visible = true;
            newColSun2.Width = 35;
            newColSun2.Frozen = true;
            newColSun2.ReadOnly = true;
            newColSun2.ReadOnly = true;
            newColSun2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gvDays2.Columns.Add(newColSun2);

            DataGridViewColumn newColMon2 = new DataGridViewColumn();
            DataGridViewCell cellMon2 = new DataGridViewTextBoxCell();
            newColMon2.CellTemplate = cellMon2;
            newColMon2.HeaderText = "Mon";
            newColMon2.Name = "Mon";
            newColMon2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            newColMon2.Visible = true;
            newColMon2.Width = 35;
            newColMon2.Frozen = true;
            newColMon2.ReadOnly = true;
            newColMon2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gvDays2.Columns.Add(newColMon2);

            DataGridViewColumn newColTue2 = new DataGridViewColumn();
            DataGridViewCell cellTue2 = new DataGridViewTextBoxCell();
            newColTue2.CellTemplate = cellTue2;
            newColTue2.HeaderText = "Tue";
            newColTue2.Name = "Tue";
            newColTue2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            newColTue2.Visible = true;
            newColTue2.Width = 35;
            newColTue2.Frozen = true;
            newColTue2.ReadOnly = true;
            newColTue2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            gvDays2.Columns.Add(newColTue2);



            DataGridViewColumn newColWed2 = new DataGridViewColumn();
            DataGridViewCell cellWed2 = new DataGridViewTextBoxCell();
            newColWed2.CellTemplate = cellWed2;
            newColWed2.HeaderText = "Wed";
            newColWed2.Name = "Wed";
            newColWed2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            newColWed2.Visible = true;
            newColWed2.Width = 35;
            newColWed2.Frozen = true;
            newColWed2.ReadOnly = true;
            newColWed2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



            gvDays2.Columns.Add(newColWed2);

            DataGridViewColumn newColThu2 = new DataGridViewColumn();
            DataGridViewCell cellThu2 = new DataGridViewTextBoxCell();
            newColThu2.CellTemplate = cellThu;
            newColThu2.HeaderText = "Thu";
            newColThu2.Name = "Thu";
            newColThu2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            newColThu2.Visible = true;
            newColThu2.Width = 35;
            newColThu2.Frozen = true;
            newColThu2.ReadOnly = true;
            newColThu2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gvDays2.Columns.Add(newColThu2);

            DataGridViewColumn newColFri2 = new DataGridViewColumn();
            DataGridViewCell cellFri2 = new DataGridViewTextBoxCell();
            newColFri2.CellTemplate = cellFri2;
            newColFri2.HeaderText = "Fri";
            newColFri2.Name = "Fri";
            newColFri2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            newColFri2.Visible = true;
            newColFri2.Width = 35;
            newColFri2.Frozen = true;
            newColFri2.ReadOnly = true;
            newColFri2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gvDays2.Columns.Add(newColFri2);

            DataGridViewColumn newColSat2 = new DataGridViewColumn();
            DataGridViewCell cellSat2 = new DataGridViewTextBoxCell();
            newColSat2.CellTemplate = cellSat2;
            newColSat2.HeaderText = "Sat";
            newColSat2.Name = "Sat";
            newColSat2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            newColSat2.Visible = true;
            newColSat2.Width = 35;
            //newColSat.Row. = 25;
            newColSat2.Frozen = true;
            newColSat2.ReadOnly = true;
            newColSat2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gvDays2.Columns.Add(newColSat2);
        }

        private void TopPnl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _dbManNS = new MWDataManager.clsDataAccess();
            _dbManNS.ConnectionString = "";
            _dbManNS.SqlStatement = _dbManNS.SqlStatement + "  ";

            if (SelectGroup.SelectedIndex == 1)
            {
                

                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " delete from mineware.dbo.tbl_BCS_SBoss_DevResults  where prodmonth = '" + ProdMonthTxt.Text + "' ";

                for (int k = 0; k <= bandedGridView11.RowCount - 1; k++)
                {
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " ";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " insert into mineware.dbo.tbl_BCS_SBoss_DevResults Values(  ";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + ProdMonthTxt.Text + "',  ";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView11.GetRowCellValue(k, bandedGridView11.Columns[0]) + "', \r\n ";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView11.GetRowCellValue(k, bandedGridView11.Columns[16]) + "', \r\n ";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView11.GetRowCellValue(k, bandedGridView11.Columns[1]) + "',  \r\n";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView11.GetRowCellValue(k, bandedGridView11.Columns[14]) + "', \r\n ";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView11.GetRowCellValue(k, bandedGridView11.Columns[15]) + "', \r\n ";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView11.GetRowCellValue(k, bandedGridView11.Columns[2]) + "', \r\n ";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView11.GetRowCellValue(k, bandedGridView11.Columns[5]) + "', \r\n ";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + "'" + bandedGridView11.GetRowCellValue(k, bandedGridView11.Columns[11]) + "', \r\n ";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + "  '" + bandedGridView11.GetRowCellValue(k, bandedGridView11.Columns[3]) + "', \r\n ";

                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView11.GetRowCellValue(k, bandedGridView11.Columns[6]) + "', \r\n ";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + "  '" + bandedGridView11.GetRowCellValue(k, bandedGridView11.Columns[12]) + "' )  \r\n";






                }
                
            }
            else
            {

                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " delete from mineware.dbo.tbl_BCS_SBBonusCriteria  where prodmonth = '" + ProdMonthTxt.Text + "' ";

                for (int k = 0; k <= bandedGridView7.RowCount - 1; k++)
                {
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " ";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " insert into mineware.dbo.tbl_BCS_SBBonusCriteria Values(  ";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + ProdMonthTxt.Text + "',  ";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView7.GetRowCellValue(k, bandedGridView7.Columns[0]) + "', \r\n ";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView7.GetRowCellValue(k, bandedGridView7.Columns[1]) + "', \r\n ";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView7.GetRowCellValue(k, bandedGridView7.Columns[2]) + "',  \r\n";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView7.GetRowCellValue(k, bandedGridView7.Columns[3]) + "', \r\n ";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView7.GetRowCellValue(k, bandedGridView7.Columns[4]) + "', \r\n ";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView7.GetRowCellValue(k, bandedGridView7.Columns[5]) + "', \r\n ";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView7.GetRowCellValue(k, bandedGridView7.Columns[7]) + "') \r\n ";
                }


            }


                _dbManNS.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManNS.queryReturnType = MWDataManager.ReturnType.longNumber;
                _dbManNS.ExecuteInstruction();


            MessageBox.Show("Bonus Details was successfully transferred", "Transferred", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //LoadBaseGrid();
        }

        private void AddRowEngBtn_Click(object sender, EventArgs e)
        {
            simpleButton1_Click(null, null);

            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = "";
            _dbMan1.SqlStatement = " insert into mineware.dbo.tbl_BCS_Eng_SurfaceIncentiveDetail Values( '" + MonthTxt.Text.ToString() + "', 0, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null) \r\n" +
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

        private void MonthTxt_Click(object sender, EventArgs e)
        {
            Procedures procs = new Procedures();
            procs.ProdMonthCalc(Convert.ToInt32(MonthTxt.Text));
            MonthTxt.Text = Procedures.Prod.ToString();
            procs.ProdMonthVis(Convert.ToInt32(MonthTxt.Text));
            Month1Txt.Text = Procedures.Prod2;

            LoadEngFactors();
        }

        private void SBBonusItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            button6_Click(null, null);
        }

        private void navBarItem12_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //ucEngBonus EngForm = (ucEngBonus)IsBookingFormAlreadyOpen(typeof(ucEngBonus));
            //if (EngForm == null)
            //{
            //    EngForm = new ucEngBonus();
            //    //EngForm.Text = "Development Shift Boss Bonus";
            //    EngForm.Show();
            //}
            //else
            //{
            //    EngForm.WindowState = FormWindowState.Maximized;
            //    EngForm.Select();
            //}
        }

        private void LoadDataESFA_Click(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = "";
            _dbMan.SqlStatement = " delete from [NorthamPas].dbo.tbl_BCS_Eng_FactorNew1  where prodmonth = '" + MonthTxt.Text.ToString() + "'";


            _dbMan.SqlStatement = _dbMan.SqlStatement + " delete from [Mineware].[dbo].BMCS_CalShifts where yearmonth = '" + MonthTxt.Text.ToString() + "' and calendartypeid = 'Eng Total'";

            _dbMan.SqlStatement = _dbMan.SqlStatement + " insert into [Mineware].[dbo].BMCS_CalShifts values ('" + MonthTxt.Text.ToString() + "' , 'Eng Total', ";
            _dbMan.SqlStatement = _dbMan.SqlStatement + " '" + String.Format("{0:yyyy-MM-dd}", FromDate.Value) + "', '" + String.Format("{0:yyyy-MM-dd}", ToDate.Value) + "', ";
            _dbMan.SqlStatement = _dbMan.SqlStatement + " '" +ESFATxt.Text+ "', '" +ESFBTxt.Text+ "') ";


            _dbMan.SqlStatement = _dbMan.SqlStatement + " delete from [Mineware].[dbo].BMCS_CalShifts where yearmonth = '" + MonthTxt.Text.ToString() + "' and calendartypeid = 'Eng Plant Total'";

            _dbMan.SqlStatement = _dbMan.SqlStatement + " insert into [Mineware].[dbo].BMCS_CalShifts values ('" + MonthTxt.Text.ToString() + "' , 'Eng Plant Total', ";
            _dbMan.SqlStatement = _dbMan.SqlStatement + " '" + String.Format("{0:yyyy-MM-dd}", FromDate.Value) + "', '" + String.Format("{0:yyyy-MM-dd}", ToDate.Value) + "', ";
            _dbMan.SqlStatement = _dbMan.SqlStatement + " '" + ESFPlantATxt.Text + "', '" + ESFPlantBTxt.Text + "') ";
            
            
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();


             MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = "";
            _dbMan1.SqlStatement = " insert into [NorthamPas].dbo.tbl_BCS_Eng_FactorNew1 Values( '" + MonthTxt.Text.ToString() + "', '" + TonsHoistCallTxt.Text.ToString() + "', '" + TonsHoistActTxt.Text.ToString() + "', \r\n" +
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
            _dbManNS.ConnectionString = "";
            _dbManNS.SqlStatement = _dbManNS.SqlStatement + "  ";

            _dbManNS.SqlStatement = _dbManNS.SqlStatement + " ";

            for (int k = 0; k <= bandedGridView2.RowCount-1; k++)
            {
                if (bandedGridView2.GetRowCellValue(k, bandedGridView2.Columns[0]) != null)
                {
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " ";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " update [NorthamPas].dbo.tbl_BMCS_DesignationFact set factor = '" + bandedGridView2.GetRowCellValue(k, bandedGridView2.Columns[1]) + "', factorShaft  = '" + bandedGridView2.GetRowCellValue(k, bandedGridView2.Columns[2]) + "',   factorplant  = '" + bandedGridView2.GetRowCellValue(k, bandedGridView2.Columns[3]) + "' \r\n";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " where occupation = '" + bandedGridView2.GetRowCellValue(k, bandedGridView2.Columns[0]) + "' \r\n ";
                }
            }
            _dbManNS.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManNS.queryReturnType = MWDataManager.ReturnType.longNumber;
            _dbManNS.ExecuteInstruction();


            MWDataManager.clsDataAccess _dbManRel = new MWDataManager.clsDataAccess();
            _dbManRel.ConnectionString = "";
            _dbManRel.SqlStatement = " exec [NorthamPas].dbo.[sp_BMCS_GetRelieving] '" + MonthTxt.Text.ToString() + "' , '" + String.Format("{0:yyyy-MM-dd}", FromDate.Value) + "', '" + String.Format("{0:yyyy-MM-dd}", ToDate.Value) + "'";

            _dbManRel.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManRel.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManRel.ExecuteInstruction();


            MessageBox.Show("Factors were successfully saved", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LoadEngFactors();


        }

       

        private void Month1Txt_TextChanged(object sender, EventArgs e)
        {
            txtMonth.Value = Convert.ToInt32((MonthTxt.Value.ToString()).Substring(4, 2));
            txtYear.Value = Convert.ToInt32((MonthTxt.Value.ToString()).Substring(0, 4));
            txtMonth2.Value = Convert.ToInt32((MonthTxt.Value.ToString()).Substring(4,2));
            txtYear2.Value = Convert.ToInt32((MonthTxt.Value.ToString()).Substring(0, 4));
            LoadDailyColumns();
            LoadTheCalendar();
            LoadBaseGrid();


            MWDataManager.clsDataAccess _dbMan11aa = new MWDataManager.clsDataAccess();
            _dbMan11aa.ConnectionString = "";
            _dbMan11aa.SqlStatement = " ";
            _dbMan11aa.SqlStatement = _dbMan11aa.SqlStatement + "select * from [Mineware].[dbo].BMCS_CalShifts where yearmonth = '" + MonthTxt.Text.ToString() + "' and calendartypeid = 'Eng Plant Total' " +

                                    " ";
            _dbMan11aa.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan11aa.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan11aa.ExecuteInstruction();

            DataTable dtaa = _dbMan11aa.ResultsDataTable;

            ESFPlantATxt.Text = "0";
            ESFPlantBTxt.Text = "0";

            if (dtaa.Rows.Count > 0)
            {
                ESFPlantATxt.Text = dtaa.Rows[0]["TOTALSHIFTS"].ToString();
                ESFPlantBTxt.Text = dtaa.Rows[0]["TotalWorkingShifts"].ToString();

            }


            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = "";
            _dbMan.SqlStatement = " ";
            _dbMan.SqlStatement = _dbMan.SqlStatement + "select * from [Mineware].[dbo].BMCS_CalShifts where yearmonth = '" + MonthTxt.Text.ToString() + "' and calendartypeid = 'Eng Total' " +
                                    
                                    " ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            DataTable dt = _dbMan.ResultsDataTable;
            ESFATxt.Text = "0";
            ESFBTxt.Text = "0";

            if (dt.Rows.Count > 0)
            {
                FromDate.Value =    Convert.ToDateTime(dt.Rows[0]["begindate"].ToString());
                ToDate.Value = Convert.ToDateTime(dt.Rows[0]["enddate"].ToString());
                FromDate_CloseUp(null, null);
            }

        }

        private void ProdMonth1Txt_TextChanged(object sender, EventArgs e)
        {
            if (SelectGroup.SelectedIndex == 0)
              LoadMiningResultsStope();
            else
              LoadMiningResultsDev();
        }

        private void navBarItem6_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            pnlSBResults.Visible = false;
            MiningFactorsPnl.Visible = true;
            MiningFactorsPnl.Dock = DockStyle.Fill;
            //SelectGroup.SelectedIndex = 1;

            TramPnl.Visible = false;
            TramPnl.Visible = false;
            BasicIncTablePnl.Visible = false;           
            PeramPnl.Visible = false;
            pnlSBResults.Visible = false;


            LoadMiningFact();
        }

        void LoadMiningFact()
        {
            
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = "";
                _dbMan.SqlStatement = " select * from [NorthamPas].[dbo].[tbl_BMCS_SBBonusFactorNew]  where prodmonth = '" + ProdMonthTxt.Text.ToString() + "'";

                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ExecuteInstruction();



                DataTable dt = _dbMan.ResultsDataTable;

                if (dt.Rows.Count > 0)
                {
                    StopeLimitOfCall.Text = dt.Rows[0]["CallLimit"].ToString();
                    StopeSafetyInspec1.Text = dt.Rows[0]["SafetyFactor1"].ToString();
                    StopeSafetyInspec2.Text = dt.Rows[0]["SafetyFactor2"].ToString();
                    StopeSafetyInspec3.Text = dt.Rows[0]["SafetyFactor3"].ToString();
                    StopeSafetyInspec4.Text = dt.Rows[0]["SafetyFactor4"].ToString();
                    StopeRockEng1.Text = dt.Rows[0]["RockFactor1"].ToString();
                    StopeRockEng2.Text = dt.Rows[0]["RockFactor2"].ToString();
                    StopeRockEng3.Text = dt.Rows[0]["RockFactor3"].ToString();
                    StopeRockEng4.Text = dt.Rows[0]["RockFactor4"].ToString();
                    StopeReefTonsHoist1.Text = dt.Rows[0]["TonsFactor1"].ToString();
                    StopeReefTonsHoist2.Text = dt.Rows[0]["TonsFactor2"].ToString();
                    StopeReefTonsHoist3.Text = dt.Rows[0]["TonsFactor3"].ToString();
                    StopeReefTonsHoist4.Text = dt.Rows[0]["TonsFactor4"].ToString();
                    StopePercSwept1.Text = dt.Rows[0]["SweepsFactor1"].ToString();
                    StopePercSwept2.Text = dt.Rows[0]["SweepsFactor2"].ToString();
                    StopePercSwept3.Text = dt.Rows[0]["SweepsFactor3"].ToString();
                    StopePercSwept4.Text = dt.Rows[0]["SweepsFactor4"].ToString();
                    StopeDSFactor.Text = dt.Rows[0]["DSFactor"].ToString();
                    StopeNSFactor.Text = dt.Rows[0]["NSFactor"].ToString();
                    LTI0.Text = dt.Rows[0]["ZeroLti"].ToString();
                    LTI1.Text = dt.Rows[0]["OneLti"].ToString();
                    LTI2.Text = dt.Rows[0]["TwoLti"].ToString();
                    LTI3.Text = dt.Rows[0]["ThreeLti"].ToString();
                    AWOP0.Text = dt.Rows[0]["ZeroAwop"].ToString();
                    AWOP1.Text = dt.Rows[0]["OneAwop"].ToString();
                    AWOP2.Text = dt.Rows[0]["TwoAwop"].ToString();
                    AWOP3.Text = dt.Rows[0]["ThreeAwop"].ToString();
                    DevLatDev1.Text = dt.Rows[0]["LateralFactor1"].ToString();
                    DevLatDev2.Text = dt.Rows[0]["LateralFactor2"].ToString();
                    DevLatDev3.Text = dt.Rows[0]["LateralFactor3"].ToString();
                    DevLatDev4.Text = dt.Rows[0]["LateralFactor4"].ToString();
                    DevMerRaise1.Text = dt.Rows[0]["RaiseFactor1"].ToString();
                    DevMerRaise2.Text = dt.Rows[0]["RaiseFactor2"].ToString();
                    DevMerRaise3.Text = dt.Rows[0]["RaiseFactor3"].ToString();
                    DevMerRaise4.Text = dt.Rows[0]["RaiseFactor4"].ToString();
                    //skip4sql
                    DevSafetyInsp1.Text = dt.Rows[0]["DevSafetyFactor1"].ToString();
                    DevSafetyInsp2.Text = dt.Rows[0]["DevSafetyFactor2"].ToString();
                    DevSafetyInsp3.Text = dt.Rows[0]["DevSafetyFactor3"].ToString();
                    DevSafetyInsp4.Text = dt.Rows[0]["DevSafetyFactor4"].ToString();
                    DevRockEng1.Text = dt.Rows[0]["DevRockFactor1"].ToString();
                    DevRockEng2.Text = dt.Rows[0]["DevRockFactor2"].ToString();
                    DevRockEng3.Text = dt.Rows[0]["DevRockFactor3"].ToString();
                    DevRockEng4.Text = dt.Rows[0]["DevRockFactor4"].ToString();
                    DevReefTonsHoist1.Text = dt.Rows[0]["DevTonsFactor1"].ToString();
                    DevReefTonsHoist2.Text = dt.Rows[0]["DevTonsFactor2"].ToString();
                    DevReefTonsHoist3.Text = dt.Rows[0]["DevTonsFactor3"].ToString();
                    DevReefTonsHoist4.Text = dt.Rows[0]["DevTonsFactor4"].ToString();
                    DevLimitCall.Text = dt.Rows[0]["DevCallLimit"].ToString();
                    DevDSFact.Text = dt.Rows[0]["DevDSFactor"].ToString();
                    DevNSFact.Text = dt.Rows[0]["DevNSFactor"].ToString();



                }         




           
        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void textEdit5_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void MiningFactorsPnl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SaveFactorsBtn_Click(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = "";
            _dbMan.SqlStatement = " delete from [NorthamPas].[dbo].[tbl_BMCS_SBBonusFactorNew]  where prodmonth = '" + ProdMonthTxt.Text.ToString() + "'";

            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();


            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = "";
            _dbMan1.SqlStatement = " insert into [NorthamPas].[dbo].[tbl_BMCS_SBBonusFactorNew] Values( '" + ProdMonthTxt.Text.ToString() + "', '" + StopeLimitOfCall.Text.ToString() + "', '" + StopeSafetyInspec1.Text.ToString() + "', '" + StopeSafetyInspec2.Text.ToString() + "', '" + StopeSafetyInspec3.Text.ToString() + "', '" + StopeSafetyInspec4.Text.ToString() + "', \r\n" +
                                   " '" + StopeRockEng1.Text.ToString() + "', '" + StopeRockEng2.Text.ToString() + "', '" + StopeRockEng3.Text.ToString() + "', '" + StopeRockEng4.Text.ToString() + "', \r\n" +
                                   " '" + StopeReefTonsHoist1.Text.ToString() + "', '" + StopeReefTonsHoist2.Text.ToString() + "', '" + StopeReefTonsHoist3.Text.ToString() + "', '" + StopeReefTonsHoist4.Text.ToString() + "',  \r\n" +
                                   " '" + StopePercSwept1.Text.ToString() + "', '" + StopePercSwept2.Text.ToString() + "', '" + StopePercSwept3.Text.ToString() + "', '" + StopePercSwept4.Text.ToString() + "',  \r\n" +
                                   " '" + StopeDSFactor.Text.ToString() + "',  '" + StopeNSFactor.Text.ToString() + "',   \r\n" +
                                   " '" + LTI0.Text.ToString() + "', '" + LTI1.Text.ToString() + "', '" + LTI2.Text.ToString() + "', '" + LTI3.Text.ToString() + "',  \r\n" +
                                   " '" + AWOP0.Text.ToString() + "', '" + AWOP1.Text.ToString() + "', '" + AWOP2.Text.ToString() + "', '" + AWOP3.Text.ToString() + "', \r\n" +
                                   " '" + DevLatDev1.Text.ToString() + "', '" + DevLatDev2.Text.ToString() + "',  '" + DevLatDev3.Text.ToString() + "',  '" + DevLatDev4.Text.ToString() + "',   \r\n" +
                                   " '" + DevMerRaise1.Text.ToString() + "', '" + DevMerRaise2.Text.ToString() + "', '" + DevMerRaise3.Text.ToString() + "', '" + DevMerRaise4.Text.ToString() + "', \r\n" +
                /////TotalDevFactor1,TotalDevFactor2,TotalDevFactor3,TotalDevFactor4
                                   " 0,0,0,0, \r\n" +
                                   " '" + DevSafetyInsp1.Text.ToString() + "', '" + DevSafetyInsp2.Text.ToString() + "', '" + DevSafetyInsp3.Text.ToString() + "', '" + DevSafetyInsp4.Text.ToString() + "', \r\n" +
                                   " '" + DevRockEng1.Text.ToString() + "', '" + DevRockEng2.Text.ToString() + "', '" + DevRockEng3.Text.ToString() + "', '" + DevRockEng4.Text.ToString() + "', \r\n" +
                                   " '" + DevReefTonsHoist1.Text.ToString() + "', '" + DevReefTonsHoist2.Text.ToString() + "', '" + DevReefTonsHoist3.Text.ToString() + "', '" + DevReefTonsHoist4.Text.ToString() + "', \r\n" +
                                   " '" + DevLimitCall.Text.ToString() + "', '" + DevDSFact.Text.ToString() + "', '" + DevNSFact.Text.ToString() + "' ) \r\n" +
                                   "  \r\n";

            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();

            MessageBox.Show("Factors were successfully saved", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            

            LoadMiningFact();
        }

        private void FromDate_CloseUp(object sender, EventArgs e)
        {
             MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = "";
            _dbMan1.SqlStatement = " select sum(a) ESFA1, sum(b) ESFB1 from( select "+
                                   " case when Calendartypeid = 'ESF A' and workingday = 'Y' then 1 else 0 end as a, "+
                                   " case when Calendartypeid = 'ESF B' and workingday = 'Y' then 1 else 0 end as b "+

                                   "  from [Mineware].[dbo].[BMCS_CALTYPE] " +
                                   " where calendardate >= '" + String.Format("{0:yyyy-MM-dd}", FromDate.Value) + "'  and calendardate <= '" + String.Format("{0:yyyy-MM-dd}", ToDate.Value) + "') a ";

            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();
            DataTable dt = _dbMan1.ResultsDataTable;

            if (dt.Rows.Count > 0)
            {
                ESFATxt.Text = dt.Rows[0]["ESFA1"].ToString();
                ESFBTxt.Text = dt.Rows[0]["ESFB1"].ToString();


                if (ESFPlantATxt.Text == "0")
                {
                    ESFPlantATxt.Text = dt.Rows[0]["ESFA1"].ToString();
                    ESFPlantBTxt.Text = dt.Rows[0]["ESFB1"].ToString();
                }
            }



        }

        private void ToDate_CloseUp(object sender, EventArgs e)
        {
            FromDate_CloseUp(null, null);
        }

        private void navBarControl1_Click(object sender, EventArgs e)
        {

        }

        private void navBarItem7_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //frmSBBonus SBForm = (frmSBBonus)IsBookingFormAlreadyOpen(typeof(frmSBBonus));
            //if (SBForm == null)
            //{
            //    SBForm = new frmSBBonus();
            //    SBForm.Text = "Stoping Shift Boss Bonus";
            //    SBForm.Show();
            //}
            //else
            //{
            //    SBForm.WindowState = FormWindowState.Maximized;
            //    SBForm.Select();
            //}
        }

        private void navBarItem7_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            pnlTestGangMapping.Visible = false;

            //frmStpCrewBonus RepFrm = (frmStpCrewBonus)IsBookingFormAlreadyOpen(typeof(frmStpCrewBonus));
            //if (RepFrm == null)
            //{
            //    RepFrm = new frmStpCrewBonus();
            //    RepFrm.Text = "Stope Crew Bonus";
            //    RepFrm.Show();
            //}
            //else
            //{
            //    RepFrm.WindowState = FormWindowState.Maximized;
            //    RepFrm.Select();
            //}
        }

        private void btnTramCapt_Click(object sender, EventArgs e)
        {

        }

        private void navBarItem13_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            pnlTestGangMapping.Visible = false;

            frmCat2Capt RepFrm = (frmCat2Capt)IsBookingFormAlreadyOpen(typeof(frmCat2Capt));
            if (RepFrm == null)
            {
                RepFrm = new frmCat2Capt();
                RepFrm.Text = "Cat 2 to 8 Capture";
                RepFrm.Show();
            }
            else
            {
                RepFrm.WindowState = FormWindowState.Maximized;
                RepFrm.Select();
            }
        }

        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            HidePnls();
            PnlMingParam.Visible = false;
            TrammingPnl.Visible = false;
            EngPnl.Visible = false;
            UsersPnl.Visible = true;
            UsersPnl.Dock = DockStyle.Fill;
            gridControl1.Dock = DockStyle.Fill;
            PeramPnl.Visible = false;


            LoadUsersGrid();
            

        }

        public void LoadUsersGrid()
        {

            MWDataManager.clsDataAccess _dbManUsers = new MWDataManager.clsDataAccess();
            _dbManUsers.ConnectionString = "";
            _dbManUsers.SqlStatement = "  select * from mineware.dbo.tbl_BCS_Users" +
                                       " ";

            _dbManUsers.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManUsers.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManUsers.ExecuteInstruction();

           
            DataTable dt = _dbManUsers.ResultsDataTable;

            
            DataSet ds = new DataSet();

            //if (ds.Tables.Count > 0)
            //    ds.Tables.Clear();

            ds.Tables.Add(dt);

            gridControl1.DataSource = ds.Tables[0];

            col1.FieldName = "Username";
            col2.FieldName = "name";
            col3.FieldName = "expiryDate";
            col4.FieldName = "MoSec";
            col5.FieldName = "safety";
            col6.FieldName = "shiftCapt";
            col7.FieldName = "MonthParamEng";
            col8.FieldName = "Mining";
            col9.FieldName = "MiningCrewBonus";
            col10.FieldName = "SBBonusCalc";
            col11.FieldName = "BonusEng";
            col12.FieldName = "Users";
            Col13.FieldName = "General";
            col14.FieldName = "TrammingCapt";
            col15.FieldName = "ProductionCapt";

            //bandedGridView3.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            frmProp frmProp = (frmProp)IsBookingFormAlreadyOpen(typeof(frmProp));
            if (frmProp == null)
            {
                frmProp = new frmProp();
                //frmProp.UserLbl.Text = UserLbl.Text;
                frmProp.Text = "Setup Users";
                frmProp.Show();
            }
            else
            {
                frmProp.WindowState = FormWindowState.Maximized;
                frmProp.Select();
            }
        }

        private void bandedGridView3_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            UserLbl.Text = bandedGridView3.GetRowCellValue(e.RowHandle, bandedGridView3.Columns[0]).ToString();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {

            frmProp frmProp = (frmProp)IsBookingFormAlreadyOpen(typeof(frmProp));
            if (frmProp == null)
            {
                frmProp = new frmProp();
                frmProp.UserLbl.Text = UserLbl.Text;
                frmProp.EditLbl.Text = "Y";
                frmProp.Text = "Setup Users";
                frmProp.Show();
            }
            else
            {
                frmProp.WindowState = FormWindowState.Maximized;
                frmProp.Select();
            }
            
        }

        private void navBarItem14_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

            HidePnls();
            PnlMingParam.Visible = false;
            TrammingPnl.Visible = false;
            EngPnl.Visible = false;
            GenPnl.Visible = true;
            GenPnl.Dock = DockStyle.Fill;
            //gridControl1.Dock = DockStyle.Fill;
            PeramPnl.Visible = false;

        }

        private void navBarItem15_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AbsentPnl.Visible = true;
            //AbsentPnl.Dock = DockStyle.Fill;



            LoadRow();
            LoadAbsentGrid();
        }

        public void LoadRow()
        {
            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = "";
            _dbMan1.SqlStatement = " insert into mineware.dbo.tbl_BCS_AbsenteeismFactors Values( '" + PMnthTxt.Text.ToString() + "', 0,0) \r\n" +
                                   "  \r\n" +
                                   "  \r\n" +
                                   "  \r\n" +
                                   "  \r\n" +
                                   "  \r\n" +
                                   "  \r\n";

            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();
        }

        public void LoadAbsentGrid()
        {
            


            MWDataManager.clsDataAccess _dbManUsers = new MWDataManager.clsDataAccess();
            _dbManUsers.ConnectionString = "";
            _dbManUsers.SqlStatement = "  select * from mineware.dbo.tbl_BCS_AbsenteeismFactors" +
                                       " where prodmonth = '"+ PMnthTxt.Text +"' ";

            _dbManUsers.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManUsers.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManUsers.ExecuteInstruction();


            DataTable dt = _dbManUsers.ResultsDataTable;


            DataSet ds = new DataSet();

            //if (ds.Tables.Count > 0)
            //    ds.Tables.Clear();

            ds.Tables.Add(dt);

            gridControl2.DataSource = ds.Tables[0];

            Column1.FieldName = "ShiftNo";
            Column2.FieldName = "Factor";
            
            bandedGridView4.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            //LoadAbsentGrid();




        }

        private void PMnthTxt_Click(object sender, EventArgs e)
        {
            Procedures procs = new Procedures();
            procs.ProdMonthCalc(Convert.ToInt32(PMnthTxt.Text));
            ProdMonthTxt.Text = Procedures.Prod.ToString();
            procs.ProdMonthVis(Convert.ToInt32(PMnthTxt.Text));
            PMnthTxt1.Text = Procedures.Prod2;

            LoadAbsentGrid();
        }

        private void TransferDataBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void button13_Click(object sender, EventArgs e)
        {
            

            //LoadAbsentGrid();
        }

        void LoadDataDev()
        {
            string @table = "";
            string @Collumn = "";
            string @Collumn2 = "";
            string @Collumn3 = "";

            if (RaiseCbx.Checked == true)
            {
                @table = "BMCS_BIPDevRaises";
                @Collumn = "BIPDevRaisesAvgEmp";
                @Collumn2 = "BIPDevRaisesSqm";
                @Collumn3 = "BIPDevRaisesAmount";

                //RaiseCbx.Checked = true;
                //LatDevCbx.Checked = false;
                //WaterEndsCbx.Checked = false;
                //BHCbx.Checked = false;
                //ChairliftCbx.Checked = false;
 
            }

            if (LatDevCbx.Checked == true)
            {
                @table = "tbl_BCS_BIPDevLateral";
                @Collumn = "BIPDevLateralAvgEmp";
                @Collumn2 = "BIPDevLateralSqm";
                @Collumn3 = "BIPDevLateralAmount";

                //LatDevCbx.Checked = true;
                //RaiseCbx.Checked = false;
                //WaterEndsCbx.Checked = false;
                //BHCbx.Checked = false;
                //ChairliftCbx.Checked = false;
            }

            if (WaterEndsCbx.Checked == true)
            {
                @table = "tbl_BCS_BIPDevLateralWaterEnds";
                @Collumn = "BIPDevLateralAvgEmp";
                @Collumn2 = "BIPDevLateralSqm";
                @Collumn3 = "BIPDevLateralAmount";

                //WaterEndsCbx.Checked = true;
                //LatDevCbx.Checked = false;
                //RaiseCbx.Checked = false;
                //BHCbx.Checked = false;
                //ChairliftCbx.Checked = false;
            }

            if (BHCbx.Checked == true)
            {
                @table = "tbl_BCS_BIPDevBoxHole";
                @Collumn = "BIPDevBHAvgEmp";
                @Collumn2 = "BIPDevBHSqm";
                @Collumn3 = "BIPDevBHAmount";

                //BHCbx.Checked = true;
                //LatDevCbx.Checked = false;
                //RaiseCbx.Checked = false;
                //WaterEndsCbx.Checked = false;
                //ChairliftCbx.Checked = false;
            }

            if (ChairliftCbx.Checked == true)
            {
                @table = "BMCS_BIPDevChairlift";
                @Collumn = "ID";
                @Collumn2 = "Percent";
                @Collumn3 = "Amount";

                //ChairliftCbx.Checked = true;
                //LatDevCbx.Checked = false;
                //RaiseCbx.Checked = false;
                //WaterEndsCbx.Checked = false;
                //BHCbx.Checked = false;

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = "";
                _dbMan1.SqlStatement = " select * from " + @table + " where prodmonth = '" + ProdMonthTxt.Text + "'  order by " + @Collumn + "  ";
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ExecuteInstruction();

                 DataTable dt3 = _dbMan1.ResultsDataTable;
                 DataSet ds1 = new DataSet();

                 ds1.Tables.Add(dt3);

                 gridControl3.DataSource = ds1.Tables[0];

                 if (dt3.Rows.Count > 0)
                 {
                     BIP1.Caption = "ID";
                     BIP2.Caption = "Percent";
                     BIP3.Caption = "Amount";

                    
                     BIP4.Visible = false;
                     BIP5.Visible = false;
                     BIP6.Visible = false;
                     BIP7.Visible = false;
                     BIP8.Visible = false;
                     BIP9.Visible = false;
                     BIP10.Visible = false;
                     BIP11.Visible = false;
                     BIP12.Visible = false;
                     BIP13.Visible = false;
                     BIP14.Visible = false;
                     BIP15.Visible = false;
                     BIP16.Visible = false;
                     BIP17.Visible = false;
                     BIP18.Visible = false;
                     BIP19.Visible = false;
                     BIP20.Visible = false;

                     BIP1.FieldName = @Collumn;
                     BIP2.FieldName = @Collumn2;
                     BIP3.FieldName = @Collumn3;
                 }
            }

            if (@table != "" && ChairliftCbx.Checked != true)
            {

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = "";
                _dbMan1.SqlStatement = " select " + @Collumn + " from " + @table + " where prodmonth = '" + ProdMonthTxt.Text + "'  group by " + @Collumn + "  order by " + @Collumn + "  ";
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ExecuteInstruction();

                DataTable dt2 = _dbMan1.ResultsDataTable;

                if (dt2.Rows.Count > 0)
                {

                    BIP1.Caption = "m2";


                    BIP2.Visible = false;
                    BIP3.Visible = false;
                    BIP4.Visible = false;
                    BIP5.Visible = false;
                    BIP6.Visible = false;
                    BIP7.Visible = false;
                    BIP8.Visible = false;
                    BIP9.Visible = false;
                    BIP10.Visible = false;
                    BIP11.Visible = false;
                    BIP12.Visible = false;
                    BIP13.Visible = false;
                    BIP14.Visible = false;
                    BIP15.Visible = false;
                    BIP16.Visible = false;
                    BIP17.Visible = false;
                    BIP18.Visible = false;
                    BIP19.Visible = false;
                    BIP20.Visible = false;

                    for (int k = 0; k <= dt2.Rows.Count - 1; k++)
                    {
                        if (k == 0)
                        {
                            BIP2.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP2.Visible = true;
                        }
                        if (k == 1)
                        {
                            BIP3.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP3.Visible = true;
                        }
                        if (k == 2)
                        {
                            BIP4.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP4.Visible = true;
                        }
                        if (k == 3)
                        {
                            BIP5.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP5.Visible = true;
                        }
                        if (k == 4)
                        {
                            BIP6.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP6.Visible = true;
                        }
                        if (k == 5)
                        {
                            BIP7.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP7.Visible = true;
                        }
                        if (k == 6)
                        {
                            BIP8.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP8.Visible = true;
                        }
                        if (k == 7)
                        {
                            BIP9.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP9.Visible = true;
                        }
                        if (k == 8)
                        {
                            BIP10.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP10.Visible = true;
                        }
                        if (k == 9)
                        {
                            BIP11.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP11.Visible = true;
                        }
                        if (k == 10)
                        {
                            BIP12.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP12.Visible = true;
                        }
                        if (k == 11)
                        {
                            BIP13.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP13.Visible = true;
                        }
                        if (k == 12)
                        {
                            BIP14.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP14.Visible = true;
                        }
                        if (k == 13)
                        {
                            BIP15.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP15.Visible = true;
                        }
                        if (k == 14)
                        {
                            BIP16.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP16.Visible = true;
                        }
                        if (k == 15)
                        {
                            BIP17.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP17.Visible = true;
                        }
                        if (k == 16)
                        {
                            BIP18.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP18.Visible = true;
                        }
                        if (k == 17)
                        {
                            BIP19.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP19.Visible = true;
                        }
                        if (k == 18)
                        {
                            BIP20.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP20.Visible = true;
                        }

                    }




                }

                MWDataManager.clsDataAccess _dbManUsers = new MWDataManager.clsDataAccess();
                _dbManUsers.ConnectionString = "";
                _dbManUsers.SqlStatement = "   " +
                                            " declare @pm varchar(50) \r\n" +

                                            " declare @Bip1 varchar(50) \r\n" +
                                            " declare @Bip2 varchar(50) \r\n" +
                                            " declare @Bip3 varchar(50) \r\n" +
                                            " declare @Bip4 varchar(50) \r\n" +
                                            " declare @Bip5 varchar(50) \r\n" +
                                            " declare @Bip6 varchar(50) \r\n" +
                                            " declare @Bip7 varchar(50) \r\n" +
                                            " declare @Bip8 varchar(50) \r\n" +
                                            " declare @Bip9 varchar(50) \r\n" +
                                            " declare @Bip10 varchar(50) \r\n" +
                                            " declare @Bip11 varchar(50) \r\n" +
                                            " declare @Bip12 varchar(50) \r\n" +
                                            " declare @Bip13 varchar(50) \r\n" +
                                            " declare @Bip14 varchar(50) \r\n" +
                                            " declare @Bip15 varchar(50) \r\n" +
                                            " declare @Bip16 varchar(50) \r\n" +
                                            " declare @Bip17 varchar(50) \r\n" +
                                            " declare @Bip18 varchar(50) \r\n" +
                                            " declare @Bip19 varchar(50) \r\n" +
                                            " declare @Bip20 varchar(50) \r\n" +



                                            " set @pm = '" + ProdMonthTxt.Text + "' \r\n" +


                                            " set @Bip1 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm) \r\n" +
                                            " set @Bip2 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip1) \r\n" +
                                            " set @Bip3 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip2) \r\n" +
                                            " set @Bip4 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip3) \r\n" +
                                            " set @Bip5 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip4) \r\n" +
                                            " set @Bip6 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip5) \r\n" +
                                            " set @Bip7 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip6) \r\n" +
                                            " set @Bip8 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip7) \r\n" +
                                            " set @Bip9 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip8) \r\n" +
                                            " set @Bip10 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip9) \r\n" +
                                            " set @Bip11 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip10) \r\n" +
                                            " set @Bip12 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip11) \r\n" +
                                            " set @Bip13 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip12) \r\n" +
                                            " set @Bip14 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip13) \r\n" +
                                            " set @Bip15 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip14) \r\n" +
                                            " set @Bip16 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip15) \r\n" +
                                            " set @Bip17 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip16) \r\n" +
                                            " set @Bip18 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip17) \r\n" +
                                            " set @Bip19 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip18) \r\n" +
                                            " set @Bip20 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip19) \r\n" +

                                            " select a." + @Collumn2 + ", a." + @Collumn3 + " aa1, b." + @Collumn3 + " aa2, c." + @Collumn3 + " aa3, d." + @Collumn3 + " aa4, e." + @Collumn3 + " aa5 \r\n" +
                                            " , f." + @Collumn3 + " aa6, g." + @Collumn3 + " aa7, h." + @Collumn3 + " aa8, i." + @Collumn3 + " aa9, j." + @Collumn3 + " aa10, k." + @Collumn3 + " aa11 \r\n" +
                                            " , l." + @Collumn3 + " aa12, m." + @Collumn3 + " aa13, n." + @Collumn3 + " aa14, o." + @Collumn3 + " aa15, p." + @Collumn3 + " aa16, q." + @Collumn3 + " aa17 \r\n" +
                                            " , r." + @Collumn3 + " aa18, s." + @Collumn3 + " aa19, t." + @Collumn3 + " aa20 \r\n" +

                                            " from ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip1) a \r\n" +
                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip2) b on a." + @Collumn2 + "  = b." + @Collumn2 + "  \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip3) c on a." + @Collumn2 + "  = c." + @Collumn2 + "  \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip4) d on a." + @Collumn2 + "  = d." + @Collumn2 + "  \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip5) e on a." + @Collumn2 + "  = e." + @Collumn2 + "  \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip6) f on a." + @Collumn2 + "  = f." + @Collumn2 + "  \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip7) g on a." + @Collumn2 + "  = g." + @Collumn2 + "  \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + " \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip8) h on a." + @Collumn2 + "  = h." + @Collumn2 + "  \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip9) i on a." + @Collumn2 + "  = i." + @Collumn2 + "  \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip10) j on a." + @Collumn2 + "  = j." + @Collumn2 + "  \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip11) k on a." + @Collumn2 + "  = k." + @Collumn2 + "  \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip12) l on a." + @Collumn2 + "  = l." + @Collumn2 + "  \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip13) m on a." + @Collumn2 + "  = m." + @Collumn2 + "  \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip14) n on a." + @Collumn2 + "  = n." + @Collumn2 + "  \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip15) o on a." + @Collumn2 + "  = o." + @Collumn2 + "  \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip16) p on a." + @Collumn2 + "  = p." + @Collumn2 + "  \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip17) q on a." + @Collumn2 + "  = q." + @Collumn2 + "  \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip18) r on a." + @Collumn2 + "  = r." + @Collumn2 + "  \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip19) s on a." + @Collumn2 + "  = s." + @Collumn2 + "  \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip20) t on a." + @Collumn2 + "  = t." + @Collumn2 + "  \r\n" +
                                           "  ";

                _dbManUsers.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManUsers.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManUsers.ExecuteInstruction();

                DataTable dt = _dbManUsers.ResultsDataTable;


                DataSet ds = new DataSet();

                ds.Tables.Add(dt);

                gridControl3.DataSource = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    BIP1.FieldName = @Collumn2;
                    BIP2.FieldName = "aa1";
                    BIP3.FieldName = "aa2";
                    BIP4.FieldName = "aa3";
                    BIP5.FieldName = "aa4";
                    BIP6.FieldName = "aa5";
                    BIP7.FieldName = "aa6";
                    BIP8.FieldName = "aa7";
                    BIP9.FieldName = "aa8";
                    BIP10.FieldName = "aa9";
                    BIP11.FieldName = "aa10";
                    BIP12.FieldName = "aa11";
                    BIP13.FieldName = "aa12";
                    BIP14.FieldName = "aa13";
                    BIP15.FieldName = "aa14";
                    BIP16.FieldName = "aa15";
                    BIP17.FieldName = "aa16";
                    BIP18.FieldName = "aa17";
                    BIP19.FieldName = "aa18";
                    BIP20.FieldName = "aa19";


                    bandedGridView5.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

                }
            }
            
        }

        void LoadDataStoping()
        {
            ////Load Headers Grid/////

            string @table = "BMCS_BIPStoping";
            string @Collumn = "BIPStopingAvgEmp";

            if (MerCbx.Checked == true)
            {
                @table = "BMCS_BIPStopingMer";

                //MerCbx.Checked = true;
                //UnitSweepCbx.Checked = false;
                //RCrewsCbx.Checked = false;
                //SweepCbx.Checked = false;
            }

            if (UnitSweepCbx.Checked == true)
            {
                @table = "BMCS_BIPStopingSW";

                //UnitSweepCbx.Checked = true;
                //RCrewsCbx.Checked = false;
                //SweepCbx.Checked = false;
                //MerCbx.Checked = false;
                
            }

            if (RCrewsCbx.Checked == true)
            {
                @table = "BMCS_BIPStopingRCrews";

                //RCrewsCbx.Checked = true;
                //UnitSweepCbx.Checked = false;
                //SweepCbx.Checked = false;
                //MerCbx.Checked = false;
            }

            if (SweepCbx.Checked == true)
            {
                @table = "BMCS_BIPSweepings";
                @Collumn = "BIPSweepingsAvgEmp";

                //SweepCbx.Checked = true;
                //UnitSweepCbx.Checked = false;
                //RCrewsCbx.Checked = false;
                //MerCbx.Checked = false;

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = "";
                _dbMan1.SqlStatement = " select " + @Collumn + " from " + @table + " where prodmonth = '" + ProdMonthTxt.Text + "'  group by " + @Collumn + "  order by " + @Collumn + "  ";
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ExecuteInstruction();

                DataTable dt2 = _dbMan1.ResultsDataTable;

                if (dt2.Rows.Count > 0)
                {

                    BIP1.Caption = "m2";
                    BIP2.Caption = "From";
                    BIP3.Caption = "To";

                    BIP4.Visible = false;
                    BIP5.Visible = false;
                    BIP6.Visible = false;
                    BIP7.Visible = false;
                    BIP8.Visible = false;
                    BIP9.Visible = false;
                    BIP10.Visible = false;
                    BIP11.Visible = false;
                    BIP12.Visible = false;
                    BIP13.Visible = false;
                    BIP14.Visible = false;
                    BIP15.Visible = false;
                    BIP16.Visible = false;
                    BIP17.Visible = false;
                    BIP18.Visible = false;
                    BIP19.Visible = false;
                    BIP20.Visible = false;

                    for (int k = 0; k <= dt2.Rows.Count - 1; k++)
                    {

                        if (k == 0)
                        {
                            BIP4.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP4.Visible = true;
                        }
                        if (k == 1)
                        {
                            BIP5.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP5.Visible = true;
                        }
                        if (k == 2)
                        {
                            BIP6.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP6.Visible = true;
                        }
                        if (k == 3)
                        {
                            BIP7.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP7.Visible = true;
                        }
                        if (k == 4)
                        {
                            BIP8.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP8.Visible = true;
                        }
                        if (k == 5)
                        {
                            BIP9.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP9.Visible = true;
                        }
                        if (k == 6)
                        {
                            BIP10.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP10.Visible = true;
                        }
                        if (k == 7)
                        {
                            BIP11.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP11.Visible = true;
                        }
                        if (k == 8)
                        {
                            BIP12.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP12.Visible = true;
                        }
                        if (k == 9)
                        {
                            BIP13.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP13.Visible = true;
                        }
                        if (k == 10)
                        {
                            BIP14.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP14.Visible = true;
                        }
                        if (k == 11)
                        {
                            BIP15.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP15.Visible = true;
                        }
                        if (k == 12)
                        {
                            BIP16.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP16.Visible = true;
                        }
                        if (k == 13)
                        {
                            BIP17.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP17.Visible = true;
                        }
                        if (k == 14)
                        {
                            BIP18.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP18.Visible = true;
                        }
                        if (k == 15)
                        {
                            BIP19.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP19.Visible = true;
                        }
                        if (k == 16)
                        {
                            BIP20.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP20.Visible = true;
                        }

                    }




                }

                MWDataManager.clsDataAccess _dbManUsers = new MWDataManager.clsDataAccess();
                _dbManUsers.ConnectionString = "";
                _dbManUsers.SqlStatement = "   " +
                                            " declare @pm varchar(50) \r\n" +

                                            " declare @Bip1 varchar(50) \r\n" +
                                            " declare @Bip2 varchar(50) \r\n" +
                                            " declare @Bip3 varchar(50) \r\n" +
                                            " declare @Bip4 varchar(50) \r\n" +
                                            " declare @Bip5 varchar(50) \r\n" +
                                            " declare @Bip6 varchar(50) \r\n" +
                                            " declare @Bip7 varchar(50) \r\n" +
                                            " declare @Bip8 varchar(50) \r\n" +
                                            " declare @Bip9 varchar(50) \r\n" +
                                            " declare @Bip10 varchar(50) \r\n" +
                                            " declare @Bip11 varchar(50) \r\n" +
                                            " declare @Bip12 varchar(50) \r\n" +
                                            " declare @Bip13 varchar(50) \r\n" +
                                            " declare @Bip14 varchar(50) \r\n" +
                                            " declare @Bip15 varchar(50) \r\n" +
                                            " declare @Bip16 varchar(50) \r\n" +
                                            " declare @Bip17 varchar(50) \r\n" +
                                            " declare @Bip18 varchar(50) \r\n" +
                                            " declare @Bip19 varchar(50) \r\n" +
                                            " declare @Bip20 varchar(50) \r\n" +



                                            " set @pm = '" + ProdMonthTxt.Text + "' \r\n" +


                                            " set @Bip1 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm) \r\n" +
                                            " set @Bip2 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip1) \r\n" +
                                            " set @Bip3 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip2) \r\n" +
                                            " set @Bip4 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip3) \r\n" +
                                            " set @Bip5 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip4) \r\n" +
                                            " set @Bip6 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip5) \r\n" +
                                            " set @Bip7 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip6) \r\n" +
                                            " set @Bip8 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip7) \r\n" +
                                            " set @Bip9 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip8) \r\n" +
                                            " set @Bip10 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip9) \r\n" +
                                            " set @Bip11 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip10) \r\n" +
                                            " set @Bip12 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip11) \r\n" +
                                            " set @Bip13 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip12) \r\n" +
                                            " set @Bip14 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip13) \r\n" +
                                            " set @Bip15 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip14) \r\n" +
                                            " set @Bip16 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip15) \r\n" +
                                            " set @Bip17 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip16) \r\n" +
                                            " set @Bip18 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip17) \r\n" +
                                            " set @Bip19 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip18) \r\n" +
                                            " set @Bip20 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip19) \r\n" +

                                            " select a.BIPSweepingsPercentFrom, a.BIPSweepingsPercentTo, a.BIPSweepingsAmount aa1, b.BIPSweepingsAmount aa2, c.BIPSweepingsAmount aa3, d.BIPSweepingsAmount aa4, e.BIPSweepingsAmount aa5 \r\n" +
                                            " , f.BIPSweepingsAmount aa6, g.BIPSweepingsAmount aa7, h.BIPSweepingsAmount aa8, i.BIPSweepingsAmount aa9, j.BIPSweepingsAmount aa10, k.BIPSweepingsAmount aa11 \r\n" +
                                            " , l.BIPSweepingsAmount aa12, m.BIPSweepingsAmount aa13, n.BIPSweepingsAmount aa14, o.BIPSweepingsAmount aa15, p.BIPSweepingsAmount aa16, q.BIPSweepingsAmount aa17 \r\n" +
                                            " , r.BIPSweepingsAmount aa18, s.BIPSweepingsAmount aa19, t.BIPSweepingsAmount aa20 \r\n" +

                                            " from ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip1) a \r\n" +
                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip2) b on a.BIPSweepingsPercentFrom = b.BIPSweepingsPercentFrom \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip3) c on a.BIPSweepingsPercentFrom = c.BIPSweepingsPercentFrom \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip4) d on a.BIPSweepingsPercentFrom = d.BIPSweepingsPercentFrom \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip5) e on a.BIPSweepingsPercentFrom = e.BIPSweepingsPercentFrom \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip6) f on a.BIPSweepingsPercentFrom = f.BIPSweepingsPercentFrom \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip7) g on a.BIPSweepingsPercentFrom = g.BIPSweepingsPercentFrom \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + " \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip8) h on a.BIPSweepingsPercentFrom = h.BIPSweepingsPercentFrom \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip9) i on a.BIPSweepingsPercentFrom = i.BIPSweepingsPercentFrom \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip10) j on a.BIPSweepingsPercentFrom = j.BIPSweepingsPercentFrom \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip11) k on a.BIPSweepingsPercentFrom = k.BIPSweepingsPercentFrom \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip12) l on a.BIPSweepingsPercentFrom = l.BIPSweepingsPercentFrom \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip13) m on a.BIPSweepingsPercentFrom = m.BIPSweepingsPercentFrom \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip14) n on a.BIPSweepingsPercentFrom = n.BIPSweepingsPercentFrom \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip15) o on a.BIPSweepingsPercentFrom = o.BIPSweepingsPercentFrom \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip16) p on a.BIPSweepingsPercentFrom = p.BIPSweepingsPercentFrom \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip17) q on a.BIPSweepingsPercentFrom = q.BIPSweepingsPercentFrom \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip18) r on a.BIPSweepingsPercentFrom = r.BIPSweepingsPercentFrom \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip19) s on a.BIPSweepingsPercentFrom = s.BIPSweepingsPercentFrom \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip20) t on a.BIPSweepingsPercentFrom = t.BIPSweepingsPercentFrom \r\n" +
                                           "  ";

                _dbManUsers.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManUsers.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManUsers.ExecuteInstruction();

                DataTable dt = _dbManUsers.ResultsDataTable;


                DataSet ds = new DataSet();

                ds.Tables.Add(dt);

                gridControl3.DataSource = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    //BIP1.FieldName = "BIPStopingSQM";
                    BIP2.FieldName = "BIPSweepingsPercentFrom";
                    BIP3.FieldName = "BIPSweepingsPercentTo";
                    BIP4.FieldName = "aa1";
                    BIP5.FieldName = "aa2";
                    BIP6.FieldName = "aa3";
                    BIP7.FieldName = "aa4";
                    BIP8.FieldName = "aa5";
                    BIP9.FieldName = "aa6";
                    BIP10.FieldName = "aa7";
                    BIP11.FieldName = "aa8";
                    BIP12.FieldName = "aa9";
                    BIP13.FieldName = "aa10";
                    BIP14.FieldName = "aa11";
                    BIP15.FieldName = "aa12";
                    BIP16.FieldName = "aa13";
                    BIP17.FieldName = "aa14";
                    BIP18.FieldName = "aa15";
                    BIP19.FieldName = "aa16";
                    BIP20.FieldName = "aa17";


                }
            }


            if (SweepCbx.Checked == false)
            {

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = "";
                _dbMan1.SqlStatement = " select " + @Collumn + " from " + @table + " where prodmonth = '" + ProdMonthTxt.Text + "'  group by " + @Collumn + "  order by " + @Collumn + "  ";
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ExecuteInstruction();

                DataTable dt2 = _dbMan1.ResultsDataTable;

                if (dt2.Rows.Count > 0)
                {

                    BIP1.Caption = "m2";


                    BIP2.Visible = false;
                    BIP3.Visible = false;
                    BIP4.Visible = false;
                    BIP5.Visible = false;
                    BIP6.Visible = false;
                    BIP7.Visible = false;
                    BIP8.Visible = false;
                    BIP9.Visible = false;
                    BIP10.Visible = false;
                    BIP11.Visible = false;
                    BIP12.Visible = false;
                    BIP13.Visible = false;
                    BIP14.Visible = false;
                    BIP15.Visible = false;
                    BIP16.Visible = false;
                    BIP17.Visible = false;
                    BIP18.Visible = false;
                    BIP19.Visible = false;
                    BIP20.Visible = false;

                    for (int k = 0; k <= dt2.Rows.Count - 1; k++)
                    {
                        if (k == 0)
                        {
                            BIP2.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP2.Visible = true;
                        }
                        if (k == 1)
                        {
                            BIP3.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP3.Visible = true;
                        }
                        if (k == 2)
                        {
                            BIP4.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP4.Visible = true;
                        }
                        if (k == 3)
                        {
                            BIP5.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP5.Visible = true;
                        }
                        if (k == 4)
                        {
                            BIP6.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP6.Visible = true;
                        }
                        if (k == 5)
                        {
                            BIP7.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP7.Visible = true;
                        }
                        if (k == 6)
                        {
                            BIP8.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP8.Visible = true;
                        }
                        if (k == 7)
                        {
                            BIP9.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP9.Visible = true;
                        }
                        if (k == 8)
                        {
                            BIP10.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP10.Visible = true;
                        }
                        if (k == 9)
                        {
                            BIP11.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP11.Visible = true;
                        }
                        if (k == 10)
                        {
                            BIP12.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP12.Visible = true;
                        }
                        if (k == 11)
                        {
                            BIP13.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP13.Visible = true;
                        }
                        if (k == 12)
                        {
                            BIP14.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP14.Visible = true;
                        }
                        if (k == 13)
                        {
                            BIP15.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP15.Visible = true;
                        }
                        if (k == 14)
                        {
                            BIP16.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP16.Visible = true;
                        }
                        if (k == 15)
                        {
                            BIP17.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP17.Visible = true;
                        }
                        if (k == 16)
                        {
                            BIP18.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP18.Visible = true;
                        }
                        if (k == 17)
                        {
                            BIP19.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP19.Visible = true;
                        }
                        if (k == 18)
                        {
                            BIP20.Caption = dt2.Rows[k][@Collumn].ToString();
                            BIP20.Visible = true;
                        }

                    }




                }

                MWDataManager.clsDataAccess _dbManUsers = new MWDataManager.clsDataAccess();
                _dbManUsers.ConnectionString = "";
                _dbManUsers.SqlStatement = "   " +
                                            " declare @pm varchar(50) \r\n" +

                                            " declare @Bip1 varchar(50) \r\n" +
                                            " declare @Bip2 varchar(50) \r\n" +
                                            " declare @Bip3 varchar(50) \r\n" +
                                            " declare @Bip4 varchar(50) \r\n" +
                                            " declare @Bip5 varchar(50) \r\n" +
                                            " declare @Bip6 varchar(50) \r\n" +
                                            " declare @Bip7 varchar(50) \r\n" +
                                            " declare @Bip8 varchar(50) \r\n" +
                                            " declare @Bip9 varchar(50) \r\n" +
                                            " declare @Bip10 varchar(50) \r\n" +
                                            " declare @Bip11 varchar(50) \r\n" +
                                            " declare @Bip12 varchar(50) \r\n" +
                                            " declare @Bip13 varchar(50) \r\n" +
                                            " declare @Bip14 varchar(50) \r\n" +
                                            " declare @Bip15 varchar(50) \r\n" +
                                            " declare @Bip16 varchar(50) \r\n" +
                                            " declare @Bip17 varchar(50) \r\n" +
                                            " declare @Bip18 varchar(50) \r\n" +
                                            " declare @Bip19 varchar(50) \r\n" +
                                            " declare @Bip20 varchar(50) \r\n" +



                                            " set @pm = '" + ProdMonthTxt.Text + "' \r\n" +


                                            " set @Bip1 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm) \r\n" +
                                            " set @Bip2 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip1) \r\n" +
                                            " set @Bip3 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip2) \r\n" +
                                            " set @Bip4 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip3) \r\n" +
                                            " set @Bip5 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip4) \r\n" +
                                            " set @Bip6 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip5) \r\n" +
                                            " set @Bip7 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip6) \r\n" +
                                            " set @Bip8 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip7) \r\n" +
                                            " set @Bip9 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip8) \r\n" +
                                            " set @Bip10 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip9) \r\n" +
                                            " set @Bip11 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip10) \r\n" +
                                            " set @Bip12 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip11) \r\n" +
                                            " set @Bip13 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip12) \r\n" +
                                            " set @Bip14 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip13) \r\n" +
                                            " set @Bip15 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip14) \r\n" +
                                            " set @Bip16 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip15) \r\n" +
                                            " set @Bip17 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip16) \r\n" +
                                            " set @Bip18 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip17) \r\n" +
                                            " set @Bip19 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip18) \r\n" +
                                            " set @Bip20 = (select min(" + @Collumn + " ) aa from  " + @table + "  where prodmonth = @pm and " + @Collumn + "  > @Bip19) \r\n" +

                                            " select a.BIPStopingSQM, a.bipstopingamount aa1, b.bipstopingamount aa2, c.bipstopingamount aa3, d.bipstopingamount aa4, e.bipstopingamount aa5 \r\n" +
                                            " , f.bipstopingamount aa6, g.bipstopingamount aa7, h.bipstopingamount aa8, i.bipstopingamount aa9, j.bipstopingamount aa10, k.bipstopingamount aa11 \r\n" +
                                            " , l.bipstopingamount aa12, m.bipstopingamount aa13, n.bipstopingamount aa14, o.bipstopingamount aa15, p.bipstopingamount aa16, q.bipstopingamount aa17 \r\n" +
                                            " , r.bipstopingamount aa18, s.bipstopingamount aa19, t.bipstopingamount aa20 \r\n" +

                                            " from ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip1) a \r\n" +
                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip2) b on a.BIPStopingSQM = b.BIPStopingSQM \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip3) c on a.BIPStopingSQM = c.BIPStopingSQM \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip4) d on a.BIPStopingSQM = d.BIPStopingSQM \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip5) e on a.BIPStopingSQM = e.BIPStopingSQM \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip6) f on a.BIPStopingSQM = f.BIPStopingSQM \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip7) g on a.BIPStopingSQM = g.BIPStopingSQM \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + " \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip8) h on a.BIPStopingSQM = h.BIPStopingSQM \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip9) i on a.BIPStopingSQM = i.BIPStopingSQM \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip10) j on a.BIPStopingSQM = j.BIPStopingSQM \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip11) k on a.BIPStopingSQM = k.BIPStopingSQM \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip12) l on a.BIPStopingSQM = l.BIPStopingSQM \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip13) m on a.BIPStopingSQM = m.BIPStopingSQM \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip14) n on a.BIPStopingSQM = n.BIPStopingSQM \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip15) o on a.BIPStopingSQM = o.BIPStopingSQM \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip16) p on a.BIPStopingSQM = p.BIPStopingSQM \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip17) q on a.BIPStopingSQM = q.BIPStopingSQM \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip18) r on a.BIPStopingSQM = r.BIPStopingSQM \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip19) s on a.BIPStopingSQM = s.BIPStopingSQM \r\n" +

                                            " left outer join \r\n" +
                                            " ( \r\n" +
                                            " select * from " + @table + "  \r\n" +
                                            " where prodmonth = @pm \r\n" +
                                            " and " + @Collumn + "  = @Bip20) t on a.BIPStopingSQM = t.BIPStopingSQM \r\n" +
                                           "  ";

                _dbManUsers.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManUsers.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManUsers.ExecuteInstruction();

                DataTable dt = _dbManUsers.ResultsDataTable;


                DataSet ds = new DataSet();

                ds.Tables.Add(dt);

                gridControl3.DataSource = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    BIP1.FieldName = "BIPStopingSQM";
                    BIP2.FieldName = "aa1";
                    BIP3.FieldName = "aa2";
                    BIP4.FieldName = "aa3";
                    BIP5.FieldName = "aa4";
                    BIP6.FieldName = "aa5";
                    BIP7.FieldName = "aa6";
                    BIP8.FieldName = "aa7";
                    BIP9.FieldName = "aa8";
                    BIP10.FieldName = "aa9";
                    BIP11.FieldName = "aa10";
                    BIP12.FieldName = "aa11";
                    BIP13.FieldName = "aa12";
                    BIP14.FieldName = "aa13";
                    BIP15.FieldName = "aa14";
                    BIP16.FieldName = "aa15";
                    BIP17.FieldName = "aa16";
                    BIP18.FieldName = "aa17";
                    BIP19.FieldName = "aa18";
                    BIP20.FieldName = "aa19";


                }

               


               
            }

            bandedGridView5.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
        }

        private void navBarItem16_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

            //HidePnls();
           // PnlMingParam.Visible = true;
           // EngPnl.Visible = false;
            BasicIncTablePnl.Visible = true;
            BasicIncTablePnl.Dock = DockStyle.Fill;
            //gridControl1.Dock = DockStyle.Fill;
            PeramPnl.Visible = false;           
           
            TramPnl.Visible = false;
            MiningFactorsPnl.Visible = false;            
            pnlSBResults.Visible = false;

            

            if (BIPRG.SelectedIndex == 0)
            {
                LoadDataStoping();

            }



        }

        private void TransferDataBtn_Click_1(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _dbManNS = new MWDataManager.clsDataAccess();
            _dbManNS.ConnectionString = "";
            _dbManNS.SqlStatement = _dbManNS.SqlStatement + "  ";

            _dbManNS.SqlStatement = _dbManNS.SqlStatement + " delete from mineware.dbo.tbl_BCS_AbsenteeismFactors where prodmonth = '" + PMnthTxt.Text + "' ";
            for (int k = 0; k <= bandedGridView4.RowCount - 1; k++)
            {
                if (bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[0]) != null && bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[1]) != null)
                {
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " delete from mineware.dbo.tbl_BCS_AbsenteeismFactors where prodmonth = '" + PMnthTxt.Text + "' and shiftno =  '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[0]) + "' and  factor =  '" + bandedGridView1.GetRowCellValue(k, bandedGridView1.Columns[1]) + "' ";
                }
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " insert into mineware.dbo.tbl_BCS_AbsenteeismFactors Values(  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + PMnthTxt.Text + "',  ";
                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView4.GetRowCellValue(k, bandedGridView4.Columns[0]) + "',  ";

                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + bandedGridView4.GetRowCellValue(k, bandedGridView4.Columns[1]) + "')  ";



            }
            _dbManNS.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManNS.queryReturnType = MWDataManager.ReturnType.longNumber;
            _dbManNS.ExecuteInstruction();

            MessageBox.Show("Factors were successfully saved", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadRow();
            LoadAbsentGrid();
            
        }

        private void MerCbx_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataStoping();
        }

        private void UnitSweepCbx_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataStoping();
        }

        private void RCrewsCbx_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataStoping();
        }

        private void SweepCbx_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataStoping();
        }

        private void ReEstCbx_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataStoping();
        }

        private void BIPRG_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BIPRG.SelectedIndex == 0)
            {
                StopingGB.Visible = true;
                DevGB.Visible = false;
                LoadDataStoping();
            }
            else
            {
                DevGB.Visible = true;
                StopingGB.Visible = false;
                gridControl3.Refresh();
            }
        }

        private void RaiseCbx_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataDev();
        }

        private void LatDevCbx_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataDev();
        }

        private void WaterEndsCbx_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataDev();
        }

        private void BHCbx_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataDev();
        }

        private void ChairliftCbx_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataDev();
        }

        private void RaiseCbx_Click(object sender, EventArgs e)
        {
            LatDevCbx.Checked = false;
            //RaiseCbx.Checked = false;
            WaterEndsCbx.Checked = false;
            BHCbx.Checked = false;
            ChairliftCbx.Checked = false;
        }

        private void MerCbx_Click(object sender, EventArgs e)
        {

            UnitSweepCbx.Checked = false;
            RCrewsCbx.Checked = false;
            SweepCbx.Checked = false;

        }

        private void UnitSweepCbx_Click(object sender, EventArgs e)
        {
            MerCbx.Checked = false;
            RCrewsCbx.Checked = false;
            SweepCbx.Checked = false;
        }

        private void RCrewsCbx_Click(object sender, EventArgs e)
        {
            MerCbx.Checked = false;
            UnitSweepCbx.Checked = false;
            SweepCbx.Checked = false;
        }

        private void SweepCbx_Click(object sender, EventArgs e)
        {
            MerCbx.Checked = false;
            RCrewsCbx.Checked = false;
            UnitSweepCbx.Checked = false;
        }

        private void LatDevCbx_Click(object sender, EventArgs e)
        {
            //LatDevCbx.Checked = false;
            RaiseCbx.Checked = false;
            WaterEndsCbx.Checked = false;
            BHCbx.Checked = false;
            ChairliftCbx.Checked = false;
        }

        private void WaterEndsCbx_Click(object sender, EventArgs e)
        {
            LatDevCbx.Checked = false;
            RaiseCbx.Checked = false;
            //WaterEndsCbx.Checked = false;
            BHCbx.Checked = false;
            ChairliftCbx.Checked = false;
        }

        private void BHCbx_Click(object sender, EventArgs e)
        {
            LatDevCbx.Checked = false;
            RaiseCbx.Checked = false;
            WaterEndsCbx.Checked = false;
            //BHCbx.Checked = false;
            ChairliftCbx.Checked = false;
        }

        private void ChairliftCbx_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void ChairliftCbx_Click(object sender, EventArgs e)
        {
            LatDevCbx.Checked = false;
            RaiseCbx.Checked = false;
            WaterEndsCbx.Checked = false;
            BHCbx.Checked = false;
            //ChairliftCbx.Checked = false;
        }

        private void navBarItem18_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            pnlTestGangMapping.Visible = false;
            //HidePnls();
            //PnlMingParam.Visible = false;
            //EngPnl.Visible = false;
            //TrammingPnl.Visible = true;
            //TrammingPnl.Dock = DockStyle.Fill;
            //gridControl4.Dock = DockStyle.Fill;
            //gridControl4.Visible = true;
            //PeramPnl.Visible = false;
            //LoadDataTram();

            HidePnls();
            PnlMingParam.Visible = false;
            EngPnl.Visible = false;
            //TrammingPnl.Visible = true;
            //TrammingPnl.Dock = DockStyle.Fill;
            //gridControl4.Dock = DockStyle.Fill;
            gridControl4.Visible = true;
            PeramPnl.Visible = false;

            ucTrammingCapture uctram = new ucTrammingCapture();
            pnlTrammingCapture.Controls.Add(uctram);
            pnlTrammingCapture.Visible = true;
            pnlTrammingCapture.Dock = DockStyle.Fill;
            uctram.Dock = DockStyle.Fill;
            uctram.BringToFront();
            uctram.Show();
            LoadDataTram();

        }

        void LoadDataTram()
        {


            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = "";
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
            _dbMan1.ConnectionString = "";
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

        private void OrgUnitCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Level = LevelCombo.SelectedItem.ToString();
            string Orgunit = OrgUnitCombo.SelectedItem.ToString();

            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = "";
            _dbMan1.SqlStatement = " select section from tbl_BCS_Tramming_Levels where level = '" + Level + "' and YearMonth = '201602' and shift = '" + Shift + "' and OrgUnit = '" + Orgunit + "'  order by Section   ";
            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();

            DataTable dt3 = _dbMan1.ResultsDataTable;

            SecLbl.Text = dt3.Rows[0]["Section"].ToString();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {


             MWDataManager.clsDataAccess _dbMan1New = new MWDataManager.clsDataAccess();
             _dbMan1New.ConnectionString = "";

             _dbMan1New.SqlStatement = "declare @pm varchar(10) \r\n" +
                                    "set @pm = '" + MillMonth.Value + "' \r\n" +

                                    "select Startdate, (startdate + 32)-day((startdate + 32)) bb from ( \r\n" +
                                    "select convert(datetime,(substring(@pm,1,4)+ '-'+substring(@pm,5,2) +'-01')) Startdate) a \r\n";
             _dbMan1New.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
             _dbMan1New.queryReturnType = MWDataManager.ReturnType.DataTable;
             _dbMan1New.ExecuteInstruction();




             MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
             _dbMan1.ConnectionString = "";

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





             return;



            //if (DS.Checked == true)
            //{
            //    Shift = "D";

            //}

            //if (AS.Checked == true)
            //{
            //    Shift = "A";

            //}

            //if (NS.Checked == true)
            //{
            //    Shift = "N";

            //}

            // string Level = LevelCombo.SelectedItem.ToString();
            // string Orgunit = OrgUnitCombo.SelectedItem.ToString();

            //MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            //_dbMan1.ConnectionString = "";

            //_dbMan1.SqlStatement = "  Select a.*,  \r\n" +
            //        " case when   b.IndustryNumber is null then 'No' else 'Yes' end as Saved from ( \r\n" +

            //        "  select * from tbl_BCS_Tramming_Gang  \r\n" +
            //        "where workingorgunit = '" + Orgunitlbl.Text + "'  \r\n" +
            //        "and date = (select max(date) from tbl_BCS_Tramming_Gang   \r\n" +
            //        "where workingorgunit = '" + Orgunitlbl.Text + "')  \r\n" +
            //        "  ) a  \r\n" +
            //        "left outer join   \r\n" +
            //        "(  \r\n" +
            //        "Select * from [dbo].[tbl_BCS_Tramming_Gang_3Month] \r\n" +
            //        "where workingorgunit = '" + Orgunitlbl.Text + "'  \r\n" +
            //        " and date = (select max(date) from [tbl_BCS_Tramming_Gang_3Month]   \r\n" +
            //        "where workingorgunit = '" + Orgunitlbl.Text + "') ) b   \r\n" +
            //        " on a.ID = b.ID and a.IndustryNumber = b.IndustryNumber  \r\n" +
            //        " order by a.team ";





            //_dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            //_dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            //_dbMan1.ExecuteInstruction();

            DataTable dt = _dbMan1.ResultsDataTable;


            DataSet ds = new DataSet();

            ds.Tables.Add(dt);

            gridControl4.DataSource = ds.Tables[0];

            if (dt.Rows.Count > 0)
            {
                Tram1.FieldName = "IndustryNumber";
                Tram2.FieldName = "Designation";
                Tram3.FieldName = "Attendance";
                Tram4.FieldName = "TeamGroup";
                Tram5.FieldName = "Team";
                Tram6.FieldName = "Hoppers";
                Tram7.FieldName = "Added";
                Tram8.FieldName = "OrgUnit";
                Tram9.FieldName = "Saved";


            }

        }


        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (Orgunitlbl.Text == "Orgunitlbl")
            {
                MessageBox.Show("Please Select a Level");
                return;
            }


            frmProp RepFrm = (frmProp)IsBookingFormAlreadyOpen(typeof(frmProp));
            if (RepFrm == null)
            {
                RepFrm = new frmProp();
                RepFrm.Text = "Add Gang Member";
                RepFrm.IDLbl.Text = "1";
                RepFrm.MonthLbl.Text = MillMonth.Value.ToString();
                RepFrm.OrgUnitLbl.Text = Orgunitlbl.Text;
                string ss = Orgunitlbl.Text;
                RepFrm.LvlLbl.Text = "zzz";
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
                //RepFrm.IndNoLbl;

                RepFrm.Show();
            }
            else
            {
                RepFrm.WindowState = FormWindowState.Maximized;
                RepFrm.Select();
            }

        }

        private void bandedGridView6_ShowFilterPopupListBox(object sender, FilterPopupListBoxEventArgs e)
        {

           
            
        }

        private void bandedGridView6_ShowFilterPopupCheckedListBox(object sender, FilterPopupCheckedListBoxEventArgs e)
        {
                     


            
 

      
    

        }

        private void bandedGridView6_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            IndNoLbl.Text = bandedGridView6.GetRowCellValue(e.RowHandle, bandedGridView6.Columns[0]).ToString();
            Desiglbl.Text = bandedGridView6.GetRowCellValue(e.RowHandle, bandedGridView6.Columns[1]).ToString();
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
                }
            }
        }

        private void Orgunitlbl_TextChanged(object sender, EventArgs e)
        {
            simpleButton3_Click(null, null);
        }

        private void bandedGridView6_DoubleClick(object sender, EventArgs e)
        {
            //simpleButton2_Click(null, null);

            frmProp RepFrm = (frmProp)IsBookingFormAlreadyOpen(typeof(frmProp));
            if (RepFrm == null)
            {
                RepFrm = new frmProp();
                RepFrm.Text = "Edit Gang Member";
                RepFrm.IDLbl.Text = "1";
                RepFrm.MonthLbl.Text = MillMonth.Value.ToString();
                RepFrm.OrgUnitLbl.Text = Orgunitlbl.Text;
                RepFrm.lblHoppersTramEdit.Text = bandedGridView6.GetRowCellValue(bandedGridView6.FocusedRowHandle, bandedGridView6.Columns["Hoppers"]).ToString();
                RepFrm.lblTeamGroupTramEdit.Text = bandedGridView6.GetRowCellValue(bandedGridView6.FocusedRowHandle, bandedGridView6.Columns["TeamGroup"]).ToString();
                RepFrm.lblTeamTramEdit.Text = bandedGridView6.GetRowCellValue(bandedGridView6.FocusedRowHandle, bandedGridView6.Columns["Team"]).ToString();
                string ss = Orgunitlbl.Text;
                RepFrm.LvlLbl.Text = "zzz";

                // string test = bandedGridView6.GetRowCellValue(bandedGridView6.FocusedRowHandle, bandedGridView6.Columns["Attendance"]).ToString();

                if (bandedGridView6.GetRowCellValue(bandedGridView6.FocusedRowHandle, bandedGridView6.Columns["Attendance"]).ToString() == "N ")
                {
                    RepFrm.AttRG.SelectedIndex = 1;
                }

                if (bandedGridView6.GetRowCellValue(bandedGridView6.FocusedRowHandle, bandedGridView6.Columns["Attendance"]).ToString() == "Y ")
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

        private void navBarItem19_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //ucTrammingBonus RepFrm = (ucTrammingBonus)IsBookingFormAlreadyOpen(typeof(ucTrammingBonus));
            //if (RepFrm == null)
            //{
            //    RepFrm = new ucTrammingBonus();
            //    RepFrm.Icon = this.Icon;
            //    RepFrm.Show();
            //}
            //else
            //{
            //    RepFrm.WindowState = FormWindowState.Maximized;
            //    RepFrm.Select();
            //}
        }

        private void navBarItem21_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            TramPnl.Visible = true;
            TramPnl.Dock = DockStyle.Fill;
           
            MiningFactorsPnl.Visible = false;
            BasicIncTablePnl.Visible = false;
            pnlSBResults.Visible = false;
        }

        private void navBarItem20_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //ucMinersBonus MinersForm = (ucMinersBonus)IsBookingFormAlreadyOpen(typeof(ucMinersBonus));
            //if (MinersForm == null)
            //{
            //    MinersForm = new ucMinersBonus();
            //    MinersForm.Icon = this.Icon;
            //    //EngForm.Text = "Development Shift Boss Bonus";
            //    MinersForm.Show();
            //}
            //else
            //{
            //    MinersForm.WindowState = FormWindowState.Maximized;
            //    MinersForm.Select();
            //} 
        }

        private void navBarItem22_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //ucReports frmReports = (ucReports)IsBookingFormAlreadyOpen(typeof(ucReports));
            //if (frmReports == null)
            //{
            //    frmReports = new ucReports();
            //    frmReports.Icon = this.Icon;
            //    frmReports.Text = "Reporting";
            //    frmReports.Show();
            //}
            //else
            //{
            //    frmReports.WindowState = FormWindowState.Maximized;
            //    frmReports.Select();
            //} 

        }

        private void gvDays_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string month = txtMonth.Value.ToString();
            string day = gvDays.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            string workingday = "";

            string isWorkingday = "";

            if (month.Length == 1)
            {
                month = "0" + month;
            }

            if (day.Length == 1)
            {
                day = "0" + day;
            }



            if (gvDays[e.ColumnIndex, e.RowIndex].Style.BackColor == Color.Red)
            {
                workingday = "N";
                isWorkingday = "Y";
            }
            else
            {
                workingday = "Y";
                isWorkingday = "N";
            }

            //if (gvDays[e.ColumnIndex, e.RowIndex].Style.BackColor == SystemColors.Window)
            //{
            //    workingday = "Y";
            //}

            string Calendardate = txtYear.Value + "-" + month + "-" + day;


            if (gvDays.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && gvDays.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
            {
                //MessageBox.Show(txtYear.Value + "-" + month + "-" + day + "\r\n" + workingday);

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = "";
                _dbMan.SqlStatement = " update [Mineware].[dbo].[BMCS_CALTYPE] set WORKINGDAY = '" + isWorkingday + "' where CALENDARTYPEID = 'ESF A' and CALENDARDATE = '" + Calendardate + "' ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ExecuteInstruction();
            }

            //int DaysInMonth = (System.DateTime.DaysInMonth(Convert.ToInt32(txtYear.Value), Convert.ToInt32(txtMonth.Value)));


            LoadTheCalendar();
        }

        private void gvDays2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string month = txtMonth.Value.ToString();
            string day = gvDays.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            string workingday = "";

            string isWorkingday = "";

            if (month.Length == 1)
            {
                month = "0" + month;
            }

            if (day.Length == 1)
            {
                day = "0" + day;
            }



            if (gvDays2[e.ColumnIndex, e.RowIndex].Style.BackColor == Color.Red)
            {
                workingday = "N";
                isWorkingday = "Y";
            }
            else
            {
                workingday = "Y";
                isWorkingday = "N";
            }

            //if (gvDays[e.ColumnIndex, e.RowIndex].Style.BackColor == SystemColors.Window)
            //{
            //    workingday = "Y";
            //}

            string Calendardate = txtYear.Value + "-" + month + "-" + day;


            if (gvDays2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && gvDays2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
            {
                //MessageBox.Show(txtYear.Value + "-" + month + "-" + day + "\r\n" + workingday);

                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = "";
                _dbMan.SqlStatement = " update [Mineware].[dbo].[BMCS_CALTYPE] set WORKINGDAY = '" + isWorkingday + "' where CALENDARTYPEID = 'ESF B' and CALENDARDATE = '" + Calendardate + "' ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ExecuteInstruction();
            }

            //int DaysInMonth = (System.DateTime.DaysInMonth(Convert.ToInt32(txtYear.Value), Convert.ToInt32(txtMonth.Value)));

            LoadTheCalendar();
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {

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
            _dbMan.ConnectionString = "";
            _dbMan.SqlStatement = " delete from tbl_BCS_Tramming_Gang_3Month  \r\n" +
                                  " where  ID = '1' and YearMonth = '" + MillMonth.Value.ToString() + "' \r\n" +
                                  " and [Date] = '" + String.Format("{0:yyyy-MM-dd}", DateTxt.Value) + "' and IndustryNumber = '" + IndNoLbl.Text + "'  \r\n" +
                                  " and OrgUnit = '" + Orgunitlbl.Text + "' and WorkingOrgUnit = '" + Orgunitlbl.Text + "'  \r\n" +
                                  " and Section = '" + Section + "' and [Level] = '" + lvl + "' ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            // _dbMan.ExecuteInstruction();
        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            frmAddTramCrewcs ABSfrm = new frmAddTramCrewcs();
            ABSfrm.Icon = this.Icon;
            ABSfrm.ShowDialog();

            LoadDataTram();
        }

        private void navBarItem23_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            HidePnls();
           
            SysAdminPnl.Visible = true;
            SysAdminPnl.Dock = DockStyle.Fill;

            CreateTable();


          
        }




        private void MillMontha_Click(object sender, EventArgs e)
        {
            Procedures procs = new Procedures();
            procs.ProdMonthCalc(Convert.ToInt32(MillMontha.Text));
            MillMontha.Text = Procedures.Prod.ToString();
            procs.ProdMonthVis(Convert.ToInt32(MillMontha.Text));
            MillMonth1a.Text = Procedures.Prod2;

            CreateTable();
        }

        private void CreateTable()
        {

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = "";
            _dbMan.SqlStatement = " exec NorthamPas.[dbo].[sp_BMCS_import_CreateNewTable] '" + MillMontha.Value.ToString() + "' \r\n";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = "";
            _dbMan1.SqlStatement = " select substring(orgunit,1,4) oo, convert(varchar(50),captdate,106) +' '+substring(convert(varchar(50),captdate,108),1,5) captdate, username from mineware.dbo.tbl_BCS_Imports_" + MillMontha.Value.ToString() + " group by substring(orgunit,1,4) , captdate, username order by substring(orgunit,1,4) , captdate desc \r\n";
            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();


            DataTable dtMain = _dbMan1.ResultsDataTable;

            DataSet ds = new DataSet();

            ds.Tables.Add(dtMain);

            gcBonusImport.DataSource = ds.Tables[0];

            colUser.FieldName = "username";
            colDate.FieldName = "captdate";
            colSection.FieldName = "oo";




            MWDataManager.clsDataAccess _dbMan1a = new MWDataManager.clsDataAccess();
            _dbMan1a.ConnectionString = "";
            _dbMan1a.SqlStatement = "select substring(orgunit,1,4) ss from mineware.[dbo].[tbl_BCS_Gangs_3Month] " +
                                    " where prodmonth = '" + MillMontha.Value.ToString() + "' group by substring(orgunit,1,4) order by substring(orgunit,1,4)";
            _dbMan1a.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1a.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1a.ExecuteInstruction();

            DataTable dtMain1 = _dbMan1a.ResultsDataTable;
            MOlistBox.Items.Clear();

            foreach (DataRow dr1 in dtMain1.Rows)
            {
                MOlistBox.Items.Add(dr1["ss"].ToString());
            }

           



           


        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            string crew = crewTxt.Text +'%';

            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = "";
            _dbMan1.SqlStatement = " insert into mineware.dbo.tbl_BCS_Imports_" + MillMontha.Value.ToString() + " \r\n"+
                                    " select  '" + clsUserInfo.UserName + "' , getdate(), * from mineware.[dbo].[tbl_Import_BMCS_Clocking_Total] \r\n" +
                                    "where thedate >= ( \r\n" +

                                    "select min(date) from mineware.[dbo].[tbl_BCS_Gangs_3Month] where prodmonth = '" + MillMontha.Value.ToString() + "' \r\n" +
                                    "and orgunit like  '" + crew + "' ) \r\n" +

                                    "and thedate <= ( \r\n" +

                                    "select max(date) from mineware.[dbo].[tbl_BCS_Gangs_3Month] where prodmonth = '" + MillMontha.Value.ToString() + "' \r\n" +
                                    "and orgunit like '" + crew + "' ) and orgunit like '" + crew + "'  \r\n";



            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();

            MWDataManager.clsDataAccess _dbMan11 = new MWDataManager.clsDataAccess();
            _dbMan11.ConnectionString = "";
            _dbMan11.SqlStatement = " exec mineware.dbo.[sp_BMCS_import_ImportData] '" + MillMontha.Value.ToString() + "',  '" + crewTxt.Text + "' \r\n";
           
            _dbMan11.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan11.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan11.ExecuteInstruction();

            MessageBox.Show("Bonus Details was successfully transferred", "Transferred", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        DialogResult result;

        private void MOlistBox_DoubleClick(object sender, EventArgs e)
        {
            if (MOlistBox.SelectedItem != null)
            {
                crewTxt.Text = MOlistBox.SelectedItem.ToString();


                result = MessageBox.Show("Are you sure you want to transfer MO " + crewTxt.Text + "?", "Transfer Record", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {

                    button13_Click_1(null,null);
                    CreateTable();

                }
            }






        }

        private void MillMonth_Click(object sender, EventArgs e)
        {
            Procedures procs = new Procedures();
            procs.ProdMonthCalc(Convert.ToInt32(MillMonth.Text));
            MillMonth.Text = Procedures.Prod.ToString();
            procs.ProdMonthVis(Convert.ToInt32(MillMonth.Text));
            MillMonth1.Text = Procedures.Prod2;
        }

        private void navBarItem24_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

        }

        private void btnGangMapping_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            HidePnls();
            pnlTrammingCapture.Visible = false;
            pnlTrammingCapture.Dock = DockStyle.None;

            ucGangMapping ucTestGang = new ucGangMapping();
            pnlTestGangMapping.Controls.Add(ucTestGang);
            pnlTestGangMapping.Visible = true;
            pnlTestGangMapping.Dock = DockStyle.Fill;
            ucTestGang.Dock = DockStyle.Fill;
            ucTestGang.BringToFront();
            ucTestGang.Show();
        }

        private void navBarItem25_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            pnlTrammingCapture.Visible = false;
            pnlTestGangMapping.Visible = false;
            //HidePnls();
            //PnlMingParam.Visible = false;
            //EngPnl.Visible = false;
            //TrammingPnl.Visible = true;
            //TrammingPnl.Dock = DockStyle.Fill;
            //gridControl4.Dock = DockStyle.Fill;
            //gridControl4.Visible = true;
            //PeramPnl.Visible = false;
            //LoadDataTram();

            HidePnls();
            PnlMingParam.Visible = false;
            EngPnl.Visible = false;
            //TrammingPnl.Visible = true;
            //TrammingPnl.Dock = DockStyle.Fill;
            //gridControl4.Dock = DockStyle.Fill;
            gridControl4.Visible = true;
            PeramPnl.Visible = false;

            ucProductionMinersCapture uctram = new ucProductionMinersCapture();
            pnlProdCapture.Controls.Add(uctram);
            pnlProdCapture.Visible = true;
            pnlProdCapture.Dock = DockStyle.Fill;
            uctram.Dock = DockStyle.Fill;
            uctram.BringToFront();
            uctram.Show();
            LoadDataTram();
        }

        private void btnDataExtraction_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            pnlTrammingCapture.Visible = false;
            pnlTestGangMapping.Visible = false;
            pnlProdCapture.Visible = false;

            HidePnls();
            PnlMingParam.Visible = false;
            EngPnl.Visible = false;
            PeramPnl.Visible = false;


            ucDataExtract ucName = new ucDataExtract();
            pnlDataExtraction.Controls.Add(ucName);
            pnlDataExtraction.Visible = true;
            pnlDataExtraction.Dock = DockStyle.Fill;
            ucName.Dock = DockStyle.Fill;
            ucName.BringToFront();
            ucName.Show();
        }

        private void label119_Click(object sender, EventArgs e)
        {

        }

        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //frmSafetyNew RepFrm = (frmSafetyNew)IsBookingFormAlreadyOpen(typeof(frmSafetyNew));
            //if (RepFrm == null)
            //{
            //    RepFrm = new frmSafetyNew();
            //    RepFrm.Text = "Safety";
            //    RepFrm.Show();
            //}
            //else
            //{
            //    RepFrm.WindowState = FormWindowState.Maximized;
            //    RepFrm.Select();
            //}

            pnlTrammingCapture.Visible = false;
            pnlTestGangMapping.Visible = false;
            pnlProdCapture.Visible = false;

            HidePnls();
            PnlMingParam.Visible = false;
            EngPnl.Visible = false;
            PeramPnl.Visible = false;


            ucSafetyCaptureNew ucName = new ucSafetyCaptureNew();
            pnlDataExtraction.Controls.Add(ucName);
            pnlDataExtraction.Visible = true;
            pnlDataExtraction.Dock = DockStyle.Fill;
            ucName.Dock = DockStyle.Fill;
            ucName.BringToFront();
            ucName.Show();

        }


        
        

        
    }
}
