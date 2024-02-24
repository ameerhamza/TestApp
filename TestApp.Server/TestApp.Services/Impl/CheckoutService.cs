using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services.Contracts.Business;
using TestApp.Services.Contracts.Repository;

namespace TestApp.Services.Impl
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IRuleRepository _ruleRepository;
        private readonly Cart _cart;
        private List<CartRule> Rules;

        public CheckoutService(IRuleRepository ruleRepository)
        {
            _ruleRepository = ruleRepository;
            _cart = new Cart();
        }
        

        public void Scan(Item item)
        {
            _cart.AddItem(item);
        }
        public void Scan(List<Item> items)
        {
            foreach (var item in items)
            {
                Scan(item);
            }
        }

        public async Task<double> GetTotal()
        {
            Rules ??= await _ruleRepository.Get();
            var total = 0.0;
            _cart.GetCartItems().ForEach(item =>
            {
                var count = _cart.GetItemCount(item.SKU);

                var rule = Rules.Where(x => x.SKU == item.SKU && x.StartQty < count)
                    .MaxBy(x => x.Discount);
                
                var discount = rule?.Discount ?? 0;
                var price = item.Price * count;

                total += price - discount * price;
            });

            return total;
        }
    }
}
