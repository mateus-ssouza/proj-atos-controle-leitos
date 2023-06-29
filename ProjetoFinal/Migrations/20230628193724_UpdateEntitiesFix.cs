using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoFinal.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntitiesFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacao_Leito_IdLeito",
                table: "Solicitacao");

            migrationBuilder.DropIndex(
                name: "IX_Solicitacao_IdLeito",
                table: "Solicitacao");

            migrationBuilder.AlterColumn<int>(
                name: "IdLeito",
                table: "Solicitacao",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacao_IdLeito",
                table: "Solicitacao",
                column: "IdLeito",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacao_Leito_IdLeito",
                table: "Solicitacao",
                column: "IdLeito",
                principalTable: "Leito",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.AlterColumn<int>(
                name: "IdLeito",
                table: "Solicitacao",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
    }
}
