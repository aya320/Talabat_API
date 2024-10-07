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

namespace Talabat.Infrastructure.Persistence.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{

		private readonly StoreContext _dbContext;

        private readonly Lazy<IGenericRepository<Product, int>> _productRepository;

		private readonly Lazy<IGenericRepository<ProductBrand, int>> _brandRepository;

		private readonly Lazy<IGenericRepository <ProductCategory, int>> _categoryRepository;

		public UnitOfWork(StoreContext dbContext)
		{
			_dbContext = dbContext;

			_productRepository = new Lazy<IGenericRepository<Product, int>>(() => new GenericRepository<Product, int>(_dbContext));
			_brandRepository = new Lazy<IGenericRepository<ProductBrand, int>>(() => new GenericRepository<ProductBrand, int>(_dbContext));
			_categoryRepository = new Lazy<IGenericRepository<ProductCategory, int>>(() => new GenericRepository<ProductCategory, int>(_dbContext));

		}


        public IGenericRepository<Product, int> Products => _productRepository.Value;
		public IGenericRepository<ProductBrand, int> ProductBrands => _brandRepository.Value;
		public IGenericRepository<ProductCategory, int> ProductCategories => _categoryRepository.Value;

		public Task<int> CompleteAsync()
		{
			throw new NotImplementedException();
		}

		public ValueTask DisposeAsync()
		{
			throw new NotImplementedException();
		}
	}
}
