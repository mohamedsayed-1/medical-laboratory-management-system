using Medical_Laboratory_Management_System.Models;
using Medical_Laboratory_Management_System.Services;
using Medical_Laboratory_Management_System.View_Models;
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
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var doctor = doctorServices.GetEditDoctorById(id);
            if (doctor is null)
            {
                return NotFound();
            }
            return View("Edit", doctor);
        }
        [HttpPost]
        public IActionResult SaveEdit(IndexDoctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", doctor);
            }
            doctorServices.SaveEdit(doctor);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult AddDoctor()
        {
            var vm = new AddDoctorViewModel();
            return View("AddDoctor", vm);
        }
        [HttpPost]
        public IActionResult SaveDoctor(AddDoctorViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("AddDoctor", vm);
            }
            doctorServices.SaveDoctor(vm);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (!doctorServices.Delete(id))
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
    }
}