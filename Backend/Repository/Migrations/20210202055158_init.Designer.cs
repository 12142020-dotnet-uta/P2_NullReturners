﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository;

namespace Repository.Migrations
{
    [DbContext(typeof(ProgContext))]
    [Migration("20210202055158_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Models.EquipmentItem", b =>
                {
                    b.Property<int>("EquipmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EquipmentID");

                    b.ToTable("EquipmentItems");
                });

            modelBuilder.Entity("Models.EquipmentRequest", b =>
                {
                    b.Property<int>("RequestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeamID")
                        .HasColumnType("int");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RequestID");

                    b.ToTable("EquipmentRequests");
                });

            modelBuilder.Entity("Models.Game", b =>
                {
                    b.Property<int>("GameID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AwayScore")
                        .HasColumnType("int");

                    b.Property<int>("AwayTeamID")
                        .HasColumnType("int");

                    b.Property<DateTime>("GameDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("HomeScore")
                        .HasColumnType("int");

                    b.Property<int>("HomeTeamID")
                        .HasColumnType("int");

                    b.Property<string>("Statistic1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Statistic2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Statistic3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WinningTeam")
                        .HasColumnType("int");

                    b.HasKey("GameID");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Models.Message", b =>
                {
                    b.Property<Guid>("MessageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsCarpool")
                        .HasColumnType("bit");

                    b.Property<string>("MessageText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RecipientListID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SenderID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("SentDate")
                        .HasColumnType("datetime2");

                    b.HasKey("MessageID");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Models.Play", b =>
                {
                    b.Property<int>("PlayID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("DrawnPlay")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlaybookId")
                        .HasColumnType("int");

                    b.HasKey("PlayID");

                    b.ToTable("Plays");
                });

            modelBuilder.Entity("Models.Playbook", b =>
                {
                    b.Property<int>("PlaybookID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("TeamID")
                        .HasColumnType("int");

                    b.HasKey("PlaybookID");

                    b.ToTable("Playbooks");
                });

            modelBuilder.Entity("Models.RecipientList", b =>
                {
                    b.Property<Guid>("RecipientListID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RecipientID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RecipientListID", "RecipientID");

                    b.ToTable("RecipientLists");
                });

            modelBuilder.Entity("Models.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Models.Team", b =>
                {
                    b.Property<int>("TeamID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Losses")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Wins")
                        .HasColumnType("int");

                    b.HasKey("TeamID");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Property<Guid>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.Property<int>("TeamID")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Models.UserInbox", b =>
                {
                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MessageID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.HasKey("UserID", "MessageID");

                    b.ToTable("UserInboxes");
                });
#pragma warning restore 612, 618
        }
    }
}
