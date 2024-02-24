using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services.Contracts.Business;
using TestApp.Services.Impl.Business;
using TestApp.Services.Impl.Common;
using TestApp.Services.Impl.Model;

namespace TestApp.Services.Impl.Construction
{
    public static class RuleOperationFactory
    {
        public static IRuleOperation CreateOperation(List<CartRule> rules, int qty)
        {
            var orderedRules = rules.OrderBy(x => x.Priority);


            foreach (var rule in orderedRules)
            {
                if (rule.RuleType == RuleTypeEnum.Multiples && qty >= rule.Multiple)
                    return new MultiplesRuleOperation(rule);
                else if (rule.RuleType == RuleTypeEnum.Discount && qty >= rule.StartQty)
                    return new DiscountRuleOperation(rule);
            }

            return new NoDiscountRuleOperation();
        }
    }
}
