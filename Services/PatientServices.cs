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

        public IndexPatient? GetEditPatientById(int id)
        {
            return context.Set<Patient>()
                .Where(x => x.Id == id)
                .Select(x => new IndexPatient
                {
                    Id = x.Id,
                    Name = x.Name,
                    Age = x.Age,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Gender = x.Gender,
                    MaritalStatus = x.MaritalStatus
                }).FirstOrDefault();
        }
        public bool SaveEdit(IndexPatient patientVM)
        {
            var patient = GetById(patientVM.Id);
            if(patient?.PhoneNumber != patientVM.PhoneNumber)
            {
                return false;
            }
            if (patient != null)
            {
                patient.Name = patientVM.Name;
                patient.Age = patientVM.Age;
                patient.Email = patientVM.Email;
                patient.Gender = patientVM.Gender;
                patient.MaritalStatus = patientVM.MaritalStatus;
                context.SaveChanges();
            }
            return true;
        }
    }
}
