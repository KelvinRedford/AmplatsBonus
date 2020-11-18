using System;
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
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.ProductionAmplatsBonus
{
    public partial class ucSBShifts : BaseUserControl
    {
        public ucSBShifts()
        {
            InitializeComponent();
        }

        Report theReport = new Report();

        void LoadMO()
        {
            

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            if (rdbStoping.Checked == true)
            {

                _dbMan.SqlStatement = " select distinct(SUBSTRING(OrgUnit,1,4)) MO from tbl_BCS_StopingRepNew \r\n " +
                                      " where ProdMonth = '" + ProdMonthTxt.Value + "' \r\n " +
                                      " order by MO ";
            }
            if (rdbDev.Checked == true)
            {
                _dbMan.SqlStatement = " select distinct(SUBSTRING(OrgUnit,1,4)) MO from tbl_BCS_DevRepNew \r\n " +
                                     " where ProdMonth = '" + ProdMonthTxt.Value + "' \r\n " +
                                     " order by MO ";
            }
            
    
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            DataTable dt = _dbMan.ResultsDataTable;

            cmbMO.Items.Clear();

            foreach (DataRow dr in dt.Rows)
            {
                cmbMO.Items.Add(dr["MO"].ToString());
            }

            if (dt.Rows.Count > 0)
            {
                cmbMO.SelectedIndex = 0;
            }
        }

        private void frmSBShifts_Load(object sender, EventArgs e)
        {
            ProdMonthTxt.Text = Convert.ToString(SysSettings.ProdMonth);
            Procedures procs = new Procedures();
            procs.ProdMonthVis(Convert.ToInt32(ProdMonthTxt.Text));
            ProdMonth1Txt.Text = Procedures.Prod2;
            ProdMonth1Txt.TextAlign = HorizontalAlignment.Center;

            if (Text == "Shift Boss Crew Achievements")
            {
                lblMO.Visible = false;
                cmbMO.Visible = false;
            }
            else if (Text == "Shift Boss Shift Sheets")
            {
                lblMO.Visible = true;
                cmbMO.Visible = true;
                LoadMO();
            }
        }

        private void ProdMonthTxt_Click(object sender, EventArgs e)
        {
            Procedures procs = new Procedures();
            procs.ProdMonthCalc(Convert.ToInt32(ProdMonthTxt.Text));
            ProdMonthTxt.Text = Procedures.Prod.ToString();
            procs.ProdMonthVis(Convert.ToInt32(ProdMonthTxt.Text));
            ProdMonth1Txt.Text = Procedures.Prod2;

            cmbMO_SelectedIndexChanged(null, null);
        }

        private void Close1Btn_Click(object sender, EventArgs e)
        {
            
        }

        private void showBtn_Click(object sender, EventArgs e)
        {
            // Do Crew Achievement
            #region
            if (Text == "Shift Boss Crew Achievements")
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                if (rdbStoping.Checked == true)
                {
                    _dbMan.SqlStatement = " select substring(OrgUnit,1,5) SB, orgunit, Production, BIP from dbo.tbl_BCS_StopingRepNew \r\n " +
                                          " where prodmonth = '" + ProdMonthTxt.Value + "' \r\n " +
                                          " order by orgunit ";
                }
                else
                {
                    _dbMan.SqlStatement = " select substring(OrgUnit,1,5) SB, orgunit, Production, BIP from dbo.tbl_BCS_DevRepNew \r\n " +
                                                              " where prodmonth = '" + ProdMonthTxt.Value + "' \r\n " +
                                                              " order by orgunit ";
                }
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Achievements";
                _dbMan.ExecuteInstruction();

                DataTable dt = _dbMan.ResultsDataTable;

                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show("There is no data for selected period.");
                    return;
                }

                grid.RowCount = 500;
                grid.ColumnCount = 13;

                //Blank the grid
                for (int R = 0; R < 500; R++) // Rows
                {
                    for (int C = 0; C < 13; C++)
                    {
                        grid.Rows[R].Cells[C].Value = "";
                        if (C == 12)
                            grid.Rows[R].Cells[C].Value = "0";
                    }
                }
                
                string MySB = dt.Rows[0][0].ToString();
                int MyCol = 2;
                int MyRow = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (MySB == dr["SB"].ToString())
                    {
                        if (MyCol == 2)
                            grid.Rows[MyRow].Cells[1].Value = dr["SB"].ToString();

                        grid.Rows[MyRow].Cells[MyCol].Value = dr["orgunit"].ToString();
                        grid.Rows[MyRow + 1].Cells[MyCol].Value = dr["Production"].ToString();
                        grid.Rows[MyRow + 2].Cells[MyCol].Value = dr["BIP"].ToString();

                        grid.Rows[MyRow].Cells[12].Value = "SB Total";
                        grid.Rows[MyRow + 1].Cells[12].Value = Convert.ToString(Convert.ToDecimal(grid.Rows[MyRow + 1].Cells[12].Value) + Convert.ToDecimal(dr["Production"].ToString()));
                        grid.Rows[MyRow + 2].Cells[12].Value = Convert.ToString(Convert.ToDecimal(grid.Rows[MyRow + 2].Cells[12].Value) + Convert.ToDecimal(dr["BIP"].ToString()));
                        grid.Rows[MyRow + 3].Cells[12].Value = "";

                        grid.Rows[MyRow].Cells[0].Value = "OrgUnit";
                        grid.Rows[MyRow + 1].Cells[0].Value = "Metres";
                        grid.Rows[MyRow + 2].Cells[0].Value = "Payment";

                        MyCol++;
                    }
                    else
                    {
                        MyCol = 2;
                        MyRow = MyRow + 4;
                        MySB = dr["SB"].ToString();

                        if (MyCol == 2)
                            grid.Rows[MyRow].Cells[1].Value = dr["SB"].ToString();

                        grid.Rows[MyRow].Cells[MyCol].Value = dr["orgunit"].ToString();
                        grid.Rows[MyRow + 1].Cells[MyCol].Value = dr["Production"].ToString();
                        grid.Rows[MyRow + 2].Cells[MyCol].Value = dr["BIP"].ToString();

                        grid.Rows[MyRow].Cells[12].Value = "SB Total";
                        grid.Rows[MyRow + 1].Cells[12].Value = Convert.ToString(Convert.ToDecimal(grid.Rows[MyRow + 1].Cells[12].Value) + Convert.ToDecimal(dr["Production"].ToString()));
                        grid.Rows[MyRow + 2].Cells[12].Value = Convert.ToString(Convert.ToDecimal(grid.Rows[MyRow + 2].Cells[12].Value) + Convert.ToDecimal(dr["BIP"].ToString()));
                        grid.Rows[MyRow + 3].Cells[12].Value = "";

                        grid.Rows[MyRow].Cells[0].Value = "OrgUnit";
                        grid.Rows[MyRow + 1].Cells[0].Value = "Metres";
                        grid.Rows[MyRow + 2].Cells[0].Value = "Payment";

                        MyCol++;
                    }
                }

                grid.RowCount = MyRow + 4;

                grid.Rows[grid.RowCount-1].Cells[12].Value = "";

                //write to datable
                DataTable dtNew = new DataTable();


                for (int R = 0; R < grid.RowCount; R++) // Rows
                {
                    dtNew.Rows.Add();
                }
                for (int C = 0; C < 13; C++)
                {
                    dtNew.Columns.Add();
                }


                for (int i = 0; i < dtNew.Rows.Count; i++)
                {
                    dtNew.Rows[i][0] = grid.Rows[i].Cells[0].Value.ToString();
                    dtNew.Rows[i][1] = grid.Rows[i].Cells[1].Value.ToString();
                    dtNew.Rows[i][2] = grid.Rows[i].Cells[2].Value.ToString();
                    dtNew.Rows[i][3] = grid.Rows[i].Cells[3].Value.ToString();
                    dtNew.Rows[i][4] = grid.Rows[i].Cells[4].Value.ToString();
                    dtNew.Rows[i][5] = grid.Rows[i].Cells[5].Value.ToString();
                    dtNew.Rows[i][6] = grid.Rows[i].Cells[6].Value.ToString();
                    dtNew.Rows[i][7] = grid.Rows[i].Cells[7].Value.ToString();
                    dtNew.Rows[i][8] = grid.Rows[i].Cells[8].Value.ToString();
                    dtNew.Rows[i][9] = grid.Rows[i].Cells[9].Value.ToString();
                    dtNew.Rows[i][10] = grid.Rows[i].Cells[10].Value.ToString();
                    dtNew.Rows[i][11] = grid.Rows[i].Cells[11].Value.ToString();
                    dtNew.Rows[i][12] = grid.Rows[i].Cells[12].Value.ToString();
                }


                DataTable dt2 = new DataTable();
                dt2.TableName = "Table2";
                dt2.Rows.Add();
                dt2.Columns.Add();
                dt2.Rows[0][0] = ProdMonthTxt.Value.ToString();

                
                DataSet ds = new DataSet();
                ds.Tables.Add(dtNew);
                theReport.RegisterData(ds);

                DataSet ds2 = new DataSet();
                ds2.Tables.Add(dt2);
                theReport.RegisterData(ds2);

                theReport.Load("SBAchievement.frx");

                //theReport.Design();

                pcReport.Clear();
                theReport.Prepare();
                theReport.Preview = pcReport;
                theReport.ShowPrepared();



            }
            #endregion


            // Do Shift Sheet
            #region
            if (Text == "Shift Boss Shift Sheets")
            {
                //Get Ind No's and build up a strings
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " select distinct(IndustryNumber) ind from tbl_BCS_Gangs \r\n "+
                                      " where ProdMonth = '"+ProdMonthTxt.Value.ToString()+"' \r\n "+
                                      " and Category = 'Shift Boss' \r\n " +
                                      " and orgunit like '"+cmbMO.Text+"%' ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "People";
                _dbMan.ExecuteInstruction();

                DataTable dt = _dbMan.ResultsDataTable;

                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show("There is no data for selected period.");
                    return;
                }

                string ind = "";
                foreach (DataRow dr in dt.Rows)
                {
                    ind = ind + @"'" + dr["ind"].ToString() + @"',";
                }
                ind = ind + @"''";


                //Get Ind No's and build up a strings
                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

                if(rdbStoping.Checked == true)
                {
                _dbMan1.SqlStatement = " select '"+ProdMonthTxt.Value+"' ProdMonth, '"+cmbMO.Text+"' MyMO, * from ( \r\n "+
                            " select IndustryNumber, OrgUnit, Shift, SUM(Work) Work, sum(Ab) Ab, MyName \r\n "+
                            " from (  \r\n "+
                            " select IndustryNumber, Date,  case when Shift = 'D' then 'Day Shift' else 'Night Shift' end as Shift, OrgUnit, Codes,  \r\n "+
                            " case when Work is null then 0 else Work end as work,  \r\n "+
                            " case when Ab is null then 0 else Ab end as Ab,  \r\n "+
                            " case when IndNo is null then IndustryNumber else IndNo end as IndNo,  \r\n "+
                            " case when TheDate is null then Date else TheDate end as TheDate, IndNo1, MyName \r\n "+
                            " from (   \r\n "+
                            " select IndustryNumber, Date, Shift, OrgUnit, Codes from tbl_BCS_Gangs \r\n "+
                            " where prodmonth = '"+ProdMonthTxt.Value+"' \r\n "+
                            " and orgunit like '"+cmbMO.Text+"%' \r\n "+
                            " and Category = 'Shift Boss' ) bcs \r\n "+
                            " left outer join \r\n "+
                            " (select [IndustryNumber] IndNo, TheDate, [LeaveFlag] [Leave Flag], \r\n "+
                            " case when [LeaveFlag] in('N','NA') then 1 else 0 end as Work, \r\n "+
                            " case when [LeaveFlag] in('A') then 1 else 0 end as Ab \r\n "+
                            " from dbo.Import_BMCS_Clocking_3Month \r\n "+
                            " where TheDate >= '"+String.Format("{0:yyyy-MM-dd}", BeginDate.Value)+"' \r\n "+
                            " and TheDate <= '" + String.Format("{0:yyyy-MM-dd}", EndDate.Value) + "'   \r\n " +
                            " and [IndustryNumber] in (" + ind + ") and [expectedatwork] = 'Y'  \r\n " +
                            " )sym on bcs.IndustryNumber = sym.IndNo and bcs.Date = sym.TheDate \r\n "+
                            " left outer join \r\n "+
                            " (select [Resource Reference] IndNo1, Surname+'.'+Initials MyName from ( \r\n "+
                            " select * from [DBSYMNHM-ZON].symnhm.[dbo].[Resource Information] \r\n "+
                            " where [Resource Reference] in (" + ind + ") \r\n " +
                            " and [End Date] >= '" + String.Format("{0:yyyy-MM-dd}", BeginDate.Value) + "' ) a \r\n " +
                            " left outer join \r\n "+
                            " (select * from [DBSYMNHM-ZON].symnhm.[dbo].[PER HRA Personal])b on a.[Resource Tag] = b.[Resource Tag] ) sym2 \r\n "+
                            " on bcs.IndustryNumber = sym2.IndNo1 \r\n "+
                            " ) w \r\n "+
                            " group by IndustryNumber, Shift, OrgUnit, MyName \r\n "+
                            " )Main \r\n "+
                            " left outer join  \r\n "+
                            " (select substring(OrgUnit,1,5) SB, SUM(DS_LTI) DS_LTI, SUM(NS_LTI) NS_LTI from tbl_BCS_StopingRepNew  \r\n "+
                            " where prodmonth = '" + ProdMonthTxt.Value + "'  \r\n " +
                            " and orgunit like '"+cmbMO.Text+"%'  \r\n "+
                            " group by substring(OrgUnit,1,5))q on Main.OrgUnit = q.SB  \r\n " +
                            " order by Shift, Orgunit, IndustryNumber, Work ";
                }
                else
                {
                    _dbMan1.SqlStatement = " select '"+ProdMonthTxt.Value+"' ProdMonth, '"+cmbMO.Text+"' MyMO, * from ( \r\n "+
                            " select IndustryNumber, OrgUnit, Shift, SUM(Work) Work, sum(Ab) Ab, MyName \r\n "+
                            " from (  \r\n "+
                            " select IndustryNumber, Date,  case when Shift = 'D' then 'Day Shift' else 'Night Shift' end as Shift, OrgUnit, Codes,  \r\n "+
                            " case when Work is null then 0 else Work end as work,  \r\n "+
                            " case when Ab is null then 0 else Ab end as Ab,  \r\n "+
                            " case when IndNo is null then IndustryNumber else IndNo end as IndNo,  \r\n "+
                            " case when TheDate is null then Date else TheDate end as TheDate, IndNo1, MyName \r\n "+
                            " from (   \r\n "+
                            " select IndustryNumber, Date, Shift, OrgUnit, Codes from tbl_BCS_Gangs \r\n "+
                            " where prodmonth = '"+ProdMonthTxt.Value+"' \r\n "+
                            " and orgunit like '"+cmbMO.Text+"%' \r\n "+
                            " and Category = 'Shift Boss' ) bcs \r\n "+
                            " left outer join \r\n "+
                            " (select [IndustryNumber] IndNo, TheDate, [LeaveFlag] [Leave Flag], \r\n "+
                            " case when [LeaveFlag] in('N','NA') then 1 else 0 end as Work, \r\n "+
                            " case when [LeaveFlag] in('A') then 1 else 0 end as Ab \r\n "+
                            " from dbo.Import_BMCS_Clocking_3Month \r\n "+
                            " where TheDate >= '"+String.Format("{0:yyyy-MM-dd}", BeginDate.Value)+"' \r\n "+
                            " and TheDate <= '" + String.Format("{0:yyyy-MM-dd}", EndDate.Value) + "'   \r\n " +
                            " and [IndustryNumber] in (" + ind + ") and [expectedatwork] = 'Y'  \r\n " +
                            " )sym on bcs.IndustryNumber = sym.IndNo and bcs.Date = sym.TheDate \r\n "+
                            " left outer join \r\n "+
                            " (select [Resource Reference] IndNo1, Surname+'.'+Initials MyName from ( \r\n "+
                            " select * from [DBSYMNHM-ZON].symnhm.[dbo].[Resource Information] \r\n "+
                            " where [Resource Reference] in (" + ind + ") \r\n " +
                            " and [End Date] >= '" + String.Format("{0:yyyy-MM-dd}", BeginDate.Value) + "' ) a \r\n " +
                            " left outer join \r\n "+
                            " (select * from [DBSYMNHM-ZON].symnhm.[dbo].[PER HRA Personal])b on a.[Resource Tag] = b.[Resource Tag] ) sym2 \r\n "+
                            " on bcs.IndustryNumber = sym2.IndNo1 \r\n "+
                            " ) w \r\n "+
                            " group by IndustryNumber, Shift, OrgUnit, MyName \r\n "+
                            " )Main \r\n "+
                            " left outer join  \r\n "+
                            " (select substring(OrgUnit,1,5) SB, SUM(DS_LTI) DS_LTI, SUM(NS_LTI) NS_LTI from tbl_BCS_DevRepNew  \r\n "+
                            " where prodmonth = '" + ProdMonthTxt.Value + "'  \r\n " +
                            " and orgunit like '"+cmbMO.Text+"%'  \r\n "+
                            " group by substring(OrgUnit,1,5))q on Main.OrgUnit = q.SB  \r\n " +
                            " order by Shift, Orgunit, IndustryNumber, Work ";
                }
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ResultsTableName = "People";
                _dbMan1.ExecuteInstruction();

                DataTable dt1 = _dbMan1.ResultsDataTable;

                DataSet ds = new DataSet();
                ds.Tables.Add(dt1);
                theReport.RegisterData(ds);

                if (rdbStoping.Checked)
                {

                    theReport.Load("SBShiftSheet.frx");
                }
                else
                {
                    theReport.Load("SBDevShiftSheet.frx");
                }

                //theReport.Design();

                pcReport.Clear();
                theReport.Prepare();
                theReport.Preview = pcReport;
                theReport.ShowPrepared();

            }
            #endregion


            // Do SB Incentive
            #region
            if (Text == "Shift Boss Incentive Summary")
            {
                string Act = "0";

                if (rdbStoping.Checked == true)
                {
                    Act = "0";
                }
                else
                {
                    Act = "1";
                }

                MWDataManager.clsDataAccess _dbManSB = new MWDataManager.clsDataAccess();
                _dbManSB.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManSB.SqlStatement = " select * from BMCS_ShiftBoss_Transfer where ProdMonth = '" + ProdMonthTxt.Value + "' and activity = '" + Act + "'  ";
                _dbManSB.SqlStatement = _dbManSB.SqlStatement + " ";
                _dbManSB.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManSB.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManSB.ResultsTableName = "Incentive";
                _dbManSB.ExecuteInstruction();

                DataTable dt = _dbManSB.ResultsDataTable;

                DataSet ds2 = new DataSet();
                ds2.Tables.Add(dt);
                theReport.RegisterData(ds2);

                theReport.Load("SBIncentive.frx");

                //theReport.Design();

                pcReport.Clear();
                theReport.Prepare();
                theReport.Preview = pcReport;
                theReport.ShowPrepared();
            }
            #endregion

            // Do Yearly Summary
            #region
            if (Text == "Yearly Summary")
            {
                string Act = "0";

                if (rdbStoping.Checked == true)
                    Act = "0";
                else
                    Act = "1";

                MWDataManager.clsDataAccess _dbManSB = new MWDataManager.clsDataAccess();
                _dbManSB.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManSB.SqlStatement = " select * from BMCS_ShiftBoss_Transfer where Substring(convert(varchar(150),ProdMonth),1,4) = '" + ProdMonthTxt.Value.ToString().Substring(0, 4) + "' and activity = '" + Act + "'  ";
                _dbManSB.SqlStatement = _dbManSB.SqlStatement + " ";
                _dbManSB.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManSB.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManSB.ResultsTableName = "Incentive";
                _dbManSB.ExecuteInstruction();

                DataTable dt = _dbManSB.ResultsDataTable;

                grid.Rows.Clear();
                // grid.Visible = true;
                grid.Rows.Clear();
                grid.RowCount = 500;
                grid.ColumnCount = 10;

                grid.Columns[0].HeaderText = "Month";
                grid.Columns[1].HeaderText = "Call%";
                grid.Columns[2].HeaderText = "Mined";
                grid.Columns[3].HeaderText = "Call";
                grid.Columns[4].HeaderText = "Base Rate";
                grid.Columns[5].HeaderText = "Adj Rate";
                grid.Columns[6].HeaderText = "DayShift";
                grid.Columns[7].HeaderText = "NightShift";
                grid.Columns[8].HeaderText = "Total";
                grid.Columns[9].HeaderText = "R/Sqm";


                grid.Columns[0].Width = 70;
                grid.Columns[1].Width = 70;
                grid.Columns[2].Width = 70;
                grid.Columns[3].Width = 70;
                grid.Columns[4].Width = 70;
                grid.Columns[5].Width = 70;
                grid.Columns[6].Width = 70;
                grid.Columns[7].Width = 70;
                grid.Columns[8].Width = 70;
                grid.Columns[9].Width = 70;


                for (int i = 0; i <= grid.Rows.Count - 1; i++)
                {
                    grid.Rows[i].Cells[0].Value = "";
                    grid.Rows[i].Cells[1].Value = "";
                    grid.Rows[i].Cells[2].Value = "";
                    grid.Rows[i].Cells[3].Value = "";
                    grid.Rows[i].Cells[4].Value = "";
                    grid.Rows[i].Cells[5].Value = "";
                    grid.Rows[i].Cells[6].Value = "";
                    grid.Rows[i].Cells[7].Value = "";
                    grid.Rows[i].Cells[8].Value = "";
                    grid.Rows[i].Cells[9].Value = "";

                }

                grid.Rows[0].Cells[0].Value = "Jan -" + ProdMonthTxt.Value.ToString().Substring(0, 4);
                grid.Rows[1].Cells[0].Value = "Feb -" + ProdMonthTxt.Value.ToString().Substring(0, 4);
                grid.Rows[2].Cells[0].Value = "Mar -" + ProdMonthTxt.Value.ToString().Substring(0, 4);
                grid.Rows[3].Cells[0].Value = "Apr -" + ProdMonthTxt.Value.ToString().Substring(0, 4);
                grid.Rows[4].Cells[0].Value = "May -" + ProdMonthTxt.Value.ToString().Substring(0, 4);
                grid.Rows[5].Cells[0].Value = "Jun -" + ProdMonthTxt.Value.ToString().Substring(0, 4);
                grid.Rows[6].Cells[0].Value = "Jul -" + ProdMonthTxt.Value.ToString().Substring(0, 4);
                grid.Rows[7].Cells[0].Value = "Aug -" + ProdMonthTxt.Value.ToString().Substring(0, 4);
                grid.Rows[8].Cells[0].Value = "Sep -" + ProdMonthTxt.Value.ToString().Substring(0, 4);
                grid.Rows[9].Cells[0].Value = "Oct -" + ProdMonthTxt.Value.ToString().Substring(0, 4);
                grid.Rows[10].Cells[0].Value = "Nov -" + ProdMonthTxt.Value.ToString().Substring(0, 4);
                grid.Rows[11].Cells[0].Value = "Dec -" + ProdMonthTxt.Value.ToString().Substring(0, 4);

                string month = "";
                decimal CallPerc = 0;
                decimal Mined = 0;
                decimal Call = 0;

                decimal BaseRate = 0;
                decimal AdjRate = 0;

                decimal DayShift = 0;
                decimal NightShift = 0;
                decimal Total = 0;

                decimal RperSqm = 0;

                int Count = 0;

                foreach (DataRow dr in dt.Rows)
                {


                    if (month != dr["Prodmonth"].ToString().Substring(4, 2))
                    {
                        CallPerc = 0;
                        Call = 0;
                        Mined = 0;
                        BaseRate = 0;
                        AdjRate = 0;
                        DayShift = 0;
                        NightShift = 0;
                        Total = 0;
                        RperSqm = 0;
                        Count = 0;

                        Count = Count + 1;

                        month = dr["Prodmonth"].ToString().Substring(4, 2);

                        if (month == "01")
                        {
                            CallPerc = CallPerc + (Convert.ToDecimal(dr["CallPerc"].ToString()) * 100);
                            CallPerc = Math.Round((CallPerc / (Count * 100)) * 100, 2);
                            Mined = Mined + Convert.ToDecimal(dr["SQMMined"].ToString());
                            Call = Call + Convert.ToDecimal(dr["SQMCall"].ToString());
                            BaseRate = BaseRate + (Convert.ToDecimal(dr["BasePayment"].ToString()));
                            BaseRate = Math.Round(BaseRate / Count, 2);
                            AdjRate = AdjRate + (Convert.ToDecimal(dr["PaymentAfterFactor"].ToString()));
                            AdjRate = Math.Round(AdjRate / Count, 2);

                            DayShift = DayShift + Convert.ToDecimal(dr["DayShiftPayment"].ToString());
                            NightShift = NightShift + Convert.ToDecimal(dr["NightShiftPayment"].ToString());

                            Total = Total + Convert.ToDecimal(dr["TotalPayment"].ToString());

                            RperSqm = Math.Round(Total / Mined, 2);

                            grid.Rows[0].Cells[1].Value = CallPerc;
                            grid.Rows[0].Cells[2].Value = Mined;
                            grid.Rows[0].Cells[3].Value = Call;
                            grid.Rows[0].Cells[4].Value = BaseRate;
                            grid.Rows[0].Cells[5].Value = AdjRate;
                            grid.Rows[0].Cells[6].Value = DayShift;
                            grid.Rows[0].Cells[7].Value = NightShift;
                            grid.Rows[0].Cells[8].Value = Total;
                            grid.Rows[0].Cells[9].Value = RperSqm;


                        }

                        if (month == "02")
                        {

                            CallPerc = CallPerc + (Convert.ToDecimal(dr["CallPerc"].ToString()) * 100);
                            CallPerc = Math.Round((CallPerc / (Count * 100)) * 100, 2);
                            Mined = Mined + Convert.ToDecimal(dr["SQMMined"].ToString());
                            Call = Call + Convert.ToDecimal(dr["SQMCall"].ToString());
                            BaseRate = BaseRate + (Convert.ToDecimal(dr["BasePayment"].ToString()));
                            BaseRate = Math.Round(BaseRate / Count, 2);
                            AdjRate = AdjRate + (Convert.ToDecimal(dr["PaymentAfterFactor"].ToString()));
                            AdjRate = Math.Round(AdjRate / Count, 2);

                            DayShift = DayShift + Convert.ToDecimal(dr["DayShiftPayment"].ToString());
                            NightShift = NightShift + Convert.ToDecimal(dr["NightShiftPayment"].ToString());

                            Total = Total + Convert.ToDecimal(dr["TotalPayment"].ToString());

                            RperSqm = Math.Round(Total / Mined, 2);

                            grid.Rows[1].Cells[1].Value = CallPerc;
                            grid.Rows[1].Cells[2].Value = Mined;
                            grid.Rows[1].Cells[3].Value = Call;
                            grid.Rows[1].Cells[4].Value = BaseRate;
                            grid.Rows[1].Cells[5].Value = AdjRate;
                            grid.Rows[1].Cells[6].Value = DayShift;
                            grid.Rows[1].Cells[7].Value = NightShift;
                            grid.Rows[1].Cells[8].Value = Total;
                            grid.Rows[1].Cells[9].Value = RperSqm;

                        }

                        if (month == "03")
                        {

                            CallPerc = CallPerc + (Convert.ToDecimal(dr["CallPerc"].ToString()) * 100);
                            CallPerc = Math.Round((CallPerc / (Count * 100)) * 100, 2);
                            Mined = Mined + Convert.ToDecimal(dr["SQMMined"].ToString());
                            Call = Call + Convert.ToDecimal(dr["SQMCall"].ToString());
                            BaseRate = BaseRate + (Convert.ToDecimal(dr["BasePayment"].ToString()));
                            BaseRate = Math.Round(BaseRate / Count, 2);
                            AdjRate = AdjRate + (Convert.ToDecimal(dr["PaymentAfterFactor"].ToString()));
                            AdjRate = Math.Round(AdjRate / Count, 2);

                            DayShift = DayShift + Convert.ToDecimal(dr["DayShiftPayment"].ToString());
                            NightShift = NightShift + Convert.ToDecimal(dr["NightShiftPayment"].ToString());

                            Total = Total + Convert.ToDecimal(dr["TotalPayment"].ToString());

                            RperSqm = Math.Round(Total / Mined, 2);

                            grid.Rows[2].Cells[1].Value = CallPerc;
                            grid.Rows[2].Cells[2].Value = Mined;
                            grid.Rows[2].Cells[3].Value = Call;
                            grid.Rows[2].Cells[4].Value = BaseRate;
                            grid.Rows[2].Cells[5].Value = AdjRate;
                            grid.Rows[2].Cells[6].Value = DayShift;
                            grid.Rows[2].Cells[7].Value = NightShift;
                            grid.Rows[2].Cells[8].Value = Total;
                            grid.Rows[2].Cells[9].Value = RperSqm;

                        }

                        if (month == "04")
                        {

                            CallPerc = CallPerc + (Convert.ToDecimal(dr["CallPerc"].ToString()) * 100);
                            CallPerc = Math.Round((CallPerc / (Count * 100)) * 100, 2);
                            Mined = Mined + Convert.ToDecimal(dr["SQMMined"].ToString());
                            Call = Call + Convert.ToDecimal(dr["SQMCall"].ToString());
                            BaseRate = BaseRate + (Convert.ToDecimal(dr["BasePayment"].ToString()));
                            BaseRate = Math.Round(BaseRate / Count, 2);
                            AdjRate = AdjRate + (Convert.ToDecimal(dr["PaymentAfterFactor"].ToString()));
                            AdjRate = Math.Round(AdjRate / Count, 2);

                            DayShift = DayShift + Convert.ToDecimal(dr["DayShiftPayment"].ToString());
                            NightShift = NightShift + Convert.ToDecimal(dr["NightShiftPayment"].ToString());

                            Total = Total + Convert.ToDecimal(dr["TotalPayment"].ToString());

                            RperSqm = Math.Round(Total / Mined, 2);

                            grid.Rows[3].Cells[1].Value = CallPerc;
                            grid.Rows[3].Cells[2].Value = Mined;
                            grid.Rows[3].Cells[3].Value = Call;
                            grid.Rows[3].Cells[4].Value = BaseRate;
                            grid.Rows[3].Cells[5].Value = AdjRate;
                            grid.Rows[3].Cells[6].Value = DayShift;
                            grid.Rows[3].Cells[7].Value = NightShift;
                            grid.Rows[3].Cells[8].Value = Total;
                            grid.Rows[3].Cells[9].Value = RperSqm;

                        }

                        if (month == "05")
                        {

                            CallPerc = CallPerc + (Convert.ToDecimal(dr["CallPerc"].ToString()) * 100);
                            CallPerc = Math.Round((CallPerc / (Count * 100)) * 100, 2);
                            Mined = Mined + Convert.ToDecimal(dr["SQMMined"].ToString());
                            Call = Call + Convert.ToDecimal(dr["SQMCall"].ToString());
                            BaseRate = BaseRate + (Convert.ToDecimal(dr["BasePayment"].ToString()));
                            BaseRate = Math.Round(BaseRate / Count, 2);
                            AdjRate = AdjRate + (Convert.ToDecimal(dr["PaymentAfterFactor"].ToString()));
                            AdjRate = Math.Round(AdjRate / Count, 2);

                            DayShift = DayShift + Convert.ToDecimal(dr["DayShiftPayment"].ToString());
                            NightShift = NightShift + Convert.ToDecimal(dr["NightShiftPayment"].ToString());

                            Total = Total + Convert.ToDecimal(dr["TotalPayment"].ToString());

                            RperSqm = Math.Round(Total / Mined, 2);

                            grid.Rows[4].Cells[1].Value = CallPerc;
                            grid.Rows[4].Cells[2].Value = Mined;
                            grid.Rows[4].Cells[3].Value = Call;
                            grid.Rows[4].Cells[4].Value = BaseRate;
                            grid.Rows[4].Cells[5].Value = AdjRate;
                            grid.Rows[4].Cells[6].Value = DayShift;
                            grid.Rows[4].Cells[7].Value = NightShift;
                            grid.Rows[4].Cells[8].Value = Total;
                            grid.Rows[4].Cells[9].Value = RperSqm;

                        }

                        if (month == "06")
                        {

                            CallPerc = CallPerc + (Convert.ToDecimal(dr["CallPerc"].ToString()) * 100);
                            CallPerc = Math.Round((CallPerc / (Count * 100)) * 100, 2);
                            Mined = Mined + Convert.ToDecimal(dr["SQMMined"].ToString());
                            Call = Call + Convert.ToDecimal(dr["SQMCall"].ToString());
                            BaseRate = BaseRate + (Convert.ToDecimal(dr["BasePayment"].ToString()));
                            BaseRate = Math.Round(BaseRate / Count, 2);
                            AdjRate = AdjRate + (Convert.ToDecimal(dr["PaymentAfterFactor"].ToString()));
                            AdjRate = Math.Round(AdjRate / Count, 2);

                            DayShift = DayShift + Convert.ToDecimal(dr["DayShiftPayment"].ToString());
                            NightShift = NightShift + Convert.ToDecimal(dr["NightShiftPayment"].ToString());

                            Total = Total + Convert.ToDecimal(dr["TotalPayment"].ToString());

                            RperSqm = Math.Round(Total / Mined, 2);

                            grid.Rows[5].Cells[1].Value = CallPerc;
                            grid.Rows[5].Cells[2].Value = Mined;
                            grid.Rows[5].Cells[3].Value = Call;
                            grid.Rows[5].Cells[4].Value = BaseRate;
                            grid.Rows[5].Cells[5].Value = AdjRate;
                            grid.Rows[5].Cells[6].Value = DayShift;
                            grid.Rows[5].Cells[7].Value = NightShift;
                            grid.Rows[5].Cells[8].Value = Total;
                            grid.Rows[5].Cells[9].Value = RperSqm;

                        }

                        if (month == "07")
                        {

                            CallPerc = CallPerc + (Convert.ToDecimal(dr["CallPerc"].ToString()) * 100);
                            CallPerc = Math.Round((CallPerc / (Count * 100)) * 100, 2);
                            Mined = Mined + Convert.ToDecimal(dr["SQMMined"].ToString());
                            Call = Call + Convert.ToDecimal(dr["SQMCall"].ToString());
                            BaseRate = BaseRate + (Convert.ToDecimal(dr["BasePayment"].ToString()));
                            BaseRate = Math.Round(BaseRate / Count, 2);
                            AdjRate = AdjRate + (Convert.ToDecimal(dr["PaymentAfterFactor"].ToString()));
                            AdjRate = Math.Round(AdjRate / Count, 2);

                            DayShift = DayShift + Convert.ToDecimal(dr["DayShiftPayment"].ToString());
                            NightShift = NightShift + Convert.ToDecimal(dr["NightShiftPayment"].ToString());

                            Total = Total + Convert.ToDecimal(dr["TotalPayment"].ToString());

                            RperSqm = Math.Round(Total / Mined, 2);

                            grid.Rows[6].Cells[1].Value = CallPerc;
                            grid.Rows[6].Cells[2].Value = Mined;
                            grid.Rows[6].Cells[3].Value = Call;
                            grid.Rows[6].Cells[4].Value = BaseRate;
                            grid.Rows[6].Cells[5].Value = AdjRate;
                            grid.Rows[6].Cells[6].Value = DayShift;
                            grid.Rows[6].Cells[7].Value = NightShift;
                            grid.Rows[6].Cells[8].Value = Total;
                            grid.Rows[6].Cells[9].Value = RperSqm;

                        }

                        if (month == "08")
                        {

                            CallPerc = CallPerc + (Convert.ToDecimal(dr["CallPerc"].ToString()) * 100);
                            CallPerc = Math.Round((CallPerc / (Count * 100)) * 100, 2);
                            Mined = Mined + Convert.ToDecimal(dr["SQMMined"].ToString());
                            Call = Call + Convert.ToDecimal(dr["SQMCall"].ToString());
                            BaseRate = BaseRate + (Convert.ToDecimal(dr["BasePayment"].ToString()));
                            BaseRate = Math.Round(BaseRate / Count, 2);
                            AdjRate = AdjRate + (Convert.ToDecimal(dr["PaymentAfterFactor"].ToString()));
                            AdjRate = Math.Round(AdjRate / Count, 2);

                            DayShift = DayShift + Convert.ToDecimal(dr["DayShiftPayment"].ToString());
                            NightShift = NightShift + Convert.ToDecimal(dr["NightShiftPayment"].ToString());

                            Total = Total + Convert.ToDecimal(dr["TotalPayment"].ToString());

                            RperSqm = Math.Round(Total / Mined, 2);

                            grid.Rows[7].Cells[1].Value = CallPerc;
                            grid.Rows[7].Cells[2].Value = Mined;
                            grid.Rows[7].Cells[3].Value = Call;
                            grid.Rows[7].Cells[4].Value = BaseRate;
                            grid.Rows[7].Cells[5].Value = AdjRate;
                            grid.Rows[7].Cells[6].Value = DayShift;
                            grid.Rows[7].Cells[7].Value = NightShift;
                            grid.Rows[7].Cells[8].Value = Total;
                            grid.Rows[7].Cells[9].Value = RperSqm;

                        }

                        if (month == "09")
                        {

                            CallPerc = CallPerc + (Convert.ToDecimal(dr["CallPerc"].ToString()) * 100);
                            CallPerc = Math.Round((CallPerc / (Count * 100)) * 100, 2);
                            Mined = Mined + Convert.ToDecimal(dr["SQMMined"].ToString());
                            Call = Call + Convert.ToDecimal(dr["SQMCall"].ToString());
                            BaseRate = BaseRate + (Convert.ToDecimal(dr["BasePayment"].ToString()));
                            BaseRate = Math.Round(BaseRate / Count, 2);
                            AdjRate = AdjRate + (Convert.ToDecimal(dr["PaymentAfterFactor"].ToString()));
                            AdjRate = Math.Round(AdjRate / Count, 2);

                            DayShift = DayShift + Convert.ToDecimal(dr["DayShiftPayment"].ToString());
                            NightShift = NightShift + Convert.ToDecimal(dr["NightShiftPayment"].ToString());

                            Total = Total + Convert.ToDecimal(dr["TotalPayment"].ToString());

                            RperSqm = Math.Round(Total / Mined, 2);

                            grid.Rows[8].Cells[1].Value = CallPerc;
                            grid.Rows[8].Cells[2].Value = Mined;
                            grid.Rows[8].Cells[3].Value = Call;
                            grid.Rows[8].Cells[4].Value = BaseRate;
                            grid.Rows[8].Cells[5].Value = AdjRate;
                            grid.Rows[8].Cells[6].Value = DayShift;
                            grid.Rows[8].Cells[7].Value = NightShift;
                            grid.Rows[8].Cells[8].Value = Total;
                            grid.Rows[8].Cells[9].Value = RperSqm;

                        }

                        if (month == "10")
                        {

                            CallPerc = CallPerc + (Convert.ToDecimal(dr["CallPerc"].ToString()) * 100);
                            CallPerc = Math.Round((CallPerc / (Count * 100)) * 100, 2);
                            Mined = Mined + Convert.ToDecimal(dr["SQMMined"].ToString());
                            Call = Call + Convert.ToDecimal(dr["SQMCall"].ToString());
                            BaseRate = BaseRate + (Convert.ToDecimal(dr["BasePayment"].ToString()));
                            BaseRate = Math.Round(BaseRate / Count, 2);
                            AdjRate = AdjRate + (Convert.ToDecimal(dr["PaymentAfterFactor"].ToString()));
                            AdjRate = Math.Round(AdjRate / Count, 2);

                            DayShift = DayShift + Convert.ToDecimal(dr["DayShiftPayment"].ToString());
                            NightShift = NightShift + Convert.ToDecimal(dr["NightShiftPayment"].ToString());

                            Total = Total + Convert.ToDecimal(dr["TotalPayment"].ToString());

                            RperSqm = Math.Round(Total / Mined, 2);

                            grid.Rows[9].Cells[1].Value = CallPerc;
                            grid.Rows[9].Cells[2].Value = Mined;
                            grid.Rows[9].Cells[3].Value = Call;
                            grid.Rows[9].Cells[4].Value = BaseRate;
                            grid.Rows[9].Cells[5].Value = AdjRate;
                            grid.Rows[9].Cells[6].Value = DayShift;
                            grid.Rows[9].Cells[7].Value = NightShift;
                            grid.Rows[9].Cells[8].Value = Total;
                            grid.Rows[9].Cells[9].Value = RperSqm;

                        }

                        if (month == "11")
                        {

                            CallPerc = CallPerc + (Convert.ToDecimal(dr["CallPerc"].ToString()) * 100);
                            CallPerc = Math.Round((CallPerc / (Count * 100)) * 100, 2);
                            Mined = Mined + Convert.ToDecimal(dr["SQMMined"].ToString());
                            Call = Call + Convert.ToDecimal(dr["SQMCall"].ToString());
                            BaseRate = BaseRate + (Convert.ToDecimal(dr["BasePayment"].ToString()));
                            BaseRate = Math.Round(BaseRate / Count, 2);
                            AdjRate = AdjRate + (Convert.ToDecimal(dr["PaymentAfterFactor"].ToString()));
                            AdjRate = Math.Round(AdjRate / Count, 2);

                            DayShift = DayShift + Convert.ToDecimal(dr["DayShiftPayment"].ToString());
                            NightShift = NightShift + Convert.ToDecimal(dr["NightShiftPayment"].ToString());

                            Total = Total + Convert.ToDecimal(dr["TotalPayment"].ToString());

                            RperSqm = Math.Round(Total / Mined, 2);

                            grid.Rows[10].Cells[1].Value = CallPerc;
                            grid.Rows[10].Cells[2].Value = Mined;
                            grid.Rows[10].Cells[3].Value = Call;
                            grid.Rows[10].Cells[4].Value = BaseRate;
                            grid.Rows[10].Cells[5].Value = AdjRate;
                            grid.Rows[10].Cells[6].Value = DayShift;
                            grid.Rows[10].Cells[7].Value = NightShift;
                            grid.Rows[10].Cells[8].Value = Total;
                            grid.Rows[10].Cells[9].Value = RperSqm;

                        }

                        if (month == "12")
                        {

                            CallPerc = CallPerc + (Convert.ToDecimal(dr["CallPerc"].ToString()) * 100);
                            CallPerc = Math.Round((CallPerc / (Count * 100)) * 100, 2);
                            Mined = Mined + Convert.ToDecimal(dr["SQMMined"].ToString());
                            Call = Call + Convert.ToDecimal(dr["SQMCall"].ToString());
                            BaseRate = BaseRate + (Convert.ToDecimal(dr["BasePayment"].ToString()));
                            BaseRate = Math.Round(BaseRate / Count, 2);
                            AdjRate = AdjRate + (Convert.ToDecimal(dr["PaymentAfterFactor"].ToString()));
                            AdjRate = Math.Round(AdjRate / Count, 2);

                            DayShift = DayShift + Convert.ToDecimal(dr["DayShiftPayment"].ToString());
                            NightShift = NightShift + Convert.ToDecimal(dr["NightShiftPayment"].ToString());

                            Total = Total + Convert.ToDecimal(dr["TotalPayment"].ToString());

                            RperSqm = Math.Round(Total / Mined, 2);

                            grid.Rows[11].Cells[1].Value = CallPerc;
                            grid.Rows[11].Cells[2].Value = Mined;
                            grid.Rows[11].Cells[3].Value = Call;
                            grid.Rows[11].Cells[4].Value = BaseRate;
                            grid.Rows[11].Cells[5].Value = AdjRate;
                            grid.Rows[11].Cells[6].Value = DayShift;
                            grid.Rows[11].Cells[7].Value = NightShift;
                            grid.Rows[11].Cells[8].Value = Total;
                            grid.Rows[11].Cells[9].Value = RperSqm;


                        }
                    }
                    else
                    {
                        Count = Count + 1;

                        if (month == "01")
                        {

                            CallPerc = CallPerc + (Convert.ToDecimal(dr["CallPerc"].ToString()) * 100);
                            CallPerc = Math.Round((CallPerc / (Count * 100)) * 100, 2);
                            Mined = Mined + Convert.ToDecimal(dr["SQMMined"].ToString());
                            Call = Call + Convert.ToDecimal(dr["SQMCall"].ToString());
                            BaseRate = BaseRate + (Convert.ToDecimal(dr["BasePayment"].ToString()));
                            BaseRate = Math.Round(BaseRate / Count, 2);
                            AdjRate = AdjRate + (Convert.ToDecimal(dr["PaymentAfterFactor"].ToString()));
                            AdjRate = Math.Round(AdjRate / Count, 2);

                            DayShift = DayShift + Convert.ToDecimal(dr["DayShiftPayment"].ToString());
                            NightShift = NightShift + Convert.ToDecimal(dr["NightShiftPayment"].ToString());

                            Total = Total + Convert.ToDecimal(dr["TotalPayment"].ToString());

                            RperSqm = Math.Round(Total / Mined, 2);

                            grid.Rows[0].Cells[1].Value = CallPerc;
                            grid.Rows[0].Cells[2].Value = Mined;
                            grid.Rows[0].Cells[3].Value = Call;
                            grid.Rows[0].Cells[4].Value = BaseRate;
                            grid.Rows[0].Cells[5].Value = AdjRate;
                            grid.Rows[0].Cells[6].Value = DayShift;
                            grid.Rows[0].Cells[7].Value = NightShift;
                            grid.Rows[0].Cells[8].Value = Total;
                            grid.Rows[0].Cells[9].Value = RperSqm;




                        }

                        if (month == "02")
                        {

                            CallPerc = CallPerc + (Convert.ToDecimal(dr["CallPerc"].ToString()) * 100);
                            CallPerc = Math.Round((CallPerc / (Count * 100)) * 100, 2);
                            Mined = Mined + Convert.ToDecimal(dr["SQMMined"].ToString());
                            Call = Call + Convert.ToDecimal(dr["SQMCall"].ToString());
                            BaseRate = BaseRate + (Convert.ToDecimal(dr["BasePayment"].ToString()));
                            BaseRate = Math.Round(BaseRate / Count, 2);
                            AdjRate = AdjRate + (Convert.ToDecimal(dr["PaymentAfterFactor"].ToString()));
                            AdjRate = Math.Round(AdjRate / Count, 2);

                            DayShift = DayShift + Convert.ToDecimal(dr["DayShiftPayment"].ToString());
                            NightShift = NightShift + Convert.ToDecimal(dr["NightShiftPayment"].ToString());

                            Total = Total + Convert.ToDecimal(dr["TotalPayment"].ToString());

                            RperSqm = Math.Round(Total / Mined, 2);

                            grid.Rows[1].Cells[1].Value = CallPerc;
                            grid.Rows[1].Cells[2].Value = Mined;
                            grid.Rows[1].Cells[3].Value = Call;
                            grid.Rows[1].Cells[4].Value = BaseRate;
                            grid.Rows[1].Cells[5].Value = AdjRate;
                            grid.Rows[1].Cells[6].Value = DayShift;
                            grid.Rows[1].Cells[7].Value = NightShift;
                            grid.Rows[1].Cells[8].Value = Total;
                            grid.Rows[1].Cells[9].Value = RperSqm;

                        }

                        if (month == "03")
                        {

                            CallPerc = CallPerc + (Convert.ToDecimal(dr["CallPerc"].ToString()) * 100);
                            CallPerc = Math.Round((CallPerc / (Count * 100)) * 100, 2);
                            Mined = Mined + Convert.ToDecimal(dr["SQMMined"].ToString());
                            Call = Call + Convert.ToDecimal(dr["SQMCall"].ToString());
                            BaseRate = BaseRate + (Convert.ToDecimal(dr["BasePayment"].ToString()));
                            BaseRate = Math.Round(BaseRate / Count, 2);
                            AdjRate = AdjRate + (Convert.ToDecimal(dr["PaymentAfterFactor"].ToString()));
                            AdjRate = Math.Round(AdjRate / Count, 2);

                            DayShift = DayShift + Convert.ToDecimal(dr["DayShiftPayment"].ToString());
                            NightShift = NightShift + Convert.ToDecimal(dr["NightShiftPayment"].ToString());

                            Total = Total + Convert.ToDecimal(dr["TotalPayment"].ToString());

                            RperSqm = Math.Round(Total / Mined, 2);

                            grid.Rows[2].Cells[1].Value = CallPerc;
                            grid.Rows[2].Cells[2].Value = Mined;
                            grid.Rows[2].Cells[3].Value = Call;
                            grid.Rows[2].Cells[4].Value = BaseRate;
                            grid.Rows[2].Cells[5].Value = AdjRate;
                            grid.Rows[2].Cells[6].Value = DayShift;
                            grid.Rows[2].Cells[7].Value = NightShift;
                            grid.Rows[2].Cells[8].Value = Total;
                            grid.Rows[2].Cells[9].Value = RperSqm;

                        }

                        if (month == "04")
                        {
                            CallPerc = CallPerc + (Convert.ToDecimal(dr["CallPerc"].ToString()) * 100);
                            CallPerc = Math.Round((CallPerc / (Count * 100)) * 100, 2);
                            Mined = Mined + Convert.ToDecimal(dr["SQMMined"].ToString());
                            Call = Call + Convert.ToDecimal(dr["SQMCall"].ToString());
                            BaseRate = BaseRate + (Convert.ToDecimal(dr["BasePayment"].ToString()));
                            BaseRate = Math.Round(BaseRate / Count, 2);
                            AdjRate = AdjRate + (Convert.ToDecimal(dr["PaymentAfterFactor"].ToString()));
                            AdjRate = Math.Round(AdjRate / Count, 2);

                            DayShift = DayShift + Convert.ToDecimal(dr["DayShiftPayment"].ToString());
                            NightShift = NightShift + Convert.ToDecimal(dr["NightShiftPayment"].ToString());

                            Total = Total + Convert.ToDecimal(dr["TotalPayment"].ToString());

                            RperSqm = Math.Round(Total / Mined, 2);

                            grid.Rows[3].Cells[1].Value = CallPerc;
                            grid.Rows[3].Cells[2].Value = Mined;
                            grid.Rows[3].Cells[3].Value = Call;
                            grid.Rows[3].Cells[4].Value = BaseRate;
                            grid.Rows[3].Cells[5].Value = AdjRate;
                            grid.Rows[3].Cells[6].Value = DayShift;
                            grid.Rows[3].Cells[7].Value = NightShift;
                            grid.Rows[3].Cells[8].Value = Total;
                            grid.Rows[3].Cells[9].Value = RperSqm;

                        }

                        if (month == "05")
                        {

                            CallPerc = CallPerc + (Convert.ToDecimal(dr["CallPerc"].ToString()) * 100);
                            CallPerc = Math.Round((CallPerc / (Count * 100)) * 100, 2);
                            Mined = Mined + Convert.ToDecimal(dr["SQMMined"].ToString());
                            Call = Call + Convert.ToDecimal(dr["SQMCall"].ToString());
                            BaseRate = BaseRate + (Convert.ToDecimal(dr["BasePayment"].ToString()));
                            BaseRate = Math.Round(BaseRate / Count, 2);
                            AdjRate = AdjRate + (Convert.ToDecimal(dr["PaymentAfterFactor"].ToString()));
                            AdjRate = Math.Round(AdjRate / Count, 2);

                            DayShift = DayShift + Convert.ToDecimal(dr["DayShiftPayment"].ToString());
                            NightShift = NightShift + Convert.ToDecimal(dr["NightShiftPayment"].ToString());

                            Total = Total + Convert.ToDecimal(dr["TotalPayment"].ToString());

                            RperSqm = Math.Round(Total / Mined, 2);

                            grid.Rows[4].Cells[1].Value = CallPerc;
                            grid.Rows[4].Cells[2].Value = Mined;
                            grid.Rows[4].Cells[3].Value = Call;
                            grid.Rows[4].Cells[4].Value = BaseRate;
                            grid.Rows[4].Cells[5].Value = AdjRate;
                            grid.Rows[4].Cells[6].Value = DayShift;
                            grid.Rows[4].Cells[7].Value = NightShift;
                            grid.Rows[4].Cells[8].Value = Total;
                            grid.Rows[4].Cells[9].Value = RperSqm;

                        }

                        if (month == "06")
                        {

                            CallPerc = CallPerc + (Convert.ToDecimal(dr["CallPerc"].ToString()) * 100);
                            CallPerc = Math.Round((CallPerc / (Count * 100)) * 100, 2);
                            Mined = Mined + Convert.ToDecimal(dr["SQMMined"].ToString());
                            Call = Call + Convert.ToDecimal(dr["SQMCall"].ToString());
                            BaseRate = BaseRate + (Convert.ToDecimal(dr["BasePayment"].ToString()));
                            BaseRate = Math.Round(BaseRate / Count, 2);
                            AdjRate = AdjRate + (Convert.ToDecimal(dr["PaymentAfterFactor"].ToString()));
                            AdjRate = Math.Round(AdjRate / Count, 2);

                            DayShift = DayShift + Convert.ToDecimal(dr["DayShiftPayment"].ToString());
                            NightShift = NightShift + Convert.ToDecimal(dr["NightShiftPayment"].ToString());

                            Total = Total + Convert.ToDecimal(dr["TotalPayment"].ToString());

                            RperSqm = Math.Round(Total / Mined, 2);

                            grid.Rows[5].Cells[1].Value = CallPerc;
                            grid.Rows[5].Cells[2].Value = Mined;
                            grid.Rows[5].Cells[3].Value = Call;
                            grid.Rows[5].Cells[4].Value = BaseRate;
                            grid.Rows[5].Cells[5].Value = AdjRate;
                            grid.Rows[5].Cells[6].Value = DayShift;
                            grid.Rows[5].Cells[7].Value = NightShift;
                            grid.Rows[5].Cells[8].Value = Total;
                            grid.Rows[5].Cells[9].Value = RperSqm;

                        }

                        if (month == "07")
                        {

                            CallPerc = CallPerc + (Convert.ToDecimal(dr["CallPerc"].ToString()) * 100);
                            CallPerc = Math.Round((CallPerc / (Count * 100)) * 100, 2);
                            Mined = Mined + Convert.ToDecimal(dr["SQMMined"].ToString());
                            Call = Call + Convert.ToDecimal(dr["SQMCall"].ToString());
                            BaseRate = BaseRate + (Convert.ToDecimal(dr["BasePayment"].ToString()));
                            BaseRate = Math.Round(BaseRate / Count, 2);
                            AdjRate = AdjRate + (Convert.ToDecimal(dr["PaymentAfterFactor"].ToString()));
                            AdjRate = Math.Round(AdjRate / Count, 2);

                            DayShift = DayShift + Convert.ToDecimal(dr["DayShiftPayment"].ToString());
                            NightShift = NightShift + Convert.ToDecimal(dr["NightShiftPayment"].ToString());

                            Total = Total + Convert.ToDecimal(dr["TotalPayment"].ToString());

                            RperSqm = Math.Round(Total / Mined, 2);

                            grid.Rows[6].Cells[1].Value = CallPerc;
                            grid.Rows[6].Cells[2].Value = Mined;
                            grid.Rows[6].Cells[3].Value = Call;
                            grid.Rows[6].Cells[4].Value = BaseRate;
                            grid.Rows[6].Cells[5].Value = AdjRate;
                            grid.Rows[6].Cells[6].Value = DayShift;
                            grid.Rows[6].Cells[7].Value = NightShift;
                            grid.Rows[6].Cells[8].Value = Total;
                            grid.Rows[6].Cells[9].Value = RperSqm;
                        }

                        if (month == "08")
                        {

                            CallPerc = CallPerc + (Convert.ToDecimal(dr["CallPerc"].ToString()) * 100);
                            CallPerc = Math.Round((CallPerc / (Count * 100)) * 100, 2);
                            Mined = Mined + Convert.ToDecimal(dr["SQMMined"].ToString());
                            Call = Call + Convert.ToDecimal(dr["SQMCall"].ToString());
                            BaseRate = BaseRate + (Convert.ToDecimal(dr["BasePayment"].ToString()));
                            BaseRate = Math.Round(BaseRate / Count, 2);
                            AdjRate = AdjRate + (Convert.ToDecimal(dr["PaymentAfterFactor"].ToString()));
                            AdjRate = Math.Round(AdjRate / Count, 2);

                            DayShift = DayShift + Convert.ToDecimal(dr["DayShiftPayment"].ToString());
                            NightShift = NightShift + Convert.ToDecimal(dr["NightShiftPayment"].ToString());

                            Total = Total + Convert.ToDecimal(dr["TotalPayment"].ToString());

                            RperSqm = Math.Round(Total / Mined, 2);

                            grid.Rows[7].Cells[1].Value = CallPerc;
                            grid.Rows[7].Cells[2].Value = Mined;
                            grid.Rows[7].Cells[3].Value = Call;
                            grid.Rows[7].Cells[4].Value = BaseRate;
                            grid.Rows[7].Cells[5].Value = AdjRate;
                            grid.Rows[7].Cells[6].Value = DayShift;
                            grid.Rows[7].Cells[7].Value = NightShift;
                            grid.Rows[7].Cells[8].Value = Total;
                            grid.Rows[7].Cells[9].Value = RperSqm;

                        }

                        if (month == "09")
                        {

                            CallPerc = CallPerc + (Convert.ToDecimal(dr["CallPerc"].ToString()) * 100);
                            CallPerc = Math.Round((CallPerc / (Count * 100)) * 100, 2);
                            Mined = Mined + Convert.ToDecimal(dr["SQMMined"].ToString());
                            Call = Call + Convert.ToDecimal(dr["SQMCall"].ToString());
                            BaseRate = BaseRate + (Convert.ToDecimal(dr["BasePayment"].ToString()));
                            BaseRate = Math.Round(BaseRate / Count, 2);
                            AdjRate = AdjRate + (Convert.ToDecimal(dr["PaymentAfterFactor"].ToString()));
                            AdjRate = Math.Round(AdjRate / Count, 2);

                            DayShift = DayShift + Convert.ToDecimal(dr["DayShiftPayment"].ToString());
                            NightShift = NightShift + Convert.ToDecimal(dr["NightShiftPayment"].ToString());

                            Total = Total + Convert.ToDecimal(dr["TotalPayment"].ToString());

                            RperSqm = Math.Round(Total / Mined, 2);

                            grid.Rows[8].Cells[1].Value = CallPerc;
                            grid.Rows[8].Cells[2].Value = Mined;
                            grid.Rows[8].Cells[3].Value = Call;
                            grid.Rows[8].Cells[4].Value = BaseRate;
                            grid.Rows[8].Cells[5].Value = AdjRate;
                            grid.Rows[8].Cells[6].Value = DayShift;
                            grid.Rows[8].Cells[7].Value = NightShift;
                            grid.Rows[8].Cells[8].Value = Total;
                            grid.Rows[8].Cells[9].Value = RperSqm;

                        }

                        if (month == "10")
                        {

                            CallPerc = CallPerc + (Convert.ToDecimal(dr["CallPerc"].ToString()) * 100);
                            CallPerc = Math.Round((CallPerc / (Count * 100)) * 100, 2);
                            Mined = Mined + Convert.ToDecimal(dr["SQMMined"].ToString());
                            Call = Call + Convert.ToDecimal(dr["SQMCall"].ToString());
                            BaseRate = BaseRate + (Convert.ToDecimal(dr["BasePayment"].ToString()));
                            BaseRate = Math.Round(BaseRate / Count, 2);
                            AdjRate = AdjRate + (Convert.ToDecimal(dr["PaymentAfterFactor"].ToString()));
                            AdjRate = Math.Round(AdjRate / Count, 2);

                            DayShift = DayShift + Convert.ToDecimal(dr["DayShiftPayment"].ToString());
                            NightShift = NightShift + Convert.ToDecimal(dr["NightShiftPayment"].ToString());

                            Total = Total + Convert.ToDecimal(dr["TotalPayment"].ToString());

                            RperSqm = Math.Round(Total / Mined, 2);

                            grid.Rows[9].Cells[1].Value = CallPerc;
                            grid.Rows[9].Cells[2].Value = Mined;
                            grid.Rows[9].Cells[3].Value = Call;
                            grid.Rows[9].Cells[4].Value = BaseRate;
                            grid.Rows[9].Cells[5].Value = AdjRate;
                            grid.Rows[9].Cells[6].Value = DayShift;
                            grid.Rows[9].Cells[7].Value = NightShift;
                            grid.Rows[9].Cells[8].Value = Total;
                            grid.Rows[9].Cells[9].Value = RperSqm;

                        }

                        if (month == "11")
                        {

                            CallPerc = CallPerc + (Convert.ToDecimal(dr["CallPerc"].ToString()) * 100);
                            CallPerc = Math.Round((CallPerc / (Count * 100)) * 100, 2);
                            Mined = Mined + Convert.ToDecimal(dr["SQMMined"].ToString());
                            Call = Call + Convert.ToDecimal(dr["SQMCall"].ToString());
                            BaseRate = BaseRate + (Convert.ToDecimal(dr["BasePayment"].ToString()));
                            BaseRate = Math.Round(BaseRate / Count, 2);
                            AdjRate = AdjRate + (Convert.ToDecimal(dr["PaymentAfterFactor"].ToString()));
                            AdjRate = Math.Round(AdjRate / Count, 2);

                            DayShift = DayShift + Convert.ToDecimal(dr["DayShiftPayment"].ToString());
                            NightShift = NightShift + Convert.ToDecimal(dr["NightShiftPayment"].ToString());

                            Total = Total + Convert.ToDecimal(dr["TotalPayment"].ToString());

                            RperSqm = Math.Round(Total / Mined, 2);

                            grid.Rows[10].Cells[1].Value = CallPerc;
                            grid.Rows[10].Cells[2].Value = Mined;
                            grid.Rows[10].Cells[3].Value = Call;
                            grid.Rows[10].Cells[4].Value = BaseRate;
                            grid.Rows[10].Cells[5].Value = AdjRate;
                            grid.Rows[10].Cells[6].Value = DayShift;
                            grid.Rows[10].Cells[7].Value = NightShift;
                            grid.Rows[10].Cells[8].Value = Total;
                            grid.Rows[10].Cells[9].Value = RperSqm;

                        }

                        if (month == "12")
                        {

                            CallPerc = CallPerc + (Convert.ToDecimal(dr["CallPerc"].ToString()) * 100);
                            CallPerc = Math.Round((CallPerc / (Count * 100)) * 100, 2);
                            Mined = Mined + Convert.ToDecimal(dr["SQMMined"].ToString());
                            Call = Call + Convert.ToDecimal(dr["SQMCall"].ToString());
                            BaseRate = BaseRate + (Convert.ToDecimal(dr["BasePayment"].ToString()));
                            BaseRate = Math.Round(BaseRate / Count, 2);
                            AdjRate = AdjRate + (Convert.ToDecimal(dr["PaymentAfterFactor"].ToString()));
                            AdjRate = Math.Round(AdjRate / Count, 2);

                            DayShift = DayShift + Convert.ToDecimal(dr["DayShiftPayment"].ToString());
                            NightShift = NightShift + Convert.ToDecimal(dr["NightShiftPayment"].ToString());

                            Total = Total + Convert.ToDecimal(dr["TotalPayment"].ToString());

                            RperSqm = Math.Round(Total / Mined, 2);

                            grid.Rows[11].Cells[1].Value = CallPerc;
                            grid.Rows[11].Cells[2].Value = Mined;
                            grid.Rows[11].Cells[3].Value = Call;
                            grid.Rows[11].Cells[4].Value = BaseRate;
                            grid.Rows[11].Cells[5].Value = AdjRate;
                            grid.Rows[11].Cells[6].Value = DayShift;
                            grid.Rows[11].Cells[7].Value = NightShift;
                            grid.Rows[11].Cells[8].Value = Total;
                            grid.Rows[11].Cells[9].Value = RperSqm;


                        }
                    }
                }

                grid.RowCount = 12;


                MWDataManager.clsDataAccess _dbManGrid = new MWDataManager.clsDataAccess();
                _dbManGrid.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);


                _dbManGrid.SqlStatement = _dbManGrid.SqlStatement + " ";

                for (int i = 0; i <= grid.Rows.Count - 1; i++)
                {
                    if (i != grid.Rows.Count - 1)
                    {
                        _dbManGrid.SqlStatement = _dbManGrid.SqlStatement + " select '" + grid.Rows[i].Cells[0].Value + "','" + grid.Rows[i].Cells[1].Value + "','" + grid.Rows[i].Cells[2].Value + "','" + grid.Rows[i].Cells[3].Value + "','" + grid.Rows[i].Cells[4].Value + "','" + grid.Rows[i].Cells[5].Value + "', ";
                        _dbManGrid.SqlStatement = _dbManGrid.SqlStatement + " '" + grid.Rows[i].Cells[6].Value + "','" + grid.Rows[i].Cells[7].Value + "','" + grid.Rows[i].Cells[8].Value + "','" + grid.Rows[i].Cells[9].Value + "' ";
                        _dbManGrid.SqlStatement = _dbManGrid.SqlStatement + " union ";
                    }
                    if (i == grid.Rows.Count - 1)
                    {
                        _dbManGrid.SqlStatement = _dbManGrid.SqlStatement + " select '" + grid.Rows[i].Cells[0].Value + "','" + grid.Rows[i].Cells[1].Value + "','" + grid.Rows[i].Cells[2].Value + "','" + grid.Rows[i].Cells[3].Value + "','" + grid.Rows[i].Cells[4].Value + "','" + grid.Rows[i].Cells[5].Value + "', ";
                        _dbManGrid.SqlStatement = _dbManGrid.SqlStatement + " '" + grid.Rows[i].Cells[6].Value + "','" + grid.Rows[i].Cells[7].Value + "','" + grid.Rows[i].Cells[8].Value + "','" + grid.Rows[i].Cells[9].Value + "' ";
                        _dbManGrid.SqlStatement = _dbManGrid.SqlStatement + "  ";
                    }
                }


                _dbManGrid.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManGrid.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManGrid.ResultsTableName = "Yearly";
                _dbManGrid.ExecuteInstruction();

                DataTable dt2 = _dbManGrid.ResultsDataTable;



                DataSet ds2 = new DataSet();
                ds2.Tables.Add(dt2);
                theReport.RegisterData(ds2);

                theReport.Load("YearlySummary.frx");

                // theReport.Design();

                pcReport.Clear();
                theReport.Prepare();
                theReport.Preview = pcReport;
                theReport.ShowPrepared();
            }
            #endregion
        }

        private void cmbMO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMO.SelectedIndex >= 0)
            {
                //Set begin and end date
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " select MAX(BeginDate) BeginDate, MAX(EndDate) EndDate from mineware.dbo.tbl_BCS_SECCAL \r\n " +
                                      " where prodmonth = '" + ProdMonthTxt.Value.ToString() + "' \r\n " +
                                      " and Sectionid like '" + cmbMO.Text + "%' ";
                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ExecuteInstruction();

                DataTable dt = _dbMan.ResultsDataTable;

                foreach (DataRow dr in dt.Rows)
                {
                    BeginDate.Value = Convert.ToDateTime(dr["BeginDate"].ToString());
                    EndDate.Value = Convert.ToDateTime(dr["EndDate"].ToString());
                }
            }
        }

        private void rdbStoping_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbStoping.Checked == true)
            {
                LoadMO();
            }
        }

        private void rdbDev_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbDev.Checked == true)
            {
                LoadMO();
            }
        }
    }
}
