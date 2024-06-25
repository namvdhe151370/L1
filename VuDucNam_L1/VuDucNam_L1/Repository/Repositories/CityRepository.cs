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
            var cities = await _context.Cities.Include(c => c.Districts).ToListAsync();
            return _mapper.Map<List<CityModel>>(cities);
        }
        public async Task<IEnumerable<CityModel>> GetAllAsync(int pageNumber, int pageSize)
        {
            var cities = await _context.Cities
                .Include(c => c.Districts)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CityModel>>(cities);
        }

        public async Task<CityModel> GetByIdAsync(int id)
        {
            var city = await _context.Cities
                .Include(c => c.Districts)
                .FirstOrDefaultAsync(c => c.CityId == id);
            return _mapper.Map<CityModel>(city);
        }

        public async Task<ValidationResult> AddAsync(CityModel cityModel)
        {
            var validationResult = await _validator.ValidateAsync(cityModel);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            var city = _mapper.Map<City>(cityModel);
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
            return validationResult;
        }

        public async Task<ValidationResult> UpdateAsync(CityModel cityModel)
        {
            var validationResult = await _validator.ValidateAsync(cityModel);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            var city = await _context.Cities.FindAsync(cityModel.CityId);
            if (city == null)
            {
                validationResult.Errors.Add(new ValidationFailure("CityId", "City not found"));
                return validationResult;
            }

            _mapper.Map(cityModel, city);
            await _context.SaveChangesAsync();
            return validationResult;
        }

        public async Task<ValidationResult> DeleteAsync(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                var validationResult = new ValidationResult();
                validationResult.Errors.Add(new ValidationFailure("CityId", "City not found"));
                return validationResult;
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            return new ValidationResult();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Cities.CountAsync();
        }
    }
}
