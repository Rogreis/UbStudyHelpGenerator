using System;
using System.Globalization;
using System.IO;
using System.Text;
using UbStudyHelpGenerator.UbStandardObjects;
using UbStudyHelpGenerator.UbStandardObjects.Objects;

namespace UbStudyHelpGenerator.Classes
{
    public class TUB_PT_BR
    {
        public event ShowPaperNumber ShowPaperNumber = null;

        public event ShowStatusMessage ShowStatusMessage = null;

        private Parameters Param = null;

        private TUB_PT_BR_Page Formatter = null;

        #region events
        private void FireShowMessage(string message)
        {
            StaticObjects.FireSendMessage(message);
        }

        private void FireShowStatusMessage(string message)
        {
            ShowStatusMessage?.Invoke(message);
        }

        private void FireShowPaperNumber(short paperNo)
        {
            ShowPaperNumber?.Invoke(paperNo);
        }

        private void HtmlGenerator_ShowPaperNumber(short paperNo)
        {
            ShowPaperNumber?.Invoke(paperNo);
        }

        private void HtmlGenerator_ShowMessage(string message, bool isError = false, bool isFatal = false)
        {
            StaticObjects.FireSendMessage(message);
        }
        #endregion

        public TUB_PT_BR(Parameters param, TUB_PT_BR_Page formatter)
        {
            Param = param;
            Formatter = formatter;
        }


        private int GetIso8601WeekOfYear(DateTime time)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        private int WeekDay()
        {
            DateTime currentDate = DateTime.Now;
            return GetIso8601WeekOfYear(currentDate);
        }

        public void MainPage(string mainPageFilePath)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html> ");
            sb.AppendLine("<head>  ");
            sb.AppendLine("	<meta http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1252\">  ");
            sb.AppendLine("	<title>Paper 1</title>  ");
            sb.AppendLine("	<meta charset=\"utf-8\">  ");
            sb.AppendLine("	<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">  ");
            sb.AppendLine("	<link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/css/bootstrap.min.css\" rel=\"stylesheet\">  ");
            sb.AppendLine("	<link href=\"css/tub_pt_br.css\" rel=\"stylesheet\">  ");
            sb.AppendLine("	<script src=\"https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/js/bootstrap.bundle.min.js\"></script> ");
            sb.AppendLine("	<script src=\"https://cdn.jsdelivr.net/npm/jquery@3.6.1/dist/jquery.min.js\"></script> ");
            sb.AppendLine("	<script src=\"js/tub_pt_br.js\"></script> ");
            sb.AppendLine("	<script type=\"module\">  ");
            sb.AppendLine("		import { Octokit } from \"https://cdn.skypack.dev/@octokit/core\";  ");
            sb.AppendLine("	</script>  ");
            sb.AppendLine("</head>  ");
            sb.AppendLine("<html> ");
            sb.AppendLine("	<body class=\"bg-dark text-white\" onload=\"StartPage()\"> ");
            sb.AppendLine("		<nav class=\"navbar navbar-expand-sm bg-primary text-white\"> ");
            sb.AppendLine("			<div class=\"container-fluid\"> ");
            sb.AppendLine("				<ul class=\"navbar-nav\"> ");
            sb.AppendLine("					<li class=\"nav-item bg-primary text-white\"> ");
            sb.AppendLine($"						<span>O Livro de Urântia - Tradução/Revisão PT BR -  v Week {WeekDay()}-{DateTime.Now.ToString("dd/MM/yyyy")}&nbsp;&nbsp;&nbsp;Status:&nbsp;</span> ");
            sb.AppendLine("					</li> ");
            sb.AppendLine("					<li class=\"nav-item bg-primary text-white\"> ");
            sb.AppendLine("						<span class=\"nav-link badge badgeStarted\">Started</span>&nbsp;");
            sb.AppendLine("						<span class=\"badge badgeWorking\">Working</span>&nbsp;");
            sb.AppendLine("						<span class=\"badge badgeDoubt\">Doubt</span>&nbsp;");
            sb.AppendLine("						<span class=\"badge badgeOk\">Ok</span>&nbsp;");
            sb.AppendLine("						<span class=\"badge badgeClosed\">Closed</span>");
            sb.AppendLine("					</li> ");
            sb.AppendLine("				</ul> ");
            sb.AppendLine("			</div> ");
            sb.AppendLine("		</nav> ");
            sb.AppendLine("		<div id=\"leftColumn\" class=\"black splitLeft left mt-0 overflow-auto\"> ");
            sb.AppendLine("			<h3>Table of Contents</h3> ");
            sb.AppendLine("		</div> ");
            sb.AppendLine("		<div id=\"rightColumn\" class=\"black splitRight right mt-0 overflow-auto\"> ");
            sb.AppendLine("		</div> ");
            sb.AppendLine("		 ");

            //sb.AppendLine("		<!-- Modal edit window --> ");
            //sb.AppendLine("		<div class=\"modal\" id=\"editChildWindow\" data-backdrop=\"static\"> ");
            //sb.AppendLine("			<div class=\"modal-dialog modal-dialog-scrollable modal-xl\"> ");
            //sb.AppendLine("				<div class=\"modal-content\"> ");
            //sb.AppendLine("					<!-- Modal Header --> ");
            //sb.AppendLine("					<div class=\"modal-header\"> ");
            //sb.AppendLine("						<h4 id=\"EditTitle\" class=\"modal-title text-dark\">Edit 10:2-2</h4> ");
            //sb.AppendLine("						<button type=\"button\" class=\"btn-close\" onclick=\"ChangeStatus(0)\"/> ");
            //sb.AppendLine("					</div> ");
            //sb.AppendLine("					<!-- Modal body --> ");
            //sb.AppendLine("					<div class=\"modal-body\"> ");
            //sb.AppendLine("						<div class=\"card\"> ");
            //sb.AppendLine("							<div id=\"divEditParagraph\" class=\"card-body\"> ");
            //sb.AppendLine("								<textarea id=\"EditParagraph\" rows=\"12\">Há 360 milhões de anos as terras ainda se estavam elevando. A América do Norte e do Sul encontravam-se bem altas. A Europa ocidental e as Ilhas Britânicas estavam emergindo, exceto partes do País de Gales, que se achavam profundamente submersas. Não havia grandes lençóis de gelo durante estas idades. Os supostos depósitos glaciais que surgiram em conexão com esses estratos na Europa, África, China e Austrália são devidos às geleiras de montanha isoladas ou ao deslocamento de detritos glaciais de origem posterior. O clima mundial era oceânico, não continental. Os mares do sul eram então mais tépidos do que atualmente e se estendiam desde a América do Norte até as regiões polares. A corrente do Golfo corria pela parte central da América do Norte, sendo desviada na direção leste para banhar e aquecer as margens da Groenlândia, fazendo daquele continente, hoje coberto por um manto de gelo, um verdadeiro paraíso tropical.</textarea> ");
            //sb.AppendLine("							</div> ");
            //sb.AppendLine("						</div> ");
            //sb.AppendLine("						<div class=\"card\"> ");
            //sb.AppendLine("							<div class=\"card-body\"> ");
            //sb.AppendLine("								<h4 class=\"card-title text-dark\">Comments</h4> ");
            //sb.AppendLine("								<textarea id=\"CommentsParagraph\" rows=\"3\"/> ");
            //sb.AppendLine("							</div> ");
            //sb.AppendLine("						</div> ");
            //sb.AppendLine("						<div class=\"card\"> ");
            //sb.AppendLine("							<div class=\"card-body\"> ");
            //sb.AppendLine("								<h4 class=\"card-title text-dark\">Translation Notes</h4> ");
            //sb.AppendLine("								<textarea id=\"TranslationNotesParagraph\" rows=\"3\"/> ");
            //sb.AppendLine("							</div> ");
            //sb.AppendLine("						</div> ");
            //sb.AppendLine("					</div> ");
            //sb.AppendLine("					<!-- Modal footer --> ");
            //sb.AppendLine("					<div class=\"modal-footer\"> ");
            //sb.AppendLine("						<button type=\"button\" class=\"btn badgeWorking\" onclick=\"ChangeStatus(1)\">Working</button> ");
            //sb.AppendLine("						<button type=\"button\" class=\"btn badgeDoubt\" onclick=\"ChangeStatus(2)\">Doubt</button> ");
            //sb.AppendLine("						<button type=\"button\" class=\"btn badgeOk\" onclick=\"ChangeStatus(3)\">Finished</button> ");
            //sb.AppendLine("						<button type=\"button\" class=\"btn badgeClosed\" onclick=\"ChangeStatus(4)\">Close</button> ");
            //sb.AppendLine("						<button type=\"button\" class=\"btn\" onclick=\"ChangeStatus(9)\">Ok</button> ");
            //sb.AppendLine("						<button type=\"button\" class=\"btn\" onclick=\"ChangeStatus(0)\">Cancel</button> ");
            //sb.AppendLine("					</div> ");
            //sb.AppendLine("				</div> ");
            //sb.AppendLine("			</div> ");
            //sb.AppendLine("		</div> ");
            //sb.AppendLine("		 ");
            //sb.AppendLine("		<!-- Modal loading --> ");
            //sb.AppendLine("		<div class=\"modal\" id=\"modalWaiting\"> ");
            //sb.AppendLine("			<div class=\"modal-dialog\"> ");
            //sb.AppendLine("				<div class=\"modal-content\"> ");
            //sb.AppendLine("					<div class=\"modal-header\"> ");
            //sb.AppendLine("					</div> ");
            //sb.AppendLine("					<div class=\"modal-body\"> ");
            //sb.AppendLine("						<button class=\"btn btn-primary\" type=\"button\" disabled> ");
            //sb.AppendLine("						<span class=\"spinner-border spinner-border-sm\" role=\"status\" aria-hidden=\"true\"/> ");
            //sb.AppendLine("						Loading... ");
            //sb.AppendLine("						</button> ");
            //sb.AppendLine("					</div> ");
            //sb.AppendLine("				<div class=\"modal-footer\"> ");
            //sb.AppendLine("				</div> ");
            //sb.AppendLine("			</div> ");
            //sb.AppendLine("		</div> ");
            //sb.AppendLine("		</div> ");
            sb.AppendLine("	</BODY> ");
            sb.AppendLine("</HTML> ");
            File.WriteAllText(mainPageFilePath, sb.ToString(), Encoding.UTF8);
        }

        public bool RepositoryToBookHtmlPages(TUB_TOC_Html toc_table, Translation translatioLeft, Translation translatioRight)
        {
            try
            {
                for (short paperNo = 0; paperNo < 197; paperNo++)
                {
                    FireShowMessage($"Exporting paper {paperNo}");
                    FireShowPaperNumber((short)paperNo);
                    Paper paperEnglish = translatioLeft.Paper(paperNo);
                    PaperEdit paperEdit = new PaperEdit(paperNo, Param.EditParagraphsRepositoryFolder);
                    Formatter.GenerateGitHubPage(Param.EditBookRepositoryFolder, paperEnglish, paperEdit, toc_table, paperNo);
                }
                FireShowMessage("Finished");
                return true;
            }
            catch (Exception ex)
            {
                FireShowMessage($"Exporting translation alternative {ex.Message}");
                UbStandardObjects.StaticObjects.Logger.Error("Exporting translation alternative", ex);
                return false;
            }
        }

        public void Test() => Formatter.Test();

    }
}
