using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Services;
using Talabat.Core.Application.Abstraction.Services.Products;
using Talabat.Core.Application.Mapping;
using Talabat.Core.Application.Services;
using Talabat.Core.Application.Services.Products;

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

			services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));



			return services;
		}
	}
}
