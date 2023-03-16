using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scheduler.Web.Persistence.Migrations;

/// <inheritdoc />
public partial class RecurrenceMappingChanges : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "Occurences",
            table: "Recurrences",
            newName: "Occurrences");

        migrationBuilder.AlterColumn<string>(
            name: "Discriminator",
            table: "Events",
            type: "nvarchar(8)",
            maxLength: 8,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.UpdateData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
            column: "ConcurrencyStamp",
            value: "b18d5b59-091e-4147-824f-ee6c73649cb1");

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
            columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            values: new object[] { "6df4a04f-92f6-4113-be1e-9cb5bc900e29", "AQAAAAIAAYagAAAAEG4TNZ+bSJOZe9fZmemmjbO4bFVmXYkca9RR9WOaWpXtJUd+HwGT4bfbQ0KcJxhOlQ==", "c6f32d01-b6cc-4144-86a6-bd8acab5a2d6" });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
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
