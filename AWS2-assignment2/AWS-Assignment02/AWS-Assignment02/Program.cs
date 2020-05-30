using System;
using System.Windows.Forms;

namespace AWS_Assignment02
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
            Application.Run(new Main());
            //Application.Run(new Translate());
            //Application.Run(new VoiceTrans());
            //Application.Run(new ocr());
            //Application.Run(new unsafeText());
        }
    }
}
