using AutoMapper;

namespace TestApp.Repo.Model
{
    public class RepoMapperProfile : Profile
    {
        public RepoMapperProfile()
        {
            CreateMap<Services.Impl.Item, Item>()
                .ReverseMap(); // Th
        }
    }


}
