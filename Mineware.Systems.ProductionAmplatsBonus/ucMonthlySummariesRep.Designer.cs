namespace Mineware.Systems.ProductionAmplatsBonus
{
    partial class ucMonthlySummariesRep
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
            this.pcReport = new FastReport.Preview.PreviewControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbDev = new System.Windows.Forms.RadioButton();
            this.rdbStoping = new System.Windows.Forms.RadioButton();
            this.txtDisplayMonth = new System.Windows.Forms.TextBox();
            this.showBtn = new DevExpress.XtraEditors.SimpleButton();
            this.Close1Btn = new DevExpress.XtraEditors.SimpleButton();
            this.lblProdmonth = new System.Windows.Forms.Label();
            this.ProdMonthTxt = new System.Windows.Forms.NumericUpDown();
            this.ProdMonth1Txt = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProdMonthTxt)).BeginInit();
            this.SuspendLayout();
            // 
            // pcReport
            // 
            this.pcReport.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pcReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcReport.Font = new System.Drawing.Font("Tahoma", 8F);
            this.pcReport.Location = new System.Drawing.Point(0, 103);
            this.pcReport.Name = "pcReport";
            this.pcReport.PageOffset = new System.Drawing.Point(10, 10);
            this.pcReport.Size = new System.Drawing.Size(992, 399);
            this.pcReport.TabIndex = 11;
            this.pcReport.UIStyle = FastReport.Utils.UIStyle.Office2007Black;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.txtDisplayMonth);
            this.panel1.Controls.Add(this.showBtn);
            this.panel1.Controls.Add(this.Close1Btn);
            this.panel1.Controls.Add(this.lblProdmonth);
            this.panel1.Controls.Add(this.ProdMonthTxt);
            this.panel1.Controls.Add(this.ProdMonth1Txt);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(992, 103);
            this.panel1.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbDev);
            this.groupBox1.Controls.Add(this.rdbStoping);
            this.groupBox1.Location = new System.Drawing.Point(298, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(202, 47);
            this.groupBox1.TabIndex = 109;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Activity";
            // 
            // rdbDev
            // 
            this.rdbDev.AutoSize = true;
            this.rdbDev.Location = new System.Drawing.Point(99, 20);
            this.rdbDev.Name = "rdbDev";
            this.rdbDev.Size = new System.Drawing.Size(88, 17);
            this.rdbDev.TabIndex = 1;
            this.rdbDev.Text = "Development";
            this.rdbDev.UseVisualStyleBackColor = true;
            // 
            // rdbStoping
            // 
            this.rdbStoping.AutoSize = true;
            this.rdbStoping.Checked = true;
            this.rdbStoping.Location = new System.Drawing.Point(17, 20);
            this.rdbStoping.Name = "rdbStoping";
            this.rdbStoping.Size = new System.Drawing.Size(61, 17);
            this.rdbStoping.TabIndex = 0;
            this.rdbStoping.TabStop = true;
            this.rdbStoping.Text = "Stoping";
            this.rdbStoping.UseVisualStyleBackColor = true;
            // 
            // txtDisplayMonth
            // 
            this.txtDisplayMonth.Location = new System.Drawing.Point(136, 76);
            this.txtDisplayMonth.Name = "txtDisplayMonth";
            this.txtDisplayMonth.Size = new System.Drawing.Size(100, 20);
            this.txtDisplayMonth.TabIndex = 108;
            this.txtDisplayMonth.Visible = false;
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
            this.showBtn.Location = new System.Drawing.Point(643, 22);
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
            this.Close1Btn.Location = new System.Drawing.Point(829, 22);
            this.Close1Btn.Name = "Close1Btn";
            this.Close1Btn.Size = new System.Drawing.Size(136, 48);
            this.Close1Btn.TabIndex = 106;
            this.Close1Btn.Text = "     Close                ";
            this.Close1Btn.Click += new System.EventHandler(this.Close1Btn_Click);
            // 
            // lblProdmonth
            // 
            this.lblProdmonth.AutoSize = true;
            this.lblProdmonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProdmonth.Location = new System.Drawing.Point(48, 41);
            this.lblProdmonth.Name = "lblProdmonth";
            this.lblProdmonth.Size = new System.Drawing.Size(68, 15);
            this.lblProdmonth.TabIndex = 99;
            this.lblProdmonth.Text = "ProdMonth";
            // 
            // ProdMonthTxt
            // 
            this.ProdMonthTxt.Location = new System.Drawing.Point(218, 37);
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
            this.ProdMonth1Txt.Location = new System.Drawing.Point(136, 37);
            this.ProdMonth1Txt.MaxLength = 10000000;
            this.ProdMonth1Txt.Name = "ProdMonth1Txt";
            this.ProdMonth1Txt.ReadOnly = true;
            this.ProdMonth1Txt.Size = new System.Drawing.Size(100, 20);
            this.ProdMonth1Txt.TabIndex = 97;
            this.ProdMonth1Txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // frmMonthlySummariesRep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 502);
            this.Controls.Add(this.pcReport);
            this.Controls.Add(this.panel1);
            this.Name = "frmMonthlySummariesRep";
            
            this.Text = "Monthl Summaries";
           
            this.Load += new System.EventHandler(this.frmMonthlySummariesRep_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProdMonthTxt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FastReport.Preview.PreviewControl pcReport;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton showBtn;
        private DevExpress.XtraEditors.SimpleButton Close1Btn;
        private System.Windows.Forms.Label lblProdmonth;
        private System.Windows.Forms.NumericUpDown ProdMonthTxt;
        private System.Windows.Forms.TextBox ProdMonth1Txt;
        private System.Windows.Forms.TextBox txtDisplayMonth;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbDev;
        private System.Windows.Forms.RadioButton rdbStoping;
    }
}