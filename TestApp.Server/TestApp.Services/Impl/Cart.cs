using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Services.Impl
{
    public class Cart
    {
        private Dictionary<char, int> itemQtyDictionary = new();
        private Dictionary<char, Item> itemDictionary = new();

        public void AddItem(Item item)
        {
            if (!itemDictionary.TryGetValue(item.SKU, out var localItem))
            {
                itemDictionary.Add(item.SKU, item);
            }

            itemQtyDictionary.TryGetValue(item.SKU, out var count);
            itemQtyDictionary[item.SKU] = count + 1;
        }

        public List<Item> GetCartItems()
        {
            return itemDictionary.Values.ToList();
        }

        public int GetItemCount(char itemSku)
        {
            return itemQtyDictionary[itemSku];
        }
    }
}
