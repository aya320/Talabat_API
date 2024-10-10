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
        public ProductWithBrandAndCategorySpecfications():base()
        {
            Includes.Add(b => b.Brand!);
			Includes.Add(c => c.Category!);

		}

	}
}
