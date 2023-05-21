using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using UbStudyHelpGenerator.UbStandardObjects;
using UBT_WebSite.Classes;

using static System.Net.Mime.MediaTypeNames;

namespace UbStudyHelpGenerator.UbStandardObjects.Objects
{
    /// <summary>
    /// Format html to be shown
    /// </summary>
    public abstract class HtmlFormatAbstract
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


        public HtmlFormatAbstract(Parameters parameters)
        {
            Param = parameters;
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
            sb.AppendLine($"a.{cssName} {{ text-decoration: none; }}");
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
