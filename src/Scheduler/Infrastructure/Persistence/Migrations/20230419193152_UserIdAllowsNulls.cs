using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Scheduler.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UserIdAllowsNulls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("e9bdb5d1-98e5-4932-9554-d5c5c580581f"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("3a3ba68f-f80c-4c84-abce-cdff53f565eb"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("c70fd2b1-c46c-4fae-9385-f0f52f48febc"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("cf352abf-d72d-4d79-8633-65c00272c496"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e9bdb5d1-98e5-4932-9554-d5c5c580581f"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Teams",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
                column: "ConcurrencyStamp",
                value: "3551b23f-55c9-464f-a651-689afe98f573");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("1bb1e9bf-c41e-428b-9b9b-f8941be4de12"), "f858750a-1aa9-428b-a959-635fdca88810", "Coach", "Coach" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "56adea76-624a-4130-8290-8c25ea3c30cd", "AQAAAAIAAYagAAAAEFxJMjcChkgMKMpnPTMhkfCfov4Y/xI14CtX1FnP3f6s5ns8vnLasS3Vmu0R6oLyWw==", "e4416792-9763-445e-a48b-8d35217eea97" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e55284c-a2ba-425f-be26-a18e384668a7"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78cec94f-ae31-480f-88be-72ca61aa3834", "AQAAAAIAAYagAAAAEHhQgiCllp/pn7eS9YiufQIt7fbHMhm59LR4j8Tefi7jiZNXpYF9B/NWJsCuCEMr0Q==", "16fe593e-c61c-446d-9789-2681bd16d4ac" });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3ae67273-61d2-4e24-b6a6-a614729871a7"), "Recreation" },
                    { new Guid("800e198f-d209-46fe-a29b-0ccd3525e430"), "Select" },
                    { new Guid("ac454bbb-a298-4b99-a4db-21450225cce0"), "Classic" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("1bb1e9bf-c41e-428b-9b9b-f8941be4de12"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("1bb1e9bf-c41e-428b-9b9b-f8941be4de12"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("3ae67273-61d2-4e24-b6a6-a614729871a7"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("800e198f-d209-46fe-a29b-0ccd3525e430"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("ac454bbb-a298-4b99-a4db-21450225cce0"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1bb1e9bf-c41e-428b-9b9b-f8941be4de12"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Teams",
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
                value: "32928678-8775-4d86-95bc-87d711ca213c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("e9bdb5d1-98e5-4932-9554-d5c5c580581f"), "6f60ea27-2798-4708-85b3-7022f359d241", "Coach", "Coach" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f9c789c-d3d8-40f9-8deb-0b32becefecb", "AQAAAAIAAYagAAAAEMAWMuwUqcA+WZqMuxYZRqGOGp4pBLYn1Ufr22pGFUVxO3ua/XMSL+8V+XE22jIRWg==", "8b5c2f82-d768-4fd0-b9b5-a050f32fbdf7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e55284c-a2ba-425f-be26-a18e384668a7"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4db3d395-d665-468e-b1e1-119c2f478cab", "AQAAAAIAAYagAAAAEImrA3oyP6spBVKNChcsS8bHyqZyQTSMBpbSEiod+d4UsKJY3wNkUtK0uhBxqLrFdg==", "c35f2088-0e4e-47b8-b941-6fd33d69980c" });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3a3ba68f-f80c-4c84-abce-cdff53f565eb"), "Recreation" },
                    { new Guid("c70fd2b1-c46c-4fae-9385-f0f52f48febc"), "Classic" },
                    { new Guid("cf352abf-d72d-4d79-8633-65c00272c496"), "Select" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("e9bdb5d1-98e5-4932-9554-d5c5c580581f"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });
        }
    }
}
