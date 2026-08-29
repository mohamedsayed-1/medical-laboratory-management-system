using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_Laboratory_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceDecimalResultWithLabTestResultEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LabTestResult",
                table: "RequestedLabTests");

            migrationBuilder.AddColumn<int>(
                name: "LabTestResultId",
                table: "RequestedLabTests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LabTestResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestedLabTestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTestResults", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestedLabTests_LabTestResultId",
                table: "RequestedLabTests",
                column: "LabTestResultId",
                unique: true,
                filter: "[LabTestResultId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestedLabTests_LabTestResults_LabTestResultId",
                table: "RequestedLabTests",
                column: "LabTestResultId",
                principalTable: "LabTestResults",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestedLabTests_LabTestResults_LabTestResultId",
                table: "RequestedLabTests");

            migrationBuilder.DropTable(
                name: "LabTestResults");

            migrationBuilder.DropIndex(
                name: "IX_RequestedLabTests_LabTestResultId",
                table: "RequestedLabTests");

            migrationBuilder.DropColumn(
                name: "LabTestResultId",
                table: "RequestedLabTests");

            migrationBuilder.AddColumn<decimal>(
                name: "LabTestResult",
                table: "RequestedLabTests",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
