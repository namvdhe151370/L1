using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VuDucNam_L1.DataAccess;
using VuDucNam_L1.Models;
using VuDucNam_L1.Repository.IRepositories;

namespace VuDucNam_L1.Repository.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IJobRepository _jobRepository;
        private readonly IWardRepository _wardRepository;
        private readonly IEthnicRepository _ethnicRepository;
        private readonly IDistrictRepository _districtRepository;
        private readonly ICityRepository _cityRepository;

        public EmployeeRepository(
            AppDbContext context, 
            IMapper mapper, 
            IJobRepository jobRepository, 
            IWardRepository wardRepository, 
            IEthnicRepository ethnicRepository, 
            IDistrictRepository districtRepository, 
            ICityRepository cityRepository)
        {
            _context = context;
            _mapper = mapper;
            _jobRepository = jobRepository;
            _wardRepository = wardRepository;
            _ethnicRepository = ethnicRepository;
            _districtRepository = districtRepository;
            _cityRepository = cityRepository;
        }

        public async Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync(int pageNumber, int pageSize)
        {
            var employees = await _context.Employees
                .AsNoTracking()
                .Include(e => e.Ethnic)
                .Include(e => e.Job)
                .Include(e => e.City)
                .Include(e => e.District)
                .Include(e => e.Ward)
                .Include(e => e.Certificates)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return _mapper.Map<IEnumerable<EmployeeModel>>(employees);
        }

        public async Task<int> GetTotalEmployeeCountAsync()
        {
            return await _context.Employees.AsNoTracking().CountAsync();
        }

        public async Task<IEnumerable<EmployeeModel>> GetEmployeesToExportAsync(List<int> employeeIds)
        {
            var employees = await _context.Employees
                .AsNoTracking()
                .Include(e => e.Ethnic)
                .Include(e => e.Job)
                .Include(e => e.City)
                .Include(e => e.District)
                .Include(e => e.Ward)
                .Include(e => e.Certificates)
                .Where(e => employeeIds.Contains(e.EmployeeId))
                .ToListAsync();
            return _mapper.Map<IEnumerable<EmployeeModel>>(employees);
        }

        public async Task<EmployeeModel> PrepareEmployeeModelToImportAsync(EmployeeImportModel employeeImport)
        {
            int cityId = 0, districtId = 0, wardId = 0, ethnicId = 0, jobId = 0;
            if (!string.IsNullOrEmpty(employeeImport.CityName))
            {
                cityId = await _cityRepository.GetCityIdByNameAsync(employeeImport.CityName);
            }
            if (!string.IsNullOrEmpty(employeeImport.DistrictName))
            {
                districtId = await _districtRepository.GetDistrictIdByNameAsync(employeeImport.DistrictName);
            }
            if (!string.IsNullOrEmpty(employeeImport.WardName))
            {
                wardId = await _wardRepository.GetWardIdByNameAsync(employeeImport.WardName);
            }
            if (!string.IsNullOrEmpty(employeeImport.EthnicName))
            {
                ethnicId = await _ethnicRepository.GetEthnicIdByNameAsync(employeeImport.EthnicName);
            }
            if (!string.IsNullOrEmpty(employeeImport.JobName))
            {
                jobId = await _jobRepository.GetJobIdByNameAsync(employeeImport.JobName);
            }

            var employeeModel = new EmployeeModel
            {
                EmployeeName = employeeImport.EmployeeName,
                Dob = employeeImport.Dob,
                Age = employeeImport.Age,
                EthnicId = ethnicId,
                JobId = jobId,
                CitizenNumber = employeeImport.CitizenNumber,
                PhoneNumber = employeeImport.PhoneNumber,
                CityId = cityId,
                DistrictId = districtId,
                WardId = wardId,
                SpecificAddress = employeeImport.SpecificAddress
            };

            return employeeModel;
        }

        public async Task CreateEmployeeToImportAsync(EmployeeModel employeeModel)
        {
            var employee = _mapper.Map<Employee>(employeeModel);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<EmployeeModel> GetEmployeeByIdAsync(int employeeId)
        {
            var employee = await _context.Employees
                .AsNoTracking()
                .Include(e => e.Ethnic)
                .Include(e => e.Job)
                .Include(e => e.City)
                .Include(e => e.District)
                .Include(e => e.Ward)
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            return _mapper.Map<EmployeeModel>(employee);
        }

        public async Task<IEnumerable<CertificateModel>> GetCertificatesByEmployeeIdAsync(int employeeId)
        {
            var certificates = await _context.Certificates
                .AsNoTracking()
                .Where(c => c.EmployeeId == employeeId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CertificateModel>>(certificates);
        }

        public async Task AddEmployeeAsync(EmployeeModel employeeModel)
        {
            await _ethnicRepository.CheckEthnicIdAsync(employeeModel.EthnicId);
            await _jobRepository.CheckJobIdAsync(employeeModel.JobId);
            await _cityRepository.CheckCityIdAsync(employeeModel.CityId);
            await _districtRepository.CheckDistrictIdAsync(employeeModel.DistrictId , employeeModel.CityId);
            await _wardRepository.CheckWardIdAsync(employeeModel.WardId, employeeModel.DistrictId);
            var employee = _mapper.Map<Employee>(employeeModel);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            employeeModel.EmployeeId = employee.EmployeeId;
        }

        public async Task AddCertificatesAsync(int employeeId, IEnumerable<CertificateModel> certificates)
        {
            var employeeExists = await _context.Employees.AsNoTracking().AnyAsync(e => e.EmployeeId == employeeId);
            if (!employeeExists)
            {
                throw new ValidationException($"Employee with ID {employeeId} not found.");
            }

            var entities = certificates.Select(c => {
                var entity = _mapper.Map<Certificate>(c);
                entity.EmployeeId = employeeId;
                return entity;
            });
            await _context.Certificates.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(EmployeeModel employeeModel)
        {
            var employeeExists = await _context.Employees.AsNoTracking().AnyAsync(e => e.EmployeeId == employeeModel.EmployeeId);
            if (!employeeExists)
            {
                throw new Exception($"Employee with ID {employeeModel.EmployeeId} not found.");
            }
            await _ethnicRepository.CheckEthnicIdAsync(employeeModel.EthnicId);
            await _jobRepository.CheckJobIdAsync(employeeModel.JobId);
            await _cityRepository.CheckCityIdAsync(employeeModel.CityId);
            await _districtRepository.CheckDistrictIdAsync(employeeModel.DistrictId, employeeModel.CityId);
            await _wardRepository.CheckWardIdAsync(employeeModel.WardId, employeeModel.DistrictId);

            var existingEmployee = await _context.Employees.FindAsync(employeeModel.EmployeeId);
            _mapper.Map(employeeModel, existingEmployee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCertificatesAsync(int employeeId, IEnumerable<CertificateModel> certificates)
        {
            var employeeExists = await _context.Employees.AsNoTracking().AnyAsync(e => e.EmployeeId == employeeId);
            if (!employeeExists)
            {
                throw new ValidationException($"Employee with ID {employeeId} not found.");
            }

            var existingCertificates = await _context.Certificates
                .Where(c => c.EmployeeId == employeeId)
                .ToListAsync();
            _context.Certificates.RemoveRange(existingCertificates);

            var entities = certificates.Select(c => {
                var entity = _mapper.Map<Certificate>(c);
                entity.EmployeeId = employeeId;
                return entity;
            });
            await _context.Certificates.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var employeeExists = await _context.Employees.AsNoTracking().AnyAsync(e => e.EmployeeId == employeeId);
            if (!employeeExists)
            {
                throw new Exception($"Employee with ID {employeeId} not found.");
            }

            var employee = await _context.Employees.FindAsync(employeeId);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCertificatesAsync(int employeeId)
        {
            var certificates = await _context.Certificates
                .AsNoTracking()
                .Where(c => c.EmployeeId == employeeId)
                .ToListAsync();

            _context.Certificates.RemoveRange(certificates);
            await _context.SaveChangesAsync();
        }
    }
}
