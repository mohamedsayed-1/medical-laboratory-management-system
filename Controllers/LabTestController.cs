using Medical_Laboratory_Management_System.Services;
using Medical_Laboratory_Management_System.View_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Medical_Laboratory_Management_System.Controllers
{
    public class LabTestController : Controller
    {
        private readonly ILabTestServices labTestServices;

        public LabTestController(ILabTestServices labTestServices)
        {
            this.labTestServices = labTestServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var allLabTests = labTestServices.GetIndexLabTests();
            return View("Index", allLabTests);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var labTest = labTestServices.GetEditLabTestById(id);
            if(labTest is null)
            {
                return NotFound();
            }
            return View("Edit", labTest);
        }
        [HttpPost]
        public IActionResult SaveEdit(IndexLabTest labTest)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", labTest);
            }
            labTestServices.SaveEdit(labTest);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult AddLabTest()
        {
            var vm = new AddLabTestViewModel();
            return View("AddLabTest", vm);
        }
        [HttpPost]
        public IActionResult SaveLabTest(AddLabTestViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("AddLabTest", vm);
            }
            labTestServices.SaveLabTest(vm);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (!labTestServices.Delete(id))
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
    }
}