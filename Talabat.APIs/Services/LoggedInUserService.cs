using System.Security.Claims;
using Talabat.Core.Application.Abstraction;

namespace Talabat.APIs.Services
{
	public class LoggedInUserService : ILoggedInUserService
	{
		private readonly IHttpContextAccessor? _contextAccessor;

		public LoggedInUserService(IHttpContextAccessor? contextAccessor)
		{
			_contextAccessor = contextAccessor;
			UserId = _contextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
		}

		public string? UserId {  get; }
	}
}
