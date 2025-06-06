using DoctorAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorAPI.Infrastructure.Context;

public class DoctorDbContext<TDoctorId, TSpecializationId>
    : DbContext
{
    public DoctorDbContext(
        DbContextOptions<DoctorDbContext<TDoctorId, TSpecializationId>> options)
        : base(options)
    {
    }

    public DbSet<DoctorEntity<TDoctorId, TSpecializationId>> Doctors { get; set; }
    public DbSet<SpecializationEntity<TDoctorId, TSpecializationId>> Specializations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DoctorEntity<TDoctorId, TSpecializationId>>(entity =>
        {
            entity.HasKey(d => d.Id);

            entity.HasOne(d => d.Specialization)
                  .WithOne(s => s.Doctor)
                  .HasForeignKey<DoctorEntity<TDoctorId, TSpecializationId>>(
                d => d.SpecializationId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SpecializationEntity<TDoctorId, TSpecializationId>>(entity =>
        {
            entity.HasKey(s => s.Id);

            entity.HasOne(s => s.Doctor)
                  .WithOne(d => d.Specialization)
                  .HasForeignKey<SpecializationEntity<TDoctorId, TSpecializationId>>(
                s => s.DoctorId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}