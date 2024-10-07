using AutoMapper;
using LMT.Api.DTOs;
using LMT.Api.Entities;

namespace LMT.Api.MappingProfiles
{
    public class TaskPurposeProfile : Profile
    {
        public TaskPurposeProfile()
        {
            CreateMap<M_TaskPurposes, M_TaskPurposesDTO>().ReverseMap();
        }
    }
}
