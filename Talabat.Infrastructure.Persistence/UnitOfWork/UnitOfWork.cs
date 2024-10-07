using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Contracts;
using Talabat.Core.Domain.Entities.Products;
using Talabat.Infrastructure.Persistence.Data;
using Talabat.Infrastructure.Persistence.Repositories;
using Talabat.Core.Domain.Common;

namespace Talabat.Infrastructure.Persistence.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly StoreContext _dbContext;
		public UnitOfWork(StoreContext dbContext)
		{
			_dbContext = dbContext;

		}

		public async Task<int> CompleteAsync()=>await _dbContext.SaveChangesAsync();

		public async ValueTask DisposeAsync()=> await _dbContext.DisposeAsync();

		public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
			where TEntity : BaseEntity<TKey>
			where TKey : IEquatable<TKey>
		{
			throw new NotImplementedException();
		}
	}
}
