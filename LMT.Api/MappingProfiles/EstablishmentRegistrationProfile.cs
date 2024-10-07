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
    public class EstablishmentRegistrationProfile:Profile
    {
        public EstablishmentRegistrationProfile()
        {
            CreateMap<T_EstablishmentRegistrations, T_EstablishmentRegistrationsDTO>().ReverseMap();
            CreateMap<T_EstablishmentRegistrations, GetT_EstablishmentDTO>().ReverseMap();
        }
    }
}
