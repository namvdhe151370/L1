using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using VuDucNam_L1.DataAccess;
using VuDucNam_L1.Models;
using VuDucNam_L1.Repository.IRepositories;

namespace VuDucNam_L1.Repository.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<CityModel> _validator;

        public CityRepository(AppDbContext context, IMapper mapper, IValidator<CityModel> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IEnumerable<CityModel>> GetAllCities()
        {
            var cities = await _context.Cities.Include(c => c.Districts).AsNoTracking().ToListAsync();
            return _mapper.Map<List<CityModel>>(cities);
        }
        public async Task<IEnumerable<CityModel>> GetAllAsync(int pageNumber, int pageSize)
        {
            var cities = await _context.Cities
                .AsNoTracking()
                .Include(c => c.Districts)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CityModel>>(cities);
        }

        public async Task<CityModel> GetByIdAsync(int id)
        {
            var city = await _context.Cities.AsNoTracking()
                .Include(c => c.Districts)
                .FirstOrDefaultAsync(c => c.CityId == id);
            return _mapper.Map<CityModel>(city);
        }

        public async Task AddAsync(CityModel cityModel)
        {
            var city = _mapper.Map<City>(cityModel);
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CityModel cityModel)
        {
           await CheckCityIdAsync(cityModel.CityId);
            var city = await _context.Cities.FindAsync(cityModel.CityId);
            _mapper.Map(cityModel, city);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int CityId)
        {
            await CheckCityIdAsync(CityId);
            var city = await _context.Cities.FindAsync(CityId);
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Cities.AsNoTracking().CountAsync();
        }
        public async Task CheckCityIdAsync(int cityId)
        {
            var cityExists = await _context.Cities.AsNoTracking().AnyAsync(e => e.CityId == cityId);
            if (!cityExists)
            {
                throw new Exception($"City with ID {cityId} not found.");
            }
        }
        public async Task<int> GetCityIdByNameAsync(string cityName)
        {
            var city = await _context.Cities.AsNoTracking().FirstOrDefaultAsync(c => c.CityName == cityName);
            return city == null ? throw new InvalidOperationException($"City '{cityName}' not found.") : city.CityId;
        }
    }
}
