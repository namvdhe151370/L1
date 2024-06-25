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
    }
}
