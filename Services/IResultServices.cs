using Medical_Laboratory_Management_System.View_Models;

namespace Medical_Laboratory_Management_System.Services
{
    public interface IResultServices
    {
        public AddLabTestResult? GetLabTestDetails(int id);
        public bool SaveResult(AddLabTestResult vm);
    }
}
