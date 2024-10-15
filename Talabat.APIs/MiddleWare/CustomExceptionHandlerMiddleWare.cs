using System.Net;
using Talabat.APIs.Controllers.Common.Exceptions;
using Talabat.APIs.Controllers.Errors;

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
			}

			catch (Exception ex)
			{
				ApiResponse response;

				switch (ex)
				{
					case  NotFoundException:
						httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
						httpContext.Response.ContentType = "application/json";
						 response = new ApiResponse(404, ex.Message);
						 await httpContext.Response.WriteAsync(response.ToString());

						break;


					default:
						if (_webHostEnvironment.IsDevelopment())
						{
							// DevelopmentMode

							_logger.LogError(ex, ex.Message);
							response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace!.ToString());

						}
						else
						{
							//Production
							response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
						}

						httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
						httpContext.Response.ContentType = "application/json";
						await httpContext.Response.WriteAsync(response.ToString());

						break;
				}
				
			}
		}
	}
}
