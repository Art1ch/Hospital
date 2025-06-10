using DoctorAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorAPI.Infrastructure.Context;

public class DoctorDbContext
    : DbContext
{
    public DoctorDbContext(
        DbContextOptions<DoctorDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<DoctorEntity> Doctors { get; set; }
    public DbSet<SpecializationEntity> Specializations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DoctorEntity>(entity =>
        {
            entity.HasKey(d => d.Id);

            entity.HasOne(d => d.Specialization)
                  .WithOne(s => s.Doctor)
                  .HasForeignKey<DoctorEntity>(
                d => d.SpecializationId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SpecializationEntity>(entity =>
        {
            entity.HasKey(s => s.Id);

            entity.HasOne(s => s.Doctor)
                  .WithOne(d => d.Specialization)
                  .HasForeignKey<SpecializationEntity>(
                s => s.DoctorId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}