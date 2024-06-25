using VuDucNam_L1.Models;

namespace VuDucNam_L1.Service.IServices
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync(int pageNumber, int pageSize);
        Task<int> GetTotalEmployeeCountAsync();
        Task<EmployeeModel> GetEmployeeByIdAsync(int employeeId);
        Task<IEnumerable<EmployeeModel>> GetEmployeesToExportAsync(List<int> employeeIds);
        Task CreateEmployeeToImportAsync(EmployeeImportModel employee);
        Task<IEnumerable<CertificateModel>> GetCertificatesByEmployeeIdAsync(int employeeId);
        Task AddEmployeeAsync(EmployeeModel employee);
        Task AddCertificatesAsync(int employeeId, IEnumerable<CertificateModel> certificates);
        Task UpdateEmployeeAsync(EmployeeModel employee);
        Task UpdateCertificatesAsync(EmployeeModel employee);
        Task DeleteEmployeeAsync(int employeeId);
        Task DeleteCertificatesAsync(int employeeId);
        Task<(List<EmployeeImportModel> Employees, List<string> Errors)> ValidateAndParseExcelAsync(IFormFile file);
        MemoryStream ExportEmployeesToCsv(IEnumerable<EmployeeModel> employees);
    }
}
