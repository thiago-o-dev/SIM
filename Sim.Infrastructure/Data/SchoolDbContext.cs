using Sim.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Sim.Infrastructure.Data;

public class SystemDbContext : DbContext
{
    public SystemDbContext(DbContextOptions<SystemDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.HasMany(s => s.Enrollments)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.HasMany(c => c.Enrollments)
                .WithOne(e => e.Course)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Course>()
            .HasMany(c => c.Users)
            .WithMany(s => s.Courses)
            .UsingEntity<Enrollment>(
                j => j
                    .HasOne(e => e.User)
                    .WithMany(s => s.Enrollments)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne(e => e.Course)
                    .WithMany(c => c.Enrollments)
                    .HasForeignKey(e => e.CourseId)
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey(e => e.Id);
                    j.HasIndex(e => new { e.UserId, e.CourseId })
                        .IsUnique()
                        .HasDatabaseName("IX_Enrollments_UserId_CourseId");
                    j.Property(e => e.CreatedAt)
                        .HasDefaultValueSql("GETDATE()");
                    j.ToTable("Enrollments");
                });

        base.OnModelCreating(modelBuilder);
    }
}