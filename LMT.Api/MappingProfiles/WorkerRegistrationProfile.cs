using AutoMapper;
using LMT.Api.DTOs;
using LMT.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMT.Api.MappingProfiles
{
    public class WorkerRegistrationProfile : Profile
    {
        public WorkerRegistrationProfile()
        {
            CreateMap<T_WorkerRegistrations, T_WorkerRegistrationsDTO>().ReverseMap();
        }
    }
}
