﻿using Human_Resources.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Human_Resources.Data
{
    public class AppDbContext:IdentityDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the foreign key relationship between Promotion and Position
            modelBuilder.Entity<Promotion>()
                .HasOne(p => p.PositionFrom)
                .WithMany()
                .HasForeignKey(p => p.fromPositionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Promotion>()
                .HasOne(p => p.PositionTo)
                .WithMany()
                .HasForeignKey(p => p.toPositionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the foreign key relationship between Employee and Position
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Position)
                .WithMany()
                .HasForeignKey(e => e.PositionId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Position>()
                        .HasOne(p => p.Department)
                        .WithMany()
                        .HasForeignKey(p => p.DepartmentId)
                        .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Position>()
                        .HasOne(p => p.position)
                        .WithMany()
                        .HasForeignKey(p=>p.PositionId)
                        .OnDelete(DeleteBehavior.Restrict);

        }


        public DbSet<Employee> Employees { get; set; }
        public DbSet<EducationalField> EducationalFields { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<GradeCategory> GradeCategories { get; set; }
        public DbSet<Allowance> Allowances { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<Appraisal> Appraisals { get; set;}
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ConfirmedLeave> ConfirmedLeaves { get; set; }
        public DbSet<RejectedLeave> RejectedLeaves { get; set; }
        public DbSet<LeaveTypes> LeaveType { get; set; }
        public DbSet<LeaveEncashment> LeaveEncashments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<CheckInTrackList> CheckInTrackLists { get; set;}
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<Holiday> Holidays { get; set; }

    }
}
