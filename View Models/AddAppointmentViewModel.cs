using System.ComponentModel.DataAnnotations;
using Medical_Laboratory_Management_System.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Medical_Laboratory_Management_System.View_Models
{
    public class AddAppointmentViewModel
    {
        // Appointment information
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; } = DateTime.Now;
        public string? Notes { get; set; }
        public bool Urgent { get; set; }


        // Patient information
        [Required]
        [Display(Name = "Patient Name")]
        public string PatientName { get; set; } = string.Empty;
        
        [Required]
        [Range(1, 120)]
        [Display(Name = "Patient Age")]
        public int? PatientAge {  get; set; }
        
        [Required]
        [Display(Name = "Patient Gender")]
        public Gender? PatientGender {  get; set; }
        
        [Required]
        [Display(Name = "Patient Phone Number")]
        public string PatientPhoneNumber { get; set; } = string.Empty;
        
        [EmailAddress]
        [Display(Name = "Patient Email")]
        public string? PatientEmail { get; set; }
        
        [Display(Name = "Patient Marital Status")]
        public MaritalStatus? PatientMaritalStatus { get; set; } 

        // Doctor information
        [Required]
        [Display(Name ="Doctor")]
        public int DoctorId { get; set; }
        public IEnumerable<SelectListItem> Doctors { get; set; } = [];

        [Required]
        [MinLength(1, ErrorMessage ="Please select at least one lab test.")]
        [Display(Name = "Lab Tests")]
        public List<int> LabTestsIds { get; set; } = [];
        public IEnumerable<SelectListItem> LabTests { get; set; } = [];
    }
}