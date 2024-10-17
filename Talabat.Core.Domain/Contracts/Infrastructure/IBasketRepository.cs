using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Entities.Basket;

namespace Talabat.Core.Domain.Contracts.Infrastructure
{
	public interface IBasketRepository
	{
		Task<CustomerBasket?> GetAsync(string id);
		Task<CustomerBasket?>UpdateAsync(CustomerBasket customerBasket , TimeSpan time);
		Task<bool> DeleteAsync(string id);
	}
}
