using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coching.Dal.Migrations
{
    public partial class db13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifyTime",
                table: "Nodes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModifyTime",
                table: "Nodes");
        }
    }
}
