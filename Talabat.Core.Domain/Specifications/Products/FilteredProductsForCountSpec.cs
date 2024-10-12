using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Entities.Products;

namespace Talabat.Core.Domain.Specifications.Products
{
	public class FilteredProductsForCountSpec : BaseSpecifications<Product,int>
	{
        public FilteredProductsForCountSpec(int? brandId, int? categoryId,string? search) :
            base
            (
			   x =>
				(string.IsNullOrEmpty(search) || x.NormalizedName.Contains(search))
			   &&
			   (!brandId.HasValue || x.BrandId == brandId.Value)
			   &&
			   (!categoryId.HasValue || x.CategoryId == categoryId.Value)
			)
        {
            
        }
    }
}
