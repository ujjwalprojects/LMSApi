using LMT.Api.DTOs;
using LMT.Api.DTOs.Paging;
using LMT.Api.Entities;

namespace LMT.Api.Interfaces
{
    public interface IWorkerRegistrationRepository
    {
        Task<List<GetT_WorkerRegistrationDTO>> GetAllWorkerRegistrationsAsync();
        Task<List<GetT_WorkerRegistrationDTO>> GetAllWorkerRegistrationsAsync(string? searchText);
        Task<T_WorkerRegistrations?> GetWorkerRegistrationByIdAsync(string workerRegistrationId);
        Task CreateWorkerRegistrationAsync(T_WorkerRegistrations workerRegistration);
        Task UpdateWorkerRegistrationAsync(T_WorkerRegistrations workerRegistration);
        Task DeleteWorkerRegistrationAsync(string workerRegistrationId);
        Task<IEnumerable<GetWorkerRegistrationCountDTO>> GetWorkerRegistrationCountDTO(string? userId);
        Task<IEnumerable<GetWorkerRegistrationReportDTO>> GetWorkerRegistrationReportDTO(int? estdId, int? distId, string? independentWorker);
        Task<PaginatedResult<GetT_WorkerRegistrationDTO>> GetWorkerWithPagingAsync(string? userId, string? searchText, int pageNumber = 1, int pageSize = 10);
    }

}
