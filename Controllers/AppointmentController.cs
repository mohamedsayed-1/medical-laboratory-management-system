using Medical_Laboratory_Management_System.Models;
using Medical_Laboratory_Management_System.Models.Enums;
using Medical_Laboratory_Management_System.Services;
using Medical_Laboratory_Management_System.View_Models;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Laboratory_Management_System.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IServices<Doctor> doctors;
        private readonly IServices<LabTest> labTests;
        private readonly IServices<Appointment> appointments;
        private readonly IServices<RequestedLabTest> requestedLabTests;
        private readonly IPatientServices patients;

        public AppointmentController(IServices<Doctor> doctors, 
            IServices<LabTest> labTests,
            IPatientServices patients,
            IServices<Appointment> appointments,
            IServices<RequestedLabTest> requestedLabTests)
        {
            this.doctors = doctors;
            this.labTests = labTests;
            this.appointments = appointments;
            this.requestedLabTests = requestedLabTests;
            this.patients = patients;
        }
        public IActionResult AddAppointment()
        {
            ViewBag.Doctors = doctors.GetAll();
            ViewBag.labTests = labTests.GetAll();
            return View("AddAppointment", new AddAppointmentViewModel());
        }
        public IActionResult SaveAppointment(AddAppointmentViewModel appointmentVM)
        {
            if (!ModelState.IsValid)
            {
                return View("AddAppointment", appointmentVM);
            }
            var patient = patients.GetByPhoneNumber(appointmentVM.PatientPhoneNumber);
            if (patient is null) {
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
            var appointment = new Appointment()
            {
                Date = (DateTime)appointmentVM.Date,
                Notes = appointmentVM.Notes,
                Urgent = appointmentVM.Urgent,
                DoctorId = appointmentVM.DoctorId,
                PatientId = patient.Id,
            };

            var reqLabTests = new List<LabTest>();
            foreach (var labTest in appointmentVM.LabTestsIds)
            {
                reqLabTests.Add(labTests.GetById(labTest));
            }
            foreach (var labTest in reqLabTests)
            {
                requestedLabTests.Add(new RequestedLabTest()
                {
                    LabTest = labTest,
                    Appointment = appointment,
                    LabTestStatus = LabTestStatus.Queued
                });
            }
            appointments.Add(appointment);
            patient.Appointments.Add(appointment);
            appointments.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            List<Appointment> allAppointments
                = appointments.GetAllWithIncludes().ToList();
            return View("Index", allAppointments);
        }
    }
}