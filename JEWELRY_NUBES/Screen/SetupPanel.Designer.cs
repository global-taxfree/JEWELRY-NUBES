namespace JEWELRY_NUBES.Screen
{
    partial class PreferencesPanel
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.COM_PASS_SCAN = new MetroFramework.Controls.MetroComboBox();
            this.BTN_HELP = new MetroFramework.Controls.MetroButton();
            this.BTN_DOWNLOAD = new MetroFramework.Controls.MetroButton();
            this.BTN_SCAN_TEST = new MetroFramework.Controls.MetroButton();
            this.LAY_CONFIG = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_Passport = new MetroFramework.Controls.MetroLabel();
            this.lbl_OPOSPrinter = new MetroFramework.Controls.MetroLabel();
            this.COM_OPOS = new MetroFramework.Controls.MetroComboBox();
            this.lbl_Printer = new MetroFramework.Controls.MetroLabel();
            this.COM_PRINTER = new MetroFramework.Controls.MetroComboBox();
            this.lbl_Printsetting = new MetroFramework.Controls.MetroLabel();
            this.COM_PRINT_SELECT = new MetroFramework.Controls.MetroComboBox();
            this.BTN_SAVE = new MetroFramework.Controls.MetroButton();
            this.BTN_OPOS_TEST = new MetroFramework.Controls.MetroButton();
            this.BTN_PRINT_TEST = new MetroFramework.Controls.MetroButton();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.TIL_1 = new MetroFramework.Controls.MetroTile();
            this.TIL_2 = new MetroFramework.Controls.MetroTile();
            this.LAY_CONFIG.SuspendLayout();
            this.SuspendLayout();
            // 
            // COM_PASS_SCAN
            // 
            this.COM_PASS_SCAN.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LAY_CONFIG.SetColumnSpan(this.COM_PASS_SCAN, 2);
            this.COM_PASS_SCAN.FormattingEnabled = true;
            this.COM_PASS_SCAN.ItemHeight = 23;
            this.COM_PASS_SCAN.Items.AddRange(new object[] {
            "GTF",
            "WISESCAN420",
            "DAWIN(GTF SG)",
            "OKPOS"});
            this.COM_PASS_SCAN.Location = new System.Drawing.Point(161, 3);
            this.COM_PASS_SCAN.Name = "COM_PASS_SCAN";
            this.COM_PASS_SCAN.Size = new System.Drawing.Size(245, 29);
            this.COM_PASS_SCAN.TabIndex = 0;
            this.COM_PASS_SCAN.UseSelectable = true;
            // 
            // BTN_HELP
            // 
            this.BTN_HELP.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BTN_HELP.Enabled = false;
            this.BTN_HELP.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.BTN_HELP.Location = new System.Drawing.Point(591, 78);
            this.BTN_HELP.Name = "BTN_HELP";
            this.BTN_HELP.Size = new System.Drawing.Size(106, 23);
            this.BTN_HELP.TabIndex = 3;
            this.BTN_HELP.Text = "Help";
            this.BTN_HELP.UseSelectable = true;
            this.BTN_HELP.Visible = false;
            // 
            // BTN_DOWNLOAD
            // 
            this.BTN_DOWNLOAD.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BTN_DOWNLOAD.Enabled = false;
            this.BTN_DOWNLOAD.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.BTN_DOWNLOAD.Location = new System.Drawing.Point(591, 42);
            this.BTN_DOWNLOAD.Name = "BTN_DOWNLOAD";
            this.BTN_DOWNLOAD.Size = new System.Drawing.Size(106, 23);
            this.BTN_DOWNLOAD.TabIndex = 2;
            this.BTN_DOWNLOAD.Text = "Download";
            this.BTN_DOWNLOAD.UseSelectable = true;
            this.BTN_DOWNLOAD.Visible = false;
            // 
            // BTN_SCAN_TEST
            // 
            this.BTN_SCAN_TEST.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BTN_SCAN_TEST.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.BTN_SCAN_TEST.Location = new System.Drawing.Point(418, 6);
            this.BTN_SCAN_TEST.Name = "BTN_SCAN_TEST";
            this.BTN_SCAN_TEST.Size = new System.Drawing.Size(116, 23);
            this.BTN_SCAN_TEST.TabIndex = 5;
            this.BTN_SCAN_TEST.Text = "Scan Test";
            this.BTN_SCAN_TEST.UseSelectable = true;
            this.BTN_SCAN_TEST.Click += new System.EventHandler(this.BTN_SCAN_TEST_Click);
            // 
            // LAY_CONFIG
            // 
            this.LAY_CONFIG.ColumnCount = 5;
            this.LAY_CONFIG.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 158F));
            this.LAY_CONFIG.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 148F));
            this.LAY_CONFIG.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.LAY_CONFIG.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.LAY_CONFIG.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 163F));
            this.LAY_CONFIG.Controls.Add(this.lbl_Passport, 0, 0);
            this.LAY_CONFIG.Controls.Add(this.COM_PASS_SCAN, 1, 0);
            this.LAY_CONFIG.Controls.Add(this.lbl_OPOSPrinter, 0, 3);
            this.LAY_CONFIG.Controls.Add(this.COM_OPOS, 1, 3);
            this.LAY_CONFIG.Controls.Add(this.lbl_Printer, 0, 2);
            this.LAY_CONFIG.Controls.Add(this.COM_PRINTER, 1, 2);
            this.LAY_CONFIG.Controls.Add(this.lbl_Printsetting, 0, 1);
            this.LAY_CONFIG.Controls.Add(this.COM_PRINT_SELECT, 1, 1);
            this.LAY_CONFIG.Controls.Add(this.BTN_HELP, 4, 2);
            this.LAY_CONFIG.Controls.Add(this.BTN_DOWNLOAD, 4, 1);
            this.LAY_CONFIG.Controls.Add(this.BTN_SAVE, 4, 0);
            this.LAY_CONFIG.Controls.Add(this.BTN_SCAN_TEST, 3, 0);
            this.LAY_CONFIG.Controls.Add(this.BTN_OPOS_TEST, 3, 3);
            this.LAY_CONFIG.Controls.Add(this.BTN_PRINT_TEST, 3, 2);
            this.LAY_CONFIG.Location = new System.Drawing.Point(11, 46);
            this.LAY_CONFIG.Name = "LAY_CONFIG";
            this.LAY_CONFIG.RowCount = 4;
            this.LAY_CONFIG.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.LAY_CONFIG.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.LAY_CONFIG.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.LAY_CONFIG.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.LAY_CONFIG.Size = new System.Drawing.Size(700, 144);
            this.LAY_CONFIG.TabIndex = 0;
            // 
            // lbl_Passport
            // 
            this.lbl_Passport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Passport.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lbl_Passport.Location = new System.Drawing.Point(3, 6);
            this.lbl_Passport.Name = "lbl_Passport";
            this.lbl_Passport.Size = new System.Drawing.Size(152, 23);
            this.lbl_Passport.TabIndex = 101;
            this.lbl_Passport.Text = "Passport Scanner :";
            this.lbl_Passport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_OPOSPrinter
            // 
            this.lbl_OPOSPrinter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_OPOSPrinter.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lbl_OPOSPrinter.Location = new System.Drawing.Point(3, 114);
            this.lbl_OPOSPrinter.Name = "lbl_OPOSPrinter";
            this.lbl_OPOSPrinter.Size = new System.Drawing.Size(152, 23);
            this.lbl_OPOSPrinter.TabIndex = 103;
            this.lbl_OPOSPrinter.Text = "OPOS Printer :";
            this.lbl_OPOSPrinter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // COM_OPOS
            // 
            this.COM_OPOS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LAY_CONFIG.SetColumnSpan(this.COM_OPOS, 2);
            this.COM_OPOS.FormattingEnabled = true;
            this.COM_OPOS.ItemHeight = 23;
            this.COM_OPOS.Items.AddRange(new object[] {
            "SRP-350III",
            "SRP-350plusII",
            "SRP-350plusIII"});
            this.COM_OPOS.Location = new System.Drawing.Point(161, 111);
            this.COM_OPOS.Name = "COM_OPOS";
            this.COM_OPOS.Size = new System.Drawing.Size(245, 29);
            this.COM_OPOS.TabIndex = 3;
            this.COM_OPOS.UseSelectable = true;
            // 
            // lbl_Printer
            // 
            this.lbl_Printer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Printer.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lbl_Printer.Location = new System.Drawing.Point(3, 78);
            this.lbl_Printer.Name = "lbl_Printer";
            this.lbl_Printer.Size = new System.Drawing.Size(152, 23);
            this.lbl_Printer.TabIndex = 104;
            this.lbl_Printer.Text = "Windows Printer :";
            this.lbl_Printer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // COM_PRINTER
            // 
            this.COM_PRINTER.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LAY_CONFIG.SetColumnSpan(this.COM_PRINTER, 2);
            this.COM_PRINTER.FormattingEnabled = true;
            this.COM_PRINTER.ItemHeight = 23;
            this.COM_PRINTER.Items.AddRange(new object[] {
            "SRP-350III",
            "SRP-350plusII",
            "SRP-350plusIII"});
            this.COM_PRINTER.Location = new System.Drawing.Point(161, 75);
            this.COM_PRINTER.Name = "COM_PRINTER";
            this.COM_PRINTER.Size = new System.Drawing.Size(245, 29);
            this.COM_PRINTER.TabIndex = 2;
            this.COM_PRINTER.UseSelectable = true;
            // 
            // lbl_Printsetting
            // 
            this.lbl_Printsetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Printsetting.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.lbl_Printsetting.Location = new System.Drawing.Point(3, 42);
            this.lbl_Printsetting.Name = "lbl_Printsetting";
            this.lbl_Printsetting.Size = new System.Drawing.Size(152, 23);
            this.lbl_Printsetting.TabIndex = 107;
            this.lbl_Printsetting.Text = "Printer Selct :";
            this.lbl_Printsetting.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // COM_PRINT_SELECT
            // 
            this.COM_PRINT_SELECT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LAY_CONFIG.SetColumnSpan(this.COM_PRINT_SELECT, 2);
            this.COM_PRINT_SELECT.FormattingEnabled = true;
            this.COM_PRINT_SELECT.ItemHeight = 23;
            this.COM_PRINT_SELECT.Items.AddRange(new object[] {
            "Windows Printer",
            "OPOS Printer"});
            this.COM_PRINT_SELECT.Location = new System.Drawing.Point(161, 39);
            this.COM_PRINT_SELECT.Name = "COM_PRINT_SELECT";
            this.COM_PRINT_SELECT.Size = new System.Drawing.Size(245, 29);
            this.COM_PRINT_SELECT.TabIndex = 1;
            this.COM_PRINT_SELECT.UseSelectable = true;
            this.COM_PRINT_SELECT.SelectedIndexChanged += new System.EventHandler(this.COM_PRINT_SELECT_SelectedIndexChanged);
            // 
            // BTN_SAVE
            // 
            this.BTN_SAVE.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BTN_SAVE.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.BTN_SAVE.Location = new System.Drawing.Point(591, 6);
            this.BTN_SAVE.Name = "BTN_SAVE";
            this.BTN_SAVE.Size = new System.Drawing.Size(106, 23);
            this.BTN_SAVE.TabIndex = 4;
            this.BTN_SAVE.Text = "Save";
            this.BTN_SAVE.UseSelectable = true;
            this.BTN_SAVE.Click += new System.EventHandler(this.BTN_SAVE_Click);
            // 
            // BTN_OPOS_TEST
            // 
            this.BTN_OPOS_TEST.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BTN_OPOS_TEST.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.BTN_OPOS_TEST.Location = new System.Drawing.Point(418, 114);
            this.BTN_OPOS_TEST.Name = "BTN_OPOS_TEST";
            this.BTN_OPOS_TEST.Size = new System.Drawing.Size(116, 23);
            this.BTN_OPOS_TEST.TabIndex = 7;
            this.BTN_OPOS_TEST.Text = "OPOS Print Test";
            this.BTN_OPOS_TEST.UseSelectable = true;
            this.BTN_OPOS_TEST.Click += new System.EventHandler(this.BTN_OPOS_TEST_Click);
            // 
            // BTN_PRINT_TEST
            // 
            this.BTN_PRINT_TEST.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BTN_PRINT_TEST.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.BTN_PRINT_TEST.Location = new System.Drawing.Point(418, 78);
            this.BTN_PRINT_TEST.Name = "BTN_PRINT_TEST";
            this.BTN_PRINT_TEST.Size = new System.Drawing.Size(116, 23);
            this.BTN_PRINT_TEST.TabIndex = 6;
            this.BTN_PRINT_TEST.Text = "Print Test";
            this.BTN_PRINT_TEST.UseSelectable = true;
            this.BTN_PRINT_TEST.Click += new System.EventHandler(this.BTN_PRINT_TEST_Click);
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("Calibri", 13.8F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.Location = new System.Drawing.Point(13, 0);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(125, 23);
            this.lbl_Title.TabIndex = 110;
            this.lbl_Title.Text = "System Setting";
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
            this.TIL_1.TabIndex = 111;
            this.TIL_1.TabStop = false;
            this.TIL_1.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.TIL_1.UseSelectable = true;
            // 
            // TIL_2
            // 
            this.TIL_2.ActiveControl = null;
            this.TIL_2.Enabled = false;
            this.TIL_2.Location = new System.Drawing.Point(11, 199);
            this.TIL_2.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.TIL_2.Name = "TIL_2";
            this.TIL_2.Size = new System.Drawing.Size(766, 2);
            this.TIL_2.Style = MetroFramework.MetroColorStyle.Orange;
            this.TIL_2.TabIndex = 112;
            this.TIL_2.TabStop = false;
            this.TIL_2.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.TIL_2.UseSelectable = true;
            // 
            // PreferencesPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.TIL_2);
            this.Controls.Add(this.TIL_1);
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.LAY_CONFIG);
            this.Name = "PreferencesPanel";
            this.Size = new System.Drawing.Size(841, 706);
            this.Load += new System.EventHandler(this.PreferencesPanel_Load);
            this.SizeChanged += new System.EventHandler(this.PreferencesPanel_SizeChanged);
            this.LAY_CONFIG.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroComboBox COM_PASS_SCAN;
        private MetroFramework.Controls.MetroButton BTN_HELP;
        private MetroFramework.Controls.MetroButton BTN_DOWNLOAD;
        private MetroFramework.Controls.MetroButton BTN_SCAN_TEST;
        private System.Windows.Forms.TableLayoutPanel LAY_CONFIG;
        private MetroFramework.Controls.MetroButton BTN_SAVE;
        private MetroFramework.Controls.MetroButton BTN_OPOS_TEST;
        private MetroFramework.Controls.MetroComboBox COM_OPOS;
        private MetroFramework.Controls.MetroLabel lbl_Passport;
        private MetroFramework.Controls.MetroLabel lbl_OPOSPrinter;
        private System.Windows.Forms.Label lbl_Title;
        private MetroFramework.Controls.MetroTile TIL_1;
        private MetroFramework.Controls.MetroTile TIL_2;
        private MetroFramework.Controls.MetroLabel lbl_Printer;
        private MetroFramework.Controls.MetroComboBox COM_PRINTER;
        private MetroFramework.Controls.MetroButton BTN_PRINT_TEST;
        private MetroFramework.Controls.MetroLabel lbl_Printsetting;
        private MetroFramework.Controls.MetroComboBox COM_PRINT_SELECT;
    }
}
