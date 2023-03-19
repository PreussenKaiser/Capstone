using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scheduler.Web.Persistence.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			name: "AspNetRoles",
			columns: table => new
			{
				Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetRoles", x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "AspNetUsers",
			columns: table => new
			{
				Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				FirstName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
				LastName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
				UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
				EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
				PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
				SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
				ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
				PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
				PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
				TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
				LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
				LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
				AccessFailedCount = table.Column<int>(type: "int", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetUsers", x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "Events",
			columns: table => new
			{
				Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
				StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
				EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Events", x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "Fields",
			columns: table => new
			{
				Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Fields", x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "Leagues",
			columns: table => new
			{
				Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Leagues", x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "AspNetRoleClaims",
			columns: table => new
			{
				Id = table.Column<int>(type: "int", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
				ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
				table.ForeignKey(
					name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
					column: x => x.RoleId,
					principalTable: "AspNetRoles",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "AspNetUserClaims",
			columns: table => new
			{
				Id = table.Column<int>(type: "int", nullable: false)
					.Annotation("SqlServer:Identity", "1, 1"),
				UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
				ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
				table.ForeignKey(
					name: "FK_AspNetUserClaims_AspNetUsers_UserId",
					column: x => x.UserId,
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "AspNetUserLogins",
			columns: table => new
			{
				LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
				ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
				ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
				UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
				table.ForeignKey(
					name: "FK_AspNetUserLogins_AspNetUsers_UserId",
					column: x => x.UserId,
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "AspNetUserRoles",
			columns: table => new
			{
				UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
				table.ForeignKey(
					name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
					column: x => x.RoleId,
					principalTable: "AspNetRoles",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
				table.ForeignKey(
					name: "FK_AspNetUserRoles_AspNetUsers_UserId",
					column: x => x.UserId,
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "AspNetUserTokens",
			columns: table => new
			{
				UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
				Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
				Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
				table.ForeignKey(
					name: "FK_AspNetUserTokens_AspNetUsers_UserId",
					column: x => x.UserId,
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "Recurrences",
			columns: table => new
			{
				Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				Occurrences = table.Column<byte>(type: "tinyint", nullable: false),
				Type = table.Column<byte>(type: "tinyint", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Recurrences", x => x.Id);
				table.ForeignKey(
					name: "FK_Recurrences_Events_Id",
					column: x => x.Id,
					principalTable: "Events",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "EventField",
			columns: table => new
			{
				EventsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				FieldsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_EventField", x => new { x.EventsId, x.FieldsId });
				table.ForeignKey(
					name: "FK_EventField_Events_EventsId",
					column: x => x.EventsId,
					principalTable: "Events",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
				table.ForeignKey(
					name: "FK_EventField_Fields_FieldsId",
					column: x => x.FieldsId,
					principalTable: "Fields",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "Teams",
			columns: table => new
			{
				Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				LeagueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Teams", x => x.Id);
				table.ForeignKey(
					name: "FK_Teams_Leagues_LeagueId",
					column: x => x.LeagueId,
					principalTable: "Leagues",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "Games",
			columns: table => new
			{
				Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				HomeTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				OpposingTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Games", x => x.Id);
				table.ForeignKey(
					name: "FK_Games_Events_Id",
					column: x => x.Id,
					principalTable: "Events",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
				table.ForeignKey(
					name: "FK_Games_Teams_HomeTeamId",
					column: x => x.HomeTeamId,
					principalTable: "Teams",
					principalColumn: "Id",
					onDelete: ReferentialAction.NoAction);
				table.ForeignKey(
					name: "FK_Games_Teams_OpposingTeamId",
					column: x => x.OpposingTeamId,
					principalTable: "Teams",
					principalColumn: "Id",
					onDelete: ReferentialAction.NoAction);
			});

		migrationBuilder.CreateTable(
			name: "Practices",
			columns: table => new
			{
				Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
				TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Practices", x => x.Id);
				table.ForeignKey(
					name: "FK_Practices_Events_Id",
					column: x => x.Id,
					principalTable: "Events",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
				table.ForeignKey(
					name: "FK_Practices_Teams_TeamId",
					column: x => x.TeamId,
					principalTable: "Teams",
					principalColumn: "Id",
					onDelete: ReferentialAction.NoAction);
			});

		migrationBuilder.InsertData(
			table: "AspNetRoles",
			columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
			values: new object[] { new Guid("cfd242d3-2107-4563-b2a4-15383e683964"), "c4fe3c52-9820-45bc-9baa-5e36da9e199f", "Admin", "Admin" });

		migrationBuilder.InsertData(
			table: "AspNetUsers",
			columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
			values: new object[] { new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"), 0, "2c439680-d019-4538-a3ba-f880ee9587e8", "teamnull@gmail.com", false, "Team", "Null", false, null, null, "TEAMNULL@GMAIL.COM", "AQAAAAIAAYagAAAAEB2s9ALjq5E9z3GGuLW746mYvZrHgzh2N4olxMZAWK/TiMLlqGOFKxFUxeyQd5wuGg==", null, false, "7e49be21-b5bf-4141-975d-52acb54c90ab", false, "teamnull@gmail.com" });

		migrationBuilder.InsertData(
			table: "Leagues",
			columns: new[] { "Id", "Name" },
			values: new object[,]
			{
				{ new Guid("14c0805c-9601-4380-ae41-3f7e10349ca5"), "Classic" },
				{ new Guid("2f7092c7-25aa-4d96-aca9-2e12649ddb36"), "Recreation" },
				{ new Guid("9e75f01f-19e7-4d80-b764-f87d3701fbd7"), "Select" }
			});

		migrationBuilder.InsertData(
			table: "AspNetUserRoles",
			columns: new[] { "RoleId", "UserId" },
			values: new object[] { new Guid("cfd242d3-2107-4563-b2a4-15383e683964"), new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c") });

		migrationBuilder.CreateIndex(
			name: "IX_AspNetRoleClaims_RoleId",
			table: "AspNetRoleClaims",
			column: "RoleId");

		migrationBuilder.CreateIndex(
			name: "RoleNameIndex",
			table: "AspNetRoles",
			column: "NormalizedName",
			unique: true,
			filter: "[NormalizedName] IS NOT NULL");

		migrationBuilder.CreateIndex(
			name: "IX_AspNetUserClaims_UserId",
			table: "AspNetUserClaims",
			column: "UserId");

		migrationBuilder.CreateIndex(
			name: "IX_AspNetUserLogins_UserId",
			table: "AspNetUserLogins",
			column: "UserId");

		migrationBuilder.CreateIndex(
			name: "IX_AspNetUserRoles_RoleId",
			table: "AspNetUserRoles",
			column: "RoleId");

		migrationBuilder.CreateIndex(
			name: "EmailIndex",
			table: "AspNetUsers",
			column: "NormalizedEmail");

		migrationBuilder.CreateIndex(
			name: "UserNameIndex",
			table: "AspNetUsers",
			column: "NormalizedUserName",
			unique: true,
			filter: "[NormalizedUserName] IS NOT NULL");

		migrationBuilder.CreateIndex(
			name: "IX_EventField_FieldsId",
			table: "EventField",
			column: "FieldsId");

		migrationBuilder.CreateIndex(
			name: "IX_Games_HomeTeamId",
			table: "Games",
			column: "HomeTeamId");

		migrationBuilder.CreateIndex(
			name: "IX_Games_OpposingTeamId",
			table: "Games",
			column: "OpposingTeamId");

		migrationBuilder.CreateIndex(
			name: "IX_Practices_TeamId",
			table: "Practices",
			column: "TeamId");

		migrationBuilder.CreateIndex(
			name: "IX_Teams_LeagueId",
			table: "Teams",
			column: "LeagueId");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "AspNetRoleClaims");

		migrationBuilder.DropTable(
			name: "AspNetUserClaims");

		migrationBuilder.DropTable(
			name: "AspNetUserLogins");

		migrationBuilder.DropTable(
			name: "AspNetUserRoles");

		migrationBuilder.DropTable(
			name: "AspNetUserTokens");

		migrationBuilder.DropTable(
			name: "EventField");

		migrationBuilder.DropTable(
			name: "Games");

		migrationBuilder.DropTable(
			name: "Practices");

		migrationBuilder.DropTable(
			name: "Recurrences");

		migrationBuilder.DropTable(
			name: "AspNetRoles");

		migrationBuilder.DropTable(
			name: "AspNetUsers");

		migrationBuilder.DropTable(
			name: "Fields");

		migrationBuilder.DropTable(
			name: "Teams");

		migrationBuilder.DropTable(
			name: "Events");

		migrationBuilder.DropTable(
			name: "Leagues");
	}
}
