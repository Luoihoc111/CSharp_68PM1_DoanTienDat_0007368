using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormApp
{
    public partial class UCQuanLySinhVien : UserControl
    {
        private bool _dangThem = false;

        public UCQuanLySinhVien()
        {
            InitializeComponent();
            LoadLopVaoComboBox();
            LoadData();
            DatTrangThaiForm(false);
        }

        // ══════════════════════════════════════════════
        //  LOAD COMBOBOX BẰNG LINQ
        // ══════════════════════════════════════════════
        private void LoadLopVaoComboBox()
        {
            comboBox2.Items.Clear();
            using (var db = new AppDbContext())
            {
                var dsLop = db.LopHocs.Select(l => l.malop).ToList();
                foreach (var lop in dsLop)
                    comboBox2.Items.Add(lop);
            }
            if (comboBox2.Items.Count > 0) comboBox2.SelectedIndex = 0;
        }

        // ══════════════════════════════════════════════
        //  HIỂN THỊ DANH SÁCH (LINQ)
        // ══════════════════════════════════════════════
        private void LoadData()
        {
            using (var db = new AppDbContext())
            {
                var dsSinhVien = db.SinhViens.OrderBy(s => s.id).ToList();

                dataGridView1.Rows.Clear();
                foreach (var sv in dsSinhVien)
                {
                    dataGridView1.Rows.Add(
                        sv.id.ToString(),
                        sv.hoten,
                        sv.gioitinh,
                        sv.ngaysinh?.ToString("dd/MM/yyyy") ?? "",
                        sv.malop
                    );
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridView1.Rows[e.RowIndex];
            textBox1.Text = row.Cells[0].Value?.ToString();
            textBox2.Text = row.Cells[1].Value?.ToString();

            string gt = row.Cells[2].Value?.ToString();
            if (comboBox1.Items.Contains(gt))
                comboBox1.SelectedItem = gt;

            if (DateTime.TryParseExact(row.Cells[3].Value?.ToString(), "dd/MM/yyyy",
                    null, System.Globalization.DateTimeStyles.None, out DateTime ngay))
                dateTimePicker1.Value = ngay;

            string malop = row.Cells[4].Value?.ToString();
            if (comboBox2.Items.Contains(malop))
                comboBox2.SelectedItem = malop;
        }

        // ══════════════════════════════════════════════
        //  THÊM DỮ LIỆU
        // ══════════════════════════════════════════════
        private void btnThem_Click(object sender, EventArgs e)
        {
            ResetForm();
            using (var db = new AppDbContext())
            {
                int maxId = db.SinhViens.Any() ? db.SinhViens.Max(s => s.id) : 0;
                textBox1.Text = (maxId + 1).ToString();
            }
            _dangThem = true;
            DatTrangThaiForm(true);
            textBox2.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e) { }

        private void btnXoa_Click(object sender, EventArgs e) { }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus(); return;
            }

            using (var db = new AppDbContext())
            {
                if (_dangThem)
                {
                    var svMoi = new SinhVien
                    {
                        id       = int.Parse(textBox1.Text),
                        hoten    = textBox2.Text.Trim(),
                        gioitinh = comboBox1.Text,
                        ngaysinh = dateTimePicker1.Value,
                        malop    = comboBox2.Text
                    };
                    db.SinhViens.Add(svMoi);
                    db.SaveChanges();
                    MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            DatTrangThaiForm(false);
            LoadData();
        }

        // ══════════════════════════════════════════════
        //  CÁC NÚT CHỨC NĂNG KHÁC
        // ══════════════════════════════════════════════
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
            LoadData();
            DatTrangThaiForm(false);
        }

        private void btnTimKiem_Click(object sender, EventArgs e) { }

        private void btnTrangDau_Click(object sender, EventArgs e) { }
        private void btnTrangTruoc_Click(object sender, EventArgs e) { }
        private void btnTrangSau_Click(object sender, EventArgs e) { }
        private void btnTrangCuoi_Click(object sender, EventArgs e) { }

        // ══════════════════════════════════════════════
        //  HELPER
        // ══════════════════════════════════════════════
        private void ResetForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            dateTimePicker1.Value = DateTime.Today;
            comboBox1.SelectedIndex = 0;
            if (comboBox2.Items.Count > 0) comboBox2.SelectedIndex = 0;
        }

        private void DatTrangThaiForm(bool editing)
        {
            textBox2.ReadOnly = !editing;
            comboBox1.Enabled = editing;
            dateTimePicker1.Enabled = editing;
            comboBox2.Enabled = editing;

            button1.Visible = !editing;   // Thêm
            button2.Visible = !editing;   // Sửa
            button3.Visible = !editing;   // Xóa
            button4.Visible = !editing;   // Làm mới
            btnLuu.Visible = editing;     // Lưu
        }
    }
}