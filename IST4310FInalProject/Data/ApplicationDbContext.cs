using IST4310FInalProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace IST4310FInalProject.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<StudentInfo> StudentInfos { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<WorkExperience> WorkExperiences { get; set; }
    public DbSet<Project> Projects { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Define relationship between StudentInfo and IdentityUser
        builder.Entity<StudentInfo>()
            .HasOne(s => s.ApplicationUser)
            .WithMany() // or .WithMany(u => u.StudentInfos) if you add a collection in IdentityUser
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade); // Optional: Configure delete behavior

        builder.Entity<Education>()
            .HasOne(e => e.StudentInfo)
            .WithMany(s => s.Educations)  // You can add Educations property in StudentInfo if needed
            .HasForeignKey(e => e.StudentInfoId)
            .OnDelete(DeleteBehavior.Cascade); // Specify delete behavior (optional)

        // One-to-many relationship: StudentInfo to WorkExperience
        builder.Entity<WorkExperience>()
            .HasOne(w => w.StudentInfo)
            .WithMany(s => s.WorkExperiences)  // You can add WorkExperiences property in StudentInfo if needed
            .HasForeignKey(w => w.StudentInfoId)
            .OnDelete(DeleteBehavior.Cascade); // Specify delete behavior (optional)

        // One-to-many relationship: StudentInfo to Project
        builder.Entity<Project>()
            .HasOne(p => p.StudentInfo)
            .WithMany(s => s.Projects)  // You can add Projects property in StudentInfo if needed
            .HasForeignKey(p => p.StudentInfoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
