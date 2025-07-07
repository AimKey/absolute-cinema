using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessObjects.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserEmailAndFixedReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieDirector_Directors_DirectorId",
                table: "MovieDirector");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieDirector_Movies_MovieId",
                table: "MovieDirector");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieDirector",
                table: "MovieDirector");

            migrationBuilder.RenameTable(
                name: "MovieDirector",
                newName: "MovieDirectors");

            migrationBuilder.RenameIndex(
                name: "IX_MovieDirector_MovieId",
                table: "MovieDirectors",
                newName: "IX_MovieDirectors_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieDirector_DirectorId",
                table: "MovieDirectors",
                newName: "IX_MovieDirectors_DirectorId");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsBooked",
                table: "ShowtimeSeats",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Showtimes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewDate",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieDirectors",
                table: "MovieDirectors",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieDirectors_Directors_DirectorId",
                table: "MovieDirectors",
                column: "DirectorId",
                principalTable: "Directors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieDirectors_Movies_MovieId",
                table: "MovieDirectors",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieDirectors_Directors_DirectorId",
                table: "MovieDirectors");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieDirectors_Movies_MovieId",
                table: "MovieDirectors");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieDirectors",
                table: "MovieDirectors");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsBooked",
                table: "ShowtimeSeats");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Showtimes");

            migrationBuilder.DropColumn(
                name: "ReviewDate",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reviews");

            migrationBuilder.RenameTable(
                name: "MovieDirectors",
                newName: "MovieDirector");

            migrationBuilder.RenameIndex(
                name: "IX_MovieDirectors_MovieId",
                table: "MovieDirector",
                newName: "IX_MovieDirector_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieDirectors_DirectorId",
                table: "MovieDirector",
                newName: "IX_MovieDirector_DirectorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieDirector",
                table: "MovieDirector",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieDirector_Directors_DirectorId",
                table: "MovieDirector",
                column: "DirectorId",
                principalTable: "Directors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieDirector_Movies_MovieId",
                table: "MovieDirector",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
