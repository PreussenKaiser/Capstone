using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scheduler.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UserRoleChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("cfd242d3-2107-4563-b2a4-15383e683964"), "1dd4a233-39ea-4c38-9ff3-b9c09a36dad1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"), 0, "fec6665b-2b60-4804-8495-75e88c95fd32", "teamnull@gmail.com", false, "Team", "Null", false, null, null, "TEAMNULL@GMAIL.COM", "AQAAAAIAAYagAAAAEHY489KfvkJimBNsX39Sb+cA/+25BzWWHxHr5paI3PTQg9WAK+vTmxtwDlQnXTclGg==", null, false, "fced7ef7-6eed-4f47-b808-89b9619020a8", false, "teamnull@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("cfd242d3-2107-4563-b2a4-15383e683964"), new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("cfd242d3-2107-4563-b2a4-15383e683964"), new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"));

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
