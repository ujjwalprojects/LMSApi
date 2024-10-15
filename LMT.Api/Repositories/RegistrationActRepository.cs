using LMT.Api.Interfaces;
using LMT.Api.Entities;
using LMT.Api.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMT.Api.Repositories
{
    public class RegistrationActRepository : IRegistrationActRepository
    {
        private readonly EFDBContext _dbContext;

        public RegistrationActRepository(EFDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateRegistrationActAsync(M_RegistrationActs registrationAct)
        {
            _dbContext.M_RegistrationActs.Add(registrationAct);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRegistrationActAsync(int registrationActId)
        {
            var registrationAct = await _dbContext.M_RegistrationActs.FindAsync(registrationActId);
            if (registrationAct != null)
            {
                _dbContext.M_RegistrationActs.Remove(registrationAct);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<M_RegistrationActs>> GetAllRegistrationActsAsync()
        {
            return await _dbContext.M_RegistrationActs.ToListAsync();
        }

        public async Task<M_RegistrationActs> GetRegistrationActByIdAsync(int registrationActId)
        {
            return await _dbContext.M_RegistrationActs.FindAsync(registrationActId);
        }

        public async Task UpdateRegistrationActAsync(M_RegistrationActs registrationAct)
        {
            try
            {
                _dbContext.Entry(registrationAct).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
