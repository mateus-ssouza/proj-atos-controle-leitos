using ProjetoFinal.Models.Enums;
using System.ComponentModel;

namespace ProjetoFinal.Models
{
    public class Solicitacao
    {
        public int Id { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public DateTime? DataRegulacao { get; set; }
        public String Motivo { get; set; }
        public SolicitacaoPrioridade Prioridade { get; set; }
        
        [DefaultValue(0)]
        public SolicitacaoStatus Status { get; set; }

        public String NomeMedico { get; set; }
        public String NomeEnfermeiro { get; set; }
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
