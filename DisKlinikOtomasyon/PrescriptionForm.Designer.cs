namespace DisKlinikOtomasyon
{
    partial class PrescriptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrescriptionForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtPatientNationalId = new System.Windows.Forms.MaskedTextBox();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.lblPatienNationalID = new System.Windows.Forms.Label();
            this.lblMedicine = new System.Windows.Forms.Label();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.lblDiagnosis = new System.Windows.Forms.Label();
            this.lblDoctor = new System.Windows.Forms.Label();
            this.txtMedicine = new System.Windows.Forms.TextBox();
            this.txtDoctorName = new System.Windows.Forms.TextBox();
            this.txtPatientName = new System.Windows.Forms.TextBox();
            this.txtInstructions = new System.Windows.Forms.RichTextBox();
            this.cmbDiagnosis = new System.Windows.Forms.ComboBox();
            this.btnCreatePrescription = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.txtPatientNationalId, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblPatientName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblPatienNationalID, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblMedicine, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblInstructions, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblDiagnosis, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblDoctor, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtMedicine, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtDoctorName, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtPatientName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtInstructions, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.cmbDiagnosis, 1, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 113);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.18405F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.48797F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.48797F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.48797F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.26476F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.22898F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(668, 453);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // txtPatientNationalId
            // 
            this.txtPatientNationalId.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPatientNationalId.Location = new System.Drawing.Point(378, 88);
            this.txtPatientNationalId.Margin = new System.Windows.Forms.Padding(4);
            this.txtPatientNationalId.Mask = "00000000000";
            this.txtPatientNationalId.Name = "txtPatientNationalId";
            this.txtPatientNationalId.Size = new System.Drawing.Size(244, 22);
            this.txtPatientNationalId.TabIndex = 1;
            this.txtPatientNationalId.ValidatingType = typeof(int);
            // 
            // lblPatientName
            // 
            this.lblPatientName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblPatientName.Location = new System.Drawing.Point(4, 23);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(104, 19);
            this.lblPatientName.TabIndex = 0;
            this.lblPatientName.Text = "Patient Name:";
            // 
            // lblPatienNationalID
            // 
            this.lblPatienNationalID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPatienNationalID.AutoSize = true;
            this.lblPatienNationalID.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblPatienNationalID.Location = new System.Drawing.Point(4, 90);
            this.lblPatienNationalID.Name = "lblPatienNationalID";
            this.lblPatienNationalID.Size = new System.Drawing.Size(88, 19);
            this.lblPatienNationalID.TabIndex = 1;
            this.lblPatienNationalID.Text = "National ID:";
            // 
            // lblMedicine
            // 
            this.lblMedicine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMedicine.AutoSize = true;
            this.lblMedicine.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblMedicine.Location = new System.Drawing.Point(4, 160);
            this.lblMedicine.Name = "lblMedicine";
            this.lblMedicine.Size = new System.Drawing.Size(118, 19);
            this.lblMedicine.TabIndex = 2;
            this.lblMedicine.Text = "Medicine Name:";
            // 
            // lblInstructions
            // 
            this.lblInstructions.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblInstructions.Location = new System.Drawing.Point(4, 230);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(134, 19);
            this.lblInstructions.TabIndex = 3;
            this.lblInstructions.Text = "Usage Instructions:";
            // 
            // lblDiagnosis
            // 
            this.lblDiagnosis.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDiagnosis.AutoSize = true;
            this.lblDiagnosis.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDiagnosis.Location = new System.Drawing.Point(4, 333);
            this.lblDiagnosis.Name = "lblDiagnosis";
            this.lblDiagnosis.Size = new System.Drawing.Size(77, 19);
            this.lblDiagnosis.TabIndex = 4;
            this.lblDiagnosis.Text = "Diagnosis:";
            // 
            // lblDoctor
            // 
            this.lblDoctor.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDoctor.AutoSize = true;
            this.lblDoctor.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDoctor.Location = new System.Drawing.Point(4, 422);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Size = new System.Drawing.Size(103, 19);
            this.lblDoctor.TabIndex = 5;
            this.lblDoctor.Text = "Doctor Name:";
            // 
            // txtMedicine
            // 
            this.txtMedicine.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtMedicine.Location = new System.Drawing.Point(378, 158);
            this.txtMedicine.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMedicine.Name = "txtMedicine";
            this.txtMedicine.Size = new System.Drawing.Size(245, 22);
            this.txtMedicine.TabIndex = 2;
            // 
            // txtDoctorName
            // 
            this.txtDoctorName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtDoctorName.Location = new System.Drawing.Point(376, 420);
            this.txtDoctorName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDoctorName.Name = "txtDoctorName";
            this.txtDoctorName.Size = new System.Drawing.Size(248, 22);
            this.txtDoctorName.TabIndex = 5;
            // 
            // txtPatientName
            // 
            this.txtPatientName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPatientName.Location = new System.Drawing.Point(378, 21);
            this.txtPatientName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPatientName.Name = "txtPatientName";
            this.txtPatientName.Size = new System.Drawing.Size(245, 22);
            this.txtPatientName.TabIndex = 0;
            // 
            // txtInstructions
            // 
            this.txtInstructions.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtInstructions.Location = new System.Drawing.Point(377, 208);
            this.txtInstructions.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtInstructions.Name = "txtInstructions";
            this.txtInstructions.Size = new System.Drawing.Size(247, 63);
            this.txtInstructions.TabIndex = 3;
            this.txtInstructions.Text = "";
            // 
            // cmbDiagnosis
            // 
            this.cmbDiagnosis.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbDiagnosis.FormattingEnabled = true;
            this.cmbDiagnosis.Items.AddRange(new object[] {
            "ODR120",
            "ODR140",
            "ODR145",
            "ODR150",
            "ODR160",
            "ODR170",
            "ODR180",
            "ODR210",
            "ODR220",
            "ODR230",
            "ODR240",
            "ODR250",
            "ODR260",
            "ODR270",
            "ODR272",
            "ODR274",
            "ODR277",
            "ODR290",
            "ODR310",
            "ODR320",
            "ODR321",
            "ODR322",
            "ODR330",
            "ODR340",
            "ODR350",
            "ODR360",
            "ODR362",
            "ODR363",
            "ODR415",
            "ODR416",
            "ODR417",
            "ODR418",
            "ODR421",
            "ODR425",
            "ODR431",
            "ODR460",
            "ODR470",
            "ODR472",
            "ODR473",
            "ODR474",
            "ODR480",
            "ODR486",
            "ODR475",
            "ODR476",
            "ODR477",
            "ODR478",
            "ODR479",
            "ODR481",
            "ODR482",
            "ODR483",
            "ODR484",
            "ODR485",
            "ODR502",
            "ODR999"});
            this.cmbDiagnosis.Location = new System.Drawing.Point(376, 330);
            this.cmbDiagnosis.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDiagnosis.Name = "cmbDiagnosis";
            this.cmbDiagnosis.Size = new System.Drawing.Size(249, 24);
            this.cmbDiagnosis.TabIndex = 4;
            // 
            // btnCreatePrescription
            // 
            this.btnCreatePrescription.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCreatePrescription.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCreatePrescription.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnCreatePrescription.Location = new System.Drawing.Point(245, 608);
            this.btnCreatePrescription.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCreatePrescription.Name = "btnCreatePrescription";
            this.btnCreatePrescription.Size = new System.Drawing.Size(221, 59);
            this.btnCreatePrescription.TabIndex = 0;
            this.btnCreatePrescription.Text = "CREATE PRESCRIPTION";
            this.btnCreatePrescription.UseVisualStyleBackColor = false;
            this.btnCreatePrescription.Click += new System.EventHandler(this.btnCreatePrescription_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTitle.Location = new System.Drawing.Point(159, 30);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(383, 64);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.Text = "PRESCRIPTION";
            // 
            // PrescriptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(697, 692);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnCreatePrescription);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "PrescriptionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Prescription";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label lblPatienNationalID;
        private System.Windows.Forms.Label lblMedicine;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Label lblDiagnosis;
        private System.Windows.Forms.Label lblDoctor;
        private System.Windows.Forms.TextBox txtMedicine;
        private System.Windows.Forms.TextBox txtDoctorName;
        private System.Windows.Forms.TextBox txtPatientName;
        private System.Windows.Forms.RichTextBox txtInstructions;
        private System.Windows.Forms.Button btnCreatePrescription;
        private System.Windows.Forms.ComboBox cmbDiagnosis;
        private System.Windows.Forms.MaskedTextBox txtPatientNationalId;
        private System.Windows.Forms.Label lblTitle;
    }
}