using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Domain.Contracts
{
	public interface IGenericRepository<TEntity,TKey> where TEntity : BaseAuditableEntity<TKey> where TKey : IEquatable<TKey>
	{
		Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking=false);
		Task<TEntity?> GetAsync(TKey Id);
		Task AddAsync(TEntity Entity);
		void Update(TEntity Entity);
		void Delete(TEntity Entity);

	}
}
