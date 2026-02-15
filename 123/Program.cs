using System;
using System.Windows.Forms;
using taxi4;

namespace TaxiClientApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm()); // Запуск с формы входа
        }
    }
}