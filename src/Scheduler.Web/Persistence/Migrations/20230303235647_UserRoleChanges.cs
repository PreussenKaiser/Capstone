using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scheduler.Web.Persistence.Migrations;

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
            values: new object[] { new Guid("cfd242d3-2107-4563-b2a4-15383e683964"), "f2dea1c6-5646-4e9f-b4d0-18232668c6a7", "Admin", "Admin" });

        migrationBuilder.InsertData(
            table: "AspNetUsers",
            columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
            values: new object[] { new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"), 0, "1a99a38a-88ff-408e-95b3-f7744eefa4c0", "teamnull@gmail.com", false, "Team", "Null", false, null, null, "TEAMNULL@GMAIL.COM", "AQAAAAIAAYagAAAAEPLI7Moxj2pm1dDKShsW16uHLi+5R3FgcVfC0mXxxwIoNBxVD+bOT2U1YYWsMijUDA==", null, false, "c6ccc3ad-caf4-460d-8aeb-5ab571152e22", false, "teamnull@gmail.com" });

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
