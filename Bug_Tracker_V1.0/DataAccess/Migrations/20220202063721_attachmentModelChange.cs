using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bug_Tracker_V1._0.Data.Migrations
{
    public partial class attachmentModelChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataFiles",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Attachments");

            migrationBuilder.RenameColumn(
                name: "DocumentId",
                table: "Attachments",
                newName: "FileId");

            migrationBuilder.AddColumn<string>(
                name: "FileContentType",
                table: "Attachments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Attachments",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileContentType",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Attachments");

            migrationBuilder.RenameColumn(
                name: "FileId",
                table: "Attachments",
                newName: "DocumentId");

            migrationBuilder.AddColumn<byte[]>(
                name: "DataFiles",
                table: "Attachments",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "Attachments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Attachments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
