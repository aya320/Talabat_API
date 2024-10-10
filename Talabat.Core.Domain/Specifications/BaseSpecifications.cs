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
		


		public BaseSpecifications()
        {
            //Criteria= null;
        }

        public BaseSpecifications(TKey Id)
        {
            Criteria = a=>a.Id.Equals(Id);
        }
    }
}
