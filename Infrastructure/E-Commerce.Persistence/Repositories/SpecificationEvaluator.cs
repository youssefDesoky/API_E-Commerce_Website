using System;
using E_Commerce.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Persistence.Repositories;

public static class SpecificationEvaluator
{
    public static IQueryable<TEntity> ApplySpecification<TEntity>(this IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec) where TEntity : class
    {
        var query = inputQuery;

        if (spec.Criteria is not null)
        {
            query = query.Where(spec.Criteria);
        }

        query = spec.Includes.Aggregate(query, (current, expression) => current.Include(expression));

        return query;
    }
}
