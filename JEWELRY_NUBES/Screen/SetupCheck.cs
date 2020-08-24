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
    public partial class SetupCheck : MetroFramework.Forms.MetroForm
    {

        public delegate void closeSetupCheckDelegate();
        public event closeSetupCheckDelegate closeSetupCheck;
        public Boolean setup_visible = false ;
        public SetupCheck()
        {
            InitializeComponent();
            txt_SetupPassword.Focus();
        }

        private void SetupCheck_Load(object sender, EventArgs e)
        {
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            txt_SetupPassword.Focus();
        }

        private void SetupCheck_Shown(object sender, EventArgs e)
        {
            txt_SetupPassword.Focus();
        }

        public void initialize()
        {
            txt_SetupPassword.Text = "";
            txt_SetupPassword.Focus();
            setup_visible = false;
        }

        private void BTN_OK_Click(object sender, EventArgs e)
        {
            Check_Password();
        }

        private void Check_Password()
        {
            if (txt_SetupPassword.Text == "")
            {
                MessageBox.Show("Please enter Setup Password !");
                txt_SetupPassword.Focus();
                return;
            }
            else if (txt_SetupPassword.Text.Equals("gtf123"))
            {
                setup_visible = true;
                this.closeSetupCheck();
                return;
            }
            else
            {
                setup_visible = false;
                txt_SetupPassword.Text = "";
                MessageBox.Show("Incorrect  Password !");
                return;
            }
        }

        private void BTN_CLOSE_Click(object sender, EventArgs e)
        {
            this.closeSetupCheck();
        }

        private void Close_Panel()
        {
            setup_visible = false;
            this.closeSetupCheck();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;
            //Keys key = keyData & ~(Keys.Shift | Keys.Control);
            if (((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN)) && txt_SetupPassword.Focused)
            {
                switch (keyData)
                {
                    case Keys.Enter:
                        Check_Password();
                        break;
                    case Keys.Escape:
                        Close_Panel();
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
