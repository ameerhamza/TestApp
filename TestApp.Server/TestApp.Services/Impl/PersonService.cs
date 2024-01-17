using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services.Contracts;

namespace TestApp.Services.Impl
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<IPerson> AddPersonAsync(IPerson person)
        {
            return await _personRepository.AddPersonAsync(person);
        }
    }

}
