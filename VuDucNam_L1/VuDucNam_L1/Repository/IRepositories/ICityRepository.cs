using FluentValidation.Results;
using VuDucNam_L1.DataAccess;
using VuDucNam_L1.Models;

namespace VuDucNam_L1.Repository.IRepositories
{
    public interface ICityRepository
    {
        Task<IEnumerable<CityModel>> GetAllCities();
        Task<IEnumerable<CityModel>> GetAllAsync(int pageNumber, int pageSize);
        Task<CityModel> GetByIdAsync(int id);
        Task<ValidationResult> AddAsync(CityModel cityModel);
        Task<ValidationResult> UpdateAsync(CityModel cityModel);
        Task<ValidationResult> DeleteAsync(int id);
        Task<int> GetTotalCountAsync();
    }
}
