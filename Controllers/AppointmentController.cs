using Medical_Laboratory_Management_System.Models;
using Medical_Laboratory_Management_System.Services;
using Medical_Laboratory_Management_System.View_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Medical_Laboratory_Management_System.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IServices<Doctor> doctors;
        private readonly IServices<LabTest> labTests;
        private readonly IAppointmentServices appointmentServices;
        public AppointmentController(IServices<Doctor> doctors,
            IServices<LabTest> labTests,
            IAppointmentServices appointmentServices)
        {
            this.doctors = doctors;
            this.labTests = labTests;
            this.appointmentServices = appointmentServices;
        }

        [HttpGet]
        public IActionResult AddAppointment()
        {
            var vm = new AddAppointmentViewModel()
            {
                Doctors = doctors.GetAll().Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name,
                }),
                LabTests = labTests.GetAll().Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                })
            };
            return View("AddAppointment", vm);
        }
        [HttpPost]
        public IActionResult SaveAppointment(AddAppointmentViewModel appointmentVM)
        {
            if (!ModelState.IsValid)
            {
                appointmentVM.Doctors = doctors.GetAll().Select(d => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name,
                });
                appointmentVM.LabTests = labTests.GetAll().Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                });
                return View("AddAppointment", appointmentVM);
            }
            if (!appointmentServices.SaveAppointment(appointmentVM))
            {
                appointmentVM.Doctors = doctors.GetAll().Select(d => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name,
                });
                appointmentVM.LabTests = labTests.GetAll().Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                });
                ModelState.AddModelError("LabTestsIds", "Please, choose the tests correctly");
                return View("AddAppointment", appointmentVM);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            return View("Index", appointmentServices.GetAll());
        }
    }
}