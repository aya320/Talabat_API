using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Domain.Contracts.Persistence.DbInitializer;
using Talabat.Core.Domain.Entities.Identity;
using Talabat.Infrastructure.Persistence._Common;

namespace Talabat.Infrastructure.Persistence._Identity
{
    public class StoreIdentityDbInitializer(StoreIdentityDbContext _dbContext , UserManager<ApplicationUser> _userManager) : DbInitializer(_dbContext) , IStoreIdentityDbInitializer
    {

        public override async Task SeedAsync()
        {
            if (!_userManager.Users.Any())
            {
                var User = new ApplicationUser()
                {
                    DisplayName = "yoyo Ali",
                    UserName = "Yoyo.Ali",
                    Email = "yoyoali@gmail.com",
                    PhoneNumber = "01026139025",

                };
                await _userManager.CreateAsync(User, "p@ssw0rd");
            }
        }
        
    }
}
