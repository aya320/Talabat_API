using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.APIs.Controllers.Base;

namespace Talabat.APIs.Controllers.Controllers.Buggy
{
	public class BuggyController: BaseAPIController
	{
		[HttpGet("notfound")] // GET: /api/buggy/notfound
      public IActionResult GetNotFoundRequest()
		{

			return NotFound(new {StatusCode=404 ,Messsage="NotFound"}); // 404

		}

		[HttpGet("servererror")] // GET: /api/buggy/servererror
      public IActionResult GetServerError()
		{

			throw new Exception(); // 500

		}


		[HttpGet("badrequest")] // GET: /api/buggy/badrequest
      public IActionResult GetBadRequest()
		{

			return BadRequest(new { StatusCode = 400, Messsage = "BadRequest" }); // 400

		}

		[HttpGet("badrequest/{id}")] // GET: /api/buggy/badrequest/fiv
       public IActionResult GetValidationError(int id)
		{

			return Ok();

		}

		[HttpGet("unauthorized")] // GET: /api/buggy/unauthorized
       public IActionResult GetUnauthorizedError()
		{

			return Unauthorized(new { StatusCode = 401, Messsage = "Unauthorized" });

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
