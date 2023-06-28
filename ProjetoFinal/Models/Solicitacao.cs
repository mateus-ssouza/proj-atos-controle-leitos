using ProjetoFinal.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjetoFinal.Models
{
    public class Solicitacao
    {
        public int Id { get; set; }

        [Display(Name = "Data da Solicitação")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataSolicitacao { get; set; }

        [Display(Name = "Data da Regulação")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataRegulacao { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [StringLength(60, MinimumLength = 3,
            ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.")]
        public String Motivo { get; set; }

        [Required(ErrorMessage = "{0} é requerida")]
        public SolicitacaoPrioridade Prioridade { get; set; }
        
        [Required(ErrorMessage = "{0} é requerido")]
        [Display(Name = "Tipo do Leito")]
        public TipoLeito TipoLeito { get; set; }

        [DefaultValue(0)]
        public SolicitacaoStatus Status { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [StringLength(40, MinimumLength = 3,
            ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.")]
        [Display(Name = "Médico")]
        public String NomeMedico { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [StringLength(40, MinimumLength = 3,
            ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.")]
        [Display(Name = "Enfermeiro")]
        public String NomeEnfermeiro { get; set; }

        [Display(Name = "Observações")]
        [StringLength(maximumLength:300)]
        public String? Observacoes { get; set; }

        [Display(Name = "Paciente")]
        [Required(ErrorMessage = "{0} é requerido")]
        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; }

        public Leito Leito { get; set; }

        public Solicitacao() {}

        public Solicitacao(int id, DateTime dataSolicitacao, DateTime? dataRegulacao, string motivo, SolicitacaoPrioridade prioridade, TipoLeito tipoLeito, SolicitacaoStatus status, string nomeMedico, string nomeEnfermeiro, string? observacoes, Paciente paciente, Leito leito)
        {
            Id = id;
            DataSolicitacao = dataSolicitacao;
            DataRegulacao = dataRegulacao;
            Motivo = motivo;
            Prioridade = prioridade;
            TipoLeito = tipoLeito;
            Status = status;
            NomeMedico = nomeMedico;
            NomeEnfermeiro = nomeEnfermeiro;
            Observacoes = observacoes;
            Paciente = paciente;
            Leito = leito;
        }
    }
}
