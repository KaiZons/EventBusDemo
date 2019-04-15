namespace MessageControl
{
    partial class Form1
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
            this.m_text = new System.Windows.Forms.TextBox();
            this.m_asyncButton = new System.Windows.Forms.Button();
            this.m_richTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // m_text
            // 
            this.m_text.Location = new System.Drawing.Point(56, 58);
            this.m_text.Name = "m_text";
            this.m_text.Size = new System.Drawing.Size(184, 21);
            this.m_text.TabIndex = 0;
            // 
            // m_asyncButton
            // 
            this.m_asyncButton.Location = new System.Drawing.Point(275, 58);
            this.m_asyncButton.Name = "m_asyncButton";
            this.m_asyncButton.Size = new System.Drawing.Size(75, 23);
            this.m_asyncButton.TabIndex = 1;
            this.m_asyncButton.Text = "Run";
            this.m_asyncButton.UseVisualStyleBackColor = true;
            this.m_asyncButton.Click += new System.EventHandler(this.m_asyncButton_Click);
            // 
            // m_richTextBox
            // 
            this.m_richTextBox.Location = new System.Drawing.Point(56, 103);
            this.m_richTextBox.Name = "m_richTextBox";
            this.m_richTextBox.Size = new System.Drawing.Size(618, 474);
            this.m_richTextBox.TabIndex = 2;
            this.m_richTextBox.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 589);
            this.Controls.Add(this.m_richTextBox);
            this.Controls.Add(this.m_asyncButton);
            this.Controls.Add(this.m_text);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_text;
        private System.Windows.Forms.Button m_asyncButton;
        private System.Windows.Forms.RichTextBox m_richTextBox;
    }
}

