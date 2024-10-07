using AutoMapper;
using LMT.Api.DTOs;
using LMT.Api.Entities;

namespace LMT.Api.MappingProfiles
{
    public class RegistrationActProfile : Profile
    {
        public RegistrationActProfile()
        {
            CreateMap<M_RegistrationActs, M_RegistrationActsDTO>().ReverseMap();
        }
    }
}
