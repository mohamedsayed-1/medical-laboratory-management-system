using Medical_Laboratory_Management_System.Data;
using Medical_Laboratory_Management_System.Models;
using Medical_Laboratory_Management_System.View_Models;

namespace Medical_Laboratory_Management_System.Services
{
    public class DoctorServices : GenericServices<Doctor>, IDoctorServices
    {
        private readonly MLMSDbContext context;

        public DoctorServices(MLMSDbContext context):base(context)
        {
            this.context = context;
        }
        public ICollection<IndexDoctor> GetIndexDoctors()
        {
            return context.Set<Doctor>().Select(x => new IndexDoctor
            {
                Id = x.Id,
                Name = x.Name,
                Age = x.Age,
                Gender = x.Gender,
                JoinDate = x.JoinDate
            }).ToList();
        }
    }
}
