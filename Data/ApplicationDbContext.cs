using Npgsql;
using Microsoft.EntityFrameworkCore;
using Kiemtragiuaky.Models;

namespace Kiemtragiuaky.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        // Các DbSet cho các bảng trong cơ sở dữ liệu
        public DbSet<SinhVien> SinhVien { get; set; }
        public DbSet<NganhHoc> NganhHoc { get; set; }
        public DbSet<HocPhan> HocPhan { get; set; }
        public DbSet<DangKy> DangKy { get; set; }
        public DbSet<ChiTietDangKy> ChiTietDangKy { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietDangKy>(entity =>
            {
                entity.HasKey(e => new { e.MaDK, e.MaHP });
            });

            modelBuilder.Entity<DangKy>(entity =>
            {
                entity.HasKey(e => e.MaDK);
            });

            modelBuilder.Entity<HocPhan>(entity =>
            {
                entity.HasKey(e => e.MaHP);
            });

            modelBuilder.Entity<NganhHoc>(entity =>
            {
                entity.HasKey(e => e.MaNganh);
            });

            modelBuilder.Entity<SinhVien>()
                .HasKey(s => s.MaSV);

            modelBuilder.Entity<SinhVien>()
                .HasOne(s => s.NganhHoc)  // Quan hệ với bảng NganhHoc
                .WithMany(n => n.SinhVien)  // Quan hệ một-nhiều, hoặc nhiều-một tùy vào mô hình dữ liệu
                .HasForeignKey(s => s.MaNganh)  // Đảm bảo rằng cột này là khóa ngoại
                .HasConstraintName("fk_MaNganh");
        }
    }
}
