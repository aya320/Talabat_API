using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Domain.Contracts.Infrastructure;
using Talabat.Core.Domain.Entities.Basket;

namespace Talabat.Infrastructure.BasketRepository
{
	public class BasketRepository : IBasketRepository
	{
		private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
			_database = redis.GetDatabase();   
        }
        public async Task<CustomerBasket?> GetAsync(string id)
		{
			var basket = await _database.StringGetAsync(id);
			
			return basket.IsNullOrEmpty ? null :JsonSerializer.Deserialize<CustomerBasket>(basket!) ;
		}

		public async Task<CustomerBasket?> UpdateAsync(CustomerBasket customerBasket , TimeSpan time)
		{
			var value = JsonSerializer.Serialize(customerBasket);
			var updated = await  _database.StringSetAsync(customerBasket.Id, value , time);
			if (updated)
				return customerBasket;
			else
				return null;

		}

		public async Task<bool> DeleteAsync(string id)
		{
			var deleted = await _database.KeyDeleteAsync(id);
			return deleted; 
		}

		
	}
}
