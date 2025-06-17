using AuthAPI.Application.Commands.Account.Login;
using AuthAPI.Application.Mapping;
using AuthAPI.Application.PipelineBehavior;
using AuthAPI.Application.Validation.Validators.Account;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AuthAPI.Application;

public static class Injection
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        AddValidation(services);
        AddCommands(services);
        AddMapping(services);
        AddPipelineBehavior(services);
    }

    private static void AddValidation(IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<RegistrationValidator>();
    }

    private static void AddCommands(IServiceCollection services)
    {
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssemblyContaining<LoginCommandHandler>();
        });
    }

    private static void AddMapping(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AccountProfile));
    }

    private static void AddPipelineBehavior(IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
    }
}
