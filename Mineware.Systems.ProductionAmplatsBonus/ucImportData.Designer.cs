namespace Mineware.Systems.ProductionAmplatsBonus
{
    partial class ucImportData
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

        #region Component Designer generated code

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
            this.rcImportData = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.tbProdMonth = new DevExpress.XtraBars.BarEditItem();
            this.mwRepositoryItemProdMonth1 = new Mineware.Systems.Global.CustomControls.MWRepositoryItemProdMonth();
            this.btnClose = new DevExpress.XtraBars.BarButtonItem();
            this.rpGangMapping = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.gcBonusImport = new DevExpress.XtraGrid.GridControl();
            this.gvBonusImport = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand58 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colUser = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colSection = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.label133 = new System.Windows.Forms.Label();
            this.MOlistBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.rcImportData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mwRepositoryItemProdMonth1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBonusImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBonusImport)).BeginInit();
            this.SuspendLayout();
            // 
            // rcImportData
            // 
            this.rcImportData.AllowKeyTips = false;
            this.rcImportData.AllowMdiChildButtons = false;
            this.rcImportData.AllowMinimizeRibbon = false;
            this.rcImportData.AllowTrimPageText = false;
            this.rcImportData.ColorScheme = DevExpress.XtraBars.Ribbon.RibbonControlColorScheme.DarkBlue;
            this.rcImportData.DrawGroupCaptions = DevExpress.Utils.DefaultBoolean.True;
            this.rcImportData.DrawGroupsBorderMode = DevExpress.Utils.DefaultBoolean.True;
            this.rcImportData.ExpandCollapseItem.Id = 0;
            this.rcImportData.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.rcImportData.ExpandCollapseItem,
            this.rcImportData.SearchEditItem,
            this.tbProdMonth,
            this.btnClose});
            this.rcImportData.Location = new System.Drawing.Point(0, 0);
            this.rcImportData.Margin = new System.Windows.Forms.Padding(4);
            this.rcImportData.MaxItemId = 14;
            this.rcImportData.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Never;
            this.rcImportData.Name = "rcImportData";
            this.rcImportData.OptionsPageCategories.ShowCaptions = false;
            this.rcImportData.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpGangMapping});
            this.rcImportData.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.mwRepositoryItemProdMonth1});
            this.rcImportData.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.rcImportData.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.rcImportData.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.rcImportData.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.rcImportData.ShowToolbarCustomizeItem = false;
            this.rcImportData.Size = new System.Drawing.Size(1007, 122);
            this.rcImportData.Toolbar.ShowCustomizeItem = false;
            this.rcImportData.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // tbProdMonth
            // 
            this.tbProdMonth.Caption = "ProdMonth";
            this.tbProdMonth.Edit = this.mwRepositoryItemProdMonth1;
            this.tbProdMonth.EditWidth = 100;
            this.tbProdMonth.Id = 12;
            this.tbProdMonth.Name = "tbProdMonth";
            this.tbProdMonth.EditValueChanged += new System.EventHandler(this.tbProdMonth_EditValueChanged);
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
            // btnClose
            // 
            this.btnClose.Caption = "Close";
            this.btnClose.Id = 13;
            this.btnClose.ImageOptions.SvgImage = global::Mineware.Systems.ProductionAmplatsBonus.Properties.Resources.CloseRed;
            this.btnClose.Name = "btnClose";
            this.btnClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnClose_ItemClick);
            // 
            // rpGangMapping
            // 
            this.rpGangMapping.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2});
            this.rpGangMapping.ImageOptions.SvgImage = global::Mineware.Systems.ProductionAmplatsBonus.Properties.Resources.EmployeesBlue;
            this.rpGangMapping.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.rpGangMapping.Name = "rpGangMapping";
            this.rpGangMapping.Text = "Gang Mapping";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.tbProdMonth);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Options";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.btnClose);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Actions";
            // 
            // gcBonusImport
            // 
            this.gcBonusImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gcBonusImport.Cursor = System.Windows.Forms.Cursors.Default;
            this.gcBonusImport.EmbeddedNavigator.Appearance.Options.UseTextOptions = true;
            this.gcBonusImport.EmbeddedNavigator.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gcBonusImport.Location = new System.Drawing.Point(15, 159);
            this.gcBonusImport.MainView = this.gvBonusImport;
            this.gcBonusImport.Name = "gcBonusImport";
            this.gcBonusImport.Size = new System.Drawing.Size(484, 472);
            this.gcBonusImport.TabIndex = 218;
            this.gcBonusImport.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBonusImport});
            // 
            // gvBonusImport
            // 
            this.gvBonusImport.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvBonusImport.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvBonusImport.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvBonusImport.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gvBonusImport.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand58});
            this.gvBonusImport.ColumnPanelRowHeight = 20;
            this.gvBonusImport.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colUser,
            this.colDate,
            this.colSection});
            this.gvBonusImport.GridControl = this.gcBonusImport;
            this.gvBonusImport.Name = "gvBonusImport";
            this.gvBonusImport.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvBonusImport.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvBonusImport.OptionsView.ColumnAutoWidth = false;
            this.gvBonusImport.OptionsView.ShowBands = false;
            this.gvBonusImport.OptionsView.ShowGroupPanel = false;
            this.gvBonusImport.OptionsView.ShowIndicator = false;
            // 
            // gridBand58
            // 
            this.gridBand58.Caption = "gridBand11";
            this.gridBand58.Columns.Add(this.colUser);
            this.gridBand58.Columns.Add(this.colDate);
            this.gridBand58.Columns.Add(this.colSection);
            this.gridBand58.Name = "gridBand58";
            this.gridBand58.VisibleIndex = 0;
            this.gridBand58.Width = 450;
            // 
            // colUser
            // 
            this.colUser.Caption = "User";
            this.colUser.Name = "colUser";
            this.colUser.OptionsColumn.AllowEdit = false;
            this.colUser.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colUser.OptionsColumn.AllowMove = false;
            this.colUser.OptionsColumn.AllowSize = false;
            this.colUser.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colUser.OptionsColumn.FixedWidth = true;
            this.colUser.OptionsColumn.ReadOnly = true;
            this.colUser.OptionsFilter.AllowFilter = false;
            this.colUser.Visible = true;
            this.colUser.Width = 150;
            // 
            // colDate
            // 
            this.colDate.AppearanceCell.Options.UseTextOptions = true;
            this.colDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDate.Caption = "Date";
            this.colDate.Name = "colDate";
            this.colDate.OptionsColumn.AllowEdit = false;
            this.colDate.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colDate.OptionsColumn.AllowMove = false;
            this.colDate.OptionsColumn.AllowSize = false;
            this.colDate.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDate.OptionsColumn.FixedWidth = true;
            this.colDate.OptionsColumn.ReadOnly = true;
            this.colDate.OptionsFilter.AllowFilter = false;
            this.colDate.Visible = true;
            this.colDate.Width = 150;
            // 
            // colSection
            // 
            this.colSection.Caption = "Section";
            this.colSection.Name = "colSection";
            this.colSection.OptionsColumn.AllowEdit = false;
            this.colSection.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colSection.OptionsColumn.AllowMove = false;
            this.colSection.OptionsColumn.AllowSize = false;
            this.colSection.OptionsColumn.FixedWidth = true;
            this.colSection.Visible = true;
            this.colSection.Width = 150;
            // 
            // label133
            // 
            this.label133.AutoSize = true;
            this.label133.Location = new System.Drawing.Point(522, 136);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(151, 17);
            this.label133.TabIndex = 221;
            this.label133.Text = "DQlik Section to Import";
            // 
            // MOlistBox
            // 
            this.MOlistBox.FormattingEnabled = true;
            this.MOlistBox.ItemHeight = 16;
            this.MOlistBox.Location = new System.Drawing.Point(525, 159);
            this.MOlistBox.Name = "MOlistBox";
            this.MOlistBox.Size = new System.Drawing.Size(169, 148);
            this.MOlistBox.TabIndex = 220;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 223;
            this.label1.Text = "Data Import";
            // 
            // ucImportData
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label133);
            this.Controls.Add(this.MOlistBox);
            this.Controls.Add(this.gcBonusImport);
            this.Controls.Add(this.rcImportData);
            this.Name = "ucImportData";
            this.ShowIInfo = false;
            this.Size = new System.Drawing.Size(1007, 655);
            this.Load += new System.EventHandler(this.ucImportData_Load);
            this.Controls.SetChildIndex(this.rcImportData, 0);
            this.Controls.SetChildIndex(this.gcBonusImport, 0);
            this.Controls.SetChildIndex(this.MOlistBox, 0);
            this.Controls.SetChildIndex(this.label133, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.rcImportData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mwRepositoryItemProdMonth1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBonusImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBonusImport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl rcImportData;
        private DevExpress.XtraBars.BarEditItem tbProdMonth;
        private Global.CustomControls.MWRepositoryItemProdMonth mwRepositoryItemProdMonth1;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpGangMapping;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraGrid.GridControl gcBonusImport;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView gvBonusImport;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand58;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colUser;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSection;
        private System.Windows.Forms.Label label133;
        private System.Windows.Forms.ListBox MOlistBox;
        private DevExpress.XtraBars.BarButtonItem btnClose;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private System.Windows.Forms.Label label1;
    }
}
