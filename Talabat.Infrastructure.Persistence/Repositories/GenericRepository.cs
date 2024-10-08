using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Common;
using Talabat.Core.Domain.Contracts;
using Talabat.Infrastructure.Persistence.Data;

namespace Talabat.Infrastructure.Persistence.Repositories
{
	public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseAuditableEntity<TKey> where TKey : IEquatable<TKey>
	{
        
		private readonly StoreContext _storecontext;

		public GenericRepository(StoreContext storecontext)
		{
			_storecontext = storecontext;
		}

		public async Task AddAsync(TEntity Entity)=>await _storecontext.Set<TEntity>().AddAsync(Entity);
		
		public  void Delete(TEntity Entity) => _storecontext.Set<TEntity>().Remove(Entity);
		

		public async Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false)=>WithTracking ? await _storecontext.Set<TEntity>().ToListAsync() : await _storecontext.Set<TEntity>().AsNoTracking().ToListAsync();
		//{
		//	if (WithTracking) return await _storecontext.Set<TEntity>().ToListAsync();
		//	else return await _storecontext.Set<TEntity>().AsNoTracking().ToListAsync();
		//}

		public async Task<TEntity?> GetAsync(TKey Id)=> await _storecontext.Set<TEntity>().FindAsync(Id);
			
		public void Update(TEntity Entity) => _storecontext.Set<TEntity>().Update(Entity);
		
	}

}
