using AutoMapper;
using TestApp.Services.Contracts;

namespace TestApp.API.Model
{
    

    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<Person, IPerson>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ConstructUsing(src => IPerson.Create(src.FirstName, src.LastName))

                .ReverseMap(); // Th
        }
    }

}
