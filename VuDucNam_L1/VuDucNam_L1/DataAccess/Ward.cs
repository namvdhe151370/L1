using System.ComponentModel.DataAnnotations;

namespace VuDucNam_L1.DataAccess
{
    public class Ward
    {
        [Key]
        public int WardId { get; set; }

        [Required]
        public string? WardName { get; set; }

        public int DistrictId { get; set; }
        public virtual District? District { get; set; }
    }
}
