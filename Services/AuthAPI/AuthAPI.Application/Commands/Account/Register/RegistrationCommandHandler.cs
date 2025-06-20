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

namespace AuthAPI.Application.Commands.Account.Register;

internal sealed class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, RegistrationResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenProvider _tokenProvider;
    private readonly IMapper _mapper;

    public RegistrationCommandHandler(
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        ITokenProvider tokenProvider,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _tokenProvider = tokenProvider;
        _mapper = mapper;
    }

    public async Task<RegistrationResponse> Handle(RegistrationCommand command, CancellationToken cancellationToken)
    {
        var accountRepository = _unitOfWork.GetRepository<IAccountRepository>();
        var referenceTokenRepository = _unitOfWork.GetRepository<IReferenceTokenRepository>();

        var accountEntity = _mapper.Map<AccountEntity>(command.Request);
        var isExists = await accountRepository.IsExistsAsync(accountEntity.Email, cancellationToken);

        if (isExists)
        {
            throw new AccountAlreadyExistsException("Account already exists");
        }

        var hashPassword = _passwordHasher.GeneratePassword(command.Request.Password!);
        accountEntity.HashPassword = hashPassword;

        await accountRepository.CreateAsync(accountEntity, cancellationToken);
        var referenceToken = _tokenProvider.GenerateReferenceToken(accountEntity);
        await referenceTokenRepository.CreateAsync(referenceToken, cancellationToken);

        return new RegistrationResponse(referenceToken.Token);
    }
}