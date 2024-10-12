using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Contracts;
using Talabat.Core.Domain.Entities.Products;

namespace Talabat.Core.Domain.Specifications.Products
{
	public class ProductWithBrandAndCategorySpecfications : BaseSpecifications<Product, int>
	{
        public ProductWithBrandAndCategorySpecfications(string? sort, int? brandId, int? categoryId,int pageSize , int pageIndex) 
			:base
			(
			   x=> 
			   (!brandId.HasValue ||x.BrandId==brandId.Value  )
			   &&
			   (!categoryId.HasValue || x.CategoryId == categoryId.Value)
			)
        {
			AddIncludes();
			
				switch (sort)
				{
					case "NameDesc":
						AddOrderByDesc(x => x.Name);
						break;
					case "PriceAsyc":
						AddOrderBy(x => x.Price);
						break;
					case "PriceDesc":
						AddOrderByDesc(x => x.Price);
						break;
					default:
						AddOrderByDesc(x => x.Name);
						break;

				
			    }
			    
			ApplyPagination(pageSize*(pageIndex-1), pageSize);
				

		}

		public ProductWithBrandAndCategorySpecfications(int Id) : base(Id)
		{
			AddIncludes();

		}

		public ProductWithBrandAndCategorySpecfications()
		{
		}

		private protected override void AddIncludes()
		{
			base.AddIncludes();
			Includes.Add(b => b.Brand!);
			Includes.Add(c => c.Category!);
		}
	
	}
}
