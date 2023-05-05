using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Scheduler.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class EventFieldCascadeDelete : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		DeleteLeagues(in migrationBuilder);

		migrationBuilder.DropForeignKey(
			name: "FK_Events_Fields_FieldId",
			table: "Events");

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

		migrationBuilder.UpdateData(
			table: "AspNetRoles",
			keyColumn: "Id",
			keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
			column: "ConcurrencyStamp",
			value: "0a320e9a-1cc2-449d-ae14-58254ab295b0");

		migrationBuilder.InsertData(
			table: "AspNetRoles",
			columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
			values: new object[] { new Guid("0b32fb4e-1c0b-4b62-8535-114023faed36"), "16b6218b-0ecf-475b-92e3-251f58675dd5", "Coach", "Coach" });

		migrationBuilder.UpdateData(
			table: "AspNetUsers",
			keyColumn: "Id",
			keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
			columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
			values: new object[] { "924f3d2e-7822-44e8-8955-837dc9086104", "AQAAAAIAAYagAAAAEM/4KNADzohdlSYaE7ZfgdFUcSXyjssHB/FshEmm+dFMWox63OYzn3i9wf6yuB4u8g==", "d1d6f9c6-fa7d-42e6-995b-6ea8b7c033d9" });

		migrationBuilder.UpdateData(
			table: "AspNetUsers",
			keyColumn: "Id",
			keyValue: new Guid("9e55284c-a2ba-425f-be26-a18e384668a7"),
			columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
			values: new object[] { "166cda6d-4795-4c74-a7a5-18b613cbcab6", "AQAAAAIAAYagAAAAEFTN6zqhWIQNqECU2FQ8WgJQ+mNRTukg/FZPSn/Yopn4TbrmxHpriDAGzD+sM6gAtA==", "ce669a38-5496-4a16-8655-4cc584e15012" });

		migrationBuilder.InsertData(
			table: "Leagues",
			columns: new[] { "Id", "Name" },
			values: new object[,]
			{
				{ new Guid("4bfcc09b-a3bd-4ef0-9d9c-13a1d460a2f1"), "Recreation" },
				{ new Guid("6a9b5dfe-6816-4c92-aa4a-3019361031c3"), "Select" },
				{ new Guid("ed07d69a-23e5-4f31-8853-51c4ff15b8e7"), "Classic" }
			});

		migrationBuilder.InsertData(
			table: "AspNetUserRoles",
			columns: new[] { "RoleId", "UserId" },
			values: new object[] { new Guid("0b32fb4e-1c0b-4b62-8535-114023faed36"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });

		migrationBuilder.AddForeignKey(
			name: "FK_Events_Fields_FieldId",
			table: "Events",
			column: "FieldId",
			principalTable: "Fields",
			principalColumn: "Id",
			onDelete: ReferentialAction.Cascade);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		DeleteLeagues(in migrationBuilder);

		migrationBuilder.DropForeignKey(
			name: "FK_Events_Fields_FieldId",
			table: "Events");

		migrationBuilder.DeleteData(
			table: "AspNetUserRoles",
			keyColumns: new[] { "RoleId", "UserId" },
			keyValues: new object[] { new Guid("0b32fb4e-1c0b-4b62-8535-114023faed36"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });

		migrationBuilder.DeleteData(
			table: "Leagues",
			keyColumn: "Id",
			keyValue: new Guid("4bfcc09b-a3bd-4ef0-9d9c-13a1d460a2f1"));

		migrationBuilder.DeleteData(
			table: "Leagues",
			keyColumn: "Id",
			keyValue: new Guid("6a9b5dfe-6816-4c92-aa4a-3019361031c3"));

		migrationBuilder.DeleteData(
			table: "Leagues",
			keyColumn: "Id",
			keyValue: new Guid("ed07d69a-23e5-4f31-8853-51c4ff15b8e7"));

		migrationBuilder.DeleteData(
			table: "AspNetRoles",
			keyColumn: "Id",
			keyValue: new Guid("0b32fb4e-1c0b-4b62-8535-114023faed36"));

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

		migrationBuilder.AddForeignKey(
			name: "FK_Events_Fields_FieldId",
			table: "Events",
			column: "FieldId",
			principalTable: "Fields",
			principalColumn: "Id");
	}

	/// <summary>
	/// Deletes all leagues and their relations.
	/// </summary>
	/// <param name="migrationBuilder">The migration to remove them from.</param>
	private static void DeleteLeagues(
		in MigrationBuilder migrationBuilder)
	{
		migrationBuilder.Sql("DELETE FROM [Practices]");
		migrationBuilder.Sql("DELETE FROM [Games]");
		migrationBuilder.Sql("DELETE FROM [Teams]");
		migrationBuilder.Sql("DELETE FROM [Leagues]");
	}
}
