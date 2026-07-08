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
    public partial class InventoryForm : Form
    {
        public InventoryForm()
        {
            InitializeComponent();
        }
        private OleDbConnection connection;
        private OleDbDataAdapter adapter;
        private DataTable dataTable;
        

        void ClearFields()
        {
            txtItemName.Clear();
            txtQuantity.Clear();
            txtSupplier.Clear();
            txtDescription.Clear();
          
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "disKlinik.accdb");
            string itemName = txtItemName.Text;
            string quantity = txtQuantity.Text;
            string supplier = txtSupplier.Text;
            string description = txtDescription.Text;

            if (string.IsNullOrEmpty(itemName) || string.IsNullOrEmpty(quantity))
            {
                MessageBox.Show("Please fill in the Item Name and Quantity fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            string accessConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(accessConnectionString))
                {
                    connection.Open();
                    string insertQuery = @"
                    INSERT INTO Malzeme
                    (malzemeAdi, malzemeAdedi, firmaAdi, malzemeAciklama, Aktif) VALUES (?, ?, ?, ?, ?)";

                    using (OleDbCommand command = new OleDbCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("?", itemName );
                        command.Parameters.AddWithValue("?", quantity );
                        command.Parameters.AddWithValue("?", supplier);
                        command.Parameters.AddWithValue("?", description);
                        command.Parameters.AddWithValue("?", true);

                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Item added successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearFields(); // Temizle
                LoadInventoryData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadInventoryData()
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "disKlinik.accdb");

            string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";
            try
            {
                connection = new OleDbConnection(connectionString);
                connection.Open();
                string query = "SELECT * FROM Malzeme WHERE Aktif = True";
                adapter = new OleDbDataAdapter(query, connection);
                dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgvInventory.DataSource = dataTable;
                if (dgvInventory.Columns.Contains("Aktif"))
                {
                    dgvInventory.Columns["Aktif"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {
                connection?.Close();
            }
        }

        private void InventoryForm_Activated(object sender, EventArgs e)
        {
            LoadInventoryData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count > 0)
            {
                string selectedId = dgvInventory.SelectedRows[0].Cells
                ["malzemeId"].Value.ToString();
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    DeleteInventoryItem(selectedId);
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Please select a row to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void DeleteInventoryItem(string id)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "disKlinik.accdb");
            try
            {
                string accessConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}; ";

                using (OleDbConnection connection = new OleDbConnection(accessConnectionString))
                {
                    connection.Open();

                    // DÜZENLEME: Veriyi silmek yerine 'Aktif' sütununu False (Pasif) yapıyoruz.
                    string updateQuery = "UPDATE Malzeme SET Aktif = False WHERE malzemeId = @id";

                    using (OleDbCommand command = new OleDbCommand(updateQuery, connection))
                    {
                        // Parametre ekleme
                        command.Parameters.AddWithValue("@id", id);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Item deleted successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Listeyi yenile ki silinen malzeme ekrandan gitsin
                            LoadInventoryData();
                        }
                        else
                        {
                            MessageBox.Show("Item not found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        



        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (dgvInventory.DataSource is DataTable dt)
            {
                // RowFilter ile anlık filtreleme 
                // Escape karakteri (') hatasını önlemek için Replace yaptık
                string filterText = txtSearch.Text.Replace("'", "''");
                dt.DefaultView.RowFilter = $"malzemeAdi LIKE '%{filterText}%' OR firmaAdi LIKE '%{filterText}%'";
            }
        }

        private void dgvInventory_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvInventory.Rows[e.RowIndex];

                txtItemName.Text = row.Cells
                ["malzemeAdi"].Value.ToString();
                txtQuantity.Text = row.Cells
                ["malzemeAdedi"].Value.ToString();
                txtSupplier.Text = row.Cells
                ["firmaAdi"].Value.ToString();
                txtDescription.Text = row.Cells
                ["malzemeAciklama"].Value.ToString();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }
    }
}
