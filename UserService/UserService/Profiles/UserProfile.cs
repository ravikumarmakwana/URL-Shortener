using AutoMapper;
using UserService.Entities;
using UserService.Models;

namespace UserService.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegistrationRequest, User>();
            CreateMap<User, UserRegistrationResponse>();

            CreateMap<User, AuthenticationResponse>();
            CreateMap<User, TokenResponse>();
        }
    }
}
