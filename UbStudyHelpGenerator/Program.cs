using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UbStudyHelpGenerator.Classes;
using static System.Environment;

namespace UbStudyHelpGenerator
{
    static class Program
    {

        private static string pathParameters;
        public static Parameters ParametersData = new Parameters();

        private static string MakeProgramDataFolder(string fileName)
        {
            string processName = System.IO.Path.GetFileNameWithoutExtension(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var commonpath = GetFolderPath(SpecialFolder.CommonApplicationData);
            string folder = Path.Combine(commonpath, processName);
            Directory.CreateDirectory(folder);
            return Path.Combine(folder, fileName);
        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            pathParameters = MakeProgramDataFolder("UbStudyHelpGenerator.json");
            ParametersData = Parameters.Deserialize(pathParameters);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());

            Parameters.Serialize(ParametersData, pathParameters);

        }
    }
}
