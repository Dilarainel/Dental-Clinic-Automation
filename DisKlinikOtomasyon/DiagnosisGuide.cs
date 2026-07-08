using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisKlinikOtomasyon
{
    public partial class DiagnosisGuide : Form
    {
        public DiagnosisGuide()
        {
            InitializeComponent();
        }

        private void DiagnosisGuide_Load(object sender, EventArgs e)
        {
            // 1. Dosyanın yerini belirliyoruz (bin/Debug içindeki dosya)
            string pdfPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TaniRehberi.pdf");

            // 2. Dosya orada mı diye bakıyoruz
            if (System.IO.File.Exists(pdfPath))
            {
                try { 
                // 3. Dosyayı Adobe aracına yüklüyoruz
                // (Aracın adı genelde axAcroPDF1 olur, değilse düzelt)
                axAcroPDF1.LoadFile(pdfPath);
                axAcroPDF1.setShowToolbar(false); // Menüleri gizler, daha şık durur
                 }
                catch (Exception ex)
                {
                    // PDF aracı bazen hata verebilir (COM hatası), bunu yakalamak güvenlidir.
                    MessageBox.Show("Error initializing PDF viewer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("The diagnosis guide file (TaniRehberi.pdf) was not found in the application directory.",
                            "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
