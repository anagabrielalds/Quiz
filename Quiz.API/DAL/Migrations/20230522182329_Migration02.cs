using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizzes.API.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Migration02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Respostas_Perguntas_PerguntasId",
                table: "Respostas");

            migrationBuilder.DropIndex(
                name: "IX_Respostas_PerguntasId",
                table: "Respostas");

            migrationBuilder.DropColumn(
                name: "PerguntasId",
                table: "Respostas");

            migrationBuilder.CreateIndex(
                name: "IX_Respostas_IdPergunta",
                table: "Respostas",
                column: "IdPergunta");

            migrationBuilder.AddForeignKey(
                name: "FK_Respostas_Perguntas_IdPergunta",
                table: "Respostas",
                column: "IdPergunta",
                principalTable: "Perguntas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Respostas_Perguntas_IdPergunta",
                table: "Respostas");

            migrationBuilder.DropIndex(
                name: "IX_Respostas_IdPergunta",
                table: "Respostas");

            migrationBuilder.AddColumn<int>(
                name: "PerguntasId",
                table: "Respostas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Respostas_PerguntasId",
                table: "Respostas",
                column: "PerguntasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Respostas_Perguntas_PerguntasId",
                table: "Respostas",
                column: "PerguntasId",
                principalTable: "Perguntas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
