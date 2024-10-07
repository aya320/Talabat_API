using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Domain.Entities.Products;
using Talabat.Infrastructure.Persistence.Data;

namespace Talabat.Infrastructure.Persistence
{
	public static class StoreContextSeed
	{
		public static async Task SeedAsync(StoreContext dbContext)
		{
			if (!dbContext.Brands.Any())
			{
				var BrandsData = await File.ReadAllTextAsync("../Talabat.Infrastructure.Persistence\\Data\\Seeds\\brands.json");
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

				if (brands?.Count > 0)
				{
					await dbContext.Brands.AddRangeAsync(brands);
					await dbContext.SaveChangesAsync();
				}

			}

			if (!dbContext.Categories.Any())
			{

				var CategoriesData = await File.ReadAllTextAsync("../Talabat.Infrastructure.Persistence\\Data\\Seeds\\categories.json");
				var categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);

				if (categories?.Count > 0)
				{
					await dbContext.Set<ProductCategory>().AddRangeAsync(categories);
					await dbContext.SaveChangesAsync();
				}

			}

			if (!dbContext.Products.Any())
			{

				var ProductsData = await File.ReadAllTextAsync("../Talabat.Infrastructure.Persistence\\Data\\Seeds\\products.json");
				var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

				if (products?.Count > 0)
				{
					await dbContext.Set<Product>().AddRangeAsync(products);
					await dbContext.SaveChangesAsync();
				}
			}
		}
	}
}
