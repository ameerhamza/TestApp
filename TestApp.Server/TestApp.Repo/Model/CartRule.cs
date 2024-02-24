using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Repo.Model
{
    public class CartRule
    {
        public int Id { get; set; }
        public char SKU { get; set; }
        public int StartQty { get; set; }
        public int EndQty { get; set; }
        public double Discount { get; set; }
    }
}
