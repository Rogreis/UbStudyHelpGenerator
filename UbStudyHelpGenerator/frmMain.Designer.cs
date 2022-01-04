
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
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageFromHtml = new System.Windows.Forms.TabPage();
            this.splitContainerFromHtml = new System.Windows.Forms.SplitContainer();
            this.btSpanishDownload = new System.Windows.Forms.Button();
            this.btHtmlFilesPath = new System.Windows.Forms.Button();
            this.lblHtmlPath = new System.Windows.Forms.Label();
            this.txHtmlFilesPath = new System.Windows.Forms.TextBox();
            this.btSpanish = new System.Windows.Forms.Button();
            this.textBoxFromHtml = new System.Windows.Forms.TextBox();
            this.tabPageFromSqlServer = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btOutputFilesFromSqlServer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txOutputHttmlFilesFromSqlServer = new System.Windows.Forms.TextBox();
            this.btGenerateFromSql = new System.Windows.Forms.Button();
            this.textBoxFromSqlServer = new System.Windows.Forms.TextBox();
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
            this.btGetRepositoryOutputFolder = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txRepositoryOutputFolder = new System.Windows.Forms.TextBox();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.tabControlMain.SuspendLayout();
            this.tabPageFromHtml.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerFromHtml)).BeginInit();
            this.splitContainerFromHtml.Panel1.SuspendLayout();
            this.splitContainerFromHtml.Panel2.SuspendLayout();
            this.splitContainerFromHtml.SuspendLayout();
            this.tabPageFromSqlServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPageUbIndex.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 1060);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1737, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageFromHtml);
            this.tabControlMain.Controls.Add(this.tabPageFromSqlServer);
            this.tabControlMain.Controls.Add(this.tabPageUbIndex);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1737, 947);
            this.tabControlMain.TabIndex = 1;
            // 
            // tabPageFromHtml
            // 
            this.tabPageFromHtml.Controls.Add(this.splitContainerFromHtml);
            this.tabPageFromHtml.Location = new System.Drawing.Point(4, 25);
            this.tabPageFromHtml.Name = "tabPageFromHtml";
            this.tabPageFromHtml.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFromHtml.Size = new System.Drawing.Size(1729, 918);
            this.tabPageFromHtml.TabIndex = 0;
            this.tabPageFromHtml.Text = "From HTML";
            this.tabPageFromHtml.UseVisualStyleBackColor = true;
            // 
            // splitContainerFromHtml
            // 
            this.splitContainerFromHtml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerFromHtml.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerFromHtml.Location = new System.Drawing.Point(3, 3);
            this.splitContainerFromHtml.Name = "splitContainerFromHtml";
            this.splitContainerFromHtml.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerFromHtml.Panel1
            // 
            this.splitContainerFromHtml.Panel1.Controls.Add(this.btSpanishDownload);
            this.splitContainerFromHtml.Panel1.Controls.Add(this.btHtmlFilesPath);
            this.splitContainerFromHtml.Panel1.Controls.Add(this.lblHtmlPath);
            this.splitContainerFromHtml.Panel1.Controls.Add(this.txHtmlFilesPath);
            this.splitContainerFromHtml.Panel1.Controls.Add(this.btSpanish);
            // 
            // splitContainerFromHtml.Panel2
            // 
            this.splitContainerFromHtml.Panel2.Controls.Add(this.textBoxFromHtml);
            this.splitContainerFromHtml.Size = new System.Drawing.Size(1723, 912);
            this.splitContainerFromHtml.SplitterDistance = 163;
            this.splitContainerFromHtml.TabIndex = 0;
            // 
            // btSpanishDownload
            // 
            this.btSpanishDownload.Enabled = false;
            this.btSpanishDownload.Location = new System.Drawing.Point(15, 106);
            this.btSpanishDownload.Name = "btSpanishDownload";
            this.btSpanishDownload.Size = new System.Drawing.Size(112, 48);
            this.btSpanishDownload.TabIndex = 9;
            this.btSpanishDownload.Text = "Download";
            this.btSpanishDownload.UseVisualStyleBackColor = true;
            this.btSpanishDownload.Click += new System.EventHandler(this.btSpanishDownload_Click);
            // 
            // btHtmlFilesPath
            // 
            this.btHtmlFilesPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btHtmlFilesPath.Location = new System.Drawing.Point(1100, 16);
            this.btHtmlFilesPath.Name = "btHtmlFilesPath";
            this.btHtmlFilesPath.Size = new System.Drawing.Size(59, 45);
            this.btHtmlFilesPath.TabIndex = 3;
            this.btHtmlFilesPath.Text = "...";
            this.btHtmlFilesPath.UseVisualStyleBackColor = true;
            this.btHtmlFilesPath.Click += new System.EventHandler(this.btHtmlFilesPath_Click);
            // 
            // lblHtmlPath
            // 
            this.lblHtmlPath.AutoSize = true;
            this.lblHtmlPath.Location = new System.Drawing.Point(15, 7);
            this.lblHtmlPath.Name = "lblHtmlPath";
            this.lblHtmlPath.Size = new System.Drawing.Size(102, 17);
            this.lblHtmlPath.TabIndex = 2;
            this.lblHtmlPath.Text = "Html Files Path";
            // 
            // txHtmlFilesPath
            // 
            this.txHtmlFilesPath.Location = new System.Drawing.Point(15, 30);
            this.txHtmlFilesPath.Name = "txHtmlFilesPath";
            this.txHtmlFilesPath.Size = new System.Drawing.Size(1079, 22);
            this.txHtmlFilesPath.TabIndex = 1;
            // 
            // btSpanish
            // 
            this.btSpanish.Location = new System.Drawing.Point(144, 106);
            this.btSpanish.Name = "btSpanish";
            this.btSpanish.Size = new System.Drawing.Size(112, 48);
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
            this.textBoxFromHtml.Multiline = true;
            this.textBoxFromHtml.Name = "textBoxFromHtml";
            this.textBoxFromHtml.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxFromHtml.Size = new System.Drawing.Size(1723, 745);
            this.textBoxFromHtml.TabIndex = 0;
            // 
            // tabPageFromSqlServer
            // 
            this.tabPageFromSqlServer.Controls.Add(this.splitContainer1);
            this.tabPageFromSqlServer.Location = new System.Drawing.Point(4, 25);
            this.tabPageFromSqlServer.Name = "tabPageFromSqlServer";
            this.tabPageFromSqlServer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFromSqlServer.Size = new System.Drawing.Size(1180, 871);
            this.tabPageFromSqlServer.TabIndex = 1;
            this.tabPageFromSqlServer.Text = "From Sql Server";
            this.tabPageFromSqlServer.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btOutputFilesFromSqlServer);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.txOutputHttmlFilesFromSqlServer);
            this.splitContainer1.Panel1.Controls.Add(this.btGenerateFromSql);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBoxFromSqlServer);
            this.splitContainer1.Size = new System.Drawing.Size(1174, 865);
            this.splitContainer1.SplitterDistance = 163;
            this.splitContainer1.TabIndex = 1;
            // 
            // btOutputFilesFromSqlServer
            // 
            this.btOutputFilesFromSqlServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btOutputFilesFromSqlServer.Location = new System.Drawing.Point(1100, 25);
            this.btOutputFilesFromSqlServer.Name = "btOutputFilesFromSqlServer";
            this.btOutputFilesFromSqlServer.Size = new System.Drawing.Size(59, 45);
            this.btOutputFilesFromSqlServer.TabIndex = 3;
            this.btOutputFilesFromSqlServer.Text = "...";
            this.btOutputFilesFromSqlServer.UseVisualStyleBackColor = true;
            this.btOutputFilesFromSqlServer.Click += new System.EventHandler(this.btOutputFilesFromSqlServer_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Output Html Files Path";
            // 
            // txOutputHttmlFilesFromSqlServer
            // 
            this.txOutputHttmlFilesFromSqlServer.Location = new System.Drawing.Point(15, 39);
            this.txOutputHttmlFilesFromSqlServer.Name = "txOutputHttmlFilesFromSqlServer";
            this.txOutputHttmlFilesFromSqlServer.Size = new System.Drawing.Size(1079, 22);
            this.txOutputHttmlFilesFromSqlServer.TabIndex = 1;
            // 
            // btGenerateFromSql
            // 
            this.btGenerateFromSql.Location = new System.Drawing.Point(15, 81);
            this.btGenerateFromSql.Name = "btGenerateFromSql";
            this.btGenerateFromSql.Size = new System.Drawing.Size(112, 48);
            this.btGenerateFromSql.TabIndex = 0;
            this.btGenerateFromSql.Text = "Generate";
            this.btGenerateFromSql.UseVisualStyleBackColor = true;
            // 
            // textBoxFromSqlServer
            // 
            this.textBoxFromSqlServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxFromSqlServer.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFromSqlServer.Location = new System.Drawing.Point(0, 0);
            this.textBoxFromSqlServer.Multiline = true;
            this.textBoxFromSqlServer.Name = "textBoxFromSqlServer";
            this.textBoxFromSqlServer.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxFromSqlServer.Size = new System.Drawing.Size(1174, 698);
            this.textBoxFromSqlServer.TabIndex = 0;
            // 
            // tabPageUbIndex
            // 
            this.tabPageUbIndex.Controls.Add(this.splitContainer2);
            this.tabPageUbIndex.Location = new System.Drawing.Point(4, 25);
            this.tabPageUbIndex.Name = "tabPageUbIndex";
            this.tabPageUbIndex.Size = new System.Drawing.Size(1180, 871);
            this.tabPageUbIndex.TabIndex = 2;
            this.tabPageUbIndex.Text = "UB Index";
            this.tabPageUbIndex.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
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
            this.splitContainer2.Size = new System.Drawing.Size(1180, 871);
            this.splitContainer2.SplitterDistance = 188;
            this.splitContainer2.TabIndex = 1;
            // 
            // btUfIndexDownload
            // 
            this.btUfIndexDownload.Location = new System.Drawing.Point(15, 116);
            this.btUfIndexDownload.Name = "btUfIndexDownload";
            this.btUfIndexDownload.Size = new System.Drawing.Size(112, 48);
            this.btUfIndexDownload.TabIndex = 8;
            this.btUfIndexDownload.Text = "Download";
            this.btUfIndexDownload.UseVisualStyleBackColor = true;
            this.btUfIndexDownload.Click += new System.EventHandler(this.btDownload_Click);
            // 
            // btUfIndexOutputFiles
            // 
            this.btUfIndexOutputFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btUfIndexOutputFiles.Location = new System.Drawing.Point(1100, 74);
            this.btUfIndexOutputFiles.Name = "btUfIndexOutputFiles";
            this.btUfIndexOutputFiles.Size = new System.Drawing.Size(59, 45);
            this.btUfIndexOutputFiles.TabIndex = 7;
            this.btUfIndexOutputFiles.Text = "...";
            this.btUfIndexOutputFiles.UseVisualStyleBackColor = true;
            this.btUfIndexOutputFiles.Click += new System.EventHandler(this.btUfIndexOutputFiles_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Output folder";
            // 
            // txUfIndexOutputFolder
            // 
            this.txUfIndexOutputFolder.Location = new System.Drawing.Point(15, 88);
            this.txUfIndexOutputFolder.Name = "txUfIndexOutputFolder";
            this.txUfIndexOutputFolder.Size = new System.Drawing.Size(1079, 22);
            this.txUfIndexOutputFolder.TabIndex = 5;
            // 
            // btUfIndex
            // 
            this.btUfIndex.Location = new System.Drawing.Point(145, 116);
            this.btUfIndex.Name = "btUfIndex";
            this.btUfIndex.Size = new System.Drawing.Size(112, 48);
            this.btUfIndex.TabIndex = 4;
            this.btUfIndex.Text = "Generate";
            this.btUfIndex.UseVisualStyleBackColor = true;
            this.btUfIndex.Click += new System.EventHandler(this.btUfIndex_Click);
            // 
            // btUfIndexDownloadedFiles
            // 
            this.btUfIndexDownloadedFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btUfIndexDownloadedFiles.Location = new System.Drawing.Point(1100, 25);
            this.btUfIndexDownloadedFiles.Name = "btUfIndexDownloadedFiles";
            this.btUfIndexDownloadedFiles.Size = new System.Drawing.Size(59, 45);
            this.btUfIndexDownloadedFiles.TabIndex = 3;
            this.btUfIndexDownloadedFiles.Text = "...";
            this.btUfIndexDownloadedFiles.UseVisualStyleBackColor = true;
            this.btUfIndexDownloadedFiles.Click += new System.EventHandler(this.btUfIndexDownloadedFiles_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Downloaded files";
            // 
            // txUfIndexDownloadeFiles
            // 
            this.txUfIndexDownloadeFiles.Location = new System.Drawing.Point(15, 39);
            this.txUfIndexDownloadeFiles.Name = "txUfIndexDownloadeFiles";
            this.txUfIndexDownloadeFiles.Size = new System.Drawing.Size(1079, 22);
            this.txUfIndexDownloadeFiles.TabIndex = 1;
            // 
            // txUbIndexMessages
            // 
            this.txUbIndexMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txUbIndexMessages.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txUbIndexMessages.Location = new System.Drawing.Point(0, 0);
            this.txUbIndexMessages.Multiline = true;
            this.txUbIndexMessages.Name = "txUbIndexMessages";
            this.txUbIndexMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txUbIndexMessages.Size = new System.Drawing.Size(1180, 679);
            this.txUbIndexMessages.TabIndex = 0;
            // 
            // btGetRepositoryOutputFolder
            // 
            this.btGetRepositoryOutputFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btGetRepositoryOutputFolder.Location = new System.Drawing.Point(1097, 25);
            this.btGetRepositoryOutputFolder.Name = "btGetRepositoryOutputFolder";
            this.btGetRepositoryOutputFolder.Size = new System.Drawing.Size(59, 45);
            this.btGetRepositoryOutputFolder.TabIndex = 12;
            this.btGetRepositoryOutputFolder.Text = "...";
            this.btGetRepositoryOutputFolder.UseVisualStyleBackColor = true;
            this.btGetRepositoryOutputFolder.Click += new System.EventHandler(this.btGetRepositoryOutputFolder_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Repository Output Folder";
            // 
            // txRepositoryOutputFolder
            // 
            this.txRepositoryOutputFolder.Location = new System.Drawing.Point(12, 39);
            this.txRepositoryOutputFolder.Name = "txRepositoryOutputFolder";
            this.txRepositoryOutputFolder.Size = new System.Drawing.Size(1079, 22);
            this.txRepositoryOutputFolder.TabIndex = 10;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
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
            this.splitContainerMain.Size = new System.Drawing.Size(1737, 1060);
            this.splitContainerMain.SplitterDistance = 109;
            this.splitContainerMain.TabIndex = 2;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1737, 1082);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.statusStrip1);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ub Study Help Generator";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageFromHtml.ResumeLayout(false);
            this.splitContainerFromHtml.Panel1.ResumeLayout(false);
            this.splitContainerFromHtml.Panel1.PerformLayout();
            this.splitContainerFromHtml.Panel2.ResumeLayout(false);
            this.splitContainerFromHtml.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerFromHtml)).EndInit();
            this.splitContainerFromHtml.ResumeLayout(false);
            this.tabPageFromSqlServer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPageUbIndex.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
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
        private System.Windows.Forms.Button btOutputFilesFromSqlServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txOutputHttmlFilesFromSqlServer;
        private System.Windows.Forms.Button btGenerateFromSql;
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
    }
}

