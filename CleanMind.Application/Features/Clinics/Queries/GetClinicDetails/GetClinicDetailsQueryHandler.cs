using CleanMind.Application.Contracts.Repositories;
using CleanMind.Application.Exceptions;
using CleanMind.Application.Utilities;

namespace CleanMind.Application.Features.Clinics.Queries.GetClinicDetails;

public class GetClinicDetailsQueryHandler : IRequestHandler<GetClinicDetailsQuery, ClinicDetailsDto>
{
    private readonly IClinicRepository _repository;

    public GetClinicDetailsQueryHandler ( IClinicRepository repository )
    {
        _repository = repository;
    }

    public async Task<ClinicDetailsDto> HandleAsync ( GetClinicDetailsQuery request )
    {
        var clinic = await _repository.GetById(request.Id);

        if (clinic is null)
        {
            throw new NotFoundException();
        }

        return clinic.ToDto();
    }
}