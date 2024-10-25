using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Contracts.Persistence;
using Talabat.Core.Domain.Contracts.Persistence.DbInitializer;
using Talabat.Core.Domain.Entities.Identity;
using Talabat.Infrastructure.Persistence._Data;
using Talabat.Infrastructure.Persistence._Identity;
using Talabat.Infrastructure.Persistence.Data;
using Talabat.Infrastructure.Persistence.Data.Interceptors;
using Talabat.Infrastructure.Persistence.UnitOfWork;

namespace Talabat.Infrastructure.Persistence
{
    public static class DependancyInjection
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services ,IConfiguration Configuration)
		{
			#region StoreContext
			services.AddDbContext<StoreDbContext>((options) =>
			{
				options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("StoreContext"));

			});
			//services.AddScoped<IStoreContextInitializer, StoreContextInitializer>();
			services.AddScoped(typeof(IStoreInitializer), typeof(StoreContextInitializer));

			services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CustomSaveChangesInterceptors));
            #endregion

            #region IdentityContext
            services.AddDbContext<StoreIdentityDbContext>((options) =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("IdentityContext"));

            });
			services.AddScoped(typeof(IStoreIdentityDbInitializer), typeof(StoreIdentityDbInitializer));
            #endregion
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));
            //services.AddIdentityCore<ApplicationUser>();
            //services.AddIdentityCore<ApplicationUser>(identityOptions =>
            //{
            //    identityOptions
			//});



            return services;
		}
	}
}
