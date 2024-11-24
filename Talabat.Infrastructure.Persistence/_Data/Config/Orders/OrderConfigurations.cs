﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Entities.Orders;
using Talabat.Infrastructure.Persistence.Data.Config.Common;

namespace Talabat.Infrastructure.Persistence._Data.Config.Orders
{
    public class OrderConfigurations : BaseAuditableEntityConfigurations< Order,int>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);
            builder.OwnsOne(order => order.ShippingAddress, shippingAddress => shippingAddress.WithOwner());
            builder.Property(order => order.Status)
                .HasConversion
                (
                (OStatus) => OStatus.ToString(),
                (OStatus) => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus)
                );
            builder.Property(order => order.Subtotal)
                .HasColumnType("decimal(8,2)");

            builder.HasOne(order => order.DeliveryMethod)
                .WithMany()
                .HasForeignKey(order => order.DeliveryMethodId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(order => order.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}