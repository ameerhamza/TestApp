using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TestApp.Repo.DataStores;
using TestApp.Services.Contracts;
using TestApp.Services.Exceptions;
using TestApp.Services.Impl;

namespace TestApp.Repo.Repositories
{
    

    public class PersonRepository : IPersonRepository
    {
        private readonly IDataStore<Person> _dataStore;
        private readonly IMapper _mapper;

        public PersonRepository(IDataStore<Person> dataStore, IMapper mapper)
        {
            _dataStore = dataStore;
            _mapper = mapper;
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

            await _dataStore.AddAsync(_mapper.Map<Person>(person));
            return await Task.FromResult(person);
        }
    }

}
