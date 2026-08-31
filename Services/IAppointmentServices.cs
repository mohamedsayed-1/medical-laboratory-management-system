using Medical_Laboratory_Management_System.Models;
using Medical_Laboratory_Management_System.View_Models;

namespace Medical_Laboratory_Management_System.Services
{
    public interface IAppointmentServices
    {
        public bool SaveAppointment(AddAppointmentViewModel appointmentVM);
        public IQueryable<IndexAppointmentViewModel> GetAll();
    }
}
