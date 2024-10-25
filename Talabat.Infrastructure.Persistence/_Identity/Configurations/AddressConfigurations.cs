using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Entities.Identity;
using Talabat.Infrastructure.Persistence._Common;
using Talabat.Infrastructure.Persistence.Data;

namespace Talabat.Infrastructure.Persistence._Identity.Configurations
{
    [DbContextType(typeof(StoreIdentityDbContext))]

    internal class AddressConfigurations : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(nameof(Address.Id)).ValueGeneratedOnAdd() ;
            builder.ToTable("Addresses");
            builder.Property(A => A.FirstName).HasColumnType("nvarchar").HasMaxLength(100).IsRequired(true);
            builder.Property(A => A.LastName).HasColumnType("nvarchar").HasMaxLength(100).IsRequired(true);
            builder.Property(A => A.City).HasColumnType("varchar").HasMaxLength(100).IsRequired(true);
            builder.Property(A => A.Country).HasColumnType("varchar").HasMaxLength(100).IsRequired(true);
            builder.Property(A => A.Street).HasColumnType("varchar").HasMaxLength(100).IsRequired(true);


        }
    }
}
