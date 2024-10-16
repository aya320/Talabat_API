using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Services.Basket;
using Talabat.Core.Application.Abstraction.Services.Products;

namespace Talabat.Core.Application.Abstraction.Services
{
	public  interface IServiceManager
	{
		public IProductService ProductService { get; }
		public IBasketService BasketService { get; }
	}
}
