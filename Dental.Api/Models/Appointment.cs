namespace Dental.Api.Models
{
    public class Appointment
    { 
        public int AppointmentId { get; set; }
        public string DentistName { get; set; }
        public string PatientName { get; set; }
        public DateTime AppointDate { get; set; }
        public string AppointTime { get; set; }
    }
}
