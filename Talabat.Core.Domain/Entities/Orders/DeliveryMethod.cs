﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Domain.Entities.Orders
{
    public class DeliveryMethod : BaseEntity<int>
    {
        public required String ShortName { get; set; }
        public required string Description { get; set; }
        public decimal Cost { get; set; }
        public required string DeliveryTime { get; set; }
    }


}
