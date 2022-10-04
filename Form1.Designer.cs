
namespace TSVProject
{
    partial class TSV_Export
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
            this.exportBtn = new System.Windows.Forms.Button();
            this.dataTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // exportBtn
            // 
            this.exportBtn.Location = new System.Drawing.Point(12, 222);
            this.exportBtn.Name = "exportBtn";
            this.exportBtn.Size = new System.Drawing.Size(480, 53);
            this.exportBtn.TabIndex = 0;
            this.exportBtn.Text = "EXPORT";
            this.exportBtn.UseVisualStyleBackColor = true;
            this.exportBtn.Click += new System.EventHandler(this.exportFunc);
            // 
            // dataTextBox
            // 
            this.dataTextBox.Location = new System.Drawing.Point(12, 66);
            this.dataTextBox.Multiline = true;
            this.dataTextBox.Name = "dataTextBox";
            this.dataTextBox.Size = new System.Drawing.Size(480, 150);
            this.dataTextBox.TabIndex = 1;
            // 
            // TSV_Export
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 287);
            this.Controls.Add(this.dataTextBox);
            this.Controls.Add(this.exportBtn);
            this.Name = "TSV_Export";
            this.Text = "TSV_Screen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exportBtn;
        private System.Windows.Forms.TextBox dataTextBox;
    }
}

