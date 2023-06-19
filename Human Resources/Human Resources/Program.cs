using Human_Resources.Data;
using Human_Resources.Data.Services;
using Human_Resources.Hubs;
using Human_Resources.Middlewares;
using Human_Resources.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using MailKit;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));
var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IDepartmentService,DepartmentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IEducationalFieldService, EducationalFieldService>();
builder.Services.AddScoped<IPromotionService, PromotionService>();
builder.Services.AddScoped<IAppraisalService, AppraisalService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IRewardService, RewardService>();
builder.Services.AddScoped<ILeaveService, LeaveService>();
builder.Services.AddScoped<IRejectedLeaveService, RejectedLeaveService>();
builder.Services.AddScoped<IConfirmedLeaveService, ConfirmedLeaveService>();
builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
builder.Services.AddScoped<ILeaveEncashmentService, LeaveEncashmentService>();
builder.Services.AddScoped<ICheckInTrackListService, CheckInTrackListService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<IHolidayService, HolidayService>();
builder.Services.AddScoped<ICheckOutTrackListService, CheckOutTrackListService>();
builder.Services.AddScoped<ICertificationService, CertificationService>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddCors();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});
builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()1234567890";
});
builder.Services.AddSignalR();
builder.Services.AddTransient<IEmailService, EmailService>();



var app = builder.Build();
var User = new ClaimsPrincipal();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());


app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();
app.UseMiddleware<AuthenticationMiddleware>();
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Department}/{action=Index}/{id?}");



AppDbInitializer.SeedRole(app).Wait();
AppDbInitializer.SeedEncashment(app).Wait();
AppDbInitializer.CheckTruants(app).Wait();

app.MapHub<MessageHub>("/messageHub");

app.Run();
