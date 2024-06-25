namespace VuDucNam_L1.Models
{
    public class CertificateModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime IssuedDate { get; set; }
        public string? IssuedBy { get; set; } 
        public DateTime ExpiryDate { get; set; }
        public int EmployeeId { get; set; }
    }
}
