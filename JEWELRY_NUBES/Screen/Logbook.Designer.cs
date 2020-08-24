namespace JEWELRY_NUBES.Screen
{
    partial class Logbook
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.LAY_SEARCH = new System.Windows.Forms.TableLayoutPanel();
            this.cmbbox_Page = new System.Windows.Forms.ComboBox();
            this.BTN_SEARCH = new MetroFramework.Controls.MetroButton();
            this.BTN_PRINT = new MetroFramework.Controls.MetroButton();
            this.BTN_EXCEL = new MetroFramework.Controls.MetroButton();
            this.txt_EndDate = new MetroFramework.Controls.MetroDateTime();
            this.txt_StartDate = new MetroFramework.Controls.MetroDateTime();
            this.lbl_Delimiter = new MetroFramework.Controls.MetroLabel();
            this.lbl_DateOfIssue = new MetroFramework.Controls.MetroLabel();
            this.COM_STORE = new System.Windows.Forms.ComboBox();
            this.TIL_1 = new MetroFramework.Controls.MetroTile();
            this.GRD_LOG = new MetroFramework.Controls.MetroGrid();
            this.TIL_2 = new MetroFramework.Controls.MetroTile();
            this.DateOfInvoice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvoiceNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GstCharged = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DocID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateOfIssue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeOfIssue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PassportNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameStaff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isTarget = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Packed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SeqNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LAY_SEARCH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GRD_LOG)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("Calibri", 13.8F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.Location = new System.Drawing.Point(13, 0);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(81, 23);
            this.lbl_Title.TabIndex = 110;
            this.lbl_Title.Text = "Log book";
            // 
            // LAY_SEARCH
            // 
            this.LAY_SEARCH.ColumnCount = 10;
            this.LAY_SEARCH.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.LAY_SEARCH.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LAY_SEARCH.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 240F));
            this.LAY_SEARCH.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.LAY_SEARCH.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.LAY_SEARCH.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.LAY_SEARCH.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.LAY_SEARCH.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.LAY_SEARCH.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.LAY_SEARCH.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.LAY_SEARCH.Controls.Add(this.cmbbox_Page, 0, 0);
            this.LAY_SEARCH.Controls.Add(this.BTN_SEARCH, 7, 0);
            this.LAY_SEARCH.Controls.Add(this.BTN_PRINT, 8, 0);
            this.LAY_SEARCH.Controls.Add(this.BTN_EXCEL, 9, 0);
            this.LAY_SEARCH.Controls.Add(this.txt_EndDate, 6, 0);
            this.LAY_SEARCH.Controls.Add(this.txt_StartDate, 4, 0);
            this.LAY_SEARCH.Controls.Add(this.lbl_Delimiter, 5, 0);
            this.LAY_SEARCH.Controls.Add(this.lbl_DateOfIssue, 3, 0);
            this.LAY_SEARCH.Controls.Add(this.COM_STORE, 2, 0);
            this.LAY_SEARCH.Location = new System.Drawing.Point(17, 45);
            this.LAY_SEARCH.Name = "LAY_SEARCH";
            this.LAY_SEARCH.RowCount = 1;
            this.LAY_SEARCH.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LAY_SEARCH.Size = new System.Drawing.Size(759, 35);
            this.LAY_SEARCH.TabIndex = 111;
            // 
            // cmbbox_Page
            // 
            this.cmbbox_Page.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbbox_Page.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbox_Page.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbbox_Page.FormattingEnabled = true;
            this.cmbbox_Page.Location = new System.Drawing.Point(3, 5);
            this.cmbbox_Page.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbbox_Page.Name = "cmbbox_Page";
            this.cmbbox_Page.Size = new System.Drawing.Size(54, 25);
            this.cmbbox_Page.TabIndex = 114;
            this.cmbbox_Page.SelectedIndexChanged += new System.EventHandler(this.cmbbox_Page_SelectedIndexChanged);
            // 
            // BTN_SEARCH
            // 
            this.BTN_SEARCH.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.BTN_SEARCH.Location = new System.Drawing.Point(402, 3);
            this.BTN_SEARCH.Name = "BTN_SEARCH";
            this.BTN_SEARCH.Size = new System.Drawing.Size(112, 28);
            this.BTN_SEARCH.TabIndex = 0;
            this.BTN_SEARCH.Text = "Search";
            this.BTN_SEARCH.UseSelectable = true;
            this.BTN_SEARCH.Click += new System.EventHandler(this.BTN_SEARCH_Click);
            // 
            // BTN_PRINT
            // 
            this.BTN_PRINT.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.BTN_PRINT.Location = new System.Drawing.Point(522, 3);
            this.BTN_PRINT.Name = "BTN_PRINT";
            this.BTN_PRINT.Size = new System.Drawing.Size(112, 28);
            this.BTN_PRINT.TabIndex = 1;
            this.BTN_PRINT.Text = "Print";
            this.BTN_PRINT.UseSelectable = true;
            this.BTN_PRINT.Click += new System.EventHandler(this.BTN_PRINT_Click);
            // 
            // BTN_EXCEL
            // 
            this.BTN_EXCEL.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.BTN_EXCEL.Location = new System.Drawing.Point(642, 3);
            this.BTN_EXCEL.Name = "BTN_EXCEL";
            this.BTN_EXCEL.Size = new System.Drawing.Size(112, 28);
            this.BTN_EXCEL.TabIndex = 2;
            this.BTN_EXCEL.Text = "Excel";
            this.BTN_EXCEL.UseSelectable = true;
            this.BTN_EXCEL.Click += new System.EventHandler(this.BTN_EXCEL_Click);
            // 
            // txt_EndDate
            // 
            this.txt_EndDate.CustomFormat = "dd/MM/yyyy";
            this.txt_EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txt_EndDate.Location = new System.Drawing.Point(282, 3);
            this.txt_EndDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.txt_EndDate.Name = "txt_EndDate";
            this.txt_EndDate.Size = new System.Drawing.Size(114, 29);
            this.txt_EndDate.TabIndex = 117;
            // 
            // txt_StartDate
            // 
            this.txt_StartDate.CustomFormat = "dd/MM/yyyy";
            this.txt_StartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txt_StartDate.Location = new System.Drawing.Point(137, 3);
            this.txt_StartDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.txt_StartDate.Name = "txt_StartDate";
            this.txt_StartDate.Size = new System.Drawing.Size(114, 29);
            this.txt_StartDate.TabIndex = 115;
            // 
            // lbl_Delimiter
            // 
            this.lbl_Delimiter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Delimiter.Location = new System.Drawing.Point(257, 6);
            this.lbl_Delimiter.Name = "lbl_Delimiter";
            this.lbl_Delimiter.Size = new System.Drawing.Size(19, 23);
            this.lbl_Delimiter.TabIndex = 116;
            this.lbl_Delimiter.Text = "~";
            this.lbl_Delimiter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DateOfIssue
            // 
            this.lbl_DateOfIssue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_DateOfIssue.Location = new System.Drawing.Point(37, 6);
            this.lbl_DateOfIssue.Name = "lbl_DateOfIssue";
            this.lbl_DateOfIssue.Size = new System.Drawing.Size(94, 23);
            this.lbl_DateOfIssue.TabIndex = 118;
            this.lbl_DateOfIssue.Text = "Date of Issue : ";
            this.lbl_DateOfIssue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // COM_STORE
            // 
            this.COM_STORE.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.COM_STORE.FormattingEnabled = true;
            this.COM_STORE.ItemHeight = 12;
            this.COM_STORE.Location = new System.Drawing.Point(-149, 7);
            this.COM_STORE.Name = "COM_STORE";
            this.COM_STORE.Size = new System.Drawing.Size(180, 20);
            this.COM_STORE.TabIndex = 148;
            this.COM_STORE.Visible = false;
            this.COM_STORE.SelectedIndexChanged += new System.EventHandler(this.COM_STORE_SelectedIndexChanged);
            // 
            // TIL_1
            // 
            this.TIL_1.ActiveControl = null;
            this.TIL_1.Enabled = false;
            this.TIL_1.Location = new System.Drawing.Point(16, 34);
            this.TIL_1.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.TIL_1.Name = "TIL_1";
            this.TIL_1.Size = new System.Drawing.Size(766, 2);
            this.TIL_1.Style = MetroFramework.MetroColorStyle.Orange;
            this.TIL_1.TabIndex = 112;
            this.TIL_1.TabStop = false;
            this.TIL_1.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.TIL_1.UseSelectable = true;
            // 
            // GRD_LOG
            // 
            this.GRD_LOG.AllowUserToAddRows = false;
            this.GRD_LOG.AllowUserToDeleteRows = false;
            this.GRD_LOG.AllowUserToResizeRows = false;
            this.GRD_LOG.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GRD_LOG.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.GRD_LOG.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GRD_LOG.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GRD_LOG.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GRD_LOG.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(119)))), ((int)(((byte)(53)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(119)))), ((int)(((byte)(53)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GRD_LOG.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GRD_LOG.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GRD_LOG.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DateOfInvoice,
            this.InvoiceNumber,
            this.GstCharged,
            this.DocID,
            this.DateOfIssue,
            this.TimeOfIssue,
            this.PassportNumber,
            this.NameStaff,
            this.Status,
            this.isTarget,
            this.Packed,
            this.SeqNumber});
            this.GRD_LOG.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(119)))), ((int)(((byte)(53)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GRD_LOG.DefaultCellStyle = dataGridViewCellStyle9;
            this.GRD_LOG.EnableHeadersVisualStyles = false;
            this.GRD_LOG.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.GRD_LOG.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GRD_LOG.Location = new System.Drawing.Point(16, 96);
            this.GRD_LOG.Name = "GRD_LOG";
            this.GRD_LOG.ReadOnly = true;
            this.GRD_LOG.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GRD_LOG.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.GRD_LOG.RowHeadersVisible = false;
            this.GRD_LOG.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GRD_LOG.RowsDefaultCellStyle = dataGridViewCellStyle11;
            this.GRD_LOG.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GRD_LOG.RowTemplate.Height = 23;
            this.GRD_LOG.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GRD_LOG.Size = new System.Drawing.Size(765, 594);
            this.GRD_LOG.TabIndex = 113;
            // 
            // TIL_2
            // 
            this.TIL_2.ActiveControl = null;
            this.TIL_2.Enabled = false;
            this.TIL_2.Location = new System.Drawing.Point(17, 85);
            this.TIL_2.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.TIL_2.Name = "TIL_2";
            this.TIL_2.Size = new System.Drawing.Size(766, 2);
            this.TIL_2.Style = MetroFramework.MetroColorStyle.Orange;
            this.TIL_2.TabIndex = 114;
            this.TIL_2.TabStop = false;
            this.TIL_2.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.TIL_2.UseSelectable = true;
            // 
            // DateOfInvoice
            // 
            this.DateOfInvoice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DateOfInvoice.DefaultCellStyle = dataGridViewCellStyle2;
            this.DateOfInvoice.FillWeight = 50F;
            this.DateOfInvoice.HeaderText = "Invoice Date";
            this.DateOfInvoice.MinimumWidth = 100;
            this.DateOfInvoice.Name = "DateOfInvoice";
            this.DateOfInvoice.ReadOnly = true;
            this.DateOfInvoice.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DateOfInvoice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DateOfInvoice.ToolTipText = "Date of Invoice";
            // 
            // InvoiceNumber
            // 
            this.InvoiceNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.InvoiceNumber.FillWeight = 60F;
            this.InvoiceNumber.HeaderText = "Invoice No.";
            this.InvoiceNumber.MinimumWidth = 120;
            this.InvoiceNumber.Name = "InvoiceNumber";
            this.InvoiceNumber.ReadOnly = true;
            this.InvoiceNumber.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.InvoiceNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.InvoiceNumber.ToolTipText = "Invoice Number";
            this.InvoiceNumber.Width = 120;
            // 
            // GstCharged
            // 
            this.GstCharged.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.GstCharged.DefaultCellStyle = dataGridViewCellStyle3;
            this.GstCharged.FillWeight = 50F;
            this.GstCharged.HeaderText = "GST Charged";
            this.GstCharged.MinimumWidth = 120;
            this.GstCharged.Name = "GstCharged";
            this.GstCharged.ReadOnly = true;
            this.GstCharged.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.GstCharged.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GstCharged.ToolTipText = "GST Charged";
            this.GstCharged.Width = 120;
            // 
            // DocID
            // 
            this.DocID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DocID.FillWeight = 110F;
            this.DocID.HeaderText = "Doc ID";
            this.DocID.MinimumWidth = 185;
            this.DocID.Name = "DocID";
            this.DocID.ReadOnly = true;
            this.DocID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DocID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DocID.ToolTipText = "Document ID";
            this.DocID.Width = 185;
            // 
            // DateOfIssue
            // 
            this.DateOfIssue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DateOfIssue.DefaultCellStyle = dataGridViewCellStyle4;
            this.DateOfIssue.FillWeight = 50F;
            this.DateOfIssue.HeaderText = "Date of Issue";
            this.DateOfIssue.MinimumWidth = 100;
            this.DateOfIssue.Name = "DateOfIssue";
            this.DateOfIssue.ReadOnly = true;
            this.DateOfIssue.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DateOfIssue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DateOfIssue.ToolTipText = "Date of Issuing eTRS Ticket";
            // 
            // TimeOfIssue
            // 
            this.TimeOfIssue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.TimeOfIssue.DefaultCellStyle = dataGridViewCellStyle5;
            this.TimeOfIssue.FillWeight = 45F;
            this.TimeOfIssue.HeaderText = "Time of Issue";
            this.TimeOfIssue.MinimumWidth = 100;
            this.TimeOfIssue.Name = "TimeOfIssue";
            this.TimeOfIssue.ReadOnly = true;
            this.TimeOfIssue.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TimeOfIssue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TimeOfIssue.ToolTipText = "Time of Issuing eTRS ticket";
            // 
            // PassportNumber
            // 
            this.PassportNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PassportNumber.FillWeight = 35F;
            this.PassportNumber.HeaderText = "Passport No.";
            this.PassportNumber.MinimumWidth = 120;
            this.PassportNumber.Name = "PassportNumber";
            this.PassportNumber.ReadOnly = true;
            this.PassportNumber.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PassportNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PassportNumber.ToolTipText = "Passport number";
            this.PassportNumber.Width = 120;
            // 
            // NameStaff
            // 
            this.NameStaff.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.NameStaff.FillWeight = 45F;
            this.NameStaff.HeaderText = "Staff Name";
            this.NameStaff.MinimumWidth = 140;
            this.NameStaff.Name = "NameStaff";
            this.NameStaff.ReadOnly = true;
            this.NameStaff.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.NameStaff.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NameStaff.ToolTipText = "Name of Staff";
            this.NameStaff.Width = 140;
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Status.DefaultCellStyle = dataGridViewCellStyle6;
            this.Status.FillWeight = 30F;
            this.Status.HeaderText = "Status";
            this.Status.MinimumWidth = 100;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Status.ToolTipText = "Issue/Void";
            // 
            // isTarget
            // 
            this.isTarget.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.isTarget.DefaultCellStyle = dataGridViewCellStyle7;
            this.isTarget.FillWeight = 25F;
            this.isTarget.HeaderText = "Sales $7,000 & above?";
            this.isTarget.MinimumWidth = 120;
            this.isTarget.Name = "isTarget";
            this.isTarget.ReadOnly = true;
            this.isTarget.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.isTarget.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.isTarget.ToolTipText = "Total value of the sales made to the tourist under TRS for the preceding three (3" +
    ") months at the point of sales is $7,000 or more [Yes/No]";
            this.isTarget.Width = 120;
            // 
            // Packed
            // 
            this.Packed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Packed.DefaultCellStyle = dataGridViewCellStyle8;
            this.Packed.FillWeight = 25F;
            this.Packed.HeaderText = "Sealed Bag(Y/N)";
            this.Packed.MinimumWidth = 100;
            this.Packed.Name = "Packed";
            this.Packed.ReadOnly = true;
            this.Packed.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Packed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Packed.ToolTipText = "Are goods packed in sealed bag? [Yes/No]";
            // 
            // SeqNumber
            // 
            this.SeqNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SeqNumber.FillWeight = 45F;
            this.SeqNumber.HeaderText = "Sealed bag serial number";
            this.SeqNumber.MinimumWidth = 180;
            this.SeqNumber.Name = "SeqNumber";
            this.SeqNumber.ReadOnly = true;
            this.SeqNumber.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SeqNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SeqNumber.ToolTipText = "Serial number of the sealed bag (if applicable)";
            this.SeqNumber.Width = 180;
            // 
            // Logbook
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.TIL_2);
            this.Controls.Add(this.GRD_LOG);
            this.Controls.Add(this.TIL_1);
            this.Controls.Add(this.LAY_SEARCH);
            this.Controls.Add(this.lbl_Title);
            this.Name = "Logbook";
            this.Size = new System.Drawing.Size(841, 706);
            this.Load += new System.EventHandler(this.Logbook_Load);
            this.SizeChanged += new System.EventHandler(this.Logbook_SizeChanged);
            this.LAY_SEARCH.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GRD_LOG)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.TableLayoutPanel LAY_SEARCH;
        private MetroFramework.Controls.MetroButton BTN_SEARCH;
        private MetroFramework.Controls.MetroTile TIL_1;
        private MetroFramework.Controls.MetroGrid GRD_LOG;
        private MetroFramework.Controls.MetroTile TIL_2;
        private MetroFramework.Controls.MetroButton BTN_PRINT;
        private MetroFramework.Controls.MetroButton BTN_EXCEL;
        private MetroFramework.Controls.MetroDateTime txt_EndDate;
        private System.Windows.Forms.ComboBox cmbbox_Page;
        private MetroFramework.Controls.MetroDateTime txt_StartDate;
        private MetroFramework.Controls.MetroLabel lbl_Delimiter;
        private MetroFramework.Controls.MetroLabel lbl_DateOfIssue;
        private new System.Windows.Forms.ComboBox COM_STORE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateOfInvoice;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn GstCharged;
        private System.Windows.Forms.DataGridViewTextBoxColumn DocID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateOfIssue;
        private System.Windows.Forms.DataGridViewTextBoxColumn TimeOfIssue;
        private System.Windows.Forms.DataGridViewTextBoxColumn PassportNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameStaff;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn isTarget;
        private System.Windows.Forms.DataGridViewTextBoxColumn Packed;
        private System.Windows.Forms.DataGridViewTextBoxColumn SeqNumber;
    }
}
