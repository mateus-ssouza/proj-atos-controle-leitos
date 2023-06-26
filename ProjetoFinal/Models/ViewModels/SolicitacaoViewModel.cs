namespace ProjetoFinal.Models.ViewModels
{
    public class SolicitacaoViewModel
    {
        public Solicitacao Solicitacao { get; set; }
        public ICollection<Paciente> Pacientes { get; set; }
    }
}
