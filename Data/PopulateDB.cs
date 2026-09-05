using Medical_Laboratory_Management_System.Models;
using Medical_Laboratory_Management_System.Models.Enums;

namespace Medical_Laboratory_Management_System.Data
{
    // Manually-invoked seed helper — NOT run automatically on every app startup.
    //
    // Usage: after applying migrations against a fresh (or freshly-dropped) database,
    // temporarily add this to Program.cs, between `var app = builder.Build();` and `app.Run();`:
    //
    //     using (var scope = app.Services.CreateScope())
    //     {
    //         var context = scope.ServiceProvider.GetRequiredService<MLMSDbContext>();
    //         PopulateDB.Seed(context);
    //     }
    //
    // Run the app once, then remove the block again — it's a one-time seed, not part
    // of normal startup.
    public static class PopulateDB
    {
        public static void Seed(MLMSDbContext context)
        {
            if (context.Doctors.Any() || context.Patients.Any())
            {
                return; // already seeded, don't duplicate
            }

            var doctors = new List<Doctor>
            {
                new Doctor { Name = "Dr. Ahmed Samir", Gender = Gender.Male, Age = 42, JoinDate = new DateOnly(2015, 3, 1) },
                new Doctor { Name = "Dr. Mona Farid", Gender = Gender.Female, Age = 37, JoinDate = new DateOnly(2018, 7, 15) },
                new Doctor { Name = "Dr. Youssef Kamal", Gender = Gender.Male, Age = 50, JoinDate = new DateOnly(2010, 1, 10) }
            };
            context.Doctors.AddRange(doctors);

            var labTests = new List<LabTest>
            {
                new LabTest { Name = "Complete Blood Count (CBC)", Price = 150m },
                new LabTest { Name = "Blood Glucose Test", Price = 80m },
                new LabTest { Name = "Liver Function Test", Price = 200m },
                new LabTest { Name = "Kidney Function Test", Price = 180m },
                new LabTest { Name = "Lipid Profile", Price = 220m }
            };
            context.LabTests.AddRange(labTests);

            var patients = new List<Patient>
            {
                new Patient { Name = "Sara Ali", Age = 29, Gender = Gender.Female, PhoneNumber = "01000000001", Email = "sara.ali@example.com", MaritalStatus = MaritalStatus.Married },
                new Patient { Name = "Omar Hassan", Age = 45, Gender = Gender.Male, PhoneNumber = "01000000002", Email = "omar.hassan@example.com", MaritalStatus = MaritalStatus.Single },
                new Patient { Name = "Laila Nabil", Age = 34, Gender = Gender.Female, PhoneNumber = "01000000003", MaritalStatus = MaritalStatus.Divorced }
            };
            context.Patients.AddRange(patients);

            // Save now so Doctors/LabTests/Patients get real database-generated Ids
            // before appointments reference them.
            context.SaveChanges();

            var appointments = new List<Appointment>
            {
                new Appointment { Date = DateTime.Now.AddDays(-10), Notes = "Routine checkup", Urgent = false, DoctorId = doctors[0].Id, PatientId = patients[0].Id },
                new Appointment { Date = DateTime.Now.AddDays(-3), Notes = "Follow-up visit", Urgent = false, DoctorId = doctors[1].Id, PatientId = patients[1].Id },
                new Appointment { Date = DateTime.Now.AddDays(2), Notes = "Reported fever and fatigue", Urgent = true, DoctorId = doctors[2].Id, PatientId = patients[2].Id }
            };
            context.Appointments.AddRange(appointments);
            context.SaveChanges();

            // Appointment 1: two requested tests, both completed with results.
            var reqTest1 = RequestedLabTest.Create(appointments[0], labTests[0]);
            var reqTest2 = RequestedLabTest.Create(appointments[0], labTests[1]);
            context.RequestedLabTests.AddRange(reqTest1, reqTest2);
            context.SaveChanges();

            reqTest1.CompleteWithResult(new LabTestResult
            {
                Value = "Normal",
                Notes = "All parameters within range.",
                RequestedLabTest = reqTest1
            });
            reqTest2.CompleteWithResult(new LabTestResult
            {
                Value = "95 mg/dL",
                Notes = "Fasting glucose, within normal range.",
                RequestedLabTest = reqTest2
            });

            // Appointment 2: one requested test, still queued (no result yet).
            var reqTest3 = RequestedLabTest.Create(appointments[1], labTests[2]);
            context.RequestedLabTests.Add(reqTest3);

            // Appointment 3 (urgent): two requested tests, still queued.
            var reqTest4 = RequestedLabTest.Create(appointments[2], labTests[3]);
            var reqTest5 = RequestedLabTest.Create(appointments[2], labTests[4]);
            context.RequestedLabTests.AddRange(reqTest4, reqTest5);

            context.SaveChanges();
        }
    }
}
