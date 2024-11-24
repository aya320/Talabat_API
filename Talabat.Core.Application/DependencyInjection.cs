using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Services;
using Talabat.Core.Application.Abstraction.Services.Basket;
using Talabat.Core.Application.Abstraction.Services.Orders;
using Talabat.Core.Application.Abstraction.Services.Products;
using Talabat.Core.Application.Mapping;
using Talabat.Core.Application.Services;
using Talabat.Core.Application.Services.Basket;
using Talabat.Core.Application.Services.Orders;
using Talabat.Core.Application.Services.Products;
using Talabat.Core.Domain.Contracts.Infrastructure;

namespace Talabat.Core.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationService(this IServiceCollection services)
		{
			//services.AddAutoMapper(m=>m.AddProfile(new MappingProfile()));
			//services.AddAutoMapper(m => m.AddProfile< MappingProfile>());
			services.AddAutoMapper(typeof(MappingProfile));
            //services.AddAutoMapper(typeof(MappingProfile).Assembly);


            services.AddScoped(typeof(IOrderService), typeof(OrderService));
          
            services.AddScoped(typeof(Func<IOrderService>), (serviceProvider) =>
            {
                return () => serviceProvider.GetRequiredService<IOrderService>();
            });


            services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));


			//services.AddScoped(typeof(IBasketService), typeof(BasketService));

			//services.AddScoped(typeof(Func<IBasketService>), typeof(Func<BasketService>));

			services.AddScoped(typeof(Func<IBasketService>), serviceProvider =>
			{
                return ()=>serviceProvider.GetRequiredService<IBasketService>();

			});



			return services;
		}
	}
}
