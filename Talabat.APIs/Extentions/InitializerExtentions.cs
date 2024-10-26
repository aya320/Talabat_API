using Talabat.Core.Domain.Contracts.Persistence.DbInitializer;
using Talabat.Infrastructure.Persistence._Identity;

namespace Talabat.APIs.Extentions
{
    public static class InitializerExtentions
	{
		public static async Task<WebApplication> InitializeAsync(this WebApplication app)
		{
			using var Scope = app.Services.CreateAsyncScope();
			var Services = Scope.ServiceProvider;
			var storecontextinitializer = Services.GetRequiredService<IStoreInitializer>();
            var Identitycontextinitializer = Services.GetRequiredService<IStoreIdentityDbInitializer>();

            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
			try
			{
				await storecontextinitializer.InitializeAsync();
				await storecontextinitializer.SeedAsync();
				await Identitycontextinitializer.InitializeAsync();
				await Identitycontextinitializer.SeedAsync();

            }
			catch (Exception ex)
			{
				var logger = LoggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "Error Has Been Occured During Applying Migrations Or Data Seeding");
			}

			return app;
		}
	}
}
