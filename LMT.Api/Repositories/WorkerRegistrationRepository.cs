using LMT.Api.DTOs;
using LMT.Api.Interfaces;
using LMT.Api.Entities;
using LMT.Api.Data;
using Microsoft.EntityFrameworkCore;
using LMT.Api.IDGenerators;
using LMT.Api.DTOs.Paging;
using Microsoft.Data.SqlClient;
using System.Data;

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

            _dbContext.T_WorkerRegistrations.Add(workerRegistration);
            await _dbContext.SaveChangesAsync();
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

        public async Task<PaginatedResult<GetT_WorkerRegistrationDTO>> GetWorkerWithPagingAsync(string? userId, string? searchText, int pageNumber = 1, int pageSize = 10)
        {
            var totalRecordCountParam = new SqlParameter("@TotalRecordCount", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            var items = await _dbContext.GetT_WorkerRegistrationDTO
             .FromSqlRaw("EXEC SP_GetWorkerRegistrationsWithPaging @UserId = {0}, @SearchText = {1}, " +
             "@PageNumber = {2}, @PageSize = {3}, @TotalRecordCount = {4} OUTPUT",

             userId ?? (object)DBNull.Value, searchText ?? (object)DBNull.Value,
             pageNumber, pageSize, totalRecordCountParam)

             .ToListAsync();

            int totalRecordCount = (int)(totalRecordCountParam.Value ?? 0);

            return new PaginatedResult<GetT_WorkerRegistrationDTO>(items, totalRecordCount, pageNumber, pageSize);
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

        public async Task<IEnumerable<GetWorkerRegistrationReportDTO>> GetWorkerRegistrationReportDTO(int? estdId, int? distId, string? independentWorker)
        {

            var result = await _dbContext.GetWorkerRegistrationReportDTOs
              .FromSqlRaw("EXEC [dbo].[SP_GetWorkersReport] @EstdId = {0} , @DistrictId = {1}, @IsIndependentWorker = {2}"
                   , estdId ?? (object)DBNull.Value
                   , distId ?? (object)DBNull.Value
                   , independentWorker ?? (object)DBNull.Value
                   ).ToListAsync();

            return result;

        }

        public async Task UpdateWorkerRegistrationAsync(T_WorkerRegistrations workerRegistration)
        {
            _dbContext.Entry(workerRegistration).State = EntityState.Modified;
        }
    }
}
