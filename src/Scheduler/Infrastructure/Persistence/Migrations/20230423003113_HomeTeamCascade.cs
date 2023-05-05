using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Scheduler.Infrastructure.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class HomeTeamCascade : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				table: "AspNetUserRoles",
				keyColumns: new[] { "RoleId", "UserId" },
				keyValues: new object[] { new Guid("29a91b89-fede-47d6-8de3-3745263bdcc1"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });

			migrationBuilder.DeleteData(
				table: "Leagues",
				keyColumn: "Id",
				keyValue: new Guid("2071e691-7bb7-47d3-8419-8e9b85df14b2"));

			migrationBuilder.DeleteData(
				table: "Leagues",
				keyColumn: "Id",
				keyValue: new Guid("ab2312ac-5895-4a11-b981-74b01ddb8c55"));

			migrationBuilder.DeleteData(
				table: "Leagues",
				keyColumn: "Id",
				keyValue: new Guid("b6459b97-fd75-4ee9-b65a-181772b2b858"));

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: new Guid("29a91b89-fede-47d6-8de3-3745263bdcc1"));

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

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
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

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
				column: "ConcurrencyStamp",
				value: "609030cd-97c9-4408-93b1-a620dd00bcc1");

			migrationBuilder.InsertData(
				table: "AspNetRoles",
				columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
				values: new object[] { new Guid("29a91b89-fede-47d6-8de3-3745263bdcc1"), "2aa06cdf-5523-4887-aad9-1356b81465bb", "Coach", "Coach" });

			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
				columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
				values: new object[] { "ef086a90-942d-4fe7-b888-9da356011ba4", "AQAAAAIAAYagAAAAEEz0BnuZDGXJqba1ni7itbDMQkMrsPzS7eUXRl1AHjdy4iTdKdHSoTePdPPHmqxqJw==", "e168074b-508f-4109-a0ab-81ebf76505b7" });

			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: new Guid("9e55284c-a2ba-425f-be26-a18e384668a7"),
				columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
				values: new object[] { "186f5d56-bce4-4ff1-8a21-5054aaa6759f", "AQAAAAIAAYagAAAAEN7iwgrnomzDHzS8kYr52BaTdAKGq4uaRIuBggHti3vsYffRWX8qcRsMGzLZvX0Idw==", "37114001-93d6-4313-ad9c-b9188fa501df" });

			migrationBuilder.InsertData(
				table: "Leagues",
				columns: new[] { "Id", "Name" },
				values: new object[,]
				{
					{ new Guid("2071e691-7bb7-47d3-8419-8e9b85df14b2"), "Recreation" },
					{ new Guid("ab2312ac-5895-4a11-b981-74b01ddb8c55"), "Classic" },
					{ new Guid("b6459b97-fd75-4ee9-b65a-181772b2b858"), "Select" }
				});

			migrationBuilder.InsertData(
				table: "AspNetUserRoles",
				columns: new[] { "RoleId", "UserId" },
				values: new object[] { new Guid("29a91b89-fede-47d6-8de3-3745263bdcc1"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });
		}
	}
}
