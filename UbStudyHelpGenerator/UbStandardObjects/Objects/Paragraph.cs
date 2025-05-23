﻿using System;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace UbStudyHelpGenerator.UbStandardObjects.Objects
{

    /// <summary>
    /// Represents the translation status of each paragraph being worked.
    /// </summary>
    public enum ParagraphStatus
    {
        Started = 0,
        Working = 1,
        Doubt = 2,
        Ok = 3,
        Closed = 4
    }

    /// <summary>
    /// Represents the html format for a paragraph
    /// </summary>
    public enum ParagraphHtmlType
    {
        BookTitle = 0,
        PaperTitle = 1,
        SectionTitle = 2,
        NormalParagraph = 3,
        IdentedParagraph = 4,
        Divider= 5,
        PartIntroduction = 6
    }


    public class Paragraph
    {
        [JsonPropertyName("TranslationID")]
        public short TranslationId { get; set; } = 0;
        public short Paper { get; set; }
        public short PK_Seq { get; set; }
        public short Section { get; set; }
        public short ParagraphNo { get; set; }
        public short Page { get; set; }
        public short Line { get; set; }
        public int FormatInt { get; set; }

        private string _text { get; set; } = "";

        public string Text 
        {
            get { return ToHtml(_text); }
            set { _text = value; } 
        }

        public string RawText { get; set; } = "";


        [JsonIgnore]
        private TOC_Entry entry = null;

        /// <summary>
        /// Status is not to be exported to json files for UbStudyHelp
        /// </summary>
        [JsonIgnore]
        [JsonPropertyName("Status")]
        public int _status { get; set; }

        [JsonIgnore]
        public ParagraphStatus Status
        {
            get
            {
                return (ParagraphStatus)_status;
            }
            set
            {
                _status = (int)value;
            }
        }


        public ParagraphHtmlType Format
        {
            get
            {
                return (ParagraphHtmlType)FormatInt;
            }
        }


        [JsonIgnore]
        public string Identification
        {
            get
            {
                return string.Format("{0}:{1}-{2} ({3}.{4})", Paper, Section, ParagraphNo, Page, Line); ;
            }
        }

        [JsonIgnore]
        public TOC_Entry Entry
        {
            get
            {
                if (entry == null)
                {
                    entry = new TOC_Entry(TranslationId, Paper, Section, ParagraphNo, Page, Line);
                    entry.Text = Text;
                }
                return entry;
            }
        }


        [JsonIgnore]
        public string ID
        {
            get
            {
                return string.Format($"{Paper}:{Section}-{ParagraphNo}");
            }
        }

        [JsonIgnore]
        public bool IsDivider
        {
            get
            {
                return Text.StartsWith("* * *") || Text.StartsWith("~ ~ ~");
            }
        }


        [JsonIgnore]
        public string AName
        {
            get
            {
                return string.Format("U{0}_{1}_{2}", Paper, Section, ParagraphNo); ;
            }
        }

        [JsonIgnore]
        public bool IsPaperTitle
        {
            get
            {
                return Section == 0 && ParagraphNo == 0;
            }
        }

        [JsonIgnore]
        public bool IsSectionTitle
        {
            get
            {
                return ParagraphNo == 0;
            }
        }

        [JsonIgnore]
        public string ParaIdent
        {
            get
            {
                return Paper.ToString("000") + PK_Seq.ToString("000");
            }
        }


        [JsonIgnore]
        public bool IsEditTranslation { get; set; } = false;

        [JsonIgnore]
        public string TextNoHtml
        {
            get => Regex.Replace(Text, "<.*?>", String.Empty);
        }

        public Paragraph()
        {
        }

        public Paragraph(Paragraph fromParagraph)
        {
            Text = "** text not found: error";
            FormatInt = fromParagraph.FormatInt;
            Paper = fromParagraph.Paper;
            Section = fromParagraph.Section;
            ParagraphNo = fromParagraph.ParagraphNo;
            TranslationId = fromParagraph.TranslationId;
            Line = fromParagraph.Line;
            Page = fromParagraph.Page;
        }


        public static Paragraph DefaultParagraph(Paragraph fromParagraph)
        {
                Paragraph paragraph = new Paragraph();
                paragraph.Text = "** text not found: error";
                paragraph.FormatInt = (int)ParagraphHtmlType.NormalParagraph;
                return paragraph;
        }


        public Paragraph(short translationId)
        {
            TranslationId = translationId;
        }

        public string FormatText(bool useAnchor, string startTag, string endTag= "")
        {
            if (endTag == "") endTag = startTag;
            return $"<{startTag}{(useAnchor ? $" id=\"{AName}\"" : "" )}>{ID} {Text}</{endTag}>";
        }



        // Add this if using nested MemberwiseClone.
        // This is a class, which is a reference type, so cloning is more difficult.
        public Paragraph DeepCopy()
        {
            // Clone the root ...
            Paragraph other = (Paragraph)this.MemberwiseClone();
            return other;
        }

        public static string ToHtml(string text)
        {
            return text.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&nbsp;", " ").Replace("&quot;", "\"").Replace("&amp;", "&").Trim();
        }

        public override string ToString()
        {
            string partText = Text;
            if (string.IsNullOrEmpty(Text))
            {
                partText = "";
            }
            return $"{Identification} {partText}";
        }


    }
}
