using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework;
using JEWELRY_NUBES.Tran;
using JEWELRY_NUBES.Util;
using JEWELRY_NUBES.Properties;
using log4net;

namespace JEWELRY_NUBES.Screen
{
    public partial class Login : MetroFramework.Forms.MetroForm
    {
        private Boolean m_bStart = false;
        public string m_strID = string.Empty;
        public string m_strPassword = string.Empty;
        
        private Login(ILog Logger = null)
        {
            InitializeComponent();
            this.ActiveControl = TEXT_ID;
        }

        public Login(Boolean bStart = true, String strID = "", String strPassword = "")
        {
            InitializeComponent();

            m_bStart = bStart;          //초기 load 여부
            m_strID = strID;            //초기 load : localdb에 저장된 마지막 로그인 id , 중간 load : 접속한 id
            m_strPassword = strPassword;//초기 load : localdb에 저장된 마지막 로그인 password , 중간 load : 접속한 password

        }

        private void Login_Load(object sender, EventArgs e)
        {
            ControlManager ctlManager = new ControlManager(this);
            ctlManager.ChageLabel(this);//Label 변경

            if (!m_bStart)
            {
                //ID 비활성화 , ID 입력
                TEXT_ID.Enabled = false;
                TEXT_ID.Text = m_strID;
            }            
            Activate();
        }

        private void BTN_CANCEL_Click(object sender, EventArgs e)
        {
            //Confirm 메시지박스 호출
            DialogResult bResult = MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/LoginCancel"), 
                "Login", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(bResult.Equals(DialogResult.Yes))
            {
                //프로그램 종료
                Application.ExitThread();
                Environment.Exit(0);
            }
            
        }

        private void BTN_LOGIN_Click(object sender, EventArgs e)
        {

            //단말기 초기 Load 시 창 호출하는 경우에는 전문처리
            if (TEXT_ID.Text == null || "".Equals(TEXT_ID.Text.Trim())) // PASSWORD 아무것도 입력 안한 경우에는 return
            {
                MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/Login_EnterId"), 
                    "Loigin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TEXT_ID.Focus();
                return;
            }

            if (TEXT_PASSWORD.Text == null || "".Equals(TEXT_PASSWORD.Text.Trim())) // PASSWORD 아무것도 입력 안한 경우에는 return
            {
                MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/Login_EnterPassword"), 
                    "Loigin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TEXT_PASSWORD.Focus();
                return;
            }

            Transaction tran = new Transaction();
            string strRsult = tran.Login(TEXT_ID.Text, TEXT_PASSWORD.Text);
            //로그인성공시
            //사용자 정보 가져옴
            if (strRsult.Equals("1") && Constants.CHECK_SERVERDATE.Equals("Y"))
            {
                m_strID = TEXT_ID.Text;
                m_strPassword = TEXT_PASSWORD.Text;
                this.Close();
            }
            else if (strRsult.Equals("1") && Constants.CHECK_SERVERDATE.Equals("N"))
            {
                //string message = string.Format(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/LoginErrorMessage"), Environment.NewLine);
                MetroMessageBox.Show(this, "Check your pc's time settings",  "Loigin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else {
                //실패 시 
                string message = string.Format(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/LoginErrorMessage"), Environment.NewLine);
                TEXT_PASSWORD.Text = "";
                MetroMessageBox.Show(this, message, 
                    "Loigin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
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
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!base.ProcessCmdKey(ref msg, keyData))
            {
                // 여기에 처리코드를 넣는다.
                if (keyData.Equals(Keys.Enter))//만약 Enter 키가 눌리면
                {
                    if (!"".Equals(TEXT_ID.Text.Trim()) && !"".Equals(TEXT_PASSWORD.Text.Trim())) // PASSWORD 아무것도 입력 안한 경우에는 return
                    {
                        BTN_LOGIN_Click(null, null);
                        return true;
                    }
                    //else {
                    //    if (this.ActiveControl is TextBox)
                    //    {
                    //        this.SelectNextControl((Control)this.ActiveControl, true, true, true, true);
                    //    }
                    //}
                }
                else if (keyData.Equals(Keys.Escape))//만약 Esc키가 눌리면
                {
                    BTN_CANCEL_Click(null, null);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return false;
        }

        private void Login_Shown(object sender, EventArgs e)
        {
            //TEXT_ID.Select();
            //TEXT_ID.Focus();
        }
    }
}
