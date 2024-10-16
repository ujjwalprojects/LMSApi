using Asp.Versioning;
using AutoMapper;
using LMT.Api.DTOs;
using LMT.Api.Interfaces;
using LMT.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMT.Api.Repositories;

namespace LMT.Api.Controllers.v1
{
    [ApiController]
    //[Authorize]
    [ApiVersion("1")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class EstablishmentRegistrationController : ControllerBase
    {
        private readonly IEstablishmentRegistrationRepository _establishmentRegistrationRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EstablishmentRegistrationController> _logger;

        public EstablishmentRegistrationController(IEstablishmentRegistrationRepository establishmentRegistrationRepository, IMapper mapper, ILogger<EstablishmentRegistrationController> logger)
        {
            _establishmentRegistrationRepository = establishmentRegistrationRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/EstablishmentRegistration
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetT_EstablishmentDTO>>> GetEstablishmentRegistrations()
        {
            _logger.LogInformation("Method GetEstablishmentRegistrations invoked.");

            var establishmentRegistrations = await _establishmentRegistrationRepository.GetAllEstablishmentRegistrationsAsync();
            return Ok(_mapper.Map<List<GetT_EstablishmentDTO>>(establishmentRegistrations));
        }
        [HttpGet("establishment-list")]
        public async Task<ActionResult<IEnumerable<GetT_EstablishmentDTO>>> GetEstablishmentRegistrations(string? searchText)
        {
            _logger.LogInformation("Method GetEstablishmentRegistrations by searchText invoked.");

            var establishmentRegistrations = await _establishmentRegistrationRepository.GetAllEstablishmentRegistrationsAsync(searchText);
            return Ok(_mapper.Map<List<GetT_EstablishmentDTO>>(establishmentRegistrations));
        }

        [HttpGet("establishment-count")]
        public async Task<ActionResult<IEnumerable<GetEstablishmentCountDTO>>> GetEstablishmentCount(string? userId)
        {
            _logger.LogInformation("Method GetEstablishmentRegistrations by searchText invoked.");
            var result = await _establishmentRegistrationRepository.GetEstablishmentCountDTOs(userId);

            if (result == null || !result.Any())
                return NotFound();

            return Ok(result);
        }

        // GET: api/EstablishmentRegistration/id
        [HttpGet("{id}")]
        public async Task<ActionResult<T_EstablishmentRegistrationsDTO>> GetEstablishmentRegistration(int id)
        {
            _logger.LogInformation($"Method GetEstablishmentRegistration({id}) invoked.");

            var establishmentRegistration = await _establishmentRegistrationRepository.GetEstablishmentRegistrationByIdAsync(id);
            if (establishmentRegistration == null)
            {
                throw new BadHttpRequestException($"EstablishmentRegistration with Id {id} not found.", StatusCodes.Status400BadRequest);
            }
            return Ok(_mapper.Map<T_EstablishmentRegistrationsDTO>(establishmentRegistration));
        }

        // POST: api/EstablishmentRegistration
        [HttpPost]
        public async Task<ActionResult<T_EstablishmentRegistrationsDTO>> PostEstablishmentRegistration(T_EstablishmentRegistrationsDTO establishmentRegistrationDto)
        {
            _logger.LogInformation("Method PostEstablishmentRegistration invoked.");

            var establishmentRegistration = _mapper.Map<T_EstablishmentRegistrations>(establishmentRegistrationDto);
            await _establishmentRegistrationRepository.CreateEstablishmentRegistrationAsync(establishmentRegistration);

            return CreatedAtAction(nameof(GetEstablishmentRegistration), new { id = establishmentRegistration.Estd_Id }, _mapper.Map<T_EstablishmentRegistrationsDTO>(establishmentRegistration));
        }

        // PUT: api/EstablishmentRegistration/id
        [HttpPut]
        public async Task<IActionResult> PutEstablishmentRegistration(T_EstablishmentRegistrationsDTO establishmentRegistrationDto)
        {
            _logger.LogInformation($"Method PutEstablishmentRegistration({establishmentRegistrationDto.Estd_Id}) invoked.");

            if (establishmentRegistrationDto.Estd_Id<=0)
            {
                throw new BadHttpRequestException($"EstablishmentRegistration with Id {establishmentRegistrationDto.Estd_Id} not found.", StatusCodes.Status400BadRequest);
            }

            var establishmentRegistration = _mapper.Map<T_EstablishmentRegistrations>(establishmentRegistrationDto);
            try
            {
                await _establishmentRegistrationRepository.UpdateEstablishmentRegistrationAsync(establishmentRegistration);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EstablishmentRegistrationExists(establishmentRegistrationDto.Estd_Id))
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

        // DELETE: api/EstablishmentRegistration/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstablishmentRegistration(int id)
        {
            _logger.LogInformation($"Method DeleteEstablishmentRegistration({id}) invoked.");

            var establishmentRegistration = await _establishmentRegistrationRepository.GetEstablishmentRegistrationByIdAsync(id);
            if (establishmentRegistration == null)
            {
                throw new BadHttpRequestException($"EstablishmentRegistration with Id {id} not found.", StatusCodes.Status400BadRequest);
            }

            await _establishmentRegistrationRepository.DeleteEstablishmentRegistrationAsync(id);

            return NoContent();
        }

        [HttpGet("establishment-registration-report")]
        public async Task<ActionResult<IEnumerable<GetEstablishmentRegistrationReportDTO>>> GetEstablishmentRegistrationReport(int? distId)
        {
            _logger.LogInformation("Method GetWorkerRegistrationReport by searchText invoked.");
            var result = await _establishmentRegistrationRepository.GetEstablishmentRegistrationReportDTO(distId);

            if (result == null || !result.Any())
                return NotFound("No Results Found");

            return Ok(result);
        }

        private async Task<bool> EstablishmentRegistrationExists(int id)
        {
            var establishmentRegistration = await _establishmentRegistrationRepository.GetEstablishmentRegistrationByIdAsync(id);
            return establishmentRegistration != null;
        }
    }
}
