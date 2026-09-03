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
        public IndexLabTest? GetEditLabTestById(int id)
        {
            return context.Set<LabTest>()
                .Where(x => x.Id == id)
                .Select(x => new IndexLabTest
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price
                }).FirstOrDefault();
        }

        public void SaveEdit(IndexLabTest labTestVM)
        {
            var labTest = GetById(labTestVM.Id);
            if (labTest != null)
            {
                labTest.Name = labTestVM.Name;
                labTest.Price = labTestVM.Price;
                context.SaveChanges();
            }
        }
    }
}
