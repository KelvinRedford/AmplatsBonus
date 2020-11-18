using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;

namespace Mineware.Systems.ProductionAmplatsBonus
{
    public partial class ucImportData : BaseUserControl
    {
        public ucImportData()
        {
            InitializeComponent();
            FormRibbonPages.Add(rpGangMapping);
            FormActiveRibbonPage = rpGangMapping;
            FormMainRibbonPage = rpGangMapping;
            RibbonControl = rcImportData;
        }

        private void ucImportData_Load(object sender, EventArgs e)
        {
            getProdMonth();
            CreateTable();
        }

        void getProdMonth()
        {
            //Gets and Sets Production month listbox
            tbProdMonth.EditValue = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsDate(ProductionAmplatsGlobal.ProductionAmplatsGlobalTSysSettings._currentProductionMonth.ToString());
        }

        private void CreateTable()
        {

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " exec Mineware.[dbo].[sp_BCS_Import_CreateNewTable] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(tbProdMonth.EditValue)) + "' \r\n";
            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();

            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan1.SqlStatement = " select substring(orgunit,1,4) oo, convert(varchar(50),captdate,106) +' '+substring(convert(varchar(50),captdate,108),1,5) captdate, username from Mineware.dbo.tbl_BCS_Imports_" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(tbProdMonth.EditValue)) + " group by substring(orgunit,1,4) , captdate, username order by substring(orgunit,1,4) , captdate desc \r\n";
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
            _dbMan1a.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan1a.SqlStatement = "select substring(orgunit,1,4) ss from [Mineware].[dbo].[tbl_BCS_Gangs_3Month] " +
                                    " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(tbProdMonth.EditValue)) + "' group by substring(orgunit,1,4) order by substring(orgunit,1,4)";
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

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OnCloseTabRequest(new CloseTabArg(tabCaption));
        }

        private void tbProdMonth_EditValueChanged(object sender, EventArgs e)
        {
            CreateTable();
        }
    }
}
