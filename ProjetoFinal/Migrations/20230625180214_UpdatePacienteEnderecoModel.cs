using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoFinal.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePacienteEnderecoModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Rg",
                table: "Paciente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Rua",
                table: "Endereco",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rg",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "Rua",
                table: "Endereco");
        }
    }
}
