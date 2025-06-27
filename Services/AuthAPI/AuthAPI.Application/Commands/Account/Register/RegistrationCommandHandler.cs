using AuthAPI.Application.Contracts.PasswordHasher;
using AuthAPI.Application.Contracts.Repository.Account;
using AuthAPI.Application.Contracts.Repository.Token;
using AuthAPI.Application.Contracts.TokenProvider;
using AuthAPI.Application.Contracts.UnitOfWork;
using AuthAPI.Application.Responses.Account;
using AuthAPI.Core.Entities;
using AutoMapper;
using MediatR;

namespace AuthAPI.Application.Commands.Account.Register;

internal sealed class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, RegistrationResponse>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenProvider _tokenProvider;
    private readonly IMapper _mapper;
    private readonly IAccountRepository _accountRepository;
    private readonly IReferenceTokenRepository _referenceTokenRepository;

    public RegistrationCommandHandler(
        IPasswordHasher passwordHasher,
        ITokenProvider tokenProvider,
        IMapper mapper,
        IAccountRepository accountRepository,
        IReferenceTokenRepository referenceTokenRepository)
    {
        _passwordHasher = passwordHasher;
        _tokenProvider = tokenProvider;
        _mapper = mapper;
        _accountRepository = accountRepository;
        _referenceTokenRepository = referenceTokenRepository;
    }

    public async Task<RegistrationResponse> Handle(RegistrationCommand command, CancellationToken cancellationToken)
    {
        var accountEntity = _mapper.Map<AccountEntity>(command.Request);
        var isExists = await _accountRepository.IsExistsAsync(accountEntity.Email, cancellationToken);

        if (isExists)
        {
            return new RegistrationResponse(false, null, "Account already exists");
        }

        var hashPassword = _passwordHasher.GeneratePasswordHash(command.Request.Password!);
        accountEntity.HashPassword = hashPassword;

        await _accountRepository.CreateAsync(accountEntity, cancellationToken);
        var referenceToken = _tokenProvider.GenerateReferenceToken(accountEntity);
        await _referenceTokenRepository.CreateAsync(referenceToken, cancellationToken);

        return new RegistrationResponse(true, referenceToken.Token, null);
    }
}