using System;
using System.Linq.Expressions;
using E_Commerce.Domain.Contracts;

namespace E_Commerce.Service.Specifications;

public abstract class BaseSpecification<TEntity> : ISpecification<TEntity> where TEntity : class
{
    protected BaseSpecification(Expression<Func<TEntity, bool>>? criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

    public ICollection<Expression<Func<TEntity, object>>> Includes { get; private set; } = new List<Expression<Func<TEntity, object>>>();
    
    protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) => Includes.Add(includeExpression);
}
