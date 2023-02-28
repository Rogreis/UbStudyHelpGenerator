
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
            this.toolStripStatusLabelMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.btGetRepositoryOutputFolder = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txRepositoryOutputFolder = new System.Windows.Forms.TextBox();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.btGetTranslations = new System.Windows.Forms.Button();
            this.comboBoxTranslations = new System.Windows.Forms.ComboBox();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageEditTranslation = new System.Windows.Forms.TabPage();
            this.splitContainerPtAlternative = new System.Windows.Forms.SplitContainer();
            this.btRecreateTrans = new System.Windows.Forms.Button();
            this.btExportToUbHelp = new System.Windows.Forms.Button();
            this.btTocTable = new System.Windows.Forms.Button();
            this.btEditBookRepositoryFolder = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txEditBookRepositoryFolder = new System.Windows.Forms.TextBox();
            this.btImportDocx = new System.Windows.Forms.Button();
            this.btGenerateNotes = new System.Windows.Forms.Button();
            this.btExportPtAlternativeDocx = new System.Windows.Forms.Button();
            this.btPtBrIndex = new System.Windows.Forms.Button();
            this.btEditTranslationRepositoryFolder = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txTranslationRepositoryFolder = new System.Windows.Forms.TextBox();
            this.btPTAlternativeGenerate = new System.Windows.Forms.Button();
            this.txPTalternative = new System.Windows.Forms.TextBox();
            this.tabPageFromSqlServer = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btExportFormat = new System.Windows.Forms.Button();
            this.btCheck = new System.Windows.Forms.Button();
            this.btGetTranslation = new System.Windows.Forms.Button();
            this.btGetAllTranslations = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txSqlServerConnectionString = new System.Windows.Forms.TextBox();
            this.textBoxFromSqlServer = new System.Windows.Forms.TextBox();
            this.tabPageFromHtml = new System.Windows.Forms.TabPage();
            this.splitContainerFromHtml = new System.Windows.Forms.SplitContainer();
            this.btSpanishEscobar = new System.Windows.Forms.Button();
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
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageEditTranslation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPtAlternative)).BeginInit();
            this.splitContainerPtAlternative.Panel1.SuspendLayout();
            this.splitContainerPtAlternative.Panel2.SuspendLayout();
            this.splitContainerPtAlternative.SuspendLayout();
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
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelPaperNumber,
            this.toolStripStatusLabelMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1035);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1484, 53);
            this.statusStrip1.TabIndex = 0;
            // 
            // toolStripStatusLabelPaperNumber
            // 
            this.toolStripStatusLabelPaperNumber.AutoSize = false;
            this.toolStripStatusLabelPaperNumber.Name = "toolStripStatusLabelPaperNumber";
            this.toolStripStatusLabelPaperNumber.Size = new System.Drawing.Size(100, 46);
            // 
            // toolStripStatusLabelMessage
            // 
            this.toolStripStatusLabelMessage.BackColor = System.Drawing.SystemColors.ControlDark;
            this.toolStripStatusLabelMessage.Name = "toolStripStatusLabelMessage";
            this.toolStripStatusLabelMessage.Size = new System.Drawing.Size(1366, 46);
            this.toolStripStatusLabelMessage.Spring = true;
            this.toolStripStatusLabelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btGetRepositoryOutputFolder
            // 
            this.btGetRepositoryOutputFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btGetRepositoryOutputFolder.Location = new System.Drawing.Point(1406, 5);
            this.btGetRepositoryOutputFolder.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btGetRepositoryOutputFolder.Name = "btGetRepositoryOutputFolder";
            this.btGetRepositoryOutputFolder.Size = new System.Drawing.Size(66, 39);
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
            this.label4.Size = new System.Drawing.Size(207, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "TUB Files Repository Folder";
            // 
            // txRepositoryOutputFolder
            // 
            this.txRepositoryOutputFolder.Location = new System.Drawing.Point(227, 14);
            this.txRepositoryOutputFolder.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txRepositoryOutputFolder.Name = "txRepositoryOutputFolder";
            this.txRepositoryOutputFolder.Size = new System.Drawing.Size(1163, 26);
            this.txRepositoryOutputFolder.TabIndex = 10;
            this.txRepositoryOutputFolder.TextChanged += new System.EventHandler(this.txRepositoryOutputFolder_TextChanged);
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.btGetRepositoryOutputFolder);
            this.splitContainerMain.Panel1.Controls.Add(this.label4);
            this.splitContainerMain.Panel1.Controls.Add(this.txRepositoryOutputFolder);
            this.splitContainerMain.Panel1.Controls.Add(this.btGetTranslations);
            this.splitContainerMain.Panel1.Controls.Add(this.comboBoxTranslations);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.tabControlMain);
            this.splitContainerMain.Size = new System.Drawing.Size(1484, 1035);
            this.splitContainerMain.SplitterDistance = 109;
            this.splitContainerMain.SplitterWidth = 5;
            this.splitContainerMain.TabIndex = 2;
            // 
            // btGetTranslations
            // 
            this.btGetTranslations.Location = new System.Drawing.Point(18, 44);
            this.btGetTranslations.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btGetTranslations.Name = "btGetTranslations";
            this.btGetTranslations.Size = new System.Drawing.Size(203, 41);
            this.btGetTranslations.TabIndex = 0;
            this.btGetTranslations.Text = "Get Translations";
            this.btGetTranslations.UseVisualStyleBackColor = true;
            this.btGetTranslations.Click += new System.EventHandler(this.btGenerateFromSql_Click);
            // 
            // comboBoxTranslations
            // 
            this.comboBoxTranslations.FormattingEnabled = true;
            this.comboBoxTranslations.Location = new System.Drawing.Point(227, 51);
            this.comboBoxTranslations.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxTranslations.Name = "comboBoxTranslations";
            this.comboBoxTranslations.Size = new System.Drawing.Size(404, 28);
            this.comboBoxTranslations.TabIndex = 3;
            this.comboBoxTranslations.SelectedIndexChanged += new System.EventHandler(this.comboBoxTranslations_SelectedIndexChanged);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageEditTranslation);
            this.tabControlMain.Controls.Add(this.tabPageFromSqlServer);
            this.tabControlMain.Controls.Add(this.tabPageFromHtml);
            this.tabControlMain.Controls.Add(this.tabPageUbIndex);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1484, 921);
            this.tabControlMain.TabIndex = 1;
            // 
            // tabPageEditTranslation
            // 
            this.tabPageEditTranslation.Controls.Add(this.splitContainerPtAlternative);
            this.tabPageEditTranslation.Location = new System.Drawing.Point(4, 29);
            this.tabPageEditTranslation.Name = "tabPageEditTranslation";
            this.tabPageEditTranslation.Size = new System.Drawing.Size(1476, 888);
            this.tabPageEditTranslation.TabIndex = 3;
            this.tabPageEditTranslation.Tag = "Edit";
            this.tabPageEditTranslation.Text = "Edit Translation";
            this.tabPageEditTranslation.UseVisualStyleBackColor = true;
            // 
            // splitContainerPtAlternative
            // 
            this.splitContainerPtAlternative.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerPtAlternative.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerPtAlternative.Location = new System.Drawing.Point(0, 0);
            this.splitContainerPtAlternative.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.splitContainerPtAlternative.Name = "splitContainerPtAlternative";
            this.splitContainerPtAlternative.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerPtAlternative.Panel1
            // 
            this.splitContainerPtAlternative.Panel1.Controls.Add(this.btRecreateTrans);
            this.splitContainerPtAlternative.Panel1.Controls.Add(this.btExportToUbHelp);
            this.splitContainerPtAlternative.Panel1.Controls.Add(this.btTocTable);
            this.splitContainerPtAlternative.Panel1.Controls.Add(this.btEditBookRepositoryFolder);
            this.splitContainerPtAlternative.Panel1.Controls.Add(this.label6);
            this.splitContainerPtAlternative.Panel1.Controls.Add(this.txEditBookRepositoryFolder);
            this.splitContainerPtAlternative.Panel1.Controls.Add(this.btImportDocx);
            this.splitContainerPtAlternative.Panel1.Controls.Add(this.btGenerateNotes);
            this.splitContainerPtAlternative.Panel1.Controls.Add(this.btExportPtAlternativeDocx);
            this.splitContainerPtAlternative.Panel1.Controls.Add(this.btPtBrIndex);
            this.splitContainerPtAlternative.Panel1.Controls.Add(this.btEditTranslationRepositoryFolder);
            this.splitContainerPtAlternative.Panel1.Controls.Add(this.label5);
            this.splitContainerPtAlternative.Panel1.Controls.Add(this.txTranslationRepositoryFolder);
            this.splitContainerPtAlternative.Panel1.Controls.Add(this.btPTAlternativeGenerate);
            // 
            // splitContainerPtAlternative.Panel2
            // 
            this.splitContainerPtAlternative.Panel2.Controls.Add(this.txPTalternative);
            this.splitContainerPtAlternative.Size = new System.Drawing.Size(1476, 888);
            this.splitContainerPtAlternative.SplitterDistance = 288;
            this.splitContainerPtAlternative.SplitterWidth = 5;
            this.splitContainerPtAlternative.TabIndex = 2;
            // 
            // btRecreateTrans
            // 
            this.btRecreateTrans.BackColor = System.Drawing.Color.PeachPuff;
            this.btRecreateTrans.Location = new System.Drawing.Point(20, 211);
            this.btRecreateTrans.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btRecreateTrans.Name = "btRecreateTrans";
            this.btRecreateTrans.Size = new System.Drawing.Size(158, 43);
            this.btRecreateTrans.TabIndex = 17;
            this.btRecreateTrans.Text = "Trans GZ file";
            this.btRecreateTrans.UseVisualStyleBackColor = false;
            this.btRecreateTrans.Click += new System.EventHandler(this.btRecreateTrans_Click);
            // 
            // btExportToUbHelp
            // 
            this.btExportToUbHelp.BackColor = System.Drawing.Color.PeachPuff;
            this.btExportToUbHelp.Location = new System.Drawing.Point(770, 158);
            this.btExportToUbHelp.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btExportToUbHelp.Name = "btExportToUbHelp";
            this.btExportToUbHelp.Size = new System.Drawing.Size(158, 43);
            this.btExportToUbHelp.TabIndex = 16;
            this.btExportToUbHelp.Text = "To UB Study Help";
            this.btExportToUbHelp.UseVisualStyleBackColor = false;
            this.btExportToUbHelp.Click += new System.EventHandler(this.btExportToUbHelp_Click);
            // 
            // btTocTable
            // 
            this.btTocTable.BackColor = System.Drawing.Color.PeachPuff;
            this.btTocTable.Location = new System.Drawing.Point(412, 158);
            this.btTocTable.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btTocTable.Name = "btTocTable";
            this.btTocTable.Size = new System.Drawing.Size(188, 43);
            this.btTocTable.TabIndex = 15;
            this.btTocTable.Text = "Generate TOC Table";
            this.btTocTable.UseVisualStyleBackColor = false;
            this.btTocTable.Click += new System.EventHandler(this.btTocTable_Click);
            // 
            // btEditBookRepositoryFolder
            // 
            this.btEditBookRepositoryFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btEditBookRepositoryFolder.Location = new System.Drawing.Point(1204, 103);
            this.btEditBookRepositoryFolder.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btEditBookRepositoryFolder.Name = "btEditBookRepositoryFolder";
            this.btEditBookRepositoryFolder.Size = new System.Drawing.Size(66, 45);
            this.btEditBookRepositoryFolder.TabIndex = 14;
            this.btEditBookRepositoryFolder.Text = "...";
            this.btEditBookRepositoryFolder.UseVisualStyleBackColor = true;
            this.btEditBookRepositoryFolder.Click += new System.EventHandler(this.btEditBookRepositoryFolder_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(207, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "Edit Book Repostiroy Folder";
            // 
            // txEditBookRepositoryFolder
            // 
            this.txEditBookRepositoryFolder.Location = new System.Drawing.Point(16, 122);
            this.txEditBookRepositoryFolder.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txEditBookRepositoryFolder.Name = "txEditBookRepositoryFolder";
            this.txEditBookRepositoryFolder.Size = new System.Drawing.Size(1171, 26);
            this.txEditBookRepositoryFolder.TabIndex = 12;
            // 
            // btImportDocx
            // 
            this.btImportDocx.Location = new System.Drawing.Point(1291, 172);
            this.btImportDocx.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btImportDocx.Name = "btImportDocx";
            this.btImportDocx.Size = new System.Drawing.Size(158, 60);
            this.btImportDocx.TabIndex = 11;
            this.btImportDocx.Text = "Voice Changes From Word";
            this.btImportDocx.UseVisualStyleBackColor = true;
            this.btImportDocx.Click += new System.EventHandler(this.btImportDocx_Click);
            // 
            // btGenerateNotes
            // 
            this.btGenerateNotes.BackColor = System.Drawing.Color.PeachPuff;
            this.btGenerateNotes.Location = new System.Drawing.Point(606, 158);
            this.btGenerateNotes.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btGenerateNotes.Name = "btGenerateNotes";
            this.btGenerateNotes.Size = new System.Drawing.Size(158, 43);
            this.btGenerateNotes.TabIndex = 10;
            this.btGenerateNotes.Text = "Generate Notes";
            this.btGenerateNotes.UseVisualStyleBackColor = false;
            this.btGenerateNotes.Click += new System.EventHandler(this.btGenerateNotes_Click);
            // 
            // btExportPtAlternativeDocx
            // 
            this.btExportPtAlternativeDocx.Enabled = false;
            this.btExportPtAlternativeDocx.Location = new System.Drawing.Point(1127, 192);
            this.btExportPtAlternativeDocx.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btExportPtAlternativeDocx.Name = "btExportPtAlternativeDocx";
            this.btExportPtAlternativeDocx.Size = new System.Drawing.Size(158, 40);
            this.btExportPtAlternativeDocx.TabIndex = 9;
            this.btExportPtAlternativeDocx.Text = "Export docx";
            this.btExportPtAlternativeDocx.UseVisualStyleBackColor = true;
            this.btExportPtAlternativeDocx.Visible = false;
            this.btExportPtAlternativeDocx.Click += new System.EventHandler(this.btExportPtAlternativeDocx_Click);
            // 
            // btPtBrIndex
            // 
            this.btPtBrIndex.BackColor = System.Drawing.Color.PeachPuff;
            this.btPtBrIndex.Location = new System.Drawing.Point(230, 158);
            this.btPtBrIndex.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btPtBrIndex.Name = "btPtBrIndex";
            this.btPtBrIndex.Size = new System.Drawing.Size(176, 43);
            this.btPtBrIndex.TabIndex = 8;
            this.btPtBrIndex.Text = "Generate index.html";
            this.btPtBrIndex.UseVisualStyleBackColor = false;
            this.btPtBrIndex.Click += new System.EventHandler(this.btPtBrIndex_Click);
            // 
            // btEditTranslationRepositoryFolder
            // 
            this.btEditTranslationRepositoryFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btEditTranslationRepositoryFolder.Location = new System.Drawing.Point(1204, 31);
            this.btEditTranslationRepositoryFolder.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btEditTranslationRepositoryFolder.Name = "btEditTranslationRepositoryFolder";
            this.btEditTranslationRepositoryFolder.Size = new System.Drawing.Size(66, 45);
            this.btEditTranslationRepositoryFolder.TabIndex = 7;
            this.btEditTranslationRepositoryFolder.Text = "...";
            this.btEditTranslationRepositoryFolder.UseVisualStyleBackColor = true;
            this.btEditTranslationRepositoryFolder.Click += new System.EventHandler(this.btEditTranslationRepositoryFolder_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(248, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "Edit Translation Repository Folder";
            // 
            // txTranslationRepositoryFolder
            // 
            this.txTranslationRepositoryFolder.Location = new System.Drawing.Point(16, 49);
            this.txTranslationRepositoryFolder.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txTranslationRepositoryFolder.Name = "txTranslationRepositoryFolder";
            this.txTranslationRepositoryFolder.Size = new System.Drawing.Size(1171, 26);
            this.txTranslationRepositoryFolder.TabIndex = 1;
            // 
            // btPTAlternativeGenerate
            // 
            this.btPTAlternativeGenerate.BackColor = System.Drawing.Color.PeachPuff;
            this.btPTAlternativeGenerate.Location = new System.Drawing.Point(15, 158);
            this.btPTAlternativeGenerate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btPTAlternativeGenerate.Name = "btPTAlternativeGenerate";
            this.btPTAlternativeGenerate.Size = new System.Drawing.Size(209, 43);
            this.btPTAlternativeGenerate.TabIndex = 0;
            this.btPTAlternativeGenerate.Text = "Generate rogreis.github.io";
            this.btPTAlternativeGenerate.UseVisualStyleBackColor = false;
            this.btPTAlternativeGenerate.Click += new System.EventHandler(this.btPTAlternativeGenerate_Click);
            // 
            // txPTalternative
            // 
            this.txPTalternative.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txPTalternative.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txPTalternative.Location = new System.Drawing.Point(0, 0);
            this.txPTalternative.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txPTalternative.Multiline = true;
            this.txPTalternative.Name = "txPTalternative";
            this.txPTalternative.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txPTalternative.Size = new System.Drawing.Size(1476, 595);
            this.txPTalternative.TabIndex = 1;
            // 
            // tabPageFromSqlServer
            // 
            this.tabPageFromSqlServer.Controls.Add(this.splitContainer1);
            this.tabPageFromSqlServer.Location = new System.Drawing.Point(4, 29);
            this.tabPageFromSqlServer.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPageFromSqlServer.Name = "tabPageFromSqlServer";
            this.tabPageFromSqlServer.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPageFromSqlServer.Size = new System.Drawing.Size(1476, 888);
            this.tabPageFromSqlServer.TabIndex = 1;
            this.tabPageFromSqlServer.Tag = "Sql";
            this.tabPageFromSqlServer.Text = "From Sql Server";
            this.tabPageFromSqlServer.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(3, 5);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btExportFormat);
            this.splitContainer1.Panel1.Controls.Add(this.btCheck);
            this.splitContainer1.Panel1.Controls.Add(this.btGetTranslation);
            this.splitContainer1.Panel1.Controls.Add(this.btGetAllTranslations);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.txSqlServerConnectionString);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBoxFromSqlServer);
            this.splitContainer1.Size = new System.Drawing.Size(1470, 878);
            this.splitContainer1.SplitterDistance = 163;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // btExportFormat
            // 
            this.btExportFormat.Location = new System.Drawing.Point(966, 85);
            this.btExportFormat.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btExportFormat.Name = "btExportFormat";
            this.btExportFormat.Size = new System.Drawing.Size(138, 60);
            this.btExportFormat.TabIndex = 7;
            this.btExportFormat.Text = "Export Format";
            this.btExportFormat.UseVisualStyleBackColor = true;
            this.btExportFormat.Click += new System.EventHandler(this.btExportFormat_Click);
            // 
            // btCheck
            // 
            this.btCheck.Location = new System.Drawing.Point(797, 86);
            this.btCheck.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btCheck.Name = "btCheck";
            this.btCheck.Size = new System.Drawing.Size(154, 60);
            this.btCheck.TabIndex = 6;
            this.btCheck.Text = "Check";
            this.btCheck.UseVisualStyleBackColor = true;
            this.btCheck.Click += new System.EventHandler(this.btCheck_Click);
            // 
            // btGetTranslation
            // 
            this.btGetTranslation.Enabled = false;
            this.btGetTranslation.Location = new System.Drawing.Point(618, 85);
            this.btGetTranslation.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btGetTranslation.Name = "btGetTranslation";
            this.btGetTranslation.Size = new System.Drawing.Size(164, 60);
            this.btGetTranslation.TabIndex = 5;
            this.btGetTranslation.Text = "Get Translations";
            this.btGetTranslation.UseVisualStyleBackColor = true;
            this.btGetTranslation.Click += new System.EventHandler(this.btGetTranslation_Click);
            // 
            // btGetAllTranslations
            // 
            this.btGetAllTranslations.Enabled = false;
            this.btGetAllTranslations.Location = new System.Drawing.Point(444, 85);
            this.btGetAllTranslations.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btGetAllTranslations.Name = "btGetAllTranslations";
            this.btGetAllTranslations.Size = new System.Drawing.Size(158, 60);
            this.btGetAllTranslations.TabIndex = 4;
            this.btGetAllTranslations.Text = "Gell All Translations Papers";
            this.btGetAllTranslations.UseVisualStyleBackColor = true;
            this.btGetAllTranslations.Click += new System.EventHandler(this.btGetAllTranslations_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sql Server Connection String";
            // 
            // txSqlServerConnectionString
            // 
            this.txSqlServerConnectionString.Location = new System.Drawing.Point(16, 49);
            this.txSqlServerConnectionString.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txSqlServerConnectionString.Name = "txSqlServerConnectionString";
            this.txSqlServerConnectionString.Size = new System.Drawing.Size(1213, 26);
            this.txSqlServerConnectionString.TabIndex = 1;
            this.txSqlServerConnectionString.TextChanged += new System.EventHandler(this.txSqlServerConnectionString_TextChanged);
            // 
            // textBoxFromSqlServer
            // 
            this.textBoxFromSqlServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxFromSqlServer.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFromSqlServer.Location = new System.Drawing.Point(0, 0);
            this.textBoxFromSqlServer.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.textBoxFromSqlServer.Multiline = true;
            this.textBoxFromSqlServer.Name = "textBoxFromSqlServer";
            this.textBoxFromSqlServer.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxFromSqlServer.Size = new System.Drawing.Size(1470, 710);
            this.textBoxFromSqlServer.TabIndex = 0;
            // 
            // tabPageFromHtml
            // 
            this.tabPageFromHtml.Controls.Add(this.splitContainerFromHtml);
            this.tabPageFromHtml.Location = new System.Drawing.Point(4, 29);
            this.tabPageFromHtml.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPageFromHtml.Name = "tabPageFromHtml";
            this.tabPageFromHtml.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPageFromHtml.Size = new System.Drawing.Size(1476, 888);
            this.tabPageFromHtml.TabIndex = 0;
            this.tabPageFromHtml.Tag = "Html";
            this.tabPageFromHtml.Text = "Spanish HTML";
            this.tabPageFromHtml.UseVisualStyleBackColor = true;
            // 
            // splitContainerFromHtml
            // 
            this.splitContainerFromHtml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerFromHtml.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerFromHtml.Location = new System.Drawing.Point(3, 5);
            this.splitContainerFromHtml.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.splitContainerFromHtml.Name = "splitContainerFromHtml";
            this.splitContainerFromHtml.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerFromHtml.Panel1
            // 
            this.splitContainerFromHtml.Panel1.Controls.Add(this.btSpanishEscobar);
            this.splitContainerFromHtml.Panel1.Controls.Add(this.btSpanishDownload);
            this.splitContainerFromHtml.Panel1.Controls.Add(this.btHtmlFilesPath);
            this.splitContainerFromHtml.Panel1.Controls.Add(this.lblHtmlPath);
            this.splitContainerFromHtml.Panel1.Controls.Add(this.txHtmlFilesPath);
            this.splitContainerFromHtml.Panel1.Controls.Add(this.btSpanish);
            // 
            // splitContainerFromHtml.Panel2
            // 
            this.splitContainerFromHtml.Panel2.Controls.Add(this.textBoxFromHtml);
            this.splitContainerFromHtml.Size = new System.Drawing.Size(1470, 878);
            this.splitContainerFromHtml.SplitterDistance = 163;
            this.splitContainerFromHtml.SplitterWidth = 5;
            this.splitContainerFromHtml.TabIndex = 0;
            // 
            // btSpanishEscobar
            // 
            this.btSpanishEscobar.Location = new System.Drawing.Point(312, 74);
            this.btSpanishEscobar.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btSpanishEscobar.Name = "btSpanishEscobar";
            this.btSpanishEscobar.Size = new System.Drawing.Size(150, 60);
            this.btSpanishEscobar.TabIndex = 10;
            this.btSpanishEscobar.Text = "Spanish Escobar";
            this.btSpanishEscobar.UseVisualStyleBackColor = true;
            this.btSpanishEscobar.Click += new System.EventHandler(this.btSpanishEscobar_Click);
            // 
            // btSpanishDownload
            // 
            this.btSpanishDownload.Enabled = false;
            this.btSpanishDownload.Location = new System.Drawing.Point(20, 72);
            this.btSpanishDownload.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.btHtmlFilesPath.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btHtmlFilesPath.Name = "btHtmlFilesPath";
            this.btHtmlFilesPath.Size = new System.Drawing.Size(66, 55);
            this.btHtmlFilesPath.TabIndex = 3;
            this.btHtmlFilesPath.Text = "...";
            this.btHtmlFilesPath.UseVisualStyleBackColor = true;
            this.btHtmlFilesPath.Click += new System.EventHandler(this.btHtmlFilesPath_Click);
            // 
            // lblHtmlPath
            // 
            this.lblHtmlPath.AutoSize = true;
            this.lblHtmlPath.Location = new System.Drawing.Point(16, 9);
            this.lblHtmlPath.Name = "lblHtmlPath";
            this.lblHtmlPath.Size = new System.Drawing.Size(116, 20);
            this.lblHtmlPath.TabIndex = 2;
            this.lblHtmlPath.Text = "Html Files Path";
            // 
            // txHtmlFilesPath
            // 
            this.txHtmlFilesPath.Location = new System.Drawing.Point(16, 38);
            this.txHtmlFilesPath.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txHtmlFilesPath.Name = "txHtmlFilesPath";
            this.txHtmlFilesPath.Size = new System.Drawing.Size(1213, 26);
            this.txHtmlFilesPath.TabIndex = 1;
            // 
            // btSpanish
            // 
            this.btSpanish.Enabled = false;
            this.btSpanish.Location = new System.Drawing.Point(164, 72);
            this.btSpanish.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.textBoxFromHtml.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.textBoxFromHtml.Multiline = true;
            this.textBoxFromHtml.Name = "textBoxFromHtml";
            this.textBoxFromHtml.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxFromHtml.Size = new System.Drawing.Size(1470, 710);
            this.textBoxFromHtml.TabIndex = 0;
            // 
            // tabPageUbIndex
            // 
            this.tabPageUbIndex.Controls.Add(this.splitContainer2);
            this.tabPageUbIndex.Location = new System.Drawing.Point(4, 29);
            this.tabPageUbIndex.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPageUbIndex.Name = "tabPageUbIndex";
            this.tabPageUbIndex.Size = new System.Drawing.Size(1476, 888);
            this.tabPageUbIndex.TabIndex = 2;
            this.tabPageUbIndex.Tag = "Index";
            this.tabPageUbIndex.Text = "Generate UB Index";
            this.tabPageUbIndex.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.splitContainer2.Size = new System.Drawing.Size(1476, 888);
            this.splitContainer2.SplitterDistance = 188;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 1;
            // 
            // btUfIndexDownload
            // 
            this.btUfIndexDownload.Enabled = false;
            this.btUfIndexDownload.Location = new System.Drawing.Point(16, 145);
            this.btUfIndexDownload.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.btUfIndexOutputFiles.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btUfIndexOutputFiles.Name = "btUfIndexOutputFiles";
            this.btUfIndexOutputFiles.Size = new System.Drawing.Size(66, 55);
            this.btUfIndexOutputFiles.TabIndex = 7;
            this.btUfIndexOutputFiles.Text = "...";
            this.btUfIndexOutputFiles.UseVisualStyleBackColor = true;
            this.btUfIndexOutputFiles.Click += new System.EventHandler(this.btUfIndexOutputFiles_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Output folder";
            // 
            // txUfIndexOutputFolder
            // 
            this.txUfIndexOutputFolder.Location = new System.Drawing.Point(16, 111);
            this.txUfIndexOutputFolder.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txUfIndexOutputFolder.Name = "txUfIndexOutputFolder";
            this.txUfIndexOutputFolder.Size = new System.Drawing.Size(1213, 26);
            this.txUfIndexOutputFolder.TabIndex = 5;
            // 
            // btUfIndex
            // 
            this.btUfIndex.Enabled = false;
            this.btUfIndex.Location = new System.Drawing.Point(164, 145);
            this.btUfIndex.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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
            this.btUfIndexDownloadedFiles.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btUfIndexDownloadedFiles.Name = "btUfIndexDownloadedFiles";
            this.btUfIndexDownloadedFiles.Size = new System.Drawing.Size(66, 55);
            this.btUfIndexDownloadedFiles.TabIndex = 3;
            this.btUfIndexDownloadedFiles.Text = "...";
            this.btUfIndexDownloadedFiles.UseVisualStyleBackColor = true;
            this.btUfIndexDownloadedFiles.Click += new System.EventHandler(this.btUfIndexDownloadedFiles_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Downloaded files";
            // 
            // txUfIndexDownloadeFiles
            // 
            this.txUfIndexDownloadeFiles.Location = new System.Drawing.Point(16, 49);
            this.txUfIndexDownloadeFiles.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txUfIndexDownloadeFiles.Name = "txUfIndexDownloadeFiles";
            this.txUfIndexDownloadeFiles.Size = new System.Drawing.Size(1213, 26);
            this.txUfIndexDownloadeFiles.TabIndex = 1;
            // 
            // txUbIndexMessages
            // 
            this.txUbIndexMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txUbIndexMessages.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txUbIndexMessages.Location = new System.Drawing.Point(0, 0);
            this.txUbIndexMessages.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txUbIndexMessages.Multiline = true;
            this.txUbIndexMessages.Name = "txUbIndexMessages";
            this.txUbIndexMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txUbIndexMessages.Size = new System.Drawing.Size(1476, 695);
            this.txUbIndexMessages.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 1088);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.statusStrip1);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ub Study Help Generator";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel1.PerformLayout();
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageEditTranslation.ResumeLayout(false);
            this.splitContainerPtAlternative.Panel1.ResumeLayout(false);
            this.splitContainerPtAlternative.Panel1.PerformLayout();
            this.splitContainerPtAlternative.Panel2.ResumeLayout(false);
            this.splitContainerPtAlternative.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPtAlternative)).EndInit();
            this.splitContainerPtAlternative.ResumeLayout(false);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btGetRepositoryOutputFolder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txRepositoryOutputFolder;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelPaperNumber;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelMessage;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageEditTranslation;
        private System.Windows.Forms.SplitContainer splitContainerPtAlternative;
        private System.Windows.Forms.Button btEditBookRepositoryFolder;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txEditBookRepositoryFolder;
        private System.Windows.Forms.Button btImportDocx;
        private System.Windows.Forms.Button btGenerateNotes;
        private System.Windows.Forms.Button btExportPtAlternativeDocx;
        private System.Windows.Forms.Button btPtBrIndex;
        private System.Windows.Forms.Button btEditTranslationRepositoryFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txTranslationRepositoryFolder;
        private System.Windows.Forms.Button btPTAlternativeGenerate;
        private System.Windows.Forms.TextBox txPTalternative;
        private System.Windows.Forms.TabPage tabPageFromSqlServer;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btExportFormat;
        private System.Windows.Forms.Button btCheck;
        private System.Windows.Forms.Button btGetTranslation;
        private System.Windows.Forms.Button btGetAllTranslations;
        private System.Windows.Forms.ComboBox comboBoxTranslations;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txSqlServerConnectionString;
        private System.Windows.Forms.Button btGetTranslations;
        private System.Windows.Forms.TextBox textBoxFromSqlServer;
        private System.Windows.Forms.TabPage tabPageFromHtml;
        private System.Windows.Forms.SplitContainer splitContainerFromHtml;
        private System.Windows.Forms.Button btSpanishDownload;
        private System.Windows.Forms.Button btHtmlFilesPath;
        private System.Windows.Forms.Label lblHtmlPath;
        private System.Windows.Forms.TextBox txHtmlFilesPath;
        private System.Windows.Forms.Button btSpanish;
        private System.Windows.Forms.TextBox textBoxFromHtml;
        private System.Windows.Forms.TabPage tabPageUbIndex;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btUfIndexDownload;
        private System.Windows.Forms.Button btUfIndexOutputFiles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txUfIndexOutputFolder;
        private System.Windows.Forms.Button btUfIndex;
        private System.Windows.Forms.Button btUfIndexDownloadedFiles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txUfIndexDownloadeFiles;
        private System.Windows.Forms.TextBox txUbIndexMessages;
        private System.Windows.Forms.Button btTocTable;
        private System.Windows.Forms.Button btSpanishEscobar;
        private System.Windows.Forms.Button btExportToUbHelp;
        private System.Windows.Forms.Button btRecreateTrans;
    }
}

