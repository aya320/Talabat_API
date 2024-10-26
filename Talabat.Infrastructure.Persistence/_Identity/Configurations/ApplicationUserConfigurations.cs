using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Entities.Identity;
using Talabat.Infrastructure.Persistence._Common;

namespace Talabat.Infrastructure.Persistence._Identity.Configurations
{
    [DbContextType(typeof(StoreIdentityDbContext))]

    public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(A => A.DisplayName).HasColumnType("varchar").HasMaxLength(100).IsRequired(true);
            builder.HasOne(a => a.Address).WithOne(a => a.User).HasForeignKey<Address>(a=>a.UserId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
