using DoctorAPI.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DoctorAPI.Extensions;

public static class WebApplicationBuilderExtension
{
    public static WebApplicationBuilder AddDbContext<TId1, TId2>(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<DoctorDbContext<TId1, TId2>>(opt =>
            opt.UseNpgsql(builder.Configuration.GetConnectionString("DoctorDbString")));
        return builder;
    }

    
}
