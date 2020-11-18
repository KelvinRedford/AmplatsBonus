namespace Mineware.Systems.ProductionAmplatsBonus
{
    partial class frmProductionProp
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
            this.components = new System.ComponentModel.Container();
            this.alertControl1 = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            this.lblHint = new System.Windows.Forms.Label();
            this.lbGangs = new System.Windows.Forms.ListBox();
            this.lblGangMembers = new System.Windows.Forms.Label();
            this.tbLevel = new System.Windows.Forms.TextBox();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblDesignation = new System.Windows.Forms.Label();
            this.luShift = new DevExpress.XtraEditors.LookUpEdit();
            this.luEmpDetail = new DevExpress.XtraEditors.LookUpEdit();
            this.tbOrg = new System.Windows.Forms.TextBox();
            this.tbMillMonth = new System.Windows.Forms.TextBox();
            this.lblShift = new System.Windows.Forms.Label();
            this.lblOrg = new System.Windows.Forms.Label();
            this.lblEmp = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.luCategory = new DevExpress.XtraEditors.LookUpEdit();
            this.sbtnAdd = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.luShift.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.luEmpDetail.Properties)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.luCategory.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblHint.Location = new System.Drawing.Point(428, 11);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(257, 17);
            this.lblHint.TabIndex = 3;
            this.lblHint.Text = "Double click on Gang Member to remove";
            // 
            // lbGangs
            // 
            this.lbGangs.FormattingEnabled = true;
            this.lbGangs.ItemHeight = 16;
            this.lbGangs.Location = new System.Drawing.Point(388, 48);
            this.lbGangs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbGangs.Name = "lbGangs";
            this.lbGangs.Size = new System.Drawing.Size(322, 180);
            this.lbGangs.TabIndex = 182;
            this.lbGangs.DoubleClick += new System.EventHandler(this.lbGangs_DoubleClick);
            // 
            // lblGangMembers
            // 
            this.lblGangMembers.AutoSize = true;
            this.lblGangMembers.Location = new System.Drawing.Point(385, 28);
            this.lblGangMembers.Name = "lblGangMembers";
            this.lblGangMembers.Size = new System.Drawing.Size(104, 17);
            this.lblGangMembers.TabIndex = 181;
            this.lblGangMembers.Text = "Gang Members:";
            // 
            // tbLevel
            // 
            this.tbLevel.BackColor = System.Drawing.Color.White;
            this.tbLevel.Enabled = false;
            this.tbLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLevel.Location = new System.Drawing.Point(281, 16);
            this.tbLevel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbLevel.MaxLength = 10000000;
            this.tbLevel.Name = "tbLevel";
            this.tbLevel.ReadOnly = true;
            this.tbLevel.Size = new System.Drawing.Size(94, 23);
            this.tbLevel.TabIndex = 176;
            this.tbLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbLevel.Visible = false;
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(232, 20);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(44, 17);
            this.lblLevel.TabIndex = 175;
            this.lblLevel.Text = "Level:";
            this.lblLevel.Visible = false;
            // 
            // lblDesignation
            // 
            this.lblDesignation.AutoSize = true;
            this.lblDesignation.Location = new System.Drawing.Point(47, 113);
            this.lblDesignation.Name = "lblDesignation";
            this.lblDesignation.Size = new System.Drawing.Size(84, 17);
            this.lblDesignation.TabIndex = 173;
            this.lblDesignation.Text = "Designation:";
            // 
            // luShift
            // 
            this.luShift.Location = new System.Drawing.Point(131, 142);
            this.luShift.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.luShift.Name = "luShift";
            this.luShift.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luShift.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Shift", "Shift")});
            this.luShift.Properties.DisplayMember = "Shift";
            this.luShift.Properties.DropDownRows = 6;
            this.luShift.Properties.NullText = "";
            this.luShift.Properties.ValueMember = "Shift";
            this.luShift.Size = new System.Drawing.Size(94, 22);
            this.luShift.TabIndex = 172;
            // 
            // luEmpDetail
            // 
            this.luEmpDetail.Location = new System.Drawing.Point(131, 78);
            this.luEmpDetail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.luEmpDetail.Name = "luEmpDetail";
            this.luEmpDetail.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luEmpDetail.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("EmployeeDetail", "Employee Detail")});
            this.luEmpDetail.Properties.DisplayMember = "EmployeeDetail";
            this.luEmpDetail.Properties.DropDownRows = 20;
            this.luEmpDetail.Properties.NullText = "";
            this.luEmpDetail.Properties.ValueMember = "EmployeeDetail";
            this.luEmpDetail.Size = new System.Drawing.Size(220, 22);
            this.luEmpDetail.TabIndex = 170;
            this.luEmpDetail.EditValueChanged += new System.EventHandler(this.luEmpDetail_EditValueChanged);
            // 
            // tbOrg
            // 
            this.tbOrg.BackColor = System.Drawing.Color.White;
            this.tbOrg.Enabled = false;
            this.tbOrg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOrg.Location = new System.Drawing.Point(131, 46);
            this.tbOrg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbOrg.MaxLength = 10000000;
            this.tbOrg.Name = "tbOrg";
            this.tbOrg.ReadOnly = true;
            this.tbOrg.Size = new System.Drawing.Size(94, 23);
            this.tbOrg.TabIndex = 165;
            this.tbOrg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbMillMonth
            // 
            this.tbMillMonth.BackColor = System.Drawing.Color.White;
            this.tbMillMonth.Enabled = false;
            this.tbMillMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMillMonth.Location = new System.Drawing.Point(131, 16);
            this.tbMillMonth.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbMillMonth.MaxLength = 10000000;
            this.tbMillMonth.Name = "tbMillMonth";
            this.tbMillMonth.ReadOnly = true;
            this.tbMillMonth.Size = new System.Drawing.Size(94, 23);
            this.tbMillMonth.TabIndex = 164;
            this.tbMillMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbMillMonth.Visible = false;
            // 
            // lblShift
            // 
            this.lblShift.AutoSize = true;
            this.lblShift.Location = new System.Drawing.Point(87, 145);
            this.lblShift.Name = "lblShift";
            this.lblShift.Size = new System.Drawing.Size(40, 17);
            this.lblShift.TabIndex = 4;
            this.lblShift.Text = "Shift:";
            // 
            // lblOrg
            // 
            this.lblOrg.AutoSize = true;
            this.lblOrg.Location = new System.Drawing.Point(72, 49);
            this.lblOrg.Name = "lblOrg";
            this.lblOrg.Size = new System.Drawing.Size(59, 17);
            this.lblOrg.TabIndex = 2;
            this.lblOrg.Text = "Orgunit:";
            // 
            // lblEmp
            // 
            this.lblEmp.AutoSize = true;
            this.lblEmp.Location = new System.Drawing.Point(23, 81);
            this.lblEmp.Name = "lblEmp";
            this.lblEmp.Size = new System.Drawing.Size(110, 17);
            this.lblEmp.TabIndex = 1;
            this.lblEmp.Text = "Employee Detail:";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(14, 20);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(123, 17);
            this.lblDate.TabIndex = 0;
            this.lblDate.Text = "Production Month:";
            this.lblDate.Visible = false;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.luCategory);
            this.pnlMain.Controls.Add(this.lblHint);
            this.pnlMain.Controls.Add(this.sbtnAdd);
            this.pnlMain.Controls.Add(this.lbGangs);
            this.pnlMain.Controls.Add(this.lblGangMembers);
            this.pnlMain.Controls.Add(this.tbLevel);
            this.pnlMain.Controls.Add(this.lblLevel);
            this.pnlMain.Controls.Add(this.lblDesignation);
            this.pnlMain.Controls.Add(this.luShift);
            this.pnlMain.Controls.Add(this.luEmpDetail);
            this.pnlMain.Controls.Add(this.tbOrg);
            this.pnlMain.Controls.Add(this.tbMillMonth);
            this.pnlMain.Controls.Add(this.lblShift);
            this.pnlMain.Controls.Add(this.lblOrg);
            this.pnlMain.Controls.Add(this.lblEmp);
            this.pnlMain.Controls.Add(this.lblDate);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(716, 233);
            this.pnlMain.TabIndex = 2;
            // 
            // luCategory
            // 
            this.luCategory.Location = new System.Drawing.Point(131, 110);
            this.luCategory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.luCategory.Name = "luCategory";
            this.luCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luCategory.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Designation", "Category")});
            this.luCategory.Properties.DisplayMember = "Designation";
            this.luCategory.Properties.DropDownRows = 20;
            this.luCategory.Properties.NullText = "";
            this.luCategory.Properties.ValueMember = "Designation";
            this.luCategory.Size = new System.Drawing.Size(220, 22);
            this.luCategory.TabIndex = 185;
            // 
            // sbtnAdd
            // 
            this.sbtnAdd.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.sbtnAdd.ImageOptions.SvgImage = global::Mineware.Systems.ProductionAmplatsBonus.Properties.Resources.AddBlue;
            this.sbtnAdd.ImageOptions.SvgImageSize = new System.Drawing.Size(18, 18);
            this.sbtnAdd.Location = new System.Drawing.Point(287, 187);
            this.sbtnAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.sbtnAdd.Name = "sbtnAdd";
            this.sbtnAdd.Size = new System.Drawing.Size(88, 33);
            this.sbtnAdd.TabIndex = 183;
            this.sbtnAdd.Text = "Add";
            this.sbtnAdd.Click += new System.EventHandler(this.sbtnAdd_Click);
            // 
            // frmProductionProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 233);
            this.Controls.Add(this.pnlMain);
            this.IconOptions.Image = global::Mineware.Systems.ProductionAmplatsBonus.Properties.Resources.SM;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmProductionProp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add / Remove Gang Member";
            this.Load += new System.EventHandler(this.frmProductionProp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.luShift.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.luEmpDetail.Properties)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.luCategory.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Alerter.AlertControl alertControl1;
        private System.Windows.Forms.Label lblHint;
        private DevExpress.XtraEditors.SimpleButton sbtnAdd;
        private System.Windows.Forms.ListBox lbGangs;
        private System.Windows.Forms.Label lblGangMembers;
        public System.Windows.Forms.TextBox tbLevel;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblDesignation;
        private DevExpress.XtraEditors.LookUpEdit luShift;
        private DevExpress.XtraEditors.LookUpEdit luEmpDetail;
        public System.Windows.Forms.TextBox tbOrg;
        public System.Windows.Forms.TextBox tbMillMonth;
        private System.Windows.Forms.Label lblShift;
        private System.Windows.Forms.Label lblOrg;
        private System.Windows.Forms.Label lblEmp;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Panel pnlMain;
        private DevExpress.XtraEditors.LookUpEdit luCategory;
    }
}