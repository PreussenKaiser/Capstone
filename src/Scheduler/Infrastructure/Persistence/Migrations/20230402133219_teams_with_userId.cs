using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Scheduler.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class teams_with_userId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Teams",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
                column: "ConcurrencyStamp",
                value: "975709f7-2596-43ed-a216-727b9a363a83");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "038ea5c6-088f-4cec-bc2e-4bf1a483184d", "AQAAAAIAAYagAAAAEFhoHggjujt+IP64uFivJFQ9ssuUinNqsNywxD2mbDV3G6r1FNAQmC1EgslAsOEO+w==", "a2f7d1a7-3269-41b9-a2cb-6206de36d148" });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4cec025d-5132-453f-ad61-8358c0653b8a"), "Recreation" },
                    { new Guid("ae21405c-0d8e-4d8f-aa1c-7ea68369255c"), "Classic" },
                    { new Guid("ccb0c5aa-6047-4a83-a7f7-dd496cd30134"), "Select" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("4cec025d-5132-453f-ad61-8358c0653b8a"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("ae21405c-0d8e-4d8f-aa1c-7ea68369255c"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("ccb0c5aa-6047-4a83-a7f7-dd496cd30134"));

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Teams");

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
    }
}
