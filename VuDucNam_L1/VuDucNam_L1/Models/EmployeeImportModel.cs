using System.ComponentModel.DataAnnotations;

namespace VuDucNam_L1.Models
{
    public class EmployeeImportModel
    {
        public string? EmployeeName { get; set; }
        public DateTime Dob { get; set; }
        public int Age { get; set; }
        public string? EthnicName { get; set; }
        public string? JobName { get; set; }
        public string? CitizenNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? CityName { get; set; }
        public string? DistrictName { get; set; }
        public string? WardName { get; set; }
        public string? SpecificAddress { get; set; }
    }
}
