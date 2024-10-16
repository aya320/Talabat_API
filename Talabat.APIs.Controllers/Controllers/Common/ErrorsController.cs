using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Talabat.APIs.Controllers.Errors;

namespace Talabat.APIs.Controllers.Controllers.Common
{
	[ApiController]
	[Route("Errors/{code}")]
	[ApiExplorerSettings(IgnoreApi =false)]
	public class ErrorsController :ControllerBase
	{
		[HttpGet]
		public IActionResult Error(int code)
		{
			if (code == (int)HttpStatusCode.NotFound)
			{
				var response = new ApiResponse((int)HttpStatusCode.NotFound, $"The Request Endpoint {Request.Path} is Not Found");
				return NotFound(response);
			}

			return StatusCode(code , new ApiResponse(code));
		}

	}
}
