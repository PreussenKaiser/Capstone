using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Scheduler.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TeamChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("aae1ec73-0fe6-4196-b897-aee8c74d4f0f"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("23dd3848-7963-40a6-8ea9-b2b3b6a1b1ab"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("5dbd27e7-5c77-4c97-9b41-e7b93be8455b"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("a9c84622-0218-4774-9c10-24982ead3dea"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("aae1ec73-0fe6-4196-b897-aee8c74d4f0f"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
                column: "ConcurrencyStamp",
                value: "6f261165-0758-41ee-8a16-c1cdd787e68c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("aae1ec73-0fe6-4196-b897-aee8c74d4f0f"), "2099b545-c139-4eb7-ac58-752e5225c6ab", "Coach", "Coach" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "506a5b38-d5ad-47ab-940d-0876d30c39c6", "AQAAAAIAAYagAAAAEGpTVtyOdBmp8nhhP9k6WjahGBYPpx3qudYz5GRGXC6gH8GWk8cAT4MWXYTcn4v0dg==", "8289052b-abcf-41e0-9378-c423ccbf6456" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9e55284c-a2ba-425f-be26-a18e384668a7"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aff92367-8c7f-4304-82e3-ba3108060a10", "AQAAAAIAAYagAAAAEGlaoVucn9Bz18aaa4fbcWB/H3ICK+CoHOf3bOLoLZzHmXHU8UXKrT5vs6fhxqNTbw==", "7d780c8e-70d6-4750-995d-4271a665d5a3" });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("23dd3848-7963-40a6-8ea9-b2b3b6a1b1ab"), "Select" },
                    { new Guid("5dbd27e7-5c77-4c97-9b41-e7b93be8455b"), "Classic" },
                    { new Guid("a9c84622-0218-4774-9c10-24982ead3dea"), "Recreation" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("aae1ec73-0fe6-4196-b897-aee8c74d4f0f"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });
        }
    }
}
