using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services.Contracts.Business;
using TestApp.Services.Impl.Model;

namespace TestApp.Services.Impl.Business
{
    public class DiscountRuleOperation: IRuleOperation
    {
        private CartRule _rule { get; set; }

        public DiscountRuleOperation(CartRule rule)
        {
            _rule = rule;
        }

        public double EvaluateRule(double price, int qty)
        {
            if (_rule.Discount.HasValue == false) throw new Exception("Invalid Rule");
            var discount = price * qty * _rule.Discount;

            return price * qty - discount.Value;
        }
    }
}
