namespace Medical_Laboratory_Management_System.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date {  get; set; }
        public string? Notes { get; set; }
        public bool Urgent { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;
        public int DoctorId {  get; set; }
        public Doctor Doctor { get; set; } = null!;
        public ICollection<RequestedLabTest> RequestedLabTests { get; set; } = [];
    }
}