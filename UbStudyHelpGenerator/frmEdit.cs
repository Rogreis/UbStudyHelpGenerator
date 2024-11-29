using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using UbStandardObjects.Objects;
using UbStudyHelpGenerator.UbStandardObjects.Objects;
using UBT_WebSite.Classes;
using Paragraph = UbStudyHelpGenerator.UbStandardObjects.Objects.Paragraph;

namespace UbStudyHelpGenerator
{
    public partial class frmEdit : Form
    {
        // Public data
        public Translation TranslationUpLeft {  get; set; }
        public Translation TranslationUpRight { get; set; }
        public Translation TranslationDownLeft { get; set; }
        public TranslationEdit TranslationEdit { get; set; }
        public bool ShowCompare { get; set; } = false;
        public TOC_Entry CurrentEntry {  get; set; } = null;
        private GetDataFiles dataFiles = new GetDataFiles();

        private bool StartingParagraphEdition = true;

        public frmEdit()
        {
            InitializeComponent();
        }


        private string GenerateHtmlPage(string text)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html> ");
            sb.AppendLine("<html lang=\"en\"> ");
            sb.AppendLine("<head> ");
            sb.AppendLine("    <meta charset=\"UTF-8\"> ");
            sb.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"> ");
            sb.AppendLine("    <title>Styled Paragraph</title> ");
            sb.AppendLine("    <style> ");
            sb.AppendLine("        body { ");
            sb.AppendLine("            font-family: Roboto Slab, serif; ");
            sb.AppendLine("            font-size: 20px; font-weight: 400;");
            sb.AppendLine("            margin: 0; ");
            sb.AppendLine("            background-color: rgb(33, 37, 41);");
            sb.AppendLine("            color: white;\r\n}");
            sb.AppendLine("            padding: 20px;  ");
            sb.AppendLine("        } ");
            sb.AppendLine("        .TextRemoved { color:#ff3333 } ");
            sb.AppendLine("        .TextInserted { color:#66ff33 } ");
            sb.AppendLine("    </style> ");
            sb.AppendLine("</head> ");
            sb.AppendLine("<body> ");
            sb.AppendLine(" ");
            sb.AppendLine($"    <p>{text}</p> ");
            sb.AppendLine(" ");
            sb.AppendLine("</body> ");
            sb.AppendLine("</html> ");
            return sb.ToString();
        }

        public string ConvertMarkdownToHtml(string markdown, bool createPage= true)
        {
            if (string.IsNullOrEmpty(markdown))
            {
                return string.Empty;
            }
            // Replace bold (**text**) with <strong>text</strong>
            string html = Regex.Replace(markdown, @"\*\*(.+?)\*\*", "<strong>$1</strong>");
            // Replace italic (*text*) with <em>text</em>
            html = Regex.Replace(html, @"\*(.+?)\*", "<em>$1</em>");
            // Handle line breaks (\n) to <br>
            html = html.Replace("\n", "<br>");
            if (createPage)
                return GenerateHtmlPage(html);
            else return html;
        }

        private Color EditTextBackground(Paragraph p)
        {
            switch (p.Status)
            {
                case ParagraphStatus.Started:
                    return Color.FloralWhite;
                case ParagraphStatus.Working:
                    return Color.LemonChiffon;
                case ParagraphStatus.Doubt:
                    return Color.Firebrick;
                case ParagraphStatus.Ok:
                    return Color.Aquamarine;
                default:
                    return Color.FromArgb(33, 37, 41);
            }
        }

        private Color EditTextForeground(Paragraph p)
        {
            switch (p.Status)
            {
                case ParagraphStatus.Started:
                    return Color.Black;
                case ParagraphStatus.Working:
                    return Color.Black;
                case ParagraphStatus.Doubt:
                    return Color.Black;
                case ParagraphStatus.Ok:
                    return Color.Black;
                case ParagraphStatus.Closed:
                    return Color.White;
                default:
                    return Color.FromArgb(33, 37, 41);
            }
        }

        #region MyRegion
        private string ShowDifferences(string oldHtmlText, string newHtmlText)
        {
            Merger merger = new Merger(oldHtmlText, newHtmlText);
            return merger.merge();
        }

        private void WorkWithComparedTexts(bool showCompare)
        {
            WebBrowser browserCompare = null;
            Translation transCompare = null;
            if (TranslationUpLeft.LanguageID == 34)
            {
                transCompare = TranslationUpLeft;
                browserCompare = webBrowserDownLeft;
            }
            else if (TranslationUpRight.LanguageID == 34)
            {
                transCompare = TranslationUpRight;
                browserCompare = webBrowserUpRight;
            }
            if (TranslationDownLeft.LanguageID == 34)
            {
                transCompare = TranslationDownLeft;
                browserCompare = webBrowserDownLeft;
            }

            if (transCompare == null)
            {
                checkBoxShowCompare.Enabled = false;
                return;
            }
            else
            {
                checkBoxShowCompare.Enabled = true;
            }

            Paragraph par = (from p in transCompare.Paper(CurrentEntry.Paper).Paragraphs
                             where p.Entry * CurrentEntry
                             select p).FirstOrDefault();
            if (showCompare)
            {
                string htmlCompare = ConvertMarkdownToHtml(par.Text, false);
                string htmlEdit = ConvertMarkdownToHtml(ParagraphEdit.MarkdownToHtml(richTextBoxEdit.Text), false);
                browserCompare.Navigate("about:blank");
                if (browserCompare.Document != null)
                    browserCompare.Document.OpenNew(true);
                browserCompare.DocumentText = ConvertMarkdownToHtml(ShowDifferences(htmlCompare, htmlEdit));
            }
            else
            {
                browserCompare.DocumentText = ConvertMarkdownToHtml(par.Text);
            }
        }
        #endregion


        private void LoadData(TOC_Entry entry)
        {

            TranslationUpLeft = dataFiles.GetTranslation(TranslationUpLeft.LanguageID);

            Paragraph upLeft = (from p in TranslationUpLeft.Paper(entry.Paper).Paragraphs
                                   where p.Entry * entry
                                select p).FirstOrDefault();
            string html = ConvertMarkdownToHtml(upLeft.Text);
            webBrowserUpLeft.DocumentText = html;
            TranslationUpRight = dataFiles.GetTranslation(TranslationUpRight.LanguageID);
            Paragraph upRight = (from p in TranslationUpRight.Paper(entry.Paper).Paragraphs
                                           where p.Entry * entry
                                           select p).FirstOrDefault();
            html = ConvertMarkdownToHtml(upRight.Text);
            webBrowserUpRight.DocumentText = html;
            Paragraph downLeft = null;
            TranslationDownLeft = dataFiles.GetTranslation(TranslationDownLeft.LanguageID);
            downLeft = (from p in TranslationDownLeft.Paper(entry.Paper).Paragraphs
                        where p.Entry * entry
                        select p).FirstOrDefault();
            html = ConvertMarkdownToHtml(downLeft.Text);
            webBrowserDownLeft.DocumentText = html;
            StartingParagraphEdition = true;



            ParagraphEdit paragraphEdit= TranslationEdit.GetParagraph(entry);
            richTextBoxEdit.Text = paragraphEdit.MarkdownContent;
            richTextBoxEdit.BackColor = EditTextBackground(paragraphEdit);
            richTextBoxEdit.ForeColor = EditTextForeground(paragraphEdit);
            toolStripStatusLabelIdent.Text = entry.Ident;
            toolStripStatusLabelStatus.Text = paragraphEdit.Status.ToString();
            lblIdentificacao.Text = TranslationEdit.Description;
            Application.DoEvents();

            if (ShowCompare)
            {
                WorkWithComparedTexts(ShowCompare);
            }

        }


        private void frmEdit_Load(object sender, EventArgs e)
        {
            StartingParagraphEdition = true;
            LoadData(CurrentEntry);
        }


        private void checkBoxShowCompare_CheckedChanged(object sender, EventArgs e)
        {
            ShowCompare= checkBoxShowCompare.Checked;
            WorkWithComparedTexts(ShowCompare);
        }

        private void btNext_Click(object sender, EventArgs e)
        {
            CurrentEntry= TOC_Entry.NextHRef(CurrentEntry);
            StartingParagraphEdition = true;
            LoadData(CurrentEntry);
        }

        private void btPrevious_Click(object sender, EventArgs e)
        {
            CurrentEntry = TOC_Entry.PreviousHRef(CurrentEntry);
            StartingParagraphEdition = true;
            LoadData(CurrentEntry);
        }

        private void richTextBoxEdit_TextChanged(object sender, EventArgs e)
        {
            StartingParagraphEdition = false;
        }
    }
}
