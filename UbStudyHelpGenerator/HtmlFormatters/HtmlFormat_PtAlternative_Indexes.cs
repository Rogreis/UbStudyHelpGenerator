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
    /// Generate the index pages for the PT Alternativa web site
    /// </summary>
    internal class HtmlFormat_PtAlternative_Indexes
    {

        const string PageIndexTocName = "indexToc";
        const string PageIndexSubjectName = "indexSubject";
        const string PageIndexTocStudyName = "indexStudy";
        const string PageIndexTocSearchName = "indexSearch";

        const string PageIndexTocTitle = "Documentos";
        const string PageIndexSubjectTitle = "Assuntos";
        const string PageIndexTocStudyTitle = "Artigos";
        const string PageIndexTocSearchTitle = "Busca";

        private string WebSiteTitle { get => "Estudos sobre \"O Livro de Urântia\"";  }


        /// <summary>
        /// Print the head for each page
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="pageData"></param>
        /// <param name="useDarkTheme"></param>
        private void PrintHead(StringBuilder sb, PageData pageData, bool useDarkTheme = true)
        {
            string theme = useDarkTheme ? "data-bs-theme=\"dark\"" : "";
            sb.AppendLine("<!DOCTYPE html>  ");
            sb.AppendLine($"<html lang=\"en\" {theme}>");
            sb.AppendLine("  ");
            sb.AppendLine("<head>  ");
            sb.AppendLine(" <meta http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1252\">  ");
            sb.AppendLine($" <title>{pageData.Title}</title> ");
            sb.AppendLine(" <meta charset=\"utf-8\">  ");
            sb.AppendLine(" <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">  ");
            sb.AppendLine("  ");
            sb.AppendLine(" <link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css\" rel=\"stylesheet\">  ");
            sb.AppendLine(" <script src=\"https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js\"></script>  ");
            sb.AppendLine("  ");
            sb.AppendLine("  ");


            // Lato font from google
            sb.AppendLine("  ");
            sb.AppendLine(" <link rel=\"preconnect\" href=\"https://fonts.googleapis.com\"> ");
            sb.AppendLine(" <link rel=\"preconnect\" href=\"https://fonts.gstatic.com\" crossorigin> ");
            sb.AppendLine(" <link href=\"https://fonts.googleapis.com/css2?family=Lato:wght@100;300;400;700&display=swap\" rel=\"stylesheet\"> ");
            sb.AppendLine("  ");

            //sb.AppendLine("	<link href=\"https://fonts.googleapis.com/css2?family=Roboto+Slab:wght@400;700&display=swap\" rel=\"stylesheet\">  ");

            sb.AppendLine(" <script src=\"https://cdn.jsdelivr.net/npm/jquery@3.6.1/dist/jquery.min.js\"></script>  ");
            sb.AppendLine(" <script src=\"https://cdnjs.cloudflare.com/ajax/libs/jszip/3.10.1/jszip.min.js\" crossorigin=\"anonymous\"></script>  ");
            sb.AppendLine("  ");

            sb.AppendLine(" <link href=\"css/tub_pt_br.css\"  rel=\"stylesheet\">");
            sb.AppendLine(" <link href=\"css/combotrack.css\" rel=\"stylesheet\">");

            string cssFile = Path.Combine(StaticObjects.Parameters.EditBookRepositoryFolder, "css/" + pageData.Name + ".css");
            if (File.Exists(cssFile))
                sb.AppendLine($" <link href=\"css/{pageData.Name}.css\" rel=\"stylesheet\">");
            sb.AppendLine($" <link href=\"css/articles.css\" rel=\"stylesheet\">");
            sb.AppendLine(" <script src=\"js/tub_pt_br.js\"></script>  ");
            sb.AppendLine($" <script src=\"js/{pageData.Name}.js\"></script> ");  // Specific javascript
            sb.AppendLine("</head> ");
            sb.AppendLine("  ");
        }

        /// <summary>
        /// Print the nav
        /// ~Take care of enabled and active pages
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="listPages"></param>
        private void PrintNavBar(StringBuilder sb, List<PageData> listPages, PageData pageData)
        {
            sb.AppendLine("    <nav class=\"navbar navbar-expand-lg navbar-light bg-primary\"> ");
            sb.AppendLine("        <div class=\"container-fluid\"> ");
            sb.AppendLine("            <button class=\"navbar-toggler\" type=\"button\" data-bs-toggle=\"collapse\" ");
            sb.AppendLine("                data-bs-target=\"#navbarNav\" aria-controls=\"navbarNav\" aria-expanded=\"false\" ");
            sb.AppendLine("                aria-label=\"Toggle navigation\"> ");
            sb.AppendLine("                <span class=\"navbar-toggler-icon\"></span> ");
            sb.AppendLine("            </button> ");
            sb.AppendLine("            <div class=\"collapse navbar-collapse\" id=\"navbarNav\"> ");
            sb.AppendLine(" ");
            sb.AppendLine("				<ul class=\"navbar-nav\"> ");
            foreach (PageData data in listPages)
            {
                // Only create nav entry for enabled pages
                if (data.Enabled)
                {
                    sb.AppendLine("          <li class=\"nav-item bg-primary\">  ");
                    if (data.Active)
                        sb.AppendLine($"            <a class=\"nav-link active navactive\" aria-current=\"page\" href=\"javascript:open_page('{data.Name}')\">" +
                                                            $"{data.Title}</a> ");
                    else
                        sb.AppendLine($"            <a class=\"nav-link navinactive\" aria-current=\"page\" href=\"javascript:open_page('{data.Name}')\">" +
                                                            $"{data.Title}</a> ");
                    sb.AppendLine("          </li>  ");
                }
            }
            sb.AppendLine("   ");
            sb.AppendLine(" ");

            // Título do web site
            sb.AppendLine("                    <li class=\"nav-item\"> ");
            sb.AppendLine($"                        <span class=\"navbar-brand mx-3 titulo\">{WebSiteTitle}</span> ");
            sb.AppendLine("                    </li> ");
            sb.AppendLine(" ");

            sb.AppendLine("                     ");

            // TOC entry box
            sb.AppendLine("            <div class=\"trackCombo\"> ");
            sb.AppendLine("              <div class=\"trackCombo-input-wrapper\"> ");
            sb.AppendLine("                <input type=\"text\" class=\"trackCombo-input\" id=\"mytrackCombo\" autocomplete=\"off\"> ");
            sb.AppendLine("                <button class=\"trackCombo-button\" id=\"mytrackComboButton\"></button> ");
            sb.AppendLine("              </div> ");
            sb.AppendLine("              <div class=\"trackCombo-options\" id=\"mytrackComboOptions\"></div> ");
            sb.AppendLine("            </div> ");


            sb.AppendLine(" ");
            sb.AppendLine("                </ul> ");

            // Data atualização
            sb.AppendLine("                    <div class=\"small-text-container\"> ");
            sb.AppendLine($"                        <p class=\"small-text\">Texto PT-BR atualizado em: {DateTime.Now.ToString("dd/MM/yyyy")}</p> ");
            sb.AppendLine("                    </div> ");


            sb.AppendLine(" ");
            sb.AppendLine("                <div class=\"navbar-nav ms-auto\">   ");

            if (pageData.Name == PageIndexTocStudyName || pageData.Name == PageIndexTocName)
                sb.AppendLine("			<button class=\"btn btn-primary\" onclick=\"window.print()\" title=\"Imprime o texto do artigo selecionado.\" >Imprimir</button> ");

            sb.AppendLine("			    <button class=\"btn btn-primary\" data-bs-toggle=\"modal\"  ");
            sb.AppendLine("				    data-bs-target=\"#myModal\" title=\"Clique para entender o significado das cores de fundo de cada parágrafo.\">Cores</button>  ");

            sb.AppendLine("                </div>   ");
            sb.AppendLine(" ");
            sb.AppendLine("            </div> ");
            sb.AppendLine("        </div> ");
            sb.AppendLine("    </nav> ");
            sb.AppendLine(" ");
        }


        /// <summary>
        /// Print the body got an index toc page
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="listPages"></param>
        /// <param name="pageData"></param>
        /// <param name="useDarkTheme"></param>
        private void PrintBodyIndexToc(StringBuilder sb, PageData pageData)
        {
            sb.AppendLine("    <main> ");
            sb.AppendLine("        <div id=\"leftColumn\"> ");
            sb.AppendLine("        </div> ");
            sb.AppendLine(" ");
            sb.AppendLine("        <div id=\"divisor\" role=\"separator\" aria-orientation=\"horizontal\" tabindex=\"0\" aria-valuemin=\"0\" aria-valuemax=\"100\" aria-valuenow=\"25\" aria-label=\"Divisor entre colunas\"></div> ");
            sb.AppendLine(" ");
            sb.AppendLine("        <div id=\"rightColumn\"> ");
            sb.AppendLine("        </div> ");
            sb.AppendLine("    </main> ");

        }


        /// <summary>
        /// Print the body got an index subject page
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="listPages"></param>
        /// <param name="pageData"></param>
        /// <param name="useDarkTheme"></param>
        private void PrintBodyIndexSubject(StringBuilder sb, PageData pageData)
        {
            sb.AppendLine("    <main> ");
            sb.AppendLine("        <div id=\"leftColumn\"> ");
            sb.AppendLine("			<div id=\"searchData\">  ");
            sb.AppendLine("				<form>  ");
            sb.AppendLine("					<label for=\"searchInputBox\">Busca nos assuntos:</label><br>  ");
            sb.AppendLine("					<input   ");
            sb.AppendLine("						type=\"text\"   ");
            sb.AppendLine("						id=\"searchInputBox\"   ");
            sb.AppendLine("						name=\"inputBox\"   ");
            sb.AppendLine("						style=\"width: 100%;\"  ");
            sb.AppendLine("						placeholder=\"Mínimo 3 letras e enter\" title=\"Digite um mínimo de 3 letras do assunto a procurar e tecle enter.\"  ");
            sb.AppendLine("						>  ");
            sb.AppendLine("						<br><label for=\"listBoxAssuntos\">Assuntos encontrados:</label><br>  ");
            sb.AppendLine("						<select id=\"listBoxAssuntos\" size=\"6\" style=\"width: 100%;\" title=\"Faça um duplo-clique num item para expandir os assuntos.\">  ");
            sb.AppendLine("						</select>  ");
            sb.AppendLine("				</form>  ");
            sb.AppendLine("			</div>  ");
            sb.AppendLine("			<div id=\"detailsList\">  ");
            sb.AppendLine("			</div>  ");
            sb.AppendLine("        </div> ");
            sb.AppendLine(" ");
            sb.AppendLine("        <div id=\"divisor\" role=\"separator\" aria-orientation=\"horizontal\" tabindex=\"0\" aria-valuemin=\"0\" aria-valuemax=\"100\" aria-valuenow=\"25\" aria-label=\"Divisor entre colunas\"></div> ");
            sb.AppendLine(" ");
            sb.AppendLine("        <div id=\"rightColumn\"> ");
            sb.AppendLine("        </div> ");
            sb.AppendLine("    </main> ");




        }


        /// <summary>
        /// Print the body got an index subject page
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="listPages"></param>
        /// <param name="pageData"></param>
        /// <param name="useDarkTheme"></param>
        private void PrintBodyIndexStudy(StringBuilder sb, PageData pageData)
        {
            HtmlFormat_PtAlternative_Articles htmlFormat_PtAlternative_Articles= new HtmlFormat_PtAlternative_Articles();
            sb.AppendLine("    <main> ");
            sb.AppendLine("        <div id=\"leftColumn\"> ");
            sb.AppendLine("<div id=\"divsumario\"> ");
            htmlFormat_PtAlternative_Articles.ArticlesIndex(sb);
            sb.AppendLine("</div>");
            sb.AppendLine("        </div> ");
            sb.AppendLine(" ");
            sb.AppendLine("        <div id=\"divisor\" role=\"separator\" aria-orientation=\"horizontal\" tabindex=\"0\" aria-valuemin=\"0\" aria-valuemax=\"100\" aria-valuenow=\"25\" aria-label=\"Divisor entre colunas\"></div> ");
            sb.AppendLine(" ");
            sb.AppendLine("        <div id=\"rightColumn\"> ");
            sb.AppendLine("	<div id=\"divartigo\"> ");
            sb.AppendLine("	</div> ");
            sb.AppendLine("        </div> ");
            sb.AppendLine("    </main> ");
        }

        /// <summary>
        /// Generate the modal for colors
        /// </summary>
        /// <param name="sb"></param>
        private void PrintModalColors(StringBuilder sb)
        {

            int[] coutStauts = new int[5];
            Notes.CountStatus(ref coutStauts);

            sb.AppendLine("<div class=\"modal\" id=\"myModal\">  ");
            sb.AppendLine("		<div class=\"modal-dialog\">  ");
            sb.AppendLine("			<div class=\"modal-content\">  ");
            sb.AppendLine("  ");
            sb.AppendLine("				<!-- Modal Header -->  ");
            sb.AppendLine("				<div class=\"modal-header\">  ");
            sb.AppendLine("					<h4 class=\"modal-title\">Significado das cores de fundo</h4>  ");
            sb.AppendLine("					<button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"modal\"></button>  ");
            sb.AppendLine("				</div>  ");
            sb.AppendLine("  ");
            sb.AppendLine("				<!-- Modal body -->  ");
            sb.AppendLine("				<div class=\"modal-body\">  ");
            sb.AppendLine("  ");
            sb.AppendLine("					<table class=\"table\">  ");
            sb.AppendLine("						<thead>  ");
            sb.AppendLine("							<tr>  ");
            sb.AppendLine("								<th>Cor de fundo</th>  ");
            sb.AppendLine("								<th>Significado</th>  ");
            sb.AppendLine("							</tr>  ");
            sb.AppendLine("						</thead>  ");
            sb.AppendLine("						<tbody>  ");
            sb.AppendLine("							<tr>  ");
            sb.AppendLine("								<td>  ");
            sb.AppendLine("									<div class=\"badge badgeStarted\">Iniciado</div>  ");
            sb.AppendLine("								</td>  ");
            sb.AppendLine($"								<td>{coutStauts[0]} Parágrafo(s) ainda na versão 2007.</td>  ");
            sb.AppendLine("							</tr>  ");
            sb.AppendLine("							<tr>  ");
            sb.AppendLine("								<td>  ");
            sb.AppendLine("									<div class=\"badge badgeWorking\">Em trabalho</div>  ");
            sb.AppendLine("								</td>  ");
            sb.AppendLine($"								<td>{coutStauts[1]} Parágrafo(s) com tradução em andamento.</td>  ");
            sb.AppendLine("							</tr>  ");
            sb.AppendLine("							<tr>  ");
            sb.AppendLine("								<td>  ");
            sb.AppendLine("									<div class=\"badge badgeDoubt\">Em dúvida</div>  ");
            sb.AppendLine("								</td>  ");
            sb.AppendLine($"								<td>{coutStauts[2]} Parágrafo(s) em dúvida.</td>  ");
            sb.AppendLine("							</tr>  ");
            sb.AppendLine("							<tr>  ");
            sb.AppendLine("								<td>  ");
            sb.AppendLine("									<div class=\"badge badgeOk\">Ok</div>  ");
            sb.AppendLine("								</td>  ");
            sb.AppendLine($"								<td>{coutStauts[3]} Parágrafo(s) finalizado(s).</td>  ");
            sb.AppendLine("							</tr>  ");
            sb.AppendLine("							<tr>  ");
            sb.AppendLine("								<td>  ");
            sb.AppendLine("									<div class=\"badge badgeClosed\">Fechado</div>  ");
            sb.AppendLine("								</td>  ");
            sb.AppendLine($"								<td>{coutStauts[4]} Parágrafo(s) fechado(s).</td>  ");
            sb.AppendLine("							</tr>  ");
            sb.AppendLine("						</tbody>  ");
            sb.AppendLine("					</table>  ");
            sb.AppendLine("				</div>  ");
            sb.AppendLine("  ");
            sb.AppendLine("				<!-- Modal footer -->  ");
            sb.AppendLine("				<div class=\"modal-footer\">  ");
            sb.AppendLine("					<button type=\"button\" class=\"btn btn-danger\" data-bs-dismiss=\"modal\">Fechar</button>  ");
            sb.AppendLine("				</div>  ");
            sb.AppendLine("  ");
            sb.AppendLine("			</div>  ");
            sb.AppendLine("		</div>  ");
            sb.AppendLine("	</div>  ");
            sb.AppendLine(" ");
        }

        /// <summary>
        /// Used for pages withou a modal
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="pageData"></param>
        private void PrintModalBodyDummy(StringBuilder sb, PageData pageData)
        {

        }

        private void PrintModalStudy(StringBuilder sb, PageData pageData)
        {

        }

        private void PrintIndexPage(List<PageData> listPages, PageData pageData, bool useDarkTheme = true)
        {
            StringBuilder sb= new StringBuilder();

            PrintHead(sb, pageData, useDarkTheme);

            sb.AppendLine("<body onload=\"StartPage()\"> <!-- common -->  ");
            PrintNavBar(sb, listPages, pageData);

            pageData.BodyFunction(sb, pageData);
            pageData.ModalBodyFunction(sb, pageData);
            PrintModalColors(sb);
            sb.AppendLine("<script src=\"js/combotrack.js\"></script> "); // For combobox track
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            string pathIndexToc = Path.Combine(StaticObjects.Parameters.EditBookRepositoryFolder, pageData.Name + ".html");
            File.WriteAllText(pathIndexToc, sb.ToString(), Encoding.UTF8);
        }


        public void PrintAll()
        {
            List<PageData> listPages = new List<PageData>()
            {
                new PageData() {Name=PageIndexTocName, Type=PageType.Toc, Title=PageIndexTocTitle, BodyFunction= PrintBodyIndexToc, ModalBodyFunction= PrintModalBodyDummy},
                new PageData() {Name=PageIndexSubjectName, Type=PageType.Subject, Title=PageIndexSubjectTitle, BodyFunction= PrintBodyIndexSubject, ModalBodyFunction= PrintModalBodyDummy},
                new PageData() {Name=PageIndexTocStudyName, Type=PageType.Study, Title=PageIndexTocStudyTitle, BodyFunction = PrintBodyIndexStudy, ModalBodyFunction = PrintModalStudy},
                new PageData() {Name=PageIndexTocSearchName, Type=PageType.Query, Title=PageIndexTocSearchTitle, Enabled=false, BodyFunction= PrintBodyIndexToc, ModalBodyFunction= PrintModalBodyDummy}
            };

            foreach (PageData pageData in listPages)
            {
                var newList = listPages.Select(page => {
                    page.Active = false;
                    return page;
                }).ToList();
                pageData.Active = true;
                PrintIndexPage(listPages, pageData);
            }
        }

        public void PrintIndexStudy()
        {
            List<PageData> listPages = new List<PageData>()
            {
                new PageData() {Name=PageIndexTocStudyName, Type=PageType.Study, Title=PageIndexTocStudyTitle, BodyFunction = PrintBodyIndexStudy, ModalBodyFunction = PrintModalStudy},
            };

            foreach (PageData pageData in listPages)
            {
                var newList = listPages.Select(page => {
                    page.Active = false;
                    return page;
                }).ToList();
                pageData.Active = true;
                PrintIndexPage(listPages, pageData);
            }
        }



    }
}
