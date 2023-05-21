namespace UbStudyHelpGenerator
{
    partial class frmEdit
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
            this.panelBotton = new System.Windows.Forms.Panel();
            this.btHistoryTrack = new System.Windows.Forms.Button();
            this.btClosed = new System.Windows.Forms.Button();
            this.btDoubt = new System.Windows.Forms.Button();
            this.btDone = new System.Windows.Forms.Button();
            this.btWorking = new System.Windows.Forms.Button();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.splitContainerMainEdit = new System.Windows.Forms.SplitContainer();
            this.splitContainerLeft = new System.Windows.Forms.SplitContainer();
            this.btRemoveSelectLine = new System.Windows.Forms.Button();
            this.listBoxLines = new System.Windows.Forms.ListBox();
            this.labelReferenceEdited = new System.Windows.Forms.Label();
            this.btSearchForReferenceFile = new System.Windows.Forms.Button();
            this.textBoxMessages = new System.Windows.Forms.TextBox();
            this.splitContainerEdit = new System.Windows.Forms.SplitContainer();
            this.chromiumWebBrowser1 = new CefSharp.WinForms.ChromiumWebBrowser();
            this.panelBotton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMainEdit)).BeginInit();
            this.splitContainerMainEdit.Panel1.SuspendLayout();
            this.splitContainerMainEdit.Panel2.SuspendLayout();
            this.splitContainerMainEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeft)).BeginInit();
            this.splitContainerLeft.Panel1.SuspendLayout();
            this.splitContainerLeft.Panel2.SuspendLayout();
            this.splitContainerLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEdit)).BeginInit();
            this.splitContainerEdit.Panel1.SuspendLayout();
            this.splitContainerEdit.Panel2.SuspendLayout();
            this.splitContainerEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBotton
            // 
            this.panelBotton.Controls.Add(this.btHistoryTrack);
            this.panelBotton.Controls.Add(this.btClosed);
            this.panelBotton.Controls.Add(this.btDoubt);
            this.panelBotton.Controls.Add(this.btDone);
            this.panelBotton.Controls.Add(this.btWorking);
            this.panelBotton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotton.Location = new System.Drawing.Point(0, 998);
            this.panelBotton.Name = "panelBotton";
            this.panelBotton.Size = new System.Drawing.Size(1628, 64);
            this.panelBotton.TabIndex = 3;
            // 
            // btHistoryTrack
            // 
            this.btHistoryTrack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btHistoryTrack.Location = new System.Drawing.Point(172, 8);
            this.btHistoryTrack.Name = "btHistoryTrack";
            this.btHistoryTrack.Size = new System.Drawing.Size(154, 48);
            this.btHistoryTrack.TabIndex = 8;
            this.btHistoryTrack.Text = "History Track";
            this.btHistoryTrack.UseVisualStyleBackColor = true;
            this.btHistoryTrack.Click += new System.EventHandler(this.btHistoryTrack_Click);
            // 
            // btClosed
            // 
            this.btClosed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btClosed.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btClosed.Location = new System.Drawing.Point(1523, 8);
            this.btClosed.Name = "btClosed";
            this.btClosed.Size = new System.Drawing.Size(93, 48);
            this.btClosed.TabIndex = 5;
            this.btClosed.Text = "Closed";
            this.btClosed.UseVisualStyleBackColor = false;
            this.btClosed.Click += new System.EventHandler(this.btClosed_Click);
            // 
            // btDoubt
            // 
            this.btDoubt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btDoubt.BackColor = System.Drawing.Color.Salmon;
            this.btDoubt.Location = new System.Drawing.Point(1430, 8);
            this.btDoubt.Name = "btDoubt";
            this.btDoubt.Size = new System.Drawing.Size(93, 48);
            this.btDoubt.TabIndex = 4;
            this.btDoubt.Text = "Doubt";
            this.btDoubt.UseVisualStyleBackColor = false;
            this.btDoubt.Click += new System.EventHandler(this.btDoubt_Click);
            // 
            // btDone
            // 
            this.btDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btDone.BackColor = System.Drawing.Color.Chartreuse;
            this.btDone.Location = new System.Drawing.Point(1336, 8);
            this.btDone.Name = "btDone";
            this.btDone.Size = new System.Drawing.Size(93, 48);
            this.btDone.TabIndex = 3;
            this.btDone.Text = "Done";
            this.btDone.UseVisualStyleBackColor = false;
            this.btDone.Click += new System.EventHandler(this.btDone_Click);
            // 
            // btWorking
            // 
            this.btWorking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btWorking.BackColor = System.Drawing.Color.NavajoWhite;
            this.btWorking.Location = new System.Drawing.Point(1241, 8);
            this.btWorking.Name = "btWorking";
            this.btWorking.Size = new System.Drawing.Size(93, 48);
            this.btWorking.TabIndex = 2;
            this.btWorking.Text = "Working";
            this.btWorking.UseVisualStyleBackColor = false;
            this.btWorking.Click += new System.EventHandler(this.btWorking_Click);
            // 
            // textBoxText
            // 
            this.textBoxText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxText.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxText.HideSelection = false;
            this.textBoxText.Location = new System.Drawing.Point(0, 0);
            this.textBoxText.Multiline = true;
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxText.Size = new System.Drawing.Size(1083, 556);
            this.textBoxText.TabIndex = 0;
            this.textBoxText.TextChanged += new System.EventHandler(this.textBoxText_TextChanged);
            // 
            // splitContainerMainEdit
            // 
            this.splitContainerMainEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMainEdit.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMainEdit.Name = "splitContainerMainEdit";
            // 
            // splitContainerMainEdit.Panel1
            // 
            this.splitContainerMainEdit.Panel1.Controls.Add(this.splitContainerLeft);
            // 
            // splitContainerMainEdit.Panel2
            // 
            this.splitContainerMainEdit.Panel2.Controls.Add(this.splitContainerEdit);
            this.splitContainerMainEdit.Size = new System.Drawing.Size(1628, 998);
            this.splitContainerMainEdit.SplitterDistance = 541;
            this.splitContainerMainEdit.TabIndex = 5;
            // 
            // splitContainerLeft
            // 
            this.splitContainerLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerLeft.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerLeft.Location = new System.Drawing.Point(0, 0);
            this.splitContainerLeft.Name = "splitContainerLeft";
            this.splitContainerLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerLeft.Panel1
            // 
            this.splitContainerLeft.Panel1.Controls.Add(this.btRemoveSelectLine);
            this.splitContainerLeft.Panel1.Controls.Add(this.listBoxLines);
            this.splitContainerLeft.Panel1.Controls.Add(this.labelReferenceEdited);
            this.splitContainerLeft.Panel1.Controls.Add(this.btSearchForReferenceFile);
            // 
            // splitContainerLeft.Panel2
            // 
            this.splitContainerLeft.Panel2.Controls.Add(this.textBoxMessages);
            this.splitContainerLeft.Size = new System.Drawing.Size(541, 998);
            this.splitContainerLeft.SplitterDistance = 712;
            this.splitContainerLeft.TabIndex = 0;
            // 
            // btRemoveSelectLine
            // 
            this.btRemoveSelectLine.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btRemoveSelectLine.Location = new System.Drawing.Point(16, 488);
            this.btRemoveSelectLine.Name = "btRemoveSelectLine";
            this.btRemoveSelectLine.Size = new System.Drawing.Size(447, 66);
            this.btRemoveSelectLine.TabIndex = 3;
            this.btRemoveSelectLine.Text = "Remove Selected Line";
            this.btRemoveSelectLine.UseVisualStyleBackColor = true;
            this.btRemoveSelectLine.Click += new System.EventHandler(this.btRemoveSelectLine_Click);
            // 
            // listBoxLines
            // 
            this.listBoxLines.Dock = System.Windows.Forms.DockStyle.Top;
            this.listBoxLines.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxLines.FormattingEnabled = true;
            this.listBoxLines.ItemHeight = 33;
            this.listBoxLines.Location = new System.Drawing.Point(0, 0);
            this.listBoxLines.Name = "listBoxLines";
            this.listBoxLines.Size = new System.Drawing.Size(541, 334);
            this.listBoxLines.TabIndex = 2;
            this.listBoxLines.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxLines_MouseDoubleClick);
            // 
            // labelReferenceEdited
            // 
            this.labelReferenceEdited.AutoSize = true;
            this.labelReferenceEdited.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReferenceEdited.Location = new System.Drawing.Point(8, 374);
            this.labelReferenceEdited.Name = "labelReferenceEdited";
            this.labelReferenceEdited.Size = new System.Drawing.Size(92, 32);
            this.labelReferenceEdited.TabIndex = 1;
            this.labelReferenceEdited.Text = "label1";
            // 
            // btSearchForReferenceFile
            // 
            this.btSearchForReferenceFile.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSearchForReferenceFile.Location = new System.Drawing.Point(16, 414);
            this.btSearchForReferenceFile.Name = "btSearchForReferenceFile";
            this.btSearchForReferenceFile.Size = new System.Drawing.Size(447, 68);
            this.btSearchForReferenceFile.TabIndex = 0;
            this.btSearchForReferenceFile.Text = "Search Reference File";
            this.btSearchForReferenceFile.UseVisualStyleBackColor = true;
            this.btSearchForReferenceFile.Click += new System.EventHandler(this.btSearchForReferenceFile_Click);
            // 
            // textBoxMessages
            // 
            this.textBoxMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMessages.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMessages.Location = new System.Drawing.Point(0, 0);
            this.textBoxMessages.Multiline = true;
            this.textBoxMessages.Name = "textBoxMessages";
            this.textBoxMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxMessages.Size = new System.Drawing.Size(541, 282);
            this.textBoxMessages.TabIndex = 0;
            // 
            // splitContainerEdit
            // 
            this.splitContainerEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerEdit.Location = new System.Drawing.Point(0, 0);
            this.splitContainerEdit.Name = "splitContainerEdit";
            this.splitContainerEdit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerEdit.Panel1
            // 
            this.splitContainerEdit.Panel1.Controls.Add(this.chromiumWebBrowser1);
            // 
            // splitContainerEdit.Panel2
            // 
            this.splitContainerEdit.Panel2.Controls.Add(this.textBoxText);
            this.splitContainerEdit.Size = new System.Drawing.Size(1083, 998);
            this.splitContainerEdit.SplitterDistance = 438;
            this.splitContainerEdit.TabIndex = 0;
            // 
            // chromiumWebBrowser1
            // 
            this.chromiumWebBrowser1.ActivateBrowserOnCreation = false;
            this.chromiumWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chromiumWebBrowser1.Location = new System.Drawing.Point(0, 0);
            this.chromiumWebBrowser1.Name = "chromiumWebBrowser1";
            this.chromiumWebBrowser1.Size = new System.Drawing.Size(1083, 438);
            this.chromiumWebBrowser1.TabIndex = 0;
            // 
            // frmEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1628, 1062);
            this.Controls.Add(this.splitContainerMainEdit);
            this.Controls.Add(this.panelBotton);
            this.Name = "frmEdit";
            this.Text = "frmEdit";
            this.Load += new System.EventHandler(this.frmEdit_Load_1);
            this.panelBotton.ResumeLayout(false);
            this.splitContainerMainEdit.Panel1.ResumeLayout(false);
            this.splitContainerMainEdit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMainEdit)).EndInit();
            this.splitContainerMainEdit.ResumeLayout(false);
            this.splitContainerLeft.Panel1.ResumeLayout(false);
            this.splitContainerLeft.Panel1.PerformLayout();
            this.splitContainerLeft.Panel2.ResumeLayout(false);
            this.splitContainerLeft.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeft)).EndInit();
            this.splitContainerLeft.ResumeLayout(false);
            this.splitContainerEdit.Panel1.ResumeLayout(false);
            this.splitContainerEdit.Panel2.ResumeLayout(false);
            this.splitContainerEdit.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerEdit)).EndInit();
            this.splitContainerEdit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBotton;
        private System.Windows.Forms.Button btHistoryTrack;
        private System.Windows.Forms.Button btClosed;
        private System.Windows.Forms.Button btDoubt;
        private System.Windows.Forms.Button btDone;
        private System.Windows.Forms.Button btWorking;
        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.SplitContainer splitContainerMainEdit;
        private System.Windows.Forms.SplitContainer splitContainerEdit;
        private CefSharp.WinForms.ChromiumWebBrowser chromiumWebBrowser1;
        private System.Windows.Forms.SplitContainer splitContainerLeft;
        private System.Windows.Forms.Button btRemoveSelectLine;
        private System.Windows.Forms.ListBox listBoxLines;
        private System.Windows.Forms.Label labelReferenceEdited;
        private System.Windows.Forms.Button btSearchForReferenceFile;
        private System.Windows.Forms.TextBox textBoxMessages;
    }
}