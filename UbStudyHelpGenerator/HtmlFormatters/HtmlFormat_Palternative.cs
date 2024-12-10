using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private void GenerateSubjectIndexHeader(StringBuilder sb)
        {
            sb.AppendLine("			<div id=\"searchData\"> ");
            sb.AppendLine("				<form> ");
            sb.AppendLine("					<label for=\"searchInputBox\">Busca nos assuntos:</label><br> ");
            sb.AppendLine("					<input  ");
            sb.AppendLine("						type=\"text\"  ");
            sb.AppendLine("						id=\"searchInputBox\"  ");
            sb.AppendLine("						name=\"inputBox\"  ");
            sb.AppendLine("						style=\"width: 200px;\" ");
            sb.AppendLine("						placeholder=\"Mínimo 3 letras e enter\" title=\"Digite um mínimo de 3 letras do assunto a procurar e tecle enter.\" ");
            sb.AppendLine("						> ");
            sb.AppendLine("						<br><label for=\"listBoxAssuntos\">Assuntos encontrados:</label><br> ");
            sb.AppendLine("						<select id=\"listBoxAssuntos\" size=\"6\" style=\"width: 200px;\" title=\"Faça um duplo-clique num item para expandir os assuntos.\"> ");
            sb.AppendLine("						</select> ");
            sb.AppendLine("				</form> ");
            sb.AppendLine("			</div> ");
            sb.AppendLine("			<div id=\"detailsList\"> ");
            sb.AppendLine("			</div> ");
            sb.AppendLine(" ");
        }

        private void GenerateSubjectIndexScript(StringBuilder sb)
        {
            sb.AppendLine(" ");
            sb.AppendLine("		<script> ");
            sb.AppendLine("			// Ensure this script runs after the DOM is fully loaded ");
            sb.AppendLine("			document.addEventListener(\"DOMContentLoaded\", () => { ");
            sb.AppendLine("				const inputBox = document.getElementById(\"searchInputBox\"); ");
            sb.AppendLine("				inputBox.addEventListener(\"keydown\", function handleEnter(event) { ");
            sb.AppendLine("					if (event.key === \"Enter\") { ");
            sb.AppendLine("						event.preventDefault(); ");
            sb.AppendLine("						findTitlesContainingSubstring(event.target.value); ");
            sb.AppendLine("					} ");
            sb.AppendLine("				}); ");
            sb.AppendLine("			}); ");
            sb.AppendLine(" ");
            sb.AppendLine("			// Add event listener for double-click on the listbox items ");
            sb.AppendLine("			const listBox = document.getElementById(\"listBoxAssuntos\"); ");
            sb.AppendLine(" ");
            sb.AppendLine("			listBox.addEventListener(\"dblclick\", function(event) { ");
            sb.AppendLine("				const selectedItem = listBox.options[listBox.selectedIndex]?.text; ");
            sb.AppendLine("				if (selectedItem) { ");
            sb.AppendLine("					showSubjectDetails(selectedItem); ");
            sb.AppendLine("				} ");
            sb.AppendLine("			}); ");
            sb.AppendLine("		</script> ");
            sb.AppendLine(" ");
        }

        protected override void PrintIndexPage(List<PageData> listPages, PageData pageData, string webSiteTitle, bool useDarkTheme = true)
        {
            StringBuilder sb = new StringBuilder();
            string theme = useDarkTheme ? "data-bs-theme=\"dark\"" : "";
            sb.AppendLine("<!DOCTYPE html> ");
            sb.AppendLine($"<html lang=\"en\" {theme}>");
            sb.AppendLine(" ");
            sb.AppendLine("<head> ");
            sb.AppendLine("	<meta http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1252\"> ");
            sb.AppendLine($"	<title>{pageData.Title}</title> ");
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
            sb.AppendLine(" <script src=\"https://cdnjs.cloudflare.com/ajax/libs/jszip/3.10.1/jszip.min.js\" crossorigin=\"anonymous\"></script> ");

            // Add javascript according page name
            sb.AppendLine(" ");
            sb.AppendLine("<script src=\"js/tub_pt_br.js\"></script> ");  // Common javascript
            sb.AppendLine($"<script src=\"js/{pageData.Name}.js\"></script> ");  // Specific javascript
            sb.AppendLine(" ");

            sb.AppendLine("</head> ");
            sb.AppendLine("<html> ");
            sb.AppendLine("<body onload=\"StartPage()\"> ");

            PrintNavBar(sb, listPages, pageData, webSiteTitle);


            sb.AppendLine(" ");
            sb.AppendLine("	<div id=\"leftColumn\" class=\"black splitLeft left mt-2 p-2 overflow-auto\"> ");
            if (pageData.Type == PageType.Subject)
            {
                GenerateSubjectIndexHeader(sb);
            }
            else
            {

            }
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
            sb.AppendLine("					<button type=\"button\" class=\"btn btn-danger\" data-bs-dismiss=\"modal\">Fechar</button> ");
            sb.AppendLine("				</div> ");
            sb.AppendLine(" ");
            sb.AppendLine("			</div> ");
            sb.AppendLine("		</div> ");
            sb.AppendLine("	</div> ");
            sb.AppendLine(" ");

            if (pageData.Type == PageType.Subject)
            {
                GenerateSubjectIndexScript(sb);
            }

            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            string pathIndexToc = Path.Combine(StaticObjects.Parameters.EditBookRepositoryFolder, pageData.Name + ".html");
            File.WriteAllText(pathIndexToc, sb.ToString(), Encoding.UTF8);
        }

        public void PrintAllIndexPAges()
        {
            List<PageData> listPages = new List<PageData>()
            {
                new PageData() {Name="indexToc", Type=PageType.Toc, Title="Documentos"},
                new PageData() {Name="indexSubject", Type=PageType.Subject, Title="Assuntos"},
                new PageData() {Name="indexStudy", Type=PageType.Study, Title="Estudos", Enabled=false},
                new PageData() {Name="indexSearch", Type=PageType.Query, Title="Busca", Enabled=false},
                new PageData() {Name="indexTrack", Type=PageType.Track, Title="Trilha", Enabled = false}
            };
            string webSiteTitle = $"O Livro de Urântia - Tradução PT BR - {DateTime.Now.ToString("dd/MM/yyyy")}";

            foreach (PageData pageData in listPages)
            {
                var newList = listPages.Select(page => {
                    page.Active = false;
                    return page;
                }).ToList();
                pageData.Active = true;
                PrintIndexPage(listPages, pageData, webSiteTitle);
            }

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
