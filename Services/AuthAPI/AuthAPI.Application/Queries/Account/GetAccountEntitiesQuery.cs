using AuthAPI.Core.Entities;
using MediatR;

namespace AuthAPI.Application.Queries.Account;

public sealed record GetAccountEntitiesQuery : IRequest<IQueryable<AccountEntity>>;
