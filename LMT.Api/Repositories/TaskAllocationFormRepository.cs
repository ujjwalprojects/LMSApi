using LMT.Api.Interfaces;
using LMT.Api.Entities;
using LMT.Api.Data;
using LMT.Api.IDGenerators;
using Microsoft.EntityFrameworkCore;
using LMT.Api.DTOs;

namespace LMT.Api.Repositories
{
    public class TaskAllocationFormRepository : ITaskAllocationFormRepository
    {
        private readonly EFDBContext _dbContext;
        private readonly UniqueIdGenerator _uniqueIdGenerator;

        public TaskAllocationFormRepository(EFDBContext dbContext)
        {
            _dbContext = dbContext;
            _uniqueIdGenerator = new UniqueIdGenerator(_dbContext);
        }

        public async Task CreateTaskAllocationFormAsync(T_TaskAllocationForms taskAllocationForm)
        {
            //Generate TaskID
            taskAllocationForm.Task_Id = _uniqueIdGenerator.GenerateUniqueId();

            _dbContext.T_TaskAllocationForms.Add(taskAllocationForm);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTaskAllocationFormAsync(string taskAllocationFormId)
        {
            var taskAllocationForm = await _dbContext.T_TaskAllocationForms.FindAsync(taskAllocationFormId);
            if (taskAllocationForm != null)
            {
                _dbContext.T_TaskAllocationForms.Remove(taskAllocationForm);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<T_TaskAllocationForms>> GetAllTaskAllocationFormsAsync()
        {
            return await _dbContext.T_TaskAllocationForms.ToListAsync();
        }

        public async Task<IEnumerable<GetTaskAllocationCountDTO>> GetTaskAllocationCountDTOs(string? userId)
        {
            var result = await _dbContext.GetTaskAllocationCountDTOs
            .FromSqlRaw("EXEC [dbo].[SP_GetTaskAllocationCountForDashboard] @UserID = {0}", userId ?? (object)DBNull.Value)
           .ToListAsync();
            return result;
        }

        public async Task<T_TaskAllocationForms> GetTaskAllocationFormByIdAsync(string taskAllocationFormId)
        {
            return await _dbContext.T_TaskAllocationForms.FindAsync(taskAllocationFormId);
        }

        public async Task<IEnumerable<GetTaskWorkersMapDTO>> getTaskWorkersMapDTOsAsync(string taskId, string? searchText)
        {
            var result = await _dbContext.GetTaskWorkersMapDTO
                 .FromSqlRaw("EXEC [dbo].[SP_GetWorkersMappedWithTasks] @TaskID = {0}, @SearchTerm = {1}", taskId ?? (object)DBNull.Value, searchText ?? (object)DBNull.Value)
                .ToListAsync();

            return result;
        }

        public async Task SaveTaskWorkMappings(T_TaskWorkerMapperManageDTO t_TaskWorkerMapperManageDTO)
        {
            var taskWorkers = t_TaskWorkerMapperManageDTO.TaskWorkerMap.Select(dto => new T_TaskWorkerMappers
            {
                TaskID = dto.TaskID,
                Worker_Reg_Id = dto.Worker_Reg_Id
            }).ToList();



            _dbContext.T_TaskWorkerMappers.AddRange(taskWorkers);
            await _dbContext.SaveChangesAsync();

        }

        public async Task UpdateTaskAllocationFormAsync(T_TaskAllocationForms taskAllocationForm)
        {
            _dbContext.Entry(taskAllocationForm).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }


    }
}
