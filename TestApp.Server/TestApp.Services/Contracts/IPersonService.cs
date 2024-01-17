using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Services.Contracts
{
    public interface IPersonService
    {
        Task<IPerson> AddPersonAsync(IPerson person);
    }

}
