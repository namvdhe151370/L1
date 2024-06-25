namespace VuDucNam_L1.Models
{
    public class CityModel
    {
        public int CityId { get; set; }
        public string? CityName { get; set; }
        public List<DistrictModel> Districts { get; set; } = new List<DistrictModel>();
    }
}
