using AutoMapper;
using LMT.Api.DTOs;
using LMT.Api.Entities;

namespace LMT.Api.MappingProfiles
{
    public class StateProfile : Profile
    {
        public StateProfile()
        {
            CreateMap<M_States, M_StatesDTO>().ReverseMap();
        }
    }
    
}
