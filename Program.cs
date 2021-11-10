using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conway
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            int gridWidth = 200;
            int gridHeight = 100;
            int pixelSize = 10; // pixels
            int computeInterval = 10; // ms
            int sparseness = 2;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow(gridWidth, gridHeight, pixelSize, computeInterval, sparseness));
        }
    }
}
