using Medical_Laboratory_Management_System.Services;
using Microsoft.AspNetCore.Mvc;

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
    }
}
