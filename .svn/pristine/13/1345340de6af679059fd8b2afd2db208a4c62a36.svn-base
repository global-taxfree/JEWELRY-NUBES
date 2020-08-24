using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;

using MetroFramework;
using MetroFramework.Forms;

using GTF_Passport;
using GTF_Printer;
using JEWELRY_NUBES.Util;



namespace JEWELRY_NUBES.Screen
{
    public partial class PreferencesPanel : UserControl
    {
        ControlManager m_CtlSizeManager = null;
        const long OPOS_SUCCESS = 0;
        const long OPOS_E_CLOSED = 101;
        const long OPOS_E_CLAIMED = 102;
        const long OPOS_E_NOTCLAIMED = 103;
        const long OPOS_E_NOSERVICE = 104;
        const long OPOS_E_DISABLED = 105;
        const long OPOS_E_ILLEGAL = 106;
        const long OPOS_E_NOHARDWARE = 107;
        const long OPOS_E_OFFLINE = 108;
        const long OPOS_E_NOEXIST = 109;
        const long OPOS_E_EXISTS = 110;
        const long OPOS_E_FAILURE = 111;
        const long OPOS_E_TIMEOUT = 112;
        const long OPOS_E_BUSY = 113;
        const long OPOS_E_EXTENDED = 114;
        const int PTR_S_RECEIPT = 2;

        public PreferencesPanel(ILog Logger = null)
        {
            InitializeComponent();
            //최초생성시 좌표, 크기 조정여부 등록함. 화면별로 Manager 를 가진다. 
            m_CtlSizeManager = new ControlManager(this);

            //횡이동
            m_CtlSizeManager.addControlMove(BTN_SAVE, true, false, false, false);
            m_CtlSizeManager.addControlMove(BTN_DOWNLOAD, true, false, false, false);
            m_CtlSizeManager.addControlMove(BTN_HELP, true, false, false, false);

            m_CtlSizeManager.addControlMove(TIL_1, false, false, true, false);
            m_CtlSizeManager.addControlMove(LAY_CONFIG, false, false, true, false);
            m_CtlSizeManager.addControlMove(TIL_2, false, false, true, false);
        }

        private void PreferencesPanel_Load(object sender, EventArgs e)
        {
            COM_PRINTER.Items.Clear();
            //COM_PRINTER.SelectedIndex = -1;
            COM_PASS_SCAN.SelectedIndex = 1;//로컬 저장 Scanner 설정


            PrinterSettings settings = new PrinterSettings();
            string strDeaultPrinter = "";
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                COM_PRINTER.Items.Add(printer);
                settings.PrinterName = printer;
                if (settings.IsDefaultPrinter)   // 기본 설정 여부
                    strDeaultPrinter = printer;
            }
            if(!"".Equals(strDeaultPrinter))
            {
                COM_PRINTER.SelectedItem = strDeaultPrinter;
            }

            COM_PASS_SCAN.SelectedIndex = Constants.PASSPORT_TYPE;
            COM_PRINTER.SelectedItem = Constants.PRINTER_TYPE;
            COM_OPOS.SelectedItem = Constants.PRINTER_OPOS_TYPE;
            COM_PRINT_SELECT.SelectedItem = Constants.PRINTER_SELECT;
            COM_PRINTER.DropDownWidth = DropDownWidth(COM_PRINTER);
        }

        private void BTN_SCAN_TEST_Click(object sender, EventArgs e)
        {
            GTF_PassportScanner passScan = GTF_PassportScanner.Instance();

            try
            {
                setWaitCursor(true);    
                int nRet = 0;
                passScan.close();
                if ("GTF".Equals(COM_PASS_SCAN.SelectedItem))
                {
                    nRet = passScan.open(0);
                }
                else if ("OKPOS".Equals(COM_PASS_SCAN.SelectedItem))
                {
                    nRet = passScan.open(3);
                }
                else if ("WISESCAN420".Equals(COM_PASS_SCAN.SelectedItem))
                {
                    nRet = passScan.open(1);
                }
                else if ("DAWIN(GTF SG)".Equals(COM_PASS_SCAN.SelectedItem))
                {
                    nRet = passScan.open(2);
                }

                if (nRet > 0)
                {
                    int strmrz = passScan.scan(30);
                    if (strmrz > 0)
                    {
                        string strTempData = "Passport Data\n" + passScan.getMRZ1() + "\n" + passScan.getMRZ2();
                        MetroMessageBox.Show(this, strTempData, "Passport Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MetroMessageBox.Show(this, "Please check if passport is inserted into slot", "Passport Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MetroMessageBox.Show(this, "Passport scanner not connected", "Passport Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                passScan.close();
                setWaitCursor(false);
                BTN_SCAN_TEST.Focus();
            }
        }

        //private void BTN_PRINT_TEST_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        setWaitCursor(true);
        //        if (COM_PRINTER.SelectedItem == null || "".Equals(COM_PRINTER.SelectedItem.ToString().Trim()))
        //        {
        //            MetroMessageBox.Show(this, "Printer is not working properly", "Passport Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        else {
        //            GTF_ReceiptPrinter printer = new GTF_ReceiptPrinter(null);
        //            printer.setPrinter(COM_PRINTER.SelectedItem.ToString());
        //            printer.PrintSlip_Test();
        //        }
        //    }
        //    finally
        //    {
        //        setWaitCursor(false);
        //        BTN_PRINT_TEST.Focus();
        //    }
        //    /*
        //    PrintDocument printDoc = new PrintDocument();
        //    printDoc.PrinterSettings.PrinterName = COM_PRINTER.SelectedItem.ToString();
        //    printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
        //    printDoc.Print();
        //    */
        //}

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

        private void PreferencesPanel_SizeChanged(object sender, EventArgs e)
        {
            if (m_CtlSizeManager != null)
            {
                m_CtlSizeManager.MoveControls();
                this.Refresh();
            }
        }

        private void BTN_OPOS_TEST_Click(object sender, EventArgs e)
        {
            try
            {
                setWaitCursor(true);
                if(COM_OPOS.SelectedItem != null && !"".Equals(COM_OPOS.SelectedItem.ToString()))
                {
                    GTF_ReceiptPrinter printer = new GTF_ReceiptPrinter(null);
                    printer.PrintSlip_sg(COM_OPOS.SelectedItem.ToString());
                    
                }
                
            }
            finally
            {
                setWaitCursor(false);
                BTN_OPOS_TEST.Focus();
            }
        }

        private void BTN_SAVE_Click(object sender, EventArgs e)
        {
            try
            {
                setWaitCursor(true);

                //Constants 저장
                Constants.PASSPORT_TYPE = COM_PASS_SCAN.SelectedIndex;
                Constants.PRINTER_TYPE = COM_PRINTER.SelectedItem.ToString();
                if(COM_OPOS.SelectedItem != null)
                    Constants.PRINTER_OPOS_TYPE = COM_OPOS.SelectedItem.ToString();
                Constants.SLIP_TYPE = 0;
                Constants.PRINTER_SELECT = COM_PRINT_SELECT.SelectedItem.ToString();

                //LOCAL DB 저장
                Constants.LDB_MANAGER.updateConfigData("PASSPORT_TYPE", "" + Constants.PASSPORT_TYPE, "여권스캐너");
                Constants.LDB_MANAGER.updateConfigData("PRINTER_TYPE", Constants.PRINTER_TYPE, "영수증프린터");
                Constants.LDB_MANAGER.updateConfigData("PRINTER_OPOS_TYPE", Constants.PRINTER_OPOS_TYPE, "영수증프린터");
                Constants.LDB_MANAGER.updateConfigData("PRINTER_SELECT", Constants.PRINTER_SELECT, "프린터 선택");
                MetroMessageBox.Show(this, "Saved completely!", "Config Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                setWaitCursor(false);
            }
                
        }
        private int DropDownWidth(ComboBox myCombo)
        {
            int maxWidth = 0;
            int temp = 0;
            Label label1 = new Label();

            foreach (var obj in myCombo.Items)
            {
                label1.Text = obj.ToString();
                temp = label1.PreferredWidth;
                if (temp > maxWidth)
                {
                    maxWidth = temp;
                }
            }
            label1.Dispose();
            return maxWidth;
        }

        private void BTN_PRINT_TEST_Click(object sender, EventArgs e)
        {
            try
            {
                setWaitCursor(true);
                if (COM_PRINTER.SelectedItem == null || "".Equals(COM_PRINTER.SelectedItem.ToString().Trim()))
                {
                    MetroMessageBox.Show(this, "Printer is not working properly", "Passport Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    GTF_ReceiptPrinter printer = new GTF_ReceiptPrinter(null);
                    printer.setPrinter(COM_PRINTER.SelectedItem.ToString());
                    printer.PrintSlip_Test();
                }
            }
            finally
            {
                setWaitCursor(false);
                BTN_PRINT_TEST.Focus();
            }
        }

        private void COM_PRINT_SELECT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(COM_PRINT_SELECT.SelectedIndex == 0)
            {
                COM_PRINTER.Enabled = true;
                BTN_PRINT_TEST.Enabled = true;
                COM_OPOS.Enabled = false;
                BTN_OPOS_TEST.Enabled = false;
            }
            else
            {
                COM_PRINTER.Enabled = false;
                BTN_PRINT_TEST.Enabled = false;
                COM_OPOS.Enabled = true;
                BTN_OPOS_TEST.Enabled = true;
            }
        }
    }
}
