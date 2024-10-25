using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Domain.Contracts.Persistence.DbInitializer;
using Talabat.Core.Domain.Entities.Products;
using Talabat.Infrastructure.Persistence._Common;
using Talabat.Infrastructure.Persistence.Data;

namespace Talabat.Infrastructure.Persistence._Data
{
    internal class StoreContextInitializer(StoreDbContext _dbContext) : DbInitializer(_dbContext),IStoreInitializer
    {

        public override async  Task SeedAsync()
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
