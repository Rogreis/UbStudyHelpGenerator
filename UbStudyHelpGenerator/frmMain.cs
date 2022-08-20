using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using UbStandardObjects;
using UbStandardObjects.Objects;
using UbStudyHelpGenerator.Classes;
using UbStudyHelpGenerator.Database;
using UbStudyHelpGenerator.Properties;
using static System.Environment;
using Application = System.Windows.Forms.Application;

namespace UbStudyHelpGenerator
{
    public partial class frmMain : Form
    {
        private bool StillStarting = true;

        private UbtDatabaseSqlServer Server = new UbtDatabaseSqlServer();

        private GetDataFilesGenerator getDataFilesGenerator = null;


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
            UbStandardObjects.StaticObjects.Logger.ShowMessage += Logger_ShowMessage;
            string appFolder = Application.StartupPath;
            string localStorageFolder = MakeProgramDataFolder("TUB_Files");
            getDataFilesGenerator = new GetDataFilesGenerator(Server, appFolder, localStorageFolder);
            getDataFilesGenerator.ShowMessage += Logger_ShowMessage;
            getDataFilesGenerator.ShowPaperNumber += ShowPaperNumber;
            StaticObjects.Book.Inicialize(getDataFilesGenerator);
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            txHtmlFilesPath.Text = UbStandardObjects.StaticObjects.Parameters.InputHtmlFilesPath;
            txRepositoryOutputFolder.Text = UbStandardObjects.StaticObjects.Parameters.RepositoryOutputFolder;
            txPRAlternativeFolder.Text = UbStandardObjects.StaticObjects.Parameters.RepositoryOutputPTAlternativeFolder;
            txSqlServerConnectionString.Text = Settings.Default.SqlServerConnectionString;
            txQuery.Text = ((ParametersGenerator)UbStandardObjects.StaticObjects.Parameters).SqlEdit;
            StillStarting = false;
        }


        /// <summary>
        /// Show a message in the visible textbox
        /// </summary>
        /// <param name="message"></param>
        private void ShowMessage(string message)
        {
            TextBox tx = null;
            switch (tabControlMain.SelectedIndex)
            {
                case 0:
                    tx = textBoxFromSqlServer;
                    break;
                case 1:
                    tx = textBoxFromHtml;
                    break;
                case 2:
                    tx = txUbIndexMessages;
                    break;
                case 3:
                    tx = txPTalternative;
                    break;
                case 4:
                    tx = txPTalternative;
                    break;
                default:
                    tx = textBoxFromHtml;
                    break;
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
            browserDialog.SelectedPath = UbStandardObjects.StaticObjects.Parameters.InputHtmlFilesPath;
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
            string folder = UbStandardObjects.StaticObjects.Parameters.RepositoryOutputFolder;
            GetFolder(txRepositoryOutputFolder, ref folder);
            UbStandardObjects.StaticObjects.Parameters.RepositoryOutputFolder = folder;
        }

        private void btHtmlFilesPath_Click(object sender, EventArgs e)
        {
            string folder = UbStandardObjects.StaticObjects.Parameters.InputHtmlFilesPath;
            GetFolder(txHtmlFilesPath, ref folder);
            UbStandardObjects.StaticObjects.Parameters.InputHtmlFilesPath = folder;
        }

        private void btPTALternativeFolder_Click(object sender, EventArgs e)
        {
            string folder = UbStandardObjects.StaticObjects.Parameters.RepositoryOutputPTAlternativeFolder;
            GetFolder(txPRAlternativeFolder, ref folder);
            UbStandardObjects.StaticObjects.Parameters.RepositoryOutputPTAlternativeFolder = folder;
        }

        private void btUfIndexDownloadedFiles_Click(object sender, EventArgs e)
        {
            string folder = UbStandardObjects.StaticObjects.Parameters.IndexDownloadedFiles;
            GetFolder(txUfIndexDownloadeFiles, ref folder);
            UbStandardObjects.StaticObjects.Parameters.IndexDownloadedFiles = folder;
        }

        private void btUfIndexOutputFiles_Click(object sender, EventArgs e)
        {
            string folder = UbStandardObjects.StaticObjects.Parameters.IndexOutputFilesPath;
            GetFolder(txUfIndexOutputFolder, ref folder);
            UbStandardObjects.StaticObjects.Parameters.IndexOutputFilesPath = folder;
        }


        #endregion


        private void btSpanish_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UbStandardObjects.StaticObjects.Parameters.InputHtmlFilesPath))
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
                spanish.ProcessFiles(Server, UbStandardObjects.StaticObjects.Parameters.InputHtmlFilesPath);
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
            if (string.IsNullOrWhiteSpace(UbStandardObjects.StaticObjects.Parameters.InputHtmlFilesPath))
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
                spanish.ProcessFiles(Server, UbStandardObjects.StaticObjects.Parameters.InputHtmlFilesPath);
            }


        }

        private void txSqlServerConnectionString_TextChanged(object sender, EventArgs e)
        {
            if (StillStarting)
                return;
            UbStandardObjects.StaticObjects.Parameters.SqlServerConnectionString = txSqlServerConnectionString.Text;
        }

        private void txRepositoryOutputFolder_TextChanged(object sender, EventArgs e)
        {
            if (StillStarting)
                return;
            UbStandardObjects.StaticObjects.Parameters.RepositoryOutputFolder = txRepositoryOutputFolder.Text;
        }



        private void btGenerateFromSql_Click(object sender, EventArgs e)
        {

            ShowMessage(null);
            ShowMessage("Getting all Ok for Use translation from database...");

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
                string pathRepositoryJson = Path.Combine(UbStandardObjects.StaticObjects.Parameters.RepositoryOutputFolder, fileNameWithoutExtension + ".json");
                string pathRepositoryZipped = Path.Combine(UbStandardObjects.StaticObjects.Parameters.RepositoryOutputFolder, fileNameWithoutExtension + ".gz");
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
                UbStandardObjects.StaticObjects.Logger.Error($"Exporting translation {translation}", ex);
            }
        }

        private void btGetTranslation_Click(object sender, EventArgs e)
        {
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
            //openFileDialog.InitialDirectory = UbStandardObjects.StaticObjects.Parameters.RepositoryOutputFolder;
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
            // Delete also the application
            string appFolder = UbStandardObjects.StaticObjects.Parameters.RepositoryOutputFolder;
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
            if (MessageBox.Show("Are you sure to generate UbStudyHelp format from PtAlternative repository?",
                        "Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            UbStandardObjects.StaticObjects.Parameters.RepositoryOutputPTAlternativeFolder = txPRAlternativeFolder.Text;
            PTAlternative alternative = new PTAlternative();
            alternative.ShowMessage += Logger_ShowMessage;
            alternative.ShowPaperNumber += ShowPaperNumber;
            alternative.ShowStatusMessage += Alternative_ShowStatusMessage;

            alternative.ImportFromRepositoryToTUB_Files();
        }

        private void btPtRepository_Click(object sender, EventArgs e)
        {
            UbStandardObjects.StaticObjects.Parameters.RepositoryOutputPTAlternativeFolder = txPRAlternativeFolder.Text;
            PTAlternative alternative = new PTAlternative();
            alternative.ShowMessage += Logger_ShowMessage;
            alternative.ShowPaperNumber += ShowPaperNumber;
            alternative.ShowStatusMessage += Alternative_ShowStatusMessage;
            alternative.ExportFromDatabaseToRepository();
        }

        private void btRecordChanged_Click(object sender, EventArgs e)
        {
            UbStandardObjects.StaticObjects.Parameters.RepositoryOutputPTAlternativeFolder = txPRAlternativeFolder.Text;
            PTAlternative alternative = new PTAlternative();
            alternative.ShowMessage += Logger_ShowMessage;
            alternative.ShowPaperNumber += ShowPaperNumber;
            alternative.ShowStatusMessage += Alternative_ShowStatusMessage;
            alternative.ExportRecordsChangedFromDatabase_Temp();
        }


        private void btExportPtAlternativeDocx_Click(object sender, EventArgs e)
        {
            UbStandardObjects.StaticObjects.Parameters.RepositoryOutputPTAlternativeFolder = txPRAlternativeFolder.Text;
            PTAlternative alternative = new PTAlternative();
            alternative.ShowMessage += Logger_ShowMessage;
            alternative.ShowPaperNumber += ShowPaperNumber;
            alternative.ShowStatusMessage += Alternative_ShowStatusMessage;
            alternative.ExportToDocx();
        }

        private void btImportDocx_Click(object sender, EventArgs e)
        {
            UbStandardObjects.StaticObjects.Parameters.RepositoryOutputPTAlternativeFolder = txPRAlternativeFolder.Text;
            PTAlternative alternative = new PTAlternative();
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



        UbtDatabaseSqlServer server = new UbtDatabaseSqlServer();
        System.Data.DataTable table = null;
        int rowNo = 0;

        private void FillFileds(ref int rowNo)
        {
            System.Data.DataRow row = table.Rows[rowNo];
            txEnglish.Text = ""; // row["English"].ToString();
            txPortuguese.Text = row["Text"].ToString();
            lblMessage.Text = $"{row["Identity"]}   {rowNo}-{table.Rows.Count}";
        }

        private void btFirst_Click(object sender, EventArgs e)
        {
            string sql = txQuery.Text;
            table = server.Query(sql);
            rowNo = 0;
            if (table != null && table.Rows.Count > 0)
            {
                System.Data.DataRow row = table.Rows[rowNo];
                FillFileds(ref rowNo);
                lblMessage.Text += " Starting";
            }
            else
            {
                lblMessage.Text = "No more records";
            }
            ((ParametersGenerator)UbStandardObjects.StaticObjects.Parameters).SqlEdit = sql;
        }

        private void btNext_Click(object sender, EventArgs e)
        {
            if (table == null)
                return;
            if (table == null)
                return;
            if (table.Rows.Count == 0) return;

            rowNo++;
            if (rowNo > table.Rows.Count - 1) rowNo = 0;
            FillFileds(ref rowNo);
            lblMessage.Text += " Next";
        }

        private void btPrevious_Click(object sender, EventArgs e)
        {
            if (table == null)
                return;
            if (table == null)
                return;
            if (table.Rows.Count == 0) return;

            rowNo--;
            if (rowNo < 0) rowNo = table.Rows.Count - 1;
            FillFileds(ref rowNo);
            lblMessage.Text += " Previous";
        }

        private void btStore_Click(object sender, EventArgs e)
        {
            if (table == null || table.Rows.Count == 0 || rowNo < 0)
                return;

            System.Data.DataRow row = table.Rows[rowNo];
            int IndexWorK = Convert.ToInt32(row["IndexWorK"]);
            // string sql = $"Update UB_Texts_Work set text= '{txPortuguese.Text}' where IndexWorK = {IndexWorK}";
            string sql = $"UPDATE [dbo].[CaioComments] SET [Notes] = '{txPortuguese.Text}', [Status] = 2 WHERE [IndexWorK] = {IndexWorK}";
            row["Text"] = txPortuguese.Text;
            server.RunCommand(sql);
            lblMessage.Text = "Stored";
            btNext_Click(null, null);
        }

        private void btSeleciona_Click(object sender, EventArgs e)
        {
            string text = txPortuguese.Text;
            if (!String.IsNullOrEmpty(text))
            {
                int ind = text.IndexOf("<br", 0, StringComparison.OrdinalIgnoreCase);
                if (ind >= 0)
                {
                    txPortuguese.SelectionStart = ind;
                    txPortuguese.SelectionLength = 4;
                    //string s = txPortuguese.SelectedText;
                }
            }

        }

        private string ReplaceAll(string input)
        {
            Regex.Replace(input, @"<br />+", Environment.NewLine);
            return Regex.Replace(input, @"<br>+", Environment.NewLine);
        }

        private void btMudaBr_Click(object sender, EventArgs e)
        {
            //txPortuguese.Text= ReplaceAll(text);
            txPortuguese.Text = txPortuguese.Text.Replace("<br>", Environment.NewLine);
            txPortuguese.Text = txPortuguese.Text.Replace("<br />", Environment.NewLine);
            txPortuguese.Text = txPortuguese.Text.Replace("<BR>", Environment.NewLine);
            txPortuguese.Text = txPortuguese.Text.Replace("<BR />", Environment.NewLine);
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            txPortuguese.Text = "";
            btStore_Click(null, null);
        }

        private void btExportJson_Click(object sender, EventArgs e)
        {
            string pathFile = Path.Combine(@"C:\Trabalho\Github\Rogerio\UbReviewer\UbReviewerLibrary\DataFiles", "ParagraphsNumber.json");
            string sql = txQuery.Text;
            string jsonString = server.GetJsonStringFromDatabase(sql);
            if (jsonString != null)
            {
                File.WriteAllText(pathFile, jsonString);
                lblMessage.Text = "Json saved";
            }
            else
            {
                lblMessage.Text = "Erro getting json";
            }
            ((ParametersGenerator)UbStandardObjects.StaticObjects.Parameters).SqlEdit = sql;
        }

    }
}
