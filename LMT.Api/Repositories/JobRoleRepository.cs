using LMT.Api.Interfaces;
using LMT.Api.Entities;
using LMT.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace LMT.Api.Repositories
{
    public class JobRoleRepository : IJobRoleRepository
    {
        private readonly EFDBContext _dbContext;

        public JobRoleRepository(EFDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateJobRoleAsync(M_JobRoles jobRole)
        {
            _dbContext.M_JobRoles.Add(jobRole);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteJobRoleAsync(int jobRoleId)
        {
            var jobRole = await _dbContext.M_JobRoles.FindAsync(jobRoleId);
            if (jobRole != null)
            {
                
                    _dbContext.M_JobRoles.Remove(jobRole);
                    await _dbContext.SaveChangesAsync();

            }
        }

        public async Task<List<M_JobRoles>> GetAllJobRolesAsync()
        {
            return await _dbContext.M_JobRoles.ToListAsync();
        }

        public async Task<M_JobRoles> GetJobRoleByIdAsync(int jobRoleId)
        {
            return await _dbContext.M_JobRoles.FindAsync(jobRoleId);
        }

        public async Task UpdateJobRoleAsync(M_JobRoles jobRole)
        {
            _dbContext.Entry(jobRole).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
