using LMT.Api.Interfaces;
using LMT.Api.Entities;
using LMT.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace LMT.Api.Repositories
{
    public class TaskAllocationSiteImageRepository : ITaskAllocationSiteImageRepository
    {
        private readonly EFDBContext _dbContext;

        public TaskAllocationSiteImageRepository(EFDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateTaskAllocationSiteImageAsync(T_TaskAllocationSiteImages taskAllocationSiteImage)
        {
            _dbContext.T_TaskAllocationSiteImages.Add(taskAllocationSiteImage);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTaskAllocationSiteImageAsync(int taskAllocationSiteImageId)
        {
            var taskAllocationSiteImage = await _dbContext.T_TaskAllocationSiteImages.FindAsync(taskAllocationSiteImageId);
            if (taskAllocationSiteImage != null)
            {
                _dbContext.T_TaskAllocationSiteImages.Remove(taskAllocationSiteImage);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<T_TaskAllocationSiteImages>> GetAllTaskAllocationSiteImagesAsync()
        {
            return await _dbContext.T_TaskAllocationSiteImages.ToListAsync();
        }

        public async Task<T_TaskAllocationSiteImages> GetTaskAllocationSiteImageByIdAsync(int taskAllocationSiteImageId)
        {
            return await _dbContext.T_TaskAllocationSiteImages.FindAsync(taskAllocationSiteImageId);
        }

        public async Task UpdateTaskAllocationSiteImageAsync(T_TaskAllocationSiteImages taskAllocationSiteImage)
        {
            _dbContext.Entry(taskAllocationSiteImage).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
