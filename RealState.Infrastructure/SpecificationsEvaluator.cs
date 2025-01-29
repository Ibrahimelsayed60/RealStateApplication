using Microsoft.EntityFrameworkCore;
using RealState.Domain.Entities;
using RealState.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Infrastructure
{
    public static class SpecificationsEvaluator<TEntity> where TEntity : BaseModel
    {

        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery , ISpecifications<TEntity> spec)
        {

            var query = inputQuery;

            if(spec.Criteria is null)
                query = query.Where(spec.Criteria);

            query = spec.Includes.Aggregate(query, (currentQuery, IncludeExpressions) => currentQuery.Include(IncludeExpressions));

            return query;

        }

    }
}
