using Medical_Laboratory_Management_System.Data;
using Medical_Laboratory_Management_System.Models;
using Medical_Laboratory_Management_System.View_Models;
using Microsoft.EntityFrameworkCore;

namespace Medical_Laboratory_Management_System.Services
{
    public class AppointmentServices : GenericServices<Appointment> ,IAppointmentServices
    {
        private readonly ILabTestServices labTests;
        private readonly MLMSDbContext context;
        private readonly IDoctorServices doctorServices;
        private readonly IServices<RequestedLabTest> requestedLabTests;
        private readonly IPatientServices patients;

        public AppointmentServices(ILabTestServices labTests,
            IPatientServices patients,
            MLMSDbContext context,
            IDoctorServices doctorServices,
            IServices<RequestedLabTest> requestedLabTests) : base(context)
        {
            this.labTests = labTests;
            this.context = context;
            this.doctorServices = doctorServices;
            this.requestedLabTests = requestedLabTests;
            this.patients = patients;
        }
        public AppointmentDetailsViewModel? GetDetails(int id)
        {
            var vm 
                = context.Set<Appointment>().Where(x => x.Id == id)
                .Select(x => new AppointmentDetailsViewModel
                {
                    AppointmentId = x.Id,
                    Date = x.Date,
                    DoctorName = x.Doctor.Name,
                    Notes = x.Notes,
                    PatientAge = x.Patient.Age,
                    PatientEmail = x.Patient.Email,
                    PatientGender = x.Patient.Gender,
                    PatientName = x.Patient.Name,
                    PatientMaritalStatus = x.Patient.MaritalStatus,
                    PatientPhoneNumber = x.Patient.PhoneNumber,
                    Urgent = x.Urgent,
                    LabTestsDetails = x.RequestedLabTests
                    .Select(r => new LabTestDetails()
                    {
                        RequestedLabTestId = r.Id,
                        RequestedLabTestName = r.LabTest.Name,
                        LabTestResult = r.LabTestResult == null ? null : r.LabTestResult.Value,
                        LabTestStatus = r.LabTestStatus,
                        Notes = r.LabTestResult == null ? null : r.LabTestResult.Notes
                    }).ToList()
                }).FirstOrDefault();
            return vm;
        }
        public IQueryable<IndexAppointmentViewModel> GetAllIndex()
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
                    Age = appointmentVM.PatientAge!.Value,
                    Email = appointmentVM.PatientEmail,
                    Gender = appointmentVM.PatientGender!.Value,
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
                requestedLabTests.Add(RequestedLabTest.Create(appointment, labTest));
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
                Date = appointmentVM.Date!.Value,
                Notes = appointmentVM.Notes,
                Urgent = appointmentVM.Urgent,
                DoctorId = appointmentVM.DoctorId,
            };
            AddRequestedLabTests(reqLabtests, appointment);
            Add(appointment);
            patient.Appointments.Add(appointment);
            Save();
            return true;
        }

        public EditAppointmentViewModel? GetEditAppointmentById(int id)
        {
            return context.Set<Appointment>()
                .Where(x => x.Id == id)
                .Select(x => new EditAppointmentViewModel
                {
                    Id = x.Id,
                    Date = x.Date,
                    Notes = x.Notes,
                    Urgent = x.Urgent,
                    DoctorId= x.DoctorId,
                    PatientName = x.Patient.Name
                }).FirstOrDefault();
        }
        public void SaveEdit(EditAppointmentViewModel appointmentVM)
        {
            var appointment = GetById(appointmentVM.Id);
            if (appointment != null)
            {
                appointment.Date = appointmentVM.Date;
                appointment.Notes = appointmentVM.Notes;
                appointment.Urgent = appointmentVM.Urgent;
                if (doctorServices.GetById(appointmentVM.DoctorId) is not null)
                {
                    appointment.DoctorId = appointmentVM.DoctorId;
                }
                Save();
            }
        }
        public bool Delete(int id)
        {
            var appointment = context.Set<Appointment>()
                .Where(x => x.Id == id)
                .Include(x => x.Patient)
                    .ThenInclude(x => x.Appointments)
                .Include(x => x.RequestedLabTests)
                    .ThenInclude(x => x.LabTestResult)
                .FirstOrDefault();
            if (appointment is null)
                return false;
            foreach(var x in appointment.RequestedLabTests)
            {
                if(x.LabTestResult != null)
                {
                    context.Remove(x.LabTestResult);
                }
                context.Remove(x);
            }
            context.Remove(appointment);
            if (appointment.Patient.Appointments.Count == 1)
            {
                context.Remove(appointment.Patient);
            }
            context.SaveChanges();
            return true;
        }
    }
}