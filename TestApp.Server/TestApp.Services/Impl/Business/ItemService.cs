using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services.Contracts.Business;
using TestApp.Services.Contracts.Repository;
using TestApp.Services.Impl.Model;

namespace TestApp.Services.Impl.Business
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public async Task<Item> Get(char sku)
        {
            return await _itemRepository.Get(sku);
        }

        public async Task<List<Item>> Get(string skus)
        {
            return await _itemRepository.Get(skus);
        }
    }
}
