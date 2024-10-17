﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Application.Abstraction.Models.Basket
{
	public class CustomerBasketDto
	{
		[Required]
		public int Id { get; set; }
		public IEnumerable<BasketItemDto> Items { get; set; } =new List<BasketItemDto>();
	}
}
