using LMT.Api.Interfaces;
using LMT.Api.Entities;
using LMT.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace LMT.Api.Repositories
{
    public class DistrictRepository : IDistrictRepository
    {
        private readonly EFDBContext _dbContext;

        public DistrictRepository(EFDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateDistrictAsync(M_Districts district)
        {
            _dbContext.M_Districts.Add(district);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteDistrictAsync(int districtId)
        {
            var district = await _dbContext.M_Districts.FindAsync(districtId);
            if (district != null)
            {
                _dbContext.M_Districts.Remove(district);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<M_Districts>> GetAllDistrictsAsync()
        {
            return await _dbContext.M_Districts.ToListAsync();
        }

        public async Task<M_Districts> GetDistrictByIdAsync(int districtId)
        {
            return await _dbContext.M_Districts.FindAsync(districtId);
        }

        public async Task UpdateDistrictAsync(M_Districts district)
        {
            _dbContext.Entry(district).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
