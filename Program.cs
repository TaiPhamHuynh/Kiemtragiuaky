using Kiemtragiuaky.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".MaSVSession";
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn phiên làm việc
    options.Cookie.HttpOnly = true;  // Đảm bảo cookie không thể truy cập từ JavaScript
    options.Cookie.IsEssential = true;  // Đảm bảo cookie luôn được gửi cùng với yêu cầu
});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Cấu hình DbContext với kết nối PostgreSQL và logging câu lệnh SQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"))
           .LogTo(Console.WriteLine, LogLevel.Information)  // Ghi log câu lệnh SQL
           .EnableSensitiveDataLogging());  // Tùy chọn để ghi log các dữ liệu nhạy cảm (nên dùng cẩn thận)

builder.Services.AddControllersWithViews();

var cultureInfo = new CultureInfo("vi-VN");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllersWithViews().AddDataAnnotationsLocalization();

var app = builder.Build();

// Cấu hình các middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SinhVien}/{action=Index}/{id?}");

app.Run();
