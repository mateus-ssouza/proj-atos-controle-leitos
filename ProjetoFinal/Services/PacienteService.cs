using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Data;
using ProjetoFinal.Models;
using ProjetoFinal.Models.ViewModels;
using ProjetoFinal.Services.Exceptions;
using ProjetoFinal.Models.Enums;

namespace ProjetoFinal.Services
{
    public class PacienteService
    {
        private readonly Contexto _contexto;

        public PacienteService(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Paciente>> FindAllAsync()
        {
            return await _contexto.Paciente.ToListAsync();
        }

        public async Task InsertAsync(Paciente obj)
        {
            _contexto.Add(obj);
            await _contexto.SaveChangesAsync();
        }

        public async Task<Paciente> FindByIdAsync(int id)
        {
            return await _contexto.Paciente
                .Include(obj => obj.Endereco)
                .FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task<List<Paciente>> FindPacientesSemSolicitacao()
        {
            // Realiza a consulta para encontrar os pacientes sem solicitação
            var  list = await _contexto.Paciente
                // Verifica se o paciente não possui nenhuma solicitação
                .Where(p => p.Solicitacoes.Count == 0
                // Verifica se o paciente possui alguma solicitação ATIVA
                || !(p.Solicitacoes.Any(s => s.Status == SolicitacaoStatus.SOLICITADO 
                || s.Status == SolicitacaoStatus.REGULADO))) 
                .ToListAsync();

            return list;
        }


        public async Task RemoveAsync(int id)
        {
            var obj =  await _contexto.Paciente.FindAsync(id);
            _contexto.Paciente.Remove(obj);
            await _contexto.SaveChangesAsync();
        }

        public async Task UpdateAsync(Paciente obj)
        {
            bool existeAlgum = await _contexto.Paciente.AnyAsync(x => x.Id == obj.Id);

            // Verificar se não existe o Paciente no banco
            if (!existeAlgum)
            {
                throw new NotFoundException("Id não encontrado!");
            }

            try
            {
                _contexto.Update(obj);
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            } 
        }

        public void UpdateData(Paciente _old, PacienteViewModel _new)
        {
            _old.Nome = _new.Paciente.Nome;
            _old.DataNascimento = _new.Paciente.DataNascimento;
            _old.Cpf = _new.Paciente.Cpf;
            _old.Rg = _new.Paciente.Rg;
            _old.Email = _new.Paciente.Email;
            _old.Telefone = _new.Paciente.Telefone;
            _old.Endereco = _new.Endereco;
        }
    }
}
