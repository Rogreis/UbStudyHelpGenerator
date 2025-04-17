using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using UbStandardObjects.Objects;
using UbStudyHelpGenerator.Classes;
using UbStudyHelpGenerator.Database;
using UbStudyHelpGenerator.HtmlFormatters;
using UbStudyHelpGenerator.Properties;
using UbStudyHelpGenerator.PtBr;
using UbStudyHelpGenerator.UbStandardObjects;
using UbStudyHelpGenerator.UbStandardObjects.Exporters;
using UbStudyHelpGenerator.UbStandardObjects.Helpers;
using UbStudyHelpGenerator.UbStandardObjects.ImportExport;
using UbStudyHelpGenerator.UbStandardObjects.Objects;

using static UbStudyHelpGenerator.Classes.UrantiaIndex;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace UbStudyHelpGenerator
{
    public partial class frmMain : Form
    {
        private bool StillStarting = true;
        private bool _initialized = false;
        private UbtDatabaseSqlServer Server = new UbtDatabaseSqlServer();
        private GrepMarkdown grepMarkdown = new GrepMarkdown();

        public frmMain()
        {
            InitializeComponent();
            StaticObjects.ShowMessage += Logger_ShowMessage;

            StaticObjects.ShowPaperNumber += ShowPaperNumber;
            StaticObjects.ShowStatusMessage += Alternative_ShowStatusMessage;

            EventsControl.EntryEdited += AddEntryEdited;
            EventsControl.ShowMessage += ShowMessage;

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
            comboBoxDocsNoTranslation.Text = StaticObjects.Parameters.LastGTPPaper.ToString();
            numericUpDownStatusPaperNo.Value = Convert.ToDecimal(StaticObjects.Parameters.LastDocumentToChangeStatus);
            comboBoxDoc.Text = StaticObjects.Parameters.LastDocumentToRecover.ToString();

            comboBoxGrepCommands.DisplayMember = "Name";
            comboBoxGrepCommands.ValueMember = "Command";
            comboBoxGrepCommands.DataSource = grepMarkdown.Commands;


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
                case "geral":
                    tx = txGeralMessages;
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
            Application.DoEvents();
        }

        private void ShowPaperNumber(short paperNo)
        {
            toolStripStatusLabelPaperNumber.Text = paperNo.ToString();
            Application.DoEvents();
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
            tubPT_BR.Print(StaticObjects.Book.EnglishTranslation,
                           StaticObjects.Book.WorkTranslation,
                           StaticObjects.Book.EditTranslation);
        }

        private void ExportPaper(short paperEditNo)
        {
            HtmlFormat_PTalternative formatter = new HtmlFormat_PTalternative(StaticObjects.Parameters);
            PtBr_Website tubPT_BR = new PtBr_Website(StaticObjects.Parameters, formatter);

            tubPT_BR.Print(StaticObjects.Book.EnglishTranslation,
                          StaticObjects.Book.WorkTranslation,
                          StaticObjects.Book.EditTranslation,
                          paperEditNo);
        }


        private void btPTAlternativeGenerate_Click(object sender, EventArgs e)
        {
            ShowMessage(null);
            ShowMessage($"Repositório para o texto em páginas html: {StaticObjects.Parameters.EditBookRepositoryFolder}");
            if (cbGerarIndicesArtigos.Checked)
            {
                ShowMessage("Gerando páginas de índices e artigos html");
                HtmlFormat_PtAlternative_Indexes indexes = new HtmlFormat_PtAlternative_Indexes();
                indexes.PrintAll();
                //indexes.PrintIndexStudy();
                ShowMessage("Páginas índice e artigos gerados");
                Articles articles = new Articles();
                articles.Process(StaticObjects.Parameters.EditBookRepositoryFolder);
            }


            if (cbGerarTocTable.Checked)
            {
                // TOC Table not forced
                ShowMessage("Criando Tabela de Conteúdos");
                List<TUB_TOC_Entry> tocEntries = StaticObjects.Book.EditTranslation.GetTranslation_TOC_Table(true);
                TUB_TOC_Html toc_table = new TUB_TOC_Html(StaticObjects.Parameters, tocEntries);
                string pathTocTable = Path.Combine(StaticObjects.Parameters.EditBookRepositoryFolder, @"content\TocTable.html");
                toc_table.Html(pathTocTable);
                ShowMessage("Tabela de conteúdo gerada");
            }

            if (chGerarTexto.Checked)
            {
                if (MessageBox.Show($"Are you sure to generate all pages for rogreis.github.io into {StaticObjects.Parameters.EditBookRepositoryFolder}?",
                            "Confirmation",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                ShowMessage("Gerando rogreis.github.io pages");
                ExportAllPapers();
                ShowMessage("Finished");
            }
            Process.Start("chrome.exe", "localhost");

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

            short startDoc = 99;
            short endDoc = 113;
            for (short paperNo = startDoc; paperNo < endDoc; paperNo++)
            {
                paperSet.Add(paperNo);
            }


            //paperSet.Add(5);
            //paperSet.Add(143);
            //paperSet.Add(144);

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


            try
            {
                ShowMessage("Exporting PtAlternative...");
                PTAlternative alternative = new PTAlternative(StaticObjects.Parameters);
                alternative.ExportToAmadon();
                // Verify respository existence
                if (!DataInitializer.ReInicializeTranslations())
                {
                    return;
                }

                // Copia para o diretório de dados do Amadon
                ShowMessage("Enviando TR002 para dados do Amadon...");
                string origem = Path.Combine(StaticObjects.Parameters.TUB_Files_RepositoryFolder, "TR002.gz");
                string destino = @"C:\ProgramData\Amadon\TUB_Files\TR002.gz";  // Diret+orio local de dados do Amadon
                File.Copy(origem, destino, true);

                ShowMessage("Enviando AvailableTranslations para dados do Amadon...");
                //Translation trans = StaticObjects.Book.Translations.Find(t => t.LanguageID == StaticObjects.Parameters.EditTranslationId);
                //trans.Description = $"PT-BR {DateTime.Now:dd-MM-yy}";
                StaticObjects.Book.EditTranslation.Description = $"PT-BR {DateTime.Now:dd-MM-yy}";
                DataInitializer.StoreTranslationsList();
                origem = Path.Combine(StaticObjects.Parameters.TUB_Files_RepositoryFolder, "AvailableTranslations.json");
                destino = @"C:\ProgramData\Amadon\AvailableTranslations.json";  // Diretório local de dados do Amadon
                File.Copy(origem, destino, true);

                // 

                // Delete each file
                string pathAmandonLuceneData = @"C:\ProgramData\Amadon\TubSearch\T002";
                ShowMessage($"Deletando todos os arquivos para busca Lucene: {pathAmandonLuceneData}");

                foreach (string file in Directory.GetFiles(pathAmandonLuceneData))
                {
                    File.Delete(file);
                }


                ShowMessage("Finished.");
            }
            catch (Exception ex)
            {
                ShowMessage("Erro: Exporting PtAlternative..." + ex.Message);
            }
        }

        private string ubDatabasePath = @"Y:\home\r\tools\amadonweb\data\UbData.db";
        private string ufEnglishMarkdownPAth = @"C:\Urantia\Textos\UF-ENG-001-1955-1.22.md";


        private void btAmadonWebGenerator_Click(object sender, EventArgs e)
        {
            ShowMessage(null);
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            StaticObjects.Parameters.EditBookRepositoryFolder = txEditBookRepositoryFolder.Text;
            ShowMessage($"Repositório para o texto em páginas html: {StaticObjects.Parameters.EditBookRepositoryFolder}");
            ShowMessage($"Repositório com o texto PT-BR: {StaticObjects.Parameters.EditParagraphsRepositoryFolder}");

            if (checkBoxImportEnglish.Checked)
            {
                Import_English import_English = new Import_English();
                import_English.Run(ufEnglishMarkdownPAth, ubDatabasePath);
            }
            if (checkBoxImportPtBr.Checked)
            {
                Import_PtBr import_PtBr = new Import_PtBr();
                import_PtBr.Run(StaticObjects.Parameters.EditParagraphsRepositoryFolder, ubDatabasePath);
            }

            if (chGerarTexto.Checked)
            {
                Export_HtmlBilingual export= new Export_HtmlBilingual();
                export.Run(StaticObjects.Parameters.EditBookRepositoryFolder, ubDatabasePath);
            }


        }

        /// <summary>
        /// Gera a string para efeturar buscas na página rogreis.github.io
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btGenerateSearchString_Click(object sender, EventArgs e)
        {
            //ShowMessage(null);
            //ShowMessage($"Repositório para o texto em páginas html: {StaticObjects.Parameters.EditBookRepositoryFolder}");
            //ShowMessage($"Repositório com o texto PT-BR: {StaticObjects.Parameters.EditParagraphsRepositoryFolder}");
            //if (MessageBox.Show($"Are you sure to generate the search string for rogreis.github.io into {StaticObjects.Parameters.EditBookRepositoryFolder}?",
            //            "Confirmation",
            //            MessageBoxButtons.YesNo,
            //            MessageBoxIcon.Question) == DialogResult.No)
            //{
            //    return;
            //}
            //Export_HtmlBilingual export = new Export_HtmlBilingual();
            //export.Run();
            //ShowMessage("Finished");
        }

        private void btSemanticSearchString_Click(object sender, EventArgs e)
        {
            ShowMessage(null);
            ShowMessage($"Repositório para o texto em páginas html: {StaticObjects.Parameters.EditBookRepositoryFolder}");
            ShowMessage($"Repositório com o texto PT-BR: {StaticObjects.Parameters.EditParagraphsRepositoryFolder}");
            if (MessageBox.Show($"Are you sure to generate the search string for rogreis.github.io into {StaticObjects.Parameters.EditBookRepositoryFolder}?",
                        "Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            Export_SemanticQuery export = new Export_SemanticQuery();
            export.Run("", ubDatabasePath); // TODO definir onde serão salvos os dados
            ShowMessage("Finished");
        }

        private void btPaperShow_Click(object sender, EventArgs e)
        {
            ShowMessage(null);
            short paperNo = Convert.ToInt16(numericUpDownPaper.Value);
            Export_Excel export= new Export_Excel();
            export.Run(paperNo.ToString(), ubDatabasePath);
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

            //PaperCheckingUsingChatGPT gpt = new PaperCheckingUsingChatGPT();
            //short paperNo = 100;
            //ShowMessage($"Generating Compare for paper {paperNo}");


            //ShowMessage("Finished.");
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
            if (message == null)
            {
                txGeralMessages.Text = "";
                return;
            }
            txGeralMessages.AppendText(message + Environment.NewLine);
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

        class DataMoral
        {
            public string Texto { get; set; }
            public string Referencia { get; set; }
            public DataMoral(string texto, string referencia)
            {
                Texto = texto;
                Referencia = referencia;
            }

            public override string ToString()
            {
                TOC_Entry entry = TOC_Entry.FromHref(Referencia);
                string markdownLink = $"**<a href=\"javascript:showParagraph({entry.Paper},{entry.Section},{entry.ParagraphNo})\" title=\"Abrir o parágrafo {entry.Ident}\">{entry.Ident}</a>**";
                return $"{Texto} - {markdownLink}";
            }
        }

        private List<DataMoral> Data()
        {
            List<DataMoral> list = new List<DataMoral>();

            list.Add(new DataMoral("duty man's consciousness of a value level", "0:2.1"));
            list.Add(new DataMoral("personality of and Universal Father", "0:3.19"));
            list.Add(new DataMoral("being(s) fragment of God indwells", "1:4.3"));
            list.Add(new DataMoral("affinity an essential to friendship", "1:6.5"));
            list.Add(new DataMoral("God the source of", "2:6.5"));
            list.Add(new DataMoral("status a factor in Adjuster communion", "5:2.6"));
            list.Add(new DataMoral("God as a vindicator of", "5:4.11"));
            list.Add(new DataMoral("Zoroastrianism a religion of", "5:4.7"));
            list.Add(new DataMoral("situations choices necessitated by", "5:5.1"));
            list.Add(new DataMoral("relation of to religion", "5:5.4"));
            list.Add(new DataMoral("thinking social service based on", "5:5.4"));
            list.Add(new DataMoral("and philosophical satisfaction secured from religious experience", "5:5.9"));
            list.Add(new DataMoral("duty man's consciousness of a value level", "16:6.10"));
            list.Add(new DataMoral("virtue and personality", "16:7.0"));
            list.Add(new DataMoral("nature character of the", "16:7.1"));
            list.Add(new DataMoral("acts definition", "16:7.10"));
            list.Add(new DataMoral("function of insight of", "16:7.4"));
            list.Add(new DataMoral("evidence of possession of", "16:7.6"));
            list.Add(new DataMoral("keenness of a factor in choosing between good and evil", "16:7.7"));
            list.Add(new DataMoral("values a definition", "16:9.11"));
            list.Add(new DataMoral("power of a characteristic of will", "36:3.8"));
            list.Add(new DataMoral("stamina intellectual root of", "36:5.8"));
            list.Add(new DataMoral("choice the highest definition", "39:4.14"));
            list.Add(new DataMoral("will definition", "52:1.6"));
            list.Add(new DataMoral("insight a part of the ancestry of true religion", "52:6.7"));
            list.Add(new DataMoral("satisfaction a fruit of divinity", "56:10.20"));
            list.Add(new DataMoral("Hap's", "66:7.8"));
            list.Add(new DataMoral("affairs supremacy of man's free will in", "66:8.6"));
            list.Add(new DataMoral("steadfastness basis of Van's", "67:3.6"));
            list.Add(new DataMoral("guilt personal nature of sin as to", "67:7.7"));
            list.Add(new DataMoral("law of Eden vs. seven commandments of Dalamatia", "74:7.20"));
            list.Add(new DataMoral("standard-bearer woman the", "84:6.4"));
            list.Add(new DataMoral("growth and a cult's survival", "87:7.10"));
            list.Add(new DataMoral("dignity man's final acquirement of", "89:8.6"));
            list.Add(new DataMoral("progress and prayer", "91:1.2"));
            list.Add(new DataMoral("progress and prayer", "91:6.3"));
            list.Add(new DataMoral("culture early development of in Egypt", "95:3.1"));
            list.Add(new DataMoral("evolution nondependence of on revelation", "95:3.2"));
            list.Add(new DataMoral("the Jewish religion's conservation of", "97:10.5"));
            list.Add(new DataMoral("depravity of the Greeks under the mystery cults", "98:2.11"));
            list.Add(new DataMoral("stability religion's necessary influence for in the changing world", "99:1.2"));
            list.Add(new DataMoral("insight a part of the ancestry of true religion", "101:1.6"));
            list.Add(new DataMoral("the persistence of the", "101:3.1"));
            list.Add(new DataMoral("spiritual faith", "101:3.4"));
            list.Add(new DataMoral("trends influence of on man's philosophy of religion", "101:7.1"));
            list.Add(new DataMoral("consciousness a definition", "101:9.5"));
            list.Add(new DataMoral("man's importance of", "103:2.3"));
            list.Add(new DataMoral("impulses emergence of the arrival of the Thought Adjuster and the", "103:2.5"));
            list.Add(new DataMoral("importance of the creature's ability to make", "103:2.7"));
            list.Add(new DataMoral("vs. magic in the evolution of religion", "103:3.4"));
            list.Add(new DataMoral("and social meanings paramount religious experience is the feeling regarding", "103:3.4"));
            list.Add(new DataMoral("ideals man's highest comparison of with the will of God", "103:4.3"));
            list.Add(new DataMoral("importance of the creature's ability to make", "108:2.2"));
            list.Add(new DataMoral("decision Adjuster's inability to invade a mortal mind prior to its making a", "108:2.4"));
            list.Add(new DataMoral("choosing(s) human's effect of on Adjuster function", "110:6.5"));
            list.Add(new DataMoral("attitude of Paul's Christianity toward", "121:5.17"));
            list.Add(new DataMoral("Jesus' first significance of", "123:2.1"));
            list.Add(new DataMoral("nonnecessity of the actuality of evil to", "132:2.10"));
            list.Add(new DataMoral("importance of the creature's ability to make", "133:6.5"));
            list.Add(new DataMoral("the soul's dependence on", "133:6.5"));
            list.Add(new DataMoral("possibility of modification of the", "140:4.8"));
            list.Add(new DataMoral("reformer Jesus not a", "140:8.21"));
            list.Add(new DataMoral("the measure of", "143:2.3"));
            list.Add(new DataMoral("level of the golden rule", "147:4.8"));
            list.Add(new DataMoral("immaturity evidences of", "160:1.6"));
            list.Add(new DataMoral("conduct basis of", "170:2.18"));
            list.Add(new DataMoral("the kingdom of heaven as a new standard of", "170:2.4"));
            list.Add(new DataMoral("unfolding means of", "170:3.10"));
            list.Add(new DataMoral("conduct basis of", "170:3.7"));
            list.Add(new DataMoral("human Christianity's higher influence on", "195:0.14"));
            list.Add(new DataMoral("attitude of Paul's Christianity toward", "195:0.3"));
            list.Add(new DataMoral("of Rome Christianity the", "195:2.5"));
            list.Add(new DataMoral("decline of Rome Christianity's arrival too late to prevent the", "195:3.8"));
            list.Add(new DataMoral("convictions reality and certainty of", "195:6.12"));
            list.Add(new DataMoral("discipline vs. self-gratification and education", "195:10.17"));
            list.Add(new DataMoral("impossibility of measuring", "196:3.18"));
            list.Add(new DataMoral("importance of the creature's ability to make", "196:3.20"));
            list.Add(new DataMoral("nature character of the", "196:3.25"));
            list.Add(new DataMoral("influence of religion on", "196:3.27"));
            list.Add(new DataMoral("relation of religion and human experience to", "196:3.28"));

            int i = 0;
            txPTalternative.Text = "";
            foreach (DataMoral data in list)
            {
                i++;
                txPTalternative.Text += $"{i}. {data.ToString()}" + Environment.NewLine;
            }
            return list;
        }

        private void btGeraLink_Click(object sender, EventArgs e)
        {

            TOC_Entry entry = TOC_Entry.FromHref(txEntryForLink.Text);
            // string markdownLink = $"[{entry.Href}](javascript:loadDoc('content/Doc{entry.Paper:000}.html','p{entry.Paper:000}_{entry.Section:000}_{entry.ParagraphNo:000}'))";
            //string markdownLink = $"LINKTO{entry.Paper:000}{entry.Section:000}{entry.ParagraphNo:000}";
            string markdownLink = $"**<a href=\"javascript:showParagraph({entry.Paper},{entry.Section},{entry.ParagraphNo})\" title=\"Abrir o parágrafo {entry.Ident}\">{entry.Ident}</a>**";
            txBoxLinkForArticles.Text = markdownLink;
            Clipboard.SetText(markdownLink);
            txBoxLinkForArticles.SelectAll();
        }


        // <a class="liIndex" href="">1. O Nome do Pai</a> 

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

        private void btEditVerDocumento_Click(object sender, EventArgs e)
        {
            short paperNo = Convert.ToInt16(comboBoxDoc.Text);
            if (paperNo == -1) return;
            StaticObjects.Parameters.TUB_Files_RepositoryFolder = txRepositoryOutputFolder.Text;
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            StaticObjects.Parameters.EditBookRepositoryFolder = txEditBookRepositoryFolder.Text;
            StaticObjects.Parameters.LastDocumentToRecover = paperNo;
            TOC_Entry entry = new TOC_Entry(0, paperNo, 0, 1, 0, 0);

            ExportPaper(paperNo);
            ShowMessage($"Paper {paperNo} exported");
            string url = $"http://localhost/indexToc.html#p{paperNo:000}_000_000";
            Process.Start("chrome.exe", url);
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
            string subjectIndexSelected = listBoxSubjects.SelectedItems[0].ToString();
            SubjectIndex subjectIndex = urantiaIndex.GetSubjectIndex(subjectIndexSelected);
        }

        #endregion

        private void PrintSummaryParagraphs(string basePath, StringBuilder sb, short paperNo)
        {
            var mdParagraphs = Directory.GetFiles(basePath, $"Doc{paperNo:000}\\Par_{paperNo:000}_*.md");
            string mdPath = Path.Combine(basePath, $"Doc{paperNo:000}\\Par_{paperNo:000}_000_000.md");
            string titleFile = File.ReadAllText(mdPath);
            sb.AppendLine("");
            sb.AppendLine($"## {titleFile}");
            short lastSectionNo = -1;
            foreach (string mdFilePath in mdParagraphs)
            {
                string numbersString = mdFilePath.Substring(4, mdFilePath.Length - 7); // Remove "Par_" and ".md"
                string[] numbers = numbersString.Split('_');
                short sectionNo = short.Parse(numbers[2]);
                if (lastSectionNo != sectionNo)
                {
                    lastSectionNo = sectionNo;
                    sb.AppendLine("");
                    titleFile = File.ReadAllText(Path.Combine(basePath, $"Doc{paperNo:000}\\Par_{paperNo:000}_{sectionNo:000}_000.md"));
                    sb.AppendLine($"### {titleFile}");
                    sb.AppendLine("");
                }
                short paragraphNo = short.Parse(numbers[3]);
                TOC_Entry entry = new TOC_Entry(0, paperNo, sectionNo, paragraphNo, 0, 0);
                sb.Append($" [{entry.Ident}](Doc{paperNo:000}\\Par_{paperNo:000}_{sectionNo:000}_000.md)");
            }
        }

        public static void TestRegex(string text, Regex regex, string description)
        {
            bool result = regex.IsMatch(text);
            StaticObjects.FireShowMessage($"{description}: \"{text}\" => {result} ({regex})");
        }

        public void TestRegexMatches(string text, Regex regex, string description)
        {
            MatchCollection matches = regex.Matches(text);

            if (matches.Count > 0)
            {
                StaticObjects.FireShowMessage($"  Found {matches.Count} matches:");
                foreach (Match match in matches)
                {
                    StaticObjects.FireShowMessage($"  Match: \"{match.Value}\" (Index: {match.Index}, Length: {match.Length})");
                    if (match.Groups.Count > 1) // Group 0 is the entire match
                    {
                        StaticObjects.FireShowMessage("    Groups:");
                        for (int i = 0; i < match.Groups.Count; i++)
                        {
                            Group group = match.Groups[i];
                            StaticObjects.FireShowMessage($"      Group {i}: \"{group.Value}\" (Index: {group.Index}, Length: {group.Length})");
                        }
                    }
                    else
                    {
                        StaticObjects.FireShowMessage("    No capturing groups defined in the regex.");
                    }
                    StaticObjects.FireShowMessage("  ---");
                }
            }
            else
            {
                StaticObjects.FireShowMessage("  No matches found.");
            }
            StaticObjects.FireShowMessage($"  Regex: {regex}");
            StaticObjects.FireShowMessage("---");
        }

        private void ProcessMarkdownFiles(string directory)
        {
            tabControlMain.Enabled = true;
            tabControlMain.SelectedTab = tabPageEditTranslation;

            Import_English import_English = new Import_English();
            import_English.Run(ufEnglishMarkdownPAth, ubDatabasePath);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowMessage(null);
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            StaticObjects.Parameters.EditBookRepositoryFolder = txEditBookRepositoryFolder.Text;
            StaticObjects.Parameters.EditParagraphsRepositoryFolder = txTranslationRepositoryFolder.Text;
            ProcessMarkdownFiles(@"C:\Trabalho\Github\Rogerio\p");

            //Export_SimpleQuery export= new Export_SimpleQuery();
            //string queryDataPtBr = Path.Combine(StaticObjects.Parameters.EditBookRepositoryFolder, @"query\qsd_ptbr.zip");
            //string idxDataPtBr = Path.Combine(StaticObjects.Parameters.EditBookRepositoryFolder, @"query\idx_ptbr.zip");

            //Tuple<string, string> tuple= export.Import(queryDataPtBr, idxDataPtBr);

            //// Test strings
            //string text1 = "Eterno Sustentador";
            //string text2 = "sustentador Eterno";
            //string text3 = "Eterno";
            //string text4 = "Sustentador";
            //string text5 = "eterno sustentador";
            //string text6 = "ETERNO SUSTENTADOR";
            //string text7 = "Outra palavra";
            //string text8 = "Sustentador de algo";
            //string text9 = "Algo Eterno";
            //string text10 = "Sustentador Sustentador";
            //string text11 = "Eterno Eterno";

            ////StaticObjects.FireSendMessage("--- Case Sensitive ---");
            ////// 1 - both words mandatory case sensitive
            ////Regex regex1 = new Regex("Eterno.*Sustentador|Sustentador.*Eterno");
            ////TestRegex(text1, regex1, "1 - Ambos obrigatórios (case sensitive)");
            ////TestRegex(text2, regex1, "1 - Ambos obrigatórios (case sensitive)");
            ////TestRegex(text3, regex1, "1 - Ambos obrigatórios (case sensitive)");
            ////TestRegex(text4, regex1, "1 - Ambos obrigatórios (case sensitive)");
            ////TestRegex(text5, regex1, "1 - Ambos obrigatórios (case sensitive)");
            ////TestRegex(text6, regex1, "1 - Ambos obrigatórios (case sensitive)");
            ////TestRegex(text7, regex1, "1 - Ambos obrigatórios (case sensitive)");

            //StaticObjects.FireSendMessage("\n--- Case Insensitive ---");
            //// 2 - both words mandatory case insensitive
            //Regex regex2 = new Regex("eterno.*sustentador|sustentador.*eterno", RegexOptions.IgnoreCase);
            //TestRegexMatches(tuple.Item1, regex2, "2 - Ambos obrigatórios (case insensitive)");

            ////StaticObjects.FireSendMessage("\n--- One or Other (Optional) ---");
            ////// 3 - one or other word, optional
            ////Regex regex3 = new Regex("Eterno|Sustentador", RegexOptions.IgnoreCase);
            ////TestRegex(text1, regex3, "3 - Um ou outro (opcional)");
            ////TestRegex(text2, regex3, "3 - Um ou outro (opcional)");
            ////TestRegex(text3, regex3, "3 - Um ou outro (opcional)");
            ////TestRegex(text4, regex3, "3 - Um ou outro (opcional)");
            ////TestRegex(text5, regex3, "3 - Um ou outro (opcional)");
            ////TestRegex(text6, regex3, "3 - Um ou outro (opcional)");
            ////TestRegex(text7, regex3, "3 - Um ou outro (opcional)");

            ////StaticObjects.FireSendMessage("\n--- Sustentador occurs and Eterno not ---");
            ////// 4 - sustentador occurs and Eterno not
            ////Regex regex4 = new Regex("^(?!.*Eterno).*Sustentador.*$", RegexOptions.IgnoreCase);
            ////TestRegex(text1, regex4, "4 - Sustentador ocorre e Eterno não");
            ////TestRegex(text2, regex4, "4 - Sustentador ocorre e Eterno não");
            ////TestRegex(text3, regex4, "4 - Sustentador ocorre e Eterno não");
            ////TestRegex(text4, regex4, "4 - Sustentador ocorre e Eterno não");
            ////TestRegex(text5, regex4, "4 - Sustentador ocorre e Eterno não");
            ////TestRegex(text6, regex4, "4 - Sustentador ocorre e Eterno não");
            ////TestRegex(text7, regex4, "4 - Sustentador ocorre e Eterno não");
            ////TestRegex(text8, regex4, "4 - Sustentador ocorre e Eterno não");
            ////TestRegex(text9, regex4, "4 - Sustentador ocorre e Eterno não");
            ////TestRegex(text10, regex4, "4 - Sustentador ocorre e Eterno não");
            ////TestRegex(text11, regex4, "4 - Sustentador ocorre e Eterno não");

        }



        private void btEditSearch_Click(object sender, EventArgs e)
        {
            if (comboBoxGrepCommands.Text.Trim().Length == 0) return;
            string command = ((GrepCommand)comboBoxGrepCommands.SelectedItem).Command;
            if (string.IsNullOrEmpty(command)) return;

            List<ParagraphsMdFound> resultList =
                grepMarkdown.ExecuteGrepCommand(StaticObjects.Parameters.EditParagraphsRepositoryFolder, command);
            listBoxEditSearchResults.DataSource = resultList;
        }

        private void listBoxEditSearchResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            TOC_Entry entry = TOC_Entry.FromHref(listBoxEditSearchResults.SelectedItem.ToString());
            OpenEditForms(entry);

        }

        private void btMoral_Click(object sender, EventArgs e)
        {
            ArticlesFromExcel articles = new ArticlesFromExcel();
            articles.ShowMessageEvent += ShowParamonyMessage;
            articles.UniqueTable();
        }

        private void btArtigoPorGrupo_Click(object sender, EventArgs e)
        {
            ArticlesFromExcel articles = new ArticlesFromExcel();
            articles.ShowMessageEvent += ShowParamonyMessage;
            articles.ByGroups();
        }
    }
}
