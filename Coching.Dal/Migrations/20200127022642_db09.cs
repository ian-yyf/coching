using Microsoft.EntityFrameworkCore.Migrations;

namespace Coching.Dal.Migrations
{
    public partial class db09 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Partners_ProjectGuid",
                table: "Partners",
                column: "ProjectGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Partners_UserGuid",
                table: "Partners",
                column: "UserGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_NodeGuid",
                table: "Offers",
                column: "NodeGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_UserGuid",
                table: "Offers",
                column: "UserGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_CreatorGuid",
                table: "Notes",
                column: "CreatorGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_NodeGuid",
                table: "Notes",
                column: "NodeGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_ParentGuid",
                table: "Nodes",
                column: "ParentGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_ProjectGuid",
                table: "Nodes",
                column: "ProjectGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_RootGuid",
                table: "Nodes",
                column: "RootGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Nodes_WorkerGuid",
                table: "Nodes",
                column: "WorkerGuid");

            migrationBuilder.CreateIndex(
                name: "IX_ActionLogs_ProjectGuid",
                table: "ActionLogs",
                column: "ProjectGuid");

            migrationBuilder.CreateIndex(
                name: "IX_ActionLogs_UserGuid",
                table: "ActionLogs",
                column: "UserGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Partners_ProjectGuid",
                table: "Partners");

            migrationBuilder.DropIndex(
                name: "IX_Partners_UserGuid",
                table: "Partners");

            migrationBuilder.DropIndex(
                name: "IX_Offers_NodeGuid",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_UserGuid",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Notes_CreatorGuid",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_NodeGuid",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Nodes_ParentGuid",
                table: "Nodes");

            migrationBuilder.DropIndex(
                name: "IX_Nodes_ProjectGuid",
                table: "Nodes");

            migrationBuilder.DropIndex(
                name: "IX_Nodes_RootGuid",
                table: "Nodes");

            migrationBuilder.DropIndex(
                name: "IX_Nodes_WorkerGuid",
                table: "Nodes");

            migrationBuilder.DropIndex(
                name: "IX_ActionLogs_ProjectGuid",
                table: "ActionLogs");

            migrationBuilder.DropIndex(
                name: "IX_ActionLogs_UserGuid",
                table: "ActionLogs");
        }
    }
}
