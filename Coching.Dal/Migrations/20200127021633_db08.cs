using Microsoft.EntityFrameworkCore.Migrations;

namespace Coching.Dal.Migrations
{
    public partial class db08 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tel",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrderNo",
                table: "RechargeOrders",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TimeText",
                table: "PrefixCodeCursors",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Prefix",
                table: "PrefixCodeCursors",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Parent",
                table: "Areas",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UnionId",
                table: "ApiAuths",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OpenId",
                table: "ApiAuths",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ApiAppId",
                table: "ApiAuths",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Users_KindGuid",
                table: "Users",
                column: "KindGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Name",
                table: "Users",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Tel",
                table: "Users",
                column: "Tel");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_CreatedTime",
                table: "Tokens",
                column: "CreatedTime");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_OwnerGuid",
                table: "Tokens",
                column: "OwnerGuid");

            migrationBuilder.CreateIndex(
                name: "IX_StatusGroups_Key",
                table: "StatusGroups",
                column: "Key");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_GroupGuid",
                table: "Statuses",
                column: "GroupGuid");

            migrationBuilder.CreateIndex(
                name: "IX_RechargeOrders_OrderNo",
                table: "RechargeOrders",
                column: "OrderNo");

            migrationBuilder.CreateIndex(
                name: "IX_RechargeOrders_OwnerGuid",
                table: "RechargeOrders",
                column: "OwnerGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CreatorGuid",
                table: "Projects",
                column: "CreatorGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Name",
                table: "Projects",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_PrefixCodeCursors_GroupGuid",
                table: "PrefixCodeCursors",
                column: "GroupGuid");

            migrationBuilder.CreateIndex(
                name: "IX_PrefixCodeCursors_Prefix",
                table: "PrefixCodeCursors",
                column: "Prefix");

            migrationBuilder.CreateIndex(
                name: "IX_PrefixCodeCursors_TimeText",
                table: "PrefixCodeCursors",
                column: "TimeText");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Kind",
                table: "Permissions",
                column: "Kind");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionConfigs_OwnerGuid",
                table: "PermissionConfigs",
                column: "OwnerGuid");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionConfigs_PermissionGuid",
                table: "PermissionConfigs",
                column: "PermissionGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Kinds_Key",
                table: "Kinds",
                column: "Key");

            migrationBuilder.CreateIndex(
                name: "IX_Kinds_ParentGuid",
                table: "Kinds",
                column: "ParentGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_UserGuid",
                table: "Documents",
                column: "UserGuid");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRefs_DocumentGuid",
                table: "DocumentRefs",
                column: "DocumentGuid");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentRefs_OwnerGuid",
                table: "DocumentRefs",
                column: "OwnerGuid");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryAccounts_Kind",
                table: "CategoryAccounts",
                column: "Kind");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryAccounts_OwnerGuid",
                table: "CategoryAccounts",
                column: "OwnerGuid");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryAccountLogs_AccountGuid",
                table: "CategoryAccountLogs",
                column: "AccountGuid");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryAccountLogs_CreatedTime",
                table: "CategoryAccountLogs",
                column: "CreatedTime");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryAccountLogs_OrderGuid",
                table: "CategoryAccountLogs",
                column: "OrderGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_Parent",
                table: "Areas",
                column: "Parent");

            migrationBuilder.CreateIndex(
                name: "IX_ApiAuths_ApiAppId",
                table: "ApiAuths",
                column: "ApiAppId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiAuths_Kind",
                table: "ApiAuths",
                column: "Kind");

            migrationBuilder.CreateIndex(
                name: "IX_ApiAuths_OpenId",
                table: "ApiAuths",
                column: "OpenId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiAuths_OwnerGuid",
                table: "ApiAuths",
                column: "OwnerGuid");

            migrationBuilder.CreateIndex(
                name: "IX_ApiAuths_UnionId",
                table: "ApiAuths",
                column: "UnionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_KindGuid",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Name",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Tel",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_CreatedTime",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_OwnerGuid",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_StatusGroups_Key",
                table: "StatusGroups");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_GroupGuid",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_RechargeOrders_OrderNo",
                table: "RechargeOrders");

            migrationBuilder.DropIndex(
                name: "IX_RechargeOrders_OwnerGuid",
                table: "RechargeOrders");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CreatorGuid",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_Name",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_PrefixCodeCursors_GroupGuid",
                table: "PrefixCodeCursors");

            migrationBuilder.DropIndex(
                name: "IX_PrefixCodeCursors_Prefix",
                table: "PrefixCodeCursors");

            migrationBuilder.DropIndex(
                name: "IX_PrefixCodeCursors_TimeText",
                table: "PrefixCodeCursors");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_Kind",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_PermissionConfigs_OwnerGuid",
                table: "PermissionConfigs");

            migrationBuilder.DropIndex(
                name: "IX_PermissionConfigs_PermissionGuid",
                table: "PermissionConfigs");

            migrationBuilder.DropIndex(
                name: "IX_Kinds_Key",
                table: "Kinds");

            migrationBuilder.DropIndex(
                name: "IX_Kinds_ParentGuid",
                table: "Kinds");

            migrationBuilder.DropIndex(
                name: "IX_Documents_UserGuid",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_DocumentRefs_DocumentGuid",
                table: "DocumentRefs");

            migrationBuilder.DropIndex(
                name: "IX_DocumentRefs_OwnerGuid",
                table: "DocumentRefs");

            migrationBuilder.DropIndex(
                name: "IX_CategoryAccounts_Kind",
                table: "CategoryAccounts");

            migrationBuilder.DropIndex(
                name: "IX_CategoryAccounts_OwnerGuid",
                table: "CategoryAccounts");

            migrationBuilder.DropIndex(
                name: "IX_CategoryAccountLogs_AccountGuid",
                table: "CategoryAccountLogs");

            migrationBuilder.DropIndex(
                name: "IX_CategoryAccountLogs_CreatedTime",
                table: "CategoryAccountLogs");

            migrationBuilder.DropIndex(
                name: "IX_CategoryAccountLogs_OrderGuid",
                table: "CategoryAccountLogs");

            migrationBuilder.DropIndex(
                name: "IX_Areas_Parent",
                table: "Areas");

            migrationBuilder.DropIndex(
                name: "IX_ApiAuths_ApiAppId",
                table: "ApiAuths");

            migrationBuilder.DropIndex(
                name: "IX_ApiAuths_Kind",
                table: "ApiAuths");

            migrationBuilder.DropIndex(
                name: "IX_ApiAuths_OpenId",
                table: "ApiAuths");

            migrationBuilder.DropIndex(
                name: "IX_ApiAuths_OwnerGuid",
                table: "ApiAuths");

            migrationBuilder.DropIndex(
                name: "IX_ApiAuths_UnionId",
                table: "ApiAuths");

            migrationBuilder.AlterColumn<string>(
                name: "Tel",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrderNo",
                table: "RechargeOrders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TimeText",
                table: "PrefixCodeCursors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Prefix",
                table: "PrefixCodeCursors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Parent",
                table: "Areas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UnionId",
                table: "ApiAuths",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OpenId",
                table: "ApiAuths",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ApiAppId",
                table: "ApiAuths",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
