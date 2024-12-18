using System;
namespace Kiemtragiuaky.Models
{
    public class SinhVien
    {
        public long MaSV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public String Hinh { get; set; }
        public string MaNganh { get; set; }

        public NganhHoc NganhHoc { get; set; }
        public ICollection<DangKy> DangKy { get; set; }
    }

}

