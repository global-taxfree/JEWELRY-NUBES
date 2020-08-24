namespace JEWELRY_NUBES
{
    partial class CardForm
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
            this.BTN_OK = new MetroFramework.Controls.MetroButton();
            this.TIL_1 = new MetroFramework.Controls.MetroTile();
            this.LAY_CARD = new System.Windows.Forms.TableLayoutPanel();
            this.txt_DisplayCardNo = new System.Windows.Forms.TextBox();
            this.txt_CardNo = new System.Windows.Forms.TextBox();
            this.txt_Track2 = new System.Windows.Forms.TextBox();
            this.LAY_CARD.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN_OK
            // 
            this.BTN_OK.Location = new System.Drawing.Point(112, 132);
            this.BTN_OK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BTN_OK.Name = "BTN_OK";
            this.BTN_OK.Size = new System.Drawing.Size(121, 36);
            this.BTN_OK.TabIndex = 7;
            this.BTN_OK.Text = "Ok";
            this.BTN_OK.UseSelectable = true;
            this.BTN_OK.Click += new System.EventHandler(this.BTN_OK_Click);
            // 
            // TIL_1
            // 
            this.TIL_1.ActiveControl = null;
            this.TIL_1.Enabled = false;
            this.TIL_1.Location = new System.Drawing.Point(19, 58);
            this.TIL_1.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.TIL_1.Name = "TIL_1";
            this.TIL_1.Size = new System.Drawing.Size(307, 2);
            this.TIL_1.Style = MetroFramework.MetroColorStyle.Orange;
            this.TIL_1.TabIndex = 103;
            this.TIL_1.TabStop = false;
            this.TIL_1.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.TIL_1.UseSelectable = true;
            // 
            // LAY_CARD
            // 
            this.LAY_CARD.ColumnCount = 3;
            this.LAY_CARD.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 209F));
            this.LAY_CARD.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.LAY_CARD.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 346F));
            this.LAY_CARD.Controls.Add(this.txt_DisplayCardNo, 0, 0);
            this.LAY_CARD.Controls.Add(this.txt_CardNo, 2, 0);
            this.LAY_CARD.Controls.Add(this.txt_Track2, 1, 0);
            this.LAY_CARD.Location = new System.Drawing.Point(70, 67);
            this.LAY_CARD.Name = "LAY_CARD";
            this.LAY_CARD.RowCount = 1;
            this.LAY_CARD.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LAY_CARD.Size = new System.Drawing.Size(268, 41);
            this.LAY_CARD.TabIndex = 105;
            // 
            // txt_DisplayCardNo
            // 
            this.txt_DisplayCardNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_DisplayCardNo.Location = new System.Drawing.Point(3, 8);
            this.txt_DisplayCardNo.Name = "txt_DisplayCardNo";
            this.txt_DisplayCardNo.ReadOnly = true;
            this.txt_DisplayCardNo.Size = new System.Drawing.Size(191, 24);
            this.txt_DisplayCardNo.TabIndex = 0;
            this.txt_DisplayCardNo.Click += new System.EventHandler(this.txt_DisplayCardNo_Click);
            // 
            // txt_CardNo
            // 
            this.txt_CardNo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_CardNo.Location = new System.Drawing.Point(242, 8);
            this.txt_CardNo.Name = "txt_CardNo";
            this.txt_CardNo.ReadOnly = true;
            this.txt_CardNo.Size = new System.Drawing.Size(16, 24);
            this.txt_CardNo.TabIndex = 1;
            this.txt_CardNo.Visible = false;
            // 
            // txt_Track2
            // 
            this.txt_Track2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt_Track2.Location = new System.Drawing.Point(223, 8);
            this.txt_Track2.Name = "txt_Track2";
            this.txt_Track2.Size = new System.Drawing.Size(1, 24);
            this.txt_Track2.TabIndex = 2;
            this.txt_Track2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_Track2_KeyUp);
            // 
            // CardForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(345, 181);
            this.Controls.Add(this.LAY_CARD);
            this.Controls.Add(this.TIL_1);
            this.Controls.Add(this.BTN_OK);
            this.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CardForm";
            this.Padding = new System.Windows.Forms.Padding(26, 105, 26, 35);
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "Read Card Number";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CardForm_FormClosing);
            this.Load += new System.EventHandler(this.CardForm_Load);
            this.Shown += new System.EventHandler(this.CardForm_Shown);
            this.LAY_CARD.ResumeLayout(false);
            this.LAY_CARD.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroButton BTN_OK;
        private MetroFramework.Controls.MetroTile TIL_1;
        private System.Windows.Forms.TableLayoutPanel LAY_CARD;
        private System.Windows.Forms.TextBox txt_DisplayCardNo;
        private System.Windows.Forms.TextBox txt_CardNo;
        private System.Windows.Forms.TextBox txt_Track2;
    }
}