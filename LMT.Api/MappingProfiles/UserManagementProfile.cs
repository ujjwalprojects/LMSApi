using AutoMapper;
using LMT.Api.DTOs;
using Microsoft.AspNetCore.Identity;

namespace LMT.Api.MappingProfiles
{
    public class UserManagementProfile:Profile
    {
        public UserManagementProfile() {
            CreateMap<IdentityUser, UserListDTO>()
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
