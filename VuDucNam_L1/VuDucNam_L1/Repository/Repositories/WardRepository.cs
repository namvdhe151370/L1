using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using VuDucNam_L1.DataAccess;
using VuDucNam_L1.Migrations;
using VuDucNam_L1.Models;
using VuDucNam_L1.Repository.IRepositories;

namespace VuDucNam_L1.Repository.Repositories
{
    public class WardRepository : IWardRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public WardRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WardModel>> GetAllAsync()
        {
            var wards = await _context.Wards.AsNoTracking().ToListAsync();
            return _mapper.Map<List<WardModel>>(wards);
        }

        public async Task<IEnumerable<WardModel>> GetWardsByDistrictIdAsync(int districtId)
        {
            var wards = await _context.Wards
                .AsNoTracking()
                .Where(w => w.DistrictId == districtId)
                .ToListAsync();
            return _mapper.Map<List<WardModel>>(wards);
        }

        public async Task<IEnumerable<WardModel>> GetAllByPageAsync(int pageNumber, int pageSize)
        {
            var wards = await _context.Wards
                .AsNoTracking()
                .Include(w => w.District)
                .OrderBy(w => w.WardId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return _mapper.Map<IEnumerable<WardModel>>(wards);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Wards.AsNoTracking().CountAsync();
        }

        public async Task<WardModel> GetByIdAsync(int id)
        {
            var ward = await _context.Wards
                .AsNoTracking()
                .Include(w => w.District)
                .FirstOrDefaultAsync(w => w.WardId == id);

            return _mapper.Map<WardModel>(ward);
        }

        public async Task AddAsync(WardModel wardModel)
        {
            var ward = _mapper.Map<Ward>(wardModel);
            _context.Wards.Add(ward);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(WardModel wardModel)
        {
            var wardExists = await _context.Wards.AsNoTracking().AnyAsync(e => e.WardId == wardModel.WardId);
            if (!wardExists)
            {
                throw new Exception($"Ward with ID {wardModel.WardId} not found.");
            }
            var ward = await _context.Wards.FindAsync(wardModel.WardId);
            _mapper.Map(wardModel, ward);
            _context.Wards.Update(ward);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int wardId)
        {
            var wardExists = await _context.Wards.AsNoTracking().AnyAsync(e => e.WardId == wardId);
            if (!wardExists)
            {
                throw new Exception($"Ward with ID {wardId} not found.");
            }
            var ward = await _context.Wards.FindAsync(wardId);
            _context.Wards.Remove(ward);
            await _context.SaveChangesAsync();
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

        public async Task CheckWardIdAsync(int wardId, int districtId)
        {
            var wardExists = await _context.Wards.AsNoTracking().AnyAsync(e => e.WardId == wardId && e.DistrictId == districtId);
            if (!wardExists)
            {
                throw new Exception($"Ward with ID {wardId} and {districtId} not found.");
            }
        }
        public async Task<int> GetWardIdByNameAsync(string wardName)
        {
            var ward = await _context.Wards.AsNoTracking().FirstOrDefaultAsync(w => w.WardName == wardName);
            return ward == null ? throw new InvalidOperationException($"Ward '{wardName}' not found.") : ward.WardId;
        }
    }
}
