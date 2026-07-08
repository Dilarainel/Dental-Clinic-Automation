using System;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace DisKlinikOtomasyon
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "disKlinik.accdb");
            string userName = txtUsername.Text.Trim();
            string pass = txtPassword.Text.Trim();

            // Boşluk kontrolü
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Username and password cannot be left blank!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool loginSuccessful = false; // Giriş bayrağı

            // SENARYO 1: DOSYA YOKSA (İLK KURULUM)
            if (!File.Exists(dbPath))
            {
                if (userName == "admin" && pass == "12345")
                {
                    MessageBox.Show("System setup mode initiated.", "INITIAL SETUP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loginSuccessful = true; // Bayrağı kaldır
                }
                else
                {
                    MessageBox.Show("Database not found! Login with Admin to setup.", "SETUP REQUIRED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            // SENARYO 2: DOSYA VARSA (NORMAL GİRİŞ)
            else
            {
                try
                {
                    string accessConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";
                    using (OleDbConnection connection = new OleDbConnection(accessConnectionString))
                    {
                        connection.Open();
                        string loginQuery = @"SELECT COUNT(*) FROM KullaniciBilgi WHERE userName = @kullaniciAdi AND userPass = @sifre";
                        using (OleDbCommand command = new OleDbCommand(loginQuery, connection))
                        {
                            command.Parameters.AddWithValue("@kullaniciAdi", userName);
                            command.Parameters.AddWithValue("@sifre", pass);

                            int count = (int)command.ExecuteScalar();

                            if (count > 0)
                            {
                                loginSuccessful = true; // BURAYI DEĞİŞTİRDİM: Direkt açmak yerine bayrağı kaldırıyoruz.
                            }
                            else
                            {
                                MessageBox.Show("Invalid username or password.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // SONUÇ: EĞER GİRİŞ (HERHANGİ BİR YOLDAN) BAŞARILIYSA
            if (loginSuccessful)
            {
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
                this.Hide();
            }
        }



        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            NewUserForm newUserForm = new NewUserForm();
            newUserForm.Show();

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
        }
    }
}
