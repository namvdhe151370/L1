using FluentValidation.Results;
using VuDucNam_L1.DataAccess;
using VuDucNam_L1.Models;

namespace VuDucNam_L1.Service.IServices
{
    public interface ICityService
    {
        Task<IEnumerable<CityModel>>  GetAllCities();
        Task<IEnumerable<CityModel>> GetAllAsync(int pageNumber, int pageSize);
        Task<CityModel> GetByIdAsync(int id);
        Task AddAsync(CityModel cityModel);
        Task UpdateAsync(CityModel cityModel);
        Task DeleteAsync(int id);
        Task<int> GetTotalCountAsync();

    }
}
