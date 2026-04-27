using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmDiary.API.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToFilm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Films",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Films_UserId",
                table: "Films",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Films_Users_UserId",
                table: "Films",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Films_Users_UserId",
                table: "Films");

            migrationBuilder.DropIndex(
                name: "IX_Films_UserId",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Films");
        }
    }
}
