using CleanMind.Application.Contracts.Repositories;
using CleanMind.Application.Utilities;

namespace CleanMind.Application.Features.Clinics.Queries.GetClinicsLIst;

public class GetClinicsListQueryHandler : IRequestHandler<GetClinicsListQuery, List<ClinicsListDto>>
    {
        private readonly IClinicRepository _repository;

        public GetClinicsListQueryHandler(IClinicRepository repository)
        {
            _repository = repository;
        }
    public async Task<List<ClinicsListDto>> HandleAsync ( GetClinicsListQuery request )
    {
        var clinics = await _repository.GetAll();
        var clinicsDto = clinics.Select(clinic => clinic.ToDto()).ToList();

        return clinicsDto;
    }
    }