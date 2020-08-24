using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using log4net;
using JEWELRY_NUBES.Util;
using JEWELRY_NUBES.Tran;
using MetroFramework;

namespace JEWELRY_NUBES.Screen
{
    public partial class SearchPanel : UserControl
    {
        DetailSlipInfo detailsSlip = null;

        ControlManager m_CtlSizeManager = null;
        public SearchPanel(ILog Logger = null)
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

            txt_PurchaseAmt.TextChanged += new System.EventHandler(txt_PurchaseAmt_TextChanged);
            txt_PurchaseAmt.Text = "0.00";
            txt_PurchaseAmt.SelectionStart = txt_PurchaseAmt.Text.Length;

            //setStoreName();
        }

        private void SearchPanel_SizeChanged(object sender, EventArgs e)
        {
            if (m_CtlSizeManager != null)
            {
                m_CtlSizeManager.MoveControls();
                this.Refresh();
            }
        }

        int display_num = 15;
        int init_num = 0;
        private void BTN_SEARCH_Click(object sender, EventArgs e)
        {
            if (!BTN_SEARCH.Enabled)
            {
                return;
            }
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
                //TimeSpan between_date = txt_EndDate.Value - txt_StartDate.Value;
                //int date_cnt = between_date.Days;

                if (DateTime.Compare(txt_StartDate.Value, txt_EndDate.Value) > 0)
                {
                    MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidateSearchDate"));
                    return;
                }
                /*
                else if (date_cnt > 10)
                {

                    MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ExceedSearchDate"));
                    return;
                }
                */
                jsonReq.Add("group_id", Constants.GROUP_ID);
                jsonReq.Add("shop_id", Constants.MID);
                jsonReq.Add("start_date", Util.Utils.FormatConvertDate(txt_StartDate.Text));
                jsonReq.Add("end_date", Util.Utils.FormatConvertDate(txt_EndDate.Text));

                if (!txt_DocId.Text.Replace(".", "").Replace(" ", "").Equals(""))
                {
                    jsonReq.Add("doc_id", txt_DocId.Text.Replace(".", "").Replace(" ", ""));
                }

                if (!txt_InvoiceNo.Equals(""))
                {
                    jsonReq.Add("receipt_no", txt_InvoiceNo.Text);
                }

                string value = txt_PurchaseAmt.Text.Replace(",", "").Replace("$", "");
                if (!txt_PurchaseAmt.Text.Equals("") && (decimal.Parse(value) > 0))
                {
                    jsonReq.Add("receipt_amt", value);
                }
                if(!txt_sealbagNo.Text.Equals(""))
                {
                    jsonReq.Add("seal_bag_no", txt_sealbagNo.Text);
                }

                Transaction tran = new Transaction();

                string page = "1";
                int total_cnt = tran.searchReceiptCount(jsonReq.ToString());
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
                    //MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/DataNotFound"));
                    MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/DataNotFound"), 
                        "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                setWaitCursor(false);
                BTN_SEARCH.Enabled = true ;
            }
        }

        private void cmbbox_Page_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (init_num == 0)
            {
                return;
            }
            if (!BTN_SEARCH.Enabled)
            {
                return;
            }

            searchList(((cmbbox_Page.SelectedIndex) + 1).ToString());
        }

        private void searchList(string page_num)
        {
            JObject jsonReq = new JObject();

            try
            {

                cmbbox_Page.SelectedIndex = int.Parse(page_num) - 1;

                Transaction tran = new Transaction();

                jsonReq.Add("display_num", display_num);
                jsonReq.Add("page_num", page_num);

                jsonReq.Add("group_id", Constants.GROUP_ID);
                jsonReq.Add("shop_id", Constants.MID);
                jsonReq.Add("start_date", Util.Utils.FormatConvertDate(txt_StartDate.Text));
                jsonReq.Add("end_date", Util.Utils.FormatConvertDate(txt_EndDate.Text));

                if (!txt_DocId.Text.Replace(".", "").Replace(" ", "").Equals(""))
                {
                    jsonReq.Add("doc_id", txt_DocId.Text.Replace(".", "").Replace(" ", ""));
                }

                if (!txt_InvoiceNo.Equals(""))
                {
                    jsonReq.Add("receipt_no", txt_InvoiceNo.Text);
                }

                string value = txt_PurchaseAmt.Text.Replace(",", "").Replace("$", "");
                if (!txt_PurchaseAmt.Text.Equals("") && (decimal.Parse(value) > 0))
                {
                    jsonReq.Add("receipt_amt", value);
                }
                if (!txt_sealbagNo.Text.Equals(""))
                {
                    jsonReq.Add("seal_bag_no", txt_sealbagNo.Text);
                }

                string strResult = tran.searchReceiptList(jsonReq.ToString());

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
                        GRD_SLIP.Rows[nRow + i].Cells["ReceiptNo"].Value = json["receipt_no"].ToString();
                        GRD_SLIP.Rows[nRow + i].Cells["PurchaseDate"].Value = Util.Utils.FormatDate(json["purchase_date"].ToString());
                        GRD_SLIP.Rows[nRow + i].Cells["PurchaseAmt"].Value = json["purchase_amt"].ToString();
                        GRD_SLIP.Rows[nRow + i].Cells["PurchaseItem"].Value = json["purchase_item"].ToString();
                        GRD_SLIP.Rows[nRow + i].Cells["IssueDate"].Value = json["issue_date"].ToString();
                        GRD_SLIP.Rows[nRow + i].Cells["DocId"].Value = Util.Utils.FormatDocId(json["doc_id"].ToString());
                        /*
                        GRD_SLIP.Rows[nRow + i].Cells["SalesAmt"].Value = json["sales_amt"].ToString();
                        GRD_SLIP.Rows[nRow + i].Cells["GSTAmt"].Value = json["gst_amt"].ToString();
                        GRD_SLIP.Rows[nRow + i].Cells["RefundAmt"].Value = json["refund_amt"].ToString();
                        */
                        GRD_SLIP.Rows[nRow + i].Cells["Status"].Value = json["status_name"].ToString(); ;
                        GRD_SLIP.Rows[nRow + i].Cells["StatusCode"].Value = json["status"].ToString(); ;
                        i++;
                    }
                }
                else
                {
                    //MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/DataNotFound"));
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

        private void researchDocList()
        {
            searchList(cmbbox_Page.Text);
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
            detailsSlip.ViewVoid = true;
            detailsSlip.DocId = DocId;
            detailsSlip.BTN_REPRINT.Visible = true;
            detailsSlip.ShowDialog();
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

        private void SearchPanel_Load(object sender, EventArgs e)
        {
            cmbbox_Page.Items.Add("1");
            cmbbox_Page.SelectedIndex = 0;
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

        private bool KeyEnteredIsValid(string key)
        {
            Regex regex;
            regex = new Regex("[^0-9]+$"); //regex that matches disallowed text
            return regex.IsMatch(key);
        }

        private void txt_PurchaseAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)8; // 8 is backsapce;
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

            txt_DocId.Text = "";
            txt_InvoiceNo.Text = "";
            txt_PurchaseAmt.Text = "";
            txt_StartDate.Text = "";
            txt_EndDate.Text = "";
            txt_sealbagNo.Text = "";
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
            if (txt_DocId.Text == "      .     .    .")
            {
                txt_DocId.Select(0, 0);
            }
        }
    }
}
