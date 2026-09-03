using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Medical_Laboratory_Management_System.View_Models
{
    public class EditAppointmentViewModel
    {
        public int Id { get; set; }
        public string? Notes { get; set; }
        public bool Urgent { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date {  get; set; }
        public IEnumerable<SelectListItem> Doctors { get; set; } = [];
        [Required]
        [Display(Name = "Doctor")]
        public int DoctorId { get; set; }
        public string PatientName { get; set; } = string.Empty;
    }
}