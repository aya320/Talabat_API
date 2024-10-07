using Talabat.Core.Domain.Contracts;

namespace Talabat.APIs.Extentions
{
	public static class InitializerExtentions
	{
		public static async Task<WebApplication> StoreContextInitializerAsync(this WebApplication app)
		{
			using var Scope = app.Services.CreateAsyncScope();
			var Services = Scope.ServiceProvider;
			var storecontextinitializer = Services.GetRequiredService<IStoreContextInitializer>();
			var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
			try
			{
				await storecontextinitializer.InitializeAsync();
				await storecontextinitializer.SeedAsync();
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
