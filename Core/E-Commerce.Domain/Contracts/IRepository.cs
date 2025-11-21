using System;
using E_Commerce.Domain.Entities;

namespace E_Commerce.Domain.Contracts;

public interface IRepository<TEntity, TKey> where TEntity : Entity<TKey>
{
    // make Write operations synchronous
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TKey id);

    // Make Read operations asynchronous
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(TKey id);

    // Specification-based methods
    Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification);
    Task<TEntity?> GetByIdAsync(ISpecification<TEntity> specification);

    Task<int> CountAsync(ISpecification<TEntity> specification);
}
