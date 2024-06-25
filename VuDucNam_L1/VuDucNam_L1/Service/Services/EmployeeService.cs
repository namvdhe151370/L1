using AutoMapper;
using ExcelDataReader;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VuDucNam_L1.Constants;
using VuDucNam_L1.DataAccess;
using VuDucNam_L1.Models;
using VuDucNam_L1.Repository.IRepositories;
using VuDucNam_L1.Service.IServices;
using VuDucNam_L1.Validation;

namespace VuDucNam_L1.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync(int pageNumber, int pageSize)
        {
            return await _employeeRepository.GetAllEmployeesAsync(pageNumber, pageSize);
        }

        public async Task<int> GetTotalEmployeeCountAsync()
        {
            return await _employeeRepository.GetTotalEmployeeCountAsync();
        }

        public async Task<EmployeeModel> GetEmployeeByIdAsync(int employeeId)
        {
            return await _employeeRepository.GetEmployeeByIdAsync(employeeId);
        }

        public async Task<IEnumerable<EmployeeModel>> GetEmployeesToExportAsync(List<int> employeeIds)
        {
            return await _employeeRepository.GetEmployeesToExportAsync(employeeIds);
        }

        public async Task CreateEmployeeToImportAsync(EmployeeImportModel employee)
        {
            await _employeeRepository.CreateEmployeeToImportAsync(employee);
        }

        public async Task<IEnumerable<CertificateModel>> GetCertificatesByEmployeeIdAsync(int employeeId)
        {
            return await _employeeRepository.GetCertificatesByEmployeeIdAsync(employeeId);
        }

        public async Task AddEmployeeAsync(EmployeeModel employee)
        {
            await _employeeRepository.AddEmployeeAsync(employee);
        }

        public async Task AddCertificatesAsync(int employeeId, IEnumerable<CertificateModel> certificates)
        {
            await _employeeRepository.AddCertificatesAsync(employeeId, certificates);
        }

        public async Task UpdateEmployeeAsync(EmployeeModel employee)
        {
            await _employeeRepository.UpdateEmployeeAsync(employee);
        }

        public async Task UpdateCertificatesAsync(EmployeeModel employee)
        {
            await _employeeRepository.UpdateCertificatesAsync(employee.EmployeeId, employee.Certificates);
        }

        public async Task DeleteEmployeeAsync(int employeeId)
        {
            await _employeeRepository.DeleteEmployeeAsync(employeeId);
        }

        public async Task DeleteCertificatesAsync(int employeeId)
        {
            await _employeeRepository.DeleteCertificatesAsync(employeeId);
        }

        public async Task<(List<EmployeeImportModel> Employees, List<string> Errors)> ValidateAndParseExcelAsync(IFormFile file)
        {
            var employees = new List<EmployeeImportModel>();
            var errors = new List<string>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;
                using var reader = ExcelReaderFactory.CreateReader(stream);
                var result = reader.AsDataSet(new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration { UseHeaderRow = true }
                });

                if (result.Tables.Count > 0)
                {
                    var table = result.Tables[0];
                    foreach (DataRow row in table.Rows)
                    {
                        // Check null
                        if (row.ItemArray.All(field => field is DBNull || string.IsNullOrWhiteSpace(field.ToString())))
                        {
                            continue;
                        }
                        try
                        {
                            var employee = new EmployeeImportModel
                            {
                                EmployeeName = row.Field<string>(0)?.Trim(),
                                Dob = row.Field<DateTime>(1),
                                Age = Convert.ToInt32(row.Field<double>(2)),
                                EthnicName = row.Field<string>(3)?.Trim(),
                                JobName = row.Field<string>(4)?.Trim(),
                                CitizenNumber = ParseCitizenNumber(row.Field<string>(5)?.Trim()),
                                PhoneNumber = ParsePhoneNumber(row.Field<string>(6)?.Trim()),
                                CityName = row.Field<string>(7)?.Trim(),
                                DistrictName = row.Field<string>(8)?.Trim(),
                                WardName = row.Field<string>(9)?.Trim(),
                                SpecificAddress = row.Field<string>(10)?.Trim()
                            };
                            employees.Add(employee);
                        }
                        catch (Exception ex)
                        {
                            errors.Add($"Error parsing row: {ex.Message}");
                        }
                    }
                }
            }

            return (employees, errors);
        }

        public MemoryStream ExportEmployeesToCsv(IEnumerable<EmployeeModel> employees)
        {
            int maxCertificates = employees.Max(e => e.Certificates?.Count ?? 0);
            var memoryStream = new MemoryStream();
            using (var writer = new StreamWriter(memoryStream, Encoding.UTF8, 1024, true))
            {
                var header = NotificationMessage.ExcelHeader;
                for (int i = 1; i <= maxCertificates; i++)
                {
                    header += $",CertificateName{i},IssuedDate{i},IssuedBy{i},ExpiryDate{i}";
                }
                writer.WriteLine(header);

                foreach (var employee in employees)
                {
                    var employeeDetails = $"{employee.EmployeeName}," +
                        $"{employee.Dob:dd/MM/yyyy},{employee.Age}," +
                        $"{employee.Ethnic.EthnicName},{employee.Job.JobName}," +
                        $"{employee.CitizenNumber ?? Validates.NoCitizenNumber}," +
                        $"{employee.PhoneNumber ?? Validates.NoPhoneNumber}," +
                        $"{employee.City.CityName},{employee.District.DistrictName}," +
                        $"{employee.Ward.WardName},{employee.SpecificAddress}";
                    var certificates = employee.Certificates != null ? employee.Certificates.Take(maxCertificates).ToList() : new List<CertificateModel>();
                    while (certificates.Count < maxCertificates)
                    {
                        certificates.Add(null);
                    }
                    var certificateDetails = string.Join(",", certificates.Select(c =>
                        c != null
                        ? $"{c.Name},{(c.IssuedDate != DateTime.MinValue ? c.IssuedDate.ToString(Validates.FormatDate) : "")},{c.IssuedBy},{(c.ExpiryDate != DateTime.MinValue ? c.ExpiryDate.ToString(Validates.FormatDate) : "")}"
                        : ",,,"));
                    writer.WriteLine($"{employeeDetails},{certificateDetails}");
                }
            }
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }

        private static string? ParseCitizenNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Equals(Validates.NoCitizenNumber, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return value;
        }

        private static string? ParsePhoneNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Equals(Validates.NoPhoneNumber, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return value;
        }
    }
}
