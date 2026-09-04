using Medical_Laboratory_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Medical_Laboratory_Management_System.Data
{
    public class MLMSDbContext : DbContext
    {
        public MLMSDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<LabTest> LabTests { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<RequestedLabTest> RequestedLabTests { get; set; }
        public DbSet<LabTestResult> LabTestResults { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(MLMSDbContext).Assembly);
        }
    }
}
