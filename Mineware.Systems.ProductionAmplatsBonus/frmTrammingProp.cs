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
using System.Net;
using FastReport.Utils;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.ProductionAmplatsBonus
{
    public partial class frmTrammingProp : XtraForm
    {
        public string _connection;
        public frmTrammingProp()
        {
            InitializeComponent();
        }

        private void insertValue_3Month()
        {

            
                MWDataManager.clsDataAccess _dbMan4 = new MWDataManager.clsDataAccess();
                _dbMan4.ConnectionString = _connection;
                _dbMan4.SqlStatement = "INSERT INTO [Mineware].[dbo].[tbl_BCS_Tramming_Gang_3Month] " +
                   " VALUES('1','" + tbMillMonth.Text + "','','" + luEmpDetail.EditValue.ToString().Substring(0, 8) + "','" + luDesignation.EditValue + "','" + tbOrgUnitOriginal.Text + "','" + tbOrg.Text + "','N','" + luTeamGroup.EditValue.ToString() + "', " +
                " '" + luTeam.EditValue.ToString() + "','0','0','31','" + tbOrg.Text.Substring(0, 4) + "','Y','N','" + tbLevel.Text + "','" + Environment.UserName + "','" + DateTime.Now + "','2','N') ";
                _dbMan4.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan4.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan4.ExecuteInstruction();
            
        }

        public void insertValue()
        {

                MWDataManager.clsDataAccess _dbMan4 = new MWDataManager.clsDataAccess();
                _dbMan4.ConnectionString = _connection;
                _dbMan4.SqlStatement = "INSERT INTO [Mineware].[dbo].[tbl_BCS_Tramming_Gang] " +
                   " VALUES('1','" + tbMillMonth.Text + "','','" + luEmpDetail.EditValue.ToString().Substring(0, 8) + "','" + luDesignation.EditValue + "','" + tbOrgUnitOriginal.Text + "','" + tbOrg.Text + "','N','" + luTeamGroup.EditValue.ToString() + "', " +
                " '" + luTeam.EditValue.ToString() + "','0','0','31','" + tbOrg.Text.Substring(0, 4) + "','Y','N','" + tbLevel.Text + "','" + Environment.UserName + "','" + DateTime.Now + "','2','N') ";
                _dbMan4.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan4.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan4.ExecuteInstruction();
            
        }

        public void loadData()
        {
            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = _connection;
            _dbMan1.SqlStatement = "SELECT DISTINCT IndustryNumber + ' - ' + Surname + '.' + Initials AS EmployeeDetail FROM [Mineware].[dbo].[tbl_Import_BCS_Personnel_Latest]";
            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();

            DataTable dtEmp = _dbMan1.ResultsDataTable;
            luEmpDetail.Properties.DataSource = dtEmp;


            MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
            _dbMan2.ConnectionString = _connection;
            _dbMan2.SqlStatement = "SELECT DISTINCT Team_Description FROM  tbl_BCS_Tramming_MaxInGang WHERE Team_Description != 'Select...'";
            _dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan2.ExecuteInstruction();

            DataTable dtTeam = _dbMan2.ResultsDataTable;
            luTeamGroup.Properties.DataSource = dtTeam;

            luDescription.Properties.DataSource = "";
            DataTable dt0 = new DataTable();
            dt0.Columns.Add("Description");
            dt0.Rows.Add("None");
            dt0.Rows.Add("Guard");
            dt0.Rows.Add("Driver");
            luDescription.Properties.DataSource = dt0;
            luDesignation.EditValue = "None";


            MWDataManager.clsDataAccess _dbMan4 = new MWDataManager.clsDataAccess();
            _dbMan4.ConnectionString = _connection;
            _dbMan4.SqlStatement = "SELECT DISTINCT Level FROM [Mineware].[dbo].[tbl_BCS_Tramming_Levels] WHERE OrgUnit = '" +tbOrg.Text +"' AND YearMonth = '" + tbMillMonth.Text + "'";
            _dbMan4.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan4.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan4.ExecuteInstruction();

            DataTable dtLevel = _dbMan4.ResultsDataTable;
            tbLevel.Text = dtLevel.Rows[0]["Level"].ToString();
        }

        public void lbfill()
        {
            MWDataManager.clsDataAccess _dbMan4 = new MWDataManager.clsDataAccess();
            _dbMan4.ConnectionString = _connection;
            _dbMan4.SqlStatement = "SELECT DISTINCT IndustryNumber + '-' + Designation + '-' + Team AS IndustryNumber FROM [Mineware].[dbo].[tbl_BCS_Tramming_Gang] WHERE WorkingOrgUnit = '" + tbOrg.Text + "' AND YearMonth = '" + tbMillMonth.Text + "'";
            _dbMan4.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan4.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan4.ExecuteInstruction();

            DataTable dtcrews = _dbMan4.ResultsDataTable;

            lbGangs.DisplayMember = "IndustryNumber";
            lbGangs.ValueMember = "IndustryNumber";
            lbGangs.DataSource = dtcrews;
        }

        private void delGang_3Month()
        {
            MWDataManager.clsDataAccess _dbMan4 = new MWDataManager.clsDataAccess();
            _dbMan4.ConnectionString = _connection;
            _dbMan4.SqlStatement = "DELETE FROM [Mineware].[dbo].[tbl_BCS_Tramming_Gang_3Month] WHERE WorkingOrgUnit = '" + tbOrg.Text + "' AND YearMonth = '" + tbMillMonth.Text + "' AND IndustryNumber = '" + ind + "' AND Team = '" + team + "' AND Designation = '" + des + "'";
            _dbMan4.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan4.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan4.ExecuteInstruction();
        }

        public void delGang()
        {
            MWDataManager.clsDataAccess _dbMan4 = new MWDataManager.clsDataAccess();
            _dbMan4.ConnectionString = _connection;
            _dbMan4.SqlStatement = "DELETE FROM [Mineware].[dbo].[tbl_BCS_Tramming_Gang] WHERE WorkingOrgUnit = '" + tbOrg.Text + "' AND YearMonth = '" + tbMillMonth.Text + "' AND IndustryNumber = '" + ind + "' AND Team = '" + team + "' AND Designation = '" + des + "'";
            _dbMan4.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan4.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan4.ExecuteInstruction();

            alertControl1.Show(null, "Information", val + " Deleted Successfully");
        }
        private void frmTrammingProp_Load(object sender, EventArgs e)
        {
            this.Icon = frmMain.ActiveForm.Icon;
            loadData();
            lbfill();

            luDesignation.Properties.DataSource = "";
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Designation");
            dt1.Rows.Add("Hoppers");
            dt1.Rows.Add("Loco Driver");
            dt1.Rows.Add("Loco Driver:Guard");
            dt1.Rows.Add("Loco Driver:Driver");
            dt1.Rows.Add("Loader Driver");
            dt1.Rows.Add("Transport Team Leader");
            dt1.Rows.Add("Transport General");
            dt1.Rows.Add("Transport Loco Driver");
            luDesignation.Properties.DataSource = dt1;

        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void luTeamGroup_Properties_EditValueChanged(object sender, EventArgs e)
        {
            if (luTeamGroup.EditValue.ToString() == "Hopper")
            {
                luTeam.Properties.DataSource = "";
                DataTable dt0 = new DataTable();
                dt0.Columns.Add("Team");
                dt0.Rows.Add("A");
                dt0.Rows.Add("B");
                dt0.Rows.Add("C");
                dt0.Rows.Add("D");
                dt0.Rows.Add("E");
                dt0.Rows.Add("F");
                dt0.Rows.Add("G");
                luTeam.Properties.DataSource = dt0;
            }

            if (luTeamGroup.EditValue.ToString() == "Loader Driver")
            {
                luTeam.Properties.DataSource = "";
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("Team");
                dt1.Rows.Add("L");
                luTeam.Properties.DataSource = dt1;
            }

            if (luTeamGroup.EditValue.ToString() == "Team Leader")
            {
                luTeam.Properties.DataSource = "";
                DataTable dt2 = new DataTable();
                dt2.Columns.Add("Team");
                dt2.Rows.Add("T");
                luTeam.Properties.DataSource = dt2;
            }

            if (luTeamGroup.EditValue.ToString() == "Transport")
            {
                luTeam.Properties.DataSource = "";
                DataTable dt3 = new DataTable();
                dt3.Columns.Add("Team");
                dt3.Rows.Add("R");
                luTeam.Properties.DataSource = dt3;
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            this.Close();
        }

        private void luTeamGroup_EditValueChanged(object sender, EventArgs e)
        {


        }

        private void luEmpDetail_EditValueChanged(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _dbMan3 = new MWDataManager.clsDataAccess();
            _dbMan3.ConnectionString = _connection;
            _dbMan3.SqlStatement = "SELECT DISTINCT Designation, Orgunit FROM [Mineware].[dbo].[tbl_Import_BCS_Personnel_Latest] WHERE IndustryNumber = '" + luEmpDetail.Text.Substring(0, 8) + "'";
            _dbMan3.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan3.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan3.ExecuteInstruction();

            DataTable dtDesignation = _dbMan3.ResultsDataTable;
            //luDesignation.Properties.DataSource = dtDesignation;
            var result = dtDesignation.Rows[0][1];
            tbOrgUnitOriginal.Text = result.ToString();
            //luDesignation.ItemIndex = 0; = 

            DataTable dt = _dbMan3.ResultsDataTable;
            tbDesignation.Text = dt.Rows[0][0].ToString();
        }


        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblGangMembers_Click(object sender, EventArgs e)
        {

        }

        private void sbtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                
                insertValue();
                insertValue_3Month();
                lbfill();

                luEmpDetail.SelectedText = "";
                luDesignation.SelectedText = "";
                luDescription.SelectedText = "";
                luTeam.SelectedText = "";
                alertControl1.Show(null, "Information", "Record Added Successfully");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        public string val = "";
        private string ind;
        private string team;
        private string des;

        private void lbGangs_DoubleClick(object sender, EventArgs e)
        {
            val = lbGangs.SelectedValue.ToString();

            ind = val.Split('-')[0];
            des = val.Split('-')[1];
            team = val.Split('-')[2];

            var message = "Are you sure you want to delete?";
            var caption = "Delete Notification";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Information;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons, icon);

            if (result == DialogResult.Yes)
            {
                delGang();
                delGang_3Month();
                lbfill();
            }
            else if (result == DialogResult.No)
            {

            }

        }

        private void luDescription_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void luDesignation_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void luDesignation_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
