using Medical_Laboratory_Management_System.Models.Enums;

namespace Medical_Laboratory_Management_System.View_Models
{
    public class IndexDoctor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public DateOnly JoinDate { get; set; }

    }
}