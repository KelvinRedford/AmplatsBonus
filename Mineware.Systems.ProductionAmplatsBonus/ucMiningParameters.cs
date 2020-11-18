using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Mineware.Systems.Global;
using Mineware.Systems.GlobalConnect;
using Mineware.Systems.ProductionAmplatsGlobal;

namespace Mineware.Systems.ProductionAmplatsBonus
{
    public partial class ucMiningParameters : BaseUserControl
    {
        public ucMiningParameters()
        {
            InitializeComponent();
            FormRibbonPages.Add(rpMiningParameters);
            FormActiveRibbonPage = rpMiningParameters;
            FormMainRibbonPage = rpMiningParameters;
            RibbonControl = rcMiningParameters;
        }

        private void editProdmonth_EditValueChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0)
                LoadMiningResultsStope();
            else
                LoadMiningResultsDev();
        }

        void LoadMiningResultsStope()
        {
            gridControl6.Visible = false;


            MWDataManager.clsDataAccess _dbMan3Mnth = new MWDataManager.clsDataAccess();
            _dbMan3Mnth.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan3Mnth.SqlStatement = " exec mineware.[dbo].[sp_BCS_GetStopingInfo] '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'";

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
            _dbMan3Mnth.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan3Mnth.SqlStatement = " select *, cast(round(MeasOther/PlanOther1*100,0) as int) OtherPerc  from ( select " +

                                        " case when PlanOther = 0 then null else PlanOther end as PlanOther1 , \r\n" +
                                         " *, cast(round(MeasLateral/PlanLateral1*100,0) as int) LateralPerc,cast(round( MeasRaises/PlanRaises1*100,0) as int) RaisesPerc, cast(round(MeasTotal/PlanTotal1*100,0) as int) TotalPerc  from (  select   \r\n" +
                                         " case when PlanLateral = 0 then null else PlanLateral end as PlanLateral1, \r\n" +
                                         " case when PlanRaises = 0 then null else PlanRaises end as PlanRaises1, \r\n" +
                                         " case when PlanTotal = 0 then null else PlanTotal end as PlanTotal1, \r\n" +
                                         " *, sb + ':' +sbname sb1, MeasTotal - MeasLateral - MeasRaises MeasOther,  \r\n" +
                                         " PlanTotal - PlanLateral - PlanRaises PlanOther \r\n" +
                                         " from  mineware.dbo.vw_SBoss_DevResults ) a) b  \r\n" +
                                         " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'";

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

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0)
                LoadMiningResultsStope();
            else
                LoadMiningResultsDev();
        }

        private void ucMiningParameters_Load(object sender, EventArgs e)
        {
            editProdmonth.EditValue = ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsDate(ProductionAmplatsGlobalTSysSettings._currentProductionMonth.ToString());
            pnlSBResults.Visible = true;
            pnlSBResults.Dock = DockStyle.Fill;
            radioGroup1.SelectedIndex = 1;

            MiningFactorsPnl.Visible = false;
            BasicIncTablePnl.Visible = false;
            TramPnl.Visible = false;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _dbManNS = new MWDataManager.clsDataAccess();
            _dbManNS.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbManNS.SqlStatement = _dbManNS.SqlStatement + "  ";

            if (radioGroup1.SelectedIndex == 1)
            {


                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " delete from mineware.dbo.tbl_BCS_SBoss_DevResults  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";

                for (int k = 0; k <= bandedGridView11.RowCount - 1; k++)
                {
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " ";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " insert into mineware.dbo.tbl_BCS_SBoss_DevResults Values(  ";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "',  ";
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

                _dbManNS.SqlStatement = _dbManNS.SqlStatement + " delete from mineware.dbo.tbl_BCS_SBBonusCriteria  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' ";

                for (int k = 0; k <= bandedGridView7.RowCount - 1; k++)
                {
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " ";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " insert into mineware.dbo.tbl_BCS_SBBonusCriteria Values(  ";
                    _dbManNS.SqlStatement = _dbManNS.SqlStatement + " '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "',  ";
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
        }

        private void BIPRG_EditValueChanged(object sender, EventArgs e)
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

        void LoadDataStoping()
        {
            ////Load Headers Grid/////

            string @table = "tbl_BCS_BIPStoping";
            string @Collumn = "BIPStopingAvgEmp";

            if (MerCbx.Checked == true)
            {
                @table = "tbl_BCS_BIPStopingMer";

                //MerCbx.Checked = true;
                //UnitSweepCbx.Checked = false;
                //RCrewsCbx.Checked = false;
                //SweepCbx.Checked = false;
            }

            if (UnitSweepCbx.Checked == true)
            {
                @table = "tbl_BCS_BIPStopingSW";

                //UnitSweepCbx.Checked = true;
                //RCrewsCbx.Checked = false;
                //SweepCbx.Checked = false;
                //MerCbx.Checked = false;

            }

            if (RCrewsCbx.Checked == true)
            {
                @table = "tbl_BCS_BIPStopingRCrews";

                //RCrewsCbx.Checked = true;
                //UnitSweepCbx.Checked = false;
                //SweepCbx.Checked = false;
                //MerCbx.Checked = false;
            }

            if (SweepCbx.Checked == true)
            {
                @table = "tbl_BCS_BIPSweepings";
                @Collumn = "BIPSweepingsAvgEmp";

                //SweepCbx.Checked = true;
                //UnitSweepCbx.Checked = false;
                //RCrewsCbx.Checked = false;
                //MerCbx.Checked = false;

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = " select " + @Collumn + " from " + @table + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  group by " + @Collumn + "  order by " + @Collumn + "  ";
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
                _dbManUsers.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
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



                                            " set @pm = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n" +


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
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = " select " + @Collumn + " from " + @table + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  group by " + @Collumn + "  order by " + @Collumn + "  ";
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
                _dbManUsers.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
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



                                            " set @pm = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n" +


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

        private void navBarItem5_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            pnlSBResults.Visible = true;
            pnlSBResults.Dock = DockStyle.Fill;
            radioGroup1.SelectedIndex = 1;

            MiningFactorsPnl.Visible = false;
            BasicIncTablePnl.Visible = false;
            TramPnl.Visible = false;

            ribbonPageGroupBI.Visible = false;
        }

        private void navBarItem16_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

            //HidePnls();
            // PnlMingParam.Visible = true;
            ribbonPageGroupBI.Visible = true;
            BasicIncTablePnl.Visible = true;
            BasicIncTablePnl.Dock = DockStyle.Fill;
            //gridControl1.Dock = DockStyle.Fill;
            PeramPnl.Visible = false;

            TramPnl.Visible = false;
            MiningFactorsPnl.Visible = false;
            pnlSBResults.Visible = false;



            if (editAct.EditValue.ToString() == "0")
            {
                LoadDataStoping();

            }
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
            ribbonPageGroupBI.Visible = false;


            LoadMiningFact();
        }

        void LoadMiningFact()
        {

            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " select * from [mineware].[dbo].[tbl_BCS_SBBonusFactorNew]  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'";

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

        private void navBarItem21_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            TramPnl.Visible = true;
            TramPnl.Dock = DockStyle.Fill;

            MiningFactorsPnl.Visible = false;
            BasicIncTablePnl.Visible = false;
            pnlSBResults.Visible = false;
            ribbonPageGroupBI.Visible = false;
        }

        private void btnShow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0)
                LoadMiningResultsStope();
            else
                LoadMiningResultsDev();
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

        void LoadDataDev()
        {
            string @table = "";
            string @Collumn = "";
            string @Collumn2 = "";
            string @Collumn3 = "";

            if (RaiseCbx.Checked == true)
            {
                @table = "tbl_BCS_BIPDevRaises";
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
                @table = "tbl_BCS_BIPDevChairlift";
                @Collumn = "ID";
                @Collumn2 = "Percent";
                @Collumn3 = "Amount";

                //ChairliftCbx.Checked = true;
                //LatDevCbx.Checked = false;
                //RaiseCbx.Checked = false;
                //WaterEndsCbx.Checked = false;
                //BHCbx.Checked = false;

                MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = " select * from " + @table + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  order by " + @Collumn + "  ";
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
                _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
                _dbMan1.SqlStatement = " select " + @Collumn + " from " + @table + " where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'  group by " + @Collumn + "  order by " + @Collumn + "  ";
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
                _dbManUsers.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
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



                                            " set @pm = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "' \r\n" +


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

        private void RaiseCbx_Click(object sender, EventArgs e)
        {
            LatDevCbx.Checked = false;
            //RaiseCbx.Checked = false;
            WaterEndsCbx.Checked = false;
            BHCbx.Checked = false;
            ChairliftCbx.Checked = false;
        }

        private void LatDevCbx_Click(object sender, EventArgs e)
        {
            //LatDevCbx.Checked = false;
            RaiseCbx.Checked = false;
            WaterEndsCbx.Checked = false;
            BHCbx.Checked = false;
            ChairliftCbx.Checked = false;
        }

        private void SaveFactorsBtn_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MWDataManager.clsDataAccess _dbMan = new MWDataManager.clsDataAccess();
            _dbMan.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan.SqlStatement = " delete from [Mineware].[dbo].[tbl_BCS_SBBonusFactorNew]  where prodmonth = '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "'";

            _dbMan.queryExecutionType = MWDataManager.ExecutionType.GeneralSQLStatement;
            _dbMan.queryReturnType = MWDataManager.ReturnType.DataTable;
            _dbMan.ExecuteInstruction();


            MWDataManager.clsDataAccess _dbMan1 = new MWDataManager.clsDataAccess();
            _dbMan1.ConnectionString = TConnections.GetConnectionString(theSystemDBTag, UserCurrentInfo.Connection);
            _dbMan1.SqlStatement = " insert into [Mineware].[dbo].[tbl_BCS_SBBonusFactorNew] Values( '" + ProductionAmplatsGlobal.ProductionAmplatsGlobal.ProdMonthAsString(Convert.ToDateTime(editProdmonth.EditValue)) + "', '" + StopeLimitOfCall.Text.ToString() + "', '" + StopeSafetyInspec1.Text.ToString() + "', '" + StopeSafetyInspec2.Text.ToString() + "', '" + StopeSafetyInspec3.Text.ToString() + "', '" + StopeSafetyInspec4.Text.ToString() + "', \r\n" +
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

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OnCloseTabRequest(new CloseTabArg(tabCaption));
        }

        private void RaiseCbx_EditValueChanged(object sender, EventArgs e)
        {
            LoadDataDev();
        }

        private void editType_EditValueChanged(object sender, EventArgs e)
        {
            if (editType.EditValue.ToString() == "0")
            {
                RaiseCbx.Checked = true;
                LatDevCbx.Checked = false;
                WaterEndsCbx.Checked = false;
                BHCbx.Checked = false;
                ChairliftCbx.Checked = false;
                
            }
            if (editType.EditValue.ToString() == "1")
            {
                RaiseCbx.Checked = true;
                LatDevCbx.Checked = false;
                WaterEndsCbx.Checked = false;
                BHCbx.Checked = false;
                ChairliftCbx.Checked = false;

            }
            if (editType.EditValue.ToString() == "2")
            {
                RaiseCbx.Checked = true;
                LatDevCbx.Checked = false;
                WaterEndsCbx.Checked = false;
                BHCbx.Checked = false;
                ChairliftCbx.Checked = false;

            }
            if (editType.EditValue.ToString() == "3")
            {
                RaiseCbx.Checked = true;
                LatDevCbx.Checked = false;
                WaterEndsCbx.Checked = false;
                BHCbx.Checked = false;
                ChairliftCbx.Checked = false;

            }
            if (editType.EditValue.ToString() == "4")
            {
                RaiseCbx.Checked = true;
                LatDevCbx.Checked = false;
                WaterEndsCbx.Checked = false;
                BHCbx.Checked = false;
                ChairliftCbx.Checked = false;

            }
            LoadDataDev();
        }

        private void editAct_EditValueChanged(object sender, EventArgs e)
        {
            if (editAct.EditValue.ToString() == "0")
            {
                BIPRG.SelectedIndex = 0;
                editType.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else { BIPRG.SelectedIndex = 1; }
            
            if (editAct.EditValue.ToString() == "0")
            {
                StopingGB.Visible = true;
                editType.Visibility = DevExpress.XtraBars.BarItemVisibility.Always ;
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
    }
}
