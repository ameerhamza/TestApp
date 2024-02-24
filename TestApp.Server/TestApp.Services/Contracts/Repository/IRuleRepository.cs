using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services.Impl;

namespace TestApp.Services.Contracts.Repository
{
    public interface IRuleRepository
    {
        Task<List<CartRule>> Get();
        Task<Services.Impl.CartRule> Get(char SKU);
    }
}
