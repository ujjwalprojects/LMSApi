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
    [Authorize]
    [ApiVersion("1")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class TaskAllocationSiteImageController : ControllerBase
    {
        private readonly ITaskAllocationSiteImageRepository _taskAllocationSiteImageRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TaskAllocationSiteImageController> _logger;

        public TaskAllocationSiteImageController(ITaskAllocationSiteImageRepository taskAllocationSiteImageRepository, IMapper mapper, ILogger<TaskAllocationSiteImageController> logger)
        {
            _taskAllocationSiteImageRepository = taskAllocationSiteImageRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/TaskAllocationSiteImage
        [HttpGet]
        public async Task<ActionResult<List<T_TaskAllocationSiteImagesDTO>>> GetTaskAllocationSiteImages()
        {
            _logger.LogInformation("Method GetTaskAllocationSiteImages invoked.");

            var taskAllocationSiteImages = await _taskAllocationSiteImageRepository.GetAllTaskAllocationSiteImagesAsync();
            return Ok(_mapper.Map<List<T_TaskAllocationSiteImagesDTO>>(taskAllocationSiteImages));
        }

        // GET: api/TaskAllocationSiteImage/id
        [HttpGet("{id}")]
        public async Task<ActionResult<T_TaskAllocationSiteImagesDTO>> GetTaskAllocationSiteImage(int id)
        {
            _logger.LogInformation($"Method GetTaskAllocationSiteImage({id}) invoked.");

            var taskAllocationSiteImage = await _taskAllocationSiteImageRepository.GetTaskAllocationSiteImageByIdAsync(id);
            if (taskAllocationSiteImage == null)
            {
                throw new BadHttpRequestException($"TaskAllocationSiteImage with Id {id} not found.", StatusCodes.Status400BadRequest);
            }
            return Ok(_mapper.Map<T_TaskAllocationSiteImagesDTO>(taskAllocationSiteImage));
        }

        // POST: api/TaskAllocationSiteImage
        [HttpPost]
        public async Task<ActionResult<T_TaskAllocationSiteImagesDTO>> PostTaskAllocationSiteImage(T_TaskAllocationSiteImagesDTO taskAllocationSiteImageDto)
        {
            _logger.LogInformation("Method PostTaskAllocationSiteImage invoked.");

            var taskAllocationSiteImage = _mapper.Map<T_TaskAllocationSiteImages>(taskAllocationSiteImageDto);
            await _taskAllocationSiteImageRepository.CreateTaskAllocationSiteImageAsync(taskAllocationSiteImage);

            return CreatedAtAction(nameof(GetTaskAllocationSiteImage), new { id = taskAllocationSiteImage.Task_Site_Image_Id }, _mapper.Map<T_TaskAllocationSiteImagesDTO>(taskAllocationSiteImage));
        }

        // PUT: api/TaskAllocationSiteImage/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskAllocationSiteImage(int id, T_TaskAllocationSiteImagesDTO taskAllocationSiteImageDto)
        {
            _logger.LogInformation($"Method PutTaskAllocationSiteImage({id}) invoked.");

            if (id != taskAllocationSiteImageDto.Task_Site_Image_Id)
            {
                throw new BadHttpRequestException($"TaskAllocationSiteImage with Id {id} not found.", StatusCodes.Status400BadRequest);
            }

            var taskAllocationSiteImage = _mapper.Map<T_TaskAllocationSiteImages>(taskAllocationSiteImageDto);
            try
            {
                await _taskAllocationSiteImageRepository.UpdateTaskAllocationSiteImageAsync(taskAllocationSiteImage);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TaskAllocationSiteImageExists(id))
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

        // DELETE: api/TaskAllocationSiteImage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskAllocationSiteImage(int id)
        {
            _logger.LogInformation($"Method DeleteTaskAllocationSiteImage({id}) invoked.");

            var taskAllocationSiteImage = await _taskAllocationSiteImageRepository.GetTaskAllocationSiteImageByIdAsync(id);
            if (taskAllocationSiteImage == null)
            {
                throw new BadHttpRequestException($"TaskAllocationSiteImage with Id {id} not found.", StatusCodes.Status400BadRequest);
            }

            await _taskAllocationSiteImageRepository.DeleteTaskAllocationSiteImageAsync(id);

            return NoContent();
        }

        private async Task<bool> TaskAllocationSiteImageExists(int id)
        {
            var taskAllocationSiteImage = await _taskAllocationSiteImageRepository.GetTaskAllocationSiteImageByIdAsync(id);
            return taskAllocationSiteImage != null;
        }
    }
}
