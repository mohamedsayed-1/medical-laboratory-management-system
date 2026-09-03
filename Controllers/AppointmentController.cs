using Medical_Laboratory_Management_System.Models;
using Medical_Laboratory_Management_System.Services;
using Medical_Laboratory_Management_System.View_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Medical_Laboratory_Management_System.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IDoctorServices doctors;
        private readonly ILabTestServices labTests;
        private readonly IAppointmentServices appointmentServices;
        public AppointmentController(IDoctorServices doctors,
            ILabTestServices labTests,
            IAppointmentServices appointmentServices)
        {
            this.doctors = doctors;
            this.labTests = labTests;
            this.appointmentServices = appointmentServices;
        }
        private void PopulateDoctorsDropDown(EditAppointmentViewModel vm)
        {
            vm.Doctors = doctors.GetAll().Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name,
            });
        }
        private void PopulateDropDowns(AddAppointmentViewModel vm)
        {
            vm.Doctors = doctors.GetAll().Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name,
            });
            vm.LabTests = labTests.GetAll().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            });
        }
        public IActionResult Details(int id)
        {
            var vm = appointmentServices.GetDetails(id);
            if(vm is null)
            {
                return NotFound();
            }
            return View("Details", vm);
        }

        [HttpGet]
        public IActionResult AddAppointment()
        {
            var vm = new AddAppointmentViewModel();
            PopulateDropDowns(vm);
            return View("AddAppointment", vm);
        }
        [HttpPost]
        public IActionResult SaveAppointment(AddAppointmentViewModel appointmentVM)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropDowns(appointmentVM);
                return View("AddAppointment", appointmentVM);
            }
            if (!appointmentServices.SaveAppointment(appointmentVM))
            {
                PopulateDropDowns(appointmentVM);
                ModelState.AddModelError("LabTestsIds", "Please, choose the tests correctly");
                return View("AddAppointment", appointmentVM);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            return View("Index", appointmentServices.GetAllIndex());
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var appointment = appointmentServices.GetEditAppointmentById(id);
            if (appointment is null)
            {
                return NotFound();
            }
            PopulateDoctorsDropDown(appointment);
            return View("Edit", appointment);
        }
        [HttpPost]
        public IActionResult SaveEdit(EditAppointmentViewModel appointment)
        {
            if (!ModelState.IsValid)
            {
                PopulateDoctorsDropDown(appointment);
                return View("Edit", appointment);
            }
            appointmentServices.SaveEdit(appointment);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (!appointmentServices.Delete(id))
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
    }
}