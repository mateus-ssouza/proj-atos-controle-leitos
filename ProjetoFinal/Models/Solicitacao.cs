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
        public String Motivo { get; set; }
        public SolicitacaoPrioridade Prioridade { get; set; }
        
        [DefaultValue(0)]
        public SolicitacaoStatus Status { get; set; }

        [Display(Name = "Médico")]
        public String NomeMedico { get; set; }

        [Display(Name = "Enfermeiro")]
        public String NomeEnfermeiro { get; set; }

        [Display(Name = "Observações")]
        public String? Observacoes { get; set; }

        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; }

        public Solicitacao() {}

        public Solicitacao(int id, DateTime dataSolicitacao, DateTime dataRegulacao, string motivo, SolicitacaoPrioridade prioridade, SolicitacaoStatus status, string nomeMedico, string nomeEnfermeiro, string observacoes, Paciente paciente)
        {
            Id = id;
            DataSolicitacao = dataSolicitacao;
            DataRegulacao = dataRegulacao;
            Motivo = motivo;
            Prioridade = prioridade;
            Status = status;
            NomeMedico = nomeMedico;
            NomeEnfermeiro = nomeEnfermeiro;
            Observacoes = observacoes;
            Paciente = paciente;
        }
    }
}
