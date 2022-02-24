using Microsoft.EntityFrameworkCore.Migrations;

namespace Bug_Tracker_V1._0.Data.Migrations
{
    public partial class AttachmentModelChangeFinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Attachments",
                newName: "UploadedBy");

            migrationBuilder.RenameColumn(
                name: "FileContentType",
                table: "Attachments",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FileId",
                table: "Attachments",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Attachments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "Attachments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "Attachments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "Attachments");

            migrationBuilder.RenameColumn(
                name: "UploadedBy",
                table: "Attachments",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Attachments",
                newName: "FileContentType");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Attachments",
                newName: "FileId");
        }
    }
}
