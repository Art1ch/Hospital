using DoctorAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DoctorAPI.Infrastructure.Context;

public class DoctorDbContext : DbContext
{
    public DoctorDbContext(DbContextOptions<DoctorDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<DoctorEntity> MyProperty { get; set; }
}
