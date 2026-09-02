using System.ComponentModel.DataAnnotations;

namespace Medical_Laboratory_Management_System.View_Models
{
    public class AddLabTestResult
    {
        public required int RequestedLabTestId { get; set; }
        public required int AppointmentId { get; set; }

        public string? Notes {  get; set; }

        [Display(Name = "Lab Test Name")]
        public required string RequestedLabTestName { get; set; }
        
        [Required]
        [Display(Name ="Result")]
        [MinLength(1)]
        public string LabTestResult { get; set; } = string.Empty;
    }
}