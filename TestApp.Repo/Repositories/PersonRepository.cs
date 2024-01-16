using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Repo.DataStores;
using TestApp.Services.Contracts;
using TestApp.Services.Exceptions;
using TestApp.Services.Impl;

namespace TestApp.Repo.Repositories
{
    

    public class PersonRepository : IPersonRepository
    {
        private readonly IDataStore<IPerson> _dataStore;

        public PersonRepository(IDataStore<IPerson> dataStore)
        {
            _dataStore = dataStore;
        }
     
        public async Task<IPerson> AddPersonAsync(IPerson person)
        {
            var result = await _dataStore.SearchAsync(p => 
                p.FirstName.Equals(person.FirstName, StringComparison.OrdinalIgnoreCase) &&
                p.LastName.Equals(person.LastName, StringComparison.OrdinalIgnoreCase));

            if (result.Any())
            {
                throw new AlreadyExistsException($"Person with name {person.FirstName} {person.LastName} already exists");
            }

            await _dataStore.AddAsync(person);
            return await Task.FromResult(person);
        }
    }

}
