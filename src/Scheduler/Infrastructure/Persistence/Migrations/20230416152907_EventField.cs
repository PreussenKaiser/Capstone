using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Scheduler.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EventField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventField");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("85d4056d-1d00-46fc-af1a-2a76944f650b"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("1447d54d-760c-404a-b075-1bf594ab7faf"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("8d8b4bb2-3baa-48ca-ab90-bc52db5f91e9"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("f89ca309-533c-440f-a1da-0e69f669b6a5"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("85d4056d-1d00-46fc-af1a-2a76944f650b"));

            migrationBuilder.AlterColumn<bool>(
                name: "IsBlackout",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<Guid>(
                name: "FieldId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
                column: "ConcurrencyStamp",
                value: "61ad4ade-bd55-4c71-8c15-145a97ae2e36");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("29a00391-5b39-48dd-acb7-47b94bcded24"), "a2d79844-6c19-441b-84b4-a7e4a3232a53", "Coach", "Coach" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e7a6a03-4ad9-4d02-b4ce-ca39456a81fd", "AQAAAAIAAYagAAAAEHAOmhim5q3JKKqK9OO7Gqs706fsrzSfEfgYWKpTctZhZc1x20jNZV5rRaws4iVk5w==", "a2580011-ced7-47f3-a197-3606cc8ee6bb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e55284c-a2ba-425f-be26-a18e384668a7"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ddbb9b77-50bd-4731-808b-4c9adff4e93d", "AQAAAAIAAYagAAAAEHq1H7PPmS5PzJC4Mf0KIR/JYPJ+uDXUMI3hK2R7pbx5xwgq3OqasqBKAN4QkEQBLA==", "c67888df-0494-45c0-a165-97f34949db9e" });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4f0c0db6-bcd1-4991-b2f5-62d2429da807"), "Classic" },
                    { new Guid("76a4418f-f96e-41c3-9505-6792fd4754dd"), "Recreation" },
                    { new Guid("afb0de0a-30b9-4312-93ce-624f939fbe74"), "Select" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("29a00391-5b39-48dd-acb7-47b94bcded24"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });

            migrationBuilder.CreateIndex(
                name: "IX_Events_FieldId",
                table: "Events",
                column: "FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Fields_FieldId",
                table: "Events",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Fields_FieldId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_FieldId",
                table: "Events");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("29a00391-5b39-48dd-acb7-47b94bcded24"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("4f0c0db6-bcd1-4991-b2f5-62d2429da807"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("76a4418f-f96e-41c3-9505-6792fd4754dd"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("afb0de0a-30b9-4312-93ce-624f939fbe74"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("29a00391-5b39-48dd-acb7-47b94bcded24"));

            migrationBuilder.DropColumn(
                name: "FieldId",
                table: "Events");

            migrationBuilder.AlterColumn<bool>(
                name: "IsBlackout",
                table: "Events",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

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

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
                column: "ConcurrencyStamp",
                value: "7731b12a-3d7b-4254-85fc-a4af967faab2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("85d4056d-1d00-46fc-af1a-2a76944f650b"), "411e4080-4736-40a7-b6d5-a04987ea84a1", "Coach", "Coach" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "428e8ee9-4c7e-4494-a225-d0218675e423", "AQAAAAIAAYagAAAAEDBcjzNckPAp5SFXB1003FN6ms5DAysngozzeBiUhfyw1Wite4FNrp8SEPDKIT8Gow==", "0f117bda-1ad5-444f-8329-927cd7f92a65" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e55284c-a2ba-425f-be26-a18e384668a7"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0bd94601-0a04-4cfe-9828-6eacede68271", "AQAAAAIAAYagAAAAEOrKWh8rlzDoshzbxsR8qCWU+ZS+Ob4jm4fwA9itifXTTOuMncq6WWMZna7aSPymeg==", "085953b6-b645-469a-b014-1f4a0f5e7520" });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1447d54d-760c-404a-b075-1bf594ab7faf"), "Select" },
                    { new Guid("8d8b4bb2-3baa-48ca-ab90-bc52db5f91e9"), "Recreation" },
                    { new Guid("f89ca309-533c-440f-a1da-0e69f669b6a5"), "Classic" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("85d4056d-1d00-46fc-af1a-2a76944f650b"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });

            migrationBuilder.CreateIndex(
                name: "IX_EventField_FieldsId",
                table: "EventField",
                column: "FieldsId");
        }
    }
}
