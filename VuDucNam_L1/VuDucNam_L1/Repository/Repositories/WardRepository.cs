using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VuDucNam_L1.DataAccess;
using VuDucNam_L1.Models;
using VuDucNam_L1.Repository.IRepositories;

namespace VuDucNam_L1.Repository.Repositories
{
    public class WardRepository : IWardRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<WardModel> _validator;

        public WardRepository(AppDbContext context, IMapper mapper, IValidator<WardModel> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IEnumerable<WardModel>> GetAllAsync()
        {
            var wards = await _context.Wards.ToListAsync();
            return _mapper.Map<List<WardModel>>(wards);
        }

        public async Task<IEnumerable<WardModel>> GetWardsByDistrictIdAsync(int districtId)
        {
            var wards = await _context.Wards
                .Where(w => w.DistrictId == districtId)
                .ToListAsync();
            return _mapper.Map<List<WardModel>>(wards);
        }

        public async Task<IEnumerable<WardModel>> GetAllByPageAsync(int pageNumber, int pageSize)
        {
            var wards = await _context.Wards
                .Include(w => w.District)
                .OrderBy(w => w.WardId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return _mapper.Map<IEnumerable<WardModel>>(wards);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Wards.CountAsync();
        }

        public async Task<WardModel> GetByIdAsync(int id)
        {
            var ward = await _context.Wards
                .Include(w => w.District)
                .FirstOrDefaultAsync(w => w.WardId == id);

            return _mapper.Map<WardModel>(ward);
        }

        public async Task AddAsync(WardModel wardModel)
        {
            await _validator.ValidateAndThrowAsync(wardModel);

            var ward = _mapper.Map<Ward>(wardModel);
            _context.Wards.Add(ward);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(WardModel wardModel)
        {
            await _validator.ValidateAndThrowAsync(wardModel);

            var ward = await _context.Wards.FindAsync(wardModel.WardId);
            if (ward != null)
            {
                _mapper.Map(wardModel, ward);
                _context.Wards.Update(ward);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var ward = await _context.Wards.FindAsync(id);
            if (ward != null)
            {
                _context.Wards.Remove(ward);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetDistrictsSelectListAsync()
        {
            var districts = await _context.Districts.ToListAsync();
            return districts.Select(d => new SelectListItem
            {
                Value = d.DistrictId.ToString(),
                Text = d.DistrictName
            }).ToList();
        }
    }
}
