using Microsoft.EntityFrameworkCore.Migrations;

namespace Coching.Dal.Migrations
{
    public partial class db10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Coching",
                table: "Partners",
                type: "decimal(10, 1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "Coching",
                table: "Nodes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coching",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "Coching",
                table: "Nodes");
        }
    }
}
