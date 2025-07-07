using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessObjects.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Tag_Fk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieTags_Tags_MovieId",
                table: "MovieTags");

            migrationBuilder.CreateIndex(
                name: "IX_MovieTags_TagId",
                table: "MovieTags",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieTags_Tags_TagId",
                table: "MovieTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieTags_Tags_TagId",
                table: "MovieTags");

            migrationBuilder.DropIndex(
                name: "IX_MovieTags_TagId",
                table: "MovieTags");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieTags_Tags_MovieId",
                table: "MovieTags",
                column: "MovieId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
