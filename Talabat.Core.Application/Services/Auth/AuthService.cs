using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Models.Auth;
using Talabat.Core.Application.Abstraction.Models.Orders;
using Talabat.Core.Application.Abstraction.Services.Auth;
using Talabat.Core.Application.Common.Exceptions;
using Talabat.Core.Application.Extensions;
using Talabat.Core.Domain.Entities.Identity;


namespace Talabat.Core.Application.Services.Auth
{
    public class AuthService(UserManager<ApplicationUser> _userManager ,SignInManager<ApplicationUser> _signInManager ,IOptions<JwtSetting> jwtSetting, IMapper mapper) : IAuthServices
    {
        private readonly JwtSetting _jwtSetting=jwtSetting.Value;
        public async Task<UserDto> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                throw new UnAuthorizedException("Invalid Login!");
            
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password ,lockoutOnFailure:true);

            if(result.IsNotAllowed)
                throw new UnAuthorizedException("Account Not Confirmed Yet!");
            if (result.IsLockedOut)
                throw new UnAuthorizedException("Account Is Locked!");
            if (result.RequiresTwoFactor)
                throw new UnAuthorizedException("Require Two-Factor Authentication");
            if(!result.Succeeded)
                throw new UnAuthorizedException("Invalid Login!");
            var response = new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email!,
                Id = user.Id ,
                Token = await GenerateTokenAsync(user)


            };
            return response;






        }


        public async Task<AddressDto?> GetUserAddress(ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal?.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindUserWithAddress(claimsPrincipal!);
            var address = mapper.Map<AddressDto>(user!.Address);
            return address;
        }
        public async Task<AddressDto> UpdateUserAddress(ClaimsPrincipal claimsPrincipal, AddressDto addressDto)
        {
            var User = await _userManager.FindUserWithAddress(claimsPrincipal!);
            var updateAddress = mapper.Map<Address>(addressDto);
            if (User?.Address is not null)
                updateAddress.Id = User.Address.Id;
            User!.Address = updateAddress;
            var result = await _userManager.UpdateAsync(User);
            if (!result.Succeeded) throw new BadRequestException(result.Errors.Select(error => error.Description).Aggregate((X, Y) => $"{X},{Y}"));
            return addressDto;
        }
        public async Task<UserDto> GetCurrentUser(ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email!);
            return new UserDto()
            {
                Id = user!.Id,
                Email = user.Email!,
                DisplayName = user.DisplayName,
                Token = await GenerateTokenAsync(user)
            };
        }
        public async Task<UserDto> RegisterAsync(RegisterDto model)
        {
            var user = new ApplicationUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
                throw new ValidationException() { Errors = result.Errors.Select(e=>e.Description)};
            var response = new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Id = user.Id,
                Token = await GenerateTokenAsync(user)

            };
            return response;


        }

        private async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var userClaims =await _userManager.GetClaimsAsync(user);
            var roleAsClaims = new List<Claim>();
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                roleAsClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));   
            }
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.PrimarySid , user.Id) ,
                new Claim(ClaimTypes.Email , user.Email!),
                new Claim(ClaimTypes.GivenName , user.DisplayName),
                //new Claim("Secret" , " AnyThing")

            }.Union(userClaims).Union(roleAsClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var tokenobj = new JwtSecurityToken(
                  
                   issuer: _jwtSetting.Issuer,
                   audience: _jwtSetting.Audience,
                   expires: DateTime.UtcNow.AddMinutes(_jwtSetting.DurationInMinutes),
                   claims: Claims,
                   signingCredentials: signingCredentials
                  
                   );
                  
            return new JwtSecurityTokenHandler().WriteToken(tokenobj);
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }
    }
}
