using System.Net;
using Talabat.APIs.Controllers.Errors;
using Talabat.Core.Application.Common.Exceptions;

namespace Talabat.APIs.MiddleWare
{
	public class CustomExceptionHandlerMiddleWare
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;
		private readonly IWebHostEnvironment _env;

		public CustomExceptionHandlerMiddleWare(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleWare> logger, IWebHostEnvironment env)
		{
			_next = next;
			_logger = logger;
			_env = env;
		}
		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
				//if(httpContext.Response.StatusCode== (int)HttpStatusCode.NotFound)
				//{
				//    var response = new ApiResponse((int)HttpStatusCode.NotFound, $"The Requested EndPoint :{httpContext.Request.Path}is not found");
				//    await httpContext.Response.WriteAsync(response.ToString()); 
				//}
			}
			catch (Exception ex)
			{
				#region Logging:TODO
				if (_env.IsDevelopment())
				{
					//Development
					_logger.LogError(ex, ex.Message);


				}
				else
				{

					//production

				}
				#endregion
				await HandelExceptionAsync(httpContext, ex);

			}
		}

		private async Task HandelExceptionAsync(HttpContext httpContext, Exception ex)
		{
			ApiResponse response;
			switch (ex)
			{
				case NotFoundException:
					httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
					httpContext.Response.ContentType = "application/json";
					response = new ApiResponse(404, ex.Message);
					await httpContext.Response.WriteAsync(response.ToString());
					break;

                case ValidationException validationException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    httpContext.Response.ContentType = "application/json";
                    response = new ApiValidationErrorResponse(ex.Message) { Errors = validationException.Errors };
                    await httpContext.Response.WriteAsync(response.ToString());
                    break;

                case BadRequestException:
					httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
					httpContext.Response.ContentType = "application/json";
					response = new ApiResponse(400, ex.Message);
					await httpContext.Response.WriteAsync(response.ToString());
					break;
                case UnAuthorizedException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    httpContext.Response.ContentType = "application/json";
                    response = new ApiResponse(401, ex.Message);
                    await httpContext.Response.WriteAsync(response.ToString());
                    break;
                default:
					response = _env.IsDevelopment() ?
						 new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString()) :
						 new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);


					httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					httpContext.Response.ContentType = "application/json";
					//await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
					await httpContext.Response.WriteAsync(response.ToString());
					break;
			}
		}
	}
}

