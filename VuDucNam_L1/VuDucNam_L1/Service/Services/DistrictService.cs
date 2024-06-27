using FluentValidation;
using VuDucNam_L1.DataAccess;
using VuDucNam_L1.Models;
using VuDucNam_L1.Repository.IRepositories;
using VuDucNam_L1.Service.IServices;

namespace VuDucNam_L1.Service.Services
{
    public class DistrictService : IDistrictService
    {
        private readonly IDistrictRepository _districtRepository;
        private readonly IValidator<DistrictModel> _validator;

        public DistrictService(IDistrictRepository districtRepository, IValidator<DistrictModel> validator)
        {
            _districtRepository = districtRepository;
            _validator = validator;
        }

        public async Task<IEnumerable<DistrictModel>> GetAllDistrictAsync()
        {
            return await _districtRepository.GetAllDistrictAsync();
        }

        public async Task<IEnumerable<DistrictModel>> GetDistrictsByCityIdAsync(int cityId)
        {
            return await _districtRepository.GetDistrictsByCityIdAsync(cityId);
        }

        public async Task<IEnumerable<DistrictModel>> GetAllByPageAsync(int pageNumber, int pageSize)
        {
            return await _districtRepository.GetAllByPageAsync(pageNumber, pageSize);
        }

        public async Task<DistrictModel> GetByIdAsync(int id)
        {
            return await _districtRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(DistrictModel districtModel)
        {
            var validationResult = await _validator.ValidateAsync(districtModel);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            await _districtRepository.AddAsync(districtModel);
        }

        public async Task UpdateAsync(DistrictModel districtModel)
        {
            var validationResult = await _validator.ValidateAsync(districtModel);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            await _districtRepository.UpdateAsync(districtModel);
        }

        public async Task DeleteAsync(int id)
        {
            await _districtRepository.DeleteAsync(id);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _districtRepository.GetTotalCountAsync();
        }

        public async Task<IEnumerable<DistrictModel>> GetByCityIdAsync(int cityId, int pageNumber, int pageSize)
        {
            return await _districtRepository.GetByCityIdAsync(cityId, pageNumber, pageSize);
        }
    }

}
