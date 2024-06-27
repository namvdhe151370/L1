using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VuDucNam_L1.DataAccess;
using VuDucNam_L1.Models;
using VuDucNam_L1.Repository.IRepositories;

namespace VuDucNam_L1.Repository.Repositories
{
    public class EthnicRepository : IEthnicRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EthnicRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EthnicModel>> GetAllEthnicAsync()
        {
            var ethnics = await _context.Ethnics.ToListAsync();
            return _mapper.Map<List<EthnicModel>>(ethnics);
        }
        public async Task CheckEthnicIdAsync(int ethnicId)
        {
            var ethnicExists = await _context.Ethnics.AsNoTracking().AnyAsync(e => e.EthnicId == ethnicId);
            if (!ethnicExists)
            {
                throw new Exception($"Ethnic with ID {ethnicId} not found.");
            }
        }
        public async Task<int> GetEthnicIdByNameAsync(string ethnicName)
        {
            var ethnic = await _context.Ethnics.AsNoTracking().FirstOrDefaultAsync(e => e.EthnicName == ethnicName);
            return ethnic == null ? throw new InvalidOperationException($"Ethnic '{ethnicName}' not found.") : ethnic.EthnicId;
        }
    }
}
