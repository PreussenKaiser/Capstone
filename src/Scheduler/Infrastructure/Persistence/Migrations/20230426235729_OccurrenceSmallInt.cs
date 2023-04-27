using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Scheduler.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class OccurrenceSmallInt : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "AspNetUserRoles",
            keyColumns: new[] { "RoleId", "UserId" },
            keyValues: new object[] { new Guid("f8ffe243-4320-484e-bbe2-079eccafb482"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });

        migrationBuilder.DeleteData(
            table: "Leagues",
            keyColumn: "Id",
            keyValue: new Guid("0e2879db-775c-47aa-8dc5-1596a1ae3b75"));

        migrationBuilder.DeleteData(
            table: "Leagues",
            keyColumn: "Id",
            keyValue: new Guid("c4a56d82-7d63-4a83-94f4-b11413d0f63a"));

        migrationBuilder.DeleteData(
            table: "Leagues",
            keyColumn: "Id",
            keyValue: new Guid("f8d4ad4a-5ef2-4a61-93c7-39f2f416b0c6"));

        migrationBuilder.DeleteData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: new Guid("f8ffe243-4320-484e-bbe2-079eccafb482"));

        migrationBuilder.AlterColumn<short>(
            name: "Occurrences",
            table: "Recurrences",
            type: "SMALLINT",
            nullable: false,
            oldClrType: typeof(byte),
            oldType: "tinyint");

        migrationBuilder.UpdateData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
            column: "ConcurrencyStamp",
            value: "79e3767c-2873-4b59-9f4c-fa1299866a85");

        migrationBuilder.InsertData(
            table: "AspNetRoles",
            columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            values: new object[] { new Guid("69fa87d3-38e3-4d95-b6ad-d12aabff7410"), "41d7e168-533e-4534-ba6e-6da8c417f2cd", "Coach", "Coach" });

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
            columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            values: new object[] { "4afda647-73d6-40e4-ae93-9ba2909d56f9", "AQAAAAIAAYagAAAAEKq5WEg5WRj1E9JEp0MzBI/IEqev51U1z/6OiJ7slkIf+zPEqxGPevAGxpiBR/t3qQ==", "9abb3b36-a5a0-494a-a4fa-4e272c8eea4d" });

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("9e55284c-a2ba-425f-be26-a18e384668a7"),
            columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            values: new object[] { "c59082e1-bb26-4a69-b0e9-3ff7d8a5621e", "AQAAAAIAAYagAAAAEB4aZq7kdMFCyCR0x/aGckHX+sE3NVGMT/0obDj4RNgDsRCh6eFQKehdLMDV/WueFw==", "4ef8abae-3d7d-4712-b43e-81ded9949fb0" });

        migrationBuilder.InsertData(
            table: "Leagues",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { new Guid("15aee6cc-7db8-4e87-b30c-8d9f476476fa"), "Classic" },
                { new Guid("2156e19d-5a58-41c3-bdf7-3fc22a3c9197"), "Select" },
                { new Guid("d4a95ec7-a99c-4b07-8e2f-404ad2e1638d"), "Recreation" }
            });

        migrationBuilder.InsertData(
            table: "AspNetUserRoles",
            columns: new[] { "RoleId", "UserId" },
            values: new object[] { new Guid("69fa87d3-38e3-4d95-b6ad-d12aabff7410"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "AspNetUserRoles",
            keyColumns: new[] { "RoleId", "UserId" },
            keyValues: new object[] { new Guid("69fa87d3-38e3-4d95-b6ad-d12aabff7410"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });

        migrationBuilder.DeleteData(
            table: "Leagues",
            keyColumn: "Id",
            keyValue: new Guid("15aee6cc-7db8-4e87-b30c-8d9f476476fa"));

        migrationBuilder.DeleteData(
            table: "Leagues",
            keyColumn: "Id",
            keyValue: new Guid("2156e19d-5a58-41c3-bdf7-3fc22a3c9197"));

        migrationBuilder.DeleteData(
            table: "Leagues",
            keyColumn: "Id",
            keyValue: new Guid("d4a95ec7-a99c-4b07-8e2f-404ad2e1638d"));

        migrationBuilder.DeleteData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: new Guid("69fa87d3-38e3-4d95-b6ad-d12aabff7410"));

        migrationBuilder.AlterColumn<byte>(
            name: "Occurrences",
            table: "Recurrences",
            type: "tinyint",
            nullable: false,
            oldClrType: typeof(short),
            oldType: "SMALLINT");

        migrationBuilder.UpdateData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
            column: "ConcurrencyStamp",
            value: "59558c5e-9e07-44a9-8817-02044c9caaa5");

        migrationBuilder.InsertData(
            table: "AspNetRoles",
            columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            values: new object[] { new Guid("f8ffe243-4320-484e-bbe2-079eccafb482"), "a29d43e7-8db8-41ba-874d-97ca52a725a5", "Coach", "Coach" });

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
            columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            values: new object[] { "d269bd76-ee32-405a-9e4d-6ca9b3d45bd3", "AQAAAAIAAYagAAAAEKpFaSbuCKLqkLayR5P+qTm9R8jgM02ftFr/QHYM/EOGAD+OP4odRuAVrTLFtBU3BA==", "4701da5a-0c8b-42ee-a432-1eafc1688c47" });

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("9e55284c-a2ba-425f-be26-a18e384668a7"),
            columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            values: new object[] { "54ce7a24-bf4f-4438-9f8f-a48252cfed9c", "AQAAAAIAAYagAAAAEIPs7PxPP7P3W7wMcDio64gkc+CyNewq9iY/fR/S+6zycQS7nRRENTza14hnPNuKNQ==", "f458d00e-4953-4cb6-9694-4655f6ad722e" });

        migrationBuilder.InsertData(
            table: "Leagues",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { new Guid("0e2879db-775c-47aa-8dc5-1596a1ae3b75"), "Select" },
                { new Guid("c4a56d82-7d63-4a83-94f4-b11413d0f63a"), "Classic" },
                { new Guid("f8d4ad4a-5ef2-4a61-93c7-39f2f416b0c6"), "Recreation" }
            });

        migrationBuilder.InsertData(
            table: "AspNetUserRoles",
            columns: new[] { "RoleId", "UserId" },
            values: new object[] { new Guid("f8ffe243-4320-484e-bbe2-079eccafb482"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });
    }
}
