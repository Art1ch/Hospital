using DoctorAPI.Application.Commands.Doctor.Create;
using DoctorAPI.Application.Mappings;
using DoctorAPI.Application.PipelineBehavior;
using DoctorAPI.Application.Validation.Validators.Doctor;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DoctorAPI.Application;

public static class Injection 
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        AddValidation(services);
        AddCommandsAndQueries(services);
        AddMapping(services);
        AddPipelineBehavior(services);
    }
    private static void AddValidation(IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreateDoctorRequestValidator>();
    }

    private static void AddCommandsAndQueries(IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining<CreateDoctorCommandHandler>();
        });
    }

    private static void AddMapping(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DoctorProfile));
    }

    private static void AddPipelineBehavior(IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
    }
}
