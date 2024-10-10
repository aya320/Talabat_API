using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Common;
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
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductCategory> Categories { get; set; }
		public DbSet<ProductBrand> Brands { get; set; }

		//public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		//{
   //          foreach (var entry in this.ChangeTracker.Entries<BaseAuditableEntity<int>>().Where(entity=>entity.State is EntityState.Added or EntityState.Modified))
  //          {
  //              if(entry.State is EntityState.Added)
		//		{
		//			entry.Entity.CreatedBy = "";
		//			entry.Entity.CreatedOn = DateTime.UtcNow;
		//		}

		//		entry.Entity.LastModifiedBy = "";
		//		entry.Entity.LastModifiedOn = DateTime.UtcNow;
		//	}
  //          return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		//}


	}
}
