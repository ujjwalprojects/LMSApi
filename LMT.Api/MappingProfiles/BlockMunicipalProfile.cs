using AutoMapper;
using LMT.Api.DTOs;
using LMT.Api.Entities;

namespace LMT.Api.MappingProfiles
{
    public class BlockMunicipalProfile:Profile
    {
        public BlockMunicipalProfile()
        {
            CreateMap<M_BlockMunicipals, M_BlockMunicipalsDTO>().ReverseMap();
        }
    }
}
