using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services.Contracts.Business;
using TestApp.Services.Impl.Model;

namespace TestApp.Services.Impl.Business
{
    public class CartService:ICartService
    {
        private readonly Cart _cart;

        public CartService()
        {
            _cart = new Cart();
        }
        public async Task Scan(Item item)
        {
            _cart.AddItem(item);
        }

        public List<Item> GetCartItems()
        {
            return _cart.GetCartItems();
        }

        public int GetItemCount(char itemSku)
        {
            return _cart.GetItemCount(itemSku);
        }


        public async Task Scan(List<Item> items)
        {
            foreach (var item in items)
            {
                Scan(item);
            }
        }

    }
}
