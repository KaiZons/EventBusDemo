namespace Views
{
    partial class SubForm
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
            this.m_text = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // m_text
            // 
            this.m_text.Location = new System.Drawing.Point(198, 107);
            this.m_text.Name = "m_text";
            this.m_text.Size = new System.Drawing.Size(337, 188);
            this.m_text.TabIndex = 0;
            this.m_text.Text = "";
            // 
            // SubForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.m_text);
            this.Name = "SubForm";
            this.Text = "SubForm";
            this.Load += new System.EventHandler(this.OnSubFormLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox m_text;
    }
}