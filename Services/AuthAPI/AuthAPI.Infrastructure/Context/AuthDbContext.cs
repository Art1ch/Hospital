using AuthAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Infrastructure.Context;

internal class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public DbSet<AccountEntity> Accounts { get; set; }
    public DbSet<ReferenceTokenEntity> ReferenceTokens { get; set; }
    public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<AccountEntity>(entity =>
        {
            entity.HasKey(a => a.Id);

            entity
            .HasOne(a => a.ReferenceToken)
            .WithOne(r => r.Account)
            .HasForeignKey<ReferenceTokenEntity>(r => r.AccountId)
            .OnDelete(DeleteBehavior.Cascade);

            entity
            .HasOne(a => a.RefreshToken)
            .WithOne(r => r.Account)
            .HasForeignKey<RefreshTokenEntity>(r => r.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
