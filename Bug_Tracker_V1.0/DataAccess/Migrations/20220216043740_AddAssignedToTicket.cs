using Microsoft.EntityFrameworkCore.Migrations;

namespace Bug_Tracker_V1._0.Data.Migrations
{
    public partial class AddAssignedToTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedTo",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedTo",
                table: "Tickets");
        }
    }
}
