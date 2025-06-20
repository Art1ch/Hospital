using AuthAPI.Application.Contracts.Repository.Token;
using AuthAPI.Core.Entities;
using AuthAPI.Infrastructure.Context;
using AuthAPI.Infrastructure.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Infrastructure.Repositories;

internal class ReferenceTokenRepository : Repository<ReferenceTokenEntity, int>, IReferenceTokenRepository
{
    private readonly AuthDbContext _dbContext;

    public ReferenceTokenRepository(AuthDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task DeleteByAccountId(Guid acconutId, CancellationToken cancellationToken = default)
    {
        await _dbContext.ReferenceTokens
            .Where(x => x.AccountId == acconutId)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<ReferenceTokenEntity> GetTokenByValueAsync(string tokenValue, CancellationToken cancellationToken = default)
    {
        var token = await _dbContext.ReferenceTokens
            .Where(x => x.Token == tokenValue)
            .FirstAsync(cancellationToken);
        return token;
    }
}