using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Repo.Model
{
    public class Item
    {
        public int Id { get; set; }
        public char SKU { get; set; }
        public int StockQty { get; set; }
        public double Price { get; set; }
    }
}
