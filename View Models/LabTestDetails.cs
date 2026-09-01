using Medical_Laboratory_Management_System.Models;
using Medical_Laboratory_Management_System.Models.Enums;

namespace Medical_Laboratory_Management_System.View_Models
{
    public class LabTestDetails
    {
        public int RequestedLabTestId { get; set; }
        public string RequestedLabTestName { get; set; } = string.Empty;
        public LabTestStatus LabTestStatus { get; set; }
        public string? LabTestResult { get; set; }
        public string? Notes { get; set; }
    }
}