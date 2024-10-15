using LMT.Api.DTOs;
using LMT.Api.Entities;
using System.Collections;

namespace LMT.Api.Interfaces
{
    public interface ITaskAllocationFormRepository
    {
        Task<List<T_TaskAllocationForms>> GetAllTaskAllocationFormsAsync();
        Task<List<T_TaskAllocationForms>> GetAllTaskAllocationFormsAsync(string? searchText, int? month, int? year, DateTime? date);
        Task<T_TaskAllocationForms> GetTaskAllocationFormByIdAsync(string taskAllocationFormId);
        Task CreateTaskAllocationFormAsync(T_TaskAllocationForms taskAllocationForm);
        Task UpdateTaskAllocationFormAsync(T_TaskAllocationForms taskAllocationForm);
        Task DeleteTaskAllocationFormAsync(string taskAllocationFormId);
        Task SaveTaskWorkMappings(T_TaskWorkerMapperManageDTO t_TaskWorkerMapperManageDTO);
        Task <IEnumerable<GetTaskWorkersMapDTO>> getTaskWorkersMapDTOsAsync(string taskId, string? searchText);
        Task<IEnumerable<GetTaskAllocationCountDTO>> GetTaskAllocationCountDTOs(string? userId);

    }

}
