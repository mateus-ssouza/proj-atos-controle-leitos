using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Data;
using ProjetoFinal.Models;

namespace ProjetoFinal.Services
{
    public class SolicitacaoService
    {
        private readonly Contexto _contexto;

        public SolicitacaoService(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Solicitacao>> FindAllAsync()
        {
            return await _contexto.Solicitacao
                .Include(obj => obj.Paciente)
                .ToListAsync();
        }

        public async Task InsertAsync(Solicitacao obj)
        {
            obj.DataSolicitacao = DateTime.Now;
            _contexto.Add(obj);
            await _contexto.SaveChangesAsync();
        }

        public async Task<Solicitacao> FindByIdAsync(int id)
        {
            return await _contexto.Solicitacao
                .Include(obj => obj.Paciente)
                .FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _contexto.Solicitacao.FindAsync(id);
            _contexto.Solicitacao.Remove(obj);
            await _contexto.SaveChangesAsync();
        }
    }
}
