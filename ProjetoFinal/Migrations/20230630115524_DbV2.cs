using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoFinal.Migrations
{
    /// <inheritdoc />
    public partial class DbV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Solicitacao_IdPaciente",
                table: "Solicitacao");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacao_IdPaciente",
                table: "Solicitacao",
                column: "IdPaciente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Solicitacao_IdPaciente",
                table: "Solicitacao");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacao_IdPaciente",
                table: "Solicitacao",
                column: "IdPaciente",
                unique: true);
        }
    }
}
