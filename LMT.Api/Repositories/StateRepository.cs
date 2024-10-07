using LMT.Api.Interfaces;
using LMT.Api.Entities;
using LMT.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace LMT.Api.Repositories
{
    public class StateRepository : IStateRepository
    {
        private readonly EFDBContext _dbContext;

        public StateRepository(EFDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateStateAsync(M_States state)
        {
            _dbContext.M_States.Add(state);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteStateAsync(int stateId)
        {
            var state = await _dbContext.M_States.FindAsync(stateId);
            if (state != null)
            {
                _dbContext.M_States.Remove(state);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<M_States>> GetAllStatesAsync()
        {
            return await _dbContext.M_States.ToListAsync();
        }

        public async Task<M_States> GetStateByIdAsync(int stateId)
        {
            return await _dbContext.M_States.FindAsync(stateId);
        }

        public async Task UpdateStateAsync(M_States state)
        {
            _dbContext.Entry(state).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
