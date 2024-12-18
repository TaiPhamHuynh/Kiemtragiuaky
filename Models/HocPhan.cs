using System;
namespace Kiemtragiuaky.Models
{
    public class HocPhan
    {
        public string MaHP { get; set; }
        public string TenHP { get; set; }
        public int SoTinChi { get; set; }
        public int SoLuongDuKien { get; set; }

        public ICollection<ChiTietDangKy> ChiTietDangKy { get; set; }
    }
}

