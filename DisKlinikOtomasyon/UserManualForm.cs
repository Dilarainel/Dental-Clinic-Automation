using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisKlinikOtomasyon
{
    public partial class UserManualForm : Form
    {
        public UserManualForm()
        {
            InitializeComponent();
        }

        private void UserManualForm_Load(object sender, EventArgs e)
        {
            LoadUserGuide();
        }
        private void LoadUserGuide()
        {
            StringBuilder guide = new StringBuilder();


            // --- BAŞLIK ---
            guide.AppendLine("DENTAL CLINIC AUTOMATION SYSTEM - USER MANUAL");
            guide.AppendLine("==============================================");
            guide.AppendLine("");

            // --- 1. GİRİŞ ---
            guide.AppendLine("1. INTRODUCTION");
            guide.AppendLine("This system manages patient records, appointments, treatments, and finance. The dashboard provides real-time counters for Total Patients and Procedures.");
            guide.AppendLine("");

            // --- 2. HASTA YÖNETİMİ ---
            guide.AppendLine("2. PATIENT MANAGEMENT");
            guide.AppendLine("- Registration: Add new patients via 'Patient Registration'. National ID (TC) validation is active.");
            guide.AppendLine("- Search & Update: Search allows you to find patients instantly. Double-click to update details.");
            guide.AppendLine("- Security: The system uses 'Soft Delete'. Deleted records are hidden/archived, never lost.");
            guide.AppendLine("");

            // --- 3. DOKTOR YÖNETİMİ (YENİ EKLENDİ) ---
            guide.AppendLine("3. DOCTOR MANAGEMENT");
            guide.AppendLine("- Staffing: Register new doctors to the system via 'Doctor Management'.");
            guide.AppendLine("- Reporting: You can generate a report listing all registered doctors in the database.");
            guide.AppendLine("");

            // --- 4. RANDEVU SİSTEMİ ---
            guide.AppendLine("4. APPOINTMENT SYSTEM");
            guide.AppendLine("- Booking: Schedule visits using the 'Appointment' module. The system automatically blocks busy slots to prevent double-booking.");
            guide.AppendLine("- WhatsApp Reminder: Select an appointment and click the WhatsApp icon to send an instant reminder via WhatsApp Web.");
            guide.AppendLine("");

            // --- 5. KLİNİK İŞLEMLER ---
            guide.AppendLine("5. CLINICAL OPERATIONS");
            guide.AppendLine("- Treatments: Record procedures (e.g., Fillings, Root Canal). Use the 'Diagnosis Guide' for medical references.");
            guide.AppendLine("- Prescriptions: Create and print digital prescriptions.");
            guide.AppendLine("- Sick Leave: Generate official sick leave documents with auto-calculated dates.");
            guide.AppendLine("");

            // --- 6. FİNANS VE STOK (GÜNCELLENDİ) ---
            guide.AppendLine("6. FINANCE & INVENTORY");
            guide.AppendLine("- Income/Expense: Track the clinic's cash flow. Add incomes and log expenses (bills, rent).");
            guide.AppendLine("- Inventory: Monitor clinical supplies and add new items to the stock list.");
            guide.AppendLine("");

            // --- 7. SİSTEM VE GÜVENLİK (YENİ EKLENDİ - KRİTİK!) ---
            guide.AppendLine("7. SYSTEM & SECURITY");
            guide.AppendLine("- Auto-Lock: For security, the system locks automatically after 5 minutes of inactivity.");
            guide.AppendLine("- Database Maintenance: Use 'Backup Database' to save data and 'Clean Database' to optimize performance.");
            guide.AppendLine("- Logger: All critical actions are logged in the background for security auditing.");
            guide.AppendLine("- Themes: You can change the visual theme of the application from the settings.");
            guide.AppendLine("");

            // --- 8. RAPORLAMA (GÜNCELLENDİ) ---
            guide.AppendLine("8. REPORTING");
            guide.AppendLine("All modules (Patients, Treatments, Finance) support advanced reporting.");
            guide.AppendLine("- Export: All reports can be exported to Excel or PDF formats.");
            guide.AppendLine("- Printing: Direct printing support is available for all documents.");
            guide.AppendLine("");

            // --- 9. DESTEK ---
            guide.AppendLine("9. DEVELOPER INFO");
            guide.AppendLine("Developer: Dilara Nursema Inel");
            guide.AppendLine("Tech Stack: C# WinForms, Access OLEDB, RDLC Reporting.");
            guide.AppendLine("");

            guide.AppendLine("v1.0.0 | 2026");

            // Metni kutuya yazdır
            rtbGuide.Text = guide.ToString();
        }
    }
}
