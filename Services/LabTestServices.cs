using Medical_Laboratory_Management_System.Data;
using Medical_Laboratory_Management_System.Models;
using Medical_Laboratory_Management_System.View_Models;

namespace Medical_Laboratory_Management_System.Services
{
    public class LabTestServices : GenericServices<LabTest>, ILabTestServices
    {
        private readonly MLMSDbContext context;

        public LabTestServices(MLMSDbContext context):base(context)
        {
            this.context = context;
        }
        public ICollection<IndexLabTest> GetIndexLabTests()
        {
            return context.Set<LabTest>().Select(x => new IndexLabTest
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price
            }).ToList();
        }
    }
}
