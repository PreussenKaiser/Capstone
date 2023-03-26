using Microsoft.EntityFrameworkCore.Migrations;

namespace Scheduler.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class EventFieldRelationship : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Events_Fields_FieldId",
            table: "Events");

        migrationBuilder.DropIndex(
            name: "IX_Events_FieldId",
            table: "Events");

        migrationBuilder.DropColumn(
            name: "FieldId",
            table: "Events");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Teams",
            type: "nvarchar(32)",
            maxLength: 32,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Fields",
            type: "nvarchar(32)",
            maxLength: 32,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Events",
            type: "nvarchar(32)",
            maxLength: 32,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "AspNetUserTokens",
            type: "nvarchar(450)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(128)",
            oldMaxLength: 128);

        migrationBuilder.AlterColumn<string>(
            name: "LoginProvider",
            table: "AspNetUserTokens",
            type: "nvarchar(450)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(128)",
            oldMaxLength: 128);

        migrationBuilder.AlterColumn<string>(
            name: "ProviderKey",
            table: "AspNetUserLogins",
            type: "nvarchar(450)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(128)",
            oldMaxLength: 128);

        migrationBuilder.AlterColumn<string>(
            name: "LoginProvider",
            table: "AspNetUserLogins",
            type: "nvarchar(450)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(128)",
            oldMaxLength: 128);

        migrationBuilder.CreateTable(
            name: "EventField",
            columns: table => new
            {
                EventsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                FieldsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EventField", x => new { x.EventsId, x.FieldsId });
                table.ForeignKey(
                    name: "FK_EventField_Events_EventsId",
                    column: x => x.EventsId,
                    principalTable: "Events",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_EventField_Fields_FieldsId",
                    column: x => x.FieldsId,
                    principalTable: "Fields",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_EventField_FieldsId",
            table: "EventField",
            column: "FieldsId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "EventField");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Teams",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(32)",
            oldMaxLength: 32);

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Fields",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(32)",
            oldMaxLength: 32);

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Events",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(32)",
            oldMaxLength: 32);

        migrationBuilder.AddColumn<Guid>(
            name: "FieldId",
            table: "Events",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "AspNetUserTokens",
            type: "nvarchar(128)",
            maxLength: 128,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(450)");

        migrationBuilder.AlterColumn<string>(
            name: "LoginProvider",
            table: "AspNetUserTokens",
            type: "nvarchar(128)",
            maxLength: 128,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(450)");

        migrationBuilder.AlterColumn<string>(
            name: "ProviderKey",
            table: "AspNetUserLogins",
            type: "nvarchar(128)",
            maxLength: 128,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(450)");

        migrationBuilder.AlterColumn<string>(
            name: "LoginProvider",
            table: "AspNetUserLogins",
            type: "nvarchar(128)",
            maxLength: 128,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(450)");

        migrationBuilder.CreateIndex(
            name: "IX_Events_FieldId",
            table: "Events",
            column: "FieldId");

        migrationBuilder.AddForeignKey(
            name: "FK_Events_Fields_FieldId",
            table: "Events",
            column: "FieldId",
            principalTable: "Fields",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
