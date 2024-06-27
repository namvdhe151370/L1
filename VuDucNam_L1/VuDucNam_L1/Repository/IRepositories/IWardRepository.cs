using Microsoft.AspNetCore.Mvc.Rendering;
using VuDucNam_L1.DataAccess;
using VuDucNam_L1.Models;

namespace VuDucNam_L1.Repository.IRepositories
{
    public interface IWardRepository
    {
        Task<IEnumerable<WardModel>> GetAllAsync();
        Task<IEnumerable<WardModel>> GetWardsByDistrictIdAsync(int districtId);
        Task<IEnumerable<WardModel>> GetAllByPageAsync(int pageNumber, int pageSize);
        Task<int> GetTotalCountAsync();
        Task<WardModel> GetByIdAsync(int id);
        Task AddAsync(WardModel wardModel);
        Task UpdateAsync(WardModel wardModel);
        Task DeleteAsync(int id);
        Task<IEnumerable<SelectListItem>> GetDistrictsSelectListAsync();
        Task CheckWardIdAsync(int wardId, int districtId);
        Task<int> GetWardIdByNameAsync(string wardName);
    }

}
