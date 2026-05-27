using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormApp
{
    public partial class UCQuanLySinhVien : UserControl
    {
        private int _trangHienTai = 1;
        private const int _soDoiMoiTrang = 10;
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
                {
                    comboBox2.Items.Add(lop);
                }
            }
            if (comboBox2.Items.Count > 0)
                comboBox2.SelectedIndex = 0;
        }

        // ══════════════════════════════════════════════
        //  HIỂN THỊ DANH SÁCH (LINQ + Phân trang tại DB)
        // ══════════════════════════════════════════════
        private void LoadData(string keyword = "")
        {
            using (var db = new AppDbContext())
            {
                var query = db.SinhViens.AsQueryable();

                // Lọc dữ liệu bằng LINQ nếu có từ khóa tìm kiếm
                if (!string.IsNullOrEmpty(keyword))
                {
                    keyword = keyword.ToLower();
                    query = query.Where(s => s.hoten.ToLower().Contains(keyword)
                                          || s.id.ToString().Contains(keyword)
                                          || s.malop.ToLower().Contains(keyword));
                }

                int tongBanGhi = query.Count();
                int tongTrang = (int)Math.Ceiling((double)tongBanGhi / _soDoiMoiTrang);
                if (tongTrang == 0) tongTrang = 1;

                // Đảm bảo trang hiện tại hợp lệ
                if (_trangHienTai > tongTrang) _trangHienTai = tongTrang;
                if (_trangHienTai < 1) _trangHienTai = 1;

                int skip = (_trangHienTai - 1) * _soDoiMoiTrang;

                // Lấy dữ liệu theo phân trang
                var dsHienThi = query.OrderBy(s => s.id)
                                     .Skip(skip)
                                     .Take(_soDoiMoiTrang)
                                     .ToList();

                dataGridView1.Rows.Clear();
                foreach (var sv in dsHienThi)
                {
                    dataGridView1.Rows.Add(
                        sv.id.ToString(),
                        sv.hoten,
                        sv.gioitinh,
                        sv.ngaysinh?.ToString("dd/MM/yyyy") ?? "",
                        sv.malop
                    );
                }

                label7.Text = $"Trang {_trangHienTai}/{tongTrang}  |  {tongBanGhi} bản ghi";
            }
        }

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
            {
                dateTimePicker1.Value = ngay;
            }

            string lop = row.Cells["colLop"].Value?.ToString();
            int idx = comboBox2.Items.IndexOf(lop);
            if (idx >= 0) comboBox2.SelectedIndex = idx;

            DatTrangThaiForm(false);
        }

        // ══════════════════════════════════════════════
        //  THÊM, SỬA, XÓA, LƯU DỮ LIỆU
        // ══════════════════════════════════════════════
        private void btnThem_Click(object sender, EventArgs e)
        {
            ResetForm();
            using (var db = new AppDbContext())
            {
                // Vì bảng SQL tbl_sinhviens không tự tăng ID, ta dùng LINQ tìm ID lớn nhất cộng 1
                int maxId = db.SinhViens.Any() ? db.SinhViens.Max(s => s.id) : 0;
                textBox1.Text = (maxId + 1).ToString();
            }
            _dangThem = true;
            DatTrangThaiForm(true);
            textBox2.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _dangThem = false;
            DatTrangThaiForm(true);
            textBox2.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || !int.TryParse(textBox1.Text, out int idCanXoa))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ten = textBox2.Text;
            if (MessageBox.Show($"Bạn có chắc muốn xóa sinh viên \"{ten}\"?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            using (var db = new AppDbContext())
            {
                var sv = db.SinhViens.Find(idCanXoa); // Dùng LINQ Find
                if (sv != null)
                {
                    db.SinhViens.Remove(sv);
                    db.SaveChanges(); // Áp dụng xuống CSDL
                    MessageBox.Show("Xóa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetForm();
                    _trangHienTai = 1;
                    LoadData(textBox3.Text.Trim());
                }
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
                        id = int.Parse(textBox1.Text),
                        hoten = textBox2.Text.Trim(),
                        gioitinh = comboBox1.Text,
                        ngaysinh = dateTimePicker1.Value,
                        malop = comboBox2.Text
                    };
                    db.SinhViens.Add(svMoi);
                    MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    int idSua = int.Parse(textBox1.Text);
                    var sv = db.SinhViens.Find(idSua);
                    if (sv != null)
                    {
                        sv.hoten = textBox2.Text.Trim();
                        sv.gioitinh = comboBox1.Text;
                        sv.ngaysinh = dateTimePicker1.Value;
                        sv.malop = comboBox2.Text;
                        MessageBox.Show("Cập nhật sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                db.SaveChanges(); // Áp dụng thay đổi xuống SQL Server
            }

            DatTrangThaiForm(false);
            LoadData(textBox3.Text.Trim());
        }

        // ══════════════════════════════════════════════
        //  CÁC NÚT CHỨC NĂNG KHÁC
        // ══════════════════════════════════════════════
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
            _trangHienTai = 1;
            LoadData();
            DatTrangThaiForm(false);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            _trangHienTai = 1;
            LoadData(textBox3.Text.Trim());
        }

        private void btnTrangDau_Click(object sender, EventArgs e) { _trangHienTai = 1; LoadData(textBox3.Text.Trim()); }
        private void btnTrangTruoc_Click(object sender, EventArgs e) { if (_trangHienTai > 1) { _trangHienTai--; LoadData(textBox3.Text.Trim()); } }
        private void btnTrangSau_Click(object sender, EventArgs e) { _trangHienTai++; LoadData(textBox3.Text.Trim()); }
        private void btnTrangCuoi_Click(object sender, EventArgs e) { _trangHienTai = int.MaxValue; LoadData(textBox3.Text.Trim()); }

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