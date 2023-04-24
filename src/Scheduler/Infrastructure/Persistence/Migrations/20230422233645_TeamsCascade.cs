using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Scheduler.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class TeamsCascade : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DeleteData(
			table: "AspNetUserRoles",
			keyColumns: new[] { "RoleId", "UserId" },
			keyValues: new object[] { new Guid("7685375a-74b9-4c92-a2a3-e3ee364ab066"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });

		migrationBuilder.DeleteData(
			table: "AspNetRoles",
			keyColumn: "Id",
			keyValue: new Guid("7685375a-74b9-4c92-a2a3-e3ee364ab066"));

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

		migrationBuilder.CreateIndex(
			name: "IX_Teams_UserId",
			table: "Teams",
			column: "UserId");

		migrationBuilder.AddForeignKey(
			name: "FK_Teams_AspNetUsers_UserId",
			table: "Teams",
			column: "UserId",
			principalTable: "AspNetUsers",
			principalColumn: "Id");

		migrationBuilder.DropForeignKey(
			table: "Games",
			name: "FK_Games_Teams_HomeTeamId");

		migrationBuilder.DropForeignKey(
			table: "Games",
			name: "FK_Games_Teams_OpposingTeamId");

		migrationBuilder.DropForeignKey(
			table: "Practices",
			name: "FK_Practices_Teams_TeamId");

		migrationBuilder.AddForeignKey(
			table: "Games",
			name: "FK_Games_Teams_HomeTeamId",
			column: "HomeTeamId",
			principalTable: "Teams",
			principalColumn: "Id",
			onDelete: ReferentialAction.Cascade);

		migrationBuilder.AddForeignKey(
			table: "Games",
			name: "FK_Games_Teams_OpposingTeamId",
			column: "OpposingTeamId",
			principalTable: "Teams",
			principalColumn: "Id",
			onDelete: ReferentialAction.Cascade);

		migrationBuilder.AddForeignKey(
			table: "Practices",
			name: "FK_Practices_Teams_TeamId",
			column: "TeamId",
			principalTable: "Teams",
			principalColumn: "Id",
			onDelete: ReferentialAction.Cascade);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.Sql("DELETE FROM Leagues");

		migrationBuilder.DropForeignKey(
			name: "FK_Teams_AspNetUsers_UserId",
			table: "Teams");

		migrationBuilder.DropIndex(
			name: "IX_Teams_UserId",
			table: "Teams");

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
			value: "961fc0a3-016f-41fc-ae8f-7dfd55aa2245");

		migrationBuilder.InsertData(
			table: "AspNetRoles",
			columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
			values: new object[] { new Guid("7685375a-74b9-4c92-a2a3-e3ee364ab066"), "c60a47c9-0a99-49c6-b0cb-0120188b8438", "Coach", "Coach" });

		migrationBuilder.UpdateData(
			table: "AspNetUsers",
			keyColumn: "Id",
			keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
			columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
			values: new object[] { "17cc4356-8b37-4a0c-874b-e2c1785ba5a9", "AQAAAAIAAYagAAAAEGtIiB/hQiVf0ax4MRTXqpwymgo9GnlIsp1KLXui7/LAz+WUxIIgkRmwvWfow6POJg==", "8d1e3e88-5d9a-4a83-b8c6-9b7985df5d80" });

		migrationBuilder.UpdateData(
			table: "AspNetUsers",
			keyColumn: "Id",
			keyValue: new Guid("9e55284c-a2ba-425f-be26-a18e384668a7"),
			columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
			values: new object[] { "4124d63e-14da-4848-9b31-ffd9ecd8afd0", "AQAAAAIAAYagAAAAELQpRYKRB3eivvphq6gvyBoJySePY+gWdt9HBGlSR2aDKOvvXPnRcvGYzVtYY7Ojcg==", "89104cd9-ee98-483f-8dc1-952060c11985" });

		migrationBuilder.InsertData(
			table: "Leagues",
			columns: new[] { "Id", "Name" },
			values: new object[,]
			{
				{ new Guid("7f76481f-15f9-434c-ad7d-626a0e03a14a"), "Select" },
				{ new Guid("9d364cc2-2e03-4d95-852f-c9858b536037"), "Recreation" },
				{ new Guid("a628f0ae-b85f-4a69-b52e-f4a6c0cd2b2c"), "Classic" }
			});

		migrationBuilder.InsertData(
			table: "AspNetUserRoles",
			columns: new[] { "RoleId", "UserId" },
			values: new object[] { new Guid("7685375a-74b9-4c92-a2a3-e3ee364ab066"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });
	}
}
