using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UbStudyHelpGenerator.UbStandardObjects;
using UbStudyHelpGenerator.UbStandardObjects.Objects;

namespace UbStudyHelpGenerator.HtmlFormatters
{


    /// <summary>
    /// Generates a whole book using bootstrap
    /// </summary>
    public class HtmlFormat_PTalternative : HtmlFormat_Abstract
    {

        public HtmlFormat_PTalternative(Parameters parameters) : base(parameters)
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


        #endregion

        /// <summary>
        /// Generate a paper page for github pt alternative web site
        /// </summary>
        /// <param name="destinationFolder"></param>
        /// <param name="paperNo"></param>
        /// <param name="englishPaper"></param>
        /// <param name="ptAlternativePaper"></param>
        /// <param name="toc_table"></param>
        public void GenerateGitHubPage(Paper englishPaper, List<Paragraph> paragraphs2ndColumn, string destinationFolder,
                                       short paperNo, List<Paragraph> paragraphs3rdColumn = null, bool merge2nd3rdColuimns = false)
        {
            StringBuilder sb = new StringBuilder();

            // Verify exitence of GPT translation file 
            LineFormatParameters formatParameters = new LineFormatParameters();
            string gptFilePath = System.IO.Path.Combine(destinationFolder, $"PaperTranslations\\PtAlternative_{paperNo:000}.txt");
            //formatParameters.IsToCompare = File.Exists(gptFilePath);
            formatParameters.IsToCompare = false;  // Comparação cancelada
            formatParameters.EditTranslationNumber = paragraphs3rdColumn == null ? (short)2 : (short)3;
            string[] gptTranslationLines = null;
            if (formatParameters.IsToCompare)
            {
                gptTranslationLines = File.ReadAllLines(gptFilePath);
            }

            StaticObjects.FireSendMessage($"{englishPaper} {(formatParameters.IsToCompare ? "Has GPT translation" : "")}");

            sb.AppendLine("<table class=\"table table-borderless\"> ");

            formatParameters.SetHeader($"English {paperNo}", "GPT 4 Translation", "Portuguese PT BR", "Compare");
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
                        string ptAlternativeText = "";
                        try
                        {
                            ptAlternativeText = paragraphs2ndColumn[rightCounter].Text;
                        }
                        catch (Exception)
                        {
                            ptAlternativeText = "Missing text";
                        }
                        string gptText = null;
                        string mergedLine = null;
                        if (formatParameters.IsToCompare)
                        {
                            gptText = GetGptTranslationLine(gptTranslationLines, i);
                            mergedLine = "";
                            if (!string.IsNullOrEmpty(gptText))
                            {
                                mergedLine = HtmlCompare(paragraphs2ndColumn[i].TextNoHtml, gptText);
                            }

                        }
                        formatParameters.SetData(englishPaper.Paragraphs[i], paragraphs2ndColumn[rightCounter], gptText, mergedLine);
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


        public void GenerateCompare(Paper englishPaper, List<Paragraph> paragraphs2ndColumn, List<Paragraph> paragraphs3rdColumn, string destinationFolder, short paperNo)
        {
            StringBuilder sb = new StringBuilder();

            // Verify exitence of GPT translation file 
            LineFormatParameters formatParameters = new LineFormatParameters();

            StaticObjects.FireSendMessage($"{englishPaper} {(formatParameters.IsToCompare ? "Has GPT translation" : "")}");

            sb.AppendLine("<table class=\"table table-borderless\"> ");

            formatParameters.EditTranslationNumber = (short)3;
            formatParameters.IsToCompare = true;
            formatParameters.SetHeader($"English {paperNo}", "Portuguese 2007", "Portuguese PT BR", "Compare");
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
                        string pt2007Text = paragraphs2ndColumn[i].Text;
                        string ptAlternativeText = paragraphs3rdColumn[i].Text;
                        string mergedLine = HtmlCompare(paragraphs2ndColumn[i].TextNoHtml, paragraphs3rdColumn[i].TextNoHtml);
                        formatParameters.SetData(englishPaper.Paragraphs[i], paragraphs2ndColumn[i], ptAlternativeText, mergedLine);
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

            var filePath = System.IO.Path.Combine(destinationFolder, $@"Compare{paperNo:000}.html");
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
