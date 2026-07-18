using CleanMind.Application.Utilities;

namespace CleanMind.Application.Features.Clinics.Commands.DeleteClinic;

public class DeleteClinicCommand : IRequest
    {
    public required Guid Id { get; set; }
    }