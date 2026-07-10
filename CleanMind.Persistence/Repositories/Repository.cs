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
        var result = _dbContext.Set<T>().FindAsync(id);

        return await result;
        }

    public async Task<IEnumerable<T>> GetAll ()
        {
        var result = _dbContext.Set<T>().ToListAsync();

        return await result;
        }

    public async Task<T> CreateAsync ( T entity )
        {
        _dbContext.AddAsync(entity);

        var result = Task.FromResult(entity);

        return await result;
        }

    public async Task<T> Update ( T entity )
        {
        _dbContext.Update(entity);

        var result = Task.CompletedTask;

        return await (Task<T>)result;
        }

    public Task<T> Delete ( T entity )
        {
        _dbContext.Remove(entity);

        var result = Task.CompletedTask;

        return (Task<T>)result;
        }
    }