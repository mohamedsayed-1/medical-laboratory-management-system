using Medical_Laboratory_Management_System.Models.Enums;

namespace Medical_Laboratory_Management_System.Models
{
    public class RequestedLabTest
    {
        public int Id { get; set; }
        public LabTestStatus LabTestStatus { get; private set; }
        public int? LabTestResultId { get; set; }
        public LabTestResult? LabTestResult { get; private set; }
        public int AppointmentId { get; set; }
        public required Appointment Appointment { get; set; }
        public int LabTestId { get; set; }
        public required LabTest LabTest { get; set; }
        public static RequestedLabTest Create(Appointment appointment, LabTest labTest)
        {
            return new RequestedLabTest()
            {
                LabTestStatus = LabTestStatus.Queued,
                Appointment = appointment,
                LabTest = labTest
            };
        }
        public void CompleteWithResult(LabTestResult result)
        {
            LabTestResult = result;
            LabTestStatus = LabTestStatus.Completed;
        }
    }
}