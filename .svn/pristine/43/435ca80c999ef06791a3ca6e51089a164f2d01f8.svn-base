using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Collections;
using System.Resources;
using MetroFramework;
using MetroFramework.Forms;
using Newtonsoft.Json.Linq;
using log4net;
using GTF_Comm;
using GTF_Passport;
using JEWELRY_NUBES.Util;
using GTF_Printer;
using JEWELRY_NUBES.Properties;
using JEWELRY_NUBES.Tran;
using JEWELRY_NUBES.Util;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using System.Threading;

namespace JEWELRY_NUBES.Screen
{
    public partial class IssuePanel_SG : UserControl
    {
        static ItemForm itemForm = new ItemForm();
        static CardForm cardForm = new CardForm();
        static SealBag sealForm = new SealBag();

        ControlManager m_CtlSizeManager = null;
        GTF_PassportScanner m_passScan = null;

        private Image[] StatusImgs;
        private String gender = "";
        private String date_of_birth = "";
        private String expiry_date = "";
        private String dup_rc = "N";
        private String check_yn = "N";
        private bool check_amt = false;
        ArrayList itemArrayList = new ArrayList();
        Thread scan_worker = null;
        Thread issue_worker = null;
        ProgressFrom proForm;


        public IssuePanel_SG(ILog Logger = null)
        {
            InitializeComponent();
            //최초생성시 좌표, 크기 조정여부 등록함. 화면별로 Manager 를 가진다. 
            m_CtlSizeManager = new ControlManager(this);

            //횡좌표이동
            //m_CtlSizeManager.addControlMove(BTN_PASSPORT_MANUAL, true, false, false, false);
            //m_CtlSizeManager.addControlMove(BTN_ISSUE, true, false, false, false);
            //m_CtlSizeManager.addControlMove(BTN_CLEAR, true, false, false, false);
            //m_CtlSizeManager.addControlMove(BTN_SCAN, true, false,false,false);
            //m_CtlSizeManager.addControlMove(BTN_ITEM_ADD, true, false, false, false);
            //m_CtlSizeManager.addControlMove(BTN_ITEM_DEL, true, false, false, false);

            //횡늘림
            m_CtlSizeManager.addControlMove(TIL_1, false, false, true, false);
            m_CtlSizeManager.addControlMove(LAY_PASSPORT, false, false, true, false);
            m_CtlSizeManager.addControlMove(TIL_2, false, false, true, false);
            //m_CtlSizeManager.addControlMove(LAY_ISSUE_INFO, false, false, true, false);

            //종횡 늘림
            //m_CtlSizeManager.addControlMove(GRD_ITEMS, false, false, true, true);

            //화면 디스크립트 변경
            m_CtlSizeManager.ChageLabel(this);

            sealForm.closeSealback += new SealBag.closeSealbackDelegate(closeSealBack);

            ResourceManager rm = Properties.Resources.ResourceManager;
            StatusImgs = new Image[] { (Bitmap)rm.GetObject("approved"), (Bitmap)rm.GetObject("rejected") };

            cmbbox_Token.Items.Add("Doc-ID");
            cmbbox_Token.Items.Add("Card");
            cmbbox_Token.SelectedIndex = 0;
            txt_DisplayCardNo.Visible = false;
            txt_DisplayCardNo.Text = "";
            txt_CardNo.Text = "";

            cardForm.addCardNoDetails += new CardForm.addCardNoDetailslDelegate(addCardNo);
            cardForm.closeCardNoForm += new CardForm.closeCardNoFormDelegate(closeCardNoForm);
            this.BTN_SCAN.Click += new System.EventHandler(this.BTN_SCAN_Click);
            AutoCompleteCountry();

            txt_PurchaseDate.Value = new DateTime(int.Parse(DateTime.Now.ToString("yyyy")),
                         int.Parse(DateTime.Now.ToString("MM")),
                         int.Parse(DateTime.Now.ToString("dd")));

            txt_PurchaseAmt.TextChanged += new System.EventHandler(txt_PurchaseAmt_TextChanged);
            txt_PurchaseAmt.Text = "0.00";
            txt_PurchaseAmt.SelectionStart = txt_PurchaseAmt.Text.Length;

            txt_Quantity.Mask = "90";
            txt_Quantity.Text = "1";

            // item정보 셋팅
            //addItemCode();

            cmbbox_PurchateItem.SelectedIndex = 0;
            txt_PurchaseAmt.Focus();

            if (Constants.REC_PREFIX != "")
            {
                txt_ReceiptNo.Text = Constants.REC_PREFIX;
            }

            /*
            bg.DoWork += new DoWorkEventHandler(bg_DoWork);
            bg.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);
            bg.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            bg.WorkerReportsProgress = true;
            bg.WorkerSupportsCancellation = true;
            */
            //setStoreName();

        }

        private void addCardNo()
        {
            txt_CardNo.Text = cardForm.Controls["LAY_CARD"].Controls["txt_CardNo"].Text;
            txt_DisplayCardNo.Text = cardForm.Controls["LAY_CARD"].Controls["txt_DisplayCardNo"].Text;
            cardForm.Hide();

            if (txt_CardNo.Text.Equals(""))
            {
                cmbbox_Token.SelectedIndex = 0;
                txt_DisplayCardNo.Visible = false;
                txt_DisplayCardNo.Text = "";
                txt_CardNo.Text = "";
            }
            txt_Dummy.Focus();
        }

        private void closeCardNoForm()
        {
            cardForm = new CardForm();
            cardForm.addCardNoDetails += new CardForm.addCardNoDetailslDelegate(addCardNo);
            cardForm.closeCardNoForm += new CardForm.closeCardNoFormDelegate(closeCardNoForm);
        }


        private void IssuePanel_Load(object sender, EventArgs e)
        {
            m_passScan = GTF_PassportScanner.Instance();
            txt_PurchaseAmt.Focus();
        }


        private void BTN_SCAN_Click(object sender, EventArgs e)
        {
            try
            {
                this.BTN_SCAN.Click -= new System.EventHandler(this.BTN_SCAN_Click);
                BTN_SCAN.Enabled = false;
                if (txt_PassportNo.Text.Length > 0)
                {
                    return;
                }
                
                //scan_worker = new Thread(run_progress);
                //scan_worker.Start();
                txt_PassportNo.Text = "";
                txt_CountryCode.Text = "";
                txt_FirstName.Text = "";
                txt_LastName.Text = "";
                expiry_date = "";
                date_of_birth = "";
                gender = "";

                Utils gtfUtil = new Utils();

                setWaitCursor(true);
                m_passScan.close();
                if (m_passScan.open(Constants.PASSPORT_TYPE) > 0)
                {
                    int strmrz = m_passScan.scan(Constants.SCAN_TIMEOUT);
                    if (strmrz > 0)
                    {
                        //싱가폴
                        if (m_passScan.GetNationality().Equals("SGP"))
                        {
                            MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidatePassportSGP"), "Passport Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            txt_PassportNo.Text = m_passScan.GetPassportNo();
                            txt_CountryCode.Text = m_passScan.GetNationality();
                            txt_FirstName.Text = m_passScan.GetPassportLastName();
                            txt_LastName.Text = m_passScan.GetPassportFirstName();
                            expiry_date = "20" + m_passScan.GetExpireDate();
                            date_of_birth = gtfUtil.getFullDate(m_passScan.GetBirthDate());
                            gender = m_passScan.GetSex();
                            txt_PurchaseAmt.Focus();
                        }

                    }
                    else
                    {
                        MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/PassportReadError"), "Passport Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/PassportConnectError"), "Passport Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                m_passScan.close();
                BTN_SCAN.Enabled = true;
                setWaitCursor(false);
                /*
                if(scan_worker.IsAlive)
                {
                    scan_worker.Abort();
                }
                */
                this.BTN_SCAN.Click += new System.EventHandler(this.BTN_SCAN_Click);
            }
        }

        private void IssueTicket_SingleReceipt()
        {
            try
            {
                Transaction tran = new Transaction();

                //MessageBox.Show(GRD_ITEMS[2, nIndex].Value.ToString());

                string strReq = string.Empty;
                string strRes = string.Empty;
                string message = "";

                JObject jsonReq = new JObject();
                JObject jsonRes = null;

                jsonReq.Add("status", "Issue");
                jsonReq.Add("tid", Constants.TID);
                jsonReq.Add("mid", Constants.MID);
                jsonReq.Add("userId", Constants.USER_ID);
                if (!txt_PassportNo.Text.Equals(""))
                {
                    jsonReq.Add("passport_number", txt_PassportNo.Text);
                }

                if (!txt_CountryCode.Text.Equals(""))
                {
                    jsonReq.Add("country_code", txt_CountryCode.Text);
                }

                if (!txt_LastName.Text.Equals(""))
                {
                    jsonReq.Add("last_name", txt_LastName.Text);
                }

                if (!txt_FirstName.Text.Equals(""))
                {
                    jsonReq.Add("first_name", txt_FirstName.Text);
                }

                if (!expiry_date.Equals(""))
                {
                    jsonReq.Add("expiry_date", expiry_date);
                }

                if (!date_of_birth.Equals(""))
                {
                    jsonReq.Add("date_of_birth", date_of_birth);
                }

                if (!gender.Equals(""))
                {
                    jsonReq.Add("gender", gender);
                }

                jsonReq.Add("check_yn", check_yn);

                //////////////////////////////////////////////////////////////////////////////////////////////
                JArray jsonArrary = new JArray();
                JObject jsonReceipt = new JObject();

                jsonReceipt.Add("description", cmbbox_PurchateItem.Text);

                string gross_amount = txt_PurchaseAmt.Text.Replace(",", "");
                float sale_amt = float.Parse(gross_amount);
                if (sale_amt < (float)100.00)
                {
                    message = string.Format(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/CheckMinimumSalesAmount"), Environment.NewLine);
                    MessageBox.Show(message);
                    return;
                }

                jsonReceipt.Add("gross_amount", gross_amount);
                jsonReceipt.Add("quantity", txt_Quantity.Text);

                string receipt_date = txt_PurchaseDate.Text.Replace("/", "");
                receipt_date = Util.Utils.FormatConvertDate(receipt_date);

                jsonReceipt.Add("receipt_date", receipt_date);
                jsonReceipt.Add("receipt_number", txt_ReceiptNo.Text);
                //////////////////////////////////////////////////////////////////////////////////////////////
                jsonArrary.Add(jsonReceipt);

                jsonReq.Add("purchase_list", jsonArrary.ToString());

                jsonReq.Add("outlet", Constants.OUTLET_TYPE);

                // sign
                jsonReq.Add("sign", Util.Utils.CreateMD5(Constants.MID + Constants.APPEND_KEY));

                // Token
                jsonReq = MakeTokenData(jsonReq);

                strRes = tran.issueTicket(jsonReq.ToString());

                jsonRes = JObject.Parse(strRes);



                if (jsonRes["code"].ToString().Equals("00"))
                {
                    //Constants.LOGGER_DOC.Info("Start Print(Single) DOC ID" + jsonRes["formatted_docid"].ToString() );
                    PrintTicket(jsonRes);
                    if (check_yn == "Y")
                    {
                        sealForm.docid = jsonRes["docid"].ToString();
                        sealForm.ShowDialog(this);
                        sealForm.initialize();
                    }
                    else
                    {
                        CLEAR();
                    }
                }
                else
                {

                    if (jsonRes["message"].ToString().Equals("duplicated receipt number!") || jsonRes["message"].ToString().Equals("unregistered terminal!"))
                    {
                        MessageBox.Show(jsonRes["message"].ToString());
                    }
                    else
                    {
                        MessageBox.Show("Failed");
                    }
                    Constants.LOGGER_MAIN.Error(jsonRes["message"].ToString());
                }

            }
            catch (Exception ex)
            {
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
                //MessageBox.Show(ex.Message);
            }
        }

        private JObject MakeTokenData(JObject jsonReq)
        {
            JObject json = jsonReq;

            try
            {
                //////////////////////////////////////////////////////////////////////////////////////////////
                // Token
                //////////////////////////////////////////////////////////////////////////////////////////////
                if (!txt_CardNo.Text.Equals(""))
                {
                    string card_no = txt_CardNo.Text;

                    string token_type = "";
                    string token_display = "";
                    string token_encrypted = "";
                    string token_hashed = "";
                    string token_maked = "";
                    string token_key_id = "";
                    string token_lookup = "";

                    EncryptUtil encUtil = new EncryptUtil();

                    int check_token_type = encUtil.checkCardBIN(long.Parse(card_no));
                    if (check_token_type > 0)
                    {
                        switch (check_token_type)
                        {
                            case 1:
                                token_type = "C";
                                break;
                            case 2:
                                token_type = "B";
                                break;
                            default:
                                token_type = "O";
                                break;
                        }

                        if (token_type.Equals("C"))
                        {
                            X509Certificate2 CERT_GTF_CC = encUtil.GetCertificateFromStore("GTF_CAT_AUTH");
                            Org.BouncyCastle.X509.X509Certificate cert_cch_cc = DotNetUtilities.FromX509Certificate(CERT_GTF_CC);

                            byte[] cipherData = encUtil.encryptRsa(Encoding.UTF8.GetBytes(card_no), cert_cch_cc);

                            token_encrypted = Convert.ToBase64String(cipherData);
                            token_key_id = encUtil.getSKID(cert_cch_cc);
                            token_hashed = Convert.ToBase64String(encUtil.hashData(Encoding.UTF8.GetBytes(card_no)));
                            token_maked = txt_DisplayCardNo.Text.Replace("-", "");
                            token_display = txt_DisplayCardNo.Text.Replace("-", "");
                        }

                        if (token_type.Equals("C") || token_type.Equals("B"))
                        {
                            token_lookup = token_type + "+" + Convert.ToBase64String(encUtil.hashData(Encoding.UTF8.GetBytes(card_no)));
                        }
                        else if (token_type.Equals("O"))
                        {
                            token_lookup = "O" + card_no;
                            token_display = txt_DisplayCardNo.Text.Replace("-", "");
                        }

                        ///////////////////////////////////////////////////////////////

                        jsonReq.Add("token_type", token_type);
                        jsonReq.Add("token_lookup", token_lookup);
                        jsonReq.Add("token_display", token_display);
                        jsonReq.Add("token_encrypted", token_encrypted);
                        jsonReq.Add("token_hashed", token_hashed);
                        jsonReq.Add("token_maked", token_maked);
                        jsonReq.Add("token_key_id", token_key_id);
                    }
                }
            }
            catch (Exception ex)
            {
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
                //MessageBox.Show(ex.Message);
            }
            finally
            {

            }

            return json;
        }

        private void PrintTicket(JObject jsonRes)
        {
            try
            {
                setWaitCursor(true);

                string retailer = "";
                string docid = "";
                string tourist = "";
                string purchase = "";
                string token = "";
                if (Constants.PRINTER_SELECT == "OPOS Printer")
                {

                    if (Constants.PRINTER_OPOS_TYPE != null)
                    {
                        JObject json = new JObject();
                        json.Add("retailer_name", jsonRes["retailer_name"].ToString());
                        json.Add("gst_no", jsonRes["retailer_gstno"].ToString());
                        json.Add("doc_id", jsonRes["formatted_docid"].ToString());

                        string issue_date = Utils.FormatDate(jsonRes["date_of_issue"].ToString()) + " " + Utils.FormatTime(jsonRes["time_of_issue"].ToString());

                        json.Add("issue_date", issue_date);

                        if (jsonRes["passport_number"] != null)
                            json.Add("passport_number", jsonRes["passport_number"].ToString());
                        else
                            json.Add("passport_number", "");

                        if (jsonRes["country_code"] != null)
                            json.Add("country_code", jsonRes["country_code"].ToString());
                        else
                            json.Add("country_code", "");

                        JArray jsonArrary = new JArray();

                        JArray tmp_arr = JArray.Parse(jsonRes["purchase_list"].ToString());

                        int i = 0;
                        foreach (JObject tmp_obj in tmp_arr.Children<JObject>())
                        {
                            i++;
                            JObject jsonReceipt = new JObject();

                            jsonReceipt.Add("number", i);
                            jsonReceipt.Add("receipt_number", tmp_obj["receipt_number"].ToString());
                            jsonReceipt.Add("receipt_date", Utils.FormatDate(tmp_obj["receipt_date"].ToString()));
                            jsonReceipt.Add("gross_amount", tmp_obj["gross_amount"].ToString());
                            jsonReceipt.Add("quantity", tmp_obj["quantity"].ToString());
                            jsonReceipt.Add("description", tmp_obj["description"].ToString());
                            jsonArrary.Add(jsonReceipt);
                        }

                        json.Add("purchase_list", jsonArrary.ToString());

                        json.Add("sales_amt", jsonRes["sales_amount"].ToString());
                        json.Add("gst_amt", jsonRes["gst_amount"].ToString());
                        json.Add("service_amt", jsonRes["service_amount"].ToString());
                        json.Add("refund_amt", jsonRes["refund_amount"].ToString());

                        if (jsonRes["token_type"] != null)
                        {
                            json.Add("token_type", jsonRes["token_type"].ToString());
                        }

                        if (jsonRes["token_display"] != null)
                        {
                            if (jsonRes["token_type"].ToString().Equals("C"))
                            {
                                String[] token_text = jsonRes["token_display"].ToString().Split(new String[] { "****" }, StringSplitOptions.RemoveEmptyEntries);
                                json.Add("token_display", "****-****-****-" + token_text[token_text.Length - 1].ToString());
                            }
                            else
                            {
                                json.Add("token_display", jsonRes["token_display"].ToString());
                            }
                        }


                        BixolonPrinterUtil printer = new BixolonPrinterUtil();

                        printer.PrintOPOS(Constants.PRINTER_OPOS_TYPE, json.ToString(), 1);
                    }
                }
                else
                {
                    if (Constants.PRINTER_TYPE != null)
                    {

                        retailer = jsonRes["retailer_name"].ToString() + "|" + jsonRes["retailer_gstno"].ToString() + "|" + jsonRes["retailer_addr"].ToString();

                        docid = jsonRes["formatted_docid"].ToString() + "|" + Utils.FormatDate(jsonRes["date_of_issue"].ToString()) + " " + Utils.FormatTime(jsonRes["time_of_issue"].ToString());

                        tourist = jsonRes["passport_number"].ToString() + "|" + jsonRes["country_code"].ToString();

                        JArray jsonArrary = new JArray();

                        JArray tmp_arr = JArray.Parse(jsonRes["purchase_list"].ToString());

                        int i = 0;

                        purchase += tmp_arr.Count + "|";
                        foreach (JObject tmp_obj in tmp_arr.Children<JObject>())
                        {
                            i++;
                            JObject jsonReceipt = new JObject();

                            purchase += i + "|";
                            purchase += Utils.FormatDate(tmp_obj["receipt_date"].ToString()) + "|";
                            purchase += tmp_obj["gross_amount"].ToString() + "|";
                            purchase += tmp_obj["receipt_number"].ToString() + "|";
                            purchase += jsonRes["sales_amount"].ToString() + "|" + jsonRes["gst_amount"].ToString() + "|" + jsonRes["service_amount"].ToString() + "|" + jsonRes["refund_amount"].ToString();
                        }

                        if (jsonRes["token_type"] != null)
                        {
                            token = jsonRes["token_type"].ToString() + "|";
                        }

                        if (jsonRes["token_display"] != null)
                        {
                            if (jsonRes["token_type"].ToString().Equals("C"))
                            {
                                String[] token_text = jsonRes["token_display"].ToString().Split(new String[] { "****" }, StringSplitOptions.RemoveEmptyEntries);
                                token += "****-****-****-" + token_text[token_text.Length - 1].ToString();
                            }
                            else
                            {
                                token += jsonRes["token_display"].ToString();
                            }
                        }
                    }

                    WindowPrinterUtil printer = new WindowPrinterUtil();
                    printer.PrintTicket(retailer, docid, tourist, purchase, token, "01");
                }

            }
            catch (Exception ex)
            {
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
                //MessageBox.Show(ex.StackTrace);
            }
            finally
            {
                setWaitCursor(false);
            }
        }


        private void closeSealBack()
        {
            sealForm.Hide();
            CLEAR();
        }

        private void check_buy_amt()
        {


            if (cmbbox_PurchateItem.Text.IndexOf("Jewellery") == -1)
            {
                check_yn = "N";
                check_amt = true;
                lbl_sealbagInfo.Text = "";
                return;
            }

            //issue_worker = new Thread(run_progress);
            //issue_worker.Start();
            string gross_amount = txt_PurchaseAmt.Text.Replace(",", "");
            float sale_amt = float.Parse(gross_amount);
            /*
            decimal ul = new decimal(sale_amt);
            decimal gst;
            
            gst = decimal.Multiply(ul, new decimal(10));
            gst = decimal.Divide(gst, new decimal(10.7));
            gst = decimal.Multiply(gst, new decimal(0.07));
            ul = decimal.Subtract(ul, gst);
            */
            JObject jsonReq = new JObject();
            Transaction tran = new Transaction();

            if (!txt_PassportNo.Text.Equals(""))
                jsonReq.Add("passport_no", txt_PassportNo.Text);

            if (!txt_CountryCode.Text.Equals(""))
                jsonReq.Add("country_code", txt_CountryCode.Text);

            jsonReq.Add("group_id", Constants.GROUP_ID);
            jsonReq.Add("buy_amt", sale_amt);
            try
            {
                string check_result = tran.searchBuyJewelleryAmt(jsonReq.ToString());
                JObject jsonRes = JObject.Parse(check_result);
                check_yn = jsonRes["check_yn"].ToString();
                //string buy_amt = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", double.Parse(jsonRes["buy_amt"].ToString())).Replace("$", "");
                
                if (check_yn == "N")
                {
                    check_amt = true;
                    lbl_sealbagInfo.ForeColor = System.Drawing.Color.Black;
                    //lbl_sealbagInfo.Text = "Total Sales value (excluding GST) is $" + buy_amt  + Environment.NewLine + "               " + Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/NoneedSealbag");
                    lbl_sealbagInfo.Text =    Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/NoneedSealbag");
                }
                else
                {
                    check_amt = true;
                    lbl_sealbagInfo.ForeColor = System.Drawing.Color.Green;
                    //lbl_sealbagInfo.Text = "Total Sales value (excluding GST) is $" + buy_amt + Environment.NewLine + "               " + Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/NeedSealbag");
                    lbl_sealbagInfo.Text = Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/NeedSealbag"); 
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/SearchFaild"), "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
                check_amt = false;
            }
            finally
            {
                /*
                if(issue_worker.IsAlive)
                {
                    issue_worker.Abort();
                }
                */
            }
        }



        private void IssuePanel_SizeChanged(object sender, EventArgs e)
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

        private void BTN_ISSUE_Click(object sender, EventArgs e)
        {
            try
            {
                BTN_ISSUE.Enabled = false;
                setWaitCursor(true);

                if (!ValiateIssue())
                {
                    return;
                }
                
                check_buy_amt();

                lbl_sealbagInfo.Refresh();

                if (check_yn.Equals("Y") && check_amt)
                {
                    var confirmResult = MetroMessageBox.Show(this, "Sealed bag is required! \nyou want Issue eTRS ticket", "Confirm Issue", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirmResult == DialogResult.Yes)
                    {
                        IssueTicket_SingleReceipt();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    IssueTicket_SingleReceipt();
                }

            }
            catch (Exception ex)
            {
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }
            finally
            {
                BTN_ISSUE.Enabled = true;
                setWaitCursor(false);
            }
        }

        private bool ValiateIssue()
        {
            if (txt_ReceiptNo.Text.Length < 4)
            {
                MetroMessageBox.Show(this, "Check your receipt number", "Receipt number ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_ReceiptNo.Focus();
                return false;
            }

            string gross_amount = txt_PurchaseAmt.Text.Replace(",", "");
            float sale_amt = float.Parse(gross_amount);

            if (sale_amt < 100)
            {
                MetroMessageBox.Show(this, "Purchase amount is below $100", "Check Purchase Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_PurchaseAmt.Focus();
                return false;
            }

            if (txt_PassportNo.Text.Equals(""))
            {
                MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidatePassportNo"), "Validate PassportNo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }


            if (!txt_PassportNo.Text.Equals(""))
            {
                if (txt_PassportNo.Text.Length > 10)
                {
                    MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidatePassportNo"), "Validate PassportNo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                else if (txt_PassportNo.Text.Length < 6)
                {
                    MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidatePassportNo"), "Validate PassportNo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            if (txt_CountryCode.Text.Equals("") || txt_CountryCode.Text.Length != 3)
            {
                MetroMessageBox.Show(this, "Check Nationality Code", "Check Passport Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (int.Parse(txt_Quantity.Text) == 0)
            {
                MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidateQuantity"), "Validate Quantity", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (Constants.REC_PREFIX != "")
            {
                String input_prefix = txt_ReceiptNo.Text.Substring(0, Constants.REC_PREFIX.Length);
                if (!Constants.REC_PREFIX.Equals(input_prefix))
                {
                    MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidateRecPrefix"));
                    txt_ReceiptNo.Focus();
                    return false ;
                }
            }

            if (Constants.REC_DIGITS != "" && int.Parse(Constants.REC_DIGITS) != txt_ReceiptNo.Text.Length)
            {
                MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidateRecDigits"));
                txt_ReceiptNo.Focus();
                return false;
            }


            return true;

        }

        private void BTN_CLEAR_Click(object sender, EventArgs e)
        {
            CLEAR();
        }

        public void CLEAR()
        {
            txt_PassportNo.Text = "";
            txt_CountryCode.Text = "";
            txt_FirstName.Text = "";
            txt_LastName.Text = "";
            gender = "";
            date_of_birth = "";
            expiry_date = "";

            cmbbox_Token.SelectedIndex = 0;
            txt_DisplayCardNo.Visible = false;
            txt_DisplayCardNo.Text = "";
            txt_CardNo.Text = "";

            if(cmbbox_PurchateItem.Items.Count != 0)
            {
                cmbbox_PurchateItem.SelectedIndex = 0;
            }
            txt_Quantity.Text = "1";
            txt_PurchaseAmt.Text = "0.00";
            txt_ReceiptNo.Text = "";
            check_yn = "Y";
            lbl_sealbagInfo.Text = "";
            if (sealForm != null)
            {
                sealForm.Hide();
            }
            if (Constants.REC_PREFIX != "")
            {
                txt_ReceiptNo.Text = Constants.REC_PREFIX;
            }

            //txt_Quantity.Enabled = true;
            //cmbbox_PurchateItem.Enabled = true;
            txt_PurchaseAmt.Enabled = true;

            //COM_STORE.SelectedIndex = 0;
            //BTN_ISSUE.Focus();
        }


        private void cmbbox_Token_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbbox_Token.SelectedIndex == 1)     // Card
            {
                txt_DisplayCardNo.Visible = true;
                txt_DisplayCardNo.Text = "";
                txt_CardNo.Text = "";

                cardForm.ShowDialog(this);
                cardForm.initialize();
                cardForm.Controls["LAY_CARD"].Controls["txt_Track2"].Focus();
            }
            else
            {
                txt_DisplayCardNo.Visible = false;
                txt_DisplayCardNo.Text = "";
                txt_CardNo.Text = "";
            }
        }

        public void AutoCompleteCountry()
        {


            txt_CountryCode.AutoCompleteMode = AutoCompleteMode.Suggest;
            txt_CountryCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection storeAcsCollection = new AutoCompleteStringCollection();

            Transaction tran = new Transaction();
            string strRsult = tran.searchCountryCode(txt_CountryCode.Text);

            if (!strRsult.Equals(""))
            {

                JArray a = JArray.Parse(strRsult);
                foreach (JObject json in a.Children<JObject>())
                {
                    storeAcsCollection.Add(json["country_code"].ToString());
                }

            }
            else
            {
                storeAcsCollection = null;

            }
            txt_CountryCode.AutoCompleteCustomSource = storeAcsCollection;
        }

        private void txt_CountryCode_Leave(object sender, EventArgs e)
        {
            if (txt_CountryCode.Text.Length > 3)
                txt_CountryCode.Text = txt_CountryCode.Text.Substring(0, 3);

            if (txt_CountryCode.Text.Equals("SGP"))
            {
                MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidatePassportSGP"), "Passport Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void txt_PassportNo_Leave(object sender, EventArgs e)
        {
            if (!txt_PassportNo.Text.Equals(""))
            {
                if (txt_PassportNo.Text.Length > 10)
                {
                    MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidatePassportNo"));
                }
                else if (txt_PassportNo.Text.Length < 6)
                {
                    MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidatePassportNo"));
                }
            }
        }


        private void txt_ReceiptNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.KeyChar = Char.ToUpper(e.KeyChar);
            }
        }


        private void txt_PurchaseAmt_TextChanged(object sender, EventArgs e)
        {
            //Remove previous formatting, or the decimal check will fail including leading zeros
            string value = txt_PurchaseAmt.Text.Replace(",", "")
                .Replace("$", "").Replace(".", "").TrimStart('0');
            decimal ul;
            //Check we are indeed handling a number
            if (decimal.TryParse(value, out ul))
            {
                ul /= 100;
                //Unsub the event so we don't enter a loop
                txt_PurchaseAmt.TextChanged -= txt_PurchaseAmt_TextChanged;
                //Format the text as currency
                string tmp_value = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", ul);
                txt_PurchaseAmt.Text = tmp_value.Replace("$", "");
                txt_PurchaseAmt.TextChanged += txt_PurchaseAmt_TextChanged;
                txt_PurchaseAmt.Select(txt_PurchaseAmt.Text.Length, 0);
            }
        }

        private void addItemCode()
        {
            cmbbox_PurchateItem.Items.Clear();

            if (itemArrayList.Count == 0 )
            {
                Transaction tran = new Transaction();
                string strRsult = tran.getItemCodeList();
                if (!strRsult.Equals(""))
                {
                    JArray a = JArray.Parse(strRsult);

                    foreach (JObject json in a.Children<JObject>())
                    {
                        itemArrayList.Add(json["code_id"].ToString() + ":" + json["code_name"].ToString());
                    }
                }
            }

            if (Constants.ITEM_CODE1 != null)
            {
                foreach (string item in itemArrayList)
                {
                    if (item.Substring(0, 2).Equals(Constants.ITEM_CODE1))
                    {
                        cmbbox_PurchateItem.Items.Add(item);
                    }
                }
            }

            if (Constants.ITEM_CODE2 != null)
            {
                foreach (string item in itemArrayList)
                {
                    if (item.Substring(0, 2).Equals(Constants.ITEM_CODE2))
                    {
                        cmbbox_PurchateItem.Items.Add(item);
                    }
                }
            }

            if (Constants.ITEM_CODE3 != null)
            {
                foreach (string item in itemArrayList)
                {
                    if (item.Substring(0, 2).Equals(Constants.ITEM_CODE3))
                    {
                        cmbbox_PurchateItem.Items.Add(item);
                    }
                }
            }

            if (Constants.ITEM_CODE4 != null)
            {
                foreach (string item in itemArrayList)
                {
                    if (item.Substring(0, 2).Equals(Constants.ITEM_CODE4))
                    {
                        cmbbox_PurchateItem.Items.Add(item);
                    }
                }
            }

            if (Constants.ITEM_CODE5 != null)
            {
                foreach (string item in itemArrayList)
                {
                    if (item.Substring(0, 2).Equals(Constants.ITEM_CODE5))
                    {
                        cmbbox_PurchateItem.Items.Add(item);
                    }
                }
            }

            if(cmbbox_PurchateItem.Items.Count != 0)
            {
                cmbbox_PurchateItem.SelectedIndex = 0;
            }
            else
            {
                cmbbox_PurchateItem.Text = "";
            }
        }

        private void txt_PurchaseAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void run_progress()
        {
            ProgressFrom proForm = new ProgressFrom();
            proForm.ShowDialog();
            proForm.progressBar1.Enabled = true;
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
            if(COM_STORE.SelectedIndex == 0)
            {
                BTN_ISSUE.Enabled = false;
                cmbbox_PurchateItem.Items.Clear();
                cmbbox_PurchateItem.Text = "";
            }
            else
            {
                BTN_ISSUE.Enabled = true;

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

                addItemCode();
                
            }
        }
    }
}
