using DoctorAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorAPI.Infrastructure.Context;

public class DoctorDbContext<TId1, TId2> : DbContext
{
    public DoctorDbContext(DbContextOptions<DoctorDbContext<TId1, TId2>> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<DoctorEntity<TId1, TId2>> Doctors { get; set; }
    public DbSet<SpecializationEntity<TId1, TId2>> Specializations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DoctorEntity<TId1, TId2>>()
            .HasKey(d => d.Id);

        modelBuilder.Entity<DoctorEntity<TId1, TId2>>()
            .HasOne(d => d.Specialization)
            .WithOne(s => s.Doctor)
            .HasForeignKey<DoctorEntity<TId1, TId2>>(d => d.SpecializationId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SpecializationEntity<TId1, TId2>>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<SpecializationEntity<TId1, TId2>>()
            .HasOne(s => s.Doctor)
            .WithOne(d => d.Specialization)
            .HasForeignKey<SpecializationEntity<TId1, TId2>>(s => s.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
