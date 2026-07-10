using FluentValidation;

namespace CleanMind.Application.Features.Clinics.Commands.UpdateClinic;

public class UpdateClinicCommandValidator : AbstractValidator<UpdateClinicCommand>
    {
    public UpdateClinicCommandValidator ()
        {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("The Field {PropertyName} cannot be empty!");
        }
    }