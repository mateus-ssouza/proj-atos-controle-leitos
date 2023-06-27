using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoFinal.Migrations
{
    /// <inheritdoc />
    public partial class CreateLeitoEntityFix : Migration
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

            migrationBuilder.AlterColumn<int>(
                name: "IdSolicitacao",
                table: "Leito",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leito_Solicitacao_IdSolicitacao",
                table: "Leito");

            migrationBuilder.DropIndex(
                name: "IX_Leito_IdSolicitacao",
                table: "Leito");

            migrationBuilder.AlterColumn<int>(
                name: "IdSolicitacao",
                table: "Leito",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leito_IdSolicitacao",
                table: "Leito",
                column: "IdSolicitacao",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Leito_Solicitacao_IdSolicitacao",
                table: "Leito",
                column: "IdSolicitacao",
                principalTable: "Solicitacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
