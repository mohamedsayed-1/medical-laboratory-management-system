using System.ComponentModel.DataAnnotations;
using Medical_Laboratory_Management_System.Models.Enums;

namespace Medical_Laboratory_Management_System.View_Models
{
    public class AddDoctorViewModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public Gender? Gender { get; set; }
        [Required]
        [Range(16, 100)]
        public int? Age { get; set; }
        [Required]
        [Display(Name = "Join Date")]
        public DateOnly JoinDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }

}