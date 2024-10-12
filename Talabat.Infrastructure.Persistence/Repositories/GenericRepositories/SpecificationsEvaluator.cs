using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Common;
using Talabat.Core.Domain.Contracts;

namespace Talabat.Infrastructure.Persistence.Repositories.GenericRepositories
{
    internal static class SpecificationsEvaluator<TEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{

		public static IQueryable<TEntity> GetQuery (IQueryable<TEntity> inputquery , ISpecifications<TEntity,TKey> spec )
		{
			var query = inputquery;
			if ( spec.Criteria is not null ) 
			query=query.Where(spec.Criteria);

			if(spec.OrderByDesc is not null)
				query=query.OrderByDescending(spec.OrderByDesc);
			else if (spec.OrderBy is not null)
				query=query.OrderBy(spec.OrderBy);

			query = spec.Includes.Aggregate(query,(currentquery,includesexpression)=>currentquery.Include(includesexpression) );
			return query;
		}
	}
}
