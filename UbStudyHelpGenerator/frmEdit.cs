using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using UbStandardObjects.Objects;
using UbStudyHelpGenerator.Controls;
using UbStudyHelpGenerator.UbStandardObjects;
using UbStudyHelpGenerator.UbStandardObjects.Objects;

namespace UbStudyHelpGenerator
{
    public partial class frmEdit : Form
    {
        private ChromiumWebBrowser browser;
        private Timer timer;
        private TOC_Entry currentEntry = null;
        private string currentFilePath = null;
        private string currentUrl = null;
        private bool HasChanges = false;
        private UbStandardObjects.Objects.Paragraph currentEnglishParagraph = null;
        private ParagraphEdit currentEditParagraph = null;
        private int currentLineIndex = 0;

        public frmEdit()
        {
            InitializeComponent();
            InitializeChromium();
            InitializeTimer();
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowMessage(string message)
        {
            textBoxMessages.AppendText(message + Environment.NewLine);
            Application.DoEvents();
        }


        private void frmEdit_Load_1(object sender, EventArgs e)
        {
            DataInitializer.InitAll();
            if (!string.IsNullOrWhiteSpace(StaticObjects.Parameters.TextReferenceFilePath))
            {
                LoadFileLinesToListBox(StaticObjects.Parameters.TextReferenceFilePath);
                ShowMessage($"{listBoxLines.Items.Count} remaining lines");
            }
        }

        private void InitializeTimer()
        {
            timer = new Timer
            {
                Interval = 1000 
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }



        private void InitializeChromium()
        {
            // For CefSharp.WinForms initialization, visit the link below for more details:
            // https://github.com/cefsharp/CefSharp/wiki/Quick-Start

            chromiumWebBrowser1.LoadError += ChromiumWebBrowser1_LoadError;

            chromiumWebBrowser1.IsBrowserInitializedChanged += ChromiumWebBrowser1_IsBrowserInitializedChanged;
            chromiumWebBrowser1.LoadingStateChanged += ChromiumWebBrowser1_LoadingStateChanged;
            chromiumWebBrowser1.ConsoleMessage += ChromiumWebBrowser1_ConsoleMessage;
            chromiumWebBrowser1.StatusMessage += ChromiumWebBrowser1_StatusMessage;
            chromiumWebBrowser1.FrameLoadEnd += ChromiumWebBrowser1_FrameLoadEnd;

            //browser.StatusMessage += OnBrowserStatusMessage;
            //browser.TitleChanged += OnBrowserTitleChanged;
            //browser.AddressChanged += OnBrowserAddressChanged;
            //browser.LoadError += OnBrowserLoadError;

        }

        private void ChromiumWebBrowser1_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                // Custom CSS to apply magenta background to all italics text
                string css = @"
                            <style>
                                i, em, [style*='font-style: italic'] {
                                    color: magenta;
                                }
                            </style>";

                // Inject the custom CSS into the head of the web page
                string script = $"document.head.insertAdjacentHTML('beforeend', `{css}`);";
                chromiumWebBrowser1.ExecuteScriptAsync(script);
            }
        }

        #region Browser events
        private void ChromiumWebBrowser1_LoadError(object sender, CefSharp.LoadErrorEventArgs e)
        {
            //Actions that trigger a download will raise an aborted error.
            //Aborted is generally safe to ignore
            if (e.ErrorCode == CefErrorCode.Aborted)
            {
                return;
            }

            var errorHtml = string.Format("<html><body><h2>Failed to load URL {0} with error {1} ({2}).</h2></body></html>",
                                              e.FailedUrl, e.ErrorText, e.ErrorCode);

            _ = e.Browser.SetMainFrameDocumentContentAsync(errorHtml);

            //AddressChanged isn't called for failed Urls so we need to manually update the Url TextBox
            this.InvokeOnUiThreadIfRequired(() => ShowErrorMessage(e.FailedUrl));
        }

        private void ChromiumWebBrowser1_StatusMessage(object sender, CefSharp.StatusMessageEventArgs e)
        {
        }

        private void ChromiumWebBrowser1_ConsoleMessage(object sender, CefSharp.ConsoleMessageEventArgs e)
        {
        }

        private void ChromiumWebBrowser1_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
        {
        }

        private void ChromiumWebBrowser1_IsBrowserInitializedChanged(object sender, EventArgs e)
        {
        }

        #endregion

        #region Buttons events

        private void Save(ParagraphStatus status)
        {
            currentEditParagraph.Text= textBoxText.Text;
            currentNote.Status = (short)status;
            currentEditParagraph.SetNote(currentNote);
            currentEditParagraph.SaveText(StaticObjects.Parameters.EditParagraphsRepositoryFolder);
            currentEditParagraph.SaveNotes(StaticObjects.Parameters.EditParagraphsRepositoryFolder);
            ShowMessage(currentEditParagraph.ID + " stored.");
            LoadMarkdownFile(currentEntry, currentFilePath, currentUrl, true);
            HasChanges = false;
        }


        private void btHistoryTrack_Click(object sender, EventArgs e)
        {
        }

        private void btWorking_Click(object sender, EventArgs e)
        {
            Save(ParagraphStatus.Working);
        }

        private void btDone_Click(object sender, EventArgs e)
        {
            Save(ParagraphStatus.Ok);
        }

        private void btDoubt_Click(object sender, EventArgs e)
        {
            Save(ParagraphStatus.Doubt);
        }

        private void btClosed_Click(object sender, EventArgs e)
        {
            Save(ParagraphStatus.Closed);
        }

        #endregion

        private Note currentNote = null;

        #region Loading/deleting data
        private void LoadMarkdownFile(TOC_Entry entry, string filePath, string url, bool isFirstLoad = true)
        {
            // Read the contents of the Markdown file
            string markdownContent = "";
            if (isFirstLoad)
            {
                currentEditParagraph = new ParagraphEdit(filePath);
                markdownContent = currentEditParagraph.MarkdownContent;
                textBoxText.Text = markdownContent;
                Paper paperEnglish = StaticObjects.Book.EnglishTranslation.Paper(entry.Paper);
                currentEnglishParagraph = paperEnglish.GetParagraph(entry);
                currentNote = Notes.GetNote(currentEnglishParagraph);
                switch ((ParagraphStatus)currentNote.Status)
                {
                    case ParagraphStatus.Working:
                        textBoxText.BackColor = btWorking.BackColor;
                        break;
                    case ParagraphStatus.Closed:
                        textBoxText.BackColor = btClosed.BackColor;
                        break;
                    case ParagraphStatus.Doubt:
                        textBoxText.BackColor = btDoubt.BackColor;
                        break;
                    case ParagraphStatus.Ok:
                        textBoxText.BackColor = btDone.BackColor;
                        break;
                }
            }
            else
            {
                markdownContent = textBoxText.Text;
            }


            // Convert the Markdown to HTML using Markdig
            string htmlContent = Markdig.Markdown.ToHtml(markdownContent) + "<br /><hr /><br />" + currentEnglishParagraph.Text;

            // Display the HTML in the ChromiumWebBrowser control
            try
            {
                chromiumWebBrowser1.LoadHtml(htmlContent, url);
            }
            catch 
            {
            }
        }


        private void LoadFileLinesToListBox(string filePath)
        {
            listBoxLines.Items.Clear();

            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    listBoxLines.Items.Add(sr.ReadLine());
                }
            }
            ShowMessage($"{listBoxLines.Items.Count} remaining lines");
        }



        private void btSearchForReferenceFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                Multiselect = false
            };
            if (!string.IsNullOrEmpty(StaticObjects.Parameters.TextReferenceFilePath))
            {
                openFileDialog.InitialDirectory = Path.GetDirectoryName(StaticObjects.Parameters.TextReferenceFilePath);
            }


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StaticObjects.Parameters.TextReferenceFilePath = openFileDialog.FileName;
                LoadFileLinesToListBox(openFileDialog.FileName);
            }
        }

        private TOC_Entry SplitReferenceFileLine(string line)
        {
            try
            {
                if (line == null)
                {
                    return null;
                }

                Regex regex = new Regex(@"\d+");
                List<short> numbers = new List<short>();
                foreach (Match match in regex.Matches(line))
                {
                    short number = short.Parse(match.Value);
                    numbers.Add(number);
                }
                return new TOC_Entry(0, numbers[0], numbers[1], numbers[2], 0, 0);
            }
            catch (Exception ex)
            {
                StaticObjects.FireSendMessage($"Invalid file line: {ex.Message}");
                return null;
            }
        }


        private void ShowParagraph(string selectedLine, int index)
        {
            currentEntry = SplitReferenceFileLine(selectedLine);
            if (currentEntry != null)
            {
                labelReferenceEdited.Text = $"Editing {currentEntry.ParagraphID}";

                currentFilePath = ParagraphEdit.FullPath(StaticObjects.Parameters.EditParagraphsRepositoryFolder, currentEntry);
                currentUrl = $"https://github.com/Rogreis/PtAlternative/blob/correcoes/Doc000/{selectedLine}";
                currentLineIndex = index;
                LoadMarkdownFile(currentEntry, currentFilePath, currentUrl);
                HasChanges = false;
            }
            else
            {
                ShowMessage($"Invalid first line on file: {(System.IO.Path.GetFileNameWithoutExtension(selectedLine))}");
            }
        }


        private void listBoxLines_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBoxLines.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                string selectedLine = listBoxLines.Items[index].ToString();
                ShowParagraph(selectedLine, index);
            }
        }

        private void btRemoveSelectLine_Click(object sender, EventArgs e)
        {
            if (listBoxLines.SelectedItem != null)
            {
                string selectedLine = listBoxLines.SelectedItem.ToString();
                if (MessageBox.Show($"Are you sure to remove line {selectedLine}?",
                            "Confirmation",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    listBoxLines.Items.RemoveAt(listBoxLines.SelectedIndex);
                    List<string> lines = File.ReadAllLines(StaticObjects.Parameters.TextReferenceFilePath).ToList();
                    lines.Remove(selectedLine);
                    File.WriteAllLines(StaticObjects.Parameters.TextReferenceFilePath, lines.ToArray());
                    ShowMessage(currentEditParagraph.ID + " removed.");
                    ShowMessage($"{listBoxLines.Items.Count} remaining lines.");
                    if (currentLineIndex < listBoxLines.Items.Count)
                    {
                        listBoxLines.SelectedIndex = currentLineIndex;
                        selectedLine = listBoxLines.SelectedItem.ToString();
                        ShowParagraph(selectedLine, currentLineIndex);
                    }
                }
            }
        }

        #endregion

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (HasChanges)
            {
                LoadMarkdownFile(currentEntry, currentFilePath, currentUrl, false);
            }
        }


        private void textBoxText_TextChanged(object sender, EventArgs e)
        {
            HasChanges = true;
        }
    }

}
