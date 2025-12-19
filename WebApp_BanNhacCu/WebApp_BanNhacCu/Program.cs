using WebApp_BanNhacCu.Payments;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.Use(async (context, next) =>
{
    var path = context.Request.Path.ToString().ToLower();

    if (path.StartsWith("/admin"))
    {
        var role = context.Session.GetString("UserRole");

        if (string.IsNullOrEmpty(role) || role != "Admin" && role !="Staff")
        {
            context.Response.Redirect("/TaiKhoan/DangNhap");
            return;
        }
    }

    if (path.StartsWith("/carrier"))
    {
        var role = context.Session.GetString("UserRole");

        if (string.IsNullOrEmpty(role) || role != "Carrier")
        {
            context.Response.Redirect("/TaiKhoan/DangNhap");
            return;
        }
    }

    if (path.StartsWith("/admin/thongke") || path.StartsWith("/admin/nhanvien"))
    {
        var role = context.Session.GetString("UserRole");

        if (string.IsNullOrEmpty(role) || role != "Admin")
        {
            context.Response.Redirect("/TaiKhoan/DangNhap");
            return;
        }
    }

    await next();
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
