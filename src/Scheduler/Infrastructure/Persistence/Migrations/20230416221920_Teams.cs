using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Scheduler.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Teams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("5477fa94-b14a-4ebe-8a94-7e7eb4dcac35"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("023395d9-43a9-4d0a-be18-04daad6e699e"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("03afd02b-1b17-49bf-aa13-8af5e9ad707d"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("d649fb8b-22bf-47e7-b79f-725a7d64ec86"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5477fa94-b14a-4ebe-8a94-7e7eb4dcac35"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                value: "e6cb0c09-a42c-4cc9-ae51-6f42ec7d8ff8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("5477fa94-b14a-4ebe-8a94-7e7eb4dcac35"), "0b9b0b7c-ac4d-40da-905c-101485767649", "Coach", "Coach" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8155e7a2-7dae-4577-a378-e38f6c3a860c", "AQAAAAIAAYagAAAAEO/lf8ng+ZQdWZCYYwYmW0qRhR6+SD6TQv+heU6Y6PX6YZE4eiDxmqLySmJBl54wNQ==", "b4f33499-ae4f-42f0-bd07-0af82ce3780b" });

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
                    { new Guid("023395d9-43a9-4d0a-be18-04daad6e699e"), "Classic" },
                    { new Guid("03afd02b-1b17-49bf-aa13-8af5e9ad707d"), "Select" },
                    { new Guid("d649fb8b-22bf-47e7-b79f-725a7d64ec86"), "Recreation" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("5477fa94-b14a-4ebe-8a94-7e7eb4dcac35"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });
        }
    }
}
