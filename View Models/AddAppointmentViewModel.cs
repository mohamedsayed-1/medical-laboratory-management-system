using System.ComponentModel.DataAnnotations;
using Medical_Laboratory_Management_System.Models.Enums;

namespace Medical_Laboratory_Management_System.View_Models
{
    public class AddAppointmentViewModel
    {
        // Appointment information
        [Required]
        public DateTime? Date { get; set; } = DateTime.Now;
        public string? Notes { get; set; }
        public bool Urgent { get; set; }


        // Patient information
        [Required]
        [Display(Name ="Patient Name")]
        public string PatientName { get; set; }
        
        [Required]
        [Range(1, 120)]
        [Display(Name = "Patient Age")]
        public int? PatientAge {  get; set; }
        
        [Required]
        [Display(Name = "Patient Gender")]
        public Gender? PatientGender {  get; set; }
        
        [Required]
        [Display(Name = "Patient Phone Number")]
        public string PatientPhoneNumber { get; set; }
        
        [EmailAddress]
        [Display(Name = "Patient Email")]
        public string? PatientEmail { get; set; }
        
        [Display(Name = "Patient Marital Status")]
        public MaritalStatus? PatientMaritalStatus { get; set; } 

        // Doctor information
        [Required]
        public int DoctorId { get; set; }

        [Required]
        [Display(Name = "Lab Tests")]
        public List<int> LabTestsIds { get; set; }
    }
}