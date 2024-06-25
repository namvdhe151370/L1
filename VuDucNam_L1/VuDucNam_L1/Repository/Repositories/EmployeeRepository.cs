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
        private readonly IValidator<EmployeeModel> _employeeValidator;
        private readonly IValidator<CertificateModel> _certificateValidator;

        public EmployeeRepository
            (AppDbContext context, IMapper mapper,
            IValidator<EmployeeModel> employeeValidator,
            IValidator<CertificateModel> certificateValidator)
        {
            _context = context;
            _mapper = mapper;
            _employeeValidator = employeeValidator;
            _certificateValidator = certificateValidator;
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

        public async Task CreateEmployeeToImportAsync(EmployeeImportModel employeeImport)
        {
            int cityId = 0, districtId = 0, wardId = 0, ethnicId = 0, jobId = 0;
            if (!string.IsNullOrEmpty(employeeImport.CityName))
            {
                cityId = await GetCityIdByNameAsync(employeeImport.CityName);
            }
            if (!string.IsNullOrEmpty(employeeImport.DistrictName))
            {
                districtId = await GetDistrictIdByNameAsync(employeeImport.DistrictName);
            }
            if (!string.IsNullOrEmpty(employeeImport.WardName))
            {
                wardId = await GetWardIdByNameAsync(employeeImport.WardName);
            }
            if (!string.IsNullOrEmpty(employeeImport.EthnicName))
            {
                ethnicId = await GetEthnicIdByNameAsync(employeeImport.EthnicName);
            }
            if (!string.IsNullOrEmpty(employeeImport.JobName))
            {
                jobId = await GetJobIdByNameAsync(employeeImport.JobName);
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

            var validationResult = await _employeeValidator.ValidateAsync(employeeModel);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var employee = _mapper.Map<Employee>(employeeModel);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        private async Task<int> GetCityIdByNameAsync(string cityName)
        {
            var city = await _context.Cities.AsNoTracking().FirstOrDefaultAsync(c => c.CityName == cityName);
            return city == null ? throw new InvalidOperationException($"City '{cityName}' not found.") : city.CityId;
        }

        private async Task<int> GetDistrictIdByNameAsync(string districtName)
        {
            var district = await _context.Districts.AsNoTracking().FirstOrDefaultAsync(d => d.DistrictName == districtName);
            return district == null ? throw new InvalidOperationException($"District '{districtName}' not found.") : district.DistrictId;
        }

        private async Task<int> GetWardIdByNameAsync(string wardName)
        {
            var ward = await _context.Wards.AsNoTracking().FirstOrDefaultAsync(w => w.WardName == wardName);
            return ward == null ? throw new InvalidOperationException($"Ward '{wardName}' not found.") : ward.WardId;
        }

        private async Task<int> GetEthnicIdByNameAsync(string ethnicName)
        {
            var ethnic = await _context.Ethnics.AsNoTracking().FirstOrDefaultAsync(e => e.EthnicName == ethnicName);
            return ethnic == null ? throw new InvalidOperationException($"Ethnic '{ethnicName}' not found.") : ethnic.EthnicId;
        }

        private async Task<int> GetJobIdByNameAsync(string jobName)
        {
            var job = await _context.Jobs.AsNoTracking().FirstOrDefaultAsync(j => j.JobName == jobName);
            return job == null ? throw new InvalidOperationException($"Job '{jobName}' not found.") : job.JobId;
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
            var validationResult = await _employeeValidator.ValidateAsync(employeeModel);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

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

            foreach (var cert in certificates)
            {
                var validationResult = await _certificateValidator.ValidateAsync(cert);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }
                cert.EmployeeId = employeeId;
                var certificateEntity = _mapper.Map<Certificate>(cert);
                var existingCertificate = await _context.Certificates
                    .AsNoTracking()
                    .AnyAsync(c => c.EmployeeId == employeeId && c.Name == cert.Name && c.IssuedDate == cert.IssuedDate);

                if (!existingCertificate)
                {
                    _context.Certificates.Add(certificateEntity);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(EmployeeModel employeeModel)
        {
            var validationResult = await _employeeValidator.ValidateAsync(employeeModel);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var employeeExists = await _context.Employees.AsNoTracking().AnyAsync(e => e.EmployeeId == employeeModel.EmployeeId);
            if (!employeeExists)
            {
                throw new Exception($"Employee with ID {employeeModel.EmployeeId} not found.");
            }

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

            foreach (var cert in certificates)
            {
                var validationResult = await _certificateValidator.ValidateAsync(cert);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                cert.EmployeeId = employeeId;
                var certificateEntity = _mapper.Map<Certificate>(cert);
                _context.Certificates.Add(certificateEntity);
            }
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
