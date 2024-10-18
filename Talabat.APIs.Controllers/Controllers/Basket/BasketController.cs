using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.APIs.Controllers.Base;
using Talabat.Core.Application.Abstraction.Models.Basket;
using Talabat.Core.Application.Abstraction.Services;

namespace Talabat.APIs.Controllers.Controllers.Basket
{
	public class BasketController(IServiceManager serviceManger) : BaseAPIController
	{
		[HttpGet]
		public async Task<ActionResult<CustomerBasketDto>> GetBasket(string id)
		{
			var basket = await serviceManger.BasketService.GetCustomerBasketAsync(id);
			  return Ok(basket);
		}
		[HttpPost]
		public async Task<ActionResult<CustomerBasketDto>> UpdateBasket(CustomerBasketDto basketDto)
		{
			var basket = await serviceManger.BasketService.UpdateCustomerBasketAsync(basketDto);
			return Ok(basket);
		}
		[HttpDelete]
		public async Task DeleteBasket(string id)
		{
			await serviceManger.BasketService.DeleteCustomerBasketAsync(id);
		}
	}
}
