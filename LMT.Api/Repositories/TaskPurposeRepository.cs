using LMT.Api.Interfaces;
using LMT.Api.Entities;
using LMT.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace LMT.Api.Repositories
{
    public class TaskPurposeRepository : ITaskPurposeRepository
    {
        private readonly EFDBContext _dbContext;

        public TaskPurposeRepository(EFDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateTaskPurposeAsync(M_TaskPurposes taskPurpose)
        {
            _dbContext.M_TaskPurposes.Add(taskPurpose);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTaskPurposeAsync(int taskPurposeId)
        {
            var taskPurpose = await _dbContext.M_TaskPurposes.FindAsync(taskPurposeId);
            if (taskPurpose != null)
            {
                _dbContext.M_TaskPurposes.Remove(taskPurpose);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<M_TaskPurposes>> GetAllTaskPurposesAsync()
        {
            return await _dbContext.M_TaskPurposes.ToListAsync();
        }

        public async Task<M_TaskPurposes> GetTaskPurposeByIdAsync(int taskPurposeId)
        {
            return await _dbContext.M_TaskPurposes.FindAsync(taskPurposeId);
        }

        public async Task UpdateTaskPurposeAsync(M_TaskPurposes taskPurpose)
        {
            _dbContext.Entry(taskPurpose).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
