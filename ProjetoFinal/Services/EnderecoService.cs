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

        public Endereco FindById(int id)
        {
            return _contexto.Endereco.FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _contexto.Endereco.Find(id);
            _contexto.Endereco.Remove(obj);
            _contexto.SaveChanges();
        }
    }
}
