using AuthAPI.Application.Contracts.PasswordHasher;
using AuthAPI.Application.Contracts.Repository.Account;
using AuthAPI.Application.Contracts.Repository.Token;
using AuthAPI.Application.Contracts.TokenProvider;
using AuthAPI.Application.Contracts.UnitOfWork;
using AuthAPI.Application.Responses.Account;
using AuthAPI.Core.Entities;
using AutoMapper;
using MediatR;

namespace AuthAPI.Application.Commands.Account.Login;

internal sealed class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITokenProvider _tokenProvider;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAccountRepository _accountRepository;
    private readonly IReferenceTokenRepository _referenceTokenRepository;

    public LoginCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ITokenProvider tokenProvider,
        IPasswordHasher passwordHasher,
        IAccountRepository accountRepository,
        IReferenceTokenRepository referenceTokenRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _tokenProvider = tokenProvider;
        _passwordHasher = passwordHasher;
        _accountRepository = accountRepository;
        _referenceTokenRepository = referenceTokenRepository;
    }

    public async Task<LoginResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var accountEntity = _mapper.Map<AccountEntity>(command.Request);
        var isExists = await _accountRepository.IsExistsAsync(accountEntity.Email, cancellationToken);

        if (!isExists)
        {
            return new LoginResponse(false, null, "Account doesn't exists");
        }

        var existingAccount = await _accountRepository.GetByEmailAsync(accountEntity.Email, cancellationToken);
        var isVerified = _passwordHasher.VerifyPassword(command.Request.Password, existingAccount.HashPassword);

        if (!isVerified)
        {
            return new LoginResponse(false, null, "Wrong credentials");
        }

        var referenceToken = _tokenProvider.GenerateReferenceToken(existingAccount);
        await _referenceTokenRepository.CreateAsync(referenceToken, cancellationToken);
        return new LoginResponse(true, referenceToken.Token, null);
    }
}
