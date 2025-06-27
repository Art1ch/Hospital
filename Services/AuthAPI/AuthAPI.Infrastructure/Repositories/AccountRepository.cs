using AuthAPI.Application.Contracts.Repository.Account;
using AuthAPI.Core.Entities;
using AuthAPI.Infrastructure.Context;
using AuthAPI.Infrastructure.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Infrastructure.Repositories;

internal class AccountRepository : Repository<AccountEntity, Guid>, IAccountRepository
{
    private readonly AuthDbContext _dbContext;

    public AccountRepository(AuthDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AccountEntity> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var account = await _dbContext.Accounts
            .AsNoTracking()
            .FirstAsync(x => x.Email == email, cancellationToken);
        return account;
    }

    public async Task<bool> IsExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        var isExists = await _dbContext.Accounts
            .AnyAsync(x => x.Email == email, cancellationToken);
        return isExists;
    }
}
