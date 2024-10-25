using LMT.Api.Interfaces;
using LMT.Api.Entities;
using LMT.Api.Data;
using LMT.Api.IDGenerators;
using Microsoft.EntityFrameworkCore;
using LMT.Api.DTOs;
using System.Linq;
using LMT.Api.DTOs.Paging;

namespace LMT.Api.Repositories
{
    public class TaskAllocationFormRepository : ITaskAllocationFormRepository
    {
        private readonly EFDBContext _dbContext;
        private readonly UniqueIdGenerator _uniqueIdGenerator;
        private readonly IConfiguration _configuration;
        public TaskAllocationFormRepository(EFDBContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _uniqueIdGenerator = new UniqueIdGenerator(_dbContext);
            _configuration = configuration;
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
            return await _dbContext.T_TaskAllocationForms.OrderByDescending(x=>x.Task_Assigned_Date).ToListAsync();
        }

        public async Task<List<T_TaskAllocationForms>> GetAllTaskAllocationFormsAsync(string? searchText, int? month, int? year, DateTime? date)
        {
            if (string.IsNullOrEmpty(searchText) && month == null && year == null && date == null)
            {
                return await _dbContext.T_TaskAllocationForms.ToListAsync();
            }
            return await _dbContext.T_TaskAllocationForms
                .Where(c => c.Task_Name.Contains(searchText) || c.Task_Id.Contains(searchText) || c.Task_Status.Contains(searchText)
                || c.Task_Creation_Date == date || c.Task_Creation_Date.Year == year || c.Task_Creation_Date.Month == month)
                .OrderByDescending(x => x.Task_Assigned_Date)
                .ToListAsync();
        }

        public async Task<PaginatedResult<T_TaskAllocationForms>> GetTaskAllocationWithPagingAsync(string? userId, string? searchText, int? month, int? year, int pageNumber = 1, int pageSize = 10)
        {
            var query = _dbContext.T_TaskAllocationForms.AsQueryable();
            // Conditionally filter by userId if provided
            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(c => c.Task_Assigned_To_Id == userId);  // Assuming there is a UserId property in the model
            }
            // Apply search filters if any search text is provided
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(c => c.Task_Name.Contains(searchText)
                                         || c.Task_Id.Contains(searchText)
                                         || c.Task_Status.Contains(searchText)
                                         || c.Task_Description.Contains(searchText)
                                         );
            }
            // Apply filtering by date, month, and year
            //if (date != null)
            //{
            //    query = query.Where(c => c.Task_Creation_Date == date);
            //}
            else
            {
                if (year != null)
                {
                    query = query.Where(c => c.Task_Creation_Date.Year == year);
                }

                if (month != null)
                {
                    query = query.Where(c => c.Task_Creation_Date.Month == month);
                }
            }
            // Get total record count before pagination
            int totalRecords = await query.CountAsync();

            // Apply ordering by Task_Assigned_Date in descending order
            query = query.OrderByDescending(x => x.Task_Assigned_Date);

            // Apply pagination (skip and take)
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            // Create and return paginated result
            return new PaginatedResult<T_TaskAllocationForms>(items, totalRecords, pageNumber, pageSize);
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
                 .FromSqlRaw("EXEC [dbo].[SP_GetWorkersMappedWithTasks] @TaskID = {0}, @SearchTerm = {1}", 
                    taskId ?? (object)DBNull.Value, searchText ?? (object)DBNull.Value)
                .ToListAsync();

            return result;
        }

        public async Task SaveTaskWorkMappings(T_TaskWorkerMapperManageDTO t_TaskWorkerMapperManageDTO)
        {
            
                var taskId = t_TaskWorkerMapperManageDTO.TaskWorkerMap.First().TaskID;

                var existingMappings = _dbContext.T_TaskWorkerMappers
                      .Where(t => t.TaskID == taskId)
                      .ToList();

                _dbContext.T_TaskWorkerMappers.RemoveRange(existingMappings);

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
