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
            return await _contexto.Solicitacao.ToListAsync();
        }

        public async Task InsertAsync(Solicitacao obj)
        {
            _contexto.Add(obj);
            await _contexto.SaveChangesAsync();
        }
    }
}
