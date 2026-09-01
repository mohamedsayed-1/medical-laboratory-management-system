using Medical_Laboratory_Management_System.Models.Enums;

namespace Medical_Laboratory_Management_System.View_Models
{
    public class AppointmentDetailsViewModel
    {
        public int AppointmentId { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
        public bool Urgent { get; set; }
        public string DoctorName {  get; set; } = string.Empty;
        public string PatientName {  get; set; } = string.Empty;
        public string PatientPhoneNumber {  get; set; } = string.Empty;
        public string? PatientEmail { get; set; }
        public MaritalStatus? PatientMaritalStatus { get; set; }
        public Gender PatientGender { get; set; }
        public int PatientAge { get; set; }
        public List<LabTestDetails> LabTestsDetails { get; set; } = [];
    }
}