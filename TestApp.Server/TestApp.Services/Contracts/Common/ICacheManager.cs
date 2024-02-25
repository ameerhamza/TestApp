using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Services.Contracts.Common
{
    public interface ICacheManager <T>
    {
        void AddOrUpdate(string key, T value);
        bool TryGetValue(string key, out T value);
        T Get(string key);
        bool Remove(string key);
    }

}
