using Medical_Laboratory_Management_System.Data;
using Medical_Laboratory_Management_System.Models;
using Medical_Laboratory_Management_System.Models.Enums;
using Medical_Laboratory_Management_System.View_Models;

namespace Medical_Laboratory_Management_System.Services
{
    public class AppointmentServices : IAppointmentServices
    {
        private readonly IServices<LabTest> labTests;
        private readonly IServices<Appointment> appointments;
        private readonly MLMSDbContext context;
        private readonly IServices<RequestedLabTest> requestedLabTests;
        private readonly IPatientServices patients;

        public AppointmentServices(IServices<LabTest> labTests,
            IPatientServices patients,
            IServices<Appointment> appointments,
            MLMSDbContext context,
            IServices<RequestedLabTest> requestedLabTests)
        {
            this.labTests = labTests;
            this.appointments = appointments;
            this.context = context;
            this.requestedLabTests = requestedLabTests;
            this.patients = patients;
        }
        public IQueryable<IndexAppointmentViewModel> GetAll()
        {
            IQueryable<IndexAppointmentViewModel> query 
                = context.Set<Appointment>().Select(x =>
                    new IndexAppointmentViewModel
                    {
                        AppointmentId = x.Id,
                        Date = x.Date,
                        DoctorName = x.Doctor.Name,
                        Notes = x.Notes,
                        PatientName = x.Patient.Name,
                        Urgent = x.Urgent
                    });
            return query;
        }
        private Patient GetPatient(AddAppointmentViewModel appointmentVM)
        {
            var patient = patients
                .GetByPhoneNumber(appointmentVM.PatientPhoneNumber);
            if (patient is null)
            {
                patient = new Patient()
                {
                    Name = appointmentVM.PatientName,
                    Age = (int)appointmentVM.PatientAge,
                    Email = appointmentVM.PatientEmail,
                    Gender = (Gender)appointmentVM.PatientGender,
                    MaritalStatus = appointmentVM.PatientMaritalStatus,
                    Appointments = new List<Appointment>(),
                    PhoneNumber = appointmentVM.PatientPhoneNumber
                };
                patients.Add(patient);
            }
            return patient;
        }
        private List<LabTest>? GetRequestedLabTests(List<int> labTestsIds)
        {
            var reqLabTests = new List<LabTest>();
            foreach (var labTest in labTestsIds)
            {
                var _labTest = labTests.GetById(labTest);
                if (_labTest is not null)
                {
                    reqLabTests.Add(_labTest);
                }
                else
                {
                    return null;
                }
            }
            return reqLabTests;
        }
        private void AddRequestedLabTests(List<LabTest> reqLabTests, Appointment appointment)
        {
            foreach (var labTest in reqLabTests)
            {
                requestedLabTests.Add(new RequestedLabTest()
                {
                    LabTest = labTest,
                    Appointment = appointment,
                    LabTestStatus = LabTestStatus.Queued
                });
            }
        }
        public bool SaveAppointment(AddAppointmentViewModel appointmentVM)
        {
            var reqLabtests = GetRequestedLabTests(appointmentVM.LabTestsIds);
            if (reqLabtests is null)
            {
                return false;
            }
            var patient = GetPatient(appointmentVM);
            var appointment = new Appointment()
            {
                Date = (DateTime)appointmentVM.Date,
                Notes = appointmentVM.Notes,
                Urgent = appointmentVM.Urgent,
                DoctorId = appointmentVM.DoctorId,
            };
            AddRequestedLabTests(reqLabtests, appointment);
            appointments.Add(appointment);
            patient.Appointments.Add(appointment);
            appointments.Save();
            return true;
        }
    }
}
