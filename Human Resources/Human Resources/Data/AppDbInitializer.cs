﻿using Human_Resources.Data.Helpers;
using Human_Resources.Data.Static;
using Human_Resources.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Human_Resources.Data
{
    public class AppDbInitializer
    {

        public static async Task SeedRole(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                if (!await roleManager.RoleExistsAsync(UserRoles.HRManager))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.HRManager));
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@gmail.com";

                var adminUser = await userManager.FindByNameAsync("administrator");
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        Name = "HRAdmin",
                        UserName = "administrator",
                        pictureURL = "",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        PositionName = "Admin"

                    };
                    var result = await userManager.CreateAsync(newAdminUser, "Afri@1298!");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);

                    }


                }

            }
        }
        public static async Task SeedEncashment(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                var locks = await context.LeaveLocks.ToListAsync();
                var configData = await context.Configurations.FirstOrDefaultAsync(n => n.Id == 1);
                var maxValTime = DateTime.MinValue;
                if (locks != null)
                {
                    foreach(var loc in locks)
                    {
                        if(maxValTime < loc.lockTime)
                        {
                            maxValTime = loc.lockTime;
                        }
                    }
                }
                if (locks == null || (DateTime.Now - maxValTime).TotalHours > 24)
                {
                    if (DateTime.Now.Month == configData.LeaveEncashmentSyncDate.Month && DateTime.Now.Day == configData.LeaveEncashmentSyncDate.Day)
                    {
                        var encashments = await context.LeaveEncashments.ToListAsync();
                        foreach (var encashment in encashments)
                        {
                            encashment.Credit = encashment.Credit + 50;
                            context.LeaveEncashments.Update(encashment);
                            await context.SaveChangesAsync();
                        }
                    }
                    await context.LeaveLocks.AddAsync(new LeaveLock
                    {
                        lockTime = DateTime.Now
                    });
                    await context.SaveChangesAsync();
                }
            }
        }
        public static async Task SeedAttendance(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                var employees = await context.Employees.ToListAsync();
                foreach (var employee in employees)
                {
                    var attendance = new Attendance()
                    {
                        EmployeeId = employee.Id,

                    };
                    await context.Attendances.AddAsync(attendance);
                    await context.SaveChangesAsync();
                }
            }
        }

        public static async Task CheckTruants(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                int multiplier = 1;
                bool isHoliday = false;


                if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                {
                    multiplier = 3;
                }
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                var attendances = await context.Attendances.ToListAsync();
                var checkins = await context.CheckInTrackLists.ToListAsync();
                var holidays = await context.Holidays.ToListAsync();
                var locks = await context.AttendanceLocks.ToListAsync();
                var configData = await context.Configurations.FirstOrDefaultAsync(n => n.Id == 1);
                var maxValTime = DateTime.MinValue;
                if (locks != null)
                {
                    foreach (var loc in locks)
                    {
                        if (maxValTime < loc.lockTime)
                        {
                            maxValTime = loc.lockTime;
                        }
                    }
                }
                foreach (var holiday in holidays)
                {
                    int day = DateTime.Now.Day;
                    int month = DateTime.Now.Month; //checking if the date is 1 from the 31 entries.
                    if (day == 1)
                    {
                        day = 31;
                        month -= 1;
                    }
                    else
                    {
                        day -= 1;
                    }
                    if (holiday.Month == month && holiday.Date == day)
                    {
                        isHoliday = true;
                        break;
                    }
                }
                if (DateTime.Now.Hour <= configData.AttendanceSyncTime.Hour && isHoliday == false && (locks==null || (DateTime.Now - maxValTime).TotalHours >= 24))
                {
                    foreach (var attendance in attendances)
                    {
                        var lis = new List<CheckInTrackList>();
                        foreach (var check in checkins)
                        {
                            if (check.AttendanceId == attendance.Id)
                            {
                                lis.Add(check);
                            }
                        }
                        if (lis.Count > 0)
                        {


                            lis = lis.OrderBy(n => n.CheckInTime).ToList();
                            var diff = DateTime.Now - lis[lis.Count - 1].CheckInTime;
                            if (diff.TotalHours > 24 * multiplier) //Multiplier to check for weekends
                            {
                                attendance.NoOfAbsentCheck += 1;
                                context.Attendances.Update(attendance);
                                await context.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            attendance.NoOfAbsentCheck += 1;
                            context.Attendances.Update(attendance);
                            await context.SaveChangesAsync();
                        }
                    }
                    await context.AttendanceLocks.AddAsync(new AttendanceLock
                    {
                        lockTime = DateTime.Now,
                    });
                    await context.SaveChangesAsync();

                }

            }
        }
        public static async Task SeedConfiguration(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //Cinema
                if (!context.Configurations.Any())
                {
                    context.Configurations.Add(new Configuration()
                    {
                        HoursOfWork = 7,
                        percentDecreaseAbsent = 5,
                        percentDecreaseLate = 3,
                        AttendanceSyncTime =  new DateTime(2023,6,20,8,0,0),
                        LeaveEncashmentSyncDate = new DateTime(2023,7,7,0,0,0),
                    });
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}