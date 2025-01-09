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
            this.richTextBoxEdit = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButtonPtOriginalPtAlternative = new System.Windows.Forms.RadioButton();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.btSalvar = new System.Windows.Forms.Button();
            this.checkBoxInverterComparacao = new System.Windows.Forms.CheckBox();
            this.rbPtOriginal = new System.Windows.Forms.RadioButton();
            this.rbComparaComPtAlternative = new System.Windows.Forms.RadioButton();
            this.rbPtAi = new System.Windows.Forms.RadioButton();
            this.rbComparaComPtOriginal = new System.Windows.Forms.RadioButton();
            this.btNext = new System.Windows.Forms.Button();
            this.btPrevious = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.btTranslate = new System.Windows.Forms.Button();
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 616);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1340, 22);
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
            this.toolStripStatusLabelMessages.Size = new System.Drawing.Size(1244, 17);
            this.toolStripStatusLabelMessages.Spring = true;
            this.toolStripStatusLabelMessages.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btOk
            // 
            this.btOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Enabled = false;
            this.btOk.Location = new System.Drawing.Point(1177, 2);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 2;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
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
            this.tableLayoutPanelEdit.RowCount = 2;
            this.tableLayoutPanelEdit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelEdit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelEdit.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelEdit.Size = new System.Drawing.Size(1340, 588);
            this.tableLayoutPanelEdit.TabIndex = 4;
            // 
            // webBrowserDownLeft
            // 
            this.webBrowserDownLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserDownLeft.Location = new System.Drawing.Point(13, 297);
            this.webBrowserDownLeft.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserDownLeft.Name = "webBrowserDownLeft";
            this.webBrowserDownLeft.Size = new System.Drawing.Size(654, 278);
            this.webBrowserDownLeft.TabIndex = 2;
            // 
            // webBrowserUpRight
            // 
            this.webBrowserUpRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserUpRight.Location = new System.Drawing.Point(673, 13);
            this.webBrowserUpRight.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserUpRight.Name = "webBrowserUpRight";
            this.webBrowserUpRight.Size = new System.Drawing.Size(654, 278);
            this.webBrowserUpRight.TabIndex = 1;
            // 
            // webBrowserUpLeft
            // 
            this.webBrowserUpLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserUpLeft.Location = new System.Drawing.Point(13, 13);
            this.webBrowserUpLeft.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserUpLeft.Name = "webBrowserUpLeft";
            this.webBrowserUpLeft.Size = new System.Drawing.Size(654, 278);
            this.webBrowserUpLeft.TabIndex = 0;
            // 
            // richTextBoxEdit
            // 
            this.richTextBoxEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxEdit.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxEdit.Location = new System.Drawing.Point(673, 297);
            this.richTextBoxEdit.Name = "richTextBoxEdit";
            this.richTextBoxEdit.Size = new System.Drawing.Size(654, 278);
            this.richTextBoxEdit.TabIndex = 4;
            this.richTextBoxEdit.Text = "";
            this.richTextBoxEdit.TextChanged += new System.EventHandler(this.richTextBoxEdit_TextChanged_1);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btTranslate);
            this.panel1.Controls.Add(this.radioButtonPtOriginalPtAlternative);
            this.panel1.Controls.Add(this.comboBoxStatus);
            this.panel1.Controls.Add(this.btSalvar);
            this.panel1.Controls.Add(this.checkBoxInverterComparacao);
            this.panel1.Controls.Add(this.rbPtOriginal);
            this.panel1.Controls.Add(this.rbComparaComPtAlternative);
            this.panel1.Controls.Add(this.rbPtAi);
            this.panel1.Controls.Add(this.rbComparaComPtOriginal);
            this.panel1.Controls.Add(this.btNext);
            this.panel1.Controls.Add(this.btPrevious);
            this.panel1.Controls.Add(this.btCancel);
            this.panel1.Controls.Add(this.btOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 588);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1340, 28);
            this.panel1.TabIndex = 5;
            // 
            // radioButtonPtOriginalPtAlternative
            // 
            this.radioButtonPtOriginalPtAlternative.AutoSize = true;
            this.radioButtonPtOriginalPtAlternative.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonPtOriginalPtAlternative.Location = new System.Drawing.Point(459, 5);
            this.radioButtonPtOriginalPtAlternative.Name = "radioButtonPtOriginalPtAlternative";
            this.radioButtonPtOriginalPtAlternative.Size = new System.Drawing.Size(153, 17);
            this.radioButtonPtOriginalPtAlternative.TabIndex = 51;
            this.radioButtonPtOriginalPtAlternative.Text = "Pt Original X PT Alternative";
            this.radioButtonPtOriginalPtAlternative.UseVisualStyleBackColor = true;
            this.radioButtonPtOriginalPtAlternative.CheckedChanged += new System.EventHandler(this.radioButtonPtOriginalPtAlternative_CheckedChanged);
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Items.AddRange(new object[] {
            "Starting",
            "Working",
            "Doubt",
            "Ok",
            "Closed"});
            this.comboBoxStatus.Location = new System.Drawing.Point(993, 4);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(98, 21);
            this.comboBoxStatus.TabIndex = 50;
            this.comboBoxStatus.SelectedIndexChanged += new System.EventHandler(this.comboBoxStatus_SelectedIndexChanged);
            // 
            // btSalvar
            // 
            this.btSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btSalvar.BackColor = System.Drawing.Color.Chartreuse;
            this.btSalvar.Location = new System.Drawing.Point(1097, 2);
            this.btSalvar.Name = "btSalvar";
            this.btSalvar.Size = new System.Drawing.Size(75, 23);
            this.btSalvar.TabIndex = 49;
            this.btSalvar.Text = "Salvar";
            this.btSalvar.UseVisualStyleBackColor = false;
            this.btSalvar.Click += new System.EventHandler(this.btSalvar_Click);
            // 
            // checkBoxInverterComparacao
            // 
            this.checkBoxInverterComparacao.AutoSize = true;
            this.checkBoxInverterComparacao.Location = new System.Drawing.Point(629, 6);
            this.checkBoxInverterComparacao.Name = "checkBoxInverterComparacao";
            this.checkBoxInverterComparacao.Size = new System.Drawing.Size(125, 17);
            this.checkBoxInverterComparacao.TabIndex = 48;
            this.checkBoxInverterComparacao.Text = "Inverter Comparação";
            this.checkBoxInverterComparacao.UseVisualStyleBackColor = true;
            this.checkBoxInverterComparacao.CheckedChanged += new System.EventHandler(this.checkBoxInverterComparacao_CheckedChanged);
            // 
            // rbPtOriginal
            // 
            this.rbPtOriginal.AutoSize = true;
            this.rbPtOriginal.Checked = true;
            this.rbPtOriginal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPtOriginal.Location = new System.Drawing.Point(6, 5);
            this.rbPtOriginal.Name = "rbPtOriginal";
            this.rbPtOriginal.Size = new System.Drawing.Size(77, 17);
            this.rbPtOriginal.TabIndex = 47;
            this.rbPtOriginal.TabStop = true;
            this.rbPtOriginal.Text = "PT Original";
            this.rbPtOriginal.UseVisualStyleBackColor = true;
            this.rbPtOriginal.CheckedChanged += new System.EventHandler(this.rbPtOriginal_CheckedChanged);
            // 
            // rbComparaComPtAlternative
            // 
            this.rbComparaComPtAlternative.AutoSize = true;
            this.rbComparaComPtAlternative.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbComparaComPtAlternative.Location = new System.Drawing.Point(205, 5);
            this.rbComparaComPtAlternative.Name = "rbComparaComPtAlternative";
            this.rbComparaComPtAlternative.Size = new System.Drawing.Size(111, 17);
            this.rbComparaComPtAlternative.TabIndex = 46;
            this.rbComparaComPtAlternative.Text = "AI X Pt Alternative";
            this.rbComparaComPtAlternative.UseVisualStyleBackColor = true;
            this.rbComparaComPtAlternative.CheckedChanged += new System.EventHandler(this.rbComparaComPtAlternative_CheckedChanged);
            // 
            // rbPtAi
            // 
            this.rbPtAi.AutoSize = true;
            this.rbPtAi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPtAi.Location = new System.Drawing.Point(100, 5);
            this.rbPtAi.Name = "rbPtAi";
            this.rbPtAi.Size = new System.Drawing.Size(88, 17);
            this.rbPtAi.TabIndex = 45;
            this.rbPtAi.Text = "AI Generated";
            this.rbPtAi.UseVisualStyleBackColor = true;
            this.rbPtAi.CheckedChanged += new System.EventHandler(this.rbPtAi_CheckedChanged);
            // 
            // rbComparaComPtOriginal
            // 
            this.rbComparaComPtOriginal.AutoSize = true;
            this.rbComparaComPtOriginal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbComparaComPtOriginal.Location = new System.Drawing.Point(335, 5);
            this.rbComparaComPtOriginal.Name = "rbComparaComPtOriginal";
            this.rbComparaComPtOriginal.Size = new System.Drawing.Size(96, 17);
            this.rbComparaComPtOriginal.TabIndex = 43;
            this.rbComparaComPtOriginal.Text = "AI X Pt Original";
            this.rbComparaComPtOriginal.UseVisualStyleBackColor = true;
            this.rbComparaComPtOriginal.CheckedChanged += new System.EventHandler(this.rbComparaComPtOriginal_CheckedChanged);
            // 
            // btNext
            // 
            this.btNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btNext.BackColor = System.Drawing.Color.Bisque;
            this.btNext.Location = new System.Drawing.Point(927, 2);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(58, 23);
            this.btNext.TabIndex = 42;
            this.btNext.Text = "Next";
            this.btNext.UseVisualStyleBackColor = false;
            this.btNext.Click += new System.EventHandler(this.btNext_Click);
            // 
            // btPrevious
            // 
            this.btPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btPrevious.BackColor = System.Drawing.Color.Bisque;
            this.btPrevious.Location = new System.Drawing.Point(861, 2);
            this.btPrevious.Name = "btPrevious";
            this.btPrevious.Size = new System.Drawing.Size(62, 23);
            this.btPrevious.TabIndex = 41;
            this.btPrevious.Text = "Previous";
            this.btPrevious.UseVisualStyleBackColor = false;
            this.btPrevious.Click += new System.EventHandler(this.btPrevious_Click);
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.Location = new System.Drawing.Point(1258, 2);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 3;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btTranslate
            // 
            this.btTranslate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btTranslate.BackColor = System.Drawing.Color.Bisque;
            this.btTranslate.Location = new System.Drawing.Point(793, 3);
            this.btTranslate.Name = "btTranslate";
            this.btTranslate.Size = new System.Drawing.Size(62, 23);
            this.btTranslate.TabIndex = 52;
            this.btTranslate.Text = "Traduzir";
            this.btTranslate.UseVisualStyleBackColor = false;
            this.btTranslate.Click += new System.EventHandler(this.btTranslate_Click);
            // 
            // frmEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1340, 638);
            this.Controls.Add(this.tableLayoutPanelEdit);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "frmEdit";
            this.Text = "Edit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEdit_FormClosing);
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
        private System.Windows.Forms.WebBrowser webBrowserDownLeft;
        private System.Windows.Forms.WebBrowser webBrowserUpRight;
        private System.Windows.Forms.WebBrowser webBrowserUpLeft;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelIdent;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
        private System.Windows.Forms.Button btNext;
        private System.Windows.Forms.Button btPrevious;
        private System.Windows.Forms.RichTextBox richTextBoxEdit;
        private System.Windows.Forms.RadioButton rbComparaComPtAlternative;
        private System.Windows.Forms.RadioButton rbPtAi;
        private System.Windows.Forms.RadioButton rbComparaComPtOriginal;
        private System.Windows.Forms.RadioButton rbPtOriginal;
        private System.Windows.Forms.CheckBox checkBoxInverterComparacao;
        private System.Windows.Forms.Button btSalvar;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.RadioButton radioButtonPtOriginalPtAlternative;
        private System.Windows.Forms.Button btTranslate;
    }
}