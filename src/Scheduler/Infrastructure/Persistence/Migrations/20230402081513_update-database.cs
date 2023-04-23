using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Scheduler.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("414b60a3-5024-4398-883c-ae475569ac49"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("527d987f-0d54-4359-bd39-3657729a580d"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("ff4644a5-597d-4e9f-be43-4100406f6958"));

            migrationBuilder.RenameColumn(
                name: "isBlackout",
                table: "Events",
                newName: "IsBlackout");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
                column: "ConcurrencyStamp",
                value: "47c18bc7-b27b-4064-bf6a-76b18df97148");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "69eca07c-7c33-4b03-b725-2e35bf55e002", "AQAAAAIAAYagAAAAEAjOr9EaPT9P4uTjRFl2P5R9EVz6susz6JbQE5gEkM8uC8uu70bvKhP+79U+RJ1c6g==", "65cbbd21-f409-4eef-a787-a46d03e4e4d2" });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("49cdbf8a-8671-47ea-91a7-a7d494010c7c"), "Classic" },
                    { new Guid("db9fd005-a25a-4e0e-a215-cc2bcc72a5b9"), "Recreation" },
                    { new Guid("f53f7983-4f9a-46d3-a466-a953cf544f1d"), "Select" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("49cdbf8a-8671-47ea-91a7-a7d494010c7c"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("db9fd005-a25a-4e0e-a215-cc2bcc72a5b9"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("f53f7983-4f9a-46d3-a466-a953cf544f1d"));

            migrationBuilder.RenameColumn(
                name: "IsBlackout",
                table: "Events",
                newName: "isBlackout");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
                column: "ConcurrencyStamp",
                value: "5766cbb1-e14f-4431-b3ec-127174a68b46");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "16b21a3c-eb73-4561-9dea-48c7f279b419", "AQAAAAIAAYagAAAAEAJnVO4LNlmhvnEfESZqmoYnhajk3Nqh/nwdd53a/EtFFpwYyWFC6M5ktpRvEIBmeg==", "76352acb-1653-42a9-a3ea-fb819c9d0088" });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("414b60a3-5024-4398-883c-ae475569ac49"), "Classic" },
                    { new Guid("527d987f-0d54-4359-bd39-3657729a580d"), "Select" },
                    { new Guid("ff4644a5-597d-4e9f-be43-4100406f6958"), "Recreation" }
                });
        }
    }
}
