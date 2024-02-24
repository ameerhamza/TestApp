using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Services.Contracts.Model
{
    public interface IItem
    {
        char SKU { get; set; }
        int Qty { get; set; }
        double Price { get; set; }
    }
}
