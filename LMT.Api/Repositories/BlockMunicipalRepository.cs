using LMT.Api.Interfaces;
using LMT.Api.Entities;
using LMT.Api.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMT.Api.Repositories
{
    public class BlockMunicipalRepository(EFDBContext dbContext) : IBlockMunicipalRepository
    {
        private readonly EFDBContext _dbContext = dbContext;
        public async Task CreateBlockMunicipalAsync(M_BlockMunicipals blockMunicipal)
        {
            _dbContext.M_BlockMunicipals.Add(blockMunicipal);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBlockMunicipalAsync(int blockMunicipalId)
        {
            var blockMunicipal = await _dbContext.M_BlockMunicipals.FindAsync(blockMunicipalId);
            if (blockMunicipal != null)
            {
                _dbContext.M_BlockMunicipals.Remove(blockMunicipal);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<M_BlockMunicipals>> GetAllBlockMunicipalAsync()
        {
            return await _dbContext.M_BlockMunicipals.ToListAsync();
        }

        public async Task<M_BlockMunicipals> GetBlockMunicipalByIdAsync(int blockMunicipalId)
        {
            return await _dbContext.M_BlockMunicipals.FindAsync(blockMunicipalId);
        }

        public async Task UpdateBlockMunicipalAsync(M_BlockMunicipals blockMunicipal)
        {
            _dbContext.Entry(blockMunicipal).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
