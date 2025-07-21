using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessObjects.Migrations
{
    /// <inheritdoc />
    public partial class fixShowtimeSeat_TicketRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowtimeSeats_Tickets_TicketId",
                table: "ShowtimeSeats");

            migrationBuilder.DropIndex(
                name: "IX_ShowtimeSeats_TicketId",
                table: "ShowtimeSeats");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "ShowtimeSeats");

            migrationBuilder.AddColumn<Guid>(
                name: "ShowtimeSeatId",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ShowtimeSeatId",
                table: "Tickets",
                column: "ShowtimeSeatId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_ShowtimeSeats_ShowtimeSeatId",
                table: "Tickets",
                column: "ShowtimeSeatId",
                principalTable: "ShowtimeSeats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_ShowtimeSeats_ShowtimeSeatId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ShowtimeSeatId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ShowtimeSeatId",
                table: "Tickets");

            migrationBuilder.AddColumn<Guid>(
                name: "TicketId",
                table: "ShowtimeSeats",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShowtimeSeats_TicketId",
                table: "ShowtimeSeats",
                column: "TicketId",
                unique: true,
                filter: "[TicketId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowtimeSeats_Tickets_TicketId",
                table: "ShowtimeSeats",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id");
        }
    }
}
