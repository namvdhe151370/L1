using System.ComponentModel.DataAnnotations;

namespace VuDucNam_L1.DataAccess
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }

        [Required]
        public string? JobName { get; set; }
    }
}
