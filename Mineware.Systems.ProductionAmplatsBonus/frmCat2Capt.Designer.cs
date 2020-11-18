namespace Mineware.Systems.ProductionAmplatsBonus
{
    partial class frmCat2Capt
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblActCode = new System.Windows.Forms.Label();
            this.txtActivity = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtShift = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.OrgCombo = new System.Windows.Forms.ComboBox();
            this.MOCombo = new System.Windows.Forms.ComboBox();
            this.ProdMonthTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.date1 = new System.Windows.Forms.DateTimePicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SaveBtn = new DevExpress.XtraEditors.SimpleButton();
            this.Close1Btn = new DevExpress.XtraEditors.SimpleButton();
            this.grid = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblActCode);
            this.panel1.Controls.Add(this.txtActivity);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtShift);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.OrgCombo);
            this.panel1.Controls.Add(this.MOCombo);
            this.panel1.Controls.Add(this.ProdMonthTxt);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.date1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(598, 166);
            this.panel1.TabIndex = 0;
            // 
            // lblActCode
            // 
            this.lblActCode.AutoSize = true;
            this.lblActCode.Location = new System.Drawing.Point(540, 121);
            this.lblActCode.Name = "lblActCode";
            this.lblActCode.Size = new System.Drawing.Size(42, 17);
            this.lblActCode.TabIndex = 12;
            this.lblActCode.Text = "label7";
            this.lblActCode.Visible = false;
            // 
            // txtActivity
            // 
            this.txtActivity.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtActivity.Enabled = false;
            this.txtActivity.Location = new System.Drawing.Point(390, 113);
            this.txtActivity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtActivity.Name = "txtActivity";
            this.txtActivity.Size = new System.Drawing.Size(116, 23);
            this.txtActivity.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(302, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "Activity";
            // 
            // txtShift
            // 
            this.txtShift.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtShift.Enabled = false;
            this.txtShift.Location = new System.Drawing.Point(101, 113);
            this.txtShift.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtShift.Name = "txtShift";
            this.txtShift.Size = new System.Drawing.Size(116, 23);
            this.txtShift.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Shift";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(302, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Org Unit";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(302, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "MO Section";
            // 
            // OrgCombo
            // 
            this.OrgCombo.FormattingEnabled = true;
            this.OrgCombo.Location = new System.Drawing.Point(390, 75);
            this.OrgCombo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OrgCombo.Name = "OrgCombo";
            this.OrgCombo.Size = new System.Drawing.Size(140, 24);
            this.OrgCombo.TabIndex = 5;
            this.OrgCombo.TextChanged += new System.EventHandler(this.OrgCombo_TextChanged);
            // 
            // MOCombo
            // 
            this.MOCombo.FormattingEnabled = true;
            this.MOCombo.Location = new System.Drawing.Point(390, 34);
            this.MOCombo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MOCombo.Name = "MOCombo";
            this.MOCombo.Size = new System.Drawing.Size(140, 24);
            this.MOCombo.TabIndex = 4;
            this.MOCombo.TextChanged += new System.EventHandler(this.MOCombo_TextChanged);
            // 
            // ProdMonthTxt
            // 
            this.ProdMonthTxt.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ProdMonthTxt.Enabled = false;
            this.ProdMonthTxt.Location = new System.Drawing.Point(101, 75);
            this.ProdMonthTxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ProdMonthTxt.Name = "ProdMonthTxt";
            this.ProdMonthTxt.Size = new System.Drawing.Size(116, 23);
            this.ProdMonthTxt.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "ProdMonth";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Date";
            // 
            // date1
            // 
            this.date1.Location = new System.Drawing.Point(101, 36);
            this.date1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.date1.Name = "date1";
            this.date1.Size = new System.Drawing.Size(153, 23);
            this.date1.TabIndex = 0;
            this.date1.CloseUp += new System.EventHandler(this.date1_CloseUp);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.SaveBtn);
            this.panel2.Controls.Add(this.Close1Btn);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 556);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(598, 112);
            this.panel2.TabIndex = 1;
            // 
            // SaveBtn
            // 
            this.SaveBtn.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.SaveBtn.Appearance.BackColor2 = System.Drawing.Color.WhiteSmoke;
            this.SaveBtn.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveBtn.Appearance.Options.UseBackColor = true;
            this.SaveBtn.Appearance.Options.UseFont = true;
            this.SaveBtn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.SaveBtn.ImageOptions.Image = global::Mineware.Systems.ProductionAmplatsBonus.Properties.Resources.Save;
            this.SaveBtn.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.SaveBtn.Location = new System.Drawing.Point(101, 23);
            this.SaveBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(159, 59);
            this.SaveBtn.TabIndex = 110;
            this.SaveBtn.Text = "     Save                ";
            // 
            // Close1Btn
            // 
            this.Close1Btn.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Close1Btn.Appearance.BackColor2 = System.Drawing.Color.WhiteSmoke;
            this.Close1Btn.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Close1Btn.Appearance.Options.UseBackColor = true;
            this.Close1Btn.Appearance.Options.UseFont = true;
            this.Close1Btn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.Close1Btn.ImageOptions.Image = global::Mineware.Systems.ProductionAmplatsBonus.Properties.Resources.Close;
            this.Close1Btn.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.Close1Btn.Location = new System.Drawing.Point(344, 23);
            this.Close1Btn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Close1Btn.Name = "Close1Btn";
            this.Close1Btn.Size = new System.Drawing.Size(159, 59);
            this.Close1Btn.TabIndex = 109;
            this.Close1Btn.Text = "     Close                ";
            this.Close1Btn.Click += new System.EventHandler(this.Close1Btn_Click);
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeColumns = false;
            this.grid.AllowUserToResizeRows = false;
            this.grid.BackgroundColor = System.Drawing.Color.White;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 166);
            this.grid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grid.Name = "grid";
            this.grid.RowHeadersWidth = 51;
            this.grid.Size = new System.Drawing.Size(598, 390);
            this.grid.TabIndex = 2;
            // 
            // frmCat2Capt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 668);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.IconOptions.Image = global::Mineware.Systems.ProductionAmplatsBonus.Properties.Resources.SM;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmCat2Capt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCat2Capt";
            this.Load += new System.EventHandler(this.frmCat2Capt_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtActivity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtShift;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox OrgCombo;
        private System.Windows.Forms.ComboBox MOCombo;
        private System.Windows.Forms.TextBox ProdMonthTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker date1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Label lblActCode;
        private DevExpress.XtraEditors.SimpleButton SaveBtn;
        private DevExpress.XtraEditors.SimpleButton Close1Btn;
    }
}