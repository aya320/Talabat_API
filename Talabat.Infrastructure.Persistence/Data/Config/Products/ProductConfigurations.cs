using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Entities.Products;
using Talabat.Infrastructure.Persistence.Data.Config.Common;

namespace Talabat.Infrastructure.Persistence.Data.Config.Products
{
	public class ProductConfigurations :BaseAuditableEntityConfigurations<Product,int>
	{
		public override void Configure(EntityTypeBuilder<Product> builder)
		{
			base.Configure(builder);
			builder.Property(A => A.Name).IsRequired().HasMaxLength(100);
			builder.Property(A => A.Description).IsRequired().HasMaxLength(100);
			builder.Property(A => A.Price).HasColumnType("decimal(9,2)");

			builder.HasOne(A => A.Brand).WithMany().HasForeignKey(A => A.BrandId).OnDelete(DeleteBehavior.SetNull);
			builder.HasOne(A => A.Category).WithMany().HasForeignKey(A => A.CategoryId).OnDelete(DeleteBehavior.SetNull);


		}
	}
}
