using AutoMapper;
using LMT.Api.DTOs;
using LMT.Api.Entities;

namespace LMT.Api.MappingProfiles
{
    public class TaskAllocationSiteImageProfile : Profile
    {
        public TaskAllocationSiteImageProfile()
        {
            CreateMap<T_TaskAllocationSiteImages, T_TaskAllocationSiteImagesDTO>().ReverseMap();
        }
    }
}
