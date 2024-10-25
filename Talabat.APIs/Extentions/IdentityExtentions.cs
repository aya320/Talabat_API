using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Application.Abstraction.Models.Auth;
using Talabat.Core.Application.Abstraction.Services.Auth;
using Talabat.Core.Application.Services.Auth;
using Talabat.Core.Domain.Entities.Identity;
using Talabat.Infrastructure.Persistence._Identity;

namespace Talabat.APIs.Extentions
{
    public static class IdentityExtentions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services , IConfiguration _configuration)
        {
            services.Configure<JwtSetting>(_configuration.GetSection("jwtSettings"));
            services.AddIdentity<ApplicationUser, IdentityRole>(identityOptions =>
            {
                identityOptions.User.RequireUniqueEmail = true;
                //identityOptions.User.AllowedUserNameCharacters = "asdfghiemopcvpo12";

                identityOptions.SignIn.RequireConfirmedAccount = true;
                identityOptions.SignIn.RequireConfirmedEmail = true;
                identityOptions.SignIn.RequireConfirmedPhoneNumber = true;

                identityOptions.Password.RequireNonAlphanumeric = true;
                identityOptions.Password.RequiredUniqueChars = 2;
                identityOptions.Password.RequiredLength = 6;
                identityOptions.Password.RequireDigit = true;
                identityOptions.Password.RequireLowercase = true;
                identityOptions.Password.RequireUppercase = true;

                identityOptions.Lockout.AllowedForNewUsers = true;
                identityOptions.Lockout.MaxFailedAccessAttempts = 5;
                identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(6);

            }).AddEntityFrameworkStores<StoreIdentityDbContext>();


            services.AddAuthentication((authenciationOptions) =>
            {
                authenciationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authenciationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer((options) =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
          
                    ClockSkew = TimeSpan.FromMinutes(0),
                    ValidAudience = _configuration["jwtSettings:Audience"],
                    ValidIssuer = _configuration["jwtSettings:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwtSettings:Key"]!)),
                };
            });


            services.AddScoped(typeof(IAuthServices), typeof(AuthService));
            services.AddScoped(typeof(Func<IAuthServices>), (ServiceProvider) => {

                return () => ServiceProvider.GetService<IAuthServices>();

            });
            return services;
        }
    }
}
