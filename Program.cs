using System;
using System.Drawing;
using System.Windows.Forms;

namespace FinalProject
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 userOneWindow = new Form1("User 1");
            Form1 userTwoWindow = new Form1("User 2");

            userOneWindow.StartPosition = FormStartPosition.Manual;
            userOneWindow.Location = new Point(100, 100);

            userTwoWindow.StartPosition = FormStartPosition.Manual;
            userTwoWindow.Location = new Point(180, 140);

            userTwoWindow.Show();

            Application.Run(userOneWindow);
        }
    }
}