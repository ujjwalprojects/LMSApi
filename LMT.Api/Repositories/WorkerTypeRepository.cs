using LMT.Api.Interfaces;
using LMT.Api.Entities;
using LMT.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace LMT.Api.Repositories
{
    public class WorkerTypeRepository : IWorkerTypeRepository
    {
        private readonly EFDBContext _dbContext;

        public WorkerTypeRepository(EFDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateWorkerTypeAsync(M_WorkerTypes workerType)
        {
            _dbContext.M_WorkerTypes.Add(workerType);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteWorkerTypeAsync(int workerTypeId)
        {
            var workerType = await _dbContext.M_WorkerTypes.FindAsync(workerTypeId);
            if (workerType != null)
            {
                _dbContext.M_WorkerTypes.Remove(workerType);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<M_WorkerTypes>> GetAllWorkerTypesAsync()
        {
            return await _dbContext.M_WorkerTypes.ToListAsync();
        }

        public async Task<M_WorkerTypes> GetWorkerTypeByIdAsync(int workerTypeId)
        {
            return await _dbContext.M_WorkerTypes.FindAsync(workerTypeId);
        }

        public async Task UpdateWorkerTypeAsync(M_WorkerTypes workerType)
        {
            _dbContext.Entry(workerType).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
