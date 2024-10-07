using LMT.Api.DTOs;
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
        Task<IEnumerable<GetWorkerRegistrationReportDTO>> GetWorkerRegistrationReportDTO(string? estdId);
    }

}
