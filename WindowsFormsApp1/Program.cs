using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            } catch (Exception ex) {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Errore, l'app verrà chiusa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            
        }
    }
}
