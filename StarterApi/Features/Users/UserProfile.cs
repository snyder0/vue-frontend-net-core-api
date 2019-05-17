using AutoMapper;
using StarterApi.Common.Constants;
using StarterApi.Data.Entities;
using StarterApi.Dtos;

namespace StarterApi.Features.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<GetUserDto, User>().ReverseMap();

            CreateMap<CreateUserRequest, User>()
                .ForMember(dest => dest.Role, opts => opts.Ignore())
                .ForMember(dest => dest.PasswordSalt, opts => opts.Ignore())
                .ForMember(dest => dest.PasswordHash, opts => opts.Ignore());
        }
    }
}
