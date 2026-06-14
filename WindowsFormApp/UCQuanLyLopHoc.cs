using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormApp
{
    public partial class UCQuanLyLopHoc : UserControl
    {
        private bool _dangThem = false;

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
                var dsLop = db.LopHocs.OrderBy(l => l.id).ToList();

                dataGridView1.Rows.Clear();
                foreach (var lop in dsLop)
                {
                    dataGridView1.Rows.Add(lop.malop, lop.tenlop, lop.ghichu);
                }
            }
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