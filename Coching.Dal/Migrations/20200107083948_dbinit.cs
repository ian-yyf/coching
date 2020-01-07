using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Coching.Dal.Migrations
{
    public partial class dbinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiAuths",
                columns: table => new
                {
                    KeyGuid = table.Column<Guid>(nullable: false),
                    OwnerGuid = table.Column<Guid>(nullable: false),
                    ApiAppId = table.Column<string>(nullable: false),
                    Kind = table.Column<int>(nullable: false),
                    OpenId = table.Column<string>(nullable: false),
                    UnionId = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiAuths", x => x.KeyGuid);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Grade = table.Column<int>(nullable: false),
                    Parent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "CategoryAccountLogs",
                columns: table => new
                {
                    KeyGuid = table.Column<Guid>(nullable: false),
                    AccountGuid = table.Column<Guid>(nullable: false),
                    OrderGuid = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    OriginalBalance = table.Column<decimal>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryAccountLogs", x => x.KeyGuid);
                });

            migrationBuilder.CreateTable(
                name: "CategoryAccounts",
                columns: table => new
                {
                    KeyGuid = table.Column<Guid>(nullable: false),
                    OwnerGuid = table.Column<Guid>(nullable: false),
                    Kind = table.Column<int>(nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryAccounts", x => x.KeyGuid);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    KeyGuid = table.Column<Guid>(nullable: false),
                    Src = table.Column<string>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    SortIndex = table.Column<int>(nullable: false),
                    OwnerGuid = table.Column<Guid>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.KeyGuid);
                });

            migrationBuilder.CreateTable(
                name: "Kinds",
                columns: table => new
                {
                    KeyGuid = table.Column<Guid>(nullable: false),
                    ParentGuid = table.Column<Guid>(nullable: false),
                    Key = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kinds", x => x.KeyGuid);
                });

            migrationBuilder.CreateTable(
                name: "Nodes",
                columns: table => new
                {
                    KeyGuid = table.Column<Guid>(nullable: false),
                    RootGuid = table.Column<Guid>(nullable: false),
                    ParentGuid = table.Column<Guid>(nullable: false),
                    CreatorGuid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 16, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TotalMinutes = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: true),
                    WorkerGuid = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodes", x => x.KeyGuid);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    KeyGuid = table.Column<Guid>(nullable: false),
                    NodeGuid = table.Column<Guid>(nullable: false),
                    PartnerGuid = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.KeyGuid);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    KeyGuid = table.Column<Guid>(nullable: false),
                    NodeGuid = table.Column<Guid>(nullable: false),
                    UserGuid = table.Column<Guid>(nullable: false),
                    Role = table.Column<int>(nullable: false),
                    JoinTime = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.KeyGuid);
                });

            migrationBuilder.CreateTable(
                name: "PermissionConfigs",
                columns: table => new
                {
                    KeyGuid = table.Column<Guid>(nullable: false),
                    PermissionGuid = table.Column<Guid>(nullable: false),
                    OwnerGuid = table.Column<Guid>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionConfigs", x => x.KeyGuid);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    KeyGuid = table.Column<Guid>(nullable: false),
                    Kind = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.KeyGuid);
                });

            migrationBuilder.CreateTable(
                name: "PrefixCodeCursors",
                columns: table => new
                {
                    KeyGuid = table.Column<Guid>(nullable: false),
                    GroupGuid = table.Column<Guid>(nullable: false),
                    Prefix = table.Column<string>(nullable: true),
                    TimeText = table.Column<string>(nullable: false),
                    Deleteable = table.Column<bool>(nullable: false),
                    CursorIndex = table.Column<int>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrefixCodeCursors", x => x.KeyGuid);
                });

            migrationBuilder.CreateTable(
                name: "RechargeDefines",
                columns: table => new
                {
                    KeyGuid = table.Column<Guid>(nullable: false),
                    FaceValue = table.Column<decimal>(nullable: false),
                    PresentValue = table.Column<decimal>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RechargeDefines", x => x.KeyGuid);
                });

            migrationBuilder.CreateTable(
                name: "RechargeOrders",
                columns: table => new
                {
                    KeyGuid = table.Column<Guid>(nullable: false),
                    OwnerGuid = table.Column<Guid>(nullable: false),
                    OrderNo = table.Column<string>(nullable: true),
                    FaceValue = table.Column<decimal>(nullable: false),
                    PresentValue = table.Column<decimal>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    PaiedAmount = table.Column<decimal>(nullable: false),
                    TransactionId = table.Column<string>(nullable: true),
                    PaidTime = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RechargeOrders", x => x.KeyGuid);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    KeyGuid = table.Column<Guid>(nullable: false),
                    GroupGuid = table.Column<Guid>(nullable: false),
                    Key = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.KeyGuid);
                });

            migrationBuilder.CreateTable(
                name: "StatusGroups",
                columns: table => new
                {
                    KeyGuid = table.Column<Guid>(nullable: false),
                    Key = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusGroups", x => x.KeyGuid);
                });

            migrationBuilder.CreateTable(
                name: "StatusLogs",
                columns: table => new
                {
                    KeyGuid = table.Column<Guid>(nullable: false),
                    OwnerGuid = table.Column<Guid>(nullable: false),
                    OperateUserGuid = table.Column<Guid>(nullable: false),
                    FromStatusGuid = table.Column<Guid>(nullable: false),
                    ToStatusGuid = table.Column<Guid>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Data = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    DelTime = table.Column<DateTime>(nullable: true),
                    DelReason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusLogs", x => x.KeyGuid);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    KeyGuid = table.Column<Guid>(nullable: false),
                    OwnerGuid = table.Column<Guid>(nullable: false),
                    Token = table.Column<string>(nullable: false),
                    ExpiresIn = table.Column<int>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.KeyGuid);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    KeyGuid = table.Column<Guid>(nullable: false),
                    KindGuid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Tel = table.Column<string>(nullable: true),
                    Header = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.KeyGuid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiAuths");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "CategoryAccountLogs");

            migrationBuilder.DropTable(
                name: "CategoryAccounts");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Kinds");

            migrationBuilder.DropTable(
                name: "Nodes");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "PermissionConfigs");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "PrefixCodeCursors");

            migrationBuilder.DropTable(
                name: "RechargeDefines");

            migrationBuilder.DropTable(
                name: "RechargeOrders");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "StatusGroups");

            migrationBuilder.DropTable(
                name: "StatusLogs");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
