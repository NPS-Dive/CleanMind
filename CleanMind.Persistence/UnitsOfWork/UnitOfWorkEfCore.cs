using CleanMind.Application.Contracts.Persistence;

namespace CleanMind.Persistence.UnitsOfWork;

public class UnitOfWorkEfCore : IUnitOfWork
    {
    private readonly CleanMindDbContext _dbContext;

    public UnitOfWorkEfCore ( CleanMindDbContext dbContext )
        {
        _dbContext = dbContext;
        }
    public async Task CommitAsync ()
        {
        var result = _dbContext.SaveChangesAsync();
        await result;
        }

    public Task RollbackAsync ()
        {
        var result = Task.CompletedTask;
        return result;
        }
    }