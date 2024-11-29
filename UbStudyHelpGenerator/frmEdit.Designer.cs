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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelIdent = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelMessages = new System.Windows.Forms.ToolStripStatusLabel();
            this.btOk = new System.Windows.Forms.Button();
            this.tableLayoutPanelEdit = new System.Windows.Forms.TableLayoutPanel();
            this.webBrowserDownLeft = new System.Windows.Forms.WebBrowser();
            this.webBrowserUpRight = new System.Windows.Forms.WebBrowser();
            this.webBrowserUpLeft = new System.Windows.Forms.WebBrowser();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btNext = new System.Windows.Forms.Button();
            this.btPrevious = new System.Windows.Forms.Button();
            this.checkBoxShowCompare = new System.Windows.Forms.CheckBox();
            this.lblIdentificacao = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.richTextBoxEdit = new System.Windows.Forms.RichTextBox();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanelEdit.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelIdent,
            this.toolStripStatusLabelStatus,
            this.toolStripStatusLabelMessages});
            this.statusStrip1.Location = new System.Drawing.Point(0, 617);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1235, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelIdent
            // 
            this.toolStripStatusLabelIdent.Name = "toolStripStatusLabelIdent";
            this.toolStripStatusLabelIdent.Size = new System.Drawing.Size(33, 17);
            this.toolStripStatusLabelIdent.Text = "0:0-0";
            // 
            // toolStripStatusLabelStatus
            // 
            this.toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
            this.toolStripStatusLabelStatus.Size = new System.Drawing.Size(48, 17);
            this.toolStripStatusLabelStatus.Text = "Starting";
            // 
            // toolStripStatusLabelMessages
            // 
            this.toolStripStatusLabelMessages.Name = "toolStripStatusLabelMessages";
            this.toolStripStatusLabelMessages.Size = new System.Drawing.Size(1139, 17);
            this.toolStripStatusLabelMessages.Spring = true;
            this.toolStripStatusLabelMessages.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btOk
            // 
            this.btOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Location = new System.Drawing.Point(1072, 2);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 2;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelEdit
            // 
            this.tableLayoutPanelEdit.ColumnCount = 2;
            this.tableLayoutPanelEdit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelEdit.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelEdit.Controls.Add(this.webBrowserDownLeft, 0, 1);
            this.tableLayoutPanelEdit.Controls.Add(this.webBrowserUpRight, 1, 0);
            this.tableLayoutPanelEdit.Controls.Add(this.webBrowserUpLeft, 0, 0);
            this.tableLayoutPanelEdit.Controls.Add(this.richTextBoxEdit, 1, 1);
            this.tableLayoutPanelEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelEdit.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelEdit.Margin = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanelEdit.Name = "tableLayoutPanelEdit";
            this.tableLayoutPanelEdit.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanelEdit.RowCount = 3;
            this.tableLayoutPanelEdit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelEdit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelEdit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelEdit.Size = new System.Drawing.Size(1235, 589);
            this.tableLayoutPanelEdit.TabIndex = 4;
            // 
            // webBrowserDownLeft
            // 
            this.webBrowserDownLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserDownLeft.Location = new System.Drawing.Point(13, 287);
            this.webBrowserDownLeft.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserDownLeft.Name = "webBrowserDownLeft";
            this.webBrowserDownLeft.Size = new System.Drawing.Size(601, 268);
            this.webBrowserDownLeft.TabIndex = 2;
            // 
            // webBrowserUpRight
            // 
            this.webBrowserUpRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserUpRight.Location = new System.Drawing.Point(620, 13);
            this.webBrowserUpRight.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserUpRight.Name = "webBrowserUpRight";
            this.webBrowserUpRight.Size = new System.Drawing.Size(602, 268);
            this.webBrowserUpRight.TabIndex = 1;
            // 
            // webBrowserUpLeft
            // 
            this.webBrowserUpLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserUpLeft.Location = new System.Drawing.Point(13, 13);
            this.webBrowserUpLeft.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserUpLeft.Name = "webBrowserUpLeft";
            this.webBrowserUpLeft.Size = new System.Drawing.Size(601, 268);
            this.webBrowserUpLeft.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btNext);
            this.panel1.Controls.Add(this.btPrevious);
            this.panel1.Controls.Add(this.checkBoxShowCompare);
            this.panel1.Controls.Add(this.lblIdentificacao);
            this.panel1.Controls.Add(this.btCancel);
            this.panel1.Controls.Add(this.btOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 589);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1235, 28);
            this.panel1.TabIndex = 5;
            // 
            // btNext
            // 
            this.btNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btNext.Location = new System.Drawing.Point(580, 2);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(75, 23);
            this.btNext.TabIndex = 42;
            this.btNext.Text = "Next";
            this.btNext.UseVisualStyleBackColor = true;
            this.btNext.Click += new System.EventHandler(this.btNext_Click);
            // 
            // btPrevious
            // 
            this.btPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrevious.Location = new System.Drawing.Point(499, 2);
            this.btPrevious.Name = "btPrevious";
            this.btPrevious.Size = new System.Drawing.Size(75, 23);
            this.btPrevious.TabIndex = 41;
            this.btPrevious.Text = "Previous";
            this.btPrevious.UseVisualStyleBackColor = true;
            this.btPrevious.Click += new System.EventHandler(this.btPrevious_Click);
            // 
            // checkBoxShowCompare
            // 
            this.checkBoxShowCompare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxShowCompare.AutoSize = true;
            this.checkBoxShowCompare.Location = new System.Drawing.Point(968, 6);
            this.checkBoxShowCompare.Name = "checkBoxShowCompare";
            this.checkBoxShowCompare.Size = new System.Drawing.Size(98, 17);
            this.checkBoxShowCompare.TabIndex = 40;
            this.checkBoxShowCompare.Text = "Show Compare";
            this.checkBoxShowCompare.UseVisualStyleBackColor = true;
            this.checkBoxShowCompare.CheckedChanged += new System.EventHandler(this.checkBoxShowCompare_CheckedChanged);
            // 
            // lblIdentificacao
            // 
            this.lblIdentificacao.AutoSize = true;
            this.lblIdentificacao.Location = new System.Drawing.Point(3, 7);
            this.lblIdentificacao.Name = "lblIdentificacao";
            this.lblIdentificacao.Size = new System.Drawing.Size(78, 13);
            this.lblIdentificacao.TabIndex = 4;
            this.lblIdentificacao.Text = "lblIdentificacao";
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(1153, 2);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 3;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // richTextBoxEdit
            // 
            this.richTextBoxEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxEdit.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxEdit.Location = new System.Drawing.Point(620, 287);
            this.richTextBoxEdit.Name = "richTextBoxEdit";
            this.richTextBoxEdit.Size = new System.Drawing.Size(602, 268);
            this.richTextBoxEdit.TabIndex = 4;
            this.richTextBoxEdit.Text = "";
            // 
            // frmEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1235, 639);
            this.Controls.Add(this.tableLayoutPanelEdit);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "frmEdit";
            this.Text = "Edit";
            this.Load += new System.EventHandler(this.frmEdit_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanelEdit.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelEdit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelMessages;
        private System.Windows.Forms.Label lblIdentificacao;
        private System.Windows.Forms.WebBrowser webBrowserDownLeft;
        private System.Windows.Forms.WebBrowser webBrowserUpRight;
        private System.Windows.Forms.WebBrowser webBrowserUpLeft;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelIdent;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
        private System.Windows.Forms.CheckBox checkBoxShowCompare;
        private System.Windows.Forms.Button btNext;
        private System.Windows.Forms.Button btPrevious;
        private System.Windows.Forms.RichTextBox richTextBoxEdit;
    }
}