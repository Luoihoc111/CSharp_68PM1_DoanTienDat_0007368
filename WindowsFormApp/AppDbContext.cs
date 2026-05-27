using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WindowsFormApp
{
	// Ánh xạ bảng tbl_lophoc
	[Table("tbl_lophoc")]
	public class LopHoc
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int id { get; set; }
		public string malop { get; set; }
		public string tenlop { get; set; }
		public string ghichu { get; set; }
	}

	// Ánh xạ bảng tbl_sinhviens
	[Table("tbl_sinhviens")]
	public class SinhVien
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)] // Bảng của bạn id không tự tăng
		public int id { get; set; }
		public string hoten { get; set; }
		public string gioitinh { get; set; }
		public DateTime? ngaysinh { get; set; }
		public string malop { get; set; }
	}

	// Lớp kết nối CSDL
	public class AppDbContext : DbContext
	{
		public DbSet<LopHoc> LopHocs { get; set; }
		public DbSet<SinhVien> SinhViens { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// LƯU Ý: Thay đổi Uid (User ID) và Pwd (Password) cho đúng với MySQL của bạn (thường Uid là root, Pwd để trống hoặc 123456...)
			string connectionString = "Server=localhost;Port=3306;Database=qlsv;Uid=root;Pwd=180105;";

			// Cấu hình sử dụng MySQL với Pomelo
			optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
		}
	}
}