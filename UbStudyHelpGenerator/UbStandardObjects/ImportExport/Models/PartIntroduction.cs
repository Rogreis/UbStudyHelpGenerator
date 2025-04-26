namespace UbStudyHelpGenerator.UbStandardObjects.ImportExport.Models
{
    public class ParagraphSpecial
    {
        public short Paper { get; set; }= 0;
        public short pk_seq { get; set; }= -1;
        public string Text { get; set; } = "";
        public short Format { get; set; } = (short)ParagraphExportHtmlType.PartIntroduction;
    }
}
