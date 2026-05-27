using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormApp
{
    public partial class UCQuanLyLopHoc : UserControl
    {
        private int _trangHienTai = 1;
        private const int _soDoiMoiTrang = 10;
        private bool _dangThem = false;

        public UCQuanLyLopHoc()
        {
            InitializeComponent();
            LoadData();
            DatTrangThaiForm(false);
        }

        // ══════════════════════════════════════════════
        //  HIỂN THỊ DANH SÁCH (LINQ + Phân trang tại DB)
        // ══════════════════════════════════════════════
        private void LoadData(string keyword = "")
        {
            using (var db = new AppDbContext())
            {
                var query = db.LopHocs.AsQueryable();

                // Lọc dữ liệu bằng LINQ nếu có từ khóa tìm kiếm
                if (!string.IsNullOrEmpty(keyword))
                {
                    keyword = keyword.ToLower();
                    query = query.Where(l => l.malop.ToLower().Contains(keyword)
                                          || l.tenlop.ToLower().Contains(keyword));
                }

                int tongBanGhi = query.Count();
                int tongTrang = (int)Math.Ceiling((double)tongBanGhi / _soDoiMoiTrang);
                if (tongTrang == 0) tongTrang = 1;

                // Đảm bảo trang hiện tại hợp lệ
                if (_trangHienTai > tongTrang) _trangHienTai = tongTrang;
                if (_trangHienTai < 1) _trangHienTai = 1;

                int skip = (_trangHienTai - 1) * _soDoiMoiTrang;

                // Lấy dữ liệu theo phân trang
                var dsHienThi = query.OrderBy(l => l.id)
                                     .Skip(skip)
                                     .Take(_soDoiMoiTrang)
                                     .ToList();

                dataGridView1.Rows.Clear();
                foreach (var lop in dsHienThi)
                {
                    dataGridView1.Rows.Add(
                        lop.malop,
                        lop.tenlop,
                        lop.ghichu
                    );
                }

                label7.Text = $"Trang {_trangHienTai}/{tongTrang}  |  {tongBanGhi} bản ghi";
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dataGridView1.Rows[e.RowIndex];
            txtMaLop.Text = row.Cells["colMaLop"].Value?.ToString();
            txtTenLop.Text = row.Cells["colTenLop"].Value?.ToString();
            txtGhiChu.Text = row.Cells["colGhiChu"].Value?.ToString();
            DatTrangThaiForm(false);
        }

        // ══════════════════════════════════════════════
        //  THÊM, SỬA, XÓA, LƯU DỮ LIỆU
        // ══════════════════════════════════════════════
        private void btnThem_Click(object sender, EventArgs e)
        {
            ResetForm();
            txtMaLop.ReadOnly = false; // Khi thêm mới thì cho phép nhập Mã Lớp
            _dangThem = true;
            DatTrangThaiForm(true);
            txtMaLop.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLop.Text))
            {
                MessageBox.Show("Vui lòng chọn lớp cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            txtMaLop.ReadOnly = true; // Sửa thì không cho sửa Mã Lớp
            _dangThem = false;
            DatTrangThaiForm(true);
            txtTenLop.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLop.Text))
            {
                MessageBox.Show("Vui lòng chọn lớp cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenLop = txtTenLop.Text;
            if (MessageBox.Show($"Bạn có chắc muốn xóa lớp \"{tenLop}\"? Lưu ý: Xóa lớp sẽ xóa luôn các sinh viên thuộc lớp này!",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            string maLopCanXoa = txtMaLop.Text;
            using (var db = new AppDbContext())
            {
                var lop = db.LopHocs.FirstOrDefault(l => l.malop == maLopCanXoa);
                if (lop != null)
                {
                    // Tùy chọn: Xóa các sinh viên thuộc lớp trước (để tránh lỗi Khóa ngoại)
                    var sinhViens = db.SinhViens.Where(s => s.malop == maLopCanXoa).ToList();
                    db.SinhViens.RemoveRange(sinhViens);

                    db.LopHocs.Remove(lop);
                    db.SaveChanges(); // Áp dụng xuống CSDL

                    MessageBox.Show("Xóa lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetForm();
                    _trangHienTai = 1;
                    LoadData(txtTimKiem.Text.Trim());
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLop.Text) || string.IsNullOrWhiteSpace(txtTenLop.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ Mã Lớp và Tên Lớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (string.IsNullOrWhiteSpace(txtMaLop.Text)) txtMaLop.Focus();
                else txtTenLop.Focus();
                return;
            }

            string maLopMoi = txtMaLop.Text.Trim();
            string tenLopMoi = txtTenLop.Text.Trim();

            using (var db = new AppDbContext())
            {
                if (_dangThem)
                {
                    // Kiểm tra trùng Mã Lớp
                    if (db.LopHocs.Any(l => l.malop == maLopMoi))
                    {
                        MessageBox.Show("Mã lớp này đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaLop.Focus();
                        return;
                    }

                    // Kiểm tra trùng Tên Lớp
                    if (db.LopHocs.Any(l => l.tenlop == tenLopMoi))
                    {
                        MessageBox.Show("Tên lớp này đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTenLop.Focus();
                        return;
                    }

                    var lopMoi = new LopHoc
                    {
                        malop = maLopMoi,
                        tenlop = tenLopMoi,
                        ghichu = txtGhiChu.Text.Trim()
                    };
                    db.LopHocs.Add(lopMoi);
                    MessageBox.Show("Thêm lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var lop = db.LopHocs.FirstOrDefault(l => l.malop == maLopMoi);
                    if (lop != null)
                    {
                        // Kiểm tra trùng Tên Lớp (trừ lớp hiện tại)
                        if (db.LopHocs.Any(l => l.tenlop == tenLopMoi && l.malop != maLopMoi))
                        {
                            MessageBox.Show("Tên lớp này đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtTenLop.Focus();
                            return;
                        }

                        lop.tenlop = tenLopMoi;
                        lop.ghichu = txtGhiChu.Text.Trim();
                        MessageBox.Show("Cập nhật lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                db.SaveChanges(); // Áp dụng thay đổi xuống Database
            }

            DatTrangThaiForm(false);
            LoadData(txtTimKiem.Text.Trim());
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
            LoadData(txtTimKiem.Text.Trim());
        }

        private void btnTrangDau_Click(object sender, EventArgs e) { _trangHienTai = 1; LoadData(txtTimKiem.Text.Trim()); }
        private void btnTrangTruoc_Click(object sender, EventArgs e) { if (_trangHienTai > 1) { _trangHienTai--; LoadData(txtTimKiem.Text.Trim()); } }
        private void btnTrangSau_Click(object sender, EventArgs e) { _trangHienTai++; LoadData(txtTimKiem.Text.Trim()); }
        private void btnTrangCuoi_Click(object sender, EventArgs e) { _trangHienTai = int.MaxValue; LoadData(txtTimKiem.Text.Trim()); }

        // ══════════════════════════════════════════════
        //  HELPER
        // ══════════════════════════════════════════════
        private void ResetForm()
        {
            txtMaLop.Clear();
            txtTenLop.Clear();
            txtGhiChu.Clear();
            txtTimKiem.Clear();
        }

        private void DatTrangThaiForm(bool editing)
        {
            txtTenLop.ReadOnly = !editing;
            txtGhiChu.ReadOnly = !editing;

            if (!editing) txtMaLop.ReadOnly = true;

            btnThem.Visible = !editing;
            btnSua.Visible = !editing;
            btnXoa.Visible = !editing;
            btnLamMoi.Visible = !editing;
            btnLuu.Visible = editing;
        }
    }
}