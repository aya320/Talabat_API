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
	public class CategoryConfigurations:BaseAuditableEntityConfigurations<ProductCategory,int>
	{
		public override void Configure(EntityTypeBuilder<ProductCategory> builder)
		{
			base.Configure(builder);
			builder.Property(A => A.Name).IsRequired();

		}
	}
}
