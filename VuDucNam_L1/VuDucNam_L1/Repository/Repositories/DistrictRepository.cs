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
        private readonly IValidator<DistrictModel> _validator;

        public DistrictRepository(AppDbContext context, IMapper mapper, IValidator<DistrictModel> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IEnumerable<DistrictModel>> GetAllDistrictAsync()
        {
            var districts = await _context.Districts.ToListAsync();
            return _mapper.Map<List<DistrictModel>>(districts);
        }

        public async Task<IEnumerable<DistrictModel>> GetDistrictsByCityIdAsync(int cityId)
        {
            var districts = await _context.Districts
                                    .Where(d => d.CityId == cityId)
                                    .ToListAsync();
            return _mapper.Map<List<DistrictModel>>(districts);
        }

        public async Task<IEnumerable<DistrictModel>> GetAllByPageAsync(int pageNumber, int pageSize)
        {
            var districts = await _context.Districts
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
            await _validator.ValidateAndThrowAsync(districtModel);
            var district = _mapper.Map<District>(districtModel);
            _context.Districts.Add(district);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DistrictModel districtModel)
        {
            await _validator.ValidateAndThrowAsync(districtModel);
            var district = _mapper.Map<District>(districtModel);
            _context.Districts.Update(district);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var district = await _context.Districts.FindAsync(id);
            if (district != null)
            {
                _context.Districts.Remove(district);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Districts.CountAsync();
        }

        public async Task<IEnumerable<DistrictModel>> GetByCityIdAsync(int cityId, int pageNumber, int pageSize)
        {
            var districts = await _context.Districts
                .Where(d => d.CityId == cityId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return _mapper.Map<IEnumerable<DistrictModel>>(districts);
        }
    }

}
