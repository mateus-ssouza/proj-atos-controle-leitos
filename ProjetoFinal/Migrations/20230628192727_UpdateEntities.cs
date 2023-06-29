using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoFinal.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leito_Solicitacao_IdSolicitacao",
                table: "Leito");

            migrationBuilder.DropIndex(
                name: "IX_Leito_IdSolicitacao",
                table: "Leito");

            migrationBuilder.DropColumn(
                name: "IdSolicitacao",
                table: "Leito");

            migrationBuilder.AddColumn<int>(
                name: "IdLeito",
                table: "Solicitacao",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacao_IdLeito",
                table: "Solicitacao",
                column: "IdLeito",
                unique: true,
                filter: "[IdLeito] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacao_Leito_IdLeito",
                table: "Solicitacao",
                column: "IdLeito",
                principalTable: "Leito",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacao_Leito_IdLeito",
                table: "Solicitacao");

            migrationBuilder.DropIndex(
                name: "IX_Solicitacao_IdLeito",
                table: "Solicitacao");

            migrationBuilder.DropColumn(
                name: "IdLeito",
                table: "Solicitacao");

            migrationBuilder.AddColumn<int>(
                name: "IdSolicitacao",
                table: "Leito",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leito_IdSolicitacao",
                table: "Leito",
                column: "IdSolicitacao",
                unique: true,
                filter: "[IdSolicitacao] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Leito_Solicitacao_IdSolicitacao",
                table: "Leito",
                column: "IdSolicitacao",
                principalTable: "Solicitacao",
                principalColumn: "Id");
        }
    }
}
