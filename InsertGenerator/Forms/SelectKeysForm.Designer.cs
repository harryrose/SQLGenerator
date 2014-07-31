namespace InsertGenerator.Forms
{
    partial class SelectKeysForm
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
            this.chooseKeyColumns = new System.Windows.Forms.GroupBox();
            this.columnListBox = new System.Windows.Forms.CheckedListBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.chooseKeyColumns.SuspendLayout();
            this.SuspendLayout();
            // 
            // chooseKeyColumns
            // 
            this.chooseKeyColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chooseKeyColumns.Controls.Add(this.columnListBox);
            this.chooseKeyColumns.Location = new System.Drawing.Point(12, 12);
            this.chooseKeyColumns.Name = "chooseKeyColumns";
            this.chooseKeyColumns.Size = new System.Drawing.Size(260, 209);
            this.chooseKeyColumns.TabIndex = 0;
            this.chooseKeyColumns.TabStop = false;
            this.chooseKeyColumns.Text = "Choose Key Columns";
            // 
            // columnListBox
            // 
            this.columnListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.columnListBox.FormattingEnabled = true;
            this.columnListBox.Location = new System.Drawing.Point(3, 16);
            this.columnListBox.Name = "columnListBox";
            this.columnListBox.Size = new System.Drawing.Size(254, 190);
            this.columnListBox.TabIndex = 0;
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.Location = new System.Drawing.Point(197, 227);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 1;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // SelectKeysForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.chooseKeyColumns);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectKeysForm";
            this.Text = "Select Keys";
            this.chooseKeyColumns.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox chooseKeyColumns;
        private System.Windows.Forms.CheckedListBox columnListBox;
        private System.Windows.Forms.Button OKButton;
    }
}