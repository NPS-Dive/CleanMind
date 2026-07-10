using CleanMind.Application.Contracts.Persistence;
using CleanMind.Application.Contracts.Repositories;
using CleanMind.Application.Exceptions;
using CleanMind.Application.Utilities;

namespace CleanMind.Application.Features.Clinics.Commands.UpdateClinic;

public class UpdateClinicCommandHandler : IRequestHandler<UpdateClinicCommand>
    {
    private readonly IClinicRepository _clinicRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateClinicCommandHandler ( IClinicRepository clinicRepository, IUnitOfWork unitOfWork )
        {
        _clinicRepository = clinicRepository;
        _unitOfWork = unitOfWork;
        }

    public async Task HandleAsync ( UpdateClinicCommand request )
        {
        var clinic = await _clinicRepository.GetById(request.Id);

        if (clinic is null)
            {
            throw new NotFoundException();
            }

        clinic.UpdateName(request.Name);

        try
            {
            await _clinicRepository.Update(clinic);
            await _unitOfWork.CommitAsync();
            }
        catch (Exception ex)
            {
            await _unitOfWork.RollbackAsync();
            throw;
            }
        }
    }