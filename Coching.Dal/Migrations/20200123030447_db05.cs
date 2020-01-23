using Microsoft.EntityFrameworkCore.Migrations;

namespace Coching.Dal.Migrations
{
    public partial class db05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HtmlDescription",
                table: "Projects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HtmlContent",
                table: "Notes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HtmlDescription",
                table: "Nodes",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HtmlDescription",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "HtmlContent",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "HtmlDescription",
                table: "Nodes");
        }
    }
}
