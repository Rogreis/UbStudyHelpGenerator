using System;
using System.IO;
using System.Windows.Forms;
using UbStudyHelpGenerator.Classes;
using UbStudyHelpGenerator.UbStandardObjects;
using UbStudyHelpGenerator.UbStandardObjects.Objects;
using static System.Environment;

namespace UbStudyHelpGenerator
{
    static class Program
    {

        public static string PathParameters;

        private static string DataFolder()
        {
            string processName = System.IO.Path.GetFileNameWithoutExtension(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var commonpath = GetFolderPath(SpecialFolder.CommonApplicationData);
            return Path.Combine(commonpath, processName);
        }


        private static string MakeProgramDataFolder(string fileName)
        {
            string folder = DataFolder();
            Directory.CreateDirectory(folder);
            return Path.Combine(folder, fileName);
        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            PathParameters = MakeProgramDataFolder("UbStudyHelpGenerator.json");
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());

            Parameters.Serialize(StaticObjects.Parameters, PathParameters);

        }
    }
}
