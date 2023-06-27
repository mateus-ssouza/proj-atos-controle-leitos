using System.ComponentModel.DataAnnotations;

namespace ProjetoFinal.Models
{
    public class Endereco
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage="Caracteres inválidos")]
        [StringLength(2, MinimumLength = 2,
            ErrorMessage = "{0} deve ter exatamente {1} caracteres.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [StringLength(40, MinimumLength = 3,
            ErrorMessage = "{0} deve ter entre {2} e {1} caracteres")]
        public string Cidade { get; set; }


        [Required(ErrorMessage = "O campo {0} é requerido")]
        [StringLength(40, MinimumLength = 2,
            ErrorMessage = "{0} deve ter entre {2} e {1} caracteres")]
        public string Bairro { get; set; }


        [Required(ErrorMessage = "O campo {0} é requerido")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Caracteres inválidos")]
        [StringLength(8, MinimumLength = 8,
            ErrorMessage = "{0} deve ter {1} caracteres")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [StringLength(40, MinimumLength = 3,
            ErrorMessage = "{0} deve ter entre {2} e {1} caracteres")]
        public string Rua { get; set; }

        public string? Complemento { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [StringLength(8)]
        public string Numero { get; set; }

        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; }

        public Endereco() { }

        public Endereco(int id, string estado, string cidade, string bairro, string cep, string rua, string? complemento, string numero, Paciente paciente)
        {
            Id = id;
            Estado = estado;
            Cidade = cidade;
            Bairro = bairro;
            Cep = cep;
            Rua = rua;
            Complemento = complemento;
            Numero = numero;
            Paciente = paciente;
        }
    }
}
