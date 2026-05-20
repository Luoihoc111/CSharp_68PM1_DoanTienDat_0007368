using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormApp
{
    public partial class UCQuanLySinhVien : UserControl
    {
        private List<SinhVien> _dsSinhVien = new List<SinhVien>();
        private List<string> _dsLopHoc = new List<string>();
        private int _trangHienTai = 1;
        private const int _soDoiMoiTrang = 10;
        private List<SinhVien> _dsHienThi = new List<SinhVien>();
        private bool _dangThem = false;   // true = thêm mới, false = sửa

        public UCQuanLySinhVien()
        {
            InitializeComponent();
            TaoDuLieuMau();
            LoadLopVaoComboBox();
            HienThiDanhSach(_dsSinhVien);
            DatTrangThaiForm(false);
        }

        // ══════════════════════════════════════════════
        //  DỮ LIỆU MẪU
        // ══════════════════════════════════════════════
        private void TaoDuLieuMau()
        {
            _dsLopHoc.AddRange(new[] { "68PM1", "68PM2", "68PM3" });

            _dsSinhVien.Add(new SinhVien { MaSV = "1", HoTen = "Doan Tien Dat",  GioiTinh = "Nam", NgaySinh = new DateTime(2026, 2, 17), Lop = "68PM1" });
            _dsSinhVien.Add(new SinhVien { MaSV = "2", HoTen = "Nguyen Van A",   GioiTinh = "Nam", NgaySinh = new DateTime(2026, 3, 11), Lop = "68PM2" });
            _dsSinhVien.Add(new SinhVien { MaSV = "3", HoTen = "Tran Thi B",     GioiTinh = "Nữ",  NgaySinh = new DateTime(2005, 5, 20), Lop = "68PM1" });
        }

        private void LoadLopVaoComboBox()
        {
            comboBox2.Items.Clear();
            foreach (var lop in _dsLopHoc)
                comboBox2.Items.Add(lop);
            if (comboBox2.Items.Count > 0)
                comboBox2.SelectedIndex = 0;
        }

        // ══════════════════════════════════════════════
        //  HIỂN THỊ DANH SÁCH (có phân trang)
        // ══════════════════════════════════════════════
        private void HienThiDanhSach(List<SinhVien> ds)
        {
            _dsHienThi = ds;
            int tongBanGhi = ds.Count;
            int tongTrang = (int)Math.Ceiling((double)tongBanGhi / _soDoiMoiTrang);
            if (tongTrang == 0) tongTrang = 1;
            if (_trangHienTai > tongTrang) _trangHienTai = tongTrang;
            if (_trangHienTai < 1) _trangHienTai = 1;

            int batDau = (_trangHienTai - 1) * _soDoiMoiTrang;
            int ketThuc = Math.Min(batDau + _soDoiMoiTrang, tongBanGhi);

            dataGridView1.Rows.Clear();
            for (int i = batDau; i < ketThuc; i++)
            {
                var sv = ds[i];
                dataGridView1.Rows.Add(sv.MaSV, sv.HoTen, sv.GioiTinh,
                    sv.NgaySinh.ToString("dd/MM/yyyy"), sv.Lop);
            }

            label7.Text = $"Trang {_trangHienTai}/{tongTrang}  |  {tongBanGhi} bản ghi";
        }

        // ══════════════════════════════════════════════
        //  CHỌN HÀNG TRÊN GRID → ĐIỀN VÀO FORM
        // ══════════════════════════════════════════════
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dataGridView1.Rows[e.RowIndex];
            textBox1.Text = row.Cells["colMaSV"].Value?.ToString();
            textBox2.Text = row.Cells["colHoTen"].Value?.ToString();
            comboBox1.Text = row.Cells["colGioiTinh"].Value?.ToString();

            if (DateTime.TryParseExact(row.Cells["colNgaySinh"].Value?.ToString(),
                "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out DateTime ngay))
                dateTimePicker1.Value = ngay;

            string lop = row.Cells["colLop"].Value?.ToString();
            int idx = comboBox2.Items.IndexOf(lop);
            if (idx >= 0) comboBox2.SelectedIndex = idx;

            DatTrangThaiForm(false);
        }

        // ══════════════════════════════════════════════
        //  THÊM
        // ══════════════════════════════════════════════
        private void btnThem_Click(object sender, EventArgs e)
        {
            ResetForm();
            textBox1.Text = ((_dsSinhVien.Count > 0
                ? int.Parse(_dsSinhVien.Max(s => s.MaSV)) : 0) + 1).ToString();
            _dangThem = true;
            DatTrangThaiForm(true);
            textBox2.Focus();
        }

        // ══════════════════════════════════════════════
        //  SỬA
        // ══════════════════════════════════════════════
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _dangThem = false;
            DatTrangThaiForm(true);
            textBox2.Focus();
        }

        // ══════════════════════════════════════════════
        //  XÓA
        // ══════════════════════════════════════════════
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ten = textBox2.Text;
            if (MessageBox.Show($"Bạn có chắc muốn xóa sinh viên \"{ten}\"?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            string maSV = textBox1.Text;
            var sv = _dsSinhVien.FirstOrDefault(s => s.MaSV == maSV);
            if (sv != null)
            {
                _dsSinhVien.Remove(sv);
                MessageBox.Show("Xóa sinh viên thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
                _trangHienTai = 1;
                HienThiDanhSach(_dsSinhVien);
            }
        }

        // ══════════════════════════════════════════════
        //  LƯU (nút Lưu chỉ hiện khi đang thêm/sửa)
        // ══════════════════════════════════════════════
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus(); return;
            }

            if (_dangThem)
            {
                _dsSinhVien.Add(new SinhVien
                {
                    MaSV     = textBox1.Text,
                    HoTen    = textBox2.Text.Trim(),
                    GioiTinh = comboBox1.Text,
                    NgaySinh = dateTimePicker1.Value,
                    Lop      = comboBox2.Text
                });
                MessageBox.Show("Thêm sinh viên thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var sv = _dsSinhVien.FirstOrDefault(s => s.MaSV == textBox1.Text);
                if (sv != null)
                {
                    sv.HoTen    = textBox2.Text.Trim();
                    sv.GioiTinh = comboBox1.Text;
                    sv.NgaySinh = dateTimePicker1.Value;
                    sv.Lop      = comboBox2.Text;
                }
                MessageBox.Show("Cập nhật sinh viên thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            ResetForm();
            _trangHienTai = 1;
            HienThiDanhSach(_dsSinhVien);
            DatTrangThaiForm(false);
        }

        // ══════════════════════════════════════════════
        //  LÀM MỚI
        // ══════════════════════════════════════════════
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
            _trangHienTai = 1;
            HienThiDanhSach(_dsSinhVien);
            DatTrangThaiForm(false);
        }

        // ══════════════════════════════════════════════
        //  TÌM KIẾM
        // ══════════════════════════════════════════════
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string kw = textBox3.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(kw))
            {
                _trangHienTai = 1;
                HienThiDanhSach(_dsSinhVien);
                return;
            }
            var ketQua = _dsSinhVien
                .Where(s => s.HoTen.ToLower().Contains(kw)
                         || s.MaSV.Contains(kw)
                         || s.Lop.ToLower().Contains(kw))
                .ToList();
            _trangHienTai = 1;
            HienThiDanhSach(ketQua);
        }

        // ══════════════════════════════════════════════
        //  PHÂN TRANG
        // ══════════════════════════════════════════════
        private void btnTrangDau_Click(object sender, EventArgs e)
        { _trangHienTai = 1; HienThiDanhSach(_dsHienThi); }

        private void btnTrangTruoc_Click(object sender, EventArgs e)
        { if (_trangHienTai > 1) { _trangHienTai--; HienThiDanhSach(_dsHienThi); } }

        private void btnTrangSau_Click(object sender, EventArgs e)
        {
            int tongTrang = (int)Math.Ceiling((double)_dsHienThi.Count / _soDoiMoiTrang);
            if (_trangHienTai < tongTrang) { _trangHienTai++; HienThiDanhSach(_dsHienThi); }
        }

        private void btnTrangCuoi_Click(object sender, EventArgs e)
        {
            _trangHienTai = Math.Max(1, (int)Math.Ceiling((double)_dsHienThi.Count / _soDoiMoiTrang));
            HienThiDanhSach(_dsHienThi);
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
            textBox3.Clear();
        }

        /// <summary>
        /// editing=true  → đang nhập liệu (ẩn Thêm/Sửa/Xóa/LàmMới, hiện Lưu)
        /// editing=false → chế độ xem (hiện Thêm/Sửa/Xóa/LàmMới, ẩn Lưu)
        /// </summary>
        private void DatTrangThaiForm(bool editing)
        {
            textBox2.ReadOnly       = !editing;
            comboBox1.Enabled       = editing;
            dateTimePicker1.Enabled = editing;
            comboBox2.Enabled       = editing;

            button1.Visible  = !editing;   // Thêm
            button2.Visible  = !editing;   // Sửa
            button3.Visible  = !editing;   // Xóa
            button4.Visible  = !editing;   // Làm mới
            btnLuu.Visible   =  editing;   // Lưu
        }
    }
}
