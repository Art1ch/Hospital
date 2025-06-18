using AuthAPI.Application.Contracts.Repository.Account;
using AuthAPI.Core.Entities;
using AuthAPI.Infrastructure.Context;
using AuthAPI.Infrastructure.Repositories.Abstract;

namespace AuthAPI.Infrastructure.Repositories.Implementations;

internal class RefreshTokenRepository : Repository<RefreshTokenEntity, int>, IRefreshTokenRepository
{
    public RefreshTokenRepository(AuthDbContext dbContext) : base(dbContext)
    {
        
    }
}
