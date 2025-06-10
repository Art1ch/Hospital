using DoctorAPI.Application.Contracts;
using DoctorAPI.Application.Mappings;
using DoctorAPI.Application.Validator.Doctor;
using DoctorAPI.Infrastructure.Context;
using DoctorAPI.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DoctorAPI.Extensions;

public static class WebApplicationBuilderExtension
{
    public static WebApplicationBuilder AddDbContext(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<DoctorDbContext>(opt =>
            opt.UseNpgsql(builder.Configuration.GetConnectionString("DoctorDbString")));
        return builder;
    }

    public static WebApplicationBuilder AddRepositories(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IDoctorRepository,
            DoctorRepository>();

        return builder;
    }

    public static WebApplicationBuilder AddMapping(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(DoctorProfile));

        return builder;
    }

    public static WebApplicationBuilder AddValidation(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssembly(
            typeof(DoctorEntityValidator).Assembly);

        return builder;
    }

    public static WebApplicationBuilder AddCommandsAndQueries(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(options =>
        {
            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Commands.Doctor
               .Create.CreateDoctorCommandHandler)
               .Assembly);

            options
            .RegisterServicesFromAssemblies(
                typeof(Application.Commands.Doctor
                .Update.UpdateDoctorCommandHandler)
                .Assembly);

            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Commands.Doctor
               .Delete.DeleteDoctorCommandHandler)
               .Assembly);

            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Queries.Doctor
               .GetAll.GetAllDoctorsQueryHandler)
               .Assembly);

            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Queries.Doctor
               .GetById.GetByIdDoctorQueryHandler)
               .Assembly);

            options
           .RegisterServicesFromAssemblies(
               typeof(Application.Queries.Doctor
               .GetBySpecialization.GetBySpecializationDoctorQueryHandler)
               .Assembly);

           options
          .RegisterServicesFromAssemblies(
              typeof(Application.Queries.Doctor
              .GetByStatus.GetByStatusDoctorsQueryHandler)
              .Assembly);
        });

        return builder;
    }
}
