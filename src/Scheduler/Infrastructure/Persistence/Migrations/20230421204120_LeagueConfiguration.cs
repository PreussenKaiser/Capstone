using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Scheduler.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class LeagueConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("1bb1e9bf-c41e-428b-9b9b-f8941be4de12"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("3ae67273-61d2-4e24-b6a6-a614729871a7"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("800e198f-d209-46fe-a29b-0ccd3525e430"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("ac454bbb-a298-4b99-a4db-21450225cce0"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1bb1e9bf-c41e-428b-9b9b-f8941be4de12"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
                column: "ConcurrencyStamp",
                value: "961fc0a3-016f-41fc-ae8f-7dfd55aa2245");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("7685375a-74b9-4c92-a2a3-e3ee364ab066"), "c60a47c9-0a99-49c6-b0cb-0120188b8438", "Coach", "Coach" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "17cc4356-8b37-4a0c-874b-e2c1785ba5a9", "AQAAAAIAAYagAAAAEGtIiB/hQiVf0ax4MRTXqpwymgo9GnlIsp1KLXui7/LAz+WUxIIgkRmwvWfow6POJg==", "8d1e3e88-5d9a-4a83-b8c6-9b7985df5d80" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e55284c-a2ba-425f-be26-a18e384668a7"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4124d63e-14da-4848-9b31-ffd9ecd8afd0", "AQAAAAIAAYagAAAAELQpRYKRB3eivvphq6gvyBoJySePY+gWdt9HBGlSR2aDKOvvXPnRcvGYzVtYY7Ojcg==", "89104cd9-ee98-483f-8dc1-952060c11985" });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("7f76481f-15f9-434c-ad7d-626a0e03a14a"), "Select" },
                    { new Guid("9d364cc2-2e03-4d95-852f-c9858b536037"), "Recreation" },
                    { new Guid("a628f0ae-b85f-4a69-b52e-f4a6c0cd2b2c"), "Classic" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("7685375a-74b9-4c92-a2a3-e3ee364ab066"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7685375a-74b9-4c92-a2a3-e3ee364ab066"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("7f76481f-15f9-434c-ad7d-626a0e03a14a"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("9d364cc2-2e03-4d95-852f-c9858b536037"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("a628f0ae-b85f-4a69-b52e-f4a6c0cd2b2c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7685375a-74b9-4c92-a2a3-e3ee364ab066"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
                column: "ConcurrencyStamp",
                value: "3551b23f-55c9-464f-a651-689afe98f573");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("1bb1e9bf-c41e-428b-9b9b-f8941be4de12"), "f858750a-1aa9-428b-a959-635fdca88810", "Coach", "Coach" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "56adea76-624a-4130-8290-8c25ea3c30cd", "AQAAAAIAAYagAAAAEFxJMjcChkgMKMpnPTMhkfCfov4Y/xI14CtX1FnP3f6s5ns8vnLasS3Vmu0R6oLyWw==", "e4416792-9763-445e-a48b-8d35217eea97" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e55284c-a2ba-425f-be26-a18e384668a7"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78cec94f-ae31-480f-88be-72ca61aa3834", "AQAAAAIAAYagAAAAEHhQgiCllp/pn7eS9YiufQIt7fbHMhm59LR4j8Tefi7jiZNXpYF9B/NWJsCuCEMr0Q==", "16fe593e-c61c-446d-9789-2681bd16d4ac" });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3ae67273-61d2-4e24-b6a6-a614729871a7"), "Recreation" },
                    { new Guid("800e198f-d209-46fe-a29b-0ccd3525e430"), "Select" },
                    { new Guid("ac454bbb-a298-4b99-a4db-21450225cce0"), "Classic" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("1bb1e9bf-c41e-428b-9b9b-f8941be4de12"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });
        }
    }
}
