namespace CleanMind.Application.Contracts.Repositories;

public interface IRepository<T> where T : class
    {
    Task<T?> GetById ( Guid id );
    Task<IEnumerable<T>> GetAll ();
     Task<T> CreateAsync ( T entity );
    Task<T> Update ( T entity );
    Task<T> Delete ( T entity );
    }