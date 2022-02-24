using Microsoft.EntityFrameworkCore.Migrations;

namespace Bug_Tracker_V1._0.Data.Migrations
{
    public partial class AddNotificationAuthenticationUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AuthenticationUserId",
                table: "Notifications",
                column: "AuthenticationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_AuthenticationUserId",
                table: "Notifications",
                column: "AuthenticationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_AuthenticationUserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_AuthenticationUserId",
                table: "Notifications");
        }
    }
}
