using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Scheduler.Infrastructure.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class EnableLockout : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
				columns: new[] { "LockoutEnabled" },
				values: new object[] { true });

			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: new Guid("9e55284c-a2ba-425f-be26-a18e384668a7"),
				columns: new[] { "LockoutEnabled" },
				values: new object[] { true });
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
				columns: new[] { "LockoutEnabled" },
				values: new object[] { false });

			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: new Guid("9e55284c-a2ba-425f-be26-a18e384668a7"),
				columns: new[] { "LockoutEnabled" },
				values: new object[] { false });
		}
	}
}
