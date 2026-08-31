namespace Medical_Laboratory_Management_System.View_Models
{
    public class IndexAppointmentViewModel
    {
        public int AppointmentId { get; set; }
        public required string PatientName { get; set; }
        public required string DoctorName { get; set; }
        public string? Notes { get; set; }
        public bool Urgent { get; set; }
        public DateTime Date {  get; set; }
    }
}