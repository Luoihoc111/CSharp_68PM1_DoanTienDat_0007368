using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormApp
{
    public partial class UCQuanLySinhVien : UserControl
    {
        private bool _dangThem = false;

        // ── Phân trang ────────────────────────────────
        private List<SinhVien> _dsHienThi = new List<SinhVien>();
        private int _trang    = 1;
        private int _pageSize = 10;
        private int SoTrangToi => (int)Math.Ceiling((double)_dsHienThi.Count / _pageSize);

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
                _dsHienThi = db.SinhViens.OrderBy(s => s.id).ToList();
            }
            _trang = 1;
            HienThiTrang();
        }

        // ══════════════════════════════════════════════
        //  HIỂN THỊ TRANG HIỆN TẠI
        // ══════════════════════════════════════════════
        private void HienThiTrang()
        {
            dataGridView1.Rows.Clear();
            var trangData = _dsHienThi
                .Skip((_trang - 1) * _pageSize)
                .Take(_pageSize);

            foreach (var sv in trangData)
            {
                dataGridView1.Rows.Add(
                    sv.id.ToString(),
                    sv.hoten,
                    sv.gioitinh,
                    sv.ngaysinh?.ToString("dd/MM/yyyy") ?? "",
                    sv.malop
                );
            }

            int soTrang = SoTrangToi < 1 ? 1 : SoTrangToi;
            label7.Text = $"Trang {_trang}/{soTrang}  |  {_dsHienThi.Count} bản ghi";
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _dangThem = false;
            DatTrangThaiForm(true);
            textBox1.ReadOnly = true;
            textBox2.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var ketQua = MessageBox.Show(
                $"Bạn có chắc muốn xóa sinh viên có mã \"{textBox1.Text}\" không?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ketQua == DialogResult.Yes)
            {
                using (var db = new AppDbContext())
                {
                    var sv = db.SinhViens.Find(int.Parse(textBox1.Text));
                    if (sv != null)
                    {
                        db.SinhViens.Remove(sv);
                        db.SaveChanges();
                    }
                }
                ResetForm();
                LoadData();
                DatTrangThaiForm(false);
                MessageBox.Show("Xóa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

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
                else // Sửa
                {
                    var sv = db.SinhViens.Find(int.Parse(textBox1.Text));
                    if (sv != null)
                    {
                        sv.hoten    = textBox2.Text.Trim();
                        sv.gioitinh = comboBox1.Text;
                        sv.ngaysinh = dateTimePicker1.Value;
                        sv.malop    = comboBox2.Text;
                        db.SaveChanges();
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = textBox3.Text.Trim().ToLower();
            using (var db = new AppDbContext())
            {
                _dsHienThi = db.SinhViens
                    .OrderBy(s => s.id)
                    .ToList()
                    .Where(s =>
                        s.id.ToString().Contains(keyword) ||
                        (s.hoten  ?? "").ToLower().Contains(keyword) ||
                        (s.malop  ?? "").ToLower().Contains(keyword))
                    .ToList();
            }
            _trang = 1;
            HienThiTrang();
        }

        private void btnTrangDau_Click(object sender, EventArgs e)
        {
            if (_trang == 1) return;
            _trang = 1;
            HienThiTrang();
        }

        private void btnTrangTruoc_Click(object sender, EventArgs e)
        {
            if (_trang <= 1) return;
            _trang--;
            HienThiTrang();
        }

        private void btnTrangSau_Click(object sender, EventArgs e)
        {
            if (_trang >= SoTrangToi) return;
            _trang++;
            HienThiTrang();
        }

        private void btnTrangCuoi_Click(object sender, EventArgs e)
        {
            if (_trang == SoTrangToi) return;
            _trang = SoTrangToi;
            HienThiTrang();
        }

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