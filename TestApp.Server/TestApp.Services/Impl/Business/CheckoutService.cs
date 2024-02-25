using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services.Contracts.Business;
using TestApp.Services.Contracts.Repository;
using TestApp.Services.Impl.Construction;
using TestApp.Services.Impl.Model;

namespace TestApp.Services.Impl.Business
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ICartService _cartService;
        private readonly IRuleRepository _ruleRepository;
      
        private List<CartRule> _rules;
        private double? _total;

        public CheckoutService(ICartService cartService, IRuleRepository ruleRepository)
        {
            _cartService = cartService;
            _ruleRepository = ruleRepository;
        }


       
        public async Task<double> PriceAsync(string userId)
        {
            if(_rules == null)
                _rules = await _ruleRepository.Get();

            var total = 0.0;
            _cartService.GetCartItems(userId).ForEach(item =>
            {
                var qty = _cartService.GetItemCount(item.SKU, userId);
                var rules = _rules.Where(x => x.SKU == item.SKU).ToList();
                var ruleOperation = RuleOperationFactory.CreateOperation(rules, qty);

                total += ruleOperation.EvaluateRule(item.Price, qty);
            });

            return total;
        }

        public async Task<double> TotalAsync(string userId)
        {
            _total = await PriceAsync(userId);

            return _total.Value;
        }
    }
}
