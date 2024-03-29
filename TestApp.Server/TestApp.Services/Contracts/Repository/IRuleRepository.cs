﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services.Impl.Model;

namespace TestApp.Services.Contracts.Repository
{
    public interface IRuleRepository
    {
        Task<List<CartRule>> Get();
        Task<CartRule> Get(char SKU);
    }
}
