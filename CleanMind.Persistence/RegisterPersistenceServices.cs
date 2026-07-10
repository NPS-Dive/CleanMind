using CleanMind.Application.Contracts.Persistence;
using CleanMind.Application.Contracts.Repositories;
using CleanMind.Persistence.Repositories;
using CleanMind.Persistence.UnitsOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanMind.Persistence;

public static class RegisterPersistenceServices
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<CleanMindDbContext>(options =>
        {
            options.UseSqlServer("name=CleanMindConnectionString");
        });

        services.AddScoped<IClinicRepository, ClinicRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWorkEfCore>();

        return services;
    }
}