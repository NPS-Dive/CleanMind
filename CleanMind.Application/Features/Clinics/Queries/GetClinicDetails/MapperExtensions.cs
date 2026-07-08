using CleanMind.Domain.Entities;

namespace CleanMind.Application.Features.Clinics.Queries.GetClinicDetails;

public static class MapperExtensions
{
    public static ClinicDetailsDto ToDto(this Clinic clinic)
    {
        var dto = new ClinicDetailsDto()
        {
            Id = clinic.Id,
            Name = clinic.Name
        };
        return dto;
    }
}