
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.APIs.Extentions;
using Talabat.Core.Domain.Contracts;
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

			#region  Databases Initialization


			//await InitializerExtentions.StoreContextInitializerAsync(app);
			await app.StoreContextInitializerAsync(); 
			#endregion

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
