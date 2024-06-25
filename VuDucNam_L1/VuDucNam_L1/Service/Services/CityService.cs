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

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
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

        public async Task<ValidationResult> AddAsync(CityModel cityModel)
        {
            return await _cityRepository.AddAsync(cityModel);
        }

        public async Task<ValidationResult> UpdateAsync(CityModel cityModel)
        {
            return await _cityRepository.UpdateAsync(cityModel);
        }

        public async Task<ValidationResult> DeleteAsync(int id)
        {
            return await _cityRepository.DeleteAsync(id);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _cityRepository.GetTotalCountAsync();
        }
    }
}
