using Medical_Laboratory_Management_System.Data;
using Medical_Laboratory_Management_System.Models;
using Medical_Laboratory_Management_System.View_Models;
using Microsoft.EntityFrameworkCore;

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
        public IndexDoctor? GetEditDoctorById(int id)
        {
            return context.Set<Doctor>()
                .Where(x => x.Id == id)
                .Select(x => new IndexDoctor
                {
                    Id = x.Id,
                    Name = x.Name,
                    Age = x.Age,
                    Gender = x.Gender,
                    JoinDate = x.JoinDate
                }).FirstOrDefault();
        }
        public void SaveEdit(IndexDoctor doctorVM)
        {
            var doctor = GetById(doctorVM.Id);
            if (doctor != null)
            {
                doctor.Name = doctorVM.Name;
                doctor.Age = doctorVM.Age;
                doctor.Gender = doctorVM.Gender;
                doctor.JoinDate = doctorVM.JoinDate;
                context.SaveChanges();
            }
        }
        public void SaveDoctor(AddDoctorViewModel doctor)
        {
            var doc = new Doctor()
            {
                Name = doctor.Name,
                Age = doctor.Age!.Value,
                Gender = doctor.Gender!.Value,
                JoinDate = doctor.JoinDate,
            };
            Add(doc);
            Save();
        }

        public bool Delete(int id)
        {
            var doctor = context.Set<Doctor>()
                .Where(x => x.Id == id)
                .Include(x => x.Appointments)
                .FirstOrDefault();
            if (doctor is null)
                return false;
            if (doctor.Appointments.Count > 0)
            {
                return false;
            }
            context.Remove(doctor);
            context.SaveChanges();
            return true;
        }
    }
}
