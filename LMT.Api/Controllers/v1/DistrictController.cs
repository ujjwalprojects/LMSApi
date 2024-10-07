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
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictRepository _districtRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DistrictController> _logger;

        public DistrictController(IDistrictRepository districtRepository, IMapper mapper, ILogger<DistrictController> logger)
        {
            _districtRepository = districtRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/District
        [HttpGet]
        public async Task<ActionResult<List<M_DistrictsDTO>>> GetDistricts()
        {
            _logger.LogInformation("Method GetDistricts invoked.");

            var districts = await _districtRepository.GetAllDistrictsAsync();
            return Ok(_mapper.Map<List<M_DistrictsDTO>>(districts));
        }

        // GET: api/District/id
        [HttpGet("{id}")]
        public async Task<ActionResult<M_DistrictsDTO>> GetDistrict(int id)
        {
            _logger.LogInformation($"Method GetDistrict({id}) invoked.");

            var district = await _districtRepository.GetDistrictByIdAsync(id);
            if (district == null)
            {
                throw new BadHttpRequestException($"District with Id {id} not found.", StatusCodes.Status400BadRequest);
            }
            return Ok(_mapper.Map<M_DistrictsDTO>(district));
        }

        // POST: api/District
        [HttpPost]
        public async Task<ActionResult<M_DistrictsDTO>> PostDistrict(M_DistrictsDTO districtDto)
        {
            _logger.LogInformation("Method PostDistrict invoked.");

            var district = _mapper.Map<M_Districts>(districtDto);
            await _districtRepository.CreateDistrictAsync(district);

            return CreatedAtAction(nameof(GetDistrict), new { id = district.District_Id }, _mapper.Map<M_DistrictsDTO>(district));
        }

        // PUT: api/District/id
        [HttpPut]
        public async Task<IActionResult> PutDistrict(M_DistrictsDTO districtDto)
        {
            _logger.LogInformation($"Method PutDistrict({districtDto.District_Id}) invoked.");

            if (districtDto.District_Id<=0)
            {
                throw new BadHttpRequestException($"District with Id {districtDto.District_Id} not found.", StatusCodes.Status400BadRequest);
            }

            var district = _mapper.Map<M_Districts>(districtDto);
            try
            {
                await _districtRepository.UpdateDistrictAsync(district);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await DistrictExists(districtDto.District_Id))
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

        // DELETE: api/District/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistrict(int id)
        {
            _logger.LogInformation($"Method DeleteDistrict({id}) invoked.");

            var district = await _districtRepository.GetDistrictByIdAsync(id);
            if (district == null)
            {
                throw new BadHttpRequestException($"District with Id {id} not found.", StatusCodes.Status400BadRequest);
            }

            await _districtRepository.DeleteDistrictAsync(id);

            return NoContent();
        }

        private async Task<bool> DistrictExists(int id)
        {
            var district = await _districtRepository.GetDistrictByIdAsync(id);
            return district != null;
        }
    }
}
