using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services.Impl.Model;

namespace TestApp.Services.Contracts.Business
{
    public interface ICartService
    {
        Task Scan(Item item);

        Task Scan(List<Item> item);

        List<Item> GetCartItems();
        int GetItemCount(char itemSku);
    }
}
