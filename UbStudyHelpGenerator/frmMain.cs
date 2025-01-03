﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using UbStandardObjects.Objects;
using UbStudyHelpGenerator.Classes;
using UbStudyHelpGenerator.Database;
using UbStudyHelpGenerator.HtmlFormatters;
using UbStudyHelpGenerator.Properties;
using UbStudyHelpGenerator.PtBr;
using UbStudyHelpGenerator.UbStandardObjects;
using UbStudyHelpGenerator.UbStandardObjects.Helpers;
using UbStudyHelpGenerator.UbStandardObjects.Objects;
using static System.Environment;
using static UbStudyHelpGenerator.Classes.UrantiaIndex;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Paragraph = UbStudyHelpGenerator.UbStandardObjects.Objects.Paragraph;

namespace UbStudyHelpGenerator
{
    public partial class frmMain : Form
    {
        private bool StillStarting = true;

        private UbtDatabaseSqlServer Server = new UbtDatabaseSqlServer();

        private GetDataFiles getDataFiles = new GetDataFiles();

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
            StaticObjects.ShowMessage += Logger_ShowMessage;
            EventsControl.EntryEdited += AddEntryEdited;  
        }


        private void FillComboWithTranslation(ComboBox comboBox, short selectedTranslation)
        {
            if (comboBox.Items.Count > 0) return;
            comboBox.DisplayMember = "Identification";
            comboBox.ValueMember = "LanguageID";
            List<Translation> list = new List<Translation>(StaticObjects.Book.Translations);
            comboBox.DataSource = list;
            comboBox.SelectedIndex = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].LanguageID == selectedTranslation)
                {
                    comboBox.SelectedIndex = i;
                    break;
                }
            }

        }


        private bool _initialized = false;
        private bool Initialize()
        {
            if (_initialized)
                return true;

            try
            {

                if (!DataInitializer.InitLogger())
                {
                    throw new Exception("Could not initialize logger.");
                }

                if (!DataInitializer.InitParameters())
                {
                    throw new Exception("Could not initialize parameters.");
                }


                //getDataFilesGenerator = new GetDataFilesGenerator(Server, Parameters);
                ////getDataFilesGenerator.ShowMessage += Logger_ShowMessage;
                //getDataFilesGenerator.ShowPaperNumber += ShowPaperNumber;

                //if (!StaticObjects.Book.Inicialize(getDataFilesGenerator))
                //{
                //    ShowMessage("Book not initialized in frmMain_Load");
                //}

                //Parameters.TranslationLeft = getDataFilesGenerator.GetTranslation(Parameters.TranslationIdLeft);
                //Parameters.TranslationMiddle = null; //  getDataFilesGenerator.GetTranslation(Parameters.TranslationIdMiddle);

                //// Set the edit translation (hard coded here to be PT Alternative)
                //short editLanguageId = 2;
                //Translation trans = getDataFilesGenerator.GetTranslation(editLanguageId);
                //TranslationEdit translatioEdit = new TranslationEdit(trans, Parameters.EditParagraphsRepositoryFolder);
                //Parameters.TranslationRight = translatioEdit;

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
            Initialize();

            txHtmlFilesPath.Text = StaticObjects.Parameters.InputHtmlFilesPath;
            txRepositoryOutputFolder.Text = StaticObjects.Parameters.TUB_Files_RepositoryFolder;
            txTranslationRepositoryFolder.Text = StaticObjects.Parameters.EditParagraphsRepositoryFolder;
            txSqlServerConnectionString.Text = Settings.Default.SqlServerConnectionString;
            txTranslationRepositoryFolder.Text = StaticObjects.Parameters.EditParagraphsRepositoryFolder;
            txEditBookRepositoryFolder.Text = StaticObjects.Parameters.EditBookRepositoryFolder;
            numericUpDownStatusPaperNo.Value = StaticObjects.Parameters.LastPaperStatusChanged;
            comboBoxDocsNoTranslation.Text= StaticObjects.Parameters.LastGTPPaper.ToString();
            numericUpDownStatusPaperNo.Value = Convert.ToDecimal(StaticObjects.Parameters.LastDocumentToChangeStatus);
            comboBoxDoc.Text = StaticObjects.Parameters.LastDocumentToRecover.ToString();
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
                case "Translations":
                    tx = textBoxTranslations;
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
        private void GetFolder(TextBox tx, ref string folder)
        {
            FolderBrowserDialog browserDialog = new FolderBrowserDialog();
            if (!string.IsNullOrEmpty(folder))
            {
                browserDialog.SelectedPath = folder;
            }
            else
            {
                browserDialog.SelectedPath = UbStandardObjects.StaticObjects.Parameters.InputHtmlFilesPath;
            }
            if (browserDialog.ShowDialog() == DialogResult.OK)
            {
                folder = tx.Text = browserDialog.SelectedPath;
            }
        }

        /// <summary>
        /// Set the respository output folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btGetRepositoryOutputFolder_Click(object sender, EventArgs e)
        {
            string folder = txRepositoryOutputFolder.Text;
            GetFolder(txRepositoryOutputFolder, ref folder);
            StaticObjects.Parameters.TUB_Files_RepositoryFolder = folder;
        }

        private void btHtmlFilesPath_Click(object sender, EventArgs e)
        {
            string folder = txHtmlFilesPath.Text;
            GetFolder(txHtmlFilesPath, ref folder);
            StaticObjects.Parameters.InputHtmlFilesPath = folder;
        }

        private void btEditTranslationRepositoryFolder_Click(object sender, EventArgs e)
        {
            string folder = txTranslationRepositoryFolder.Text;
            GetFolder(txTranslationRepositoryFolder, ref folder);
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = folder;
        }


        private void btEditBookRepositoryFolder_Click(object sender, EventArgs e)
        {
            string folder = txEditBookRepositoryFolder.Text;
            GetFolder(txEditBookRepositoryFolder, ref folder);
            StaticObjects.Parameters.EditBookRepositoryFolder = folder;
        }

        private void btUfIndexOutputFiles_Click(object sender, EventArgs e)
        {
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

        private void btSpanishEscobar_Click(object sender, EventArgs e)
        {
            if (!Initialize())
                return;
            StaticObjects.Parameters.TUB_Files_RepositoryFolder = txRepositoryOutputFolder.Text;
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            StaticObjects.Parameters.EditBookRepositoryFolder = txEditBookRepositoryFolder.Text;

            // Verify respository existence
            if (!DataInitializer.VerifyRepositories())
            {
                return;
            }


            // Verify respository existence
            if (!DataInitializer.InitTranslations())
            {
                return;
            }
            string pathToWordFile = @"C:\Urantia\Textos\Escobar\Escobar_Import.txt";
            SpanishEscobar escobar = new SpanishEscobar();
            Translation translation = escobar.Import(pathToWordFile);
            ExportTranslation(translation);
            ShowMessage("Finished");

        }

        private void ZipAFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Gz Files (*.txt)|*.txt|JSon files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog.Title = "GZip a File";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = Path.Combine(Path.GetDirectoryName(openFileDialog.FileName), Path.GetFileNameWithoutExtension(openFileDialog.FileName) + ".gz");
                DeleteFile(fileName);
                using (FileStream originalFileStream = File.Open(openFileDialog.FileName, FileMode.Open))
                {
                    using (FileStream compressedFileStream = File.Create(fileName))
                    {
                        using (var compressor = new GZipStream(compressedFileStream, CompressionMode.Compress))
                        {
                            originalFileStream.CopyTo(compressor);
                        }
                    }
                }
            }
        }

        private void btGZipFile_Click(object sender, EventArgs e)
        {
            ZipAFile();
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
            ShowMessage("Reading the Avalable Translations File from app data file");

            if (!Initialize())
                return;
            StaticObjects.Parameters.TUB_Files_RepositoryFolder = txRepositoryOutputFolder.Text;
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            StaticObjects.Parameters.EditBookRepositoryFolder = txEditBookRepositoryFolder.Text;

            // Verify respository existence
            if (!DataInitializer.VerifyRepositories())
            {
                return;
            }


            // Verify respository existence
            if (!DataInitializer.InitTranslations())
            {
                return;
            }



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

                Encoding utf8WithoutBOM = new UTF8Encoding(false);
                File.WriteAllText(pathRepositoryJson, jsonPapers, utf8WithoutBOM);

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
            File.WriteAllText(pathJson, json, Encoding.UTF8);
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
            //    File.WriteAllText(saveFileDialog.FileName, json, Encoding.UTF8);
            //}


            ShowMessage("Exported.");
        }

        private void Test()
        {
            HtmlFormat_PTalternative formatter = new HtmlFormat_PTalternative(StaticObjects.Parameters);
            PtBr_Website tubPT_BR = new PtBr_Website(StaticObjects.Parameters, formatter);
            //tubPT_BR.Test();
        }

        private List<short> GetChangedDocumentsList()
        {
            string[] expressions = {
            "Doc100/Par_100_005_004.md",
            "Doc104/Par_104_005_002.md",
            "Doc104/Par_104_005_006.md",
            "Doc104/Par_104_005_011.md",
            "Doc105/Par_105_000_001.md",
            "Doc105/Par_105_001_000.md",
            "Doc105/Par_105_001_002.md",
            "Doc105/Par_105_001_004.md",
            "Doc105/Par_105_001_005.md",
            "Doc105/Par_105_001_006.md",
            "Doc105/Par_105_001_008.md"
            };

            string pattern = @"\b\d{3}\b";
            List<short> list = new List<short>();

            foreach (string expression in expressions)
            {
                Match match = Regex.Match(expression, pattern);
                if (match.Success)
                {
                    list.Add(Convert.ToInt16(match.Value));
                    ShowMessage($"First 3-digit number in '{expression}': {match.Value}");
                }
                else
                {
                    ShowMessage($"No 3-digit number found in '{expression}'");
                }
            }
            return list;
        }

        private void btInitialize_Click(object sender, EventArgs e)
        {
            if (!Initialize())
                return;
            StaticObjects.Parameters.TUB_Files_RepositoryFolder = txRepositoryOutputFolder.Text;
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            StaticObjects.Parameters.EditBookRepositoryFolder = txEditBookRepositoryFolder.Text;

            // Verify respository existence
            if (!DataInitializer.VerifyRepositories())
            {
                return;
            }


            // Verify respository existence
            if (!DataInitializer.InitTranslations())
            {
                return;
            }
            tabControlMain.Enabled = true;

            StaticObjects.Parameters.DeSerializeComboBoxItems(StaticObjects.Parameters.EntriesUsed, comboBoxEntries);
            comboBoxEntries.Text = StaticObjects.Parameters.Entry.Ident;

            FillComboWithTranslation(comboBoxTranslations, 0);
            FillComboWithTranslation(comboBoxUpLeft, StaticObjects.Parameters.TranslationUpLeft);
            FillComboWithTranslation(comboBoxUpRight, StaticObjects.Parameters.TranslationUpRight);
            FillComboWithTranslation(comboBoxDownLeft, StaticObjects.Parameters.TranslationDownLeft);
        }

        private void ExportAllPapers()
        {
            HtmlFormat_PTalternative formatter = new HtmlFormat_PTalternative(StaticObjects.Parameters);
            PtBr_Website tubPT_BR = new PtBr_Website(StaticObjects.Parameters, formatter);
            tubPT_BR.ShowPaperNumber += ShowPaperNumber;
            tubPT_BR.ShowStatusMessage += Alternative_ShowStatusMessage;

            string mainPageFilePath = Path.Combine(StaticObjects.Parameters.EditBookRepositoryFolder, "indexOld.html");
            tubPT_BR.Print(StaticObjects.Book.EnglishTranslation,
                           StaticObjects.Book.WorkTranslation,
                           StaticObjects.Book.EditTranslation);
        }

        private void ExportPaper(short paperEditNo)
        {
            HtmlFormat_PTalternative formatter = new HtmlFormat_PTalternative(StaticObjects.Parameters);
            PtBr_Website tubPT_BR = new PtBr_Website(StaticObjects.Parameters, formatter);
            tubPT_BR.ShowPaperNumber += ShowPaperNumber;
            tubPT_BR.ShowStatusMessage += Alternative_ShowStatusMessage;

            string mainPageFilePath = Path.Combine(StaticObjects.Parameters.EditBookRepositoryFolder, "indexOld.html");
            tubPT_BR.Print(StaticObjects.Book.EnglishTranslation,
                           StaticObjects.Book.WorkTranslation,
                           StaticObjects.Book.EditTranslation,
                           paperEditNo);
        }


        private void btPTAlternativeGenerate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure to generate all pages for rogreis.github.io into {StaticObjects.Parameters.EditBookRepositoryFolder}?",
                        "Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            ShowMessage("Starting rogreis.github.io pages");

            // TOC Table not forced
            ShowMessage($"Creating TOC table to be stored in: {StaticObjects.Parameters.EditBookRepositoryFolder}");
            List<TUB_TOC_Entry> tocEntries = StaticObjects.Book.EditTranslation.GetTranslation_TOC_Table(false);  // Not forcing generation
            TUB_TOC_Html toc_table = new TUB_TOC_Html(StaticObjects.Parameters, tocEntries);
            string pathTocTable = Path.Combine(StaticObjects.Parameters.EditBookRepositoryFolder, @"content\TocTable.html");
            toc_table.Html(pathTocTable);

            ExportAllPapers();
            ShowMessage("Finished");
            Process.Start("chrome.exe", "localhost");

        }

        private bool ValidateXHTML(string xhtmlFilePath)
        {
            try
            {
                // Create an XML Reader Settings with validation enabled
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.DtdProcessing = DtdProcessing.Parse; // Enable DTD processing
                settings.ValidationType = ValidationType.Schema;

                // Load the XHTML schema (XHTML 1.0 or 1.1)
                settings.Schemas.Add("http://www.w3.org/1999/xhtml", "C:\\Trabalho\\Github\\Rogerio\\g\\UbStudyHelpGenerator\\xhtml11.dtd");

                // Create an XML reader and load the XHTML file
                using (XmlReader reader = XmlReader.Create(xhtmlFilePath, settings))
                {
                    while (reader.Read()) ;
                }

                // If no exceptions are thrown during validation, the file is valid
                return true;
            }
            catch (XmlSchemaValidationException ex)
            {
                // Handle validation errors here
                ShowMessage($"Validation error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        private void CreateEpup(short paperNo)
        {
            Logger_ShowMessage($"Getting paper {paperNo}");
            Paper leftPaper = StaticObjects.Book.EnglishTranslation.Papers[paperNo];
            PaperEdit rightPaper = new PaperEdit(paperNo);
            StaticObjects.Book.EditTranslation.Papers.Add(rightPaper);
            foreach (Paragraph p in StaticObjects.Book.EnglishTranslation.Papers[paperNo].Paragraphs)
            {
                string pathMdFile = Path.Combine(StaticObjects.Parameters.EditParagraphsRepositoryFolder,
                                                   $@"Doc{p.Paper:000}\Par_{p.Paper:000}_{p.Section:000}_{p.ParagraphNo:000}.md");
                if (File.Exists(pathMdFile))
                {
                    rightPaper.AddParagraph(pathMdFile);
                }
                else
                {
                    ShowMessage("Parágrafo não encontrado: " + pathMdFile);
                }
            }
        }

        private void btEpub_Click(object sender, EventArgs e)
        {

            if (!Initialize())
                return;
            StaticObjects.Parameters.TUB_Files_RepositoryFolder = txRepositoryOutputFolder.Text;
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            StaticObjects.Parameters.EditBookRepositoryFolder = txEditBookRepositoryFolder.Text;

            // Verify respository existence
            if (!DataInitializer.VerifyRepositories())
            {
                return;
            }


            // Verify respository existence
            if (!DataInitializer.InitTranslations())
            {
                return;
            }

            Logger_ShowMessage(null);
            string outputEpubFolder = $@"C:\Urantia\Epub";

            if (MessageBox.Show($"Are you sure to generate the epub version into {outputEpubFolder}?",
                        "Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            HashSet<short> paperSet = new HashSet<short>();

            //short startDoc = 0;
            //short endDoc = 197;
            //for (short paperNo = startDoc; paperNo < endDoc; paperNo++)
            //{
            //    paperSet.Add(paperNo);
            //}


            paperSet.Add(5);
            paperSet.Add(143);
            paperSet.Add(144);

            bool useSet = true;

            ShowMessage($"Starting generation of {outputEpubFolder}");

            //List<TUB_TOC_Entry> tocEntries = StaticObjects.Book.EditTranslation.GetTranslation_TOC_Table(false);  // Not forcing generation
            //TUB_TOC_Html toc_table = new TUB_TOC_Html(StaticObjects.Parameters, tocEntries);

            // Get all PT alternative papers
            if (StaticObjects.Book.EditTranslation.Papers.Count == 0)
            {
                if (useSet)
                {
                    foreach (short paperNo in paperSet)
                    {
                        CreateEpup(paperNo);
                    }
                }
                else
                {
                }

                EpubGenerator epubGenerator = new EpubGenerator();
                epubGenerator.ShowMessage += Logger_ShowMessage;
                epubGenerator.GenerateEpubWithTOC(StaticObjects.Book.EnglishTranslation, StaticObjects.Book.EditTranslation, outputEpubFolder, paperSet);
                ShowMessage("Finished");

                //Process.Start("chrome.exe", "localhost");

            }
        }

        private void btValidate_Click(object sender, EventArgs e)
        {

            string pathParagraphFile = @"C:\Trabalho\Github\Rogerio\p\Doc031\Par_031_010_022.md";
            ParagraphEdit p = new ParagraphEdit(pathParagraphFile);

            //FolderBrowserDialog dialog = new FolderBrowserDialog();
            //dialog.SelectedPath = @"C:\Urantia\Epub\EN-PT\EPUB\content";
            //if (dialog.ShowDialog() == DialogResult.OK)
            //{
            //    foreach (string xhtmlFilePath in Directory.GetFiles(dialog.SelectedPath, "*.xhtml"))
            //    {
            //        ShowMessage("");
            //        ShowMessage("xhtmlFilePath");
            //        ShowMessage($"{(ValidateXHTML(xhtmlFilePath) ? "Ok" : "INVALID")}");
            //    }
            //}
        }


        private void btTest_Click(object sender, EventArgs e)
        {

            if (!InitializeApp()) return;

            StaticObjects.Parameters.TUB_Files_RepositoryFolder = txRepositoryOutputFolder.Text;
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            StaticObjects.Parameters.EditBookRepositoryFolder = txEditBookRepositoryFolder.Text;

            // Verify respository existence
            if (!DataInitializer.VerifyRepositories())
            {
                return;
            }


            // Verify respository existence
            if (!DataInitializer.InitTranslations())
            {
                return;
            }

            ShowMessage(null);

            short paperNo = 101;
            Encoding enc = Encoding.UTF8;
            string outputPathHtmlFiles = @"Y:\home\r\github\rogerio\tub_ai\tub_text";
            ShowMessage($"Starting exporting paragraphs to html format for paper: {paperNo}");
            foreach (Paragraph p in StaticObjects.Book.EnglishTranslation.Papers[paperNo].Paragraphs)
            {
                string directory = Path.Combine(outputPathHtmlFiles, $@"Doc{p.Paper:000}");
                Directory.CreateDirectory(directory);
                string pathMdFile = Path.Combine(directory, $@"Par_{p.Paper:000}_{p.Section:000}_{p.ParagraphNo:000}.html");
                if (File.Exists(pathMdFile))
                {
                    File.Delete(pathMdFile);
                }
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<html>");
                sb.AppendLine("<head>");
                sb.AppendLine($"<title>{p.Paper:000}_{p.Section:000}_{p.ParagraphNo:000}</title>");
                //sb.AppendLine($"content=\"text/html; charset=\"{enc.WebName}\"");
                sb.AppendLine("</head>");
                sb.AppendLine("<body>");
                sb.AppendLine("<p>");
                sb.AppendLine($"{p.TextNoHtml.ToLower()}");
                sb.AppendLine("</p>");
                sb.AppendLine("</body>");
                sb.AppendLine("</html>");
                File.WriteAllText(pathMdFile, sb.ToString());
            }

            ShowMessage("Finished exporting");

        }


        /// <summary>
        /// Generate the index.html page for rogreis.github.io
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btPtBrIndex_Click(object sender, EventArgs e)
        {
            StaticObjects.Parameters.TUB_Files_RepositoryFolder = txRepositoryOutputFolder.Text;
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            StaticObjects.Parameters.EditBookRepositoryFolder = txEditBookRepositoryFolder.Text;

            if (MessageBox.Show($"Are you sure to generate all pages for rogreis.github.io into {StaticObjects.Parameters.EditBookRepositoryFolder}?",
                        "Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            ShowMessage("Generating index.html");
            HtmlFormat_PTalternative formatter = new HtmlFormat_PTalternative(StaticObjects.Parameters);
            PtBr_Website tubPT_BR = new PtBr_Website(StaticObjects.Parameters, formatter);

            //tubPT_BR.ShowMessage += Logger_ShowMessage;
            tubPT_BR.ShowPaperNumber += ShowPaperNumber;
            tubPT_BR.ShowStatusMessage += Alternative_ShowStatusMessage;

            string mainPageFilePath = Path.Combine(StaticObjects.Parameters.EditBookRepositoryFolder, "indexOld.html");
            ShowMessage("Finished");
            Process.Start("chrome.exe", "localhost");
        }

        private void PrintUndoParagraph(TUB_TOC_Entry entry)
        {
            ShowMessage($"git checkout 6766c6d635380dbb369f654297b7bc4c9532ea61 Doc{entry.PaperNo:000}\\Par_{entry.PaperNo:000}_{entry.SectionNo:000}_{entry.ParagraphNo:000}.md");
        }

        private void PrintUndo()
        {
            foreach (string filePath in Directory.GetFiles(StaticObjects.Parameters.EditParagraphsRepositoryFolder, "Par_*.md", SearchOption.AllDirectories))
            {
                ParagraphEdit edit = new ParagraphEdit(filePath);
                if (edit.Paper < 57)
                {
                    if (edit.ParagraphNo == 0)
                    {
                        ShowMessage($"git checkout 6766c6d635380dbb369f654297b7bc4c9532ea61 Doc{edit.Paper:000}\\Par_{edit.Paper:000}_{edit.Section:000}_{edit.ParagraphNo:000}.md");
                    }
                }
            }
        }

        private void btTocTable_Click(object sender, EventArgs e)
        {
            StaticObjects.Parameters.TUB_Files_RepositoryFolder = txRepositoryOutputFolder.Text;
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            StaticObjects.Parameters.EditBookRepositoryFolder = txEditBookRepositoryFolder.Text;

            // Verify respository existence
            if (!DataInitializer.VerifyRepositories())
            {
                return;
            }


            // Verify respository existence
            if (!DataInitializer.InitTranslations())
            {
                return;
            }


            if (MessageBox.Show($"Are you sure to generate the TOC Table for rogreis.github.io into {StaticObjects.Parameters.EditBookRepositoryFolder}?",
                        "Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            //PrintUndo();

            ShowMessage($"Creating TOC table to be stored in: {StaticObjects.Parameters.EditBookRepositoryFolder}");
            List<TUB_TOC_Entry> tocEntries = StaticObjects.Book.EditTranslation.GetTranslation_TOC_Table(true);
            List<TUB_TOC_Entry> commitEntries = new List<TUB_TOC_Entry>();

            //TUB_TOC_Html toc_table = new TUB_TOC_Html(StaticObjects.Parameters, tocEntries);
            //string pathTocTable = Path.Combine(StaticObjects.Parameters.EditBookRepositoryFolder, @"content\TocTable.html");
            //toc_table.Html(pathTocTable);
            //ShowMessage("Finished");
        }


        private void btExportPtAlternativeDocx_Click(object sender, EventArgs e)
        {
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            StaticObjects.Parameters.EditBookRepositoryFolder = txEditBookRepositoryFolder.Text;
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            PTAlternative alternative = new PTAlternative(StaticObjects.Parameters);
            //alternative.ShowMessage += Logger_ShowMessage;
            alternative.ShowPaperNumber += ShowPaperNumber;
            alternative.ShowStatusMessage += Alternative_ShowStatusMessage;
            alternative.ExportToDocx();
        }

        private void btImportDocx_Click(object sender, EventArgs e)
        {
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            StaticObjects.Parameters.EditBookRepositoryFolder = txEditBookRepositoryFolder.Text;
            PTAlternative alternative = new PTAlternative(StaticObjects.Parameters);
            //alternative.ShowMessage += Logger_ShowMessage;
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

        private void btExportToUbHelp_Click(object sender, EventArgs e)
        {
            if (!Initialize())
                return;
            StaticObjects.Parameters.TUB_Files_RepositoryFolder = txRepositoryOutputFolder.Text;
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            StaticObjects.Parameters.EditBookRepositoryFolder = txEditBookRepositoryFolder.Text;

            // Verify respository existence
            if (!DataInitializer.VerifyRepositories())
            {
                return;
            }


            // Verify respository existence
            if (!DataInitializer.InitTranslations())
            {
                return;
            }

            if (MessageBox.Show($"Are you sure to export PtAlternative to Ub Study Help into {StaticObjects.Parameters.TUB_Files_RepositoryFolder}?",
                        "Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            for (short paperNo = 0; paperNo < 197; paperNo++)
            {
                ShowMessage($"Getting paper {paperNo}");
                StaticObjects.Book.EditTranslation.Paper(paperNo);
            }

            ShowMessage("Exporting PtAlternative...");
            PTAlternative alternative = new PTAlternative(StaticObjects.Parameters);
            alternative.ExportToUbStudyHelp();
            // Verify respository existence
            if (!DataInitializer.ReInicializeTranslations())
            {
                return;
            }

            DataInitializer.StoreTranslationsList();
            ShowMessage("Finished.");
        }

        /// <summary>
        /// Initialize the basic data for this app: checking folders, translation list, and English data
        /// </summary>
        /// <returns></returns>
        private bool InitializeApp()
        {
            if (!Initialize()) return false;

            StaticObjects.Parameters.TUB_Files_RepositoryFolder = txRepositoryOutputFolder.Text;
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            StaticObjects.Parameters.EditBookRepositoryFolder = txEditBookRepositoryFolder.Text;

            // Verify respository existence
            if (!DataInitializer.VerifyRepositories())
            {
                return false;
            }

            // Verify respository existence
            if (!DataInitializer.InitTranslations())
            {
                ShowMessage("Translations not initialized");
                return false;
            }

            StaticObjects.Book.EnglishTranslation = new Translation();

            GetDataFiles dataFiles = new GetDataFiles();
            return DataInitializer.InitTranslation(dataFiles, 0, ref StaticObjects.Book.EnglishTranslation);
        }


        private void btPtAlternativeCompare_Click(object sender, EventArgs e)
        {
            if (!InitializeApp()) return;

            // Avoid calls, keep code
            return;

            PaperCheckingUsingChatGPT gpt = new PaperCheckingUsingChatGPT();
            short paperNo = 100;
            ShowMessage($"Generating Compare for paper {paperNo}");


            ShowMessage("Finished.");
        }

        private void btCompare2_Click(object sender, EventArgs e)
        {
            if (!InitializeApp()) return;

            // Get a list of files changed since last commit
            // git diff --name-only b46a5f308920f4182a066c1e79ece2864d37c8b2 HEAD
            // 
            //string gitCommand = $"git diff --name-only {StaticObjects.Parameters.LastCommitUsedForPTAlternative} HEAD";

            string folderCompare = Path.Combine(StaticObjects.Parameters.EditBookRepositoryFolder, "Compare");

            if (MessageBox.Show($"Are you sure to generate all compare pages for rogreis.github.io into {folderCompare}?",
                        "Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            // 34 - Portuguese 2007
            StaticObjects.Book.WorkTranslation = new Translation();
            GetDataFiles dataFiles = new GetDataFiles();
            if (!DataInitializer.InitTranslation(dataFiles, 34, ref StaticObjects.Book.WorkTranslation))
            {
                ShowMessage("Could not initialize portuguese 2007 translation");
                return;
            }
            ShowMessage("Starting rogreis.github.io pages");

            // TOC Table not forced
            ShowMessage($"Creating TOC table to be stored in: {folderCompare}");
            List<TUB_TOC_Entry> tocEntries = StaticObjects.Book.EditTranslation.GetTranslation_TOC_Table(false);  // Not forcing generation

            HtmlFormat_PTalternative formatter = new HtmlFormat_PTalternative(StaticObjects.Parameters);
            PtBr_Website tubPT_BR = new PtBr_Website(StaticObjects.Parameters, formatter);

            //tubPT_BR.ShowMessage += Logger_ShowMessage;
            tubPT_BR.ShowPaperNumber += ShowPaperNumber;
            tubPT_BR.ShowStatusMessage += Alternative_ShowStatusMessage;

            string mainPageFilePath = Path.Combine(StaticObjects.Parameters.EditBookRepositoryFolder, "indexOld.html");
            // Print(TUB_TOC_Html toc_table, Translation englishTranslation, Translation portuguse2007Translation, Translation ptAlternativeTranslation, short paperNoToPrint= -1)
            string destinationFolder = Path.Combine(StaticObjects.Parameters.EditBookRepositoryFolder, "Compare");
            Directory.CreateDirectory(destinationFolder);
            tubPT_BR.Compare(StaticObjects.Book.EnglishTranslation, StaticObjects.Book.WorkTranslation, StaticObjects.Book.EditTranslation, destinationFolder);
            ShowMessage("Finished");

            //Process.Start("chrome.exe", "localhost");


        }


        private void btRecreateTrans_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Json Files (*.json)|*.json";
            openFileDialog.Title = "Translation Json File";
            openFileDialog.InitialDirectory = StaticObjects.Parameters.EditBookRepositoryFolder;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                string pathRepositoryZipped = Path.Combine(StaticObjects.Parameters.TUB_Files_RepositoryFolder, fileNameWithoutExtension + ".gz");
                DeleteFile(pathRepositoryZipped);


                using (FileStream originalFileStream = File.Open(openFileDialog.FileName, FileMode.Open))
                {
                    using (FileStream compressedFileStream = File.Create(pathRepositoryZipped))
                    {
                        using (var compressor = new GZipStream(compressedFileStream, CompressionMode.Compress))
                        {
                            originalFileStream.CopyTo(compressor);
                        }
                    }
                }

                //using (FileStream compressedFileStream = File.Open(openFileDialog.FileName, FileMode.Open))
                //{
                //    using (MemoryStream outputStream = new MemoryStream())
                //    {
                //        using (var decompressor = new GZipStream(compressedFileStream, CompressionMode.Decompress))
                //        {
                //            decompressor.CopyTo(outputStream);
                //        }
                //        string jsonString = System.Text.Encoding.UTF8.GetString(outputStream.ToArray(), 0, (int)outputStream.Length);
                //        VerifyTranslation(jsonString);
                //    }
                //}
            }
        }

        private void btAlternativaEdit_Click(object sender, EventArgs e)
        {
            //frmEdit frmEdit = new frmEdit();
            //frmEdit.ShowDialog();
        }

        #region Translations functions
        private void btWordToList_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Word docx only files (*.docx)|*.docx";
            openFileDialog.Title = "Input docx word file";
            openFileDialog.InitialDirectory = @"C:\Urantia\Traduções";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ShowMessage($"Importing {openFileDialog.FileName}");
                PaperCheckingUsingChatGPT translator = new PaperCheckingUsingChatGPT();
                if (translator.ImportListFromWord(openFileDialog.FileName))
                {
                    ShowMessage($"Succesfully finished. No. Paragrafhs found: {translator.TranslationListData.Count}");
                }
                else
                {
                    ShowMessage("Finished with error.");
                }
            }
        }



        private void btWordCompare_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Word docx only files (*.docx)|*.docx";
            openFileDialog.Title = "Input docx word file to compare";
            openFileDialog.InitialDirectory = @"C:\Urantia\Traduções";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ShowMessage($"Importing {openFileDialog.FileName}");
                PaperCheckingUsingChatGPT translator = new PaperCheckingUsingChatGPT();
                if (translator.ImportListFromWord(openFileDialog.FileName))
                {
                    ShowMessage($"Succesfully finished. No. Paragrafhs found: {translator.TranslationListData.Count}");
                }
                else
                {
                    ShowMessage("Finished with error.");
                }
            }
        }

        private void btImportGpt4Translation_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Word docx only files (*.docx)|*.docx";
            openFileDialog.Title = "docx word file to store imported text";
            openFileDialog.InitialDirectory = @"C:\Urantia\Traduções";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ShowMessage($"Importing text translation to {openFileDialog.FileName}");
                PaperCheckingUsingChatGPT translator = new PaperCheckingUsingChatGPT();
                if (translator.ImportTextFile(openFileDialog.FileName))
                {
                    ShowMessage($"Succesfully finished. No. Paragrafhs found: {translator.TranslationListData.Count}");
                }
                else
                {
                    ShowMessage("Finished with error.");
                }
            }
        }

        private void btGeneratePtAlternativeCompare_Click(object sender, EventArgs e)
        {

            if (!Initialize())
                return;
            StaticObjects.Parameters.TUB_Files_RepositoryFolder = txRepositoryOutputFolder.Text;
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            StaticObjects.Parameters.EditBookRepositoryFolder = txEditBookRepositoryFolder.Text;

            // Verify respository existence
            if (!DataInitializer.VerifyRepositories())
            {
                return;
            }


            // Verify respository existence
            if (!DataInitializer.InitTranslations())
            {
                return;
            }


            string workFolder = @"C:\Urantia\Traduções\PtAlternative";
            PaperCheckingUsingChatGPT translator = new PaperCheckingUsingChatGPT();
            if (translator.GeneratePtAlternativeWord(workFolder, 101))
            {
                ShowMessage($"Succesfully finished. No. Paragrafhs found: {translator.TranslationListData.Count}");
            }
            else
            {
                ShowMessage("Finished with error.");
            }

        }
        private void btGetEnglishText_Click(object sender, EventArgs e)
        {
            if (comboBoxDocsNoTranslation.Text.Trim() == "") return;
            if (!Initialize())
                return;
            StaticObjects.Parameters.TUB_Files_RepositoryFolder = txRepositoryOutputFolder.Text;
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            StaticObjects.Parameters.EditBookRepositoryFolder = txEditBookRepositoryFolder.Text;

            // Verify respository existence
            if (!DataInitializer.VerifyRepositories())
            {
                return;
            }


            // Verify respository existence
            if (!DataInitializer.InitTranslations())
            {
                return;
            }

            StaticObjects.Parameters.LastGTPPaper = Convert.ToInt16(comboBoxDocsNoTranslation.Text);
            string messageToAi = "Traduza para o português do Brasil, mantenha os números no início de cada linha na tradução, faça a saída em formato texto puro e não intruduza uma linha em branco entre cada duas linhas:";

            Paper paperEnglish = StaticObjects.Book.EnglishTranslation.Paper(StaticObjects.Parameters.LastGTPPaper);
            ShowMessage(null);
            ShowMessage(messageToAi);
            int count = 0;
            foreach (Paragraph p in paperEnglish.Paragraphs)
            {
                if (p.IsPaperTitle || p.IsSectionTitle || p.IsDivider) ShowMessage("");
                ShowMessage($"{p.ID} {p.TextNoHtml}");
                count++;
                if ((count % 20) == 0)
                {
                    ShowMessage("");
                    ShowMessage("");
                    ShowMessage("");
                    ShowMessage("");
                    ShowMessage("");
                    ShowMessage(messageToAi);
                    ShowMessage("");
                }
            }
        }

        private void btAiSaveToRepository_Click(object sender, EventArgs e)
        {
            string pathAiGeneratedTranslation = Path.Combine(StaticObjects.Parameters.EditParagraphsRepositoryFolder + @"\AiGenerated\",
                                                             $@"Doc{StaticObjects.Parameters.LastGTPPaper:000}.txt");
            File.WriteAllText(pathAiGeneratedTranslation, textBoxTranslations.Text.Trim(), Encoding.UTF8);
        }


        #endregion

        private void btBylaws_Click(object sender, EventArgs e)
        {
            PaperCheckingUsingChatGPT gpt = new PaperCheckingUsingChatGPT();

            string pathInputDocx = @"C:\Urantia\Traduções\UAI\UAI Bylaws 2023 EN-PT 2-Angela.docx";
            string pathOutputDocx = @"C:\Urantia\Traduções\UAI\UAI Bylaws 2023 EN-PT 2_PT_BR_Final.docx";
            gpt.ModifyThirdCellInTable(pathInputDocx, pathOutputDocx);



            ShowMessage("Finished.");
        }

        private void ShowParamonyMessage(string message)
        {
            tsParamonyMessages.AppendText(message + Environment.NewLine);
            System.Windows.Forms.Application.DoEvents();
        }

        private void btyParamonyImport_Click(object sender, EventArgs e)
        {
            //string htmlContent = @"<br>""Are you King of Jews"": [UB <a href=""https://www.urantiabookstudy.com/HTML/UrantiaBook/p185.htm#U185_3_2"">185:3.2</a>] 
            //<a href=""https://www.urantiabookstudy.com/HTML/KJV/NewTestament.htm#Jn_18_33"">Jn 18:33,</a> 
            //<a href=""https://www.urantiabookstudy.com/HTML/KJV/NewTestament.htm#Lk_23_3"">Lk 23:3,</a> 
            //<a href=""https://www.urantiabookstudy.com/HTML/KJV/NewTestament.htm#Mk_15_2"">Mk 15:2,</a> 
            //<a href=""https://www.urantiabookstudy.com/HTML/KJV/NewTestament.htm#Mt_27_11"">Mt 27:11</a><br>";

            string htmlContent = File.ReadAllText(@"C:\Urantia\Paramony\PaginaComCitacoes.html");

            var parser = new HtmlParser();
            var result = parser.ParseHtmlExtract(htmlContent);

            ShowParamonyMessage("Quote: " + result.Quote);
            ShowParamonyMessage("UB Reference: " + result.UBReference);
            ShowParamonyMessage("Bible References:");
            foreach (var reference in result.BibleReferences)
            {
                ShowParamonyMessage($" - {reference.BibleReference}: {reference.Url}");
            }
        }

        #region Edit functions
        /// <summary>
        /// Export notes and status from database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btGenerateNotes_Click(object sender, EventArgs e)
        {
            short paperNoStatus = (short)numericUpDownStatusPaperNo.Value;
            if (MessageBox.Show($"Close for edition all paragraphs for paper {paperNoStatus}?",
                 "Confirmation",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            StaticObjects.Parameters.LastDocumentToChangeStatus = paperNoStatus;
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            StaticObjects.Parameters.EditBookRepositoryFolder = txEditBookRepositoryFolder.Text;
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            StaticObjects.Book.EditTranslation.GetPaperEdit(paperNoStatus).StoreFullPaperStatus(paperNoStatus, 4);
            ExportPaper(paperNoStatus);
            ShowMessage($"Paper {paperNoStatus} exported");
            Process.Start("chrome.exe", $"localhost");  //  content\\Doc{paperNoStatus:000}.html 
        }

        private Translation GetTranslationFromCombo(ComboBox comboBox)
        {
            if (comboBox.Items.Count == 0)
            {
                MessageBox.Show("Não Inicializado");
                return null;
            }
            if (comboBox.SelectedIndex == 0) comboBox.SelectedIndex = 0;
            return (Translation)comboBox.SelectedItem;
        }

        private void OpenEditForms(TOC_Entry entry)
        {
            frmEdit aEdit = new frmEdit();

            aEdit.TranslationUpLeft = GetTranslationFromCombo(comboBoxUpLeft);
            aEdit.TranslationUpRight = GetTranslationFromCombo(comboBoxUpRight);
            aEdit.TranslationDownLeft = GetTranslationFromCombo(comboBoxDownLeft);
            aEdit.TranslationEdit = StaticObjects.Book.EditTranslation;


            // Store data
            StaticObjects.Parameters.TranslationUpLeft = aEdit.TranslationUpLeft.LanguageID;
            StaticObjects.Parameters.TranslationUpRight = aEdit.TranslationUpRight.LanguageID;
            StaticObjects.Parameters.TranslationDownLeft = aEdit.TranslationDownLeft.LanguageID;

            aEdit.CurrentEntry = entry;
            aEdit.ShowDialog();
        }


        private void AddEntryEdited(TOC_Entry entry)
        {
            StaticObjects.Parameters.Entry = entry;
            comboBoxEntries.Text = entry.Ident;
            if (comboBoxEntries.Items.Count > 0 && (string)comboBoxEntries.Items[0] == entry.Ident) return;
            if (comboBoxEntries.Items.Count > 20)
            {
                comboBoxEntries.Items.RemoveAt(20);
            }
            comboBoxEntries.Items.Insert(0, entry.Ident);
            comboBoxEntries.Text = entry.Ident;
            StaticObjects.Parameters.EntriesUsed = StaticObjects.Parameters.SerializeComboBoxItems(comboBoxEntries);
        }


        private void btEdita_Click(object sender, EventArgs e)
        {
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            StaticObjects.Parameters.EditBookRepositoryFolder = txEditBookRepositoryFolder.Text;
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            TOC_Entry entry = TOC_Entry.FromHref(comboBoxEntries.Text);
            AddEntryEdited(entry);
            OpenEditForms(entry);
            //short paperEditNo = (short)numericUpDownStatusPaperNo.Value;
            //PaperEdit paperEdit= StaticObjects.Book.EditTranslation.GetPaperEdit(paperEditNo);
        }

        private void btDocEdit_Click(object sender, EventArgs e)
        {
            short paperNo = -1;
            short.TryParse(comboBoxDoc.Text, out paperNo);
            if (paperNo == -1) return;
            StaticObjects.Parameters.TUB_Files_RepositoryFolder = txRepositoryOutputFolder.Text;
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            StaticObjects.Parameters.EditBookRepositoryFolder = txEditBookRepositoryFolder.Text;
            StaticObjects.Parameters.LastDocumentToRecover = paperNo;
            TOC_Entry entry = new TOC_Entry(0, paperNo, 0, 1, 0, 0);
            AddEntryEdited(entry);
            OpenEditForms(entry);
        }


        #endregion

        #region Geração de páginas com assunto



        UrantiaIndex urantiaIndex = new UrantiaIndex();

        private void btLoadSubjects_Click(object sender, EventArgs e)
        {
            string searchString = txSubjectSelect.Text.Trim();
            if (string.IsNullOrEmpty(searchString) || searchString.Length < 3) return;
            string pathAllSubjects = @"C:\Urantia\TUB_Subject\tubIndex_000.json";
            List<string> subjects = urantiaIndex.GetGeneratedSubjectIndexList(pathAllSubjects, searchString);
            listBoxSubjects.Items.Clear();
            listBoxSubjects.Items.AddRange(subjects.ToArray());
        }

        private void btGenerateSubjectPage_Click(object sender, EventArgs e)
        {
            if (listBoxSubjects.SelectedItems.Count == 0) return;
            string subjectIndexSelected= listBoxSubjects.SelectedItems[0].ToString();
            SubjectIndex subjectIndex= urantiaIndex.GetSubjectIndex(subjectIndexSelected);
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            HtmlFormat_PTalternative formatter = new HtmlFormat_PTalternative(StaticObjects.Parameters);
            formatter.PrintAllIndexPAges();
        }

    }
}
