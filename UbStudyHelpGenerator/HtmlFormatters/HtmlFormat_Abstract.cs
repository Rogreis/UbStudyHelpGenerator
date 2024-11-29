using System;
using System.IO;
using System.Text;
using UbStudyHelpGenerator.UbStandardObjects;
using UbStudyHelpGenerator.UbStandardObjects.Objects;
using UBT_WebSite.Classes;

namespace UbStudyHelpGenerator.HtmlFormatters
{

    public delegate string HtmlTextFunctionDelegate();

    public enum PageType
    {
        Toc,
        Subject,
        Study,
        Query,
        Track
    }


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
                        case ParagraphStatus.Closed:
                            return HtmlFormat_Abstract.CssClassParagraphClosed;
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
                        return $"{anchor}{identification}  {htmlText}";
                    case ParagraphHtmlType.IdentedParagraph:
                        return $"<bloquote>{anchor}{identification}  {htmlText}</bloquote>";
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
        private const string darkTheme = "bg-dark text-white parClosed";
        private const string lightTheme = "bg-light text-black parClosed";

        protected const string CssClassParagraphStarted = "parStarted";
        protected const string CssClassParagraphWorking = "parWorking";
        protected const string CssClassParagraphDoubt = "parDoubt";
        protected const string CssClassParagraphOk = "parOk";
        protected const string CssClassParagraphClosed = "parClosed";
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
            sb.AppendLine(" --font: 'Roboto Slab', serif;");
            sb.AppendLine(" --fontSize: 16px;");
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

        private void PrintNavButton(StringBuilder sb, bool active, string text, bool enabled= true)
        {
            string disabled = enabled ? "" : "disabled";
            sb.AppendLine($"			<button type=\"button\" class=\"btn btn-sm {(active ? "btn-warning acive" : "btn-secondary")} {disabled}\">{text}</button> ");
        }

        public void PrintIndexPage(string pathIndexToc, PageType pageType, string title, bool useDarkTheme= true)
        {
            StringBuilder sb = new StringBuilder();
            string theme = useDarkTheme ? "data-bs-theme=\"dark\"" : "";
            sb.AppendLine("<!DOCTYPE html> ");
            sb.AppendLine($"<html lang=\"en\" {theme}>");
            sb.AppendLine(" ");
            sb.AppendLine("<head> ");
            sb.AppendLine("	<meta http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1252\"> ");
            sb.AppendLine("	<title>Paper 1</title> ");
            sb.AppendLine("	<meta charset=\"utf-8\"> ");
            sb.AppendLine("	<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"> ");
            sb.AppendLine(" ");
            sb.AppendLine("	<link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\"> ");
            sb.AppendLine("	<script src=\"https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js\"></script> ");
            sb.AppendLine(" ");
            sb.AppendLine(" ");
            sb.AppendLine("	<link href=\"https://fonts.googleapis.com/css2?family=Roboto+Slab:wght@400;700&display=swap\" rel=\"stylesheet\"> ");
            sb.AppendLine("	<link href=\"css/tub_pt_br.css\" rel=\"stylesheet\"> ");
            sb.AppendLine("	<script src=\"https://cdn.jsdelivr.net/npm/jquery@3.6.1/dist/jquery.min.js\"></script> ");
            sb.AppendLine("	<script src=\"js/tub_pt_br.js\"></script> ");
            sb.AppendLine(" ");
            sb.AppendLine("	<script type=\"module\"> ");
            sb.AppendLine("		import { Octokit } from \"https://cdn.skypack.dev/@octokit/core\"; ");
            sb.AppendLine("		var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle=\"tooltip\"]')) ");
            sb.AppendLine("		var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) { ");
            sb.AppendLine("			return new bootstrap.Tooltip(tooltipTriggerEl) ");
            sb.AppendLine("		})		 ");
            sb.AppendLine("	</script> ");
            //sb.AppendLine("	<style> ");
            //sb.AppendLine("		.badge.badgeStarted.p-1 { ");
            //sb.AppendLine("			display: flex; ");
            //sb.AppendLine("			align-items: center; ");
            //sb.AppendLine("			min-height: 20px; ");
            //sb.AppendLine("		} ");
            //sb.AppendLine("	</style> ");
            //sb.AppendLine(" ");
            sb.AppendLine("</head> ");
            sb.AppendLine("<html> ");
            sb.AppendLine(" ");
            sb.AppendLine("<body onload=\"StartPage()\"> ");
            sb.AppendLine("	<nav class=\"navbar navbar-expand-sm bg-primary navbar-dark\"> ");
            sb.AppendLine("		<div class=\"container-fluid\"> ");
            
            //sb.AppendLine("			<span class=\"text-white\">O Livro de Urântia - Tradução PT BR - versão 22/11/2024 09:37</span> ");
            sb.AppendLine($"			<span class=\"text-white\">{title}</span> ");

            sb.AppendLine("		</div> ");
            sb.AppendLine("		<div class=\"container-fluid\"> ");

            bool active = pageType == PageType.Toc;
            PrintNavButton(sb, active, "Documentos");

            active = pageType == PageType.Subject;
            PrintNavButton(sb, active, "Assuntos");

            active = pageType == PageType.Study;
            PrintNavButton(sb, active, "Estudos");

            active= false;
            PrintNavButton(sb, active, "Busca", false);
            PrintNavButton(sb, active, "Trilha", false);

            sb.AppendLine("			    <button type=\"button\" class=\"btn btn-sm btn-warning\" data-bs-toggle=\"modal\" ");
            sb.AppendLine("				    data-bs-target=\"#myModal\">Cores</button> ");
            sb.AppendLine("		</div> ");
            sb.AppendLine("	</nav> ");
            sb.AppendLine(" ");
            sb.AppendLine("	<div id=\"leftColumn\" class=\"black splitLeft left mt-0 overflow-auto\"> ");
            sb.AppendLine("		<h3>Table of Contents</h3> ");
            sb.AppendLine("	</div> ");
            sb.AppendLine("	<div id=\"rightColumn\" class=\"black splitRight right mt-0 overflow-auto\"> ");
            sb.AppendLine("	</div> ");
            sb.AppendLine(" ");
            sb.AppendLine("	<!-- The Modal --> ");
            sb.AppendLine("	<div class=\"modal\" id=\"myModal\"> ");
            sb.AppendLine("		<div class=\"modal-dialog\"> ");
            sb.AppendLine("			<div class=\"modal-content\"> ");
            sb.AppendLine(" ");
            sb.AppendLine("				<!-- Modal Header --> ");
            sb.AppendLine("				<div class=\"modal-header\"> ");
            sb.AppendLine("					<h4 class=\"modal-title\">Significado das cores de fundo</h4> ");
            sb.AppendLine("					<button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"modal\"></button> ");
            sb.AppendLine("				</div> ");
            sb.AppendLine(" ");
            sb.AppendLine("				<!-- Modal body --> ");
            sb.AppendLine("				<div class=\"modal-body\"> ");
            sb.AppendLine(" ");
            sb.AppendLine("					<table class=\"table\"> ");
            sb.AppendLine("						<thead> ");
            sb.AppendLine("							<tr> ");
            sb.AppendLine("								<th>Cor de fundo</th> ");
            sb.AppendLine("								<th>Significado</th> ");
            sb.AppendLine("							</tr> ");
            sb.AppendLine("						</thead> ");
            sb.AppendLine("						<tbody> ");
            sb.AppendLine("							<tr> ");
            sb.AppendLine("								<td> ");
            sb.AppendLine("									<div class=\"badge badgeStarted\">Iniciado</div> ");
            sb.AppendLine("								</td> ");
            sb.AppendLine("								<td>Parágrafo ainda na versão 2007</td> ");
            sb.AppendLine("							</tr> ");
            sb.AppendLine("							<tr> ");
            sb.AppendLine("								<td> ");
            sb.AppendLine("									<div class=\"badge badgeWorking\">Em trabalho</div> ");
            sb.AppendLine("								</td> ");
            sb.AppendLine("								<td>Trabalho de tradução em andamento</td> ");
            sb.AppendLine("							</tr> ");
            sb.AppendLine("							<tr> ");
            sb.AppendLine("								<td> ");
            sb.AppendLine("									<div class=\"badge badgeDoubt\">Em dúvida</div> ");
            sb.AppendLine("								</td> ");
            sb.AppendLine("								<td>Há dúvidas sobre a tradução</td> ");
            sb.AppendLine("							</tr> ");
            sb.AppendLine("							<tr> ");
            sb.AppendLine("								<td> ");
            sb.AppendLine("									<div class=\"badge badgeOk\">Ok</div> ");
            sb.AppendLine("								</td> ");
            sb.AppendLine("								<td>Parágrafo finalizado, revisão final necessária</td> ");
            sb.AppendLine("							</tr> ");
            sb.AppendLine("							<tr> ");
            sb.AppendLine("								<td> ");
            sb.AppendLine("									<div class=\"badge badgeClosed\">Fechado</div> ");
            sb.AppendLine("								</td> ");
            sb.AppendLine("								<td>Parágrafo fechado, talvez não ainda perfeito</td> ");
            sb.AppendLine("							</tr> ");
            sb.AppendLine("						</tbody> ");
            sb.AppendLine("					</table> ");
            sb.AppendLine(" ");
            sb.AppendLine(" ");
            sb.AppendLine("				</div> ");
            sb.AppendLine(" ");
            sb.AppendLine("				<!-- Modal footer --> ");
            sb.AppendLine("				<div class=\"modal-footer\"> ");
            sb.AppendLine("					<button type=\"button\" class=\"btn btn-danger\" data-bs-dismiss=\"modal\">Close</button> ");
            sb.AppendLine("				</div> ");
            sb.AppendLine(" ");
            sb.AppendLine("			</div> ");
            sb.AppendLine("		</div> ");
            sb.AppendLine("	</div> ");
            sb.AppendLine(" ");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");
            File.WriteAllText(pathIndexToc, sb.ToString(), Encoding.UTF8);
        }

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
