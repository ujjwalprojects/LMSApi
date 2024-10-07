using AutoMapper;
using LMT.Api.DTOs;
using LMT.Api.Entities;

namespace LMT.Api.MappingProfiles
{
    public class WorkerTypeProfile : Profile
    {
        public WorkerTypeProfile()
        {
            CreateMap<M_WorkerTypes, M_WorkerTypesDTO>().ReverseMap();
        }
    }
}
