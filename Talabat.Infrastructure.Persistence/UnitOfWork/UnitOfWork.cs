using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Entities.Products;
using Talabat.Infrastructure.Persistence.Data;
using Talabat.Core.Domain.Common;
using System.Collections.Concurrent;
using Talabat.Core.Domain.Contracts.Persistence;
using Talabat.Infrastructure.Persistence.GenericRepositories;

namespace Talabat.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
	{
		private readonly StoreContext _dbContext;
		private readonly ConcurrentDictionary<string, object> _Repositories;
		public UnitOfWork(StoreContext dbContext)
		{
			_dbContext = dbContext;
			_Repositories = new ConcurrentDictionary<string, object>();

		}

		public async Task<int> CompleteAsync()=>await _dbContext.SaveChangesAsync();

		public async ValueTask DisposeAsync()=> await _dbContext.DisposeAsync();

		public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
			where TEntity : BaseEntity<TKey>
			where TKey : IEquatable<TKey>
		{
			//var TypeName= typeof(TEntity).Name;
			//if (_Repositories.ContainsKey(TypeName)) return (IGenericRepository<TEntity, TKey>) _Repositories[TypeName];

			//var repository = new GenericRepository<TEntity, TKey>(_dbContext);
			//_Repositories.Add(TypeName, repository);
			//return repository;

			return (IGenericRepository<TEntity, TKey>) _Repositories.GetOrAdd(typeof(TEntity).Name, new  GenericRepository<TEntity, TKey>(_dbContext));
		}
	}
}
