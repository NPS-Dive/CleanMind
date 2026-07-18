using CleanMind.Application.Contracts.Persistence;
using CleanMind.Application.Contracts.Repositories;
using CleanMind.Application.Exceptions;
using CleanMind.Application.Utilities;

namespace CleanMind.Application.Features.Clinics.Commands.DeleteClinic;

public class DeleteClinicCommandHandler : IRequestHandler<DeleteClinicCommand>
    {
    private readonly IClinicRepository _clinicRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteClinicCommandHandler ( IClinicRepository clinicRepository, IUnitOfWork unitOfWork )
        {
        _clinicRepository = clinicRepository;
        _unitOfWork = unitOfWork;
        }


    public async Task HandleAsync ( DeleteClinicCommand request )
        {
        var clinic = await _clinicRepository.GetById(request.Id);

        if (clinic is null)
            {
            throw new NotFoundException();
            }

        try
            {
            await _clinicRepository.Delete(clinic);
            await _unitOfWork.CommitAsync();
            }
        catch (Exception e)
            {
            await _unitOfWork.RollbackAsync();
            throw;
            }
        }
    }