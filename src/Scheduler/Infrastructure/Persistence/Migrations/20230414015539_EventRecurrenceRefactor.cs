using Microsoft.EntityFrameworkCore.Migrations;
using Scheduler.Domain.Models;

#nullable disable

namespace Scheduler.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class EventRecurrenceRefactor : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.Sql($"DELETE FROM {nameof(Event)}s");
		migrationBuilder.Sql($"DELETE FROM {nameof(Recurrence)}s");

		migrationBuilder.DropForeignKey(
			name: "FK_Recurrences_Events_Id",
			table: "Recurrences");

		migrationBuilder.DeleteData(
			table: "AspNetUserRoles",
			keyColumns: new[] { "RoleId", "UserId" },
			keyValues: new object[] { new Guid("1d01aab9-71cb-48b4-a9eb-6158654c93d8"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });

		migrationBuilder.DeleteData(
			table: "Leagues",
			keyColumn: "Id",
			keyValue: new Guid("1d7617a9-e21e-4424-aa3a-92758efa6200"));

		migrationBuilder.DeleteData(
			table: "Leagues",
			keyColumn: "Id",
			keyValue: new Guid("8b633d7c-e605-4cc0-ba94-e933f4e288a6"));

		migrationBuilder.DeleteData(
			table: "Leagues",
			keyColumn: "Id",
			keyValue: new Guid("ad38d50e-3670-416d-a5be-d9a84cea55ad"));

		migrationBuilder.DeleteData(
			table: "AspNetRoles",
			keyColumn: "Id",
			keyValue: new Guid("1d01aab9-71cb-48b4-a9eb-6158654c93d8"));

		migrationBuilder.AddColumn<Guid>(
			name: "RecurrenceId",
			table: "Events",
			type: "uniqueidentifier",
			nullable: false);

		migrationBuilder.UpdateData(
			table: "AspNetRoles",
			keyColumn: "Id",
			keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
			column: "ConcurrencyStamp",
			value: "7731b12a-3d7b-4254-85fc-a4af967faab2");

		migrationBuilder.InsertData(
			table: "AspNetRoles",
			columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
			values: new object[] { new Guid("85d4056d-1d00-46fc-af1a-2a76944f650b"), "411e4080-4736-40a7-b6d5-a04987ea84a1", "Coach", "Coach" });

		migrationBuilder.UpdateData(
			table: "AspNetUsers",
			keyColumn: "Id",
			keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
			columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
			values: new object[] { "428e8ee9-4c7e-4494-a225-d0218675e423", "AQAAAAIAAYagAAAAEDBcjzNckPAp5SFXB1003FN6ms5DAysngozzeBiUhfyw1Wite4FNrp8SEPDKIT8Gow==", "0f117bda-1ad5-444f-8329-927cd7f92a65" });

		migrationBuilder.UpdateData(
			table: "AspNetUsers",
			keyColumn: "Id",
			keyValue: new Guid("9e55284c-a2ba-425f-be26-a18e384668a7"),
			columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
			values: new object[] { "0bd94601-0a04-4cfe-9828-6eacede68271", "AQAAAAIAAYagAAAAEOrKWh8rlzDoshzbxsR8qCWU+ZS+Ob4jm4fwA9itifXTTOuMncq6WWMZna7aSPymeg==", "085953b6-b645-469a-b014-1f4a0f5e7520" });

		migrationBuilder.InsertData(
			table: "Leagues",
			columns: new[] { "Id", "Name" },
			values: new object[,]
			{
				{ new Guid("1447d54d-760c-404a-b075-1bf594ab7faf"), "Select" },
				{ new Guid("8d8b4bb2-3baa-48ca-ab90-bc52db5f91e9"), "Recreation" },
				{ new Guid("f89ca309-533c-440f-a1da-0e69f669b6a5"), "Classic" }
			});

		migrationBuilder.InsertData(
			table: "AspNetUserRoles",
			columns: new[] { "RoleId", "UserId" },
			values: new object[] { new Guid("85d4056d-1d00-46fc-af1a-2a76944f650b"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });

		migrationBuilder.CreateIndex(
			name: "IX_Events_RecurrenceId",
			table: "Events",
			column: "RecurrenceId");

		migrationBuilder.AddForeignKey(
			name: "FK_Events_Recurrences_RecurrenceId",
			table: "Events",
			column: "RecurrenceId",
			principalTable: "Recurrences",
			principalColumn: "Id",
			onDelete: ReferentialAction.Cascade);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropForeignKey(
			name: "FK_Events_Recurrences_RecurrenceId",
			table: "Events");

		migrationBuilder.DropIndex(
			name: "IX_Events_RecurrenceId",
			table: "Events");

		migrationBuilder.DeleteData(
			table: "AspNetUserRoles",
			keyColumns: new[] { "RoleId", "UserId" },
			keyValues: new object[] { new Guid("85d4056d-1d00-46fc-af1a-2a76944f650b"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });

		migrationBuilder.DeleteData(
			table: "Leagues",
			keyColumn: "Id",
			keyValue: new Guid("1447d54d-760c-404a-b075-1bf594ab7faf"));

		migrationBuilder.DeleteData(
			table: "Leagues",
			keyColumn: "Id",
			keyValue: new Guid("8d8b4bb2-3baa-48ca-ab90-bc52db5f91e9"));

		migrationBuilder.DeleteData(
			table: "Leagues",
			keyColumn: "Id",
			keyValue: new Guid("f89ca309-533c-440f-a1da-0e69f669b6a5"));

		migrationBuilder.DeleteData(
			table: "AspNetRoles",
			keyColumn: "Id",
			keyValue: new Guid("85d4056d-1d00-46fc-af1a-2a76944f650b"));

		migrationBuilder.DropColumn(
			name: "RecurrenceId",
			table: "Events");

		migrationBuilder.UpdateData(
			table: "AspNetRoles",
			keyColumn: "Id",
			keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
			column: "ConcurrencyStamp",
			value: "059420df-9f33-407b-a8c6-1aa60c544098");

		migrationBuilder.InsertData(
			table: "AspNetRoles",
			columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
			values: new object[] { new Guid("1d01aab9-71cb-48b4-a9eb-6158654c93d8"), "b17a6f46-8bfd-46bc-ad1c-2fdd6792fde1", "Coach", "Coach" });

		migrationBuilder.UpdateData(
			table: "AspNetUsers",
			keyColumn: "Id",
			keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
			columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
			values: new object[] { "1a2519a6-9554-4ced-9afc-fbac67ec8cd3", "AQAAAAIAAYagAAAAEHwKbHWuNMvq3uPogouRdlXg7lj7ax/m2eVsDyg7umWR4PGg773fMhv3bEyWeX/Vnw==", "5c0e4d2a-3916-4722-a2c5-8c3df59f741c" });

		migrationBuilder.UpdateData(
			table: "AspNetUsers",
			keyColumn: "Id",
			keyValue: new Guid("9e55284c-a2ba-425f-be26-a18e384668a7"),
			columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
			values: new object[] { "c678b59e-091c-421d-8452-f786ba8b4fbb", "AQAAAAIAAYagAAAAEHops0eMXDCD7zfw24vLLDyOlxs832KtEPgxOLFH+PkpWwHpjF0WmpapdbtIlHUNiw==", "201e46f5-1de4-4ed2-811d-8b3bab2a25a3" });

		migrationBuilder.InsertData(
			table: "Leagues",
			columns: new[] { "Id", "Name" },
			values: new object[,]
			{
				{ new Guid("1d7617a9-e21e-4424-aa3a-92758efa6200"), "Recreation" },
				{ new Guid("8b633d7c-e605-4cc0-ba94-e933f4e288a6"), "Classic" },
				{ new Guid("ad38d50e-3670-416d-a5be-d9a84cea55ad"), "Select" }
			});

		migrationBuilder.InsertData(
			table: "AspNetUserRoles",
			columns: new[] { "RoleId", "UserId" },
			values: new object[] { new Guid("1d01aab9-71cb-48b4-a9eb-6158654c93d8"), new Guid("9e55284c-a2ba-425f-be26-a18e384668a7") });

		migrationBuilder.AddForeignKey(
			name: "FK_Recurrences_Events_Id",
			table: "Recurrences",
			column: "Id",
			principalTable: "Events",
			principalColumn: "Id",
			onDelete: ReferentialAction.Cascade);
	}
}
