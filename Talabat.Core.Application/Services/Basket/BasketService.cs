using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Models.Basket;
using Talabat.Core.Application.Abstraction.Services.Basket;
using Talabat.Core.Application.Common.Exceptions;
using Talabat.Core.Domain.Contracts.Infrastructure;
using Talabat.Core.Domain.Entities.Basket;

namespace Talabat.Core.Application.Services.Basket
{
	public class BasketService(IBasketRepository _repository , IMapper _mapper ,IConfiguration _configuration) : IBasketService
	{

		public async Task<CustomerBasketDto> GetCustomerBasketAsync(string basketId)
		{
			var basket = _repository.GetAsync(basketId);
			if (basket is null) throw  new NotFoundException(nameof(CustomerBasket) , basketId);
			return  _mapper.Map<CustomerBasketDto>(basket);
		}

		public async Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto customerBasket)
		{
			var basket = _mapper.Map<CustomerBasket>(customerBasket);

			var timeToLife = TimeSpan.FromDays(double.Parse( _configuration.GetSection("RedisSetting")[" TimeToLiveInDay"]!));
			var updatedBasket =await _repository.UpdateAsync(basket , timeToLife);
			if (updatedBasket == null) throw new BadRequestException("Can't Update");
			return customerBasket;


		}

		public async Task DeleteCustomerBasketAsync(string basketId)
		{
			var deleted =await _repository.DeleteAsync(basketId);
			if (!deleted) throw new BadRequestException("Unable To Delete This Basket ");

		}

	
	}
}
