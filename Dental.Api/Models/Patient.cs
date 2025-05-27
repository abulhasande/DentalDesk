namespace Dental.Api.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string? Allergies { get; set; }
        public DateTime? CreatedAt { get; set; }
    } 
}
