using Medical_Laboratory_Management_System.Data;
using Medical_Laboratory_Management_System.Models;

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
    }
}
