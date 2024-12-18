using System;
namespace Kiemtragiuaky.Models
{
    public class ChiTietDangKy
    {
        public int MaDK { get; set; }
        public string MaHP { get; set; }

        public DangKy DangKy { get; set; }
        public HocPhan HocPhan { get; set; }
    }
}

