using AutoMapper;
using ForumsService.Models;
using ForumsService.Dtos;
using UserService;

namespace ForumsService.Profiles
{
    public class ForumsProfile : Profile
    {
        public ForumsProfile()
        {
            //Source -> target
            CreateMap<User, UserReadDto>();
            CreateMap<ForumCreateDto, Forum>();
            CreateMap<Forum, ForumReadDto>();
            CreateMap<UserPublishedDto, User>()
                .ForMember(destination =>destination.ExternalID, opt => opt.MapFrom(source => source.Id));
            CreateMap<GrpcUserModel, User>()
            .ForMember(destination => destination.ExternalID, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Forums, opt =>opt.Ignore());


        }
    }
}