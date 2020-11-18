using Mineware.Menu.Structure;
using Mineware.Plugin.Interface;
using Mineware.Systems.Global;
using Mineware.Systems.Global.ReportsControls;
using Mineware.Systems.GlobalConnect;
using Mineware.Systems.GlobalExtensions;
using Mineware.Systems.Printing;
using Mineware.Systems.ProductionAmplatsGlobal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using System.Diagnostics;

namespace Mineware.Systems.ProductionAmplatsBonus
{
    public class ProductionAmplatsBonus : PluginInterface
    {
        public string SystemTag => ProductionAmplatsBonusRes.systemTag;

        public string SystemDBTag => ProductionAmplatsBonusRes.systemDBTag;

        public global::DevExpress.XtraNavBar.NavBarItem getApplicationSettingsNavBarItem()
        {
            return null;
        }

        public BaseUserControl getApplicationSettingsScreen()
        {
            return null;
        }

        public BaseUserControl getMainMenuAdditionalItem()
        {
            return null;
        }

        public global::DevExpress.XtraEditors.TileItem getMainMenuItem()
        {
            return null;
        }

        public BaseUserControl getMenuItem(string itemID)
        {
            BaseUserControl theResult = null;

            ////Booking
            //if (itemID == TProductionAmplatsGlobal.SysMenu.miDailyBookings_apsDailyBookings_MinewareSystemsProductionAmplats.ItemID)
            //{
            //    bool HasAccess = SecurityPAS.HasSyncrominePermission(Security.SyncrominePermissions.Book_DS);
            //    if (HasAccess == true)
            //    {
            //        theResult = new ucBookingProduction_Main();
            //        theResult.CanClose = true; // set the CanClose to true if the can close
            //    }

            //    //Load nightshift bookings
            //    var mimsMainFrm = System.Windows.Forms.Application.OpenForms["MIMSMain"];
            //    var dynMethod = mimsMainFrm.GetType().GetMethod("LoadSelectedItemScreen", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //    var pfi = new ProfileItem();
            //    pfi.ItemID = "BookingsNS";
            //    pfi.SystemID = TProductionAmplatsGlobal.SysMenu.miDailyBookings_apsDailyBookings_MinewareSystemsProductionAmplats.SystemID;
            //    pfi.Description = "Bookings Night Shift";

            //    dynMethod.Invoke(mimsMainFrm, new object[] { pfi });
            //}        


            //Reports
            if (itemID == TProductionAmplatsGlobal.SysMenu.miBonusReports_ReportsBonus_MinewareSystemsProductionAmplatsBonus.ItemID)
            {
                theResult = new ucReports();
                theResult.CanClose = true; // set the CanClose to true if the can close
            }

            //SafetyCaptures
            if (itemID == TProductionAmplatsGlobal.SysMenu.miSafety_CapturesSafety_MinewareSystemsProductionAmplatsBonus.ItemID)
            {
                theResult = new ucSafetyCaptureNew();
                theResult.CanClose = true; // set the CanClose to true if the can close
            }

            //ProductionMiners
            if (itemID == TProductionAmplatsGlobal.SysMenu.miProduction_CapturesProduction_MinewareSystemsProductionAmplatsBonus.ItemID)
            {
                theResult = new ucProductionMinersCapture();
                theResult.CanClose = true; // set the CanClose to true if the can close
            }

            //Tramming Capture
            if (itemID == TProductionAmplatsGlobal.SysMenu.miTramming_CapturesTramming_MinewareSystemsProductionAmplatsBonus.ItemID)
            {
                theResult = new ucTrammingCapture();
                theResult.CanClose = true; // set the CanClose to true if the can close
            }

            //Tramming Bonus
            if (itemID == TProductionAmplatsGlobal.SysMenu.miTrammingBonus_BonusCalcsTramming_MinewareSystemsProductionAmplatsBonus.ItemID)
            {
                theResult = new ucTrammingBonus();
                theResult.CanClose = true; // set the CanClose to true if the can close
            }

            //Gang Mapping
            if (itemID == TProductionAmplatsGlobal.SysMenu.miGangMappingExclusions_SystemAdminMapping_MinewareSystemsProductionAmplatsBonus.ItemID)
            {
                theResult = new ucGangMapping();
                theResult.CanClose = true; // set the CanClose to true if the can close
            }

            //Data Extract
            if (itemID == TProductionAmplatsGlobal.SysMenu.miDataExtract_SystemAdminDataExtract_MinewareSystemsProductionAmplatsBonus.ItemID)
            {
                theResult = new ucDataExtract();
                theResult.CanClose = true; // set the CanClose to true if the can close
            }

            //Mining Parameters
            if (itemID == TProductionAmplatsGlobal.SysMenu.miMiningParameters_MonthlyParametersMining_MinewareSystemsProductionAmplatsBonus.ItemID)
            {
                theResult = new ucMiningParameters();
                theResult.CanClose = true; // set the CanClose to true if the can close
            }

            //Eng Parameters
            if (itemID == TProductionAmplatsGlobal.SysMenu.miEngineeringParameters_MonthlyParametersEngineering_MinewareSystemsProductionAmplatsBonus.ItemID)
            {
                theResult = new ucEngineeringParameters();
                theResult.CanClose = true; // set the CanClose to true if the can close
            }

            //Miners Bonus
            if (itemID == TProductionAmplatsGlobal.SysMenu.miMinersBonus_BonusCalcsMinersBonus_MinewareSystemsProductionAmplatsBonus.ItemID)
            {
                theResult = new ucMinersBonus();
                theResult.CanClose = true; // set the CanClose to true if the can close
            }

            //Crew Bonus
            if (itemID == TProductionAmplatsGlobal.SysMenu.miMiningCrewBonus_BonusCalcsMiningCrewBonus_MinewareSystemsProductionAmplatsBonus.ItemID)
            {
                theResult = new ucCrewBonus();
                theResult.CanClose = true; // set the CanClose to true if the can close
            }

            //Eng Bonus
            if (itemID == TProductionAmplatsGlobal.SysMenu.miEngineeringBonus_BonusCalcsEngineering_MinewareSystemsProductionAmplatsBonus.ItemID)
            {
                theResult = new ucEngBonus();
                theResult.CanClose = true; // set the CanClose to true if the can close
            }

            //SB Bonus
            if (itemID == TProductionAmplatsGlobal.SysMenu.miShiftbossBonus_BonusCalcsSBBonusCalc_MinewareSystemsProductionAmplatsBonus.ItemID)
            {
                theResult = new ucSBBonus();
                theResult.CanClose = true; // set the CanClose to true if the can close
            }

            return theResult;
        }

        public mainMenu getMenuStructure()
        {
            return TProductionAmplatsGlobal.SysMenu.theMenu;
        }

        public List<clsParameters> getParameters()
        {
            return null;
        }

        public ReportSettings getReportSettings(string itemID)
        {
            return null;
        }

        public BaseUserControl getStartScreen()
        {
            //ucDashboardWidgetView _ucDashboardWidgetView = new ucDashboardWidgetView();
            //if (!Debugger.IsAttached)
            //{
            //    _ucDashboardWidgetView.ShowProgressPage(true);
            //}
            //return _ucDashboardWidgetView;
            return null;


        }

        public string getSystemDBTag()
        {
            return ProductionAmplatsBonusRes.systemDBTag;
        }

        public string getSystemTag()
        {
            return ProductionAmplatsBonusRes.systemTag;
        }

        public global::DevExpress.XtraNavBar.NavBarItem getUserSettingsNavBarItem()
        {
            return null;
        }

        public BaseUserControl getUserSettingsScreen(ScreenStatus _theScreenStatus, string _userID, TUserCurrentInfo userInfo, string theConnection)
        {
            return null;
        }

        public void InitializeModule()
        {
            TProductionAmplatsGlobal.SysMenu.setMenuItems();
            TProductionAmplatsGlobal.SysMenu.theMenu.systemDBTag = ProductionAmplatsBonusRes.systemDBTag;
            TProductionAmplatsGlobal.SysMenu.theMenu.systemTag = ProductionAmplatsBonusRes.systemTag;
        }

        public void LoggedOn()
        {
            ProductionAmplatsGlobal.ProductionAmplatsGlobal.SetProductionGlobalInfo(ProductionAmplatsBonusRes.systemDBTag);
           
            
        }

        /// <summary>
        /// When the menu button is clicked show menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        

        public IReportModule getReport(string itemID)
        {
            //if (itemID == TSystemGlobal.SysMenu.miUserLogonHistory_SSUsersReport_MinewareSystemsSettings.ItemID)
            //{
            //    return new UserActivityReport();
            //}

            return null;
        }
    }
}
