using System;
using System.Collections.Generic;
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
        Separator = 5,
        PartIntroduction = 6
    }


    public class ParagraphExport
    {
        public const string CssClassesDivSize = "p-3 mb-2 ";

        public short Paper { get; set; } = -1;
        public short Pk_seq { get; set; } = -1;
        public short Section { get; set; } = -1;
        public short Paragraph { get; set; }
        public short Page { get; set; } = 0;
        public short Line { get; set; } = 0;
        public string Text { get; set; } = "";
        public int Format { get; set; } = -1;
        public int Status { get; set; } = -1;
        public int Part
        {
            get
            {
                if (Paper == 0) return 0;
                if (Paper > 0 && Paper <= 31) return 1;
                if (Paper > 31 && Paper <= 56) return 2;
                if (Paper > 56 && Paper <= 119) return 3;
                return 4;
            }
        }

            
        public string CssClass
        {
            get
            {
                switch (Status)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        ParagraphExportStatus parStatus = (ParagraphExportStatus)Status;
                        var field = parStatus.GetType().GetField(parStatus.ToString());
                        var attr = field?.GetCustomAttribute<StringValueAttribute>();
                        return CssClassesDivSize + " " + attr?.Value ?? parStatus.ToString();
                    default:
                        ParagraphExportHtmlType type = (ParagraphExportHtmlType)Format;
                        switch (type)
                        {
                            case ParagraphExportHtmlType.Separator:
                                return CssClassesDivSize + "parSeparator";
                            case ParagraphExportHtmlType.PartIntroduction:
                                return CssClassesDivSize + "parTitle";
                        }
                        break;
                }
                return CssClassesDivSize + "parNormal"; 
            }
        }

        public string Ident => $"{Paper}:{Section}-{Paragraph}";
        public bool IsIntroduction => Paper == 0;
        public bool IsPaperTitle => Format == (int)ParagraphExportHtmlType.PaperTitle;
        public bool IsSectionTitle => Format == (int)ParagraphExportHtmlType.SectionTitle;
        public bool IsSeparator => Format == (int)ParagraphExportHtmlType.Separator;
        public bool IsStartingPart => Paper == 1 || Paper == 32 || Paper == 57 || Paper == 120;
        public string Title => $"{Paper.ToString().PadLeft(3, ' ')} - {Text}";

        public string Reference { get { return $"{Paper}:{Section}-{Paragraph} ({Page}-{Line})"; } }

        public string RepositoryFileName { get { return $"Par_{Paper:000}_{Section:000}_{Paragraph:000}.md"; } }

        public string Anchor(bool isLeft)
        {
            return $"<a name= \"a{Paper:000}_{Section:000}_{Paragraph:000}_{(isLeft ? "L" : "R")}\"/>";
        }

        public string HtmlId(bool isLeft)
        {
            return $"p{Paper:000}_{Section:000}_{Paragraph:000}_{(isLeft ? "L" : "R")}";
        }

        public override string ToString()
        {
            return Pk_seq.ToString().PadLeft(3, ' ') + " - " + Reference + " " + Text.Substring(0, Math.Min(50, Text.Length));
        }

    }
}
