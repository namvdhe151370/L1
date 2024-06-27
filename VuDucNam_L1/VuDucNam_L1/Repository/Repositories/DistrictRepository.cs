using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VuDucNam_L1.DataAccess;
using VuDucNam_L1.Models;
using VuDucNam_L1.Repository.IRepositories;

namespace VuDucNam_L1.Repository.Repositories
{
    public class DistrictRepository : IDistrictRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DistrictRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DistrictModel>> GetAllDistrictAsync()
        {
            var districts = await _context.Districts.AsNoTracking().ToListAsync();
            return _mapper.Map<List<DistrictModel>>(districts);
        }

        public async Task<IEnumerable<DistrictModel>> GetDistrictsByCityIdAsync(int cityId)
        {
            var districts = await _context.Districts
                                    .AsNoTracking()
                                    .Where(d => d.CityId == cityId)
                                    .ToListAsync();
            return _mapper.Map<List<DistrictModel>>(districts);
        }

        public async Task<IEnumerable<DistrictModel>> GetAllByPageAsync(int pageNumber, int pageSize)
        {
            var districts = await _context.Districts.AsNoTracking()
                .Include(d => d.City)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return _mapper.Map<IEnumerable<DistrictModel>>(districts);
        }

        public async Task<DistrictModel> GetByIdAsync(int id)
        {
            var district = await _context.Districts.FindAsync(id);
            return _mapper.Map<DistrictModel>(district);
        }

        public async Task AddAsync(DistrictModel districtModel)
        {
            var district = _mapper.Map<District>(districtModel);
            _context.Districts.Add(district);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DistrictModel districtModel)
        {
            var districtExists = await _context.Districts.AsNoTracking().AnyAsync(e => e.DistrictId == districtModel.DistrictId);
            if (!districtExists)
            {
                throw new Exception($"District with ID {districtModel.DistrictId} not found.");
            }
            var district = _mapper.Map<District>(districtModel);
            _context.Districts.Update(district);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var districtExists = await _context.Districts.AsNoTracking().AnyAsync(e => e.DistrictId == id);
            if (!districtExists)
            {
                throw new Exception($"District with ID {id} not found.");
            }
            var district = await _context.Districts.FindAsync(id);
            _context.Districts.Remove(district);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Districts.AsNoTracking().CountAsync();
        }

        public async Task<IEnumerable<DistrictModel>> GetByCityIdAsync(int cityId, int pageNumber, int pageSize)
        {
            var districts = await _context.Districts.AsNoTracking()
                .Where(d => d.CityId == cityId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return _mapper.Map<IEnumerable<DistrictModel>>(districts);
        }
        public async Task CheckDistrictIdAsync(int districtId, int cityId)
        {
            var districtExists = await _context.Districts.AsNoTracking().AnyAsync(e => e.DistrictId == districtId && e.CityId == cityId);
            if (!districtExists)
            {
                throw new Exception($"District with ID {districtId} and CityId {cityId} not found.");
            }
        }
        public async Task<int> GetDistrictIdByNameAsync(string districtName)
        {
            var district = await _context.Districts.AsNoTracking().FirstOrDefaultAsync(d => d.DistrictName == districtName);
            return district == null ? throw new InvalidOperationException($"District '{districtName}' not found.") : district.DistrictId;
        }
    }
}
