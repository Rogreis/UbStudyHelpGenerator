
namespace UbStudyHelpGenerator
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelPaperNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageFromSqlServer = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btCheck = new System.Windows.Forms.Button();
            this.btGetTranslation = new System.Windows.Forms.Button();
            this.btGetAllTranslations = new System.Windows.Forms.Button();
            this.comboBoxTranslations = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txSqlServerConnectionString = new System.Windows.Forms.TextBox();
            this.btGetTranslations = new System.Windows.Forms.Button();
            this.textBoxFromSqlServer = new System.Windows.Forms.TextBox();
            this.tabPageFromHtml = new System.Windows.Forms.TabPage();
            this.splitContainerFromHtml = new System.Windows.Forms.SplitContainer();
            this.btDummy = new System.Windows.Forms.Button();
            this.btSpanishDownload = new System.Windows.Forms.Button();
            this.btHtmlFilesPath = new System.Windows.Forms.Button();
            this.lblHtmlPath = new System.Windows.Forms.Label();
            this.txHtmlFilesPath = new System.Windows.Forms.TextBox();
            this.btSpanish = new System.Windows.Forms.Button();
            this.textBoxFromHtml = new System.Windows.Forms.TextBox();
            this.tabPageUbIndex = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.btUfIndexDownload = new System.Windows.Forms.Button();
            this.btUfIndexOutputFiles = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txUfIndexOutputFolder = new System.Windows.Forms.TextBox();
            this.btUfIndex = new System.Windows.Forms.Button();
            this.btUfIndexDownloadedFiles = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txUfIndexDownloadeFiles = new System.Windows.Forms.TextBox();
            this.txUbIndexMessages = new System.Windows.Forms.TextBox();
            this.tabPagePTAlternative = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.btPTALternativeFolder = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txPRAlternativeFolder = new System.Windows.Forms.TextBox();
            this.btPTAlternativeGenerate = new System.Windows.Forms.Button();
            this.txPTAlternative = new System.Windows.Forms.TextBox();
            this.btGetRepositoryOutputFolder = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txRepositoryOutputFolder = new System.Windows.Forms.TextBox();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.statusStrip1.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageFromSqlServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPageFromHtml.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerFromHtml)).BeginInit();
            this.splitContainerFromHtml.Panel1.SuspendLayout();
            this.splitContainerFromHtml.Panel2.SuspendLayout();
            this.splitContainerFromHtml.SuspendLayout();
            this.tabPageUbIndex.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabPagePTAlternative.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelPaperNumber});
            this.statusStrip1.Location = new System.Drawing.Point(0, 848);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1302, 53);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelPaperNumber
            // 
            this.toolStripStatusLabelPaperNumber.AutoSize = false;
            this.toolStripStatusLabelPaperNumber.Name = "toolStripStatusLabelPaperNumber";
            this.toolStripStatusLabelPaperNumber.Size = new System.Drawing.Size(100, 46);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageFromSqlServer);
            this.tabControlMain.Controls.Add(this.tabPageFromHtml);
            this.tabControlMain.Controls.Add(this.tabPageUbIndex);
            this.tabControlMain.Controls.Add(this.tabPagePTAlternative);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1302, 734);
            this.tabControlMain.TabIndex = 1;
            // 
            // tabPageFromSqlServer
            // 
            this.tabPageFromSqlServer.Controls.Add(this.splitContainer1);
            this.tabPageFromSqlServer.Location = new System.Drawing.Point(4, 29);
            this.tabPageFromSqlServer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageFromSqlServer.Name = "tabPageFromSqlServer";
            this.tabPageFromSqlServer.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageFromSqlServer.Size = new System.Drawing.Size(1294, 701);
            this.tabPageFromSqlServer.TabIndex = 1;
            this.tabPageFromSqlServer.Text = "From Sql Server";
            this.tabPageFromSqlServer.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(3, 4);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btCheck);
            this.splitContainer1.Panel1.Controls.Add(this.btGetTranslation);
            this.splitContainer1.Panel1.Controls.Add(this.btGetAllTranslations);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxTranslations);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.txSqlServerConnectionString);
            this.splitContainer1.Panel1.Controls.Add(this.btGetTranslations);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBoxFromSqlServer);
            this.splitContainer1.Size = new System.Drawing.Size(1288, 693);
            this.splitContainer1.SplitterDistance = 163;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // btCheck
            // 
            this.btCheck.Location = new System.Drawing.Point(1014, 82);
            this.btCheck.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btCheck.Name = "btCheck";
            this.btCheck.Size = new System.Drawing.Size(216, 60);
            this.btCheck.TabIndex = 6;
            this.btCheck.Text = "Check";
            this.btCheck.UseVisualStyleBackColor = true;
            this.btCheck.Click += new System.EventHandler(this.btCheck_Click);
            // 
            // btGetTranslation
            // 
            this.btGetTranslation.Enabled = false;
            this.btGetTranslation.Location = new System.Drawing.Point(778, 82);
            this.btGetTranslation.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btGetTranslation.Name = "btGetTranslation";
            this.btGetTranslation.Size = new System.Drawing.Size(216, 60);
            this.btGetTranslation.TabIndex = 5;
            this.btGetTranslation.Text = "Get Translations";
            this.btGetTranslation.UseVisualStyleBackColor = true;
            this.btGetTranslation.Click += new System.EventHandler(this.btGetTranslation_Click);
            // 
            // btGetAllTranslations
            // 
            this.btGetAllTranslations.Enabled = false;
            this.btGetAllTranslations.Location = new System.Drawing.Point(604, 82);
            this.btGetAllTranslations.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btGetAllTranslations.Name = "btGetAllTranslations";
            this.btGetAllTranslations.Size = new System.Drawing.Size(158, 60);
            this.btGetAllTranslations.TabIndex = 4;
            this.btGetAllTranslations.Text = "Gell All Translations Papers";
            this.btGetAllTranslations.UseVisualStyleBackColor = true;
            this.btGetAllTranslations.Click += new System.EventHandler(this.btGetAllTranslations_Click);
            // 
            // comboBoxTranslations
            // 
            this.comboBoxTranslations.FormattingEnabled = true;
            this.comboBoxTranslations.Location = new System.Drawing.Point(180, 100);
            this.comboBoxTranslations.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxTranslations.Name = "comboBoxTranslations";
            this.comboBoxTranslations.Size = new System.Drawing.Size(404, 28);
            this.comboBoxTranslations.TabIndex = 3;
            this.comboBoxTranslations.SelectedIndexChanged += new System.EventHandler(this.comboBoxTranslations_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sql Server Connection String";
            // 
            // txSqlServerConnectionString
            // 
            this.txSqlServerConnectionString.Location = new System.Drawing.Point(17, 49);
            this.txSqlServerConnectionString.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txSqlServerConnectionString.Name = "txSqlServerConnectionString";
            this.txSqlServerConnectionString.Size = new System.Drawing.Size(1213, 26);
            this.txSqlServerConnectionString.TabIndex = 1;
            this.txSqlServerConnectionString.TextChanged += new System.EventHandler(this.txSqlServerConnectionString_TextChanged);
            // 
            // btGetTranslations
            // 
            this.btGetTranslations.Location = new System.Drawing.Point(17, 82);
            this.btGetTranslations.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btGetTranslations.Name = "btGetTranslations";
            this.btGetTranslations.Size = new System.Drawing.Size(158, 60);
            this.btGetTranslations.TabIndex = 0;
            this.btGetTranslations.Text = "Get Translations";
            this.btGetTranslations.UseVisualStyleBackColor = true;
            this.btGetTranslations.Click += new System.EventHandler(this.btGenerateFromSql_Click);
            // 
            // textBoxFromSqlServer
            // 
            this.textBoxFromSqlServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxFromSqlServer.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFromSqlServer.Location = new System.Drawing.Point(0, 0);
            this.textBoxFromSqlServer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxFromSqlServer.Multiline = true;
            this.textBoxFromSqlServer.Name = "textBoxFromSqlServer";
            this.textBoxFromSqlServer.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxFromSqlServer.Size = new System.Drawing.Size(1288, 525);
            this.textBoxFromSqlServer.TabIndex = 0;
            // 
            // tabPageFromHtml
            // 
            this.tabPageFromHtml.Controls.Add(this.splitContainerFromHtml);
            this.tabPageFromHtml.Location = new System.Drawing.Point(4, 29);
            this.tabPageFromHtml.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageFromHtml.Name = "tabPageFromHtml";
            this.tabPageFromHtml.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageFromHtml.Size = new System.Drawing.Size(1294, 701);
            this.tabPageFromHtml.TabIndex = 0;
            this.tabPageFromHtml.Text = "Spanish HTML";
            this.tabPageFromHtml.UseVisualStyleBackColor = true;
            // 
            // splitContainerFromHtml
            // 
            this.splitContainerFromHtml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerFromHtml.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerFromHtml.Location = new System.Drawing.Point(3, 4);
            this.splitContainerFromHtml.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainerFromHtml.Name = "splitContainerFromHtml";
            this.splitContainerFromHtml.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerFromHtml.Panel1
            // 
            this.splitContainerFromHtml.Panel1.Controls.Add(this.btDummy);
            this.splitContainerFromHtml.Panel1.Controls.Add(this.btSpanishDownload);
            this.splitContainerFromHtml.Panel1.Controls.Add(this.btHtmlFilesPath);
            this.splitContainerFromHtml.Panel1.Controls.Add(this.lblHtmlPath);
            this.splitContainerFromHtml.Panel1.Controls.Add(this.txHtmlFilesPath);
            this.splitContainerFromHtml.Panel1.Controls.Add(this.btSpanish);
            // 
            // splitContainerFromHtml.Panel2
            // 
            this.splitContainerFromHtml.Panel2.Controls.Add(this.textBoxFromHtml);
            this.splitContainerFromHtml.Size = new System.Drawing.Size(1288, 693);
            this.splitContainerFromHtml.SplitterDistance = 163;
            this.splitContainerFromHtml.SplitterWidth = 5;
            this.splitContainerFromHtml.TabIndex = 0;
            // 
            // btDummy
            // 
            this.btDummy.Location = new System.Drawing.Point(461, 72);
            this.btDummy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btDummy.Name = "btDummy";
            this.btDummy.Size = new System.Drawing.Size(126, 60);
            this.btDummy.TabIndex = 10;
            this.btDummy.Text = "Par Descr";
            this.btDummy.UseVisualStyleBackColor = true;
            this.btDummy.Click += new System.EventHandler(this.btDummy_Click);
            // 
            // btSpanishDownload
            // 
            this.btSpanishDownload.Enabled = false;
            this.btSpanishDownload.Location = new System.Drawing.Point(19, 72);
            this.btSpanishDownload.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btSpanishDownload.Name = "btSpanishDownload";
            this.btSpanishDownload.Size = new System.Drawing.Size(126, 60);
            this.btSpanishDownload.TabIndex = 9;
            this.btSpanishDownload.Text = "Download";
            this.btSpanishDownload.UseVisualStyleBackColor = true;
            this.btSpanishDownload.Click += new System.EventHandler(this.btSpanishDownload_Click);
            // 
            // btHtmlFilesPath
            // 
            this.btHtmlFilesPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btHtmlFilesPath.Location = new System.Drawing.Point(1238, 20);
            this.btHtmlFilesPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btHtmlFilesPath.Name = "btHtmlFilesPath";
            this.btHtmlFilesPath.Size = new System.Drawing.Size(66, 56);
            this.btHtmlFilesPath.TabIndex = 3;
            this.btHtmlFilesPath.Text = "...";
            this.btHtmlFilesPath.UseVisualStyleBackColor = true;
            this.btHtmlFilesPath.Click += new System.EventHandler(this.btHtmlFilesPath_Click);
            // 
            // lblHtmlPath
            // 
            this.lblHtmlPath.AutoSize = true;
            this.lblHtmlPath.Location = new System.Drawing.Point(17, 9);
            this.lblHtmlPath.Name = "lblHtmlPath";
            this.lblHtmlPath.Size = new System.Drawing.Size(116, 20);
            this.lblHtmlPath.TabIndex = 2;
            this.lblHtmlPath.Text = "Html Files Path";
            // 
            // txHtmlFilesPath
            // 
            this.txHtmlFilesPath.Location = new System.Drawing.Point(17, 38);
            this.txHtmlFilesPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txHtmlFilesPath.Name = "txHtmlFilesPath";
            this.txHtmlFilesPath.Size = new System.Drawing.Size(1213, 26);
            this.txHtmlFilesPath.TabIndex = 1;
            // 
            // btSpanish
            // 
            this.btSpanish.Location = new System.Drawing.Point(164, 72);
            this.btSpanish.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btSpanish.Name = "btSpanish";
            this.btSpanish.Size = new System.Drawing.Size(126, 60);
            this.btSpanish.TabIndex = 0;
            this.btSpanish.Text = "Spanish";
            this.btSpanish.UseVisualStyleBackColor = true;
            this.btSpanish.Click += new System.EventHandler(this.btSpanish_Click);
            // 
            // textBoxFromHtml
            // 
            this.textBoxFromHtml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxFromHtml.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFromHtml.Location = new System.Drawing.Point(0, 0);
            this.textBoxFromHtml.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxFromHtml.Multiline = true;
            this.textBoxFromHtml.Name = "textBoxFromHtml";
            this.textBoxFromHtml.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxFromHtml.Size = new System.Drawing.Size(1288, 525);
            this.textBoxFromHtml.TabIndex = 0;
            // 
            // tabPageUbIndex
            // 
            this.tabPageUbIndex.Controls.Add(this.splitContainer2);
            this.tabPageUbIndex.Location = new System.Drawing.Point(4, 29);
            this.tabPageUbIndex.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPageUbIndex.Name = "tabPageUbIndex";
            this.tabPageUbIndex.Size = new System.Drawing.Size(1294, 701);
            this.tabPageUbIndex.TabIndex = 2;
            this.tabPageUbIndex.Text = "Generate UB Index";
            this.tabPageUbIndex.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btUfIndexDownload);
            this.splitContainer2.Panel1.Controls.Add(this.btUfIndexOutputFiles);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            this.splitContainer2.Panel1.Controls.Add(this.txUfIndexOutputFolder);
            this.splitContainer2.Panel1.Controls.Add(this.btUfIndex);
            this.splitContainer2.Panel1.Controls.Add(this.btUfIndexDownloadedFiles);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.txUfIndexDownloadeFiles);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txUbIndexMessages);
            this.splitContainer2.Size = new System.Drawing.Size(1294, 701);
            this.splitContainer2.SplitterDistance = 188;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 1;
            // 
            // btUfIndexDownload
            // 
            this.btUfIndexDownload.Location = new System.Drawing.Point(17, 145);
            this.btUfIndexDownload.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btUfIndexDownload.Name = "btUfIndexDownload";
            this.btUfIndexDownload.Size = new System.Drawing.Size(126, 60);
            this.btUfIndexDownload.TabIndex = 8;
            this.btUfIndexDownload.Text = "Download";
            this.btUfIndexDownload.UseVisualStyleBackColor = true;
            this.btUfIndexDownload.Click += new System.EventHandler(this.btDownload_Click);
            // 
            // btUfIndexOutputFiles
            // 
            this.btUfIndexOutputFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btUfIndexOutputFiles.Location = new System.Drawing.Point(1238, 92);
            this.btUfIndexOutputFiles.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btUfIndexOutputFiles.Name = "btUfIndexOutputFiles";
            this.btUfIndexOutputFiles.Size = new System.Drawing.Size(66, 56);
            this.btUfIndexOutputFiles.TabIndex = 7;
            this.btUfIndexOutputFiles.Text = "...";
            this.btUfIndexOutputFiles.UseVisualStyleBackColor = true;
            this.btUfIndexOutputFiles.Click += new System.EventHandler(this.btUfIndexOutputFiles_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Output folder";
            // 
            // txUfIndexOutputFolder
            // 
            this.txUfIndexOutputFolder.Location = new System.Drawing.Point(17, 110);
            this.txUfIndexOutputFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txUfIndexOutputFolder.Name = "txUfIndexOutputFolder";
            this.txUfIndexOutputFolder.Size = new System.Drawing.Size(1213, 26);
            this.txUfIndexOutputFolder.TabIndex = 5;
            // 
            // btUfIndex
            // 
            this.btUfIndex.Location = new System.Drawing.Point(163, 145);
            this.btUfIndex.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btUfIndex.Name = "btUfIndex";
            this.btUfIndex.Size = new System.Drawing.Size(126, 60);
            this.btUfIndex.TabIndex = 4;
            this.btUfIndex.Text = "Generate";
            this.btUfIndex.UseVisualStyleBackColor = true;
            this.btUfIndex.Click += new System.EventHandler(this.btUfIndex_Click);
            // 
            // btUfIndexDownloadedFiles
            // 
            this.btUfIndexDownloadedFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btUfIndexDownloadedFiles.Location = new System.Drawing.Point(1238, 31);
            this.btUfIndexDownloadedFiles.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btUfIndexDownloadedFiles.Name = "btUfIndexDownloadedFiles";
            this.btUfIndexDownloadedFiles.Size = new System.Drawing.Size(66, 56);
            this.btUfIndexDownloadedFiles.TabIndex = 3;
            this.btUfIndexDownloadedFiles.Text = "...";
            this.btUfIndexDownloadedFiles.UseVisualStyleBackColor = true;
            this.btUfIndexDownloadedFiles.Click += new System.EventHandler(this.btUfIndexDownloadedFiles_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Downloaded files";
            // 
            // txUfIndexDownloadeFiles
            // 
            this.txUfIndexDownloadeFiles.Location = new System.Drawing.Point(17, 49);
            this.txUfIndexDownloadeFiles.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txUfIndexDownloadeFiles.Name = "txUfIndexDownloadeFiles";
            this.txUfIndexDownloadeFiles.Size = new System.Drawing.Size(1213, 26);
            this.txUfIndexDownloadeFiles.TabIndex = 1;
            // 
            // txUbIndexMessages
            // 
            this.txUbIndexMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txUbIndexMessages.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txUbIndexMessages.Location = new System.Drawing.Point(0, 0);
            this.txUbIndexMessages.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txUbIndexMessages.Multiline = true;
            this.txUbIndexMessages.Name = "txUbIndexMessages";
            this.txUbIndexMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txUbIndexMessages.Size = new System.Drawing.Size(1294, 508);
            this.txUbIndexMessages.TabIndex = 0;
            // 
            // tabPagePTAlternative
            // 
            this.tabPagePTAlternative.Controls.Add(this.splitContainer3);
            this.tabPagePTAlternative.Location = new System.Drawing.Point(4, 29);
            this.tabPagePTAlternative.Name = "tabPagePTAlternative";
            this.tabPagePTAlternative.Size = new System.Drawing.Size(1945, 1125);
            this.tabPagePTAlternative.TabIndex = 3;
            this.tabPagePTAlternative.Text = "Portuguese Alternative";
            this.tabPagePTAlternative.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.btPTALternativeFolder);
            this.splitContainer3.Panel1.Controls.Add(this.label5);
            this.splitContainer3.Panel1.Controls.Add(this.txPRAlternativeFolder);
            this.splitContainer3.Panel1.Controls.Add(this.btPTAlternativeGenerate);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.txPTAlternative);
            this.splitContainer3.Size = new System.Drawing.Size(1945, 1125);
            this.splitContainer3.SplitterDistance = 163;
            this.splitContainer3.SplitterWidth = 5;
            this.splitContainer3.TabIndex = 2;
            // 
            // btPTALternativeFolder
            // 
            this.btPTALternativeFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btPTALternativeFolder.Location = new System.Drawing.Point(1252, 31);
            this.btPTALternativeFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btPTALternativeFolder.Name = "btPTALternativeFolder";
            this.btPTALternativeFolder.Size = new System.Drawing.Size(66, 56);
            this.btPTALternativeFolder.TabIndex = 7;
            this.btPTALternativeFolder.Text = "...";
            this.btPTALternativeFolder.UseVisualStyleBackColor = true;
            this.btPTALternativeFolder.Click += new System.EventHandler(this.btPTALternativeFolder_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "Destination Folder";
            // 
            // txPRAlternativeFolder
            // 
            this.txPRAlternativeFolder.Location = new System.Drawing.Point(17, 49);
            this.txPRAlternativeFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txPRAlternativeFolder.Name = "txPRAlternativeFolder";
            this.txPRAlternativeFolder.Size = new System.Drawing.Size(1213, 26);
            this.txPRAlternativeFolder.TabIndex = 1;
            // 
            // btPTAlternativeGenerate
            // 
            this.btPTAlternativeGenerate.Location = new System.Drawing.Point(17, 82);
            this.btPTAlternativeGenerate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btPTAlternativeGenerate.Name = "btPTAlternativeGenerate";
            this.btPTAlternativeGenerate.Size = new System.Drawing.Size(158, 60);
            this.btPTAlternativeGenerate.TabIndex = 0;
            this.btPTAlternativeGenerate.Text = "Generate";
            this.btPTAlternativeGenerate.UseVisualStyleBackColor = true;
            this.btPTAlternativeGenerate.Click += new System.EventHandler(this.btPTAlternativeGenerate_Click);
            // 
            // txPTAlternative
            // 
            this.txPTAlternative.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txPTAlternative.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txPTAlternative.Location = new System.Drawing.Point(0, 0);
            this.txPTAlternative.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txPTAlternative.Multiline = true;
            this.txPTAlternative.Name = "txPTAlternative";
            this.txPTAlternative.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txPTAlternative.Size = new System.Drawing.Size(1945, 957);
            this.txPTAlternative.TabIndex = 0;
            // 
            // btGetRepositoryOutputFolder
            // 
            this.btGetRepositoryOutputFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btGetRepositoryOutputFolder.Location = new System.Drawing.Point(1234, 31);
            this.btGetRepositoryOutputFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btGetRepositoryOutputFolder.Name = "btGetRepositoryOutputFolder";
            this.btGetRepositoryOutputFolder.Size = new System.Drawing.Size(66, 56);
            this.btGetRepositoryOutputFolder.TabIndex = 12;
            this.btGetRepositoryOutputFolder.Text = "...";
            this.btGetRepositoryOutputFolder.UseVisualStyleBackColor = true;
            this.btGetRepositoryOutputFolder.Click += new System.EventHandler(this.btGetRepositoryOutputFolder_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(187, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Repository Output Folder";
            // 
            // txRepositoryOutputFolder
            // 
            this.txRepositoryOutputFolder.Location = new System.Drawing.Point(14, 49);
            this.txRepositoryOutputFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txRepositoryOutputFolder.Name = "txRepositoryOutputFolder";
            this.txRepositoryOutputFolder.Size = new System.Drawing.Size(1213, 26);
            this.txRepositoryOutputFolder.TabIndex = 10;
            this.txRepositoryOutputFolder.TextChanged += new System.EventHandler(this.txRepositoryOutputFolder_TextChanged);
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.btGetRepositoryOutputFolder);
            this.splitContainerMain.Panel1.Controls.Add(this.label4);
            this.splitContainerMain.Panel1.Controls.Add(this.txRepositoryOutputFolder);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.tabControlMain);
            this.splitContainerMain.Size = new System.Drawing.Size(1302, 848);
            this.splitContainerMain.SplitterDistance = 109;
            this.splitContainerMain.SplitterWidth = 5;
            this.splitContainerMain.TabIndex = 2;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1302, 901);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.statusStrip1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ub Study Help Generator";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageFromSqlServer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPageFromHtml.ResumeLayout(false);
            this.splitContainerFromHtml.Panel1.ResumeLayout(false);
            this.splitContainerFromHtml.Panel1.PerformLayout();
            this.splitContainerFromHtml.Panel2.ResumeLayout(false);
            this.splitContainerFromHtml.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerFromHtml)).EndInit();
            this.splitContainerFromHtml.ResumeLayout(false);
            this.tabPageUbIndex.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabPagePTAlternative.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel1.PerformLayout();
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageFromHtml;
        private System.Windows.Forms.SplitContainer splitContainerFromHtml;
        private System.Windows.Forms.TextBox textBoxFromHtml;
        private System.Windows.Forms.TabPage tabPageFromSqlServer;
        private System.Windows.Forms.Button btHtmlFilesPath;
        private System.Windows.Forms.Label lblHtmlPath;
        private System.Windows.Forms.TextBox txHtmlFilesPath;
        private System.Windows.Forms.Button btSpanish;
        private System.Windows.Forms.Button btUfIndex;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txSqlServerConnectionString;
        private System.Windows.Forms.Button btGetTranslations;
        private System.Windows.Forms.TextBox textBoxFromSqlServer;
        private System.Windows.Forms.TabPage tabPageUbIndex;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btUfIndexDownloadedFiles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txUfIndexDownloadeFiles;
        private System.Windows.Forms.TextBox txUbIndexMessages;
        private System.Windows.Forms.Button btUfIndexOutputFiles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txUfIndexOutputFolder;
        private System.Windows.Forms.Button btUfIndexDownload;
        private System.Windows.Forms.Button btSpanishDownload;
        private System.Windows.Forms.Button btGetRepositoryOutputFolder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txRepositoryOutputFolder;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Button btGetTranslation;
        private System.Windows.Forms.Button btGetAllTranslations;
        private System.Windows.Forms.ComboBox comboBoxTranslations;
        private System.Windows.Forms.Button btCheck;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelPaperNumber;
        private System.Windows.Forms.Button btDummy;
        private System.Windows.Forms.TabPage tabPagePTAlternative;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button btPTALternativeFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txPRAlternativeFolder;
        private System.Windows.Forms.Button btPTAlternativeGenerate;
        private System.Windows.Forms.TextBox txPTAlternative;
    }
}

