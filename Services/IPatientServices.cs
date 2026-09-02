using Medical_Laboratory_Management_System.Models;
using Medical_Laboratory_Management_System.View_Models;

namespace Medical_Laboratory_Management_System.Services
{
    public interface IPatientServices : IServices<Patient>
    {
        Patient? GetByPhoneNumber(string phoneNumber);
        ICollection<IndexPatient> GetIndexPatients();
    }
}
