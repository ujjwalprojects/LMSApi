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
    public class StateController : ControllerBase
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<StateController> _logger;

        public StateController(IStateRepository stateRepository, IMapper mapper, ILogger<StateController> logger)
        {
            _stateRepository = stateRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/State
        [HttpGet]
        public async Task<ActionResult<List<M_StatesDTO>>> GetStates()
        {
            _logger.LogInformation("Method GetStates invoked.");

            var states = await _stateRepository.GetAllStatesAsync();
            return Ok(_mapper.Map<List<M_StatesDTO>>(states));
        }

        // GET: api/State/id
        [HttpGet("{id}")]
        public async Task<ActionResult<M_StatesDTO>> GetState(int id)
        {
            _logger.LogInformation($"Method GetState({id}) invoked.");

            var state = await _stateRepository.GetStateByIdAsync(id);
            if (state == null)
            {
                throw new BadHttpRequestException($"State with Id {id} not found.", StatusCodes.Status400BadRequest);
            }
            return Ok(_mapper.Map<M_StatesDTO>(state));
        }

        // POST: api/State
        [HttpPost]
        public async Task<ActionResult<M_StatesDTO>> PostState(M_StatesDTO stateDto)
        {
            _logger.LogInformation("Method PostState invoked.");

            var state = _mapper.Map<M_States>(stateDto);
            await _stateRepository.CreateStateAsync(state);

            return CreatedAtAction(nameof(GetState), new { id = state.State_Id }, _mapper.Map<M_StatesDTO>(state));
        }

        // PUT: api/State/id
        [HttpPut]
        public async Task<IActionResult> PutState(M_StatesDTO stateDto)
        {
            _logger.LogInformation($"Method PutState({stateDto.State_Id}) invoked.");

            if (stateDto.State_Id <= 0)
            {
                throw new BadHttpRequestException($"State with Id {stateDto.State_Id} not found.", StatusCodes.Status400BadRequest);
            }

            var state = _mapper.Map<M_States>(stateDto);
            try
            {
                await _stateRepository.UpdateStateAsync(state);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await StateExists(stateDto.State_Id))
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

        // DELETE: api/State/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteState(int id)
        {
            _logger.LogInformation($"Method DeleteState({id}) invoked.");

            var state = await _stateRepository.GetStateByIdAsync(id);
            if (state == null)
            {
                throw new BadHttpRequestException($"State with Id {id} not found.", StatusCodes.Status400BadRequest);
            }

            await _stateRepository.DeleteStateAsync(id);

            return NoContent();
        }

        private async Task<bool> StateExists(int id)
        {
            var state = await _stateRepository.GetStateByIdAsync(id);
            return state != null;
        }
    }
}
