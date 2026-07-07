using CleanMind.Application.Utilities;

namespace CleanMind.Application.Features.Clinics.Commands.CreateClinic;

public class CreateClinicCommand : IRequest<Guid>
    {
    public required string Name { get; set; }
    }