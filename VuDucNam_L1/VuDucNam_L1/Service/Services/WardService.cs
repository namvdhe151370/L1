using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Rendering;
using VuDucNam_L1.DataAccess;
using VuDucNam_L1.Models;
using VuDucNam_L1.Repository.IRepositories;
using VuDucNam_L1.Service.IServices;

namespace VuDucNam_L1.Service.Services
{
    public class WardService : IWardService
    {
        private readonly IWardRepository _wardRepository;

        public WardService(IWardRepository wardRepository)
        {
            _wardRepository = wardRepository;
        }

        public async Task<IEnumerable<WardModel>> GetAllAsync()
        {
            return await _wardRepository.GetAllAsync();
        }

        public async Task<IEnumerable<WardModel>> GetWardsByDistrictIdAsync(int districtId)
        {
            return await _wardRepository.GetWardsByDistrictIdAsync(districtId);
        }
        public async Task<IEnumerable<WardModel>> GetAllByPageAsync(int pageNumber, int pageSize)
        {
            return await _wardRepository.GetAllByPageAsync(pageNumber, pageSize);
        }

        public async Task<WardModel> GetByIdAsync(int id)
        {
            return await _wardRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(WardModel wardModel)
        {
            await _wardRepository.AddAsync(wardModel);
        }

        public async Task UpdateAsync(WardModel wardModel)
        {
            await _wardRepository.UpdateAsync(wardModel);
        }

        public async Task DeleteAsync(int id)
        {
            await _wardRepository.DeleteAsync(id);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _wardRepository.GetTotalCountAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetDistrictsSelectListAsync()
        {
            return await _wardRepository.GetDistrictsSelectListAsync();
        }
    }
}
