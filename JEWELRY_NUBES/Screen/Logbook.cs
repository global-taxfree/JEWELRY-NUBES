using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JEWELRY_NUBES.Util;
using log4net;
using System.Drawing.Printing;
using MetroFramework;
using JEWELRY_NUBES.Tran;
using Newtonsoft.Json.Linq;
using MetroFramework.Controls;
using System.Drawing.Imaging;
using ZXing.Rendering;
using ZXing.Common;
using System.Resources;

namespace JEWELRY_NUBES.Screen
{
    public partial class Logbook : UserControl
    {
        ControlManager m_CtlSizeManager = null;

        StringFormat strFormat = new StringFormat();
        StringFormat strSubFormat = new StringFormat();
        StringFormat numFormat = new StringFormat();
        StringFormat leftFormat = new StringFormat();

        Font strFont = new Font("Arial", 9);
        Font strFontSub = new Font("Arial", 9);

        private EncodingOptions EncodingOptions { get; set; }
        private Type Renderer { get; set; }

        public Logbook(ILog Logger = null)
        {
            InitializeComponent();

            //최초생성시 좌표, 크기 조정여부 등록함. 화면별로 Manager 를 가진다. 
            m_CtlSizeManager = new ControlManager(this);
            //횡좌표이동
            m_CtlSizeManager.addControlMove(BTN_SEARCH, true, false, false, false);

            //횡늘림
            m_CtlSizeManager.addControlMove(TIL_1, false, false, true, false);
            m_CtlSizeManager.addControlMove(LAY_SEARCH, false, false, true, false);
            m_CtlSizeManager.addControlMove(TIL_2, false, false, true, false);
            //m_CtlSizeManager.addControlMove(LAY_PAGE, false, false, true, false);

            //종횡 늘림S
            m_CtlSizeManager.addControlMove(GRD_LOG, false, false, true, true);

            //setStoreName();
        }

        private void Logbook_Load(object sender, EventArgs e)
        {
            txt_StartDate.Value = DateTime.Today;
            txt_EndDate.Value = DateTime.Today;
        }

        private void Logbook_SizeChanged(object sender, EventArgs e)
        {
            if (m_CtlSizeManager != null)
            {
                m_CtlSizeManager.MoveControls();

                this.Refresh();
            }
        }

        private void setWaitCursor(Boolean bWait)
        {
            if (bWait)
            {
                Cursor.Current = Cursors.WaitCursor;
                this.UseWaitCursor = true;
            }
            else
            {
                Cursor.Current = Cursors.Default;
                this.UseWaitCursor = false;
            }
        }

        int display_num = 20;
        private void BTN_SEARCH_Click(object sender, EventArgs e)
        {
            if (!BTN_SEARCH.Enabled)
            {
                return;
            }
            int nRow = GRD_LOG.RowCount;
            for (int i = nRow - 1; i >= 0; i--)
            {
                GRD_LOG.Rows.RemoveAt(i);
            }
            GRD_LOG.Refresh();
            BTN_SEARCH.Enabled = false;

            JObject jsonReq = new JObject();

            try
            {
                setWaitCursor(true);

                Constants.LOGGER_MAIN.Info("txt_StartDate.Value : " + txt_StartDate.Value);
                Constants.LOGGER_MAIN.Info("txt_EndDate.Value : " + txt_EndDate.Value);

                int diff = DateTime.Compare(txt_StartDate.Value, txt_EndDate.Value);
                Constants.LOGGER_MAIN.Info("diff : " + diff);

                TimeSpan between_date = txt_EndDate.Value - txt_StartDate.Value;
                int date_cnt = between_date.Days;
                Constants.LOGGER_MAIN.Info("date_cnt : " + date_cnt);
                if (diff > 0)
                {
                    // 미래날짜의 경우
                    MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidateSearchDate"));
                    return;
                }
                else if (date_cnt > 7)
                {
                    // 일주일 이상의 경우
                    MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ExceedSearchDate"));
                    return;
                }
                jsonReq.Add("group_id", Constants.GROUP_ID);
                jsonReq.Add("date_div", "P");
                jsonReq.Add("start_date", Util.Utils.FormatConvertDate(txt_StartDate.Text));
                jsonReq.Add("end_date", Util.Utils.FormatConvertDate(txt_EndDate.Text));
                jsonReq.Add("shop_id", Constants.MID);
                //jsonReq.Add("date", Util.Utils.FormatConvertDate(txt_EndDate.Text));

                //if (!txt_InvoiceNo.Equals(""))
                //{
                //jsonReq.Add("seal_bag_no", txt_InvoiceNo.Text);
                //jsonReq.Add("seal_bag_no", "");
                //}

                Transaction tran = new Transaction();

                string page = "1";
                int total_cnt = tran.searchLogBookCount(jsonReq.ToString());
                if (total_cnt > 0)
                {
                    if (cmbbox_Page.Items.Count > 0)
                        cmbbox_Page.Items.Clear();

                    int i = 0;
                    double quotient = total_cnt / display_num;
                    double remainder = total_cnt % display_num;

                    double tot_page = 0;
                    if (remainder != 0)
                    {
                        tot_page = System.Math.Truncate(quotient);
                    }
                    else
                    {
                        tot_page = System.Math.Truncate(quotient) - 1;
                    }

                    for (i = 0; i <= tot_page; i++)
                    {
                        cmbbox_Page.Items.Add(i + 1);
                    }

                    searchList(page);
                }
                else
                {
                    MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/DataNotFound"),
                        "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }
            finally
            {
                setWaitCursor(false);
                BTN_SEARCH.Enabled = true;
            }
        }
        
        private void searchList(string page_num)
        {
            JObject jsonReq = new JObject();

            JArray searchArr = new JArray();

            try
            {
                cmbbox_Page.SelectedIndex = int.Parse(page_num) - 1;

                Transaction tran = new Transaction();

                jsonReq.Add("query_div", "");
                jsonReq.Add("page_num", page_num);
                jsonReq.Add("display_num", display_num);

                jsonReq.Add("group_id", Constants.GROUP_ID);
                jsonReq.Add("date_div", "P");
                jsonReq.Add("start_date", Util.Utils.FormatConvertDate(txt_StartDate.Text));
                jsonReq.Add("end_date", Util.Utils.FormatConvertDate(txt_EndDate.Text));
                jsonReq.Add("shop_id", Constants.MID);
                //jsonReq.Add("date", Util.Utils.FormatConvertDate(txt_EndDate.Text));

                //if (!txt_InvoiceNo.Equals(""))
                //{
                //jsonReq.Add("seal_bag_no", txt_InvoiceNo.Text);
                //jsonReq.Add("seal_bag_no", "");
                //}

                string strResult = tran.searchLogBook(jsonReq.ToString());

                int nRow = 0;

                CLEAR();

                if (!strResult.Equals(""))
                {
                    nRow = GRD_LOG.RowCount;

                    Constants.LOGGER_MAIN.Info("Start Parse (1) : " + DateTime.Now);

                    searchArr = JArray.Parse(strResult);

                    Constants.LOGGER_MAIN.Info("Start Parse (2) : " + DateTime.Now);

                    int i = 0;
                    foreach (JObject json in searchArr.Children<JObject>())
                    {
                        GRD_LOG.Rows.Add();

                        GRD_LOG.Rows[nRow + i].Cells["DateOfInvoice"].Value = Util.Utils.FormatDate(json["INVOICE_DATE"].ToString(), 2);
                        GRD_LOG.Rows[nRow + i].Cells["InvoiceNumber"].Value = json["RCT_NO"].ToString();
                        GRD_LOG.Rows[nRow + i].Cells["GstCharged"].Value = json["GST_AMT"].ToString();
                        GRD_LOG.Rows[nRow + i].Cells["DocID"].Value = (json["DOC_ID"].ToString().Length == 20 ? Util.Utils.FormatDocId(json["DOC_ID"].ToString()) : json["DOC_ID"].ToString());
                        GRD_LOG.Rows[nRow + i].Cells["DateOfIssue"].Value = Util.Utils.FormatDate(json["ISSUE_DATE"].ToString(), 2);
                        GRD_LOG.Rows[nRow + i].Cells["TimeOfIssue"].Value = Util.Utils.FormatTime(json["ISSUE_TIME"].ToString(), "12");
                        GRD_LOG.Rows[nRow + i].Cells["PassportNumber"].Value = json["PASSPORT_NO"].ToString();
                        GRD_LOG.Rows[nRow + i].Cells["NameStaff"].Value = json["STAFF_NAME"].ToString();
                        string status = json["STATUS_DESC"].ToString();
                        if (status.Equals("Voided"))
                        {
                            GRD_LOG.Rows[nRow + i].Cells["Status"].Value = status + " (" + Util.Utils.FormatDate(json["VOID_DATE"].ToString(), 2) + ")";
                        }
                        else
                        {
                            GRD_LOG.Rows[nRow + i].Cells["Status"].Value = status;
                        }
                        GRD_LOG.Rows[nRow + i].Cells["isTarget"].Value = (json["SEAL_BAG_YN"].ToString() == "Y" ? "Yes" : "No");
                        GRD_LOG.Rows[nRow + i].Cells["Packed"].Value = (json["PACKED_YN"].ToString() == "Y" ? "Yes" : "No");
                        GRD_LOG.Rows[nRow + i].Cells["SeqNumber"].Value = json["SEAL_BAG_NO"].ToString();
                        i++;
                    }

                    Constants.LOGGER_MAIN.Info("Start Parse (3) : " + DateTime.Now);
                }
                else
                {
                    MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/DataNotFound"),
                        "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
                MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/SearchFaild"),
                       "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private JArray searchAllList()
        {
            JObject jsonReq = new JObject();

            JArray allTrArr = new JArray();

            try
            {
                Transaction tran = new Transaction();

                jsonReq.Add("query_div", "F");

                jsonReq.Add("group_id", Constants.GROUP_ID);
                jsonReq.Add("date_div", "P");
                jsonReq.Add("start_date", Util.Utils.FormatConvertDate(txt_StartDate.Text));
                jsonReq.Add("end_date", Util.Utils.FormatConvertDate(txt_EndDate.Text));
                jsonReq.Add("shop_id", Constants.MID);
                //jsonReq.Add("date", Util.Utils.FormatConvertDate(txt_EndDate.Text));

                //if (!txt_InvoiceNo.Equals(""))
                //{
                //jsonReq.Add("seal_bag_no", txt_InvoiceNo.Text);
                //jsonReq.Add("seal_bag_no", "");
                //}

                string strResult = tran.searchLogBook(jsonReq.ToString());

                if (!strResult.Equals(""))
                {
                    allTrArr = JArray.Parse(strResult);
                }
            }
            catch (Exception ex)
            {
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }

            return allTrArr;
        }

        private void BTN_PRINT_Click(object sender, EventArgs e)
        {
            try
            {
                PrintLogBook();
            }
            catch (Exception ex)
            {
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
                MetroMessageBox.Show(this, "Unknown error occurred, contact system administrator.", "Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        static Image ScaleByPercent(Image imgPhoto, int Percent)
        {
            //퍼센트 0.8 or 0.5 ..
            float nPercent = ((float)Percent / 100);
            //넓이와 높이
            int OriginalWidth = imgPhoto.Width;
            int OriginalHeight = imgPhoto.Height;
            //소스의 처음 위치
            int OriginalX = 0;
            int OriginalY = 0;
            //움직일 위치
            int adjustX = 0;
            int adjustY = 0;
            //조절될 퍼센트 계산
            int adjustWidth = (int)(OriginalWidth * nPercent);
            int adjustHeight = (int)(OriginalHeight * nPercent);
            //비어있는 비트맵 객체 생성
            Bitmap bmPhoto = new Bitmap(adjustWidth, adjustHeight, PixelFormat.Format24bppRgb);
            //이미지를 그래픽 객체로 만든다.
            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            //사각형을 그린다.
            //그릴 이미지객체 크기, 그려질 이미지객체 크기
            grPhoto.DrawImage(imgPhoto,
                   new Rectangle(adjustX, adjustY, adjustWidth, adjustHeight),
                   new Rectangle(OriginalX, OriginalY, OriginalWidth, OriginalHeight),
                   GraphicsUnit.Pixel);
            grPhoto.Dispose();
            return bmPhoto;
        }

        private void BTN_EXCEL_Click(object sender, EventArgs e)
        {
            ExcelDownloadUtil util = new ExcelDownloadUtil();

            JArray excelArr = new JArray();

            int rslt = 0;

            try
            {
                excelArr = searchAllList();

                if (excelArr.Count > 0)
                {
                    rslt = util.DownloadLogBookByExcel(excelArr);

                    if (rslt == 0)
                    {
                        MetroMessageBox.Show(this, "Download successfully!", "Message"
                            , MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (rslt == -1)
                    {
                        MetroMessageBox.Show(this, "Unknown error occurred, contact system administrator.", "Error"
                            , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/DataNotFound"),
                        "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
                MetroMessageBox.Show(this, "Unknown error occurred, contact system administrator.", "Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void CLEAR()
        {
            int nRow = GRD_LOG.RowCount;

            for (int i = nRow - 1; i >= 0; i--)
            {
                GRD_LOG.Rows.RemoveAt(i);
            }

            GRD_LOG.Refresh();
        }

        #region PRINT LOG BOOK

        private double rowCnt = 15;
        private int a4Width;
        private int a4Height;
        private int topMargin = 20;
        private int leftMargin = 20;
        public int leftStart = 0;

        public double totalPageNumber = 0;
        public double currentPageNumber = 0;
        public JArray allTrArr = new JArray();

        public int PrintLogBook()
        {
            int nRet = 0;

            Renderer = typeof(BitmapRenderer);

            PrinterSettings logBookPrinterSettings = new PrinterSettings();
            PageSettings logBookPageSettings = new PageSettings();
            PrintDialog logBookPrintDialog = new PrintDialog();
            PageSetupDialog logBookPageSetupDialog = new PageSetupDialog();
            PrintPreviewDialog logBookPrintPreviewDialog = new PrintPreviewDialog();

            double totalCnt = 0;
            double temp1 = 0;
            double temp2 = 0;

            try
            {
                allTrArr = searchAllList();

                totalCnt = allTrArr.Count;

                temp1 = System.Math.Truncate(totalCnt / rowCnt);
                temp2 = totalCnt % rowCnt;

                if (temp2 > 0) totalPageNumber = temp1 + 1;
                else totalPageNumber = temp1;
                Constants.LOGGER_MAIN.Info("### Total Page Count : " + totalPageNumber);

                PrintDocument printDoc = new PrintDocument();

                printDoc.PrintPage += new PrintPageEventHandler(DrawLogBook);
                printDoc.EndPrint += new PrintEventHandler(EndPrintLogBook);

                IEnumerable<PaperSize> paperSizes = logBookPrinterSettings.PaperSizes.Cast<PaperSize>();
                PaperSize sizeA4 = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A4); // setting paper size to A4 size
                printDoc.DefaultPageSettings.PaperSize = sizeA4;

                logBookPageSettings.Landscape = true;

                // 1188 * 880
                this.a4Width = sizeA4.Height;
                this.a4Height = sizeA4.Width;

                printDoc.DefaultPageSettings = logBookPageSettings;
                printDoc.PrinterSettings.PrinterName = Constants.PRINTER_TYPE;
                printDoc.Print(); // PRINT
            }
            catch (Exception ex)
            {
                Constants.LOGGER_MAIN.Error(ex.Message);
                nRet = -1;
            }

            return nRet;
        }

        public void EndPrintLogBook(object sender, PrintEventArgs e)
        {
            i = 0;
            totalPageNumber = 0;
            currentPageNumber = 0;
            allTrArr.Clear();
        }

        public int i = 0;
        public void DrawLogBook(object sender, PrintPageEventArgs e)
        {
            try
            {
                JArray printArray = new JArray();

                float yPos = topMargin;

                currentPageNumber += 1;

                Constants.LOGGER_MAIN.Info("### Current PageNumber : " + currentPageNumber);

                while (allTrArr.Count > i)
                {
                    JObject json = (JObject)allTrArr[i];

                    printArray.Add(json);

                    i += 1;

                    if (printArray.Count == rowCnt || allTrArr.Count == i)
                    {
                        Constants.LOGGER_MAIN.Info("### PrintArray Count : " + printArray.Count);

                        PrintLogBookHeaderInfo(e, ref yPos);

                        PrintLogBookHeaderTable(e, ref yPos);

                        PrintLogBookDataTable(e, ref yPos, printArray);

                        PrintLogBookPageNumber(e, ref yPos);

                        if (currentPageNumber < this.totalPageNumber)
                        {
                            e.HasMorePages = true;
                            return;
                        }
                        else
                        {
                            e.HasMorePages = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Constants.LOGGER_MAIN.Error(ex.Message);
                throw ex;
            }
        }

        private int headerWidth = 1080;

        private int[] logBookHeaderSizes = new int[] {
            65,
            115,
            75,
            130,
            60,
            55,
            70,
            110,
            100,
            60,
            100,
            60,
            80
        };

        public void PrintLogBookHeaderInfo(PrintPageEventArgs e, ref float yPos)
        {
            int strIdx = 0;
            int endIdx = 0;
            string printDate = "";

            try
            {
                strFont = new Font("Arial", 14, FontStyle.Bold);
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;

                strSubFormat.Alignment = StringAlignment.Near;
                strSubFormat.LineAlignment = StringAlignment.Far;

                strIdx = leftStart + leftMargin;
                endIdx = strIdx + 225; // 200(12) -> 225(14)
                Rectangle rect1 = new Rectangle(strIdx, (int)yPos, 225, strFont.Height);
                e.Graphics.DrawString("Transaction LogBook", strFont, Brushes.Black, rect1, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect1);

                Constants.LOGGER_MAIN.Info("strFont.Height : " + strFont.Height); //Font14 : 22
                //Constants.LOGGER_MAIN.Info("strFont.Height : " + strFont.Height); Font12 : 19
                //Constants.LOGGER_MAIN.Info("strFontSub.Height : " + strFontSub.Height); Font9 : 14

                strIdx = endIdx;
                printDate = "";

                Constants.LOGGER_MAIN.Info("txt_StartDate.Value.ToShortDateString() : " + txt_StartDate.Value.ToShortDateString());
                Constants.LOGGER_MAIN.Info("txt_EndDate.Value.ToShortDateString() : " + txt_EndDate.Value.ToShortDateString());

                if (DateTime.Compare(txt_StartDate.Value, txt_EndDate.Value) == 0)
                {
                    printDate = "Date : " + Util.Utils.FormatDate(Util.Utils.FormatConvertDate(txt_StartDate.Text), 2);
                }
                else
                {
                    printDate = "Date : " + Util.Utils.FormatDate(Util.Utils.FormatConvertDate(txt_StartDate.Text), 2) + " ~ " + Util.Utils.FormatDate(Util.Utils.FormatConvertDate(txt_EndDate.Text), 2);
                }
                rect1 = new Rectangle(strIdx, (int)yPos, headerWidth + leftStart + leftMargin - strIdx, strFont.Height);
                e.Graphics.DrawString(printDate, strFontSub, Brushes.Black, rect1, strSubFormat);
                e.Graphics.DrawRectangle(Pens.White, rect1);

                ResourceManager rm = JEWELRY_NUBES.Properties.Resources.ResourceManager;
                
                Color orange_color = Color.FromArgb(244, 124, 48);
                SolidBrush sbr = new SolidBrush(orange_color);

                strFont = new Font("Century Gothic", 12);
                strFormat.Alignment = StringAlignment.Near;
                rect1 = new Rectangle(headerWidth - 120, (int)yPos, 80, strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Global, strFont, sbr, rect1, strFormat);

                strFont = new Font("Century Gothic", 12, FontStyle.Bold);
                rect1 = new Rectangle(headerWidth - 120 + 67, (int)yPos, 79, strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Taxfree, strFont, sbr, rect1, strFormat);

                yPos += strFont.Height + 5;

                rect1 = new Rectangle(leftStart + leftMargin, (int)yPos, headerWidth, 2);
                e.Graphics.FillRectangle(sbr, rect1);

                //yPos += strFont.Height * 2;
                yPos += 14;
            }
            catch (Exception ex)
            {
                Constants.LOGGER_MAIN.Error(ex.Message);
                throw ex;
            }
        }

        public void PrintLogBookHeaderTable(PrintPageEventArgs e, ref float yPos)
        {
            int strIdx = 0;
            int endIdx = 0;
            int i = 0;
            int headerSize = 0;
            int headerHeight = 0;

            try
            {
                strFont = new Font("Arial", 7);

                //strFormat.LineAlignment = StringAlignment.Far;
                //strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Alignment = StringAlignment.Center;

                Color orange_color = Color.FromArgb(244, 124, 48);
                SolidBrush sbr = new SolidBrush(orange_color);

                Rectangle rect;
                headerHeight = strFont.Height * 9;

                strIdx = leftStart + leftMargin;
                headerSize = logBookHeaderSizes[i++];
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Date of Invoice", strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);
                

                strIdx = endIdx;
                headerSize = logBookHeaderSizes[i++];
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Invoice Number", strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);
                

                strIdx = endIdx;
                headerSize = logBookHeaderSizes[i++];
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("GST Charged", strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);
                

                strIdx = endIdx;
                headerSize = logBookHeaderSizes[i++];
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Document ID", strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);
                

                strIdx = endIdx;
                headerSize = logBookHeaderSizes[i++];
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Date of Issuing eTRS Ticket", strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);
                

                strIdx = endIdx;
                headerSize = logBookHeaderSizes[i++];
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Time of Issuing eTRS ticket", strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);
                

                strIdx = endIdx;
                headerSize = logBookHeaderSizes[i++];
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Passport number", strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);
                

                strIdx = endIdx;
                headerSize = logBookHeaderSizes[i++];
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Name of Staff", strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);
                

                strIdx = endIdx;
                headerSize = logBookHeaderSizes[i++];
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Signature of Staff", strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);
                

                strIdx = endIdx;
                headerSize = logBookHeaderSizes[i++];
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Void/Issued", strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);
                

                strIdx = endIdx;
                headerSize = logBookHeaderSizes[i++];
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Total value of the sales made to the tourist under TRS for the preceding three (3) months at the point of sales is $7,000 or more [Yes/No] ", strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);
                

                strIdx = endIdx;
                headerSize = logBookHeaderSizes[i++];
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Are goods packed in sealed bag? [Yes/No]", strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);
                

                strIdx = endIdx;
                headerSize = logBookHeaderSizes[i++];
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Serial number of the sealed bag (if applicable)", strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);
                

                yPos += headerHeight;
            }
            catch (Exception ex)
            {
                Constants.LOGGER_MAIN.Error(ex.Message);
                throw ex;
            }
        }

        public void PrintLogBookDataTable(PrintPageEventArgs e, ref float yPos, JArray dataArray)
        {
            int strIdx;
            int endIdx;
            int i;
            int headerSize;
            int headerHeight = 0;
            int padding = 5;

            try
            {
                Rectangle rect;

                strFont = new Font("Arial", 7);
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Alignment = StringAlignment.Center;

                numFormat.LineAlignment = StringAlignment.Center;
                numFormat.Alignment = StringAlignment.Far;

                leftFormat.LineAlignment = StringAlignment.Center;
                leftFormat.Alignment = StringAlignment.Near;

                //headerHeight = strFont.Height * 2 + 2;
                headerHeight = strFont.Height * 3 + 6;

                foreach (JObject json in dataArray.Children<JObject>())
                {
                    //Constants.LOGGER_MAIN.Info("%%% Doc ID : " + Util.Utils.FormatDocId(json["DOC_ID"].ToString()));

                    strIdx = 0;
                    endIdx = 0;
                    i = 0;
                    headerSize = 0;

                    // Date of Invoice
                    strIdx = leftStart + leftMargin;
                    headerSize = logBookHeaderSizes[i++];
                    endIdx = strIdx + headerSize;
                    rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                    e.Graphics.DrawRectangle(Pens.Black, rect);
                    e.Graphics.DrawString(Util.Utils.FormatDate(json["INVOICE_DATE"].ToString(), 2), strFont, Brushes.Black, rect, strFormat);

                    // Invoice Number
                    strIdx = endIdx;
                    headerSize = logBookHeaderSizes[i++];
                    endIdx = strIdx + headerSize;
                    rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                    e.Graphics.DrawRectangle(Pens.Black, rect);
                    rect = new Rectangle(strIdx + padding, (int)yPos, headerSize, headerHeight);
                    e.Graphics.DrawString(json["RCT_NO"].ToString(), strFont, Brushes.Black, rect, leftFormat);

                    // Gst Charged
                    strIdx = endIdx;
                    headerSize = logBookHeaderSizes[i++];
                    endIdx = strIdx + headerSize;
                    rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                    e.Graphics.DrawRectangle(Pens.Black, rect);
                    rect = new Rectangle(strIdx, (int)yPos, headerSize - padding, headerHeight);
                    e.Graphics.DrawString(json["GST_AMT"].ToString(), strFont, Brushes.Black, rect, numFormat);

                    // Document ID
                    strIdx = endIdx;
                    headerSize = logBookHeaderSizes[i++];
                    endIdx = strIdx + headerSize;
                    rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                    e.Graphics.DrawRectangle(Pens.Black, rect);
                    rect = new Rectangle(strIdx + padding, (int)yPos, headerSize, headerHeight);
                    e.Graphics.DrawString((json["DOC_ID"].ToString().Length == 20 ? Util.Utils.FormatDocId(json["DOC_ID"].ToString()) : json["DOC_ID"].ToString()), strFont, Brushes.Black, rect, leftFormat);

                    // Date of Issuing eTRS ...
                    strIdx = endIdx;
                    headerSize = logBookHeaderSizes[i++];
                    endIdx = strIdx + headerSize;
                    rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                    e.Graphics.DrawRectangle(Pens.Black, rect);
                    e.Graphics.DrawString(Util.Utils.FormatDate(json["ISSUE_DATE"].ToString(), 2), strFont, Brushes.Black, rect, strFormat);

                    // Time of Issuing eTRS ...
                    strIdx = endIdx;
                    headerSize = logBookHeaderSizes[i++];
                    endIdx = strIdx + headerSize;
                    rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                    e.Graphics.DrawRectangle(Pens.Black, rect);
                    e.Graphics.DrawString(Util.Utils.FormatTime(json["ISSUE_TIME"].ToString(), "12"), strFont, Brushes.Black, rect, strFormat);

                    // Passport number
                    strIdx = endIdx;
                    headerSize = logBookHeaderSizes[i++];
                    endIdx = strIdx + headerSize;
                    rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                    e.Graphics.DrawRectangle(Pens.Black, rect);
                    e.Graphics.DrawString(json["PASSPORT_NO"].ToString(), strFont, Brushes.Black, rect, strFormat);

                    // Name of Staff
                    strIdx = endIdx;
                    headerSize = logBookHeaderSizes[i++];
                    endIdx = strIdx + headerSize;
                    rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                    e.Graphics.DrawRectangle(Pens.Black, rect);
                    rect = new Rectangle(strIdx + padding, (int)yPos, headerSize, headerHeight);
                    e.Graphics.DrawString(json["STAFF_NAME"].ToString(), strFont, Brushes.Black, rect, leftFormat);

                    // Signature of Staff
                    strIdx = endIdx;
                    headerSize = logBookHeaderSizes[i++];
                    endIdx = strIdx + headerSize;
                    rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                    e.Graphics.DrawRectangle(Pens.Black, rect);

                    // Void/Issued
                    strIdx = endIdx;
                    headerSize = logBookHeaderSizes[i++];
                    endIdx = strIdx + headerSize;
                    rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                    e.Graphics.DrawRectangle(Pens.Black, rect);
                    string status = json["STATUS_DESC"].ToString();
                    if (status.Equals("Voided"))
                    {
                        e.Graphics.DrawString(status + " (" + Util.Utils.FormatDate(json["VOID_DATE"].ToString(), 2) + ")", strFont, Brushes.Black, rect, strFormat);
                    }
                    else
                    {
                        e.Graphics.DrawString(status, strFont, Brushes.Black, rect, strFormat);
                    }

                    // Total value of the sales ...
                    strIdx = endIdx;
                    headerSize = logBookHeaderSizes[i++];
                    endIdx = strIdx + headerSize;
                    rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                    e.Graphics.DrawRectangle(Pens.Black, rect);
                    e.Graphics.DrawString((json["SEAL_BAG_YN"].ToString() == "Y" ? "Yes" : "No"), strFont, Brushes.Black, rect, strFormat);

                    // Are gods packed ...
                    strIdx = endIdx;
                    headerSize = logBookHeaderSizes[i++];
                    endIdx = strIdx + headerSize;
                    rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                    e.Graphics.DrawRectangle(Pens.Black, rect);
                    e.Graphics.DrawString((json["PACKED_YN"].ToString() == "Y" ? "Yes" : "No"), strFont, Brushes.Black, rect, strFormat);

                    // Serial number of the sealed ...
                    strIdx = endIdx;
                    headerSize = logBookHeaderSizes[i++];
                    endIdx = strIdx + headerSize;
                    rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                    e.Graphics.DrawRectangle(Pens.Black, rect);
                    e.Graphics.DrawString(json["SEAL_BAG_NO"].ToString(), strFont, Brushes.Black, rect, strFormat);

                    yPos += headerHeight;
                }
            }
            catch (Exception ex)
            {
                Constants.LOGGER_MAIN.Error(ex.Message);
                throw ex;
            }
        }

        public void PrintLogBookPageNumber(PrintPageEventArgs e, ref float yPos)
        {
            try
            {
                strFont = new Font("Arial", 7);
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Alignment = StringAlignment.Center;

                int pager_yPos = this.a4Height - 35 - (strFont.Height * 2);

                Rectangle rect1 = new Rectangle(leftStart, pager_yPos, this.a4Width, strFont.Height);
                e.Graphics.DrawString(currentPageNumber.ToString(), strFont, Brushes.Black, rect1, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect1);

                yPos = this.a4Height;
            }
            catch (System.Exception ex)
            {
                Constants.LOGGER_MAIN.Error(ex.Message);
                throw ex;
            }
        }

        #endregion

        private void cmbbox_Page_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!BTN_SEARCH.Enabled)
            {
                return;
            }

            searchList(((cmbbox_Page.SelectedIndex) + 1).ToString());
        }

        private void setStoreName()
        {
            COM_STORE.Items.Clear();
            COM_STORE.Items.Add("Select Store Name");
            foreach (JObject json in Constants.merchant_list.Children<JObject>())
            {
                COM_STORE.Items.Add(json["store_name"].ToString());
            }
            COM_STORE.SelectedIndex = 0;
        }

        private void COM_STORE_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nRow = GRD_LOG.RowCount;
            for (int i = nRow - 1; i >= 0; i--)
            {
                GRD_LOG.Rows.RemoveAt(i);
            }
            GRD_LOG.Refresh();

            if (COM_STORE.SelectedIndex == 0)
            {
                BTN_SEARCH.Enabled = false;
                BTN_EXCEL.Enabled = false;
                BTN_PRINT.Enabled = false;
            }
            else
            {
                BTN_SEARCH.Enabled = true;
                BTN_EXCEL.Enabled = true;
                BTN_PRINT.Enabled = true;
                JObject jsonStore = Constants.merchant_list[(COM_STORE.SelectedIndex - 1)].ToObject<JObject>();
                if (jsonStore["mid"] != null)
                {
                    Constants.MID = jsonStore["mid"].ToString();
                }
                if (jsonStore["tid"] != null)
                {
                    Constants.TID = jsonStore["tid"].ToString();
                }
                if (jsonStore["gst_no"] != null)
                {
                    Constants.GST_NO = jsonStore["gst_no"].ToString();
                }
                if (jsonStore["rec_prefix"] != null)
                {
                    Constants.REC_PREFIX = jsonStore["rec_prefix"].ToString();
                }
                if (jsonStore["dup_rc"] != null)
                {
                    Constants.DUP_RC = jsonStore["dup_rc"].ToString();
                }
                if (jsonStore["rec_digits"] != null)
                {
                    Constants.REC_DIGITS = jsonStore["rec_digits"].ToString();
                }
                if (jsonStore["item_code1"] != null)
                {
                    Constants.ITEM_CODE1 = jsonStore["item_code1"].ToString();
                }
                if (jsonStore["item_code2"] != null)
                {
                    Constants.ITEM_CODE2 = jsonStore["item_code2"].ToString();
                }
                if (jsonStore["item_code3"] != null)
                {
                    Constants.ITEM_CODE3 = jsonStore["item_code3"].ToString();
                }
                if (jsonStore["item_code4"] != null)
                {
                    Constants.ITEM_CODE4 = jsonStore["item_code4"].ToString();
                }
                if (jsonStore["item_code5"] != null)
                {
                    Constants.ITEM_CODE5 = jsonStore["item_code5"].ToString();
                }

            }
        }
    }
}

