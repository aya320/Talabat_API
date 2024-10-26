using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.APIs.Controllers.Base;
using Talabat.Core.Application.Abstraction.Models.Auth;
using Talabat.Core.Application.Abstraction.Services;

namespace Talabat.APIs.Controllers.Controllers.Account
{
    public class AccountController(IServiceManager _serviceManager) : BaseAPIController
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
            {
                throw new ArgumentException("Email and password cannot be empty.");
            }
            var result= await _serviceManager.AuthServices.LoginAsync(model);
            return Ok(result);  
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var result = await _serviceManager.AuthServices.RegisterAsync(model);
            return Ok(result);
        }
    }
}
