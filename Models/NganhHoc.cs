using System;
namespace Kiemtragiuaky.Models
{
    public class NganhHoc
    {
        public string MaNganh { get; set; }
        public string TenNganh { get; set; }

        public ICollection<SinhVien> SinhVien { get; set; }
    }

}

