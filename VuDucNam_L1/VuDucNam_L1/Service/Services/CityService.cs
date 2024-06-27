using FluentValidation;
using FluentValidation.Results;
using VuDucNam_L1.DataAccess;
using VuDucNam_L1.Models;
using VuDucNam_L1.Repository.IRepositories;
using VuDucNam_L1.Service.IServices;

namespace VuDucNam_L1.Service.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IValidator<CityModel> _validator;

        public CityService(ICityRepository cityRepository, IValidator<CityModel> validator)
        {
            _cityRepository = cityRepository;
            _validator = validator;
        }

        public async Task<IEnumerable<CityModel>> GetAllCities()
        {
            return await _cityRepository.GetAllCities();
        }

        public async Task<IEnumerable<CityModel>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _cityRepository.GetAllAsync(pageNumber, pageSize);
        }

        public async Task<CityModel> GetByIdAsync(int id)
        {
            return await _cityRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(CityModel cityModel)
        {
            var validationResult = await _validator.ValidateAsync(cityModel);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            await _cityRepository.AddAsync(cityModel);
        }

        public async Task UpdateAsync(CityModel cityModel)
        {
            var validationResult = await _validator.ValidateAsync(cityModel);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            await _cityRepository.UpdateAsync(cityModel);
        }

        public async Task DeleteAsync(int id)
        {
            await _cityRepository.DeleteAsync(id);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _cityRepository.GetTotalCountAsync();
        }
    }
}
