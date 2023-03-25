using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scheduler.Web.Persistence.Migrations;

/// <inheritdoc />
public partial class TphMapping : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Events_AspNetUsers_UserId",
            table: "Events");

        migrationBuilder.DropForeignKey(
            name: "FK_Recurrences_Events_Id",
            table: "Recurrences");

        migrationBuilder.DropTable(
            name: "Games");

        migrationBuilder.DropTable(
            name: "Practices");

        migrationBuilder.DropIndex(
            name: "IX_Events_UserId",
            table: "Events");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Recurrences",
            table: "Recurrences");

        migrationBuilder.AddColumn<string>(
            name: "Discriminator",
            table: "Events",
            type: "nvarchar(8)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<Guid>(
            name: "HomeTeamId",
            table: "Events",
            type: "uniqueidentifier",
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "OpposingTeamId",
            table: "Events",
            type: "uniqueidentifier",
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "TeamId",
            table: "Events",
            type: "uniqueidentifier",
            nullable: true);

        migrationBuilder.AddPrimaryKey(
            name: "PK_Recurrences",
            table: "Recurrences",
            column: "Id");

        migrationBuilder.UpdateData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
            column: "ConcurrencyStamp",
            value: "819efbbb-f3c8-4cb1-88b6-872319169c92");

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
            columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            values: new object[] { "8912d8d1-055c-4133-b68e-89837ef3ca56", "AQAAAAIAAYagAAAAECs9MXOu9rujVr8a4fZrjM0323zbnjri7X80lQQEgOgeTLXbuM09HgOlkWol63TjEQ==", "e9e4cc7f-9cdd-4eda-ba65-04013d46d9c6" });

        migrationBuilder.CreateIndex(
            name: "IX_Events_HomeTeamId",
            table: "Events",
            column: "HomeTeamId");

        migrationBuilder.CreateIndex(
            name: "IX_Events_OpposingTeamId",
            table: "Events",
            column: "OpposingTeamId");

        migrationBuilder.CreateIndex(
            name: "IX_Events_TeamId",
            table: "Events",
            column: "TeamId");

        migrationBuilder.AddForeignKey(
            name: "FK_Events_Teams_HomeTeamId",
            table: "Events",
            column: "HomeTeamId",
            principalTable: "Teams",
            principalColumn: "Id",
            onDelete: ReferentialAction.NoAction);

        migrationBuilder.AddForeignKey(
            name: "FK_Events_Teams_OpposingTeamId",
            table: "Events",
            column: "OpposingTeamId",
            principalTable: "Teams",
            principalColumn: "Id",
            onDelete: ReferentialAction.NoAction);

        migrationBuilder.AddForeignKey(
            name: "FK_Events_Teams_TeamId",
            table: "Events",
            column: "TeamId",
            principalTable: "Teams",
            principalColumn: "Id",
            onDelete: ReferentialAction.NoAction);

        migrationBuilder.AddForeignKey(
            name: "FK_Recurrences_Events_Id",
            table: "Recurrences",
            column: "Id",
            principalTable: "Events",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Events_Teams_HomeTeamId",
            table: "Events");

        migrationBuilder.DropForeignKey(
            name: "FK_Events_Teams_OpposingTeamId",
            table: "Events");

        migrationBuilder.DropForeignKey(
            name: "FK_Events_Teams_TeamId",
            table: "Events");

        migrationBuilder.DropForeignKey(
            name: "FK_Recurrences_Events_Id",
            table: "Recurrences");

        migrationBuilder.DropIndex(
            name: "IX_Events_HomeTeamId",
            table: "Events");

        migrationBuilder.DropIndex(
            name: "IX_Events_OpposingTeamId",
            table: "Events");

        migrationBuilder.DropIndex(
            name: "IX_Events_TeamId",
            table: "Events");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Recurrences",
            table: "Recurrences");

        migrationBuilder.DropColumn(
            name: "Discriminator",
            table: "Events");

        migrationBuilder.DropColumn(
            name: "HomeTeamId",
            table: "Events");

        migrationBuilder.DropColumn(
            name: "OpposingTeamId",
            table: "Events");

        migrationBuilder.DropColumn(
            name: "TeamId",
            table: "Events");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Recurrences",
            table: "Recurrences",
            column: "Id");

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
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Games_Teams_OpposingTeamId",
                    column: x => x.OpposingTeamId,
                    principalTable: "Teams",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
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
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.UpdateData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
            column: "ConcurrencyStamp",
            value: "f61183b3-f985-4499-9a6e-5f5e75a95e74");

        migrationBuilder.UpdateData(
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
            columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
            values: new object[] { "de1c04d4-5f28-431b-b9d8-dff84f3cb228", "AQAAAAIAAYagAAAAEEwsOqseMp4odhtit3ok0WPpox0F+6wo6Ksi0xs/Ahrp5y6L/lzPu7upfMDvLtAM6A==", "51208d2d-b87b-4e77-931d-4fbe5421536e" });

        migrationBuilder.CreateIndex(
            name: "IX_Events_UserId",
            table: "Events",
            column: "UserId");

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

        migrationBuilder.AddForeignKey(
            name: "FK_Events_AspNetUsers_UserId",
            table: "Events",
            column: "UserId",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Recurrences_Events_Id",
            table: "Recurrences",
            column: "Id",
            principalTable: "Events",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
