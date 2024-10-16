using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Contracts.Infrastructure;
using Talabat.Infrastructure.BasketRepository;
	

namespace Talabat.Infrastructure
{
	public static class DependancyInjection
	{
		public static IServiceCollection AddInfrastructureServices (this IServiceCollection service , IConfiguration configuration)
		{
			service.AddSingleton(typeof(IConnectionMultiplexer),(serviceProvider)=>
			{
				var connectionString = configuration.GetConnectionString("Redis");
				return ConnectionMultiplexer.Connect("connectionString");
			});

			service.AddScoped(typeof(IBasketRepository), typeof(BasketRepository.BasketRepository));

			return service;

		}
	}
}
