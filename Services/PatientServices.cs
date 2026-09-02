using Medical_Laboratory_Management_System.Data;
using Medical_Laboratory_Management_System.Models;
using Medical_Laboratory_Management_System.View_Models;

namespace Medical_Laboratory_Management_System.Services
{
    public class PatientServices : GenericServices<Patient>, IPatientServices
    {
        private readonly MLMSDbContext context;

        public PatientServices(MLMSDbContext context) : base(context)
        {
            this.context = context;
        }
        public Patient? GetByPhoneNumber(string phoneNumber)
        {
            return context.Set<Patient>()
                .SingleOrDefault(x => x.PhoneNumber == phoneNumber);
        }

        public ICollection<IndexPatient> GetIndexPatients()
        {
            return context.Set<Patient>()
                .Select(x => new IndexPatient
                {
                    Id = x.Id,
                    Name = x.Name,
                    Age = x.Age,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Gender = x.Gender,
                    MaritalStatus = x.MaritalStatus
                }).ToList();
        }
    }
}
