using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bug_Tracker_V1._0.Data.Migrations
{
    public partial class AttachmentModelChangeFinalTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Attachments",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Attachments");
        }
    }
}
