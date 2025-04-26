using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using UbStandardObjects.Objects;
using UbStudyHelpGenerator.Classes;
using UbStudyHelpGenerator.UbStandardObjects;
using UbStudyHelpGenerator.UbStandardObjects.Helpers;
using UbStudyHelpGenerator.UbStandardObjects.Objects;
using UBT_WebSite.Classes;
using Paragraph = UbStudyHelpGenerator.UbStandardObjects.Objects.Paragraph;

namespace UbStudyHelpGenerator
{
    public partial class frmEdit : Form
    {
        private enum TextoExibirEnum
        {
            PtAi,
            PtOriginal,
            ComparaAIcomPtAlternative,
            ComparaAIcomPtOriginal,
            ComparaPtOriginalComPtAlternative
        }

        // Public data
        public Translation TranslationUpLeft { get; set; }
        public Translation TranslationUpRight { get; set; }
        public Translation TranslationDownLeft { get; set; }

        public TranslationEdit TranslationEdit { get; set; }
        private PaperEdit PaperEdit { get; set; } = null;
        private ParagraphEdit paragraphEdit { get; set; } = null;
        private string TextoIngles { get; set; } = "";
        private string TextoPortugues { get; set; } = "";
        

        public bool ShowCompare { get; set; } = false;
        public TOC_Entry CurrentEntry { get; set; } = null;
        private GetDataFiles dataFiles = new GetDataFiles();
        private bool Alterado = false;

        private WebBrowser browserTextoPortugues = null;
        private Translation transPtOriginal = null;
        private string htmlPtOriginal = null;
        private short lastPaper = -1;
        private List<string> linesAiTranslation = null;
        private string paragraphAiTranslation = null;
        private TOC_Entry lastEntry = null;



        private TextoExibirEnum TextoExibir = TextoExibirEnum.PtOriginal;
        private bool Inicializando = true;

        public frmEdit()
        {
            InitializeComponent();
        }

        private void ExibeMensagem(string mensagem)
        {
            toolStripStatusLabelMessages.Text = mensagem;
            Application.DoEvents();
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
            sb.AppendLine("sup  {font-size: 9px;  color: #666666;}   ");
            sb.AppendLine("i, b, em  {   ");
            sb.AppendLine("color: #FF33CC;   ");
            sb.AppendLine("font-weight: bold;   ");
            sb.AppendLine("}  ");

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

        public string ConvertMarkdownToHtml(string markdown, bool createPage = true)
        {
            if (string.IsNullOrEmpty(markdown))
            {
                return string.Empty;
            }
            string html = MarkdownHelper.ToHtml(markdown);
            if (createPage)
                return GenerateHtmlPage(html);
            else return html;
        }

        #region Colors
        private Color EditTextBackground(ParagraphStatus status)
        {
            switch (status)
            {
                case ParagraphStatus.Started:
                    comboBoxStatus.SelectedIndex = 0;
                    return Color.FloralWhite;
                case ParagraphStatus.Working:
                    comboBoxStatus.SelectedIndex = 1;
                    return Color.LemonChiffon;
                case ParagraphStatus.Doubt:
                    comboBoxStatus.SelectedIndex = 2;
                    return Color.FromArgb(255, 204, 204);
                case ParagraphStatus.Ok:
                    comboBoxStatus.SelectedIndex = 3;
                    return Color.Aquamarine;
                default:
                    comboBoxStatus.SelectedIndex = 4;
                    return Color.FromArgb(33, 37, 41);
            }
        }


        private Color EditTextForeground(ParagraphStatus status)
        {
            switch (status)
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



        private Color EditTextBackground(Paragraph p)
        {
            return EditTextBackground(p.Status);
        }

        private Color EditTextForeground(Paragraph p)
        {
            return EditTextForeground(p.Status);
        }

        private void comboBoxStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ParagraphStatus status = (ParagraphStatus)comboBoxStatus.SelectedIndex;
            paragraphEdit.Status = status;
            richTextBoxEdit.BackColor = EditTextBackground(status);
            richTextBoxEdit.ForeColor = EditTextForeground(status);
            btOk.Enabled = true;
            Alterado = true;
        }

        #endregion

        private string ShowDifferences(string oldHtmlText, string newHtmlText)
        {
            Merger merger = new Merger(oldHtmlText, newHtmlText);
            return merger.merge();
        }

        /// <summary>
        /// Obtem o texto do traduzido por AI
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        private string GetParagraphAiTranslationText(TOC_Entry entry)
        {
            try
            {
                string folderPath = Path.Combine(StaticObjects.Parameters.EditParagraphsRepositoryFolder, "AiGenerated");
                if (!Directory.Exists(folderPath)) return null;
                string pathTextInputFile = Path.Combine(folderPath, $@"Doc{entry.Paper:000}.txt");
                if (!File.Exists(pathTextInputFile)) return null;
                if (lastPaper != entry.Paper)
                {
                    linesAiTranslation = File.ReadAllLines(pathTextInputFile).ToList();
                }
                string line = linesAiTranslation.Find(p => p.StartsWith(entry.Ident));
                string result = line.Substring(line.IndexOf(' ') + 1);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void VerificaSalvar()
        {
            if (!Alterado) return;
            if (MessageBox.Show("Salvar parágrafo corrente?", "Salvar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                paragraphEdit.Text = richTextBoxEdit.Text;
                PaperEdit.SaveParagraph(StaticObjects.Parameters.EditParagraphsRepositoryFolder, paragraphEdit);
                ExibeMensagem("Parágrafo salvo.");
                EventsControl.FireEntryEdited(paragraphEdit.Entry);
                Alterado = false;
                btOk.Enabled = false;
            }
        }

        /// <summary>
        /// Salva o texto e o status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSalvar_Click(object sender, EventArgs e)
        {
            VerificaSalvar();
        }


        #region Exibe textos

        private void ExibeTextoBrowser(WebBrowser webBrowser, string html)
        {
            webBrowser.Navigate("about:blank");
            if (webBrowser.Document != null)
                webBrowser.Document.OpenNew(true);
            webBrowser.DocumentText = html;
        }

        private void ExibeTranslation(TOC_Entry entry, WebBrowser webBrowser, Translation trans)
        {
            trans = dataFiles.GetTranslation(trans.LanguageID);
            Paragraph par = (from p in trans.Paper(entry.Paper).Paragraphs
                             where p.Entry * entry
                             select p).FirstOrDefault();
            string html = ConvertMarkdownToHtml(par.Text);
            if (trans.LanguageID == 34)
            {
                transPtOriginal = trans;
                browserTextoPortugues = webBrowser;
                htmlPtOriginal = html;
            }
            if (trans.LanguageID == 0)
            {
                TextoIngles = par.Text;
            }
            ExibeTextoBrowser(webBrowser, html);
        }

        private void ExibeTextoPortugues(TOC_Entry entry)
        {
            string htmlTextoAI = paragraphAiTranslation == null ? "Texto AI não encontrado" : ConvertMarkdownToHtml(paragraphAiTranslation);
            string htmlTextoPtAlternative = ConvertMarkdownToHtml(richTextBoxEdit.Text, true);
            switch (TextoExibir)
            {
                case TextoExibirEnum.PtAi:
                    ExibeTextoBrowser(browserTextoPortugues, htmlTextoAI);
                    break;
                case TextoExibirEnum.PtOriginal:
                    ExibeTextoBrowser(browserTextoPortugues, htmlPtOriginal);
                    break;

                case TextoExibirEnum.ComparaAIcomPtAlternative:
                    if (checkBoxInverterComparacao.Checked)
                    {
                        ExibeMensagem("AI modificando Pt Alternative");
                        ExibeTextoBrowser(browserTextoPortugues, ShowDifferences(htmlTextoPtAlternative, htmlTextoAI));
                    }
                    else
                    {
                        ExibeMensagem("Pt Alternative modificando AI");
                        ExibeTextoBrowser(browserTextoPortugues, ShowDifferences(htmlTextoAI, htmlTextoPtAlternative));
                    }
                    break;

                case TextoExibirEnum.ComparaAIcomPtOriginal:
                    if (checkBoxInverterComparacao.Checked)
                    {
                        ExibeMensagem("AI modificando Pt Alternative");
                        ExibeTextoBrowser(browserTextoPortugues, ShowDifferences(htmlTextoAI , htmlPtOriginal));
                    }
                    else
                    {
                        ExibeMensagem("Pt Alternative modificando AI");
                        ExibeTextoBrowser(browserTextoPortugues, ShowDifferences(htmlPtOriginal, htmlTextoAI));
                    }
                    break;
                case TextoExibirEnum.ComparaPtOriginalComPtAlternative:
                    if (checkBoxInverterComparacao.Checked)
                    {
                        ExibeMensagem("PT Original modificando Pt Alternative");
                        ExibeTextoBrowser(browserTextoPortugues, ShowDifferences(htmlPtOriginal, htmlTextoPtAlternative));
                    }
                    else
                    {
                        ExibeMensagem("Pt Alternative modificando PT Original");
                        ExibeTextoBrowser(browserTextoPortugues, ShowDifferences(htmlTextoPtAlternative, htmlPtOriginal));
                    }
                    break;
                    
            }
        }


        private void ExibeTextos(TOC_Entry entry)
        {

            Inicializando = true;

            bool xxx = lastEntry * entry;
            if (lastEntry == null || !(lastEntry * entry))
            {
                lastEntry = entry;
                // Exibe as traduções
                ExibeTranslation(entry, webBrowserUpLeft, TranslationUpLeft);
                ExibeTranslation(entry, webBrowserUpRight, TranslationUpRight);
                ExibeTranslation(entry, webBrowserDownLeft, TranslationDownLeft);

                // Obtem o texto gerado por AI
                paragraphAiTranslation = GetParagraphAiTranslationText(entry);
                if (paragraphAiTranslation == null &&
                    (TextoExibir == TextoExibirEnum.ComparaAIcomPtOriginal || TextoExibir == TextoExibirEnum.ComparaAIcomPtAlternative))
                {
                    TextoExibir = TextoExibirEnum.PtOriginal;
                    rbComparaComPtOriginal.Checked = false;
                    rbPtOriginal.Checked = true;
                }

                rbComparaComPtOriginal.Enabled = rbComparaComPtAlternative.Enabled = rbPtAi.Enabled = !(paragraphAiTranslation == null);

                this.Text = "Editando " + entry.Ident;

                PaperEdit = new PaperEdit(entry.Paper);
                paragraphEdit= PaperEdit.GetParagraphFromRepository(entry);
                richTextBoxEdit.Text = paragraphEdit.MarkdownContent;
                richTextBoxEdit.BackColor = EditTextBackground(paragraphEdit);
                richTextBoxEdit.ForeColor = EditTextForeground(paragraphEdit);
                toolStripStatusLabelIdent.Text = entry.Ident;
                toolStripStatusLabelStatus.Text = paragraphEdit.Status.ToString();
                Alterado = false;
                btOk.Enabled = false;
                ExibeTextoPortugues(entry);
            }
            else
            {
                ExibeTextoPortugues(entry);
            }
            System.Windows.Forms.Application.DoEvents();
            Inicializando = false;
        }

        #endregion


        private void frmEdit_Load(object sender, EventArgs e)
        {
            ExibeTextos(CurrentEntry);
        }


        private void btNext_Click(object sender, EventArgs e)
        {
            VerificaSalvar();
            CurrentEntry = TOC_Entry.NextHRef(CurrentEntry);
            ExibeTextos(CurrentEntry);
        }

        private void btPrevious_Click(object sender, EventArgs e)
        {
            VerificaSalvar();
            CurrentEntry = TOC_Entry.PreviousHRef(CurrentEntry);
            ExibeTextos(CurrentEntry);
        }

        private void rbPtAi_CheckedChanged(object sender, EventArgs e)
        {
            if (Inicializando) return;
            TextoExibir = TextoExibirEnum.PtAi;
            Inicializando = true;
            ExibeTextos(CurrentEntry);
        }

        private void rbComparaComPtAlternative_CheckedChanged(object sender, EventArgs e)
        {
            if (Inicializando) return;
            TextoExibir = TextoExibirEnum.ComparaAIcomPtAlternative;
            Inicializando = true;
            ExibeTextos(CurrentEntry);
        }

        private void rbComparaComPtOriginal_CheckedChanged(object sender, EventArgs e)
        {
            if (Inicializando) return;
            TextoExibir = TextoExibirEnum.ComparaAIcomPtOriginal;
            Inicializando = true;
            ExibeTextos(CurrentEntry);
        }

        private void rbPtOriginal_CheckedChanged(object sender, EventArgs e)
        {
            if (Inicializando) return;
            TextoExibir = TextoExibirEnum.PtOriginal;
            Inicializando = true;
            ExibeTextos(CurrentEntry);
        }

        private void radioButtonPtOriginalPtAlternative_CheckedChanged(object sender, EventArgs e)
        {
            if (Inicializando) return;
            TextoExibir = TextoExibirEnum.ComparaPtOriginalComPtAlternative;
            Inicializando = true;
            ExibeTextos(CurrentEntry);
        }


        private void richTextBoxEdit_TextChanged_1(object sender, EventArgs e)
        {
            if (Inicializando) return;
            Alterado = true;
            btOk.Enabled = true;
            ExibeTextos(CurrentEntry);
        }

        private void checkBoxInverterComparacao_CheckedChanged(object sender, EventArgs e)
        {
            if (Inicializando) return;
            ExibeTextos(CurrentEntry);
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            if (Alterado && MessageBox.Show("Sair sem salvar?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Alterado = false;
                this.Close();                
            }
        }

        private void frmEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            VerificaSalvar();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            VerificaSalvar();
        }

        private async void btTranslate_Click(object sender, EventArgs e)
        {
            Gemini gemini = new Gemini();
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            string textoAi = await gemini.TranslateEnglishToPortuguese(TextoIngles);
            richTextBoxEdit.Text += "\n\n" + textoAi;
            this.Cursor = Cursors.Default;
        }
    }
}
