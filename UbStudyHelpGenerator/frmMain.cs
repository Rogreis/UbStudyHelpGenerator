using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using UbStudyHelpGenerator.Classes;
using UbStudyHelpGenerator.Generators;
using UbStudyHelpGenerator.Generators.Classes;

namespace UbStudyHelpGenerator
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            EventsControl.ShowMessage += ShowMessage;
        }


        /// <summary>
        /// Show a message in the visible textbox
        /// </summary>
        /// <param name="message"></param>
        private void ShowMessage(string message)
        {
            TextBox tx = null;
            switch(tabControlMain.SelectedIndex)
            {
                case 0:
                    tx = textBoxFromHtml;
                    break;
                case 1:
                    tx = textBoxFromSqlServer;
                    break;
                case 2:
                    tx = txUbIndexMessages;
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
            Application.DoEvents();
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            txRepositoryOutputFolder.Text = Program.ParametersData.RepositoryOutputFolder;
            txHtmlFilesPath.Text = Program.ParametersData.InputHtmlFilesPath;
            txSqlConnectionString.Text = Program.ParametersData.SqlConnectionString;
            txUfIndexDownloadeFiles.Text = Program.ParametersData.IndexDownloadedFiles;

            tabControlMain.Enabled = !string.IsNullOrWhiteSpace(Program.ParametersData.RepositoryOutputFolder);
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
            browserDialog.SelectedPath = Program.ParametersData.InputHtmlFilesPath;
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
            string folder = Program.ParametersData.RepositoryOutputFolder;
            GetFolder(txRepositoryOutputFolder, ref folder);
            Program.ParametersData.RepositoryOutputFolder = folder;
            tabControlMain.Enabled = !string.IsNullOrWhiteSpace(Program.ParametersData.RepositoryOutputFolder);
        }

        private void btHtmlFilesPath_Click(object sender, EventArgs e)
        {
            string folder = Program.ParametersData.InputHtmlFilesPath;
            GetFolder(txHtmlFilesPath, ref folder);
            Program.ParametersData.InputHtmlFilesPath = folder;
        }


        private void btUfIndexDownloadedFiles_Click(object sender, EventArgs e)
        {
            string folder = Program.ParametersData.IndexDownloadedFiles;
            GetFolder(txUfIndexDownloadeFiles, ref folder);
            Program.ParametersData.IndexDownloadedFiles = folder;
        }

        private void btUfIndexOutputFiles_Click(object sender, EventArgs e)
        {
        }


        #endregion


        #region Spanish
        private void btSpanish_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Program.ParametersData.InputHtmlFilesPath))
            {
                MessageBox.Show($"Error: non exitinting folcer: {Program.ParametersData.InputHtmlFilesPath}");
                return;
            }

            if (MessageBox.Show("Are you sure to generate Spanish translation?",
                        "Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ShowMessage(null);
                GeneratorSpanish spanish = new GeneratorSpanish();
                spanish.Generate(Program.ParametersData.InputHtmlFilesPath, Program.ParametersData.RepositoryOutputFolder);
            }
        }

        private void btSpanishDownload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Program.ParametersData.InputHtmlFilesPath))
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
                GeneratorSpanish spanish = new GeneratorSpanish();
                spanish.Generate(Program.ParametersData.InputHtmlFilesPath, Program.ParametersData.RepositoryOutputFolder);
            }
        }
        #endregion


        private void btUfIndex_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure that want to regenerate the UB Index?",
                        "Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
            {
                GeneratorIndex index = new GeneratorIndex();
                index.Generate(Program.ParametersData.InputHtmlFilesPath, Program.ParametersData.RepositoryOutputFolder);
            }
        }

        private void btDownload_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to download and regenerate the UB Index?",
                        "Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ShowMessage(null);
                 GeneratorIndex index = new GeneratorIndex();
                index.Generate(Program.ParametersData.InputHtmlFilesPath, Program.ParametersData.RepositoryOutputFolder);
            }
        }

        #region Sql Server

        private void txSqlConnectionString_TextChanged(object sender, EventArgs e)
        {
            Program.ParametersData.SqlConnectionString = txSqlConnectionString.Text;
        }

        /// <summary>
        /// Load translations from sql server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btTranslationsFromSqlServer_Click(object sender, EventArgs e)
        {

        }



        private void btGenerateFromSql_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Program.ParametersData.SqlConnectionString))
            {
                MessageBox.Show("A connection string must be provided",
                        "Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                return;
            }

            GenerateFromDatabaseSqlServer generator = new GenerateFromDatabaseSqlServer(Program.ParametersData.SqlConnectionString);

            string xxx= generator.GetAvailableTranslations();

            List<Paragraph> list = new List<Paragraph>();
            generator.GetPaper(2, 1, ref list);

            // 
        }


        private void btLoadTranslations_Click(object sender, EventArgs e)
        {

        }

        private void btGenerateOneTranslation_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
