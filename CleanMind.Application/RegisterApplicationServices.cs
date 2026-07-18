using CleanMind.Application.Features.Clinics.Commands.CreateClinic;
using CleanMind.Application.Features.Clinics.Commands.DeleteClinic;
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

        //scrutor usage
        services.Scan(scan => scan.FromAssembliesOf(typeof(RegisterApplicationServices))
            .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            .AddClasses(c=>c.AssignableTo(typeof(IRequestHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        //services.AddScoped<IRequestHandler<CreateClinicCommand, Guid>, CreateClinicCommandHandler>();
        //services.AddScoped<IRequestHandler<GetClinicDetailsQuery, ClinicDetailsDto>, GetClinicDetailsQueryHandler>();
        //services.AddScoped<IRequestHandler<GetClinicsListQuery, List<ClinicsListDto>>, GetClinicsListQueryHandler>();
        //services.AddScoped<IRequestHandler<UpdateClinicCommand>, UpdateClinicCommandHandler>();
        //services.AddScoped<IRequestHandler<DeleteClinicCommand>, DeleteClinicCommandHandler>();


        return services;
        }
    }