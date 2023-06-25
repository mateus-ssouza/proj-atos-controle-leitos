using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Data;
using ProjetoFinal.Models;
using ProjetoFinal.Services.Exceptions;

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

        public void Insert(Paciente obj)
        {
            _contexto.Add(obj);
            _contexto.SaveChanges();
        }

        public Paciente FindById(int id)
        {
            return _contexto.Paciente
                .Include(obj => obj.Endereco)
                .FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _contexto.Paciente.Find(id);
            _contexto.Paciente.Remove(obj);
            _contexto.SaveChanges();
        }

        public void Update(Paciente obj)
        {
            // Verificar se não existe o Paciente no banco
            if (!_contexto.Paciente.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id não encontrado!");
            }

            try
            {
                _contexto.Update(obj);
                _contexto.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            } 
        }
    }
}
