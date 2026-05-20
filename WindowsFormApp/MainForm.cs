using System;
using System.Windows.Forms;

namespace WindowsFormApp
{
    /// <summary>
    /// Form chính sau khi đăng nhập thành công.
    /// Chứa MenuStrip ở trên, load UserControl vào panel bên dưới.
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Mặc định mở Quản lý Sinh Viên
            LoadUserControl(new UCQuanLySinhVien());
        }

        // ── Menu: Quản lý Sinh Viên ───────────────────────────────────────────
        private void menuQLSinhVien_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UCQuanLySinhVien());
        }

        // ── Menu: Quản lý Lớp Học ────────────────────────────────────────────
        private void menuQLLopHoc_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UCQuanLyLopHoc());
        }

        // ── Menu: Đăng xuất ──────────────────────────────────────────────────
        private void menuDangXuat_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Close();
            }
        }

        // ── Helper: load UC vào panel ─────────────────────────────────────────
        private void LoadUserControl(UserControl uc)
        {
            panelContent.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelContent.Controls.Add(uc);
        }
    }
}
