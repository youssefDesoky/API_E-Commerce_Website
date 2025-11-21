using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Persistence.Repositories;

public class Repository<TEntity, TKey>(StoreDbContext context) : IRepository<TEntity, TKey> where TEntity : Entity<TKey>
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public void Add(TEntity entity) => _dbSet.Add(entity);
    public void Update(TEntity entity) => _dbSet.Update(entity);
    public void Delete(TKey id) => _dbSet.Remove(_dbSet.Find(id));
    public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();
    public async Task<TEntity?> GetByIdAsync(TKey id) => await _dbSet.FindAsync(id);

    public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification)
        => await _dbSet.ApplySpecification(specification).ToListAsync();

    public async Task<TEntity?> GetByIdAsync(ISpecification<TEntity> specification)
        => await _dbSet.ApplySpecification(specification).FirstOrDefaultAsync();

    public async Task<int> CountAsync(ISpecification<TEntity> specification)
        => await _dbSet.ApplySpecification(specification).CountAsync();
}
