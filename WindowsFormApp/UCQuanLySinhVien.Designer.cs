namespace WindowsFormApp
{
    partial class UCQuanLySinhVien
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();

            groupBox1      = new System.Windows.Forms.GroupBox();
            comboBox2      = new System.Windows.Forms.ComboBox();
            comboBox1      = new System.Windows.Forms.ComboBox();
            dateTimePicker1= new System.Windows.Forms.DateTimePicker();
            textBox2       = new System.Windows.Forms.TextBox();
            textBox1       = new System.Windows.Forms.TextBox();
            label5         = new System.Windows.Forms.Label();
            label4         = new System.Windows.Forms.Label();
            label3         = new System.Windows.Forms.Label();
            label2         = new System.Windows.Forms.Label();
            label1         = new System.Windows.Forms.Label();
            button1        = new System.Windows.Forms.Button();
            button2        = new System.Windows.Forms.Button();
            button3        = new System.Windows.Forms.Button();
            button4        = new System.Windows.Forms.Button();
            btnLuu         = new System.Windows.Forms.Button();
            label6         = new System.Windows.Forms.Label();
            textBox3       = new System.Windows.Forms.TextBox();
            button5        = new System.Windows.Forms.Button();
            dataGridView1  = new System.Windows.Forms.DataGridView();
            colMaSV        = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colHoTen       = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colGioiTinh    = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colNgaySinh    = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colLop         = new System.Windows.Forms.DataGridViewTextBoxColumn();
            button6        = new System.Windows.Forms.Button();
            button7        = new System.Windows.Forms.Button();
            button8        = new System.Windows.Forms.Button();
            button9        = new System.Windows.Forms.Button();
            label7         = new System.Windows.Forms.Label();

            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();

            // ── groupBox1 ────────────────────────────────────────────────────
            groupBox1.Controls.AddRange(new System.Windows.Forms.Control[]
            { comboBox2, comboBox1, dateTimePicker1, textBox2, textBox1,
              label5, label4, label3, label2, label1 });
            groupBox1.Font     = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            groupBox1.Location = new System.Drawing.Point(10, 10);
            groupBox1.Size     = new System.Drawing.Size(358, 580);
            groupBox1.Text     = "Thông tin sinh viên";

            // label1 – Mã SV
            label1.AutoSize = true; label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            label1.Location = new System.Drawing.Point(12, 30); label1.Text = "Mã sinh viên:";
            // textBox1
            textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox1.Font        = new System.Drawing.Font("Segoe UI", 10F);
            textBox1.Location    = new System.Drawing.Point(12, 52);
            textBox1.Size        = new System.Drawing.Size(330, 34);
            textBox1.ReadOnly    = true;
            textBox1.BackColor   = System.Drawing.Color.WhiteSmoke;

            // label2 – Họ và tên
            label2.AutoSize = true; label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            label2.Location = new System.Drawing.Point(12, 95); label2.Text = "Họ và tên:";
            // textBox2
            textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox2.Font        = new System.Drawing.Font("Segoe UI", 10F);
            textBox2.Location    = new System.Drawing.Point(12, 117);
            textBox2.Size        = new System.Drawing.Size(330, 34);

            // label3 – Ngày sinh
            label3.AutoSize = true; label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            label3.Location = new System.Drawing.Point(12, 160); label3.Text = "Ngày sinh:";
            // dateTimePicker1
            dateTimePicker1.Font     = new System.Drawing.Font("Segoe UI", 10F);
            dateTimePicker1.Format   = System.Windows.Forms.DateTimePickerFormat.Short;
            dateTimePicker1.Location = new System.Drawing.Point(12, 182);
            dateTimePicker1.Size     = new System.Drawing.Size(330, 34);

            // label4 – Giới tính
            label4.AutoSize = true; label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            label4.Location = new System.Drawing.Point(12, 225); label4.Text = "Giới tính:";
            // comboBox1
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox1.Font          = new System.Drawing.Font("Segoe UI", 10F);
            comboBox1.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            comboBox1.Location      = new System.Drawing.Point(12, 247);
            comboBox1.Size          = new System.Drawing.Size(330, 36);
            comboBox1.SelectedIndex = 0;

            // label5 – Lớp
            label5.AutoSize = true; label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            label5.Location = new System.Drawing.Point(12, 290); label5.Text = "Lớp:";
            // comboBox2
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.Font          = new System.Drawing.Font("Segoe UI", 10F);
            comboBox2.Location      = new System.Drawing.Point(12, 312);
            comboBox2.Size          = new System.Drawing.Size(330, 36);

            // ── Buttons CRUD ─────────────────────────────────────────────────
            StyleBtn(button1, "Thêm",    System.Drawing.Color.FromArgb(59,  130, 246));
            button1.Location = new System.Drawing.Point(10,  600); button1.Size = new System.Drawing.Size(167, 50);
            button1.Click   += new System.EventHandler(btnThem_Click);

            StyleBtn(button2, "Sửa",     System.Drawing.Color.FromArgb(22,  163,  74));
            button2.Location = new System.Drawing.Point(185, 600); button2.Size = new System.Drawing.Size(167, 50);
            button2.Click   += new System.EventHandler(btnSua_Click);

            StyleBtn(button3, "Xóa",     System.Drawing.Color.FromArgb(220,  38,  38));
            button3.Location = new System.Drawing.Point(10,  658); button3.Size = new System.Drawing.Size(167, 50);
            button3.Click   += new System.EventHandler(btnXoa_Click);

            StyleBtn(button4, "Làm mới", System.Drawing.Color.FromArgb(107, 114, 128));
            button4.Location = new System.Drawing.Point(185, 658); button4.Size = new System.Drawing.Size(167, 50);
            button4.Click   += new System.EventHandler(btnLamMoi_Click);

            StyleBtn(btnLuu, "Lưu",      System.Drawing.Color.FromArgb(0,   150, 136));
            btnLuu.Location  = new System.Drawing.Point(10,  600); btnLuu.Size  = new System.Drawing.Size(342, 50);
            btnLuu.Visible   = false;
            btnLuu.Click    += new System.EventHandler(btnLuu_Click);

            // ── Tìm kiếm ─────────────────────────────────────────────────────
            label6.AutoSize = true; label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            label6.Location = new System.Drawing.Point(383, 13); label6.Text = "Tìm kiếm (Tên / Mã SV / Lớp):";

            textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox3.Font        = new System.Drawing.Font("Segoe UI", 10F);
            textBox3.Location    = new System.Drawing.Point(383, 35);
            textBox3.Size        = new System.Drawing.Size(400, 34);

            StyleBtn(button5, "Tìm", System.Drawing.Color.FromArgb(30, 58, 95));
            button5.Location = new System.Drawing.Point(792, 33); button5.Size = new System.Drawing.Size(100, 38);
            button5.Click   += new System.EventHandler(btnTimKiem_Click);

            // ── DataGridView ──────────────────────────────────────────────────
            dataGridViewCellStyle1.BackColor  = System.Drawing.Color.FromArgb(243, 244, 246);
            dataGridViewCellStyle1.Font       = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor  = System.Drawing.Color.Black;

            dataGridView1.AllowUserToAddRows    = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode   = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor       = System.Drawing.Color.White;
            dataGridView1.BorderStyle           = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
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
                { colMaSV, colHoTen, colGioiTinh, colNgaySinh, colLop });
            dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellClick);

            colMaSV.HeaderText    = "Mã SV";      colMaSV.Name    = "colMaSV";    colMaSV.ReadOnly    = true;
            colHoTen.HeaderText   = "Họ và Tên";  colHoTen.Name   = "colHoTen";   colHoTen.ReadOnly   = true;
            colGioiTinh.HeaderText= "Giới Tính";  colGioiTinh.Name= "colGioiTinh";colGioiTinh.ReadOnly= true;
            colNgaySinh.HeaderText= "Ngày Sinh";  colNgaySinh.Name= "colNgaySinh";colNgaySinh.ReadOnly= true;
            colLop.HeaderText     = "Lớp";         colLop.Name     = "colLop";     colLop.ReadOnly     = true;

            // ── Phân trang ────────────────────────────────────────────────────
            StyleNavBtn(button6, "<<"); button6.Location = new System.Drawing.Point(383, 650);
            button6.Click += new System.EventHandler(btnTrangDau_Click);

            StyleNavBtn(button7, "<");  button7.Location = new System.Drawing.Point(429, 650);
            button7.Click += new System.EventHandler(btnTrangTruoc_Click);

            StyleNavBtn(button9, ">");  button9.Location = new System.Drawing.Point(783, 650);
            button9.Click += new System.EventHandler(btnTrangSau_Click);

            StyleNavBtn(button8, ">>"); button8.Location = new System.Drawing.Point(829, 650);
            button8.Click += new System.EventHandler(btnTrangCuoi_Click);

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
                label7, button8, button9, button7, button6,
                dataGridView1, button5, textBox3, label6,
                button4, button3, button2, button1, btnLuu,
                groupBox1
            });
            Name = "UCQuanLySinhVien";

            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        // ── helper style ─────────────────────────────────────────────────────
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

        // ── fields ───────────────────────────────────────────────────────────
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1, label2, label3, label4, label5;
        private System.Windows.Forms.TextBox textBox1, textBox2, textBox3;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox comboBox1, comboBox2;
        private System.Windows.Forms.Button button1, button2, button3, button4, btnLuu;
        private System.Windows.Forms.Label label6, label7;
        private System.Windows.Forms.Button button5, button6, button7, button8, button9;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaSV, colHoTen, colGioiTinh, colNgaySinh, colLop;
    }
}
