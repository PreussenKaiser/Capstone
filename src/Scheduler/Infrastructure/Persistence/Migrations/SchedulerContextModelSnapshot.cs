﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Scheduler.Infrastructure.Persistence;

#nullable disable

namespace Scheduler.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(SchedulerContext))]
    partial class SchedulerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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
                            RoleId = new Guid("a3bc18ef-042d-4de7-bdba-698a90c82b26")
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

                    b.Property<Guid?>("FieldId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsBlackout")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<Guid?>("RecurrenceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FieldId");

                    b.HasIndex("RecurrenceId");

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
<<<<<<< HEAD
                            Id = new Guid("0deb4a71-a3fd-421d-929c-1c846caf969b"),
=======
                            Id = new Guid("4bfcc09b-a3bd-4ef0-9d9c-13a1d460a2f1"),
>>>>>>> 19918c65347a74a6f9901858b0a703e7849376c6
                            Name = "Recreation"
                        },
                        new
                        {
<<<<<<< HEAD
                            Id = new Guid("c4d8ccb4-e1d6-48c6-a03f-75f22811d0fb"),
=======
                            Id = new Guid("ed07d69a-23e5-4f31-8853-51c4ff15b8e7"),
>>>>>>> 19918c65347a74a6f9901858b0a703e7849376c6
                            Name = "Classic"
                        },
                        new
                        {
<<<<<<< HEAD
                            Id = new Guid("6ce725e2-5d81-481a-9a0c-b7cb19d9175d"),
=======
                            Id = new Guid("6a9b5dfe-6816-4c92-aa4a-3019361031c3"),
>>>>>>> 19918c65347a74a6f9901858b0a703e7849376c6
                            Name = "Select"
                        });
                });

            modelBuilder.Entity("Scheduler.Domain.Models.Recurrence", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("Occurrences")
                        .HasColumnType("SMALLINT");

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
<<<<<<< HEAD
                            ConcurrencyStamp = "e7681df4-5523-4ead-9994-60873702e99c",
=======
                            ConcurrencyStamp = "0a320e9a-1cc2-449d-ae14-58254ab295b0",
>>>>>>> 19918c65347a74a6f9901858b0a703e7849376c6
                            Name = "Admin",
                            NormalizedName = "Admin"
                        },
                        new
                        {
<<<<<<< HEAD
                            Id = new Guid("a3bc18ef-042d-4de7-bdba-698a90c82b26"),
                            ConcurrencyStamp = "fb1ce8ea-0cff-4b4d-a589-945533b8f8cd",
=======
                            Id = new Guid("0b32fb4e-1c0b-4b62-8535-114023faed36"),
                            ConcurrencyStamp = "16b6218b-0ecf-475b-92e3-251f58675dd5",
>>>>>>> 19918c65347a74a6f9901858b0a703e7849376c6
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

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId");

                    b.HasIndex("UserId");

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
<<<<<<< HEAD
                            ConcurrencyStamp = "443a0c9e-d459-441b-aae4-4c9be42c08fb",
=======
                            ConcurrencyStamp = "924f3d2e-7822-44e8-8955-837dc9086104",
>>>>>>> 19918c65347a74a6f9901858b0a703e7849376c6
                            Email = "teamnull@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Team",
                            LastName = "Null",
                            LockoutEnabled = false,
                            NeedsNewPassword = false,
                            NormalizedUserName = "TEAMNULL@GMAIL.COM",
<<<<<<< HEAD
                            PasswordHash = "AQAAAAIAAYagAAAAEOA3Bd85FS6ANpEYTjgpLPK1lWMRkbEZ7evteVfnANTlV7ScXljYdRROBANAE6cjlw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "cfc3f529-eb7b-445c-8ffd-98e57079fb23",
=======
                            PasswordHash = "AQAAAAIAAYagAAAAEM/4KNADzohdlSYaE7ZfgdFUcSXyjssHB/FshEmm+dFMWox63OYzn3i9wf6yuB4u8g==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "d1d6f9c6-fa7d-42e6-995b-6ea8b7c033d9",
>>>>>>> 19918c65347a74a6f9901858b0a703e7849376c6
                            TwoFactorEnabled = false,
                            UserName = "teamnull@gmail.com"
                        },
                        new
                        {
                            Id = new Guid("9e55284c-a2ba-425f-be26-a18e384668a7"),
                            AccessFailedCount = 0,
<<<<<<< HEAD
                            ConcurrencyStamp = "24d50c64-0694-4553-8449-f0a87e34ba8f",
=======
                            ConcurrencyStamp = "166cda6d-4795-4c74-a7a5-18b613cbcab6",
>>>>>>> 19918c65347a74a6f9901858b0a703e7849376c6
                            Email = "johncoach@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "John",
                            LastName = "Coach",
                            LockoutEnabled = false,
                            NeedsNewPassword = false,
                            NormalizedUserName = "JOHNCOACH@GMAIL.COM",
<<<<<<< HEAD
                            PasswordHash = "AQAAAAIAAYagAAAAEDAmLfkVTmfK7CfNhUYRBKUe1u13VD53UQjj2yQ00dThMwIpqLImfHZxdqx635BRVw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "076e2cdf-d3ae-4a86-8017-6f648c6727cc",
=======
                            PasswordHash = "AQAAAAIAAYagAAAAEFTN6zqhWIQNqECU2FQ8WgJQ+mNRTukg/FZPSn/Yopn4TbrmxHpriDAGzD+sM6gAtA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "ce669a38-5496-4a16-8655-4cc584e15012",
>>>>>>> 19918c65347a74a6f9901858b0a703e7849376c6
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

            modelBuilder.Entity("Scheduler.Domain.Models.Event", b =>
                {
                    b.HasOne("Scheduler.Domain.Models.Field", "Field")
                        .WithMany("Events")
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Scheduler.Domain.Models.Recurrence", "Recurrence")
                        .WithMany("Events")
                        .HasForeignKey("RecurrenceId");

                    b.Navigation("Field");

                    b.Navigation("Recurrence");
                });

            modelBuilder.Entity("Scheduler.Domain.Models.Team", b =>
                {
                    b.HasOne("Scheduler.Domain.Models.League", "League")
                        .WithMany("Teams")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scheduler.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("League");

                    b.Navigation("User");
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

            modelBuilder.Entity("Scheduler.Domain.Models.Field", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("Scheduler.Domain.Models.League", b =>
                {
                    b.Navigation("Teams");
                });

            modelBuilder.Entity("Scheduler.Domain.Models.Recurrence", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
