using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Remoting.Contexts;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using UbStandardObjects;
using UbStandardObjects.Objects;
using UbStudyHelpGenerator.Classes;
using UbStudyHelpGenerator.Database;
using UbStudyHelpGenerator.Properties;
using static System.Environment;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace UbStudyHelpGenerator
{
    public partial class frmMain : Form
    {
        private bool StillStarting = true;

        private UbtDatabaseSqlServer Server = new UbtDatabaseSqlServer();

        private GetDataFilesGenerator getDataFilesGenerator = null;

        private ParametersGenerator Parameters = null;


        private string DataFolder()
        {
            string processName = System.IO.Path.GetFileNameWithoutExtension(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var commonpath = GetFolderPath(SpecialFolder.CommonApplicationData);
            return Path.Combine(commonpath, processName);
        }


        private string MakeProgramDataFolder(string fileName)
        {
            string folder = DataFolder();
            Directory.CreateDirectory(folder);
            return Path.Combine(folder, fileName);
        }


        public frmMain()
        {
            InitializeComponent();
            StaticObjects.Logger.ShowMessage += Logger_ShowMessage;
        }

        private bool _initialized = false;
        private bool Initialize()
        {
            if (_initialized)
                return true;
            if (string.IsNullOrEmpty(StaticObjects.Parameters.TUB_Files_RepositoryFolder))
            {
                ShowMessage("TUB_Files_RepositoryFolder not informed");
                return false;
            }
            try
            {
                getDataFilesGenerator = new GetDataFilesGenerator(Server, Parameters);
                getDataFilesGenerator.ShowMessage += Logger_ShowMessage;
                getDataFilesGenerator.ShowPaperNumber += ShowPaperNumber;

                if (!StaticObjects.Book.Inicialize(getDataFilesGenerator))
                {
                    ShowMessage("Book not initialized in frmMain_Load");
                }

                Parameters.TranslationLeft = getDataFilesGenerator.GetTranslation(Parameters.TranslationIdLeft);
                Parameters.TranslationMiddle = null; //  getDataFilesGenerator.GetTranslation(Parameters.TranslationIdMiddle);
                Parameters.TranslationRight = new TranslationEdit(Parameters.EditParagraphsRepositoryFolder);
                _initialized = true;
                StillStarting = false;
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Parameters = (ParametersGenerator)StaticObjects.Parameters;

            txHtmlFilesPath.Text = StaticObjects.Parameters.InputHtmlFilesPath;
            txRepositoryOutputFolder.Text = StaticObjects.Parameters.TUB_Files_RepositoryFolder;
            txTranslationRepositoryFolder.Text = StaticObjects.Parameters.EditParagraphsRepositoryFolder;
            txSqlServerConnectionString.Text = Settings.Default.SqlServerConnectionString;
            txTranslationRepositoryFolder.Text = StaticObjects.Parameters.EditParagraphsRepositoryFolder;
            txEditBookRepositoryFolder.Text = StaticObjects.Parameters.EditBookRepositoryFolder;
            Initialize();
        }


        /// <summary>
        /// Show a message in the visible textbox
        /// </summary>
        /// <param name="message"></param>
        private void ShowMessage(string message)
        {
            TextBox tx = null;
            switch (tabControlMain.SelectedTab.Tag.ToString())
            {
                case "Edit":
                    tx = txPTalternative;
                    break;
                case "Sql":
                    tx = textBoxFromSqlServer;
                    break;
                case "Html":
                    tx = textBoxFromHtml;
                    break;
                case "Index":
                    tx = txUbIndexMessages;
                    break;
                default:
                    MessageBox.Show(message);
                    return;
            }


            // txUbIndexMessages
            if (message == null)
            {
                tx.Text = "";
            }
            else
            {
                tx.AppendText(message + Environment.NewLine);
            }
            System.Windows.Forms.Application.DoEvents();
        }

        private void ShowPaperNumber(short paperNo)
        {
            toolStripStatusLabelPaperNumber.Text = paperNo.ToString();
            System.Windows.Forms.Application.DoEvents();
        }



        private void Logger_ShowMessage(string message, bool isError = false, bool isFatal = false)
        {
            if (isError || isFatal)
            {
                ShowMessage("Error: " + message);
            }
            else
            {
                ShowMessage(message);
            }
        }

        private void DeleteFile(string pathFile)
        {
            if (File.Exists(pathFile))
                File.Delete(pathFile);
        }



        #region Get folders

        /// <summary>
        /// Get a generic folder
        /// </summary>
        /// <param name="tx"></param>
        /// <param name="previousFolder"></param>
        private void GetFolder(TextBox tx, ref string previousFolder)
        {
            FolderBrowserDialog browserDialog = new FolderBrowserDialog();
            if (!string.IsNullOrEmpty(previousFolder))
            {
                browserDialog.SelectedPath = previousFolder;
            }
            else
            {
                browserDialog.SelectedPath = UbStandardObjects.StaticObjects.Parameters.InputHtmlFilesPath;
            }
            if (browserDialog.ShowDialog() == DialogResult.OK)
            {
                previousFolder = tx.Text = browserDialog.SelectedPath;
            }
        }

        /// <summary>
        /// Set the respository output folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btGetRepositoryOutputFolder_Click(object sender, EventArgs e)
        {
            string folder = StaticObjects.Parameters.TUB_Files_RepositoryFolder;
            GetFolder(txRepositoryOutputFolder, ref folder);
            StaticObjects.Parameters.TUB_Files_RepositoryFolder = folder;
        }

        private void btHtmlFilesPath_Click(object sender, EventArgs e)
        {
            string folder = StaticObjects.Parameters.InputHtmlFilesPath;
            GetFolder(txHtmlFilesPath, ref folder);
            StaticObjects.Parameters.InputHtmlFilesPath = folder;
        }

        private void btEditTranslationRepositoryFolder_Click(object sender, EventArgs e)
        {
            string folder = StaticObjects.Parameters.EditParagraphsRepositoryFolder;
            GetFolder(txTranslationRepositoryFolder, ref folder);
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = folder;
        }


        private void btEditBookRepositoryFolder_Click(object sender, EventArgs e)
        {
            string folder = StaticObjects.Parameters.EditBookRepositoryFolder;
            GetFolder(txEditBookRepositoryFolder, ref folder);
            StaticObjects.Parameters.EditBookRepositoryFolder = folder;
        }



        private void btUfIndexDownloadedFiles_Click(object sender, EventArgs e)
        {
            string folder = StaticObjects.Parameters.IndexDownloadedFiles;
            GetFolder(txUfIndexDownloadeFiles, ref folder);
            StaticObjects.Parameters.IndexDownloadedFiles = folder;
        }

        private void btUfIndexOutputFiles_Click(object sender, EventArgs e)
        {
            string folder = StaticObjects.Parameters.IndexOutputFilesPath;
            GetFolder(txUfIndexOutputFolder, ref folder);
            StaticObjects.Parameters.IndexOutputFilesPath = folder;
        }


        #endregion


        private void btSpanish_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaticObjects.Parameters.InputHtmlFilesPath))
            {
                MessageBox.Show("Choose a folder for html files first.");
                return;
            }

            if (MessageBox.Show("Are you sure to process all downloaded spanish html file?",
                        "Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ShowMessage(null);
                UrantiaSpanish spanish = new UrantiaSpanish();
                spanish.ShowMessage += ShowMessage;
                spanish.ProcessFiles(Server, StaticObjects.Parameters.InputHtmlFilesPath);
            }


        }

        private void btUfIndex_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure that want to regenerate the UB Index?",
                        "Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string localPath = @"C:\Urantia\Index\Originais";
                string outputFiles = @"C:\Urantia\Index\Originais";

                UrantiaIndex index = new UrantiaIndex();
                index.ShowMessage += ShowMessage;
                index.GetIndex(localPath, outputFiles);
            }
            ShowMessage("Finished");
        }

        private void btDownload_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to download and regenerate the UB Index?",
                        "Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ShowMessage(null);
                string localPath = @"C:\Urantia\Index\Originais";
                string outputFiles = @"C:\Urantia\Index\Originais";

                UrantiaIndex index = new UrantiaIndex();
                index.ShowMessage += ShowMessage;
                index.GetIndex(localPath, outputFiles);
            }
        }

        private void btSpanishDownload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaticObjects.Parameters.InputHtmlFilesPath))
            {
                MessageBox.Show("Choose a folder for html files first.");
                return;
            }

            if (MessageBox.Show("Are you sure to download all spanish files?",
                        "Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ShowMessage(null);
                UrantiaSpanish spanish = new UrantiaSpanish();
                spanish.ShowMessage += ShowMessage;
                spanish.ProcessFiles(Server, StaticObjects.Parameters.InputHtmlFilesPath);
            }


        }

        private void txSqlServerConnectionString_TextChanged(object sender, EventArgs e)
        {
            if (StillStarting)
                return;
            StaticObjects.Parameters.SqlServerConnectionString = txSqlServerConnectionString.Text;
        }

        private void txRepositoryOutputFolder_TextChanged(object sender, EventArgs e)
        {
            if (StillStarting)
                return;
            StaticObjects.Parameters.TUB_Files_RepositoryFolder = txRepositoryOutputFolder.Text;
        }



        private void btGenerateFromSql_Click(object sender, EventArgs e)
        {

            ShowMessage(null);
            ShowMessage("Getting all Ok for Use translation from database...");

            StaticObjects.Parameters.TUB_Files_RepositoryFolder = txRepositoryOutputFolder.Text;
            StaticObjects.Book.Translations = getDataFilesGenerator.GetTranslations();
            btGetAllTranslations.Enabled = StaticObjects.Book.Translations != null && StaticObjects.Book.Translations.Count > 0;
            if (btGetAllTranslations.Enabled)
            {
                comboBoxTranslations.DisplayMember = "Identification";
                comboBoxTranslations.ValueMember = "LanguageID";
                comboBoxTranslations.Items.Clear();
                comboBoxTranslations.DataSource = StaticObjects.Book.Translations;
                comboBoxTranslations.SelectedIndex = 0;
            }

            foreach (Translation t in StaticObjects.Book.Translations)
            {
                ShowMessage($"{t.Description}");
            }
            ShowMessage("Finished");
        }


        private void comboBoxTranslations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StillStarting)
                return;
            if (comboBoxTranslations.SelectedIndex >= 0)
            {
                btGetTranslation.Enabled = true;
                btGetTranslation.Text = "Generate " + ((Translation)comboBoxTranslations.SelectedItem).Description;
            }
            else
            {
                btGetTranslation.Enabled = false;
            }
        }


        private void ExportTranslation(Translation translation)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    AllowTrailingCommas = true,
                    WriteIndented = true,
                    IncludeFields = true
                };
                string jsonPapers = JsonSerializer.Serialize<Translation>(translation, options);

                // Delete old files
                string fileNameWithoutExtension = $"TR{translation.LanguageID:000}";
                string pathRepositoryJson = Path.Combine(StaticObjects.Parameters.TUB_Files_RepositoryFolder, fileNameWithoutExtension + ".json");
                string pathRepositoryZipped = Path.Combine(StaticObjects.Parameters.TUB_Files_RepositoryFolder, fileNameWithoutExtension + ".gz");
                DeleteFile(pathRepositoryJson);
                DeleteFile(pathRepositoryZipped);

                // Delete also the application (TO DO: put as parameter)
                string appFolder = @"C:\Trabalho\Github\Rogerio\UbStudyHelp\UbStudyHelpCore\UbStudyHelp\TUB_Files";
                string pathAppZipped = Path.Combine(appFolder, fileNameWithoutExtension + ".gz");
                string pathProgramDataJson = Path.Combine(appFolder, fileNameWithoutExtension + ".json");
                DeleteFile(pathProgramDataJson);
                DeleteFile(pathAppZipped);

                File.WriteAllText(pathRepositoryJson, jsonPapers);

                using (FileStream originalFileStream = File.Open(pathRepositoryJson, FileMode.Open))
                {
                    using (FileStream compressedFileStream = File.Create(pathRepositoryZipped))
                    {
                        using (var compressor = new GZipStream(compressedFileStream, CompressionMode.Compress))
                        {
                            originalFileStream.CopyTo(compressor);
                        }
                    }
                }
                File.Copy(pathRepositoryZipped, pathAppZipped);

            }
            catch (Exception ex)
            {
                StaticObjects.Logger.Error($"Exporting translation {translation}", ex);
            }
        }

        private void btGetTranslation_Click(object sender, EventArgs e)
        {
            StaticObjects.Parameters.TUB_Files_RepositoryFolder = txRepositoryOutputFolder.Text;
            Translation translation = comboBoxTranslations.SelectedItem as Translation;
            int count = translation.Papers.Count;
            if (count == 0)
            {
                count = Server.GetTranslationPapers(translation);
            }
            if (count > 0)
            {
                ExportTranslation(translation);
            }
            ShowMessage("Finished");
        }

        //private void VerifyTranslation(string jsonString)
        //{
        //    try
        //    {
        //        Translation translation = new Translation();
        //        translation.GetData(jsonString)
        //        ShowMessage(null);
        //        foreach(Paper paper in translation.Papers)
        //        {
        //            foreach(Paragraph par in paper.Paragraphs)
        //            {
        //                MatchCollection matches = Regex.Matches(par.Text, "{");
        //                if (matches.Count > 0)
        //                {
        //                    ShowMessage($"{par} with {matches.Count} starting exemplar tags");
        //                    ShowMessage("");
        //                }
        //            }
        //        }

        //        ShowMessage("");
        //        ShowMessage("");
        //        foreach (TOC_Entry entry in translation.TableOfContents)
        //        {
        //            ShowMessage($"{entry}");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ShowMessage($"Error creatings translation object {ex}");
        //    }
        //}

        private void btCheck_Click(object sender, EventArgs e)
        {
            StaticObjects.Parameters.TUB_Files_RepositoryFolder = txRepositoryOutputFolder.Text;
            string pathTxt = @"C:\Urantia\Textos\UF-ENG-001-1955-20.4.txt";

            char[] sep = { ':', '.', '(', ')' };
            // 1:3.8 (26.2)

            string pattern = "^[0-9]{1,3}:";

            int count = 0;
            int found = 0, errors = 0;
            using (StreamReader reader = new StreamReader(pathTxt))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Match m = Regex.Match(line, pattern, RegexOptions.IgnoreCase);
                    if (m.Success)
                    {
                        try
                        {
                            int ind = line.IndexOf(')');
                            string id = line.Substring(0, ind + 1);
                            string[] parts = id.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                            short paper = Convert.ToInt16(parts[0]);
                            short section = Convert.ToInt16(parts[1]);
                            short paragraph = Convert.ToInt16(parts[2]);
                            short page = Convert.ToInt16(parts[3]);
                            short lineNo = Convert.ToInt16(parts[4]);
                            //ShowMessage($"{paper}  {section}  {paragraph}  {page}  {lineNo}");
                            string command = $"UPDATE [dbo].[PaperSectionParagraphSeq] SET [Page] = {page}, [Line] = {lineNo} WHERE [Paper] = {paper} and [Section] = {section} and [Paragraph] = {paragraph}";
                            //
                            count = Server.RunCommand(command);
                            if (count < 1)
                            {
                                errors++;
                                ShowMessage($"{command}");
                            }
                            found++;
                        }
                        catch { errors++; }
                    }
                }
            }
            ShowMessage($"Found {found} occurencies with {errors} errors");
            ShowMessage("Finished");


            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "Gz Files (*.gz)|*.gz";
            //openFileDialog.Title = "Verify GZ Translation File";
            //openFileDialog.InitialDirectory = StaticObjects.Parameters.RepositoryOutputFolder;
            //if (openFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    using (FileStream compressedFileStream = File.Open(openFileDialog.FileName, FileMode.Open))
            //    {
            //        using (MemoryStream outputStream = new MemoryStream())
            //        {
            //            using (var decompressor = new GZipStream(compressedFileStream, CompressionMode.Decompress))
            //            {
            //                decompressor.CopyTo(outputStream);
            //            }
            //            string jsonString = System.Text.Encoding.UTF8.GetString(outputStream.ToArray(), 0, (int)outputStream.Length);
            //            VerifyTranslation(jsonString);
            //        }
            //    }
            //}

        }

        private void btGetAllTranslations_Click(object sender, EventArgs e)
        {
            StaticObjects.Parameters.TUB_Files_RepositoryFolder = txRepositoryOutputFolder.Text;
            if (StaticObjects.Book.Translations != null)
            {
                foreach (var t in StaticObjects.Book.Translations)
                {
                    ShowMessage($"Getting {t}");
                    int count = Server.GetTranslationPapers(t);
                    ShowMessage($"   {count} papers");
                    ExportTranslation(t);
                }
            }
            ShowMessage("Finished");
        }

        private void btExportFormat_Click(object sender, EventArgs e)
        {
            StaticObjects.Parameters.TUB_Files_RepositoryFolder = txRepositoryOutputFolder.Text;
            // Delete also the application
            string appFolder = StaticObjects.Parameters.TUB_Files_RepositoryFolder;
            string pathZipped = Path.Combine(appFolder, "FormatTable.gz");
            string pathJson = Path.Combine(appFolder, "FormatTable.json");
            DeleteFile(pathZipped);
            DeleteFile(pathJson);


            string json = Server.GetParagraphsFormat();
            File.WriteAllText(pathJson, json);
            using (FileStream originalFileStream = File.Open(pathJson, FileMode.Open))
            {
                using (FileStream compressedFileStream = File.Create(pathZipped))
                {
                    using (var compressor = new GZipStream(compressedFileStream, CompressionMode.Compress))
                    {
                        originalFileStream.CopyTo(compressor);
                    }
                }
            }
            DeleteFile(pathJson);



            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.Filter = "Json Files (*.json)|*.json";
            //saveFileDialog.RestoreDirectory = true;
            //saveFileDialog.FileName = "FormatTable.json";

            //if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    File.WriteAllText(saveFileDialog.FileName, json);
            //}


            ShowMessage("Exported.");
        }


        private void btPTAlternativeGenerate_Click(object sender, EventArgs e)
        {
            if (!Initialize())
                return;
            if (MessageBox.Show("Are you sure to generate UbStudyHelp format from edit translation repository?",
                        "Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            ShowMessage("Starting full TUB html output");

            StaticObjects.Parameters.TUB_Files_RepositoryFolder = txRepositoryOutputFolder.Text;
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            StaticObjects.Parameters.EditBookRepositoryFolder = txEditBookRepositoryFolder.Text;

            BootstrapBook formatter = new BootstrapBook(StaticObjects.Parameters.HtmlParam);
            TUB_PT_BR tubPT_BR = new TUB_PT_BR(Parameters);
            PTAlternative alternative = new PTAlternative(StaticObjects.Parameters);

            List<TUB_TOC_Entry> tocEntries = ((TranslationEdit)((ParametersGenerator)StaticObjects.Parameters).TranslationRight).GetTranslationIndex();
            TUB_TOC_Html toc_table = new TUB_TOC_Html(StaticObjects.Parameters.HtmlParam, tocEntries);

            tubPT_BR.ShowMessage += Logger_ShowMessage;
            tubPT_BR.ShowPaperNumber += ShowPaperNumber;
            tubPT_BR.ShowStatusMessage += Alternative_ShowStatusMessage;
            tubPT_BR.RepositoryToBookHtmlPages(formatter, toc_table);
            ShowMessage("Finished");
            //Process.Start("chrome.exe", "--incognito");

        }

        private void btPtRepository_Click(object sender, EventArgs e)
        {
            if (!Initialize())
                return;
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            StaticObjects.Parameters.EditBookRepositoryFolder = txEditBookRepositoryFolder.Text;
            StaticObjects.Parameters.TUB_Files_RepositoryFolder = txRepositoryOutputFolder.Text;
            PTAlternative alternative = new PTAlternative(StaticObjects.Parameters);
            alternative.ShowMessage += Logger_ShowMessage;
            alternative.ShowPaperNumber += ShowPaperNumber;
            alternative.ShowStatusMessage += Alternative_ShowStatusMessage;
            alternative.RepositoryToTUB_Files();
        }

        private void btTocTable_Click(object sender, EventArgs e)
        {
            ShowMessage("Creating TOC table");
            List<TUB_TOC_Entry> tocEntries = ((TranslationEdit)((ParametersGenerator)StaticObjects.Parameters).TranslationRight).GetTranslationIndex(true);
            ShowMessage("Finished");
        }

        /// <summary>
        /// Export notes and status from database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btRecordChanged_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to regenerate all Notes.json for edit translation repository from database?",
                 "Confirmation",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            StaticObjects.Parameters.EditBookRepositoryFolder = txEditBookRepositoryFolder.Text;
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;

            for (short paperNo = 0; paperNo < 197; paperNo++)
            {
                ShowMessage(paperNo.ToString());    
                if (!Server.GetNotesData(paperNo))
                {
                    return;
                }
            }

        }


        private void btExportPtAlternativeDocx_Click(object sender, EventArgs e)
        {
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            StaticObjects.Parameters.EditBookRepositoryFolder = txEditBookRepositoryFolder.Text;
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            PTAlternative alternative = new PTAlternative(StaticObjects.Parameters);
            alternative.ShowMessage += Logger_ShowMessage;
            alternative.ShowPaperNumber += ShowPaperNumber;
            alternative.ShowStatusMessage += Alternative_ShowStatusMessage;
            alternative.ExportToDocx();
        }

        private void btImportDocx_Click(object sender, EventArgs e)
        {
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            StaticObjects.Parameters.EditBookRepositoryFolder = txEditBookRepositoryFolder.Text;
            PTAlternative alternative = new PTAlternative(StaticObjects.Parameters);
            alternative.ShowMessage += Logger_ShowMessage;
            alternative.ShowPaperNumber += ShowPaperNumber;
            alternative.ShowStatusMessage += Alternative_ShowStatusMessage;
            alternative.ImportVoiceChangedFromWord();
        }


        private void Alternative_ShowStatusMessage(string message)
        {
            toolStripStatusLabelMessage.Text = message;
            System.Windows.Forms.Application.DoEvents();
        }



        private string ReplaceAll(string input)
        {
            Regex.Replace(input, @"<br />+", Environment.NewLine);
            return Regex.Replace(input, @"<br>+", Environment.NewLine);
        }


        private void PrintNodes(List<TUB_TOC_Entry> list, string ident)
        {
            foreach (TUB_TOC_Entry item in list)
            {
                ShowMessage($"{ident}{item.Text}");
                if (item.Nodes != null && item.Nodes.Count > 0)
                {
                    PrintNodes(item.Nodes, ident + "   ");
                }
            }
        }

    }
}
