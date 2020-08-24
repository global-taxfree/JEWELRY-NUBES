using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing;
using System.Resources;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using JEWELRY_NUBES.Properties;
using GTF_Printer;
using JEWELRY_NUBES.Util;

namespace JEWELRY_NUBES.Util
{
    class WindowPrinterUtil
    {
        StringFormat strFormat = new StringFormat();
        Font strFont = new Font("Century Gothic", 9);
        private const int nPageWidth = 700;
        private const int nStartPoint = 40;
        private const int nPageWidthHalf = 350;
        private const int nPageWidthQuater = 175;

        private EncodingOptions EncodingOptions { get; set; }
        private Type Renderer { get; set; }

        private Dictionary<string, string> m_ListDocId;
        private Dictionary<string, string> m_ListRetailer;
        private Dictionary<string, string> m_ListTourist;
        private Dictionary<string, object> m_ListPurchase;
        private Dictionary<string, object> m_ListToken;
        private string slip_status = "";
        

        public int PrintTicket(string retailer, string docid, string tourist, string purchase, string token, string status)
        {
            int nRet = 0;

            Renderer = typeof(BitmapRenderer);
            slip_status = status;

            try
            {
                ParseParameter(retailer, docid, tourist, purchase, token);
                UserPrintDocument printDoc = new UserPrintDocument();
                printDoc.UserPrintPageEvent += new UserPrintDocument.UserPrintPageEventHandler(PrintSlip);
                printDoc.PrinterSettings.PrinterName = Constants.PRINTER_TYPE;
                printDoc.Print();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nRet = -1;
            }
            return nRet;
        }

        public void ParseParameter(string retailer, string docid, string tourist, string purchase, string token)
        {
            m_ListRetailer = new Dictionary<string, string>();
            m_ListDocId = new Dictionary<string, string>();
            m_ListTourist = new Dictionary<string, string>();
            m_ListPurchase = new Dictionary<string, object>();
            m_ListToken = new Dictionary<string, object>();
            //Retailer
            if(slip_status.Equals("01") || slip_status.Equals("02"))
            {
                ParseRetailer(retailer);

                ParseDocId(docid);

                ParseTourist(tourist);

                ParsePurchase(purchase);

                ParseToken(token);
            }
            else
            {
                ParseRetailer(retailer);
                ParseDocId(docid);
                ParsePurchase(purchase);
            }
            
        }

        public void ParseRetailer(string retailer)
        {
            string[] arrRetailer = retailer.Split('|');
            m_ListRetailer.Add(JEWELRY_NUBES.Properties.Resource_MapKey.RetailerName, arrRetailer[0]);
            m_ListRetailer.Add(JEWELRY_NUBES.Properties.Resource_MapKey.BizNo, arrRetailer[1]);
            m_ListRetailer.Add(JEWELRY_NUBES.Properties.Resource_MapKey.RetailerAddr, arrRetailer[2]);
        }

        public void ParseDocId(string docid)
        {
            string[] arrDocId = docid.Split('|');
            m_ListDocId.Add(JEWELRY_NUBES.Properties.Resource_MapKey.DocId, arrDocId[0]);
            m_ListDocId.Add(JEWELRY_NUBES.Properties.Resource_MapKey.DateOfIssue, arrDocId[1]);
        }

        public void ParseTourist(string tourist)
        {
            string[] arrTourist = tourist.Split('|');
            m_ListTourist.Add(JEWELRY_NUBES.Properties.Resource_MapKey.PassportNumber, arrTourist[0]);
            m_ListTourist.Add(JEWELRY_NUBES.Properties.Resource_MapKey.Nationality, arrTourist[1]);
        }

        public void ParsePurchase(string purchase)
        {
            string[] arrPurchase = purchase.Split('|');
            m_ListPurchase.Add(JEWELRY_NUBES.Properties.Resource_MapKey.PurchaseTotalCnt, arrPurchase[0]);

            int nOffset = 1;
            for (int i = 0; i < Convert.ToInt32(m_ListPurchase[JEWELRY_NUBES.Properties.Resource_MapKey.PurchaseTotalCnt]); i++)
            {
                Dictionary<string, string> ItemData = new Dictionary<string, string>();
                ItemData.Add(String.Format("{0}_{1}", JEWELRY_NUBES.Properties.Resource_MapKey.PurchaseSeq, i), arrPurchase[nOffset++]);
                ItemData.Add(JEWELRY_NUBES.Properties.Resource_MapKey.DateOfSales, arrPurchase[nOffset++]);
                ItemData.Add(JEWELRY_NUBES.Properties.Resource_MapKey.SaleAmount, arrPurchase[nOffset++]);
                ItemData.Add(JEWELRY_NUBES.Properties.Resource_MapKey.ReceiptNo, arrPurchase[nOffset++]);

                m_ListPurchase.Add(String.Format("{0}_{1}", JEWELRY_NUBES.Properties.Resource_MapKey.PurchaseSeq, i), ItemData);
            }

            m_ListPurchase.Add(JEWELRY_NUBES.Properties.Resource_MapKey.TotalSaleAmount, arrPurchase[nOffset++]);
            m_ListPurchase.Add(JEWELRY_NUBES.Properties.Resource_MapKey.TotalTaxAmount, arrPurchase[nOffset++]);
            m_ListPurchase.Add(JEWELRY_NUBES.Properties.Resource_MapKey.TotalCharge, arrPurchase[nOffset++]);
            m_ListPurchase.Add(JEWELRY_NUBES.Properties.Resource_MapKey.TotalRefundAmount, arrPurchase[nOffset++]);
        }

        public void ParseToken(string token)
        {
            string[] arrToken = token.Split('|');
            m_ListToken.Add(JEWELRY_NUBES.Properties.Resource_MapKey.TokenType, arrToken[0]);
            m_ListToken.Add(JEWELRY_NUBES.Properties.Resource_MapKey.TokenDisplay, arrToken[1]);
        }

        public void PrintSlip(object sender, PrintPageEventArgs e)
        {
            float yPos = 60;

            if (slip_status.Equals("01") || slip_status.Equals("02"))
            {
                PrintHeader(e, ref yPos);
                PrintRetailerDetails(e, ref yPos);
                PrintTouristPassport(e, ref yPos);
                PrintPurchase(e, ref yPos);
                PrintInformation(e, ref yPos);
            }
            else
            {
                PrintHeader(e, ref yPos);
                PrintRetailerDetails(e, ref yPos);
                PrintPurchase(e, ref yPos);
                yPos += 120 ;
                PrintInformation(e, ref yPos);
            }
            
        }

        public void PrintHeader(PrintPageEventArgs e, ref float yPos)
        {
            try
            {
                if (slip_status.Equals("02"))
                {
                    strFont = new Font("Calibri", 18, FontStyle.Bold);
                    strFormat.LineAlignment = StringAlignment.Center;
                    strFormat.Alignment = StringAlignment.Center;
                    Rectangle rect1 = new Rectangle(nStartPoint, (int)yPos, nPageWidth, strFont.Height);
                    e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Re_Print, strFont, Brushes.Black, rect1, strFormat);
                    e.Graphics.DrawRectangle(Pens.White, rect1);
                    yPos += strFont.Height;

                    strFont = new Font("Century Gothic", 12, FontStyle.Bold);
                    strFormat.Alignment = StringAlignment.Far;

                    rect1 = new Rectangle(nStartPoint, (int)yPos, nPageWidth, strFont.Height);
                    e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.HeaderDesc, strFont, Brushes.Black, rect1, strFormat);
                    e.Graphics.DrawRectangle(Pens.White, rect1);
                    yPos += strFont.Height;
                }
                else if (slip_status.Equals("03"))
                {
                    strFont = new Font("Calibri", 18, FontStyle.Bold);
                    strFormat.LineAlignment = StringAlignment.Center;
                    strFormat.Alignment = StringAlignment.Center;
                    Rectangle rect1 = new Rectangle(nStartPoint, (int)yPos, nPageWidth, strFont.Height);
                    e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Void_Print, strFont, Brushes.Black, rect1, strFormat);
                    e.Graphics.DrawRectangle(Pens.White, rect1);
                    yPos += strFont.Height;

                    strFont = new Font("Century Gothic", 12, FontStyle.Bold);
                    yPos += strFont.Height;
                }
                else
                {
                    strFont = new Font("Century Gothic", 12, FontStyle.Bold);
                    strFormat.Alignment = StringAlignment.Far;

                    Rectangle rect1 = new Rectangle(nStartPoint, (int)yPos, nPageWidth, strFont.Height);
                    e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.HeaderDesc, strFont, Brushes.Black, rect1, strFormat);
                    e.Graphics.DrawRectangle(Pens.White, rect1);
                    yPos += strFont.Height;

                }


                strFormat.LineAlignment = StringAlignment.Near;
                strFormat.Alignment = StringAlignment.Near;
                ResourceManager rm = JEWELRY_NUBES.Properties.Resources.ResourceManager;

                
                Bitmap head_logo = (Bitmap)rm.GetObject("ETRS_LOGO");

                e.Graphics.DrawImage((Image)head_logo, new Rectangle(nStartPoint, (int)yPos, head_logo.Width, head_logo.Height));


                var writer = new BarcodeWriter
                {
                    Format = (BarcodeFormat)BarcodeFormat.CODE_128,
                    Options = EncodingOptions ?? new EncodingOptions
                    {
                        PureBarcode = true,
                        Height = 58,
                        Width = 450
                    },
                    Renderer = (IBarcodeRenderer<Bitmap>)Activator.CreateInstance(Renderer)
                };

                Bitmap barcodeBitmap = writer.Write(getDocId(m_ListDocId[JEWELRY_NUBES.Properties.Resource_MapKey.DocId]));
                Image img = (Image)barcodeBitmap;

                Point p = new Point(347, (int)Math.Round(yPos));
                if (!slip_status.Equals("03"))
                    e.Graphics.DrawImage(img, p);
                yPos += head_logo.Height;

                strFont = new Font("Century Gothic", 12, FontStyle.Bold);
                strFormat.Alignment = StringAlignment.Near;

                Rectangle rect = new Rectangle(nStartPoint, (int)yPos, nPageWidthHalf, strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.SINGAPORE, strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                strFont = new Font("Century Gothic", 10, FontStyle.Bold);
                strFormat.Alignment = StringAlignment.Far;
                rect = new Rectangle((nStartPoint  + nPageWidthHalf), (int)yPos, nPageWidthQuater - 20, strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.DocID03, strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                strFont = new Font("Century Gothic", 10);
                strFormat.Alignment = StringAlignment.Far;
                rect = new Rectangle((nStartPoint + nPageWidthHalf + nPageWidthQuater -20) , (int)yPos, nPageWidthQuater+ 20, strFont.Height);
                e.Graphics.DrawString(m_ListDocId[JEWELRY_NUBES.Properties.Resource_MapKey.DocId], strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                yPos += strFont.Height;


                strFont = new Font("Century Gothic", 12, FontStyle.Bold);
                strFormat.Alignment = StringAlignment.Near;
                rect = new Rectangle(nStartPoint, (int)yPos, nPageWidthHalf, strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Title_ETRS, strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                strFont = new Font("Century Gothic", 10, FontStyle.Bold);
                strFormat.Alignment = StringAlignment.Far;
                rect = new Rectangle((nStartPoint + nPageWidthHalf), (int)yPos, nPageWidthQuater -20, strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.DateTime, strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                strFormat.Alignment = StringAlignment.Far;
                strFont = new Font("Century Gothic", 10);
                rect = new Rectangle((nStartPoint + nPageWidthHalf + nPageWidthQuater -20), (int)yPos, nPageWidthQuater +20, strFont.Height);
                e.Graphics.DrawString(m_ListDocId[JEWELRY_NUBES.Properties.Resource_MapKey.DateOfIssue] , strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                yPos += strFont.Height * 4;


                Image gtf_png = (Image)rm.GetObject("new_logo");
                //Bitmap gtf_logo = (Bitmap)rm.GetObject("GTF_LOGO");
                

                e.Graphics.DrawImage((Image)gtf_png, new Rectangle(nStartPoint + 20 , (int)yPos, gtf_png.Width, gtf_png.Height));

                strFont = new Font("Century Gothic", 12, FontStyle.Bold);
                strFormat.Alignment = StringAlignment.Near;
                rect = new Rectangle(nStartPoint + gtf_png.Width + 120, (int)yPos, nPageWidth - gtf_png.Width  - 120 , strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Central_desc, strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);
                yPos += strFont.Height ;

                strFont = new Font("Century Gothic", 12);
                strFormat.Alignment = StringAlignment.Near;
                rect = new Rectangle(nStartPoint + gtf_png.Width + 120, (int)yPos, nPageWidth - gtf_png.Width - 120, strFont.Height * 4);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.CentralRefundAgency, strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);
                yPos += strFont.Height * 4;

                
                Color orange_color = Color.FromArgb(244, 124, 48);
                SolidBrush sbr = new SolidBrush(orange_color);

                strFont = new Font("Century Gothic", 12);
                strFormat.Alignment = StringAlignment.Near;
                rect = new Rectangle(nStartPoint , (int)yPos, 80, strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Global, strFont, sbr, rect, strFormat);

                strFont = new Font("Century Gothic", 12, FontStyle.Bold);
                rect = new Rectangle(nStartPoint + 67, (int)yPos, 79, strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Taxfree, strFont, sbr, rect, strFormat);

                yPos += strFont.Height ;

                /*
                Bitmap gtf_name = (Bitmap)rm.GetObject("GTF_NAME");

                e.Graphics.DrawImage((Image)gtf_name, new Rectangle(nStartPoint-10, (int)yPos, gtf_name.Width, gtf_name.Height));

                yPos += strFont.Height *2;
                */

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public void PrintRetailerDetails(PrintPageEventArgs e, ref float yPos)
        {
            try
            {
                Color orange_color = Color.FromArgb(244, 124, 48);
                SolidBrush sbr = new SolidBrush(orange_color);
                strFont = new Font("Century Gothic", 14);
                strFormat.Alignment = StringAlignment.Near;
                Rectangle rect = new Rectangle(nStartPoint, (int)yPos, nPageWidth , strFont.Height );
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Title_RetailerInfo, strFont, Brushes.Black, rect, strFormat);
                yPos += strFont.Height;

                strFont = new Font("Century Gothic", 12, FontStyle.Bold);
                rect = new Rectangle(nStartPoint, (int)yPos, nPageWidthHalf, strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Merchant_name, strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                strFormat.Alignment = StringAlignment.Near;
                rect = new Rectangle(nStartPoint + nPageWidthHalf, (int)yPos, nPageWidthHalf, strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Address, strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                yPos += strFont.Height ;

                strFont = new Font("Century Gothic", 12);
                strFormat.Alignment = StringAlignment.Near;
                rect = new Rectangle(nStartPoint, (int)yPos, nPageWidthHalf, strFont.Height);
                e.Graphics.DrawString(m_ListRetailer[JEWELRY_NUBES.Properties.Resource_MapKey.RetailerName], strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                strFormat.Alignment = StringAlignment.Near;
                rect = new Rectangle(nStartPoint + nPageWidthHalf, (int)yPos, nPageWidthHalf, strFont.Height *2);
                e.Graphics.DrawString(m_ListRetailer[JEWELRY_NUBES.Properties.Resource_MapKey.RetailerAddr], strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                yPos += strFont.Height;

                strFont = new Font("Century Gothic", 12);
                strFormat.Alignment = StringAlignment.Near;
                rect = new Rectangle(nStartPoint, (int)yPos, nPageWidthHalf, strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.GSTRegNo + m_ListRetailer[JEWELRY_NUBES.Properties.Resource_MapKey.BizNo] , strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                yPos += strFont.Height * 3;

                /*
                strFormat.Alignment = StringAlignment.Near;
                rect = new Rectangle(0, (int)yPos, nPageWidth / 2, strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.GSTRegNo, strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                strFormat.Alignment = StringAlignment.Far;
                rect = new Rectangle(nPageWidth / 2, (int)yPos, nPageWidth / 2, strFont.Height);
                e.Graphics.DrawString(m_ListRetailer[JEWELRY_NUBES.Properties.Resource_MapKey.BizNo], strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                yPos += strFont.Height * 2;
                */
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }


        public void PrintTouristPassport(PrintPageEventArgs e, ref float yPos)
        {
            try
            {
                Color orange_color = Color.FromArgb(244, 124, 48);
                SolidBrush sbr = new SolidBrush(orange_color);

                string tokenType = m_ListToken[JEWELRY_NUBES.Properties.Resource_MapKey.TokenType].ToString();
                strFont = new Font("Century Gothic", 14);
                strFormat.Alignment = StringAlignment.Near;
                Rectangle rect = new Rectangle(nStartPoint, (int)yPos, nPageWidth, strFont.Height);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Title_Touristinfo, strFont, Brushes.Black, rect, strFormat);
                yPos += strFont.Height;

                strFont = new Font("Century Gothic", 12);
                strFormat.Alignment = StringAlignment.Near;
                rect = new Rectangle(nStartPoint, (int)yPos, nPageWidthHalf * (1/2), strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Passport_no, strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                strFormat.Alignment = StringAlignment.Near;
                rect = new Rectangle(235 , (int)yPos, nPageWidthHalf * (1 / 2), strFont.Height);
                e.Graphics.DrawString(m_ListTourist[JEWELRY_NUBES.Properties.Resource_MapKey.PassportNumber], strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                strFormat.Alignment = StringAlignment.Near;
                rect = new Rectangle(nStartPoint + nPageWidthHalf, (int)yPos, nPageWidthHalf, strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Token, strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                yPos += strFont.Height;

                strFont = new Font("Century Gothic", 12);
                strFormat.Alignment = StringAlignment.Near;
                rect = new Rectangle(nStartPoint, (int)yPos, nPageWidthHalf * (1 / 2), strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Nationality, strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);


                strFormat.Alignment = StringAlignment.Near;
                rect = new Rectangle(235, (int)yPos, nPageWidthHalf * (1 / 2), strFont.Height);
                e.Graphics.DrawString(m_ListTourist[JEWELRY_NUBES.Properties.Resource_MapKey.Nationality], strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                strFormat.Alignment = StringAlignment.Near;
                rect = new Rectangle(nStartPoint + nPageWidthHalf, (int)yPos, nPageWidthHalf, strFont.Height);
                
                
                if (tokenType.Equals("C"))
                {
                    e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Title_Credit +" "+ m_ListToken[JEWELRY_NUBES.Properties.Resource_MapKey.TokenDisplay].ToString(), strFont, Brushes.Black, rect, strFormat);
                }
                else if (tokenType.Equals("B"))
                {
                    e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Title_Debit + " "+ m_ListToken[JEWELRY_NUBES.Properties.Resource_MapKey.TokenDisplay].ToString(), strFont, Brushes.Black, rect, strFormat);
                }
                else if (tokenType.Equals("O"))
                {
                    e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Title_Other + " "+ m_ListToken[JEWELRY_NUBES.Properties.Resource_MapKey.TokenDisplay].ToString(), strFont, Brushes.Black, rect, strFormat);
                }
                else
                {
                    e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.DocID02, strFont, Brushes.Black, rect, strFormat);
                }
                e.Graphics.DrawRectangle(Pens.White, rect);

                yPos += strFont.Height * 4;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public void PrintPurchase(PrintPageEventArgs e, ref float yPos)
        {
            try
            {
                Color orange_color = Color.FromArgb(244, 124, 48);
                SolidBrush sbr = new SolidBrush(orange_color);

                strFont = new Font("Century Gothic", 14);
                strFormat.Alignment = StringAlignment.Near;
                Rectangle rect = new Rectangle(nStartPoint, (int)yPos, nPageWidth, strFont.Height);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Title_Purchase, strFont, Brushes.Black, rect, strFormat);
                

                yPos += strFont.Height;

                string minus_char = "$ ";
                strFont = new Font("Century Gothic", 12, FontStyle.Bold);
                if (slip_status.Equals("03"))
                {
                    minus_char = "-$ ";
                }
                    // Print Each Items..
                    for (int i = 0; i < Convert.ToInt32(m_ListPurchase[JEWELRY_NUBES.Properties.Resource_MapKey.PurchaseTotalCnt]); i++)
                {
                    string mapKey = String.Format("{0}_{1}", JEWELRY_NUBES.Properties.Resource_MapKey.PurchaseSeq, i);
                    Dictionary<string, string> ItemData = (Dictionary<string, string>)m_ListPurchase[mapKey];

                    strFormat.Alignment = StringAlignment.Near;
                    //rect = new Rectangle(40, (int)yPos, nPageWidth * (2 / 3), strFont.Height);
                    rect = new Rectangle(nStartPoint, (int)yPos, nPageWidthHalf, strFont.Height *2);
                    e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.ReceiptNo + ItemData[JEWELRY_NUBES.Properties.Resource_MapKey.ReceiptNo] , strFont, Brushes.Black, rect, strFormat);
                    e.Graphics.DrawRectangle(Pens.White, rect);


                    strFormat.Alignment = StringAlignment.Far;
                    // rect = new Rectangle(0, (int)yPos, nPageWidth * (2 / 3), strFont.Height);
                    rect = new Rectangle(nStartPoint + nPageWidthHalf, (int)yPos, nPageWidthHalf, strFont.Height * 2);
                    e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Purchase_date + ItemData[JEWELRY_NUBES.Properties.Resource_MapKey.DateOfSales], strFont, Brushes.Black, rect, strFormat);
                    e.Graphics.DrawRectangle(Pens.White, rect);

                    yPos += strFont.Height*2;
                }


                strFont = new Font("Century Gothic", 12);

                // Print Total Items                
                strFormat.Alignment = StringAlignment.Near;
                rect = new Rectangle(nStartPoint, (int)yPos, nPageWidthHalf, strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Total_amt, strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                strFormat.Alignment = StringAlignment.Far;
                rect = new Rectangle(nStartPoint + nPageWidthHalf, (int)yPos, nPageWidthHalf, strFont.Height);
                e.Graphics.DrawString(minus_char + String.Format("{0:F2}", Convert.ToDouble(m_ListPurchase[JEWELRY_NUBES.Properties.Resource_MapKey.TotalSaleAmount])), strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);
                yPos += strFont.Height;

                strFormat.Alignment = StringAlignment.Near;
                rect = new Rectangle(nStartPoint, (int)yPos, nPageWidthHalf, strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.GSTAmount, strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                strFormat.Alignment = StringAlignment.Far;
                rect = new Rectangle(nStartPoint + nPageWidthHalf, (int)yPos, nPageWidthHalf, strFont.Height);
                e.Graphics.DrawString(minus_char + m_ListPurchase[JEWELRY_NUBES.Properties.Resource_MapKey.TotalTaxAmount].ToString(), strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);
                yPos += strFont.Height;

                strFormat.Alignment = StringAlignment.Near;
                rect = new Rectangle(nStartPoint, (int)yPos, nPageWidthHalf, strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.ServiceFee, strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                strFormat.Alignment = StringAlignment.Far;
                rect = new Rectangle(nStartPoint + nPageWidthHalf, (int)yPos, nPageWidthHalf, strFont.Height);
                e.Graphics.DrawString(minus_char + m_ListPurchase[JEWELRY_NUBES.Properties.Resource_MapKey.TotalCharge].ToString(), strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);
                yPos += strFont.Height;

                strFormat.Alignment = StringAlignment.Near;
                rect = new Rectangle(nStartPoint, (int)yPos, nPageWidthHalf, strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.ProvisionalRefundAmount, strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                strFormat.Alignment = StringAlignment.Far;
                rect = new Rectangle(nStartPoint + nPageWidthHalf, (int)yPos, nPageWidthHalf, strFont.Height);
                e.Graphics.DrawString(minus_char + String.Format("{0:F2}", Convert.ToDouble(m_ListPurchase[JEWELRY_NUBES.Properties.Resource_MapKey.TotalRefundAmount])), strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);
                yPos += strFont.Height * 3;

                strFormat.Alignment = StringAlignment.Near;
                rect = new Rectangle(nStartPoint, (int)yPos, nPageWidth, 1);
                e.Graphics.FillRectangle(Brushes.Orange, rect);
                e.Graphics.DrawRectangle(Pens.Orange, rect);
                yPos += strFont.Height;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }


        public void PrintInformation(PrintPageEventArgs e, ref float yPos)
        {
            try
            {
                strFont = new Font("Calibri", 10);

                Rectangle rect = new Rectangle(nStartPoint, (int)yPos, nPageWidth, strFont.Height * 3);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Information_foot, strFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);
                yPos += strFont.Height * 3;

                rect = new Rectangle(nStartPoint, (int)yPos, nPageWidth, strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Information_email, strFont, Brushes.Black, rect, strFormat);

                strFont = new Font("Calibri", 10, FontStyle.Underline);
                rect = new Rectangle(nStartPoint + 40, (int)yPos, nPageWidth, strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Email_addr, strFont, Brushes.Blue, rect, strFormat);

                yPos += strFont.Height * 2;

                strFont = new Font("Calibri", 10);
                rect = new Rectangle(nStartPoint, (int)yPos, nPageWidth, strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Information_qr, strFont, Brushes.Black, rect, strFormat);
                yPos += strFont.Height;

                rect = new Rectangle(nStartPoint, (int)yPos, nPageWidth, strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Information_qr1, strFont, Brushes.Black, rect, strFormat);

                strFont = new Font("Calibri", 10, FontStyle.Underline);
                rect = new Rectangle(nStartPoint + 65, (int)yPos, nPageWidth, strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Eservice_URL, strFont, Brushes.Blue, rect, strFormat);

                strFont = new Font("Calibri", 10);
                rect = new Rectangle(nStartPoint + 158, (int)yPos, nPageWidth, strFont.Height);
                e.Graphics.DrawString(JEWELRY_NUBES.Properties.Resource_Print.Information_qr2, strFont, Brushes.Black, rect, strFormat);
                yPos += strFont.Height *2 ;

                ResourceManager rm = JEWELRY_NUBES.Properties.Resources.ResourceManager;
                Bitmap qr_web = (Bitmap)rm.GetObject("NEW_QR");

                e.Graphics.DrawImage((Image)qr_web, new Rectangle(nStartPoint, (int)yPos, qr_web.Width, qr_web.Height));
                yPos += qr_web.Height;


            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public string getDocId(string docid)
        {
            string[] arrDocId = docid.Split('.');

            string strDocId = "";
            for (int i = 0; i < arrDocId.Length; i++)
            {
                strDocId += arrDocId[i];
            }

            return strDocId;
        }
    }
}
