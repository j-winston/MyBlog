using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MyBlog.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(
        options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddRazorPages();

builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddServerSideBlazor();

builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/Account/AccessDenied";
        });


var app = builder.Build();

app.UseStaticFiles();
app.UseSession();

app.MapDefaultControllerRoute();

app.UseAuthentication();
app.UseAuthorization();


app.MapBlazorHub();
app.MapRazorPages();
app.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");

app.Run();

