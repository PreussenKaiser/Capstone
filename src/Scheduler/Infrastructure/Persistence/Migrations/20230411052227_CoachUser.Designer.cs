﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Scheduler.Infrastructure.Persistence;

#nullable disable

namespace Scheduler.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(SchedulerContext))]
    [Migration("20230411052227_CoachUser")]
    partial class CoachUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EventField", b =>
                {
                    b.Property<Guid>("EventsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FieldsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EventsId", "FieldsId");

                    b.HasIndex("FieldsId");

                    b.ToTable("EventField");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
                            RoleId = new Guid("cfd242d3-2107-4563-b2a4-15383e683964")
                        },
                        new
                        {
                            UserId = new Guid("9e55284c-a2ba-425f-be26-a18e384668a7"),
                            RoleId = new Guid("1d01aab9-71cb-48b4-a9eb-6158654c93d8")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Scheduler.Domain.Models.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsBlackout")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Events");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Scheduler.Domain.Models.Field", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("Scheduler.Domain.Models.League", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("Leagues");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1d7617a9-e21e-4424-aa3a-92758efa6200"),
                            Name = "Recreation"
                        },
                        new
                        {
                            Id = new Guid("8b633d7c-e605-4cc0-ba94-e933f4e288a6"),
                            Name = "Classic"
                        },
                        new
                        {
                            Id = new Guid("ad38d50e-3670-416d-a5be-d9a84cea55ad"),
                            Name = "Select"
                        });
                });

            modelBuilder.Entity("Scheduler.Domain.Models.Recurrence", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("Occurrences")
                        .HasColumnType("tinyint");

                    b.Property<byte>("Type")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.ToTable("Recurrences");
                });

            modelBuilder.Entity("Scheduler.Domain.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
                            ConcurrencyStamp = "059420df-9f33-407b-a8c6-1aa60c544098",
                            Name = "Admin",
                            NormalizedName = "Admin"
                        },
                        new
                        {
                            Id = new Guid("1d01aab9-71cb-48b4-a9eb-6158654c93d8"),
                            ConcurrencyStamp = "b17a6f46-8bfd-46bc-ad1c-2fdd6792fde1",
                            Name = "Coach",
                            NormalizedName = "Coach"
                        });
                });

            modelBuilder.Entity("Scheduler.Domain.Models.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LeagueId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Scheduler.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("NeedsNewPassword")
                        .HasColumnType("bit");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "1a2519a6-9554-4ced-9afc-fbac67ec8cd3",
                            Email = "teamnull@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Team",
                            LastName = "Null",
                            LockoutEnabled = false,
                            NeedsNewPassword = false,
                            NormalizedUserName = "TEAMNULL@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEHwKbHWuNMvq3uPogouRdlXg7lj7ax/m2eVsDyg7umWR4PGg773fMhv3bEyWeX/Vnw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "5c0e4d2a-3916-4722-a2c5-8c3df59f741c",
                            TwoFactorEnabled = false,
                            UserName = "teamnull@gmail.com"
                        },
                        new
                        {
                            Id = new Guid("9e55284c-a2ba-425f-be26-a18e384668a7"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "c678b59e-091c-421d-8452-f786ba8b4fbb",
                            Email = "johncoach@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "John",
                            LastName = "Coach",
                            LockoutEnabled = false,
                            NeedsNewPassword = false,
                            NormalizedUserName = "JOHNCOACH@GMAIL.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEHops0eMXDCD7zfw24vLLDyOlxs832KtEPgxOLFH+PkpWwHpjF0WmpapdbtIlHUNiw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "201e46f5-1de4-4ed2-811d-8b3bab2a25a3",
                            TwoFactorEnabled = false,
                            UserName = "johncoach@gmail.com"
                        });
                });

            modelBuilder.Entity("Scheduler.Domain.Models.Game", b =>
                {
                    b.HasBaseType("Scheduler.Domain.Models.Event");

                    b.Property<Guid>("HomeTeamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OpposingTeamId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("HomeTeamId");

                    b.HasIndex("OpposingTeamId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Scheduler.Domain.Models.Practice", b =>
                {
                    b.HasBaseType("Scheduler.Domain.Models.Event");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("TeamId");

                    b.ToTable("Practices");
                });

            modelBuilder.Entity("EventField", b =>
                {
                    b.HasOne("Scheduler.Domain.Models.Event", null)
                        .WithMany()
                        .HasForeignKey("EventsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scheduler.Domain.Models.Field", null)
                        .WithMany()
                        .HasForeignKey("FieldsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Scheduler.Domain.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Scheduler.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Scheduler.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Scheduler.Domain.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scheduler.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Scheduler.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Scheduler.Domain.Models.Recurrence", b =>
                {
                    b.HasOne("Scheduler.Domain.Models.Event", "Event")
                        .WithOne("Recurrence")
                        .HasForeignKey("Scheduler.Domain.Models.Recurrence", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Scheduler.Domain.Models.Team", b =>
                {
                    b.HasOne("Scheduler.Domain.Models.League", "League")
                        .WithMany()
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("League");
                });

            modelBuilder.Entity("Scheduler.Domain.Models.Game", b =>
                {
                    b.HasOne("Scheduler.Domain.Models.Team", "HomeTeam")
                        .WithMany()
                        .HasForeignKey("HomeTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scheduler.Domain.Models.Event", null)
                        .WithOne()
                        .HasForeignKey("Scheduler.Domain.Models.Game", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scheduler.Domain.Models.Team", "OpposingTeam")
                        .WithMany()
                        .HasForeignKey("OpposingTeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HomeTeam");

                    b.Navigation("OpposingTeam");
                });

            modelBuilder.Entity("Scheduler.Domain.Models.Practice", b =>
                {
                    b.HasOne("Scheduler.Domain.Models.Event", null)
                        .WithOne()
                        .HasForeignKey("Scheduler.Domain.Models.Practice", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scheduler.Domain.Models.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Scheduler.Domain.Models.Event", b =>
                {
                    b.Navigation("Recurrence");
                });
#pragma warning restore 612, 618
        }
    }
}