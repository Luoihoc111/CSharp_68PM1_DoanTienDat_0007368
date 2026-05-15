using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormApp
{
    public partial class Form2 : Form
    {
        private List<SinhVien> _dsSinhVien = new List<SinhVien>();
        private List<string> _dsLopHoc = new List<string>();
        private int _trangHienTai = 1;
        private const int _soDoiMoiTrang = 10;
        private List<SinhVien> _dsHienThi = new List<SinhVien>();

        public Form2()
        {
            InitializeComponent();
            TaoDuLieuMau();
            LoadLopVaoComboBox();
            HienThiDanhSach(_dsSinhVien);
        }

        private void Form2_Load(object sender, EventArgs e) { }

        private void TaoDuLieuMau()
        {
            _dsLopHoc.Add("68PM1");
            _dsLopHoc.Add("68PM2");
            _dsLopHoc.Add("68PM3");

            _dsSinhVien.Add(new SinhVien { MaSV = "1", HoTen = "Doan Tien Dat", GioiTinh = "Nam", NgaySinh = new DateTime(2026, 2, 17), Lop = "68PM1" });
            _dsSinhVien.Add(new SinhVien { MaSV = "2", HoTen = "Nguyễn Văn A", GioiTinh = "Nam", NgaySinh = new DateTime(2026, 3, 11), Lop = "68PM2" });

        }

        private void LoadLopVaoComboBox()
        {
            comboBox2.Items.Clear();
            foreach (var lop in _dsLopHoc)
                comboBox2.Items.Add(lop);
            if (comboBox2.Items.Count > 0)
                comboBox2.SelectedIndex = 0;
        }

        private void HienThiDanhSach(List<SinhVien> ds)
        {
            _dsHienThi = ds;
            int tongBanGhi = ds.Count;
            int tongTrang = (int)Math.Ceiling((double)tongBanGhi / _soDoiMoiTrang);
            if (tongTrang == 0) tongTrang = 1;
            if (_trangHienTai > tongTrang) _trangHienTai = tongTrang;

            int batDau = (_trangHienTai - 1) * _soDoiMoiTrang;
            int ketThuc = Math.Min(batDau + _soDoiMoiTrang, tongBanGhi);

            dataGridView1.Rows.Clear();
            for (int i = batDau; i < ketThuc; i++)
            {
                var sv = ds[i];
                dataGridView1.Rows.Add(
                    sv.MaSV,
                    sv.HoTen,
                    sv.GioiTinh,
                    sv.NgaySinh.ToString("dd/MM/yyyy"),
                    sv.Lop
                );
            }

            label7.Text = $"Trang {_trangHienTai}/{tongTrang}  |  {tongBanGhi} bản ghi";
        }

        private void ResetForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            dateTimePicker1.Value = DateTime.Today;
            comboBox1.SelectedIndex = 0;
            if (comboBox2.Items.Count > 0) comboBox2.SelectedIndex = 0;
            textBox1.Focus();
        }


        private void btnThem_Click(object sender, EventArgs e) { }
        private void btnSua_Click(object sender, EventArgs e) { }
        private void btnXoa_Click(object sender, EventArgs e) { }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void btnTimKiem_Click(object sender, EventArgs e) { }


        private void btnTrangDau_Click(object sender, EventArgs e) { }
        private void btnTrangTruoc_Click(object sender, EventArgs e) { }
        private void btnTrangSau_Click(object sender, EventArgs e) { }
        private void btnTrangCuoi_Click(object sender, EventArgs e) { }


        private void menuQuanLyLopHoc_Click(object sender, EventArgs e) { }
        private void menuDangXuat_Click(object sender, EventArgs e) { }


        private void label1_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void sinhVienToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }

    public class SinhVien
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Lop { get; set; }
    }
}