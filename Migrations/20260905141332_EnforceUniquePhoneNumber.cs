using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_Laboratory_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class EnforceUniquePhoneNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Patients_Phone Number",
                table: "Patients",
                column: "Phone Number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patients_Phone Number",
                table: "Patients");
        }
    }
}
