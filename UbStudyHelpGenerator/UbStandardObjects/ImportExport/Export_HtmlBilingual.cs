using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UbStudyHelpGenerator.UbStandardObjects.ImportExport.Models;
using UbStudyHelpGenerator.UbStandardObjects.Objects;

namespace UbStudyHelpGenerator.UbStandardObjects.ImportExport
{



    public class Export_HtmlBilingual : Export_0base
    {

        public Export_HtmlBilingual()
        {
        }

        protected string IdentLink(ParagraphExport par)
        { 
            return $"<a href=\"#\" onclick=\"event.preventDefault(); generateUrlAndOpen('{par.Ident}');\" class=\"{par.CssClass}\" target=\"_blank\"><small>{par.Reference}</small></a>"; 
        }


        private string FullTextLink(ParagraphExport par)
        {
            return $"<a href=\"#\" onclick=\"event.preventDefault(); generateUrlAndOpen('{par.Ident}');\" class=\"{par.CssClass}\" target=\"_blank\">{par.Text}</a>";
        }

        private string HtmlText(ParagraphExport par)
        {
            ParagraphExportHtmlType parFormat = (ParagraphExportHtmlType)par.Format;
            switch (parFormat)
            {
                case ParagraphExportHtmlType.BookTitle:
                    return $"<h2>{par.Text}</h2>";
                case ParagraphExportHtmlType.PaperTitle:
                    return $"<h3>{FullTextLink(par)}</h3>";
                case ParagraphExportHtmlType.SectionTitle:
                    return $"<h4>{FullTextLink(par)}</h4>";
                case ParagraphExportHtmlType.NormalParagraph:
                    return $"{IdentLink(par)}  {par.Text}";
                case ParagraphExportHtmlType.IdentedParagraph:
                    return $"<bloquote>{IdentLink(par)}  {par.Text}</bloquote>";
                case ParagraphExportHtmlType.Separator:
                    return $"<h3>{par.Reference}  {par.Text}</h3>";
                case ParagraphExportHtmlType.PartIntroduction:
                    return $"<h5>{par.Text}</h5>";
                    
            }

            CountErrors++;
            StaticObjects.FireShowMessage($"**** ERROR {CountErrors}: Format not set for  {par}.");
            return "*** Format not set ***";
        }


        public void PrintHeader(StringBuilder sb, string englishTitle, string ptBrTitle)
        {
            // Page title
            sb.AppendLine("<thead>");
            sb.AppendLine("<tr>");
            sb.AppendLine($"<th><div class=\"{ParagraphExport.CssClassesDivSize} bg-dark text-warning\"><h3>{englishTitle}</h3></div></th>");
            sb.AppendLine($"<th><div class=\"{ParagraphExport.CssClassesDivSize} bg-dark text-warning\"><h3>{ptBrTitle}</h3></div></th>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</thead>");
        }

        public void PrintLine(StringBuilder sb, ParagraphExport english, ParagraphExport ptBr)
        {
            // Page title
            sb.AppendLine("<tr>");

            sb.AppendLine($"<td>");
            sb.AppendLine($"   <div id=\"{english.HtmlId(true)}\" class=\"{english.CssClass}\">{english.Anchor(true)}  {HtmlText(english)}</div>");
            sb.AppendLine($"</td>");

            sb.AppendLine($"<td>");
            sb.AppendLine($"   <div id=\"{ptBr.HtmlId(false)}\" class=\"{ptBr.CssClass}\">{HtmlText(ptBr)}</div>");
            sb.AppendLine($"</td>");

            sb.AppendLine("</tr>");
        }

        private void GernerateHtmlPage(List<ParagraphExport> englishParagraphs, List<ParagraphExport> ptBrParagraphs, string destinationFolder, short paperNo)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("<table class=\"table table-borderless\"> ");
                PrintHeader(sb, $"English {paperNo}", $"PT-BR {DateTime.Now.ToString("dd/MM/yyyy")}");

                sb.AppendLine("<tbody> ");

                for (int i = 0; i < englishParagraphs.Count; i++)
                {
                    PrintLine(sb, englishParagraphs[i], ptBrParagraphs[i]);
                }

                sb.AppendLine("</tbody> ");
                sb.AppendLine("</table> ");

                // Store html file
                var filePath = System.IO.Path.Combine(destinationFolder, $@"content\Doc{paperNo:000}.html");
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                CountErrors++;
                StaticObjects.FireShowMessage($"*** ERROR {CountErrors} creating papaer {paperNo}: {ex.Message}");
            }
        }



        protected override void FillPaper(LiteDatabase db, string destinationFolder, short paperNo)
        {
            StaticObjects.FireShowPaperNumber(paperNo);
            List<ParagraphExport> listEnglish = GetPaper(db, BookEnglish.TranslationLanguage, paperNo);
            List<ParagraphExport> listPtBr = GetPaper(db, BookPtBr.TranslationLanguage, paperNo);
            // Both must have same number os records
            if (listEnglish.Count == 0 || listPtBr.Count == 0 || listEnglish.Count != listPtBr.Count)
            {
                CountErrors++;
                StaticObjects.FireShowMessage($"**** ERROR {CountErrors}: English/PtBr with different number of paragraphs for paper {paperNo}.");
            }
            GernerateHtmlPage(listEnglish, listPtBr, destinationFolder, paperNo);
        }

        /// <summary>
        /// Export the bilingual html English/Portuguese for rogreis.github.io
        /// </summary>
        /// <param name="pathBase">Destination folder</param>
        /// <param name="pathDatabase">LiteDb file location</param>
        public override void Run(string destinationFolder, string pathDatabase)
        {
            CountErrors = 0;
            using (var db = new LiteDatabase(pathDatabase))
            {
                for (short i = 0; i < 197; i++)
                    FillPaper(db, destinationFolder, i);
            }
            StaticObjects.FireShowMessage($"Finished with {CountErrors} errors.");
        }

    }
}
