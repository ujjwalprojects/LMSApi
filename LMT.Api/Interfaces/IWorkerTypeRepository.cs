using LMT.Api.Entities;

namespace LMT.Api.Interfaces
{
    public interface IWorkerTypeRepository
    {
        Task<List<M_WorkerTypes>> GetAllWorkerTypesAsync();
        Task<M_WorkerTypes> GetWorkerTypeByIdAsync(int workerTypeId);
        Task CreateWorkerTypeAsync(M_WorkerTypes workerType);
        Task UpdateWorkerTypeAsync(M_WorkerTypes workerType);
        Task DeleteWorkerTypeAsync(int workerTypeId);
    }

}
