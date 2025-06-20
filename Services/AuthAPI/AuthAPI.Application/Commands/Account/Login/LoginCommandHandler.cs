using AuthAPI.Application.Contracts.PasswordHasher;
using AuthAPI.Application.Contracts.Repository.Account;
using AuthAPI.Application.Contracts.Repository.Token;
using AuthAPI.Application.Contracts.TokenProvider;
using AuthAPI.Application.Contracts.UnitOfWork;
using AuthAPI.Application.Exceptions;
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

    public LoginCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ITokenProvider tokenProvider,
        IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _tokenProvider = tokenProvider;
        _passwordHasher = passwordHasher;
    }

    public async Task<LoginResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var accountRepository = _unitOfWork.GetRepository<IAccountRepository>();
        var referenceTokenRepository = _unitOfWork.GetRepository<IReferenceTokenRepository>();

        var accountEntity = _mapper.Map<AccountEntity>(command.Request);
        var isExists = await accountRepository.IsExistsAsync(accountEntity.Email, cancellationToken);

        if (!isExists)
        {
            return new LoginResponse(false, null);
        }

        var existingAccount = await accountRepository.GetByEmailAsync(accountEntity.Email, cancellationToken);
        var isVerified = _passwordHasher.VerifyPassword(command.Request.Password, existingAccount.HashPassword);

        if (!isVerified)
        {
            return new LoginResponse(false, null);
        }

        var referenceToken = _tokenProvider.GenerateReferenceToken(existingAccount);
        await referenceTokenRepository.CreateAsync(referenceToken, cancellationToken);
        return new LoginResponse(true,referenceToken.Token);
    }
}
