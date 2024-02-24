using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services.Impl.Model;

namespace TestApp.Services.Contracts.Business
{
    public interface IRuleOperation
    {
        double EvaluateRule(double price, int qty);
    }
}
