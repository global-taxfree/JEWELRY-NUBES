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
using JEWELRY_NUBES.Tran;
using Newtonsoft.Json.Linq;
using MetroFramework;

namespace JEWELRY_NUBES.Screen
{
    public partial class RefundCalculator : UserControl
    {
        public RefundCalculator()
        {
            InitializeComponent();

            txt_totalAmt.TextChanged += new System.EventHandler(txt_totalAmt_TextChanged);
            txt_totalAmt.Text = "0.00";
            txt_GSTAmt.Text = "0.00";
            txt_RefundAmt.Text = "0.00";
            txt_totalAmt.SelectionStart = txt_totalAmt.Text.Length;
            txt_totalAmt.Focus();
        }

        private void RefundCalculator_Load(object sender, EventArgs e)
        {
            txt_totalAmt.Focus();
        }

        private void txt_totalAmt_TextChanged(object sender, EventArgs e)
        {
            //Remove previous formatting, or the decimal check will fail including leading zeros
            string value = txt_totalAmt.Text.Replace(",", "")
                .Replace("$", "").Replace(".", "").TrimStart('0');
            decimal ul;
            //decimal gst;
            //decimal refund_amt;
            //Check we are indeed handling a number
            if (decimal.TryParse(value, out ul))
            {
                ul /= 100;
                //Unsub the event so we don't enter a loop
                txt_totalAmt.TextChanged -= txt_totalAmt_TextChanged;
                //Format the text as currency

                if (ul > 100)
                {
                    //gst = decimal.Multiply(ul, new decimal(10));
                    //gst = decimal.Divide(gst, new decimal(10.7));
                    //gst = decimal.Multiply(gst, new decimal(0.07));
                    //string tmp_gst = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", gst);
                    //txt_GSTAmt.Text = tmp_gst.Replace("$", "");
                    btn_calculrate.Enabled = true;
                }
                else
                {
                    btn_calculrate.Enabled = false;
                    txt_GSTAmt.Text = "0.00";
                    txt_RefundAmt.Text = "0.00";
                }

                string tmp_value = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", ul);
                txt_totalAmt.Text = tmp_value.Replace("$", "");
                txt_totalAmt.TextChanged += txt_totalAmt_TextChanged;
                txt_totalAmt.Select(txt_totalAmt.Text.Length, 0);


            }
        }

        private void BTN_CLEAR_Click(object sender, EventArgs e)
        {
            CLEAR();
        }

        public void CLEAR()
        {
            txt_totalAmt.Text = "0.00";
            txt_GSTAmt.Text = "0.00";
            txt_RefundAmt.Text = "0.00";
        }

        private void btn_calculrate_Click(object sender, EventArgs e)
        {

            if (txt_totalAmt.Text == "")
            {
                MessageBox.Show("enter Total Amount !");
                txt_totalAmt.Focus();
                return;
            }
            string gross_amount = txt_totalAmt.Text.Replace(",", "");
            float sale_amt = float.Parse(gross_amount);

            if (sale_amt < 100)
            {
                MetroMessageBox.Show(this, "Please check Total Amount", "Check Total Amount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_totalAmt.Focus();
                return;
            }

            txt_RefundAmt.Text = "";
            txt_GSTAmt.Text = "";

            try
            {
                JObject jsonReq = new JObject();
                Transaction tran = new Transaction();

                jsonReq.Add("gross_amt", sale_amt);

                string result_code = tran.checkRefundAmt(jsonReq.ToString());
                JObject jsonRes = JObject.Parse(result_code);

                txt_RefundAmt.Text = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", double.Parse(jsonRes["refund_amt"].ToString())).Replace("$", ""); 
                txt_GSTAmt.Text = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:C2}", double.Parse(jsonRes["gst_amt"].ToString()));
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, "Please check Network", "Sever error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //MessageBox.Show(ex.Message);
            }
            finally
            {

            }

        }

        private void txt_totalAmt_KeyPress(object sender, KeyPressEventArgs e)
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

        public void setFirstFocus()
        {
            txt_totalAmt.Focus();
        }
    }
}
