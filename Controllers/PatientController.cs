using Medical_Laboratory_Management_System.Services;
using Medical_Laboratory_Management_System.View_Models;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Laboratory_Management_System.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientServices patientServices;

        public PatientController(IPatientServices patientServices)
        {
            this.patientServices = patientServices;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var allPatients = patientServices.GetIndexPatients();
            return View("Index", allPatients);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var patient = patientServices.GetEditPatientById(id);
            if (patient is null)
            {
                return NotFound();
            }
            return View("Edit", patient);
        }
        [HttpPost]
        public IActionResult SaveEdit(IndexPatient patient)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", patient);
            }
            if (!patientServices.SaveEdit(patient))
            {
                ModelState.AddModelError("PhoneNumber", "Cannot Edit Phone Number");
                return View("Edit", patient);
            }
            return RedirectToAction("Index");
        }
    }
}