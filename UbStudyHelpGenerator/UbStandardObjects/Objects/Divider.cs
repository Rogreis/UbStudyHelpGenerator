using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace UbStudyHelpGenerator.UbStandardObjects.Objects
{

    public class Divider
    {
        public int Paper { get; set; }
        public int Section { get; set; }
        public int Paragraph { get; set; }
        public int PK_Seq { get; set; }
    }

    public class PaperDividers
    {
        public Divider[] Dividers { get; set; }

        public PaperDividers()
        {
        }

        public void Load()
        {
            string pathDividersJsonFile = Path.Combine(System.Windows.Forms.Application.StartupPath, @"Data\Dividers.json");
            string json = File.ReadAllText(pathDividersJsonFile);
            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                WriteIndented = true,
                IncludeFields = true
            };

            PaperDividers paperDividers = JsonSerializer.Deserialize<PaperDividers>(json, options);
            Dividers = paperDividers.Dividers;
        }


        public List<int> Papers
        {
            get
            {
                return Dividers.Select(p => p.Paper).Distinct().ToList();
            }
        }

    }



}
