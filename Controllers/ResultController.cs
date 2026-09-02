using Medical_Laboratory_Management_System.Services;
using Medical_Laboratory_Management_System.View_Models;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Laboratory_Management_System.Controllers
{
    public class ResultController : Controller
    {
        private readonly IResultServices resultServices;

        public ResultController(IResultServices resultServices)
        {
            this.resultServices = resultServices;
        }
        [HttpGet]
        public IActionResult AddResult(int id) //id for RequestedLabTestId
        {
            var vm = resultServices.GetLabTestDetails(id);
            if (vm is null)
            {
                return NotFound();
            }
            return View("AddResult", vm);
        }
        [HttpPost]
        public IActionResult SaveResult(AddLabTestResult vm)
        {
            if (!ModelState.IsValid)
            {
                return View("AddResult", vm);
            }
            if (vm.LabTestResult.Length < 1)
            {
                ModelState.AddModelError("LabTestResult", "Please, Enter a valid result");
                return View("AddResult", vm);
            }
            if (!resultServices.SaveResult(vm))
            {
                return NotFound();
            }
            return RedirectToAction(actionName: "Details",
                controllerName: "Appointment",
                routeValues: new {id = vm.AppointmentId});
        }
    }
}