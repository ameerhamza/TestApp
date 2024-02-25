using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services.Contracts.Business;
using TestApp.Services.Contracts.Common;
using TestApp.Services.Contracts.Model;
using TestApp.Services.Impl.Model;

namespace TestApp.Services.Impl.Business
{
    public class CartService:ICartService
    {
        public ICacheManager<Cart> _cacheManager { get; }
        private string _baseKey = "CartService";
        public CartService(ICacheManager<Cart> cacheManager)
        {
            _cacheManager = cacheManager;
        }

        private Cart GetUserCart(string userId)
        {
            var key = $"{_baseKey}{userId}";
            if (!_cacheManager.TryGetValue(key, out var cart))
            {
                cart = new Cart();
                _cacheManager.AddOrUpdate(key, cart);
            }

            return cart;
        }
        public async Task Scan(Item item, string userId)
        {
            var cart = GetUserCart(userId);
            cart.AddItem(item);
        }

        public List<Item> GetCartItems(string userId)
        {
            var cart = GetUserCart(userId);
            return cart.GetCartItems();
        }

        public int GetItemCount(char itemSku, string userId)
        {
            var cart = GetUserCart(userId);
            return cart.GetItemCount(itemSku);
        }

        public void ClearCart(string userId)
        {
            var key = $"{_baseKey}{userId}";
            _cacheManager.Remove(key);
        }


        public async Task Scan(List<Item> items, string userId)
        {
            foreach (var item in items)
            {
                Scan(item, userId);
            }
        }

    }
}
