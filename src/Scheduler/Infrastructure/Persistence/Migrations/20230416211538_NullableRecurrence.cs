using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Scheduler.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NullableRecurrence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Recurrences_RecurrenceId",
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

            migrationBuilder.AlterColumn<Guid>(
                name: "RecurrenceId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
                column: "ConcurrencyStamp",
                value: "a5f34cc9-40d1-4e95-953d-1c5f7422c55e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("5477fa94-b14a-4ebe-8a94-7e7eb4dcac35"), "0b9b0b7c-ac4d-40da-905c-101485767649", "Coach", "Coach" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4afdadfa-95ce-4af4-a62d-e52acbb65daa", "AQAAAAIAAYagAAAAENPELOiYmmf38T3Hw8G59Vtd34RlAMa5OYaJur16zRZuoyy1DvLg7g5pudE1u+bMrA==", "b5fd1ec7-27a3-429a-b6f3-5c5fbae48796" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e55284c-a2ba-425f-be26-a18e384668a7"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b68dedbb-38d3-4b0a-b028-a278639a6f23", "AQAAAAIAAYagAAAAEAQKayR09QDSeTNAKkDqilosa3uTS8Hts2WHZ5Wf0Db0Gb3MDCfocGY+KP+jSiZy9g==", "ab89e5a9-e0a0-4efe-99be-b0e71a704a03" });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("911af1f1-77c3-4d25-87a8-3b8c8b3fba51"), "Recreation" },
                    { new Guid("b3ddb645-6c43-4747-aaab-d5828ab261b1"), "Select" },
                    { new Guid("b91b5d90-24c0-4ea0-a380-02b85a026f24"), "Classic" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("5477fa94-b14a-4ebe-8a94-7e7eb4dcac35"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Recurrences_RecurrenceId",
                table: "Events",
                column: "RecurrenceId",
                principalTable: "Recurrences",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Recurrences_RecurrenceId",
                table: "Events");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("5477fa94-b14a-4ebe-8a94-7e7eb4dcac35"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("911af1f1-77c3-4d25-87a8-3b8c8b3fba51"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("b3ddb645-6c43-4747-aaab-d5828ab261b1"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("b91b5d90-24c0-4ea0-a380-02b85a026f24"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5477fa94-b14a-4ebe-8a94-7e7eb4dcac35"));

            migrationBuilder.AlterColumn<Guid>(
                name: "RecurrenceId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Recurrences_RecurrenceId",
                table: "Events",
                column: "RecurrenceId",
                principalTable: "Recurrences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
