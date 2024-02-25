using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services.Impl.Model;

namespace TestApp.Services.Contracts.Business
{
    public interface ICheckoutService
    {
        Task<double> PriceAsync(string userId);

        
    }
}
