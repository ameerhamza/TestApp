using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services.Impl;

namespace TestApp.Services.Contracts
{
    public interface IPerson
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }

        static IPerson Create(string firstName, string lastName)
        {
            return new Person
            {
                FirstName = firstName,
                LastName = lastName,
            };
        }
    }

}
