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
        Task Scan(Item item, string userId);

        Task Scan(List<Item> item, string userId);

        List<Item> GetCartItems(string userId);
        int GetItemCount(char itemSku, string userId);
        void ClearCart(string userId);
    }
}
