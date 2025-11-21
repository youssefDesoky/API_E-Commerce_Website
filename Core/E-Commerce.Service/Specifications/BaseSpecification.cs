using System;
using System.Dynamic;
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

    public Expression<Func<TEntity, object>>? OrderBy { get; private set; }

    public Expression<Func<TEntity, object>>? OrderByDesc { get; private set; }

    public int Skip { get; private set; }

    public int Take { get; private set; }
    public bool IsPaginated { get; private set; }

    protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) => Includes.Add(includeExpression);

    protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression) => OrderBy = orderByExpression;

    protected void AddOrderByDesc(Expression<Func<TEntity, object>> orderByDescExpression) => OrderByDesc = orderByDescExpression;

    protected void ApplyPaging(int PageSize, int PageIndex)
    {
        IsPaginated = true;

        Take = PageSize;
        
        Skip = PageSize * (PageIndex - 1);
    }
}