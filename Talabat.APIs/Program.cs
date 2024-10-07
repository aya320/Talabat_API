
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.Infrastructure.Persistence;
using Talabat.Infrastructure.Persistence.Data;

namespace Talabat.APIs
{
	public class Program
	{
		//[FromServices]
		//public static StoreContext StoreContext { get; set; } = null! ;
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddPersistenceServices(builder.Configuration);
			
			var app = builder.Build();

			using var Scope = app.Services.CreateAsyncScope();
			var Services =Scope.ServiceProvider;
			var dbcontext = Services.GetRequiredService<StoreContext>();
			var LoggerFactory= Services.GetRequiredService<ILoggerFactory>();
			try
			{
				var PendingMigrations = dbcontext.Database.GetPendingMigrations();
				if(PendingMigrations.Any())
				    await dbcontext.Database.MigrateAsync();
			}
			catch(Exception ex)
			{
				var logger = LoggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "Error Has Been Occured During Applying Migrations");
			}
			

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
