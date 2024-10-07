using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Entities.Products;

namespace Talabat.Core.Domain.Contracts
{
	public interface IUnitOfWork : IAsyncDisposable
	{
		public IGenericRepository<Product,int> Products { get; }
		public IGenericRepository<ProductBrand, int> ProductBrands { get; }
		public IGenericRepository<ProductCategory, int> ProductCategories { get;  }

		Task<int> CompleteAsync(); 

	}
}
