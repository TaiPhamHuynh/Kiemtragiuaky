//using Kiemtragiuaky.Data;
//using Kiemtragiuaky.Models;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using System.IO;

//namespace Kiemtragiuaky.Controllers
//{
//    public class DangNhapController : Controller
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly IWebHostEnvironment _webHostEnvironment;

//        public DangNhapController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
//        {
//            _context = context;
//            _webHostEnvironment = webHostEnvironment;
//        }

//        public IActionResult Index()
//        {
//            var sinhVien = _context.SinhVien
//            .Include(sv => sv.NganhHoc);
//            return View(sinhVien);
//        }

//        // Hành động xử lý đăng nhập
//        [HttpPost]
//        public IActionResult Login(SinhVien sinhVien, int maSV)
//        {
//            if (maSV == sinhVien.MaSV)
//            {
//                HttpContext.Session.SetString("MaSV", maSV.ToString());
//                return RedirectToAction("Index", "HocPhan");
//            }
//            else
//            {
//                ViewBag.ErrorMessage = "Mã sinh viên không hợp lệ.";
//                return View("Index");
//            }
//        }
//    }
//}

using Microsoft.AspNetCore.Http;
using Kiemtragiuaky.Models;
using Kiemtragiuaky.Data;
using Microsoft.AspNetCore.Mvc;

namespace Kiemtragiuaky.Controllers
{
    public class DangNhapController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DangNhapController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(int maSV)
        {
            var sinhVien = _context.SinhVien.FirstOrDefault(sv => sv.MaSV == maSV);
            if (sinhVien != null)
            {
                // Lưu mã sinh viên vào session
                HttpContext.Session.SetString("MaSV", maSV.ToString());
                return RedirectToAction("Index", "HocPhan");
            }
            else
            {
                // Hiển thị thông báo lỗi nếu mã sinh viên không hợp lệ
                ViewBag.ErrorMessage = "Mã sinh viên không hợp lệ.";
                return View("Index");
            }
        }
    }

}


