using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Repo.DataStores;
using TestApp.Repo.Model;
using TestApp.Services.Contracts.Common;
using TestApp.Services.Contracts.Repository;

namespace TestApp.Repo.Repositories
{
    public class RuleRepository: IRuleRepository
    {
        private readonly IDataStore<Repo.Model.CartRule> _ruleDataStore;
        private readonly IMapperService _mapperService;

        public RuleRepository(IDataStore<Repo.Model.CartRule> ruleDataStore, IMapperService mapperService)
        {
            this._ruleDataStore = ruleDataStore;
            _mapperService = mapperService;
        }

        public async Task<List<Services.Impl.Model.CartRule>> Get()
        {
            var rules = await _ruleDataStore.GetAllAsync();
            return _mapperService.Mapper.Map<List<Services.Impl.Model.CartRule>>(rules);
        }

        public async Task<Services.Impl.Model.CartRule> Get(char SKU)
        {
            var rules = await _ruleDataStore.FirstOrDefaultAsync(x => x.SKU == SKU);
            return _mapperService.Mapper.Map<Services.Impl.Model.CartRule>(rules);
        }
    }
}
