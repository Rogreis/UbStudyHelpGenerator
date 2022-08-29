using System;
using System.IO;
using System.Windows.Forms;
using UbStandardObjects;
using UbStandardObjects.Objects;
using UbStudyHelpGenerator.Classes;
using static System.Environment;

namespace UbStudyHelpGenerator
{
    static class Program
    {

        private static string pathParameters;

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
            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine("N\u00E4iden sis\u00E4isten muodonmuutosten avulla MIN\u00C4 OLEN laskee perustan seitsenkertaiselle suhteelle itsens\u00E4 kanssa. Filosofinen (ajallinen) k\u00E4sitys yksin\u00E4isest\u00E4 MIN\u00C4 OLEN -olevaisesta ja v\u00E4livaihetta koskeva (ajallinen) k\u00E4sitys MIN\u00C4 OLEN -olevaisesta{z{z{105:2.4 *}z}z} kolmiyhteisen\u00E4 on nyt laajennettavissa niin, ett\u00E4 se sis\u00E4lt\u00E4\u00E4 MIN\u00C4 OLEN -olevaisen{z{z{105:2.4 *}z}z} seitsenkertaisena. T\u00E4m\u00E4 seitsenkertainen \u2014 tai seitsenvaiheinen \u2014 olemus on hahmoteltavissa parhaiten suhteessa Infiniittisyyden Seitsem\u00E4\u00E4n Absoluuttiin:\", ");

            //string y= Exemplar.ExemplarToHtml(sb.ToString());

            pathParameters = MakeProgramDataFolder("UbStudyHelpGenerator.json");
            StaticObjects.Logger = new GeneratorLog();
            StaticObjects.Parameters= ParametersGenerator.Deserialize(pathParameters);
            StaticObjects.Parameters.ApplicationFolder= Application.StartupPath;
            StaticObjects.Book= new Book();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());

            ParametersGenerator.Serialize((ParametersGenerator)UbStandardObjects.StaticObjects.Parameters, pathParameters);

        }
    }
}
