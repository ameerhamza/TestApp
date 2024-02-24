using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestApp.Repo.DataStores
{
    public class JsonDataStore<T> : IDataStore<T>
    {
        private readonly string _filePath;
        private List<T> _items;

        public JsonDataStore(string basePath, string fileName)
        {
            _filePath = Path.Combine(basePath, fileName);
            _items = new List<T>();
            LoadDataAsync().Wait(); 
        }

        public async Task<IEnumerable<T>> GetItemsAsync(Func<T, bool> condition)
        {
            return _items.Where(condition);
        }

        public async Task<T?> FirstOrDefaultAsync(Func<T, bool> condition)
        {
            return _items.FirstOrDefault(condition);
        }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return _items.ToList();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return _items.FirstOrDefault(item => GetItemId(item) == id);
        }

        public async Task<IEnumerable<T>> SearchAsync(Func<T, bool> predicate)
        {
            return _items.Where(predicate).ToList();
        }

        public async Task<T> AddAsync(T item)
        {
            SetItemId(item, _items.Count + 1);
            _items.Add(item);
            await SaveDataAsync();
            return item;
        }

        public async Task<bool> UpdateAsync(int id, T updatedItem)
        {
            var existingItem = _items.FirstOrDefault(item => GetItemId(item) == id);
            if (existingItem != null)
            {
                // Update existing item properties
                UpdateItemProperties(existingItem, updatedItem);
                await SaveDataAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var itemToRemove = _items.FirstOrDefault(item => GetItemId(item) == id);
            if (itemToRemove != null)
            {
                _items.Remove(itemToRemove);
                await SaveDataAsync();
                return true;
            }

            return false;
        }

        private async Task LoadDataAsync()
        {
            if (File.Exists(_filePath))
            {
                using (var fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var reader = new StreamReader(fileStream))
                    {
                        var jsonData = await reader.ReadToEndAsync();
                        _items = JsonConvert.DeserializeObject<List<T>>(jsonData) ?? new List<T>();
                    }
                }
            }
        }

        private async Task SaveDataAsync()
        {
            using (var fileStream = new FileStream(_filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                using (var writer = new StreamWriter(fileStream))
                {
                    var jsonData = JsonConvert.SerializeObject(_items);
                    await writer.WriteAsync(jsonData);
                }
            }
        }

        // Abstract methods that depend on the structure of your data
        protected virtual int GetItemId(T item) => 0;
        protected virtual void SetItemId(T item, int id) { }
        protected virtual void UpdateItemProperties(T existingItem, T updatedItem) { }
    }
}
