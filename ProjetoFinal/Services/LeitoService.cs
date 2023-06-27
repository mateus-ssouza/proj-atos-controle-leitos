using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Data;
using ProjetoFinal.Models;

namespace ProjetoFinal.Services
{
    public class LeitoService
    {
        private readonly Contexto _contexto;

        public LeitoService(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Leito>> FindAllAsync()
        {
            return await _contexto.Leito
                .Include(obj => obj.Solicitacao) 
                .ToListAsync();
        }
    }
}
