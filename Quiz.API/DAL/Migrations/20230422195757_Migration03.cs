using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizzes.API.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Migration03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quiz_Tema_IdTema",
                table: "Quiz");

            migrationBuilder.AddForeignKey(
                name: "FK_Quiz_Tema_IdTema",
                table: "Quiz",
                column: "IdTema",
                principalTable: "Tema",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quiz_Tema_IdTema",
                table: "Quiz");

            migrationBuilder.AddForeignKey(
                name: "FK_Quiz_Tema_IdTema",
                table: "Quiz",
                column: "IdTema",
                principalTable: "Tema",
                principalColumn: "Id");
        }
    }
}
