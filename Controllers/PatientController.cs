using Medical_Laboratory_Management_System.Models;
using Medical_Laboratory_Management_System.Services;
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
    }
}