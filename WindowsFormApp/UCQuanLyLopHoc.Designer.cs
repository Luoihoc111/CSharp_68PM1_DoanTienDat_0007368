namespace WindowsFormApp
{
    partial class UCQuanLyLopHoc
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();

            groupBox1    = new System.Windows.Forms.GroupBox();
            lblMaLop     = new System.Windows.Forms.Label();
            txtMaLop     = new System.Windows.Forms.TextBox();
            lblTenLop    = new System.Windows.Forms.Label();
            txtTenLop    = new System.Windows.Forms.TextBox();
            lblGhiChu    = new System.Windows.Forms.Label();
            txtGhiChu    = new System.Windows.Forms.TextBox();
            btnThem      = new System.Windows.Forms.Button();
            btnSua       = new System.Windows.Forms.Button();
            btnXoa       = new System.Windows.Forms.Button();
            btnLamMoi    = new System.Windows.Forms.Button();
            btnLuu       = new System.Windows.Forms.Button();
            lblTimKiem   = new System.Windows.Forms.Label();
            txtTimKiem   = new System.Windows.Forms.TextBox();
            btnTimKiem   = new System.Windows.Forms.Button();
            dataGridView1= new System.Windows.Forms.DataGridView();
            colMaLop     = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colTenLop    = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colGhiChu    = new System.Windows.Forms.DataGridViewTextBoxColumn();
            btnTrangDau  = new System.Windows.Forms.Button();
            btnTrangTruoc= new System.Windows.Forms.Button();
            btnTrangSau  = new System.Windows.Forms.Button();
            btnTrangCuoi = new System.Windows.Forms.Button();
            label7       = new System.Windows.Forms.Label();

            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();

            // ── groupBox1 ─────────────────────────────────────────────────────
            groupBox1.Controls.AddRange(new System.Windows.Forms.Control[]
                { lblMaLop, txtMaLop, lblTenLop, txtTenLop, lblGhiChu, txtGhiChu });
            groupBox1.Font     = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            groupBox1.Location = new System.Drawing.Point(10, 10);
            groupBox1.Size     = new System.Drawing.Size(358, 580);
            groupBox1.Text     = "Thông tin lớp học";

            // Mã lớp
            lblMaLop.AutoSize = true; lblMaLop.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblMaLop.Location = new System.Drawing.Point(12, 30); lblMaLop.Text = "Mã lớp:";
            txtMaLop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtMaLop.Font        = new System.Drawing.Font("Segoe UI", 10F);
            txtMaLop.Location    = new System.Drawing.Point(12, 52);
            txtMaLop.Size        = new System.Drawing.Size(330, 34);
            txtMaLop.ReadOnly    = true;
            txtMaLop.BackColor   = System.Drawing.Color.WhiteSmoke;

            // Tên lớp
            lblTenLop.AutoSize = true; lblTenLop.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblTenLop.Location = new System.Drawing.Point(12, 95); lblTenLop.Text = "Tên lớp:";
            txtTenLop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtTenLop.Font        = new System.Drawing.Font("Segoe UI", 10F);
            txtTenLop.Location    = new System.Drawing.Point(12, 117);
            txtTenLop.Size        = new System.Drawing.Size(330, 34);

            // Ghi chú
            lblGhiChu.AutoSize = true; lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblGhiChu.Location = new System.Drawing.Point(12, 160); lblGhiChu.Text = "Ghi chú:";
            txtGhiChu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtGhiChu.Font        = new System.Drawing.Font("Segoe UI", 10F);
            txtGhiChu.Location    = new System.Drawing.Point(12, 182);
            txtGhiChu.Size        = new System.Drawing.Size(330, 80);
            txtGhiChu.Multiline   = true;

            // ── Buttons CRUD ──────────────────────────────────────────────────
            StyleBtn(btnThem,   "Thêm",    System.Drawing.Color.FromArgb(59,  130, 246));
            btnThem.Location   = new System.Drawing.Point(10,  600); btnThem.Size   = new System.Drawing.Size(167, 50);
            btnThem.Click     += new System.EventHandler(btnThem_Click);

            StyleBtn(btnSua,    "Sửa",     System.Drawing.Color.FromArgb(22,  163,  74));
            btnSua.Location    = new System.Drawing.Point(185, 600); btnSua.Size    = new System.Drawing.Size(167, 50);
            btnSua.Click      += new System.EventHandler(btnSua_Click);

            StyleBtn(btnXoa,    "Xóa",     System.Drawing.Color.FromArgb(220,  38,  38));
            btnXoa.Location    = new System.Drawing.Point(10,  658); btnXoa.Size    = new System.Drawing.Size(167, 50);
            btnXoa.Click      += new System.EventHandler(btnXoa_Click);

            StyleBtn(btnLamMoi, "Làm mới", System.Drawing.Color.FromArgb(107, 114, 128));
            btnLamMoi.Location = new System.Drawing.Point(185, 658); btnLamMoi.Size = new System.Drawing.Size(167, 50);
            btnLamMoi.Click   += new System.EventHandler(btnLamMoi_Click);

            StyleBtn(btnLuu,    "Lưu",     System.Drawing.Color.FromArgb(0,   150, 136));
            btnLuu.Location    = new System.Drawing.Point(10,  600); btnLuu.Size    = new System.Drawing.Size(342, 50);
            btnLuu.Visible     = false;
            btnLuu.Click      += new System.EventHandler(btnLuu_Click);

            // ── Tìm kiếm ─────────────────────────────────────────────────────
            lblTimKiem.AutoSize = true; lblTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblTimKiem.Location = new System.Drawing.Point(383, 13); lblTimKiem.Text = "Tìm kiếm (Mã lớp / Tên lớp):";

            txtTimKiem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtTimKiem.Font        = new System.Drawing.Font("Segoe UI", 10F);
            txtTimKiem.Location    = new System.Drawing.Point(383, 35);
            txtTimKiem.Size        = new System.Drawing.Size(400, 34);

            StyleBtn(btnTimKiem, "Tìm", System.Drawing.Color.FromArgb(30, 58, 95));
            btnTimKiem.Location = new System.Drawing.Point(792, 33); btnTimKiem.Size = new System.Drawing.Size(100, 38);
            btnTimKiem.Click   += new System.EventHandler(btnTimKiem_Click);

            // ── DataGridView ──────────────────────────────────────────────────
            cellStyle.BackColor = System.Drawing.Color.FromArgb(243, 244, 246);
            cellStyle.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            cellStyle.ForeColor = System.Drawing.Color.Black;

            dataGridView1.AllowUserToAddRows    = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode   = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor       = System.Drawing.Color.White;
            dataGridView1.BorderStyle           = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridView1.ColumnHeadersDefaultCellStyle = cellStyle;
            dataGridView1.ColumnHeadersHeight   = 32;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.Font                  = new System.Drawing.Font("Segoe UI", 9F);
            dataGridView1.GridColor             = System.Drawing.Color.FromArgb(229, 231, 235);
            dataGridView1.Location              = new System.Drawing.Point(383, 80);
            dataGridView1.MultiSelect           = false;
            dataGridView1.ReadOnly              = true;
            dataGridView1.RowHeadersVisible     = false;
            dataGridView1.RowTemplate.Height    = 28;
            dataGridView1.SelectionMode         = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size                  = new System.Drawing.Size(517, 560);
            dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[]
                { colMaLop, colTenLop, colGhiChu });
            dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellClick);

            colMaLop.HeaderText  = "Mã Lớp";  colMaLop.Name  = "colMaLop";  colMaLop.ReadOnly  = true;
            colTenLop.HeaderText = "Tên Lớp"; colTenLop.Name = "colTenLop"; colTenLop.ReadOnly = true;
            colGhiChu.HeaderText = "Ghi Chú"; colGhiChu.Name = "colGhiChu"; colGhiChu.ReadOnly = true;

            // ── Phân trang ────────────────────────────────────────────────────
            StyleNavBtn(btnTrangDau,   "<<"); btnTrangDau.Location   = new System.Drawing.Point(383, 650);
            btnTrangDau.Click   += new System.EventHandler(btnTrangDau_Click);

            StyleNavBtn(btnTrangTruoc, "<");  btnTrangTruoc.Location = new System.Drawing.Point(429, 650);
            btnTrangTruoc.Click += new System.EventHandler(btnTrangTruoc_Click);

            StyleNavBtn(btnTrangSau,   ">");  btnTrangSau.Location   = new System.Drawing.Point(783, 650);
            btnTrangSau.Click   += new System.EventHandler(btnTrangSau_Click);

            StyleNavBtn(btnTrangCuoi,  ">>"); btnTrangCuoi.Location  = new System.Drawing.Point(829, 650);
            btnTrangCuoi.Click  += new System.EventHandler(btnTrangCuoi_Click);

            label7.Font      = new System.Drawing.Font("Segoe UI", 9F);
            label7.Location  = new System.Drawing.Point(471, 650);
            label7.Size      = new System.Drawing.Size(312, 35);
            label7.Text      = "Trang 1/1  |  0 bản ghi";
            label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ── UserControl ───────────────────────────────────────────────────
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            BackColor           = System.Drawing.Color.FromArgb(249, 250, 251);
            Size                = new System.Drawing.Size(917, 720);
            Controls.AddRange(new System.Windows.Forms.Control[]
            {
                label7, btnTrangCuoi, btnTrangSau, btnTrangTruoc, btnTrangDau,
                dataGridView1, btnTimKiem, txtTimKiem, lblTimKiem,
                btnLamMoi, btnXoa, btnSua, btnThem, btnLuu,
                groupBox1
            });
            Name = "UCQuanLyLopHoc";

            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void StyleBtn(System.Windows.Forms.Button btn, string text, System.Drawing.Color color)
        {
            btn.Text      = text;
            btn.BackColor = color;
            btn.ForeColor = System.Drawing.Color.White;
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font      = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            btn.Cursor    = System.Windows.Forms.Cursors.Hand;
        }
        private void StyleNavBtn(System.Windows.Forms.Button btn, string text)
        {
            btn.Text      = text;
            btn.Size      = new System.Drawing.Size(42, 35);
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btn.Cursor    = System.Windows.Forms.Cursors.Hand;
        }

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMaLop, lblTenLop, lblGhiChu;
        private System.Windows.Forms.TextBox txtMaLop, txtTenLop, txtGhiChu;
        private System.Windows.Forms.Button btnThem, btnSua, btnXoa, btnLamMoi, btnLuu;
        private System.Windows.Forms.Label lblTimKiem, label7;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaLop, colTenLop, colGhiChu;
        private System.Windows.Forms.Button btnTrangDau, btnTrangTruoc, btnTrangSau, btnTrangCuoi;
    }
}
