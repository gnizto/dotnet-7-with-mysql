using HelloWorld.Models;
using Microsoft.EntityFrameworkCore;

namespace HelloWorld.Data;

public class DataContextEF : DbContext
{
    public DbSet<Computer>? Computer { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseSqlServer("Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true",
                options => options.EnableRetryOnFailure());
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("TutorialAppSchema");

        modelBuilder.Entity<Computer>()
            .HasKey(computer => computer.ComputerId);
            // .HasNoKey();
            // .ToTable("Computer", "TutorialAppSchema");
            // .ToTable("TableName", "SchemaName");
    }
}