using Medical_Laboratory_Management_System.Data;
using Medical_Laboratory_Management_System.Models;
using Medical_Laboratory_Management_System.View_Models;

namespace Medical_Laboratory_Management_System.Services
{
    public class ResultServices : IResultServices
    {
        private readonly MLMSDbContext context;

        public ResultServices(MLMSDbContext context)
        {
            this.context = context;
        }
        public AddLabTestResult? GetLabTestDetails(int id) // id for RequestedLabTest
        {
            var vm = context.Set<RequestedLabTest>()
                .Where(x => x.Id == id)
                .Where(x => x.LabTestResult == null) // for not adding the result twice by any means
                .Select(x => new AddLabTestResult
                {
                    RequestedLabTestId = x.Id, // for changing status to complete
                    AppointmentId = x.AppointmentId, // for returning to Appointment Details
                    RequestedLabTestName = x.LabTest.Name // for showing
                });
            return vm.FirstOrDefault();
        }

        public bool SaveResult(AddLabTestResult vm)
        {
            var reqLabTest = context.Set<RequestedLabTest>()
                .Where(x => x.Id == vm.RequestedLabTestId)
                .Where(x => x.LabTestResult == null)
                .FirstOrDefault();
            if (reqLabTest is null)
                return false;
            reqLabTest.CompleteWithResult(new LabTestResult()
            {
                Value = vm.LabTestResult,
                Notes = vm.Notes,
                RequestedLabTest = reqLabTest
            });
            context.SaveChanges();
            return true;
        }
    }
}
