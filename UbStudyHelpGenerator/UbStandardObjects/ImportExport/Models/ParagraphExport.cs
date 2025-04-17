using System;
using System.Reflection;
using UbStudyHelpGenerator.UbStandardObjects.Objects;

namespace UbStudyHelpGenerator.UbStandardObjects.ImportExport.Models
{

    [AttributeUsage(AttributeTargets.Field)]
    public class StringValueAttribute : Attribute
    {
        public string Value { get; }
        public StringValueAttribute(string value) => Value = value;
    }

    /// <summary>
    /// Represents the translation status of each paragraph being worked.
    /// </summary>
    public enum ParagraphExportStatus
    {
        [StringValue("parStarted")]
        Started = 0,

        [StringValue("parWorking")]
        Working = 1,

        [StringValue("parDoubt")]
        Doubt = 2,

        [StringValue("parOk")]
        Ok = 3,

        [StringValue("parClosed")]
        Closed = 4
    }

    /// <summary>
    /// Represents the html format for a paragraph
    /// </summary>
    public enum ParagraphExportHtmlType
    {
        BookTitle = 0,
        PaperTitle = 1,
        SectionTitle = 2,
        NormalParagraph = 3,
        IdentedParagraph = 4,
        Divider = 5,
        PartIntroduction = 6
    }


    public class ParagraphExport
    {
        public short Paper { get; set; } = -1;
        public short Pk_seq { get; set; } = -1;
        public short Section { get; set; } = -1;
        public short Paragraph { get; set; }
        public short Page { get; set; } = 0;
        public short Line { get; set; } = 0;
        public string Text { get; set; } = "";
        public int Format { get; set; } = -1;
        public int Status { get; set; } = -1;

        public const string CssClassesDivSize = "p-3 mb-2 ";

        public string Reference
        {
            get
            {
                return $"{Paper}:{Section}-{Paragraph} ({Page}-{Line})";
            }
        }

        public string Anchor(bool isLeft)
        {
            return $"<a name= \"a{Paper:000}_{Section:000}_{Paragraph:000}_{(isLeft? "L" : "R")}\"/>";
        }

        public string CssClass
        {
            get
            {
                ParagraphExportStatus parStatus = (ParagraphExportStatus)Status;
                var field = parStatus.GetType().GetField(parStatus.ToString());
                var attr = field?.GetCustomAttribute<StringValueAttribute>();
                return CssClassesDivSize + " "+ attr?.Value ?? parStatus.ToString();
            }
        }


        public string HtmlId(bool isLeft)
        {
            return $"p{Paper:000}_{Section:000}_{Paragraph:000}_{(isLeft ? "L" : "R")}";
        }

        private string FullTextLink()
        {
            return $"<a href=\"#\" onclick=\"event.preventDefault(); generateUrlAndOpen('105:0-0');\" class=\"{CssClass}\" target=\"_blank\">{Text}</a>";
        }

        // 

        public string HtmlText()
        {
            ParagraphExportHtmlType parFormat = (ParagraphExportHtmlType)Format;
            switch (parFormat)
            {
                case ParagraphExportHtmlType.BookTitle:
                    return $"<h2>{Text}</h2>";
                case ParagraphExportHtmlType.PaperTitle:
                    return $"<h3>{FullTextLink()}</h3>";
                case ParagraphExportHtmlType.SectionTitle:
                    return $"<h4>{FullTextLink()}</h4>";
                case ParagraphExportHtmlType.NormalParagraph:
                    return $"{Reference}  {Text}";
                case ParagraphExportHtmlType.IdentedParagraph:
                    return $"<bloquote>{Reference}  {Text}</bloquote>";
            }
            return "*** Format not set ***";
        }



        public override string ToString()
        {
            return Pk_seq.ToString().PadLeft(3, ' ') + " - " + Reference + " " + Text.Substring(0, Math.Min(50, Text.Length));
        }

    }
}
