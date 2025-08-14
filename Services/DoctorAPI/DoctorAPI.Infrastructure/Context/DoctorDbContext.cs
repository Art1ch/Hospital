using DoctorAPI.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorAPI.Infrastructure.Context;

internal class DoctorDbContext : DbContext
{
    public DoctorDbContext(DbContextOptions<DoctorDbContext> options) : base(options)
    {
        Database.EnsureDeleted();
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
                  .HasForeignKey<DoctorEntity>(d => d.SpecializationId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}