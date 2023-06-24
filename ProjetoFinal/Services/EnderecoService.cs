using ProjetoFinal.Data;
using ProjetoFinal.Models;

namespace ProjetoFinal.Services
{
    public class EnderecoService
    {
        private readonly Contexto _contexto;

        public EnderecoService(Contexto contexto)
        {
            _contexto = contexto;
        }

        public void Insert(Endereco obj)
        {
            _contexto.Add(obj);
            _contexto.SaveChanges();
        }
    }
}
