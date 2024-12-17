using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using XYZ_Pharmaceuticals.Entities;

namespace XYZ_Pharmaceuticals.Models;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<JobApplication> JobApplications { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<Category> Categories { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>()
            .HasIndex(c => c.Email)
            .IsUnique();
        modelBuilder.Entity<Category>().HasData(
            new Category { ID = 1, CategoryName = "Tablet" },
            new Category { ID = 2, CategoryName = "Liquid Filling" },
            new Category { ID = 3, CategoryName = "Capsule/Encapsulation" }
        );
        base.OnModelCreating(modelBuilder);
    }
}
