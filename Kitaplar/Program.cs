global using Kitaplar.Data;
using Kitaplar;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(config =>
{
    config.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
    config.UseLazyLoadingProxies();
  





});

builder.Services.AddIdentity<User, Role>(config =>
{
    config.Password.RequiredLength = 6;
    config.Password.RequireLowercase = true;
    config.Password.RequireUppercase = true;
    config.Password.RequireNonAlphanumeric = true;
    config.Password.RequireDigit = true;
    config.Password.RequiredUniqueChars = 3;

    config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    config.Lockout.MaxFailedAccessAttempts = 3;
})
  .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.UseKitaplar();

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
    );


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
