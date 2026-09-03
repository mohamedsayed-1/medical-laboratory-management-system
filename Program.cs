using Medical_Laboratory_Management_System.Data;
using Medical_Laboratory_Management_System.Services;
using Microsoft.EntityFrameworkCore;

namespace Medical_Laboratory_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<MLMSDbContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration
                        .GetConnectionString("MLMS"));
            });

            builder.Services.AddScoped(typeof(IServices<>), typeof(GenericServices<>));

            builder.Services.AddScoped<IPatientServices, PatientServices>();
            builder.Services.AddScoped<IAppointmentServices, AppointmentServices>();
            builder.Services.AddScoped<IResultServices, ResultServices>();
            builder.Services.AddScoped<IDoctorServices, DoctorServices>();
            builder.Services.AddScoped<ILabTestServices, LabTestServices>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
