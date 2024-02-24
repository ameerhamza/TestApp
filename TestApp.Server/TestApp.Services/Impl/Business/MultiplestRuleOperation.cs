using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services.Contracts.Business;
using TestApp.Services.Impl.Model;

namespace TestApp.Services.Impl.Business
{
    public class MultiplesRuleOperation: IRuleOperation
    {
        private CartRule _rule { get; set; }

        public MultiplesRuleOperation(CartRule rule)
        {
            _rule = rule;
        }
        public double EvaluateRule(double price, int qty)
        {
            if (_rule.Multiple.HasValue == false || _rule.MultipleDiscount.HasValue == false) 
                throw new Exception("Invalid Rule");
            
            if(qty < _rule.Multiple.Value) return price * qty;
            

            var discountedItemCount = qty / _rule.Multiple.Value;

            return price * (qty - discountedItemCount) 
                   + (price - _rule.MultipleDiscount.Value) * discountedItemCount;
        }

        
    }
}
