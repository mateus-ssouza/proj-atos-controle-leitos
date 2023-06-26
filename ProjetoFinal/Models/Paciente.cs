﻿using System.ComponentModel.DataAnnotations;

namespace ProjetoFinal.Models
{
    public class Paciente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [StringLength(40, MinimumLength = 3,
            ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [Display(Name ="Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [StringLength(11, MinimumLength = 11,
            ErrorMessage = "{0} insira os {1} caracteres.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [StringLength(7, MinimumLength = 7,
            ErrorMessage = "{0} insira os {1} caracteres.")]
        public string Rg { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [EmailAddress(ErrorMessage = "Insira um email válido")]
        [StringLength(40, MinimumLength = 9, 
            ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [StringLength(11, MinimumLength = 11,
            ErrorMessage = "{0} insira os {1} caracteres.")]
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; }
        public Solicitacao Solicitacao { get; set; }

        public Paciente() { }

        public Paciente(int id, string nome, DateTime dataNascimento, string cpf, string rg, string email, string telefone, Endereco endereco, Solicitacao solicitacao)
        {
            Id = id;
            Nome = nome;
            DataNascimento = dataNascimento;
            Cpf = cpf;
            Rg = rg;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;
            Solicitacao = solicitacao;
        }
    }
}
