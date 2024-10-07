using AutoMapper;
using LMT.Api.DTOs;
using LMT.Api.Entities;

namespace LMT.Api.MappingProfiles
{
    public class TaskAllocationFormProfile : Profile
    {
        public TaskAllocationFormProfile()
        {
            CreateMap<T_TaskAllocationForms, T_TaskAllocationFormsDTO>().ReverseMap();
        }
    }
}
