namespace Views
{
    partial class MainForm
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
            this.m_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_button
            // 
            this.m_button.Location = new System.Drawing.Point(275, 173);
            this.m_button.Name = "m_button";
            this.m_button.Size = new System.Drawing.Size(216, 23);
            this.m_button.TabIndex = 0;
            this.m_button.Text = "利用IOC和事件总线打开弹窗";
            this.m_button.UseVisualStyleBackColor = true;
            this.m_button.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.m_button);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_button;
    }
}

