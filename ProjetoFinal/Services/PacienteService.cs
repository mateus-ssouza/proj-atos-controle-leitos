using ProjetoFinal.Data;
using ProjetoFinal.Models;

namespace ProjetoFinal.Services
{
    public class PacienteService
    {
        private readonly Contexto _contexto;

        public PacienteService(Contexto contexto)
        {
            _contexto = contexto;
        }

        public List<Paciente> FindAll()
        {
            return _contexto.Paciente.ToList();
        }
    }
}
