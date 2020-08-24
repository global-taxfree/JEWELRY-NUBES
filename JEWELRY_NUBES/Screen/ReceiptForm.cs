using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using MetroFramework;
using System.Windows.Forms;
using JEWELRY_NUBES.Tran;
using JEWELRY_NUBES.Util;
using log4net;
namespace JEWELRY_NUBES
{
    public partial class ItemForm : MetroFramework.Forms.MetroForm
    {
        public delegate void addReceiptDetailslDelegate();
        public event addReceiptDetailslDelegate addReceiptDetails;

        public delegate void closeAddReceiptDetailsDelegate();
        public event closeAddReceiptDetailsDelegate closeAddReceiptDetails;

        public delegate void closeFormDetailsDelegate();
        public event closeFormDetailsDelegate closeFormDetails;

        ArrayList itemArrayList = new ArrayList();
        public int issue_index = 0 ;
        private String rec_prefix = "";
        private int rec_digits = 0;
        private String dup_rc = "N";

        public ItemForm(ILog Logger = null)
        {
            InitializeComponent();
        }

        private void ItemForm_Load(object sender, EventArgs e)
        {
            txt_Keyword.Focus();

            txt_PurchaseDate.Value = new DateTime(int.Parse(DateTime.Now.ToString("yyyy")),
                                     int.Parse(DateTime.Now.ToString("MM")),
                                     int.Parse(DateTime.Now.ToString("dd")));

            txt_PurchaseAmt.TextChanged += new System.EventHandler(txt_PurchaseAmt_TextChanged);
            txt_PurchaseAmt.Text = "0.00";
            txt_PurchaseAmt.SelectionStart = txt_PurchaseAmt.Text.Length;

            txt_Quantity.Mask = "90";
            txt_Quantity.Text = "1";

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

        public void initialize()
        {
            txt_Keyword.Text = "";
            txt_Keyword.Enabled = true;

            txt_ReceiptNo.Text = "";
            txt_PurchaseDate.Value = new DateTime(int.Parse(DateTime.Now.ToString("yyyy")),
                                     int.Parse(DateTime.Now.ToString("MM")),
                                     int.Parse(DateTime.Now.ToString("dd")));

            txt_PurchaseAmt.Text = "0.00";
            txt_Quantity.Text = "1";

            txt_StoreName.Text = "";
            txt_GSTNo.Text = "";
            txt_TID.Text = "";
            txt_MID.Text = "";
            txt_GroupName.Text = "";

            int nRow = STORE_LIST.RowCount;
            int i = 0;
            for (i = nRow - 1; i >= 0; i--)
            {
                STORE_LIST.Rows.RemoveAt(i);
            }
            STORE_LIST.Refresh();

            for (i = 0; i < cmbbox_PurchateItem.Items.Count; i++)
            {
                cmbbox_PurchateItem.Items.RemoveAt(i);
            }
            cmbbox_PurchateItem.Text = "";

            txt_Keyword.Focus();
        }

        public void setReceiptDetail()
        {
            txt_Keyword.Text = "";
            txt_Keyword.Enabled = false;

            int nRow = STORE_LIST.RowCount;
            int i = 0;
            for (i = nRow - 1; i >= 0; i--)
            {
                STORE_LIST.Rows.RemoveAt(i);
            }
            STORE_LIST.Refresh();

            txt_ReceiptNo.Focus();
        }

        private void txt_Keyword_KeyUp(object sender, KeyEventArgs e)
        {
            if (txt_Keyword.Text.Length > 0)
            {
                //MessageBox.Show(searchStoreName.Text);
                try
                {
                    int nRow = STORE_LIST.RowCount;
                    for (int i = nRow - 1; i >= 0; i--)
                    {
                        STORE_LIST.Rows.RemoveAt(i);
                    }

                    STORE_LIST.Refresh();

                    Transaction tran = new Transaction();
                    string strRsult = tran.searchStoreName(txt_Keyword.Text);

                    if (!strRsult.Equals(""))
                    {
                        nRow = STORE_LIST.RowCount;

                        JArray a = JArray.Parse(strRsult);

                        int i = 0;
                        foreach (JObject json in a.Children<JObject>())
                        {
                            STORE_LIST.Rows.Add();

                            STORE_LIST[0, nRow + i].Value = json["company_name"].ToString();
                            STORE_LIST[1, nRow + i].Value = json["store_name"].ToString();
                            STORE_LIST[2, nRow + i].Value = json["gst_no"].ToString();
                            STORE_LIST[3, nRow + i].Value = json["tid"].ToString();
                            STORE_LIST[4, nRow + i].Value = json["mid"].ToString();
                            STORE_LIST[5, nRow + i].Value = json["group_name"].ToString();
                            STORE_LIST[6, nRow + i].Value = json["item_code1"].ToString();
                            STORE_LIST[7, nRow + i].Value = json["item_code2"].ToString();
                            STORE_LIST[8, nRow + i].Value = json["item_code3"].ToString();
                            STORE_LIST[9, nRow + i].Value = json["item_code4"].ToString();
                            STORE_LIST[10, nRow + i].Value = json["item_code5"].ToString();
                            STORE_LIST[11, nRow + i].Value = json["rec_prefix"].ToString();
                            STORE_LIST[12, nRow + i].Value = json["rec_digits"].ToString();
                            STORE_LIST[13, nRow + i].Value = json["dup_rc"].ToString();
                            i++;
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                int nRow = STORE_LIST.RowCount;
                if (nRow > 1)
                {
                    for (int i = nRow - 1; i >= 0; i--)
                        STORE_LIST.Rows.RemoveAt(i);
                }
            }
        }

        private void STORE_LIST_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            int nIndex = e.RowIndex;
            SelectedSTORE_LISTRow(nIndex);
        }

        private void SelectedSTORE_LISTRow(int index)
        {
            txt_StoreName.Text = STORE_LIST[1, index].Value.ToString();
            txt_GSTNo.Text = STORE_LIST[2, index].Value.ToString();
            txt_TID.Text = STORE_LIST[3, index].Value.ToString();
            txt_MID.Text = STORE_LIST[4, index].Value.ToString();
            txt_GroupName.Text = STORE_LIST[5, index].Value.ToString();

            if (cmbbox_PurchateItem.Items.Count > 0)
                cmbbox_PurchateItem.Items.Clear();

            for (int j = 6; j <= 10; j++)
            {
                //MessageBox.Show(STORE_LIST[j, e.RowIndex].Value.ToString());
                foreach (string item in itemArrayList)
                {
                    //MessageBox.Show(item);
                    if (!STORE_LIST[j, index].Value.ToString().Equals(""))
                    {
                        if (item.Substring(0, 2).Equals(STORE_LIST[j, index].Value.ToString()))
                        {
                            cmbbox_PurchateItem.Items.Add(item);
                        }
                    }
                }
            }

            if (cmbbox_PurchateItem.Items.Count > 0)
            {
                cmbbox_PurchateItem.SelectedIndex = 0;
            }

            if(STORE_LIST[11, index].Value.ToString() != "")
            {
                rec_prefix = STORE_LIST[11, index].Value.ToString();
            }
            else
            {
                rec_prefix = "";
            }

            if (STORE_LIST[12, index].Value.ToString() != "")
            {
                rec_digits = int.Parse(STORE_LIST[12, index].Value.ToString());
            }
            else
            {
                rec_digits = 0 ;
            }

            if (STORE_LIST[13, index].Value.ToString() != "")
            {
                dup_rc = STORE_LIST[13, index].Value.ToString();
            }
            else
            {
                dup_rc = "N";
            }

            if (rec_prefix != "" && rec_digits != 0)
            {
                String temp = rec_prefix.PadRight(rec_digits, '0');

                txt_ReceiptNo.Mask = temp;
                txt_ReceiptNo.Focus();
                txt_ReceiptNo.Select(0, rec_prefix.Length+1);
            }
            else
            {
                txt_ReceiptNo.Mask = "CCCCCCCCCCCCCCCCCCCCCCCCC";
                txt_ReceiptNo.Focus();
            }
        }

        private void BTN_OK_Click(object sender, EventArgs e)
        {
            string curDate = System.DateTime.Now.ToString("yyyyMMdd");
            string selectDate = txt_PurchaseDate.Value.ToString("yyyyMMdd").Replace("-", "").Replace("/", "");
            if (txt_StoreName.Text.Equals(""))
            {
                /*
                MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/Login_EnterId"),
                    "Loigin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                */
                MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidateStoreName"));
                txt_Keyword.Focus();
                return;
            }
            if (txt_GSTNo.Text.Equals(""))
            {
                MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidateGSTNumber"));
                txt_Keyword.Focus();
                return;
            }
            if (txt_ReceiptNo.Text.Equals(""))
            {
                MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidateReceiptNo"));
                txt_ReceiptNo.Focus();
                return;
            }

            string value = txt_PurchaseAmt.Text.Replace(",", "").Replace("$", "");
            if (txt_PurchaseAmt.Text.Equals("") || (decimal.Parse(value) <= 0) || (decimal.Parse(value) > 9999999) )
            {
                MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidatePurchaseAmount"));
                txt_PurchaseAmt.Focus();
                return;
            }
            if (txt_PurchaseDate.Text.Equals(""))
            {
                MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidatePurchaseDate"));
                txt_PurchaseDate.Focus();
                return;
            }
            if (txt_Quantity.Text.Equals(""))
            {
                MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidateQuantity"));
                txt_Quantity.Focus();
                return;
            }
            if (cmbbox_PurchateItem.Text.Equals(""))
            {
                MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidatePurchaseItem"));
                cmbbox_PurchateItem.Focus();
                return;
            }

            if(rec_prefix != "")
            {
                String input_prefix = txt_ReceiptNo.Text.Substring(0, rec_prefix.Length);
                if (!rec_prefix.Equals(input_prefix))
                {
                    MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidateRecPrefix"));
                    txt_ReceiptNo.Focus();
                    return;
                }
            }

            if(rec_digits != 0 && rec_digits != txt_ReceiptNo.Text.Length)
            {
                MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidateRecDigits"));
                txt_ReceiptNo.Focus();
                return;
            }

            // 날자 점검 미래일 체크
            if (Int32.Parse(curDate) < Int32.Parse(selectDate))
            {
                MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidatePurchaseDate"));
                //txt_PurchaseDate.Value = System.DateTime.Now;
                return;
            }
            /* 3개월전 확인 정확하지 않아 주석
            DateTime limitDate = System.DateTime.Now;
            limitDate = limitDate.AddMonths(-3).AddDays(1);
            if (Int32.Parse(limitDate.ToString("yyyyMMdd")) > Int32.Parse(selectDate))
            {
                MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidatePurchaseDate"));
                txt_PurchaseDate.Value = System.DateTime.Now;
                return;
            }
            */
            DateTime limitDate = DateTime.Today.AddMonths(-2);
            if (txt_PurchaseDate.Value.CompareTo(limitDate) <= 0)
            {
                MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidatePurchaseDate"));
                return;
            }

            this.addReceiptDetails();
        }

        private void BTN_CLOSE_Click(object sender, EventArgs e)
        {
            this.closeAddReceiptDetails();
        }

        private void ItemForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.closeFormDetails();
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

        /*
        private void txt_ReceiptDate_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            System.Text.RegularExpressions.Regex rtime = new System.Text.RegularExpressions.Regex(@"(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d");
            if (!rtime.IsMatch(txt_PurchaseDate.Text))
            {
                MessageBox.Show("Invalid Date Value");
                e.Cancel = true;
            }
        }
        */

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;
            //Keys key = keyData & ~(Keys.Shift | Keys.Control);
            if (((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN)) && STORE_LIST.Focused)
            {
                switch (keyData)
                {
                    case Keys.Enter:
                        Int32 selectedRowCount = STORE_LIST.Rows.GetRowCount(DataGridViewElementStates.Selected);
                        if (selectedRowCount == 1)
                        {
                            SelectedSTORE_LISTRow(STORE_LIST.SelectedRows[0].Index);
                            return true;
                        }
                        break;
                    case Keys.Escape:
                        this.DialogResult = DialogResult.Cancel;
                        Close();
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);

        }

        private void txt_ReceiptNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.KeyChar = Char.ToUpper(e.KeyChar);
            }
        }
    }
}
