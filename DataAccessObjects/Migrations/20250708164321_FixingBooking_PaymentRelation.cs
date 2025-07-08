using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessObjects.Migrations
{
    /// <inheritdoc />
    public partial class FixingBooking_PaymentRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Payments_PaymentId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_ShowtimeSeats_Seats_SeatId",
                table: "ShowtimeSeats");

            migrationBuilder.DropForeignKey(
                name: "FK_ShowtimeSeats_Showtimes_ShowtimeId",
                table: "ShowtimeSeats");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_PaymentId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Bookings");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                column: "BookingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowtimeSeats_Seats_SeatId",
                table: "ShowtimeSeats",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShowtimeSeats_Showtimes_ShowtimeId",
                table: "ShowtimeSeats",
                column: "ShowtimeId",
                principalTable: "Showtimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_ShowtimeSeats_Seats_SeatId",
                table: "ShowtimeSeats");

            migrationBuilder.DropForeignKey(
                name: "FK_ShowtimeSeats_Showtimes_ShowtimeId",
                table: "ShowtimeSeats");

            migrationBuilder.DropIndex(
                name: "IX_Payments_BookingId",
                table: "Payments");

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentId",
                table: "Bookings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PaymentId",
                table: "Bookings",
                column: "PaymentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Payments_PaymentId",
                table: "Bookings",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShowtimeSeats_Seats_SeatId",
                table: "ShowtimeSeats",
                column: "SeatId",
                principalTable: "Seats",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowtimeSeats_Showtimes_ShowtimeId",
                table: "ShowtimeSeats",
                column: "ShowtimeId",
                principalTable: "Showtimes",
                principalColumn: "Id");
        }
    }
}
