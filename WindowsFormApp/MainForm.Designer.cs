namespace WindowsFormApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            menuStrip1        = new System.Windows.Forms.MenuStrip();
            menuQLSinhVien    = new System.Windows.Forms.ToolStripMenuItem();
            menuQLLopHoc      = new System.Windows.Forms.ToolStripMenuItem();
            menuDangXuat      = new System.Windows.Forms.ToolStripMenuItem();
            panelContent      = new System.Windows.Forms.Panel();

            menuStrip1.SuspendLayout();
            SuspendLayout();

            // ── MenuStrip ─────────────────────────────────────────────────────
            menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
                { menuQLSinhVien, menuQLLopHoc, menuDangXuat });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name     = "menuStrip1";
            menuStrip1.Padding  = new System.Windows.Forms.Padding(5, 2, 0, 2);
            menuStrip1.Size     = new System.Drawing.Size(917, 33);
            menuStrip1.Text     = "Menu";

            menuQLSinhVien.Name  = "menuQLSinhVien";
            menuQLSinhVien.Text  = "Quản lý Sinh Viên";
            menuQLSinhVien.Size  = new System.Drawing.Size(170, 29);
            menuQLSinhVien.Click += new System.EventHandler(menuQLSinhVien_Click);

            menuQLLopHoc.Name  = "menuQLLopHoc";
            menuQLLopHoc.Text  = "Quản lý Lớp Học";
            menuQLLopHoc.Size  = new System.Drawing.Size(160, 29);
            menuQLLopHoc.Click += new System.EventHandler(menuQLLopHoc_Click);

            menuDangXuat.Name      = "menuDangXuat";
            menuDangXuat.Text      = "Đăng xuất";
            menuDangXuat.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            menuDangXuat.Size      = new System.Drawing.Size(110, 29);
            menuDangXuat.Click    += new System.EventHandler(menuDangXuat_Click);

            // ── panelContent ──────────────────────────────────────────────────
            panelContent.Dock     = System.Windows.Forms.DockStyle.Fill;
            panelContent.Location = new System.Drawing.Point(0, 33);
            panelContent.Name     = "panelContent";

            // ── MainForm ──────────────────────────────────────────────────────
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            BackColor           = System.Drawing.Color.FromArgb(249, 250, 251);
            ClientSize          = new System.Drawing.Size(960, 780);
            Controls.Add(panelContent);
            Controls.Add(menuStrip1);
            MainMenuStrip   = menuStrip1;
            Name            = "MainForm";
            Text            = "Quản Lý Sinh Viên";
            StartPosition   = System.Windows.Forms.FormStartPosition.CenterScreen;
            Load           += new System.EventHandler(MainForm_Load);

            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuQLSinhVien;
        private System.Windows.Forms.ToolStripMenuItem menuQLLopHoc;
        private System.Windows.Forms.ToolStripMenuItem menuDangXuat;
        private System.Windows.Forms.Panel panelContent;
    }
}
