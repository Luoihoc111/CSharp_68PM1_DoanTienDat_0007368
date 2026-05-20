using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormApp
{
    public partial class UCQuanLyLopHoc : UserControl
    {
        private List<LopHoc> _dsLopHoc = new List<LopHoc>();
        private int _trangHienTai = 1;
        private const int _soDoiMoiTrang = 10;
        private List<LopHoc> _dsHienThi = new List<LopHoc>();
        private bool _dangThem = false;

        public UCQuanLyLopHoc()
        {
            InitializeComponent();
            TaoDuLieuMau();
            HienThiDanhSach(_dsLopHoc);
            DatTrangThaiForm(false);
        }

        // ══════════════════════════════════════════════
        //  DỮ LIỆU MẪU
        // ══════════════════════════════════════════════
        private void TaoDuLieuMau()
        {
            _dsLopHoc.Add(new LopHoc { MaLop = "1", TenLop = "68PM1", GhiChu = "Lớp 68PM1" });
            _dsLopHoc.Add(new LopHoc { MaLop = "2", TenLop = "68PM2", GhiChu = "Lớp 68PM2" });
            _dsLopHoc.Add(new LopHoc { MaLop = "3", TenLop = "68PM3", GhiChu = "Lớp 68PM3" });
        }

        // ══════════════════════════════════════════════
        //  HIỂN THỊ DANH SÁCH (có phân trang)
        // ══════════════════════════════════════════════
        private void HienThiDanhSach(List<LopHoc> ds)
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
                var lop = ds[i];
                dataGridView1.Rows.Add(lop.MaLop, lop.TenLop, lop.GhiChu);
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
            txtMaLop.Text  = row.Cells["colMaLop"].Value?.ToString();
            txtTenLop.Text = row.Cells["colTenLop"].Value?.ToString();
            txtGhiChu.Text = row.Cells["colGhiChu"].Value?.ToString();
            DatTrangThaiForm(false);
        }

        // ══════════════════════════════════════════════
        //  THÊM
        // ══════════════════════════════════════════════
        private void btnThem_Click(object sender, EventArgs e)
        {
            ResetForm();
            txtMaLop.Text = ((_dsLopHoc.Count > 0
                ? int.Parse(_dsLopHoc.Max(l => l.MaLop)) : 0) + 1).ToString();
            _dangThem = true;
            DatTrangThaiForm(true);
            txtTenLop.Focus();
        }

        // ══════════════════════════════════════════════
        //  SỬA
        // ══════════════════════════════════════════════
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLop.Text))
            {
                MessageBox.Show("Vui lòng chọn lớp cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _dangThem = false;
            DatTrangThaiForm(true);
            txtTenLop.Focus();
        }

        // ══════════════════════════════════════════════
        //  XÓA
        // ══════════════════════════════════════════════
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLop.Text))
            {
                MessageBox.Show("Vui lòng chọn lớp cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa lớp \"{txtTenLop.Text}\"?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            string maLop = txtMaLop.Text;
            var lop = _dsLopHoc.FirstOrDefault(l => l.MaLop == maLop);
            if (lop != null)
            {
                _dsLopHoc.Remove(lop);
                MessageBox.Show("Xóa lớp thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetForm();
                _trangHienTai = 1;
                HienThiDanhSach(_dsLopHoc);
            }
        }

        // ══════════════════════════════════════════════
        //  LƯU
        // ══════════════════════════════════════════════
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenLop.Text))
            {
                MessageBox.Show("Vui lòng nhập tên lớp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLop.Focus(); return;
            }

            if (_dangThem)
            {
                // Kiểm tra trùng tên lớp
                if (_dsLopHoc.Any(l => l.TenLop.Equals(txtTenLop.Text.Trim(),
                    StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("Tên lớp đã tồn tại!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                _dsLopHoc.Add(new LopHoc
                {
                    MaLop  = txtMaLop.Text,
                    TenLop = txtTenLop.Text.Trim(),
                    GhiChu = txtGhiChu.Text.Trim()
                });
                MessageBox.Show("Thêm lớp thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var lop = _dsLopHoc.FirstOrDefault(l => l.MaLop == txtMaLop.Text);
                if (lop != null)
                {
                    lop.TenLop = txtTenLop.Text.Trim();
                    lop.GhiChu = txtGhiChu.Text.Trim();
                }
                MessageBox.Show("Cập nhật lớp thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            ResetForm();
            _trangHienTai = 1;
            HienThiDanhSach(_dsLopHoc);
            DatTrangThaiForm(false);
        }

        // ══════════════════════════════════════════════
        //  LÀM MỚI
        // ══════════════════════════════════════════════
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
            _trangHienTai = 1;
            HienThiDanhSach(_dsLopHoc);
            DatTrangThaiForm(false);
        }

        // ══════════════════════════════════════════════
        //  TÌM KIẾM
        // ══════════════════════════════════════════════
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string kw = txtTimKiem.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(kw)) { _trangHienTai = 1; HienThiDanhSach(_dsLopHoc); return; }

            var ketQua = _dsLopHoc
                .Where(l => l.MaLop.Contains(kw) || l.TenLop.ToLower().Contains(kw))
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
            txtMaLop.Clear();
            txtTenLop.Clear();
            txtGhiChu.Clear();
            txtTimKiem.Clear();
        }

        private void DatTrangThaiForm(bool editing)
        {
            txtTenLop.ReadOnly = !editing;
            txtGhiChu.ReadOnly = !editing;

            btnThem.Visible   = !editing;
            btnSua.Visible    = !editing;
            btnXoa.Visible    = !editing;
            btnLamMoi.Visible = !editing;
            btnLuu.Visible    =  editing;
        }
    }

    // ── Model ────────────────────────────────────────────────────────────────
    public class LopHoc
    {
        public string MaLop  { get; set; }
        public string TenLop { get; set; }
        public string GhiChu { get; set; }
    }
}
