using Microsoft.Office.Interop.Access;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DisKlinikOtomasyon
{
    public partial class Dashboard : System.Windows.Forms.Form
    {
        private OleDbConnection connection;
        private OleDbDataAdapter adapter;
        private DataTable dataTable;
        DataTable dtPatient2 = new DataTable();
        DataTable dtPatient = new DataTable();
        private bool isLoading = false;
        string[] allHours = { "09:00", "09:30", "10:00", "10:30", "11:00", "11:30",
                        "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30" };


        public Dashboard()
        {
            InitializeComponent();
            timer1.Start();
            this.Opacity = 0;

        }


        private void Dashboard_Load(object sender, EventArgs e)
        {
            
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");

            // Access veritabanı kontrolü
           
            if (!File.Exists(dbPath))
            {
                // Eğer dosya yanlışlıkla silinmişse sıfırdan oluşturur (Güvenlik önlemi olarak kalsın)
                CreateAccessDatabase(dbPath);
                CreatePatientTable(dbPath);
                CreateDoctorTable(dbPath);
                CreateTreatmentTable(dbPath);
                CreateAppointmentTable(dbPath);
                CreateAccountingTable(dbPath);
                CreateInventoryTable(dbPath);
                CreateUserTable(dbPath);
            }


            date.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            lockTimer.Start();
            
            grpAddDoctor.Visible = false;
            grpAddPatient.Visible = false;
            grpTreatmentEntry.Visible = false;
            grpAppointmentManagement.Visible = false;
            dentalLogo.Visible = true;
            patientNationalIdTxt.Focus();
            LoadPatientData();
            LoadDoctorData();
            LoadTreatmentsData();
            LoadAppointmentsData();
            LoadComboBoxes();
            LoadComboBoxes2();
            UpdatePatientCount();
            UpdateTreatmentCount();

            this.Opacity = 1;

        }
        private void CreatePatientTable(string dbPath)
        {
            string accessConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";
            using (OleDbConnection connection = new OleDbConnection(accessConnectionString))
            {
                connection.Open();
                string createPatientTableQuery = @"CREATE TABLE hastaKayit (
                hastaId AUTOINCREMENT PRIMARY KEY, 
                hastaTc TEXT(11),
                hastaAdi TEXT(50),
                hastaSoyadi TEXT(50),
                hastaDogumTarihi Date,
                hastaCinsiyet TEXT(10),
                hastaTelNo TEXT(15),
                hastaEmail TEXT(100),
                hastaAdres TEXT(255),
                hastaAciklama TEXT(255),
                Aktif BIT DEFAULT TRUE);";
                using (OleDbCommand command = new OleDbCommand
                (createPatientTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

            }
        }
        private void CreateUserTable(string dbPath)
        {
            string accessConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";
            using (OleDbConnection connection = new OleDbConnection(accessConnectionString))
            {
                connection.Open();
                string createUserTableQuery = @"CREATE TABLE KullaniciBilgi(
                userId AUTOINCREMENT PRIMARY KEY, 
                userName TEXT(50),
                userPass TEXT(50));";
                using (OleDbCommand command = new OleDbCommand
                (createUserTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

            }
        }
        private void CreateInventoryTable(string dbPath)
        {
            string accessConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";
            using (OleDbConnection connection = new OleDbConnection(accessConnectionString))
            {
                connection.Open();
                string createInventoryTableQuery = @"CREATE TABLE Malzeme(
                malzemeId AUTOINCREMENT PRIMARY KEY, 
                malzemeAdi TEXT(250),
                malzemeAdedi TEXT(150),
                firmaAdi TEXT(50),
                malzemeAciklama TEXT(255),
                Aktif BIT DEFAULT TRUE);";
                using (OleDbCommand command = new OleDbCommand
                (createInventoryTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

            }
        }
        private void CreateAppointmentTable(string dbPath)
        {
            string accessConnectionString = $"Provider =Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";
            using (OleDbConnection connection = new OleDbConnection(accessConnectionString))
            {
                connection.Open();
                string createAppointmentTableQuery = @"CREATE TABLE hastaRandevu (
                randevuId AUTOINCREMENT PRIMARY KEY, 
                hastaAdi TEXT(150),
                hekimAdi TEXT(150),
                randevuTarihi Date,
                randevuSaati TEXT(10),
                hastaAciklama TEXT(255),
                Aktif BIT DEFAULT TRUE);";
                using (OleDbCommand command = new OleDbCommand
                (createAppointmentTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

            }
        }
        private void CreateDoctorTable(string dbPath)
        {
            string accessConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";
            using (OleDbConnection connection = new OleDbConnection(accessConnectionString))
            {
                connection.Open();
                string createDoctorTableQuery = @"
                CREATE TABLE hekimBilgi (
                doktorId AUTOINCREMENT PRIMARY KEY,
                doktorTc TEXT(11),
                doktorAdi Text(50),
                doktorSoyadi TEXT(50),
                uzmanlikAlani TEXT(50),
                doktorDogumTarihi DateTime,
                doktorTelNo TEXT(15),
                doktorEmail TEXT(100),
                doktorAdres TEXT(255),
                doktorAciklama TEXT(255),
                Aktif BIT DEFAULT TRUE);";
                using (OleDbCommand command = new OleDbCommand
                (createDoctorTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

            }
        }
        private void CreateTreatmentTable(string dbPath)
        {
            string accessConnectionString = $"Provider =Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";
            using (OleDbConnection connection = new OleDbConnection(accessConnectionString))
            {
                connection.Open();
                string createTreatmentTableQuery = @"
              CREATE TABLE islemler(
              islemId AUTOINCREMENT PRIMARY KEY,
              secilenHasta TEXT(150),
              secilenDoktor TEXT(150),
              yapilanIslem TEXT(150),
              islemTarihi Date,
              islemDetay TEXT(150),
              islemUcret DECIMAL(10,2),
              Aktif BIT DEFAULT TRUE);";
                using (OleDbCommand command = new OleDbCommand
                (createTreatmentTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

            }
        }
        private void CreateAccountingTable(string dbPath)
        {
            string accessConnectionString = $"Provider =Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";
            using (OleDbConnection connection = new OleDbConnection(accessConnectionString))
            {
                connection.Open();
                string createAccountingTableQuery = @"
              CREATE TABLE Kasa(
              giderId AUTOINCREMENT PRIMARY KEY,
              giderAdi TEXT(150),
              giderTutar DECIMAL(10,2),
              giderAciklama TEXT(150) );";
                using (OleDbCommand command = new OleDbCommand (createAccountingTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

            }
        }
        private void CreateAccessDatabase(string dbPath)
        {
            if (!File.Exists(dbPath))
            {
                // Dosyayı oluştur
                var engine = new Microsoft.Office.Interop.Access.Application();
                engine.NewCurrentDatabase(dbPath);
                engine.CloseCurrentDatabase();
                engine.Quit();
            }
        }

        //-------------------------------------------------------------------------------------------------


        // PATIENT MANAGEMENT
        private void LoadPatientData()
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");
            string connectionString =
            $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";
            try
            {
                connection = new OleDbConnection(connectionString);
                connection.Open();
                string query = "SELECT * FROM hastaKayit WHERE Aktif = True";
                adapter = new OleDbDataAdapter(query, connection);
                dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgvPatientList.DataSource = null;
                // ------------------------

                dgvPatientList.DataSource = dataTable;
                if (dgvPatientList.Columns.Contains("Aktif"))
                {
                    dgvPatientList.Columns["Aktif"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading patients: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection?.Close();
            }
        }

        private void patientAddBtn_Click(object sender, EventArgs e)
        {

            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");
            // TextBox'lardan verileri al
            string hastaTc = patientNationalIdTxt.Text;
            string hastaAdi = patientFirstNameTxt.Text;
            string hastaSoyadi = patientLastNameTxt.Text;
            DateTime hastaDogumTarihi = patientBirthDate.Value.Date;
            string hastaCinsiyet = patientGenderTxt.Text;
            string hastaTelNo = patientPhoneTxt.Text;
            string hastaEmail = patientEmailTxt.Text;
            string hastaAdres = patientAddressTxt.Text;
            string hastaAciklama = patientNotesTxt.Text;
            try
            {
                string accessConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";
                using (OleDbConnection connection = new OleDbConnection(accessConnectionString))
                {
                    connection.Open();
                    string insertQuery = @"
                    INSERT INTO hastaKayit
                    (hastaTc, hastaAdi, hastaSoyadi,
                    hastaDogumTarihi, hastaCinsiyet, hastaTelNo,
                    hastaEmail, hastaAdres, hastaAciklama)
                    VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";
                    using (OleDbCommand command = new OleDbCommand
                    (insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("?",
                        hastaTc);
                        command.Parameters.AddWithValue("?",
                        hastaAdi);
                        command.Parameters.AddWithValue("?",
                        hastaSoyadi);
                        command.Parameters.AddWithValue("?",
                        hastaDogumTarihi);
                        command.Parameters.AddWithValue("?",
                        hastaCinsiyet);
                        command.Parameters.AddWithValue("?",
                        hastaTelNo);
                        command.Parameters.AddWithValue("?",
                        hastaEmail);
                        command.Parameters.AddWithValue("?",
                        hastaAdres);
                        command.Parameters.AddWithValue("?",
                        hastaAciklama);
                        command.ExecuteNonQuery();
                    }

                }
                UpdatePatientCount();
                LoadComboBoxes();
                LoadComboBoxes2();
                MessageBox.Show("Patient registration successful.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information); LoadPatientData();
                ClearPatientInputs();

            } 
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void patientDeleteBtn_Click(object sender, EventArgs e)
        {
            if (dgvPatientList.SelectedRows.Count > 0) // Eğer bir satır seçilmişse
            {
                // Seçili satırdan TC numarasını al
                string hastaTc = dgvPatientList.SelectedRows[0].Cells
                ["hastaTc"].Value.ToString();
                // Silme işlemi için onay iste
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this patient?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    // Hasta silme işlemini başlat
                    DeletePatient(hastaTc);
                    LoadComboBoxes();
                    LoadComboBoxes2();
                    ClearPatientInputs();
                }
            }
            else
            {
                MessageBox.Show("Please select a patient to delete.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DeletePatient(string hastaTc)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");
            try
            {
                string accessConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};";
                using (OleDbConnection connection = new OleDbConnection(accessConnectionString))
                {
                    connection.Open();

                    // ESKİSİ: DELETE FROM hastaKayit ...
                    // YENİSİ: Satırı silme, sadece 'Aktif' sütununu False yap (Gizle)
                    string updateQuery = "UPDATE hastaKayit SET Aktif = False WHERE hastaTc = @hastaTc";

                    using (OleDbCommand command = new OleDbCommand(updateQuery, connection))
                    {
                        // Parametre ekliyoruz
                        command.Parameters.AddWithValue("@hastaTc", hastaTc);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Patient deleted successfully.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Listeyi yenile ki ekrandan da gitsin
                            LoadPatientData();
                            UpdatePatientCount();
                        }
                        else
                        {
                            MessageBox.Show("Patient not found.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void dgvPatientList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPatientList.Rows[e.RowIndex];
                PatientIdTxt.Text = row.Cells["hastaId"].Value.ToString ();
                // Gizli TextBox'ta hastaId tutuluyor
                patientNationalIdTxt.Text = row.Cells["hastaTc"].Value.ToString ();
                patientFirstNameTxt.Text = row.Cells["hastaAdi"].Value.ToString();
                patientLastNameTxt.Text = row.Cells["hastaSoyadi"].Value.ToString();
                patientBirthDate.Value = Convert.ToDateTime
                (row.Cells["hastaDogumTarihi"].Value);
                patientGenderTxt.Text = row.Cells
                ["hastaCinsiyet"].Value.ToString();
                patientPhoneTxt.Text = row.Cells
                ["hastaTelNo"].Value.ToString();
                patientEmailTxt.Text = row.Cells
                ["hastaEmail"].Value.ToString();
                patientAddressTxt.Text = row.Cells
                ["hastaAdres"].Value.ToString();
                patientNotesTxt.Text = row.Cells
                ["hastaAciklama"].Value.ToString();

                patientNationalIdTxt.Tag = patientNationalIdTxt.Text;
                patientFirstNameTxt.Tag = patientFirstNameTxt.Text;
                patientLastNameTxt.Tag = patientLastNameTxt.Text;
                patientBirthDate.Tag = patientBirthDate.Value;
                patientGenderTxt.Tag = patientGenderTxt.Text;
                patientPhoneTxt.Tag = patientPhoneTxt.Text;
                patientEmailTxt.Tag = patientEmailTxt.Text;
                patientAddressTxt.Tag = patientAddressTxt.Text;
                patientNotesTxt.Tag = patientNotesTxt.Text;
            }
        }

        private void patientUpdateBtn_Click(object sender, EventArgs e)
        {
            if (PatientIdTxt.Text == "" || PatientIdTxt.Text == "0")
            {
                MessageBox.Show("Please select a patient from the list to update first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // <--- Bu 'return' çok önemli, kodu burada keser ve aşağıya inmez.
            }
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");
            string hastaId = PatientIdTxt.Text; // Gizli TextBox'tan hastaId alınıyor
            string hastaTc = patientNationalIdTxt.Text;
            string hastaAdi = patientFirstNameTxt.Text;
            string hastaSoyadi = patientLastNameTxt.Text;
            DateTime hastaDogumTarihi = patientBirthDate.Value;
            string hastaCinsiyet = patientGenderTxt.Text;
            string hastaTelNo = patientPhoneTxt.Text;
            string hastaEmail = patientEmailTxt.Text;
            string hastaAdres = patientAddressTxt.Text;
            string hastaAciklama = patientNotesTxt.Text;
            
            try
            {
                string accessConnectionString =
                $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";
                using (OleDbConnection connection = new OleDbConnection(accessConnectionString))
                {
                    connection.Open();
                    string updateQuery = @"
                    UPDATE hastaKayit
                    SET hastaTc = ?, hastaAdi = ?, hastaSoyadi = ?,
                    hastaDogumTarihi = ?, hastaCinsiyet = ?, hastaTelNo = ?,
                    hastaEmail = ?, hastaAdres = ?, hastaAciklama = ?
                    WHERE hastaId = ?"; // TC ve diğer bilgileri güncelliyoruz,hastaId ile güncellemeyi yapıyoruz

                 
                    using (OleDbCommand command = new OleDbCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("?",
                        hastaTc); // TC'yi güncelliyoruz
                        command.Parameters.AddWithValue("?", hastaAdi);
                        command.Parameters.AddWithValue("?",
                        hastaSoyadi);
                        command.Parameters.AddWithValue("?", hastaDogumTarihi);
                        command.Parameters.AddWithValue("?",
                        hastaCinsiyet);
                        command.Parameters.AddWithValue("?",
                        hastaTelNo);
                        command.Parameters.AddWithValue("?",
                        hastaEmail);
                        command.Parameters.AddWithValue("?",
                        hastaAdres);
                        command.Parameters.AddWithValue("?",
                        hastaAciklama);
                        command.Parameters.AddWithValue("?",
                        hastaId); // hastaId ile güncelleme yapılıyor
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            List<string> changes = new List<string>();

                            Action<System.Windows.Forms.Control, string> CheckChange = (ctrl, label) =>
                            {
                                if (ctrl.Tag != null && ctrl.Tag.ToString() != ctrl.Text)
                                    changes.Add($"{label}: {ctrl.Tag} → {ctrl.Text}");
                            };

                            Action<DateTimePicker, string> CheckDate = (date, label) =>
                            {
                                if (date.Tag != null && ((DateTime)date.Tag).ToShortDateString() != date.Value.ToShortDateString())
                                    changes.Add($"{label}: {((DateTime)date.Tag).ToShortDateString()} → {date.Value.ToShortDateString()}");
                            };

                            // Kontrol et
                            CheckChange(patientNationalIdTxt, "TC");
                            CheckChange(patientFirstNameTxt, "Ad");
                            CheckChange(patientLastNameTxt, "Soyad");
                            CheckDate(patientBirthDate, "Doğum Tarihi");
                            CheckChange(patientGenderTxt, "Cinsiyet");
                            CheckChange(patientPhoneTxt, "Telefon");
                            CheckChange(patientEmailTxt, "Email");
                            CheckChange(patientAddressTxt, "Adres");
                            CheckChange(patientNotesTxt, "Açıklama");

                            
                            MessageBox.Show("Patient updated successfully.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information); LoadPatientData();
                            LoadComboBoxes();
                            LoadComboBoxes2();
                           

                        }
                        else
                        {
                            MessageBox.Show("Patient to update not found.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }

                ClearPatientInputs(); // TextBox'ları temizleyen metot
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating patient: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void ClearPatientInputs()
        {
            PatientIdTxt.Clear();
            patientNationalIdTxt.Clear();
            patientFirstNameTxt.Clear();
            patientLastNameTxt.Clear();
            patientBirthDate.Value = DateTime.Now;
            patientGenderTxt.SelectedIndex = -1;
            patientGenderTxt.Text = "";
            patientPhoneTxt.Clear();
            patientEmailTxt.Clear();
            patientAddressTxt.Clear();
            patientNotesTxt.Clear();
            patientNationalIdTxt.Focus();
        }

        //------------------------------------------------------------------------------------------------------------------------------------

        // DOCTOR MANAGEMENT
        private void addDoctorBtn_Click(object sender, EventArgs e)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");
            string doktorTc = doctorNationalIdTxt.Text;
            string doktorAdi = doctorFirstNameTxt.Text;
            string doktorSoyadi = doctorLastNameTxt.Text;
            string uzmanlikAlani = doctorSpecialtyTxt.Text;
            DateTime doktorDogumTarihi = DateTime.Parse
            (doctorBirthDate.Text);
            string doktorTelNo = doctorPhoneTxt.Text;
            string doktorEmail = doctorEmailTxt.Text;
            string doktorAdres = doctorAddressTxt.Text;
            string doktorAciklama = doctorNotesTxt.Text;
            try
            {
                string accessConnectionString =
                $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";
                using (OleDbConnection connection = new OleDbConnection
                (accessConnectionString))
                {
                    connection.Open();
                    string insertQuery = @"
                    INSERT INTO hekimBilgi
                    (doktorTc, doktorAdi,doktorSoyadi,uzmanlikAlani, doktorDogumTarihi,
                    doktorTelNo, doktorEmail, doktorAdres,doktorAciklama)VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";
                    using (OleDbCommand command = new OleDbCommand
                          (insertQuery, connection))
                    {
                        command.Parameters.AddWithValue(" ? ", 
                        doktorTc);
                        command.Parameters.AddWithValue(" ? ",
                        doktorAdi);
                        command.Parameters.AddWithValue(" ? ",
                        doktorSoyadi);
                        command.Parameters.AddWithValue(" ? ",
                        uzmanlikAlani);
                        command.Parameters.AddWithValue(" ? ",
                        doktorDogumTarihi);
                        command.Parameters.AddWithValue(" ? ",
                        doktorTelNo);
                        command.Parameters.AddWithValue(" ? ",
                        doktorEmail);
                        command.Parameters.AddWithValue(" ? ",
                        doktorAdres);
                        command.Parameters.AddWithValue(" ? ",
                        doktorAciklama);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Doctor added successfully.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDoctorData();
                LoadComboBoxes();
                LoadComboBoxes2();
                ClearDoctorInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearDoctorInputs()
        {
            doctorIdTxt.Clear();
            doctorNationalIdTxt.Clear();
            doctorFirstNameTxt.Clear();
            doctorLastNameTxt.Clear();
            doctorBirthDate.Value = DateTime.Now;
            doctorSpecialtyTxt.SelectedIndex = -1;
            doctorSpecialtyTxt.Text = "";
            doctorEmailTxt.Clear();
            doctorPhoneTxt.Clear();
            doctorAddressTxt.Clear();
            doctorNotesTxt.Clear();
        }

        private void deleteDoctorbtn_Click(object sender, EventArgs e)
        {
            if (dgvDoctors.SelectedRows.Count > 0) // Eğer bir satır seçilmişse
            {
                // Seçili satırdan hekim TC numarasını al
                string doktorTc = dgvDoctors.SelectedRows[0].Cells
                ["doktorTc"].Value.ToString(); // Hekim TC'si buradan alınıyor
                // Silme işlemi için onay iste
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this doctor?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    // Hekim silme işlemini başlat
                    DeleteDoctor(doktorTc);
                    LoadComboBoxes();
                    LoadComboBoxes2();
                    ClearDoctorInputs();
                }
                else
                {
                    MessageBox.Show("Please select a doctor to delete.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
        }

        private void DeleteDoctor(string doktorTc)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");
            try
            {
                string accessConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";
                using (OleDbConnection connection = new OleDbConnection(accessConnectionString))
                {
                    connection.Open();

                    // DÜZENLEME: DELETE yerine UPDATE yapıyoruz. 
                    // Hekimi tamamen silmek yerine 'Aktif' durumunu False yapıyoruz.
                    string updateQuery = "UPDATE hekimBilgi SET Aktif = False WHERE doktorTc = @doktorTc";

                    using (OleDbCommand command = new OleDbCommand(updateQuery, connection))
                    {
                        // Parametre ekleme
                        command.Parameters.AddWithValue("@doktorTc", doktorTc);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Doctor deleted successfully.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Listeyi yenile ki silinen hekim ekrandan gitsin
                            LoadDoctorData();
                        }
                        else
                        {
                            MessageBox.Show("Doctor not found.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private void LoadDoctorData()
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");

            string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";
            try
            {
                connection = new OleDbConnection(connectionString);
                connection.Open();
                string query = "SELECT * FROM hekimBilgi WHERE Aktif = True";
                adapter = new OleDbDataAdapter(query, connection);
                dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgvDoctors.DataSource = null;
                dgvDoctors.DataSource = dataTable;
                if (dgvDoctors.Columns.Contains("Aktif"))
                {
                    dgvDoctors.Columns["Aktif"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading doctors: " + ex.Message);
            }
            finally
            {
                connection?.Close();
            }
        }

        private void dgvDoctors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDoctors.Rows[e.RowIndex];
                doctorIdTxt.Text = row.Cells["doktorId"].Value.ToString();
                doctorNationalIdTxt.Text = row.Cells["doktorTc"].Value.ToString();
                doctorFirstNameTxt.Text = row.Cells["doktorAdi"].Value.ToString();
                doctorLastNameTxt.Text = row.Cells["doktorSoyadi"].Value.ToString();
                doctorSpecialtyTxt.Text = row.Cells
                ["uzmanlikAlani"].Value.ToString();
                doctorBirthDate.Value = Convert.ToDateTime(row.Cells
                ["doktorDogumTarihi"].Value);
                doctorPhoneTxt.Text = row.Cells
                ["doktorTelNo"].Value.ToString();
                doctorEmailTxt.Text = row.Cells
                ["doktorEmail"].Value.ToString();
                doctorAddressTxt.Text = row.Cells
                ["doktorAdres"].Value.ToString();
                doctorNotesTxt.Text = row.Cells
                ["doktorAciklama"].Value.ToString();

                doctorNationalIdTxt.Tag = doctorNationalIdTxt.Text;
                doctorFirstNameTxt.Tag = doctorFirstNameTxt.Text;
                doctorLastNameTxt.Tag = doctorLastNameTxt.Text;
                doctorSpecialtyTxt.Tag = doctorSpecialtyTxt.Text;
                doctorBirthDate.Tag = doctorBirthDate.Value;
                doctorPhoneTxt.Tag = doctorPhoneTxt.Text;
                doctorEmailTxt.Tag = doctorEmailTxt.Text;
                doctorAddressTxt.Tag = doctorAddressTxt.Text;
                doctorNotesTxt.Tag = doctorNotesTxt.Text;
            }
        }

        private void updateDoctorBtn_Click(object sender, EventArgs e)
        {
            if (doctorIdTxt.Text == "" || doctorIdTxt.Text == "0")
            {
                MessageBox.Show("Please select a doctor from the list to update first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // <--- Bu 'return' çok önemli, kodu burada keser ve aşağıya inmez.
            }
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");
            string doktorId = doctorIdTxt.Text;
            string doktorTc = doctorNationalIdTxt.Text;
            string doktorAdi = doctorFirstNameTxt.Text;
            string doktorSoyadi = doctorLastNameTxt.Text;
            string uzmanlikAlani = doctorSpecialtyTxt.Text;
            DateTime doktorDogumTarihi = DateTime.Parse
            (doctorBirthDate.Text);
            string doktorTelNo = doctorPhoneTxt.Text;
            string doktorEmail = doctorEmailTxt.Text;
            string doktorAdres = doctorAddressTxt.Text;
            string doktorAciklama = doctorNotesTxt.Text;

            try
            {
                string accessConnectionString =
                $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";
                using (OleDbConnection connection = new OleDbConnection(accessConnectionString))
                {
                    connection.Open();
                    string updateQuery = @"
                    UPDATE hekimBilgi
                    SET doktorTc = ?, doktorAdi = ?, doktorSoyadi
                    = ?,uzmanlikAlani = ?, doktorDogumTarihi = ?, doktorTelNo
                    = ?, doktorEmail = ?, doktorAdres = ?, doktorAciklama = ?
                    WHERE doktorId = ?";
                    using (OleDbCommand command = new OleDbCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("?", doktorTc);
                        command.Parameters.AddWithValue("?",
                        doktorAdi);
                        command.Parameters.AddWithValue("?",
                        doktorSoyadi);
                        command.Parameters.AddWithValue("?",
                        uzmanlikAlani);
                        command.Parameters.AddWithValue("?",
                        doktorDogumTarihi);
                        command.Parameters.AddWithValue("?",
                        doktorTelNo);
                        command.Parameters.AddWithValue("?",
                        doktorEmail);
                        command.Parameters.AddWithValue("?",
                        doktorAdres);
                        command.Parameters.AddWithValue("?",
                        doktorAciklama);
                        command.Parameters.AddWithValue("?",
                        doktorId); // TC ile güncelleme yapılacak hasta aranıyor
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            List<string> changes = new List<string>();

                            Action<System.Windows.Forms.Control, string> CheckChange = (ctrl, label) =>
                            {
                                if (ctrl.Tag != null && ctrl.Tag.ToString() != ctrl.Text)
                                    changes.Add($"{label}: {ctrl.Tag} → {ctrl.Text}");
                            };

                            Action<DateTimePicker, string> CheckDate = (date, label) =>
                            {
                                if (date.Tag != null && ((DateTime)date.Tag).ToShortDateString() != date.Value.ToShortDateString())
                                    changes.Add($"{label}: {((DateTime)date.Tag).ToShortDateString()} → {date.Value.ToShortDateString()}");
                            };

                            // Kontrol et
                            CheckChange(doctorNationalIdTxt, "TC");
                            CheckChange(doctorFirstNameTxt, "Ad");
                            CheckChange(doctorLastNameTxt, "Soyad");
                            CheckChange(doctorSpecialtyTxt, "Uzmanlık Alanı");
                            CheckDate(doctorBirthDate, "Doğum Tarihi");  
                            CheckChange(doctorPhoneTxt, "Telefon");
                            CheckChange(doctorEmailTxt, "Email");
                            CheckChange(doctorAddressTxt, "Adres");
                            CheckChange(doctorNotesTxt, "Açıklama");

                            
                            MessageBox.Show("Doctor details updated successfully.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDoctorData();
                            LoadComboBoxes();
                            LoadComboBoxes2();
                        }
                        else
                        {
                            MessageBox.Show("Doctor not found to update.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                ClearDoctorInputs(); // TextBox'ları temizleyen metot
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //---------------------------------------------------------------------------------------------------------

        // TREATMENT MANAGEMENT
        private void procedureAddBtn_Click(object sender, EventArgs e)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");
            string secilenHasta = patientNameCombo.Text;
            string secilenDoktor = doctorNameCombo.Text;
            string yapilanIslemm = Procedurertb.Text;
            DateTime islemTarihi = DateTime.Parse(procedureDate.Text);
            string islemDetayi = procedureDetailsRtb.Text;
            decimal islemUcreti;
            if (!decimal.TryParse(procedurePrice.Text, out islemUcreti))
            {
                MessageBox.Show("Please enter a valid price.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            if (string.IsNullOrEmpty(secilenHasta) || string.IsNullOrEmpty(secilenDoktor))
            {
                MessageBox.Show("Please select both a patient and a doctor.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string accessConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(accessConnectionString))
                {
                    connection.Open();
                    string insertQuery = @"
                    INSERT INTO islemler
                    (secilenHasta, secilenDoktor, yapilanIslem, islemTarihi, islemDetay, islemUcret)
                    VALUES (?, ?, ?, ?, ?, ?)";

                    using (OleDbCommand command = new OleDbCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("?", secilenHasta ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("?", secilenDoktor ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("?", yapilanIslemm);
                        command.Parameters.AddWithValue("?", islemTarihi);
                        command.Parameters.AddWithValue("?", islemDetayi ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("?", islemUcreti.ToString());
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Treatment added successfully.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information); LoadTreatmentsData();
                UpdateTreatmentCount();
                ClearTreatmentInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void ClearTreatmentInputs()
        {
            patientNameCombo.Text = "";
            doctorNameCombo.Text = "";
            Procedurertb.Clear();
            procedureDate.Value = DateTime.Now;
            procedureDetailsRtb.Clear();
            procedurePrice.Clear();
        }

        private void LoadComboBoxes()
        {
           
            doctorNameCombo.Items.Clear();
           
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");
            string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};";
            patientNameCombo.AutoCompleteMode = AutoCompleteMode.None;
            patientNameCombo.DropDownStyle = ComboBoxStyle.DropDown;
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    string hastaQuery = "SELECT hastaId, (hastaAdi & ' ' & hastaSoyadi) AS AdSoyad FROM hastaKayit WHERE Aktif = True";
                    using (OleDbDataAdapter da = new OleDbDataAdapter(hastaQuery, connection))
                    {
                        dtPatient2.Clear();
                        da.Fill(dtPatient2);
                    }
                    isLoading = true;
                    // DataTable'ı ComboBox'a bağlıyoruz
                    patientNameCombo.DataSource = null;

                    // 3. Önce neyi göstereceğini söyle
                    patientNameCombo.DisplayMember = "AdSoyad";
                    patientNameCombo.ValueMember = "hastaID"; // Veritabanındaki ID sütun adın neyse o

                    // 4. EN SON veriyi ver
                    patientNameCombo.DataSource = dtPatient2;
                    patientNameCombo.SelectedIndex = -1; // Seçimi iptal et (Mavi seçimi kaldırır)
                    patientNameCombo.Text = "";          // Kutunun içindeki yazıyı sil
                    isLoading = false;
                    /* using (OleDbCommand hastaCommand = new OleDbCommand(hastaQuery, connection))
                     {
                         using (OleDbDataReader reader = hastaCommand.ExecuteReader())
                         {
                             while (reader.Read())
                             {
                                 string hastaAdSoyad = reader["hastaAdi"].ToString() + " " + reader["hastaSoyadi"].ToString();
                                 patientNameCombo.Items.Add(hastaAdSoyad);
                             }
                         }
                     }*/

                    // 2. DÜZELTME: Doktor sorgusuna da 'WHERE Aktif = True' ekledik
                    string doktorQuery = "SELECT doktorAdi, doktorSoyadi FROM hekimBilgi WHERE Aktif = True";

                    using (OleDbCommand doktorCommand = new OleDbCommand(doktorQuery, connection))
                    {
                        using (OleDbDataReader reader = doktorCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string doktorAdSoyad = reader["doktorAdi"].ToString() + " " + reader["doktorSoyadi"].ToString();
                                doctorNameCombo.Items.Add(doktorAdSoyad);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        private void LoadComboBoxes2()
        {
            //patientFullNameCombo2.Items.Clear();
            doctorFullNameCombo2.Items.Clear();
            //patientFullNameCombo2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //patientFullNameCombo2.AutoCompleteSource = AutoCompleteSource.ListItems;

            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");
            string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};";
            patientFullNameCombo2.AutoCompleteMode = AutoCompleteMode.None;
            patientFullNameCombo2.DropDownStyle = ComboBoxStyle.DropDown;
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    // 1. DÜZELTME: Sadece aktif hastaları getiriyoruz
                    string hastaQuery = "SELECT hastaId, (hastaAdi & ' ' & hastaSoyadi) AS AdSoyad FROM hastaKayit WHERE Aktif = True";
                    using (OleDbDataAdapter da = new OleDbDataAdapter(hastaQuery, connection))
                    {
                        dtPatient.Clear();
                       
                        da.Fill(dtPatient);
                    }
                    isLoading = true;
                    // DataTable'ı ComboBox'a bağlıyoruz
                    patientFullNameCombo2.DataSource = null;

                    // 3. Önce neyi göstereceğini söyle
                    patientFullNameCombo2.DisplayMember = "AdSoyad";
                    patientFullNameCombo2.ValueMember = "hastaID"; // Veritabanındaki ID sütun adın neyse o

                    // 4. EN SON veriyi ver
                    patientFullNameCombo2.DataSource = dtPatient;
                    patientFullNameCombo2.SelectedIndex = -1; // Seçimi iptal et (Mavi seçimi kaldırır)
                    patientFullNameCombo2.Text = "";          // Kutunun içindeki yazıyı sil
                    isLoading = false;
                    /*  using (OleDbCommand hastaCommand = new OleDbCommand(hastaQuery, connection))
                      {
                          using (OleDbDataReader reader = hastaCommand.ExecuteReader())
                          {
                              while (reader.Read())
                              {
                                  string hastaAdSoyad = reader["hastaAdi"].ToString() + " " + reader["hastaSoyadi"].ToString();
                                  patientFullNameCombo2.Items.Add(hastaAdSoyad);
                              }
                          }
                      }*/

                    // 2. DÜZELTME: Sadece aktif doktorları getiriyoruz
                    string doktorQuery = "SELECT doktorAdi, doktorSoyadi FROM hekimBilgi WHERE Aktif = True";

                    using (OleDbCommand doktorCommand = new OleDbCommand(doktorQuery, connection))
                    {
                        using (OleDbDataReader reader = doktorCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string doktorAdSoyad = reader["doktorAdi"].ToString() + " " + reader["doktorSoyadi"].ToString();
                                doctorFullNameCombo2.Items.Add(doktorAdSoyad);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void LoadTreatmentsData()
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");
            string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};";

            try
            {
                connection = new OleDbConnection(connectionString);
                connection.Open();
                string query = "SELECT * FROM islemler WHERE Aktif = True";
                adapter = new OleDbDataAdapter(query, connection);
                dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgvTreatments.DataSource = null;
                dgvTreatments.DataSource = dataTable;
                if (dgvTreatments.Columns.Contains("Aktif"))
                {
                    dgvTreatments.Columns["Aktif"].Visible = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading treatments: " + ex.Message);
            }
            finally
            {
                connection?.Close();
            }
        }

        private void procedureUpdateBtn_Click(object sender, EventArgs e)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");

            if (!File.Exists(dbPath))
            {
                MessageBox.Show("Database file not found.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
            }
            string islemId = procedureIdTxt.Text;
            string secilenHasta = patientNameCombo.Text;
            string secilenDoktor = doctorNameCombo.Text;
            string yapilanIslemm = Procedurertb.Text;
            DateTime islemTarihi = DateTime.Parse(procedureDate.Text);
            string islemDetayi = procedureDetailsRtb.Text;
            decimal islemUcreti;
                if (!decimal.TryParse(procedurePrice.Text, out islemUcreti))
            {
                MessageBox.Show("Please enter a valid price.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(secilenHasta) || string.IsNullOrEmpty(secilenDoktor))
            {
                MessageBox.Show("Please select both a patient and a doctor.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string accessConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(accessConnectionString))
                {
                    connection.Open();
                    string insertQuery = @"
                UPDATE  islemler
                SET secilenHasta = ?,
                secilenDoktor = ?, yapilanIslem = ?, islemTarihi = ?, islemDetay = ?, islemUcret = ?
                 WHERE islemId = ?";

                    using (OleDbCommand command = new OleDbCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("?", secilenHasta ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("?", secilenDoktor ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("?", yapilanIslemm);
                        command.Parameters.AddWithValue("?", islemTarihi);
                        command.Parameters.AddWithValue("?", islemDetayi ?? (object)DBNull.Value);

                        command.Parameters.AddWithValue("?", islemUcreti);
                        command.Parameters.AddWithValue("?", islemId);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {

                            List<string> changes = new List<string>();

                            Action<System.Windows.Forms.Control, string> CheckChange = (ctrl, label) =>
                            {
                                if (ctrl.Tag != null && ctrl.Tag.ToString() != ctrl.Text)
                                    changes.Add($"{label}: {ctrl.Tag} → {ctrl.Text}");
                            };

                            Action<DateTimePicker, string> CheckDate = (date, label) =>
                            {
                                if (date.Tag != null && ((DateTime)date.Tag).ToShortDateString() != date.Value.ToShortDateString())
                                    changes.Add($"{label}: {((DateTime)date.Tag).ToShortDateString()} → {date.Value.ToShortDateString()}");
                            };

                            // Kontrol et
                            CheckChange(patientNameCombo, "Hasta");
                            CheckChange(doctorNameCombo, "Doktor");
                            CheckChange(Procedurertb, "İşlem");
                            CheckDate(procedureDate, "İşlem Tarihi"); 
                            CheckChange(procedureDetailsRtb, "Detay");
                            CheckChange(procedurePrice, "Ücret");

                            MessageBox.Show("Treatment updated successfully.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadTreatmentsData();
                        }
                        else
                        {
                            MessageBox.Show("Treatment to update not found.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }
                }
                ClearTreatmentInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void dgvTreatments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            isLoading = true;
            try { 
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = dgvTreatments.Rows[e.RowIndex];
                procedureIdTxt.Text = row.Cells["islemId"].Value.ToString();
                patientNameCombo.Text = row.Cells["secilenHasta"].Value.ToString();
                doctorNameCombo.Text = row.Cells["secilenDoktor"].Value.ToString();
                Procedurertb.Text = row.Cells["yapilanIslem"].Value.ToString();
                procedureDate.Value = Convert.ToDateTime(row.Cells["islemTarihi"].Value);
                procedureDetailsRtb.Text = row.Cells["islemDetay"].Value.ToString();
                procedurePrice.Text = row.Cells["islemUcret"].Value.ToString();

                patientNameCombo.Tag = patientNameCombo.Text;
                doctorNameCombo.Tag = doctorNameCombo.Text;
                Procedurertb.Tag = Procedurertb.Text;
                procedureDate.Tag = procedureDate.Value;
                procedureDetailsRtb.Tag = procedureDetailsRtb.Text;
                procedurePrice.Tag = procedurePrice.Text;
               
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // KORUMAYI KALDIR (Artık arama yapılabilir)
                isLoading = false;
            }

        }

        private void DeleteTreatment(string islemId)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");
            try
            {
                string accessConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";

                using (OleDbConnection connection = new OleDbConnection(accessConnectionString))
                {
                    connection.Open();

                    // DÜZENLEME: Veriyi silmek yerine 'Aktif' sütununu False (Pasif) yapıyoruz.
                    string updateQuery = "UPDATE islemler SET Aktif = False WHERE islemId = @id";

                    using (OleDbCommand command = new OleDbCommand(updateQuery, connection))
                    {
                        // Parametre ekleme
                        command.Parameters.AddWithValue("@id", islemId);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Treatment deleted successfully.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Listeyi yenile ki silinen işlem ekrandan gitsin
                            LoadTreatmentsData();
                        }
                        else
                        {
                            MessageBox.Show("Treatment not found.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                  ClearTreatmentInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void procedureDeleteBtn_Click(object sender, EventArgs e)
        {
            if (dgvTreatments.SelectedRows.Count > 0)
            {
                string islemId = dgvTreatments.SelectedRows[0].Cells
                ["islemId"].Value.ToString();
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this treatment?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    DeleteTreatment(islemId);
                    ClearTreatmentInputs();

                 
                }
               
            } else
                {
                   
                     MessageBox.Show("Please select a treatment row to delete.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

        }

        //------------------------------------------------------------------------------------------------------------

        // APPOINTMENT MANAGEMENT

        private void LoadAppointmentsData()
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");
            string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};";

            try
            {
                connection = new OleDbConnection(connectionString);
                connection.Open();
                string query = "SELECT * FROM hastaRandevu WHERE Aktif = True";
                adapter = new OleDbDataAdapter(query, connection);
                dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgvAppointments.DataSource = null;
                dgvAppointments.DataSource = dataTable;
                if (dgvAppointments.Columns.Contains("Aktif"))
                {
                    dgvAppointments.Columns["Aktif"].Visible = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading appointments: " + ex.Message);
            }
            finally
            {
                connection?.Close();
            }
        }
        private void ClearAppointmentInputs()
        {
            appointmentId.Text = "";
            patientFullNameCombo2.SelectedIndex = -1;
            doctorFullNameCombo2.SelectedIndex = -1;
            appointmentTimeCb.SelectedIndex = -1;
            patientFullNameCombo2.Text = "";
            doctorFullNameCombo2.Text = "";
            appointmentTimeCb.Text = "";
            appointmentTimeCb.Items.Clear();
            appointmentDatedt.Value = DateTime.Now;
            appointmentNotes.Clear();


        }

        private void GetAvailableHours()
        {
          
            appointmentTimeCb.Items.Clear();

            // Doktor veya Tarih seçilmemişse işlem yapma
            if (doctorFullNameCombo2.SelectedIndex == -1 || appointmentDatedt.Value == null)
            {
                return;
            }

            // Dolu saatleri tutacağımız geçici liste
            List<string> bookedHours = new List<string>();


            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");
            string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    //Seçilen doktorun ve seçilen tarihteki randevularının SAATLERİNİ getir.
                    string query = "SELECT randevuSaati FROM hastaRandevu WHERE hekimAdi=@hekim AND randevuTarihi=@tarih AND Aktif=True";

                    using (OleDbCommand komut = new OleDbCommand(query, connection))
                    {

                        komut.Parameters.AddWithValue("@hekim", doctorFullNameCombo2.Text);
                        komut.Parameters.AddWithValue("@tarih", appointmentDatedt.Value.Date);

                        OleDbDataReader dr = komut.ExecuteReader();
                        while (dr.Read())
                        {
                            // Dolu olan saatleri listeye ekle
                            bookedHours.Add(dr["randevuSaati"].ToString());
                        }
                    }
                }

               
                // Tüm saatleri tek tek kontrol et. Eğer dolu listesinde YOKSA, ekrana ekle.
                foreach (string saat in allHours)
                {
                    if (!bookedHours.Contains(saat))
                    {
                        appointmentTimeCb.Items.Add(saat);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching available hours: " + ex.Message);
            }
        }

        private void appointmentAddBtn_Click(object sender, EventArgs e)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");
            string secilenHasta = patientFullNameCombo2.Text;
            string secilenDoktor = doctorFullNameCombo2.Text;
            string randevuSaat = appointmentTimeCb.Text;
            DateTime randevuTarihi = DateTime.Parse(appointmentDatedt.Text);
            string aciklama = appointmentNotes.Text;

            if (string.IsNullOrEmpty(secilenHasta) || string.IsNullOrEmpty(secilenDoktor))
            {
                MessageBox.Show("Please select a patient and a doctor.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string accessConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(accessConnectionString))
                {
                    connection.Open();

                    // Aynı doktor ve saat için randevu kontrolü
                    string checkQuery = @"SELECT COUNT(*) FROM hastaRandevu 
                    WHERE hekimAdi = ? AND randevuTarihi = ? AND randevuSaati = ? AND Aktif = True";

                    using (OleDbCommand checkCommand = new OleDbCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("?", secilenDoktor);
                        checkCommand.Parameters.AddWithValue("?", randevuTarihi);
                        checkCommand.Parameters.AddWithValue("?", randevuSaat);

                        int existingAppointments = (int)checkCommand.ExecuteScalar();

                        if (existingAppointments > 0)
                        {
                            MessageBox.Show("There is already an appointment for this doctor at this time. Please select another time.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Eğer kontrol geçildiyse randevu ekle
                    string insertQuery = @" INSERT INTO hastaRandevu
                       (hastaAdi, hekimAdi, randevuTarihi, randevuSaati, hastaAciklama) VALUES (?, ?, ?, ?, ?)";

                    using (OleDbCommand command = new OleDbCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("?", secilenHasta ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("?", secilenDoktor ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("?", randevuTarihi);
                        command.Parameters.AddWithValue("?", randevuSaat);
                        command.Parameters.AddWithValue("?", aciklama ?? (object)DBNull.Value);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Appointment created successfully.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAppointmentsData();
                ClearAppointmentInputs();

            }

            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }

        private void appointmentDeleteBtn_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count > 0)
            {
                string secilenHasta = dgvAppointments.SelectedRows[0].Cells
                ["randevuId"].Value.ToString();
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this appointment?", "CONFIRMATION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    DeleteAppointment(secilenHasta);
                    ClearAppointmentInputs();
                }
                else
                {
                    MessageBox.Show("Please select an appointment to delete.");
                }
            }
        }

        private void DeleteAppointment(string secilenHasta)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");
            try
            {
                string accessConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";

                using (OleDbConnection connection = new OleDbConnection(accessConnectionString))
                {
                    connection.Open();

                    // DEĞİŞİKLİK: Kaydı silmek yerine 'Aktif' sütununu False yapıyoruz.
                    string updateQuery = "UPDATE hastaRandevu SET Aktif = False WHERE randevuId = @id";

                    using (OleDbCommand command = new OleDbCommand(updateQuery, connection))
                    {
                        // Parametre ekleme (secilenHasta değişkeni randevu ID'sini taşıyor)
                        command.Parameters.AddWithValue("@id", secilenHasta);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Appointment deleted successfully.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Listeyi yenile ki silinen randevu ekrandan gitsin
                            LoadAppointmentsData();
                        }
                        else
                        {
                            MessageBox.Show("Appointment not found.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void appointmentUpdateBtn_Click(object sender, EventArgs e)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb"); ;
            string randevuId = appointmentId.Text;
            string hastaAdi = patientFullNameCombo2.Text;
            string hekimAdi = doctorFullNameCombo2.Text;
            DateTime randevuTarihi = DateTime.Parse
            (appointmentDatedt.Text);
            string randevuSaati = appointmentTimeCb.Text;
            string hastaAciklama = appointmentNotes.Text;
            try
            {
                string accessConnectionString =
                $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";

                using (OleDbConnection connection = new OleDbConnection(accessConnectionString))
                {
                    connection.Open();
                    string updateQuery = @"UPDATE hastaRandevu
               SET hastaAdi = ?, hekimAdi = ?, randevuTarihi= ?,randevuSaati = ?, hastaAciklama = ?
               WHERE randevuId = ?";
                    using (OleDbCommand command = new OleDbCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("?",
                        hastaAdi);
                        command.Parameters.AddWithValue("?",
                        hekimAdi);
                        command.Parameters.AddWithValue("?",
                        randevuTarihi);
                        command.Parameters.AddWithValue("?",
                        randevuSaati);
                        command.Parameters.AddWithValue("?",
                        hastaAciklama);
                        command.Parameters.AddWithValue("?",
                        randevuId);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            List<string> changes = new List<string>();

                            Action<System.Windows.Forms.Control, string> CheckChange = (ctrl, label) =>
                            {
                                if (ctrl.Tag != null && ctrl.Tag.ToString() != ctrl.Text)
                                    changes.Add($"{label}: {ctrl.Tag} → {ctrl.Text}");
                            };

                            Action<DateTimePicker, string> CheckDate = (date, label) =>
                            {
                                if (date.Tag != null && ((DateTime)date.Tag).ToShortDateString() != date.Value.ToShortDateString())
                                    changes.Add($"{label}: {((DateTime)date.Tag).ToShortDateString()} → {date.Value.ToShortDateString()}");
                            };

                            // Kontrol et
                            CheckChange(patientFullNameCombo2, "Hasta");
                            CheckChange(doctorFullNameCombo2, "Doktor");
                            CheckDate(appointmentDatedt, "Randevu Tarihi"); 
                            CheckChange(appointmentTimeCb, "Randevu Saati");
                            CheckChange(appointmentNotes, "Açıklama");

                            
                            MessageBox.Show("Appointment details updated successfully.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadAppointmentsData();
                            ClearAppointmentInputs();
                        }
                        else
                        {
                            MessageBox.Show("Appointment not found to update.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void dgvAppointments_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            isLoading = true;
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvAppointments.Rows[e.RowIndex];
                    appointmentId.Text = row.Cells["randevuId"].Value.ToString();
                    patientFullNameCombo2.Text = row.Cells
                    ["hastaAdi"].Value.ToString();
                    doctorFullNameCombo2.Text = row.Cells
                    ["hekimAdi"].Value.ToString();
                    appointmentDatedt.Value = Convert.ToDateTime(row.Cells
                    ["randevuTarihi"].Value);
                    appointmentTimeCb.Text = row.Cells
                    ["randevuSaati"].Value.ToString();
                    appointmentNotes.Text = row.Cells
                    ["hastaAciklama"].Value.ToString();

                    patientFullNameCombo2.Tag = patientFullNameCombo2.Text;
                    doctorFullNameCombo2.Tag = doctorFullNameCombo2.Text;
                    appointmentDatedt.Tag = appointmentDatedt.Value;
                    appointmentTimeCb.Tag = appointmentTimeCb.Text;
                    appointmentNotes.Tag = appointmentNotes.Text;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { isLoading = false; }
        }

        //----------------------------------------------------------------------------------------------------------
       
        private void btnPatientReg_Click(object sender, EventArgs e)
        {
            grpAddPatient.Visible = true;
            grpAddDoctor.Visible = false;
            grpTreatmentEntry.Visible = false;
            dentalLogo.Visible = false;
            grpAppointmentManagement.Visible = false;




        }

        private void btnDoctorReg_Click(object sender, EventArgs e)
        {

            grpAddDoctor.Visible = true;
            grpTreatmentEntry.Visible = false;
            dentalLogo.Visible = false;
            grpAppointmentManagement.Visible = false;
            grpAddPatient.Visible = false;
        }

        private void btnTreatmentReg_Click(object sender, EventArgs e)
        {
            grpAddPatient.Visible = false;
            grpAddDoctor.Visible = false;
            grpTreatmentEntry.Visible = true;
            dentalLogo.Visible = false;
            grpAppointmentManagement.Visible = false;

        }

        private void btnAppointment_Click(object sender, EventArgs e)
        {
            grpAddPatient.Visible = false;
            grpAddDoctor.Visible = false;
            grpTreatmentEntry.Visible = false;
            dentalLogo.Visible = false;
            grpAppointmentManagement.Visible = true;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            date.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
        }

       
      

   

        private void btnPrescription_Click(object sender, EventArgs e)
        {
            PrescriptionForm prescriptionReport = new PrescriptionForm();
            prescriptionReport.Show();
        }

        void UpdatePatientCount()
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");
            if (!File.Exists(dbPath))
            {
                MessageBox.Show("Database file not found.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string accessConnectionString =
            $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";
            try
            {
                using (OleDbConnection connection = new
                OleDbConnection(accessConnectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM hastaKayit WHERE Aktif = True";
                    using (OleDbCommand command = new OleDbCommand
                    (query, connection))
                    {
                        int hastaSayisi = (int)command.ExecuteScalar
                        ();
                        lblTotalPatients.Text = $"{hastaSayisi}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }
        void UpdateTreatmentCount()
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");
            if (!File.Exists(dbPath))
            {
                MessageBox.Show("Database file not found.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string accessConnectionString =
            $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";
            try
            {
                using (OleDbConnection connection = new
                OleDbConnection(accessConnectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM islemler WHERE Aktif = True";
                    using (OleDbCommand command = new OleDbCommand
                    (query, connection))
                    {
                        int islemSayisi = (int)command.ExecuteScalar
                        ();
                        lblTotalTreatments.Text = $"{islemSayisi}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }

        private void menuClearPatients_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Are you sure you want to delete ALL patients? This cannot be undone.",
                                                  "CRITICAL WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.No) 
                return;
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");
            if (!File.Exists(dbPath))
            {
                MessageBox.Show("Database file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string accessConnectionString =
            $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";
            try
            {
                using (OleDbConnection connection = new
                OleDbConnection(accessConnectionString))
                {

                    connection.Open();
                    string query = "DELETE FROM hastaKayit";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        int affectedRows = command.ExecuteNonQuery();
                        MessageBox.Show($"{affectedRows} records deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       
                        LoadPatientData();
                        UpdatePatientCount();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuClearTreatments_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Are you sure you want to delete ALL treatment records? This cannot be undone.",
                                          "CRITICAL WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.No)
                return;
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");
            if (!File.Exists(dbPath))
            {
                MessageBox.Show("Database file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string accessConnectionString =
            $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";
            try
            {
                using (OleDbConnection connection = new
                OleDbConnection(accessConnectionString))
                {

                    connection.Open();
                    string query = "DELETE FROM islemler";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        int affectedRows = command.ExecuteNonQuery();
                        MessageBox.Show($"{affectedRows} records deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadTreatmentsData();
                        UpdateTreatmentCount();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuClearDoctors_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Are you sure you want to delete ALL doctors? This cannot be undone.",
                                          "CRITICAL WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.No)
                return;
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");
            if (!File.Exists(dbPath))
            {
                MessageBox.Show("Database file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string accessConnectionString =
            $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";
            try
            {
                using (OleDbConnection connection = new
                OleDbConnection(accessConnectionString))
                {

                    connection.Open();
                    string query = "DELETE FROM hekimBilgi";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        int affectedRows = command.ExecuteNonQuery();
                        MessageBox.Show($"{affectedRows} records deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDoctorData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuClearAppointments_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete ALL appointments? This cannot be undone.",
                                          "CRITICAL WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.No)
                return;

            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");
            if (!File.Exists(dbPath))
            {
                MessageBox.Show("Database file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string accessConnectionString =
            $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";
            try
            {
                using (OleDbConnection connection = new
                OleDbConnection(accessConnectionString))
                {

                    connection.Open();
                    string query = "DELETE FROM hastaRandevu";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        int affectedRows = command.ExecuteNonQuery();
                        MessageBox.Show($"{affectedRows} records deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuClearExpenses_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Are you sure you want to delete ALL expense/cash records? This cannot be undone.",
                                          "CRITICAL WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.No)
                return;
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");
            if (!File.Exists(dbPath))
            {
                MessageBox.Show("Database file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string accessConnectionString =
            $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";
            try
            {
                using (OleDbConnection connection = new
                OleDbConnection(accessConnectionString))
                {

                    connection.Open();
                    string query = "DELETE FROM Kasa";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        int affectedRows = command.ExecuteNonQuery();
                        MessageBox.Show($"{affectedRows} records deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      
     

        private void menuAddItem_Click(object sender, EventArgs e)
        {
            InventoryForm ınventoryForm = new InventoryForm();
            ınventoryForm.Show();
        }

        private void dgvPatientList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvPatientList.Columns[e.ColumnIndex].Name == "hastaTc")
            {
                // Hücre değeri boş değilse ve 11 haneli bir TC numarası ise
                if (e.Value != null && e.Value.ToString().Length == 11)
                {
                    string tcNo = e.Value.ToString();
                    // İlk 2 ve son 2 haneyi sabit tut, ortadaki 7 haneyi yıldız yap
                    e.Value = tcNo.Substring(0, 2) + new string('*', 7) + tcNo.Substring(9, 2);
                }
            }
        }

        private void dgvDoctors_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvDoctors.Columns[e.ColumnIndex].Name == "doktorTc")
            {
                // Hücre değeri boş değilse ve 11 haneli bir TC numarası ise
                if (e.Value != null && e.Value.ToString().Length == 11)
                {
                    string tcNo = e.Value.ToString();
                    // İlk 2 ve son 2 haneyi sabit tut, ortadaki 7 haneyi yıldız yap
                    e.Value = tcNo.Substring(0, 2) + new string('*', 7) + tcNo.Substring(9, 2);
                }
            }
        }

        private void patientSearchTxt_TextChanged(object sender, EventArgs e)
        {
            (dgvPatientList.DataSource as DataTable).DefaultView.RowFilter = $"hastaAdi LIKE '%{patientSearchTxt.Text}%'";
        }
       
        private void procedureSearchTxt_TextChanged(object sender, EventArgs e)
        {
            (dgvTreatments.DataSource as DataTable).DefaultView.RowFilter = $"secilenHasta LIKE '%{procedureSearchTxt.Text}%'";

        }
        
        private void doctorSearchtxt_TextChanged(object sender, EventArgs e)
        {
            (dgvDoctors.DataSource as DataTable).DefaultView.RowFilter = $"doktorAdi LIKE '%{doctorSearchtxt.Text}%'";

        }

        private void appointmentSearchTxt_TextChanged(object sender, EventArgs e)
        {
            (dgvAppointments.DataSource as DataTable).DefaultView.RowFilter = $"hastaAdi LIKE '%{appointmentSearchTxt.Text}%'";

        }

        private void menuBackupDb_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ DÜZELTİLEN KISIM: Kaynak dosya artık programın yanındaki dosya
                string sourcePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");

                // Dosya kontrolü
                if (!System.IO.File.Exists(sourcePath))
                {
                    // Mesajı da düzelttik (Belgelerim yazısını kaldırdık)
                    MessageBox.Show("Error: Database file not found in source directory!\nPath: " + sourcePath, "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                }

                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Access Veritabanı|*.accdb";
                // Dosya isminde tarih saat olması süper, ona dokunmuyoruz
                save.FileName = "DisKlinik_Yedek_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm");
                save.Title = "Where would you like to save the backup?";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    // Kaynak (StartupPath) -> Hedef (Kullanıcının Seçtiği Yer) kopyalanıyor
                    System.IO.File.Copy(sourcePath, save.FileName, true);

                    // Dosya boyutunu gösterme
                    long fileSize = new System.IO.FileInfo(save.FileName).Length;
                    MessageBox.Show($"Backup completed successfully!\n\nBackup Size: {fileSize / 1024} KB", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred during backup:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void appointmentDatedt_ValueChanged(object sender, EventArgs e)
        {
            GetAvailableHours();
        }

        private void doctorFullNameCombo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAvailableHours();
        }

        private void Dashboard_Resize(object sender, EventArgs e)
        {
            pnlSidebar.Left = 0;          
            pnlSidebar.Top = pictureBox2.Bottom;         
            pnlSidebar.Width = 171;           
            pnlSidebar.Height = this.ClientSize.Height - pictureBox2.Height;
            panel1.Top = this.ClientSize.Height - panel1.Height;           
            panel1.Left = 0; 
            panel1.Width = this.ClientSize.Width;

            int leftPadding = 10; 
            int rightPadding = 10;         
            int newLeft = pnlSidebar.Width + leftPadding;
            int newWidth = this.ClientSize.Width - newLeft - rightPadding;

            // hata kontrolü
            if (newWidth > 0)
            {
                grpAddPatient.Left = newLeft;
                grpAddPatient.Width = newWidth;

                grpTreatmentEntry.Left = newLeft;
                grpTreatmentEntry.Width = newWidth;

                 grpAddDoctor.Left = newLeft;
                 grpAddDoctor.Width = newWidth;

                 grpAppointmentManagement.Left = newLeft;
                 grpAppointmentManagement.Width = newWidth;
            }
        }

        private void menuLegalNotice_Click(object sender, EventArgs e)
        {
            string legalText = "This automation system has been designed in accordance with the Law on the Protection of Personal Data No. 6698 (KVKK) " +
                       "and relevant copyright legislation.\n\n" +
                       "The software design, source codes, and all intellectual property rights " +
                       "belong to Dilara Nursema İnel.\n\n" +
                       "Copying, distributing, or using this software for commercial purposes without the " +
                       "written permission of the software owner is prohibited. By using this system, " +
                       "users are deemed to have accepted and committed to complying with data privacy and security principles.";

            // Başlık: YASAL UYARI -> LEGAL NOTICE AND ABOUT
            MessageBox.Show(legalText, "LEGAL NOTICE AND ABOUT", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
    

        private void Dashboard_MouseMove(object sender, MouseEventArgs e)
        {
            lockTimer.Stop();  // Sayacı durdur
            lockTimer.Start(); // Yeniden başlat (Sıfırla)
        }

        private void Dashboard_KeyPress(object sender, KeyPressEventArgs e)
        {
            lockTimer.Stop();  // Sayacı durdur
            lockTimer.Start(); // Yeniden başlat (Sıfırla)
        }

        private void lockTimer_Tick(object sender, EventArgs e)
        {
            lockTimer.Stop();

            MessageBox.Show("Your session has been locked for security reasons. Please log in again.",
                      "SESSION LOCKED", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            LoginForm loginForm = new LoginForm();
            loginForm.Show();

            // 4. Ana Sayfayı gizle
            this.Hide();
        }

        private void themeTeal_Click(object sender, EventArgs e)
        {
            pnlSidebar.BackColor = ColorTranslator.FromHtml("#008B8B");
        }

        private void themeAnthracite_Click(object sender, EventArgs e)
        {
            pnlSidebar.BackColor = ColorTranslator.FromHtml("#34495E");
        }

        private void themeSteelBlue_Click(object sender, EventArgs e)
        {
            pnlSidebar.BackColor = ColorTranslator.FromHtml("#4682B4");
        }

        private void themeNavyBlue_Click(object sender, EventArgs e)
        {
            pnlSidebar.BackColor = ColorTranslator.FromHtml("#000080");   
        }

        private void menuUserManual_Click(object sender, EventArgs e)
        {
            UserManualForm guide = new UserManualForm();
            guide.Show();
        }


        //Whatsapp Reminder
        private string GetPatientPhone(string patientName)
        {
            string phoneNumber = "";
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory  , "disKlinik.accdb");
            string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    // Veritabanındaki telefon sütununun adı 'hastaTelefon' varsayılmıştır.
                    // Eğer farklıysa (örn: telefon, cepTel) burayı düzeltmelisin.
                    // Eski sorguyu şununla değiştir:
                    string query = "SELECT hastaTelNo FROM hastaKayit WHERE (hastaAdi & ' ' & hastaSoyadi) = @name OR hastaAdi = @name";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@name", patientName);
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            phoneNumber = result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching phone number: " + ex.Message);
            }

            return phoneNumber;
        }

        
       

        private void patientFullNameCombo2_TextChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            if (dtPatient.Rows.Count == 0) return;

           

            try
            {
              
                string userText2 = patientFullNameCombo2.Text;
                int cursorPosition2 = patientFullNameCombo2.SelectionStart;

                DataView dv = dtPatient.DefaultView;

                
                if (string.IsNullOrEmpty(userText2))
                {
                    dv.RowFilter = "";
                }
                else
                {
                    string sanitizedText = userText2.Replace("'", "''");
                    dv.RowFilter = $"AdSoyad LIKE '%{sanitizedText}%'";
                }

                patientFullNameCombo2.Text = userText2;

          
                patientFullNameCombo2.SelectionStart = cursorPosition2;

              
                if (patientFullNameCombo2.Items.Count > 0 && !string.IsNullOrEmpty(userText2))
                {
                    patientFullNameCombo2.DroppedDown = true;
                    Cursor.Current = Cursors.Default; 
                }
            }
            catch
            {
             
            }
        }
        

        private void patientNameCombo_TextChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            if (dtPatient2.Rows.Count == 0) return;

        
            

            try
            {
               
                string userText = patientNameCombo.Text;
                int cursorPosition = patientNameCombo.SelectionStart;

                DataView dv = dtPatient2.DefaultView;

             
                if (string.IsNullOrEmpty(userText))
                {
                    dv.RowFilter = "";
                }
                else
                {
                    string sanitizedText = userText.Replace("'", "''");
                    dv.RowFilter = $"AdSoyad LIKE '%{sanitizedText}%'";
                }

              
                patientNameCombo.Text = userText;

               
                patientNameCombo.SelectionStart = cursorPosition;

               
                if (patientNameCombo.Items.Count > 0 && !string.IsNullOrEmpty(userText))
                {
                    patientNameCombo.DroppedDown = true;
                    Cursor.Current = Cursors.Default; 
                }
            }
            catch
            {
                
            }
        }

        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void menuClearInventory_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete ALL inventory records? This cannot be undone.",
                                          "CRITICAL WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.No)
                return;
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "disKlinik.accdb");
            if (!File.Exists(dbPath))
            {
                MessageBox.Show("Database file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string accessConnectionString =
            $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";
            try
            {
                using (OleDbConnection connection = new
                OleDbConnection(accessConnectionString))
                {

                    connection.Open();
                    string query = "DELETE FROM Malzeme";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        int affectedRows = command.ExecuteNonQuery();
                        MessageBox.Show($"{affectedRows} records deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrintAppointmentList_Click(object sender, EventArgs e)
        {

        }
    }
}

