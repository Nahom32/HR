using Human_Resources.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Human_Resources.Data
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
        //    modelBuilder.Entity<Branch>()
        //    .HasOne(b => b.Department)
        //    .WithMany(d => d.Branches)
        //    .HasForeignKey(b => b.DepartmentId)
        //    .IsRequired(false)
        //    .OnDelete(DeleteBehavior.Restrict);

        //}
        DbSet<Employee> Employees { get; set; }
        DbSet<Branch> Branches { get; set; }
        DbSet<EducationalField> EducationalFields { get; set; }
        DbSet<Grade> Grades { get; set; }
        DbSet<Position> Positions { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<GradeCategory> GradeCategories { get; set; }
        DbSet<Allowance> Allowances { get; set; }
        DbSet<Department> Departments { get; set; }


    }
}
