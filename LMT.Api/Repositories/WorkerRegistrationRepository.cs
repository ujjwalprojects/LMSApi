using LMT.Api.DTOs;
using LMT.Api.Interfaces;
using LMT.Api.Entities;
using LMT.Api.Data;
using Microsoft.EntityFrameworkCore;
using LMT.Api.IDGenerators;

namespace LMT.Api.Repositories
{
    public class WorkerRegistrationRepository : IWorkerRegistrationRepository
    {
        private readonly EFDBContext _dbContext;
        private readonly UniqueIdGeneratorForWorkers _uniqueIdGeneratorWorker;

        public WorkerRegistrationRepository(EFDBContext dbContext)
        {
            _dbContext = dbContext;
            _uniqueIdGeneratorWorker = new UniqueIdGeneratorForWorkers(_dbContext);
        }

        public async Task CreateWorkerRegistrationAsync(T_WorkerRegistrations workerRegistration)
        {

            workerRegistration.Worker_Reg_Id = _uniqueIdGeneratorWorker.GenerateUniqueId();
            try
            {
                _dbContext.T_WorkerRegistrations.Add(workerRegistration);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task DeleteWorkerRegistrationAsync(string workerRegistrationId)
        {
            var workerRegistration = await _dbContext.T_WorkerRegistrations.FindAsync(workerRegistrationId);
            if (workerRegistration != null)
            {
                _dbContext.T_WorkerRegistrations.Remove(workerRegistration);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<GetT_WorkerRegistrationDTO>> GetAllWorkerRegistrationsAsync()
        {
            var result = await _dbContext.GetT_WorkerRegistrationDTO
             .FromSqlRaw("EXEC [dbo].[SP_GetWorkerRegistrations]")
             .ToListAsync();

            return result;
            //return await _dbContext.T_WorkerRegistrations.ToListAsync();
        }

        public async Task<List<GetT_WorkerRegistrationDTO>> GetAllWorkerRegistrationsAsync(string? searchText)
        {
            var result = await _dbContext.GetT_WorkerRegistrationDTO
           .FromSqlRaw("EXEC [dbo].[SP_GetWorkerRegistrationsWithSearch] @SearchTerm = {0}", searchText ?? (object)DBNull.Value)
           .ToListAsync();
            return result;
        }

        public async Task<T_WorkerRegistrations?> GetWorkerRegistrationByIdAsync(string workerRegistrationId)
        {
            return await _dbContext.T_WorkerRegistrations.FindAsync(workerRegistrationId);
        }

        public async Task<IEnumerable<GetWorkerRegistrationCountDTO>> GetWorkerRegistrationCountDTO(string? userId)
        {
            var result = await _dbContext.GetWorkerRegistrationCountDTOs
            .FromSqlRaw("EXEC [dbo].[SP_GetWorkersRegistrationCountForDashboard] @UserID = {0}", userId ?? (object)DBNull.Value)
           .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<GetWorkerRegistrationReportDTO>> GetWorkerRegistrationReportDTO(string? estdId)
        {
            var result = await _dbContext.GetWorkerRegistrationReportDTOs
           .FromSqlRaw("EXEC [dbo].[SP_GetWorkersReport] @EstdId = {0}", estdId ?? (object)DBNull.Value)
           .ToListAsync();
            return result;
        }

        public async Task UpdateWorkerRegistrationAsync(T_WorkerRegistrations workerRegistration)
        {
            try
            {

                _dbContext.Entry(workerRegistration).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
