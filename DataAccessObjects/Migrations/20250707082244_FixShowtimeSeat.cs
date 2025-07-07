using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessObjects.Migrations
{
    /// <inheritdoc />
    public partial class FixShowtimeSeat : Migration
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
                name: "IsBooked",
                table: "ShowtimeSeats");

            migrationBuilder.AlterColumn<Guid>(
                name: "TicketId",
                table: "ShowtimeSeats",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowtimeSeats_Tickets_TicketId",
                table: "ShowtimeSeats");

            migrationBuilder.DropIndex(
                name: "IX_ShowtimeSeats_TicketId",
                table: "ShowtimeSeats");

            migrationBuilder.AlterColumn<Guid>(
                name: "TicketId",
                table: "ShowtimeSeats",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBooked",
                table: "ShowtimeSeats",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ShowtimeSeats_TicketId",
                table: "ShowtimeSeats",
                column: "TicketId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShowtimeSeats_Tickets_TicketId",
                table: "ShowtimeSeats",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
