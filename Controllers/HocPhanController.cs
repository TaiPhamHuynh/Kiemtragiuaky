using Microsoft.AspNetCore.Mvc;
using Kiemtragiuaky.Models;
using Kiemtragiuaky.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Kiemtragiuaky.Controllers
{
    public class HocPhanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HocPhanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hành động hiển thị danh sách môn học
        public async Task<IActionResult> Index()
        {
            var maSV = HttpContext.Session.GetString("MaSV");
            if (string.IsNullOrEmpty(maSV))
            {
                return RedirectToAction("Index", "DangNhap"); // Chuyển hướng đến trang đăng nhập nếu không có session
            }

            var hocPhan = await _context.HocPhan.ToListAsync(); // Lấy tất cả môn học từ DB
            return View(hocPhan);
        }

        // Hành động đăng ký học phần
        [HttpPost]
        public async Task<IActionResult> DangKyHocPhan(string maHocPhan)
        {
            // Kiểm tra xem sinh viên đã đăng nhập hay chưa
            var maSV = HttpContext.Session.GetString("MaSV");

            if (maSV == null)
            {
                // Nếu chưa đăng nhập, chuyển hướng về trang đăng nhập
                return RedirectToAction("Index", "DangNhap");
            }

            var sinhVien = await _context.SinhVien.FirstOrDefaultAsync(sv => sv.MaSV.ToString() == maSV);
            if (sinhVien == null)
            {
                return NotFound();
            }

            // Kiểm tra học phần có tồn tại không
            var hocPhan = await _context.HocPhan.FirstOrDefaultAsync(hp => hp.MaHP == maHocPhan);
            if (hocPhan == null)
            {
                return NotFound();
            }

            // Tạo đăng ký mới
            var dangKy = new DangKy
            {
                MaSV = sinhVien.MaSV,
                NgayDK = DateTime.Now
            };

            _context.DangKy.Add(dangKy);
            await _context.SaveChangesAsync();

            // Tạo chi tiết đăng ký học phần
            var chiTietDangKy = new ChiTietDangKy
            {
                MaDK = dangKy.MaDK,
                MaHP = hocPhan.MaHP
            };

            _context.ChiTietDangKy.Add(chiTietDangKy);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "HocPhan");
        }
    }
}
