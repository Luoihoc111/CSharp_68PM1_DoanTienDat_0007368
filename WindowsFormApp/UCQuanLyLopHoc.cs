using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormApp
{
    public partial class UCQuanLyLopHoc : UserControl
    {
        private bool _dangThem = false;

        // ── Phân trang ────────────────────────────────
        private List<LopHoc> _dsHienThi = new List<LopHoc>();
        private int _trang    = 1;
        private int _pageSize = 10;
        private int SoTrangToi => (int)Math.Ceiling((double)_dsHienThi.Count / _pageSize);

        public UCQuanLyLopHoc()
        {
            InitializeComponent();
            LoadData();
            DatTrangThaiForm(false);
        }

        // ══════════════════════════════════════════════
        //  HIỂN THỊ DANH SÁCH (LINQ)
        // ══════════════════════════════════════════════
        private void LoadData()
        {
            using (var db = new AppDbContext())
            {
                _dsHienThi = db.LopHocs.OrderBy(l => l.id).ToList();
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

            foreach (var lop in trangData)
            {
                dataGridView1.Rows.Add(lop.malop, lop.tenlop, lop.ghichu);
            }

            int soTrang = SoTrangToi < 1 ? 1 : SoTrangToi;
            label7.Text = $"Trang {_trang}/{soTrang}  |  {_dsHienThi.Count} bản ghi";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridView1.Rows[e.RowIndex];
            txtMaLop.Text  = row.Cells[0].Value?.ToString();
            txtTenLop.Text = row.Cells[1].Value?.ToString();
            txtGhiChu.Text = row.Cells[2].Value?.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ResetForm();
            txtMaLop.ReadOnly = false;
            txtMaLop.Focus();
            _dangThem = true;
            DatTrangThaiForm(true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLop.Text))
            {
                MessageBox.Show("Vui lòng chọn lớp học cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _dangThem = false;
            DatTrangThaiForm(true);
            txtMaLop.ReadOnly = true;
            txtTenLop.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLop.Text))
            {
                MessageBox.Show("Vui lòng chọn lớp học cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var ketQua = MessageBox.Show(
                $"Bạn có chắc muốn xóa lớp \"{txtMaLop.Text}\" không?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (ketQua == DialogResult.Yes)
            {
                using (var db = new AppDbContext())
                {
                    var lop = db.LopHocs.FirstOrDefault(l => l.malop == txtMaLop.Text);
                    if (lop != null)
                    {
                        db.LopHocs.Remove(lop);
                        db.SaveChanges();
                    }
                }
                ResetForm();
                LoadData();
                DatTrangThaiForm(false);
                MessageBox.Show("Xóa lớp học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLop.Text) || string.IsNullOrWhiteSpace(txtTenLop.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã lớp và Tên lớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var db = new AppDbContext())
            {
                if (_dangThem)
                {
                    if (db.LopHocs.Any(l => l.malop == txtMaLop.Text))
                    {
                        MessageBox.Show("Mã lớp đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    db.LopHocs.Add(new LopHoc
                    {
                        malop  = txtMaLop.Text.Trim(),
                        tenlop = txtTenLop.Text.Trim(),
                        ghichu = txtGhiChu.Text.Trim()
                    });
                    db.SaveChanges();
                    MessageBox.Show("Thêm lớp học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else // Sửa
                {
                    var lop = db.LopHocs.FirstOrDefault(l => l.malop == txtMaLop.Text);
                    if (lop != null)
                    {
                        lop.tenlop = txtTenLop.Text.Trim();
                        lop.ghichu = txtGhiChu.Text.Trim();
                        db.SaveChanges();
                        MessageBox.Show("Cập nhật lớp học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            string keyword = txtTimKiem.Text.Trim().ToLower();
            using (var db = new AppDbContext())
            {
                _dsHienThi = db.LopHocs
                    .OrderBy(l => l.id)
                    .ToList()
                    .Where(l =>
                        (l.malop  ?? "").ToLower().Contains(keyword) ||
                        (l.tenlop ?? "").ToLower().Contains(keyword))
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

            btnThem.Visible    = !editing;
            btnSua.Visible     = !editing;
            btnXoa.Visible     = !editing;
            btnLamMoi.Visible  = !editing;
            btnLuu.Visible     = editing;
        }
    }
}