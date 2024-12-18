using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kiemtragiuaky.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Kiemtragiuaky.Data;

namespace Kiemtragiuaky.Controllers
{
    public class DangKyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DangKyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hành động để đăng ký học phần
        [HttpPost]
        public async Task<IActionResult> DangKyHocPhan(string maHocPhan)
        {
            // Lấy sinh viên hiện tại (ví dụ từ session hoặc user đã đăng nhập)
            var sinhVien = await _context.SinhVien.FirstOrDefaultAsync(sv => sv.MaSV == 1); // Thay 12345 bằng mã sinh viên thực tế

            if (sinhVien == null)
            {
                return NotFound();
            }

            // Lấy học phần theo mã
            var hocPhan = await _context.HocPhan.FirstOrDefaultAsync(hp => hp.MaHP == maHocPhan);

            if (hocPhan == null)
            {
                return NotFound();
            }

            // Tạo bản ghi đăng ký học phần
            var dangKy = new DangKy
            {
                MaSV = sinhVien.MaSV,
                NgayDK = DateTime.Now
            };

            _context.DangKy.Add(dangKy);
            await _context.SaveChangesAsync();

            // Tạo bản ghi chi tiết đăng ký học phần
            var chiTietDangKy = new ChiTietDangKy
            {
                MaDK = dangKy.MaDK,
                MaHP = hocPhan.MaHP
            };

            _context.ChiTietDangKy.Add(chiTietDangKy);
            await _context.SaveChangesAsync();

            // Chuyển hướng về trang danh sách học phần sau khi đăng ký thành công
            return RedirectToAction("Index", "HocPhan");
        }
    }
}
