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
    // [Authorize]
    [ApiVersion("1")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class JobRoleController : ControllerBase
    {
        private readonly IJobRoleRepository _jobRoleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<JobRoleController> _logger;

        public JobRoleController(IJobRoleRepository jobRoleRepository, IMapper mapper, ILogger<JobRoleController> logger)
        {
            _jobRoleRepository = jobRoleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/JobRole
        [HttpGet]
        public async Task<ActionResult<List<M_JobRolesDTO>>> GetJobRoles()
        {
            _logger.LogInformation("Method GetJobRoles invoked.");

            var jobRoles = await _jobRoleRepository.GetAllJobRolesAsync();
            return Ok(_mapper.Map<List<M_JobRolesDTO>>(jobRoles));
        }

        // GET: api/JobRole/id
        [HttpGet("{id}")]
        public async Task<ActionResult<M_JobRolesDTO>> GetJobRole(int id)
        {
            _logger.LogInformation($"Method GetJobRole({id}) invoked.");

            var jobRole = await _jobRoleRepository.GetJobRoleByIdAsync(id);
            if (jobRole == null)
            {
                throw new BadHttpRequestException($"JobRole with Id {id} not found.", StatusCodes.Status400BadRequest);
            }
            return Ok(_mapper.Map<M_JobRolesDTO>(jobRole));
        }

        // POST: api/JobRole
        [HttpPost]
        public async Task<ActionResult<M_JobRolesDTO>> PostJobRole(M_JobRolesDTO jobRoleDto)
        {
            _logger.LogInformation("Method PostJobRole invoked.");

            var jobRole = _mapper.Map<M_JobRoles>(jobRoleDto);
            await _jobRoleRepository.CreateJobRoleAsync(jobRole);

            return CreatedAtAction(nameof(GetJobRole), new { id = jobRole.Job_Role_Id }, _mapper.Map<M_JobRolesDTO>(jobRole));
        }

        // PUT: api/JobRole/id
        [HttpPut]
        public async Task<IActionResult> PutJobRole(M_JobRolesDTO jobRoleDto)
        {
            _logger.LogInformation($"Method PutJobRole({jobRoleDto.Job_Role_Id}) invoked.");

            if (jobRoleDto.Job_Role_Id <= 0)
            {
                throw new BadHttpRequestException($"JobRole with Id {jobRoleDto.Job_Role_Id} not found.", StatusCodes.Status400BadRequest);
            }

            var jobRole = _mapper.Map<M_JobRoles>(jobRoleDto);
            try
            {
                await _jobRoleRepository.UpdateJobRoleAsync(jobRole);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await JobRoleExists(jobRoleDto.Job_Role_Id))
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

        // DELETE: api/JobRole/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobRole(int id)
        {
            _logger.LogInformation($"Method DeleteJobRole({id}) invoked.");

            var jobRole = await _jobRoleRepository.GetJobRoleByIdAsync(id);
            if (jobRole == null)
            {
                throw new BadHttpRequestException($"JobRole with Id {id} not found.", StatusCodes.Status400BadRequest);
            }

            await _jobRoleRepository.DeleteJobRoleAsync(id);

            return NoContent();
        }

        private async Task<bool> JobRoleExists(int id)
        {
            var jobRole = await _jobRoleRepository.GetJobRoleByIdAsync(id);
            return jobRole != null;
        }
    }
}
