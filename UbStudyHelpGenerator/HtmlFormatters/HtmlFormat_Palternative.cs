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

        //private void GenerateSubjectIndexHeader(StringBuilder sb)
        //{
        //    sb.AppendLine("			<div id=\"searchData\"> ");
        //    sb.AppendLine("				<form> ");
        //    sb.AppendLine("					<label for=\"searchInputBox\">Busca nos assuntos:</label><br> ");
        //    sb.AppendLine("					<input  ");
        //    sb.AppendLine("						type=\"text\"  ");
        //    sb.AppendLine("						id=\"searchInputBox\"  ");
        //    sb.AppendLine("						name=\"inputBox\"  ");
        //    sb.AppendLine("						style=\"width: 200px;\" ");
        //    sb.AppendLine("						placeholder=\"Mínimo 3 letras e enter\" title=\"Digite um mínimo de 3 letras do assunto a procurar e tecle enter.\" ");
        //    sb.AppendLine("						> ");
        //    sb.AppendLine("						<br><label for=\"listBoxAssuntos\">Assuntos encontrados:</label><br> ");
        //    sb.AppendLine("						<select id=\"listBoxAssuntos\" size=\"6\" style=\"width: 200px;\" title=\"Faça um duplo-clique num item para expandir os assuntos.\"> ");
        //    sb.AppendLine("						</select> ");
        //    sb.AppendLine("				</form> ");
        //    sb.AppendLine("			</div> ");
        //    sb.AppendLine("			<div id=\"detailsList\"> ");
        //    sb.AppendLine("			</div> ");
        //    sb.AppendLine(" ");
        //}

        //private void GenerateSubjectIndexScript(StringBuilder sb)
        //{
        //    sb.AppendLine(" ");
        //    sb.AppendLine("		<script> ");
        //    sb.AppendLine("			// Ensure this script runs after the DOM is fully loaded ");
        //    sb.AppendLine("			document.addEventListener(\"DOMContentLoaded\", () => { ");
        //    sb.AppendLine("				const inputBox = document.getElementById(\"searchInputBox\"); ");
        //    sb.AppendLine("				inputBox.addEventListener(\"keydown\", function handleEnter(event) { ");
        //    sb.AppendLine("					if (event.key === \"Enter\") { ");
        //    sb.AppendLine("						event.preventDefault(); ");
        //    sb.AppendLine("						findTitlesContainingSubstring(event.target.value); ");
        //    sb.AppendLine("					} ");
        //    sb.AppendLine("				}); ");
        //    sb.AppendLine("			}); ");
        //    sb.AppendLine(" ");
        //    sb.AppendLine("			// Add event listener for double-click on the listbox items ");
        //    sb.AppendLine("			const listBox = document.getElementById(\"listBoxAssuntos\"); ");
        //    sb.AppendLine(" ");
        //    sb.AppendLine("			listBox.addEventListener(\"dblclick\", function(event) { ");
        //    sb.AppendLine("				const selectedItem = listBox.options[listBox.selectedIndex]?.text; ");
        //    sb.AppendLine("				if (selectedItem) { ");
        //    sb.AppendLine("					showSubjectDetails(selectedItem); ");
        //    sb.AppendLine("				} ");
        //    sb.AppendLine("			}); ");
        //    sb.AppendLine("		</script> ");
        //    sb.AppendLine(" ");
        //}


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

            // Verify existence of GPT translation file 
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

            formatParameters.SetHeader($"English {paperNo}", "GPT 4 Translation", $"PT-BR {DateTime.Now.ToString("dd/MM/yyyy")}", "Compare");
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


    }
}
