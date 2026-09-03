using Medical_Laboratory_Management_System.Models;
using Medical_Laboratory_Management_System.View_Models;

namespace Medical_Laboratory_Management_System.Services
{
    public interface IPatientServices : IServices<Patient>
    {
        public Patient? GetByPhoneNumber(string phoneNumber);
        public ICollection<IndexPatient> GetIndexPatients();
        public IndexPatient? GetEditPatientById(int id);
        public bool SaveEdit(IndexPatient patient);
    }
}
