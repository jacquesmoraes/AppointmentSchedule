using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Appointment.Migrations
{
    public partial class AddAppointmentmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Appointments",
                newName: "EndDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Appointments",
                newName: "Date");
        }
    }
}
