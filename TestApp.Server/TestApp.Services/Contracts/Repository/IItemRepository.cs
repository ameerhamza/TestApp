using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services.Impl.Model;

namespace TestApp.Services.Contracts.Repository
{
    public interface IItemRepository
    {
        Task<Item> Get(char sku);
        Task<List<Item>> Get(string skus);
    }
}
