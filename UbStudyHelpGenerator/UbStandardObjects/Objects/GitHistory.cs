using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UbStudyHelpGenerator.UbStandardObjects.Objects
{
    public class GitHistory
    {

        public event dlShowMessage ShowMessage = null;


        public void Get(string repositoryPath, string filePath, string outputHtmlFolder)
        {
            string folderCompare= Path.Combine(outputHtmlFolder, "Compare");
            Directory.CreateDirectory(folderCompare);
            string outputHtmlPath = Path.Combine(folderCompare, Path.GetFileNameWithoutExtension(filePath) + ".htm");
            var changes = GetFileChangeHistory(repositoryPath, filePath);
            WriteChangesToHtml(changes, outputHtmlPath);

            //int i = 0;
            //foreach (var change in changes)
            //{
            //    ShowMessage?.Invoke($"{i:00} {change}");
            //    i++;
            //}
        }

        private void WriteChangesToHtml(List<string> changes, string outputHtmlPath)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append("<!DOCTYPE html><html><head><title>File Change History</title>");
            stringBuilder.Append("<style>body{font-family:Arial, sans-serif;}pre{background-color:#f6f8fa;padding:16px;}</style>");
            stringBuilder.Append("</head><body><h1>File Change History</h1><pre>");

            foreach (var change in changes)
            {
                stringBuilder.Append(System.Net.WebUtility.HtmlEncode(change));
                stringBuilder.Append(Environment.NewLine);
            }

            stringBuilder.Append("</pre></body></html>");

            File.WriteAllText(outputHtmlPath, stringBuilder.ToString());
        }


        private List<string> GetFileChangeHistory(string repositoryPath, string filePath)
        {
            var result = new List<string>();

            var processStartInfo = new ProcessStartInfo
            {
                FileName = "git",
                Arguments = $"-C \"{repositoryPath}\" log -p -- \"{filePath}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = new Process
            {
                StartInfo = processStartInfo
            };

            process.Start();

            while (!process.StandardOutput.EndOfStream)
            {
                string line = process.StandardOutput.ReadLine();
                result.Add(line);
            }

            process.WaitForExit();

            return result;
        }


    }
}
