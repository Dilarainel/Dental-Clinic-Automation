using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisKlinikOtomasyon
{
    public partial class PrescriptionForm : Form
    {
        public PrescriptionForm()
        {
            InitializeComponent();
        }





        private void btnCreatePrescription_Click(object sender, EventArgs e)
        {
            string patientName = txtPatientName.Text;
            string patientNationalId = txtPatientNationalId.Text;
            string medicine = txtMedicine.Text;
            string instructions = txtInstructions.Text;
            string diagnosis = cmbDiagnosis.Text;
            string doctorName = txtDoctorName.Text;

            if (string.IsNullOrEmpty(patientName) || string.IsNullOrEmpty(medicine) || string.IsNullOrEmpty(doctorName) || string.IsNullOrEmpty(patientNationalId) || string.IsNullOrEmpty(diagnosis))
            {
                MessageBox.Show("Please fill in all required fields (Patient, Medicine, Doctor, Patient ID and Diagnosis).", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           

        }

        
    }
}
