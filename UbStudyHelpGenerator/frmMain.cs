using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UbStudyHelpGenerator.Classes;

namespace UbStudyHelpGenerator
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
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


        private void btSpanish_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Program.ParametersData.InputHtmlFilesPath))
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
                spanish.ProcessFiles(Program.ParametersData.InputHtmlFilesPath);
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
                UrantiaSpanish spanish = new UrantiaSpanish();
                spanish.ShowMessage += ShowMessage;
                spanish.ProcessFiles(Program.ParametersData.InputHtmlFilesPath);
            }


        }

        private void btGenerateFromSql_Click(object sender, EventArgs e)
        {
            Program.ParametersData.SqlConnectionString = txSqlConnectionString.Text;
            // 
        }
    }
}
