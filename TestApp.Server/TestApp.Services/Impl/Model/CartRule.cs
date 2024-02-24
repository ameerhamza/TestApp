using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services.Impl.Common;

namespace TestApp.Services.Impl.Model
{
    public class CartRule
    {
        public char SKU { get; set; }
        public int StartQty { get; set; }
        public int EndQty { get; set; }
        public double? Discount { get; set; }
        public int? Multiple { get; set; }
        public int? MultipleDiscount { get; set; }
        public int Priority { get; set; }
        public RuleTypeEnum RuleType { get; set; }
    }
}
