using LMT.Api.Entities;

namespace LMT.Api.Interfaces
{
    public interface IDistrictRepository
    {
        Task<List<M_Districts>> GetAllDistrictsAsync();
        Task<M_Districts> GetDistrictByIdAsync(int districtId);
        Task CreateDistrictAsync(M_Districts district);
        Task UpdateDistrictAsync(M_Districts district);
        Task DeleteDistrictAsync(int districtId);
    }

}
