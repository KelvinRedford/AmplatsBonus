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
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.ProductionAmplatsBonus
{
    public partial class ucMonthlySummariesRep : BaseUserControl
    {
        public ucMonthlySummariesRep()
        {
            InitializeComponent();
        }

        Report theReport = new Report();
        Procedures proc = new Procedures();

        private void frmMonthlySummariesRep_Load(object sender, EventArgs e)
        {
            //Do Prodmonth

            ProdMonthTxt.Text = Convert.ToString(SysSettings.ProdMonth);
            Procedures procs = new Procedures();
            procs.ProdMonthVis(Convert.ToInt32(ProdMonthTxt.Text));
            ProdMonth1Txt.Text = Procedures.Prod2;
            ProdMonth1Txt.TextAlign = HorizontalAlignment.Center;

            // Now do Display Month

            procs.ProdMonthCalc(Convert.ToInt32(ProdMonthTxt.Text)-1);
            txtDisplayMonth.Text = Procedures.Prod.ToString();

        }

        private void ProdMonthTxt_Click(object sender, EventArgs e)
        {
            Procedures procs = new Procedures();
            procs.ProdMonthCalc(Convert.ToInt32(ProdMonthTxt.Text));
            ProdMonthTxt.Text = Procedures.Prod.ToString();
            procs.ProdMonthVis(Convert.ToInt32(ProdMonthTxt.Text));
            ProdMonth1Txt.Text = Procedures.Prod2;

            // Now do Display Month

            procs.ProdMonthCalc(Convert.ToInt32(ProdMonthTxt.Text) - 1);
            txtDisplayMonth.Text = Procedures.Prod.ToString();
        }

        private void Close1Btn_Click(object sender, EventArgs e)
        {
            
        }

        private void LoadMonthlySum()
        {
            if (rdbStoping.Checked == true)
            {
                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = " select *, '" + txtDisplayMonth.Text + "' PrevMonth, 'Stoping' MyActivity from ( \r\n " +
                                       " select SUM(WorkingShifts) WorkingShifts, SUM(Nett_Amount) finpay, MyOrder, MoSec, Element, ActivityCode Activity, MoSec mo, '" + ProdMonthTxt.Value + "' Prodmonth from (  \r\n " +
                                       " select case when Element = 'Special Team Leader Stoping Bonus' then '0'  \r\n " +
                                       " when Element = 'Machine Operator Stoping Bonus' then '1'  \r\n " +
                                       " when Element = 'Production Unit Stoping Bonus' then '2' end as MyOrder,  \r\n " +
                                       " SUBSTRING(OrgUnit,1,4) MoSec, *  \r\n " +
                                       " from tbl_BCS_ARMS_Interface_TransferNew  \r\n " +
                                       " where ProdMonth = '" + ProdMonthTxt.Value + "'  \r\n " +
                                       " and Type = '28' and ActivityCode = '0' ) a  \r\n " +
                                       " Group by MoSec, MyOrder, Element, ActivityCode ) d  \r\n " +
                                       " left outer join  \r\n " +
                                       " (select *, finpay2-finpay1 AdjPay from (  \r\n " +
                                       " select * from (  \r\n " +
                                       " select SUM(WorkingShifts) ws1, SUM(Nett_Amount) finpay1, MyOrder MyOrder1, MoSec MoSec1 from (  \r\n " +
                                       " select case when Element = 'Special Team Leader Stoping Bonus' then '0'  \r\n " +
                                       " when Element = 'Machine Operator Stoping Bonus' then '1'  \r\n " +
                                       " when Element = 'Production Unit Stoping Bonus' then '2' end as MyOrder,  \r\n " +
                                       " SUBSTRING(OrgUnit,1,4) MoSec, *  \r\n " +
                                        " from tbl_BCS_ARMS_Interface_TransferNew  \r\n " +
                                       " where ProdMonth = '" + txtDisplayMonth.Text + "'  \r\n " +
                                       " and Type = '28' and ActivityCode = '0' ) a  \r\n " +
                                       " Group by MoSec, MyOrder ) a  \r\n " +
                                       " left outer join  \r\n " +
                                       " (select SUM(WorkingShifts) ws2, SUM(Nett_Amount) finpay2, MyOrder MyOrder2, MoSec MoSec2 from (  \r\n " +
                                       " select case when Element = 'Special Team Leader Stoping Bonus' then '0'  \r\n " +
                                       " when Element = 'Machine Operator Stoping Bonus' then '1'  \r\n " +
                                       " when Element = 'Production Unit Stoping Bonus' then '2' end as MyOrder,  \r\n " +
                                       " SUBSTRING(OrgUnit,1,4) MoSec, *  \r\n " +
                                        " from tbl_BCS_ARMS_Interface_TransferNew_Adjustments  \r\n " +
                                       " where ProdMonth = '" + txtDisplayMonth.Text + "'  \r\n " +
                                       " and Type = '28' and ActivityCode = '0' ) a  \r\n " +
                                       " Group by MoSec, MyOrder) b on a.MoSec1 = b.MoSec2 and a.MyOrder1 = b.MyOrder2 )c )e on d.MoSec = e.MoSec1 and d.MyOrder = e.MyOrder1  \r\n " +
                                       " left outer join  \r\n " +
                                       " (select SUBSTRING(Sectionid,1,4) MoSec4, TotalShifts PossibleShifts from mineware.dbo.tbl_BCS_SECCAL  \r\n " +
                                       " where Prodmonth = '" + ProdMonthTxt.Value + "' and SUBSTRING(Sectionid,1,4) <> 'VAMP'  \r\n " +
                                       " group by SUBSTRING(Sectionid,1,4), TotalShifts ) f on d.MoSec = f.MoSec4 \r\n " +
                                       " where MoSec not in ('0123', '0733', '0743') ";

                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ResultsTableName = "MonthlySum";
                _dbMan1.ExecuteInstruction();

                DataTable dt1 = _dbMan1.ResultsDataTable;

                DataSet ds1 = new DataSet();
                ds1.Tables.Add(dt1);

                theReport.RegisterData(ds1);

                theReport.Load("MonthlySumRep.frx");

                //theReport.Design();

                pcReport.Clear();
                theReport.Prepare();
                theReport.Preview = pcReport;
                theReport.ShowPrepared();
            }
            else
            {
                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = "select *, WorkingShifts/PossibleShifts incubants, '" + txtDisplayMonth.Text + "' PrevMonth, 'Development' MyActivity from ( \r\n " +
                                " select SUM(WorkingShifts) WorkingShifts, SUM(Nett_Amount) finpay, MyOrder, MoSec, Element1 Element, MoSec mo, '" + ProdMonthTxt.Value + "' Prodmonth from (  \r\n " +
                                " select case when Element in ('Special Team Leader Development Bonus', 'Special Team Leader Stoping Bonus') then '0'  \r\n "+
                                " when Element in ('Machine Operator Development Bonus','Machine Operator Stoping Bonus') then '1'  \r\n " +
                                " when Element in ('Production Unit Development Bonus', 'Production Unit Stoping Bonus') then '2' end as MyOrder, \r\n "+
                                " case when Element = 'Special Team Leader Stoping Bonus' then 'Special Team Leader Development Bonus' \r\n "+
                                " when Element = 'Production Unit Stoping Bonus' then 'Production Unit Development Bonus' else Element end as Element1, \r\n "+
                                " SUBSTRING(OrgUnit,1,4) MoSec, *  \r\n "+
                                " from tbl_BCS_ARMS_Interface_TransferNew  \r\n "+
                                " where ProdMonth = '" + ProdMonthTxt.Value + "'  \r\n " +
                                " and Type = '28' and SUBSTRING(OrgUnit,1,4) in (select substring(sectionid,1,4) MO from mineware.dbo.tbl_BCS_Planmonth where prodmonth = '" + ProdMonthTxt.Value + "' and Activity = 1 and Adv > 0 and SUBSTRING(Sectionid,1,4) <> '0153' group by substring(sectionid,1,4)) and ActivityCode = 1 ) a   \r\n " +
                                " Group by MoSec, MyOrder, Element1 ) d  \r\n "+
                                " left outer join  \r\n "+
                                " (select *, finpay2-finpay1 AdjPay from (  \r\n "+
                                " select * from (  \r\n "+
                                " select SUM(WorkingShifts) ws1, SUM(Nett_Amount) finpay1, MyOrder MyOrder1, MoSec MoSec1 from (  \r\n "+
                                " select case when Element in ('Special Team Leader Development Bonus', 'Special Team Leader Stoping Bonus') then '0'  \r\n "+
                                " when Element in ('Machine Operator Development Bonus','Machine Operator Stoping Bonus') then '1'  \r\n " +
                                " when Element in ('Production Unit Development Bonus', 'Production Unit Stoping Bonus') then '2' end as MyOrder, \r\n "+
                                " case when Element = 'Special Team Leader Stoping Bonus' then 'Special Team Leader Development Bonus' \r\n "+
                                " when Element = 'Production Unit Stoping Bonus' then 'Production Unit Development Bonus' else Element end as Element1,  \r\n "+
                                " SUBSTRING(OrgUnit,1,4) MoSec, *  \r\n "+
                                 " from tbl_BCS_ARMS_Interface_TransferNew  \r\n "+
                                " where ProdMonth = '" + txtDisplayMonth.Text + "'  \r\n " +
                                " and Type = '28'and SUBSTRING(OrgUnit,1,4) in (select substring(sectionid,1,4) MO from mineware.dbo.tbl_BCS_Planmonth where prodmonth = '" + ProdMonthTxt.Value + "' and Activity = 1 and Adv > 0 and SUBSTRING(Sectionid,1,4) <> '0153' group by substring(sectionid,1,4)) and ActivityCode = 1 ) a    \r\n " +
                                " Group by MoSec, MyOrder ) a  \r\n "+
                                " left outer join  \r\n "+
                                " (select SUM(WorkingShifts) ws2, SUM(Nett_Amount) finpay2, MyOrder MyOrder2, MoSec MoSec2 from (  \r\n "+
                                " select case when Element in ('Special Team Leader Development Bonus', 'Special Team Leader Stoping Bonus') then '0'  \r\n "+
                                " when Element in ('Machine Operator Development Bonus','Machine Operator Stoping Bonus') then '1'  \r\n " +
                                " when Element in ('Production Unit Development Bonus', 'Production Unit Stoping Bonus') then '2' end as MyOrder,  \r\n "+
                                " case when Element = 'Special Team Leader Stoping Bonus' then 'Special Team Leader Development Bonus' \r\n "+
                                " when Element = 'Production Unit Stoping Bonus' then 'Production Unit Development Bonus' else Element end as Element1, \r\n "+
                                " SUBSTRING(OrgUnit,1,4) MoSec, *  \r\n "+
                                 " from tbl_BCS_ARMS_Interface_TransferNew_Adjustments  \r\n "+
                                " where ProdMonth = '" + txtDisplayMonth.Text + "'  \r\n " +
                                " and Type = '28' and SUBSTRING(OrgUnit,1,4) in (select substring(sectionid,1,4) MO from mineware.dbo.tbl_BCS_Planmonth where prodmonth = '" + ProdMonthTxt.Value + "' and Activity = 1 and Adv > 0 and SUBSTRING(Sectionid,1,4) <> '0153' group by substring(sectionid,1,4)) and ActivityCode = 1 ) a    \r\n " +
                                " Group by MoSec, MyOrder) b on a.MoSec1 = b.MoSec2 and a.MyOrder1 = b.MyOrder2 )c )e on d.MoSec = e.MoSec1 and d.MyOrder = e.MyOrder1  \r\n "+
                                " left outer join  \r\n "+
                                " (select SUBSTRING(Sectionid,1,4) MoSec4, TotalShifts PossibleShifts from mineware.dbo.tbl_BCS_SECCAL  \r\n " +
                                " where Prodmonth = '" + ProdMonthTxt.Value + "' and SUBSTRING(Sectionid,1,4) <> 'VAMP'  \r\n " +
                                " group by SUBSTRING(Sectionid,1,4), TotalShifts ) f on d.MoSec = f.MoSec4 \r\n " +
                                //" where mosec in ('0123', '0733', '0743')";
                                " where mosec in (select substring(sectionid,1,4) MO from mineware.dbo.tbl_BCS_Planmonth where prodmonth = '" + ProdMonthTxt.Value + "' and Activity = 1 group by substring(sectionid,1,4))  ";                             
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ResultsTableName = "MonthlySum";
                _dbMan1.ExecuteInstruction();

                DataTable dt1 = _dbMan1.ResultsDataTable;

                DataSet ds1 = new DataSet();
                ds1.Tables.Add(dt1);

                theReport.RegisterData(ds1);

                theReport.Load("MonthlySumRep.frx");

                //theReport.Design();

                pcReport.Clear();
                theReport.Prepare();
                theReport.Preview = pcReport;
                theReport.ShowPrepared();
            }
        }

        public void LoadMonthlyDetailSum()
        {
            return;

            if (rdbStoping.Checked == true)
            {
                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = "  ";

                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ResultsTableName = "MonthlySumDetail";
                _dbMan1.ExecuteInstruction();

                DataTable dt1 = _dbMan1.ResultsDataTable;

                DataSet ds1 = new DataSet();
                ds1.Tables.Add(dt1);

                theReport.RegisterData(ds1);

                theReport.Load("MonthlySumRep.frx");

                //theReport.Design();

                pcReport.Clear();
                theReport.Prepare();
                theReport.Preview = pcReport;
                theReport.ShowPrepared();
            }
            else
            {
                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = "  ";
                _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan1.ResultsTableName = "MonthlySumDetail";
                _dbMan1.ExecuteInstruction();

                DataTable dt1 = _dbMan1.ResultsDataTable;

                DataSet ds1 = new DataSet();
                ds1.Tables.Add(dt1);

                theReport.RegisterData(ds1);

                theReport.Load("MonthlySumRep.frx");

                //theReport.Design();

                pcReport.Clear();
                theReport.Prepare();
                theReport.Preview = pcReport;
                theReport.ShowPrepared();
            }
        }

        private void showBtn_Click(object sender, EventArgs e)
        {
            if (this.Text == "Monthly Summary")
                LoadMonthlySum();

            if (this.Text == "Monthly Detail Summary")
                LoadMonthlyDetailSum();

            
        }
    }
}
