using System.Net;
using Talabat.APIs.Controllers.Errors;
using Talabat.Core.Application.Common.Exceptions;

namespace Talabat.APIs.MiddleWare
{
	public class CustomExceptionHandlerMiddleWare
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;
		private readonly IWebHostEnvironment _webHostEnvironment;


		public CustomExceptionHandlerMiddleWare(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleWare> logger, IWebHostEnvironment webHostEnvironment)
		{
			_next = next;
			_logger = logger;
			_webHostEnvironment = webHostEnvironment;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				//Request Logic
				await _next(httpContext);
				//Response Logic

				//if(httpContext.Response.StatusCode==(int)HttpStatusCode.NotFound)
				//{
				//	var response = new ApiResponse((int)HttpStatusCode.NotFound , $"The Request Endpoint {httpContext.Request.Path} is Not Found");
				//	 await httpContext.Response.WriteAsync(response.ToString());
				//}
			}

			catch (Exception ex)
			{
				if (_webHostEnvironment.IsDevelopment())
				{
					// DevelopmentMode

					_logger.LogError(ex, ex.Message);

				}
				else
				{
					//Production
				}
				ApiResponse response;

				switch (ex)
				{
					case  NotFoundException:
						httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
						httpContext.Response.ContentType = "application/json";
						 response = new ApiResponse(404, ex.Message);
						 await httpContext.Response.WriteAsync(response.ToString());

						break;

					case BadRequestException:
						httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
						httpContext.Response.ContentType = "application/json";
						response = new ApiResponse(400, ex.Message);
						await httpContext.Response.WriteAsync(response.ToString());

						break;


					default:

						response = _webHostEnvironment.IsDevelopment() ?
							new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace!.ToString()) 
						  :
							new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

						httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
						httpContext.Response.ContentType = "application/json";
						await httpContext.Response.WriteAsync(response.ToString());

						break;
				}
				
			}
		}
	}
}
