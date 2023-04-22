using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Scheduler.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Teams_user_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
                column: "ConcurrencyStamp",
                value: "e6cb0c09-a42c-4cc9-ae51-6f42ec7d8ff8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8155e7a2-7dae-4577-a378-e38f6c3a860c", "AQAAAAIAAYagAAAAEO/lf8ng+ZQdWZCYYwYmW0qRhR6+SD6TQv+heU6Y6PX6YZE4eiDxmqLySmJBl54wNQ==", "b4f33499-ae4f-42f0-bd07-0af82ce3780b" });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("023395d9-43a9-4d0a-be18-04daad6e699e"), "Classic" },
                    { new Guid("03afd02b-1b17-49bf-aa13-8af5e9ad707d"), "Select" },
                    { new Guid("d649fb8b-22bf-47e7-b79f-725a7d64ec86"), "Recreation" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("023395d9-43a9-4d0a-be18-04daad6e699e"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("03afd02b-1b17-49bf-aa13-8af5e9ad707d"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("d649fb8b-22bf-47e7-b79f-725a7d64ec86"));

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
    }
}
