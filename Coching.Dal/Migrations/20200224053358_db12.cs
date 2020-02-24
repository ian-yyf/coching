using Microsoft.EntityFrameworkCore.Migrations;

namespace Coching.Dal.Migrations
{
    public partial class db12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Nodes_EndTime",
                table: "Nodes",
                column: "EndTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Nodes_EndTime",
                table: "Nodes");
        }
    }
}
