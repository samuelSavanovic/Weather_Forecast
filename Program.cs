using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Weather_Forecast {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool result;
            var mutex = new System.Threading.Mutex(true, "Weather Forecast", out result);
            if (!result) {
                MessageBox.Show("Another instance is already running!");
                return;
            }
            
           Application.Run(new Form2());
         
            
            GC.KeepAlive(mutex);
        }
    }
}
