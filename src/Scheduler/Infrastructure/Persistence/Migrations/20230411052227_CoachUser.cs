using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Scheduler.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CoachUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("095cc742-f515-44df-a9ca-f4b72ccedcdd"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("41f1839d-cf9b-4c0e-a8c7-8ee4e552c038"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("8af538be-4aaa-4489-b4e6-9bff8caa02b7"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
                column: "ConcurrencyStamp",
                value: "059420df-9f33-407b-a8c6-1aa60c544098");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("1d01aab9-71cb-48b4-a9eb-6158654c93d8"), "b17a6f46-8bfd-46bc-ad1c-2fdd6792fde1", "Coach", "Coach" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1a2519a6-9554-4ced-9afc-fbac67ec8cd3", "AQAAAAIAAYagAAAAEHwKbHWuNMvq3uPogouRdlXg7lj7ax/m2eVsDyg7umWR4PGg773fMhv3bEyWeX/Vnw==", "5c0e4d2a-3916-4722-a2c5-8c3df59f741c" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NeedsNewPassword", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("9e55284c-a2ba-425f-be26-a18e384668a7"), 0, "c678b59e-091c-421d-8452-f786ba8b4fbb", "johncoach@gmail.com", false, "John", "Coach", false, null, false, null, "JOHNCOACH@GMAIL.COM", "AQAAAAIAAYagAAAAEHops0eMXDCD7zfw24vLLDyOlxs832KtEPgxOLFH+PkpWwHpjF0WmpapdbtIlHUNiw==", null, false, "201e46f5-1de4-4ed2-811d-8b3bab2a25a3", false, "johncoach@gmail.com" });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1d7617a9-e21e-4424-aa3a-92758efa6200"), "Recreation" },
                    { new Guid("8b633d7c-e605-4cc0-ba94-e933f4e288a6"), "Classic" },
                    { new Guid("ad38d50e-3670-416d-a5be-d9a84cea55ad"), "Select" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("1d01aab9-71cb-48b4-a9eb-6158654c93d8"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("1d01aab9-71cb-48b4-a9eb-6158654c93d8"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("1d7617a9-e21e-4424-aa3a-92758efa6200"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("8b633d7c-e605-4cc0-ba94-e933f4e288a6"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("ad38d50e-3670-416d-a5be-d9a84cea55ad"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1d01aab9-71cb-48b4-a9eb-6158654c93d8"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e55284c-a2ba-425f-be26-a18e384668a7"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
                column: "ConcurrencyStamp",
                value: "f856f115-8b7f-4720-9dad-dfd3c3c036cf");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "30c01974-1c05-4592-bea7-eb9a42be512e", "AQAAAAIAAYagAAAAEMCCTnj/CtVAznSOkW9Zj8Vqbmx5mCPy6xKVgk2qzzNgLayq5KhLoeLtbdO64tuojg==", "4950cf3d-21fb-4da7-b2dd-7aa26b14b467" });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("095cc742-f515-44df-a9ca-f4b72ccedcdd"), "Classic" },
                    { new Guid("41f1839d-cf9b-4c0e-a8c7-8ee4e552c038"), "Recreation" },
                    { new Guid("8af538be-4aaa-4489-b4e6-9bff8caa02b7"), "Select" }
                });
        }
    }
}
