using System.ComponentModel.DataAnnotations;

namespace Medical_Laboratory_Management_System.View_Models
{
    public class AddLabTestViewModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [Range(1, Double.MaxValue, ErrorMessage ="Price must be greater than zero")]
        public decimal? Price { get; set; }
    }
}