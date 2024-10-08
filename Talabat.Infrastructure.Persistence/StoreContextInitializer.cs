using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Domain.Contracts;
using Talabat.Core.Domain.Entities.Products;
using Talabat.Infrastructure.Persistence.Data;

namespace Talabat.Infrastructure.Persistence
{
	internal class StoreContextInitializer(StoreContext _dbContext) : IStoreContextInitializer
	{
		

		public async Task InitializeAsync()
		{
			var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
			if (PendingMigrations.Any())
				await _dbContext.Database.MigrateAsync();  //Update Database
		}

		public async Task SeedAsync()
		{
			if (!_dbContext.Brands.Any())
			{
				var BrandsData = await File.ReadAllTextAsync("../Talabat.Infrastructure.Persistence\\Data\\Seeds\\brands.json");
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

				if (brands?.Count > 0)
				{
					await _dbContext.Brands.AddRangeAsync(brands);
					await _dbContext.SaveChangesAsync();
				}

			}

			if (!_dbContext.Categories.Any())
			{

				var CategoriesData = await File.ReadAllTextAsync("../Talabat.Infrastructure.Persistence\\Data\\Seeds\\categories.json");
				var categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);

				if (categories?.Count > 0)
				{
					await _dbContext.Set<ProductCategory>().AddRangeAsync(categories);
					await _dbContext.SaveChangesAsync();
				}

			}

			if (!_dbContext.Products.Any())
			{

				var ProductsData = await File.ReadAllTextAsync("../Talabat.Infrastructure.Persistence\\Data\\Seeds\\products.json");
				var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

				if (products?.Count > 0)
				{
					await _dbContext.Set<Product>().AddRangeAsync(products);
					await _dbContext.SaveChangesAsync();
				}
			}
		}
	}
}
