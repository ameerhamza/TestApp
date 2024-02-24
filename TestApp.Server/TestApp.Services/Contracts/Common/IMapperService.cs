using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace TestApp.Services.Contracts.Common
{
    public interface IMapperService
    {
        void AddProfile(Profile profile);
        void Initialize();
        IMapper Mapper { get; }
    }
}
