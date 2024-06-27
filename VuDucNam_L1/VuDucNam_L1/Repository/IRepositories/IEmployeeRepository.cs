using VuDucNam_L1.DataAccess;
using VuDucNam_L1.Models;

namespace VuDucNam_L1.Repository.IRepositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync(int pageNumber, int pageSize);
        Task<int> GetTotalEmployeeCountAsync();
        Task<EmployeeModel> GetEmployeeByIdAsync(int employeeId);
        Task<IEnumerable<EmployeeModel>> GetEmployeesToExportAsync(List<int> employeeIds);
        Task<EmployeeModel> PrepareEmployeeModelToImportAsync(EmployeeImportModel employeeImport);
        Task CreateEmployeeToImportAsync(EmployeeModel employeeModel);
        Task<IEnumerable<CertificateModel>> GetCertificatesByEmployeeIdAsync(int employeeId);
        Task AddEmployeeAsync(EmployeeModel employee);
        Task AddCertificatesAsync(int employeeId, IEnumerable<CertificateModel> certificates);
        Task UpdateEmployeeAsync(EmployeeModel employee);
        Task UpdateCertificatesAsync(int employeeId, IEnumerable<CertificateModel> certificates);
        Task DeleteEmployeeAsync(int employeeId);
        Task DeleteCertificatesAsync(int employeeId);
    }
}
