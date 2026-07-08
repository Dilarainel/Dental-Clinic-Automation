namespace DisKlinikOtomasyon
{
    partial class SickLeaveForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SickLeaveForm));
            this.rvSickLeave = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rvSickLeave
            // 
            this.rvSickLeave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rvSickLeave.LocalReport.ReportEmbeddedResource = "DisKlinikOtomasyon.Report8.rdlc";
            this.rvSickLeave.Location = new System.Drawing.Point(0, 0);
            this.rvSickLeave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rvSickLeave.Name = "rvSickLeave";
            this.rvSickLeave.ServerReport.BearerToken = null;
            this.rvSickLeave.Size = new System.Drawing.Size(1037, 688);
            this.rvSickLeave.TabIndex = 0;
            // 
            // SickLeaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1037, 688);
            this.Controls.Add(this.rvSickLeave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SickLeaveForm";
            this.Text = "Sick Leave Report";
            this.Load += new System.EventHandler(this.SickLeaveForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rvSickLeave;
    }
}