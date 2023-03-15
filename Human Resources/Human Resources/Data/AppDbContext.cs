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
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<EducationalField> EducationalFields { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<GradeCategory> GradeCategories { get; set; }
        public DbSet<Allowance> Allowances { get; set; }
        public DbSet<Department> Departments { get; set; }


    }
}
