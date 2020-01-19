using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coching.Dal.Migrations
{
    public partial class db03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NodeGuid",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "TotalMinutes",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "TotalMinutes",
                table: "Nodes");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectGuid",
                table: "Partners",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedManHour",
                table: "Offers",
                type: "decimal(10, 1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkerGuid",
                table: "Nodes",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ActualManHour",
                table: "Nodes",
                type: "decimal(10, 1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Nodes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedManHour",
                table: "Nodes",
                type: "decimal(10, 1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectGuid",
                table: "Nodes",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    KeyGuid = table.Column<Guid>(nullable: false),
                    CreatorGuid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    Header = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.KeyGuid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectGuid",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "EstimatedManHour",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "ActualManHour",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "EstimatedManHour",
                table: "Nodes");

            migrationBuilder.DropColumn(
                name: "ProjectGuid",
                table: "Nodes");

            migrationBuilder.AddColumn<Guid>(
                name: "NodeGuid",
                table: "Partners",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "TotalMinutes",
                table: "Offers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkerGuid",
                table: "Nodes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<int>(
                name: "TotalMinutes",
                table: "Nodes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
