using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Domain.Contracts.Persistence.DbInitializer;
using Talabat.Core.Domain.Entities.Products;

namespace Talabat.Infrastructure.Persistence._Common
{
    public abstract class DbInitializer(DbContext _dbContext) :  IDbInitializer
    {


        public async Task InitializeAsync()
        {
            var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
            if (PendingMigrations.Any())
                await _dbContext.Database.MigrateAsync();  //Update Database
        }

        public abstract  Task SeedAsync();
    }
}
