﻿using LMT.Api.DTOs;
using LMT.Api.DTOs.Paging;
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
        Task<PaginatedResult<T_TaskAllocationForms>> GetTaskAllocationWithPagingAsync(string? userId, string? searchText, int? month, int? year, int pageNumber = 1, int pageSize = 10);
        Task SaveTaskAllocationFormAsync(T_TaskAllocationForms taskAllocationForm, FIleUploadDTO fIle);
    }

}
