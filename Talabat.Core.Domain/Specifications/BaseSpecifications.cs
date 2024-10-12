using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Contracts;

namespace Talabat.Core.Domain.Specifications
{
	public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public Expression<Func<TEntity , bool>>? Criteria { get; set; }

		public List<Expression<Func<TEntity, object>>> Includes { get; set; } =  new /*List<Expression<Func<TEntity, object>>>*/();
		public Expression<Func<TEntity, object>>? OrderBy { get; set; }
		public Expression<Func<TEntity, object>>? OrderByDesc { get; set; }

		public BaseSpecifications()
        {
            //Criteria= null;
        }

        public BaseSpecifications(TKey Id)
        {
            Criteria = a=>a.Id.Equals(Id);
        }
		private protected virtual void AddIncludes()
		{

		}
		private protected virtual void AddOrderBy(Expression<Func<TEntity, object>> OrderByExpression)
		{
			OrderBy = OrderByExpression;
		}
		private protected virtual void AddOrderByDesc(Expression<Func<TEntity, object>> OrderByDescExpression)
		{
			OrderByDesc = OrderByDescExpression;
		}
	}
}
