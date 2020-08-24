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
    public partial class CardForm : MetroFramework.Forms.MetroForm
    {
        public delegate void addCardNoDetailslDelegate();
        public event addCardNoDetailslDelegate addCardNoDetails;

        public delegate void closeCardNoFormDelegate();
        public event closeCardNoFormDelegate closeCardNoForm;

        public CardForm(ILog Logger = null)
        {
            InitializeComponent();
        }

        public void initialize()
        {
            txt_CardNo.Text = "";
            txt_DisplayCardNo.Text = "";
            txt_Track2.Text = "";
            txt_Track2.Focus();
        }

        private void CardForm_Load(object sender, EventArgs e)
        {
            txt_Track2.Focus();
        }

        private void CardForm_Shown(object sender, EventArgs e)
        {
            txt_Track2.Focus();
        }

        private void BTN_OK_Click(object sender, EventArgs e)
        {
            this.addCardNoDetails();
        }

        private void CardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.closeCardNoForm();
        }

        private void txt_Track2_KeyUp(object sender, KeyEventArgs e)
        {
            string cardNumber = "";
            string searchWithinThis = txt_Track2.Text;
            
            /*
            Console.WriteLine("::::::::::::::::::::::::::" + searchWithinThis);
            MessageBox.Show(searchWithinThis);

            string searchForThis = "=";
            int firstCharacter = searchWithinThis.IndexOf(searchForThis);
            if (firstCharacter > 0 && txt_Track2.Text.Length >= 13)
            {
                //MessageBox.Show(string.Format("First occurrence: {0}", firstCharacter));
                txt_CardNo.Text = txt_Track2.Text.Substring(0, firstCharacter);
                txt_DisplayCardNo.Text = txt_CardNo.Text.Substring(0, 4) + "-" + txt_CardNo.Text.Substring(4, 4) + "-" + "****" + "-" + txt_CardNo.Text.Substring(12, txt_CardNo.Text.Length - 12);
                txt_Track2.Focus();
                //this.addCardNoDetails();
            }
            */

            if (searchWithinThis.IndexOf('^') > 0 || searchWithinThis.IndexOf('=') > 0)
            {
                if (String.IsNullOrEmpty(txt_CardNo.Text))
                {
                    cardNumber = getCardNumber(searchWithinThis);

                    Console.WriteLine(cardNumber);

                    if (cardNumber.Length >= 13)
                    {
                        txt_CardNo.Text = cardNumber;
                        txt_DisplayCardNo.Text = txt_CardNo.Text.Substring(0, 4) + "-" + txt_CardNo.Text.Substring(4, 4) + "-" + "****" + "-" + txt_CardNo.Text.Substring(12, txt_CardNo.Text.Length - 12);
                        txt_Track2.Focus();
                    }
                }
            }
        }

        private String getCardNumber(String _text)
        {
            string rtnVal = "";

            if (_text.IndexOf('^') > 0)
            {
                rtnVal = _text.Substring(_text.IndexOf('B') + 1, _text.IndexOf('^') - 2);
            }
            else if (_text.IndexOf('=') > 0)
            {
                if (_text.IndexOf(';') >= 0)
                {
                    rtnVal = _text.Substring(_text.IndexOf(';') + 1, _text.IndexOf('=') - 1);
                }
                else
                {
                    rtnVal = _text.Substring(0, _text.IndexOf('='));
                }
            }

            return rtnVal;
        }

        private void txt_DisplayCardNo_Click(object sender, EventArgs e)
        {
            txt_DisplayCardNo.Text = "";
            txt_CardNo.Text = "";
            txt_Track2.Text = "";
            txt_Track2.Focus();
        }
    }
}
