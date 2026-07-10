using CleanMind.Application.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanMind.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly CleanMindDbContext _dbContext;

    public Repository ( CleanMindDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task<T?> GetById ( Guid id )
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAll ()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<T> CreateAsync ( T entity )
    {
        await _dbContext.AddAsync(entity);
        return entity;
    }

    public Task<T> Update ( T entity )
    {
        _dbContext.Update(entity);
        // Task.FromResult creates a completed generic task holding your entity
        return Task.FromResult(entity);
    }

    public Task<T> Delete ( T entity )
    {
        _dbContext.Remove(entity);
        // Same here, properly wrap the entity in a generic Task
        return Task.FromResult(entity);
    }
}