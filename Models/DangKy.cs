using System;
namespace Kiemtragiuaky.Models
{
    public class DangKy
    {
        public int MaDK { get; set; }
        public DateTime NgayDK { get; set; }
        public long MaSV { get; set; }

        public SinhVien SinhVien { get; set; }
        public ICollection<ChiTietDangKy> ChiTietDangKy { get; set; }
    }

}

