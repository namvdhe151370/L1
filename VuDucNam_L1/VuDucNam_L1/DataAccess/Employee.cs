using System.ComponentModel.DataAnnotations;

namespace VuDucNam_L1.DataAccess
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        public string? EmployeeName { get; set; }

        public DateTime Dob { get; set; }

        public int Age { get; set; }

        public int EthnicId { get; set; }
        public virtual Ethnic? Ethnic { get; set; }

        public int JobId { get; set; }
        public virtual Job? Job { get; set; }

        public string? CitizenNumber { get; set; }

        public string? PhoneNumber { get; set; }

        public int CityId { get; set; }
        public virtual City? City { get; set; }

        public int DistrictId { get; set; }
        public virtual District? District { get; set; }

        public int WardId { get; set; }
        public virtual Ward? Ward { get; set; }

        public string? SpecificAddress { get; set; } 

        public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
    }
}
