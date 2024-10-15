using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.APIs.Controllers.Base;
using Talabat.APIs.Controllers.Errors;

namespace Talabat.APIs.Controllers.Controllers.Buggy
{
	public class BuggyController: BaseAPIController
	{
		[HttpGet("notfound")] // GET: /api/buggy/notfound
      public IActionResult GetNotFoundRequest()
		{

			return NotFound(new ApiResponse( 404 )); // 404

		}

		[HttpGet("servererror")] // GET: /api/buggy/servererror
      public IActionResult GetServerError()
		{

			throw new Exception(); // 500

		}


		[HttpGet("badrequest")] // GET: /api/buggy/badrequest
      public IActionResult GetBadRequest()
		{

			return BadRequest(new ApiResponse(400)); // 400

		}

		[HttpGet("badrequest/{id}")] // GET: /api/buggy/badrequest/fiv
       public IActionResult GetValidationError(int id)
		{

			return Ok();

		}

		[HttpGet("unauthorized")] // GET: /api/buggy/unauthorized
       public IActionResult GetUnauthorizedError()
		{

			return Unauthorized(new ApiResponse(401));

		}

		[HttpGet("forbidden")] // GET: /api/buggy/forbidden
		public IActionResult GetForbiddenError()
		{

			return Forbid();

		}

		[Authorize]
		[HttpGet("authorized")] // GET: /api/buggy/authorized
		public IActionResult UnauthorizedError()
		{

			return Ok();

		}
	}
}
