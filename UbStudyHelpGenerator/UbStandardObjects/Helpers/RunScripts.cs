using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UbStudyHelpGenerator.UbStandardObjects.Helpers
{
    internal class RunScripts
    {
        private static string BashPath = @"C:\Users\CTW01001\AppData\Local\Programs\Git\bin\bash.exe";

         private static void GetLines(StreamReader reader, List<string> list)
        {
            string line = reader.ReadLine();
            while (line != null)
            {
                list.Add(line);
                //Program.Log.Info(line);
                line = reader.ReadLine();
            }
        }

        private static string WriteScript(List<string> commands, string extension = ".sh")
        {
            string bashScriptPath = Path.Combine(Path.GetTempPath(), $"automation{extension}");
            if (File.Exists(bashScriptPath))
            {
                File.Delete(bashScriptPath);
            }
            File.WriteAllLines(bashScriptPath, commands.ToArray());
            return bashScriptPath;
        }

        private static Process GetBashProcess()
        {
            Process pProcess = new System.Diagnostics.Process();
            pProcess.StartInfo.FileName = BashPath;
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.CreateNoWindow = true;
            pProcess.StartInfo.ErrorDialog = true;

            //Set output of program to be written to process output stream
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.StartInfo.RedirectStandardError = true;
            //Optional
            //pProcess.StartInfo.WorkingDirectory = workingDirectory;
            return pProcess;
        }

        private static void ExecuteProcess(Process pProcess, int timeoutMiliseconds = 10000)
        {
            //Start the process
            pProcess.Start();
            if (timeoutMiliseconds == 0)
            {
                pProcess.WaitForExit();
            }
            else
            {
                pProcess.WaitForExit(timeoutMiliseconds);
            }
            if (!pProcess.HasExited)
            {
                pProcess.Kill();
                pProcess.WaitForExit();
            }
        }

        public static string GetUnixPath(string path)
        {
            if (path.StartsWith(@"C:", StringComparison.OrdinalIgnoreCase))
            {
                path = path.Replace(@"C:", "/c");
                path = path.Replace(@"c:", "/c");
            }
            return path.Replace(@"\", "/");
        }


        public static List<string> BashExecuteGetLines(List<string> commands, ref bool errorOcurred, int timeoutMiliseconds = 10000)
        {
            List<string> list = new List<string>();
            try
            {
                string bashScriptPath = WriteScript(commands);

                Process pProcess = GetBashProcess();
                pProcess.StartInfo.Arguments = RunScripts.GetUnixPath(bashScriptPath);
                ExecuteProcess(pProcess, timeoutMiliseconds);

                //Get program output
                if (pProcess.ExitCode > 0)
                {
                    errorOcurred = true;
                    GetLines(pProcess.StandardError, list);
                    throw new Exception("Erro em ExecuteGetLines: ");
                //    foreach (string line in list)
                //    {
                //        Messenger.FireShowLogErrorMessage($"    {line}");
                //    }
                }
                else
                {
                    errorOcurred = false;
                    GetLines(pProcess.StandardOutput, list);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro em ExecuteGetLines: ", ex);
            }
        }


    }
}
