using Medical_Laboratory_Management_System.Models;
using Medical_Laboratory_Management_System.View_Models;

namespace Medical_Laboratory_Management_System.Services
{
    public interface IDoctorServices : IServices<Doctor>
    {
        public ICollection<IndexDoctor> GetIndexDoctors();
        public IndexDoctor? GetEditDoctorById(int id);
        public void SaveEdit(IndexDoctor doctor);
    }
}
