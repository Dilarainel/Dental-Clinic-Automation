using Microsoft.Reporting.WinForms;
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
    public partial class SickLeaveForm : Form
    {
        public SickLeaveForm()
        {
            InitializeComponent();
        }

      
        public DataSet CreateSickLeaveDataSet(string patientName,string nationalId,string birthDate,string diagnosis,string startDate,
            string endDate,string totalDays,string address,string doctorName)
        {  
            // Yeni bir DataSet ve DataTable oluştur
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("SickLeaveDT");
            // Kolonları tanımla
            dt.Columns.Add("Tc", typeof(string));
            dt.Columns.Add("adSoyad", typeof(string));
            dt.Columns.Add("dogumTarihi", typeof(string));
            dt.Columns.Add("tani", typeof(string));
            dt.Columns.Add("baslangıcTarihi", typeof(string));
            dt.Columns.Add("bitisTarihi", typeof(string));
            dt.Columns.Add("toplamGun", typeof(string));
            dt.Columns.Add("adres", typeof(string));
            dt.Columns.Add("doktorAdi", typeof(string));
            // Satır ekle ve alanları doldur
            DataRow row = dt.NewRow();
            row["Tc"] = nationalId;
            row["adSoyad"] = patientName;
            row["dogumTarihi"] = birthDate;
            row["tani"] = diagnosis;
            row["baslangıcTarihi"] = startDate;
            row["bitisTarihi"] = endDate;
            row["toplamGun"] = totalDays;
            row["adres"] = address;
            row["doktorAdi"] = doctorName;
            dt.Rows.Add(row);
            // DataTable'i DataSet'e ekle
            ds.Tables.Add(dt);
            return ds;
        }
        // 2. Rapor Verileri Metodu
        public void SetReportData(string patientName, string nationalId, string birthDate, string diagnosis, string startDate,
            string endDate, string totalDays, string address, string doctorName)
        {
            // Veri setini oluştur
            DataSet ds = CreateSickLeaveDataSet(patientName,nationalId, birthDate, diagnosis, startDate, endDate, totalDays, address, doctorName);
            // ReportViewer için veri kaynağı oluştur ve ekle
            ReportDataSource rds = new ReportDataSource
            ("DataSet1", ds.Tables[0]);
            rvSickLeave.LocalReport.DataSources.Clear();
            rvSickLeave.LocalReport.DataSources.Add(rds);
            rvSickLeave.LocalReport.Refresh();
            rvSickLeave.RefreshReport();
        }

        private void SickLeaveForm_Load(object sender, EventArgs e)
        {

        }
    }
}
