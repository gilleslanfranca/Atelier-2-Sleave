using System;
using System.Windows.Forms;

namespace Sleave
{
    /// <summary>
    /// Classe d'entrée du programme
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new control.Controller();
        }
    }
}
