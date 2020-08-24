namespace JEWELRY_NUBES.Screen
{
    partial class RefundCalculator
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
            this.TIL_1 = new MetroFramework.Controls.MetroTile();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.txt_GSTAmt = new System.Windows.Forms.TextBox();
            this.txt_totalAmt = new System.Windows.Forms.TextBox();
            this.lbl_buyAmt = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.txt_RefundAmt = new System.Windows.Forms.TextBox();
            this.TIL_2 = new MetroFramework.Controls.MetroTile();
            this.BTN_CLEAR = new MetroFramework.Controls.MetroButton();
            this.btn_calculrate = new MetroFramework.Controls.MetroButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TIL_1
            // 
            this.TIL_1.ActiveControl = null;
            this.TIL_1.Enabled = false;
            this.TIL_1.Location = new System.Drawing.Point(10, 34);
            this.TIL_1.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.TIL_1.Name = "TIL_1";
            this.TIL_1.Size = new System.Drawing.Size(766, 2);
            this.TIL_1.Style = MetroFramework.MetroColorStyle.Orange;
            this.TIL_1.TabIndex = 112;
            this.TIL_1.TabStop = false;
            this.TIL_1.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.TIL_1.UseSelectable = true;
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("Calibri", 13.8F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.Location = new System.Drawing.Point(13, 0);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(152, 23);
            this.lbl_Title.TabIndex = 113;
            this.lbl_Title.Text = "Refund Calculator";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.14286F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.85714F));
            this.tableLayoutPanel1.Controls.Add(this.metroLabel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txt_GSTAmt, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txt_totalAmt, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_buyAmt, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.metroLabel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txt_RefundAmt, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(11, 46);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(700, 140);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // metroLabel2
            // 
            this.metroLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel2.Location = new System.Drawing.Point(3, 104);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(128, 23);
            this.metroLabel2.TabIndex = 118;
            this.metroLabel2.Text = "Refund Amount :";
            this.metroLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_GSTAmt
            // 
            this.txt_GSTAmt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_GSTAmt.Enabled = false;
            this.txt_GSTAmt.Font = new System.Drawing.Font("Gulim", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_GSTAmt.Location = new System.Drawing.Point(137, 49);
            this.txt_GSTAmt.Name = "txt_GSTAmt";
            this.txt_GSTAmt.Size = new System.Drawing.Size(560, 41);
            this.txt_GSTAmt.TabIndex = 117;
            this.txt_GSTAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_GSTAmt.Visible = false;
            // 
            // txt_totalAmt
            // 
            this.txt_totalAmt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_totalAmt.Font = new System.Drawing.Font("Gulim", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_totalAmt.Location = new System.Drawing.Point(137, 3);
            this.txt_totalAmt.MaxLength = 11;
            this.txt_totalAmt.Name = "txt_totalAmt";
            this.txt_totalAmt.Size = new System.Drawing.Size(560, 41);
            this.txt_totalAmt.TabIndex = 0;
            this.txt_totalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_totalAmt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_totalAmt_KeyPress);
            // 
            // lbl_buyAmt
            // 
            this.lbl_buyAmt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_buyAmt.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lbl_buyAmt.Location = new System.Drawing.Point(3, 11);
            this.lbl_buyAmt.Name = "lbl_buyAmt";
            this.lbl_buyAmt.Size = new System.Drawing.Size(128, 23);
            this.lbl_buyAmt.TabIndex = 115;
            this.lbl_buyAmt.Text = "Total Amount  :";
            this.lbl_buyAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // metroLabel1
            // 
            this.metroLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel1.Location = new System.Drawing.Point(3, 57);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(128, 23);
            this.metroLabel1.TabIndex = 116;
            this.metroLabel1.Text = "GST Amount :";
            this.metroLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.metroLabel1.Visible = false;
            // 
            // txt_RefundAmt
            // 
            this.txt_RefundAmt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_RefundAmt.Enabled = false;
            this.txt_RefundAmt.Font = new System.Drawing.Font("Gulim", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txt_RefundAmt.Location = new System.Drawing.Point(137, 95);
            this.txt_RefundAmt.Name = "txt_RefundAmt";
            this.txt_RefundAmt.Size = new System.Drawing.Size(560, 41);
            this.txt_RefundAmt.TabIndex = 119;
            this.txt_RefundAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TIL_2
            // 
            this.TIL_2.ActiveControl = null;
            this.TIL_2.Enabled = false;
            this.TIL_2.Location = new System.Drawing.Point(11, 236);
            this.TIL_2.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.TIL_2.Name = "TIL_2";
            this.TIL_2.Size = new System.Drawing.Size(766, 2);
            this.TIL_2.Style = MetroFramework.MetroColorStyle.Orange;
            this.TIL_2.TabIndex = 115;
            this.TIL_2.TabStop = false;
            this.TIL_2.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.TIL_2.UseSelectable = true;
            // 
            // BTN_CLEAR
            // 
            this.BTN_CLEAR.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BTN_CLEAR.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.BTN_CLEAR.Location = new System.Drawing.Point(369, 201);
            this.BTN_CLEAR.Name = "BTN_CLEAR";
            this.BTN_CLEAR.Size = new System.Drawing.Size(99, 26);
            this.BTN_CLEAR.TabIndex = 2;
            this.BTN_CLEAR.Text = "Reset";
            this.BTN_CLEAR.UseSelectable = true;
            this.BTN_CLEAR.Click += new System.EventHandler(this.BTN_CLEAR_Click);
            // 
            // btn_calculrate
            // 
            this.btn_calculrate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_calculrate.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btn_calculrate.Location = new System.Drawing.Point(206, 201);
            this.btn_calculrate.Name = "btn_calculrate";
            this.btn_calculrate.Size = new System.Drawing.Size(99, 26);
            this.btn_calculrate.TabIndex = 1;
            this.btn_calculrate.Text = "Calculate";
            this.btn_calculrate.UseSelectable = true;
            this.btn_calculrate.Click += new System.EventHandler(this.btn_calculrate_Click);
            // 
            // RefundCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.btn_calculrate);
            this.Controls.Add(this.BTN_CLEAR);
            this.Controls.Add(this.TIL_2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.TIL_1);
            this.Name = "RefundCalculator";
            this.Size = new System.Drawing.Size(788, 706);
            this.Load += new System.EventHandler(this.RefundCalculator_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTile TIL_1;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MetroFramework.Controls.MetroLabel lbl_buyAmt;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.TextBox txt_GSTAmt;
        private System.Windows.Forms.TextBox txt_totalAmt;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.TextBox txt_RefundAmt;
        private MetroFramework.Controls.MetroTile TIL_2;
        private MetroFramework.Controls.MetroButton BTN_CLEAR;
        private MetroFramework.Controls.MetroButton btn_calculrate;
    }
}
