using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coching.Dal.Migrations
{
    public partial class db06 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionLogs",
                columns: table => new
                {
                    KeyGuid = table.Column<Guid>(nullable: false),
                    ProjectGuid = table.Column<Guid>(nullable: false),
                    UserGuid = table.Column<Guid>(nullable: false),
                    Kind = table.Column<int>(nullable: false),
                    Content = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionLogs", x => x.KeyGuid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionLogs");
        }
    }
}
