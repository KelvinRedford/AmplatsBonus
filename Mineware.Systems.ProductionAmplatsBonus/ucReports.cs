using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using DevExpress.XtraNavBar;
using System.Drawing.Drawing2D;
using DevExpress.XtraNavBar.ViewInfo;
using DevExpress.Utils.Drawing;
using FastReport;
using Mineware.Systems.Global;
using Mineware.Systems.ProductionAmplatsGlobal;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.ProductionAmplatsBonus
{
    public partial class ucReports : BaseUserControl
    {
        
        public ucReports()
        {
            InitializeComponent();
            FormRibbonPages.Add(rpReports);
            FormActiveRibbonPage = rpReports;
            FormMainRibbonPage = rpReports;
            RibbonControl = rcReports;

        }

        private string _reportFolder = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\Reports\";
        Procedures proc = new Procedures();

        public string CurReport = "";

        private void frmReports_Load(object sender, EventArgs e)
        {
            editProdmonth.EditValue = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsDate(ProductionAmplatsGlobalTSysSettings._currentProductionMonth.ToString());


            pcReport.Width = this.Width - navBarControl1.Width - 40;

            loadsection();
        }

        private void showBtn_Click(object sender, EventArgs e)
        {
           
        }

       



        private void Close1Btn_Click(object sender, EventArgs e)
        {
            //Close();
        }

    

        private void showBtn_Click_1(object sender, EventArgs e)
        {
            
        }

        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            RepLbl.Text = "Monthly Summary";
            editActivity.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            SecLbl.Visible = false;
            treeView1.Visible = false;
        }

        private void navBarItem5_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            RepLbl.Text = "Monthly Detail Summary";
            editActivity.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            SecLbl.Visible = false;
            treeView1.Visible = false;
        }

        private void navBarItem6_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            RepLbl.Text = "Production Summary";
            editActivity.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            SecLbl.Visible = false;
            treeView1.Visible = false;
        }

        private void Close1Btn_Click_1(object sender, EventArgs e)
        {
            //this.Close();
        }

        private void navBarItem8_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            RepLbl.Text = "Production Summary Tot Mine";
            editActivity.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            SecLbl.Visible = false;
            treeView1.Visible = false;
        }

        private void navBarItem9_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            RepLbl.Text = "Production Unit Results";
            editActivity.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            SecLbl.Visible = false;
            treeView1.Visible = false;
        }

        void loadsection()
        {

            ////////////////////////////////////////////////////////Section/////////////////////////////////////////////////////
            MWDataManager.clsDataAccess _dbManMan1 = new MWDataManager.clsDataAccess();
            _dbManMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManMan1.SqlStatement = "select Sec, OrgUnit, IndustryNumber from ( " +
                                    " select distinct SUBSTRING(Orgunit,1,4) Sec, OrgUnit, IndustryNumber from (select * from tbl_BCS_ARMS_Interface_TransferNew "+
                                    " union select * from mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew) a " +
                                    " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "')q " +
                                    " group by  sec , OrgUnit, IndustryNumber " +
                                    " order by Sec , OrgUnit, IndustryNumber ";

            _dbManMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManMan1.ExecuteInstruction();

            DataTable Data1 = _dbManMan1.ResultsDataTable;

            DataTable Data2 = _dbManMan1.ResultsDataTable;

            DataTable Data3 = _dbManMan1.ResultsDataTable;

            //load tree

            treeView1.Nodes.Clear();


            string Wp = "";
            string Ring = "";
            string Ring1 = "";
            string Ring2 = "";
            string Hole = "";


            string mo = "";
            string crew = "";
            string ind = "";



            for (int i = 0; i < Data1.Rows.Count; i++)
            {
                if (mo != Data1.Rows[i]["sec"].ToString())
                {
                    TreeNode monode = new TreeNode(Data1.Rows[i]["sec"].ToString());
                    monode.NodeFont = new Font("Microsoft Sans Serif", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                    monode.ForeColor = Color.DimGray;

                    treeView1.Nodes.Add(monode);

                    mo = Data1.Rows[i]["sec"].ToString();



                    crew = "";
                    // now load orgunit
                    for (int j = 0; j < Data2.Rows.Count; j++)
                    {
                        if (mo == Data2.Rows[j]["sec"].ToString())
                        {
                            TreeNode crewnode = new TreeNode(Data2.Rows[j]["orgunit"].ToString());
                            crewnode.NodeFont = new Font("Microsoft Sans Serif", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                            crewnode.ForeColor = Color.DimGray;


                            if (crew != Data2.Rows[j]["orgunit"].ToString())
                            {

                                monode.Nodes.Add(crewnode);


                                // do ind
                                for (int k = 0; k < Data3.Rows.Count; k++)
                                {

                                    if (Data2.Rows[j]["orgunit"].ToString() == Data3.Rows[k]["orgunit"].ToString())
                                    {


                                        TreeNode indnode = new TreeNode(Data2.Rows[k]["industrynumber"].ToString());
                                        indnode.NodeFont = new Font("Microsoft Sans Serif", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                                        indnode.ForeColor = Color.DimGray;

                                        crewnode.Nodes.Add(indnode);

                                    }



                                }

                            }

                            crew = Data2.Rows[j]["orgunit"].ToString();

                        }

                    }



                }


            }
        }

        private void navBarItem11_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            RepLbl.Text = "Bonus Letters";

            editActivity.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            SecLbl.Visible = false;
            treeView1.Visible = true;

            
           






           

                
            
        }

        private void navBarItem10_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

            RepLbl.Text = "Top 5 Prod";

            editActivity.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            SecLbl.Visible = false;
            treeView1.Visible = false;

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            IndNoLbl.Text = treeView1.SelectedNode.Text;
            
            //if (treeView1.SelectedNode.Text.Length = 8)

        }

        private void IndNoLbl_Click(object sender, EventArgs e)
        {

        }

        private void navBarControl1_CustomDrawLink(object sender, DevExpress.XtraNavBar.ViewInfo.CustomDrawNavBarElementEventArgs e)
        {
            if (e.ObjectInfo.State == DevExpress.Utils.Drawing.ObjectState.Pressed)
            {
                // if a link is not hot tracked or pressed it is drawn in the normal way
                if (e.ObjectInfo.State == ObjectState.Hot || e.ObjectInfo.State == ObjectState.Pressed)
                {
                    Rectangle rect = e.RealBounds;
                    rect.Inflate(-1, -1);
                    LinearGradientBrush brush;
                    Rectangle imageRect;
                    Rectangle textRect;
                    StringFormat textFormat = new StringFormat();
                    textFormat.LineAlignment = StringAlignment.Center;

                    // identifying the painted link
                    NavLinkInfoArgs linkInfo = e.ObjectInfo as NavLinkInfoArgs;
                    if (linkInfo.Link.Group.GroupCaptionUseImage == NavBarImage.Large)
                    {
                        // adjusting the rectangles for the image and text and specifying the text's alignment
                        // if a large image is displayed within a link
                        imageRect = rect;
                        imageRect.Inflate(-(rect.Width - 32) / 2, -2);
                        textRect = rect;
                        int textHeight = Convert.ToInt16(e.Graphics.MeasureString(e.Caption,
                          e.Appearance.Font).Height);
                        textFormat.Alignment = StringAlignment.Center;
                    }
                    else
                    {
                        // adjusting the rectangles for the image and text and specifying the text's alignment
                        // if a small image is displayed within a link
                        imageRect = rect;
                        imageRect.Width = 16;
                        imageRect.Offset(2, 2);
                        textRect = new Rectangle(rect.Left + 23, rect.Top, rect.Width - 23, rect.Height);
                        textFormat.Alignment = StringAlignment.Near;
                    }



                    // creating different brushes for the hot tracked and pressed states of a link
                    if (e.ObjectInfo.State == ObjectState.Hot)
                    {
                        brush = new LinearGradientBrush(rect, Color.Orange, Color.PeachPuff,
                          LinearGradientMode.Horizontal);
                        // shifting image and text up when a link is hot tracked
                        imageRect.Offset(0, -1);
                        textRect.Offset(0, -1);
                    }
                    else
                        brush = new LinearGradientBrush(rect, Color.YellowGreen, Color.YellowGreen,
                          LinearGradientMode.Horizontal);

                    // painting borders
                    e.Graphics.FillRectangle(new SolidBrush(Color.OrangeRed), e.RealBounds);
                    // painting background
                    e.Graphics.FillRectangle(brush, rect);
                    // painting image
                    if (e.Image != null)
                        e.Graphics.DrawImageUnscaled(e.Image, imageRect);
                    // painting caption
                    e.Graphics.DrawString(e.Caption, e.Appearance.Font, new SolidBrush(Color.Black),
                      textRect, textFormat);
                    // prohibiting default link painting
                    e.Handled = true;
                }

            } 
        }

        private void navBarItem15_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            RepLbl.Text = "Tram Param";
            editActivity.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            SecLbl.Visible = false;
            treeView1.Visible = false;
        }

        private void showBtn_Click_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataSet ReportStoping = new DataSet();

            Report report = new Report();

            String PrevProdmonth = "";
            ////Get Prev prodmonth
            MWDataManager.clsDataAccess _dbManProdMonth = new MWDataManager.clsDataAccess();
            _dbManProdMonth.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManProdMonth.SqlStatement = " select max(prodmonth) prodmonth from mineware.dbo.tbl_BCS_Planning where prodmonth < '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";
            _dbManProdMonth.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbManProdMonth.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbManProdMonth.ResultsTableName = "PrevProdmonth";
            _dbManProdMonth.ExecuteInstruction();

            PrevProdmonth = _dbManProdMonth.ResultsDataTable.Rows[0]["Prodmonth"].ToString();

            if (RepLbl.Text == "Tram Param")
            {
                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan.SqlStatement = " \r\n " +
                                        " select * from mineware.dbo.tbl_Survey_Bonus_TrammingFiguresnew a left outer join mineware.dbo.tbl_Survey_Bonus_TrammingFiguresnewtotal b \r\n " +

                                        " on a.prodmonth = b.prodmonth and a.level = b.level where a.prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";

                _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan.ResultsTableName = "Stoping";
                _dbMan.ExecuteInstruction();

                ReportStoping.Tables.Add(_dbMan.ResultsDataTable);

                ///

                MWDataManager.clsDataAccess _dbMan2 = new MWDataManager.clsDataAccess();
                _dbMan2.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan2.SqlStatement = " \r\n " +
                                        " select * from mineware.dbo.tbl_Survey_Bonus_HoistingFigures \r\n " +

                                        " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";

                _dbMan2.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan2.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan2.ResultsTableName = "Stoping2";
                _dbMan2.ExecuteInstruction();

                ReportStoping.Tables.Add(_dbMan2.ResultsDataTable);

                //

                ///

                MWDataManager.clsDataAccess _dbMan3 = new MWDataManager.clsDataAccess();
                _dbMan3.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan3.SqlStatement = " \r\n " +
                                        " select * from mineware.dbo.tbl_Survey_Bonus_MOReefTypeLink \r\n " +

                                        " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";

                _dbMan3.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan3.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan3.ResultsTableName = "Stoping3";
                _dbMan3.ExecuteInstruction();

                ReportStoping.Tables.Add(_dbMan3.ResultsDataTable);

                //

                ///

                MWDataManager.clsDataAccess _dbMan4 = new MWDataManager.clsDataAccess();
                _dbMan4.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan4.SqlStatement = " select top(1) * from tbl_BCS_TrammingPerNew \r\n " +
                                        //" where prodmonth <= '201605' \r\n " +

                                        " order by prodmonth desc ";

                _dbMan4.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbMan4.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbMan4.ResultsTableName = "Stoping4";
                _dbMan4.ExecuteInstruction();

                ReportStoping.Tables.Add(_dbMan4.ResultsDataTable);

                //

                report.RegisterData(ReportStoping);

                report.Load(_reportFolder+ "TramParamNew.frx");

                // report.Design();

                pcReport.Clear();
                report.Prepare();
                report.Preview = pcReport;
                report.ShowPrepared();
            }

            if (RepLbl.Text == "Monthly Summary")
            {
                if (editActivity.EditValue.ToString() == "0")
                {

                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan.SqlStatement = " \r\n " +
                                            " select * from ( \r\n " +
                                            " select SUM(WorkingShifts) WorkingShifts, SUM(Nett_Amount) finpay, MyOrder, MoSec, Element, ActivityCode Activity, MoSec mo, '" + editActivity.EditValue + "' Prodmonth from (  \r\n " +
                                            " select case when Element = 'Special Team Leader Stoping Bonus' then '0'  \r\n " +
                                            " when Element = 'Machine Operator Stoping Bonus' then '1' \r\n " +
                                            " when Element = 'Production Unit Stoping Bonus' then '2' end as MyOrder, \r\n " +
                                            " SUBSTRING(OrgUnit,1,4) MoSec, * \r\n " +
                                            "  from tbl_BCS_ARMS_Interface_TransferNew \r\n " +
                                            " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n " +
                                            " and Type = '28' and ActivityCode = '0' ) a \r\n " +
                                            " Group by MoSec, MyOrder, Element, ActivityCode ) d \r\n " +
                                            " left outer join \r\n " +
                                            " (select *, finpay2-finpay1 AdjPay from ( \r\n " +
                                            " select * from ( \r\n " +
                                            " select SUM(WorkingShifts) ws1, SUM(Nett_Amount) finpay1, MyOrder MyOrder1, MoSec MoSec1 from ( \r\n " +
                                            " select case when Element = 'Special Team Leader Stoping Bonus' then '0' \r\n " +
                                            " when Element = 'Machine Operator Stoping Bonus' then '1' \r\n " +
                                            " when Element = 'Production Unit Stoping Bonus' then '2' end as MyOrder, \r\n " +
                                            " SUBSTRING(OrgUnit,1,4) MoSec, * \r\n " +
                                            "  from tbl_BCS_ARMS_Interface_TransferNew \r\n " +
                                            " where ProdMonth = '" + PrevProdmonth + "' \r\n " +
                                            " and Type = '28' and ActivityCode = '0' ) a \r\n " +
                                            " Group by MoSec, MyOrder ) a \r\n " +
                                            " left outer join \r\n " +
                                            " (select SUM(WorkingShifts) ws2, SUM(Nett_Amount) finpay2, MyOrder MyOrder2, MoSec MoSec2 from ( \r\n " +
                                            " select case when Element = 'Special Team Leader Stoping Bonus' then '0' \r\n " +
                                            " when Element = 'Machine Operator Stoping Bonus' then '1' \r\n " +
                                            " when Element = 'Production Unit Stoping Bonus' then '2' end as MyOrder, \r\n " +
                                            " SUBSTRING(OrgUnit,1,4) MoSec, * \r\n " +
                                            "  from tbl_BCS_ARMS_Interface_TransferNew_Adjustments \r\n " +
                                            " where ProdMonth = '" + PrevProdmonth + "' \r\n " +
                                            " and Type = '28' and ActivityCode = '0' ) a \r\n " +
                                            " Group by MoSec, MyOrder) b on a.MoSec1 = b.MoSec2 and a.MyOrder1 = b.MyOrder2 )c )e on d.MoSec = e.MoSec1 and d.MyOrder = e.MyOrder1 \r\n " +
                                            " left outer join \r\n " +
                                            " (select SUBSTRING(Sectionid,1,4) MoSec4, TotalShifts PossibleShifts from mineware.dbo.tbl_BCS_SECCAL \r\n " +
                                            " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and SUBSTRING(Sectionid,1,4) <> 'VAMP' \r\n " +
                                            " group by SUBSTRING(Sectionid,1,4), TotalShifts ) f on d.MoSec = f.MoSec4 ";

                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ResultsTableName = "Stoping";
                    _dbMan.ExecuteInstruction();

                    ReportStoping.Tables.Add(_dbMan.ResultsDataTable);

                    report.RegisterData(ReportStoping);

                    report.Load(_reportFolder + "MonthlySumRep.frx");

                    //report.Design();

                    pcReport.Clear();
                    report.Prepare();
                    report.Preview = pcReport;
                    report.ShowPrepared();
                }

                if (editActivity.EditValue.ToString() == "1")
                {
                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan.SqlStatement = " \r\n " +
                                            " select *, WorkingShifts/PossibleShifts incubants from ( \r\n " +
                                            " select SUM(WorkingShifts) WorkingShifts, SUM(Nett_Amount) finpay, MyOrder, MoSec, Element, ActivityCode Activity, MoSec mo, '" + editActivity.EditValue + "' Prodmonth from ( \r\n " +
                                            " select case when Element = 'Special Team Leader Development Bonus' then '0' \r\n " +
                                            " when Element = 'Machine Operator Development Bonus' then '1' \r\n " +
                                            " when Element = 'Production Unit Development Bonus' then '2' end as MyOrder, \r\n " +
                                            " SUBSTRING(OrgUnit,1,4) MoSec, * \r\n " +
                                            "  from tbl_BCS_ARMS_Interface_TransferNew \r\n " +
                                            " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n " +
                                            " and Type = '28' and ActivityCode = '1' ) a \r\n " +
                                            " Group by MoSec, MyOrder, Element, ActivityCode ) d \r\n " +
                                            " left outer join \r\n " +
                                            " (select *, finpay2-finpay1 AdjPay from ( \r\n " +
                                            " select * from ( \r\n " +
                                            " select SUM(WorkingShifts) ws1, SUM(Nett_Amount) finpay1, MyOrder MyOrder1, MoSec MoSec1 from ( \r\n " +
                                            " select case when Element = 'Special Team Leader Development Bonus' then '0' \r\n " +
                                            " when Element = 'Machine Operator Development Bonus' then '1' \r\n " +
                                            " when Element = 'Production Unit Development Bonus' then '2' end as MyOrder, \r\n " +
                                            " SUBSTRING(OrgUnit,1,4) MoSec, * \r\n " +
                                            "  from tbl_BCS_ARMS_Interface_TransferNew \r\n " +
                                            " where ProdMonth = '" + PrevProdmonth + "' \r\n " +
                                            " and Type = '28' and ActivityCode = '1' ) a \r\n " +
                                            " Group by MoSec, MyOrder ) a \r\n " +
                                            " left outer join \r\n " +
                                            " (select SUM(WorkingShifts) ws2, SUM(Nett_Amount) finpay2, MyOrder MyOrder2, MoSec MoSec2 from ( \r\n " +
                                            " select case when Element = 'Special Team Leader Development Bonus' then '0' \r\n " +
                                            " when Element = 'Machine Operator Development Bonus' then '1' \r\n " +
                                            " when Element = 'Production Unit Development Bonus' then '2' end as MyOrder, \r\n " +
                                            " SUBSTRING(OrgUnit,1,4) MoSec, * \r\n " +
                                            "  from tbl_BCS_ARMS_Interface_TransferNew_Adjustments \r\n " +
                                            " where ProdMonth = '" + PrevProdmonth + "' \r\n " +
                                            " and Type = '28' and ActivityCode = '1' ) a \r\n " +
                                            " Group by MoSec, MyOrder) b on a.MoSec1 = b.MoSec2 and a.MyOrder1 = b.MyOrder2 )c )e on d.MoSec = e.MoSec1 and d.MyOrder = e.MyOrder1 \r\n " +
                                            " left outer join \r\n " +
                                            " (select SUBSTRING(Sectionid,1,4) MoSec4, TotalShifts PossibleShifts from mineware.dbo.tbl_BCS_SECCAL \r\n " +
                                            " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and SUBSTRING(Sectionid,1,4) <> 'VAMP' \r\n " +
                                            " group by SUBSTRING(Sectionid,1,4), TotalShifts ) f on d.MoSec = f.MoSec4 ";
                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ResultsTableName = "Stoping";
                    _dbMan.ExecuteInstruction();

                    ReportStoping.Tables.Add(_dbMan.ResultsDataTable);

                    report.RegisterData(ReportStoping);

                    report.Load(_reportFolder + "MonthlySumRep.frx");

                    //report.Design();

                    pcReport.Clear();
                    report.Prepare();
                    report.Preview = pcReport;
                    report.ShowPrepared();
                }
            }

            if (RepLbl.Text == "Monthly Detail Summary")
            {
                if (editActivity.EditValue.ToString() == "0")
                {

                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan.SqlStatement = " \r\n " +

                                                " select * from ( \r\n " +
                                                " select 'Stoping' Activity, 'a' a, SUBSTRING(Orgunit,3,2) section, Category, initials, surname, orgunit, \r\n " +
                                                " workingshifts,  nett_amount, industrynumber, prodmonth, \r\n " +
                                                " case when Category = 'Stoper' then 1 \r\n " +
                                                " when Category = 'Dayshift Shiftboss' then 2 \r\n " +
                                                " when Category = 'Nightshift Shiftboss' then 3 \r\n " +
                                                " when Category = 'Nightshift Cleaner' then 4 \r\n " +
                                                " end as OccupationID \r\n " +
                                                "  from dbo.tbl_BCS_ARMS_Interface_TransferNew \r\n " +
                                                " where type = '08'  and prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and ActivityCode = '0' \r\n " +
                                                " ) a \r\n " +
                                                " left outer join \r\n " +
                                                " (select Activity2, a2, section2, Category2, initials2, surname2, orgunit2, \r\n " +
                                                " workingshifts2, (nett_amount2 - nett_amount1) Nett_Amount2, industrynumber2, prodmonth2, \r\n " +
                                                " OccupationID2, DisplayMonth2  from ( \r\n " +
                                                " select * from ( \r\n " +
                                                " select 'Stoping' Activity1, 'a' a1, SUBSTRING(Orgunit,3,2) section1, \r\n " +
                                                " Category Category1, initials initials1, surname surname1, orgunit orgunit1, \r\n " +
                                                " workingshifts workingshifts1,  nett_amount nett_amount1, industrynumber industrynumber1, \r\n " +
                                                " prodmonth prodmonth1, \r\n " +
                                                " case when Category = 'Stoper' then 1 \r\n " +
                                                " when Category = 'Dayshift Shiftboss' then 2 \r\n " +
                                                " when Category = 'Nightshift Shiftboss' then 3 \r\n " +
                                                " when Category = 'Nightshift Cleaner' then 4 \r\n " +
                                                " end as OccupationID1 \r\n " +
                                                 " from dbo.tbl_BCS_ARMS_Interface_TransferNew \r\n " +
                                                " where type = '08'  and ProdMonth = '" + PrevProdmonth + "' and ActivityCode = '0'  ) a \r\n " +
                                                " left outer join \r\n " +
                                                " (select 'Stoping' Activity2, 'a' a2, SUBSTRING(Orgunit,3,2) section2, \r\n " +
                                                " Category Category2, initials initials2, surname surname2, orgunit orgunit2, \r\n " +
                                                " workingshifts workingshifts2,  nett_amount nett_amount2, industrynumber industrynumber2, \r\n " +
                                                " prodmonth prodmonth2, \r\n " +
                                                " case when Category = 'Stoper' then 1 \r\n " +
                                                " when Category = 'Dayshift Shiftboss' then 2 \r\n " +
                                                " when Category = 'Nightshift Shiftboss' then 3 \r\n " +
                                                " when Category = 'Nightshift Cleaner' then 4 \r\n " +
                                                " end as OccupationID2, DisplayMonth DisplayMonth2 \r\n " +
                                                 " from dbo.tbl_BCS_ARMS_Interface_TransferNew_Adjustments \r\n " +
                                                " where type = '08'  and ProdMonth = '" + PrevProdmonth + "'  and ActivityCode = '0' ) b \r\n " +
                                                " on a.industrynumber1 = b.industrynumber2 and a.orgunit1 = b.orgunit2 ) c where Activity2 is not null \r\n " +
                                                " and DisplayMonth2 = '" + editActivity.EditValue + "') b on a.IndustryNumber = b.industrynumber2 and a.OrgUnit = b.orgunit2 \r\n " +
                                                " left outer join \r\n " +
                                                " (select distinct(substring(b.SectionID,3,2)) mo1, PossibleShifts from ( \r\n " +
                                                " select SectionID, Prodmonth, Hierarchicalid, ReportToSectionid from mineware.dbo.tbl_BCS_SECTION\r\n " +
                                                " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n " +
                                                " and Hierarchicalid = '5' and SectionID like '0%'  ) a \r\n " +
                                                " left outer join \r\n " +
                                                " (select SectionID, Prodmonth, Hierarchicalid from mineware.dbo.tbl_BCS_SECTION\r\n " +
                                                " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n " +
                                                " and Hierarchicalid = '4' ) b on a.ReportToSectionid = b.SectionID \r\n " +
                                                " left outer join \r\n " +
                                                " (select Sectionid, Prodmonth, TotalShifts PossibleShifts from mineware.dbo.tbl_BCS_SECCAL \r\n " +
                                                " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') c on a.SectionID = c.Sectionid \r\n " +
                                                " where b.SectionID in ( \r\n " +
                                                " select distinct(substring(OrgUnit,1,4)) Org from Mineware.dbo.tbl_BCS_StopingRepNew \r\n " +
                                                " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "')) c on a.section = c.mo1 order by section, OccupationID \r\n " +

                                            "  ";

                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ResultsTableName = "Stoping";
                    _dbMan.ExecuteInstruction();

                    ReportStoping.Tables.Add(_dbMan.ResultsDataTable);

                    report.RegisterData(ReportStoping);

                    report.Load(_reportFolder + "MonthlySumDetRep.frx");

                    //report.Design();

                    pcReport.Clear();
                    report.Prepare();
                    report.Preview = pcReport;
                    report.ShowPrepared();
                }

                if (editActivity.EditValue.ToString() == "1")
                {
                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan.SqlStatement = " \r\n " +

                                                " select * from (\r\n " +
                                                " select 'Development' Activity, 'a' a, SUBSTRING(Orgunit,3,2) section, Category, initials, surname, orgunit,\r\n " +
                                                " workingshifts,  nett_amount, industrynumber, prodmonth,\r\n " +
                                                 " case when Category = 'Developer' then 1\r\n " +
                                                " when Category = 'Dayshift Shiftboss' then 2\r\n " +
                                                " when Category = 'Nightshift Shiftboss' then 3\r\n " +
                                                " when Category = 'Nightshift Cleaner' then 4\r\n " +
                                                " end as OccupationID\r\n " +
                                                "  from dbo.tbl_BCS_ARMS_Interface_TransferNew\r\n " +
                                                " where type = '08' and prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' and ActivityCode = '1'\r\n " +
                                                " ) a\r\n " +
                                                " left outer join\r\n " +
                                                " (select Activity2, a2, section2, Category2, initials2, surname2, orgunit2,\r\n " +
                                                " workingshifts2, (nett_amount2 - nett_amount1) Nett_Amount2, industrynumber2, prodmonth2,\r\n " +
                                                " OccupationID2, DisplayMonth2  from (\r\n " +
                                                " select * from (\r\n " +
                                                " select 'Development' Activity1, 'a' a1, SUBSTRING(Orgunit,3,2) section1,\r\n " +
                                                " Category Category1, initials initials1, surname surname1, orgunit orgunit1,\r\n " +
                                                " workingshifts workingshifts1,  nett_amount nett_amount1, industrynumber industrynumber1,\r\n " +
                                                " prodmonth prodmonth1,\r\n " +
                                                " case when Category = 'Developer' then 1\r\n " +
                                                " when Category = 'Dayshift Shiftboss' then 2\r\n " +
                                                " when Category = 'Nightshift Shiftboss' then 3\r\n " +
                                                " when Category = 'Nightshift Cleaner' then 4\r\n " +
                                                " end as OccupationID1\r\n " +
                                                "  from dbo.tbl_BCS_ARMS_Interface_TransferNew\r\n " +
                                                " where type = '08'  and ProdMonth = '" + PrevProdmonth + "'  and ActivityCode = '1'  ) a\r\n " +
                                                " left outer join\r\n " +
                                                " (select 'Stoping' Activity2, 'a' a2, SUBSTRING(Orgunit,3,2) section2,\r\n " +
                                                " Category Category2, initials initials2, surname surname2, orgunit orgunit2,\r\n " +
                                                " workingshifts workingshifts2,  nett_amount nett_amount2, industrynumber industrynumber2,\r\n " +
                                                " prodmonth prodmonth2,\r\n " +
                                                " case when Category = 'Developer' then 1\r\n " +
                                                " when Category = 'Dayshift Shiftboss' then 2\r\n " +
                                                " when Category = 'Nightshift Shiftboss' then 3\r\n " +
                                                " when Category = 'Nightshift Cleaner' then 4\r\n " +
                                                " end as OccupationID2, DisplayMonth DisplayMonth2\r\n " +
                                                "  from dbo.tbl_BCS_ARMS_Interface_TransferNew_Adjustments\r\n " +
                                                " where type = '08'  and ProdMonth = '" + PrevProdmonth + "'  and ActivityCode = '1' ) b\r\n " +
                                                " on a.industrynumber1 = b.industrynumber2 and a.orgunit1 = b.orgunit2 ) c where Activity2 is not null\r\n " +
                                                " and DisplayMonth2 = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ) b on a.IndustryNumber = b.industrynumber2 and a.OrgUnit = b.orgunit2\r\n " +
                                                " left outer join\r\n " +
                                                " (select distinct(substring(b.SectionID,3,2)) mo1, PossibleShifts from (\r\n " +
                                                " select SectionID, Prodmonth, Hierarchicalid, ReportToSectionid from mineware.dbo.tbl_BCS_SECTION\r\n " +
                                                " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "'\r\n " +
                                                " and Hierarchicalid = '5' and SectionID like '0%'  ) a\r\n " +
                                                " left outer join\r\n " +
                                                " (select SectionID, Prodmonth, Hierarchicalid from mineware.dbo.tbl_BCS_SECTION\r\n " +
                                                " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "'\r\n " +
                                                " and Hierarchicalid = '4' ) b on a.ReportToSectionid = b.SectionID\r\n " +
                                                " left outer join\r\n " +
                                                " (select Sectionid, Prodmonth, TotalShifts PossibleShifts from mineware.dbo.tbl_BCS_SECCAL\r\n " +
                                                " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "') c on a.SectionID = c.Sectionid\r\n " +
                                                " where b.SectionID in (\r\n " +
                                                " select distinct(substring(OrgUnit,1,4)) Org from Mineware.dbo.tbl_BCS_DevRepNew\r\n " +
                                                " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "')) c on a.section = c.mo1 order by section, OccupationID \r\n " +

                                                                    "  ";

                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ResultsTableName = "Stoping";
                    _dbMan.ExecuteInstruction();

                    ReportStoping.Tables.Add(_dbMan.ResultsDataTable);

                    report.RegisterData(ReportStoping);

                    report.Load(_reportFolder + "MonthlySumDetRep.frx");

                    //report.Design();

                    pcReport.Clear();
                    report.Prepare();
                    report.Preview = pcReport;
                    report.ShowPrepared();
                }




            }

            if (RepLbl.Text == "Production Summary")
            {
                if (editActivity.EditValue.ToString() == "0")
                {

                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan.SqlStatement = " \r\n " +
                                     "  select 'Stoping' Act, *, Crew_ws/pos Crew_ppl, ws/pos ppl, b_ws/pos b_ppl, c_ws/pos c_ppl, Cont_ws/pos Cont_ppl from ( select * from ( select * from ( \r\n " +
                                    " select 'a' a, * from ( \r\n " +
                                    " select * from ( \r\n " +
                                    " select occupationID, occupation, section, COUNT(IndustryNumber) incubants, SUM(Nett_Amount) nett_amount, ProdMonth, SUM(WorkingShifts) ws from ( \r\n " +
                                    " select SUBSTRING(OrgUnit,3,2) section, * from ( \r\n " +
                                    " select case when occupation = 'D/S ShiftBoss' then '1' \r\n " +
                                    "  end as occupationID,  * from ( \r\n " +
                                    " select case when category = 'Dayshift Shiftboss' then 'D/S ShiftBoss' \r\n " +
                                    "  end as Occupation, \r\n " +
                                    "  * from tbl_BCS_ARMS_Interface_TransferNew \r\n " +
                                    " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "' and ActivityCode = '0' \r\n " +
                                    "  )a )b )c \r\n " +
                                    " where occupationID is not null \r\n " +
                                    " group by section, occupationID, Occupation, ProdMonth \r\n " +
                                    "  )a \r\n " +
                                    " left outer join \r\n " +
                                    " (select * from (select occupationID b_occupationID, occupation b_occupation, \r\n " +
                                    " section b_section, COUNT(IndustryNumber) b_incubants, SUM(Nett_Amount) b_nett_amount, ProdMonth b_ProdMonth, SUM(WorkingShifts) b_ws from ( \r\n " +
                                    " select SUBSTRING(OrgUnit,3,2) section, * from ( \r\n " +
                                    " select case \r\n " +
                                    " when occupation = 'N/S ShiftBoss' then '2' \r\n " +
                                    " end as occupationID,  * from ( \r\n " +
                                    " select case \r\n " +
                                    " when category = 'Nightshift Shiftboss' then 'N/S ShiftBoss' \r\n " +
                                    "  end as Occupation, \r\n " +
                                    "  * from tbl_BCS_ARMS_Interface_TransferNew \r\n " +
                                    " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "' and ActivityCode = '0' \r\n " +
                                    "  )a )b )c \r\n " +
                                    " where occupationID is not null \r\n " +
                                    " group by section, occupationID, Occupation, ProdMonth )d \r\n " +
                                    "  )b \r\n " +
                                    " on a.section = b.b_section \r\n " +
                                    " ) aa \r\n " +
                                    " left outer join \r\n " +
                                    " (select * from (select occupationID c_occupationID, occupation c_occupation, \r\n " +
                                    " section c_section, COUNT(IndustryNumber) c_incubants, SUM(Nett_Amount) c_nett_amount, ProdMonth c_ProdMonth, SUM(WorkingShifts) c_ws from ( \r\n " +
                                    " select SUBSTRING(OrgUnit,3,2) section, * from ( \r\n " +
                                    " select \r\n " +
                                    " case \r\n " +
                                    " when occupation = 'N/S Cleaners' then '3' \r\n " +
                                    "  end as occupationID,  * from ( \r\n " +
                                    " select case \r\n " +
                                    " when Category = 'Nightshift Cleaner' then 'N/S Cleaners' \r\n " +
                                    "  end as Occupation, \r\n " +
                                    "  * from tbl_BCS_ARMS_Interface_TransferNew \r\n " +
                                    " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "' and ActivityCode = '0' \r\n " +
                                    "  )a )b )c \r\n " +
                                    " where occupationID is not null \r\n " +
                                    " group by section, occupationID, Occupation, ProdMonth )d \r\n " +
                                    "  )b \r\n " +
                                    " on aa.section = b.c_section \r\n " +
                                    " )c \r\n " +
                                    " left outer join \r\n " +
                                    " (select occupationID Crew_occupationID, occupation Crew_occupation, \r\n " +
                                    " section Crew_section, COUNT(IndustryNumber) Crew_incubants, SUM(Nett_Amount) Crew_nett_amount, \r\n " +
                                    " ProdMonth Crew_ProdMonth, SUM(WorkingShifts) Crew_ws from ( \r\n " +
                                    " select SUBSTRING(OrgUnit,3,2) section, * from ( \r\n " +
                                    " select \r\n " +
                                    " case \r\n " +
                                    " when occupation = 'Crew' then '4' \r\n " +
                                    "  end as occupationID,  * from ( \r\n " +
                                    " select case \r\n " +
                                    " when Category <> 'Other Stoping1' then 'Crew' \r\n " +
                                    "  end as Occupation, \r\n " +
                                    "  * from tbl_BCS_ARMS_Interface_TransferNew \r\n " +
                                    " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "' and ActivityCode = '0' and type = 28 \r\n " +
                                    " )a )b )c \r\n " +
                                    " where occupationID is not null \r\n " +
                                    " group by section, occupationID, Occupation, ProdMonth \r\n " +
                                    " )aa \r\n " +
                                    " on aa.Crew_section = c.section \r\n " +
                                    " )d \r\n " +
                                    " left outer join \r\n " +
                                    " (select occupationID Cont_occupationID, occupation Cont_occupation, \r\n " +
                                    " section Cont_section, COUNT(IndustryNumber) Cont_incubants, SUM(Nett_Amount) Cont_nett_amount, \r\n " +
                                    " ProdMonth Cont_ProdMonth, SUM(WorkingShifts) Cont_ws, max(pos) pos from ( \r\n " +
                                    " select SUBSTRING(OrgUnit,3,2) section, * from ( \r\n " +
                                    " select \r\n " +
                                    " case \r\n " +
                                    " when occupation = 'Contractor' then '5' \r\n " +
                                    "  end as occupationID,  * from ( \r\n " +
                                    " select case \r\n " +
                                    " when Category = 'Stoper' then 'Contractor' \r\n " +
                                    "  end as Occupation, \r\n " +
                                    "  * from tbl_BCS_ARMS_Interface_TransferNew \r\n " +
                                    " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "' and ActivityCode = '0' \r\n " +
                                    "  )a )b \r\n " +
                                     " )c \r\n " +
                                     "  left outer join   \r\n " +
                                     " (select aprodmonth, mo1, max(pos) pos from (select ProdMonth aProdMonth, SUBSTRING(OrgUnit,3,2) mo1, PossibleShifts pos from tbl_BCS_StopingRepNew \r\n " +
                                     " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "')a group by aprodmonth, mo1) x on c.section = x.mo1  \r\n " +
                                    " where occupationID is not null \r\n " +
                                    " group by section, occupationID, Occupation, c.ProdMonth \r\n " +
                                    " ) cont \r\n " +
                                    " on d.section = cont.Cont_section \r\n " +
                                    " ) q \r\n " +
                                    " order by q.section, q.b_section, q.c_section, q.Crew_section, q.Cont_section asc \r\n " +


                                            "  ";

                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ResultsTableName = "Stoping";
                    _dbMan.ExecuteInstruction();

                    ReportStoping.Tables.Add(_dbMan.ResultsDataTable);

                    report.RegisterData(ReportStoping);

                    report.Load(_reportFolder + "ProdSum.frx");

                    //report.Design();

                    pcReport.Clear();
                    report.Prepare();
                    report.Preview = pcReport;
                    report.ShowPrepared();
                }


                if (editActivity.EditValue.ToString() == "1")
                {
                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan.SqlStatement = " \r\n " +

                      "  select 'Development' Act,*, Crew_ws/pos Crew_ppl, ws/pos ppl, b_ws/pos b_ppl, c_ws/pos c_ppl, Cont_ws/pos Cont_ppl from ( select * from ( select * from ( \r\n " +
                                        " select 'a' a, * from ( \r\n " +
                                        " select * from ( \r\n " +
                                        " select occupationID, occupation, section, COUNT(IndustryNumber) incubants, SUM(Nett_Amount) nett_amount, ProdMonth, SUM(WorkingShifts) ws from ( \r\n " +
                                        " select SUBSTRING(OrgUnit,3,2) section, * from ( \r\n " +
                                        " select case when occupation = 'D/S ShiftBoss' then '1' \r\n " +
                                        "  end as occupationID,  * from ( \r\n " +
                                        " select case when category = 'Dayshift Shiftboss' then 'D/S ShiftBoss' \r\n " +
                                        "  end as Occupation, \r\n " +
                                        "  * from tbl_BCS_ARMS_Interface_TransferNew \r\n " +
                                        " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "' and ActivityCode = '1' \r\n " +
                                        "  )a )b )c \r\n " +
                                        " where occupationID is not null \r\n " +
                                        " group by section, occupationID, Occupation, ProdMonth \r\n " +
                                         " )a \r\n " +
                                        " left outer join \r\n " +
                                        " (select * from (select occupationID b_occupationID, occupation b_occupation, \r\n " +
                                        " section b_section, COUNT(IndustryNumber) b_incubants, SUM(Nett_Amount) b_nett_amount, ProdMonth b_ProdMonth, SUM(WorkingShifts) b_ws from ( \r\n " +
                                        " select SUBSTRING(OrgUnit,3,2) section, * from ( \r\n " +
                                        " select case \r\n " +
                                        " when occupation = 'N/S ShiftBoss' then '2' \r\n " +
                                        " end as occupationID,  * from ( \r\n " +
                                        " select case \r\n " +
                                        " when category = 'Nightshift Shiftboss' then 'N/S ShiftBoss' \r\n " +
                                        "  end as Occupation, \r\n " +
                                        "  * from tbl_BCS_ARMS_Interface_TransferNew \r\n " +
                                        " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "' and ActivityCode = '1' \r\n " +
                                        "  )a )b )c \r\n " +
                                        " where occupationID is not null \r\n " +
                                        " group by section, occupationID, Occupation, ProdMonth )d \r\n " +
                                        "  )b \r\n " +
                                        " on a.section = b.b_section \r\n " +
                                        " ) aa \r\n " +
                                        " left outer join \r\n " +
                                        " (select * from (select occupationID c_occupationID, occupation c_occupation, \r\n " +
                                        " section c_section, COUNT(IndustryNumber) c_incubants, SUM(Nett_Amount) c_nett_amount, ProdMonth c_ProdMonth, SUM(WorkingShifts) c_ws from ( \r\n " +
                                        " select SUBSTRING(OrgUnit,3,2) section, * from ( \r\n " +
                                        " select \r\n " +
                                        " case \r\n " +
                                        " when occupation = 'N/S Cleaners' then '3' \r\n " +
                                         " end as occupationID,  * from ( \r\n " +
                                        " select case \r\n " +
                                        " when Category = 'Nightshift Cleaner' then 'N/S Cleaners' \r\n " +
                                        "  end as Occupation, \r\n " +
                                        "  * from tbl_BCS_ARMS_Interface_TransferNew \r\n " +
                                        " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "' and ActivityCode = '1' \r\n " +
                                        "  )a )b )c \r\n " +
                                        " where occupationID is not null \r\n " +
                                        " group by section, occupationID, Occupation, ProdMonth )d \r\n " +
                                        "  )b \r\n " +
                                        " on aa.section = b.c_section \r\n " +
                                        " )c \r\n " +
                                        " left outer join \r\n " +
                                        " (select occupationID Crew_occupationID, occupation Crew_occupation, \r\n " +
                                        " section Crew_section, COUNT(IndustryNumber) Crew_incubants, SUM(Nett_Amount) Crew_nett_amount, \r\n " +
                                        " ProdMonth Crew_ProdMonth, SUM(WorkingShifts) Crew_ws from ( \r\n " +
                                        " select SUBSTRING(OrgUnit,3,2) section, * from ( \r\n " +
                                        " select \r\n " +
                                        " case \r\n " +
                                        " when occupation = 'Crew' then '4' \r\n " +
                                        "  end as occupationID,  * from ( \r\n " +
                                        " select case \r\n " +
                                        " when type = '28' then 'Crew' \r\n " +
                                        "  end as Occupation, \r\n " +
                                        "  * from tbl_BCS_ARMS_Interface_TransferNew \r\n " +
                                        " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "' and ActivityCode = '1' \r\n " +
                                        "  )a )b )c \r\n " +
                                        " where occupationID is not null \r\n " +
                                        " group by section, occupationID, Occupation, ProdMonth \r\n " +
                                        " )aa \r\n " +
                                        " on aa.Crew_section = c.section \r\n " +
                                        " )d \r\n " +
                                        " left outer join \r\n " +
                                        " (select occupationID Cont_occupationID, occupation Cont_occupation, \r\n " +
                                        " section Cont_section, COUNT(IndustryNumber) Cont_incubants, SUM(Nett_Amount) Cont_nett_amount, \r\n " +
                                        " ProdMonth Cont_ProdMonth, SUM(WorkingShifts) Cont_ws, max(pos) pos from ( \r\n " +
                                        " select SUBSTRING(OrgUnit,3,2) section, * from ( \r\n " +
                                        " select \r\n " +
                                        " case \r\n " +
                                        " when occupation = 'Contractor' then '5' \r\n " +
                                        "  end as occupationID,  * from ( \r\n " +
                                        " select case \r\n " +
                                        " when Category = 'Developer' then 'Contractor' \r\n " +
                                        "  end as Occupation, \r\n " +
                                        "  * from tbl_BCS_ARMS_Interface_TransferNew \r\n " +
                                        " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "' and ActivityCode = '1' \r\n " +
                                        "  )a )b )c \r\n " +
                                        "   left outer join \r\n " +
                                        "  (select aprodmonth, mo1, max(pos) pos from (select ProdMonth aProdMonth, SUBSTRING(OrgUnit,3,2) mo1, PossibleShifts pos from tbl_BCS_DevRepNew \r\n " +
                                        "  where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "')a group by aprodmonth, mo1) x on c.section = x.mo1 \r\n " +
                                        " where occupationID is not null \r\n " +
                                        " group by section, occupationID, Occupation, c.ProdMonth \r\n " +
                                        " ) cont \r\n " +
                                        " on d.section = cont.Cont_section \r\n " +
                                        " ) q \r\n " +
                                        " order by q.section, q.b_section, q.c_section, q.Crew_section, q.Cont_section asc \r\n " +

                                                                    "  ";

                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ResultsTableName = "Stoping";
                    _dbMan.ExecuteInstruction();

                    ReportStoping.Tables.Add(_dbMan.ResultsDataTable);

                    report.RegisterData(ReportStoping);

                    report.Load(_reportFolder + "ProdSum.frx");

                    // report.Design();

                    pcReport.Clear();
                    report.Prepare();
                    report.Preview = pcReport;
                    report.ShowPrepared();
                }
            }

            if (RepLbl.Text == "Production Unit Results")
            {
                if (editActivity.EditValue.ToString() == "0")
                {

                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan.SqlStatement = " \r\n " +

                                " select 'a' a, 'Stoping' Act, SUBSTRING(OrgUnit,1,4) MO, \r\n " +
                                " case when sUBSTRING(workplace,5,1) <> 'A' then \r\n " +
                                " SUBSTRING(workplace,6,1) else SUBSTRING(workplace,7,1) end as reef, \r\n " +
                                " case when Shift = 'D' then OrgUnit else '' end as DS, \r\n " +
                                " case when LEN(OrgUnit) = 8 then \r\n " +
                                " case when substring(Orgunit,7,1) = 'M' then SUBSTRING(OrgUnit,1,6) + 'N' + SUBSTRING(OrgUnit,8,5) \r\n " +
                                " when substring(Orgunit,7,1) = 'U' then SUBSTRING(OrgUnit,1,6) + 'P' + SUBSTRING(OrgUnit,8,5) \r\n " +
                                " end \r\n " +
                                " else \r\n " +
                                " case when substring(Orgunit,8,1) = 'M' then SUBSTRING(OrgUnit,1,7) + 'N' + SUBSTRING(OrgUnit,9,5) \r\n " +
                                " when substring(Orgunit,7,1) = 'U' then SUBSTRING(OrgUnit,1,7) + 'P' + SUBSTRING(OrgUnit,9,5) \r\n " +
                                " end \r\n " +
                                " end as NS, \r\n " +
                                " * from dbo.tbl_BCS_StopingRepNew \r\n " +
                                " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "' \r\n " +
                                " union \r\n " +
                                " select 'a' a, 'Stoping' Act, SUBSTRING(OrgUnit,1,4) MO, \r\n " +
                                " case when sUBSTRING(workplace,5,1) <> 'A' then \r\n " +
                                " SUBSTRING(workplace,6,1) else SUBSTRING(workplace,7,1) end as reef, \r\n " +
                                " case when Shift = 'D' then OrgUnit else '' end as DS, \r\n " +
                                " case when LEN(OrgUnit) = 8 then  \r\n " +
                                " case when substring(Orgunit,7,1) = 'M' then SUBSTRING(OrgUnit,1,6) + 'N' + SUBSTRING(OrgUnit,8,5) \r\n " +
                                " when substring(Orgunit,7,1) = 'U' then SUBSTRING(OrgUnit,1,6) + 'P' + SUBSTRING(OrgUnit,8,5) \r\n " +
                                " end \r\n " +
                                " else \r\n " +
                                " case when substring(Orgunit,8,1) = 'M' then SUBSTRING(OrgUnit,1,7) + 'N' + SUBSTRING(OrgUnit,9,5) \r\n " +
                                "  when substring(Orgunit,7,1) = 'U' then SUBSTRING(OrgUnit,1,7) + 'P' + SUBSTRING(OrgUnit,9,5) \r\n " +
                                " end \r\n " +
                                " end as NS, \r\n " +
                                " * from dbo.tbl_BCS_ZRepNew \r\n " +
                                " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "' \r\n " +
                                " union \r\n " +
                                " select 'a' a, 'Stoping' Act, SUBSTRING(OrgUnit,1,4) MO, \r\n " +
                                " case when sUBSTRING(workplace,5,1) <> 'A' then \r\n " +
                                " SUBSTRING(workplace,6,1) else SUBSTRING(workplace,7,1) end as reef, \r\n " +
                                " case when Shift = 'D' then OrgUnit else '' end as DS, \r\n " +
                                " case when LEN(OrgUnit) = 8 then \r\n " +
                                " case when substring(Orgunit,7,1) = 'M' then SUBSTRING(OrgUnit,1,6) + 'N' + SUBSTRING(OrgUnit,8,5) \r\n " +
                                "  when substring(Orgunit,7,1) = 'U' then SUBSTRING(OrgUnit,1,6) + 'P' + SUBSTRING(OrgUnit,8,5) \r\n " +
                                " end \r\n " +
                                " else \r\n " +
                                " case when substring(Orgunit,8,1) = 'M' then SUBSTRING(OrgUnit,1,7) + 'N' + SUBSTRING(OrgUnit,9,5) \r\n " +
                                " when substring(Orgunit,7,1) = 'U' then SUBSTRING(OrgUnit,1,7) + 'P' + SUBSTRING(OrgUnit,9,5) \r\n " +
                                " end \r\n " +
                                " end as NS, \r\n " +
                                " * from dbo.tbl_BCS_StopingRepNew_Construction \r\n " +
                                " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "' \r\n " +
                                " order by OrgUnit \r\n " +

                                                                                            "  ";

                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ResultsTableName = "Stoping";
                    _dbMan.ExecuteInstruction();

                    ReportStoping.Tables.Add(_dbMan.ResultsDataTable);

                    report.RegisterData(ReportStoping);

                    report.Load(_reportFolder + "ProdUnitRes.frx");

                    //report.Design();

                    pcReport.Clear();
                    report.Prepare();
                    report.Preview = pcReport;
                    report.ShowPrepared();
                }

                if (editActivity.EditValue.ToString() == "1")
                {

                    MWDataManager.clsDataAccess _dbManDev = new MWDataManager.clsDataAccess();
                    _dbManDev.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManDev.SqlStatement = " \r\n " +

                                        " select 'a' a, 'Development' Act, SUBSTRING(OrgUnit,1,4) MO, \r\n " +
                                        " case when SUBSTRING(workplace,5,1) <> 'A' then \r\n " +
                                        " SUBSTRING(workplace,6,1) else SUBSTRING(workplace,7,1) end as reef, \r\n " +
                                        " case when Shift = 'D' then OrgUnit else '' end as DS, case when Shift = 'N' then OrgUnit else '' end as NS, \r\n " +
                                        " * from dbo.tbl_BCS_DevRepNew \r\n " +
                                        " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "' \r\n " +
                                        " union \r\n " +
                                        " select 'a' a, 'Development' Act, SUBSTRING(OrgUnit,1,4) MO, \r\n " +
                                        " case when SUBSTRING(workplace,5,1) <> 'A' then \r\n " +
                                        " SUBSTRING(workplace,6,1) else SUBSTRING(workplace,7,1) end as reef, \r\n " +
                                        " case when Shift = 'D' then OrgUnit else '' end as DS, case when Shift = 'N' then OrgUnit else '' end as NS, \r\n " +
                                        " * from dbo.tbl_BCS_DevRepNew_Rail \r\n " +
                                        " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "' \r\n " +
                                        " order by OrgUnit \r\n " +

                    "  ";

                    _dbManDev.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManDev.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManDev.ResultsTableName = "Stoping";
                    _dbManDev.ExecuteInstruction();

                    ReportStoping.Tables.Add(_dbManDev.ResultsDataTable);

                    report.RegisterData(ReportStoping);

                    report.Load(_reportFolder + "ProdUnitResDev.frx");

                    //report.Design();

                    pcReport.Clear();
                    report.Prepare();
                    report.Preview = pcReport;
                    report.ShowPrepared();
                }
            }

            if (RepLbl.Text == "Bonus Letters")
            {




                if (IndNoLbl.Text.Length == 8)
                {

                    MWDataManager.clsDataAccess _dbManDev = new MWDataManager.clsDataAccess();
                    _dbManDev.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbManDev.SqlStatement = " select '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "' prodmonth, industrynumber, substring(Orgunit,1,6) Orgunit,  \r\n " +

                                        "  surname, Initials, Workingshifts, nn Nett_Amount,\r\n " +
                                        " workplace, category, timestamp \r\n" +
                                        " from (select *, Nett_Amount nn from tbl_BCS_ARMS_Interface_TransferNew  union select *, finpay nn from mineware.dbo.tbl_BCS_ARMS_Interface_TransferNew) a \r\n " +
                                        " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "' \r\n " +
                                        " and industrynumber = '" + IndNoLbl.Text + "' \r\n " +
                                        "  \r\n " +
                                        "  \r\n " +
                                        "  \r\n " +
                                        "  \r\n " +
                                        "  \r\n " +
                                        "  \r\n " +
                                        "  \r\n " +
                                        " \r\n " +
                                        "  \r\n " +

                    "  ";

                    _dbManDev.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbManDev.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbManDev.ResultsTableName = "Stoping";
                    _dbManDev.ExecuteInstruction();

                    ReportStoping.Tables.Add(_dbManDev.ResultsDataTable);

                    report.RegisterData(ReportStoping);

                    report.Load(_reportFolder + "BonusLetters.frx");

                    //report.Design();

                    pcReport.Clear();
                    report.Prepare();
                    report.Preview = pcReport;
                    report.ShowPrepared();
                }







            }

            if (RepLbl.Text == "Production Summary Tot Mine")
            {

                MWDataManager.clsDataAccess _dbManDev = new MWDataManager.clsDataAccess();
                _dbManDev.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbManDev.SqlStatement = " \r\n " +
                                            " select 'a' a, occupationID, occupation, sum(pos) incubants, SUM(nett_amount) nett_amount, \r\n " +
                                            " SUM(WorkingShifts) ws, prodmonth from (\r\n " +
                                            " select case when occupation = 'Shift Bosses (Stoping)' then '1' \r\n " +
                                            " when occupation = 'Shift Bosses (Development)' then '2' \r\n " +
                                            " when occupation = 'Stoper' then '3' \r\n " +
                                            " when occupation = 'Developer' then '4' \r\n " +
                                            " when occupation = 'N/S Cleaning (Stoping)' then '5' \r\n " +
                                            " when occupation = 'N/S Cleaning (Development)' then '6' \r\n " +
                                            "  end as occupationID,  * from ( \r\n " +
                                            " select a.*, WorkingShifts/(pos+0.00000) pos from ( \r\n " +
                                            " select SUBSTRING(orgunit,1,4) mo, case when category like '%Shiftboss' and Activitycode = '0' then 'Shift Bosses (Stoping)' \r\n " +
                                            " when category like '%Shiftboss' and Activitycode = '1' then 'Shift Bosses (Development)' \r\n " +
                                            " when Category = 'Stoper' then 'Stoper' \r\n " +
                                            " when Category = 'Developer' then 'Developer' \r\n " +
                                            " when Category = 'Nightshift Cleaner' and ActivityCode = '0' then 'N/S Cleaning (Stoping)'  \r\n " +
                                            " when Category = 'Nightshift Cleaner' and ActivityCode = '1' then 'N/S Cleaning (Development)' \r\n " +
                                            "  end as Occupation, \r\n " +
                                            "  * from tbl_BCS_ARMS_Interface_TransferNew \r\n " +
                                            " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "' and ActivityCode = 0) a \r\n " +
                                            " left outer join \r\n " +
                                            " (select mo mo1, max(pos) pos from (select SUBSTRING(orgunit,1,4) mo, PossibleShifts pos from tbl_BCS_StopingRepNew \r\n " +
                                            " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "') b group by mo) b  on a.mo = b.mo1) a) a \r\n " +
                                            " where occupationID is not null \r\n " +
                                            " group by occupationID, occupation, prodmonth \r\n " +
                                            " union \r\n " +
                                            " select 'a' a, occupationID, occupation, sum(pos) incubants, SUM(nett_amount) nett_amount, \r\n " +
                                            " SUM(WorkingShifts) ws, prodmonth from ( \r\n " +
                                            " select case when occupation = 'Shift Bosses (Stoping)' then '1' \r\n " +
                                            " when occupation = 'Shift Bosses (Development)' then '2' \r\n " +
                                            " when occupation = 'Stoper' then '3' \r\n " +
                                            " when occupation = 'Developer' then '4' \r\n " +
                                            " when occupation = 'N/S Cleaning (Stoping)' then '5' \r\n " +
                                            " when occupation = 'N/S Cleaning (Development)' then '6' \r\n " +
                                            " end as occupationID,  * from ( \r\n " +
                                            " select a.*, WorkingShifts/(pos+0.00000) pos from ( \r\n " +
                                            " select SUBSTRING(orgunit,1,4) mo, case when category like '%Shiftboss' and Activitycode = '0' then 'Shift Bosses (Stoping)' \r\n " +
                                            " when category like '%Shiftboss' and Activitycode = '1' then 'Shift Bosses (Development)' \r\n " +
                                            " when Category = 'Stoper' then 'Stoper'\r\n " +
                                            " when Category = 'Developer' then 'Developer' \r\n " +
                                            " when Category = 'Nightshift Cleaner' and ActivityCode = '0' then 'N/S Cleaning (Stoping)' \r\n " +
                                            " when Category = 'Nightshift Cleaner' and ActivityCode = '1' then 'N/S Cleaning (Development)' \r\n " +
                                            "  end as Occupation, \r\n " +
                                            "  * from tbl_BCS_ARMS_Interface_TransferNew \r\n " +
                                            " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "' and ActivityCode = 1) a \r\n " +
                                            " left outer join \r\n " +
                                            " (select mo mo1, max(pos) pos from (select SUBSTRING(orgunit,1,4) mo, PossibleShifts pos from tbl_BCS_DevRepNew \r\n " +
                                            " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "') b group by mo) b  on a.mo = b.mo1) a) a \r\n " +
                                            " where occupationID is not null \r\n " +
                                            " group by occupationID, occupation, prodmonth \r\n " +
                                            " order by occupationID ASC \r\n " +

                                                                "  ";

                _dbManDev.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                _dbManDev.queryReturnType = MWDataManager.ReturnType.DataTable;
                _dbManDev.ResultsTableName = "Stoping";
                _dbManDev.ExecuteInstruction();

                ReportStoping.Tables.Add(_dbManDev.ResultsDataTable);

                report.RegisterData(ReportStoping);

                report.Load(_reportFolder + "ProdSumTotMine.frx");

                //report.Design();

                pcReport.Clear();
                report.Prepare();
                report.Preview = pcReport;
                report.ShowPrepared();
            }


            if (RepLbl.Text == "Top 5 Prod")
            {
                if (editActivity.EditValue.ToString() == "0")
                {
                    MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                    _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan1.SqlStatement = " \r\n " +

                                        "   select * from ( \r\n " +
                                        " select * from ( \r\n " +
                                        " select top(5) Nett_Amount, ActivityCode, ProdMonth, Initials, surname, Element, Category, 'a' a, Section, 'Stoping' Act from ( \r\n " +
                                        " select * from ( \r\n " +
                                        " select SUBSTRING(OrgUnit, 3,2) Section, * from dbo.tbl_BCS_ARMS_Interface_TransferNew \r\n " +
                                        " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "' )a \r\n " +
                                        " where ActivityCode = '0' and Category = 'Stoper' \r\n " +
                                        "  )a \r\n " +
                                        "  order by Nett_Amount Desc )a \r\n " +
                                        " union \r\n " +
                                        " select * from ( \r\n " +
                                        " select top(5) Nett_Amount, ActivityCode , ProdMonth , \r\n " +
                                        " Initials , surname , Element , Category, 'a' a, Section, 'Development' Act  from ( \r\n " +
                                        " select * from ( \r\n " +
                                        " select SUBSTRING(OrgUnit, 3,2) Section, * from dbo.tbl_BCS_ARMS_Interface_TransferNew \r\n " +
                                        " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "' )a1 \r\n " +
                                        " where ActivityCode = '1' and Category = 'Developer' \r\n " +
                                        "  )a2  order by Nett_Amount Desc \r\n " +
                                        "  )a3 \r\n " +
                                        " )z \r\n " +
                                        " order by ActivityCode, Nett_Amount desc \r\n " +

                         "  ";

                    _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan1.ResultsTableName = "New";
                    _dbMan1.ExecuteInstruction();

                    ReportStoping.Tables.Add(_dbMan1.ResultsDataTable);



                    ///Shift Boss
                    MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
                    _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                    _dbMan.SqlStatement = " \r\n " +

                                        "  select * from ( \r\n " +
                                        " select * from ( \r\n " +
                                        " select top(5) Nett_Amount, ActivityCode, ProdMonth, Initials, surname, Element, Category, 'a' a, Section, 'Stoping' Act from ( \r\n " +
                                        " select * from ( \r\n " +
                                        " select SUBSTRING(OrgUnit, 3,2) Section, * from dbo.tbl_BCS_ARMS_Interface_TransferNew \r\n " +
                                        " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "' )a \r\n " +
                                        " where ActivityCode = '0' and Category like '%Shiftboss'  \r\n " +
                                        "  )a \r\n " +
                                        "  order by Nett_Amount Desc )a \r\n " +
                                        " union \r\n " +
                                        " select * from ( \r\n " +
                                        " select top(5) Nett_Amount, ActivityCode , ProdMonth , \r\n " +
                                        " Initials , surname , Element , Category, 'a' a, Section, 'Development' Act  from ( \r\n " +
                                        " select * from ( \r\n " +
                                        " select SUBSTRING(OrgUnit, 3,2) Section, * from dbo.tbl_BCS_ARMS_Interface_TransferNew \r\n " +
                                        " where Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)).ToString() + "' )a1 \r\n " +
                                        " where ActivityCode = '1' and Category like '%Shiftboss' \r\n " +
                                        "  )a2  order by Nett_Amount Desc \r\n " +
                                        "  )a3 \r\n " +
                                        " )z \r\n " +
                                        " order by ActivityCode, Nett_Amount desc \r\n " +


                         "  ";

                    _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
                    _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
                    _dbMan.ResultsTableName = "Stoping";
                    _dbMan.ExecuteInstruction();

                    ReportStoping.Tables.Add(_dbMan.ResultsDataTable);

                    report.RegisterData(ReportStoping);

                    report.Load(_reportFolder + "Top5Prod.frx");

                    //report.Design();

                    pcReport.Clear();
                    report.Prepare();
                    report.Preview = pcReport;
                    report.ShowPrepared();
                }
            }
        }

        private void editProdmonth_EditValueChanged(object sender, EventArgs e)
        {
            loadsection();
        }

        private void navBarControl1_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OnCloseTabRequest(new CloseTabArg(tabCaption));
        }
    }


}
