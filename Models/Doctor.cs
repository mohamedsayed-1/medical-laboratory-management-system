using Medical_Laboratory_Management_System.Models.Enums;

namespace Medical_Laboratory_Management_System.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public Gender Gender {  get; set; }
        public int Age {  get; set; }
        public DateOnly JoinDate { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = [];
    }
}