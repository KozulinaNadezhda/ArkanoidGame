using System;
using System.Windows.Forms;
using ArkanoidGame.UI;

namespace ArkanoidGame
{
    internal static class Program
    {
        [STAThread]
        static void Main()// ����� ����� � ����������
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartForm());
        }
    }
}



