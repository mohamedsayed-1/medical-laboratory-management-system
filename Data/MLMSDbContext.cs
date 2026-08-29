using Medical_Laboratory_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Medical_Laboratory_Management_System.Data
{
    public class MLMSDbContext : DbContext
    {
        public MLMSDbContext(DbContextOptions options) : base(options)
        {
            
        }
        DbSet<Patient> Patients { get; set; }
        DbSet<Doctor> Doctors { get; set; }
        DbSet<LabTest> LabTests { get; set; }
        DbSet<Appointment> Appointments { get; set; }
        DbSet<RequestedLabTest> RequestedLabTests { get; set; }
        DbSet<LabTestResult> LabTestResults { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(MLMSDbContext).Assembly);
        }
    }
}
