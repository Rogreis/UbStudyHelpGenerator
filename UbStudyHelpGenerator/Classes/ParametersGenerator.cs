using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using UbStandardObjects;

namespace UbStudyHelpGenerator.Classes
{
    /// <summary>
    /// Parameters using new MS Json
    /// <see href="https://devblogs.microsoft.com/dotnet/try-the-new-system-text-json-apis/"/>
    /// </summary>
    public class ParametersGenerator : Parameters
    {

        public string SqlEdit { get; set; }= "SELECT dbo.FormatIdentity(Paper, Pk_Seq) as [Identity], [IndexWorK],[Paper],[PK_Seq],[Text],[Notes],[Status] FROM [dbo].[CaioComments] where Status = 1";
        /// <summary>
        /// Serialize the parameters instance
        /// </summary>
        /// <param name="p"></param>
        /// <param name="pathParameters"></param>
        public static void Serialize(ParametersGenerator p, string pathParameters)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize<ParametersGenerator>(p);
                File.WriteAllText(pathParameters, jsonString);
            }
            catch { }
        }

        /// <summary>
        /// Deserialize the parameters instance
        /// </summary>
        /// <param name="pathParameters"></param>
        /// <returns></returns>
        public static ParametersGenerator Deserialize(string pathParameters)
        {
            try
            {
                var jsonString = File.ReadAllText(pathParameters);

                var options = new JsonSerializerOptions
                {
                    AllowTrailingCommas = true
                };
                return JsonSerializer.Deserialize<ParametersGenerator>(jsonString, options);
            }
            catch
            {
                return new ParametersGenerator();
            }
        }



    }
}
