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
    public class RegistrationActController : ControllerBase
    {
        private readonly IRegistrationActRepository _registrationActRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegistrationActController> _logger;

        public RegistrationActController(IRegistrationActRepository registrationActRepository, IMapper mapper, ILogger<RegistrationActController> logger)
        {
            _registrationActRepository = registrationActRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/RegistrationAct
        [HttpGet]
        public async Task<ActionResult<List<M_RegistrationActsDTO>>> GetRegistrationActs()
        {
            _logger.LogInformation("Method GetRegistrationActs invoked.");

            var registrationActs = await _registrationActRepository.GetAllRegistrationActsAsync();
            return Ok(_mapper.Map<List<M_RegistrationActsDTO>>(registrationActs));
        }

        // GET: api/RegistrationAct/id
        [HttpGet("{id}")]
        public async Task<ActionResult<M_RegistrationActsDTO>> GetRegistrationAct(int id)
        {
            _logger.LogInformation($"Method GetRegistrationAct({id}) invoked.");

            var registrationAct = await _registrationActRepository.GetRegistrationActByIdAsync(id);
            if (registrationAct == null)
            {
                throw new BadHttpRequestException($"RegistrationAct with Id {id} not found.", StatusCodes.Status400BadRequest);
            }
            return Ok(_mapper.Map<M_RegistrationActsDTO>(registrationAct));
        }

        // POST: api/RegistrationAct
        [HttpPost]
        public async Task<ActionResult<M_RegistrationActsDTO>> PostRegistrationAct(M_RegistrationActsDTO registrationActDto)
        {
            _logger.LogInformation("Method PostRegistrationAct invoked.");

            var registrationAct = _mapper.Map<M_RegistrationActs>(registrationActDto);
            await _registrationActRepository.CreateRegistrationActAsync(registrationAct);

            return CreatedAtAction(nameof(GetRegistrationAct), new { id = registrationAct.Reg_Act_Id }, _mapper.Map<M_RegistrationActsDTO>(registrationAct));
        }

        // PUT: api/RegistrationAct/id
        [HttpPut]
        public async Task<IActionResult> PutRegistrationAct(M_RegistrationActsDTO registrationActDto)
        {
            _logger.LogInformation($"Method PutRegistrationAct({registrationActDto.Reg_Act_Id}) invoked.");

            if (registrationActDto.Reg_Act_Id <= 0)
            {
                throw new BadHttpRequestException($"RegistrationAct with Id {registrationActDto.Reg_Act_Id} not found.", StatusCodes.Status400BadRequest);
            }

            var registrationAct = _mapper.Map<M_RegistrationActs>(registrationActDto);
            try
            {
                await _registrationActRepository.UpdateRegistrationActAsync(registrationAct);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RegistrationActExists(registrationActDto.Reg_Act_Id))
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

        // DELETE: api/RegistrationAct/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistrationAct(int id)
        {
            _logger.LogInformation($"Method DeleteRegistrationAct({id}) invoked.");

            var registrationAct = await _registrationActRepository.GetRegistrationActByIdAsync(id);
            if (registrationAct == null)
            {
                throw new BadHttpRequestException($"RegistrationAct with Id {id} not found.", StatusCodes.Status400BadRequest);
            }

            await _registrationActRepository.DeleteRegistrationActAsync(id);

            return NoContent();
        }

        private async Task<bool> RegistrationActExists(int id)
        {
            var registrationAct = await _registrationActRepository.GetRegistrationActByIdAsync(id);
            return registrationAct != null;
        }
    }
}
