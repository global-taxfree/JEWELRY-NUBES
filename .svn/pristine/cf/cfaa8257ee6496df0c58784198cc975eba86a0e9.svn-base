using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using JEWELRY_NUBES.Screen;
using System.Diagnostics;

namespace JEWELRY_NUBES
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            foreach (var process in Process.GetProcessesByName("GTFS_U.exe"))
            {
                process.Kill();
            }

            if (!checkSingleInstance())
            {
                Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MetroFramework.Forms.MetroForm mainForm = new Main();
            Application.Run(mainForm);

            }
            else
            {
                Application.Exit();
            }
}

        static Boolean checkSingleInstance()
        {
            string procName = Process.GetCurrentProcess().ProcessName;
            // get the list of all processes by that name

            Process[] processes = Process.GetProcessesByName(procName);

            if (processes.Length > 1)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
