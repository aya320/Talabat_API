using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Entities.Orders;
using Talabat.Infrastructure.Persistence.Data.Config.Common;

namespace Talabat.Infrastructure.Persistence._Data.Config.Orders
{
    public class OrderItemConfigrations : BaseAuditableEntityConfigurations<OrderItem, int>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder);
            builder.OwnsOne(item => item.Product, product => product.WithOwner());
            builder.Property(item => item.Price).HasColumnType("decimal(8, 2)");
        }
    }
}