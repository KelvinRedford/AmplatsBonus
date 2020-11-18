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
using DevExpress.Utils.DragDrop;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Mineware.Systems.GlobalConnect;
using Mineware.Systems.Global;

namespace Mineware.Systems.ProductionAmplatsBonus
{
    public partial class ucGangMapping : BaseUserControl
    {
        public ucGangMapping()
        {
            InitializeComponent();
            FormRibbonPages.Add(rpGangMapping);
            FormActiveRibbonPage = rpGangMapping;
            FormMainRibbonPage = rpGangMapping;
            RibbonControl = rcGangMapping;

        }
        Procedures procs = new Procedures();

        private string _mnrDate;
        private string _mnrMineOverseer;
        private string _delDate;
        private string _delMineOverseer;
        private string _sysUser;

        private DataTable _dtMOList;
        private DataTable _dtmoExclusion;

        private void _getMOExclusions()
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = "SELECT * FROM Mineware.dbo.tbl_BCS_MinerDate_ExclusionList ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();
            _dtmoExclusion = _dbMan.ResultsDataTable;
            gcMineExc.DataSource = _dtmoExclusion;
        }

        private void _getMOList()
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " SELECT DISTINCT SectionID FROM mineware.dbo.tbl_Section WHERE Hierarchicalid = '4' AND ProdMonth IN (SELECT DISTINCT ProdMonth FROM mineware.dbo.tbl_BCS_Planning WHERE CalendarDate > GETDATE()- 120) ORDER BY SectionID";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();
            _dtMOList = _dbMan.ResultsDataTable;
            lbMineOverseers.DataSource = _dtMOList;
            lbMineOverseers.ValueMember = "SectionID";
            lbMineOverseers.DisplayMember = "SectionID";
        }

        private void _deleteMOExclusion()
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " UPDATE Mineware.dbo.tbl_BCS_MinerDate_ExclusionList SET DeletedFlag = 'Y' WHERE MineOverseer = '" + _delMineOverseer + "' AND Calenderdate = '" + _delDate + "' ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();
            _getMOExclusions();

            alertControl1.Show(null, "Information", "Mine Overseer and CalenderDate Removed");
        }

        private void _insertMOExclusion()
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " INSERT INTO MIneware.dbo.tbl_BCS_MinerDate_ExclusionList " +
              "  VALUES " +
              "  ('" + _mnrMineOverseer + "', " +
              "  '" + _mnrDate + "', " +
              "  '" + _sysUser + "', " +
              "  'N', " +
              "  '" + DateTime.Now + "')";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();
        }



        void getProdMonth()
        {
            //Gets and Sets Production month listbox
            editPM.EditValue = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsDate(ProductionAmplatsGlobal.ProductionAmplatsGlobalTSysSettings._currentProductionMonth.ToString());
        }

        public object gridval = null;
        private string excludeval;
        private string excludedel;

        void lbCrew()
        {
            //Gets and sets crews list box
            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan1.SqlStatement = "SELECT DISTINCT OrgUnit AS Gangs FROM tbl_BCS_Gangs WHERE ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editPM.EditValue)) + "' ORDER BY OrgUnit ASC";
            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();
            DataTable dtMain = _dbMan1.ResultsDataTable;
            DataSet ds = new DataSet();
            ds.Tables.Add(dtMain);
            lblContextHelp.Visible = true;
            gridControl2.DataSource = dtMain;
            tbZGangID.Text = "";
        }

       void loadgrid()
        {
           //Gets and sets gang mapping grid
            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);

            _dbMan1.SqlStatement = "SELECT ProdMonth \r\n" +
                ",GangID \r\n" +
                ",ZGangID \r\n" +
                "FROM \r\n" +
                "Mineware.dbo.tbl_BCS_ZGangsLink \r\n" +
                "WHERE ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editPM.EditValue)) + "' \r\n" +
                "ORDER BY ZGangID ASC ";
            _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan1.ExecuteInstruction();


            DataTable dtMain = _dbMan1.ResultsDataTable;
            DataSet ds = new DataSet();
            ds.Tables.Add(dtMain);
            gridControl1.DataSource = ds.Tables[0];
            ZGang3.FieldName = "GangID";
            ZGang2.FieldName = "ZGangID";
        }

       void InsertZGang()
       {
           //Inserts tbZGang.text into Gangs Database
           MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
           _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
           _dbMan1.SqlStatement = "INSERT INTO Mineware.dbo.tbl_BCS_ZGangsLink(ProdMonth, ZGangID, GangID)VALUES('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editPM.EditValue)) + "', '" + tbZGangID.Text + "', '')";
           _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
           _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
           _dbMan1.ExecuteInstruction();
           lblZGangValue.Text = lblZGangValue.Text;
       }

       void UpdateGang()
       {

           try
           {
               //Updates row containing selected ZGang with dragdrop value
               MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
               _dbMan1.SqlStatement = "INSERT INTO Mineware.dbo.tbl_BCS_ZGangsLink(ProdMonth, GangID, ZGangID)VALUES('" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editPM.EditValue)) + "','" + lblGangValue.Text + "','" + lblZGangValue.Text + "') " +
                "DELETE FROM Mineware.dbo.tbl_BCS_ZGangsLink WHERE ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editPM.EditValue)) + "'" +
                "and gangid = '' and zgangid in (SELECT zgangid FROM " +
                "(SELECT zgangid, count(zgangid) nn FROM Mineware.dbo.tbl_BCS_ZGangsLink WHERE ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editPM.EditValue)) + "'" +
                "and gangid = '' group by zgangid) a where nn > 0)";
               _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
               _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
               _dbMan1.ExecuteInstruction();
               alertControl1.Show(null, "Information", "Record Added Successfuly");
           }
           catch (Exception ex)
           {
               MessageBox.Show("Upload Failed!", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
           }
       }

       void DeleteGang()
       {
           MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
           _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
           _dbMan1.SqlStatement = "DELETE FROM Mineware.dbo.tbl_BCS_ZGangsLink WHERE GangID = '" + lblGangValue.Text + "'AND ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editPM.EditValue)) + "' AND ZGangID = '"+ lblZGangValue.Text +"'";
           _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
           _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
           _dbMan1.ExecuteInstruction();
       }

       void loadexlusionemp()
       {
           MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
           _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
           _dbMan1.SqlStatement = "SELECT * FROM [Mineware].[dbo].[tbl_BCS_Eng_Exclusions] ORDER BY IndustryNumber ASC";
           _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
           _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
           _dbMan1.ExecuteInstruction();
           lbExclude.DataSource = _dbMan1.ResultsDataTable;
           lbExclude.ValueMember = "IndustryNumber";
            lbExclude.DisplayMember = "IndustryNumber";
       }

       void loadexclusionempadd()
       {
           MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
           _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
           _dbMan1.SqlStatement = "  SELECT IndustryNumber FROM [Mineware].[dbo].[tbl_BCS_Import_Personnel_Latest] " +
                    " WHERE IndustryNumber NOT IN (SELECT IndustryNumber FROM [Mineware].[dbo].[tbl_BCS_Eng_Exclusions]) ORDER BY IndustryNumber ASC";
           _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
           _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
           _dbMan1.ExecuteInstruction();
           lbEmployees.DataSource = _dbMan1.ResultsDataTable;
           lbEmployees.ValueMember = "IndustryNumber";
           lbEmployees.DisplayMember = "IndustryNumber";
       }

       void addexclude()
       {
           if (!string.IsNullOrWhiteSpace(excludeval))
           {

               MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
               _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
               _dbMan1.SqlStatement = "INSERT INTO [Mineware].[dbo].[tbl_BCS_Eng_Exclusions]VALUES('" + excludeval + "')";
               _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
               _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
               _dbMan1.ExecuteInstruction();

               loadexlusionemp();
               loadexclusionempadd();

               alertControl1.Show(frmMain.ActiveForm, "Notification", "User added to exclusion list");

           }

       }

       void delexclude()
       {
           if (!string.IsNullOrWhiteSpace(excludedel))
           {
               MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
               _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
               _dbMan1.SqlStatement = "DELETE FROM [Mineware].[dbo].[tbl_BCS_Eng_Exclusions] WHERE IndustryNumber = '" + excludedel + "'";
               _dbMan1.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
               _dbMan1.queryReturnType = MWDataManager.ReturnType.DataTable;
               _dbMan1.ExecuteInstruction();

               loadexlusionemp();
               loadexclusionempadd();

               alertControl1.Show(frmMain.ActiveForm, "Notification", "User removed from exclusion list");

           }
       }


        private void ucTestGangMapping_Load(object sender, EventArgs e)
        {

                getProdMonth();
                lbCrew();
                loadexclusionempadd();
                loadexlusionemp();
                _getMOExclusions();
                _getMOList();


                _sysUser = Environment.UserName;

        }

        private void MillMonth1_TextChanged(object sender, EventArgs e)
        {
            loadgrid();
        }

        private void MillMonth_Click(object sender, EventArgs e)
        {
            
          
            
        }

        
        private void lbGangNumbers_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;  
        }

        private void lbGangNumbers_MouseDown(object sender, MouseEventArgs e)
        {

        }

        public object lb_Item = null;

        private void lbGangNumbers_DragLeave(object sender, EventArgs e)
        {
            ListBox lb = sender as ListBox;
        }

        private void lbGangNumbers_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void lbGangNumbers_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.StringFormat))
                {
                    string str = (string)e.Data.GetData(DataFormats.StringFormat);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void listBox1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void gridView1_DragObjectOver(object sender, DevExpress.XtraGrid.Views.Base.DragObjectOverEventArgs e)
        {
            
        }

        private void sbtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                InsertZGang();
                loadgrid();
                lblZGangValue.Text = lblZGangValue.Text;
                tbZGangID.Text = "";
                alertControl1.Show(null, "Information", "Record Added Successfuly");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Insert Failed", "Already exists");
            }
        }

        private void tbGangID_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(tbZGangID.Text))
            {
                sbtnAdd.Enabled = true;
                tbZGangID.CharacterCasing = CharacterCasing.Upper;
            }
            else
            {
                sbtnAdd.Enabled = false;
            }
        }

        private void gridControl1_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                Point p = this.gridControl1.PointToClient(new Point(e.X, e.Y));
                int row = gridView1.CalcHitInfo(p.X, p.Y).RowHandle;
                var cellValue = this.gridView1.GetRowCellValue(row, gridView1.Columns[1]);
                var cellValueZ = this.gridView1.GetRowCellValue(row, gridView1.Columns[1]);
                lblZGangValue.Text = cellValue.ToString();
                lblGangValue.Text = gridView2.GetFocusedRowCellValue(gridView2.Columns[0]).ToString();
                var gridItem = gridView2.GetFocusedRowCellValue(gridView2.Columns[0]).ToString();
                var gdItem = val2;
                if (row > -1)
                {
                    if (gridView1.CalcHitInfo(p.X, p.Y).Column.AbsoluteIndex == 1)
                    {
                        return;
                    }
                    //Determines where you drop the value from listbox into grid
                    else if (gridView1.CalcHitInfo(p.X, p.Y).Column.FieldName != null)
                    {

                        if (this.gridView1.GetRowCellValue(row, gridView1.Columns[1]).ToString() != lblGangValue.Text)
                        {
                            this.gridView1.SetRowCellValue(row, gridView1.CalcHitInfo(p.X, p.Y).Column.FieldName, gridval);
                            gridval = null;
                        }
                        else
                        {

                            this.gridView1.SetRowCellValue(row, gridView1.CalcHitInfo(p.X, p.Y).Column.FieldName, gridItem);
                            gridval = null;
                        }

                        UpdateGang();
                        loadgrid();
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("ZGang not selected","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void gridControl1_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DataRow))) e.Effect = DragDropEffects.Copy;
            else e.Effect = DragDropEffects.None;
        }

        private void gridControl1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void gridControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                var zGangValue = gridView1.GetFocusedRowCellValue("ZGangID").ToString();
                lblZGangValue.Text = zGangValue;
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Please select a ZGangID in grid","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            try
            {
                var gangvalue = gridView1.GetFocusedRowCellValue("GangID").ToString();
                lblGangValue.Text = gangvalue;
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Please select a GangID in grid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string caption = "Delete Gang";
            string message = "You are about to delete the following gangID: " + lblGangValue.Text + ", Continue?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Information;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons, icon);

            if (result == DialogResult.Yes)
            {

                DeleteGang();
                loadgrid();
                alertControl1.Show(frmMain.ActiveForm, "Information", "Record Removed Successfuly");
            }
        }

        private void gridControl1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                var zGangValue = gridView1.GetFocusedRowCellValue("ZGangID").ToString();
                lblZGangValue.Text = zGangValue;
            }
            catch (NullReferenceException ex)
            {
            }
            try
            {
                var gangvalue = gridView1.GetFocusedRowCellValue("GangID").ToString();
                lblGangValue.Text = gangvalue;
            }
            catch (NullReferenceException ex)
            {
            }
        }

        private void tbZGangID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                sbtnAdd.PerformClick();
            }
        }

        public string gc_Item = null;

        private void gcGangs_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        public string val2 = null;

        GridHitInfo downHitInfo = null;

        private void gridControl2_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            downHitInfo = null;
            GridHitInfo hitinfo = gridView2.CalcHitInfo(new Point(e.X, e.Y));
            if (Control.ModifierKeys != Keys.None) return;
            if (e.Button == MouseButtons.Left && hitinfo.RowHandle >= 0)
            {
                downHitInfo = hitinfo;
            }
        }

        private void gridControl2_MouseLeave(object sender, EventArgs e)
        {

        }

        private void gridControl2_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;  
        }

        private void gridControl2_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void gridControl2_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Button == MouseButtons.Left && downHitInfo != null)
            {
                Size dragsize = SystemInformation.DragSize;
                Rectangle dragRect = new Rectangle(new Point(downHitInfo.HitPoint.X - dragsize.Width / 2,
                    downHitInfo.HitPoint.Y - dragsize.Height / 2),
                    dragsize);

                if (!dragRect.Contains(new Point(e.X,e.Y)))
                {
                    DataRow row = gridView2.GetDataRow(downHitInfo.RowHandle);
                    gridView2.GridControl.DoDragDrop(row, DragDropEffects.Copy);
                    DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                }

            }

            
        }

        private void gridControl2_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void gridControl2_DragLeave(object sender, EventArgs e)
        {
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {

        }

        private void gridView2_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void gridView2_RowCellClick(object sender, RowCellClickEventArgs e)
        {

        }

        private void lbEmployees_Click(object sender, EventArgs e)
        {
            excludeval = lbEmployees.SelectedValue.ToString();
        }

        private void btnexcludeadd_Click(object sender, EventArgs e)
        {
            var message = "Are you sure you want to add employee to exclusion list?";
            var caption = "Transfer Notification";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Information;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons, icon);

            if (result == DialogResult.Yes)
            {
                addexclude();
            }

        }

        private void btnexcluderemove_Click(object sender, EventArgs e)
        {
            var message = "Are you sure you want to remove employee from exclusion list?";
            var caption = "Transfer Notification";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon icon = MessageBoxIcon.Information;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons, icon);

            if (result == DialogResult.Yes)
            {
                delexclude();
            }

        }

        private void lbExclude_Click(object sender, EventArgs e)
        {
            excludedel = lbExclude.SelectedValue.ToString();
        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void lbExclude_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private String MO;
        private String Date;
        private String Deleted;
        private String checkDel="N ";
        private int AllowInsert = 0;

        private void loopDuplicates()
        {
            for (int i = 0; i < _dtmoExclusion.Rows.Count; i++)
            {
                MO = gvMineExc.GetRowCellValue(i, gvMineExc.Columns["MineOverseer"]).ToString();
                Date = gvMineExc.GetRowCellValue(i, gvMineExc.Columns["CalenderDate"]).ToString();
                Date = Convert.ToDateTime(Date).ToShortDateString();
                Deleted = gvMineExc.GetRowCellValue(i, gvMineExc.Columns["DeletedFlag"]).ToString();

                if (MO == _mnrMineOverseer)
                {
                    if (Date == _mnrDate)
                    {
                        if (Deleted == checkDel)
                        {
                            AllowInsert = 1;
                        }
                    }
                }
            }
        }

        private void lbMineOverseers_DoubleClick(object sender, EventArgs e)
        {
            _mnrMineOverseer = lbMineOverseers.SelectedValue.ToString();
            _mnrDate = dtpCalenderDate.Text;
            _mnrDate = Convert.ToDateTime(_mnrDate).ToShortDateString();
            loopDuplicates();

            if (AllowInsert == 0)
            {

            var Notification = "Do you want to add " + _mnrMineOverseer + " for date " + _mnrDate + "";
            var Caption = "Add Notification";
            MessageBoxButtons Buttons = MessageBoxButtons.YesNo;
            MessageBoxIcon Icon = MessageBoxIcon.Question;
            DialogResult Result;
            Result = MessageBox.Show(Notification, Caption, Buttons, Icon);

            if (Result == DialogResult.Yes)
            {

                    _insertMOExclusion();
                    _getMOExclusions();
                    alertControl1.Show(null, "Information", "Mine Overseer and Calender Date added to exclusion");

            }
            }
            else
            {
                MessageBox.Show("Already added", "Add Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AllowInsert = 0;
            }
        }

        private void gvMineExc_DoubleClick(object sender, EventArgs e)
        {

            var Deletable = gvMineExc.GetFocusedRowCellValue(gvMineExc.Columns["DeletedFlag"]).ToString();
            if (Deletable == "N ")
            {

                    _delMineOverseer = gvMineExc.GetFocusedRowCellValue(gvMineExc.Columns["MineOverseer"]).ToString();
                    _delDate = gvMineExc.GetFocusedRowCellValue(gvMineExc.Columns["CalenderDate"]).ToString();
                    var Notification = "Do you want to remove " + _delMineOverseer + " for date " + _delDate + "";
                    var Caption = "Add Notification";
                    MessageBoxButtons Buttons = MessageBoxButtons.YesNo;
                    MessageBoxIcon Icon = MessageBoxIcon.Question;
                    DialogResult Result;
                    Result = MessageBox.Show(Notification, Caption, Buttons, Icon);

                    if (Result == DialogResult.Yes)
                    {

                        _deleteMOExclusion();

                    }
            }
            else
                {
                    MessageBox.Show("Not allowed, cannot remove Mine Overseer already removed from list", "Error Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void editPM_EditValueChanged(object sender, EventArgs e)
        {
            lbCrew();
            loadgrid();
        }
    }
}
