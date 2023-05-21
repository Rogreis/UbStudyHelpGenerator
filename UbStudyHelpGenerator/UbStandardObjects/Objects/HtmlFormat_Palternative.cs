using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using static Lucene.Net.Index.SegmentReader;
using static System.Net.Mime.MediaTypeNames;

namespace UbStudyHelpGenerator.UbStandardObjects.Objects
{

    public delegate string HtmlTextFunctionDelegate();

    /// <summary>
    /// Generates a whole book using bootstrap
    /// </summary>
    public class HtmlFormat_Palternative : HtmlFormatAbstract
    {
        public event dlShowMessage ShowMessage = null;

        public HtmlFormat_Palternative(Parameters parameters) : base(parameters)
        {

        }

        #region Private routines


        /// <summary>
        /// Get the line number count from the gpt tranalstion list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private string GetGptTranslationLine(string[] list, int count)
        {
            try
            {
                return list[count];
            }
            catch
            {
                return "";
            }
        }


        private class LineFormatParameters
        {
            private const string DividerString = "* * * * *";
            private const string HtmlSpace = "&nbsp;";
            private const string CssClassesDivSize = "p-3 mb-2";

            private short PaperNo = -1;
            private short SectionNo = -1;
            private short ParagraphNo = -1;
            private short Page = -1;
            private short Line = -1;

            private Paragraph EnglishParagraph = null;
            private Paragraph PtAlternativeParagraph = null;
            private bool IsDivider = false;
            private bool IsHeader = false;

            private ParagraphHtmlType Format = ParagraphHtmlType.NormalParagraph;

            private string TextLeft = HtmlSpace;
            private string TextMidle = HtmlSpace;
            private string TextRight = HtmlSpace;
            private string TextCompare = HtmlSpace;

            private string Anchor { get => $"<a name=\"p{PaperNo:000}_{SectionNo:000}_{ParagraphNo:000}\"/>  "; }

            private string Identification { get => $"{PaperNo}:{SectionNo}-{ParagraphNo} ({Page}.{Line})"; }

            public bool HasHelpTranslation = false;

            // Links used onlu for the edit translation
            private string Href { get => $"https://github.com/Rogreis/PtAlternative/blob/correcoes/Doc{PaperNo:000}/Par_{PaperNo:000}_{SectionNo:000}_{ParagraphNo:000}.md"; }
            private string IdentLink { get => $"<a href=\"{Href}\" class=\"{ParagraphClass(PtAlternativeParagraph)}\" target=\"_blank\"><small>{EnglishParagraph.Identification}</small></a>"; }
            private string FulltextLink(string htmlText) 
            { 
                return $"<a href=\"{Href}\" class=\"{ParagraphClass(PtAlternativeParagraph)}\" target=\"_blank\">{htmlText}</a>"; 
            }

            /// <summary>
            /// Returns the classes for a paragraph depending on IsEditTranslation and IsDarkTheme
            /// </summary>
            /// <param name="ParagraphStatus"></param>
            /// <returns></returns>
            private string ParagraphClass(Paragraph p)
            {
                if (!IsHeader && p.IsEditTranslation)
                {
                    if(p.Status != ParagraphStatus.Closed)
                    {
                        int ggg = 1;
                    }
                    switch (p.Status)
                    {
                        case ParagraphStatus.Started:
                            return HtmlFormatAbstract.CssClassParagraphStarted;
                        case ParagraphStatus.Working:
                            return HtmlFormatAbstract.CssClassParagraphWorking;
                        case ParagraphStatus.Doubt:
                            return HtmlFormatAbstract.CssClassParagraphDoubt;
                        case ParagraphStatus.Ok:
                            return HtmlFormatAbstract.CssClassParagraphOk;
                    }
                }
                return HtmlFormatAbstract.CssNormalText;
            }

            private string GetHtmlText(string htmlText, string identification, string anchor, string fullTextLink)
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
                        return $"<h1>{anchor}{htmlText}</h1>";
                    case ParagraphHtmlType.PaperTitle:
                        return $"<h2>{anchor}{fullTextLink}</h2>";
                    case ParagraphHtmlType.SectionTitle:
                        return $"<h3>{anchor}{fullTextLink}</h3>";
                    case ParagraphHtmlType.NormalParagraph:
                        return $"{identification}  {htmlText}";
                    case ParagraphHtmlType.IdentedParagraph:
                        return $"<bloquote>{identification}  {htmlText}</bloquote>";
                }
                return "";
            }

            private string PrintParagraphLeft()
            {
                return GetHtmlText(TextLeft, Identification, Anchor, TextLeft);
            }

            private string PrintParagraphMiddle()
            {
                if (IsDivider || IsHeader)
                {
                    return TextLeft;
                }
                // When not divider or header, middle is always the edit column
                string fullTextLink = EnglishParagraph.IsPaperTitle || EnglishParagraph.IsSectionTitle ? FulltextLink(TextMidle) : TextMidle;
                return GetHtmlText(TextMidle, IdentLink, "", fullTextLink);
            }

            private string PrintParagraphRight()
            {
                if (IsDivider || IsHeader)
                {
                    return TextLeft;
                }

                // When help translation is available right is not the edit one
                if (HasHelpTranslation)
                {
                    // When there is Help translation, right is the help translation, then no identification
                    return GetHtmlText(TextRight, "", "", TextRight);
                }
                string fullTextLink = EnglishParagraph.IsPaperTitle || EnglishParagraph.IsSectionTitle ? FulltextLink(TextRight) : TextRight;
                return GetHtmlText(TextRight, IdentLink, "", fullTextLink);
            }

            private string PrintParagraphCompare()
            {
                if (IsDivider || IsHeader)
                {
                    return TextLeft;
                }
                return GetHtmlText(TextCompare, "", "", TextCompare);
            }

            private void PrintColumn(StringBuilder sb, HtmlTextFunctionDelegate htmlTextFunction, string cssClass= null)
            {
                if (string.IsNullOrEmpty(cssClass)) cssClass = HtmlFormatAbstract.CssNormalText;
                sb.AppendLine($"<td>");
                sb.AppendLine($"   <div class=\"{CssClassesDivSize} {cssClass}\">");
                sb.AppendLine($"       {htmlTextFunction()}");
                sb.AppendLine($"   <div>");
                sb.AppendLine($"</td>");
            }

            private void PrintColumnHeader(StringBuilder sb, string text)
            {
                sb.AppendLine($"<th><div class=\"{CssClassesDivSize} bg-dark text-warning\"><h2>{text}</h2></div></th>");
            }


            public void SetHeader(short paperNo)
            {
                IsDivider = false;
                IsHeader = true;

                TextLeft = $"Paper {paperNo}";
                if (HasHelpTranslation)
                {
                    TextMidle = $"Documento {paperNo}";
                    TextRight = "Tradução Auxiliar";
                    TextCompare = "Comparador";
                }
                else
                {
                    TextRight = $"Documento {paperNo}";
                }
            }

            public void SetData(Paragraph englishParagraph, Paragraph ptAlternative, string htmlHelpTranslation, string htmlCompare)
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
                if (HasHelpTranslation)
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
                if (HasHelpTranslation)
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
                if (HasHelpTranslation)
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
                if (HasHelpTranslation)
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
        }

        #endregion

        /// <summary>
        /// Generate a paper page for github pt alternative web site
        /// </summary>
        /// <param name="destinationFolder"></param>
        /// <param name="paperNo"></param>
        /// <param name="englishPaper"></param>
        /// <param name="ptAlternativePaper"></param>
        /// <param name="toc_table"></param>
        public void GenerateGitHubPage(string destinationFolder, short paperNo, Paper englishPaper, Paper ptAlternativePaper, TUB_TOC_Html toc_table)
        {
            StringBuilder sb = new StringBuilder();

            // Verify exitence of GPT translation file 
            LineFormatParameters formatParameters = new LineFormatParameters();
            string gptFilePath = System.IO.Path.Combine(destinationFolder, $"PaperTranslations\\PtAlternative_{paperNo:000}.txt");
            formatParameters.HasHelpTranslation = File.Exists(gptFilePath);
            string[] gptTranslationLines = null;
            if (formatParameters.HasHelpTranslation)
            {
                gptTranslationLines = File.ReadAllLines(gptFilePath);
            }

            StaticObjects.FireSendMessage($"{englishPaper} {(formatParameters.HasHelpTranslation ? "Has GPT translation" : "")}");

            sb.AppendLine("<table class=\"table table-borderless\"> ");

            formatParameters.SetHeader(paperNo);
            formatParameters.PrintHeader(sb);

            // Text
            sb.AppendLine("<tbody> ");

            int rightCounter = 0;
            for (int i = 0; i < englishPaper.Paragraphs.Count; i++)
            {
                try
                {
                    if (englishPaper.Paragraphs[i].IsDivider)
                    {
                        formatParameters.SetDividerline();
                        formatParameters.PrintLine(sb);
                    }
                    else
                    {
                        string englishText = englishPaper.Paragraphs[i].Text;
                        string ptAlternativeText = ptAlternativePaper.Paragraphs[i].Text;
                        string gptText = null;
                        string mergedLine = null;
                        if (formatParameters.HasHelpTranslation)
                        {
                            gptText = GetGptTranslationLine(gptTranslationLines, i);
                            mergedLine = "";
                            if (gptText != null)
                            {
                                mergedLine = HtmlCompare(ptAlternativePaper.Paragraphs[i].TextNoHtml, gptText);
                            }

                        }
                        formatParameters.SetData(englishPaper.Paragraphs[i], ptAlternativePaper.Paragraphs[i], gptText, mergedLine);
                        formatParameters.PrintLine(sb);
                        rightCounter++;
                    }
                }
                catch (Exception EX)
                {
                    formatParameters.PrintError(sb, $"Error generating this paragraph: {EX.Message}");
                }
            }

            sb.AppendLine("	    </tbody> ");
            sb.AppendLine("	  </table> ");

            var filePath = System.IO.Path.Combine(destinationFolder, $@"content\Doc{paperNo:000}.html");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }


        ///// <summary>
        ///// Main page structure
        ///// </summary>
        ///// <param name="destinationFolder"></param>
        ///// <param name="paperNo"></param>
        ///// <param name="leftPaper"></param>
        ///// <param name="rightPaper"></param>
        ///// <param name="toc_table"></param>
        //private void PrintPaper(string destinationFolder, short paperNo, Paper leftPaper, Paper rightPaper, TUB_TOC_Html toc_table)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    PageStart(sb, toc_table, paperNo);

        //    PrintJumbotron(sb, $"O Livro de Urântia - Documento {paperNo}", $"PT-BR version: {DateTime.Now.ToString("dd-MM-yyyy")}", paperNo);

        //    string textColor = Param.IsDarkTheme ? Param.DarkText : Param.LightText;
        //    string backColor = Param.IsDarkTheme ? Param.DarkText : Param.LightText;

        //    sb.AppendLine("<div class=\"container-fluid mt-5 \"> ");
        //    sb.AppendLine("  <div class=\"row\"> ");
        //    sb.AppendLine(" ");

        //    // Index
        //    sb.AppendLine($"    <div class=\"col-sm-3 {textColor}\"> ");
        //    sb.AppendLine("      <h3>Index</h3> ");
        //    toc_table.Html(sb);
        //    sb.AppendLine("    </div> ");
        //    sb.AppendLine(" ");

        //    sb.AppendLine($"<div class=\"col-sm-9 {textColor}\"> ");
        //    sb.AppendLine("	  <table class=\"table table-borderless\"> ");

        //    // Page title
        //    sb.AppendLine("	    <thead> ");
        //    sb.AppendLine("	      <tr> ");
        //    sb.AppendLine($"	        <th><h2 {TextClass()}>Paper {paperNo}</h2></th> ");
        //    sb.AppendLine($"	        <th><h2 {TextClass()}>Documento {paperNo}</h2></th> ");
        //    sb.AppendLine("	      </tr> ");
        //    sb.AppendLine("	    </thead> ");

        //    // Text
        //    sb.AppendLine("	    <tbody> ");

        //    for (int i = 0; i < leftPaper.Paragraphs.Count; i++)
        //    {
        //        try
        //        {
        //            PrintLine(sb, leftPaper.Paragraphs[i], rightPaper.Paragraphs[i]);
        //        }
        //        catch (Exception EX)
        //        {
        //            string SSS = EX.Message;
        //        }
        //    }

        //    sb.AppendLine("	    </tbody> ");

        //    sb.AppendLine("	  </table> ");
        //    sb.AppendLine("  </div> ");
        //    sb.AppendLine("</div> ");
        //    sb.AppendLine("</div> ");

        //    //PrintPager(sb, paperNo);
        //    PageEnd(sb, toc_table);

        //    var filePath = Path.Combine(destinationFolder, $"Doc{paperNo:000}.html");
        //    if (File.Exists(filePath))
        //    {
        //        File.Delete(filePath);
        //    }

        //    File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        //}


        //public void MainPage(StringBuilder sb)
        //{
        //    // main page is no more generated 
        //}



        //public virtual void GeneratePaper(string destinationFolder, Translation leftTranslation, Paper rightPaper, TUB_TOC_Html toc_table, short paperNo)
        //{
        //    try
        //    {
        //        Paper leftPaper = null;
        //        TranslationIdRight = TranslationIdLeft = Translation.NoTranslation;
        //        TranslationTextDirection[0] = TranslationTextDirection[2] = false;

        //        if (leftTranslation != null)
        //        {
        //            TranslationIdLeft = leftTranslation.LanguageID;
        //            TranslationTextDirection[2] = leftTranslation.RightToLeft;
        //        }

        //        leftPaper = leftTranslation.Paper(paperNo);

        //        FireSendMessage(leftPaper.ToString());
        //        PrintPaperForGitHubWebSite(destinationFolder, paperNo, leftPaper, rightPaper, toc_table);

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}



        //public void GeneratBook(string destinationFolder, Translation leftTranslation, Translation rightTranslation)
        //{
        //    try
        //    {
        //        Paper rightPaper = null, leftPaper = null;
        //        TranslationIdRight = TranslationIdLeft = Translation.NoTranslation;
        //        TranslationTextDirection[0] = TranslationTextDirection[2] = false;

        //        if (rightTranslation != null)
        //        {
        //            TranslationIdRight = rightTranslation.LanguageID;
        //            TranslationTextDirection[0] = rightTranslation.RightToLeft;
        //        }

        //        if (leftTranslation != null)
        //        {
        //            TranslationIdLeft = leftTranslation.LanguageID;
        //            TranslationTextDirection[2] = leftTranslation.RightToLeft;
        //        }

        //        for (short paperNo = 0; paperNo < 197; paperNo++)
        //        {
        //            leftPaper = GetPaper(paperNo, leftTranslation);
        //            rightPaper = GetPaper(paperNo, rightTranslation);
        //            ShowMessage?.Invoke(leftPaper.ToString());
        //            PrintPaper(destinationFolder, paperNo, leftPaper, rightPaper);
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}



    }
}
