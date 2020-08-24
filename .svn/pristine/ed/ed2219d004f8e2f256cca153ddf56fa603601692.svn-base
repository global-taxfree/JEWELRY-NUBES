using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Windows.Forms;
using JEWELRY_NUBES.Util;
using MetroFramework;
using JEWELRY_NUBES.Tran;

namespace JEWELRY_NUBES.Screen
{
    public partial class Main : MetroFramework.Forms.MetroForm, IMessageFilter
    {
        IssuePanel_SG pIssuePanel = null;
        VoidPanel pVoidPanel = null;
        SearchPanel pSearchPanel = null;
        PreferencesPanel pPreferencesPanel = null;
        Logbook pLogbookPanel = null;
        SetupCheck setupcheck = new SetupCheck();
        RefundCalculator pRefundCalculatorPanel = null;
        public static Util.Constants m_Constants;
        ControlManager m_CtlSizeManager = null;
        private Timer mTimer;
        private int mDialogCount = - 1;
        private Image[] StatusImgs;
        private string version = "1.0.0.2";
        //int nWidth = 1235;
        //int nHeight = 695;

        int nWidth = 1078;
        int nHeight = 700;


        public Main()
        {
            InitializeComponent();

            m_Constants = new Constants();
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            setupcheck.closeSetupCheck += new SetupCheck.closeSetupCheckDelegate(closeSetupCheck);
            Constants.LOGGER_MAIN.Info("Start Application................");
            //Constants.GROUP_ID = "0000000005";
            
            if (!login())
            {
                Application.ExitThread();
                Environment.Exit(0);
            }
            
            backgroundWorker1.RunWorkerAsync();

            ResourceManager rm = JEWELRY_NUBES.Properties.Resources.ResourceManager;
            StatusImgs = new Image[] { (Bitmap)rm.GetObject("appbar_cloud"), (Bitmap)rm.GetObject("appbar_cloud_delete") };

            TIL_NETWORK.TileImage = StatusImgs[0];
            TIL_LOGBOOK.Text = "Log" + Environment.NewLine + "Book";
            TIL_CALCULATOR.Text = "Refund" + Environment.NewLine + "Calculator";
            TIL_EMPTY_2.Text = "Version :" + version ;
            mTimer = new Timer();
            mTimer.Interval = 300000;
            mTimer.Tick += LogoutUser;
            mTimer.Enabled = true;
            Application.AddMessageFilter(this);
        }

        delegate void SetNetworkStatusDelegate(bool flag);
        public void setNetworkStatus(bool flag)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    SetNetworkStatusDelegate d = new SetNetworkStatusDelegate(setNetworkStatus);
                    this.Invoke(d, new object[] { flag });
                }
                else
                {
                    if (!flag)
                    {
                        this.TIL_NETWORK.Text = "Offline";
                        this.TIL_NETWORK.Enabled = false;
                        this.TIL_NETWORK.TileImage = StatusImgs[1];
                    }
                    else
                    {
                        this.TIL_NETWORK.Text = "Online";
                        this.TIL_NETWORK.Enabled = true;
                        this.TIL_NETWORK.TileImage = StatusImgs[0];
                    }
                }
            }catch(Exception ex)
            {

            }
        }

        public Boolean login()
        {
            Boolean bRet = true;
            Login fLogin = new Login();
            fLogin.lbl_version.Text = version;
            fLogin.ShowDialog();
            string strTemp = fLogin.m_strID;
            string strTemp2 = fLogin.m_strPassword;
            fLogin = null;
            if (string.Empty.Equals(strTemp) || string.Empty.Equals(strTemp2))
            {
                bRet = false;
                return false;
            }
            else
            {
                Constants.USER_ID = strTemp;

                //TIL_USERNAME.Text = Constants.GROUP_NAME + "(" + Constants.GROUP_ID + ")";
                TIL_USERNAME.Text = "Additional control" + Environment.NewLine + "version" ;

            }
            return bRet;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            pIssuePanel = new IssuePanel_SG();
            metroPanel1.Controls.Add(pIssuePanel);

            pVoidPanel = new VoidPanel();
            metroPanel2.Controls.Add(pVoidPanel);

            pSearchPanel = new SearchPanel();
            metroPanel3.Controls.Add(pSearchPanel);

            pPreferencesPanel = new PreferencesPanel();
            metroPanel4.Controls.Add(pPreferencesPanel);

            pLogbookPanel = new Logbook(); 
            metroPanel5.Controls.Add(pLogbookPanel);

            pRefundCalculatorPanel = new RefundCalculator();
            metroPanel6.Controls.Add(pRefundCalculatorPanel);

            metroPanel1.Location = new Point(139, 40);
            metroPanel2.Location = new Point(139, 40);
            metroPanel3.Location = new Point(139, 40);
            metroPanel4.Location = new Point(139, 40);
            metroPanel5.Location = new Point(139, 40);
            metroPanel6.Location = new Point(139, 40);

            metroPanel1.Size = new Size(nWidth, nHeight);
            metroPanel2.Size = new Size(nWidth, nHeight);
            metroPanel3.Size = new Size(nWidth, nHeight);
            metroPanel4.Size = new Size(nWidth, nHeight);
            metroPanel5.Size = new Size(nWidth, nHeight);
            metroPanel6.Size = new Size(nWidth, nHeight);

            pIssuePanel.Size = new Size(nWidth, nHeight);
            pVoidPanel.Size = new Size(nWidth, nHeight);
            pSearchPanel.Size = new Size(nWidth, nHeight);
            pLogbookPanel.Size = new Size(nWidth, nHeight);
            pRefundCalculatorPanel.Size = new Size(nWidth, nHeight);


            Size = new Size(nWidth, nHeight);
            metroPanel1.Visible = true;
            metroPanel2.Visible = false;
            metroPanel3.Visible = false;
            metroPanel4.Visible = false;
            metroPanel5.Visible = false;
            metroPanel6.Visible = false;
            metroPanel1.Refresh();

            /*            
            metroPanel1.Visible = false;
            metroPanel2.Visible = true;
            metroPanel3.Visible = false;
            metroPanel4.Visible = false;
            metroPanel2.Refresh();
            */
            
            this.Activate();
            //backgroundWorker1.RunWorkerAsync();
            //단말기 초기 세팅 시 환경설정 화면으로 전환

            //Main2_SizeChanged(null,null);

            //최초생성시 좌표, 크기 조정여부 등록함. 화면별로 Manager 를 가진다. 
            this.Size = new Size(1195, 728);

            m_CtlSizeManager = new ControlManager(this);

            //종횡 늘림
            m_CtlSizeManager.addControlMove(metroPanel1, false, false, true, true);
            m_CtlSizeManager.addControlMove(metroPanel2, false, false, true, true);
            m_CtlSizeManager.addControlMove(metroPanel3, false, false, true, true);
            m_CtlSizeManager.addControlMove(metroPanel4, false, false, true, true);
            m_CtlSizeManager.addControlMove(metroPanel5, false, false, true, true);
            m_CtlSizeManager.addControlMove(metroPanel6, false, false, true, true);

            m_CtlSizeManager.addControlMove(pIssuePanel, false, false, true, true);
            m_CtlSizeManager.addControlMove(pVoidPanel, false, false, true, true);
            m_CtlSizeManager.addControlMove(pSearchPanel, false, false, true, true);
            m_CtlSizeManager.addControlMove(pPreferencesPanel, false, false, true, true);
            m_CtlSizeManager.addControlMove(pLogbookPanel, false, false, true, true);
            m_CtlSizeManager.addControlMove(pRefundCalculatorPanel, false, false, true, true);

            //종늘림
            m_CtlSizeManager.addControlMove(TIL_LINE_1, false, false, false, true);
            m_CtlSizeManager.addControlMove(TIL_EMPTY_1, false, false, false, true);

            //횡이동
            m_CtlSizeManager.addControlMove(TIL_USERNAME, false, true, false, false);
            m_CtlSizeManager.addControlMove(TIL_NETWORK, false, true, false, false);
            m_CtlSizeManager.addControlMove(TIL_EMPTY_2, false, true, false, false);

            //m_CtlSizeManager.addControlMove(pictureBox_Outlet, false, true, false, false); 이미지 제거
            m_CtlSizeManager.addControlMove(label_Copyright, false, true, false, false);

            if (m_CtlSizeManager != null)
                m_CtlSizeManager.MoveControls();
        }

        private void TIL_ISSUE_Click(object sender, EventArgs e)
        {
            metroPanel1.Visible = true;
            metroPanel2.Visible = false;
            metroPanel3.Visible = false;
            metroPanel4.Visible = false;
            metroPanel5.Visible = false;
            metroPanel6.Visible = false;
            metroPanel1.Refresh();
            pIssuePanel.CLEAR();
        }

        private void TIL_VOID_Click(object sender, EventArgs e)
        {
            metroPanel1.Visible = false;
            metroPanel2.Visible = true;
            metroPanel3.Visible = false;
            metroPanel4.Visible = false;
            metroPanel5.Visible = false;
            metroPanel6.Visible = false;
            metroPanel2.Refresh();
        }

        private void TIL_SEARCH_Click(object sender, EventArgs e)
        {
            metroPanel1.Visible = false;
            metroPanel2.Visible = false;
            metroPanel3.Visible = true;
            metroPanel4.Visible = false;
            metroPanel5.Visible = false;
            metroPanel6.Visible = false;
            metroPanel3.Refresh();
        }

        private void TIL_LOG_Click(object sender, EventArgs e)
        {
            metroPanel1.Visible = false;
            metroPanel2.Visible = false;
            metroPanel3.Visible = false;
            metroPanel4.Visible = false;
            metroPanel5.Visible = true;
            metroPanel6.Visible = false;
            metroPanel5.Refresh();
        }

        private void TIL_CAL_Click(object sender, EventArgs e)
        {
            metroPanel1.Visible = false;
            metroPanel2.Visible = false;
            metroPanel3.Visible = false;
            metroPanel4.Visible = false;
            metroPanel5.Visible = false;
            metroPanel6.Visible = true;
            metroPanel6.Refresh();
            pRefundCalculatorPanel.setFirstFocus();
        }

        private void TIL_PRE_Click(object sender, EventArgs e)
        {
            setupcheck.ShowDialog(this);
            setupcheck.initialize();
        }

        private void initConstant()
        {
            //Constants.PASSPORT_TYPE = 1;

        }

        //Maing 화면은 컨트롤 변경기능 하드코딩
        private void Main_SizeChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(nWidth.ToString());

            if (m_CtlSizeManager != null)
                m_CtlSizeManager.MoveControls();
            this.Refresh();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            Transaction tran = new Transaction();
            while (true)
            {
                if (!tran.checkNetworkStatus())
                {
                    Constants.LOGGER_MAIN.Error("--> network status check error");

                    setNetworkStatus(false);
                }
                else
                {
                    setNetworkStatus(true);
                }
                System.Threading.Thread.Sleep(1000*3);
            }
        }

        private void closeSetupCheck()
        {
            setupcheck.Hide();
            if(setupcheck.setup_visible)
            {
                metroPanel1.Visible = false;
                metroPanel2.Visible = false;
                metroPanel3.Visible = false;
                metroPanel4.Visible = true;
                metroPanel5.Visible = false;
                metroPanel6.Visible = false;
                metroPanel4.Refresh();
            }
        }


        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Confirm 메시지박스 호출
            DialogResult bResult = MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/LoginCancel"),
                "Main", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (bResult.Equals(DialogResult.Yes))
            {
                //프로그램 종료
                Application.ExitThread();
                Environment.Exit(0);
            }else
            {
                e.Cancel = true;
            }
        }

        public bool PreFilterMessage(ref Message m)
        {
            bool active = m.Msg == 0x100 || m.Msg == 0x101;  // WM_KEYDOWN/UP
            active = active || m.Msg == 0xA0;  // WM_(NC)MOUSEMOVE
            active = active || m.Msg == 0x201 || m.Msg == 0x204;  // WM_LBUTTONDOWN  WM_RBUTTONDOWN 
            active = active || m.Msg == 0x10;  // WM_CLOSE, in case dialog closes
            if (active)
            {
               // if (!mTimer.Enabled) lbl_active.Text = "Wakeup";
                mTimer.Enabled = false;
                mTimer.Start();
            }
            return false;
        }

        private void LogoutUser(object sender, EventArgs e)
        {
            // No activity, logout user
            mTimer.Stop();
            mTimer.Enabled = false;
            if (mDialogCount > 0) return;
            mDialogCount = 1;
            //lbl_active.Text = "Time'z up";
            pIssuePanel.CLEAR();
            pVoidPanel.CLEAR();
            pSearchPanel.CLEAR();
            pRefundCalculatorPanel.CLEAR();
            pLogbookPanel.CLEAR();
            this.Hide();
            if (!login())
            {
                Application.ExitThread();
                Environment.Exit(0);
            }
            else
            {
                this.Show();

                mTimer.Start();
                mDialogCount = - 1;
                // 이슈화면 으로 전환 및 모든 화면 클리어
                metroPanel1.Visible = true;
                metroPanel2.Visible = false;
                metroPanel3.Visible = false;
                metroPanel4.Visible = false;
                metroPanel5.Visible = false;
                metroPanel6.Visible = false;
                metroPanel1.Refresh();
            }
        }
    }
}
