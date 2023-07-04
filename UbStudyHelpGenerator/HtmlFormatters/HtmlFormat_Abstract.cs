using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using UbStudyHelpGenerator.UbStandardObjects;
using UbStudyHelpGenerator.UbStandardObjects.Objects;
using UBT_WebSite.Classes;

using static System.Net.Mime.MediaTypeNames;

namespace UbStudyHelpGenerator.HtmlFormatters
{

    public delegate string HtmlTextFunctionDelegate();

    /// <summary>
    /// Format html to be shown
    /// </summary>
    public abstract class HtmlFormat_Abstract
    {
        protected const bool RightToLeft = false;

        // Data used to help columns page
        protected double percent = 0;

        protected Parameters Param = null;

        protected short TranslationIdLeft = Translation.NoTranslation;

        protected short TranslationIdMiddle = Translation.NoTranslation;

        protected short TranslationIdRight = Translation.NoTranslation;

        // By default all 3 translations are not right to left written
        protected bool[] TranslationTextDirection = { RightToLeft, RightToLeft, RightToLeft };

        // Padding information
        protected int ParagraphPadding = 10;
        protected int CellPadding = 15;

        public event dlShowMessage ShowMessage = null;


        public HtmlFormat_Abstract(Parameters parameters)
        {
            Param = parameters;
        }


        protected class LineFormatParameters
        {
            protected const string DividerString = "* * * * *";
            protected const string HtmlSpace = "&nbsp;";
            protected const string CssClassesDivSize = "p-3 mb-2 ";

            protected short PaperNo = -1;
            protected short SectionNo = -1;
            protected short ParagraphNo = -1;
            protected short Page = -1;
            protected short Line = -1;

            protected Paragraph EnglishParagraph = null;
            protected Paragraph PtAlternativeParagraph = null;
            protected bool IsDivider = false;
            protected bool IsHeader = false;

            protected ParagraphHtmlType Format = ParagraphHtmlType.NormalParagraph;

            protected string TextLeft = HtmlSpace;
            protected string TextMidle = HtmlSpace;
            protected string TextRight = HtmlSpace;
            protected string TextCompare = HtmlSpace;

            protected string Anchor { get => $"<a name=\"p{PaperNo:000}_{SectionNo:000}_{ParagraphNo:000}\"/>  "; }

            protected string Identification { get => $"{PaperNo}:{SectionNo}-{ParagraphNo} ({Page}.{Line})"; }

            public bool IsToCompare = false;
            public short EditTranslationNumber = -1;

            // Links used onlu for the edit translation
            protected string Href { get => $"https://github.com/Rogreis/PtAlternative/blob/correcoes/Doc{PaperNo:000}/Par_{PaperNo:000}_{SectionNo:000}_{ParagraphNo:000}.md"; }
            protected string IdentLink { get => $"<a href=\"{Href}\" class=\"{ParagraphClass(PtAlternativeParagraph)}\" target=\"_blank\"><small>{EnglishParagraph.Identification}</small></a>"; }
            protected string FulltextLink(string htmlText)
            {
                return $"<a href=\"{Href}\" class=\"{ParagraphClass(PtAlternativeParagraph)}\" target=\"_blank\">{htmlText}</a>";
            }

            /// <summary>
            /// Returns the classes for a paragraph depending on IsEditTranslation and IsDarkTheme
            /// </summary>
            /// <param name="ParagraphStatus"></param>
            /// <returns></returns>
            protected string ParagraphClass(Paragraph p)
            {
                if (!IsHeader && p.IsEditTranslation)
                {
                    if (p.Status != ParagraphStatus.Closed)
                    {
                        int ggg = 1;
                    }
                    switch (p.Status)
                    {
                        case ParagraphStatus.Started:
                            return HtmlFormat_Abstract.CssClassParagraphStarted;
                        case ParagraphStatus.Working:
                            return HtmlFormat_Abstract.CssClassParagraphWorking;
                        case ParagraphStatus.Doubt:
                            return HtmlFormat_Abstract.CssClassParagraphDoubt;
                        case ParagraphStatus.Ok:
                            return HtmlFormat_Abstract.CssClassParagraphOk;
                    }
                }
                return HtmlFormat_Abstract.CssNormalText;
            }

            protected string GetHtmlText(string htmlText, string identification, string anchor, string fullTextLink)
            {
                if (IsDivider)
                {
                    return DividerString;
                }
                if (IsHeader)
                {
                    return $"<h3>{htmlText}</h3>"; ;
                }
                switch (Format)
                {
                    case ParagraphHtmlType.BookTitle:
                        return $"<h2>{anchor}{htmlText}</h2>";
                    case ParagraphHtmlType.PaperTitle:
                        return $"<h3>{anchor}{fullTextLink}</h3>";
                    case ParagraphHtmlType.SectionTitle:
                        return $"<h4>{anchor}{fullTextLink}</h4>";
                    case ParagraphHtmlType.NormalParagraph:
                        return $"{identification}  {htmlText}";
                    case ParagraphHtmlType.IdentedParagraph:
                        return $"<bloquote>{identification}  {htmlText}</bloquote>";
                }
                return "";
            }

            protected string PrintParagraphLeft()
            {
                return GetHtmlText(TextLeft, Identification, Anchor, TextLeft);
            }

            protected string PrintParagraphMiddle()
            {
                if (IsDivider || IsHeader)
                {
                    return TextLeft;
                }
                // When not divider or header, middle is always the edit column
                string fullTextLink = EnglishParagraph.IsPaperTitle || EnglishParagraph.IsSectionTitle ? FulltextLink(TextMidle) : TextMidle;
                return GetHtmlText(TextMidle, IdentLink, "", fullTextLink);
            }

            protected string PrintParagraphRight()
            {
                if (IsDivider || IsHeader)
                {
                    return TextLeft;
                }

                // When help translation is available right is not the edit one
                if (IsToCompare)
                {
                    // When there is Help translation, right is the help translation, then no identification
                    return GetHtmlText(TextRight, "", "", TextRight);
                }
                string fullTextLink = EnglishParagraph.IsPaperTitle || EnglishParagraph.IsSectionTitle ? FulltextLink(TextRight) : TextRight;
                return GetHtmlText(TextRight, IdentLink, "", fullTextLink);
            }

            protected string PrintParagraphCompare()
            {
                if (IsDivider || IsHeader)
                {
                    return TextLeft;
                }
                return GetHtmlText(TextCompare, "", "", TextCompare);
            }

            protected void PrintColumn(StringBuilder sb, HtmlTextFunctionDelegate htmlTextFunction, string cssClass = null)
            {
                if (string.IsNullOrEmpty(cssClass)) cssClass = HtmlFormat_Abstract.CssNormalText;
                sb.AppendLine($"<td>");
                sb.AppendLine($"   <div class=\"{CssClassesDivSize} {cssClass}\">");
                sb.AppendLine($"       {htmlTextFunction()}");
                sb.AppendLine($"   <div>");
                sb.AppendLine($"</td>");
            }

            protected void PrintColumnHeader(StringBuilder sb, string text)
            {
                sb.AppendLine($"<th><div class=\"{CssClassesDivSize} bg-dark text-warning\"><h3>{text}</h3></div></th>");
            }


            public void SetDividerline()
            {
                IsDivider = true;
                IsHeader = false;
                TextLeft = TextMidle = TextRight = TextCompare = DividerString;
            }

            public void PrintError(StringBuilder sb, string errorMessage)
            {
                IsDivider = false;
                IsHeader = false;

                Format = EnglishParagraph.Format;
                TextLeft = HtmlSpace;
                if (IsToCompare)
                {
                    TextMidle = HtmlSpace;
                    TextRight = errorMessage;
                    TextCompare = HtmlSpace;
                }
                else
                {
                    TextRight = HtmlSpace;
                }
                PrintLine(sb);
            }

            public void PrintHeader(StringBuilder sb)
            {
                // Page title
                sb.AppendLine("<thead>");
                sb.AppendLine("<tr>");
                PrintColumnHeader(sb, TextLeft);
                if (IsToCompare)
                {
                    PrintColumnHeader(sb, TextMidle);
                    PrintColumnHeader(sb, TextRight);
                    PrintColumnHeader(sb, TextCompare);
                }
                else
                {
                    PrintColumnHeader(sb, TextRight);
                }
                sb.AppendLine("</tr>");

                sb.AppendLine("</thead>");
            }

            public void PrintLine(StringBuilder sb)
            {
                sb.AppendLine("<tr>");
                PrintColumn(sb, PrintParagraphLeft);
                if (IsToCompare)
                {
                    PrintColumn(sb, PrintParagraphMiddle, ParagraphClass(PtAlternativeParagraph));
                    PrintColumn(sb, PrintParagraphRight);
                    PrintColumn(sb, PrintParagraphCompare);
                }
                else
                {
                    PrintColumn(sb, PrintParagraphRight, ParagraphClass(PtAlternativeParagraph));
                }
                sb.AppendLine("</tr>");
            }

            public virtual void SetHeader(string  title1, string title2, string title3, string title4)
            {
                IsDivider = false;
                IsHeader = true;
                TextLeft = title1;
                TextMidle = title2;
                TextRight = title3;
                TextCompare = title4;
            }

            public virtual void SetData(Paragraph englishParagraph, Paragraph ptAlternative, string htmlHelpTranslation, string htmlCompare)
            {
                EnglishParagraph = englishParagraph;
                PtAlternativeParagraph = ptAlternative;

                PaperNo = EnglishParagraph.Paper;
                SectionNo = EnglishParagraph.Section;
                ParagraphNo = EnglishParagraph.ParagraphNo;
                Page = EnglishParagraph.Page;
                Line = EnglishParagraph.Line;

                IsDivider = false;
                IsHeader = false;

                Format = EnglishParagraph.Format;
                TextLeft = EnglishParagraph.Text;
                if (IsToCompare)
                {
                    TextMidle = PtAlternativeParagraph.Text;
                    TextRight = htmlHelpTranslation;
                    TextCompare = htmlCompare;
                }
                else
                {
                    TextRight = PtAlternativeParagraph.Text;
                }
            }

        }


        #region Styles

        // Bootstrap definitions
        private const string darkTheme = "bg-dark text-white";
        private const string lightTheme = "bg-light text-black";

        protected const string CssClassParagraphStarted = "parStarted";
        protected const string CssClassParagraphWorking = "parWorking";
        protected const string CssClassParagraphDoubt = "parDoubt";
        protected const string CssClassParagraphOk = "parOk";
        public static string CssNormalText
        {
            get
            {
                return StaticObjects.Parameters.IsDarkTheme ? darkTheme : lightTheme;
            }
        }


        private void CssVariables(StringBuilder sb)
        {
            sb.AppendLine(":root { ");
            sb.AppendLine(" font-family: var(--font);");
            sb.AppendLine(" font-size: var(--fontSize);");
            sb.AppendLine("} ");
        }

        private void ItalicBoldStyles(StringBuilder sb)
        {
            sb.AppendLine("i, b, em  {  ");
            sb.AppendLine(" font-family: var(--font);");
            sb.AppendLine(" font-size: var(--fontSize);");
            sb.AppendLine(" color: #FF33CC;  ");
            sb.AppendLine(" font-weight: bold;  ");
            sb.AppendLine("}  ");
        }

        private void ParagraphCssClass(StringBuilder sb, string cssName, string textColor, string backColor)
        {
            sb.AppendLine($".{cssName} {{ color:{textColor}; background-color:{backColor}; }}");
            sb.AppendLine($"a.{cssName} {{ color:{textColor}; text-decoration: none; }}");
            sb.AppendLine($"a.{cssName}:hover {{ text-decoration: underline; }}");
            sb.AppendLine(" ");
        }

        /// <summary>
        /// Create the styles for the page
        /// </summary>
        /// <param name="sb"></param>
        public virtual void Styles(StringBuilder sb)
        {
            sb.AppendLine("<style type=\"text/css\">  ");

            CssVariables(sb);

            string fontFamily = $"'{Param.FontFamily}'";
            string fontSize = $"{Param.FontSize}px";

            // Define all text font but colors
            sb.AppendLine($".{CssClassParagraphStarted}, .{CssClassParagraphWorking}, .{CssClassParagraphDoubt}, .{CssClassParagraphOk} ");
            sb.AppendLine("{  ");
            sb.AppendLine(" font-family: var(--font);");
            sb.AppendLine(" font-size: var(--fontSize);");
            sb.AppendLine(" text-align: justify;   ");
            //sb.AppendLine(" text-justify: inter-word; ");
            //sb.AppendLine($" padding: {ParagraphPadding}px;   ");
            sb.AppendLine("}  ");
            sb.AppendLine(" ");

            // Css classes for text color
            ParagraphCssClass(sb, CssClassParagraphStarted, "black", "FloralWhite");
            ParagraphCssClass(sb, CssClassParagraphWorking, "black", "LemonChiffon");
            ParagraphCssClass(sb, CssClassParagraphDoubt, "white", "FireBrick");
            ParagraphCssClass(sb, CssClassParagraphOk, "black", "Aquamarine");

            sb.AppendLine("sup  {font-size: 9px;  color: #666666;}  ");

            ItalicBoldStyles(sb);

            sb.AppendLine("table {  ");
            sb.AppendLine("    border: 0px solid;  ");
            sb.AppendLine("    border-collapse: collapse;  ");
            sb.AppendLine("}  ");
            sb.AppendLine($"td {{ padding: {CellPadding}px; border: none;}}");

            // Classes for compare text
            sb.AppendLine(".TextRemoved { color:#ff3333 } ");
            sb.AppendLine(".TextInserted { color:#66ff33 } ");


            sb.AppendLine("</style>  ");

            //ItalicBoldStyles(sb);
            //sb.AppendLine("  ");

            //// Links
            //LinkStyles(sb);


        }

        #endregion


        #region Html Start and End
        protected virtual void pageStart(StringBuilder sb, int paperNo, bool compareStyles = false)
        {
            sb.AppendLine("<!DOCTYPE html>  ");
            sb.AppendLine("<html>  ");
            sb.AppendLine("  ");
            sb.AppendLine("<head>   ");
            sb.AppendLine("    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1252\">  ");
            sb.AppendLine($"    <title>Paper {paperNo}</title>  ");
            sb.AppendLine("    <meta charset=\"utf-8\">   ");
            sb.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">   ");
            sb.AppendLine("    <link href=\"css/bootstrap.min.css\" rel=\"stylesheet\">    ");

            Styles(sb);

            sb.AppendLine(" ");
            sb.AppendLine("</head>   ");
            sb.AppendLine("  ");
            sb.AppendLine("<body class=\"textNormal\" \">   ");
            sb.AppendLine("<div class=\"container-fluid mt-5 textNormal\">    ");

        }

        protected void pageEnd(StringBuilder sb)
        {
            sb.AppendLine("</BODY>");
            sb.AppendLine("</HTML>");
        }

        #endregion


        protected string HtmlCompare(string textOld, string textNew)
        {
            try
            {
                Merger merger = null;
                merger = new Merger(textOld, textNew);
                return merger.merge();
            }
            catch (Exception ex)
            {
                return $"Error: merge engine failure: {ex.Message}";
            }
        }

    }
}
