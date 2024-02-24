using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services.Contracts.Common;

namespace TestApp.Services.Impl
{
    public class AutoMapperService: IMapperService
    {
        private IMapper _mapper;
        private MapperConfigurationExpression _baseConfig;
        public AutoMapperService()
        {
            _baseConfig = new MapperConfigurationExpression();
        }

        public void AddProfile(Profile profile)
        {
            _baseConfig.AddProfile(profile);
        }
        public  void Initialize()
        {
            var config = new MapperConfiguration(_baseConfig);
            _mapper = config.CreateMapper();
        }

        public IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    Initialize();
                }

                return _mapper;
            }
        }
    }
}
