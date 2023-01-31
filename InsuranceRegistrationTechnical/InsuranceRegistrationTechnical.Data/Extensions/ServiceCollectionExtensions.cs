using InsuranceRegistrationTechnical.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InsuranceRegistrationTechnical.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {
        return services.AddDbContext<RegistrationDatabaseContext>();
    }

    public static IServiceProvider RunDatabaseMigrations(this IServiceProvider services)
    {
        using (var scope = services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<RegistrationDatabaseContext>();
            db.Database.Migrate();
        }
        return services;
    }
}

