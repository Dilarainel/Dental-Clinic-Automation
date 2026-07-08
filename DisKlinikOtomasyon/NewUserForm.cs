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
    public partial class NewUserForm : Form
    {
        public NewUserForm()
        {
            InitializeComponent();
        }

        private OleDbConnection connection;
        private OleDbDataAdapter adapter;
        private DataTable dataTable;

        

        private void btnSave_Click(object sender, EventArgs e)
        {

            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "disKlinik.accdb");
            // TextBox'lardan verileri al
            string userNamee = userName.Text;
            string passs = pass.Text;
            if(string.IsNullOrEmpty(userNamee) || string.IsNullOrEmpty(passs))
            {
                MessageBox.Show("Username and password cannot be left blank!",
              "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            try
            {
                string accessConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";
                using (OleDbConnection connection = new OleDbConnection(accessConnectionString))
                {
                    connection.Open();
                    string insertQuery = @"
                    INSERT INTO KullaniciBilgi
                    (userName, userPass)
                    VALUES (?, ?)";
                    using (OleDbCommand command = new OleDbCommand
                    (insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("?",
                        userNamee);
                        command.Parameters.AddWithValue("?",
                        passs);

                        command.ExecuteNonQuery();
                    }

                }
                MessageBox.Show("User registration has been completed successfully.", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
    }
}
