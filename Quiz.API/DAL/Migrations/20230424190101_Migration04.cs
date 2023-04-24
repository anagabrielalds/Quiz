using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizzes.API.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Migration04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quiz_Tema_IdTema",
                table: "Quiz");

            migrationBuilder.AddColumn<byte[]>(
                name: "Imagem",
                table: "Tema",
                type: "varbinary(max)",
                unicode: false,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_IdTema",
                table: "Quiz",
                column: "IdTema");

            migrationBuilder.AddForeignKey(
                name: "FK_Quiz_Tema_IdTema",
                table: "Quiz",
                column: "IdTema",
                principalTable: "Tema",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quiz_Tema_IdTema",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Tema");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_IdTema",
                table: "Quiz",
                column: "IdTema",
                unique: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Quiz_Tema_IdTema",
                table: "Quiz",
                column: "IdTema",
                principalTable: "Tema",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
