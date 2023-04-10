using Human_Resources.Data;
using Human_Resources.Data.Services;
using Human_Resources.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));
builder.Services.AddScoped<IDepartmentService,DepartmentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IEducationalFieldService, EducationalFieldService>();
builder.Services.AddScoped<IPromotionService, PromotionService>();
builder.Services.AddScoped<IAppraisalService, AppraisalService>();
builder.Services.AddScoped<IRewardService, RewardService>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Department}/{action=Index}/{id?}");
AppDbInitializer.SeedRole(app).Wait();
app.Run();
