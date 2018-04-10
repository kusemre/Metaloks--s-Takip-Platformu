using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Metaloks_İş_Takip_Platformu
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new giris());
        }
    }
}
