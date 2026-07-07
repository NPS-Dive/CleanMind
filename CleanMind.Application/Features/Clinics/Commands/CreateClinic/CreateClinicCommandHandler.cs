using CleanMind.Application.Contracts.Persistence;
using CleanMind.Application.Contracts.Repositories;
using CleanMind.Application.Exceptions;
using CleanMind.Application.Utilities;
using CleanMind.Domain.Entities;
using FluentValidation;

namespace CleanMind.Application.Features.Clinics.Commands.CreateClinic;

public class CreateClinicCommandHandler :  IRequestHandler<CreateClinicCommand, Guid>
    {
    
    private readonly IClinicRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    public IValidator<CreateClinicCommand> _validator;

    public CreateClinicCommandHandler ( IClinicRepository repository, IUnitOfWork unitOfWork,
        IValidator<CreateClinicCommand> validator )
        {
        _validator = validator;
        _repository = repository;
        _unitOfWork = unitOfWork;
        }
    public async Task<Guid> HandleAsync ( CreateClinicCommand command )
        {
        var validationResult = await _validator.ValidateAsync(command);

        if (!validationResult.IsValid)
            {
            throw new CustomValidationException(validationResult);
            }

        var clinic = new Clinic(command.Name);
        try
            {
            var result = await _repository.CreateAsync(clinic);
            await _unitOfWork.CommitAsync();
            return result.Id;
            }
        catch (Exception)
            {
            await _unitOfWork.RollbackAsync();
            throw;
            }

        }
    }