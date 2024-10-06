using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Entities.Products;

namespace Talabat.Infrastructure.Persistence.Data
{
	public class StoreContext :DbContext
	{
        public StoreContext(DbContextOptions<StoreContext> options):base(options)
        {
           
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly);
		}
		DbSet<Product> Products { get; set; }
		DbSet<ProductCategory> Categories { get; set; }
		DbSet<ProductBrand> Brands { get; set; }


	}
}
