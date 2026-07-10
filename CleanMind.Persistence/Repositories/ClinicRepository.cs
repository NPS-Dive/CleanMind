using CleanMind.Application.Contracts.Repositories;
using CleanMind.Domain.Entities;

namespace CleanMind.Persistence.Repositories;

public class ClinicRepository : Repository<Clinic>, IClinicRepository
    {
    public ClinicRepository ( CleanMindDbContext dbContext )
    : base(dbContext)
        {

        }
    }