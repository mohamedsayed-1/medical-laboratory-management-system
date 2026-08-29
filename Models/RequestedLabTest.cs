using Medical_Laboratory_Management_System.Models.Enums;

namespace Medical_Laboratory_Management_System.Models
{
    public class RequestedLabTest
    {
        public int Id { get; set; }
        public LabTestStatus LabTestStatus { get; set; }
        public int? LabTestResultId { get; set; }
        public LabTestResult? LabTestResult { get; set; }
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        public int LabTestId { get; set; }
        public LabTest LabTest { get; set; }
    }
}