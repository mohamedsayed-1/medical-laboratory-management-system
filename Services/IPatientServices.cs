using Medical_Laboratory_Management_System.Models;

namespace Medical_Laboratory_Management_System.Services
{
    public interface IPatientServices : IServices<Patient>
    {
        Patient? GetByPhoneNumber(string phoneNumber);
    }
}
