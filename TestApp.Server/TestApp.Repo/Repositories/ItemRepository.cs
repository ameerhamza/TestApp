using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Repo.DataStores;
using TestApp.Services.Contracts.Common;
using TestApp.Services.Contracts.Repository;
using TestApp.Services.Impl.Model;

namespace TestApp.Repo.Repositories
{
    public class ItemRepository: IItemRepository
    {
        private readonly IDataStore<Repo.Model.Item> _itemDataStore;
        private readonly IMapperService _mapperService;

        public ItemRepository(IDataStore<Repo.Model.Item> itemDataStore, IMapperService mapperService)
        {
            _itemDataStore = itemDataStore;
            _mapperService = mapperService;
        }
        public async Task<Item> Get(char sku)
        {
            var item =  await _itemDataStore.FirstOrDefaultAsync(x => x.SKU == sku);
            return _mapperService.Mapper.Map<Item>(item);
        }

        public async Task<List<Item>> Get(string skus)
        {
            var items =
                from i in (await _itemDataStore.GetAllAsync())
                join sk in skus on i.SKU equals sk
                select i;

            return _mapperService.Mapper.Map<List<Item>>(items);
        }
    }
}
