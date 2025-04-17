using System.Collections.Generic;

namespace UbStudyHelpGenerator.UbStandardObjects.ImportExport.Models
{
    public class PaperExport
    {
        public short PaperNo { get; set; }
        public List<ParagraphExport> Paragraphs { get; set; }= new List<ParagraphExport>();

        public PaperExport(short paperNo)
        {
            PaperNo= paperNo;
        }

        public override string ToString()
        {
            return PaperNo.ToString();
        }


    }
}
