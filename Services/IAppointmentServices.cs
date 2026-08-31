using Medical_Laboratory_Management_System.Models;
using Medical_Laboratory_Management_System.View_Models;

namespace Medical_Laboratory_Management_System.Services
{
    public interface IAppointmentServices
    {
        public void SaveAppointment(AddAppointmentViewModel appointmentVM);
        public Patient GetPatient(AddAppointmentViewModel appointmentVM);
        public void GetRequestedLabTests(List<int> labTestsIds, Appointment appointment);
        public IQueryable<IndexAppointmentViewModel> GetAll();
    }
}
