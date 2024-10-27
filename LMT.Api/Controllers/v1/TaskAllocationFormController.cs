using Asp.Versioning;
using AutoMapper;
using LMT.Api.DTOs;
using LMT.Api.Interfaces;
using LMT.Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMT.Api.Repositories;
using LMT.Api.DTOs.Paging;
using MailKit.Search;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LMT.Api.Controllers.v1
{
    [ApiController]
    //[Authorize]
    [ApiVersion("1")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class TaskAllocationFormController : ControllerBase
    {
        private readonly ITaskAllocationFormRepository _taskAllocationFormRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TaskAllocationFormController> _logger;

        public TaskAllocationFormController(ITaskAllocationFormRepository taskAllocationFormRepository, IMapper mapper, ILogger<TaskAllocationFormController> logger)
        {
            _taskAllocationFormRepository = taskAllocationFormRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/TaskAllocationForm
        [HttpGet]
        public async Task<ActionResult<List<T_TaskAllocationFormsDTO>>> GetTaskAllocationForms()
        {
            _logger.LogInformation("Method GetTaskAllocationForms invoked.");

            var taskAllocationForms = await _taskAllocationFormRepository.GetAllTaskAllocationFormsAsync();
            return Ok(_mapper.Map<List<T_TaskAllocationFormsDTO>>(taskAllocationForms));
        }

        [HttpGet("task-list")]
        public async Task<ActionResult<List<T_TaskAllocationFormsDTO>>> GetTaskAllocationFormsWithSearch(string? searchText, int? month, int? year, DateTime? date)
        {
            _logger.LogInformation("Method GetTaskAllocationFormsWithSearch invoked.");

            var taskAllocationForms = await _taskAllocationFormRepository.GetAllTaskAllocationFormsAsync(searchText, month, year, date);
            return Ok(_mapper.Map<List<T_TaskAllocationFormsDTO>>(taskAllocationForms));
        }

        [HttpGet("task-list-paging")]
        public async Task<IActionResult> GetTaskAllocationWithPaging(string? userId, string? searchText, int? month, int? year, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var result = await _taskAllocationFormRepository.GetTaskAllocationWithPagingAsync(userId, searchText, month, year, pageNumber, pageSize);

                if (result.Items.Any())
                {
                    return Ok(result); // Return 200 OK with the paginated result
                }
                else
                {
                    return NotFound("No record found for the specified filters.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception (assumed that ILogger is injected)
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving task allocations: {ex.Message}");
            }
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<T_TaskAllocationFormsDTO>> GetTaskAllocationForm(string id)
        {


            _logger.LogInformation($"Method GetTaskAllocationForm({id}) invoked.");

            var taskAllocationForm = await _taskAllocationFormRepository.GetTaskAllocationFormByIdAsync(id);
            if (taskAllocationForm == null)
            {
                throw new BadHttpRequestException($"TaskAllocationForm with Id {id} not found.", StatusCodes.Status400BadRequest);
            }
            return Ok(_mapper.Map<T_TaskAllocationFormsDTO>(taskAllocationForm));
        }

        
        [HttpPost]
        public async Task<ActionResult<T_TaskAllocationFormsDTO>> PostTaskAllocationForm(T_TaskAllocationFormsDTO taskAllocationFormDto)
        {
            _logger.LogInformation("Method PostTaskAllocationForm invoked.");

            var taskAllocationForm = _mapper.Map<T_TaskAllocationForms>(taskAllocationFormDto);
            await _taskAllocationFormRepository.CreateTaskAllocationFormAsync(taskAllocationForm);

            return CreatedAtAction(nameof(GetTaskAllocationForm), new { id = taskAllocationForm.Task_Id }, _mapper.Map<T_TaskAllocationFormsDTO>(taskAllocationForm));
        }

        [HttpPost("save-task-form")]
        public async Task<ActionResult<T_TaskAllocationFormsDTO>> PostTaskAllocationFormWithFile([FromForm] T_TaskAllocationFormsDTO taskAllocationFormDto, [FromForm] FIleUploadDTO fileUploadDto)
        {
            _logger.LogInformation("Method PostTaskAllocationForm invoked.");

            var taskAllocationForm = _mapper.Map<T_TaskAllocationForms>(taskAllocationFormDto);
            await _taskAllocationFormRepository.SaveTaskAllocationFormAsync(taskAllocationForm, fileUploadDto);

            return CreatedAtAction(nameof(GetTaskAllocationForm), new { id = taskAllocationForm.Task_Id }, _mapper.Map<T_TaskAllocationFormsDTO>(taskAllocationForm));
        }

        [HttpPost("save-task-workers-map")]
        public async Task<IActionResult> SaveTaskWorkersMap(T_TaskWorkerMapperManageDTO taskWorkerMapperManageDTO)
        {
            _logger.LogInformation("Method SaveTaskWorkersMap invoked.");
            await _taskAllocationFormRepository.SaveTaskWorkMappings(taskWorkerMapperManageDTO);
            return Ok("Task Workers Mapping Saved.");
        }

        // PUT: api/TaskAllocationForm
        [HttpPut]
        public async Task<IActionResult> PutTaskAllocationForm(T_TaskAllocationFormsDTO taskAllocationFormDto)
        {
            _logger.LogInformation($"Method PutTaskAllocationForm({taskAllocationFormDto.Task_Id}) invoked.");

            if (taskAllocationFormDto.Task_Id == null)
            {
                throw new BadHttpRequestException($"TaskAllocationForm with Id {taskAllocationFormDto.Task_Id} not found.", StatusCodes.Status400BadRequest);
            }

            var taskAllocationForm = _mapper.Map<T_TaskAllocationForms>(taskAllocationFormDto);
            try
            {
                await _taskAllocationFormRepository.UpdateTaskAllocationFormAsync(taskAllocationForm);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TaskAllocationFormExists(taskAllocationFormDto.Task_Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/TaskAllocationForm/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskAllocationForm(string id)
        {

            _logger.LogInformation($"Method DeleteTaskAllocationForm({id}) invoked.");

            var taskAllocationForm = await _taskAllocationFormRepository.GetTaskAllocationFormByIdAsync(id);
            if (taskAllocationForm == null)
            {
                throw new BadHttpRequestException($"TaskAllocationForm with Id {id} not found.", StatusCodes.Status400BadRequest);
            }

            await _taskAllocationFormRepository.DeleteTaskAllocationFormAsync(id);

            return NoContent();
        }

        private async Task<bool> TaskAllocationFormExists(string id)
        {
            var taskAllocationForm = await _taskAllocationFormRepository.GetTaskAllocationFormByIdAsync(id);
            return taskAllocationForm != null;
        }

        [HttpGet("task-worker-map/{taskId}")]
        public async Task<ActionResult<IEnumerable<GetTaskWorkersMapDTO>>> GetTaskWorkerMap(string taskId, string? searchText)
        {
            _logger.LogInformation("Method GetTaskWorkerMap invoked.");

            var taskWorkersMap = await _taskAllocationFormRepository.getTaskWorkersMapDTOsAsync(taskId, searchText);
            return Ok(taskWorkersMap);
        }

        [HttpGet("task-allocation-count")]
        public async Task<ActionResult<IEnumerable<GetTaskAllocationCountDTO>>> GetTaskAllocationCount(string? userId)
        {
            _logger.LogInformation("Method GetTaskAllocationCount by searchText invoked.");
            var result = await _taskAllocationFormRepository.GetTaskAllocationCountDTOs(userId);

            if (result == null || !result.Any())
                return NotFound();

            return Ok(result);
        }

    }
}
