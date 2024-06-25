using System.ComponentModel.DataAnnotations;

namespace VuDucNam_L1.DataAccess
{
    public class Certificate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public DateTime IssuedDate { get; set; }

        [Required]
        public string? IssuedBy { get; set; }  

        public DateTime ExpiryDate { get; set; }

        public int EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }
    }

}
