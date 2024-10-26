using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Common;
using Talabat.Infrastructure.Persistence._Common;

namespace Talabat.Infrastructure.Persistence.Data.Config.Common
{
	[DbContextType(typeof(StoreDbContext))]
	public class BaseEntityConfigurations<TEntity, TKey> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
	{
		public virtual void Configure(EntityTypeBuilder<TEntity> builder)
		{
			builder.Property(A => A.Id).ValueGeneratedOnAdd();
		}
	}
}
