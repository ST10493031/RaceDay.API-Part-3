using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RaceDay.API.Migrations
{
    /// <inheritdoc />
    public partial class AddBannerUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventEnrolments_Categories_CategoryID",
                table: "EventEnrolments");

            migrationBuilder.DropForeignKey(
                name: "FK_EventEnrolments_Events_EventID",
                table: "EventEnrolments");

            migrationBuilder.DropForeignKey(
                name: "FK_EventEnrolments_Participants_ParticipantID",
                table: "EventEnrolments");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_EventEnrolments_EnrolmentID",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Events_EventID",
                table: "Routes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Routes",
                table: "Routes");

            migrationBuilder.RenameTable(
                name: "Routes",
                newName: "EventRoutes");

            migrationBuilder.RenameIndex(
                name: "IX_Routes_EventID",
                table: "EventRoutes",
                newName: "IX_EventRoutes_EventID");

            migrationBuilder.AddColumn<string>(
                name: "BannerUrl",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventRoutes",
                table: "EventRoutes",
                column: "RouteID");

            migrationBuilder.AddForeignKey(
                name: "FK_EventEnrolments_Categories_CategoryID",
                table: "EventEnrolments",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventEnrolments_Events_EventID",
                table: "EventEnrolments",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "EventID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventEnrolments_Participants_ParticipantID",
                table: "EventEnrolments",
                column: "ParticipantID",
                principalTable: "Participants",
                principalColumn: "ParticipantID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventRoutes_Events_EventID",
                table: "EventRoutes",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "EventID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_EventEnrolments_EnrolmentID",
                table: "Results",
                column: "EnrolmentID",
                principalTable: "EventEnrolments",
                principalColumn: "EnrolmentID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventEnrolments_Categories_CategoryID",
                table: "EventEnrolments");

            migrationBuilder.DropForeignKey(
                name: "FK_EventEnrolments_Events_EventID",
                table: "EventEnrolments");

            migrationBuilder.DropForeignKey(
                name: "FK_EventEnrolments_Participants_ParticipantID",
                table: "EventEnrolments");

            migrationBuilder.DropForeignKey(
                name: "FK_EventRoutes_Events_EventID",
                table: "EventRoutes");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_EventEnrolments_EnrolmentID",
                table: "Results");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventRoutes",
                table: "EventRoutes");

            migrationBuilder.DropColumn(
                name: "BannerUrl",
                table: "Events");

            migrationBuilder.RenameTable(
                name: "EventRoutes",
                newName: "Routes");

            migrationBuilder.RenameIndex(
                name: "IX_EventRoutes_EventID",
                table: "Routes",
                newName: "IX_Routes_EventID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Routes",
                table: "Routes",
                column: "RouteID");

            migrationBuilder.AddForeignKey(
                name: "FK_EventEnrolments_Categories_CategoryID",
                table: "EventEnrolments",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventEnrolments_Events_EventID",
                table: "EventEnrolments",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "EventID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventEnrolments_Participants_ParticipantID",
                table: "EventEnrolments",
                column: "ParticipantID",
                principalTable: "Participants",
                principalColumn: "ParticipantID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_EventEnrolments_EnrolmentID",
                table: "Results",
                column: "EnrolmentID",
                principalTable: "EventEnrolments",
                principalColumn: "EnrolmentID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Events_EventID",
                table: "Routes",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "EventID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
