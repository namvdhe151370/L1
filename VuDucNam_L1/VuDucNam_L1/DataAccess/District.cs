using System.ComponentModel.DataAnnotations;

namespace VuDucNam_L1.DataAccess
{
    public class District
    {
        [Key]
        public int DistrictId { get; set; }

        [Required]
        public string? DistrictName { get; set; }

        public int CityId { get; set; }
        public virtual City? City { get; set; }

        public virtual ICollection<Ward> Wards { get; set; } = new List<Ward>();
    }
}
