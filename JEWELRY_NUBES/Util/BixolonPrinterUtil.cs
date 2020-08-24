using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using JEWELRY_NUBES;
using System.Drawing;
using OposPOSPrinter_CCO;
using Newtonsoft.Json.Linq;
using ZXing;
using JEWELRY_NUBES.Util;

namespace JEWELRY_NUBES.Util
{
    class BixolonPrinterUtil
    {
        const long OPOS_SUCCESS = 0;
        const long OPOS_E_CLOSED = 101;
        const long OPOS_E_CLAIMED = 102;
        const long OPOS_E_NOTCLAIMED = 103;
        const long OPOS_E_NOSERVICE = 104;
        const long OPOS_E_DISABLED = 105;
        const long OPOS_E_ILLEGAL = 106;
        const long OPOS_E_NOHARDWARE = 107;
        const long OPOS_E_OFFLINE = 108;
        const long OPOS_E_NOEXIST = 109;
        const long OPOS_E_EXISTS = 110;
        const long OPOS_E_FAILURE = 111;
        const long OPOS_E_TIMEOUT = 112;
        const long OPOS_E_BUSY = 113;
        const long OPOS_E_EXTENDED = 114;
        const int PTR_S_RECEIPT = 2;

        const int PTR_BC_CENTER = -2;
        const int PTR_BCS_Code128 = 110;

        const int PTR_BC_TEXT_NONE = -11;
        const int PTR_BC_TEXT_ABOVE = -12;
        const int PTR_BC_TEXT_BELOW = -13;

        const int PTR_BMT_BMP = 1;
        const int PTR_BM_LEFT = -1;
        const int PTR_BM_CENTER = -2;
        const int PTR_BM_RIGHT = -3;

        const int PTR_BM_ASIS = -11;  // One pixel per printer dot

        OposPOSPrinter_CCO.OPOSPOSPrinter axOPOSPOSPrinter1 = null;

        public Boolean PrintOPOS(string strPrinterName, string parameter, int flag)
        {
            Boolean bRet = true;
            string strErr = "";
            try
            {
                if (axOPOSPOSPrinter1 == null)
                    axOPOSPOSPrinter1 = new OposPOSPrinter_CCO.OPOSPOSPrinter();
                int lRet = axOPOSPOSPrinter1.Open(strPrinterName); // LDN

                if (lRet == OPOS_SUCCESS)
                {
                    lRet = axOPOSPOSPrinter1.ClaimDevice(0);
                    if (lRet == OPOS_SUCCESS)
                    {
                        axOPOSPOSPrinter1.DeviceEnabled = true;
                        axOPOSPOSPrinter1.FlagWhenIdle = true;

                        string strCharLists = axOPOSPOSPrinter1.RecBarCodeRotationList;
                        //StrCharLists is pair (Font A, Font B)//F312 48,64
                        //m_ctlPosPrinter1.SetRecLineChars(48); //select a Font A
                        axOPOSPOSPrinter1.RecLineChars = 64; //select a Font B
                        axOPOSPOSPrinter1.AsyncMode = false;

                        ///////////////////////////////////////////////////////
                        //Constants.LOGGER_MAIN.Info("flag : " + flag + "-->" + parameter);
                        JObject json = JObject.Parse(parameter);

                        if (flag == 1 || flag == 3) // Issue
                        {
                            if(flag == 3)
                            {
                                axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|2C" + ESC + "|cA" + "** Re-print **" + CRLF);
                            }

                            PrintHeader();
                            PrintRetailerDetails(json["retailer_name"].ToString(), json["gst_no"].ToString());
                            PrintDocIdDetails(json["doc_id"].ToString(), json["issue_date"].ToString());
                            TouristPassportDetails(json["passport_number"].ToString(), json["country_code"].ToString());
                            PurchaseDetails(json["purchase_list"].ToString());
                            TotalAmountDetails(json["sales_amt"].ToString(), json["gst_amt"].ToString(), json["service_amt"].ToString(), json["refund_amt"].ToString());
                            Identifier(json["doc_id"].ToString(), json["token_type"].ToString(), json["token_display"].ToString());
                            PrintFooter(json["doc_id"].ToString());
                        }else if(flag == 2)         // Void
                        {
                            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, CRLF);
                            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|2C" + ESC + "|cA" + "** Void **" + CRLF);
                            PrintRetailerDetails(json["retailer_name"].ToString(), json["gst_no"].ToString());
                            Void_PrintDocIdDetails(json["doc_id"].ToString(), json["issue_date"].ToString());
                            Void_PurchaseDetails(json["purchase_list"].ToString());
                            Void_TotalAmountDetails(json["sales_amt"].ToString(), json["gst_amt"].ToString(), json["service_amt"].ToString(), json["refund_amt"].ToString());
                            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, CRLF + CRLF + CRLF + CRLF + CRLF + CRLF);
                        }
                        ///////////////////////////////////////////////////////

                        axOPOSPOSPrinter1.CutPaper(95);
                    }
                    else
                    {
                        strErr = "OPOSPrinter Claim Error : " + axOPOSPOSPrinter1.ErrorString;
                        //AfxMessageBox(strErr);
                        // return FALSE;
                    }
                    axOPOSPOSPrinter1.Close();
                }
                else
                {
                    strErr = "OPOSPrinter Open Error : " + axOPOSPOSPrinter1.ErrorString;
                    // AfxMessageBox(strErr);
                    //return FALSE;
                }
            }
            catch (Exception ex)
            {
                if (axOPOSPOSPrinter1 != null)
                    strErr = "OPOSPrinter Open Error : " + axOPOSPOSPrinter1.ErrorString;
                else
                    strErr = "OPOSPrinter is null Error :" + ex.Message;

                Constants.LOGGER_MAIN.Error(strErr);
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }
            return bRet;
        }

        string CRLF = "\r\n";
        string ESC = "\x1b";

        string strBold = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27 }) + "|bC";
        string strCenterJustify = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'c', (byte)'A' });
        string strRightJustify = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'r', (byte)'A' });
        string strCutComm = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'9', (byte)'5', (byte)'P' });

        void PrintHeader()
        {
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|cA" + ESC + "|N" + CRLF);
            axOPOSPOSPrinter1.PrintBitmap(PTR_S_RECEIPT, Constants.PATH_ROOT + "..//Image//gtfhead.bmp", PTR_BM_ASIS, PTR_BM_CENTER);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|N" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|2C" + ESC + "|cA" + "ELECTONIC TOURIST" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|2C" + ESC + "|cA" + "REFUND TICKET" + CRLF);

            string temp = " " + JEWELRY_NUBES.Properties.Resource_Print.Title_CentralRefundAgency;
            temp = temp.PadRight(28);

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "----------------------------------------------------------" + CRLF);
            //axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|2hC" + ESC + "|rvC" + temp + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|2hC" + temp + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "----------------------------------------------------------" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|N");

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|cA" + "GLOBAL TAX FREE Pte.Ltd." + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|cA" + "541 Orchard Road #17-01 Liat Towers," + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|cA" + "Singapore 238881" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|cA" + "Tel : +65 6221 7058" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|cA" + "GST Reg. No. 201136344W" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|cA" + "www.global-taxfree.com.sg" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|N");
        }

        void PrintRetailerDetails(string retailer_name, string gst_no)
        {
            string temp = " " + JEWELRY_NUBES.Properties.Resource_Print.Title_RetailerDetails;
            temp = temp.PadRight(28);

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "----------------------------------------------------------" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|2hC" + temp + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "----------------------------------------------------------" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|N");

            temp = retailer_name;
            temp = temp.PadLeft(40);

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Retailer Name : " + temp + CRLF);

            temp = gst_no;
            temp = temp.PadLeft(43);

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "GST Reg No : " + temp);
        }

        void PrintDocIdDetails(string foramt_doc_id , string issue_date)
        {
            string temp = " " + JEWELRY_NUBES.Properties.Resource_Print.Title_DocId;
            temp = temp.PadRight(28);

            string doc_id = foramt_doc_id.Replace(".", "");

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "----------------------------------------------------------" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|2hC" + temp + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "----------------------------------------------------------" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|N");
            axOPOSPOSPrinter1.PrintBarCode(PTR_S_RECEIPT, doc_id, PTR_BCS_Code128, 100, 100, PTR_BC_CENTER, PTR_BC_TEXT_NONE);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, CRLF);

            temp = foramt_doc_id;
            temp = temp.PadLeft(47);

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Doc-ID : " + temp + CRLF);

            temp = issue_date;
            temp = temp.PadLeft(40);

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Date of Issue : " + temp + CRLF);
        }

        void Void_PrintDocIdDetails(string foramt_doc_id, string issue_date)
        {
            string temp = " " + JEWELRY_NUBES.Properties.Resource_Print.Title_DocId;
            temp = temp.PadRight(28);

            string doc_id = foramt_doc_id.Replace(".", "");

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "----------------------------------------------------------" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|2hC" + temp + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "----------------------------------------------------------" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|N");

            temp = foramt_doc_id;
            temp = temp.PadLeft(47);

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Doc-ID : " + temp + CRLF);

            temp = issue_date;
            temp = temp.PadLeft(40);

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Date of Issue : " + temp + CRLF);
        }

        void TouristPassportDetails(string passport_no, string country_code)
        {
            string temp = " " + JEWELRY_NUBES.Properties.Resource_Print.Title_TouristPassport;
            temp = temp.PadRight(28);

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "----------------------------------------------------------" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|2hC" + temp + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "----------------------------------------------------------" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|N");

            temp = passport_no;
            temp = temp.PadLeft(47);

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Number : " + temp + CRLF);

            temp = country_code;
            temp = temp.PadLeft(42);

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Nationality : " + temp + CRLF);
        }

        void PurchaseDetails(string purchase_list)
        {
            string temp = " " + JEWELRY_NUBES.Properties.Resource_Print.Title_Purchase;
            temp = temp.PadRight(28);

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "----------------------------------------------------------" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|2hC" + temp + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "----------------------------------------------------------" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|N");

            JArray a = JArray.Parse(purchase_list);
            foreach (JObject json in a.Children<JObject>())
            {
                temp = json["receipt_date"].ToString();
                temp = temp.PadLeft(40);

                axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Date of Sales : " + temp + CRLF);

                temp = json["receipt_number"].ToString();
                temp = temp.PadLeft(45);

                axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Receipt No." + temp + CRLF);

                decimal gross_amount = decimal.Parse(json["gross_amount"].ToString());
                temp = string.Format("{0}", gross_amount.ToString("#,###.#0"));
                temp = temp.PadLeft(45);

                axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Amount(SGD)" + temp + CRLF + CRLF);
            }
        }

        void Void_PurchaseDetails(string purchase_list)
        {
            string temp = " " + JEWELRY_NUBES.Properties.Resource_Print.Title_Purchase;
            temp = temp.PadRight(28);

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "----------------------------------------------------------" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|2hC" + temp + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "----------------------------------------------------------" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|N");

            JArray a = JArray.Parse(purchase_list);
            foreach (JObject json in a.Children<JObject>())
            {
                temp = json["receipt_date"].ToString();
                temp = temp.PadLeft(40);

                axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Date of Sales : " + temp + CRLF);

                temp = json["receipt_number"].ToString();
                temp = temp.PadLeft(45);

                axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Receipt No." + temp + CRLF);

                decimal purchase_amt = decimal.Parse(json["gross_amount"].ToString());
                temp = "-" + string.Format("{0}", purchase_amt.ToString("#,###.#0"));
                temp = temp.PadLeft(45);

                axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Amount(SGD)" + temp + CRLF + CRLF);
            }
        }

        void TotalAmountDetails(string sales_amt, string gst_amt, string service_amt, string refund_amt)
        {
            string temp = "";
            decimal tmp_amt = 0;

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|N");

            tmp_amt = decimal.Parse(sales_amt);
            temp = string.Format("{0}", tmp_amt.ToString("#,###.#0"));
            temp = temp.PadLeft(32);

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Sales Amount Incl GST : " + temp + CRLF);

            tmp_amt = decimal.Parse(gst_amt);
            temp = string.Format("{0}", tmp_amt.ToString("#,###.#0"));
            temp = temp.PadLeft(43);

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "GST Amount : " + temp + CRLF);

            tmp_amt = decimal.Parse(service_amt);
            temp = string.Format("{0}", tmp_amt.ToString("#,###.#0"));
            temp = temp.PadLeft(42);

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Service Fee : " + temp + CRLF);

            tmp_amt = decimal.Parse(refund_amt);
            temp = string.Format("{0}", tmp_amt.ToString("#,###.#0"));
            temp = temp.PadLeft(31);

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Provisional Refund Amount" + temp + CRLF + CRLF);

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Provisional refund amounts are subject to eligibility" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "check and approval." + CRLF + CRLF);
        }

        void Void_TotalAmountDetails(string sales_amt, string gst_amt, string service_amt, string refund_amt)
        {
            string temp = "";
            decimal tmp_amt = 0;

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|N");

            tmp_amt = decimal.Parse(sales_amt);
            temp = "-" + string.Format("{0}", tmp_amt.ToString("#,###.#0"));
            temp = temp.PadLeft(32);

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Sales Amount Incl GST : " + temp + CRLF);

            tmp_amt = decimal.Parse(gst_amt);
            temp = "-" + string.Format("{0}", tmp_amt.ToString("#,###.#0"));
            temp = temp.PadLeft(43);

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "GST Amount : " + temp + CRLF);

            tmp_amt = decimal.Parse(service_amt);
            temp = "-" + string.Format("{0}", tmp_amt.ToString("#,###.#0"));
            temp = temp.PadLeft(42);

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Service Fee : " + temp + CRLF);

            tmp_amt = decimal.Parse(refund_amt);
            temp = "-" + string.Format("{0}", tmp_amt.ToString("#,###.#0"));
            temp = temp.PadLeft(31);

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Provisional Refund Amount" + temp + CRLF + CRLF);
        }

        void Identifier(string foramt_doc_id, string token_type, string display_value)
        {
            string temp = " " + JEWELRY_NUBES.Properties.Resource_Print.Title_Identifier;
            temp = temp.PadRight(28);

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "----------------------------------------------------------" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|2hC" + temp + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "----------------------------------------------------------" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|N");

            if (token_type.Equals("C"))
            {
                temp = display_value.Trim();
                temp = temp.PadLeft(45);

                axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Credit Card" + temp+ CRLF + CRLF);
            }
            else if (token_type.Equals("B"))
            {
                temp = display_value.Trim();
                temp = temp.PadLeft(46);
                axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Debit Card" + temp + CRLF + CRLF);
            }
            else if (token_type.Equals("O"))
            {
                temp = display_value.Trim();
                temp = temp.PadLeft(46);
                axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Other Card" + temp + CRLF + CRLF);
            }
            else
            {
                axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Doc-ID" + CRLF + CRLF);
            }

            temp = " " + JEWELRY_NUBES.Properties.Resource_Print.Title_Information;
            temp = temp.PadRight(28);
        }

        void PrintFooter(string foramt_doc_id)
        {
            string temp = " " + JEWELRY_NUBES.Properties.Resource_Print.Title_Information;
            temp = temp.PadRight(28);

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "----------------------------------------------------------" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|2hC" + temp + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "----------------------------------------------------------" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|N");
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Visit touristrefund.sg" + CRLF);

            temp = foramt_doc_id;
            temp = temp.PadLeft(47);

            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, "Doc-ID : " + temp + CRLF + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|bC" + "Please keep this Ticket, the receipts and goods for" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|bC" + "Customs' insepction." + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|bC" + "Your personal data may be used for purpose of tax" + CRLF);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, ESC + "|bC" + "refunds." + CRLF);
            axOPOSPOSPrinter1.PrintBitmap(PTR_S_RECEIPT, Constants.PATH_ROOT + "..//Image//NEW_QR.bmp", PTR_BM_ASIS, PTR_BM_CENTER);
            axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, CRLF + CRLF + CRLF + CRLF + CRLF + CRLF);

            //Print 'advance and cut' escape command.
            //// 출력 후 전표 컷팅 (95%)
            //PrintTextLine(axOPOSPOSPrinter1, ESC + "|98P");
        }

        public void PrintLineItem(OposPOSPrinter_CCO.OPOSPOSPrinter printer, string itemCode, int quantity, double unitPrice)
        {
            PrintText(printer, TruncateAt(itemCode.PadRight(9), 9));
            PrintText(printer, TruncateAt(quantity.ToString("#0.00").PadLeft(9), 9));
            PrintText(printer, TruncateAt(unitPrice.ToString("#0.00").PadLeft(10), 10));
            PrintTextLine(printer, TruncateAt((quantity * unitPrice).ToString("#0.00").PadLeft(10), 10));
        }

        public void PrintText(OposPOSPrinter_CCO.OPOSPOSPrinter printer, string text)
        {
            if (text.Length <= printer.RecLineChars)
                printer.PrintNormal(PTR_S_RECEIPT, text); //Print text
            else if (text.Length > printer.RecLineChars)
                printer.PrintNormal(PTR_S_RECEIPT, TruncateAt(text, printer.RecLineChars)); //Print exactly as many characters as the printer allows, truncating the rest.
        }

        public void PrintTextLine(OposPOSPrinter_CCO.OPOSPOSPrinter printer, string text)
        {
            if (text.Length < printer.RecLineChars)
                printer.PrintNormal(PTR_S_RECEIPT, text + Environment.NewLine); //Print text, then a new line character.
            else if (text.Length > printer.RecLineChars)
                printer.PrintNormal(PTR_S_RECEIPT, TruncateAt(text, printer.RecLineChars)); //Print exactly as many characters as the printer allows, truncating the rest, no new line character (printer will probably auto-feed for us)
            else if (text.Length == printer.RecLineChars)
                printer.PrintNormal(PTR_S_RECEIPT, text + Environment.NewLine); //Print text, no new line character, printer will probably auto-feed for us.
        }

        public string TruncateAt(string text, int maxWidth)
        {
            string retVal = text;
            if (text.Length > maxWidth)
                retVal = text.Substring(0, maxWidth);

            return retVal;
        }
    }
}
