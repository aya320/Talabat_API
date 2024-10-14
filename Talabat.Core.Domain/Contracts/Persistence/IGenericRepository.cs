using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Domain.Contracts.Persistence
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false);
		Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity,TKey> spec , bool WithTracking = false);

		Task<TEntity?> GetAsync(TKey Id);
		Task<TEntity?> GetWithSpecAsync(ISpecifications<TEntity, TKey> spec);
        Task<int> GetCountAsync(ISpecifications<TEntity,TKey> spec);

		Task AddAsync(TEntity Entity);
        void Update(TEntity Entity);
        void Delete(TEntity Entity);

    }
}
