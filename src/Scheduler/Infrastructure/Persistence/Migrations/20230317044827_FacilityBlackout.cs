using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scheduler.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FacilityBlackout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Events",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "isBlackout",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
                column: "ConcurrencyStamp",
                value: "9cce3822-bdb5-4f72-b47a-15602357a774");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f55ff292-0192-4850-9ec6-ddc304fb9c8d", "AQAAAAIAAYagAAAAEJerrQ13iIm3nOnum9x5AISfgB3J64hDC0PHTJvH8eGBcbSZl6HSQV7tY/03AL/yhQ==", "370d3a38-3894-4758-9169-a199bed11148" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isBlackout",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Occurrences",
                table: "Recurrences",
                newName: "Occurences");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
                column: "ConcurrencyStamp",
                value: "819efbbb-f3c8-4cb1-88b6-872319169c92");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8912d8d1-055c-4133-b68e-89837ef3ca56", "AQAAAAIAAYagAAAAECs9MXOu9rujVr8a4fZrjM0323zbnjri7X80lQQEgOgeTLXbuM09HgOlkWol63TjEQ==", "e9e4cc7f-9cdd-4eda-ba65-04013d46d9c6" });
        }
    }
}
