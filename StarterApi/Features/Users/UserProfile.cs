using AutoMapper;
using StarterApi.Data.Entities;
using StarterApi.Dtos;

namespace StarterApi.Features.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<GetUserDto, User>().ReverseMap();
            CreateMap<CreateUserRequest, User>().ReverseMap();
        }
    }
}
