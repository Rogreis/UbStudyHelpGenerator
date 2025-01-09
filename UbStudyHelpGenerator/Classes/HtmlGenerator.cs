using System.Collections.Generic;
using System.Drawing;
using System.Text;
using UbStudyHelpGenerator.Classes;
using UbStudyHelpGenerator.UbStandardObjects;
using UbStudyHelpGenerator.UbStandardObjects.Objects;

namespace UBT_Tools_WorkLib
{
    public class HtmlGenerator
    {

        private string FontName { get; set; } = "Verdana";
        private float FontSize { get; set; } = 10;

        public event ShowPaperNumber ShowPaperNumber = null;


        #region Background Colors Properties
        private int _backgroundStarted = Color.White.ToArgb();
        private Color BackgroundStarted
        {
            get { return Color.FromArgb(_backgroundStarted); }
            set { _backgroundStarted = value.ToArgb(); }
        }

        private int _backgroundWorking = Color.FromArgb(238, 255, 204).ToArgb();
        private Color BackgroundWorking
        {
            get { return Color.FromArgb(_backgroundWorking); }
            set { _backgroundWorking = value.ToArgb(); }
        }

        private int _backgroundDoubt = Color.FromArgb(255, 179, 179).ToArgb();
        private Color BackgroundDoubt
        {
            get { return Color.FromArgb(_backgroundDoubt); }
            set { _backgroundDoubt = value.ToArgb(); }
        }

        private int _backgroundOk = Color.FromArgb(204, 255, 230).ToArgb();
        private Color BackgroundOk
        {
            get { return Color.FromArgb(_backgroundOk); }
            set { _backgroundOk = value.ToArgb(); }
        }

        private int _backgroundClosed = Color.FromArgb(212, 212, 212).ToArgb();
        private Color BackgroundClosed
        {
            get { return Color.FromArgb(_backgroundClosed); }
            set { _backgroundClosed = value.ToArgb(); }
        }


        #endregion

        public HtmlGenerator()
        {
        }

        private void FireShowMessage(string message)
        {
            StaticObjects.FireSendMessage(message);
        }

        private void FireShowPaperNumber(short paperNo)
        {
            ShowPaperNumber?.Invoke(paperNo);
        }


        #region CSS Style

        private string statusBackgroundColor(ParagraphStatus ParagraphStatus)
        {
            switch (ParagraphStatus)
            {
                case ParagraphStatus.Started:
                    return System.Drawing.ColorTranslator.ToHtml(BackgroundStarted).Trim();
                case ParagraphStatus.Working:
                    return System.Drawing.ColorTranslator.ToHtml(BackgroundWorking).Trim();
                case ParagraphStatus.Doubt:
                    return System.Drawing.ColorTranslator.ToHtml(BackgroundDoubt).Trim();
                case ParagraphStatus.Ok:
                    return System.Drawing.ColorTranslator.ToHtml(BackgroundOk).Trim();
                case ParagraphStatus.Closed:
                    return System.Drawing.ColorTranslator.ToHtml(BackgroundClosed).Trim();
            }
            return System.Drawing.ColorTranslator.ToHtml(BackgroundStarted).Trim();
        }

        private string statusStyleName(ParagraphStatus ParagraphStatus)
        {
            return "stPar" + ParagraphStatus.ToString();
        }
        private string statusStyleHighlightedName(ParagraphStatus ParagraphStatus)
        {
            return "stParHigh" + ParagraphStatus.ToString();
        }


        private void paragraphStyle(StringBuilder sb, ParagraphStatus ParagraphStatus)
        {
            sb.AppendLine("." + statusStyleName(ParagraphStatus));
            sb.AppendLine("{  ");
            sb.AppendLine(" font-family: " + FontName + ";");
            sb.AppendLine(" font-size: " + FontSize.ToString() + ";  ");
            sb.AppendLine(" background-color: " + statusBackgroundColor(ParagraphStatus) + ";");
            sb.AppendLine(" padding: 16px; ");
            sb.AppendLine("}  ");
        }


        private void ItalicBoldStyles(StringBuilder sb)
        {
            sb.AppendLine("i, b, em  {  ");
            sb.AppendLine(" font-family: " + FontName + ";");
            sb.AppendLine(" font-size: " + FontSize.ToString() + ";  ");
            sb.AppendLine(" color: #FF33CC;  ");
            sb.AppendLine(" font-weight: bold;  ");
            sb.AppendLine("}  ");
        }

        private void StylesCompare(StringBuilder sb)
        {
            string FontFamily = " Verdana,Arial,Helvetica";
            sb.AppendLine("<style type=\"text/css\">");
            sb.AppendLine("body {font-family: " + FontFamily + "; font-size: 14px; color: #000000;}");
            sb.AppendLine("h1   {font-family: " + FontFamily + "; font-size: 32px; color: #000000; text-align: center; font-style: italic; text-transform: none; font-weight: none;}");
            sb.AppendLine("h2   {font-family: " + FontFamily + "; font-size: 28px; color: #000000; text-align: center; font-style: none;   text-transform: none; font-weight: none;}");
            sb.AppendLine("h3   {font-family: " + FontFamily + "; font-size: 22px; color: #000000; text-align: center; font-style: none;   text-transform: none; font-weight: none;}");
            sb.AppendLine("h4   {font-family: " + FontFamily + "; font-size: 18px; color: #000000; text-align: center; font-style: none;   text-transform: none; font-weight: none;}");
            sb.AppendLine("h5   {font-family: " + FontFamily + "; font-size: 14px; color: #000000; text-align: center; font-style: none;   text-transform: none; font-weight: none;}");
            sb.AppendLine("h6   {font-family: " + FontFamily + "; font-size: 12px; color: #000000; text-align: center; font-style: none;   text-transform: none; font-weight: none;}");
            sb.AppendLine("td   {font-family: " + FontFamily + "; font-size: 14px; color: #000000; text-align: left; font-style: none;   text-transform: none; font-weight: none;}");
            sb.AppendLine("sup  {font-size: 9px;  color: #666666;}");
            sb.AppendLine(".ColItal {font-family: " + FontFamily + "; font-size: 14px; color: #663333; font-style: italic;}");
            sb.AppendLine(".Colored {font-family: " + FontFamily + "; font-size: 14px; color: #663333;}");
            sb.AppendLine(".ppg     {font-family: " + FontFamily + "; font-size: 9px;  color: #999999;  vertical-align:top;}");
            sb.AppendLine(".super   {font-family: " + FontFamily + "; font-size: 9px;  color: #000000;  vertical-align:top;}");
            sb.AppendLine(".head4   {font-family: " + FontFamily + "; font-size: 14px; color: #000000;}");
            sb.AppendLine(".head5   {font-family: " + FontFamily + "; font-size: 18px; color: #000000;}");

            sb.AppendLine("h1.title {");
            sb.AppendLine("padding: 6px;");
            sb.AppendLine("font-size: 1.5em;");
            sb.AppendLine("background: ");
            sb.AppendLine("#009DDC;");
            sb.AppendLine("color: ");
            sb.AppendLine("white;");
            sb.AppendLine("}");
            sb.AppendLine("</style>");
        }

        protected virtual void Styles(StringBuilder sb)
        {
            sb.AppendLine("<style type=\"text/css\">  ");
            sb.AppendLine("  ");

            paragraphStyle(sb, ParagraphStatus.Started);
            paragraphStyle(sb, ParagraphStatus.Working);
            paragraphStyle(sb, ParagraphStatus.Doubt);
            paragraphStyle(sb, ParagraphStatus.Ok);
            paragraphStyle(sb, ParagraphStatus.Closed);

            sb.AppendLine("  ");
            sb.AppendLine(".stTitle  ");
            sb.AppendLine("{  ");
            sb.AppendLine(" font-family: " + FontName + ";");
            sb.AppendLine(" font-size: " + FontSize.ToString() + ";  ");
            sb.AppendLine(" font-weight: bold;  ");
            sb.AppendLine("}  ");
            sb.AppendLine(" ");
            ItalicBoldStyles(sb);

            sb.AppendLine("  ");
            sb.AppendLine("table {  ");
            //sb.AppendLine("    border: 1px solid #CCC;  ");
            sb.AppendLine("    border-collapse: collapse;  ");
            sb.AppendLine("}  ");
            sb.AppendLine("td { border: none; }  ");

            // Lists
            sb.AppendLine("li ");
            sb.AppendLine("{   ");
            sb.AppendLine(" font-family: " + FontName + ";");
            sb.AppendLine(" font-size: " + FontSize.ToString() + ";  ");
            sb.AppendLine(" padding: 6px;  ");
            sb.AppendLine("}   ");
            sb.AppendLine(" ");


            sb.AppendLine("</style>  ");
            sb.AppendLine(" ");
        }

        #endregion



        protected string ShowErrorMessage(string Message)
        {
            return "<P>" + Message + "</P>";
        }


        private void pageStart(StringBuilder sb, int paperNo, bool compareStyles = false)
        {
            sb.AppendLine("<HTML>");
            sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1252\">");
            sb.AppendLine("<title>Paper " + paperNo.ToString() + "</title>");
            if (compareStyles)
            {
                StylesCompare(sb);
            }
            else
            {
                Styles(sb);
            }
            sb.AppendLine("<BODY>");
            sb.AppendLine("<table border=\"1\" width=\"100%\" id=\"table1\" cellspacing=\"10\" cellpadding=\"10\">");

        }

        private void pageEnd(StringBuilder sb)
        {
            sb.AppendLine("</table>");
            sb.AppendLine("</BODY>");
            sb.AppendLine("</HTML>");
        }



        private string FormatColumn(string text, char letter, string textClass, string paraIdent)
        {
            string div = $"<div id =\"div{letter}_{paraIdent}\" class=\"{textClass.Trim()}\">{text}</div>";
            return $"<td width= \"33%\" valign=\"top\">{div}</td>";
        }



        public string FormatPaper(List<PT_AlternativeRecord> list)
        {
            StringBuilder sb = new StringBuilder();
            int paperNo = list[0].Paper;
            pageStart(sb, paperNo);

            //foreach(PT_AlternativeRecord p in list)
            //{
            //    sb.AppendLine("<tr>");
            //    sb.AppendLine(FormatColumn(p.Identification + " " + p.English, 'l', statusStyleName(ParagraphStatus.Started), p.ParaIdent));
            //    sb.AppendLine(FormatColumn(p.Portugues2007, 'm', statusStyleName(ParagraphStatus.Started), p.ParaIdent));
            //    sb.AppendLine(FormatColumn(p.Text, 'r', statusStyleName(p.TranslationStatus), p.ParaIdent));
            //    sb.AppendLine("</tr>");
            //}
            pageEnd(sb);
            return sb.ToString();
        }


        //private string textDirection(Translation translation)
        //{
        //	return translation.RightToLeft ? " dir=\"rtl\" " : " dir=\"ltr\" ";
        //}



        //private void titleLine(StringBuilder sb)
        //{
        //	sb.AppendLine("<div class=\"titleDiv\"><tr>");
        //	sb.AppendLine("<table border=\"1\" width=\"100%\" id=\"table1\" cellspacing=\"4\" cellpadding=\"0\">");

        //	if (percentFullPaperLeftColumn > 0)
        //	{
        //		TranslationToShow trans = User.GetTranslationData(workPageParameters.FullPageLeftTranslationId);
        //		if (trans != null)
        //			sb.AppendLine("<td width= \"" + percent.ToString("0.00") + "%\" valign=\"top\">" + trans.Description + "</td>");
        //		else
        //			sb.AppendLine("<td width= \"" + percent.ToString("0.00") + "%\" valign=\"top\">Left Translation</td>");
        //	}

        //	if (percentFullPaperColumnMiddle > 0)
        //	{
        //		percentFullPaperColumnMiddle = 1;
        //		TranslationToShow trans = User.GetTranslationData(workPageParameters.FullPageMiddleTranslationId);
        //		if (trans != null)
        //			sb.AppendLine("<td width= \"" + percent.ToString("0.00") + "%\" valign=\"top\">" + trans.Description + "</td>");
        //		else
        //			sb.AppendLine("<td width= \"" + percent.ToString("0.00") + "%\" valign=\"top\">Middle Translation</td>");
        //	}

        //	if (percentFullPaperColumnRight > 0)
        //		sb.AppendLine("<td width= \"" + percent.ToString("0.00") + "%\" valign=\"top\">Working text</td>");

        //	if (percentColumnMerge > 0)
        //		sb.AppendLine("<td width= \"" + percent.ToString("0.00") + "%\" valign=\"top\">Compare</td>");

        //	sb.AppendLine("</tr></table></div>");
        //}


        //private void formatFullPageLine(ParagraphFullPage p, StringBuilder sb, FullPaperCompareTranslation fullPaperCompareTranslation)
        //{
        //	sb.AppendLine("<tr>");
        //	if (percentFullPaperLeftColumn > 0)
        //		sb.AppendLine("<td width= \"" + percent.ToString("0.00") + "%\" valign=\"top\">" + makeLeftDIV(p) + "</td>");

        //	if (percentFullPaperColumnMiddle > 0)
        //		sb.AppendLine("<td width= \"" + percent.ToString("0.00") + "%\" valign=\"top\">" + makeMiddleDIV(p, false) + "</td>");

        //	if (percentFullPaperColumnRight > 0)
        //		sb.AppendLine("<td width= \"" + percent.ToString("0.00") + "%\" valign=\"top\">" + makeRightDIV(p, false) + "</td>");

        //	if (percentColumnMerge > 0)
        //		sb.AppendLine("<td width= \"" + percent.ToString("0.00") + "%\" valign=\"top\">" + HtmlCompare(p, fullPaperCompareTranslation) + "</td>");

        //	sb.AppendLine("</tr>");
        //}

        //private string PlainText(SearchDataResult s)
        //{
        //	var document = new HtmlDocument();
        //	document.LoadHtml(s.Text);
        //	return HtmlEntity.DeEntitize(document.DocumentNode.InnerText);
        //}


        //private void formatSearchPageLine(SearchDataResult s, StringBuilder sb)
        //{
        //	string HtmlLink = "<a target=\"_blank\" href=\"about:blank\" id=\"" + s.ParaIdent + "\">" + s.Identification + " " + PlainText(s) + "</a>";
        //	sb.AppendLine("<li>" + HtmlLink + "</li> ");
        //}



        //private void GenerateFullWorkHtmlPage(List<ParagraphFullPage> workFullPageParagraphs, FullPaperCompareTranslation fullPaperCompareTranslation)
        //{
        //	StringBuilder sb = new StringBuilder();
        //	try
        //	{
        //		pageStart(sb, workPageParameters.PaperId);
        //		titleLine(sb);
        //		sb.AppendLine("<table border=\"1\" width=\"100%\" id=\"table1\" cellspacing=\"4\" cellpadding=\"0\">");

        //		foreach (ParagraphFullPage p in workFullPageParagraphs)
        //			formatFullPageLine(p, sb, fullPaperCompareTranslation);

        //		sb.AppendLine("</table>");
        //		pageEnd(sb);

        //		HtmlPage = sb.ToString();
        //	}
        //	catch (Exception ex)
        //	{
        //		HtmlPage = ShowErrorMessage(ex.Message);
        //	}
        //}

        //public string makeDIV(ParagraphFullPage p, string text)
        //{
        //	string openStyle = "", closeStyle= "";
        //	switch (p.ParagraphType)
        //          {
        //		case FullPageParagraphType.BookTitle:
        //			openStyle = "<h1>";
        //			closeStyle = "</h1>";
        //			break;
        //		case FullPageParagraphType.PaperTitle:
        //			openStyle = "<h2>";
        //			closeStyle = "</h2>";
        //			break;
        //		case FullPageParagraphType.SectionTitle:
        //			openStyle = "<h3>";
        //			closeStyle = "</h3>";
        //			break;
        //		case FullPageParagraphType.NormalParagraph:
        //			openStyle = "<p>";
        //			closeStyle = "</p>";
        //			break;
        //		case FullPageParagraphType.IdentedParagraph:
        //			openStyle = "<p><bloquote>";
        //			closeStyle = "</bloquote></p>";
        //			break;
        //	}
        //	string TextClass = (p.IsSectionTitle) ? "class=\"stSection\"" : "class=\"" + statusStyleName(ParagraphStatus.Started) + "\"";
        //	return "<div id=\"divl_" + p.ParaIdent + "\">" + openStyle + p.Identification + " " + text + closeStyle + "</div>";
        //}



        //public virtual bool Format(List<ParagraphFullPage> workFullPageParagraphs, FullPaperCompareTranslation fullPaperCompareTranslation)
        //{
        //	if (workFullPageParagraphs == null)
        //	{
        //		return false;
        //	}
        //	try
        //	{
        //		GenerateFullWorkHtmlPage(workFullPageParagraphs, fullPaperCompareTranslation);
        //		return true;
        //	}
        //	catch (Exception ex)
        //	{
        //		HtmlPage = ShowErrorMessage(ex.Message);
        //		return false;
        //	}
        //}

        //public bool FormatSearchHtmlPaper(List<SearchDataResult> searchdataResults)
        //{
        //	try
        //	{
        //		GenerateSearchHtmlPage(searchdataResults);
        //		return true;
        //	}
        //	catch (Exception ex)
        //	{
        //		HtmlPage = ShowErrorMessage(ex.Message);
        //		return false;
        //	}
        //}


        //public void FormatPageWithErrorMessage(string message)
        //{
        //	StringBuilder sb = new StringBuilder();
        //	pageStart(sb, workPageParameters.PaperId);
        //	sb.AppendLine("<p><b>" + message + "</b></p>");
        //	pageEnd(sb);
        //	HtmlPage = sb.ToString();
        //}

        //public string FormatParagraph(string html)
        //{
        //	StringBuilder sb = new StringBuilder();
        //	pageParagrahStart(sb, workPageParameters.PaperId);
        //	sb.AppendLine("<p>" + html + "</p>");
        //	pageEnd(sb);
        //	return sb.ToString();
        //}


    }

}
