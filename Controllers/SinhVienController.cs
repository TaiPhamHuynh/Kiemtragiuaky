using Kiemtragiuaky.Data;
using Kiemtragiuaky.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;

public class SinhVienController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;


    public SinhVienController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var sinhVien = _context.SinhVien
        .Include(sv => sv.NganhHoc)
        .OrderBy(sv => sv.MaSV)
        .ToList();

        return View(sinhVien);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create(SinhVien sinhVien, IFormFile HinhFile)
    {
        if (HinhFile != null && HinhFile.Length > 0)
        {
            // Đường dẫn thư mục lưu hình ảnh
            string uploadDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "image");

            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            // Đặt tên hình ảnh (có thể tạo tên ngẫu nhiên để tránh trùng lặp)
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(HinhFile.FileName);
            string filePath = Path.Combine(uploadDirectory, fileName);

            // Lưu hình ảnh vào thư mục
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await HinhFile.CopyToAsync(stream);
            }

            // Gán đường dẫn hình ảnh vào thuộc tính Hinh của SinhVien
            sinhVien.Hinh = "/image/" + fileName;
        }

        // Lưu sinh viên vào cơ sở dữ liệu (thực hiện lưu vào DB ở đây)
        _context.Add(sinhVien);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));  // Điều hướng về trang danh sách sinh viên
    }

    [HttpGet]
    public async Task<IActionResult> Edit(long id)
    {
        if (id == 0) return NotFound();

        var sinhVien = await _context.SinhVien
            .Include(sv => sv.NganhHoc)
            .FirstOrDefaultAsync(sv => sv.MaSV == id);

        if (sinhVien == null) return NotFound();

        // Truyền danh sách ngành học vào ViewBag
        ViewBag.NganhHoc = new SelectList(_context.NganhHoc, "MaNganh", "TenNganh", sinhVien.MaNganh);

        return View(sinhVien);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long id, SinhVien sinhVien, IFormFile file)
    {
        if (id != sinhVien.MaSV)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                // Lấy thông tin sinh viên hiện tại từ database
                var existingSinhVien = await _context.SinhVien.AsNoTracking().FirstOrDefaultAsync(sv => sv.MaSV == id);
                if (existingSinhVien == null)
                    return NotFound();

                // Nếu có file mới thì cập nhật hình ảnh
                if (file != null && file.Length > 0)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "image");
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(uploadDir, fileName);

                    // Lưu hình mới vào thư mục
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    sinhVien.Hinh = "/image/" + fileName; // Gán đường dẫn hình mới
                }
                else
                {
                    // Giữ nguyên hình ảnh cũ nếu không chọn file mới
                    sinhVien.Hinh = existingSinhVien.Hinh;
                }

                // Cập nhật thông tin khác của sinh viên
                //_context.Entry(sinhVien).State = EntityState.Modified;
                _context.Update(sinhVien);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Có lỗi xảy ra: {ex.Message}");
            }
        }

        // Truyền lại SelectList khi có lỗi
        ViewBag.NganhHoc = new SelectList(_context.NganhHoc, "MaNganh", "TenNganh", sinhVien.MaNganh);
        return View(sinhVien);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(long id)
    {
        if (id == 0) return NotFound();

        var sinhVien = await _context.SinhVien.FindAsync(id);
        if (sinhVien == null)
        {
            return NotFound();
        }

        return View(sinhVien);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        var sinhVien = await _context.SinhVien.FindAsync(id);
        if (sinhVien == null)
        {
            return NotFound();
        }

        _context.SinhVien.Remove(sinhVien);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(long id)
    {
        if (id == 0) return NotFound();

        var sinhVien = await _context.SinhVien
            .Include(sv => sv.NganhHoc)  // Include NganhHoc if you need to display the student's major
            .FirstOrDefaultAsync(sv => sv.MaSV == id);

        if (sinhVien == null)
        {
            return NotFound();
        }

        return View(sinhVien);
    }

}
