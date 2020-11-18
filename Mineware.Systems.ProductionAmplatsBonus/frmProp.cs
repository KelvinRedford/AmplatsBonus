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
using System.IO;
using DevExpress.XtraEditors;

namespace Mineware.Systems.ProductionAmplatsBonus
{
    public partial class frmProp : XtraForm
    {
        public string _connection;
        public frmProp()
        {
            InitializeComponent();
        }

        
        public frmMain frmMain;

     

        public frmProp(frmMain _frmMain)
        {
            InitializeComponent();
            frmMain = _frmMain;
        }

        string Locked;
        string MyID = "";

        private void frmProp_Load(object sender, EventArgs e)
        {


            if (Text == "Edit Gang Member")
            {
                IndNumTxt.ReadOnly = false;

                TramPnl.Visible = true;
                TramPnl.Dock = DockStyle.Fill;
                DelGangBtn.Visible = true;


                DefDaysTxt.Text = lblHoppersTramEdit.Text;

                for (int i = 0; i < TeamGroupCmb.Items.Count; i++)
                {
                    if (TeamGroupCmb.Items[i].ToString() == lblTeamGroupTramEdit.Text)
                    {
                        TeamGroupCmb.SelectedIndex = i;
                    }
                }

                for (int i = 0; i < TeamCmb.Items.Count; i++)
                {
                    if (TeamCmb.Items[i].ToString() == lblTeamTramEdit.Text)
                    {
                        TeamCmb.SelectedIndex = i;
                    }
                }

                //  MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                //_dbMan1.ConnectionString = _connection;
                //_dbMan1.SqlStatement = " \r\n " +
                //                        "select * from tbl_BCS_Tramming_Gang \r\n " +
                //                        "where yearmonth <> '2'\r\n " +
                //                        "and industryNumber = '" + IndNoLbl.Text + "' \r\n " +
                //                        "and OrgUnit = '" + OrgUnitLbl.Text + "' \r\n " +
                //                        "and TypeShift = '" + ShiftLbl.Text + "' \r\n " +
                //                        "and Level = '" + LvlLbl.Text + "' \r\n " +
                //                        "and date = '" + DateLbl.Text + "' \r\n ";

                //_dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                //_dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                //_dbMan1.ExecuteInstruction();

                //DataTable dt1 = _dbMan1.ResultsDataTable;


                //IndNumTxt.Text = dt1.Rows[0]["IndustryNumber"].ToString();
              //  DesignationTxt.Text = dt1.Rows[0]["Designation"].ToString();

                //if (dt1.Rows[0]["Attendance"].ToString() == "Y")
                //{
                //    AttRG.SelectedIndex = 0;
                   
                //}
                //else
                //{
                //    AttRG.SelectedIndex = 1;
                    
                //}

                /////Load TeamGroup Combo

                //MWDataManager.clsDataAccess _dbManTG = new MWDataManager.clsDataAccess();
                //_dbManTG.ConnectionString = _connection;
                //_dbManTG.SqlStatement = " \r\n " +
                //                        "Select Team_Description from BMCS_Tramming_MaxInGang\r\n " +
                //                         " where yearmonth = '201602' \r\n ";


                //_dbManTG.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                //_dbManTG.queryReturnType = MWDataManager.ReturnType.DataTable;
                //_dbManTG.ExecuteInstruction();

                //DataTable dtTG = _dbManTG.ResultsDataTable;

                //foreach (DataRow drTG in dtTG.Rows)
                //{
                //    TeamGroupCmb.Items.Add(drTG["Team_Description"].ToString());
                //}

               // TeamGroupCmb.SelectedItem = dt1.Rows[0]["TeamGroup"].ToString();

               

              //  TeamCmb.SelectedItem = dt1.Rows[0]["Team"].ToString();

                //HoppersTxt.Text = dt1.Rows[0]["Hoppers"].ToString();

             //   if (dt1.Rows[0]["Added"].ToString() == "Y")
             //   {
            //        AddRG.SelectedIndex = 0;
                    
           //     }
           //     else
           //     {
                    AddRG.SelectedIndex = 1;
          //          
          //      }




            }

            if (Text == "Add Gang Member")
            {

                TramPnl.Visible = true;
                TramPnl.Dock = DockStyle.Fill;
                DelGangBtn.Visible = true;

                //  MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                //_dbMan1.ConnectionString = _connection;
                //_dbMan1.SqlStatement = " \r\n " +
                //                        "select * from tbl_BCS_Tramming_Gang \r\n " +
                //                        "where yearmonth <> '2'\r\n " +
                //                        "and industryNumber = '" + IndNoLbl.Text + "' \r\n " +
                //                        "and OrgUnit = '" + OrgUnitLbl.Text + "' \r\n " +
                //                        "and TypeShift = '" + ShiftLbl.Text + "' \r\n " +
                //                        "and Level = '" + LvlLbl.Text + "' \r\n " +
                //                        "and date = '" + DateLbl.Text + "' \r\n ";

                //_dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                //_dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                //_dbMan1.ExecuteInstruction();

                //DataTable dt1 = _dbMan1.ResultsDataTable;


                //IndNumTxt.Text = dt1.Rows[0]["IndustryNumber"].ToString();
                //  DesignationTxt.Text = dt1.Rows[0]["Designation"].ToString();

                //if (dt1.Rows[0]["Attendance"].ToString() == "Y")
                //{
                //    AttRG.SelectedIndex = 0;

                //}
                //else
                //{
                //    AttRG.SelectedIndex = 1;

                //}

                /////Load TeamGroup Combo

                //MWDataManager.clsDataAccess _dbManTG = new MWDataManager.clsDataAccess();
                //_dbManTG.ConnectionString = _connection;
                //_dbManTG.SqlStatement = " \r\n " +
                //                        "Select Team_Description from BMCS_Tramming_MaxInGang\r\n " +
                //                         " where yearmonth = '201602' \r\n ";


                //_dbManTG.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                //_dbManTG.queryReturnType = MWDataManager.ReturnType.DataTable;
                //_dbManTG.ExecuteInstruction();

                //DataTable dtTG = _dbManTG.ResultsDataTable;

                //foreach (DataRow drTG in dtTG.Rows)
                //{
                //    TeamGroupCmb.Items.Add(drTG["Team_Description"].ToString());
                //}

                // TeamGroupCmb.SelectedItem = dt1.Rows[0]["TeamGroup"].ToString();



                //  TeamCmb.SelectedItem = dt1.Rows[0]["Team"].ToString();

                //HoppersTxt.Text = dt1.Rows[0]["Hoppers"].ToString();

                //   if (dt1.Rows[0]["Added"].ToString() == "Y")
                //   {
                //        AddRG.SelectedIndex = 0;

                //     }
                //     else
                //     {
          //AddRG.SelectedIndex = 1;
                //          
                //      }




            }


            if (Text == "Setup Users")
            {
                pnlSysAdmin.Visible = true;
                pnlSysAdmin.Dock = DockStyle.Fill;

                MoSecLB.Items.Clear();

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = _connection;
                _dbMan1.SqlStatement = "select * from mineware.dbo.tbl_BCS_SECTION \r\n " +
                                       "where Prodmonth = (select CurrentProductionMonth from mineware.dbo.tbl_SYSSET) \r\n " +
                                       "and Hierarchicalid = 4";
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ExecuteInstruction();

                DataTable dt1 = _dbMan1.ResultsDataTable;

               
                foreach (DataRow dr1 in dt1.Rows)
                {
                    MoSecLB.Items.Add(dr1["SectionID"].ToString());
                }



                if (UserLbl.Text != "0")
                {

                    MWDataManager.clsDataAccess _dbManUsers = new MWDataManager.clsDataAccess();
                    _dbManUsers.ConnectionString = _connection;
                    _dbManUsers.SqlStatement = "  select * from mineware.dbo.tbl_BCS_Users " +
                                               " where username = '" + UserLbl.Text + "' ";

                    _dbManUsers.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManUsers.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManUsers.ExecuteInstruction();

                    DataTable dtEdit = _dbManUsers.ResultsDataTable;

                    txtUserName.Text = dtEdit.Rows[0]["Username"].ToString();


                    txtName.Text = dtEdit.Rows[0]["name"].ToString();
                    txtPassword.Text = dtEdit.Rows[0]["Password"].ToString();
                    txtConfirmPass.Text = dtEdit.Rows[0]["ConfirmPassword"].ToString();

                    ExpiryDateTxt.Text = dtEdit.Rows[0]["expiryDate"].ToString();

                    MoSecLB.Text = dtEdit.Rows[0]["safety"].ToString();


                    if (dtEdit.Rows[0]["safety"].ToString() == "Y")
                    {
                        SafetyCbx.Checked = true;
                    }


                    if (dtEdit.Rows[0]["shiftCapt"].ToString() == "Y")
                    {
                        ShifCaptureCbx.Checked = true;
                    }

                    if (dtEdit.Rows[0]["MonthParamEng"].ToString() == "Y")
                    {
                        MonthParamEngCbx.Checked = true;
                    }

                    if (dtEdit.Rows[0]["Mining"].ToString() == "Y")
                    {
                        MiningCbx.Checked = true;
                    }

                    if (dtEdit.Rows[0]["MiningCrewBonus"].ToString() == "Y")
                    {
                        MineCrewBonusCbx.Checked = true;
                    }

                    if (dtEdit.Rows[0]["SBBonusCalc"].ToString() == "Y")
                    {
                        SBBonusCalcCbx.Checked = true;
                    }


                    if (dtEdit.Rows[0]["BonusEng"].ToString() == "Y")
                    {
                        EngBonusCalsCbx.Checked = true;
                    }

                    if (dtEdit.Rows[0]["Users"].ToString() == "Y")
                    {
                        UsersCbx.Checked = true;
                    }

                    if (dtEdit.Rows[0]["General"].ToString() == "Y")
                    {
                        GenCbx.Checked = true;
                    }

                    if (dtEdit.Rows[0]["TrammingCapt"].ToString() == "Y")
                    {
                        ceTramming.Checked = true;
                    }

                    if (dtEdit.Rows[0]["ProductionCapt"].ToString() == "Y")
                    {
                        ceProduction.Checked = true;
                    }

                }


    

            }


            return;


            if (Text == "System Users")
            {
                pnlSysAdmin.Visible = true;
                pnlSysAdmin.Dock = DockStyle.Fill;

                               
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = _connection;                
                _dbMan.SqlStatement = "select ProfileDesc from dbo.BMCS_Profile ";
                
                if(clsUserInfo.SuperUser == "N")
                    _dbMan.SqlStatement = _dbMan.SqlStatement + " where ProfileID <> '14'";

                _dbMan.SqlStatement = _dbMan.SqlStatement + " order by ProfileDesc";
                
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ExecuteInstruction();

                DataTable dt = _dbMan.ResultsDataTable;

                foreach (DataRow dr in dt.Rows)
                {
                    //cmbProfileID.Items.Add(dr["ProfileDesc"]);
                }

                
                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = _connection;
                _dbMan1.SqlStatement = "select * from mineware.dbo.tbl_Section \r\n "+
                                       "where Prodmonth = (select CurrentProductionMonth from mineware.dbo.tbl_SysSet) \r\n " +
                                       "and Hierarchicalid = 4";
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ExecuteInstruction();

                DataTable dt1 = _dbMan1.ResultsDataTable;

                //cmbMO.Items.Add("");
                //foreach (DataRow dr1 in dt1.Rows)
                //{
                //    cmbMO.Items.Add(dr1["SectionID"]);
                //}

            }
            else if (Text == "Profile Groups")
            {
                pnlSecurity.Visible = true;
                pnlSecurity.Dock = DockStyle.Fill;

                //if (lblEdit.Text == "Y")
                //    txtDesc.Enabled = false; 

                //if (clsUserInfo.SuperUser == "Y")
                //    cbxSuperUser.Enabled = true;

            }
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LogonBtn_Click(object sender, EventArgs e)
        {
            //System Users
            #region
            if (Text == "System Users")
            {
                if (lblEdit.Text != "Y")
                {
                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = _connection;
                    _dbMan.SqlStatement = "select * from BMCS_Users " +
                                           "where UserID = '" + txtUserName.Text + "' ";
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ExecuteInstruction();

                
                    if (_dbMan.ResultsDataTable.Rows.Count != 0)
                    {
                        MessageBox.Show("The User Name already exists", "User already exists", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        txtUserName.Focus();
                        return;
                    }
                }

                if (txtUserName.Text == "")
                {
                    MessageBox.Show("Please enter a UserName", "No UserName", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    txtUserName.Focus();
                    return;
                }

                if (txtPassword.Text == "")
                {
                    MessageBox.Show("Please enter a Password", "No Password", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    txtPassword.Focus();
                    return;
                }

                if (txtConfirmPass.Text == "")
                {
                    MessageBox.Show("Please Confirm Password", "No Password", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    txtConfirmPass.Focus();
                    return;
                }

                //if (cmbProfileID.Text == "")
                //{
                //    MessageBox.Show("Please select a profile", "No Profile", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //    cmbProfileID.DroppedDown = true;
                //    return;
                //}                

                if (txtPassword.Text != txtConfirmPass.Text)
                {
                    MessageBox.Show("Your Password does not match your Confirm Password", "Incorrect Password", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    txtPassword.Focus();
                    return;
                }

                if (cbxLocked.Checked == true)
                    Locked = "Y";
                else
                    Locked = "N";


                //MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                //_dbMan1.ConnectionString = _connection;
                //_dbMan1.SqlStatement = "Delete BMCS_Users where userid = '" + txtUserName.Text+ "'  insert into BMCS_Users values ( '" + txtUserName.Text + "', '" + txtPassword.Text + "', '" + txtName.Text + "', \r\n " +
                //                        " (select ProfileID from BMCS_Profile where ProfileDesc = '"+cmbProfileID.Text+"'), null, '" + String.Format("{0:yyyy-MM-dd}", ExpiryDate.Value) + "', \r\n  " +
                //                        " '" + Locked + "', '" + cmbMO.Text + "' )  ";
                //_dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                //_dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                //_dbMan1.ExecuteInstruction();

                frmMessage MsgFrm = new frmMessage();
                MsgFrm.Text = "Saved";
                MsgFrm.Text = "Saved Successfully";
                MsgFrm.Show();

                
                Close();

            }
            #endregion

            //Profile Groups
            #region
            if (Text == "Profile Groups")
            {
                //if (txtDesc.Text == "")
                //{
                //    MessageBox.Show("Please enter a profile group", "Incorrect Profile", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //    txtDesc.Focus();
                //    return;
                //}



                if (lblEdit.Text != "Y")
                {
                    //MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
                    //_dbMan2.ConnectionString = _connection;
                    //_dbMan2.SqlStatement = "select * from BMCS_Profile where profiledesc = '" + txtDesc.Text + "' ";
                    //_dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    //_dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
                    //_dbMan2.ExecuteInstruction();

                    //if (_dbMan2.ResultsDataTable.Rows.Count > 0)
                    //{
                    //    MessageBox.Show("The Profile Group already exists", "Incorrect Profile", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //   // txtDesc.Focus();
                    //    return;
                    //}

                    MWDataManager.clsDataAccess _dbMan4 = new MWDataManager.clsDataAccess();
                    _dbMan4.ConnectionString = _connection;
                    _dbMan4.SqlStatement = "select MAX(ProfileID) + 1 NextID from BMCS_Profile ";
                    _dbMan4.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan4.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan4.ExecuteInstruction();

                    MyID = _dbMan4.ResultsDataTable.Rows[0][0].ToString();
                                        
                }
                else
                {
                    //MWDataManager.clsDataAccess _dbMan5 = new MWDataManager.clsDataAccess();
                    //_dbMan5.ConnectionString = _connection;
                    //_dbMan5.SqlStatement = "select ProfileID from BMCS_Profile where ProfileDesc = '"+txtDesc.Text+"' ";
                    //_dbMan5.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    //_dbMan5.queryReturnType = MWDataManager.ReturnType.DataTable;
                    //_dbMan5.ExecuteInstruction();

                    //MyID = _dbMan5.ResultsDataTable.Rows[0][0].ToString();
                }


                string SuperUser = "N";
                string Sys = "N";
                string Mining = "N";
                string Eng = "N";
                string Att = "N";
                string Report1 = "N";
                string Safety = "N";
                string Transfer = "N";                

                //if (cbxSuperUser.Checked == true)
                //    SuperUser = "Y";
                //if (cbxSysAdmin.Checked == true)
                //    Sys = "Y";
                //if (cbxMining.Checked == true)
                //    Mining = "Y";
                //if (cbxEng.Checked == true)
                //    Eng = "Y";
                //if (cbxAtt.Checked == true)
                //    Att = "Y";
                //if (cbxRep.Checked == true)
                //    Report1 = "Y";
                //if (cbxSafety.Checked == true)
                //    Safety = "Y";
                //if (cbxTrans.Checked == true)
                //    Transfer = "Y";

                //MWDataManager.clsDataAccess _dbMan3 = new MWDataManager.clsDataAccess();
                //_dbMan3.ConnectionString = _connection;
                //_dbMan3.SqlStatement = "Delete BMCS_Profile where ProfileDesc = '" + txtDesc.Text + "' \r\n "+ 
                //                       "insert into BMCS_Profile (ProfileDesc, SystemAdmin, StopingDailyShiftReturns, \r\n "+
                //                       "DevDailyShiftReturns, StopingCalcSheets, DevCalcSheets,  \r\n "+
                //                       "Reports, MOInput, MOCalcSheets, Eng, MOView, SweepDSR,  \r\n "+
                //                       "SweepCS, TramDSR, TramCS, SurveyMeas, EngView, TramView, SuperUser) \r\n " +
                //                       "values ('" + txtDesc.Text + "', \r\n " + //'" + MyID + "',
                //                       "'" + Sys + "', '" + Safety + "', 'N', '"+Mining+"', 'N', \r\n " +
                //                       "'" + Report1 + "', 'N', 'N', \r\n  " +
                //                       "'" + Eng + "', '" + Transfer + "', 'N', 'N', 'N', 'N', '" + Att + "', 'N', 'N', '"+SuperUser+"' )  ";
                //_dbMan3.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                //_dbMan3.queryReturnType = MWDataManager.ReturnType.DataTable;
                //_dbMan3.ExecuteInstruction();


                frmMessage MsgFrm = new frmMessage();
                MsgFrm.Text = "Saved";
                MsgFrm.Text = "Saved Successfully";
                MsgFrm.Show();

                
                Close();
            }
            #endregion


        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pnlSecurity_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Close1Btn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private byte[] encodeStringToBytes(string dataToEncode)
        {
            List<byte> bytes = new List<byte>();
            foreach (char character in dataToEncode.ToCharArray())
            {
                bytes.Add((byte)character);
            }
            return bytes.ToArray();
        }

        private string encodeBytesToString(byte[] dataToEncode)
        {
            String encodedString = "";
            foreach (byte bite in dataToEncode)
            {
                encodedString = encodedString + (char)bite;
            }
            return encodedString;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            string Attendance = "";
            string Section = "";

            if (AttRG.SelectedIndex == 0)
            {
                Attendance = "Y";
            }

            if (AttRG.SelectedIndex == 1)
            {
                Attendance = "N";
            }



            Section = OrgUnitLbl.Text+"              ".Substring(0, 4);



            if (Text == "Edit Gang Member  exclude")
            {
                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = _connection;
                _dbMan1.SqlStatement = "Update tbl_BCS_Tramming_Gang_3Month\r\n " +
                                       " Set Attendance = '" + Attendance + "' ,TeamGroup = '" + TeamGroupCmb.Text + "'    \r\n" +
                                       " ,Team = '" + TeamCmb.Text + "' ,Hoppers = '" + DefDaysTxt.Text + "'    \r\n" +


                                       " where  ID = '" + IDLbl.Text + "' and YearMonth = '" + MonthLbl.Text + "' \r\n"+
                                       " and [Date] = '" + DateLbl.Text + "' and IndustryNumber = '" + IndNumTxt.Text + "'  \r\n"+
                                       " and OrgUnit = '" + OrgUnitLbl.Text + "' and WorkingOrgUnit = '" + OrgUnitLbl.Text + "'  \r\n"+
                                       " and Section = '" + Section + "' and [Level] = '" + LvlLbl.Text + "' ";
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                //_dbMan1.ExecuteInstruction();

                frmMessage MsgFrm = new frmMessage();
                MsgFrm.Text = "Saved";
                MsgFrm.Text = "Saved Successfully";
                MsgFrm.Show();

                this.Close();
            }

            if (Text == "Add Gang Member  exclude")
            {
                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = _connection;
                _dbMan1.SqlStatement = "insert into  tbl_BCS_Tramming_Gang_3Month \r\n"+
                                        "(ID,YearMonth,[Date]  \r\n"+
                                        ",IndustryNumber,OrgUnit,WorkingOrgUnit   \r\n"+
                                        ",Attendance,TeamGroup,Team   \r\n"+
                                        ",Hoppers,Section,[Level]   \r\n"+
                                        ",SystemUser,[TimeStamp],TypeShift)   \r\n"+
                                    "Values (    \r\n"+
                                    "'" + IDLbl.Text + "','" + MonthLbl.Text + "','" + DateLbl.Text + "'    \r\n" +
                                    ",'" + IndNumTxt.Text + "','" + OrgUnitLbl.Text + "','" + OrgUnitLbl.Text + "'   \r\n" +
                                    ",'" + Attendance + "','" + TeamGroupCmb.Text + "','" + TeamCmb.Text + "'   \r\n" +
                                    ",'" + DefDaysTxt.Text + "','" + Section + "','" + LvlLbl.Text + "'   \r\n" +
                                    ",'aaaa',GetDate(),'" + ShiftLbl.Text + "'   \r\n" +
                                    ")";
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
               // _dbMan1.ExecuteInstruction();

                frmMessage MsgFrm = new frmMessage();
                MsgFrm.Text = "Saved";
                MsgFrm.Text = "Saved Successfully";
                MsgFrm.Show();


                this.Close();
            }




            if (txtUserName.Text == "")
            {
                MessageBox.Show("Please enter a user name.", "UserName is Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUserName.Focus();
                return;
            }

            if (txtPassword.Text == "")
            {
                MessageBox.Show("Please enter a password.", "Password is Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPassword.Focus();
                return;
            }

            if (txtConfirmPass.Text == "")
            {
                MessageBox.Show("Please confirm password.", "Password Confirmation is Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtConfirmPass.Focus();
                return;
            }

            if (txtName.Text == "")
            {
                MessageBox.Show("Please enter a name", "Name is Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtName.Focus();
                return;
            }

            if (MoSecLB.SelectedIndex < 0)
            {
                MessageBox.Show("Please select an MO Section.", "Mo Section is Blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                MoSecLB.Focus();
                return;
            }

            String safety = "N";
            if (SafetyCbx.Checked == true)
            {
                safety = "Y";
            }

            String shiftCapture = "N";
            if (ShifCaptureCbx.Checked == true)
            {
                shiftCapture = "Y";
            }

            String TrammingCapt = "N";
            if (ceTramming.Checked == true)
            {
                TrammingCapt = "Y";
            }

            String ProductionCapt = "N";
            if (ceProduction.Checked == true)
            {
                ProductionCapt = "Y";
            }

            String MonthParamEng = "N";
            if (MonthParamEngCbx.Checked == true)
            {
                MonthParamEng = "Y";
            }
            
            String Mining = "N";
            if (MiningCbx.Checked == true)
            {
                Mining = "Y";
            }

            String MiningCrewBonus = "N";
            if (MineCrewBonusCbx.Checked == true)
            {
                MiningCrewBonus = "Y";
            }

            String SBBonusCalc = "N";
            if (SBBonusCalcCbx.Checked == true)
            {
                SBBonusCalc = "Y";
            }

            String EngBonusCalc = "N";
            if (EngBonusCalsCbx.Checked == true)
            {
                EngBonusCalc = "Y";
            }

            String users = "N";
            if (UsersCbx.Checked == true)
            {
                users = "Y";
            }

            String gen = "N";
            if (GenCbx.Checked == true)
            {
                gen = "Y";
            }

            if (EditLbl.Text == "Y")
            {

                if (txtPassword.Text != txtConfirmPass.Text)
                {
                    MessageBox.Show("Passwords dont match", "Password confirmation Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtConfirmPass.Focus();
                    return;
                }

                PassTxt.Text = Encoding.Unicode.GetString(encodeStringToBytes(txtPassword.Text), 0, encodeStringToBytes(txtPassword.Text).Length);

                MWDataManager.clsDataAccess _dbMan11 = new MWDataManager.clsDataAccess();
                _dbMan11.ConnectionString = _connection;
                _dbMan11.SqlStatement = " delete from mineware.dbo.tbl_BCS_Users where username = '" + txtUserName.Text + "' ";

                _dbMan11.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan11.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan11.ExecuteInstruction();

                DataTable dt = _dbMan11.ResultsDataTable;

                

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = _connection;
                _dbMan1.SqlStatement = "insert into  mineware.dbo.tbl_BCS_Users \r\n " +
                                       " values ( '" + txtUserName.Text + "', '" + txtPassword.Text + "', '" + txtConfirmPass.Text + "',  \r\n " +
                                       " '" + txtName.Text + "', '" + ExpiryDateTxt.Text + "', '" + MoSecLB.SelectedItem.ToString() + "', " +
                                       " '" + safety + "', '" + shiftCapture + "', '" + MonthParamEng + "', " +
                                       " '" + Mining + "','" + MiningCrewBonus + "','" + SBBonusCalc + "', " +
                                       " '" + EngBonusCalc + "', '" + users + "', '" + gen + "' , '" + TrammingCapt + "' , '" + ProductionCapt + "')";
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ExecuteInstruction();

                frmMessage MsgFrm = new frmMessage();
                MsgFrm.Text = "Saved";
                MsgFrm.Text = "Saved Successfully";
                MsgFrm.Show();

                //frmMain.LoadUsersGrid();
                Close();
            }
            else
            {

                if (txtPassword.Text != txtConfirmPass.Text)
                {
                    MessageBox.Show("Passwords dont match", "Password confirmation Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtConfirmPass.Focus();
                    return;
                }

                //PassTxt.Text = Encoding.Unicode.GetString(encodeStringToBytes(txtPassword.Text), 0, encodeStringToBytes(txtPassword.Text).Length);

                MWDataManager.clsDataAccess _dbMan11 = new MWDataManager.clsDataAccess();
                _dbMan11.ConnectionString = _connection;
                _dbMan11.SqlStatement = " select * from mineware.dbo.tbl_BCS_Users where username = '" + txtUserName.Text + "' ";

                _dbMan11.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan11.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan11.ExecuteInstruction();

                DataTable dt = _dbMan11.ResultsDataTable;

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Username already Exists", "Unable to Add user.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUserName.Focus();
                    return;
                }

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = _connection;
                _dbMan1.SqlStatement = "insert into  mineware.dbo.tbl_BCS_Users \r\n " +
                                       " values ( '" + txtUserName.Text + "', '" + txtPassword.Text + "', '" + txtConfirmPass.Text + "',  \r\n " +
                                       " '" + txtName.Text + "', '" + ExpiryDateTxt.Text + "', '" + MoSecLB.SelectedItem.ToString() + "', " +
                                       " '" + safety + "', '" + shiftCapture + "', '" + MonthParamEng + "', " +
                                       " '" + Mining + "','" + MiningCrewBonus + "','" + SBBonusCalc + "', " +
                                       " '" + EngBonusCalc + "', '" + users + "', '" + gen + "' ,'" + TrammingCapt + "' ,'" + ProductionCapt + "')";
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ExecuteInstruction();

                frmMessage MsgFrm = new frmMessage();
                MsgFrm.Text = "Saved";
                MsgFrm.Text = "Saved Successfully";
                MsgFrm.Show();

                //frmMain.LoadUsersGrid();
                Close();
            }

        }

        private void TramPnl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TeamGroupCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TeamGroupCmb.SelectedIndex == 0)
            {
                TeamCmb.Items.Clear();
                TeamCmb.Items.Add("A");
                TeamCmb.Items.Add("B");
                TeamCmb.Items.Add("C");
                TeamCmb.Items.Add("D");
                TeamCmb.Items.Add("E");
                TeamCmb.Items.Add("F");
                TeamCmb.Items.Add("G");
            }

            if (TeamGroupCmb.SelectedIndex == 1)
            {
                TeamCmb.Items.Clear();
                TeamCmb.Items.Add("L");
                TeamCmb.SelectedIndex = 0;                
            }

            if (TeamGroupCmb.SelectedIndex == 2)
            {
                TeamCmb.Items.Clear();
                TeamCmb.Items.Add("T");
                TeamCmb.SelectedIndex = 0;
            }

            if (TeamGroupCmb.SelectedIndex == 3)
            {
                TeamCmb.Items.Clear();
                TeamCmb.Items.Add("R");
                TeamCmb.SelectedIndex = 0;
            }
        }

        private void IndNumTxt_TextChanged(object sender, EventArgs e)
        {            
               MWDataManager.clsDataAccess _dbMan11 = new MWDataManager.clsDataAccess();
                _dbMan11.ConnectionString = _connection;
                _dbMan11.SqlStatement = "select * from [dbo].[tbl_Import_BCS_Personnel_Latest] "+
                                        " where industrynumber = '" + IndNumTxt.Text + "' "+
                                        " and enddate >= '" + DateLbl.Text + "' ";  
                _dbMan11.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan11.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan11.ExecuteInstruction();

                if (_dbMan11.ResultsDataTable.Rows.Count > 0)
                {
                    Nametxt.Text = _dbMan11.ResultsDataTable.Rows[0]["surname"].ToString() + ". " + _dbMan11.ResultsDataTable.Rows[0]["initials"].ToString();
                    DesignationTxt.Text = _dbMan11.ResultsDataTable.Rows[0]["designation"].ToString();
                }

        }


    }
}
