using Asp.Versioning;
using AutoMapper;
using LMT.Api.DTOs;
using LMT.Api.Interfaces;
using LMT.Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMT.Api.Controllers.v1
{
    [ApiController]
    //[Authorize]
    [ApiVersion("1")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class TaskPurposeController : ControllerBase
    {
        private readonly ITaskPurposeRepository _taskPurposeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TaskPurposeController> _logger;

        public TaskPurposeController(ITaskPurposeRepository taskPurposeRepository, IMapper mapper, ILogger<TaskPurposeController> logger)
        {
            _taskPurposeRepository = taskPurposeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/TaskPurpose
        [HttpGet]
        public async Task<ActionResult<List<M_TaskPurposesDTO>>> GetTaskPurposes()
        {
            _logger.LogInformation("Method GetTaskPurposes invoked.");

            var taskPurposes = await _taskPurposeRepository.GetAllTaskPurposesAsync();
            return Ok(_mapper.Map<List<M_TaskPurposesDTO>>(taskPurposes));
        }

        // GET: api/TaskPurpose/id
        [HttpGet("{id}")]
        public async Task<ActionResult<M_TaskPurposesDTO>> GetTaskPurpose(int id)
        {
            _logger.LogInformation($"Method GetTaskPurpose({id}) invoked.");

            var taskPurpose = await _taskPurposeRepository.GetTaskPurposeByIdAsync(id);
            if (taskPurpose == null)
            {
                throw new BadHttpRequestException($"TaskPurpose with Id {id} not found.", StatusCodes.Status400BadRequest);
            }
            return Ok(_mapper.Map<M_TaskPurposesDTO>(taskPurpose));
        }

        // POST: api/TaskPurpose
        [HttpPost]
        public async Task<ActionResult<M_TaskPurposesDTO>> PostTaskPurpose(M_TaskPurposesDTO taskPurposeDto)
        {
            _logger.LogInformation("Method PostTaskPurpose invoked.");
            var taskPurpose = _mapper.Map<M_TaskPurposes>(taskPurposeDto);
            await _taskPurposeRepository.CreateTaskPurposeAsync(taskPurpose);

            return CreatedAtAction(nameof(GetTaskPurpose), new { id = taskPurpose.Task_Purpose_Id }, _mapper.Map<M_TaskPurposesDTO>(taskPurpose));
        }

        // PUT: api/TaskPurpose/id
        [HttpPut]
        public async Task<IActionResult> PutTaskPurpose(M_TaskPurposesDTO taskPurposeDto)
        {
            _logger.LogInformation($"Method PutTaskPurpose({taskPurposeDto.Task_Purpose_Id}) invoked.");

            if (taskPurposeDto.Task_Purpose_Id <= 0)
            {
                throw new BadHttpRequestException($"TaskPurpose with Id {taskPurposeDto.Task_Purpose_Id} not found.", StatusCodes.Status400BadRequest);
            }

            var taskPurpose = _mapper.Map<M_TaskPurposes>(taskPurposeDto);
            try
            {
                await _taskPurposeRepository.UpdateTaskPurposeAsync(taskPurpose);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TaskPurposeExists(taskPurposeDto.Task_Purpose_Id))
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

        // DELETE: api/TaskPurpose/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskPurpose(int id)
        {
            _logger.LogInformation($"Method DeleteTaskPurpose({id}) invoked.");

            var taskPurpose = await _taskPurposeRepository.GetTaskPurposeByIdAsync(id);
            if (taskPurpose == null)
            {
                throw new BadHttpRequestException($"TaskPurpose with Id {id} not found.", StatusCodes.Status400BadRequest);
            }

            await _taskPurposeRepository.DeleteTaskPurposeAsync(id);

            return NoContent();
        }

        private async Task<bool> TaskPurposeExists(int id)
        {
            var taskPurpose = await _taskPurposeRepository.GetTaskPurposeByIdAsync(id);
            return taskPurpose != null;
        }
    }
}
