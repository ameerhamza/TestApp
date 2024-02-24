using AutoMapper;

namespace TestApp.Repo.Model
{
    public class RepoMapperProfile : Profile
    {
        public RepoMapperProfile()
        {
            CreateMap<Services.Impl.Model.Item, Item>()
                .ReverseMap(); // Th
            CreateMap<Services.Impl.Model.CartRule, CartRule>()
                .ReverseMap(); // Th
        }
    }


}
