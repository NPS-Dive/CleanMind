using CleanMind.Application.Utilities;

namespace CleanMind.Application.Features.Clinics.Queries.GetClinicDetails;

public class GetClinicDetailsQuery : IRequest<ClinicDetailsDto>
    {
    public required Guid Id { get; set; }
    }