
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Talabat.APIs.Controllers.Errors;
using Talabat.APIs.Extentions;
using Talabat.APIs.MiddleWare;
using Talabat.APIs.Services;
using Talabat.Core.Application;
using Talabat.Core.Application.Abstraction;
using Talabat.Core.Domain.Contracts;
using Talabat.Infrastructure.Persistence;
using Talabat.Infrastructure.Persistence.Data;
using Talabat.Infrastructure;
using Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Talabat.Infrastructure.Persistence._Identity;

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


			//builder.Services.Configure<ApiBehaviorOptions>(options =>
			//{

			//	options.SuppressModelStateInvalidFilter = false;
			//	options.InvalidModelStateResponseFactory = (actionContext) =>
			//	{
			//		var errors = actionContext.ModelState.Where(p => p.Value!.Errors.Count > 0)
			//					   .SelectMany(p => p.Value!.Errors)
			//					   .Select(p => p.ErrorMessage);

			//		return new BadRequestObjectResult(new ApiValidationErrorResponse()
			//		{ Errors = errors });
			//	};
			//});


			builder.Services.AddControllers()
				.ConfigureApiBehaviorOptions(options=>
				{ 
				   options.SuppressModelStateInvalidFilter = false;
					options.InvalidModelStateResponseFactory = (actionContext)=>
					{
                        //var errors = actionContext.ModelState.Where(p => p.Value!.Errors.Count > 0)
                        //			   .Select(p=>new ApiValidationErrorResponse.ValidationError ()
                        //			   {
                        //				   Field =p.Key ,
                        //				   Errors =p.Value!.Errors.Select(e=>e.ErrorMessage)

                        //			   });

                        //return new BadRequestObjectResult(new ApiValidationErrorResponse()
                        //{ Errors=errors};
                        //{ Errors = errors });

                        var errors = actionContext.ModelState
                            .Where(p => p.Value.Errors.Count > 0)
                            .SelectMany(p => p.Value.Errors.Select(e => e.ErrorMessage)) 
                            .ToArray();

                        return new BadRequestObjectResult(new ApiValidationErrorResponse
                        {
                            Errors = errors 
                        });
                    } ;
				})
				.AddApplicationPart(typeof(Controllers.AssemblyInformation).Assembly);
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddScoped(typeof(ILoggedInUserService),typeof(LoggedInUserService));
			builder.Services.AddHttpContextAccessor();
			builder.Services.AddPersistenceServices(builder.Configuration);
			builder.Services.AddApplicationService();
			builder.Services.AddInfrastructureServices(builder.Configuration);
			//builder.Services.AddIdentityCore<ApplicationUser>();
			builder.Services.AddIdentityServices(builder.Configuration);


            var app = builder.Build();

			#region  Databases Initialization


			//await InitializerExtentions.StoreContextInitializerAsync(app);
			await app.InitializeAsync(); 
			#endregion

			// Configure the HTTP request pipeline.
			app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseStatusCodePagesWithReExecute("/Errors/{0}");

			app.UseAuthorization();
			app.UseAuthentication();
			
			app.UseStaticFiles();
			app.MapControllers();

			app.Run();
		}
	}
}
