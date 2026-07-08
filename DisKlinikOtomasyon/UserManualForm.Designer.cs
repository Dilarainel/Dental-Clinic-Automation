namespace DisKlinikOtomasyon
{
    partial class UserManualForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserManualForm));
            this.rtbGuide = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbGuide
            // 
            this.rtbGuide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbGuide.Location = new System.Drawing.Point(0, 0);
            this.rtbGuide.Name = "rtbGuide";
            this.rtbGuide.ReadOnly = true;
            this.rtbGuide.Size = new System.Drawing.Size(899, 778);
            this.rtbGuide.TabIndex = 0;
            this.rtbGuide.Text = "";
            // 
            // UserManualForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 778);
            this.Controls.Add(this.rtbGuide);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "UserManualForm";
            this.Text = "User Manual Form";
            this.Load += new System.EventHandler(this.UserManualForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbGuide;
    }
}