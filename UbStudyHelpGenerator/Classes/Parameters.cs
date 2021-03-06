using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace UbStudyHelpGenerator.Classes
{
    /// <summary>
    /// Parameters using new MS Json
    /// <see href="https://devblogs.microsoft.com/dotnet/try-the-new-system-text-json-apis/"/>
    /// </summary>
    public class Parameters
    {
        /// <summary>
        /// Git associated repository folder
        /// </summary>
        public string RepositoryOutputFolder { get; set; } = "";

        public string InputHtmlFilesPath { get; set; } = "";


        public string OutputHtmlFilesPathFromSql { get; set; } = "";

        public string IndexDownloadedFiles { get; set; } = "";

        public string IndexOutputFilesPath { get; set; } = "";

        /// <summary>
        /// Serialize the parameters instance
        /// </summary>
        /// <param name="p"></param>
        /// <param name="pathParameters"></param>
        public static void Serialize(Parameters p, string pathParameters)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize<Parameters>(p);
                File.WriteAllText(pathParameters, jsonString);
            }
            catch { }
        }

        /// <summary>
        /// Deserialize the parameters instance
        /// </summary>
        /// <param name="pathParameters"></param>
        /// <returns></returns>
        public static Parameters Deserialize(string pathParameters)
        {
            try
            {
                var jsonString = File.ReadAllText(pathParameters);

                var options = new JsonSerializerOptions
                {
                    AllowTrailingCommas = true
                };
                return JsonSerializer.Deserialize<Parameters>(jsonString, options);
            }
            catch
            {
                return new Parameters();
            }
        }



    }
}
