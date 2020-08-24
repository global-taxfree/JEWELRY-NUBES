using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using log4net;
using JEWELRY_NUBES.Util;
using JEWELRY_NUBES.Tran;
using MetroFramework;
using System.Globalization;

namespace JEWELRY_NUBES.Screen
{
    public partial class VoidPanel : UserControl
    {
        DetailSlipInfo detailsSlip = null;

        ControlManager m_CtlSizeManager = null;
        public VoidPanel(ILog Logger = null)
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
            m_CtlSizeManager.addControlMove(LAY_PAGE, false, false, true, false);

            //종횡 늘림
            m_CtlSizeManager.addControlMove(GRD_SLIP, false, false, true, true);

            m_CtlSizeManager.MoveControls();
            this.Refresh();
            //AutoCompleteCompany();

            txt_PurchaseAmt.TextChanged += new System.EventHandler(txt_PurchaseAmt_TextChanged);
            txt_PurchaseAmt.Text = "0.00";
            txt_PurchaseAmt.SelectionStart = txt_PurchaseAmt.Text.Length;

            //setStoreName();
        }

        private void VoidPanel_SizeChanged(object sender, EventArgs e)
        {
            if (m_CtlSizeManager != null)
            {
                m_CtlSizeManager.MoveControls();
                this.Refresh();
            }
        }

        int display_num = 15;
        int init_num = 0;
        string preAction = "";
        private void BTN_SEARCH_Click(object sender, EventArgs e)
        {
            BTN_SEARCH.Enabled = false;
            JObject jsonReq = new JObject();
            init_num = 1;
            int nRow = GRD_SLIP.RowCount;
            for (int i = nRow - 1; i >= 0; i--)
            {
                GRD_SLIP.Rows.RemoveAt(i);
            }
            GRD_SLIP.Refresh();

            try
            {
                setWaitCursor(true);
                jsonReq.Add("group_id", Constants.GROUP_ID);
                jsonReq.Add("shop_id", Constants.MID);
                jsonReq.Add("start_date", string.Empty);
                jsonReq.Add("end_date", string.Empty);

                if (!txt_DocId.Text.Replace(".", "").Replace(" ", "").Equals(""))
                { 
                    jsonReq.Add("doc_id", txt_DocId.Text.Replace(".", "").Replace(" ", ""));
                }
                else
                {
                    MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidateDocID"), "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                jsonReq.Add("receipt_no", string.Empty);
                jsonReq.Add("receipt_amt", string.Empty);
                jsonReq.Add("gst_no", string.Empty);
                jsonReq.Add("company_name", string.Empty);
                jsonReq.Add("shop_name", string.Empty);
                Transaction tran = new Transaction();

                //string page = "1";
                int total_cnt = tran.searchDocIdCount(jsonReq.ToString());
                if (total_cnt > 0)
                {
                    if (cmbbox_Page.Items.Count > 0)
                    {
                        cmbbox_Page.Items.Clear();
                    }

                    int i = 0;
                    double quotient = total_cnt / display_num;
                    double remainder = total_cnt % display_num;

                    double tot_page = 0;
                    if(remainder != 0)
                    {
                        tot_page = System.Math.Truncate(quotient);
                    }else
                    {
                        tot_page = System.Math.Truncate(quotient) - 1;
                    }
                    
                    for (i = 0; i <= tot_page; i++)
                    {
                        cmbbox_Page.Items.Add(i + 1);
                    }

                    //searchList(page);
                    preAction = "BTN_SEARCH";
                    cmbbox_Page.SelectedIndex = 0;
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
            finally
            {
                BTN_SEARCH.Enabled = true;
                setWaitCursor(false);
            }
        }

        private void cmbbox_Page_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (init_num == 0)
            {
                return;
            }

            /*
            if (!BTN_SEARCH.Enabled || !BTN_SEARCH2.Enabled)
            {
                if (!BTN_SEARCH.Enabled)
                {
                    searchList(((cmbbox_Page.SelectedIndex) + 1).ToString());
                }

                if (!BTN_SEARCH2.Enabled)
                {
                    searchListByInvoice(((cmbbox_Page.SelectedIndex) + 1).ToString());
                }
            }
            else
            {
            */
                if (preAction.Equals("BTN_SEARCH"))
                {
                    searchList(((cmbbox_Page.SelectedIndex) + 1).ToString());
                }
                
                if (preAction.Equals("BTN_SEARCH2"))
                {
                    searchListByInvoice(((cmbbox_Page.SelectedIndex) + 1).ToString());
                }
            //}

            //searchList(((cmbbox_Page.SelectedIndex)+1).ToString());
        }

        private void searchList(string page_num)
        {
            JObject jsonReq = new JObject();

            try
            {
                //Console.WriteLine("#################################  searchList();");

                //cmbbox_Page.SelectedIndex = int.Parse(page_num) - 1;

                Transaction tran = new Transaction();

                jsonReq.Add("display_num", display_num);
                jsonReq.Add("page_num", page_num);
                jsonReq.Add("group_id", Constants.GROUP_ID);
                jsonReq.Add("shop_id", Constants.MID);
                jsonReq.Add("start_date", string.Empty);
                jsonReq.Add("end_date", string.Empty);
                if (!txt_DocId.Text.Replace(".", "").Replace(" ", "").Equals(""))
                {
                    jsonReq.Add("doc_id", txt_DocId.Text.Replace(".", "").Replace(" ", ""));
                }
                jsonReq.Add("receipt_no", string.Empty);
                jsonReq.Add("receipt_amt", string.Empty);
                jsonReq.Add("gst_no", string.Empty);

                string strResult = tran.searchDocIdList(jsonReq.ToString());

                int nRow = GRD_SLIP.RowCount;
                for (int i = nRow - 1; i >= 0; i--)
                {
                    GRD_SLIP.Rows.RemoveAt(i);
                }
                GRD_SLIP.Refresh();

                if (!strResult.Equals(""))
                {
                    nRow = GRD_SLIP.RowCount;

                    JArray a = JArray.Parse(strResult);

                    int i = 0;
                    foreach (JObject json in a.Children<JObject>())
                    {
                        GRD_SLIP.Rows.Add();

                        GRD_SLIP.Rows[nRow + i].Cells["No"].Value = json["row_no"].ToString();
                        GRD_SLIP.Rows[nRow + i].Cells["Outlet"].Value = json["group_name"].ToString();
                        GRD_SLIP.Rows[nRow + i].Cells["StoreName"].Value = json["store_name"].ToString();
                        GRD_SLIP.Rows[nRow + i].Cells["GSTNo"].Value = json["gst_no"].ToString();
                        GRD_SLIP.Rows[nRow + i].Cells["IssueDate"].Value = json["issue_date"].ToString();
                        GRD_SLIP.Rows[nRow + i].Cells["DocId"].Value = Util.Utils.FormatDocId(json["doc_id"].ToString());
                        GRD_SLIP.Rows[nRow + i].Cells["SalesAmt"].Value = json["sales_amt"].ToString();
                        GRD_SLIP.Rows[nRow + i].Cells["GSTAmt"].Value = json["gst_amt"].ToString();
                        GRD_SLIP.Rows[nRow + i].Cells["RefundAmt"].Value = json["refund_amt"].ToString();
                        GRD_SLIP.Rows[nRow + i].Cells["Status"].Value = json["status_name"].ToString();
                        GRD_SLIP.Rows[nRow + i].Cells["StatusCode"].Value = json["status"].ToString();
                        i++;
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
                //MessageBox.Show(ex.StackTrace);
                MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/SearchFaild"),
                    "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }
        }

        private void researchDocList()
        {
            if (preAction.Equals("BTN_SEARCH"))
            {
                searchList(cmbbox_Page.Text);
            }

            if (preAction.Equals("BTN_SEARCH2"))
            {
                searchListByInvoice(cmbbox_Page.Text);
            }
        }

        private void GRD_SLIP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            string DocId = GRD_SLIP.Rows[e.RowIndex].Cells["DocId"].Value.ToString().Replace(".", "");

            detailsSlip = new DetailSlipInfo();
            detailsSlip.researchDocIdList += new DetailSlipInfo.researchDocIdListDelegate(researchDocList);

            detailsSlip.DocId = DocId;
            detailsSlip.BTN_REPRINT.Visible = false;
            detailsSlip.ShowDialog();
        }

        private void VoidPanel_Load(object sender, EventArgs e)
        {
            cmbbox_Page.Items.Add("1");
            cmbbox_Page.SelectedIndex = 0;
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

      



        private void BTN_SEARCH2_Click(object sender, EventArgs e)
        {
            BTN_SEARCH2.Enabled = false;
            JObject jsonReq = new JObject();
            init_num = 1;
            int nRow = GRD_SLIP.RowCount;
            for (int i = nRow - 1; i >= 0; i--)
            {
                GRD_SLIP.Rows.RemoveAt(i);
            }
            GRD_SLIP.Refresh();

            try
            {
                setWaitCursor(true);
                string value = txt_PurchaseAmt.Text.Replace(",", "").Replace("$", "");
                jsonReq.Add("group_id", Constants.GROUP_ID);
                jsonReq.Add("start_date", Util.Utils.FormatConvertDate(txt_StartDate.Text));
                jsonReq.Add("end_date", string.Empty);
                jsonReq.Add("doc_id", string.Empty);
                if(txt_InvoiceNo.Text.Equals("") || (decimal.Parse(value) < 1))
                {
                    MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/SearchValidate"),
                      "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!txt_InvoiceNo.Text.Equals(""))
                {
                    jsonReq.Add("receipt_no", txt_InvoiceNo.Text);
                }
                
                if (!txt_PurchaseAmt.Text.Equals("") && (decimal.Parse(value) > 0))
                {
                    jsonReq.Add("receipt_amt", value);
                }
                jsonReq.Add("gst_no", string.Empty);
                Transaction tran = new Transaction();

                string page = "1";
                int total_cnt = tran.searchDocIdCount(jsonReq.ToString());
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

                    //searchListByInvoice(page);
                    preAction = "BTN_SEARCH2";
                    cmbbox_Page.SelectedIndex = 0;
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
                BTN_SEARCH2.Enabled = true;
                setWaitCursor(false);
            }
        }

        private void searchListByInvoice(string page_num)
        {
            JObject jsonReq = new JObject();

            try
            {
                //Console.WriteLine("#################################  searchListByInvoice();");
                //cmbbox_Page.SelectedIndex = int.Parse(page_num) - 1;

                Transaction tran = new Transaction();

                jsonReq.Add("display_num", display_num);
                jsonReq.Add("page_num", page_num);
                jsonReq.Add("group_id", Constants.GROUP_ID);
                jsonReq.Add("start_date", Util.Utils.FormatConvertDate(txt_StartDate.Text));
                jsonReq.Add("end_date", string.Empty);
                jsonReq.Add("doc_id", string.Empty);
                if (!txt_InvoiceNo.Equals(""))
                {
                    jsonReq.Add("receipt_no", txt_InvoiceNo.Text);
                }
                string value = txt_PurchaseAmt.Text.Replace(",", "").Replace("$", "");
                if (!txt_PurchaseAmt.Text.Equals("") && (decimal.Parse(value) > 0))
                {
                    jsonReq.Add("receipt_amt", value);
                }
                jsonReq.Add("gst_no", string.Empty);

                string strResult = tran.searchDocIdList(jsonReq.ToString());

                int nRow = GRD_SLIP.RowCount;
                for (int i = nRow - 1; i >= 0; i--)
                {
                    GRD_SLIP.Rows.RemoveAt(i);
                }
                GRD_SLIP.Refresh();

                if (!strResult.Equals(""))
                {
                    nRow = GRD_SLIP.RowCount;

                    JArray a = JArray.Parse(strResult);

                    int i = 0;
                    foreach (JObject json in a.Children<JObject>())
                    {
                        GRD_SLIP.Rows.Add();

                        GRD_SLIP.Rows[nRow + i].Cells["No"].Value = json["row_no"].ToString();
                        GRD_SLIP.Rows[nRow + i].Cells["Outlet"].Value = json["group_name"].ToString();
                        GRD_SLIP.Rows[nRow + i].Cells["StoreName"].Value = json["store_name"].ToString();
                        GRD_SLIP.Rows[nRow + i].Cells["GSTNo"].Value = json["gst_no"].ToString();
                        GRD_SLIP.Rows[nRow + i].Cells["IssueDate"].Value = json["issue_date"].ToString();
                        GRD_SLIP.Rows[nRow + i].Cells["DocId"].Value = Util.Utils.FormatDocId(json["doc_id"].ToString());
                        GRD_SLIP.Rows[nRow + i].Cells["SalesAmt"].Value = json["sales_amt"].ToString();
                        GRD_SLIP.Rows[nRow + i].Cells["GSTAmt"].Value = json["gst_amt"].ToString();
                        GRD_SLIP.Rows[nRow + i].Cells["RefundAmt"].Value = json["refund_amt"].ToString();
                        GRD_SLIP.Rows[nRow + i].Cells["Status"].Value = json["status_name"].ToString(); ;
                        GRD_SLIP.Rows[nRow + i].Cells["StatusCode"].Value = json["status"].ToString(); ;
                        i++;
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
                //MessageBox.Show(ex.StackTrace);
                MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/SearchFaild"),
                    "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
            }
        }

        private void BTN_RESET_Click(object sender, EventArgs e)
        {
            CLEAR();
        }

        public void CLEAR()
        {
            if (detailsSlip != null)
            {
                detailsSlip.Hide();
            }
            int nRow = GRD_SLIP.RowCount;
            for (int i = nRow - 1; i >= 0; i--)
            {
                GRD_SLIP.Rows.RemoveAt(i);
            }
            GRD_SLIP.Refresh();
            preAction = string.Empty;
            txt_DocId.Text = string.Empty;
            txt_StartDate.Value = DateTime.Today;
            txt_PurchaseAmt.Text = string.Empty;
            txt_InvoiceNo.Text = string.Empty;
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
            int nRow = GRD_SLIP.RowCount;
            for (int i = nRow - 1; i >= 0; i--)
            {
                GRD_SLIP.Rows.RemoveAt(i);
            }
            GRD_SLIP.Refresh();

            if (COM_STORE.SelectedIndex == 0)
            {
                BTN_SEARCH.Enabled = false;
            }
            else
            {
                BTN_SEARCH.Enabled = true;

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

        private void txt_DocId_MouseClick(object sender, MouseEventArgs e)
        {
            if(txt_DocId.Text == "      .     .    .")
            {
                txt_DocId.Select(0, 0);
            }
        }
    }
}
