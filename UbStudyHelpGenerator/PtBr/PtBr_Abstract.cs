using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UbStudyHelpGenerator.UbStandardObjects.Objects;
using UbStudyHelpGenerator.UbStandardObjects;
using System.Globalization;
using System.IO;
using UbStudyHelpGenerator.HtmlFormatters;

namespace UbStudyHelpGenerator.PtBr
{
    internal abstract class PtBr_Abstract
    {
        public event ShowPaperNumber ShowPaperNumber = null;

        public event ShowStatusMessage ShowStatusMessage = null;

        protected Parameters Param = null;

        protected HtmlFormat_Palternative Formatter = null;

        #region events
        protected void FireShowMessage(string message)
        {
            StaticObjects.FireSendMessage(message);
        }

        protected void FireShowStatusMessage(string message)
        {
            ShowStatusMessage?.Invoke(message);
        }

        protected void FireShowPaperNumber(short paperNo)
        {
            ShowPaperNumber?.Invoke(paperNo);
        }

        protected void HtmlGenerator_ShowPaperNumber(short paperNo)
        {
            ShowPaperNumber?.Invoke(paperNo);
        }

        protected void HtmlGenerator_ShowMessage(string message, bool isError = false, bool isFatal = false)
        {
            StaticObjects.FireSendMessage(message);
        }
        #endregion

        protected int GetIso8601WeekOfYear(DateTime time)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        protected int WeekDay()
        {
            DateTime currentDate = DateTime.Now;
            return GetIso8601WeekOfYear(currentDate);
        }

        protected void PrintPaper(TUB_TOC_Html toc_table, Translation englishTranslation, Translation translatioRight, short paperNoToPrint)
        {
            FireShowMessage($"Exporting paper {paperNoToPrint}");
            FireShowPaperNumber(paperNoToPrint);
            Paper paperEnglish = englishTranslation.Paper(paperNoToPrint);
            PaperEdit paperEdit = new PaperEdit(paperNoToPrint, Param.EditParagraphsRepositoryFolder);
            /*
                   public void GenerateGitHubPage(Paper englishPaper, List<Paragraph> paragraphs2ndColumn, string destinationFolder, 
                                                   short paperNo, List<Paragraph> paragraphs3rdColumn= null, bool merge2nd3rdColuimns= false)
             * */
            Formatter.GenerateGitHubPage(paperEnglish, paperEdit.Paragraphs, Param.EditBookRepositoryFolder, paperNoToPrint, null, true);
        }


        public void MainPage(HtmlFormat_Palternative formatter, string mainPageFilePath)
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

            formatter.Styles(sb);

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
            sb.AppendLine("						<span class=\"badge btn btn-warning\" onclick=\"loadCompare()\">Show Compare</span>");
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
            sb.AppendLine("	</BODY> ");
            sb.AppendLine("</HTML> ");
            File.WriteAllText(mainPageFilePath, sb.ToString(), Encoding.UTF8);
        }

        public abstract bool Print(TUB_TOC_Html toc_table, Translation englishTranslation, Translation portuguse2007Translation, Translation ptAlternativeTranslation, short paperNoToPrint = -1);

    }
}
