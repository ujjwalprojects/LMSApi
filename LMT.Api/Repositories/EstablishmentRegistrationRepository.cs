using LMT.Api.Interfaces;
using LMT.Api.Entities;
using LMT.Api.Data;
using Microsoft.EntityFrameworkCore;
using LMT.Api.DTOs;

namespace LMT.Api.Repositories
{
    public class EstablishmentRegistrationRepository : IEstablishmentRegistrationRepository
    {
        private readonly EFDBContext _dbContext;

        public EstablishmentRegistrationRepository(EFDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateEstablishmentRegistrationAsync(T_EstablishmentRegistrations establishmentRegistration)
        {
            _dbContext.T_EstablishmentRegistrations.Add(establishmentRegistration);
            await _dbContext.SaveChangesAsync();

        }

        public async Task DeleteEstablishmentRegistrationAsync(int establishmentRegistrationId)
        {
            var establishmentRegistration = await _dbContext.T_EstablishmentRegistrations.FindAsync(establishmentRegistrationId);
            if (establishmentRegistration != null)
            {
                _dbContext.T_EstablishmentRegistrations.Remove(establishmentRegistration);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<T_EstablishmentRegistrations>> GetAllEstablishmentRegistrationsAsync()
        {
            return await _dbContext.T_EstablishmentRegistrations.ToListAsync();
        }

        public async Task<List<T_EstablishmentRegistrations>> GetAllEstablishmentRegistrationsAsync(string? searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return await _dbContext.T_EstablishmentRegistrations.ToListAsync();
            }
            return await _dbContext.T_EstablishmentRegistrations
                .Where(c => c.Estd_Name.Contains(searchText) || 
                c.Estd_Reg_No!.Contains(searchText) || 
                c.Estd_TradeLicense_No!.Contains(searchText) ||
                c.Estd_Owner_Name.Contains(searchText) ||
                c.Estd_Contact_No.Contains(searchText)
                ).ToListAsync();
        }

        public async Task<T_EstablishmentRegistrations?> GetEstablishmentRegistrationByIdAsync(int establishmentRegistrationId)
        {
            return await _dbContext.T_EstablishmentRegistrations.FindAsync(establishmentRegistrationId);
        }

        public async Task<IEnumerable<GetEstablishmentCountDTO>> GetEstablishmentCountDTOs(string? userId)
        {
            var result =  await _dbContext.GetEstablishmentCountDTOs
            .FromSqlRaw("EXEC [dbo].[SP_GetEstablishmentCountForDashboard] @UserID = {0}", userId ?? (object)DBNull.Value)
           .ToListAsync();
            return result;
        }

        public async Task UpdateEstablishmentRegistrationAsync(T_EstablishmentRegistrations establishmentRegistration)
        {
            _dbContext.Entry(establishmentRegistration).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetEstablishmentRegistrationReportDTO>> GetEstablishmentRegistrationReportDTO(int? distId)
        {
            var result = await _dbContext.GetEstablishmentRegistrationReportDTOs
              .FromSqlRaw("EXEC [dbo].[SP_GetEstablishmentReport]  @DistrictId = {0}"
                   , distId ?? (object)DBNull.Value)
              .ToListAsync();
            return result;
        }
    }
}
