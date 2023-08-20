using AutoMapper;
using CommentsService.Models;
using CommentsService.Dtos;
using ForumsService;

namespace CommentsService.Profiles
{
    public class CommentsProfile : Profile
    {
        public CommentsProfile()
        {
            //Source -> target
            CreateMap<Forum, ForumReadDto>();
            CreateMap<CommentCreateDto, Comment>();
            CreateMap<Comment, CommentReadDto>();
            CreateMap<ForumPublishedDto, Forum>()
                .ForMember(destination =>destination.ExternalID, opt => opt.MapFrom(source => source.Id));
            CreateMap<GrpcForumModel, Forum>()
            .ForMember(destination => destination.ExternalID, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Comments, opt =>opt.Ignore());


        }
    }
}