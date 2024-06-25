using VuDucNam_L1.DataAccess;
using VuDucNam_L1.Models;

namespace VuDucNam_L1.Repository.IRepositories
{
    public interface IDistrictRepository
    {
        Task<IEnumerable<DistrictModel>> GetAllDistrictAsync();
        Task<IEnumerable<DistrictModel>> GetDistrictsByCityIdAsync(int cityId);
        Task<IEnumerable<DistrictModel>> GetAllByPageAsync(int pageNumber, int pageSize);
        Task<DistrictModel> GetByIdAsync(int id);
        Task AddAsync(DistrictModel districtModel);
        Task UpdateAsync(DistrictModel districtModel);
        Task DeleteAsync(int id);
        Task<int> GetTotalCountAsync();
        Task<IEnumerable<DistrictModel>> GetByCityIdAsync(int cityId, int pageNumber, int pageSize);
    }
}
