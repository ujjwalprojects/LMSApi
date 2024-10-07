using LMT.Api.Entities;

namespace LMT.Api.Interfaces
{
    public interface IBlockMunicipalRepository
    {
        Task<List<M_BlockMunicipals>> GetAllBlockMunicipalAsync();
        Task<M_BlockMunicipals> GetBlockMunicipalByIdAsync(int blockMunicipalId);
        Task CreateBlockMunicipalAsync(M_BlockMunicipals blockMunicipal);
        Task UpdateBlockMunicipalAsync(M_BlockMunicipals blockMunicipal);
        Task DeleteBlockMunicipalAsync(int blockMunicipalId);
    }
}
