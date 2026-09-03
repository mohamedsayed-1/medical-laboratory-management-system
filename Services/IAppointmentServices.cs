using Medical_Laboratory_Management_System.Models;
using Medical_Laboratory_Management_System.View_Models;

namespace Medical_Laboratory_Management_System.Services
{
    public interface IAppointmentServices : IServices<Appointment>
    {
        public bool SaveAppointment(AddAppointmentViewModel appointmentVM);
        public IQueryable<IndexAppointmentViewModel> GetAllIndex();
        public AppointmentDetailsViewModel? GetDetails(int id);
        public EditAppointmentViewModel? GetEditAppointmentById(int id);
        public void SaveEdit(EditAppointmentViewModel appointment);
    }
}
