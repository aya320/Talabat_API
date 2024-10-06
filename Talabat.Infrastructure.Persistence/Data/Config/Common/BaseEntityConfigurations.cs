using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Common;

namespace Talabat.Infrastructure.Persistence.Data.Config.Common
{
	public class BaseEntityConfigurations<TEntity, TKey> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
	{
		public virtual void Configure(EntityTypeBuilder<TEntity> builder)
		{
			builder.Property(A => A.Id).ValueGeneratedOnAdd();
			builder.Property(A => A.CreatedBy).IsRequired();
			builder.Property(A => A.CreatedOn).IsRequired()
				/*.HasDefaultValueSql("GETUTCDATE()")*/;
			builder.Property(A => A.LastModifiedBy).IsRequired();
			builder.Property(A => A.LastModifiedOn).IsRequired()
			   /*.HasDefaultValueSql("GETUTCDATE()")*/;
		}
	}
}
