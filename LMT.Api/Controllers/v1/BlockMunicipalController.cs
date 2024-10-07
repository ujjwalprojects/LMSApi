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
    public class BlockMunicipalController : ControllerBase
    {
        private readonly IBlockMunicipalRepository _blockMunicipalRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BlockMunicipalController> _logger;

        public BlockMunicipalController(IBlockMunicipalRepository blockMunicipalRepository, IMapper mapper, ILogger<BlockMunicipalController> logger)
        {
            _blockMunicipalRepository = blockMunicipalRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/BlockMunicipal
        [HttpGet]
        public async Task<ActionResult<List<M_BlockMunicipalsDTO>>> GetBlockMunicipals()
        {
            _logger.LogInformation("Method GetBlockMunicipals invoked.");

            var blockMunicipals = await _blockMunicipalRepository.GetAllBlockMunicipalAsync();
            return Ok(_mapper.Map<List<M_BlockMunicipalsDTO>>(blockMunicipals));
        }

        // GET: api/BlockMunicipal/id
        [HttpGet("{id}")]
        public async Task<ActionResult<M_BlockMunicipalsDTO>> GetBlockMunicipal(int id)
        {
            _logger.LogInformation($"Method GetBlockMunicipal({id}) invoked.");

            var blockMunicipal = await _blockMunicipalRepository.GetBlockMunicipalByIdAsync(id);
            if (blockMunicipal == null)
            {
                throw new BadHttpRequestException($"BlockMunicipal with Id {id} not found.", StatusCodes.Status400BadRequest);
            }
            return Ok(_mapper.Map<M_BlockMunicipalsDTO>(blockMunicipal));
        }

        // POST: api/BlockMunicipal
        [HttpPost]
        public async Task<ActionResult<M_BlockMunicipalsDTO>> PostBlockMunicipal(M_BlockMunicipalsDTO blockMunicipalDto)
        {
            _logger.LogInformation("Method PostBlockMunicipal invoked.");

            var blockMunicipal = _mapper.Map<M_BlockMunicipals>(blockMunicipalDto);
            await _blockMunicipalRepository.CreateBlockMunicipalAsync(blockMunicipal);

            return CreatedAtAction(nameof(GetBlockMunicipal), new { id = blockMunicipal.Block_Municipal_Id }, _mapper.Map<M_BlockMunicipalsDTO>(blockMunicipal));
        }

        // PUT: api/BlockMunicipal/
        [HttpPut]
        public async Task<IActionResult> PutBlockMunicipal(M_BlockMunicipalsDTO blockMunicipalDto)
        {
            _logger.LogInformation($"Method PutBlockMunicipal({blockMunicipalDto.Block_Municipal_Id}) invoked.");

            if (blockMunicipalDto.Block_Municipal_Id <= 0)
            {
                throw new BadHttpRequestException($"BlockMunicipal with Id {blockMunicipalDto.Block_Municipal_Id} not found.", StatusCodes.Status400BadRequest);
            }

            var blockMunicipal = _mapper.Map<M_BlockMunicipals>(blockMunicipalDto);
            try
            {
                await _blockMunicipalRepository.UpdateBlockMunicipalAsync(blockMunicipal);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BlockMunicipalExists(blockMunicipalDto.Block_Municipal_Id))
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

        // DELETE: api/BlockMunicipal/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlockMunicipal(int id)
        {
            _logger.LogInformation($"Method DeleteBlockMunicipal({id}) invoked.");

            var blockMunicipal = await _blockMunicipalRepository.GetBlockMunicipalByIdAsync(id);
            if (blockMunicipal == null)
            {
                throw new BadHttpRequestException($"BlockMunicipal with Id {id} not found.", StatusCodes.Status400BadRequest);
            }

            await _blockMunicipalRepository.DeleteBlockMunicipalAsync(id);

            return NoContent();
        }

        private async Task<bool> BlockMunicipalExists(int id)
        {
            var blockMunicipal = await _blockMunicipalRepository.GetBlockMunicipalByIdAsync(id);
            return blockMunicipal != null;
        }
    }
}
