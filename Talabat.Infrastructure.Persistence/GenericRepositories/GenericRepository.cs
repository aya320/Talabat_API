using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Common;
using Talabat.Core.Domain.Contracts;
using Talabat.Core.Domain.Contracts.Persistence;
using Talabat.Core.Domain.Entities.Products;
using Talabat.Infrastructure.Persistence.Data;

namespace Talabat.Infrastructure.Persistence.GenericRepositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {

        private readonly StoreContext _storecontext;

        public GenericRepository(StoreContext storecontext)
        {
            _storecontext = storecontext;
        }

        public async Task AddAsync(TEntity Entity) => await _storecontext.Set<TEntity>().AddAsync(Entity);

        public void Delete(TEntity Entity) => _storecontext.Set<TEntity>().Remove(Entity);


        public async Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false)
        {

            //if (typeof(TEntity) == typeof(Product))

            //    return WithTracking ?
            //        (IEnumerable<TEntity>)await _storecontext.Set<Product>().Include(P => P.Brand).Include(c => c.Category).ToListAsync() :
            //        (IEnumerable<TEntity>)await _storecontext.Set<Product>().Include(P => P.Brand).Include(c => c.Category).AsNoTracking().ToListAsync();

            return WithTracking ?
            await _storecontext.Set<TEntity>().ToListAsync() :
            await _storecontext.Set<TEntity>().AsNoTracking().ToListAsync();


            //{
            //	if (WithTracking) return await _storecontext.Set<TEntity>().ToListAsync();
            //	else return await _storecontext.Set<TEntity>().AsNoTracking().ToListAsync();
            //}


        }

        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> spec, bool WithTracking = false)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }

        public async Task<TEntity?> GetAsync(TKey Id)
        {
            //if (typeof(TEntity) == typeof(Product))
            //    return await _storecontext.Set<Product>().Where(a => a.Id.Equals(Id)).Include(P => P.Brand).Include(c => c.Category).FirstOrDefaultAsync() as TEntity;
            return await _storecontext.Set<TEntity>().FindAsync(Id);

        }

        public async Task<int> GetCountAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await ApplySpecifications(spec).CountAsync();

        }

        public async Task<TEntity?> GetWithSpecAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();

        }

        //=> await _storecontext.Set<TEntity>().FindAsync(Id);

        public void Update(TEntity Entity) => _storecontext.Set<TEntity>().Update(Entity);

        private IQueryable<TEntity> ApplySpecifications(ISpecifications<TEntity, TKey> spec)
        {
            return SpecificationsEvaluator<TEntity, TKey>.GetQuery(_storecontext.Set<TEntity>(), spec);

        }
    }

}
