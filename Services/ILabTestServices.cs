using Medical_Laboratory_Management_System.Models;
using Medical_Laboratory_Management_System.View_Models;

namespace Medical_Laboratory_Management_System.Services
{
    public interface ILabTestServices : IServices<LabTest>
    {
        public ICollection<IndexLabTest> GetIndexLabTests();
    }
}
