using Medical_Laboratory_Management_System.Services;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Laboratory_Management_System.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorServices doctorServices;

        public DoctorController(IDoctorServices doctorServices)
        {
            this.doctorServices = doctorServices;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var allDoctors = doctorServices.GetIndexDoctors();
            return View("Index", allDoctors);
        }
    }
}