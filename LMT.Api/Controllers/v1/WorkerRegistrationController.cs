using Asp.Versioning;
using AutoMapper;
using LMT.Api.DTOs;
using LMT.Api.Interfaces;
using LMT.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMT.Api.Controllers.v1
{
    [ApiController]
    //[Authorize]
    [ApiVersion("1")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class WorkerRegistrationController : ControllerBase
    {
        private readonly IWorkerRegistrationRepository _workerRegistrationRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<WorkerRegistrationController> _logger;

        public WorkerRegistrationController(IWorkerRegistrationRepository workerRegistrationRepository, IMapper mapper, ILogger<WorkerRegistrationController> logger)
        {
            _workerRegistrationRepository = workerRegistrationRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/WorkerRegistration
        [HttpGet]
        public async Task<ActionResult<List<GetT_WorkerRegistrationDTO>>> GetWorkerRegistrations()
        {
            _logger.LogInformation("Method GetWorkerRegistrations invoked.");

            var workerRegistrations = await _workerRegistrationRepository.GetAllWorkerRegistrationsAsync();
            return Ok(workerRegistrations);
        }
        [HttpGet("workers-list")]
        public async Task<ActionResult<IEnumerable<GetT_WorkerRegistrationDTO>>> GetWorkerRegistrations(string? searchText)
        {
            _logger.LogInformation("Method GetWorkerRegistrations invoked.");

            var workerRegistrations = await _workerRegistrationRepository.GetAllWorkerRegistrationsAsync(searchText);
            return Ok(workerRegistrations);
        }
        // GET: api/WorkerRegistration/id
        [HttpGet("{id}")]
        public async Task<ActionResult<T_WorkerRegistrationsDTO>> GetWorkerRegistration(string id)
        {
            _logger.LogInformation($"Method GetWorkerRegistration({id}) invoked.");

            var workerRegistration = await _workerRegistrationRepository.GetWorkerRegistrationByIdAsync(id);
            if (workerRegistration == null)
            {
                throw new BadHttpRequestException($"WorkerRegistration with Id {id} not found.", StatusCodes.Status400BadRequest);
            }
            return Ok(_mapper.Map<T_WorkerRegistrationsDTO>(workerRegistration));
        }

        // POST: api/WorkerRegistration
        [HttpPost]
        public async Task<ActionResult<T_WorkerRegistrationsDTO>> PostWorkerRegistration(T_WorkerRegistrationsDTO workerRegistrationDto)
        {
            _logger.LogInformation("Method PostWorkerRegistration invoked.");

            var workerRegistration = _mapper.Map<T_WorkerRegistrations>(workerRegistrationDto);
            await _workerRegistrationRepository.CreateWorkerRegistrationAsync(workerRegistration);

            return CreatedAtAction(nameof(GetWorkerRegistration), new { id = workerRegistration.Worker_Reg_Id }, _mapper.Map<T_WorkerRegistrationsDTO>(workerRegistration));
        }

        // PUT: api/WorkerRegistration/id
        [HttpPut]
        public async Task<IActionResult> PutWorkerRegistration(T_WorkerRegistrationsDTO workerRegistrationDto)
        {
            _logger.LogInformation($"Method PutWorkerRegistration({workerRegistrationDto.Worker_Reg_Id}) invoked.");

            if (workerRegistrationDto.Worker_Reg_Id == "")
            {
                throw new BadHttpRequestException($"WorkerRegistration with Id {workerRegistrationDto.Worker_Reg_Id} not found.", StatusCodes.Status400BadRequest);
            }

            var workerRegistration = _mapper.Map<T_WorkerRegistrations>(workerRegistrationDto);
            try
            {
                await _workerRegistrationRepository.UpdateWorkerRegistrationAsync(workerRegistration);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await WorkerRegistrationExists(workerRegistrationDto.Worker_Reg_Id))
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

        // DELETE: api/WorkerRegistration/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkerRegistration(string id)
        {
            _logger.LogInformation($"Method DeleteWorkerRegistration({id}) invoked.");

            var workerRegistration = await _workerRegistrationRepository.GetWorkerRegistrationByIdAsync(id);
            if (workerRegistration == null)
            {
                throw new BadHttpRequestException($"WorkerRegistration with Id {id} not found.", StatusCodes.Status400BadRequest);
            }

            await _workerRegistrationRepository.DeleteWorkerRegistrationAsync(id);

            return NoContent();
        }

        [HttpGet("worker-registration-count")]
        public async Task<ActionResult<IEnumerable<GetWorkerRegistrationCountDTO>>> GetWorkersRegistrationCount(string? userId)
        {
            _logger.LogInformation("Method GetWorkersRegistrationCount by searchText invoked.");
            var result = await _workerRegistrationRepository.GetWorkerRegistrationCountDTO(userId);

            if (result == null || !result.Any())
                return NotFound();

            return Ok(result);
        }

        [HttpGet("worker-registration-report")]
        public async Task<ActionResult<IEnumerable<GetWorkerRegistrationReportDTO>>> GetWorkerRegistrationReport(string? estdId)
        {
            _logger.LogInformation("Method GetWorkerRegistrationReport by searchText invoked.");
            var result = await _workerRegistrationRepository.GetWorkerRegistrationReportDTO(estdId);

            if (result == null || !result.Any())
                return NotFound();

            return Ok(result);
        }
        private async Task<bool> WorkerRegistrationExists(string id)
        {
            var workerRegistration = await _workerRegistrationRepository.GetWorkerRegistrationByIdAsync(id);
            return workerRegistration != null;
        }
    }
}
