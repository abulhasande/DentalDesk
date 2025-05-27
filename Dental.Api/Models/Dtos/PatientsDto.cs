using System.ComponentModel.DataAnnotations;

namespace Dental.Api.Models.Dtos
{
    public class Patient
    {

        public int PatientId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public double Email { get; set; }
        public string Address { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string? Allergies { get; set; }
        DateTime? CreatedAt { get; set; }
    }
}
