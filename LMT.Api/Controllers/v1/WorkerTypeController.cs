using AutoMapper;
using LMT.Api.DTOs;
using LMT.Api.Interfaces;
using LMT.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMT.Api.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerTypeController : ControllerBase
    {
        private readonly IWorkerTypeRepository _workerTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<WorkerTypeController> _logger;

        public WorkerTypeController(IWorkerTypeRepository workerTypeRepository, IMapper mapper, ILogger<WorkerTypeController> logger)
        {
            _workerTypeRepository = workerTypeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/WorkerType
        [HttpGet]
        public async Task<ActionResult<List<M_WorkerTypesDTO>>> GetWorkerTypes()
        {
            _logger.LogInformation("Method GetWorkerTypes invoked.");

            var workerTypes = await _workerTypeRepository.GetAllWorkerTypesAsync();
            return Ok(_mapper.Map<List<M_WorkerTypesDTO>>(workerTypes));
        }

        // GET: api/WorkerType/id
        [HttpGet("{id}")]
        public async Task<ActionResult<M_WorkerTypesDTO>> GetWorkerType(int id)
        {
            _logger.LogInformation($"Method GetWorkerType({id}) invoked.");

            var workerType = await _workerTypeRepository.GetWorkerTypeByIdAsync(id);
            if (workerType == null)
            {
                throw new BadHttpRequestException($"WorkerType with Id {id} not found.", StatusCodes.Status400BadRequest);
            }
            return Ok(_mapper.Map<M_WorkerTypesDTO>(workerType));
        }

        // POST: api/WorkerType
        [HttpPost]
        public async Task<ActionResult<M_WorkerTypesDTO>> PostWorkerType(M_WorkerTypesDTO workerTypeDto)
        {
            _logger.LogInformation("Method PostWorkerType invoked.");

            var workerType = _mapper.Map<M_WorkerTypes>(workerTypeDto);
            await _workerTypeRepository.CreateWorkerTypeAsync(workerType);

            return CreatedAtAction(nameof(GetWorkerType), new { id = workerType.WorkerType_Id }, _mapper.Map<M_WorkerTypesDTO>(workerType));
        }

        // PUT: api/WorkerType/id
        [HttpPut]
        public async Task<IActionResult> PutWorkerType(M_WorkerTypesDTO workerTypeDto)
        {
            _logger.LogInformation($"Method PutWorkerType({workerTypeDto.WorkerType_Id}) invoked.");

            if (workerTypeDto.WorkerType_Id <= 0)
            {
                throw new BadHttpRequestException($"WorkerType with Id {workerTypeDto.WorkerType_Id} not found.", StatusCodes.Status400BadRequest);
            }

            var workerType = _mapper.Map<M_WorkerTypes>(workerTypeDto);
            try
            {
                await _workerTypeRepository.UpdateWorkerTypeAsync(workerType);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await WorkerTypeExists(workerTypeDto.WorkerType_Id))
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

        // DELETE: api/WorkerType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkerType(int id)
        {
            _logger.LogInformation($"Method DeleteWorkerType({id}) invoked.");

            var workerType = await _workerTypeRepository.GetWorkerTypeByIdAsync(id);
            if (workerType == null)
            {
                throw new BadHttpRequestException($"WorkerType with Id {id} not found.", StatusCodes.Status400BadRequest);
            }

            await _workerTypeRepository.DeleteWorkerTypeAsync(id);

            return NoContent();
        }

        private async Task<bool> WorkerTypeExists(int id)
        {
            var workerType = await _workerTypeRepository.GetWorkerTypeByIdAsync(id);
            return workerType != null;
        }
    }
}
