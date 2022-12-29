using AutoMapper;
using Clips.Bll.Dtos;
using Clips.Models;

namespace Clips.Bll.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterUserDto, User>();
            CreateMap<LoginUserDto, User>();
        }
    }
}
