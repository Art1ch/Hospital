using AuthAPI.Core.Entities;

namespace AuthAPI.Application.Contracts.Repository.Account;

public interface IAccountRepository : IRepository<AccountEntity, Guid>
{
    Task<bool> IsExistsAsync(string email, CancellationToken cancellationToken = default);
    Task<AccountEntity> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
