using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Scheduler.Infrastructure.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class GameOpposingCascade : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				table: "AspNetUserRoles",
				keyColumns: new[] { "RoleId", "UserId" },
				keyValues: new object[] { new Guid("f597997e-08b1-4a62-bb4a-a27ce4f3607b"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });

			migrationBuilder.DeleteData(
				table: "Leagues",
				keyColumn: "Id",
				keyValue: new Guid("83141cd7-6712-4be3-baac-260678955835"));

			migrationBuilder.DeleteData(
				table: "Leagues",
				keyColumn: "Id",
				keyValue: new Guid("90ea4da3-ba9d-47bb-9a1a-27193651ab57"));

			migrationBuilder.DeleteData(
				table: "Leagues",
				keyColumn: "Id",
				keyValue: new Guid("debb5f94-f677-4cb0-829e-51fbeaeee73c"));

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: new Guid("f597997e-08b1-4a62-bb4a-a27ce4f3607b"));

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

			migrationBuilder.DropForeignKey(
				table: "Games",
				name: "FK_Games_Teams_HomeTeamId");

			migrationBuilder.AddForeignKey(
				table: "Games",
				name: "FK_Games_Teams_HomeTeamId",
				column: "HomeTeamId",
				principalTable: "Teams",
				principalColumn: "Id",
				onDelete: ReferentialAction.NoAction);

			migrationBuilder.DropForeignKey(
				table: "Games",
				name: "FK_Games_Teams_OpposingTeamId");

			migrationBuilder.AddForeignKey(
				table: "Games",
				name: "FK_Games_Teams_OpposingTeamId",
				column: "OpposingTeamId",
				principalTable: "Teams",
				principalColumn: "Id",
				onDelete: ReferentialAction.NoAction);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
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
				value: "f121f11a-04cf-4377-98b1-d67b037f9ae1");

			migrationBuilder.InsertData(
				table: "AspNetRoles",
				columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
				values: new object[] { new Guid("f597997e-08b1-4a62-bb4a-a27ce4f3607b"), "834d622f-9adf-43ae-8f59-35519f726dd9", "Coach", "Coach" });

			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
				columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
				values: new object[] { "8fe462c2-3a57-40b9-bf08-33727c7ffc70", "AQAAAAIAAYagAAAAENWP0NY+P8qckzU3P1/2LMlWgOPZmiId1JcZ7zW1xCDZ7TVj43cVHzORb4iZVhoTBA==", "1390bd72-7d60-48d2-a0f4-f33b2d2afd9d" });

			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: new Guid("9e55284c-a2ba-425f-be26-a18e384668a7"),
				columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
				values: new object[] { "451c67ad-a8dd-4ef0-9bfc-0c1c4acb4882", "AQAAAAIAAYagAAAAEI9R3CDPT4bIPTVa47kFLUd9mvSt8jP4/z1db9zRKChefuyKH0/U4A31cRBwChrYkA==", "324392ce-3e89-48da-bd7b-2a91329c7af1" });

			migrationBuilder.InsertData(
				table: "Leagues",
				columns: new[] { "Id", "Name" },
				values: new object[,]
				{
					{ new Guid("83141cd7-6712-4be3-baac-260678955835"), "Classic" },
					{ new Guid("90ea4da3-ba9d-47bb-9a1a-27193651ab57"), "Recreation" },
					{ new Guid("debb5f94-f677-4cb0-829e-51fbeaeee73c"), "Select" }
				});

			migrationBuilder.InsertData(
				table: "AspNetUserRoles",
				columns: new[] { "RoleId", "UserId" },
				values: new object[] { new Guid("f597997e-08b1-4a62-bb4a-a27ce4f3607b"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });
		}
	}
}
