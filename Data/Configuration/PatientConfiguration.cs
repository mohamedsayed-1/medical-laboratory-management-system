using Medical_Laboratory_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Medical_Laboratory_Management_System.Data.Configuration
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired();
            builder.Property(x => x.PhoneNumber)
                .HasColumnName("Phone Number")
                .HasMaxLength(30)
                .IsRequired();
            builder.Property(x => x.Email)
                .HasMaxLength(256);
            builder.Property(x => x.Gender)
                .HasConversion<string>()
                .IsRequired();
            builder.Property(x => x.MaritalStatus)
                .HasConversion<string>()
                .HasColumnName("Marital Status")
                .IsRequired(false);
        }
    }
}