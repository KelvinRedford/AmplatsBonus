﻿namespace Mineware.Systems.ProductionAmplatsBonus
{
    partial class ucSBShifts
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ActivityGroup = new System.Windows.Forms.GroupBox();
            this.rdbDev = new System.Windows.Forms.RadioButton();
            this.rdbStoping = new System.Windows.Forms.RadioButton();
            this.EndDate = new System.Windows.Forms.DateTimePicker();
            this.BeginDate = new System.Windows.Forms.DateTimePicker();
            this.cmbMO = new System.Windows.Forms.ComboBox();
            this.lblMO = new System.Windows.Forms.Label();
            this.showBtn = new DevExpress.XtraEditors.SimpleButton();
            this.Close1Btn = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.ProdMonthTxt = new System.Windows.Forms.NumericUpDown();
            this.ProdMonth1Txt = new System.Windows.Forms.TextBox();
            this.pcReport = new FastReport.Preview.PreviewControl();
            this.grid = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.ActivityGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProdMonthTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ActivityGroup);
            this.panel1.Controls.Add(this.EndDate);
            this.panel1.Controls.Add(this.BeginDate);
            this.panel1.Controls.Add(this.cmbMO);
            this.panel1.Controls.Add(this.lblMO);
            this.panel1.Controls.Add(this.showBtn);
            this.panel1.Controls.Add(this.Close1Btn);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.ProdMonthTxt);
            this.panel1.Controls.Add(this.ProdMonth1Txt);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(989, 103);
            this.panel1.TabIndex = 0;
            // 
            // ActivityGroup
            // 
            this.ActivityGroup.Controls.Add(this.rdbDev);
            this.ActivityGroup.Controls.Add(this.rdbStoping);
            this.ActivityGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActivityGroup.Location = new System.Drawing.Point(243, 1);
            this.ActivityGroup.Name = "ActivityGroup";
            this.ActivityGroup.Size = new System.Drawing.Size(139, 95);
            this.ActivityGroup.TabIndex = 112;
            this.ActivityGroup.TabStop = false;
            this.ActivityGroup.Text = "Activity";
            // 
            // rdbDev
            // 
            this.rdbDev.AutoSize = true;
            this.rdbDev.Location = new System.Drawing.Point(12, 58);
            this.rdbDev.Name = "rdbDev";
            this.rdbDev.Size = new System.Drawing.Size(98, 19);
            this.rdbDev.TabIndex = 1;
            this.rdbDev.Text = "Development";
            this.rdbDev.UseVisualStyleBackColor = true;
            this.rdbDev.CheckedChanged += new System.EventHandler(this.rdbDev_CheckedChanged);
            // 
            // rdbStoping
            // 
            this.rdbStoping.AutoSize = true;
            this.rdbStoping.Checked = true;
            this.rdbStoping.Location = new System.Drawing.Point(12, 27);
            this.rdbStoping.Name = "rdbStoping";
            this.rdbStoping.Size = new System.Drawing.Size(67, 19);
            this.rdbStoping.TabIndex = 0;
            this.rdbStoping.TabStop = true;
            this.rdbStoping.Text = "Stoping";
            this.rdbStoping.UseVisualStyleBackColor = true;
            this.rdbStoping.CheckedChanged += new System.EventHandler(this.rdbStoping_CheckedChanged);
            // 
            // EndDate
            // 
            this.EndDate.Location = new System.Drawing.Point(754, 3);
            this.EndDate.Name = "EndDate";
            this.EndDate.Size = new System.Drawing.Size(147, 20);
            this.EndDate.TabIndex = 111;
            // 
            // BeginDate
            // 
            this.BeginDate.Location = new System.Drawing.Point(590, 4);
            this.BeginDate.Name = "BeginDate";
            this.BeginDate.Size = new System.Drawing.Size(147, 20);
            this.BeginDate.TabIndex = 110;
            // 
            // cmbMO
            // 
            this.cmbMO.FormattingEnabled = true;
            this.cmbMO.Location = new System.Drawing.Point(137, 63);
            this.cmbMO.Name = "cmbMO";
            this.cmbMO.Size = new System.Drawing.Size(100, 21);
            this.cmbMO.TabIndex = 109;
            this.cmbMO.Visible = false;
            this.cmbMO.SelectedIndexChanged += new System.EventHandler(this.cmbMO_SelectedIndexChanged);
            // 
            // lblMO
            // 
            this.lblMO.AutoSize = true;
            this.lblMO.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMO.Location = new System.Drawing.Point(49, 67);
            this.lblMO.Name = "lblMO";
            this.lblMO.Size = new System.Drawing.Size(71, 15);
            this.lblMO.TabIndex = 108;
            this.lblMO.Text = "MO Section";
            this.lblMO.Visible = false;
            // 
            // showBtn
            // 
            this.showBtn.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.showBtn.Appearance.BackColor2 = System.Drawing.Color.WhiteSmoke;
            this.showBtn.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showBtn.Appearance.Options.UseBackColor = true;
            this.showBtn.Appearance.Options.UseFont = true;
            this.showBtn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.showBtn.Image = global::Mineware.Systems.ProductionAmplatsBonus.Properties.Resources.Show;
            this.showBtn.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.showBtn.Location = new System.Drawing.Point(415, 24);
            this.showBtn.Name = "showBtn";
            this.showBtn.Size = new System.Drawing.Size(136, 48);
            this.showBtn.TabIndex = 107;
            this.showBtn.Text = "     Show               ";
            this.showBtn.Click += new System.EventHandler(this.showBtn_Click);
            // 
            // Close1Btn
            // 
            this.Close1Btn.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Close1Btn.Appearance.BackColor2 = System.Drawing.Color.WhiteSmoke;
            this.Close1Btn.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Close1Btn.Appearance.Options.UseBackColor = true;
            this.Close1Btn.Appearance.Options.UseFont = true;
            this.Close1Btn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.Close1Btn.Image = global::Mineware.Systems.ProductionAmplatsBonus.Properties.Resources.Close;
            this.Close1Btn.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.Close1Btn.Location = new System.Drawing.Point(601, 24);
            this.Close1Btn.Name = "Close1Btn";
            this.Close1Btn.Size = new System.Drawing.Size(136, 48);
            this.Close1Btn.TabIndex = 106;
            this.Close1Btn.Text = "     Close                ";
            this.Close1Btn.Click += new System.EventHandler(this.Close1Btn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(49, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 15);
            this.label2.TabIndex = 99;
            this.label2.Text = "ProdMonth";
            // 
            // ProdMonthTxt
            // 
            this.ProdMonthTxt.Location = new System.Drawing.Point(219, 24);
            this.ProdMonthTxt.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.ProdMonthTxt.Name = "ProdMonthTxt";
            this.ProdMonthTxt.Size = new System.Drawing.Size(18, 20);
            this.ProdMonthTxt.TabIndex = 98;
            this.ProdMonthTxt.Click += new System.EventHandler(this.ProdMonthTxt_Click);
            // 
            // ProdMonth1Txt
            // 
            this.ProdMonth1Txt.BackColor = System.Drawing.Color.White;
            this.ProdMonth1Txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProdMonth1Txt.Location = new System.Drawing.Point(137, 24);
            this.ProdMonth1Txt.MaxLength = 10000000;
            this.ProdMonth1Txt.Name = "ProdMonth1Txt";
            this.ProdMonth1Txt.ReadOnly = true;
            this.ProdMonth1Txt.Size = new System.Drawing.Size(100, 20);
            this.ProdMonth1Txt.TabIndex = 97;
            this.ProdMonth1Txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pcReport
            // 
            this.pcReport.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pcReport.Buttons = ((FastReport.PreviewButtons)(((((((((FastReport.PreviewButtons.Print | FastReport.PreviewButtons.Open) 
            | FastReport.PreviewButtons.Save) 
            | FastReport.PreviewButtons.Email) 
            | FastReport.PreviewButtons.Find) 
            | FastReport.PreviewButtons.Zoom) 
            | FastReport.PreviewButtons.PageSetup) 
            | FastReport.PreviewButtons.Navigator) 
            | FastReport.PreviewButtons.Close)));
            this.pcReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcReport.Font = new System.Drawing.Font("Tahoma", 8F);
            this.pcReport.Location = new System.Drawing.Point(0, 103);
            this.pcReport.Name = "pcReport";
            this.pcReport.PageOffset = new System.Drawing.Point(10, 10);
            this.pcReport.Size = new System.Drawing.Size(989, 450);
            this.pcReport.TabIndex = 3;
            this.pcReport.UIStyle = FastReport.Utils.UIStyle.Office2007Black;
            // 
            // grid
            // 
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeColumns = false;
            this.grid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid.DefaultCellStyle = dataGridViewCellStyle2;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 103);
            this.grid.Name = "grid";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grid.Size = new System.Drawing.Size(989, 450);
            this.grid.TabIndex = 4;
            this.grid.Visible = false;
            // 
            // frmSBShifts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 553);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.pcReport);
            this.Controls.Add(this.panel1);
            this.Name = "frmSBShifts";
            this.Text = "frmSBShifts";
           
            this.Load += new System.EventHandler(this.frmSBShifts_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ActivityGroup.ResumeLayout(false);
            this.ActivityGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProdMonthTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown ProdMonthTxt;
        private System.Windows.Forms.TextBox ProdMonth1Txt;
        private DevExpress.XtraEditors.SimpleButton showBtn;
        private DevExpress.XtraEditors.SimpleButton Close1Btn;
        private FastReport.Preview.PreviewControl pcReport;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.ComboBox cmbMO;
        private System.Windows.Forms.Label lblMO;
        private System.Windows.Forms.DateTimePicker EndDate;
        private System.Windows.Forms.DateTimePicker BeginDate;
        private System.Windows.Forms.GroupBox ActivityGroup;
        private System.Windows.Forms.RadioButton rdbDev;
        private System.Windows.Forms.RadioButton rdbStoping;
    }
}