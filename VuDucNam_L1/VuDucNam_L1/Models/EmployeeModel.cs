using System.Numerics;
using VuDucNam_L1.DataAccess;

namespace VuDucNam_L1.Models
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public DateTime Dob { get; set; }
        public int Age { get; set; }
        public int EthnicId { get; set; }
        public virtual EthnicModel? Ethnic { get; set; }
        public int JobId { get; set; }
        public virtual JobModel? Job { get; set; }
        public string? CitizenNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public int CityId { get; set; }
        public virtual CityModel? City { get; set; }
        public int DistrictId { get; set; }
        public virtual DistrictModel? District { get; set; }
        public int WardId { get; set; }
        public virtual WardModel? Ward { get; set; }
        public string? SpecificAddress { get; set; }
        public List<CertificateModel> Certificates { get; set; } = new List<CertificateModel>();
    }
}
