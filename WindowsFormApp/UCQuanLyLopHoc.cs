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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) { }

        private void btnThem_Click(object sender, EventArgs e) { }

        private void btnSua_Click(object sender, EventArgs e) { }

        private void btnXoa_Click(object sender, EventArgs e) { }

        private void btnLuu_Click(object sender, EventArgs e) { }

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