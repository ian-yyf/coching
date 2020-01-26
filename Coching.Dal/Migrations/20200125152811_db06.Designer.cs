﻿// <auto-generated />
using System;
using Coching.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Coching.Dal.Migrations
{
    [DbContext(typeof(CochingModels))]
    [Migration("20200125152811_db06")]
    partial class db06
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Coching.Model.ActionLogs", b =>
                {
                    b.Property<Guid>("KeyGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int>("Kind")
                        .HasColumnType("int");

                    b.Property<Guid>("ProjectGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("KeyGuid");

                    b.ToTable("ActionLogs");
                });

            modelBuilder.Entity("Coching.Model.Nodes", b =>
                {
                    b.Property<Guid>("KeyGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("ActualManHour")
                        .HasColumnType("decimal(10, 1)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatorGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("EstimatedManHour")
                        .HasColumnType("decimal(10, 1)");

                    b.Property<string>("HtmlDescription")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(16)")
                        .HasMaxLength(16);

                    b.Property<Guid>("ParentGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProjectGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RootGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("WorkerGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("KeyGuid");

                    b.ToTable("Nodes");
                });

            modelBuilder.Entity("Coching.Model.Notes", b =>
                {
                    b.Property<Guid>("KeyGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatorGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("HtmlContent")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("NodeGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("KeyGuid");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("Coching.Model.Offers", b =>
                {
                    b.Property<Guid>("KeyGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<decimal>("EstimatedManHour")
                        .HasColumnType("decimal(10, 1)");

                    b.Property<Guid>("NodeGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("KeyGuid");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("Coching.Model.Partners", b =>
                {
                    b.Property<Guid>("KeyGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("JoinTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ProjectGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<Guid>("UserGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("KeyGuid");

                    b.ToTable("Partners");
                });

            modelBuilder.Entity("Coching.Model.Projects", b =>
                {
                    b.Property<Guid>("KeyGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatorGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Header")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("HtmlDescription")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.HasKey("KeyGuid");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Public.Model.ApiAuths", b =>
                {
                    b.Property<Guid>("KeyGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ApiAppId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int>("Kind")
                        .HasColumnType("int");

                    b.Property<string>("OpenId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OwnerGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UnionId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KeyGuid");

                    b.ToTable("ApiAuths");
                });

            modelBuilder.Entity("Public.Model.Areas", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Parent")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Code");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("Public.Model.CategoryAccountLogs", b =>
                {
                    b.Property<Guid>("KeyGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("OrderGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("OriginalBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KeyGuid");

                    b.ToTable("CategoryAccountLogs");
                });

            modelBuilder.Entity("Public.Model.CategoryAccounts", b =>
                {
                    b.Property<Guid>("KeyGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int>("Kind")
                        .HasColumnType("int");

                    b.Property<Guid>("OwnerGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("KeyGuid");

                    b.ToTable("CategoryAccounts");
                });

            modelBuilder.Entity("Public.Model.DocumentRefs", b =>
                {
                    b.Property<Guid>("KeyGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("DocumentGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OwnerGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("KeyGuid");

                    b.ToTable("DocumentRefs");
                });

            modelBuilder.Entity("Public.Model.Documents", b =>
                {
                    b.Property<Guid>("KeyGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Kind")
                        .IsRequired()
                        .HasColumnType("nvarchar(8)")
                        .HasMaxLength(8);

                    b.Property<string>("Src")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<Guid>("UserGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("KeyGuid");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Public.Model.Kinds", b =>
                {
                    b.Property<Guid>("KeyGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int>("Key")
                        .HasColumnType("int");

                    b.Property<Guid>("ParentGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KeyGuid");

                    b.ToTable("Kinds");
                });

            modelBuilder.Entity("Public.Model.PermissionConfigs", b =>
                {
                    b.Property<Guid>("KeyGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("OwnerGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PermissionGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("KeyGuid");

                    b.ToTable("PermissionConfigs");
                });

            modelBuilder.Entity("Public.Model.Permissions", b =>
                {
                    b.Property<Guid>("KeyGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Kind")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KeyGuid");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Public.Model.PrefixCodeCursors", b =>
                {
                    b.Property<Guid>("KeyGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("CursorIndex")
                        .HasColumnType("int");

                    b.Property<bool>("Deleteable")
                        .HasColumnType("bit");

                    b.Property<Guid>("GroupGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Prefix")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimeText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KeyGuid");

                    b.ToTable("PrefixCodeCursors");
                });

            modelBuilder.Entity("Public.Model.RechargeDefines", b =>
                {
                    b.Property<Guid>("KeyGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<decimal>("FaceValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PresentValue")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("KeyGuid");

                    b.ToTable("RechargeDefines");
                });

            modelBuilder.Entity("Public.Model.RechargeOrders", b =>
                {
                    b.Property<Guid>("KeyGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<decimal>("FaceValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("OrderNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OwnerGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("PaidTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PaiedAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PresentValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TransactionId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KeyGuid");

                    b.ToTable("RechargeOrders");
                });

            modelBuilder.Entity("Public.Model.StatusGroups", b =>
                {
                    b.Property<Guid>("KeyGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int>("Key")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KeyGuid");

                    b.ToTable("StatusGroups");
                });

            modelBuilder.Entity("Public.Model.StatusLogs", b =>
                {
                    b.Property<Guid>("KeyGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DelReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DelTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("FromStatusGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OperateUserGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OwnerGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ToStatusGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("KeyGuid");

                    b.ToTable("StatusLogs");
                });

            modelBuilder.Entity("Public.Model.Statuses", b =>
                {
                    b.Property<Guid>("KeyGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("GroupGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Key")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KeyGuid");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("Public.Model.Tokens", b =>
                {
                    b.Property<Guid>("KeyGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int>("ExpiresIn")
                        .HasColumnType("int");

                    b.Property<Guid>("OwnerGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KeyGuid");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("Public.Model.Users", b =>
                {
                    b.Property<Guid>("KeyGuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Header")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("KindGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tel")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KeyGuid");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
