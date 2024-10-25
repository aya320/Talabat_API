﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Domain.Contracts.Persistence.DbInitializer
{
    public interface IDbInitializer
    {
        Task InitializeAsync();
        Task SeedAsync();
    }
}
