using Microsoft.AspNetCore.Mvc.Rendering;
using VuDucNam_L1.DataAccess;
using VuDucNam_L1.Models;

namespace VuDucNam_L1.Service.IServices
{
    public interface IWardService
    {
        Task<IEnumerable<WardModel>> GetAllAsync();
        Task<IEnumerable<WardModel>> GetWardsByDistrictIdAsync(int districtId);
        Task<IEnumerable<WardModel>> GetAllByPageAsync(int pageNumber, int pageSize);
        Task<WardModel> GetByIdAsync(int id);
        Task AddAsync(WardModel wardModel);
        Task UpdateAsync(WardModel wardModel);
        Task DeleteAsync(int id);
        Task<int> GetTotalCountAsync();
        Task<IEnumerable<SelectListItem>> GetDistrictsSelectListAsync();
    }

}
