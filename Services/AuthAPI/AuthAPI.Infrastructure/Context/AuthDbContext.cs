using AuthAPI.Core.Entities;
using AuthAPI.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Infrastructure.Context;

internal class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
        SeedAdmin();
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

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<AccountEntity>()
        .Where(e => e.State == EntityState.Modified);

        var now = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            entry.Entity.UpdatedAtUtc = now;
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    private void SeedAdmin()
    {
        Accounts.Add(
            new AccountEntity
            {
                Id = Guid.NewGuid(),
                Email = "admin@gmail.com",
                HashPassword = "$2y$12$Gx1zD6C6V3SAbMNtFpuRp.dJvPRAjJzplYrjXABhnsIezCrx7VgxS",
                Role = Roles.Admin,
            });
    }
}
