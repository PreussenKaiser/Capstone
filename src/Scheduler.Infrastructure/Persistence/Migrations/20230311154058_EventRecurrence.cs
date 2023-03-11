using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scheduler.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class EventRecurrence : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "IsArchived",
            table: "Leagues");

        migrationBuilder.CreateTable(
            name: "Recurrences",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Occurences = table.Column<byte>(type: "tinyint", nullable: false),
                Type = table.Column<byte>(type: "tinyint", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Recurrences", x => x.Id);
                table.ForeignKey(
                    name: "FK_Recurrences_Events_Id",
                    column: x => x.Id,
                    principalTable: "Events",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.UpdateData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
            column: "ConcurrencyStamp",
            value: "f61183b3-f985-4499-9a6e-5f5e75a95e74");

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
            columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            values: new object[] { "de1c04d4-5f28-431b-b9d8-dff84f3cb228", "AQAAAAIAAYagAAAAEEwsOqseMp4odhtit3ok0WPpox0F+6wo6Ksi0xs/Ahrp5y6L/lzPu7upfMDvLtAM6A==", "51208d2d-b87b-4e77-931d-4fbe5421536e" });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Recurrences");

        migrationBuilder.AddColumn<bool>(
            name: "IsArchived",
            table: "Leagues",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.UpdateData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
            column: "ConcurrencyStamp",
            value: "f2dea1c6-5646-4e9f-b4d0-18232668c6a7");

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
            columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            values: new object[] { "1a99a38a-88ff-408e-95b3-f7744eefa4c0", "AQAAAAIAAYagAAAAEPLI7Moxj2pm1dDKShsW16uHLi+5R3FgcVfC0mXxxwIoNBxVD+bOT2U1YYWsMijUDA==", "c6ccc3ad-caf4-460d-8aeb-5ab571152e22" });
    }
}
