using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Infrastructure.Persistence.Data;

namespace Talabat.Infrastructure.Persistence
{
	public static class DependancyInjection
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services ,IConfiguration Configuration)
		{
			services.AddDbContext<StoreContext>((options) =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("StoreContext"));

			});
			return services;
		}
	}
}
