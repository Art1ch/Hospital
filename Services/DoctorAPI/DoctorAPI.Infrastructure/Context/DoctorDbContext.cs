using DoctorAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorAPI.Infrastructure.Context;

public class DoctorDbContext : DbContext
{
    public DoctorDbContext(DbContextOptions<DoctorDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<DoctorEntity> Doctors { get; set; }
    public DbSet<SpecializationEntity> Specializations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DoctorEntity>()
            .HasKey(d => d.Id);

        modelBuilder.Entity<DoctorEntity>()
            .HasOne(d => d.Specialization)
            .WithOne(s => s.Doctor)
            .HasForeignKey<DoctorEntity>(d => d.SpecializationId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SpecializationEntity>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<SpecializationEntity>()
            .HasOne(s => s.Doctor)
            .WithOne(s => s.Specialization)
            .HasForeignKey<SpecializationEntity>(s => s.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
