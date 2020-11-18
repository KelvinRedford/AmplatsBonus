namespace Mineware.Systems.ProductionAmplatsBonus
{
    partial class ucAuditReports
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucAuditReports));
            this.theDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblReportType = new System.Windows.Forms.Label();
            this.pcReport = new FastReport.Preview.PreviewControl();
            this.rcReports = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItem2 = new DevExpress.XtraBars.BarEditItem();
            this.editProdmonth = new DevExpress.XtraBars.BarEditItem();
            this.mwRepositoryItemProdMonth1 = new Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth();
            this.editSections = new DevExpress.XtraBars.BarEditItem();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.btnShow = new DevExpress.XtraBars.BarButtonItem();
            this.SelectGroup = new DevExpress.XtraBars.BarEditItem();
            this.btnAddOrgunit = new DevExpress.XtraBars.BarButtonItem();
            this.editLevel = new DevExpress.XtraBars.BarEditItem();
            this.editShift = new DevExpress.XtraBars.BarEditItem();
            this.editActivity = new DevExpress.XtraBars.BarEditItem();
            this.editUsers = new DevExpress.XtraBars.BarEditItem();
            this.LookUpEditUsers = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.repositoryItemSearchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.rpReports = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ((System.ComponentModel.ISupportInitialize)(this.rcReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mwRepositoryItemProdMonth1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LookUpEditUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // theDate
            // 
            this.theDate.Location = new System.Drawing.Point(830, 38);
            this.theDate.Name = "theDate";
            this.theDate.Size = new System.Drawing.Size(123, 23);
            this.theDate.TabIndex = 111;
            this.theDate.Visible = false;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(785, 43);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(39, 18);
            this.lblDate.TabIndex = 110;
            this.lblDate.Text = "Date";
            this.lblDate.Visible = false;
            // 
            // lblReportType
            // 
            this.lblReportType.AutoSize = true;
            this.lblReportType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportType.ForeColor = System.Drawing.Color.Red;
            this.lblReportType.Location = new System.Drawing.Point(975, 62);
            this.lblReportType.Name = "lblReportType";
            this.lblReportType.Size = new System.Drawing.Size(59, 20);
            this.lblReportType.TabIndex = 108;
            this.lblReportType.Text = "label1";
            // 
            // pcReport
            // 
            this.pcReport.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pcReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcReport.Font = new System.Drawing.Font("Tahoma", 8F);
            this.pcReport.Location = new System.Drawing.Point(0, 122);
            this.pcReport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pcReport.Name = "pcReport";
            this.pcReport.PageOffset = new System.Drawing.Point(10, 10);
            this.pcReport.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pcReport.SaveInitialDirectory = null;
            this.pcReport.Size = new System.Drawing.Size(1196, 568);
            this.pcReport.TabIndex = 9;
            this.pcReport.UIStyle = FastReport.Utils.UIStyle.Office2007Black;
            // 
            // rcReports
            // 
            this.rcReports.AllowKeyTips = false;
            this.rcReports.AllowMdiChildButtons = false;
            this.rcReports.AllowMinimizeRibbon = false;
            this.rcReports.AllowTrimPageText = false;
            this.rcReports.AutoSizeItems = true;
            this.rcReports.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.rcReports.ColorScheme = DevExpress.XtraBars.Ribbon.RibbonControlColorScheme.DarkBlue;
            this.rcReports.ExpandCollapseItem.Id = 0;
            this.rcReports.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.rcReports.ExpandCollapseItem,
            this.rcReports.SearchEditItem,
            this.barButtonItem5,
            this.barButtonItem6,
            this.barEditItem2,
            this.editProdmonth,
            this.editSections,
            this.barButtonItem7,
            this.btnShow,
            this.SelectGroup,
            this.btnAddOrgunit,
            this.editLevel,
            this.editShift,
            this.editActivity,
            this.editUsers});
            this.rcReports.Location = new System.Drawing.Point(0, 0);
            this.rcReports.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rcReports.MaxItemId = 38;
            this.rcReports.Name = "rcReports";
            this.rcReports.OptionsPageCategories.ShowCaptions = false;
            this.rcReports.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpReports});
            this.rcReports.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.mwRepositoryItemProdMonth1,
            this.LookUpEditUsers});
            this.rcReports.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2019;
            this.rcReports.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rcReports.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.rcReports.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.rcReports.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.rcReports.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.rcReports.ShowToolbarCustomizeItem = false;
            this.rcReports.Size = new System.Drawing.Size(1196, 122);
            this.rcReports.Toolbar.ShowCustomizeItem = false;
            this.rcReports.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "                                                                                 " +
    "        ";
            this.barButtonItem5.Id = 3;
            this.barButtonItem5.Name = "barButtonItem5";
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "                               ";
            this.barButtonItem6.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.barButtonItem6.Id = 4;
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // barEditItem2
            // 
            this.barEditItem2.Caption = "Date     ";
            this.barEditItem2.Edit = null;
            this.barEditItem2.EditWidth = 120;
            this.barEditItem2.Id = 21;
            this.barEditItem2.Name = "barEditItem2";
            // 
            // editProdmonth
            // 
            this.editProdmonth.Caption = "Production Month";
            this.editProdmonth.Edit = this.mwRepositoryItemProdMonth1;
            this.editProdmonth.EditWidth = 100;
            this.editProdmonth.Id = 27;
            this.editProdmonth.Name = "editProdmonth";
            this.editProdmonth.EditValueChanged += new System.EventHandler(this.editProdmonth_EditValueChanged);
            // 
            // mwRepositoryItemProdMonth1
            // 
            this.mwRepositoryItemProdMonth1.AutoHeight = false;
            this.mwRepositoryItemProdMonth1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinUp, "1", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinDown, "2", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.mwRepositoryItemProdMonth1.Mask.EditMask = "yyyyMM";
            this.mwRepositoryItemProdMonth1.Mask.IgnoreMaskBlank = false;
            this.mwRepositoryItemProdMonth1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime;
            this.mwRepositoryItemProdMonth1.Mask.UseMaskAsDisplayFormat = true;
            this.mwRepositoryItemProdMonth1.Name = "mwRepositoryItemProdMonth1";
            // 
            // editSections
            // 
            this.editSections.Caption = "Section ";
            this.editSections.Edit = null;
            this.editSections.EditWidth = 160;
            this.editSections.Id = 28;
            this.editSections.Name = "editSections";
            // 
            // barButtonItem7
            // 
            this.barButtonItem7.Caption = "Close";
            this.barButtonItem7.Id = 29;
            this.barButtonItem7.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem7.ImageOptions.SvgImage")));
            this.barButtonItem7.Name = "barButtonItem7";
            this.barButtonItem7.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem7_ItemClick);
            // 
            // btnShow
            // 
            this.btnShow.Caption = "Show";
            this.btnShow.Id = 30;
            this.btnShow.ImageOptions.SvgImage = global::Mineware.Systems.ProductionAmplatsBonus.Properties.Resources.ZoomBlue;
            this.btnShow.Name = "btnShow";
            this.btnShow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnShow_ItemClick);
            // 
            // SelectGroup
            // 
            this.SelectGroup.Caption = " ";
            this.SelectGroup.Edit = null;
            this.SelectGroup.EditHeight = 65;
            this.SelectGroup.EditValue = "Stoping";
            this.SelectGroup.EditWidth = 100;
            this.SelectGroup.Id = 32;
            this.SelectGroup.Name = "SelectGroup";
            // 
            // btnAddOrgunit
            // 
            this.btnAddOrgunit.Caption = "Add   Orgunit";
            this.btnAddOrgunit.Id = 33;
            this.btnAddOrgunit.ImageOptions.SvgImage = global::Mineware.Systems.ProductionAmplatsBonus.Properties.Resources.EmployeesBlue;
            this.btnAddOrgunit.Name = "btnAddOrgunit";
            // 
            // editLevel
            // 
            this.editLevel.Caption = "Level";
            this.editLevel.Edit = null;
            this.editLevel.EditWidth = 100;
            this.editLevel.Id = 34;
            this.editLevel.Name = "editLevel";
            // 
            // editShift
            // 
            this.editShift.Caption = "Shift";
            this.editShift.Edit = null;
            this.editShift.Id = 35;
            this.editShift.Name = "editShift";
            // 
            // editActivity
            // 
            this.editActivity.Caption = "Activity";
            this.editActivity.Edit = null;
            this.editActivity.EditHeight = 50;
            this.editActivity.EditValue = "0";
            this.editActivity.EditWidth = 100;
            this.editActivity.Id = 36;
            this.editActivity.Name = "editActivity";
            // 
            // editUsers
            // 
            this.editUsers.Caption = "Users ";
            this.editUsers.Edit = this.LookUpEditUsers;
            this.editUsers.EditWidth = 165;
            this.editUsers.Id = 37;
            this.editUsers.Name = "editUsers";
            // 
            // LookUpEditUsers
            // 
            this.LookUpEditUsers.AutoHeight = false;
            this.LookUpEditUsers.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.LookUpEditUsers.Name = "LookUpEditUsers";
            this.LookUpEditUsers.NullText = "";
            this.LookUpEditUsers.PopupView = this.repositoryItemSearchLookUpEdit1View;
            // 
            // repositoryItemSearchLookUpEdit1View
            // 
            this.repositoryItemSearchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemSearchLookUpEdit1View.Name = "repositoryItemSearchLookUpEdit1View";
            this.repositoryItemSearchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemSearchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // rpReports
            // 
            this.rpReports.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup3});
            this.rpReports.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("rpReports.ImageOptions.SvgImage")));
            this.rpReports.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.rpReports.Name = "rpReports";
            this.rpReports.Text = "Audit Report";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.editProdmonth);
            this.ribbonPageGroup1.ItemLinks.Add(this.editSections);
            this.ribbonPageGroup1.ItemLinks.Add(this.editLevel);
            this.ribbonPageGroup1.ItemLinks.Add(this.editShift);
            this.ribbonPageGroup1.ItemLinks.Add(this.editActivity);
            this.ribbonPageGroup1.ItemLinks.Add(this.editUsers);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Filter";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.btnShow);
            this.ribbonPageGroup3.ItemLinks.Add(this.barButtonItem7);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "Options";
            // 
            // ucAuditReports
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pcReport);
            this.Controls.Add(this.theDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblReportType);
            this.Controls.Add(this.rcReports);
            this.Name = "ucAuditReports";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(1196, 690);
            this.Load += new System.EventHandler(this.frmAuditReports_Load);
            this.Controls.SetChildIndex(this.rcReports, 0);
            this.Controls.SetChildIndex(this.lblReportType, 0);
            this.Controls.SetChildIndex(this.lblDate, 0);
            this.Controls.SetChildIndex(this.theDate, 0);
            this.Controls.SetChildIndex(this.pcReport, 0);
            ((System.ComponentModel.ISupportInitialize)(this.rcReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mwRepositoryItemProdMonth1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LookUpEditUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label lblReportType;
        private System.Windows.Forms.DateTimePicker theDate;
        private System.Windows.Forms.Label lblDate;
        private FastReport.Preview.PreviewControl pcReport;
        private DevExpress.XtraBars.Ribbon.RibbonControl rcReports;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarEditItem barEditItem2;
        private DevExpress.XtraBars.BarEditItem editProdmonth;
        private Global.CustomControls.MWRepositoryItemProdMonth mwRepositoryItemProdMonth1;
        private DevExpress.XtraBars.BarEditItem editSections;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraBars.BarButtonItem btnShow;
        private DevExpress.XtraBars.BarEditItem SelectGroup;
        private DevExpress.XtraBars.BarButtonItem btnAddOrgunit;
        private DevExpress.XtraBars.BarEditItem editLevel;
        private DevExpress.XtraBars.BarEditItem editShift;
        private DevExpress.XtraBars.BarEditItem editActivity;
        private DevExpress.XtraBars.BarEditItem editUsers;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit LookUpEditUsers;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemSearchLookUpEdit1View;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpReports;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
    }
}