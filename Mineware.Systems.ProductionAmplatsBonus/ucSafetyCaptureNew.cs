using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.ProductionAmplatsBonus
{
    public partial class ucSafetyCaptureNew : BaseUserControl
    {
        public ucSafetyCaptureNew()
        {
            InitializeComponent();
            FormRibbonPages.Add(rpSafetyCapture);
            FormActiveRibbonPage = rpSafetyCapture;
            FormMainRibbonPage = rpSafetyCapture;
            RibbonControl = rcSafetyCapture;
        }

        Procedures procs = new Procedures();

        private int check = 0;

        private string Org;
        private string RI;
        private string LTI;
        private string Fatal;

        private void deleteIncident()
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " DELETE FROM [Mineware].[dbo].[tbl_BCS_SafetyCapture] " +
              "  WHERE OrgUnit = '" + Org + "' " + 
              "  AND Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();
            acAlert.Show(frmMain.ActiveForm, "Notification", "Delete was successful");

            loadGrid();
        }

        private void insertIncident()
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " IF NOT EXISTS (SELECT OrgUnit FROM [Mineware].[dbo].[tbl_BCS_SafetyCapture] WHERE OrgUnit = '" + Org + "' AND Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "') " +
            " BEGIN INSERT INTO [Mineware].[dbo].[tbl_BCS_SafetyCapture] " +
                " VALUES('" + Org + "','" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "','" + RI + "','" + LTI + "','" + Fatal + "') END " +
            "ELSE " +
            "UPDATE  [Mineware].[dbo].[tbl_BCS_SafetyCapture]" +
            "SET RI = '" + RI + "', LTI = '" + LTI + "', Fatal = '" + Fatal + "' WHERE OrgUnit = '" + Org + "' AND Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();
            acAlert.Show(frmMain.ActiveForm, "Notification", "Insert was successful");

            loadGrid();
        }

        private void updateIncident()
        {


                MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = "UPDATE [Mineware].[dbo].[tbl_BCS_SafetyCapture] " +
              "  SET RI = '" + RI + "' " +
              "  ,LTI = '" + LTI + "' " +
              "  ,Fatal = '" + Fatal + "' " +
              "  WHERE OrgUnit = '" + Org + "' AND Prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();
            loadGrid();
            acAlert.Show(frmMain.ActiveForm, "Notification", "Update was successful");

            check = 0;
        }

        private void loadProdMonth()
        {
            editProdmonth.EditValue = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsDate(ProductionAmplatsGlobal.ProductionAmplatsGlobalTSysSettings._currentProductionMonth.ToString());
        }

        private void loadSafetyCapt()
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = "SELECT distinct(Orgunit) unit FROM [dbo].[tbl_Import_BCS_Personnel] ORDER BY Orgunit ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            DataTable dtOrg = _dbMan.ResultsDataTable;
            luOrgUnits.Properties.DataSource = dtOrg;
            luOrgUnits.Properties.DisplayMember = "unit";
            luOrgUnits.Properties.ValueMember = "unit";
        }

        private void loadGrid()
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = "Select OrgUnit as Orgunit, RI, LTI, Fatal as Fatals from tbl_BCS_SafetyCapture " +
                                  "Where ProdMonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' order by orgunit ";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();
            DataTable dtGrid = _dbMan.ResultsDataTable;
            gcProd.DataSource = dtGrid;
        }

        private void ucSafetyCaptureNew_Load(object sender, EventArgs e)
        {
            loadProdMonth();
            loadSafetyCapt();
            loadGrid();

            pnlMainDockLeft.Width = 0;
            btnDelete.Enabled = false;
        }

       

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            pnlMainDockLeft.Width = 225;

            tbRI.EditValue = Convert.ToInt32(0);
            tbLTI.EditValue = Convert.ToInt32(0);
            tbFatals.EditValue = Convert.ToInt32(0);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            pnlMainDockLeft.Width = 0;
            btnAdd.Enabled = true;
            luOrgUnits.Properties.NullText = "Select Orgunit...";
            luOrgUnits.ReadOnly = false;

            if (check == 0)
            {
                insertIncident();
            }
            else
            {
                updateIncident();
            }
        }

        private void gvProd_DoubleClick(object sender, EventArgs e)
        {
             Org = gvProd.GetFocusedRowCellValue(gvProd.Columns["Orgunit"]).ToString();
             RI = gvProd.GetFocusedRowCellValue(gvProd.Columns["RI"]).ToString();
             LTI = gvProd.GetFocusedRowCellValue(gvProd.Columns["LTI"]).ToString();
             Fatal = gvProd.GetFocusedRowCellValue(gvProd.Columns["Fatals"]).ToString();
            pnlMainDockLeft.Width = 225;
            luOrgUnits.Properties.NullText = Org;
            luOrgUnits.EditValue = Org;
            luOrgUnits.ReadOnly = true;
            tbRI.EditValue = Convert.ToInt32(RI);
            tbLTI.EditValue = Convert.ToInt32(LTI);
            tbFatals.EditValue = Convert.ToInt32(Fatal);
            btnAdd.Enabled = false;
            check = 1;
            btnDelete.Enabled = true;
        }

        private void tbRI_Validating(object sender, CancelEventArgs e)
        {

        }

        private void tbRI_Validated(object sender, EventArgs e)
        {

        }

        private void luOrgUnits_EditValueChanged(object sender, EventArgs e)
        {
            if (luOrgUnits.EditValue == null)
            {

            }
            else
            {
                Org = luOrgUnits.EditValue.ToString();
            }
        }

        private void tbRI_EditValueChanged(object sender, EventArgs e)
        {
            RI = tbRI.EditValue.ToString();
        }

        private void tbLTI_EditValueChanged(object sender, EventArgs e)
        {
            LTI = tbLTI.EditValue.ToString();
        }

        private void tbFatals_EditValueChanged(object sender, EventArgs e)
        {
            Fatal = tbFatals.EditValue.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteIncident();
            pnlMainDockLeft.Width = 0;
            tbFatals.EditValue = "0";
            tbLTI.EditValue = "0";
            tbRI.EditValue = "0";
            luOrgUnits.EditValue = null;
            luOrgUnits.Properties.NullText = "Select Orgunit...";
            luOrgUnits.ReadOnly = false;
            btnAdd.Enabled = true;
            btnDelete.Enabled = false;
            check = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            pnlMainDockLeft.Width = 0;
            tbFatals.EditValue = "0";
            tbLTI.EditValue = "0";
            tbRI.EditValue = "0";
            luOrgUnits.EditValue = null;
            luOrgUnits.Properties.NullText = "Select Orgunit...";
            luOrgUnits.ReadOnly = false;
            btnAdd.Enabled = true;
            btnDelete.Enabled = false;
            check = 0;
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OnCloseTabRequest(new CloseTabArg(tabCaption));
        }

        private void editProdmonth_EditValueChanged(object sender, EventArgs e)
        {
            loadGrid();
        }
    }
}
