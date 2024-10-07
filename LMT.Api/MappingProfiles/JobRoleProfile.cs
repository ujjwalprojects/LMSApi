using AutoMapper;
using LMT.Api.DTOs;
using LMT.Api.Entities;

namespace LMT.Api.MappingProfiles
{
    public class JobRoleProfile:Profile
    {
        public JobRoleProfile()
        {
            CreateMap<M_JobRoles, M_JobRolesDTO>().ReverseMap();
        }
    }
}
