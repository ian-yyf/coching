using Microsoft.EntityFrameworkCore.Migrations;

namespace Coching.Dal.Migrations
{
    public partial class db11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FromStatusKey",
                table: "StatusLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToStatusKey",
                table: "StatusLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StatusLogs_FromStatusGuid",
                table: "StatusLogs",
                column: "FromStatusGuid");

            migrationBuilder.CreateIndex(
                name: "IX_StatusLogs_FromStatusKey",
                table: "StatusLogs",
                column: "FromStatusKey");

            migrationBuilder.CreateIndex(
                name: "IX_StatusLogs_OwnerGuid",
                table: "StatusLogs",
                column: "OwnerGuid");

            migrationBuilder.CreateIndex(
                name: "IX_StatusLogs_ToStatusGuid",
                table: "StatusLogs",
                column: "ToStatusGuid");

            migrationBuilder.CreateIndex(
                name: "IX_StatusLogs_ToStatusKey",
                table: "StatusLogs",
                column: "ToStatusKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StatusLogs_FromStatusGuid",
                table: "StatusLogs");

            migrationBuilder.DropIndex(
                name: "IX_StatusLogs_FromStatusKey",
                table: "StatusLogs");

            migrationBuilder.DropIndex(
                name: "IX_StatusLogs_OwnerGuid",
                table: "StatusLogs");

            migrationBuilder.DropIndex(
                name: "IX_StatusLogs_ToStatusGuid",
                table: "StatusLogs");

            migrationBuilder.DropIndex(
                name: "IX_StatusLogs_ToStatusKey",
                table: "StatusLogs");

            migrationBuilder.DropColumn(
                name: "FromStatusKey",
                table: "StatusLogs");

            migrationBuilder.DropColumn(
                name: "ToStatusKey",
                table: "StatusLogs");
        }
    }
}
