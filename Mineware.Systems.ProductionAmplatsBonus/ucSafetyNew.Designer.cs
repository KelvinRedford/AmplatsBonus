namespace Mineware.Systems.ProductionAmplatsBonus
{
    //using DevComponents;

    partial class ucSafetyNew
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.ShowBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ProdMonthTxt = new System.Windows.Forms.NumericUpDown();
            this.ProdMonth1Txt = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtFatal = new DevExpress.XtraEditors.TextEdit();
            this.txtLti = new DevExpress.XtraEditors.TextEdit();
            this.txtRI = new DevExpress.XtraEditors.TextEdit();
            this.OrgGrid = new System.Windows.Forms.DataGridView();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.grid = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProdMonthTxt)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFatal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLti.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRI.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrgGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.CloseBtn);
            this.panel1.Controls.Add(this.ShowBtn);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.ProdMonthTxt);
            this.panel1.Controls.Add(this.ProdMonth1Txt);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(973, 83);
            this.panel1.TabIndex = 1;
            // 
            // CloseBtn
            // 
            this.CloseBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CloseBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CloseBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseBtn.Image = global::Mineware.Systems.ProductionAmplatsBonus.Properties.Resources.Close;
            this.CloseBtn.Location = new System.Drawing.Point(451, 16);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(138, 48);
            this.CloseBtn.TabIndex = 90;
            this.CloseBtn.Text = "   Close";
            this.CloseBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.CloseBtn.UseVisualStyleBackColor = false;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // ShowBtn
            // 
            this.ShowBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ShowBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ShowBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShowBtn.Image = global::Mineware.Systems.ProductionAmplatsBonus.Properties.Resources.Show;
            this.ShowBtn.Location = new System.Drawing.Point(256, 16);
            this.ShowBtn.Name = "ShowBtn";
            this.ShowBtn.Size = new System.Drawing.Size(138, 48);
            this.ShowBtn.TabIndex = 89;
            this.ShowBtn.Text = "   Show";
            this.ShowBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ShowBtn.UseVisualStyleBackColor = false;
            this.ShowBtn.Click += new System.EventHandler(this.ShowBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(25, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 15);
            this.label2.TabIndex = 88;
            this.label2.Text = "ProdMonth";
            // 
            // ProdMonthTxt
            // 
            this.ProdMonthTxt.Location = new System.Drawing.Point(109, 37);
            this.ProdMonthTxt.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.ProdMonthTxt.Name = "ProdMonthTxt";
            this.ProdMonthTxt.Size = new System.Drawing.Size(18, 20);
            this.ProdMonthTxt.TabIndex = 87;
            this.ProdMonthTxt.Click += new System.EventHandler(this.ProdMonthTxt_Click);
            // 
            // ProdMonth1Txt
            // 
            this.ProdMonth1Txt.BackColor = System.Drawing.Color.White;
            this.ProdMonth1Txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProdMonth1Txt.Location = new System.Drawing.Point(27, 37);
            this.ProdMonth1Txt.MaxLength = 10000000;
            this.ProdMonth1Txt.Name = "ProdMonth1Txt";
            this.ProdMonth1Txt.ReadOnly = true;
            this.ProdMonth1Txt.Size = new System.Drawing.Size(100, 20);
            this.ProdMonth1Txt.TabIndex = 86;
            this.ProdMonth1Txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtFatal);
            this.panel2.Controls.Add(this.txtLti);
            this.panel2.Controls.Add(this.txtRI);
            this.panel2.Controls.Add(this.OrgGrid);
            this.panel2.Controls.Add(this.SaveBtn);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtFilter);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 83);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(214, 499);
            this.panel2.TabIndex = 2;
            // 
            // txtFatal
            // 
            this.txtFatal.EditValue = 0;
            this.txtFatal.Location = new System.Drawing.Point(97, 272);
            this.txtFatal.Name = "txtFatal";
            this.txtFatal.Properties.Mask.EditMask = "n0";
            this.txtFatal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtFatal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtFatal.Size = new System.Drawing.Size(78, 20);
            this.txtFatal.TabIndex = 102;
            // 
            // txtLti
            // 
            this.txtLti.EditValue = 0;
            this.txtLti.Location = new System.Drawing.Point(97, 239);
            this.txtLti.Name = "txtLti";
            this.txtLti.Properties.Mask.EditMask = "n0";
            this.txtLti.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtLti.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtLti.Size = new System.Drawing.Size(78, 20);
            this.txtLti.TabIndex = 101;
            // 
            // txtRI
            // 
            this.txtRI.EditValue = 0;
            this.txtRI.Location = new System.Drawing.Point(97, 207);
            this.txtRI.Name = "txtRI";
            this.txtRI.Properties.Mask.EditMask = "n0";
            this.txtRI.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtRI.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtRI.Size = new System.Drawing.Size(78, 20);
            this.txtRI.TabIndex = 100;
            // 
            // OrgGrid
            // 
            this.OrgGrid.AllowUserToAddRows = false;
            this.OrgGrid.AllowUserToDeleteRows = false;
            this.OrgGrid.AllowUserToResizeColumns = false;
            this.OrgGrid.AllowUserToResizeRows = false;
            this.OrgGrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.OrgGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.OrgGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OrgGrid.Location = new System.Drawing.Point(27, 63);
            this.OrgGrid.Name = "OrgGrid";
            this.OrgGrid.ReadOnly = true;
            this.OrgGrid.RowHeadersVisible = false;
            this.OrgGrid.Size = new System.Drawing.Size(163, 125);
            this.OrgGrid.TabIndex = 99;
            // 
            // SaveBtn
            // 
            this.SaveBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.SaveBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.SaveBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveBtn.Image = global::Mineware.Systems.ProductionAmplatsBonus.Properties.Resources.Save;
            this.SaveBtn.Location = new System.Drawing.Point(27, 330);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(138, 48);
            this.SaveBtn.TabIndex = 97;
            this.SaveBtn.Text = "   Save";
            this.SaveBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SaveBtn.UseVisualStyleBackColor = false;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(25, 276);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 15);
            this.label5.TabIndex = 93;
            this.label5.Text = "Fatals";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(25, 243);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 15);
            this.label4.TabIndex = 92;
            this.label4.Text = "LTI";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(25, 211);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 15);
            this.label3.TabIndex = 91;
            this.label3.Text = "RI";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 15);
            this.label1.TabIndex = 89;
            this.label1.Text = "Filter";
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(27, 32);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(100, 20);
            this.txtFilter.TabIndex = 0;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
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
            this.grid.Location = new System.Drawing.Point(214, 83);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(759, 499);
            this.grid.TabIndex = 3;
            // 
            // frmSafetyNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 582);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmSafetyNew";
          
            this.Text = "frmSafety";
            this.Load += new System.EventHandler(this.frmSafetyNew_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProdMonthTxt)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFatal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLti.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRI.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrgGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion



        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CloseBtn;
        private System.Windows.Forms.Button ShowBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown ProdMonthTxt;
        private System.Windows.Forms.TextBox ProdMonth1Txt;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridView OrgGrid;
        private DevExpress.XtraEditors.TextEdit txtFatal;
        private DevExpress.XtraEditors.TextEdit txtLti;
        private DevExpress.XtraEditors.TextEdit txtRI;
    }
}