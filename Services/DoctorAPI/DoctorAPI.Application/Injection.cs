using DoctorAPI.Application.Mappings;
using DoctorAPI.Application.Validation.Validators.Doctor;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace DoctorAPI.Application;

public static class Injection 
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        AddValidation(services);
        AddCommandsAndQueries(services);
        AddMapping(services);
    }
    private static void AddValidation(IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreateDoctorRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateDoctorRequestValidator>();
    }

    private static void AddCommandsAndQueries(IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options
           .RegisterServicesFromAssemblies(
               typeof(Commands.Doctor
               .Create.CreateDoctorCommandHandler)
               .Assembly);

            options
            .RegisterServicesFromAssemblies(
                typeof(Commands.Doctor
                .Update.UpdateDoctorCommandHandler)
                .Assembly);

            options
           .RegisterServicesFromAssemblies(
               typeof(Commands.Doctor
               .Delete.DeleteDoctorCommandHandler)
               .Assembly);

            options
           .RegisterServicesFromAssemblies(
               typeof(Queries.Doctor
               .GetAll.GetAllDoctorsQueryHandler)
               .Assembly);

            options
           .RegisterServicesFromAssemblies(
               typeof(Queries.Doctor
               .GetById.GetByIdDoctorQueryHandler)
               .Assembly);

            options
           .RegisterServicesFromAssemblies(
               typeof(Queries.Doctor
               .GetBySpecialization.GetBySpecializationDoctorQueryHandler)
               .Assembly);

            options
           .RegisterServicesFromAssemblies(
               typeof(Queries.Doctor
               .GetByStatus.GetByStatusDoctorsQueryHandler)
               .Assembly);
        });
    }

    private static void AddMapping(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DoctorProfile));
    }
}
