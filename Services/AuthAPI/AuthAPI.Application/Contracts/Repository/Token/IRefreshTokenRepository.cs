using AuthAPI.Core.Entities;

namespace AuthAPI.Application.Contracts.Repository.Account;

public interface IRefreshTokenRepository : IRepository<RefreshTokenEntity, int>
{

}
