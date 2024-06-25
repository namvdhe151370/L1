using Microsoft.AspNetCore.Mvc.Rendering;

namespace VuDucNam_L1.Models
{
    public class WardModel
    {
        public int WardId { get; set; }
        public string? WardName { get; set; }
        public int DistrictId { get; set; }
        public string? DistrictName { get; set; }
        public IEnumerable<SelectListItem>? Districts { get; set; }
    }
}
