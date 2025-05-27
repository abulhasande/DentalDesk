namespace Dental.Api.Models.Dtos
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }
        public string DentistName { get; set; }
        public string PatientName { get; set; }
        public DateTime AppointDate { get; set; }
        public string AppointTime { get; set; }
    }
}
