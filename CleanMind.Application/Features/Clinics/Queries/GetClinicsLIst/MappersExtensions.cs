using CleanMind.Domain.Entities;

namespace CleanMind.Application.Features.Clinics.Queries.GetClinicsLIst;

public static class MappersExtensions
{
    public static ClinicsListDto ToDto(this Clinic clinic)
    {
        var dto = new ClinicsListDto()
        {
            Id = clinic.Id,
            Name = clinic.Name
        };

        return dto;
    }
}