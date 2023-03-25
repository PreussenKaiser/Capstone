using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scheduler.Web.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TptMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Events_EventId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Practices_Events_EventId",
                table: "Practices");

            migrationBuilder.DropIndex(
                name: "IX_Practices_EventId",
                table: "Practices");

            migrationBuilder.DropIndex(
                name: "IX_Games_EventId",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Practices",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Games",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Practices",
                table: "Practices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Events_Id",
                table: "Games",
                column: "Id",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Practices_Events_Id",
                table: "Practices",
                column: "Id",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Events_Id",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Practices_Events_Id",
                table: "Practices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Practices",
                table: "Practices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Practices",
                newName: "EventId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Games",
                newName: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Practices_EventId",
                table: "Practices",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_EventId",
                table: "Games",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Events_EventId",
                table: "Games",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Practices_Events_EventId",
                table: "Practices",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
