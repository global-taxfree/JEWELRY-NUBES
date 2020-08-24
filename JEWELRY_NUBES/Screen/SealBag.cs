using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JEWELRY_NUBES.Util;
using Newtonsoft.Json.Linq;
using JEWELRY_NUBES.Tran;

namespace JEWELRY_NUBES.Screen
{
    public partial class SealBag : MetroFramework.Forms.MetroForm
    {

        public delegate void closeSealbackDelegate();
        public event closeSealbackDelegate closeSealback;

        public String docid = "";

        public SealBag()
        {
            InitializeComponent();
        }

        private void SealBag_Load(object sender, EventArgs e)
        {

        }

        public void initialize()
        {
            txt_sealbackNo.Text = "";
            txt_sealbackNo.Focus();
        }

        private void BTN_OK_Click(object sender, EventArgs e)
        {
            if(txt_sealbackNo.Text =="")
            {
                MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidateSealedBag"));
                txt_sealbackNo.Focus();
                return;
            }
            JObject jsonReq = new JObject();
            Transaction tran = new Transaction();
            jsonReq.Add("user_id", Constants.USER_ID);
            jsonReq.Add("docid", docid);
            jsonReq.Add("sealbag_no", txt_sealbackNo.Text);
            string result_code = tran.updateSealBagInfo(jsonReq.ToString());

            if(result_code == "2")
            {
                MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/DuplicateSealedBag"));
            }
            else if (result_code == "1")
            {
                MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/SealBagSuccess"));
                this.closeSealback(); 
            }
            else
            {
                MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/SealBagFail"));
            }
        }

        private void BTN_CLOSE_Click(object sender, EventArgs e)
        {
            this.closeSealback();
        }

        private void txt_sealbackNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar))
            {
                e.KeyChar = Char.ToUpper(e.KeyChar);
            }
        }

        private void SealBag_Shown(object sender, EventArgs e)
        {
            txt_sealbackNo.Focus();
        }
    }
}
