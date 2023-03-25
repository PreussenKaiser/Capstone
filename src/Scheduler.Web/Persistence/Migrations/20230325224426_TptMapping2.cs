using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Scheduler.Web.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TptMapping2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_Events_HomeTeamId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_OpposingTeamId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_TeamId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "HomeTeamId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "IsRecurring",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "OpposingTeamId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "isBlackout",
                table: "Events");

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
                value: "efbdbe20-46c3-4104-84c1-6e3757855b7f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "49eb8f6a-431e-4807-8621-d45e5de5b967", "AQAAAAIAAYagAAAAECLdKEG0ANtUiB9rwzbLKW1zED4UPw/AbpbkWVSj6HABX6NcWa+VSsJ39sgnnzsHiQ==", "806d602c-de7c-4bb0-b63d-566f1f671493" });

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("9817a7d5-3a22-468d-8f8d-e22772cae71b"), "Select" },
                    { new Guid("f67bb085-44df-45e4-b666-748292b9daac"), "Classic" },
                    { new Guid("faacfd92-7b48-41e5-86e9-c77da2a54a42"), "Recreation" }
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Practices");

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("9817a7d5-3a22-468d-8f8d-e22772cae71b"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("f67bb085-44df-45e4-b666-748292b9daac"));

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: new Guid("faacfd92-7b48-41e5-86e9-c77da2a54a42"));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Events",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "HomeTeamId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRecurring",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.AddColumn<bool>(
                name: "isBlackout",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cfd242d3-2107-4563-b2a4-15383e683964"),
                column: "ConcurrencyStamp",
                value: "9cce3822-bdb5-4f72-b47a-15602357a774");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7eb05375-f2a2-4323-8371-8f81efba9a9c"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f55ff292-0192-4850-9ec6-ddc304fb9c8d", "AQAAAAIAAYagAAAAEJerrQ13iIm3nOnum9x5AISfgB3J64hDC0PHTJvH8eGBcbSZl6HSQV7tY/03AL/yhQ==", "370d3a38-3894-4758-9169-a199bed11148" });

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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Teams_OpposingTeamId",
                table: "Events",
                column: "OpposingTeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Teams_TeamId",
                table: "Events",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
