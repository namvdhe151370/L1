using System.ComponentModel.DataAnnotations;

namespace VuDucNam_L1.DataAccess
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Required]
        public string? CityName { get; set; }

        public virtual ICollection<District> Districts { get; set; } = new List<District>();
    }
}
