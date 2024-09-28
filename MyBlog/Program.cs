using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MyBlog.Models;
using MyBlog.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlOptions => sqlOptions.EnableRetryOnFailure(
        maxRetryCount: 5,
        maxRetryDelay: TimeSpan.FromSeconds(10),
        errorNumbersToAdd: null)));


builder.Services.AddDbContext<IdentityContext>(
        options =>
        {
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(10),
                            errorNumbersToAdd: null);
                    });

        });

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
.AddEntityFrameworkStores<IdentityContext>();

builder.Services.AddRazorPages();

builder.Services.AddSession();
//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//builder.Services.AddServerSideBlazor();

builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Login";
            options.AccessDeniedPath = "/Account/AccessDenied";
        });


builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbcontext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbcontext.Database.EnsureCreated();

    var identityContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
    identityContext.Database.EnsureCreated();

    var services = scope.ServiceProvider;
    await SeedData.Initialize(services);
}

app.UseStaticFiles();
app.UseSession();

app.MapControllerRoute(
        name: "blogPost",
        pattern: "{slug}",
        defaults: new { controller = "Home", action = "Details" }
        );


app.MapDefaultControllerRoute();

app.UseAuthentication();
app.UseAuthorization();

//app.MapBlazorHub();
app.MapRazorPages();
//app.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");

app.UseRequestLocalization(opts =>
{
    opts.AddSupportedCultures("en-US")
    .AddSupportedUICultures("eb-US")
    .SetDefaultCulture("en-US");
});
app.Run();

