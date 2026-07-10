using CleanMind.Application.Utilities;

namespace CleanMind.Application.Features.Clinics.Commands.UpdateClinic;

public class UpdateClinicCommand : IRequest
    {
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    }