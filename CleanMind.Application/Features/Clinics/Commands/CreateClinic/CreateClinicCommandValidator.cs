using FluentValidation;

namespace CleanMind.Application.Features.Clinics.Commands.CreateClinic;

public class CreateClinicCommandValidator : AbstractValidator<CreateClinicCommand>
    {
    public CreateClinicCommandValidator ()
        {
        RuleFor(p => p.Name).NotEmpty().WithMessage("The Field {PropertyName} is required!");
        }
    }