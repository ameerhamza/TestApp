using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services.Contracts.Business;
using TestApp.Services.Impl.Model;

namespace TestApp.Services.Impl.Business
{
    public class NoDiscountRuleOperation : IRuleOperation
    {
        public NoDiscountRuleOperation()
        {
            
        }
        public double EvaluateRule(double price, int qty)
        {
            return price * qty;
        }
    }
}
