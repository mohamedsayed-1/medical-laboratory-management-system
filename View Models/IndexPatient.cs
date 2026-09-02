using System.ComponentModel.DataAnnotations;
using Medical_Laboratory_Management_System.Models.Enums;

namespace Medical_Laboratory_Management_System.View_Models
{
    public class IndexPatient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Email { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
    }
}