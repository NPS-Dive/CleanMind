using CleanMind.Application.Features.Clinics.Commands.CreateClinic;
using CleanMind.Application.Features.Clinics.Commands.UpdateClinic;
using CleanMind.Application.Features.Clinics.Queries.GetClinicDetails;
using CleanMind.Application.Features.Clinics.Queries.GetClinicsLIst;
using CleanMind.Application.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace CleanMind.Application;

public static class RegisterApplicationServices
    {
    public static IServiceCollection AddApplicationServices ( this IServiceCollection services )
        {
        services.AddTransient<IMediator, SimpleMediator>();
        services.AddScoped<IRequestHandler<CreateClinicCommand, Guid>, CreateClinicCommandHandler>();
        services.AddScoped<IRequestHandler<GetClinicDetailsQuery, ClinicDetailsDto>, GetClinicDetailsQueryHandler>();
        services.AddScoped<IRequestHandler<GetClinicsListQuery, List<ClinicsListDto>>, GetClinicsListQueryHandler>();
        services.AddScoped<IRequestHandler<UpdateClinicCommand>, UpdateClinicCommandHandler>();

        return services;
        }
    }